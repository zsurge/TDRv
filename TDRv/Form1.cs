using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TDRv.Driver;

namespace TDRv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        //设置参数设置窗体的表数据
        DataTable gdt;

        //获取当前
        public string exPortFilePath = string.Empty;

        //单端
        public const int SINGLE = 1;

        //差分
        public const int DIFFERENCE = 2;

        //流水
        public int gSerialInc = 0;

        E5080B analyzer = new E5080B();

     
        //配方列表
        List<TestResult> paramList = new List<TestResult>();

        //记录已测试到第几层
        MeasIndex measIndex = new MeasIndex();

        private void tsb_DevConnect_Click(object sender, EventArgs e)
        {
            DevConnectSet devConnectSet = new DevConnectSet();
            devConnectSet.ChangeValue += new DevConnectSet.ChangeTsbHandler(Change_Tsb_Index);
            devConnectSet.Show();
        }

        //根据连接设置传过来的值，控制是否可以使用
        public void Change_Tsb_Index(string addr)
        {
            if (addr.Length == 0)
            {
                MessageBox.Show("错误的设备地址");
            }
            int error = analyzer.Open(addr);
            string idn = string.Empty;
            error = analyzer.GetInstrumentIdentifier(out idn);

            if (error == 0)
            {
                tsb_GetTestIndex.Enabled = true;
                tsb_StartTest.Enabled = true;
                optStatus.isConnect = true;
            }
            else
            {
                MessageBox.Show("错误的设备地址");
            }
        }


        private void tsb_DevOptSet_Click(object sender, EventArgs e)
        {
            DevOptSet devOptSet = new DevOptSet();
            devOptSet.Show();
        }

        private void tsb_DevParamSet_Click(object sender, EventArgs e)
        {           
            DevParamSet devParamSet = new DevParamSet(gdt);
            devParamSet.ChangeDgv += new DevParamSet.ChangeDgvHandler(Change_DataGridView);
            devParamSet.Show();
        }

        //子窗体传回配方参数
        public void Change_DataGridView(DataGridView dt)
        {
            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();

            //获取开路定义起始位置
            GetIndexStart(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int index = dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells[1].Value = dt.Rows[i].Cells[1].Value;
                dataGridView1.Rows[index].Cells[2].Value = dt.Rows[i].Cells[2].Value;
                dataGridView1.Rows[index].Cells[3].Value = dt.Rows[i].Cells[3].Value;
            }
        }


        /// <summary>
        /// 将DataGridView里面的数据提取到DataTable中
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public DataTable DataGridViewToDataTable(DataGridView dataGridView)
        {
            var dataTable = new DataTable();

            for (var i = 0; i < dataGridView.RowCount; i++)
            {
                var dr = dataTable.NewRow();
                for (var j = 0; j < dataGridView.ColumnCount; j++)
                {
                    if (i == 0)
                    {
                        var dc = new DataColumn(dataGridView.Columns[j].Name);
                        dataTable.Columns.Add(dc);
                    }

                    dr[j] = dataGridView[j, i].Value;
                }
                dataTable.Rows.Add(dr);
            }

            return dataTable;
        }

        //获取配方中开路位置信息
        //并且记录所有的待测参数
        public void GetIndexStart(DataGridView dt)
        {
            bool single = true;
            bool diff = true;

            gdt = DataGridViewToDataTable(dt);

            //赋值总的层数
            measIndex.total = dt.Rows.Count;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TestResult tr = new TestResult();

                if (string.Compare(dt.Rows[i].Cells[10].Value.ToString(), "Differential") == 0 && diff)
                {
                    MeasPosition.tdd11start = Convert.ToInt32(dt.Rows[i].Cells[14].Value);
                    diff = false;
                    tr.DevMode = DIFFERENCE;
                }

                if (string.Compare(dt.Rows[i].Cells[10].Value.ToString(), "SingleEnded") == 0 && single)
                {
                    MeasPosition.tdd22start = Convert.ToInt32(dt.Rows[i].Cells[14].Value);
                    single = false;
                    tr.DevMode = SINGLE;
                }

                if (string.Compare(dt.Rows[i].Cells[10].Value.ToString(), "SingleEnded") == 0)
                {
                    tr.DevMode = SINGLE;
                }
                else
                {
                    tr.DevMode = DIFFERENCE;
                }
                
                tr.Layer = dt.Rows[i].Cells["Layer"].Value.ToString();
                tr.Spec = dt.Rows[i].Cells["ImpedanceDefine"].Value.ToString();
                tr.Upper_limit = dt.Rows[i].Cells["ImpedanceLimitUpper"].Value.ToString();
                tr.Low_limit = dt.Rows[i].Cells["ImpedanceLimitLower"].Value.ToString();
                //tr.Average = dt.Rows[i].Cells[].Value.ToString();
                //tr.Max = dt.Rows[i].Cells[].Value.ToString();
                //tr.Min = dt.Rows[i].Cells[].Value.ToString();
                //tr.Result = dt.Rows[i].Cells[].Value.ToString();
                //tr.Serial = dt.Rows[i].Cells[].Value.ToString();
                //tr.Date = dt.Rows[i].Cells[].Value.ToString();
                //tr.Time = dt.Rows[i].Cells[].Value.ToString();
                tr.Valid_Begin = dt.Rows[i].Cells["TestFromThreshold"].Value.ToString();
                tr.Valid_End = dt.Rows[i].Cells["TestToThreshold"].Value.ToString();
                tr.Mode = dt.Rows[i].Cells["InputMode"].Value.ToString();
                tr.Std = dt.Rows[i].Cells["DataPointCheck"].Value.ToString();
                //tr.Curve_data = dt.Rows[i].Cells["SaveCurve"].Value.ToString();
                //tr.Curve_image = dt.Rows[i].Cells["SaveImage"].Value.ToString();
                tr.Curve_data = dt.Rows[i].Cells["RecordPath"].Value.ToString();
                tr.Curve_image = dt.Rows[i].Cells["RecordPath"].Value.ToString();
                
                paramList.Add(tr);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //获取序列号起始值
            gSerialInc =  Convert.ToInt32(optParam.snBegin);

            initChart();

            //禁止列排序
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            for (int i = 0; i < dgv_CurrentResult.Columns.Count; i++)
            {
                dgv_CurrentResult.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void initChart()
        {
            //chart1.Series.Clear();
            chart1.Series[0].LegendText = "TDR Curve";
            chart1.Series[1].LegendText = "limit";
            chart1.Series[2].LegendText = "limit";
            chart1.Series[3].LegendText = "100";
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].BorderWidth = 2;
            chart1.Series[1].BorderWidth = 2;
            chart1.Series[2].BorderWidth = 2;
            chart1.Series[3].BorderWidth = 2;

            //背景黑色
            chart1.BackColor = Color.Black;
            chart1.ChartAreas[0].BackColor = Color.Black;

            //XY标签改为白色
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            //X，Y轴白色
            chart1.ChartAreas[0].AxisX.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.LineColor = Color.White;

            //差分线条黄色
            chart1.Series[0].Color = Color.Yellow; //线条颜色

            //有效区域线红色
            chart1.Series[1].Color = Color.Red; //线条颜色
            chart1.Series[2].Color = Color.Red; //线条颜色

            //单端线条蓝色
            chart1.Series[3].Color = Color.Blue; //线条颜色

            //网格线颜色白色
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
        }

        //开路定义
        private void tsb_GetTestIndex_Click(object sender, EventArgs e)
        {
            List<float> tmpDiffMeasData = new List<float>();
            List<float> tmpSingleMeasData = new List<float>();
            string result = string.Empty;

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("请先装载配方");
                return;
            }

            if (!optStatus.isConnect)
            {
                MessageBox.Show("请先连接设备");
                return;
            }

            //string cmd2 = "FORM:DATA ASCII";
            //analyzer.ExecuteCmd(cmd2);

            //string cmd3 = "MMEM:STOR:TRAC:FORM:SNP MA";
            //analyzer.ExecuteCmd(cmd3);

            string cmd4 = "CALCulate:PARameter:CAT?";
            analyzer.QueryCommand(cmd4, out result, 256);

            string cmd5 = ":CALCulate1:TRANsform:TIME:STARt -5E-10";
            analyzer.ExecuteCmd(cmd5);

            string cmd6 = ":CALCulate1:TRANsform:TIME:STOP 9.5E-9";
            analyzer.ExecuteCmd(cmd6);

            string cmd7 = ":SENSe1:SWEep:POINts?";
            analyzer.QueryCommand(cmd7, out result, 256);

            string cmd8 = ":CALCulate1:TRANsform:TIME:STARt?";
            analyzer.QueryCommand(cmd8, out result, 256);

            string cmd9 = ":CALCulate1:TRANsform:TIME:STOP?";
            analyzer.QueryCommand(cmd9, out result, 256);

            string cmd10 = ":INITiate1:CONTinuous ON";
            analyzer.ExecuteCmd(cmd10);

            string cmd11 = ":CALC:PAR:SEL \"win1_tr2\"";
            analyzer.ExecuteCmd(cmd11);
            analyzer.viClear();

            result = string.Empty;
            string cmd12 = ":CALCulate1:DATA? FDATa";
            analyzer.QueryCommand(cmd12, out result, 200000);

            tmpSingleMeasData = packetMaesData(result, 0, 0);
            string[] tdd22_array = result.Split(new char[] { ',' });
            result = string.Empty;

            if (tdd22_array.Length < 200)
            {
                MessageBox.Show("获取差分开路定义失败");
            }

            //查找tdd22单端的索引值
            for (int i = 0; i < tdd22_array.Length; i++)
            {
                //logger.Trace(tdd22_array[i]);
                if (Convert.ToSingle(tdd22_array[i]) >= Convert.ToSingle(MeasPosition.tdd22start))
                {            
                    MeasPosition.tdd22IndexValue = i - 1;
                    //这里需要将开路定义后的索引写入到配方的XML文件中去
                    break;
                }
            }



            string cmd13 = ":CALC:PAR:SEL \"win1_tr1\"";
            analyzer.ExecuteCmd(cmd13);
            analyzer.viClear();

            result = string.Empty;
            string cmd14 = ":CALCulate1:DATA? FDATa";
            analyzer.QueryCommand(cmd14, out result, 200000);
            tmpDiffMeasData = packetMaesData(result, 0, 0);
            string[] tdd11_array = result.Split(new char[] { ',' });

            //查找tdd11差分的索引值
            for (int i = 0; i < tdd11_array.Length; i++)
            {
                //logger.Info(tdd11_array[i]);
                if (Convert.ToSingle(tdd11_array[i]) >= Convert.ToSingle(MeasPosition.tdd11start))
                {
                    MeasPosition.tdd11IndexValue = i - 1;
                    //这里需要将开路定义后的索引写入到配方的XML文件中去
                    break;
                }
            }

            result = string.Empty;

            MeasPosition.isOpen = true;
            optStatus.isGetIndex = true;
 
            CreateInitMeasChart(tmpDiffMeasData, tmpSingleMeasData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="measBuff">量测的数据</param>
        /// <param name="index">开路的位置</param>
        /// <param name="mode">测试模式 单端or差分</param>
        /// <returns></returns>
        private List<float> packetMaesData(string measBuff, int index, int mode)
        {
            List<float> result = new List<float>();
            string[] tmpArray = measBuff.Split(new char[] { ',' });
            float tmp = 0;
            int i = 0;


            if (mode == 1) //单端模式
            {
                for (i = index; i < tmpArray.Length; i++)
                {
                    tmp = Convert.ToSingle(tmpArray[i]);
                    if (tmp < Convert.ToSingle(MeasPosition.tdd22start))
                    {
                        //logger.Error(tmpArray[i]);
                        result.Add(tmp);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (mode == 2)//差分模式
            {
                for (i = index; i < tmpArray.Length; i++)
                {
                    tmp = Convert.ToSingle(tmpArray[i]);
                    if (tmp < Convert.ToSingle(MeasPosition.tdd11start))
                    {
                        result.Add(tmp);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                //开路定义模式，返回所有数据
                for (i = 0; i < tmpArray.Length; i++)
                {
                    //logger.Trace(tmpArray[i]);
                    tmp = Convert.ToSingle(tmpArray[i]);
                    result.Add(tmp);
                }
            }

            return result;
        }

        /// <summary>
        /// 开路定义时，显示的图表
        /// </summary>
        /// <param name="measDiffData">差分开路数据</param>
        /// <param name="measSingleData">单端开路数据</param>
        private void CreateInitMeasChart(List<float> measDiffData, List<float> measSingleData)
        {
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            //求最大值及最小值
            if (measDiffData.Count != 0 && measSingleData.Count != 0)
            {

                //设置网格间距
                chart1.ChartAreas[0].AxisX.Interval = (float)measDiffData.Count / 10;//X轴间距
                chart1.ChartAreas[0].AxisX.Maximum = (float)measDiffData.Count; //设置X坐标最大值
                chart1.ChartAreas[0].AxisX.Minimum = 0;//设置X坐标最小值

                chart1.ChartAreas[0].AxisY.Interval = 25;//Y轴间距
                chart1.ChartAreas[0].AxisY.Maximum = 250;//设置Y坐标最大值
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;

                //设置几条线的文字为空
                chart1.Series[0].LegendText = "diff";
                chart1.Series[1].LegendText = "limit";
                chart1.Series[2].LegendText = "limit";
                chart1.Series[3].LegendText = "single";
            }
            else
            {
                //设置默认网格间距
                chart1.ChartAreas[0].AxisX.Interval = 250;//X轴间距
                chart1.ChartAreas[0].AxisX.Maximum = 2500; //设置X坐标最大值
                chart1.ChartAreas[0].AxisX.Minimum = 0;//设置X坐标最小值

                chart1.ChartAreas[0].AxisY.Interval = 25;//Y轴间距
                chart1.ChartAreas[0].AxisY.Maximum = 250;//设置Y坐标最大值
                chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            //生成测试数据曲线
            for (int i = 0; i < measDiffData.Count; i++)
            {
                chart1.Series[0].Points.AddXY(i, measDiffData[i]);
            }

            for (int i = 0; i < measSingleData.Count; i++)
            {
                chart1.Series[3].Points.AddXY(i, measSingleData[i]);
            }
        }


        /// <summary>
        /// 创建量测时的图表
        /// </summary>
        /// <param name="measData">量测试的数据</param>
        /// <param name="channel">单端OR差分</param>
        private void CreateMeasChart(List<float> measData, int channel)
        {
            float xbegin = 0;
            float xend = 0;           

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            
            //计算有效区域起始结束位置 
            if (channel == SINGLE)
            {
                xbegin = measData.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_Begin) / 100;
                xend = measData.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_End) / 100;

                chart1.ChartAreas[0].AxisY.Interval = 25;//Y轴间距
                chart1.ChartAreas[0].AxisY.Maximum = 125;//设置Y坐标最大值
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                //生成上半位有效区域
                chart1.Series[1].Points.AddXY(xbegin, 100);
                chart1.Series[1].Points.AddXY(xbegin, 56);
                chart1.Series[1].Points.AddXY(xend, 56);
                chart1.Series[1].Points.AddXY(xend, 100);

                //生成下半部有效区域
                chart1.Series[2].Points.AddXY(xbegin, 0);
                chart1.Series[2].Points.AddXY(xbegin, 44);
                chart1.Series[2].Points.AddXY(xend, 44);
                chart1.Series[2].Points.AddXY(xend, 0);


            }
            else
            {
                xbegin = measData.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_Begin) / 100;
                xend = measData.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_End) / 100;

                chart1.ChartAreas[0].AxisY.Interval = 50;//Y轴间距
                chart1.ChartAreas[0].AxisY.Maximum = 250;//设置Y坐标最大值
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                //生成上半位有效区域
                chart1.Series[1].Points.AddXY(xbegin, 200);
                chart1.Series[1].Points.AddXY(xbegin, 115);
                chart1.Series[1].Points.AddXY(xend, 115);
                chart1.Series[1].Points.AddXY(xend, 200);

                //生成下半部有效区域
                chart1.Series[2].Points.AddXY(xbegin, 0);
                chart1.Series[2].Points.AddXY(xbegin, 85);
                chart1.Series[2].Points.AddXY(xend, 85);
                chart1.Series[2].Points.AddXY(xend, 0);

            }
            //获取有效区域的LIST
            List<float> tmpResult = measData.Skip((int)xbegin).Take((int)(measData.Count - xend)).ToList();

            //求最大值及最小值
            if (tmpResult.Count != 0)
            {
                //设置网格间距
                chart1.ChartAreas[0].AxisX.Interval = (float)measData.Count / 10;//X轴间距
                chart1.ChartAreas[0].AxisX.Maximum = (float)measData.Count; //设置X坐标最大值
                chart1.ChartAreas[0].AxisX.Minimum = 0;//设置X坐标最小值

                chart1.Series[0].LegendText = "平均值:" + tmpResult.Average().ToString();
                chart1.Series[1].LegendText = "最大值:" + tmpResult.Max().ToString();
                chart1.Series[2].LegendText = "最小值:" + tmpResult.Min().ToString();
            }
            else
            {
                //设置默认网格间距
                chart1.ChartAreas[0].AxisX.Interval = 250;//X轴间距
                chart1.ChartAreas[0].AxisX.Maximum = 2500; //设置X坐标最大值
                chart1.ChartAreas[0].AxisX.Minimum = 0;//设置X坐标最小值

                chart1.ChartAreas[0].AxisY.Interval = 25;//Y轴间距
                chart1.ChartAreas[0].AxisY.Maximum = 250;//设置Y坐标最大值
                chart1.ChartAreas[0].AxisY.Minimum = 0;
            }

            //生成测试数据曲线
            for (int i = 0; i < measData.Count; i++)
            {
                chart1.Series[0].Points.AddXY(i, measData[i]);
            }
        }


        private void startMeasuration(int channel)
        {
            string result = string.Empty;
            int index = 0;
            string cmd1, cmd2, cmd3, cmd4, cmd5, cmd6;

            if (channel == SINGLE)
            {
                index = MeasPosition.tdd22IndexValue;
                cmd1 = ":CALC:PAR:SEL \"win1_tr2\"";
            }
            else
            {
                index = MeasPosition.tdd11IndexValue;
                cmd1 = ":CALC:PAR:SEL \"win1_tr1\"";
            }
            analyzer.ExecuteCmd(cmd1);

            result = string.Empty;
            cmd2 = ":CALCulate1:TRANsform:TIME:STARt?";
            analyzer.QueryCommand(cmd2, out result, 256);
     

            cmd3 = "DISPlay:ENABle ON";
            analyzer.ExecuteCmd(cmd3);

            analyzer.ExecuteCmd(cmd1);

            cmd5 = ":INITiate1:CONTinuous ON";
            analyzer.ExecuteCmd(cmd5);
            analyzer.viClear();

            result = string.Empty;
            cmd6 = ":CALCulate1:DATA? FDATa";
            analyzer.QueryCommand(cmd6, out result, 200000);

            //获取要生成报表的数据
            CreateMeasChart(packetMaesData(result, index, channel), channel);

            result = string.Empty;
            analyzer.QueryErrorStatus(out result);
        }

        /// <summary>
        /// 字符串转浮点型数据
        /// </summary>
        /// <param name="FloatString">浮点型字符串</param>
        /// <returns>转换后的浮点数据</returns>
        public float StrToFloat(object FloatString)
        {
            float result;
            if (FloatString != null)
            {
                if (float.TryParse(FloatString.ToString(), out result))
                    return result;
                else
                {
                    return (float)0.00;
                }
            }
            else
            {
                return (float)0.00;
            }
        }

        /// <summary>
        /// 更新测试结果到datagridview中去
        /// </summary>
        /// <param name="channel">这个好像不需要？</param>
        private bool upgradeTestResult(int channel)
        {
            bool ret = false;               

            float avg = StrToFloat(Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", "")); //设备平均值
            float max = StrToFloat(Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", "")); //设备最大值
            float min = StrToFloat(Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", "")); //设备最小值

            float stdValue = StrToFloat(paramList[measIndex.currentIndex].Spec); //标准值
            float loLimite = StrToFloat(paramList[measIndex.currentIndex].Low_limit); //下限
            float hiLimite = StrToFloat(paramList[measIndex.currentIndex].Upper_limit); //上限

            float stdLowValue = stdValue * ((100 + loLimite) / 100);
            float stdHiValue = stdValue * ((100 + hiLimite) / 100);


            //这里判定是以点的方式还是以平均值的方式来判定结果
            if (string.Compare(paramList[measIndex.currentIndex].Std, "AverageValue") == 0) //平均值的判定
            {
                if (avg > stdLowValue && avg < stdHiValue)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            else //以点的形式去判定
            {
                if (((max < stdHiValue) && (max > stdLowValue)) && ((min < stdHiValue) && (min > stdLowValue)))
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }

            //这里需要添加对比
            if (ret == false)
            {    
                //仅记录通过时进行下一笔，不通过时，一直当前笔，并不记录
                if (optParam.testMode == 4)
                {                   
                    return ret;
                }
            }

            int index = this.dgv_CurrentResult.Rows.Add();
            int history_index = this.dgv_HistoryResult.Rows.Add();
            if (ret)
            {
                this.dgv_CurrentResult.Rows[index].Cells[7].Value = "PASS";     
                this.dgv_HistoryResult.Rows[history_index].Cells[7].Value = "PASS";
            }
            else
            {
                this.dgv_CurrentResult.Rows[index].Cells[7].Value = "FAIL";
                this.dgv_HistoryResult.Rows[history_index].Cells[7].Value = "FAIL";
            }

            
            //目前量测
            this.dgv_CurrentResult.Rows[index].Cells[0].Value = paramList[measIndex.currentIndex].Layer;  //layer
            this.dgv_CurrentResult.Rows[index].Cells[1].Value = paramList[measIndex.currentIndex].Spec;     //标准值
            this.dgv_CurrentResult.Rows[index].Cells[2].Value = paramList[measIndex.currentIndex].Upper_limit;  //最大上限比例 
            this.dgv_CurrentResult.Rows[index].Cells[3].Value = paramList[measIndex.currentIndex].Low_limit;    //最小下限比例
            this.dgv_CurrentResult.Rows[index].Cells[4].Value = Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", ""); //平均值
            this.dgv_CurrentResult.Rows[index].Cells[5].Value = Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", ""); //最大值
            this.dgv_CurrentResult.Rows[index].Cells[6].Value = Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", ""); //最小值
            this.dgv_CurrentResult.Rows[index].Cells[8].Value = optParam.snPrefix + (gSerialInc).ToString().PadLeft(6, '0'); //流水号
            this.dgv_CurrentResult.Rows[index].Cells[9].Value = DateTime.Now.ToString("yyyy-MM-dd");    //日期    
            this.dgv_CurrentResult.Rows[index].Cells[10].Value = DateTime.Now.ToString("hh:mm:ss");     //时间
            this.dgv_CurrentResult.Rows[index].Cells[11].Value = paramList[measIndex.currentIndex].Mode;    //当前模式，单端or差分
            this.dgv_CurrentResult.Rows[index].Cells[12].Value = paramList[measIndex.currentIndex].Curve_data; //记录存放地址
            this.dgv_CurrentResult.Rows[index].Cells[13].Value = paramList[measIndex.currentIndex].Curve_image; //截图存放地址


            //历史量测
            this.dgv_HistoryResult.Rows[history_index].Cells[0].Value = paramList[measIndex.currentIndex].Layer;  //layer
            this.dgv_HistoryResult.Rows[history_index].Cells[1].Value = paramList[measIndex.currentIndex].Spec;     //标准值
            this.dgv_HistoryResult.Rows[history_index].Cells[2].Value = paramList[measIndex.currentIndex].Upper_limit;  //最大上限比例 
            this.dgv_HistoryResult.Rows[history_index].Cells[3].Value = paramList[measIndex.currentIndex].Low_limit;    //最小下限比例
            this.dgv_HistoryResult.Rows[history_index].Cells[4].Value = Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", ""); //平均值
            this.dgv_HistoryResult.Rows[history_index].Cells[5].Value = Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", ""); //最大值
            this.dgv_HistoryResult.Rows[history_index].Cells[6].Value = Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", ""); //最小值
            this.dgv_HistoryResult.Rows[history_index].Cells[8].Value = optParam.snPrefix + (gSerialInc).ToString().PadLeft(6, '0'); //流水号
            this.dgv_HistoryResult.Rows[history_index].Cells[9].Value = DateTime.Now.ToString("yyyy-MM-dd");    //日期    
            this.dgv_HistoryResult.Rows[history_index].Cells[10].Value = DateTime.Now.ToString("hh:mm:ss");     //时间
            this.dgv_HistoryResult.Rows[history_index].Cells[11].Value = paramList[measIndex.currentIndex].Mode;    //当前模式，单端or差分
            this.dgv_HistoryResult.Rows[history_index].Cells[12].Value = paramList[measIndex.currentIndex].Curve_data; //记录存放地址
            this.dgv_HistoryResult.Rows[history_index].Cells[13].Value = paramList[measIndex.currentIndex].Curve_image; //截图存放地址

            //只有最后一个走完，流水才++
            if (measIndex.currentIndex == paramList.Count - 1)
            {
                gSerialInc++;
            }

            //光标在最后一行
            dgv_CurrentResult.CurrentCell = dgv_CurrentResult.Rows[this.dgv_CurrentResult.Rows.Count - 1].Cells[0];

            return ret;
        }

        

        private void tsb_StartTest_Click(object sender, EventArgs e)
        {

                bool ret = false;

                if (optStatus.isConnect && optStatus.isGetIndex)
                {
                    //量测并生成图表
                    startMeasuration(paramList[measIndex.currentIndex].DevMode);

                    //更新测试数据到主界面测试结果中
                    ret = upgradeTestResult(paramList[measIndex.currentIndex].DevMode);

                    if (optParam.testMode == 3)
                    {
                        //量测配方参数依次向后移
                        measIndex.incIndex();
                    }
                    else if (optParam.testMode == 1 || optParam.testMode == 4)
                    {
                        if (ret)
                        {
                            //量测配方参数依次向后移
                            measIndex.incIndex();
                        }
                    }
                    else if (optParam.testMode == 2)
                    {
                        measIndex.currentIndex = dataGridView1.CurrentCell.RowIndex; //是当前活动的单元格的行的索引
                    }

                    //高亮显示相应测试配方的那一行            
                    dataGridView1.CurrentCell = dataGridView1.Rows[measIndex.currentIndex].Cells[0];

                    //CaptureScreen(paramList[measIndex.currentIndex].Curve_image);
                }
                else
                {
                    MessageBox.Show("设备未连接或者未开路");
                }

                System.Threading.Thread.Sleep(500);
          
        }

        private void tsmi_delAll_Click(object sender, EventArgs e)
        {
            //删除所有测试数据
            dgv_CurrentResult.Rows.Clear();
        }

        //删除选中的测试结果
        private void tsmi_delselect_Click(object sender, EventArgs e)
        {
            dgv_CurrentResult.Rows.Remove(dgv_CurrentResult.CurrentRow);
        }

        //输出测试报告
        private void tsmi_export_Click(object sender, EventArgs e)
        {
            //输出报告 
            DataGridViewToExcel(dgv_CurrentResult);

            //复制到已量测表格中
            for (int i = 0; i < dgv_CurrentResult.Rows.Count; i++)
            {
                int index = dgv_OutPutResult.Rows.Add();//在gridview2中添加一空行

                //为空行添加列值
                for (int j = 0; j < dgv_CurrentResult.Rows[i].Cells.Count; j++)
                {
                    dgv_OutPutResult.Rows[i].Cells[j].Value = dgv_CurrentResult.Rows[i].Cells[j].Value;
                }
            }

            //清空参数表格
            dgv_CurrentResult.Rows.Clear();

        }

        /// <summary>
        /// DataGridView输出到CSV文件
        /// </summary>
        /// <param name="dgv">数据源文件</param>
        public void DataGridViewToExcel(DataGridView dgv)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl files (*.csv)|*.csv";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为csv文件";
            dlg.FileName = optParam.outputExportFileName;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                string columnTitle = "";
                try
                {
                    //写入列标题    
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += ",";
                        }
                        columnTitle += dgv.Columns[i].HeaderText;
                    }

                    sw.WriteLine(columnTitle);

                    //写入列内容    
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += ",";
                            }
                            if (dgv.Rows[j].Cells[k].Value == null)
                                columnValue += "";
                            else if (dgv.Rows[j].Cells[k].Value.ToString().Contains(","))
                            {
                                columnValue += "\"" + dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\"";
                            }
                            else
                            {
                                columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\t";
                            }
                        }
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    myStream.Close();
                    MessageBox.Show("导出报告成功！");
                }
                catch (Exception e)
                {
                    MessageBox.Show("导出报告失败！");
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
            else
            {
                MessageBox.Show("取消导出报告操作!");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (optParam.keyMode == 1)
                {
                    bool ret = false;

                    if (optStatus.isConnect && optStatus.isGetIndex)
                    {
                        //量测并生成图表
                        startMeasuration(paramList[measIndex.currentIndex].DevMode);

                        //更新测试数据到主界面测试结果中
                        ret = upgradeTestResult(paramList[measIndex.currentIndex].DevMode);

                        if (optParam.testMode == 3)
                        {
                            //量测配方参数依次向后移
                            measIndex.incIndex();
                        }
                        else if (optParam.testMode == 1 || optParam.testMode == 4)
                        {
                            if (ret)
                            {
                                //量测配方参数依次向后移
                                measIndex.incIndex();
                            }
                        }
                        else if (optParam.testMode == 2)
                        {
                            measIndex.currentIndex = dataGridView1.CurrentCell.RowIndex; //是当前活动的单元格的行的索引
                        }

                        //高亮显示相应测试配方的那一行            
                        dataGridView1.CurrentCell = dataGridView1.Rows[measIndex.currentIndex].Cells[0];
                    }
                    else
                    {
                        MessageBox.Show("设备未连接或者未开路");
                    }
                }
            }
        }


        //点击2号卡时，自动把已理测试数据添加到
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                //for (int i = 0; i < dgv_CurrentResult.Rows.Count; i++)
                //{
                //    //int index = dataGridView2.Rows.Add();//在gridview2中添加一空行

                //    //为空行添加列值
                //    for (int j = 0; j < dgv_CurrentResult.Rows[i].Cells.Count; j++)
                //    {
                //        dgv_OutPutResult.Rows[i].Cells[j].Value = dgv_CurrentResult.Rows[i].Cells[j].Value;
                //    }
                //}
            }
        }

        //鼠标点击单元框选，更改测试次序,同时对比LIMIT里，需要对比相应的LIMIT
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {  
            if (e.RowIndex > -1)
            {
                measIndex.currentIndex = dataGridView1.CurrentCell.RowIndex; //是当前活动的单元格的行的索引     
                //measIndex.currentIndex = dataGridView1.CurrentRow.Index; //获得包含当前单元格的行的索引 
            }
        }

        //截图
        private void CaptureScreen(string path)  //导出数据（截图表格部分）
        {
            Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
            //g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
            g.CopyFromScreen(chart1.PointToScreen(Point.Empty), Point.Empty, chart1.Size);//只保存某个控件
            //g.CopyFromScreen(tabPage1.PointToScreen(Point.Empty), Point.Empty, tabPage1.Size);//只保存某个控件
            bit.Save(path + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");//默认保存格式为PNG，保存成jpg格式质量不是很好  
        }




    }//end form

    public class MeasIndex
    {
        private int _total = 0;
        private int _currentIndex = 0;

        public int total
        {
            get { return _total; }
            set { _total = value; }
        }

        public int currentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; }
        }
        public int incIndex()
        {
            if (_currentIndex++ == _total - 1)
            {
                _currentIndex = 0;
            }

            return _currentIndex;
        }
    }//end class
}
