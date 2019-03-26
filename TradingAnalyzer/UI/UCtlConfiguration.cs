using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradingAnalyzer.BLL;

namespace TradingAnalyzer.UI
{
    public partial class UCtlConfiguration : UserControl
    {
        public UCtlConfiguration()
        {
            InitializeComponent();
        }

        private void btnImportCheckList_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CheckListManager manager = new CheckListManager();
                //int count1 = manager.ImportFromXls(@"D:\stockdata\20150726 交割单查询.xls");
                //int count2 = manager.ImportFromTxt(@"D:\stockdata\20150911 交割单查询.txt", 1, @"\t", new string[] { "=", "\"" });
                //int count3 = manager.ImportFromTxt(@"D:\stockdata\20151016 交割单查询.txt", 1, @"\t", new string[] { "=", "\"" });
                //MessageBox.Show(string.Format("Done! Inserted {0} lines.", count1 + count2 + count3));
                
                //根据文件扩展名，选择读取程序
                //string expStr = ofd.FileName.Substring(ofd.FileName.LastIndexOf('.'));
                string expStr = System.IO.Path.GetExtension(ofd.FileName);
                int count = 0;
                try
                {
                    if (expStr.ToLower() == ".xlsx" || expStr.ToLower() == ".xls")
                    {
                        count = manager.ImportFromXls(ofd.FileName);
                    }
                    else if (expStr.ToLower() == ".txt")
                    {
                        count = manager.ImportFromTxt(ofd.FileName, 1, @"\t", new string[] { "=", "\"" });
                    }
                    else
                    {
                        MessageBox.Show("请选择txt或者xlsx文件。");
                        return;
                    }
                }
                catch (NPOI.POIFS.FileSystem.NotOLE2FileException)
                {
                    MessageBox.Show("文件格式错误！文件的格式与文件扩展名指定的格式不一致。请尝试另存为。");
                    return;
                    //throw;
                }
                string p = new PortfolioGenerater().PGenerater();
                string a = new AccountStatusGenerater().AGenerater();
                MessageBox.Show(string.Format("Done! Inserted {0} lines." + "\n" + p.ToString() + "\n" + a.ToString(), count));

                //查询checklist数据库的最大日期，并显示在文本框中
                if (manager.GetCount() == 0)
                {
                    this.txtMaxDate.Text = "There is no checklist.";
                }
                else
                {
                    this.txtMaxDate.Text = manager.GetMaxDate().ToShortDateString();
                }
            }
        }

        private void btnClearCheckListDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("警告！是否确定要清空交割单数据库？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                CheckListManager manager = new CheckListManager();
                int count = manager.DeleteAll();
                MessageBox.Show(string.Format("Done! Deteled {0} lines.", count));
            }
        }

        private void btnClearPortfolioDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("警告！是否确定要清空投资组合数据库？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                PortfolioManager manager = new PortfolioManager();
                int count = manager.DeleteAll();
                MessageBox.Show(string.Format("Done! Deteled {0} lines.", count));
            }
        }

        private void btnClearAccountStatusDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("警告！是否确定要清空账户状态数据库？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                AccountStatusManager manager = new AccountStatusManager();
                int count = manager.DeleteAll();
                MessageBox.Show(string.Format("Done! Deteled {0} lines.", count));
            }
        }

        private void btnClearStockDayPriceDB_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("警告！是否确定要清空股票日线数据库？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                StockDayPriceManager manager = new StockDayPriceManager();
                int count = manager.DeleteAll();
                MessageBox.Show(string.Format("Done! Deteled {0} lines.", count));
            }
        }

        private void UCtlConfiguration_Load(object sender, EventArgs e)
        {
            //查询checklist数据库的最大日期，并显示在文本框中
            CheckListManager manager = new CheckListManager();
            if (manager.GetCount() == 0)
            {
                this.txtMaxDate.Text = "There is no checklist.";
            }
            else
            {
                this.txtMaxDate.Text = manager.GetMaxDate().ToShortDateString();
            }
        }
    }
}
