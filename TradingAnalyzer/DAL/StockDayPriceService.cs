using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Model;

namespace TradingAnalyzer.DAL
{
    class StockDayPriceService
    {
        public int AddNew(StockDayPrice stock)
        {
            return SQLHelper.ExecuteNonQuery("insert into T_DayPrice([Code],[Name],[Date],[Open],[High] ,[Low] ,[Close],[Volume] ,[AdjClose]) values(@Code,@Name,@Date,@Open,@High,@Low,@Close,@Volume,@AdjClose)",
                new SqlParameter("Code",stock.Code),
                new SqlParameter("Name", stock.Name),
                new SqlParameter("Date", stock.Date),
                new SqlParameter("Open", stock.Open),
                new SqlParameter("High", stock.High),
                new SqlParameter("Low", stock.Low),
                new SqlParameter("Close", stock.Close),
                new SqlParameter("Volume", stock.Volume),
                new SqlParameter("AdjClose", stock.AdjClose)
                );
        }

        public int DeleteByDateCode(DateTime date, string code)
        {
            return SQLHelper.ExecuteNonQuery("delete from T_DayPrice where [Date]=@Date and [Code]=@Code", new SqlParameter("Date",date),new SqlParameter("Code",code));
        }

        public int DeleteAll()
        {
            return SQLHelper.ExecuteNonQuery("delete from T_DayPrice");
        }

        public int Update(StockDayPrice stock) 
        {
            return SQLHelper.ExecuteNonQuery("update T_DayPrice set [Open]=@Open,[High]=@High,[Low]=@Low,[Close]=@Close,[Volume]=@Volume,[AdjClose]=@AdjClose where [Code]=@Code and [Name]=@Name and [Date]=@Date",
                new SqlParameter("Code", stock.Code),
                new SqlParameter("Name", stock.Name),
                new SqlParameter("Date", stock.Date),
                new SqlParameter("Open", stock.Open),
                new SqlParameter("High", stock.High),
                new SqlParameter("Low", stock.Low),
                new SqlParameter("Close", stock.Close),
                new SqlParameter("Volume", stock.Volume),
                new SqlParameter("AdjClose", stock.AdjClose)
                );
        }

        public IEnumerable<StockDayPrice> GetAll()
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_DayPrice");
            List<StockDayPrice> list =new List<StockDayPrice>();
            foreach (DataRow row in dt.Rows)
            {
                StockDayPrice st = new StockDayPrice();
                st.ID = (int)row["ID"];
                st.Code = (string)row["Code"];
                st.Name = (string)row["Name"];
                st.Date = (DateTime)row["Date"];
                st.Open = (double)row["Open"];
                st.High = (double)row["High"];
                st.Low = (double)row["Low"];
                st.Close = (double)row["Close"];
                st.Volume = (double)row["Volume"];
                st.AdjClose = (double)row["AdjClose"];
                list.Add(st);
            }
            return list;
        }

        public IEnumerable<StockDayPrice> GetByCode(string code)
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_DayPrice where [Code]=@Code order by [Date]", new SqlParameter("Code", code));
            List<StockDayPrice> list = new List<StockDayPrice>();
            foreach (DataRow row in dt.Rows)
            {
                StockDayPrice st = new StockDayPrice();
                st.ID = (int)row["ID"];
                st.Code = (string)row["Code"];
                st.Name = (string)row["Name"];
                st.Date = (DateTime)row["Date"];
                st.Open = (double)row["Open"];
                st.High = (double)row["High"];
                st.Low = (double)row["Low"];
                st.Close = (double)row["Close"];
                st.Volume = (double)row["Volume"];
                st.AdjClose = (double)row["AdjClose"];
                list.Add(st);
            }
            return list;
        }

        /// <summary>
        /// 根据指定的日期和股票代码，查询相应日K线数据
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="code">股票代码</param>
        /// <returns>StockDayPrice对象</returns>
        /// <exception cref="System.Exception">指定日期股票数据重复</exception>
        public StockDayPrice GetByDateCode(DateTime date, string code)
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_DayPrice where [Date]=@Date and [Code]=@Code", new SqlParameter("Date",date),new SqlParameter("Code",code));
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                StockDayPrice st = new StockDayPrice();
                st.ID = (int)row["ID"];
                st.Code = (string)row["Code"];
                st.Name = (string)row["Name"];
                st.Date = (DateTime)row["Date"];
                st.Open = (double)row["Open"];
                st.High = (double)row["High"];
                st.Low = (double)row["Low"];
                st.Close = (double)row["Close"];
                st.Volume = (double)row["Volume"];
                st.AdjClose = (double)row["AdjClose"];
                return st;
            }
            else if(dt.Rows.Count>1)
            {
                throw new Exception(string.Format("{0}股票{1}日数据重复！",code,date));
            }
            else
            {
                return null;
            }
        }

        public DateTime GetMaxDate()
        {
            return (DateTime)SQLHelper.ExecuteScalar("select max([Date]) from T_DayPrice");
        }

        public int GetCount()
        {
            return (int)SQLHelper.ExecuteScalar("select COUNT(*) from T_DayPrice");
        }
    }
}
