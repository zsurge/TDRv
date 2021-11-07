using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TDRv.Driver;

namespace TDRv 
{
    public partial class DevConnectSet : Form
    {
        public DevConnectSet()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        public delegate void ChangeTsbHandler(string addr);  //定义委托
        public event ChangeTsbHandler ChangeValue;  //定义事件

        public string GetCuerrtTime()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssxxx");
        }

        private void SetParentFormTsbControl()
        {
            if (ChangeValue != null)
            {
                ChangeValue(combDevString.Text);
            }
        }

        private void btn_ConnectDev_Click(object sender, EventArgs e)
        {
            string sn = string.Empty;

            string NowDate = DateTime.Now.ToString("yyyyMMdd");

            if (20211217 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
            {
                optStatus.isConnect = false;
                combDevString.BackColor = Color.Red;
                return;
            }

            CGloabal.g_InstrE5080BModule.adress = combDevString.Text;

            INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
            INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

            CGloabal.g_curInstrument = CGloabal.g_InstrE5080BModule;

            int ret = E5080B.Open(CGloabal.g_InstrE5080BModule.adress, ref CGloabal.g_InstrE5080BModule.nHandle);

            E5080B.GetInstrumentIdentifier(CGloabal.g_InstrE5080BModule.nHandle, out sn);

            //INI.WriteValueToIniFile("Instrument", "SN", sn);

            if (sn.Contains("MY59101265") || sn.Contains("MY59101017") || sn.Contains("MY59201572"))
            {               

                if (ret != 0)
                {
                    optStatus.isConnect = false;
                    combDevString.BackColor = Color.Red;
                    MessageBox.Show("error!");
                }
                else
                {
                    optStatus.isConnect = true;
                    combDevString.BackColor = Color.Green;
                }
            }
            else
            {
                optStatus.isConnect = false;
                combDevString.BackColor = Color.Red;      
            }
        }

        private void DevConnectSet_Load(object sender, EventArgs e)
        {
            //注册推送器 接收SOCKET数据
            //SocketHelper.pushSockets = new SocketHelper.PushSockets(Rec);
            SocketHelper.pushSockets += Rec;

            combDevString.BackColor = SystemColors.Window;
            combDevString.Text = INI.GetValueFromIniFile("Instrument", "AddressNA");
            CGloabal.g_InstrE5080BModule.adress = INI.GetValueFromIniFile("Instrument", "AddressNA");

            if (INI.GetValueFromIniFile("NetWork", "ServerIP").Length > 8)
            {
                tx_server_ip.Text = INI.GetValueFromIniFile("NetWork", "ServerIP");
            }

            if (INI.GetValueFromIniFile("NetWork", "ServerPORT").Length > 0)
            {
                tx_server_port.Text = INI.GetValueFromIniFile("NetWork", "ServerPORT");
            }

            if (INI.GetValueFromIniFile("Instrument", "SN").Length > 0)
            {
                tx_sn.Text = INI.GetValueFromIniFile("Instrument", "SN");
            }

            if (string.Compare(INI.GetValueFromIniFile("ControlMode", "Mode"), "OnLine") == 0)
            {
                comb_control_mode.SelectedIndex = 0;
            }
            else
            {
                comb_control_mode.SelectedIndex = 1;
            }

        }

        private void Rec(SocketHelper.Sockets sks)
        {
            if (sks.Client == null)
            {
                return;
            }

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
                            break;
                        case SocketHelper.Sockets.ErrorCodes.TrySendData:
                            break;
                        default:
                            break;
                    }
                    //logger.Trace(string.Format("客户端信息{0}", sks.ex));
                    //LoggerHelper._.Trace(string.Format("客户端信息{0}", sks.ex));
                }
                else
                {
                    if (sks.Offset >= 2)
                    {
                        byte[] buffer = new byte[sks.Offset - 2];
                        //Array.Copy(sks.RecBuffer, buffer, sks.Offset);

                        Array.Copy(sks.RecBuffer, 1, buffer, 0, sks.Offset - 2);

                        string stohbuff = string.Empty;
                        string ret = string.Empty;

                        string str = Encoding.Unicode.GetString(buffer);
                        if (str == "ServerOff")
                        {
                            //LoggerHelper._.Trace("服务端主动关闭");
                        }
                        else
                        {
                            //LoggerHelper._.Trace(string.Format("服务端{0}发来消息：{1}", sks.Ip, str) + "\r\n");

                            switch (QueryElementByName(str).Replace(" ", "").ToUpper())
                            {
                                //机台当前控制模式4.14
                                case "EQUIPMENTCONTROLMODEREPLY":
                                    //1.处理返回数据
                                    ret = QueryElementByName(str, "body", "return_code");
                                    //2.记录日志
                                    //LoggerHelper._.Info("HOST 响应机台工作模式 result = " + ret);
                                    break;

                            }
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

            //注释于20210511 为了方便测试 码制不同
            XElement xe = XElement.Parse(xmlData);

            ///查询元素
            var elements = xe.Elements(strSubElement).Descendants(lastSubElement).ToList();

            if (elements.Count < 1)
            {
                return cmd;
            }

            cmd = elements[0].Value;

            //LoggerHelper._.Info("当前元素值为：" + cmd);
            return cmd;

        }

        public string ControlModeResp(string mode)
        {
            string xmlbuf = string.Empty;

            XDocument xmldata = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement("message",
                new XElement("header",
                    new XElement("messagename", "EquipmentControlMode"),
                    new XElement("transactionid", GetCuerrtTime())),
                new XElement("body",
                    new XElement("eqp_id", optParam.devSn),
                    new XElement("control_mode", mode)),
                new XElement("return",
                    new XElement("returncode", " "),
                    new XElement("returnmessage", " "))));
            xmlbuf = xmldata.Declaration.ToString() + xmldata.ToString();

            return xmlbuf;
        }


        private void btn_setServerIP_Click(object sender, EventArgs e)
        {
            if (tx_server_ip.Text.Length != 0 && tx_server_port.Text.Length != 0)
            {
                INI.WriteValueToIniFile("NetWork", "ServerIP", tx_server_ip.Text);
                INI.WriteValueToIniFile("NetWork", "ServerPORT", tx_server_port.Text);
            }
            else
            {
                MessageBox.Show("IP地址或PORT不能为空");
            }
        }

        private void btn_SetControlMode_Click(object sender, EventArgs e)
        {
            INI.WriteValueToIniFile("ControlMode", "Mode", comb_control_mode.Text);
            string sendBuff = string.Empty;
            if (comb_control_mode.Text.Equals("OnLine"))
            {
                //1.打包数据       
                sendBuff = ControlModeResp("2");
            }
            else
            {
                sendBuff = ControlModeResp("1");
            }

            //2.发送到服务器
            SocketHelper.TcpClients.Instance.SendData(sendBuff);
        }

        private void btn_SetSn_Click(object sender, EventArgs e)
        {
            if (tx_sn.Text.Length != 0)
            {
                INI.WriteValueToIniFile("Instrument", "SN", tx_sn.Text);         
            }
            else
            {
                MessageBox.Show("EQP_ID不能为空");
            }
        }
    }//end class
}//end namespace
