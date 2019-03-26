namespace TradingAnalyzer.UI
{
    partial class UCtlYieldCurve
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartYieldCurve = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartUnitNetWorth = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartYieldCurve)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartUnitNetWorth)).BeginInit();
            this.SuspendLayout();
            // 
            // chartYieldCurve
            // 
            chartArea1.Name = "ChartArea1";
            this.chartYieldCurve.ChartAreas.Add(chartArea1);
            this.chartYieldCurve.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartYieldCurve.Legends.Add(legend1);
            this.chartYieldCurve.Location = new System.Drawing.Point(3, 17);
            this.chartYieldCurve.Name = "chartYieldCurve";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartYieldCurve.Series.Add(series1);
            this.chartYieldCurve.Size = new System.Drawing.Size(726, 191);
            this.chartYieldCurve.TabIndex = 0;
            this.chartYieldCurve.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chartYieldCurve);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 211);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "总收益曲线";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chartUnitNetWorth);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(732, 222);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "单位净值曲线";
            // 
            // chartUnitNetWorth
            // 
            chartArea2.Name = "ChartArea1";
            this.chartUnitNetWorth.ChartAreas.Add(chartArea2);
            this.chartUnitNetWorth.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartUnitNetWorth.Legends.Add(legend2);
            this.chartUnitNetWorth.Location = new System.Drawing.Point(3, 17);
            this.chartUnitNetWorth.Name = "chartUnitNetWorth";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartUnitNetWorth.Series.Add(series2);
            this.chartUnitNetWorth.Size = new System.Drawing.Size(726, 202);
            this.chartUnitNetWorth.TabIndex = 0;
            this.chartUnitNetWorth.Text = "chart1";
            // 
            // UCtlYieldCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UCtlYieldCurve";
            this.Size = new System.Drawing.Size(732, 433);
            this.Load += new System.EventHandler(this.UCtlYieldCurve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartYieldCurve)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartUnitNetWorth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartYieldCurve;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUnitNetWorth;
    }
}
