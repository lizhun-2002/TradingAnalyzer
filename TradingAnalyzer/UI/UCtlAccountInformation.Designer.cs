namespace TradingAnalyzer.UI
{
    partial class UCtlAccountInformation
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelAvailableMoney = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelMarketValue = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelChange = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTotalAsset = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dGVPortfolio = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看交易轨迹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVPortfolio)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelAvailableMoney);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.labelMarketValue);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelChange);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelTotalAsset);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "账户状态";
            // 
            // labelAvailableMoney
            // 
            this.labelAvailableMoney.AutoSize = true;
            this.labelAvailableMoney.Location = new System.Drawing.Point(367, 65);
            this.labelAvailableMoney.Name = "labelAvailableMoney";
            this.labelAvailableMoney.Size = new System.Drawing.Size(41, 12);
            this.labelAvailableMoney.TabIndex = 7;
            this.labelAvailableMoney.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(280, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "可用余额：";
            // 
            // labelMarketValue
            // 
            this.labelMarketValue.AutoSize = true;
            this.labelMarketValue.Location = new System.Drawing.Point(99, 65);
            this.labelMarketValue.Name = "labelMarketValue";
            this.labelMarketValue.Size = new System.Drawing.Size(41, 12);
            this.labelMarketValue.TabIndex = 5;
            this.labelMarketValue.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "总市值：";
            // 
            // labelChange
            // 
            this.labelChange.AutoSize = true;
            this.labelChange.Location = new System.Drawing.Point(367, 28);
            this.labelChange.Name = "labelChange";
            this.labelChange.Size = new System.Drawing.Size(41, 12);
            this.labelChange.TabIndex = 3;
            this.labelChange.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "当日变动：";
            // 
            // labelTotalAsset
            // 
            this.labelTotalAsset.AutoSize = true;
            this.labelTotalAsset.Location = new System.Drawing.Point(99, 28);
            this.labelTotalAsset.Name = "labelTotalAsset";
            this.labelTotalAsset.Size = new System.Drawing.Size(41, 12);
            this.labelTotalAsset.TabIndex = 1;
            this.labelTotalAsset.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "总资产：";
            // 
            // dGVPortfolio
            // 
            this.dGVPortfolio.AllowUserToAddRows = false;
            this.dGVPortfolio.AllowUserToDeleteRows = false;
            this.dGVPortfolio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVPortfolio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGVPortfolio.Location = new System.Drawing.Point(3, 17);
            this.dGVPortfolio.Name = "dGVPortfolio";
            this.dGVPortfolio.ReadOnly = true;
            this.dGVPortfolio.RowTemplate.Height = 23;
            this.dGVPortfolio.Size = new System.Drawing.Size(628, 166);
            this.dGVPortfolio.TabIndex = 1;
            this.dGVPortfolio.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dGVPortfolio_CellMouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dGVPortfolio);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 186);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "投资组合";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(19, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.Value = new System.DateTime(2015, 10, 12, 0, 0, 0, 0);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(274, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateTimePicker1);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(634, 63);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "日期";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看交易轨迹ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // 查看交易轨迹ToolStripMenuItem
            // 
            this.查看交易轨迹ToolStripMenuItem.Name = "查看交易轨迹ToolStripMenuItem";
            this.查看交易轨迹ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.查看交易轨迹ToolStripMenuItem.Text = "查看交易轨迹";
            this.查看交易轨迹ToolStripMenuItem.Click += new System.EventHandler(this.查看交易轨迹ToolStripMenuItem_Click);
            // 
            // UCtlAccountInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "UCtlAccountInformation";
            this.Size = new System.Drawing.Size(634, 346);
            this.Load += new System.EventHandler(this.UCtlAccountInformation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVPortfolio)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dGVPortfolio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelAvailableMoney;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelMarketValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelChange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelTotalAsset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看交易轨迹ToolStripMenuItem;
    }
}
