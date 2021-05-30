using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
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

        //SOCKET通信接口
        IDevToHost dev = new IDevToHost();

        //设置参数设置窗体的表数据
        DataTable gdt;

        //获取当前
        public string exPortFilePath = string.Empty;

        //差分
        public const int DIFFERENCE = 1;

        //单端
        public const int SINGLE = 2;

        //用于区分测试结果的模式（这里并不合理，可以进行datagridview复制，然后再自动保存）
        public const int CURRENT_RECORD = 100;
        public const int HISTORY_RECORD = 999;

        //流水
        public int gSerialInc = 0;

        //E5080B analyzer = new E5080B();

        private bool gEmptyFlag = true; //标志是否有空的待测物

        //标志位，用于标志是否进行下一步
        public static int gTestResultValue = 0;

        //以当前时间为名字的文件名
        public static string logFileName = string.Empty;

        public static bool isExecuteComplete = true;
        public static bool isExecuteIndex = true;

        //配方列表
        List<TestResult> paramList = new List<TestResult>();

        //记录已测试到第几层
        MeasIndex measIndex = new MeasIndex();

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
            if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
            {
                optStatus.isConnect = false;
                optStatus.isGetIndex = false;
                optStatus.isLoadXml = false;
                tsb_DevPOptSet.Enabled = false;
                return;
            }

            DevOptSet devOptSet = new DevOptSet();
            devOptSet.Show();
        }

        private void tsb_DevParamSet_Click(object sender, EventArgs e)
        {
            if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
            {
                optStatus.isConnect = false;
                optStatus.isGetIndex = false;
                optStatus.isLoadXml = false;
                tsb_DevPOptSet.Enabled = false;
                return;
            }

            DevParamSet devParamSet = new DevParamSet(gdt);
            devParamSet.ChangeDgv += new DevParamSet.ChangeDgvHandler(Change_DataGridView);
            devParamSet.Show();
        }

        //子窗体传回配方参数
        public void Change_DataGridView(DataGridView dt)
        {
            if (dgv_CurrentResult.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("将要清除测试数据，是否保存","提示",MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    DataGridViewToExcel(dgv_CurrentResult);
                }
            }

            //清除开路定义，需重新开路
            optStatus.isGetIndex = false;
            tsb_StartTest.Enabled = false;

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
            MeasPosition.tdd11start = 0;
            MeasPosition.tdd22IndexValue = 0;
            MeasPosition.tdd22start = 0;

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
                    MeasPosition.tdd11start = Convert.ToInt32(dt.Rows[i].Cells[14].Value);
                    diff = false;
                    tr.DevMode = DIFFERENCE;
                }

                if (string.Compare(dt.Rows[i].Cells[10].Value.ToString(), "SingleEnded") == 0 && single) //单端
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

        /// <summary>
        /// 处理推送过来的消息
        /// </summary>
        /// <param name="rec"></param>
        private void Rec(SocketHelper.Sockets sks)
        {
            this.Invoke(new ThreadStart(delegate
            {
                if (sks.ex != null)
                {
                    //在这里判断ErrorCode  可以自由扩展
                    switch (sks.ErrorCode)
                    {
                        case SocketHelper.Sockets.ErrorCodes.objectNull:
                            break;
                        case SocketHelper.Sockets.ErrorCodes.ConnectError:
                            break;
                        case SocketHelper.Sockets.ErrorCodes.ConnectSuccess:
                            LoggerHelper._.Trace("连接成功.!");
                            break;
                        case SocketHelper.Sockets.ErrorCodes.TrySendData:
                            break;
                        default:
                            break;
                    }
                    LoggerHelper._.Trace(string.Format("客户端信息{0}", sks.ex));

                }
                else
                {

                    //release
                    //byte[] buffer = new byte[sks.Offset - 2];  
                    //Array.Copy(sks.RecBuffer, 1, buffer, 0, sks.Offset - 2);

                    //DEBUG 20210510
                    byte[] buffer = new byte[sks.Offset];
                    Array.Copy(sks.RecBuffer, 0, buffer, 0, sks.Offset);

                    string stohbuff = string.Empty;
                    string ret = string.Empty;

                    //string str = Encoding.Unicode.GetString(buffer);
                    string str = Encoding.UTF8.GetString(buffer);
                    if (str == "ServerOff")
                    {
                        LoggerHelper._.Trace("服务端主动关闭");
                    }
                    else
                    {
                        LoggerHelper._.Trace(string.Format("服务端{0}发来消息：{1}", sks.Ip, str) + "\r\n");                     


                        switch (QueryElementByName(str).Replace(" ", "").ToUpper())
                        {
                            //响应初始数据访问
                            case "INITIALDATAREQUEST":
                                //1.这里记录日志
                                LoggerHelper._.Info("开始处理初始化请求");
                                //2.获取要发送到服务器的数据
                                ISendToHost _devInitResp = new DevInitResp();
                                _devInitResp.eventSend += new DelegateSend(dev._DevInitResp);
                                stohbuff = _devInitResp.packetXmlData();
                                //3.发送到服务器                                
                                SocketHelper.TcpClients.Instance.SendData(stohbuff);         
                                
                                LoggerHelper._.Info("初始化返回数据为：" + stohbuff);
                                break;
                            //响应校验时间
                            case "DATETIMESYNCCOMMAND":
                                //1.这里记录日志
                                LoggerHelper._.Info("开始处理校时请求");
                                //2.获取要发送到服务器的数据
                                ISendToHost _syncTimeResp = new SyncTimeResp();
                                _syncTimeResp.eventSend += new DelegateSend(dev._SyncTimeResp);
                                stohbuff = _syncTimeResp.packetXmlData();
                                //3.发送到服务器
                                SocketHelper.TcpClients.Instance.SendData(stohbuff);
                                LoggerHelper._.Info("校时返回数据为：" + stohbuff);
                                break;
                            //响应任务信息下载
                            case "JOBDATADOWNLOAD":
                                //1.这里记录日志
                                LoggerHelper._.Info("开始处理JOB下载任务");
                                //2.获取要发送到服务器的数据
                                ISendToHost _jobDownResp = new JobDownResp();
                                _jobDownResp.eventSend += new DelegateSend(dev._JobDownResp);
                                stohbuff = _jobDownResp.packetXmlData();
                                //3.发送到服务器
                                SocketHelper.TcpClients.Instance.SendData(stohbuff);
                                LoggerHelper._.Info("JOB下载任务返回：" + stohbuff);
                                break;

                            //响应人员上机确认
                            case "OPERATORLOGINCONFIRM":
                                //1.这里记录日志
                                LoggerHelper._.Info("开始处理人员上机确认任务");
                                //2.获取要发送到服务器的数据
                                ISendToHost _loginConfrim = new LoginConfrim();
                                _loginConfrim.eventSend += new DelegateSend(dev._LoginConfrim);
                                stohbuff = _loginConfrim.packetXmlData();
                                //3.发送到服务器
                                SocketHelper.TcpClients.Instance.SendData(stohbuff);
                                LoggerHelper._.Info("人员上机确认：" + stohbuff);
                                break;
                            //-----------------------------------------------------------------------------------------------
                            //处理查询服务状态后的返回信息4.1
                            case "AREYOUTHEREREQUESTREPLY":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "eqp_id");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 响应对方是否存在. result = " + ret);
                                break;
                            //机台当前控制模式4.14
                            case "EQUIPMENTCONTROLMODEREPLY":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "return_code");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 响应机台工作模式 result = " + ret);
                                break;
                            //设备上报当前时间4.16
                            case "EQUIPMENTCURRENTDATETIMEREPLY":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "return_code");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 设备上报当前时间 result = " + ret);
                                break;
                            //人员上下岗报告4.20
                            case "OPERATORLOGINLOGOUTREPORTREPLY":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "return_code");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 人员下机报告 result = " + ret);
                                break;
                            //4.23
                            case "PANELREADREPORTREPLY":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "return_code");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 读板报告 result = " + ret);
                                break;
                            //4.26
                            case "EQUIPMENTRECIPESETUPREPORTREPLY":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "return_code");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 机台配方参数调用报告 result = " + ret);
                                break;
                            //4.34
                            case "PROCESSDATAREPORTREPLY":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "return_code");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 制程/量测数据报告 result = " + ret);
                                break;

                        }

                    }
                }
            }));
        }

        /// <summary>
        /// 查询指定元素的子元素
        /// </summary>
        /// <param name="xmlData">xml字符串数据</param>
        /// <param name="strSubElement">指定的元素</param>
        /// <param name="lastSubElement">指定的远素</param>
        public string QueryElementByName(string xmlData, string strSubElement = "header", string lastSubElement = "messagename")
        {
            string cmd = string.Empty;

            if (xmlData.Length == 0)
            {
                return cmd;
            }


            //为了测试才打开 注释于20210511 为了方便测试 码制不同
            //if (xmlData.Substring(0, 2).CompareTo("02") != 0 && xmlData.Substring(xmlData.Length - 2, 2).CompareTo("03") != 0)
            //{
            //    LoggerHelper._.Error("错误的包头包尾:" + xmlData);
            //    return cmd;
            //}


            //-4 去除包头包尾
            //XElement xe = XElement.Parse(xmlData.Substring(2, xmlData.Length - 4));

            //注释于20210511 为了方便测试 码制不同
            XElement xe = XElement.Parse(xmlData); 

            ///查询元素
            var elements = xe.Elements(strSubElement).Descendants(lastSubElement).ToList();

            if (elements.Count < 1)
            {
                return cmd;
            }

            cmd = elements[0].Value;

            LoggerHelper._.Info("当前接收到的指令是：" + cmd);
            return cmd;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //注册推送器 接收SOCKET数据
            SocketHelper.pushSockets = new SocketHelper.PushSockets(Rec);

            //创建默认文件夹
            CreateDefaultDir();

            //获取当前测试模式
            ReadTestMode();

            //获取序列号起始值
            gSerialInc =  Convert.ToInt32(optParam.snBegin);
          
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

                if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    optStatus.isConnect = false;
                    optStatus.isGetIndex = false;
                    optStatus.isLoadXml = false;
                    tsb_GetTestIndex.Enabled = false;
                    return;
                }

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

                //差分开路定义
                E5080B.getStartIndex(CGloabal.g_InstrE5080BModule.nHandle, DIFFERENCE, out result);

                //这里需要处理win1_tr1的数据
                tmpSingleMeasData = packetMaesData(result, 0, 0);
                string[] tdd11_array = result.Split(new char[] { ',' });
                result = string.Empty;

                if (tdd11_array.Length < 200)
                {
                    MessageBox.Show("获取差分开路定义失败");
                }

                //查找tdd11单端的索引值
                for (int i = 0; i < tdd11_array.Length; i++)
                {
                    //logger.Trace(tdd22_array[i]);
                    if (Convert.ToSingle(tdd11_array[i]) >= Convert.ToSingle(MeasPosition.tdd11start))
                    {
                        MeasPosition.tdd11IndexValue = i - 1;
                        //这里需要将开路定义后的索引写入到配方的XML文件中去
                        break;
                    }
                }

                //单端开路定义
                result = string.Empty;
                E5080B.getStartIndex(CGloabal.g_InstrE5080BModule.nHandle, SINGLE, out result);

                tmpDiffMeasData = packetMaesData(result, 0, 0);
                string[] tdd22_array = result.Split(new char[] { ',' });

                //查找tdd22单端的索引值
                for (int i = 0; i < tdd22_array.Length; i++)
                {
                    if (Convert.ToSingle(tdd22_array[i]) >= Convert.ToSingle(MeasPosition.tdd22start))
                    {
                        MeasPosition.tdd22IndexValue = i - 1;
                        //这里需要将开路定义后的索引写入到配方的XML文件中去
                        break;
                    }
                }

                MeasPosition.isOpen = true;
                optStatus.isGetIndex = true;
                tsb_StartTest.Enabled = true;

                CreateInitMeasChart(tmpDiffMeasData, tmpSingleMeasData);
            }
        }



/*
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

            System.Threading.Thread.Sleep(200);

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
*/



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


            if (mode == DIFFERENCE) //差分模式
            {
                for (i = index; i < tmpArray.Length; i++)
                {
                    tmp = Convert.ToSingle(tmpArray[i]) + paramList[measIndex.currentIndex].Offset;
                    if (tmp < Convert.ToSingle(MeasPosition.tdd11start))
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
            else if (mode == SINGLE)//单端模式
            {
                for (i = index; i < tmpArray.Length; i++)
                {
                    tmp = Convert.ToSingle(tmpArray[i]) + paramList[measIndex.currentIndex].Offset;
                    if (tmp < Convert.ToSingle(MeasPosition.tdd22start))
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

            logFileName = DateTime.Now.ToString("yyyyMMddhh:mm:ss.ff");
            SaveDataToCSVFile(result, logFileName);

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

            isExecuteIndex = true;
            isExecuteComplete = true;
        }


        /// <summary>
        /// Save the List data to CSV file
        /// </summary>
        /// <param name="studentList">data source</param>
        /// <param name="filePath">file path</param>
        /// <returns>success flag</returns>
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

            string spath = CurveDir + "\\"+ fileName.Replace(":","").Replace(".","")+".csv";

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

            if (xend - xbegin < 10)
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


        private void startMeasuration(int channel)
        {
            string result = string.Empty;
            int index = 0;


            if (channel == SINGLE)
            {
                index = MeasPosition.tdd22IndexValue;
            }
            else
            {
                index = MeasPosition.tdd11IndexValue;  
            }


            E5080B.measuration(CGloabal.g_InstrE5080BModule.nHandle, channel, out result);

            //获取要生成报表的数据
            CreateMeasChart(packetMaesData(result, index, channel));

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

        delegate void SetLableCB(string text,string color);
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


        /// <summary>
        /// 更新测试结果到datagridview中去
        /// </summary>
        /// <param name="channel">这个好像不需要？</param>
        private bool upgradeTestResult(int channel)
        {
            bool ret = false;
            float avg = 0;
            float max = 0;
            float min = 0;

            if (gEmptyFlag)
            {
                avg = 9999;
                max = 9999;
                min = 9999;
            }
            else
            {
                avg = StrToFloat(Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", "")); //设备平均值
                max = StrToFloat(Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", "")); //设备最大值
                min = StrToFloat(Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", "")); //设备最小值              
            }


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
                SetLableText("PASS", "Green");
                this.dgv_CurrentResult.Rows[index].Cells[7].Value = "PASS";     
                this.dgv_HistoryResult.Rows[history_index].Cells[7].Value = "PASS";
            }
            else
            {
                SetLableText("FAIL", "Red");
                this.dgv_CurrentResult.Rows[index].Cells[7].Value = "FAIL";
                this.dgv_HistoryResult.Rows[history_index].Cells[7].Value = "FAIL";
            }

            
            //目前量测
            this.dgv_CurrentResult.Rows[index].Cells[0].Value = paramList[measIndex.currentIndex].Layer;  //layer
            this.dgv_CurrentResult.Rows[index].Cells[1].Value = paramList[measIndex.currentIndex].Spec;     //标准值
            this.dgv_CurrentResult.Rows[index].Cells[2].Value = paramList[measIndex.currentIndex].Upper_limit;  //最大上限比例 
            this.dgv_CurrentResult.Rows[index].Cells[3].Value = paramList[measIndex.currentIndex].Low_limit;    //最小下限比例

            if (gEmptyFlag)
            {
                this.dgv_CurrentResult.Rows[index].Cells[4].Value = "∞"; //平均值
                this.dgv_CurrentResult.Rows[index].Cells[5].Value = "∞"; //最大值
                this.dgv_CurrentResult.Rows[index].Cells[6].Value = "∞"; //最小值
            }
            else
            {
                this.dgv_CurrentResult.Rows[index].Cells[4].Value = Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", ""); //平均值
                this.dgv_CurrentResult.Rows[index].Cells[5].Value = Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", ""); //最大值
                this.dgv_CurrentResult.Rows[index].Cells[6].Value = Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", ""); //最小值             
            }

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

            if (gEmptyFlag)
            {
                this.dgv_HistoryResult.Rows[history_index].Cells[4].Value = "9999"; //平均值
                this.dgv_HistoryResult.Rows[history_index].Cells[5].Value = "9999"; //最大值
                this.dgv_HistoryResult.Rows[history_index].Cells[6].Value = "9999"; //最小值
            }
            else
            {
                this.dgv_HistoryResult.Rows[history_index].Cells[4].Value = Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", ""); //平均值
                this.dgv_HistoryResult.Rows[history_index].Cells[5].Value = Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", ""); //最大值
                this.dgv_HistoryResult.Rows[history_index].Cells[6].Value = Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", ""); //最小值
            }


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
            if (isExecuteComplete)
            {
                isExecuteComplete = false;
                
                if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                {
                    optStatus.isConnect = false;
                    optStatus.isGetIndex = false;
                    optStatus.isLoadXml = false;
                    tsb_StartTest.Enabled = false;
                    return;
                }

                toDoWork();
            }
        }

        public void toDoWork()
        {
            var task1 = new Task(() =>
            {           

                if (optStatus.isConnect && optStatus.isGetIndex)
                {
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

                    E5080B.measuration(CGloabal.g_InstrE5080BModule.nHandle, channel, out result);

                    //量测并生成图表
                    //startMeasuration(paramList[measIndex.currentIndex].DevMode);
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
                    //CaptureScreen(paramList[measIndex.currentIndex].Curve_image);
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

            if(paramList[measIndex.currentIndex].Curve_image.Length != 0)
            {
                task1.ContinueWith((Task) =>
                {
                    CaptureScreenChart(chart1, paramList[measIndex.currentIndex].Curve_image);
                });
            }  
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
            string defName = INI.GetValueFromIniFile("TDR", "ExportFile"); ;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = reportDir;
            dlg.Filter = "Execl files (*.csv)|*.csv";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为csv文件";    
            dlg.FileName = reportDir + "//" + Path.GetFileNameWithoutExtension(defName);            

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
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("导出报告失败！");
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
                MessageBox.Show("取消导出报告操作!");
                return false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (isExecuteComplete)
                {
                    isExecuteComplete = false;                    

                    if (optParam.keyMode == 1)
                    {
                        if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                        {
                            optStatus.isConnect = false;
                            optStatus.isGetIndex = false;
                            optStatus.isLoadXml = false;
                            return;
                        }
                        toDoWork();
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
                float xbegin = 0;
                float xend = 0;
                float yhigh = 0;
                float ylow = 0;


                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }

                xbegin = result.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_Begin) / 100; //有效区起始位置
                xend = result.Count * Convert.ToSingle(paramList[measIndex.currentIndex].Valid_End) / 100;     //有效区结束位置

                if (string.Compare(paramList[measIndex.currentIndex].ImpedanceLimit_Unit, "%") == 0)
                {
                    yhigh = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 + (Convert.ToSingle(paramList[measIndex.currentIndex].Upper_limit) / 100)); //量测值上限
                    ylow = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 + (Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit) / 100));//量测值下限
                }
                else
                {
                    float yhigh_offset = (Convert.ToSingle(paramList[measIndex.currentIndex].Upper_limit) - Convert.ToSingle(paramList[measIndex.currentIndex].Spec))/100;
                    float ylow_offset = (Convert.ToSingle(paramList[measIndex.currentIndex].Spec) - Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit))/100;
                    yhigh = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 + yhigh_offset); //量测值上限
                    ylow = Convert.ToSingle(paramList[measIndex.currentIndex].Spec) * (1 - ylow_offset);//量测值下限
                }

                if (xend - xbegin < 10)
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
                for (int i = 0; i < result.Count; i++)
                {
                    chart1.Series[0].Points.AddXY(i, result[i]);
                }
            }
        }


        delegate void CreateDatagridviewDelegate(DataGridView _dgv, int channel,int flag);
        public void CreateResultDatagridview(DataGridView _dgv, int channel,int flag)
        {

            if (_dgv.InvokeRequired)
            {
                CreateDatagridviewDelegate d = new CreateDatagridviewDelegate(CreateResultDatagridview);
                this.Invoke(d, new object[] { _dgv, channel ,flag});
            }
            else
            {         
                bool ret = false;
                float avg = 0;
                float max = 0;
                float min = 0;

                float lowLimit = 0;
                float hiLimit = 0;

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

                if (string.Compare(paramList[measIndex.currentIndex].ImpedanceLimit_Unit, "%") == 0)
                {
                    lowLimit = stdValue * (1 + StrToFloat(paramList[measIndex.currentIndex].Low_limit) / 100); //下限
                    hiLimit = stdValue * (1 + StrToFloat(paramList[measIndex.currentIndex].Upper_limit) / 100); //上限
                }
                else
                {
                    float hi_offset = (Convert.ToSingle(paramList[measIndex.currentIndex].Upper_limit) - Convert.ToSingle(paramList[measIndex.currentIndex].Spec)) / 100;
                    float low_offset = (Convert.ToSingle(paramList[measIndex.currentIndex].Spec) - Convert.ToSingle(paramList[measIndex.currentIndex].Low_limit)) / 100;
                    hiLimit = stdValue * (1 + hi_offset);
                    lowLimit = stdValue * (1 - low_offset);
                }
                

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
                    _dgv.Rows[index].Cells[7].Value = "PASS";     
                }
                else
                {
                    SetLableText("FAIL", "Red");
                    _dgv.Rows[index].Cells[7].Value = "FAIL";             
                }

                string strUnit = paramList[measIndex.currentIndex].ImpedanceLimit_Unit;
                if (string.Compare(strUnit, "ohms") == 0)
                {
                    strUnit = " Ohm";
                }
                else
                {
                    strUnit = " %";
                }

                //目前量测
                _dgv.Rows[index].Cells[0].Value = paramList[measIndex.currentIndex].Layer;  //layer
                _dgv.Rows[index].Cells[1].Value = paramList[measIndex.currentIndex].Spec;     //标准值
                _dgv.Rows[index].Cells[2].Value = paramList[measIndex.currentIndex].Upper_limit + strUnit;  //最大上限比例 
                _dgv.Rows[index].Cells[3].Value = paramList[measIndex.currentIndex].Low_limit + strUnit;    //最小下限比例

                if (gEmptyFlag)
                {
                    _dgv.Rows[index].Cells[4].Value = "9999"; //平均值
                    _dgv.Rows[index].Cells[5].Value = "9999"; //最大值
                    _dgv.Rows[index].Cells[6].Value = "9999"; //最小值
                }
                else
                {
                    _dgv.Rows[index].Cells[4].Value = Regex.Replace(chart1.Series[0].LegendText, @"[^\d.\d]", ""); //平均值
                    _dgv.Rows[index].Cells[5].Value = Regex.Replace(chart1.Series[1].LegendText, @"[^\d.\d]", ""); //最大值
                    _dgv.Rows[index].Cells[6].Value = Regex.Replace(chart1.Series[2].LegendText, @"[^\d.\d]", ""); //最小值             
                }

                _dgv.Rows[index].Cells[8].Value = optParam.snPrefix + (gSerialInc).ToString().PadLeft(6, '0'); //流水号
                _dgv.Rows[index].Cells[9].Value = DateTime.Now.ToString("yyyy-MM-dd");    //日期 
                _dgv.Rows[index].Cells[10].Value = logFileName.Substring(8, logFileName.Length - 8);     //时间
                _dgv.Rows[index].Cells[11].Value = paramList[measIndex.currentIndex].Mode;    //当前模式，单端or差分
                _dgv.Rows[index].Cells[12].Value = paramList[measIndex.currentIndex].Curve_data; //记录存放地址
                _dgv.Rows[index].Cells[13].Value = paramList[measIndex.currentIndex].Curve_image; //截图存放地址           


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
                        historyRecord.Add(_dgv.Rows[index].Cells[j].Value.ToString());
                    }
                    string defName = INI.GetValueFromIniFile("TDR", "HistoryFile"); 
                    writeHistoryRecord(historyRecord, defName);
                }

            }
        }

        private void writeHistoryRecord(List<string>data, string filePath)
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
                string str = "Layer," + "SPEC," + "Up," + "Down," + "Average," + "Max," + "Min," + "Result," + "Serial," + "Data," + "Time," + "SE/DIFF," + "CurveData," + "CurveImage";
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
                    strline += (data[i]+",");
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
                this.Invoke(d, new object[] { _dgv});
            }
            else
            {
                _dgv.CurrentCell = _dgv.Rows[measIndex.currentIndex].Cells[0];
            }
        }

        delegate void CaptureScreenChartDelegate(Chart _chart,string path);
        public void CaptureScreenChart(Chart _chart, string path)
        {
            if (_chart.InvokeRequired)
            {
                CaptureScreenChartDelegate d = new CaptureScreenChartDelegate(CaptureScreenChart);
                this.Invoke(d, new object[] { _chart , path });
            }
            else
            {
                //string fileDir = path + "\\Image";

                //if (!Directory.Exists(fileDir))
                //{
                //    Directory.CreateDirectory(fileDir);
                //}

                Point FrmP = new Point(splitContainer1.Left, splitContainer1.Top);
                Point ScreenP = this.PointToScreen(FrmP);
                int x = splitContainer2.SplitterDistance + ScreenP.X;
                int y = ScreenP.Y;

                //Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
                Bitmap bit = new Bitmap(_chart.Width, _chart.Height);//实例化一个和窗体一样大的bitmap
                Graphics g = Graphics.FromImage(bit);
                g.CompositingQuality = CompositingQuality.HighSpeed;//质量设为最高
                //g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
                g.CopyFromScreen(x,y,0,0, _chart.Size);//只保存某个控件
                //g.CopyFromScreen(tabPage1.PointToScreen(Point.Empty), Point.Empty, tabPage1.Size);//只保存某个控件
                
                bit.Save(imageDir + "\\" + logFileName.Replace(":", "").Replace(".", "") + ".png");//默认保存格式为PNG，保存成jpg格式质量不是很好    
               //bit.Save(fileDir + "\\" + logFileName.Replace('.','-') + ".png");//默认保存格式为PNG，保存成jpg格式质量不是很好   
            }
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
