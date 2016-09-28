using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAnalyzer.Model;
using TradingAnalyzer.DAL;

namespace TradingAnalyzer.BLL
{
    class PortfolioManager
    {
        private PortfolioService portfolioService = new PortfolioService();

        public int AddNew(Portfolio portfolio)
        {
            if (this.CheckExists(portfolio.Date, portfolio.Code))
            {
                return 0;
            }
            else
            {
                return this.portfolioService.AddNew(portfolio);
            }

        }

        public int DeleteByDate(DateTime date)
        {
            return this.portfolioService.DeleteByDate(date);
        }

        public int DeleteByDateCode(DateTime date, string code)
        {
            return this.portfolioService.DeleteByDateCode(date, code);
        }

        public int DeleteAll()
        {
            return this.portfolioService.DeleteAll();
        }

        public int Update(Portfolio portfolio)
        {
            return this.portfolioService.Update(portfolio);
        }

        public IEnumerable<Portfolio> GetAll()
        {
            return this.portfolioService.GetAll();
        }

        public IEnumerable<Portfolio> GetByDate(DateTime date)
        {
            return this.portfolioService.GetByDate(date);
        }

        public Portfolio GetByDateCode(DateTime date, string code)
        {
            return this.portfolioService.GetByDateCode(date, code);
        }

        public bool CheckExists(DateTime date, string code)
        {
            Portfolio portfolio = this.portfolioService.GetByDateCode(date, code);
            if (portfolio == null)
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
            return this.portfolioService.GetMaxDate();
        }

        public DateTime GetMinDate()
        {
            return this.portfolioService.GetMinDate();
        }

        public int GetCount()
        {
            return this.portfolioService.GetCount();
        }
    }
}
