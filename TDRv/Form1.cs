using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using TDRv.Driver;
using System.Web;



namespace TDRv
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        public const int isDebug = 1; //isDebug=0 是release模式，=1是debug模式

        //设置参数设置窗体的表数据
        DataTable gdt;

        //获取当前
        public string exPortFilePath = string.Empty;

        //差分
        public const int DIFFERENCE = 1;

        //单端
        public const int SINGLE = 2;

        //E5080B设备类型
        public static int gDevType = 0;

        public const int CURRENT_RECORD = 100;
        public const int HISTORY_RECORD = 999;

        //流水
        public int gSerialInc = 0;

        private bool gEmptyFlag = true; //标志是否有空的待测物
        public static int gTestResultValue = 0;

        public static string logFileName = string.Empty;

        public static bool isExecuteComplete = true;
        public static bool isExecuteIndex = true;

     
         //public const string gUrl = "Http://58.254.36.190/OrBitWCFServiceR13/PostHole.asmx/ETIImpedancePostData";


        public const string gUrl = "http://172.16.1.67/OrBitWCFServiceR13/PostHole.asmx/ETIImpedancePostData";


        public double utilization_rate = 0.0;
        public TimeSpan day_shift_idle = TimeSpan.Zero;
        public TimeSpan night_shift_idle = TimeSpan.Zero;



        //配方列表
        List<TestResult> paramList = new List<TestResult>();

        //记录已测试到第几层
        MeasIndex measIndex = new MeasIndex();

        //用于保存列表数据，好上送到服务器
        List<Dictionary<string, string>> postDataList = new List<Dictionary<string, string>>();


        //测试数据目录，该目录下有子目录
        public string fileDir = Environment.CurrentDirectory + "\\MeasureData";
        public string configDir = Environment.CurrentDirectory + "\\Config";
        public string autoSaveDir = Environment.CurrentDirectory + "\\AutoSave";
        public string historyDir = Environment.CurrentDirectory + "\\MeasureData\\History";
        public string imageDir = Environment.CurrentDirectory + "\\AutoSave\\Image";
        public string CurveDir = Environment.CurrentDirectory + "\\AutoSave\\Curve";
        public string reportDir = Environment.CurrentDirectory + "\\MeasureData\\Report";

        private void tsb_DevConnect_Click(object sender, EventArgs e)
        {
            DevConnectSet devConnectSet = new DevConnectSet();
            //devConnectSet.ChangeValue += new DevConnectSet.ChangeTsbHandler(Change_Tsb_Index);
            devConnectSet.Show();
        }


        private void tsb_DevOptSet_Click(object sender, EventArgs e)
        {
            //if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
            //{
            //    optStatus.isConnect = false;
            //    optStatus.isGetIndex = false;
            //    optStatus.isLoadXml = false;
            //    tsb_DevPOptSet.Enabled = false;
            //    return;
            //}

            DevOptSet devOptSet = new DevOptSet();
            devOptSet.ChangeSn += new DevOptSet.ChangeSnHandler(update_sn_begin);
            devOptSet.Show();
        }

        public void update_sn_begin(string sn)
        {
            gSerialInc = Convert.ToInt32(sn);
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
            if (dgv_CurrentResult.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("将要清除测试数据，是否保存并上传服务器", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    ExecutePacketAndUploadData();
                    DataGridViewToExcel(dgv_CurrentResult);
                }
            }

            //清除开路定义，需重新开路
            if (isDebug == 0)
            {
                optStatus.isGetIndex = false;
                tsb_StartTest.Enabled = false;
            }
            else
            {
                optStatus.isGetIndex = true;
                tsb_StartTest.Enabled = true;
            }

            //指令步骤显示栏清空
            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();

            //子窗体返回参数缓冲区清空
            paramList.Clear();

            //结果显示框清空
            dgv_CurrentResult.Rows.Clear();

            //输出栏清空
            dgv_OutPutResult.Rows.Clear();

            //历史显示框清空
            dgv_HistoryResult.Rows.Clear();

            //开路位置清空
            MeasPosition.tdd11IndexValue = 0;
            MeasPosition.tdd11start = 0.0f;
            MeasPosition.tdd22IndexValue = 0;
            MeasPosition.tdd22start = 0.0f;

            //记录索引清零
            measIndex.total = 0;
            measIndex.currentIndex = 0;

            //清空流水号
            gSerialInc = Convert.ToInt32(optParam.snBegin);

            //判定传回的数据是否为空
            if (dt.RowCount == 0)
            {
                optStatus.isLoadXml = false;
                return;
            }

            optStatus.isLoadXml = true;

            if (dt.Tag != null)
            {
                tsb_XmlFileName.Text = dt.Tag.ToString();
            }

            //读取配方中的数据
            GetIndexStart(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int index = dataGridView1.Rows.Add();

                dataGridView1.Rows[index].Cells[1].Value = dt.Rows[i].Cells[1].Value;
                dataGridView1.Rows[index].Cells[2].Value = dt.Rows[i].Cells[2].Value;
                dataGridView1.Rows[index].Cells[3].Value = dt.Rows[i].Cells[3].Value;
            }

            if (optStatus.isConnect && optStatus.isLoadXml)
            {
                tsb_GetTestIndex.Enabled = true;
                CommonFuncs.ShowMsg(eHintInfoType.hint, "请执行开路定义");
            }

            //禁止删除行
            dataGridView1.AllowUserToDeleteRows = false;

            //设置为可读
            dataGridView1.ReadOnly = true;
            isExecuteIndex = true;
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

        //获取配方中的数据，包括开路位置信息
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

                if (string.Compare(dt.Rows[i].Cells[10].Value.ToString(), "Differential") == 0 && diff) //差分
                {
                    MeasPosition.tdd11start = Convert.ToSingle(dt.Rows[i].Cells[14].Value);
                    diff = false;
                    tr.DevMode = DIFFERENCE;
                }

                if (string.Compare(dt.Rows[i].Cells[10].Value.ToString(), "SingleEnded") == 0 && single) //单端
                {
                    MeasPosition.tdd22start = Convert.ToSingle(dt.Rows[i].Cells[14].Value);
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
                tr.Open_hreshold = int.Parse(dt.Rows[i].Cells["OpenThreshold"].Value.ToString()); //开路位置
                tr.ImpedanceLimit_Unit = dt.Rows[i].Cells["ImpedanceLimitUnit"].Value.ToString(); //单位
                tr.Offset = Convert.ToSingle(dt.Rows[i].Cells["CalibrateOffset"].Value.ToString());
                paramList.Add(tr);
            }
        }


        private void ReadTestMode()
        {
            if (string.Compare(INI.GetValueFromIniFile("TDR", "TestStep"), "Pass") == 0)
            {

                optParam.testMode = 1;
            }
            else if (string.Compare(INI.GetValueFromIniFile("TDR", "TestStep"), "Manual") == 0)
            {

                optParam.testMode = 2;
            }
            else if (string.Compare(INI.GetValueFromIniFile("TDR", "TestStep"), "Next") == 0)
            {

                optParam.testMode = 3;
            }
            else if (string.Compare(INI.GetValueFromIniFile("TDR", "TestStep"), "PassRecord") == 0)
            {

                optParam.testMode = 4;
            }


        }

        //建立默认文件夹
        public void CreateDefaultDir()
        {
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            if (!Directory.Exists(historyDir))
            {
                Directory.CreateDirectory(historyDir);
            }

            if (!Directory.Exists(reportDir))
            {
                Directory.CreateDirectory(reportDir);
            }

            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }

            if (!Directory.Exists(autoSaveDir))
            {
                Directory.CreateDirectory(autoSaveDir);
            }

            if (!Directory.Exists(imageDir))
            {
                Directory.CreateDirectory(imageDir);
            }

            if (!Directory.Exists(CurveDir))
            {
                Directory.CreateDirectory(CurveDir);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            //创建默认文件夹
            CreateDefaultDir();
            //获取当前测试模式
            ReadTestMode();
            //获取序列号起始值
            gSerialInc = Convert.ToInt32(optParam.snBegin);

            //初始化折线图
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
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            chart1.Series[0].LegendText = "TDR Curve";
            chart1.Series[1].LegendText = "limit";
            chart1.Series[2].LegendText = "limit";
            chart1.Series[3].LegendText = "100";
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].BorderWidth = 2;
            chart1.Series[1].BorderWidth = 2;
            chart1.Series[2].BorderWidth = 2;
            chart1.Series[3].BorderWidth = 2;

            //背景灰色
            chart1.BackColor = Color.Gray;
            chart1.ChartAreas[0].BackColor = Color.Gray;

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

        private void tsb_GetTestIndex_Click(object sender, EventArgs e)
        {
            if (isExecuteIndex)
            {
                isExecuteIndex = false;

                //if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                //{
                //    optStatus.isConnect = false;
                //    optStatus.isGetIndex = false;
                //    optStatus.isLoadXml = false;
                //    tsb_GetTestIndex.Enabled = false;
                //    return;
                //}

                List<float> tmpDiffMeasData = new List<float>();
                List<float> tmpSingleMeasData = new List<float>();
                string result = string.Empty;
                string result2 = string.Empty;

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

                if (CGloabal.g_curInstrument.strInstruName.Equals("E5080B"))
                {
                    string strDevType = INI.GetValueFromIniFile("Instrument", "NA");
                    if (strDevType.Equals("E5080B 2-port"))
                    {
                        gDevType = 2;
                    }
                    else
                    {
                        gDevType = 0;
                    }

                    //差分开路定义
                    E5080B.getStartIndex(CGloabal.g_curInstrument.nHandle, DIFFERENCE, gDevType, out result);
                }
                else if (CGloabal.g_curInstrument.strInstruName.Equals("E5063A"))
                {
                    //E5063A.getStartIndex(CGloabal.g_curInstrument.nHandle, out result, out result2);

                    E5063A.measuration(CGloabal.g_curInstrument.nHandle, DIFFERENCE, out result);

                }
                else if (CGloabal.g_curInstrument.strInstruName.Equals("E5071C"))
                {
                    string strDevType = INI.GetValueFromIniFile("Instrument", "NA");
                    if (strDevType.Equals("E5071C 2-port"))
                    {
                        gDevType = 2;
                    }
                    else
                    {
                        gDevType = 0;
                    }
                    //差分开路定义
                    E5071C.getStartIndex(CGloabal.g_curInstrument.nHandle, DIFFERENCE, gDevType, out result);
                }

                //这里需要处理win1_tr1的数据
                tmpDiffMeasData = packetMaesData(result, 0, 0);
                string[] tdd11_array = result.Split(new char[] { ',' });
                result = string.Empty;

                if (tdd11_array.Length < 200)
                {
                    MessageBox.Show("获取差分开路定义失败");
                }

                //查找tdd11差分的索引值
                for (int i = 0; i < tdd11_array.Length; i++)
                {
                    //logger.Trace(tdd22_array[i]);
                    if (Convert.ToSingle(tdd11_array[i]) >= Convert.ToSingle(MeasPosition.tdd11start))
                    {
                        if (i == 0)
                        {
                            MeasPosition.tdd11IndexValue = 0;
                        }
                        else
                        {
                            MeasPosition.tdd11IndexValue = i - 1;
                        }
                        //这里需要将开路定义后的索引写入到配方的XML文件中去
                        LoggerHelper.mlog.Debug("差分开路位置：" + MeasPosition.tdd11IndexValue.ToString());
                        break;
                    }
                }

                if (CGloabal.g_curInstrument.strInstruName.Equals("E5080B"))
                {
                    //单端开路定义
                    result2 = string.Empty;
                    E5080B.getStartIndex(CGloabal.g_curInstrument.nHandle, SINGLE, gDevType, out result2);
                    tmpSingleMeasData = packetMaesData(result2, 0, 0);
                }
                else if (CGloabal.g_curInstrument.strInstruName.Equals("E5063A"))
                {
                    //单端开路定义
                    result2 = string.Empty;
                    E5063A.measuration(CGloabal.g_curInstrument.nHandle, SINGLE, out result2);
                    tmpSingleMeasData = packetMaesData(result2, 0, 0);
                }
                else if (CGloabal.g_curInstrument.strInstruName.Equals("E5071C"))
                {
                    //单端开路定义
                    result2 = string.Empty;
                    E5071C.getStartIndex(CGloabal.g_curInstrument.nHandle, SINGLE, gDevType, out result2);
                    tmpSingleMeasData = packetMaesData(result2, 0, 0);
                }

                string[] tdd22_array = result2.Split(new char[] { ',' });

                //查找tdd22单端的索引值
                for (int i = 0; i < tdd22_array.Length; i++)
                {
                    if (Convert.ToSingle(tdd22_array[i]) >= Convert.ToSingle(MeasPosition.tdd22start))
                    {
                        if (i == 0)
                        {
                            MeasPosition.tdd22IndexValue = 0;
                        }
                        else
                        {
                            MeasPosition.tdd22IndexValue = i - 1;
                        }

                        //这里需要将开路定义后的索引写入到配方的XML文件中去
                        LoggerHelper.mlog.Debug("单端开路位置：" + MeasPosition.tdd22IndexValue.ToString());
                        break;
                    }
                }

                MeasPosition.isOpen = true;
                optStatus.isGetIndex = true;
                tsb_StartTest.Enabled = true;

                CreateInitMeasChart(tmpDiffMeasData, tmpSingleMeasData);
            }
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

            try
            {
                if (mode == DIFFERENCE) //差分模式
                {
                    for (i = index; i < tmpArray.Length; i++)
                    {
                        tmp = Convert.ToSingle(tmpArray[i]) + paramList[measIndex.currentIndex].Offset;
                        if (tmp < Convert.ToSingle(MeasPosition.tdd11start))
                        {
                            //LoggerHelper.mlog.Trace(tmpArray[i]+"\r\n");
                            result.Add(Convert.ToSingle(tmp.ToString("#0.00")));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if (mode == SINGLE)//单端模式
                {
                    for (i = index; i < tmpArray.Length; i++)
                    {
                        tmp = Convert.ToSingle(tmpArray[i]) + paramList[measIndex.currentIndex].Offset;
                        if (tmp < Convert.ToSingle(MeasPosition.tdd22start))
                        {
                            result.Add(Convert.ToSingle(tmp.ToString("#0.00")));
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
                        //LoggerHelper.mlog.Debug(tmpArray[i]);
                        tmp = Convert.ToSingle(tmpArray[i]);
                        result.Add(Convert.ToSingle(tmp.ToString("#0.00")));
                    }
                }

                logFileName = DateTime.Now.ToString("yyyyMMddHH:mm:ss.ff");
                SaveDataToCSVFile(result, logFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            for (int i = 0; i < measDiffData.Count - 1; i++)
            {
                chart1.Series[0].Points.AddXY(i, measDiffData[i]);
            }

            for (int i = 0; i < measSingleData.Count - 1; i++)
            {
                chart1.Series[3].Points.AddXY(i, measSingleData[i]);
            }

            isExecuteIndex = true;
            isExecuteComplete = true;
        }

        private bool SaveDataToCSVFile(List<float> measData, string fileName)
        {
            bool successFlag = true;

            //StringBuilder strColumn = new StringBuilder();
            StringBuilder strValue = new StringBuilder();
            StreamWriter sw = null;
            //PropertyInfo[] props = GetPropertyInfoArray();


            if (!Directory.Exists(CurveDir))
            {
                Directory.CreateDirectory(CurveDir);
            }

            string spath = CurveDir + "\\" + fileName.Replace(":", "").Replace(".", "") + ".csv";

            try
            {
                sw = new StreamWriter(spath);

                for (int i = 0; i < measData.Count; i++)
                {
                    strValue.Remove(0, strValue.Length); //clear the temp row value
                    strValue.Append(measData[i]);
                    sw.WriteLine(strValue); //write the row value
                }
            }
            catch (Exception ex)
            {
                successFlag = false;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                }
            }

            return successFlag;
        }





        /// <summary>
        /// 创建量测时的图表
        /// </summary>
        /// <param name="measData">量测试的数据</param>
        /// <param name="channel">单端OR差分</param>
        private void CreateMeasChart(List<float> measData)
        {
            float xbegin = 0;
            float xend = 0;
            float yhigh = 0;
            float ylow = 0;


            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }

            xbegin = measData.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_Begin) / 100;
            xend = measData.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_End) / 100;
            yhigh = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 + (Convert.ToSingle(paramList[measIndex.currentIndex].Upper_limit) / 100));
            ylow = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 + (Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit) / 100));

            if (xend - xbegin < 3)
            {
                //initChart();
                gEmptyFlag = true;
                return;
            }
            else
            {
                gEmptyFlag = false;
            }

            //计算有效区域起始结束位置 
            chart1.ChartAreas[0].AxisY.Interval = paramList[measIndex.currentIndex].Open_hreshold / 5; //Y轴间距
            chart1.ChartAreas[0].AxisY.Maximum = paramList[measIndex.currentIndex].Open_hreshold;//设置Y坐标最大值
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            //生成上半位有效区域
            chart1.Series[1].Points.AddXY(xbegin, paramList[measIndex.currentIndex].Open_hreshold);
            chart1.Series[1].Points.AddXY(xbegin, yhigh);
            chart1.Series[1].Points.AddXY(xend, yhigh);
            chart1.Series[1].Points.AddXY(xend, paramList[measIndex.currentIndex].Open_hreshold);

            //生成下半部有效区域
            chart1.Series[2].Points.AddXY(xbegin, 0);
            chart1.Series[2].Points.AddXY(xbegin, ylow);
            chart1.Series[2].Points.AddXY(xend, ylow);
            chart1.Series[2].Points.AddXY(xend, 0);

            //获取有效区域的LIST
            List<float> tmpResult = measData.Skip((int)xbegin).Take((int)(xend - xbegin)).ToList();


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

        delegate void SetLableCB(string text, string color);
        public void SetLableText(string text, string color)
        {
            if (this.lable_test_result.InvokeRequired)
            {
                SetLableCB d = new SetLableCB(SetLableText);
                this.Invoke(d, new object[] { text, color });
            }
            else
            {
                Color myColor = ColorTranslator.FromHtml(color);
                this.lable_test_result.Text = text;
                this.lable_test_result.BackColor = myColor;
            }
        }


        private DateTime lastClickTime = DateTime.Now; // 上一次点击时间

        public double CalculateAvailability(TimeSpan idle_time)
        {
            double availability = (720 - Math.Abs(idle_time.TotalMinutes)) / 720 * 100;

            return Math.Round(availability, 2);
        }

        public void CheckIdleTime()
        {
            DateTime currentTime = DateTime.Now; // 当前时间
            TimeSpan startTime = new TimeSpan(8, 0, 0); // 早上8点
            TimeSpan endTime = new TimeSpan(20, 0, 0); // 晚上8点
            TimeSpan currentTimeOfDay = currentTime.TimeOfDay; // 获取当前时间的小时、分钟和秒数部分


            // 判断当前时间是否在早上8点到晚上8点之间
            if (currentTimeOfDay >= startTime && currentTimeOfDay <= endTime)
            {
                night_shift_idle = TimeSpan.Zero;
                TimeSpan timeDifference = currentTime - lastClickTime;

                if (timeDifference.TotalMinutes > 10)
                {
                    // 空闲开始时间为上一次点击时间
                    day_shift_idle += lastClickTime - currentTime;
                }
                utilization_rate = CalculateAvailability(day_shift_idle);
            }
            else //("当前时间晚上8点到早上8点到");
            {
                day_shift_idle = TimeSpan.Zero;
                TimeSpan timeDifference = currentTime - lastClickTime;

                if (timeDifference.TotalMinutes > 10)
                {
                    // 空闲开始时间为上一次点击时间
                    night_shift_idle += lastClickTime - currentTime;
                }

                utilization_rate = CalculateAvailability(night_shift_idle);
            }

            lastClickTime = currentTime;
        }




        private void tsb_StartTest_Click(object sender, EventArgs e)
        {

            if (isDebug == 0)
            {
                if (tsb_Pnl_ID.Text.Length == 0)
                {
                    CommonFuncs.ShowMsg(eHintInfoType.waring, "panel id 不能为空");
                    return;
                }

                if (isExecuteComplete)
                {
                    isExecuteComplete = false;

                    toDoWork();
                }
            }
            else
            {
                toDoTest();
            }
        }

        public void toDoWork()
        {
            var task1 = new Task(() =>
            {

                if (optStatus.isConnect && optStatus.isGetIndex)
                {
                    CheckIdleTime();

                    bool ret = false;

                    string result = string.Empty;
                    int index = 0;

                    int channel = paramList[measIndex.currentIndex].DevMode;

                    if (channel == SINGLE)
                    {
                        index = MeasPosition.tdd22IndexValue;
                    }
                    else
                    {
                        index = MeasPosition.tdd11IndexValue;
                    }

                    SetLableText("", "Control");

                    if (CGloabal.g_curInstrument.strInstruName.Equals("E5080B"))
                    {
                        E5080B.measuration(CGloabal.g_curInstrument.nHandle, channel, gDevType, out result);
                    }
                    else if (CGloabal.g_curInstrument.strInstruName.Equals("E5063A"))
                    {
                        E5063A.measuration(CGloabal.g_curInstrument.nHandle, channel, out result);
                    }
                    else if (CGloabal.g_curInstrument.strInstruName.Equals("E5071C"))
                    {
                        E5071C.measuration(CGloabal.g_curInstrument.nHandle, channel, gDevType, out result);
                    }

                    ////量测并生成图表                    
                    List<float> disResult = packetMaesData(result, index, channel);

                    DisplayChartValue(chart1, disResult);

                    //更新测试数据到主界面测试结果中
                    CreateResultDatagridview(dgv_CurrentResult, paramList[measIndex.currentIndex].DevMode, CURRENT_RECORD);
                    CreateResultDatagridview(dgv_HistoryResult, paramList[measIndex.currentIndex].DevMode, HISTORY_RECORD);



                    if (optParam.testMode == 3)
                    {
                        //量测配方参数依次向后移
                        measIndex.incIndex();
                    }
                    else if (optParam.testMode == 1 || optParam.testMode == 4)
                    {
                        if (gTestResultValue == 1)
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
                    //dataGridView1.CurrentCell = dataGridView1.Rows[measIndex.currentIndex].Cells[0];
                    reFreshDatagridview(dataGridView1);
                    isExecuteComplete = true;

                }
                else
                {
                    CommonFuncs.ShowMsg(eHintInfoType.waring, "设备未连接或者未开路");
                }
            });

            task1.Start();

            if (paramList.Count <= 0)
            {
                return;
            }

            if (paramList[0].Curve_image.Length > 3)
            {
                task1.ContinueWith((Task) =>
                {
                    CaptureScreenChart(chart1, paramList[0].Curve_image);
                });
            }

        }

        public void toDoTest()
        {
            var task1 = new Task(() =>
            {

                CheckIdleTime();

                bool ret = false;

                string result = string.Empty;
                int index = 0;

                int channel = paramList[measIndex.currentIndex].DevMode;

                if (channel == SINGLE)
                {
                    index = MeasPosition.tdd22IndexValue;
                }
                else
                {
                    index = MeasPosition.tdd11IndexValue;
                }

                ////量测并生成图表                    
                List<float> disResult = packetMaesData(result, index, channel);

                DisplayChartValue(chart1, disResult);

                //更新测试数据到主界面测试结果中
                CreateResultDatagridview(dgv_CurrentResult, paramList[measIndex.currentIndex].DevMode, CURRENT_RECORD);
                CreateResultDatagridview(dgv_HistoryResult, paramList[measIndex.currentIndex].DevMode, HISTORY_RECORD);



                if (optParam.testMode == 3)
                {
                    //量测配方参数依次向后移
                    measIndex.incIndex();
                }
                else if (optParam.testMode == 1 || optParam.testMode == 4)
                {
                    if (gTestResultValue == 1)
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
                //dataGridView1.CurrentCell = dataGridView1.Rows[measIndex.currentIndex].Cells[0];
                reFreshDatagridview(dataGridView1);
                isExecuteComplete = true;


            });

            task1.Start();

            if (paramList.Count <= 0)
            {
                return;
            }

            if (paramList[0].Curve_image.Length > 3)
            {
                task1.ContinueWith((Task) =>
                {
                    CaptureScreenChart(chart1, paramList[0].Curve_image);
                });
            }

        }


        //private void packet_result()
        //{
        //    // 创建一个 List<Dictionary<string, string>> 对象
        //    List<Dictionary<string, string>> postDataList = new List<Dictionary<string, string>>();

        //    // 遍历 DataGridView 的行
        //    foreach (DataGridViewRow row in dgv_CurrentResult.Rows)
        //    {
        //        // 创建一个 IDictionary<string, string> 对象
        //        Dictionary<string, string> parameters = new Dictionary<string, string>();

        //        // 遍历 DataGridView 的列
        //        foreach (DataGridViewColumn column in dgv_CurrentResult.Columns)
        //        {
        //            string key = column.HeaderText;

        //            // 获取当前行、当前列的单元格值
        //            string value = row.Cells[column.Index].Value?.ToString();

        //            // 对包含"+-"符号的值进行特殊处理
        //            if (key == "ImpedanceSpec")
        //            {
        //                // 进行解码操作，将"+-" 替换为 "%2B-"
        //                value = value.Replace("+-", "%2B-");
        //            }

        //            // 将键值对添加到 IDictionary 对象中
        //            parameters[key] = value;
        //        }

        //        // 将当前行的数据添加到 List 中
        //        postDataList.Add(parameters);
        //    }
        //}
        ////在后台发送数据到服务器
        //public void UploadDataInBackground()
        //{
        //    Task.Run(() =>
        //    {
        //        foreach (var parameters in postDataList)
        //        {
        //            HttpWebResponse res = HttpHelper.CreatePostHttpResponse(gUrl, parameters, 2000, null, null);

        //            if (res == null)
        //            {
        //                MessageBox.Show("URL无法访问");
        //                LoggerHelper.mlog.Debug("URL无法访问");
        //                return;
        //            }

        //            // 进一步处理响应或保存响应的代码（可以根据自己的需求进行操作）
        //            string resp = HttpHelper.GetResponseString(res);

        //            XmlDocument docXml = new XmlDocument();
        //            docXml.LoadXml(resp);
        //            string str = docXml.ChildNodes[1].InnerText;

        //            if (str.ToUpper().Contains("OK"))
        //            {
        //                LoggerHelper.mlog.Debug("发送成功");
        //            }
        //        }

        //        // 清空postDataList
        //        postDataList.Clear();
        //    });
        //}


        private void tsmi_delAll_Click(object sender, EventArgs e)
        {
            // 提示用户确认删除
            DialogResult result = MessageBox.Show("是否删除所有数据？", "确认删除", MessageBoxButtons.OKCancel);

            // 根据用户的选择执行相应的操作
            if (result == DialogResult.OK)
            {
                //删除所有测试数据
                dgv_CurrentResult.Rows.Clear();
            }

        }

        //删除选中的测试结果
        private void tsmi_delselect_Click(object sender, EventArgs e)
        {
            dgv_CurrentResult.Rows.Remove(dgv_CurrentResult.CurrentRow);
        }

        //输出测试报告
        private void tsmi_export_Click(object sender, EventArgs e)
        {
            bool ret = false;
            //输出报告 
            ret = DataGridViewToExcel(dgv_CurrentResult);
           

            if (ret)
            {
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
        }

        /// <summary>
        /// DataGridView输出到CSV文件
        /// </summary>
        /// <param name="dgv">数据源文件</param>
        public bool DataGridViewToExcel(DataGridView dgv)
        {
            string defName = INI.GetValueFromIniFile("TDR", "ExportFile");
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = reportDir;
            dlg.Filter = "Execl files (*.csv)|*.csv";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为csv文件";
            dlg.FileName = reportDir + "\\" + Path.GetFileNameWithoutExtension(defName);

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
                                //columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\t";
                                //columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim();
                                if (k == 3)
                                {
                                    columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\t";
                                }
                                else
                                {
                                    columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim();
                                }
                            }
                        }
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    myStream.Close();
                    CommonFuncs.ShowMsg(eHintInfoType.hint, "导出报告成功!");
                    return true;
                }
                catch (Exception e)
                {
                    CommonFuncs.ShowMsg(eHintInfoType.error, "导出报告失败!");
                    return false;
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
            else
            {
                CommonFuncs.ShowMsg(eHintInfoType.hint, "取消导出报告操作!");
                return false;
            }
        }

        public bool Backend_Storage_DataGridViewToExcel(DataGridView dgv)
        {
            string defName = INI.GetValueFromIniFile("TDR", "ExportFile");
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = reportDir;
            dlg.Filter = "Execl files (*.csv)|*.csv";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为csv文件";
            dlg.FileName = reportDir + "\\" + Path.GetFileNameWithoutExtension(defName)+ DateTime.Now.ToString("yyyyMMddHHmmss")+"_" + ".csv";

            Stream myStream;
            myStream = dlg.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.UTF8);
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
                            if (k == 3)
                            {
                                columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim() + "\t";
                            }
                            else
                            {
                                columnValue += dgv.Rows[j].Cells[k].Value.ToString().Trim();
                            }
                        }
                    }
                    sw.WriteLine(columnValue);
                }
                sw.Close();
                myStream.Close();
                LoggerHelper.mlog.Debug("导出报告成功!");
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.mlog.Debug("导出报告失败!");
                return false;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (tsb_Pnl_ID.Text.Length == 0)
                {
                    CommonFuncs.ShowMsg(eHintInfoType.waring, "panel id 不能为空!");
                    return;
                }

                if (isExecuteComplete)
                {
                    isExecuteComplete = false;

                    if (optParam.keyMode == 1)
                    {
                        //if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                        //{
                        //    optStatus.isConnect = false;
                        //    optStatus.isGetIndex = false;
                        //    optStatus.isLoadXml = false;
                        //    return;
                        //}
                        if (isDebug == 0)
                        {
                            toDoWork();
                        }
                        else
                        {
                            toDoTest();
                        }
                    }
                }
            }
        }

        //鼠标点击单元框选，更改测试次序,同时对比LIMIT里，需要对比相应的LIMIT
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                measIndex.currentIndex = dataGridView1.CurrentCell.RowIndex; //是当前活动的单元格的行的索引 
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

        delegate void DisplayChartDelegate(Chart _chart, List<float> result);
        public void DisplayChartValue(Chart _chart, List<float> result)
        {
            if (_chart.InvokeRequired)
            {
                DisplayChartDelegate d = new DisplayChartDelegate(DisplayChartValue);
                this.Invoke(d, new object[] { _chart, result });
            }
            else
            {
                float xbegin = 0.0f;
                float xend = 0.0f;
                float yhigh = 0.0f;
                float ylow = 0.0f;


                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }

                xbegin = result.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_Begin) / 100; //有效区起始位置
                xend = result.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_End) / 100;     //有效区结束位置

                //if (string.Compare(paramList[measIndex.currentIndex].ImpedanceLimit_Unit, "%") == 0)
                //{
                //    yhigh = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 + (Convert.ToSingle(paramList[measIndex.currentIndex].Upper_limit) / 100)); //量测值上限
                //    ylow = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 + (Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit) / 100));//量测值下限
                //}
                //else
                //{
                //    //float yhigh_offset = (Convert.ToSingle(paramList[measIndex.currentIndex].Upper_limit) - Convert.ToSingle(paramList[measIndex.currentIndex].Spec))/100;
                //    //float ylow_offset = (Convert.ToSingle(paramList[measIndex.currentIndex].Spec) - Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit))/100;
                //    //yhigh = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 + yhigh_offset); //量测值上限
                //    //ylow = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 - ylow_offset);//量测值下限
                //    //float ylow_offset = (Convert.ToSingle(paramList[measIndex.currentIndex].Spec) - Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit))/100;
                yhigh = Convert.ToSingle(paramList[measIndex.currentIndex].Upper_limit);
                ylow = Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit);

                //}

                if (xend - xbegin < 3)
                {
                    //initChart();
                    gEmptyFlag = true;
                    return;
                }
                else
                {
                    gEmptyFlag = false;
                }

                //计算有效区域起始结束位置 
                chart1.ChartAreas[0].AxisY.Interval = paramList[measIndex.currentIndex].Open_hreshold / 5; //Y轴间距
                chart1.ChartAreas[0].AxisY.Maximum = paramList[measIndex.currentIndex].Open_hreshold;//设置Y坐标最大值,开路位置
                chart1.ChartAreas[0].AxisY.Minimum = 0;

                //生成上半位有效区域
                chart1.Series[1].Points.AddXY(xbegin, paramList[measIndex.currentIndex].Open_hreshold);
                chart1.Series[1].Points.AddXY(xbegin, yhigh);
                chart1.Series[1].Points.AddXY(xend, yhigh);
                chart1.Series[1].Points.AddXY(xend, paramList[measIndex.currentIndex].Open_hreshold);

                //生成下半部有效区域
                chart1.Series[2].Points.AddXY(xbegin, 0);
                chart1.Series[2].Points.AddXY(xbegin, ylow);
                chart1.Series[2].Points.AddXY(xend, ylow);
                chart1.Series[2].Points.AddXY(xend, 0);

                //获取有效区域的LIST
                List<float> tmpResult = result.Skip((int)xbegin).Take((int)(xend - xbegin)).ToList();


                //求最大值及最小值
                if (tmpResult.Count != 0)
                {
                    //设置网格间距
                    chart1.ChartAreas[0].AxisX.Interval = (float)result.Count / 10;//X轴间距
                    chart1.ChartAreas[0].AxisX.Maximum = (float)result.Count; //设置X坐标最大值
                    chart1.ChartAreas[0].AxisX.Minimum = 0;//设置X坐标最小值

                    chart1.Series[0].LegendText = "平均值:" + tmpResult.Average().ToString("F2");
                    chart1.Series[1].LegendText = "最大值:" + tmpResult.Max().ToString("F2");
                    chart1.Series[2].LegendText = "最小值:" + tmpResult.Min().ToString("F2");
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
                for (int i = 0; i < result.Count; i++)
                {
                    chart1.Series[0].Points.AddXY(i, result[i]);
                }
            }
        }


        delegate void CreateDatagridviewDelegate(DataGridView _dgv, int channel, int flag);
        public void CreateResultDatagridview(DataGridView _dgv, int channel, int flag)
        {

            if (_dgv.InvokeRequired)
            {
                CreateDatagridviewDelegate d = new CreateDatagridviewDelegate(CreateResultDatagridview);
                this.Invoke(d, new object[] { _dgv, channel, flag });
            }
            else
            {
                bool ret = false;
                float avg = 0.0f;
                float max = 0.0f;
                float min = 0.0f;

                float lowLimit = 0.0f;
                float hiLimit = 0.0f;

                if (gEmptyFlag)
                {
                    avg = 9999;
                    max = 9999;
                    min = 9999;
                }
                else
                {
                    avg = Convert.ToSingle(Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", "")); //设备平均值
                    max = Convert.ToSingle(Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", "")); //设备最大值
                    min = Convert.ToSingle(Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", "")); //设备最小值              
                }


                float stdValue = Convert.ToSingle(paramList[measIndex.currentIndex].Spec); //标准值

                //if (string.Compare(paramList[measIndex.currentIndex].ImpedanceLimit_Unit, "%") == 0)
                //{
                //    lowLimit = stdValue * (1 + StrToFloat(paramList[measIndex.currentIndex].Low_limit) / 100); //下限
                //    hiLimit = stdValue * (1 + StrToFloat(paramList[measIndex.currentIndex].Upper_limit) / 100); //上限
                //}
                //else
                //{
                hiLimit = Convert.ToSingle(paramList[measIndex.currentIndex].Upper_limit);
                lowLimit = Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit);
                //}


                //这里判定是以点的方式还是以平均值的方式来判定结果
                if (string.Compare(paramList[measIndex.currentIndex].Std, "AverageValue") == 0) //平均值的判定
                {
                    if (avg > lowLimit && avg < hiLimit)
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
                    if (((max < hiLimit) && (max > lowLimit)) && ((min < hiLimit) && (min > lowLimit)))
                    {
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                }

                if (flag == CURRENT_RECORD) //当前量测
                {
                    //这里需要添加对比
                    if (ret == false)
                    {
                        gTestResultValue = -1;
                    }
                    else
                    {
                        gTestResultValue = 1;
                    }
                }


                if (optParam.testMode == 4 && gTestResultValue == -1)
                {
                    return;
                }

                int index = _dgv.Rows.Add();
                if (ret)
                {
                    SetLableText("PASS", "Green");
                    _dgv.Rows[index].Cells[4].Value = "PASS";
                }
                else
                {
                    SetLableText("FAIL", "Red");
                    _dgv.Rows[index].Cells[4].Value = "NG";
                }

                string strUnit = paramList[measIndex.currentIndex].ImpedanceLimit_Unit;

                //if (string.Compare(strUnit, "ohms") == 0)
                //{
                strUnit = " Ohm";
                //}
                //else
                //{
                //    strUnit = " Ohm";
                //}

                //   [ImpedanceCheckId] [char](12) NULL           --ID
                //   [LotNo] [nvarchar](20) NOT NULL              --批量卡号、工单条码
                //   [WorkNo][nvarchar] (20) NULL                 --批号
                //   [ProdNo][nvarchar] (20) NULL                 --料号流水号
                //   [CheckTime][datetime] NULL                  --检查时间
                //   [CheckOp][nvarchar] (10) NULL                --检查结果
                //   [Layer][nvarchar] (10) NOT NULL              --层别
                //   [ImpedanceSpec][nvarchar] (20) NOT NULL      --标准阻抗
                //   [ImpedanceMax][numeric] (15, 3) NULL         --最大阻抗
                //   [ImpedanceMin][numeric] (15, 3) NULL         --最小阻抗
                //   [ImpedanceAVG][numeric] (15, 3) NULL         --评价阻抗
                //   [EquitMent][nvarchar] (10) NULL              --设备名称编号
                //   [FileName][nvarchar] (100) NULL              --文件名
                //   [Number][nvarchar] (10) NULL                 --稼动率0%~100%        
                //   [Operator][nvarchar] (50) NULL               --作业员
                //   [TechNo][nvarchar] (10) NULL                 --工艺
                //   [DataChainId][char] (12) NULL                --每片板数据链ID

                ////目前量测
                //_dgv.Rows[index].Cells["ImpedanceCheckId"].Value = "test id";  //ID
                //_dgv.Rows[index].Cells["LotNo"].Value = tsb_Pnl_ID.Text;     //批量卡号、工单条码
                //_dgv.Rows[index].Cells["WorkNo"].Value = tsb_Set_id.Text;    //批号
                //_dgv.Rows[index].Cells["ProdNo"].Value = optParam.snPrefix + (gSerialInc).ToString().PadLeft(6, '0');    //料号流水号

                ////_dgv.Rows[index].Cells["CheckTime"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //检查时间
                ////这样做的目的是让日志和记录可以对得上
                //_dgv.Rows[index].Cells["CheckTime"].Value = DateTime.Now.ToString("yyyy-MM-dd") +" "+ logFileName.Substring(8, logFileName.Length - 8); //检查时间

                //_dgv.Rows[index].Cells["Layer"].Value = paramList[measIndex.currentIndex].Layer; //层别
                //_dgv.Rows[index].Cells["ImpedanceSpec"].Value = paramList[measIndex.currentIndex].Spec + "+-" + (hiLimit - stdValue).ToString() + "Ohm"; //标准阻抗
                //_dgv.Rows[index].Cells["ImpedanceMax"].Value = Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", "");    //最大阻抗
                //_dgv.Rows[index].Cells["ImpedanceMin"].Value = Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", "");    //最小阻抗
                //_dgv.Rows[index].Cells["ImpedanceAVG"].Value = Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", "");    //平均阻抗
                //_dgv.Rows[index].Cells["EquitMent"].Value = "E5063A"; //设备名称编号
                //_dgv.Rows[index].Cells["FileName"].Value = paramList[measIndex.currentIndex].Curve_data; //文件路径名
                //_dgv.Rows[index].Cells["CurveImage"].Value = paramList[measIndex.currentIndex].Curve_image; //图片路径名      
                //_dgv.Rows[index].Cells["Number"].Value = "100"; //稼动率0%~100%   
                //_dgv.Rows[index].Cells["Operator"].Value = tsb_Set_operator.Text; //作业员     
                //_dgv.Rows[index].Cells["TechNo"].Value = "test"; //工艺
                //_dgv.Rows[index].Cells["DataChainId"].Value = (gSerialInc).ToString().PadLeft(6, '0'); //每片板数据链ID  

                //测试用，要删除掉
                if (isDebug != 0)
                {
                    logFileName = DateTime.Now.ToString("yyyyMMddHH:mm:ss.ff");
                }

                //目前量测
                _dgv.Rows[index].Cells[0].Value = tsb_Pnl_ID.Text;     //批量卡号、工单条码
                _dgv.Rows[index].Cells[1].Value = tsb_Set_id.Text;    //批号
                _dgv.Rows[index].Cells[2].Value = optParam.snPrefix + (gSerialInc).ToString().PadLeft(6, '0');    //料号流水号

                //_dgv.Rows[index].Cells["CheckTime"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //检查时间
                //这样做的目的是让日志和记录可以对得上
                _dgv.Rows[index].Cells[3].Value = DateTime.Now.ToString("yyyy-MM-dd") + " " + logFileName.Substring(8, logFileName.Length - 8); //检查时间

                _dgv.Rows[index].Cells[5].Value = paramList[measIndex.currentIndex].Layer; //层别
                //_dgv.Rows[index].Cells[6].Value = paramList[measIndex.currentIndex].Spec + Uri.EscapeDataString("+-") + (hiLimit - stdValue).ToString() + "Ω"; //标准阻抗

                double offset_value = hiLimit - stdValue;
                string strOffset = offset_value.ToString(offset_value % 1 == 0 ? "0" : "0.0");

                _dgv.Rows[index].Cells[6].Value = paramList[measIndex.currentIndex].Spec + "+-" + strOffset + "Ω"; //标准阻抗

                if (gEmptyFlag)
                {
                    _dgv.Rows[index].Cells[7].Value = "9999"; //平均值
                    _dgv.Rows[index].Cells[8].Value = "9999"; //最大值
                    _dgv.Rows[index].Cells[9].Value = "9999"; //最小值
                }
                else
                {
                    _dgv.Rows[index].Cells[7].Value = Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", ""); //最大值
                    _dgv.Rows[index].Cells[8].Value = Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", ""); //最小值    
                    _dgv.Rows[index].Cells[9].Value = Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", ""); //平均值             
                }



                //_dgv.Rows[index].Cells[8].Value = Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", "");    //最大阻抗
                //_dgv.Rows[index].Cells[9].Value = Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", "");    //最小阻抗
                //_dgv.Rows[index].Cells[10].Value = Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", "");    //平均阻抗




                _dgv.Rows[index].Cells[10].Value = "外层领创阻抗机"; //设备名称编号
                _dgv.Rows[index].Cells[11].Value = paramList[measIndex.currentIndex].Curve_data +"-" +tsc_cobox_test_type.Text; //文件路径名
                _dgv.Rows[index].Cells[12].Value = paramList[measIndex.currentIndex].Curve_image; //图片路径名      
                _dgv.Rows[index].Cells[13].Value = utilization_rate.ToString() + "%"; //稼动率0%~100%   
                _dgv.Rows[index].Cells[14].Value = tsb_Set_operator.Text; //作业员     
                _dgv.Rows[index].Cells[15].Value = " "; //工艺
                _dgv.Rows[index].Cells[16].Value = (gSerialInc).ToString().PadLeft(6, '0'); //每片板数据链ID  

                if (flag == CURRENT_RECORD) //当前量测
                {
                    //只有最后一个走完，流水才++
                    if (measIndex.currentIndex == paramList.Count - 1)
                    {
                        gSerialInc++;
                    }

                    //光标在最后一行
                    _dgv.CurrentCell = _dgv.Rows[_dgv.Rows.Count - 1].Cells[0];
                }
                else
                {
                    List<string> historyRecord = new List<string>();
                    //这里要写历史记录       
                    for (int j = 0; j < _dgv.Rows[index].Cells.Count; j++)
                    {
                        if (j == 3)//这里时间要加一个空格，要不会显示不正确
                        {
                            //可以将时间转换为字符串并在其前面添加引号。这样做可以确保时间以文本格式保存，而不是被解析为数值
                            //historyRecord.Add(" " + _dgv.Rows[index].Cells[j].Value.ToString());
                            historyRecord.Add($"\"{_dgv.Rows[index].Cells[j].Value.ToString()}\"");
                        }
                        else
                        {
                            historyRecord.Add(_dgv.Rows[index].Cells[j].Value.ToString());
                        }
                    }
                    string defName = INI.GetValueFromIniFile("TDR", "HistoryFile");
                    writeHistoryRecord(historyRecord, defName);
                }

            }
        }

        private void writeHistoryRecord(List<string> data, string filePath)
        {
            string fileDir = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }


            if (!File.Exists(filePath))
            {
                //不存在 
                StreamWriter fileWriter = new StreamWriter(filePath, true, Encoding.Default);
                //string str = "Layer," + "SPEC," + "Up," + "Down," + "Average," + "Max," + "Min," + "Result," + "Serial," + "Data," + "Time," + "SE/DIFF," + "CurveData," + "CurveImage," + "PanelID," + "SETID";
                string str = "LotNo," + "WorkNo," + "ProdNo," + "CheckTime," + "CheckOp" + "Layer,"
                             + "ImpedanceSpec," + "ImpedanceMax," + "ImpedanceMin," + "ImpedanceAVG," + "EquitMent,"
                             + "FileName," + "CurveImage," + "Number," + "Operator," + "TechNo," + "DataChainId";

                fileWriter.WriteLine(str);

                string strline = string.Empty;
                for (int i = 0; i < data.Count; i++)
                {
                    strline += (data[i] + ",");
                }
                fileWriter.WriteLine(strline);
                fileWriter.Flush();
                fileWriter.Close();
            }
            else
            {
                //存在
                StreamWriter fileWriter = new StreamWriter(filePath, true, Encoding.Default);
                string strline = string.Empty;
                for (int i = 0; i < data.Count; i++)
                {
                    strline += (data[i] + ",");
                }
                fileWriter.WriteLine(strline);
                fileWriter.Flush();
                fileWriter.Close();
            }
        }

        delegate void RefreshDatagridviewDelegate(DataGridView _dgv);
        public void reFreshDatagridview(DataGridView _dgv)
        {
            if (_dgv.InvokeRequired)
            {
                RefreshDatagridviewDelegate d = new RefreshDatagridviewDelegate(reFreshDatagridview);
                this.Invoke(d, new object[] { _dgv });
            }
            else
            {
                _dgv.CurrentCell = _dgv.Rows[measIndex.currentIndex].Cells[0];
            }
        }

        delegate void CaptureScreenChartDelegate(Chart _chart, string path);
        public void CaptureScreenChart(Chart _chart, string path)
        {
            if (_chart.InvokeRequired)
            {
                CaptureScreenChartDelegate d = new CaptureScreenChartDelegate(CaptureScreenChart);
                this.Invoke(d, new object[] { _chart, path });
            }
            else
            {
                try
                {
                    string fileDir = path + "\\Image";

                    if (!Directory.Exists(fileDir))
                    {
                        Directory.CreateDirectory(fileDir);
                    }

                    Point FrmP = new Point(splitContainer1.Left, splitContainer1.Top);
                    Point ScreenP = this.PointToScreen(FrmP);
                    int x = splitContainer2.SplitterDistance + ScreenP.X;
                    int y = ScreenP.Y;

                    //Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
                    Bitmap bit = new Bitmap(_chart.Width, _chart.Height);//实例化一个和窗体一样大的bitmap
                    Graphics g = Graphics.FromImage(bit);
                    g.CompositingQuality = CompositingQuality.HighSpeed;//质量设为最高
                                                                        //g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
                    g.CopyFromScreen(x, y, 0, 0, _chart.Size);//只保存某个控件
                                                              //g.CopyFromScreen(tabPage1.PointToScreen(Point.Empty), Point.Empty, tabPage1.Size);//只保存某个控件

                    bit.Save(fileDir + "\\" + logFileName.Replace(":", "").Replace(".", "") + ".png");//默认保存格式为PNG，保存成jpg格式质量不是很好    
                                                                                                      //bit.Save(fileDir + "\\" + logFileName.Replace('.','-') + ".png");//默认保存格式为PNG，保存成jpg格式质量不是很好   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("日志文件路径错误，请重新设置\r\n", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CGloabal.g_curInstrument != null)
            {
                if (CGloabal.g_curInstrument.strInstruName.Equals("E5080B"))
                {
                    //清空
                    E5080B.viClear(CGloabal.g_curInstrument.nHandle);
                    E5080B.viClose(CGloabal.g_curInstrument.nHandle);
                }
                else if (CGloabal.g_curInstrument.strInstruName.Equals("E5063A"))
                {
                    E5063A.viClear(CGloabal.g_curInstrument.nHandle);
                    E5063A.viClose(CGloabal.g_curInstrument.nHandle);
                }
                else if (CGloabal.g_curInstrument.strInstruName.Equals("E5071C"))
                {
                    //差分开路定义
                    E5071C.viClear(CGloabal.g_curInstrument.nHandle);
                    E5071C.viClose(CGloabal.g_curInstrument.nHandle);
                }
            }
        }



        //保存日志文件
        private async Task SaveResultInBackground()
        {
            bool ret = await Task.Run(() => Backend_Storage_DataGridViewToExcel(dgv_CurrentResult));
            if (ret)
            {
                // 复制到已量测表格中
                for (int i = 0; i < dgv_CurrentResult.Rows.Count; i++)
                {
                    int index = dgv_OutPutResult.Rows.Add(); // 在gridview2中添加一空行

                    // 为空行添加列值
                    for (int j = 0; j < dgv_CurrentResult.Rows[i].Cells.Count; j++)
                    {
                        dgv_OutPutResult.Rows[i].Cells[j].Value = dgv_CurrentResult.Rows[i].Cells[j].Value;
                    }
                }

                // 清空参数表格
                dgv_CurrentResult.Rows.Clear();
            }
        }

        public void ExecutePacketAndUploadData()
        {
            Task.Run(() =>
            {
                List<Dictionary<string, string>> postDataList = new List<Dictionary<string, string>>();

                if (dgv_CurrentResult.Rows.Count <= 0)
                {
                    return;
                }

                // packet_result()方法
                foreach (DataGridViewRow row in dgv_CurrentResult.Rows)
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();

                    foreach (DataGridViewColumn column in dgv_CurrentResult.Columns)
                    {
                        string key = column.HeaderText;
                        string value = row.Cells[column.Index].Value?.ToString();

                        if (key == "ImpedanceSpec")
                        {
                            value = value.Replace("+-", "%2B-");
                        }

                        parameters[key] = value;
                    }

                    postDataList.Add(parameters);
                }

                // UploadDataInBackground方法
                Task.Run(() =>
                {
                    foreach (var parameters in postDataList)
                    {
                        HttpWebResponse res = HttpHelper.CreatePostHttpResponse(gUrl, parameters, 2000, null, null);

                        if (res == null)
                        {
                            MessageBox.Show("URL无法访问");
                            LoggerHelper.mlog.Debug("URL无法访问");
                            return;
                        }

                        // 进一步处理响应或保存响应的代码（可以根据自己的需求进行操作）
                        string resp = HttpHelper.GetResponseString(res);

                        XmlDocument docXml = new XmlDocument();
                        docXml.LoadXml(resp);
                        string str = docXml.ChildNodes[1].InnerText;

                        if (str.ToUpper().Contains("OK"))
                        {
                            LoggerHelper.mlog.Debug("发送成功");
                        }
                    }

                    // 清空postDataList
                    postDataList.Clear();
                });
            });
        }


        //上传报告
        private async void tsb_trans_result_Click(object sender, EventArgs e)
        {
            //打包需要上送的数据
            //开始上送
            //packet_result();
            //UploadDataInBackground();
            //打包并上送
            ExecutePacketAndUploadData();

            await SaveResultInBackground();
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
