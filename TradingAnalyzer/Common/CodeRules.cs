using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Common
{
    class CodeRules
    {
        //适用于yahoo网站
        public static string CodeWithMarket(string code)
        {
            string firstLetter = code.Substring(0, 1);
            switch(firstLetter)
            {
                case "6":
                case "5":
                    return code + ".ss";
                case "I":
                    return code + ".cfe";
                case "0":
                case "3":
                case "1":
                    return code + ".sz";
                default:
                    return code;
            }
        }
    }
}
