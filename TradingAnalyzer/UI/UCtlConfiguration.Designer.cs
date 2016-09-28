namespace TradingAnalyzer.UI
{
    partial class UCtlConfiguration
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
            this.btnImportCheckList = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMaxDate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearCheckListDB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClearPortfolioDB = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearAccountStatusDB = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClearStockDayPriceDB = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImportCheckList
            // 
            this.btnImportCheckList.Location = new System.Drawing.Point(6, 57);
            this.btnImportCheckList.Name = "btnImportCheckList";
            this.btnImportCheckList.Size = new System.Drawing.Size(127, 23);
            this.btnImportCheckList.TabIndex = 0;
            this.btnImportCheckList.Text = "导入交割单";
            this.btnImportCheckList.UseVisualStyleBackColor = true;
            this.btnImportCheckList.Click += new System.EventHandler(this.btnImportCheckList_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaxDate);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnClearCheckListDB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnImportCheckList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(607, 125);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "交割单数据管理";
            // 
            // txtMaxDate
            // 
            this.txtMaxDate.BackColor = System.Drawing.SystemColors.Window;
            this.txtMaxDate.Location = new System.Drawing.Point(131, 24);
            this.txtMaxDate.Name = "txtMaxDate";
            this.txtMaxDate.ReadOnly = true;
            this.txtMaxDate.Size = new System.Drawing.Size(100, 21);
            this.txtMaxDate.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "交割单已更新至";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "说明：程序将清空交割单数据库，请谨慎操作。";
            // 
            // btnClearCheckListDB
            // 
            this.btnClearCheckListDB.Location = new System.Drawing.Point(6, 87);
            this.btnClearCheckListDB.Name = "btnClearCheckListDB";
            this.btnClearCheckListDB.Size = new System.Drawing.Size(127, 23);
            this.btnClearCheckListDB.TabIndex = 2;
            this.btnClearCheckListDB.Text = "清空交割单数据库";
            this.btnClearCheckListDB.UseVisualStyleBackColor = true;
            this.btnClearCheckListDB.Click += new System.EventHandler(this.btnClearCheckListDB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "说明：请选择交割单文件(.txt/.xls)。程序将自动更新组合与账户数据。";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnClearPortfolioDB);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(607, 59);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "组合数据管理";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(269, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "说明：程序将清空投资组合数据库，请谨慎操作。";
            // 
            // btnClearPortfolioDB
            // 
            this.btnClearPortfolioDB.Location = new System.Drawing.Point(6, 20);
            this.btnClearPortfolioDB.Name = "btnClearPortfolioDB";
            this.btnClearPortfolioDB.Size = new System.Drawing.Size(127, 23);
            this.btnClearPortfolioDB.TabIndex = 4;
            this.btnClearPortfolioDB.Text = "清空投资组合数据库";
            this.btnClearPortfolioDB.UseVisualStyleBackColor = true;
            this.btnClearPortfolioDB.Click += new System.EventHandler(this.btnClearPortfolioDB_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.btnClearAccountStatusDB);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 184);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(607, 54);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "账户数据管理";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "说明：程序将清空账户状态数据库，请谨慎操作。";
            // 
            // btnClearAccountStatusDB
            // 
            this.btnClearAccountStatusDB.Location = new System.Drawing.Point(6, 20);
            this.btnClearAccountStatusDB.Name = "btnClearAccountStatusDB";
            this.btnClearAccountStatusDB.Size = new System.Drawing.Size(127, 23);
            this.btnClearAccountStatusDB.TabIndex = 6;
            this.btnClearAccountStatusDB.Text = "清空账户状态数据库";
            this.btnClearAccountStatusDB.UseVisualStyleBackColor = true;
            this.btnClearAccountStatusDB.Click += new System.EventHandler(this.btnClearAccountStatusDB_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.btnClearStockDayPriceDB);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 238);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(607, 52);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "股票日线数据管理";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(269, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "说明：程序将清空股票日线数据库，请谨慎操作。";
            // 
            // btnClearStockDayPriceDB
            // 
            this.btnClearStockDayPriceDB.Location = new System.Drawing.Point(6, 20);
            this.btnClearStockDayPriceDB.Name = "btnClearStockDayPriceDB";
            this.btnClearStockDayPriceDB.Size = new System.Drawing.Size(127, 23);
            this.btnClearStockDayPriceDB.TabIndex = 6;
            this.btnClearStockDayPriceDB.Text = "清空股票日线数据库";
            this.btnClearStockDayPriceDB.UseVisualStyleBackColor = true;
            this.btnClearStockDayPriceDB.Click += new System.EventHandler(this.btnClearStockDayPriceDB_Click);
            // 
            // UCtlConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UCtlConfiguration";
            this.Size = new System.Drawing.Size(607, 362);
            this.Load += new System.EventHandler(this.UCtlConfiguration_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportCheckList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClearCheckListDB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClearPortfolioDB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClearAccountStatusDB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClearStockDayPriceDB;
        private System.Windows.Forms.TextBox txtMaxDate;
        private System.Windows.Forms.Label label6;
    }
}
