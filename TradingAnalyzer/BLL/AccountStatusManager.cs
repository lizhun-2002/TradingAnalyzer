using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Model;
using TradingAnalyzer.DAL;

namespace TradingAnalyzer.BLL
{
    class AccountStatusManager
    {
        private AccountStatusService accountStatusService = new AccountStatusService();

        public int AddNew(AccountStatus accountStatus)
        {
            if (this.CheckExists(accountStatus.Date))
            {
                return 0;
            }
            else
            {
                return this.accountStatusService.AddNew(accountStatus);
            }

        }

        public int DeleteByDate(DateTime date)
        {
            return this.accountStatusService.DeleteByDate(date);
        }

        public int DeleteAll()
        {
            return this.accountStatusService.DeleteAll();
        }

        public int Update(AccountStatus accountStatus)
        {
            return this.accountStatusService.Update(accountStatus);
        }

        public IEnumerable<AccountStatus> GetAll()
        {
            return this.accountStatusService.GetAll();
        }

        public AccountStatus GetByDate(DateTime date)
        {
            return this.accountStatusService.GetByDate(date);
        }

        public bool CheckExists(DateTime date)
        {
            AccountStatus accountStatus = this.accountStatusService.GetByDate(date);
            if (accountStatus == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DateTime GetMaxDate()
        {
            return this.accountStatusService.GetMaxDate();
        }

        public int GetCount()
        {
            return this.accountStatusService.GetCount();
        }
    }
}
