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
    class PortfolioService
    {
        public int AddNew(Portfolio portfolio)
        {
            return SQLHelper.ExecuteNonQuery("insert into T_Portfolio([Date],[Code],[Name],[Position],[ClosePrice] ,[BeginDate] ,[CostPrice]) values(@Date,@Code,@Name,@Position,@ClosePrice,@BeginDate,@CostPrice)",
                new SqlParameter("Date", portfolio.Date),
                new SqlParameter("Code", portfolio.Code),
                new SqlParameter("Name", portfolio.Name),
                new SqlParameter("Position", portfolio.Position),
                new SqlParameter("ClosePrice", portfolio.ClosePrice),
                new SqlParameter("BeginDate", portfolio.BeginDate),
                new SqlParameter("CostPrice", portfolio.CostPrice)
                );
        }

        public int DeleteByDate(DateTime date)
        {
            return SQLHelper.ExecuteNonQuery("delete from T_Portfolio where [Date]=@Date", new SqlParameter("Date", date));
        }

        public int DeleteByDateCode(DateTime date, string code)
        {
            return SQLHelper.ExecuteNonQuery("delete from T_Portfolio where [Date]=@Date and [Code]=@Code", new SqlParameter("Date", date), new SqlParameter("Code", code));
        }

        public int DeleteAll()
        {
            return SQLHelper.ExecuteNonQuery("delete from T_Portfolio");
        }

        public int Update(Portfolio portfolio)
        {
            return SQLHelper.ExecuteNonQuery("update T_Portfolio set [Position]=@Position,[ClosePrice]=@ClosePrice,[BeginDate]=@BeginDate,[CostPrice]=@CostPrice where [Code]=@Code and [Name]=@Name and [Date]=@Date",
                new SqlParameter("Date", portfolio.Date),
                new SqlParameter("Code", portfolio.Code),
                new SqlParameter("Name", portfolio.Name),
                new SqlParameter("Position", portfolio.Position),
                new SqlParameter("ClosePrice", portfolio.ClosePrice),
                new SqlParameter("BeginDate", portfolio.BeginDate),
                new SqlParameter("CostPrice", portfolio.CostPrice)
                );
        }

        public IEnumerable<Portfolio> GetAll()
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_Portfolio order by [Date]");
            List<Portfolio> list = new List<Portfolio>();
            foreach (DataRow row in dt.Rows)
            {
                Portfolio portfolio = new Portfolio();
                portfolio.Date = (DateTime)row["Date"];
                portfolio.Code = (string)row["Code"];
                portfolio.Name = (string)row["Name"];
                portfolio.Position = (int)row["Position"];
                portfolio.ClosePrice = (double)row["ClosePrice"];
                portfolio.BeginDate = (DateTime)row["BeginDate"];
                portfolio.CostPrice = (double)row["CostPrice"];
                list.Add(portfolio);
            }
            return list;
        }

        public IEnumerable<Portfolio> GetByDate(DateTime date)
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_Portfolio where [Date]=@Date", new SqlParameter("Date", date));
            List<Portfolio> list = new List<Portfolio>();
            foreach (DataRow row in dt.Rows)
            {
                Portfolio portfolio = new Portfolio();
                portfolio.Date = (DateTime)row["Date"];
                portfolio.Code = (string)row["Code"];
                portfolio.Name = (string)row["Name"];
                portfolio.Position = (int)row["Position"];
                portfolio.ClosePrice = (double)row["ClosePrice"];
                portfolio.BeginDate = (DateTime)row["BeginDate"];
                portfolio.CostPrice = (double)row["CostPrice"];
                list.Add(portfolio);
            }
            return list;
        }

        public Portfolio GetByDateCode(DateTime date, string code)
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_Portfolio where [Date]=@Date and [Code]=@Code", new SqlParameter("Date", date), new SqlParameter("Code", code));
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                Portfolio portfolio = new Portfolio();
                portfolio.Date = (DateTime)row["Date"];
                portfolio.Code = (string)row["Code"];
                portfolio.Name = (string)row["Name"];
                portfolio.Position = (int)row["Position"];
                portfolio.ClosePrice = (double)row["ClosePrice"];
                portfolio.BeginDate = (DateTime)row["BeginDate"];
                portfolio.CostPrice = (double)row["CostPrice"];
                return portfolio;
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception(string.Format("{1}日投资组合中{0}股票数据重复！", code, date));
            }
            else
            {
                return null;
            }
        }

        public DateTime GetMaxDate()
        {
            return (DateTime)SQLHelper.ExecuteScalar("select max([Date]) from T_Portfolio");
        }

        public DateTime GetMinDate()
        {
            return (DateTime)SQLHelper.ExecuteScalar("select min([Date]) from T_Portfolio");
        }

        public int GetCount()
        {
            return (int)SQLHelper.ExecuteScalar("select COUNT(*) from T_Portfolio");
        }
    }
}
