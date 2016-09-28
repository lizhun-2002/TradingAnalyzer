using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAnalyzer.Common
{
    /// <summary>
    /// csv 数据访问帮助类.
    /// </summary>
    public class CSVUtility
    {
        private CSVUtility()
        {
        }

        //write a new file, existed file will be overwritten
        public static void WriteCSV(string filePathName,List<String[]>ls)
        {
            WriteCSV(filePathName,false,ls);
        }

        //write a file, existed file will be overwritten if append = false
        public static void WriteCSV(string filePathName,bool append, List<String[]> ls)
        {
            StreamWriter fileWriter=new StreamWriter(filePathName,append,Encoding.Default);
            foreach(String[] strArr in ls)
            {
                fileWriter.WriteLine(String.Join (",",strArr) );
            }
            fileWriter.Flush();
            fileWriter.Close();
            
        }

        public static List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            StreamReader fileReader=new   StreamReader(filePathName);  
            string strLine="";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length>0)
                {
                    ls.Add(strLine.Split(','));
                    //Debug.WriteLine(strLine);
                }
            } 
            fileReader.Close();
            return ls;
        }

        /// <summary>
        /// 以数据库的形式读取CSV文件(不推荐使用，因为当数据超过int范围时为DBNull)
        /// </summary>
        /// <param name="filePathName"></param>
        /// <returns></returns>
        public static DataTable ReadCSVAsDB(string filePathName)
        {
            try
            {
                string fileFullName = Path.GetFileName(filePathName);
                string folderPath = filePathName.Substring(0, filePathName.LastIndexOf('\\') + 1);
                string connStr = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='text;HDR=Yes;IMEX =1'", folderPath);   //FMT=Delimited;   
                string sql = string.Format(@"SELECT * FROM [{0}]", fileFullName);   //fileFullName包含“.csv”，不能包含其他“.”;如“000568.sz.csv”会报错
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr);
                da.Fill(dt);     // 填充DataSet  
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                //DataTable dt = new DataTable();
                return null;
            }
        }

    }
}
