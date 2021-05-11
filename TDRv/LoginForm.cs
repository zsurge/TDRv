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

namespace TDRv
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (tx_UserName.Text.Equals("") || tx_PassWord.Text.Equals(""))//用户名或密码为空
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            else//用户名或密码不为空
            {
                if (String.Compare(tx_UserName.Text, "tsj") == 0 && String.Compare(tx_PassWord.Text, "123456") == 0)
                {
                    //写日志，用户名及登录时间
                    SocketHelper.TcpClients.Instance.InitSocket("192.168.2.106", 6800);
                    SocketHelper.TcpClients.Instance.Start();

                    //if (SocketHelper.TcpClients.Instance.client.Connected)
                    //{
                    //    // 开启心跳线程
                    //    Thread t = new Thread(new ThreadStart(Heartbeat));
                    //    t.IsBackground = true;
                    //    t.Start();
                    //}

                    // 跳转主界面
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }

            }
        }

        //private void Heartbeat()
        //{
        //    while (SocketHelper.TcpClients.Instance.client.Connected)
        //    {
        //        // 向服务端发送心跳包
        //        SocketHelper.TcpClients.Instance.SendData("111111");

        //        Thread.Sleep(5000);
        //    }
        //}

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
                            break;
                        case SocketHelper.Sockets.ErrorCodes.ConnectSuccess:
                            LoggerHelper._.Trace("连接成功.!");
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
                    }
                }
            }));
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            SocketHelper.pushSockets = new SocketHelper.PushSockets(Rec);//注册推送器
        }
    }
}
