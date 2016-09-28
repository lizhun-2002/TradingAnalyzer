using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingAnalyzer.UI
{
    public partial class UCtlAccountInformation : UserControl
    {
        public delegate void ShowTradingTraceEventHandler(object sender, ShowTradingTraceEventArgs e);
        public event ShowTradingTraceEventHandler ShowTradingTrace;
        public class ShowTradingTraceEventArgs:EventArgs
        {
            public string StockCode { get; set; }
            public ShowTradingTraceEventArgs(string code)
            {
                this.StockCode = code;
            }
        }

        public UCtlAccountInformation()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //清空label显示的数值
            this.labelTotalAsset.Text = "";
            this.labelChange.Text = "";
            this.labelMarketValue.Text = "";
            this.labelAvailableMoney.Text = "";

            //查询数据，并显示
            DateTime date = this.dateTimePicker1.Value;
            Model.AccountStatus accountStatus = new BLL.AccountStatusManager().GetByDate(date);
            if (accountStatus != null)
            {
                this.labelTotalAsset.Text = accountStatus.TotalAsset.ToString();
                this.labelChange.Text = accountStatus.Change.ToString();
                this.labelMarketValue.Text = accountStatus.MarketValue.ToString();
                this.labelAvailableMoney.Text = accountStatus.AvailableMoney.ToString();
            }

            this.dGVPortfolio.DataSource = new BLL.PortfolioManager().GetByDate(date);
        }

        private void UCtlAccountInformation_Load(object sender, EventArgs e)
        {
            //默认显示AccountStatus数据库的最大日期的账号和组合数据。
            BLL.AccountStatusManager manager = new BLL.AccountStatusManager();
            if (manager.GetCount() == 0)
            {
                this.dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                this.dateTimePicker1.Value = manager.GetMaxDate();
                this.btnSearch_Click(this, new EventArgs());
            }
        }

        private void dGVPortfolio_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dGVPortfolio.Rows[e.RowIndex].Selected == false)
                    {
                        dGVPortfolio.ClearSelection();
                        dGVPortfolio.Rows[e.RowIndex].Selected = true;
                    }

                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    contextMenuStrip1.Tag = dGVPortfolio.Rows[e.RowIndex].Cells[1].Value;
                }
            }
        }

        private void 查看交易轨迹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ShowTradingTrace != null)
            {
                this.ShowTradingTrace(this,new ShowTradingTraceEventArgs(this.contextMenuStrip1.Tag.ToString()));
            }
        }
    }
}
