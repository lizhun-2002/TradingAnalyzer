using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Model;

namespace TradingAnalyzer.BLL
{
    class PortfolioGenerater
    {
        StockDayPriceManager stockDayPriceManager = new StockDayPriceManager();
        CheckListManager checkListManager = new CheckListManager();
        PortfolioManager portfolioManager = new PortfolioManager();
        AccountStatusManager accountStatusManager = new AccountStatusManager();

        //初始入金的日期和金额。如果checklist中有第一次入金记录，则initialCapital=0;initialDate=this.checkListManager.GetMinDate().AddDays(-1)
        DateTime initialDate = new DateTime(2015, 2, 3);
        double initialCapital = 57943.87;

        /// <summary>
        /// 根据checkList和stockDayPrice数据，生成portfolio数据（保存到数据库）
        /// </summary>
        /// <returns>执行情况</returns>
        public string PGenerater()
        {
            DateTime maxDateOfCheckList, maxDateOfPortfolio;
            if (checkListManager.GetCount() == 0)
            {
                return "There is no checklist.";
            }
            else
            {
                maxDateOfCheckList = this.checkListManager.GetMaxDate();
            }

            if (portfolioManager.GetCount() == 0)
            {
                //第一次计算时根据初始入金的日期和金额创建第一个portfolio
                Portfolio initialPortfolio = new Portfolio();
                initialPortfolio.Date = this.initialDate;
                initialPortfolio.Code = "cash";
                initialPortfolio.Name = "现金";
                initialPortfolio.Position = 1;
                initialPortfolio.ClosePrice = this.initialCapital;
                initialPortfolio.BeginDate = this.initialDate;
                initialPortfolio.CostPrice = this.initialCapital;
                this.portfolioManager.AddNew(initialPortfolio);
                maxDateOfPortfolio = this.initialDate;
            }
            else
            {
                maxDateOfPortfolio = this.portfolioManager.GetMaxDate();
            }

            if (DateTime.Compare(maxDateOfPortfolio, maxDateOfCheckList) < 0)
            {
                for (DateTime dt = maxDateOfPortfolio.AddDays(1); dt <= maxDateOfCheckList; dt = dt.AddDays(1))
                {
                    //周末不生成portfolio
                    if (!TradingAnalyzer.Common.DateRules.IsWeekend(dt))
                    {
                        PGeneraterByDate(dt);
                    }
                }
                return string.Format("OK! Portfolios from {0} to {1} are added.", maxDateOfPortfolio.AddDays(1).ToShortDateString(), maxDateOfCheckList.ToShortDateString());
            }
            else
            {
                return string.Format("Failed! maxDateOfPortfolio is {0}, maxDateOfCheckList is {1}.", maxDateOfPortfolio.ToShortDateString(), maxDateOfCheckList.ToShortDateString());
            }

        }

        //注意：当date日的portfolio存在时，该函数什么也不做
        public void PGeneraterByDate(DateTime date)
        {
            DateTime yesterday = this.portfolioManager.GetMaxDate();
            if (date <= yesterday)
            {
                return;
            }
            List<Portfolio> oldPortfolios = this.portfolioManager.GetByDate(yesterday).ToList();//count>0，至少含cash
            IEnumerable<CheckList> newCheckLists = this.checkListManager.GetByDate(date);//count可能为0,
            bool done = false;
            
            //有交割单则逐条处理交割单
            if (newCheckLists.Count() != 0)
            {
                
                foreach (var checkList in newCheckLists)
                {
                    if (checkList.TradeName == "银行转证券" || checkList.TradeName == "证券转银行")
                    {
                        foreach (var portfolio in oldPortfolios)
                        {
                            if (portfolio.Code == "cash")
                            {
                                portfolio.ClosePrice += checkList.SettlementAmount;
                                portfolio.CostPrice += checkList.SettlementAmount;
                                done = true;
                            }
                        }
                    }
                    else if (checkList.TradeName.Trim() == "利息归本")
                    {
                        foreach (var portfolio in oldPortfolios)
                        {
                            if (portfolio.Code == "cash")
                            {
                                portfolio.ClosePrice += checkList.SettlementAmount;
                                done = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (var portfolio in oldPortfolios)
                        {
                            if (portfolio.Code == "cash")
                            {
                                portfolio.ClosePrice += checkList.SettlementAmount;
                            }
                            if (portfolio.Code == checkList.Code)
                            {
                                if ((portfolio.Position + checkList.TradeDirection * checkList.TradeVolume) == 0)
                                {
                                    portfolio.CostPrice = 0;
                                }
                                else
                                {
                                    portfolio.CostPrice = (portfolio.CostPrice * portfolio.Position + checkList.SettlementAmount * (-1)) / (portfolio.Position + checkList.TradeDirection * checkList.TradeVolume);
                                }
                                portfolio.Position += (checkList.TradeDirection * checkList.TradeVolume);
                                //portfolio.ClosePrice = this.stockDayPriceManager.GetByDateCode(date, portfolio.Code).Close;
                                done = true;
                            }
                        }
                    }

                    //将没有处理的交割单（新建仓的证券）加入portfolio
                    if (done == false)
                    {
                        Portfolio newPortfolio = new Portfolio();
                        newPortfolio.Date = date;
                        newPortfolio.Code = checkList.Code;
                        if (checkList.TradeName.Trim() == "新股申购")
                        {
                            newPortfolio.Name = checkList.Name + "(新股申购)";
                        }
                        else
                        { 
                            newPortfolio.Name = checkList.Name; 
                        }
                        newPortfolio.Position = (checkList.TradeDirection * checkList.TradeVolume);
                        newPortfolio.BeginDate = date;
                        newPortfolio.CostPrice = checkList.SettlementAmount / checkList.TradeVolume * (-1);
                        newPortfolio.ClosePrice = newPortfolio.CostPrice;//暂时取CostPrice，后面会统一更新//this.stockDayPriceManager.GetByDateCode(date, checkList.Code).Close;
                        oldPortfolios.Add(newPortfolio);
                    }
                    else
                    {
                        done = false;
                    }
                }
            }

            //更新portfolio的日期和收盘价，然后将portfolio中股数非0的portfolio数据上传数据库
            foreach (var portfolio in oldPortfolios)
            {
                if (portfolio.Position != 0)
                {
                    portfolio.Date = date;
                    if (portfolio.Code != "cash" && portfolio.Name.Substring(portfolio.Name.Length - Math.Min(portfolio.Name.Length,6)) != "(新股申购)")
                    {
                        StockDayPrice dayPrice = this.stockDayPriceManager.GetByDateCode(date, portfolio.Code);
                        if (dayPrice != null)
                        {
                            portfolio.ClosePrice = dayPrice.Close;
                        }
                        else
                        {
                            //如果数据库中找不到收盘价，则自动下载更新股票数据，并重新查找收盘价
                            this.stockDayPriceManager.AutoUpdateByCodeName(portfolio.Code, portfolio.Name, date);
                            dayPrice = this.stockDayPriceManager.GetByDateCode(date, portfolio.Code);
                            if (dayPrice != null)
                            {
                                portfolio.ClosePrice = dayPrice.Close;
                            }
                            else //如果仍然找不到收盘价，说明更新数据失败。删除date日的数据（cash数据），跳出循环，不更新portfolio数据。
                            {
                                this.portfolioManager.DeleteByDate(date);
                                return;
                            }
                        }
                    }
                    this.portfolioManager.AddNew(portfolio);
                }
            }
        }



    }
}
