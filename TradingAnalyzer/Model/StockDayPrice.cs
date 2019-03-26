using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Model
{
    class StockDayPrice
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double AdjClose { get; set; }

        public StockDayPrice()
        {
            Code = "";
            Name = "";
            Date = new DateTime(1900, 1, 1);
            Open = 0;
            High = 0;
            Low = 0;
            Close = 0;
            Volume = 0;
            AdjClose = 0;

        }
    }
}
