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
        public string Description { get; set; } = "50";//"Description_Default";
        public string Layer { get; set; } = "L1";//"Layer1";
        public string Remark { get; set; } = "";
        public string ImpedanceDefine { get; set; } = "50";
        public string ImpedanceLimitLower { get; set; } = "-10";
        public string ImpedanceLimitUpper { get; set; } = "10";
        public string ImpedanceLimitUnit { get; set; } = "%";

        public string ImpedanceMax { get; set; } = "125";
        public string ImpedanceMaxLimitLower { get; set; } = "-10";
        public string ImpedanceMaxLimitUpper { get; set; } = "10";

        public string ImpedanceMin { get; set; } = "30";
        public string ImpedanceMinLimitLower { get; set; } = "-10";
        public string ImpedanceMinLimitUpper { get; set; } = "10";

        public string InputChannel { get; set; } = "1";
        public string InputMode { get; set; } = "SingleEnded";
        public string TestMethod { get; set; } = "Enable";
        //public string TestFromThreshold { get; set; } = "50";
        public string TestFromThreshold { get; set; } = "30";
        public string TestToThreshold { get; set; } = "70";
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
