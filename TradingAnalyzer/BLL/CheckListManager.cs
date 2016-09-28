using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Model;
using TradingAnalyzer.DAL;
using TradingAnalyzer.Common;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using NPOI.XSSF.UserModel;

namespace TradingAnalyzer.BLL
{
    class CheckListManager
    {
        private CheckListService checkListService = new CheckListService();

        public int AddNew(CheckList checkList)
        {
            if (this.CheckExists(checkList.Date, checkList.RemainingAmount, checkList.TradeCode))
            {
                return 0;
            }
            else
            {
                return this.checkListService.AddNew(checkList);
            }

        }

        public int DeleteByDateCode(DateTime date, string code)
        {
            return this.checkListService.DeleteByDateCode(date, code);
        }

        public int DeleteByDateTradeCode(DateTime date, string tradeCode)
        {
            return this.checkListService.DeleteByDateTradeCode(date,tradeCode);
        }

        public int DeleteAll()
        {
            return this.checkListService.DeleteAll();
        }

        public int Update(CheckList checkList)
        {
            return this.checkListService.Update(checkList);
        }

        public IEnumerable<CheckList> GetAll()
        {
            return this.checkListService.GetAll();
        }

        public IEnumerable<CheckList> GetByDate(DateTime date)
        {
            return this.checkListService.GetByDate(date);
        }

        public IEnumerable<CheckList> GetByCode(string code)
        {
            return this.checkListService.GetByCode(code);
        }

        public CheckList GetByDateRemainingAmountTradeCode(DateTime date, double reAmount, string tradeCode)
        {
            return this.checkListService.GetByDateRemainingAmountTradeCode(date, reAmount, tradeCode);
        }

        public bool CheckExists(DateTime date, double reAmount, string tradeCode)
        {
            CheckList checkList = this.checkListService.GetByDateRemainingAmountTradeCode(date,reAmount,tradeCode);
            if (checkList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 判断是否曾经交易并清仓过指定股票
        /// </summary>
        /// <param name="code">股票代码</param>
        /// <returns>是否曾经交易并清仓过指定股票</returns>
        public bool HasBeenTraded(string code)
        {
            List<CheckList> list = this.checkListService.GetByCode(code).ToList<CheckList>();
            if (list.Count > 0)
            {
                foreach (CheckList checkList in list)
                {
                    if (checkList.RemainingVolume == 0 && checkList.TradeVolume > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>  
        /// 将xls、xlsx文件导入数据库
        /// </summary>
        /// <param name="xlsPath">xls、xlsx文件路径</param>
        /// <returns>受影响行数</returns>
        public int ImportFromXls(string xlsPath)
        {
            using (FileStream stream = new FileStream(xlsPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = null;
                if (System.IO.Path.GetExtension(xlsPath) == ".xls")
                {
                    workbook = new HSSFWorkbook(stream);
                }
                else if (System.IO.Path.GetExtension(xlsPath) == ".xlsx")
                {
                    workbook = new XSSFWorkbook(stream);
                }
                else
                {
                    return 0;
                }

                ISheet sheet = workbook.GetSheetAt(0);
                int insertedLinesCount = 0;
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    CheckList checkList = new CheckList();
                    checkList.Date = DateTime.ParseExact(row.GetCell(0).StringCellValue,"yyyyMMdd",System.Globalization.CultureInfo.CurrentCulture);
                    checkList.TradeName = row.GetCell(1).StringCellValue;

                    if (string.Equals(checkList.TradeName, "证券买入") || string.Equals(checkList.TradeName, "红股入账") || string.Equals(checkList.TradeName, "新股申购"))
                    {
                        checkList.TradeDirection = 1;
                    }
                    else if (string.Equals(checkList.TradeName, "证券卖出") || string.Equals(checkList.TradeName, "申购还款"))
                    {
                        checkList.TradeDirection = -1;
                    }
                    else
                    {
                        checkList.TradeDirection = 0;
                    }

                    checkList.Code = row.GetCell(2).StringCellValue;
                    checkList.Name = row.GetCell(3).StringCellValue;
                    checkList.TradePrice = row.GetCell(4).NumericCellValue;
                    checkList.TradeVolume = (int)row.GetCell(5).NumericCellValue;
                    checkList.RemainingVolume = (int)row.GetCell(6).NumericCellValue;
                    checkList.TradeAmount = row.GetCell(7).NumericCellValue;
                    checkList.SettlementAmount = row.GetCell(8).NumericCellValue;
                    checkList.RemainingAmount = row.GetCell(9).NumericCellValue;
                    checkList.Commission = row.GetCell(10).NumericCellValue;
                    checkList.Tax = row.GetCell(11).NumericCellValue;
                    checkList.Fee = row.GetCell(12).NumericCellValue;
                    checkList.TradeCode = row.GetCell(16).StringCellValue;

                    int j = this.AddNew(checkList);
                    insertedLinesCount+=j;
                }
                return insertedLinesCount;
            }
        }

        /// <summary>  
        /// 将txt文件导入数据库
        /// </summary>
        /// <param name="txtPath">txt文件路径</param>
        /// <param name="headerLine">列标题所在行号，没有则为0</param>
        /// <param name="regStr">正则表达式</param>
        /// <returns>受影响行数</returns>
        public int ImportFromTxt(string txtPath, int headerLine, string regStr, string[] delStr)
        {
            DataTable dt = TXTUtility.ReadTxtSplitByRegStr(txtPath, headerLine, regStr, delStr);
            if (dt == null)
            {
                return 0;
            }
            else
            {
                int insertedLinesCount = 0;
                foreach (DataRow row in dt.Rows)
                {
                    CheckList checkList = new CheckList();
                    checkList.Date = DateTime.ParseExact((string)row["交割日期"], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    checkList.TradeName = (string)row["业务名称"];

                    if (string.Equals(checkList.TradeName, "证券买入") || string.Equals(checkList.TradeName, "红股入账") || string.Equals(checkList.TradeName, "新股申购"))
                    {
                        checkList.TradeDirection = 1;
                    }
                    else if (string.Equals(checkList.TradeName, "证券卖出") || string.Equals(checkList.TradeName, "申购还款"))
                    {
                        checkList.TradeDirection = -1;
                    }
                    else
                    {
                        checkList.TradeDirection = 0;
                    }

                    checkList.Code = (string)row["证券代码"];
                    checkList.Name = (string)row["证券名称"];
                    checkList.TradePrice = double.Parse((string)row["成交价格"]);
                    checkList.TradeVolume = int.Parse((string)row["成交数量"]);
                    checkList.RemainingVolume = int.Parse((string)row["剩余数量"]);
                    checkList.TradeAmount = double.Parse((string)row["成交金额"]);
                    checkList.SettlementAmount = double.Parse((string)row["清算金额"]);
                    checkList.RemainingAmount = double.Parse((string)row["剩余金额"]);
                    checkList.Commission = double.Parse((string)row["佣金"]);
                    checkList.Tax = double.Parse((string)row["印花税"]);
                    checkList.Fee = double.Parse((string)row["过户费"]);
                    checkList.TradeCode = (string)row["成交编号"];

                    int j = this.AddNew(checkList);
                    insertedLinesCount += j;
                }
                return insertedLinesCount;
            }
        }

        public DateTime GetMaxDate()
        {
            return this.checkListService.GetMaxDate();
        }

        public DateTime GetMinDate()
        {
            return this.checkListService.GetMinDate();
        }

        public int GetCount()
        {
            return this.checkListService.GetCount();
        }

        /// <summary>
        /// 获得交易过的全部股票的不重复列表
        /// </summary>
        /// <returns>代码和名称的字典</returns>
        public Dictionary<string, string> GetTradedStock()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            List<CheckList> list = this.GetAll().ToList<CheckList>();
            foreach (CheckList checkList in list)
            {
                if (checkList.Code=="" || dict.Keys.Contains<string>(checkList.Code))
                {
                    continue;
                }
                else
                {
                    dict.Add(checkList.Code, checkList.Name);
                }
            }
            return dict;
        }
    }
}
