using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TradingAnalyzer.Common
{
    class TXTUtility
    {
        /// <summary>
        /// 将正则表达式分割的txt文件转换成DataTable
        /// </summary>
        /// <param name="filePathName"></param>
        /// <param name="headerLine">列标题所在行号，没有则为0</param>
        /// <param name="regStr">正则表达式</param>
        /// <param name="delStr">需要删除的字符串数组，没有则为null</param>
        /// <returns>string类型DataTable</returns>
        /// <example>regStr=@"\s+"表示若干空白字符；regStr=@"\t"表示一个制表符</example>
        public static DataTable ReadTxtSplitByRegStr(string filePathName, int headerLine, string regStr, string[] delStr)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePathName,Encoding.Default))
                {
                    DataTable dt = new DataTable();
                    int rowNum=0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (delStr != null)
                        {
                            for (int i = 0; i < delStr.Length; i++)
                            {
                                line = line.Replace(delStr[i], "");
                            }
                        }
                        rowNum++;
                        if (rowNum < headerLine)
                        {
                            continue;
                        }
                        else if (rowNum == headerLine)
                        {
                            string[] fieldArray = Regex.Split(line.Trim(), regStr);
                            for (int i = 0; i < fieldArray.Length; i++)
                            {
                                dt.Columns.Add(fieldArray[i], typeof(string));
                            }
                        }
                        else
                        {
                            string[] fieldArray = Regex.Split(line.Trim(), regStr);
                            //无列标题则使用数字作为列名
                            if (headerLine == 0 && rowNum == 1)
                            {
                                for (int i = 0; i < fieldArray.Length; i++)
                                {
                                    dt.Columns.Add(i.ToString(), typeof(string));
                                }
                            }
                            DataRow row = dt.NewRow();
                            for (int i = 0; i < fieldArray.Length; i++)
                            {
                                row[i] = fieldArray[i];
                            }
                            dt.Rows.Add(row);
                        }
                    }
                    return dt;
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
