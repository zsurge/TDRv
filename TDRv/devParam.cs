using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    class devParam
    {          
        //id
        public string Id { get; set; } = "";
        
        //序号
        public int TestStep { get; set; } = 1;

        //描述
        public string Description { get; set; } = "50";//"Description_Default";
        
        //层别
        public string Layer { get; set; } = "L1";//"Layer1";
        
        //标记
        public string Remark { get; set; } = "";

        //标准值
        public string ImpedanceDefine { get; set; } = "50";

        //下限
        public string ImpedanceLimitLower { get; set; } = "-10";

        //上限
        public string ImpedanceLimitUpper { get; set; } = "10";

        //单位
        public string ImpedanceLimitUnit { get; set; } = "%";

        //输入通道
        public string InputChannel { get; set; } = "1";
        
        //测量模式
        public string InputMode { get; set; } = "SingleEnded";


        public string TestMethod { get; set; } = "Enable";
        

        //起始位置
        public string TestFromThreshold { get; set; } = "30";

        //结束位置
        public string TestToThreshold { get; set; } = "70";

        //开路位置
        public string OpenThreshold { get; set; } = "125";


        public string TraceStartPosition { get; set; } = "0";
        public string TraceEndPosition { get; set; } = "0";
        public string CalibratedTimeScale { get; set; } = "0";
        public string CalibrateOffset { get; set; } = "0";
        public string RecordPath { get; set; } = "";
        public string SaveCurve { get; set; } = "Enable";
        public string SaveImage { get; set; } = "Disable";
        public string DielectricConstant { get; set; } = "4.2";
        public string DataPointCheck { get; set; } = "DataPoints";
    }
}
