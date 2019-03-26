using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Model
{
    //现金作为portfolio的一部分。code=cash，name=现金，position=1，closePrice=现金金额，costPrice=总入金金额；
    class Portfolio
    {
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
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

        private int position;

        public int Position
        {
            get { return position; }
            set { position = value; }
        }

        private double closePrice;

        public double ClosePrice
        {
            get { return closePrice; }
            set { closePrice = value; }
        }

        private DateTime beginDate;

        public DateTime BeginDate
        {
            get { return beginDate; }
            set { beginDate = value; }
        }

        private double costPrice;

        public double CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; }
        }
    }
}
