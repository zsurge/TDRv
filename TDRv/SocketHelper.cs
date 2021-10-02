using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace TDRv
{
    /// <summary>
    /// 玄机SocketHelper 
    /// Coding By 君临
    /// 更新时间08-03/2015
    /// 版本号: 2.6.3
    /// </summary>
    public class SocketHelper
    {
        public delegate void PushSockets(Sockets sockets);
        public static PushSockets pushSockets;  

        public class TcpClients : SocketObject
        {
            /// <summary>
            /// 是否关闭.(窗体关闭时关闭代码)
            /// </summary>
            bool IsClose = false;
            /// <summary>
            /// 当前管理对象
            /// </summary>
            Sockets sk;
            /// <summary>
            /// 客户端
            /// </summary>
            public TcpClient client;
            /// <summary>
            /// 当前连接服务端地址
            /// </summary>
            IPAddress Ipaddress;
            /// <summary>
            /// 当前连接服务端端口号
            /// </summary>
            int Port;
            /// <summary>
            /// 服务端IP+端口
            /// </summary>
            IPEndPoint ip;
            /// <summary>
            /// 发送与接收使用的流
            /// </summary>
            NetworkStream nStream;

            private static readonly Lazy<TcpClients> _instance = new Lazy<TcpClients>(() => new TcpClients());

            public static TcpClients Instance
            {
                get { return _instance.Value; }
            }



            /// <summary>
            /// 初始化Socket
            /// </summary>
            /// <param name="ipaddress"></param>
            /// <param name="port"></param>
            /// 
            public override void InitSocket(string ipaddress, int port)
            {
                Ipaddress = IPAddress.Parse(ipaddress);
                Port = port;
                ip = new IPEndPoint(Ipaddress, Port);

                if(client!=null)
                {
                    client.Close();
                }

                //设置本地端口
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 6501);
                client = new TcpClient(iep);

                //client = new TcpClient();

            }
            public override void InitSocket(int port)
            {
                Port = port;
            }
            /// <summary>
            /// 初始化Socket
            /// </summary>
            /// <param name="ipaddress"></param>
            /// <param name="port"></param>
            public override void InitSocket(IPAddress ipaddress, int port)
            {
                Ipaddress = ipaddress;
                Port = port;
                ip = new IPEndPoint(Ipaddress, Port);

                if (client != null)
                //if (client != null && client.Connected)
                {
                    client.Close();
                }

                //设置本地端口
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 6501);
                client = new TcpClient(iep);

                //client = new TcpClient();

            }
            /// <summary>
            /// 重连上端.
            /// </summary>
            /// 



            public void RestartInit()
            {
                // if(client != null&& client.Connected)
                if (client != null)
                    client.Close();

                InitSocket(Ipaddress, Port);
                Connect();
            }
            public void SendData(string SendData)
            {
                try
                {                    
                    //如果连接则发送
                    if (client != null)
                    {
                        if (client.Connected)
                        {
                            if (nStream == null)
                            {
                                nStream = client.GetStream();
                            }
                            //byte[] buffer = Encoding.Unicode.GetBytes(SendData);
                            //nStream.Write(buffer, 0, buffer.Length);

                            char HEAD = (char)0x02;
                            char TAIL = (char)0x03;
                            List<byte> lstData = new List<byte>();
                            lstData.Add((byte)HEAD);
                            byte[] stringBytes = Encoding.Unicode.GetBytes(SendData);
                            lstData.AddRange(stringBytes);
                            lstData.Add((byte)TAIL);
                            byte[] buffer = lstData.ToArray();
                            nStream.Write(buffer, 0, buffer.Length);
                            LoggerHelper._.Info(SendData);

                        }
                        else
                        {
                            Sockets sks = new Sockets();
                            sks.ErrorCode = Sockets.ErrorCodes.TrySendData;
                            sks.ex = new Exception("客户端发送时无连接,开始进行重连上端..");
                            sks.ClientDispose = true;
                            //pushSockets.Invoke(sks);//推送至UI
                            RestartInit();
                        }
                    }
                    else
                    {
                        Sockets sks = new Sockets();
                        sks.ErrorCode = Sockets.ErrorCodes.TrySendData;
                        sks.ex = new Exception("客户端对象为null,开始重连上端..");
                        sks.ClientDispose = true;
                        //pushSockets.Invoke(sks);//推送至UI 
                        RestartInit();
                    }
                }
                catch (Exception skex)
                {
                    Sockets sks = new Sockets();
                    sks.ErrorCode = Sockets.ErrorCodes.TrySendData;
                    sks.ex = new Exception("客户端出现异常,开始重连上端..异常信息:" + skex.Message);
                    sks.ClientDispose = true;
                    //pushSockets.Invoke(sks);//推送至UI
                    RestartInit();
                }
            }
            public void SendData(byte[] SendData)
            {
                try
                {
                    //如果连接则发送
                    if (client != null)
                    {
                        if (client.Connected)
                        {
                            if (nStream == null)
                            {
                                nStream = client.GetStream();
                            }

                            byte[] buffer = SendData;
                            nStream.Write(buffer, 0, buffer.Length);

                            //byte[] buffer = new byte[SendData.Length + 2];
                            //buffer[0] = 0x02;
                            //Array.Copy(SendData, 0, buffer, 1, SendData.Length);
                            //buffer[SendData.Length + 1] = 0x03;
                            //nStream.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            Sockets sks = new Sockets();
                            sks.ErrorCode = Sockets.ErrorCodes.TrySendData;
                            sks.ex = new Exception("客户端发送时无连接,开始进行重连上端..");
                            sks.ClientDispose = true;
                            pushSockets.Invoke(sks);//推送至UI
                            RestartInit();
                        }
                    }
                    else
                    {
                        Sockets sks = new Sockets();
                        sks.ErrorCode = Sockets.ErrorCodes.TrySendData;
                        sks.ex = new Exception("客户端无连接..");
                        sks.ClientDispose = true;
                        pushSockets.Invoke(sks);//推送至UI 
                    }
                }
                catch (Exception skex)
                {
                    Sockets sks = new Sockets();
                    sks.ErrorCode = Sockets.ErrorCodes.TrySendData;
                    sks.ex = new Exception("客户端出现异常,开始重连上端..异常信息:" + skex.Message);
                    sks.ClientDispose = true;
                    pushSockets.Invoke(sks);//推送至UI
                    RestartInit();
                }
            }

            private void Connect()
            {
                try
                {
                    //连接远程服务器
                    client.Connect(ip);
                    //client.Connect(new IPEndPoint(IPAddress.Parse("10.8.26.54"), 6501));
                    //client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.0"), 6501));

                    nStream = new NetworkStream(client.Client, true);

                    sk = new Sockets(ip, client, nStream);
                    sk.nStream.BeginRead(sk.RecBuffer, 0, sk.RecBuffer.Length, new AsyncCallback(EndReader), sk);
                    //推送连接成功.
                    Sockets sks = new Sockets();
                    sks.ErrorCode = Sockets.ErrorCodes.ConnectSuccess;
                    sks.ex = new Exception("客户端连接成功.");
                    sks.ClientDispose = false;
                    pushSockets.Invoke(sks);
                }
                catch (Exception skex)
                {
                    Sockets sks = new Sockets();
                    sks.ErrorCode = Sockets.ErrorCodes.ConnectError;
                    sks.ex = new Exception("客户端连接失败..异常信息:" + skex.Message);
                    sks.ClientDispose = true;
                    pushSockets.Invoke(sks);
                }

            }
            private void EndReader(IAsyncResult ir)
            {
                Sockets s = ir.AsyncState as Sockets;
                try
                {
                    if (s != null)
                    {
                        if (IsClose && client == null)
                        {
                            sk.nStream.Close();
                            sk.nStream.Dispose();
                            return;
                        }
                        s.Offset = s.nStream.EndRead(ir);
                        pushSockets.Invoke(s);//推送至UI
                        sk.nStream.BeginRead(sk.RecBuffer, 0, sk.RecBuffer.Length, new AsyncCallback(EndReader), sk);
                    }
                }
                catch (Exception skex)
                {
                    Sockets sks = s;
                    sks.ex = skex;
                    sks.ClientDispose = true;
                    //pushSockets.Invoke(sks);//推送至UI
                }

            }
            /// <summary>
            /// 重写Start方法,其实就是连接服务端
            /// </summary>
            public override void Start()
            {
                Connect();
            }
            public override void Stop()
            {
                Sockets sks = new Sockets();
                if (client != null)
                {
                    client.Client.Shutdown(SocketShutdown.Both);
                    Thread.Sleep(10);
                    client.Close();
                    IsClose = true;
                    client = null;
                }
                else
                {
                    sks.ex = new Exception("客户端没有初始化.!");
                }
                sks.ex = new Exception("客户端与上端断开连接..");
                pushSockets.Invoke(sks);//推送至UI
            }


           
        }
        /// <summary>
        /// Socket基类(抽象类)
        /// 抽象3个方法,初始化Socket(含一个构造),停止,启动方法.
        /// 此抽象类为TcpServer与TcpClient的基类,前者实现后者抽象方法.
        /// 对象基类
        /// </summary>
        public abstract class SocketObject
        {
            public abstract void InitSocket(IPAddress ipaddress, int port);
            public abstract void InitSocket(string ipaddress, int port);
            public abstract void InitSocket(int port);
            public abstract void Start();
            public abstract void Stop();

        }
        /// <summary>
        /// 自定义Socket对象
        /// </summary>
        public class Sockets
        {
            /// <summary>
            /// 接收缓冲区
            /// </summary>
            public byte[] RecBuffer = new byte[1024*1024];
            /// <summary>
            /// 发送缓冲区
            /// </summary>
            public byte[] SendBuffer = new byte[1024*512];
            /// <summary>
            /// 异步接收后包的大小
            /// </summary>
            public int Offset { get; set; }
            /// <summary>
            /// 空构造
            /// </summary>
            public Sockets() { }
            /// <summary>
            /// 创建Sockets对象
            /// </summary>
            /// <param name="ip">Ip地址</param>
            /// <param name="client">TcpClient</param>
            /// <param name="ns">承载客户端Socket的网络流</param>
            public Sockets(IPEndPoint ip, TcpClient client, NetworkStream ns)
            {
                Ip = ip;
                Client = client;
                nStream = ns;
            }
            /// <summary>
            /// 当前IP地址,端口号
            /// </summary>
            public IPEndPoint Ip { get; set; }
            /// <summary>
            /// 客户端主通信程序
            /// </summary>
            public TcpClient Client { get; set; }
            /// <summary>
            /// 承载客户端Socket的网络流
            /// </summary>
            public NetworkStream nStream { get; set; }
            /// <summary>
            /// 发生异常时不为null.
            /// </summary>
            public Exception ex { get; set; }
            /// <summary>
            /// 异常枚举
            /// </summary>
            public ErrorCodes ErrorCode { get; set; }
            /// <summary>
            /// 新客户端标识.如果推送器发现此标识为true,那么认为是客户端上线
            /// 仅服务端有效
            /// </summary>
            public bool NewClientFlag { get; set; }
            /// <summary>
            /// 客户端退出标识.如果服务端发现此标识为true,那么认为客户端下线
            /// 客户端接收此标识时,认为客户端异常.
            /// </summary>
            public bool ClientDispose { get; set; }

            /// <summary>
            /// 具体错误类型
            /// </summary>
            public enum ErrorCodes
            {
                /// <summary>
                /// 对象为null
                /// </summary>
                objectNull,
                /// <summary>
                /// 连接时发生错误
                /// </summary>
                ConnectError,
                /// <summary>
                /// 连接成功.
                /// </summary>
                ConnectSuccess,
                /// <summary>
                /// 尝试发送失败异常
                /// </summary>
                TrySendData,
            }
        }
    }
}
