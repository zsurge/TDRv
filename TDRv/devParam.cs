using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    class devParam
    {          
        public string Id { get; set; } = "";
        public int TestStep { get; set; } = 1;
        public string Description { get; set; } = "Description_Default";
        public string Layer { get; set; } = "Layer1";
        public string Remark { get; set; } = "Default";
        public string ImpedanceDefine { get; set; } = "50";
        public string ImpedanceLimitLower { get; set; } = "45";
        public string ImpedanceLimitUpper { get; set; } = "55";
        public string ImpedanceLimitUnit { get; set; } = "ohms";
        public string InputChannel { get; set; } = "1";
        public string InputMode { get; set; } = "SingleEnded";
        public string TestMethod { get; set; } = "Enable";
        public string TestFromThreshold { get; set; } = "20";
        public string TestToThreshold { get; set; } = "80";
        public string OpenThreshold { get; set; } = "125";
        public string TraceStartPosition { get; set; } = "20";
        public string TraceEndPosition { get; set; } = "80";
        public string CalibratedTimeScale { get; set; } = "0";
        public string CalibrateOffset { get; set; } = "0";
        public string RecordPath { get; set; } = @"D:\";
        public string SaveCurve { get; set; } = "Enable";
        public string SaveImage { get; set; } = "Disable";
        public string DielectricConstant { get; set; } = "4.2";
        public string DataPointCheck { get; set; } = "AverageValue";
    }
}
