using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Model
{
    class AccountStatus
    {
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private double availableMoney;

        public double AvailableMoney
        {
            get { return availableMoney; }
            set { availableMoney = value; }
        }

        private double marketValue;

        public double MarketValue
        {
            get { return marketValue; }
            set { marketValue = value; }
        }

        private double totalAsset;

        public double TotalAsset
        {
            get { return totalAsset; }
            set { totalAsset = value; }
        }

        private double change;

        public double Change
        {
            get { return change; }
            set { change = value; }
        }

        private double totalCost;

        public double TotalCost
        {
            get { return totalCost; }
            set { totalCost = value; }
        }

        //尝试:AccountStatus包含一个List<Portfolio>类型成员
        //private List<Portfolio> portfolios;

        //internal List<Portfolio> Portfolios
        //{
        //    get { return portfolios; }
        //    set { portfolios = value; }
        //}
    }
}
