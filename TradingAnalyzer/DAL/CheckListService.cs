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
    class CheckListService
    {
        public int AddNew(CheckList checklist)
        {
            string sql = @"INSERT INTO [dbo].[T_CheckList]
           ([Date]
           ,[TradeName]
           ,[TradeDirection]
           ,[Code]
           ,[Name]
           ,[TradePrice]
           ,[TradeVolume]
           ,[RemainingVolume]
           ,[TradeAmount]
           ,[SettlementAmount]
           ,[RemainingAmount]
           ,[Commission]
           ,[Tax]
           ,[Fee]
           ,[TradeCode])
     VALUES
           (@Date
           ,@TradeName
           ,@TradeDirection
           ,@Code
           ,@Name
           ,@TradePrice
           ,@TradeVolume
           ,@RemainingVolume
           ,@TradeAmount
           ,@SettlementAmount
           ,@RemainingAmount
           ,@Commission
           ,@Tax
           ,@Fee
           ,@TradeCode)";

            SqlParameter[] para =
            {
                new SqlParameter("@Date", SqlDbType.Date, 3),
                new SqlParameter("@TradeName", SqlDbType.NVarChar, 50),
                new SqlParameter("@TradeDirection", SqlDbType.Int, 4),
                new SqlParameter("@Code", SqlDbType.NVarChar, 50),
                new SqlParameter("@Name", SqlDbType.NVarChar, 50),
                new SqlParameter("@TradePrice", SqlDbType.Float, 8),
                new SqlParameter("@TradeVolume", SqlDbType.Int, 4),
                new SqlParameter("@RemainingVolume", SqlDbType.Int, 4),
                new SqlParameter("@TradeAmount", SqlDbType.Float, 8),
                new SqlParameter("@SettlementAmount", SqlDbType.Float, 8),
                new SqlParameter("@RemainingAmount", SqlDbType.Float, 8),
                new SqlParameter("@Commission", SqlDbType.Float, 8),
                new SqlParameter("@Tax", SqlDbType.Float, 8),
                new SqlParameter("@Fee", SqlDbType.Float, 8),
                new SqlParameter("@TradeCode", SqlDbType.NVarChar, 50)
            };

            para[0].Value = checklist.Date;
            para[1].Value = checklist.TradeName;
            para[2].Value = checklist.TradeDirection;
            para[3].Value = checklist.Code;
            para[4].Value = checklist.Name;
            para[5].Value = checklist.TradePrice;
            para[6].Value = checklist.TradeVolume;
            para[7].Value = checklist.RemainingVolume;
            para[8].Value = checklist.TradeAmount;
            para[9].Value = checklist.SettlementAmount;
            para[10].Value = checklist.RemainingAmount;
            para[11].Value = checklist.Commission;
            para[12].Value = checklist.Tax;
            para[13].Value = checklist.Fee;
            para[14].Value = checklist.TradeCode;

            return SQLHelper.ExecuteNonQuery(sql, para);
        }

        public int DeleteByDateCode(DateTime date, string code)
        {
            return SQLHelper.ExecuteNonQuery("delete from T_CheckList where [Date]=@Date and [Code]=@Code", new SqlParameter("Date", date), new SqlParameter("Code", code));
        }

        public int DeleteByDateTradeCode(DateTime date, string tradeCode)
        {
            return SQLHelper.ExecuteNonQuery("delete from T_CheckList where [Date]=@Date and [TradeCode]=@TradeCode", new SqlParameter("Date", date), new SqlParameter("TradeCode", tradeCode));
        }

        public int DeleteAll()
        {
            return SQLHelper.ExecuteNonQuery("delete from T_CheckList");
        }

        //更新银证转账的交割单时，可能出错。例如同日两笔转账金额相等。
        public int Update(CheckList checklist)
        {
            string sql = @"update [dbo].[T_CheckList] set
            [TradeName]= @TradeName
            ,[TradeDirection]= @TradeDirection
            ,[Code]= @Code
            ,[Name]= @Name
            ,[TradePrice]= @TradePrice
            ,[TradeVolume]= @TradeVolume
            ,[RemainingVolume]= @RemainingVolume
            ,[TradeAmount]= @TradeAmount
            ,[SettlementAmount]= @SettlementAmount
            ,[RemainingAmount]= @RemainingAmount
            ,[Commission]= @Commission
            ,[Tax]= @Tax
            ,[Fee]= @Fee
            where [Date]= @Date and [TradeCode]=@TradeCode
            ";

            SqlParameter[] para =
            {
                new SqlParameter("@Date", SqlDbType.Date, 3),
                new SqlParameter("@TradeName", SqlDbType.NVarChar, 50),
                new SqlParameter("@TradeDirection", SqlDbType.Int, 4),
                new SqlParameter("@Code", SqlDbType.NVarChar, 50),
                new SqlParameter("@Name", SqlDbType.NVarChar, 50),
                new SqlParameter("@TradePrice", SqlDbType.Float, 8),
                new SqlParameter("@TradeVolume", SqlDbType.Int, 4),
                new SqlParameter("@RemainingVolume", SqlDbType.Int, 4),
                new SqlParameter("@TradeAmount", SqlDbType.Float, 8),
                new SqlParameter("@SettlementAmount", SqlDbType.Float, 8),
                new SqlParameter("@RemainingAmount", SqlDbType.Float, 8),
                new SqlParameter("@Commission", SqlDbType.Float, 8),
                new SqlParameter("@Tax", SqlDbType.Float, 8),
                new SqlParameter("@Fee", SqlDbType.Float, 8),
                new SqlParameter("@TradeCode", SqlDbType.NVarChar, 50)
            };

            para[0].Value = checklist.Date;
            para[1].Value = checklist.TradeName;
            para[2].Value = checklist.TradeDirection;
            para[3].Value = checklist.Code;
            para[4].Value = checklist.Name;
            para[5].Value = checklist.TradePrice;
            para[6].Value = checklist.TradeVolume;
            para[7].Value = checklist.RemainingVolume;
            para[8].Value = checklist.TradeAmount;
            para[9].Value = checklist.SettlementAmount;
            para[10].Value = checklist.RemainingAmount;
            para[11].Value = checklist.Commission;
            para[12].Value = checklist.Tax;
            para[13].Value = checklist.Fee;
            para[14].Value = checklist.TradeCode;

            return SQLHelper.ExecuteNonQuery(sql, para);
        }

        public IEnumerable<CheckList> GetAll()
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_CheckList order by [Date]");
            List<CheckList> list = new List<CheckList>();
            foreach (DataRow row in dt.Rows)
            {
                CheckList checklist = new CheckList();
                checklist.Date=(DateTime)row["Date"];
                checklist.TradeName=(string)row["TradeName"];
                checklist.TradeDirection=(int)row["TradeDirection"];
                checklist.Code=(string)row["Code"];
                checklist.Name=(string)row["Name"];
                checklist.TradePrice=(double)row["TradePrice"];
                checklist.TradeVolume=(int)row["TradeVolume"];
                checklist.RemainingVolume=(int)row["RemainingVolume"];
                checklist.TradeAmount=(double)row["TradeAmount"];
                checklist.SettlementAmount=(double)row["SettlementAmount"];
                checklist.RemainingAmount=(double)row["RemainingAmount"];
                checklist.Commission=(double)row["Commission"];
                checklist.Tax=(double)row["Tax"];
                checklist.Fee = (double)row["Fee"];
                checklist.TradeCode=(string)row["TradeCode"];

                list.Add(checklist);
            }
            return list;
        }

        public IEnumerable<CheckList> GetByDate(DateTime date)
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_CheckList where [Date]=@Date ", new SqlParameter("@Date",date));
            List<CheckList> list = new List<CheckList>();
            foreach (DataRow row in dt.Rows)
            {
                CheckList checklist = new CheckList();
                checklist.Date = (DateTime)row["Date"];
                checklist.TradeName = (string)row["TradeName"];
                checklist.TradeDirection = (int)row["TradeDirection"];
                checklist.Code = (string)row["Code"];
                checklist.Name = (string)row["Name"];
                checklist.TradePrice = (double)row["TradePrice"];
                checklist.TradeVolume = (int)row["TradeVolume"];
                checklist.RemainingVolume = (int)row["RemainingVolume"];
                checklist.TradeAmount = (double)row["TradeAmount"];
                checklist.SettlementAmount = (double)row["SettlementAmount"];
                checklist.RemainingAmount = (double)row["RemainingAmount"];
                checklist.Commission = (double)row["Commission"];
                checklist.Tax = (double)row["Tax"];
                checklist.Fee = (double)row["Fee"];
                checklist.TradeCode = (string)row["TradeCode"];

                list.Add(checklist);
            }
            return list;
        }

        public IEnumerable<CheckList> GetByCode(string code)
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_CheckList where [Code]=@Code order by [date]", new SqlParameter("@Code", code));
            List<CheckList> list = new List<CheckList>();
            foreach (DataRow row in dt.Rows)
            {
                CheckList checklist = new CheckList();
                checklist.Date = (DateTime)row["Date"];
                checklist.TradeName = (string)row["TradeName"];
                checklist.TradeDirection = (int)row["TradeDirection"];
                checklist.Code = (string)row["Code"];
                checklist.Name = (string)row["Name"];
                checklist.TradePrice = (double)row["TradePrice"];
                checklist.TradeVolume = (int)row["TradeVolume"];
                checklist.RemainingVolume = (int)row["RemainingVolume"];
                checklist.TradeAmount = (double)row["TradeAmount"];
                checklist.SettlementAmount = (double)row["SettlementAmount"];
                checklist.RemainingAmount = (double)row["RemainingAmount"];
                checklist.Commission = (double)row["Commission"];
                checklist.Tax = (double)row["Tax"];
                checklist.Fee = (double)row["Fee"];
                checklist.TradeCode = (string)row["TradeCode"];

                list.Add(checklist);
            }
            return list;
        }
        

        public CheckList GetByDateRemainingAmountTradeCode(DateTime date, double reAmount, string tradeCode)
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_CheckList where [Date]=@Date and [RemainingAmount]=@RemainingAmount and [TradeCode]=@TradeCode", new SqlParameter("Date", date), new SqlParameter("RemainingAmount", reAmount), new SqlParameter("TradeCode", tradeCode));
            if (dt.Rows.Count != 0)
            {
                DataRow row = dt.Rows[0];
                CheckList cl = new CheckList();
                cl.Date = (DateTime)row["Date"];
                cl.TradeName = (string)row["TradeName"];
                cl.TradeDirection = (int)row["TradeDirection"];
                cl.Code = (string)row["Code"];
                cl.Name = (string)row["Name"];
                cl.TradePrice = (double)row["TradePrice"];
                cl.TradeVolume = (int)row["TradeVolume"];
                cl.RemainingVolume = (int)row["RemainingVolume"];
                cl.TradeAmount = (double)row["TradeAmount"];
                cl.SettlementAmount = (double)row["SettlementAmount"];
                cl.RemainingAmount = (double)row["RemainingAmount"];
                cl.Commission = (double)row["Commission"];
                cl.Tax = (double)row["Tax"];
                cl.Fee = (double)row["Fee"];
                cl.TradeCode = (string)row["TradeCode"];
                return cl;
            }
            else
            {
                return null;
            }
        }

        public DateTime GetMaxDate()
        {
            return (DateTime)SQLHelper.ExecuteScalar("select max([Date]) from T_CheckList");
        }

        public DateTime GetMinDate()
        {
            return (DateTime)SQLHelper.ExecuteScalar("select min([Date]) from T_CheckList");
        }

        public int GetCount()
        {
            return (int)SQLHelper.ExecuteScalar("select COUNT(*) from T_CheckList");
        }
    }
}
