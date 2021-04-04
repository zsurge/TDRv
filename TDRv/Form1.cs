using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public const int SINGLE = 1;
        public const int DIFFERENCE = 2;
        E5080B analyzer = new E5080B();

        List<TestResult> paramList = new List<TestResult>();
        public static int gCurrentIndex = 0;

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

            if (error != 0)
            {
                tsb_GetTestIndex.Enabled = true;
                tsb_StartTest.Enabled = true;
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
            DevParamSet devParamSet = new DevParamSet();
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

        //获取配方中开路位置信息
        //并且记录所有的待测参数
        public void GetIndexStart(DataGridView dt)
        {
            bool single = true;
            bool diff = true;

            TestResult tr = new TestResult();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.Compare(dt.Rows[i].Cells[10].Value.ToString(), "Differential") == 0 && diff)
                {
                    MeasPosition.tdd11start = Convert.ToInt32(dt.Rows[i].Cells[14].Value);
                    diff = false;
                }

                if (string.Compare(dt.Rows[i].Cells[10].Value.ToString(), "SingleEnded") == 0 && single)
                {
                    MeasPosition.tdd22start = Convert.ToInt32(dt.Rows[i].Cells[14].Value);
                    single = false;
                }

                //if (!diff && !single)
                //{
                //    break;
                //}

                tr.Layer = dt.Rows[i].Cells["pLayer"].Value.ToString();
                tr.Spec = dt.Rows[i].Cells["pImpedanceDefine"].Value.ToString();
                tr.Upper_limit = dt.Rows[i].Cells["pImpedanceLimitUpper"].Value.ToString();
                tr.Low_limit = dt.Rows[i].Cells["pImpedanceLimitLower"].Value.ToString();
                //tr.Average = dt.Rows[i].Cells[].Value.ToString();
                //tr.Max = dt.Rows[i].Cells[].Value.ToString();
                //tr.Min = dt.Rows[i].Cells[].Value.ToString();
                //tr.Result = dt.Rows[i].Cells[].Value.ToString();
                //tr.Serial = dt.Rows[i].Cells[].Value.ToString();
                //tr.Date = dt.Rows[i].Cells[].Value.ToString();
                //tr.Time = dt.Rows[i].Cells[].Value.ToString();
                tr.Mode = dt.Rows[i].Cells["pInputMode"].Value.ToString();
                //tr.Std = dt.Rows[i].Cells[].Value.ToString();
                tr.Curve_data = dt.Rows[i].Cells["pSaveCurve"].Value.ToString();
                tr.Curve_image = dt.Rows[i].Cells["pSaveImage"].Value.ToString();

                paramList.Add(tr);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initChart();
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

            string cmd2 = "FORM:DATA ASCII";
            analyzer.ExecuteCmd(cmd2);

            string cmd3 = "MMEM:STOR:TRAC:FORM:SNP MA";
            analyzer.ExecuteCmd(cmd3);

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

            string cmd12 = ":CALCulate1:DATA? FDATa";
            analyzer.QueryCommand(cmd12, out result, 200000);

            tmpSingleMeasData = packetMaesData(result, 0, 0);
            string[] tdd22_array = result.Split(new char[] { ',' });
            result = string.Empty;

            //查找tdd22单端的索引值
            for (int i = 0; i < tdd22_array.Length; i++)
            {
                //logger.Trace(tdd22_array[i]);
                if (Convert.ToSingle(tdd22_array[i]) >= Convert.ToSingle(MeasPosition.tdd22start))
                {            
                    MeasPosition.tdd22IndexValue = i - 1;
                    break;
                }
            }


            string cmd13 = ":CALC:PAR:SEL \"win1_tr1\"";
            analyzer.ExecuteCmd(cmd13);
            analyzer.viClear();

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
                    break;
                }
            }


            MeasPosition.isOpen = true;
            result = string.Empty;
            CreateInitMeasChart(tmpDiffMeasData, tmpSingleMeasData);
        }
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
                    //logger.Info(tmpArray[i]);
                }

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
        /// <param name="measData"></param>
        private void CreateMeasChart(List<float> measData, int channel)
        {
            float xbegin = 0;
            float xend = 0;
            int index = gCurrentIndex++;

            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            //计算有效区域起始结束位置 
            if (channel == SINGLE)
            {
                xbegin = measData.Count * Convert.ToSingle(paramList[index].Low_limit) / 100;
                xend = measData.Count * Convert.ToSingle(paramList[index].Upper_limit) / 100;

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
                xbegin = measData.Count * Convert.ToSingle(paramList[index].Low_limit) / 100;
                xend = measData.Count * Convert.ToSingle(paramList[index].Upper_limit) / 100;

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

                chart1.Series[0].LegendText = "平均值：" + tmpResult.Average().ToString();
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


    }//end form
}
