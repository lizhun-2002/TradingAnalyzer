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
using TradingAnalyzer.Model;
using System.Windows.Forms.DataVisualization.Charting;

namespace TradingAnalyzer.UI
{
    public partial class UCtlTradingTrace : UserControl
    {
        public UCtlTradingTrace()
        {
            InitializeComponent();
        }

        private void UCtlTradingTrace_Load(object sender, EventArgs e)
        {
            this.InitUCtlTradingTrace();
        }

        //初始化自定义控件UCtlTradingTrace
        private void InitUCtlTradingTrace()
        {
            this.txtStockCode.Text = "请输入股票代码";
            this.dgvCheckList.DataSource = null;
            this.InitChartCandel();
        }

        //更新自定义控件UCtlTradingTrace
        public void UpdateUCtlTradingTrace(string code)
        {
            this.txtStockCode.Text = code;
            
            //查询交易记录
            List<CheckList> checkListData = new CheckListManager().GetByCode(code).ToList<CheckList>();
            this.dgvCheckList.DataSource = checkListData;

            //设置图形数据
            this.SetDataToChartCandel(code);
        }

        private void InitChartCandel()
        {
            #region 设置图形
            this.chartCandel.ChartAreas.Clear();
            this.chartCandel.Series.Clear();
            this.chartCandel.Titles.Clear();

            ChartArea chartArea1 = new ChartArea();
            this.chartCandel.ChartAreas.Add(chartArea1);
            chartArea1.Name = "areaPrice";
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chartArea1.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            //this.chartCandel.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            //chartArea1.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;


            // Enable range selection and zooming end user interface
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            //this.chartCandel.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //this.chartCandel.ChartAreas[0].AxisX.ScaleView.Zoom(1, 3);

            //设置滚动条
            this.chartCandel.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            this.chartCandel.ChartAreas[0].AxisX.ScrollBar.Size = 10;
            this.chartCandel.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;

            // 设置自动放大与缩小的最小量
            //this.chartCandel.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = double.NaN;
            //this.chartCandel.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 2; 
            #endregion

            #region 显示K线
            Series price = new Series("Price");
            this.chartCandel.Series.Add(price);
            price.ChartType = SeriesChartType.Candlestick;
            price.ChartArea = "areaPrice";
            price.YValuesPerPoint = 4;
            price.XValueType = ChartValueType.String;

            // Set the style of the open-close marks
            //price["OpenCloseStyle"] = "Triangle";
            // Show both open and close marks
            //price["ShowOpenClose"] = "Both";
            // Set point width
            //series["PointWidth"] = "1.0";
            // Set colors bars
            price["PriceUpColor"] = "Red";
            price["PriceDownColor"] = "Green";
            #endregion

            #region 显示交易记录
            Series tradingRecord = new Series("Trading Record");
            this.chartCandel.Series.Add(tradingRecord);
            tradingRecord.ChartType = SeriesChartType.Point;
            tradingRecord.ChartArea = "areaPrice";
            #endregion
            
            #region 显示成交量
            ChartArea chartArea2 = new ChartArea();
            this.chartCandel.ChartAreas.Add(chartArea2);
            chartArea2.Name = "areaVolume";
            chartArea2.AlignWithChartArea = "areaPrice";
            chartArea2.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chartArea2.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
            chartArea2.AxisX.LabelStyle.Angle = -45;

            Series volume = new Series("Volume");
            this.chartCandel.Series.Add(volume);
            volume.ChartType = SeriesChartType.Column;
            volume.ChartArea = "areaVolume";
            #endregion
        }

        private void SetDataToChartCandel(string code)
        {
            List<CheckList> checkListData;
            List<StockDayPrice> stockData;
            //定义一个日期到整数映射的字典，用整数替代日期作为K线横轴。避免交易休市造成的空隙。
            Dictionary<DateTime, int> dateMapToInt;
            new ChartDataManager().GetDataForCandelWithTradingTraceChart(code, out checkListData, out stockData, out dateMapToInt);
            //无数据则直接返回（新股的情形）
            if (dateMapToInt.Count == 0)
            { 
                return; 
            }

            //设置图形标题
            this.chartCandel.Titles.Clear();
            this.chartCandel.Titles.Add(code);

            #region K线
            this.chartCandel.Series["Price"].Points.Clear();
            foreach (StockDayPrice stockDayPrice in stockData)
            {
                //price.Points.AddXY(stockDayPrice.Date, stockDayPrice.High, stockDayPrice.Low, stockDayPrice.Open, stockDayPrice.Close);
                DataPoint candle = new DataPoint();
                candle.SetValueXY(dateMapToInt[stockDayPrice.Date], stockDayPrice.High, stockDayPrice.Low, stockDayPrice.Open, stockDayPrice.Close);
                if (stockDayPrice.Close >= stockDayPrice.Open)
                {
                    candle.Color = Color.Red;
                }
                else
                {
                    candle.Color = Color.Green;
                }
                //用日期做x轴label
                candle.AxisLabel = stockDayPrice.Date.ToShortDateString();
                this.chartCandel.Series["Price"].Points.Add(candle);
            }
            #endregion

            #region 交易记录
            this.chartCandel.Series["Trading Record"].Points.Clear();
            foreach (CheckList checkList in checkListData)
            {
                DataPoint tradingPoint = new DataPoint();
                tradingPoint.SetValueXY(dateMapToInt[checkList.Date], checkList.TradePrice);
                tradingPoint.MarkerStyle = MarkerStyle.Circle;
                tradingPoint.MarkerBorderColor = Color.Black;
                if (checkList.TradeDirection == 1)
                {
                    tradingPoint.MarkerColor = Color.Red;
                }
                else
                {
                    tradingPoint.MarkerColor = Color.Green;
                }
                //除权除息记录暂时处理如下
                if (checkList.TradePrice == 0)
                {
                    tradingPoint.IsEmpty = true;
                }
                this.chartCandel.Series["Trading Record"].Points.Add(tradingPoint);
            }
            #endregion

            #region 成交量
            this.chartCandel.Series["Volume"].Points.Clear();
            foreach (StockDayPrice stockDayPrice in stockData)
            {
                DataPoint bar = new DataPoint();
                bar.SetValueXY(dateMapToInt[stockDayPrice.Date], stockDayPrice.Volume);
                if (stockDayPrice.Close >= stockDayPrice.Open)
                {
                    bar.Color = Color.Red;
                }
                else
                {
                    bar.Color = Color.Green;
                }
                //用日期做x轴label
                bar.AxisLabel = stockDayPrice.Date.ToShortDateString();
                this.chartCandel.Series["Volume"].Points.Add(bar);
            }
            #endregion
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.UpdateUCtlTradingTrace(this.txtStockCode.Text);
        }

        private void dgvCheckList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            if (e.RowIndex >= 0)
                {
                    //操作
                    this.SetDataToChartCandel(this.txtStockCode.Text);
                    DataPoint selectedPoint = this.chartCandel.Series["Trading Record"].Points[e.RowIndex];
                    if ((int)this.dgvCheckList.Rows[e.RowIndex].Cells[2].Value == 1)
                    {
                        selectedPoint.MarkerColor = Color.OrangeRed; //Color.FromArgb(255, 200, 200);
                    }
                    else
                    {
                        selectedPoint.MarkerColor = Color.GreenYellow;
                    }
                    
                    selectedPoint.MarkerSize = 10;
                    selectedPoint.IsValueShownAsLabel = true;
                }
            //}
        }

        private void btnAllStockList_Click(object sender, EventArgs e)
        {
            bool isFormExist = false;
            foreach (Form form in this.ParentForm.OwnedForms)
            {
                if (form.Name == "formAllStock")
                {
                    isFormExist = true;
                    form.Show();
                }
            }
            if (isFormExist)
            {
                return;
            }
             else
            {
                Form formAllStock = new Form();
                formAllStock.Name = "formAllStock";
                formAllStock.Owner = this.ParentForm;
                //子窗体大小固定
                formAllStock.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                formAllStock.ShowInTaskbar = false;
                formAllStock.MaximizeBox = false;
                formAllStock.MinimizeBox = false;
                formAllStock.Width = 276;
                formAllStock.Height = this.ParentForm.Height;
                //子窗体贴在主窗体右侧边缘显示
                formAllStock.StartPosition = FormStartPosition.Manual;
                Point startPosition = new Point();
                startPosition.X = this.ParentForm.Location.X + this.ParentForm.Width;
                startPosition.Y = this.ParentForm.Location.Y;
                formAllStock.Location = startPosition;

                BindingSource bs = new BindingSource();
                bs.DataSource = new CheckListManager().GetTradedStock();
                DataGridView dgvAllStock = new DataGridView();
                dgvAllStock.Name = "dgvAllStockList";
                dgvAllStock.Parent = formAllStock;
                dgvAllStock.Dock = DockStyle.Fill;
                dgvAllStock.ReadOnly = true;
                dgvAllStock.DataSource = bs;
                formAllStock.Show();
                dgvAllStock.SelectionChanged += dgvAllStock_SelectionChanged;
            }

        }

        void dgvAllStock_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            string code = dgv.CurrentRow.Cells[0].Value.ToString();
            this.UpdateUCtlTradingTrace(code);
        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            string code = this.txtStockCode.Text;
            bool traded = new CheckListManager().HasBeenTraded(code);
            if (traded == true)
            {
                List<CheckList>  checkListData = new CheckListManager().GetByCode(code).ToList<CheckList>();
                DateTime updateBeginDate = checkListData[0].Date.AddDays(-30);
                DateTime updateEndDate = checkListData[checkListData.Count - 1].Date.AddDays(30);
                new StockDayPriceManager().AutoUpdateByCodeName(code, checkListData[0].Name, updateBeginDate, updateEndDate);
                //设置图形数据
                this.SetDataToChartCandel(code);
            }

        }








    }
}
