using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    public class CGloabal
    {
        public class InstrMentsParas
        {
            public string strInstruName;    //仪器名字
            public string adress;           //地址
            public int port;                //端口号
            public bool bInternet;          //连接状态， 0断开  1连接
            public int nHandle;             //句柄   

            public InstrMentsParas(string name)
            {
                this.strInstruName = name;
                this.adress = "172.141.10.30";
                this.port = 8000;
                this.bInternet = false;
                this.nHandle = 0; //默认为0
            }
        };

        //定义五个仪器的对象
        public static InstrMentsParas g_InstrE5080BModule = new InstrMentsParas("E5080B");
        public static InstrMentsParas g_InstrE5063AModule = new InstrMentsParas("E5063A");

        public static InstrMentsParas g_curInstrument; //表征当前正在操作的仪器
        public static int g_nSimulteFlag = 0;  //整机模拟状态标示 false,真实状态；true，模拟状态
        public static string strExecuteBeginDir;
    }


}
