using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Model;

namespace TradingAnalyzer.BLL
{
    class ChartDataManager
    {
        private StockDayPriceManager stockDayPriceManager = new StockDayPriceManager();
        private PortfolioManager portfolioManager = new PortfolioManager();
        private AccountStatusManager accountStatusManager = new AccountStatusManager();

        /// <summary>
        /// 根据股票代码返回交易轨迹K线图的数据
        /// </summary>
        /// <param name="code">股票代码</param>
        /// <param name="checkListData">checkListData列表</param>
        /// <param name="stockData">stockData列表</param>
        /// <param name="dateMapToInt">日期到整数映射的字典</param>
        public void GetDataForCandelWithTradingTraceChart(string code, out List<CheckList> checkListData, out List<StockDayPrice> stockData, out Dictionary<DateTime, int> dateMapToInt)
        {
            #region 判断是否缺少数据，缺少则下载数据
            checkListData = new CheckListManager().GetByCode(code).ToList<CheckList>();
            List<StockDayPrice> stockDataAll = this.stockDayPriceManager.GetByCode(code).ToList<StockDayPrice>();
            
            //对于没有交易过的股票
            if (checkListData.Count == 0)
            {
                checkListData = new List<CheckList>();
                stockData = new List<StockDayPrice>();
                dateMapToInt=new Dictionary<DateTime,int>();
                return;
            }

            //图形默认显示建仓前5天到清仓后5天的数据
            DateTime showBeginDate = checkListData[0].Date.AddDays(-20);
            DateTime showEndDate = checkListData[checkListData.Count - 1].Date.AddDays(20) >= DateTime.Today ? DateTime.Today.AddDays(-1) : checkListData[checkListData.Count - 1].Date.AddDays(20);

            //持仓期间两侧5天缺少数据时自动更新数据,更新范围是持仓期间向两侧各更加10天数据
            DateTime updateBeginDate = checkListData[0].Date.AddDays(-30);
            DateTime updateEndDate = checkListData[checkListData.Count - 1].Date.AddDays(30);

            if (stockDataAll.Count==0 || showBeginDate < stockDataAll[0].Date || stockDataAll[stockDataAll.Count - 1].Date < showEndDate)
            {
                this.stockDayPriceManager.AutoUpdateByCodeName(code, checkListData[0].Name, updateBeginDate, updateEndDate);
            }
             
            #endregion

            #region 截取一段数据（建仓前5天到清仓后5天）返回
            stockDataAll = this.stockDayPriceManager.GetByCode(code).ToList<StockDayPrice>();
            stockData = new List<StockDayPrice>();

            //定义一个日期到整数映射的字典，用整数替代日期作为K线横轴。避免交易休市造成的空隙。
            dateMapToInt = new Dictionary<DateTime, int>();

            int i = 1;//作为K线的x轴数值，因为用date做x轴会显示周末。
            foreach (StockDayPrice stockDayPrice in stockDataAll)
            {
                //去除停牌日k线
                //if (stockDayPrice.High == stockDayPrice.Low && stockDayPrice.High == stockDayPrice.Open && stockDayPrice.High == stockDayPrice.Close && stockDayPrice.Volume == 0)
                //{
                //    continue;
                //}
                //给字典增加键值对
                if (stockDayPrice.Date < showBeginDate || stockDayPrice.Date > showEndDate)
                {
                    continue;
                }
                stockData.Add(stockDayPrice);
                dateMapToInt.Add(stockDayPrice.Date, i);
                i++;
            } 
            #endregion
        }

        /// <summary>
        /// 计算基金净值图的数据，包括基金单位净值和总份额
        /// </summary>
        /// <param name="dateNum"></param>
        /// <param name="dateUnitNetWorth"></param>
        /// <param name="dateTotalShare"></param>
        public void GetDataForFundUnitNetWorthChart(out Dictionary<DateTime,int> dateNum, out Dictionary<DateTime,double> dateUnitNetWorth, out Dictionary<DateTime,double> dateTotalShare)
        {
            dateNum = new Dictionary<DateTime, int>();
            dateUnitNetWorth = new Dictionary<DateTime, double>();
            dateTotalShare = new Dictionary<DateTime, double>();
            List<AccountStatus> accountStatusData = new AccountStatusManager().GetAll().ToList<AccountStatus>();
            //fund share基金总份额
            double totalShare = 0;
            double unitNetWorth = 0;
            for (int i = 0; i < accountStatusData.Count; i++)
            {
                if(i==0)
                {
                    dateNum.Add(accountStatusData[i].Date, i);
                    dateUnitNetWorth.Add(accountStatusData[i].Date, 1);
                    dateTotalShare.Add(accountStatusData[i].Date, 0);
                    continue;
                }
                totalShare = dateTotalShare[accountStatusData[i - 1].Date] + (accountStatusData[i].TotalCost - accountStatusData[i-1].TotalCost)/dateUnitNetWorth[accountStatusData[i-1].Date];  //昨天的总份额+今天新增资金/昨天的单位净值
                
                unitNetWorth = accountStatusData[i].TotalAsset / totalShare;  //总资产/总份额
                dateNum.Add(accountStatusData[i].Date, i);
                dateTotalShare.Add(accountStatusData[i].Date, totalShare);
                dateUnitNetWorth.Add(accountStatusData[i].Date, unitNetWorth);
            }
        }
    }
}
