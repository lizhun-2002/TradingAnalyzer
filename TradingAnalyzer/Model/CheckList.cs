using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Model
{
    class CheckList
    {
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        //交易名称：证券卖出、证券买入、银证转账等
        private string tradeName;

        public string TradeName
        {
            get { return tradeName; }
            set { tradeName = value; }
        }

        //买入为1，卖出为-1，其他为0
        private int tradeDirection;

        public int TradeDirection
        {
            get { return tradeDirection; }
            set { tradeDirection = value; }
        }
        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private double tradePrice;

        public double TradePrice
        {
            get { return tradePrice; }
            set { tradePrice = value; }
        }
        private int tradeVolume;

        public int TradeVolume
        {
            get { return tradeVolume; }
            set { tradeVolume = value; }
        }
        private int remainingVolume;

        public int RemainingVolume
        {
            get { return remainingVolume; }
            set { remainingVolume = value; }
        }
        private double tradeAmount;

        public double TradeAmount
        {
            get { return tradeAmount; }
            set { tradeAmount = value; }
        }
        private double settlementAmount;

        public double SettlementAmount
        {
            get { return settlementAmount; }
            set { settlementAmount = value; }
        }
        private double remainingAmount;

        public double RemainingAmount
        {
            get { return remainingAmount; }
            set { remainingAmount = value; }
        }
        private double commission;

        public double Commission
        {
            get { return commission; }
            set { commission = value; }
        }
        private double tax;

        public double Tax
        {
            get { return tax; }
            set { tax = value; }
        }
        private double fee;

        public double Fee
        {
            get { return fee; }
            set { fee = value; }
        }

        private string tradeCode;

        public string TradeCode
        {
            get { return tradeCode; }
            set { tradeCode = value; }
        }
    }
}
