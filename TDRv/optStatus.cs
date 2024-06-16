using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    public static class optStatus
    {
        public static bool isConnect { get; set; } = false;
        public static bool isLoadXml { get; set; } = false;
        public static bool isGetIndex { get; set; } = false;        
    }

    public static class optParam
    {
        //键盘默认模式，0是关闭，1是启用
        public static int keyMode { get; set; } = 1;

        //流水号前缀
        public static string snPrefix { get; set; } = "SN";

        //起始流水号
        public static string snBegin { get; set; } = "0001";

        //测试模式 1 通过；2 手动 3 直接下一笔 4 仅记录通过
        public static int testMode { get; set; } = 3;

        //报告的存储方式 1按日期；2.按量测参数
        public static int exportMode { get; set; } = 1;

        //测试方式，1：即时确认 2:常规测试；
        public static int realCheck { get; set; } = 2;


        //补偿值
        public static string offsetValue { get; set; } = "";

        //历史报告的默认文件名
        public static string historyExportFileName { get; set; } = "TDR_Project_History";

        //输出报告的默认文件名
        public static string outputExportFileName { get; set; } = "TDR_Project_Export.csv";
    }
}
