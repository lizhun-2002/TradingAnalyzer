using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Common;
using TradingAnalyzer.Model;

namespace TradingAnalyzer.BLL
{
    class AccountStatusGenerater
    {
        PortfolioManager portfolioManager = new PortfolioManager();
        AccountStatusManager accountStatusManager = new AccountStatusManager();

        /// <summary>
        /// 根据portfolio数据，生成AccountStatus数据（保存到数据库）
        /// </summary>
        /// <returns>执行情况</returns>
        public string AGenerater()
        {
            DateTime maxDateOfPortfolio, minDateOfPortfolio, maxDateOfAccountStatus;

            if (this.portfolioManager.GetCount() == 0)
            {
                return "There is no portfolio.";
            }
            else
            {
                maxDateOfPortfolio = this.portfolioManager.GetMaxDate();
                minDateOfPortfolio = this.portfolioManager.GetMinDate();
            }

            if (this.accountStatusManager.GetCount() == 0)
            {
                //第一次计算时创建第一个AccountStatus
                AccountStatus initialAccountStatus = new AccountStatus();
                initialAccountStatus.Date = minDateOfPortfolio.AddDays(-1);
                initialAccountStatus.AvailableMoney = 0;
                initialAccountStatus.MarketValue = 0;
                initialAccountStatus.TotalAsset = 0;
                initialAccountStatus.Change = 0;
                initialAccountStatus.TotalCost = 0;
                this.accountStatusManager.AddNew(initialAccountStatus);
                maxDateOfAccountStatus = minDateOfPortfolio.AddDays(-1);
            }
            else
            {
                maxDateOfAccountStatus = this.accountStatusManager.GetMaxDate();
            }

            if (DateTime.Compare(maxDateOfAccountStatus, maxDateOfPortfolio) < 0)
            {
                for (DateTime dt = maxDateOfAccountStatus.AddDays(1); dt <= maxDateOfPortfolio; dt = dt.AddDays(1))
                {
                    //周末不生成AccountStatus
                    if (!TradingAnalyzer.Common.DateRules.IsWeekend(dt))
                    {
                        AGeneraterByDate(dt);
                    }
                }
                return string.Format("OK! AccountStatus from {0} to {1} are added.", maxDateOfAccountStatus.AddDays(1).ToShortDateString(), maxDateOfPortfolio.ToShortDateString());
            }
            else
            {
                return string.Format("Failed! maxDateOfAccountStatus is {0}, maxDateOfPortfolio is {1}.", maxDateOfAccountStatus.ToShortDateString(), maxDateOfPortfolio.ToShortDateString());
            }

        }

        //注意：当date日的accountStatus存在时，该函数什么也不做
        public void AGeneraterByDate(DateTime date)
        {
            DateTime yesterday=this.accountStatusManager.GetMaxDate();
            if(date<=yesterday)
            {
                return;
            }
            AccountStatus oldAccountStatus = this.accountStatusManager.GetByDate(yesterday);
            AccountStatus newAccountStatus = new AccountStatus();
            newAccountStatus.Date=date;
            newAccountStatus.MarketValue=0;
            List<Portfolio> portfolios = this.portfolioManager.GetByDate(date).ToList();//count>0，至少含cash
            if (portfolios.Count == 0)
            {
                return;
            }
            else
            {
                foreach (var portfolio in portfolios)
                {
                    if (portfolio.Code == "cash")
                    {
                        newAccountStatus.AvailableMoney = portfolio.ClosePrice;
                        newAccountStatus.TotalCost = portfolio.CostPrice;
                    }
                    else
                    {
                        newAccountStatus.MarketValue += portfolio.Position * portfolio.ClosePrice;
                    }
                }
                newAccountStatus.TotalAsset = newAccountStatus.AvailableMoney + newAccountStatus.MarketValue;
                newAccountStatus.Change = newAccountStatus.TotalAsset - oldAccountStatus.TotalAsset;
                this.accountStatusManager.AddNew(newAccountStatus);
            }
        }

    }
}
