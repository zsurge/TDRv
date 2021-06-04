using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TDRv
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件
        }

        private SerialPort ComDevice = new SerialPort();

        public string GetCuerrtTime()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssxxx");
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            login();
        }

        public void login()
        {
            string stohbuff = string.Empty;

            if (tx_UserName.Text.Equals("") || tx_PassWord.Text.Equals("") || tx_server_ip.Text.Equals("") || tx_server_port.Equals(""))//用户名或密码为空
            {
                MessageBox.Show("服务器地址或用户名密码不能为空");
            }
            else//用户名或密码不为空
            {
                //写日志，用户名及登录时间
                SocketHelper.TcpClients.Instance.InitSocket(tx_server_ip.Text, Convert.ToInt32(tx_server_port.Text));
                SocketHelper.TcpClients.Instance.Start();


                if (SocketHelper.TcpClients.Instance.client.Connected)
                {
                    //2.打包要发送到服务器的数据
                    stohbuff = LoginReportPacket(tx_UserName.Text, "1");
                    //3.发送到服务器                                
                    SocketHelper.TcpClients.Instance.SendData(stohbuff);
                }

                //if (SocketHelper.TcpClients.Instance.client.Connected)
                //{
                //    // 开启心跳线程
                //    Thread t = new Thread(new ThreadStart(Heartbeat));
                //    t.IsBackground = true;
                //    t.Start();
                //}

                //// 跳转主界面
                //this.DialogResult = DialogResult.OK;
                //this.Dispose();
                //this.Close();
            }
        }

        //private void Heartbeat()
        //{
        //    while (SocketHelper.TcpClients.Instance.client.Connected)
        //    { 
        //        // 向服务端发送心跳包
        //        SocketHelper.TcpClients.Instance.SendData(HeartbeatPacket());

        //        Thread.Sleep(60000);
        //    }
        //}

        private delegate void UpdateUiTextDelegate(byte[] text);
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            byte[] ReDatas = new byte[ComDevice.BytesToRead];
            ComDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据

            if (ReDatas.Length != 0)
                this.Invoke(new UpdateUiTextDelegate(AddData), ReDatas);

        }

        public void AddData(byte[] data)
        {
            byte[] tmp = new byte[1024];
            string str = string.Empty;

            //StringBuilder sb = new StringBuilder();
            //for (int i = 1; i < data.Length-4; i++)
            //{
            //    sb.AppendFormat("{0:x2}" + " ", data[i]);
            //}
            //str = sb.ToString().ToUpper().Replace(" ", "");

            if (data[0] == 0x02 && data[data.Length - 1] == 0x03)
            {
                byte[] tmpID = new byte[10];
                Array.Copy(data, 1, tmpID, 0, data.Length - 4);
                tx_UserName.Text = new ASCIIEncoding().GetString(tmpID);
                login();
            }
        }


        public void OpenSerialPort()
        {
            ComDevice.PortName = INI.GetValueFromIniFile("COM", "PORT");
            ComDevice.BaudRate = Convert.ToInt32(INI.GetValueFromIniFile("COM", "BaudRate"));
            ComDevice.Parity = (Parity)(0);
            ComDevice.DataBits = Convert.ToInt32(INI.GetValueFromIniFile("COM", "DataBits"));
            ComDevice.StopBits = (StopBits)(1);


            try
            {
                ComDevice.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件
        }



        private void btn_Login_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
                            MessageBox.Show("连接失败，未找到服务器!");
                            LoggerHelper._.Error("连接失败，未找到服务器!");
                            INI.WriteValueToIniFile("NetWork", "Status", "OffLine");
                            break;
                        case SocketHelper.Sockets.ErrorCodes.ConnectSuccess:
                            LoggerHelper._.Trace("连接成功!");
                            INI.WriteValueToIniFile("NetWork", "Status", "OnLine");
                            INI.WriteValueToIniFile("NetWork", "ServerIP", tx_server_ip.Text);
                            INI.WriteValueToIniFile("NetWork", "ServerPORT", tx_server_port.Text);
                            break;
                        case SocketHelper.Sockets.ErrorCodes.TrySendData:
                            break;
                        default:
                            break;
                    }
                    //logger.Trace(string.Format("客户端信息{0}", sks.ex));
                    LoggerHelper._.Trace(string.Format("客户端信息{0}", sks.ex));
                }
                else
                {
                    byte[] buffer = new byte[sks.Offset - 2];
                    //Array.Copy(sks.RecBuffer, buffer, sks.Offset);

                    Array.Copy(sks.RecBuffer, 1, buffer, 0, sks.Offset - 2);

                    string stohbuff = string.Empty;
                    string ret = string.Empty;

                    string str = Encoding.Unicode.GetString(buffer);
                    if (str == "ServerOff")
                    {
                        LoggerHelper._.Trace("服务端主动关闭");
                    }
                    else
                    {
                        LoggerHelper._.Trace(string.Format("服务端{0}发来消息：{1}", sks.Ip, str) + "\r\n");

                        switch (QueryElementByName(str).Replace(" ", "").ToUpper())
                        {
                            //响应上机报告
                            case "OPERATORLOGINLOGOUTREPORTREPLY":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "return_code");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 人员下机报告 result = " + ret);
                                break;
                            //响应人员上机确认
                            case "OPERATORLOGINCONFIRM":
                                //1.处理返回数据
                                ret = QueryElementByName(str, "body", "result");
                                //2.记录日志
                                LoggerHelper._.Info("HOST 人员下机报告 result = " + ret);

                                if(ret.Equals("1"))
                                {
                                    //3.打包数据
                                    stohbuff = string.Empty;
                                    stohbuff = LoginConfrimPacket("1");

                                    //4.发送到服务器
                                    SocketHelper.TcpClients.Instance.SendData(stohbuff);
                                    // 跳转主界面
                                    this.DialogResult = DialogResult.OK;
                                    this.Dispose();
                                    this.Close();
                                }
                                else
                                {
                                    //3.打包数据
                                    stohbuff = string.Empty;
                                    stohbuff = LoginConfrimPacket("0");

                                    //4.发送到服务器
                                    SocketHelper.TcpClients.Instance.SendData(stohbuff);
                                }
                                break;
                        }
                    }


                }
            }));
        }

        //查询主机是否存在,心跳包
        public string HeartbeatPacket()
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "AreYouThereRequest"),
                    new XElement("transactionid", GetCuerrtTime())),
                new XElement("body",
                    new XElement("eqp_id", optParam.devSn),
                    new XElement("server_ip", tx_server_ip.Text)),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf = xmldata.Declaration.ToString() + xmldata.ToString();

            return xmlbuf;
        }


        //上机报告确认
        public string LoginConfrimPacket(string ret)
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "OperatorLoginConfirmReply"),
                    new XElement("transactionid", GetCuerrtTime())),
                new XElement("body",
                    new XElement("eqp_id", optParam.devSn),                   
                    new XElement("return_code", ret)),
                new XElement("return",
                    new XElement("returncode", ""),
                    new XElement("returnmessage", ""))));
            xmlbuf = xmldata.Declaration.ToString() + xmldata.ToString();

            return xmlbuf;
        }

        //上机请求打包
        public string LoginReportPacket(string operator_id,string identify_type)
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "OperatorLoginLogoutReport"),
                    new XElement("transactionid", GetCuerrtTime())),
                new XElement("body",
                    new XElement("eqp_id", optParam.devSn),
                    new XElement("identify_type", identify_type),
                    new XElement("operator_id", operator_id)),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf = xmldata.Declaration.ToString() + xmldata.ToString();

            return xmlbuf;
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

            LoggerHelper._.Info("当前元素值为：" + cmd);
            return cmd;

        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
            SocketHelper.pushSockets = new SocketHelper.PushSockets(Rec);//注册推送器

            if(INI.GetValueFromIniFile("NetWork", "ServerIP").Length >8)
            {
                tx_server_ip.Text = INI.GetValueFromIniFile("NetWork", "ServerIP");
            }

            if (INI.GetValueFromIniFile("NetWork", "ServerPORT").Length > 0)
            {
                tx_server_port.Text = INI.GetValueFromIniFile("NetWork", "ServerPORT");
            }

            optParam.devSn = INI.GetValueFromIniFile("Instrument", "SN");

            tx_UserName.Focus();

            OpenSerialPort();

        }
    }
}
