namespace TradingAnalyzer
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("系统配置");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("账户信息");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("账户状态历史");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("组合持仓历史");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("交割单");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("信息查询", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("收益率曲线");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("交易轨迹");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("图表", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(1102, 673);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "NodeConfiguration";
            treeNode1.Text = "系统配置";
            treeNode2.Name = "NodeAccountInformation";
            treeNode2.Text = "账户信息";
            treeNode3.Name = "NodeAccountStatus";
            treeNode3.Text = "账户状态历史";
            treeNode4.Name = "NodePortfolio";
            treeNode4.Text = "组合持仓历史";
            treeNode5.Name = "NodeCheckList";
            treeNode5.Text = "交割单";
            treeNode6.Name = "NodeInformation";
            treeNode6.Text = "信息查询";
            treeNode7.Name = "NodeYieldCurve";
            treeNode7.Text = "收益率曲线";
            treeNode8.Name = "NodeTradingTrace";
            treeNode8.Text = "交易轨迹";
            treeNode9.Name = "NodeChart";
            treeNode9.Text = "图表";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode9});
            this.treeView1.Size = new System.Drawing.Size(154, 673);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 673);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trading Analyzer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;

    }
}

