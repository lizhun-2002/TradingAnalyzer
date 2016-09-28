using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingAnalyzer.BLL;
using TradingAnalyzer.DAL;
using TradingAnalyzer.Model;
using TradingAnalyzer.UI;
using TradingAnalyzer.Common;

namespace TradingAnalyzer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //展开所有节点
            this.treeView1.ExpandAll();

            //默认显示系统配置节点的内容
            splitContainer1.Panel2.Controls.Clear();
            this.showControl<UCtlConfiguration>("uCtlConfiguration");
            //UCtlConfiguration uCtlConfiguration = new UCtlConfiguration();
            //uCtlConfiguration.Parent = splitContainer1.Panel2;
            //uCtlConfiguration.Dock = DockStyle.Fill;
            //uCtlConfiguration.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Name == "NodeConfiguration")
                {
                    this.showControl<UCtlConfiguration>("uCtlConfiguration");
                }
                if (e.Node.Name == "NodeAccountInformation")
                {
                    this.showControl<UCtlAccountInformation>("uCtlAccountInformation");
                    UCtlAccountInformation temp = splitContainer1.Panel2.Controls["uCtlAccountInformation"] as UCtlAccountInformation;
                    if (temp != null)
                    {
                        temp.ShowTradingTrace += uCtlAccountInformation_ShowTradingTrace;
                    }
                }
                if (e.Node.Name == "NodeAccountStatus")
                {
                    this.showControl<DataGridView>("dgvAccountStatus");
                    DataGridView dgv = splitContainer1.Panel2.Controls["dgvAccountStatus"] as DataGridView;
                    dgv.ReadOnly = true;
                    dgv.DataSource = new AccountStatusManager().GetAll();
                }
                if (e.Node.Name == "NodePortfolio")
                {
                    this.showControl<DataGridView>("dgvPortfolio");
                    DataGridView dgv = splitContainer1.Panel2.Controls["dgvPortfolio"] as DataGridView;
                    dgv.ReadOnly = true;
                    dgv.DataSource = new PortfolioManager().GetAll();
                }
                if (e.Node.Name == "NodeCheckList")
                {
                    this.showControl<DataGridView>("dgvCheckList");
                    DataGridView dgv = splitContainer1.Panel2.Controls["dgvCheckList"] as DataGridView;
                    dgv.ReadOnly = true;
                    dgv.DataSource = new CheckListManager().GetAll();
                }
                if (e.Node.Name == "NodeYieldCurve")
                {
                    this.showControl<UCtlYieldCurve>("uCtlYieldCurve");
                }
                if (e.Node.Name == "NodeTradingTrace")
                {
                    this.showControl<UCtlTradingTrace>("uCtlTradingTrace");
                }
            }
        }

        void uCtlAccountInformation_ShowTradingTrace(object sender, UCtlAccountInformation.ShowTradingTraceEventArgs e)
        {
            this.treeView1.SelectedNode = this.treeView1.Nodes[2].Nodes[1];

            this.showControl<UCtlTradingTrace>("uCtlTradingTrace");
            UCtlTradingTrace temp = splitContainer1.Panel2.Controls["uCtlTradingTrace"] as UCtlTradingTrace;
            if (temp != null)
            {
                temp.UpdateUCtlTradingTrace(e.StockCode);
            }
        }

        /// <summary>
        /// 显示splitContainer1.Panel2.Controls中指定名称的控件
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="ctlName">控件名称</param>
        void showControl<T>(string ctlName) where T : Control, new()
        {
            //若控件不存在，则创建T类型控件
            if (splitContainer1.Panel2.Controls[ctlName] == null)
            {
                T ctl = new T();
                ctl.Name = ctlName;
                ctl.Parent = splitContainer1.Panel2;
                ctl.Dock = DockStyle.Fill;
                ctl.Show();
            }
            //显示指定控件，并隐藏其他控件
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                control.Visible = false;
            }
            splitContainer1.Panel2.Controls[ctlName].Visible = true;

            //另外方法
            //splitContainer1.Panel2.Controls.OfType<UCtlTradingTrace>().ToList<UCtlTradingTrace>()[0].Visible = true;

            //另外方法
            //splitContainer1.Panel2.Controls.Clear();
            //UCtlTradingTrace uCtlTradingTrace = new UCtlTradingTrace();
            //uCtlTradingTrace.Parent = splitContainer1.Panel2;
            //uCtlTradingTrace.Dock = DockStyle.Fill;
            //uCtlTradingTrace.Show();
        }

    }
}
