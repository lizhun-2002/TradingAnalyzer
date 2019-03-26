using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Model;
using System.Data.SqlClient;
using System.Data;

namespace TradingAnalyzer.DAL
{
    class AccountStatusService
    {
        public int AddNew(AccountStatus accountStatus)
        {
            return SQLHelper.ExecuteNonQuery("insert into T_AccountStatus([Date],[AvailableMoney],[MarketValue],[TotalAsset],[Change],[TotalCost]) values(@Date,@AvailableMoney,@MarketValue,@TotalAsset,@Change,@TotalCost)",
                new SqlParameter("Date", accountStatus.Date),
                new SqlParameter("AvailableMoney", accountStatus.AvailableMoney),
                new SqlParameter("MarketValue", accountStatus.MarketValue),
                new SqlParameter("TotalAsset", accountStatus.TotalAsset),
                new SqlParameter("Change", accountStatus.Change),
                new SqlParameter("@TotalCost",accountStatus.TotalCost)
                );
        }

        public int DeleteByDate(DateTime date)
        {
            return SQLHelper.ExecuteNonQuery("delete from T_AccountStatus where [Date]=@Date", new SqlParameter("Date", date));
        }

        public int DeleteAll()
        {
            return SQLHelper.ExecuteNonQuery("delete from T_AccountStatus");
        }

        public int Update(AccountStatus accountStatus)
        {
            return SQLHelper.ExecuteNonQuery("update T_AccountStatus set [AvailableMoney]=@AvailableMoney,[MarketValue]=@MarketValue,[TotalAsset]=@TotalAsset,[Change]=@Change,[TotalCost]=@TotalCost where [Date]=@Date",
                new SqlParameter("Date", accountStatus.Date),
                new SqlParameter("AvailableMoney", accountStatus.AvailableMoney),
                new SqlParameter("MarketValue", accountStatus.MarketValue),
                new SqlParameter("TotalAsset", accountStatus.TotalAsset),
                new SqlParameter("Change", accountStatus.Change),
                new SqlParameter("@TotalCost",accountStatus.TotalCost)
                );
        }

        public IEnumerable<AccountStatus> GetAll()
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_AccountStatus order by [Date]");
            List<AccountStatus> list = new List<AccountStatus>();
            foreach (DataRow row in dt.Rows)
            {
                AccountStatus accountStatus = new AccountStatus();
                accountStatus.Date = (DateTime)row["Date"];
                accountStatus.AvailableMoney = (double)row["AvailableMoney"];
                accountStatus.MarketValue = (double)row["MarketValue"];
                accountStatus.TotalAsset = (double)row["TotalAsset"];
                accountStatus.Change = (double)row["Change"];
                accountStatus.TotalCost = (double)row["TotalCost"];
                list.Add(accountStatus);
            }
            return list;
        }

        public AccountStatus GetByDate(DateTime date)
        {
            DataTable dt = new DataTable();
            dt = SQLHelper.ExecuteDataTable("select * from T_AccountStatus where [Date]=@Date", new SqlParameter("Date", date));
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                AccountStatus accountStatus = new AccountStatus();
                accountStatus.Date = (DateTime)row["Date"];
                accountStatus.AvailableMoney = (double)row["AvailableMoney"];
                accountStatus.MarketValue = (double)row["MarketValue"];
                accountStatus.TotalAsset = (double)row["TotalAsset"];
                accountStatus.Change = (double)row["Change"];
                accountStatus.TotalCost = (double)row["TotalCost"];
                return accountStatus;
            }
            else if (dt.Rows.Count > 1)
            {
                throw new Exception(string.Format("{0}日账号状态数据重复！", date));
            }
            else
            {
                return null;
            }
        }

        public DateTime GetMaxDate()
        {
            return (DateTime)SQLHelper.ExecuteScalar("select max([Date]) from T_AccountStatus");
        }

        public int GetCount()
        {
            return (int)SQLHelper.ExecuteScalar("select COUNT(*) from T_AccountStatus");
        }
    }
}
