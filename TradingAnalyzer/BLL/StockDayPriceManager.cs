using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Common;
using TradingAnalyzer.DAL;
using TradingAnalyzer.Model;

namespace TradingAnalyzer.BLL
{
    class StockDayPriceManager
    {
        private StockDayPriceService stockService = new StockDayPriceService();

        public int AddNew(StockDayPrice stock)
        {
            if (this.CheckExists(stock.Date, stock.Code))
            {
                return 0;
            }
            else
            {
                return stockService.AddNew(stock);
            }
            
        }

        public int DeleteByDateCode(DateTime date, string code)
        {
            return stockService.DeleteByDateCode(date, code);
        }

        public int DeleteAll()
        {
            return stockService.DeleteAll();
        }

        public int Update(StockDayPrice stock)
        {
            return stockService.Update(stock);
        }

        public IEnumerable<StockDayPrice> GetAll()
        {
            return stockService.GetAll();
        }

        public IEnumerable<StockDayPrice> GetByCode(string code)
        {
            return stockService.GetByCode(code);
        }

        public StockDayPrice GetByDateCode(DateTime date, string code)
        {
            return stockService.GetByDateCode(date, code);
        }

        public bool CheckExists(DateTime date, string code)
        {
            StockDayPrice stock = stockService.GetByDateCode(date, code);
            if (stock == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 将xlsx文件导入数据库
        /// </summary>
        /// <param name="xlsxPath">xlsx文件路径</param>
        /// <returns>受影响行数</returns>
        public int ImportFromXlsx(string xlsxPath)
        {
            using (FileStream stream = new FileStream(xlsxPath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);
                int insertedLinesCount = 0;
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    StockDayPrice stock1 = new StockDayPrice();
                    stock1.Code = row.GetCell(0).StringCellValue;
                    stock1.Name = "Unknown";
                    stock1.Date = row.GetCell(1).DateCellValue;
                    stock1.Open = row.GetCell(2).NumericCellValue;
                    stock1.High = row.GetCell(3).NumericCellValue;
                    stock1.Low = row.GetCell(4).NumericCellValue;
                    stock1.Close = row.GetCell(5).NumericCellValue;
                    stock1.Volume = row.GetCell(6).NumericCellValue;
                    stock1.AdjClose = row.GetCell(7).NumericCellValue;
                    int j = this.AddNew(stock1);
                    insertedLinesCount+=j;
                }
                return insertedLinesCount;
            }
        }

        /// <summary>
        /// 将csv文件导入数据库
        /// </summary>
        /// <param name="code">证券代码</param>
        /// <param name="name">证券名称</param>
        /// <param name="csvPath">csv文件路径</param>
        /// <returns>受影响行数</returns>
        public int ImportFromCSV(string code, string name, string csvPath)
        {
            DataTable dt = TXTUtility.ReadTxtSplitByRegStr(csvPath,1,@",",null);
            if (dt == null)
            {
                return 0;
            }
            else
            {
                int insertedLinesCount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    StockDayPrice stock1 = new StockDayPrice();
                    stock1.Code = code;
                    stock1.Name = name;
                    stock1.Date = Convert.ToDateTime(row["Date"]);
                    stock1.Open = Convert.ToDouble(row["Open"]);
                    stock1.High = Convert.ToDouble(row["High"]);
                    stock1.Low = Convert.ToDouble(row["Low"]);
                    stock1.Close = Convert.ToDouble(row["Close"]);
                    stock1.Volume = Convert.ToDouble(row["Volume"]);
                    stock1.AdjClose = Convert.ToDouble(row["Adj Close"]);
                    int j = this.AddNew(stock1);
                    insertedLinesCount += j;
                }
                return insertedLinesCount;
            }
        }

        /// <summary>
        /// 将csv文件导入数据库(不推荐使用)
        /// </summary>
        /// <param name="code">证券代码</param>
        /// <param name="name">证券名称</param>
        /// <param name="csvPath">csv文件路径</param>
        /// <returns>受影响行数</returns>
        public int ImportFromCSVAsDB(string code, string name, string csvPath)
        {
            DataTable dt = CSVUtility.ReadCSVAsDB(csvPath);
            if (dt == null)
            {
                return 0;
            }
            else
            {
                int insertedLinesCount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    StockDayPrice stock1 = new StockDayPrice();
                    stock1.Code = code;
                    stock1.Name = name;
                    stock1.Date = (DateTime)row["Date"];
                    stock1.Open = (double)row["Open"];
                    stock1.High = (double)row["High"];
                    stock1.Low = (double)row["Low"];
                    stock1.Close = (double)row["Close"];
                    stock1.Volume = Convert.ToDouble(row["Volume"]);//(double)会报错Specified cast is not valid.奇怪！！！超过int范围时row["Volume"]为DBNull
                    stock1.AdjClose = (double)row["Adj Close"];
                    int j = this.AddNew(stock1);
                    insertedLinesCount += j;
                }
                return insertedLinesCount;
            }
        }

        /// <summary>
        /// 自动下载股票数据(全部历史数据)并导入数据库
        /// </summary>
        /// <param name="code">股票代码</param>
        /// <param name="name">股票名称</param>
        /// <returns>导入行数</returns>
        public int AutoUpdateByCodeName(string code, string name)
        {
            string newCode = TradingAnalyzer.Common.CodeRules.CodeWithMarket(code);
            using (WebClient wc = new WebClient())
            {
                Uri uri = new Uri("http://ichart.finance.yahoo.com/table.csv?s="+newCode, UriKind.Absolute);//返回全部历史数据
                //Uri uri1 = new Uri(" http://ichart.finance.yahoo.com/table.csv?s=300072.sz&d=7&e=23&f=2010&a=5&b=11&c=2010", UriKind.Absolute);
                string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, code + ".csv");
                try
                {
                    wc.DownloadFile(uri, filePath);
                }
                catch (WebException)
                {
                    return 0;
                }

                if (System.IO.File.Exists(filePath))
                {
                    return this.ImportFromCSV(code, name, filePath);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 自动下载股票数据(指定日期以来的历史数据)并导入数据库
        /// </summary>
        /// <param name="code">股票代码</param>
        /// <param name="name">股票名称</param>
        /// <param name="beginDate">开始日期</param>
        /// <returns>导入行数</returns>
        public int AutoUpdateByCodeName(string code, string name, DateTime beginDate)
        {
            string newCode = TradingAnalyzer.Common.CodeRules.CodeWithMarket(code);
            using (WebClient wc = new WebClient())
            {
                Uri uri = new Uri("http://ichart.finance.yahoo.com/table.csv?s=" + newCode + "&a=" + (beginDate.Month-1) + "&b=" + beginDate.Day + "&c=" + beginDate.Year, UriKind.Absolute);
                string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, code + ".csv");
                try
                {
                    wc.DownloadFile(uri, filePath);
                }
                catch (WebException)
                {
                    return 0;
                }

                if (System.IO.File.Exists(filePath))
                {
                    return this.ImportFromCSV(code, name, filePath);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 自动下载股票数据(指定日期区间的历史数据)并导入数据库
        /// </summary>
        /// <param name="code">股票代码</param>
        /// <param name="name">股票名称</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>导入行数</returns>
        public int AutoUpdateByCodeName(string code, string name, DateTime beginDate, DateTime endDate)
        {
            string newCode = TradingAnalyzer.Common.CodeRules.CodeWithMarket(code);
            using (WebClient wc = new WebClient())
            {
                Uri uri = new Uri("http://ichart.finance.yahoo.com/table.csv?s=" + newCode + "&a=" + (beginDate.Month - 1) + "&b=" + beginDate.Day + "&c=" + beginDate.Year + "&d=" + (endDate.Month - 1) + "&e=" + endDate.Day + "&f=" + endDate.Year, UriKind.Absolute);
                string filePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, code + ".csv");
                try
                {
                    wc.DownloadFile(uri, filePath);
                }
                catch (WebException)
                {
                    return 0;
                }

                if (System.IO.File.Exists(filePath))
                {
                    return this.ImportFromCSV(code, name, filePath);
                }
                else
                {
                    return 0;
                }
            }
        }

        public DateTime GetMaxDate()
        {
            return this.stockService.GetMaxDate();
        }

        public int GetCount()
        {
            return this.stockService.GetCount();
        }
    }
}
