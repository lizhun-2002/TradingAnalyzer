using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TradingAnalyzer.Model;
using TradingAnalyzer.BLL;

namespace TradingAnalyzer.UI
{
    public partial class UCtlYieldCurve : UserControl
    {
        public UCtlYieldCurve()
        {
            InitializeComponent();
        }

        private void UCtlYieldCurve_Load(object sender, EventArgs e)
        {
            this.InitChartYieldCurve();
            this.InitChartUnitNetWorth();

        }

        public void InitChartYieldCurve()
        {
            this.chartYieldCurve.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            this.chartYieldCurve.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            this.chartYieldCurve.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

            this.chartYieldCurve.Series.Clear();
            List<AccountStatus> chartData = new AccountStatusManager().GetAll().ToList<AccountStatus>();
            Series series = new Series("总收益");
            series.ChartType = SeriesChartType.Line;
            foreach (AccountStatus accountStatus in chartData)
            {
                series.Points.AddXY(accountStatus.Date, accountStatus.TotalAsset - accountStatus.TotalCost);
            }
            series.BorderWidth = 2;
            //series.MarkerStyle = MarkerStyle.Circle;
            //series.MarkerSize = 5;
            this.chartYieldCurve.Series.Add(series);
        }

        public void InitChartUnitNetWorth()
        {
            Dictionary<DateTime, int> dateNum;
            Dictionary<DateTime, double> dateUnitNetWorth;
            Dictionary<DateTime, double> dateTotalShare;
            new ChartDataManager().GetDataForFundUnitNetWorthChart(out dateNum, out dateUnitNetWorth,out dateTotalShare);

            this.chartUnitNetWorth.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            this.chartUnitNetWorth.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            //this.chartUnitNetWorth.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            this.chartUnitNetWorth.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            this.chartUnitNetWorth.ChartAreas[0].AxisY.IsStartedFromZero = false;
            this.chartUnitNetWorth.ChartAreas[0].Name = "areaUnitNetWorth";

            this.chartUnitNetWorth.Series.Clear();

            Series series = new Series("单位净值");
            series.ChartType = SeriesChartType.Line;
            foreach (DateTime key in dateNum.Keys )
            {
                series.Points.AddXY(dateNum[key], dateUnitNetWorth[key]);
            }
            series.BorderWidth = 2;
            //series.MarkerStyle = MarkerStyle.Circle;
            //series.MarkerSize = 5;
            this.chartUnitNetWorth.Series.Add(series);

            ChartArea chartArea2= new ChartArea();
            //注意：先把chartArea2添加到ChartAreas集合中，再设置属性。否则“chartArea2.AxisX.LabelStyle.Angle = -45”无效
            this.chartUnitNetWorth.ChartAreas.Add(chartArea2);
            chartArea2.Name = "areaTotalShare";
            chartArea2.AlignWithChartArea = "areaUnitNetWorth";
            chartArea2.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea2.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chartArea2.AxisX.LabelStyle.Angle = -45;

            Series seriesTotalShare = new Series("总份额");
            seriesTotalShare.ChartArea = "areaTotalShare";
            seriesTotalShare.ChartType = SeriesChartType.Column;
            foreach (DateTime key in dateNum.Keys)
            {
                DataPoint point = new DataPoint();
                point.SetValueXY(dateNum[key], dateTotalShare[key]);
                point.AxisLabel = key.ToShortDateString();
                seriesTotalShare.Points.Add(point);
            }
            this.chartUnitNetWorth.Series.Add(seriesTotalShare);
        }

        /// <summary>
        /// 初始化Char控件样式
        /// </summary>
        //public void InitializeChart()
        //{
        //    #region 设置图表的属性
        //    //图表的背景色
        //    this.chartYieldCurve.BackColor = Color.FromArgb(211, 223, 240);
        //    //图表背景色的渐变方式
        //    this.chartYieldCurve.BackGradientStyle = GradientStyle.TopBottom;
        //    //图表的边框颜色、
        //    this.chartYieldCurve.BorderlineColor = Color.FromArgb(26, 59, 105);
        //    //图表的边框线条样式
        //    this.chartYieldCurve.BorderlineDashStyle = ChartDashStyle.Solid;
        //    //图表边框线条的宽度
        //    this.chartYieldCurve.BorderlineWidth = 2;
        //    //图表边框的皮肤
        //    this.chartYieldCurve.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
        //    #endregion

        //    #region 设置图表的Title
        //    Title title = new Title();
        //    //标题内容
        //    title.Text = "总资产曲线";
        //    //标题的字体
        //    title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Bold);
        //    //标题字体颜色
        //    title.ForeColor = Color.FromArgb(26, 59, 105);
        //    //标题阴影颜色
        //    title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
        //    //标题阴影偏移量
        //    title.ShadowOffset = 3;

        //    this.chartYieldCurve.Titles.Add(title);
        //    #endregion

        //    #region 设置图表区属性
        //    //图表区的名字
        //    ChartArea chartArea = new ChartArea("Default");
        //    //背景色
        //    chartArea.BackColor = Color.FromArgb(64, 165, 191, 228);
        //    //背景渐变方式
        //    chartArea.BackGradientStyle = GradientStyle.TopBottom;
        //    //渐变和阴影的辅助背景色
        //    chartArea.BackSecondaryColor = Color.White;
        //    //边框颜色
        //    chartArea.BorderColor = Color.FromArgb(64, 64, 64, 64);
        //    //阴影颜色
        //    chartArea.ShadowColor = Color.Transparent;

        //    //设置X轴和Y轴线条的颜色和宽度
        //    chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
        //    chartArea.AxisX.LineWidth = 1;
        //    chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
        //    chartArea.AxisY.LineWidth = 1;

        //    //设置X轴和Y轴的标题
        //    chartArea.AxisX.Title = "日期";
        //    chartArea.AxisY.Title = "总资产";

        //    //设置图表区网格横纵线条的颜色和宽度
        //    chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
        //    chartArea.AxisX.MajorGrid.LineWidth = 1;
        //    chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
        //    chartArea.AxisY.MajorGrid.LineWidth = 1;

        //    this.chartYieldCurve.ChartAreas.Add(chartArea);
        //    #endregion

        //    #region 图例及图例的位置
        //    Legend legend = new Legend();
        //    legend.Alignment = StringAlignment.Center;
        //    legend.Docking = Docking.Bottom;

        //    this.chartYieldCurve.Legends.Add(legend);
        //    #endregion
        //}
    }
}
