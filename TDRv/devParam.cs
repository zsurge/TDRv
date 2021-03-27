using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    class devParam
    {        
        public string Id { get; set; }
        public string TestStep { get; set; }
        public string Description { get; set; }
        public string Layer { get; set; }
        public string Remark { get; set; }
        public string ImpedanceDefine { get; set; }
        public string ImpedanceLimitLower { get; set; }
        public string ImpedanceLimitUpper { get; set; }
        public string ImpedanceLimitUnit { get; set; }
        public string InputChannel { get; set; }
        public string InputMode { get; set; }
        public string TestMethod { get; set; }
        public string TestFromThreshold { get; set; }
        public string TestToThreshold { get; set; }
        public string OpenThreshold { get; set; }
        public string TraceStartPosition { get; set; }
        public string TraceEndPosition { get; set; }
        public string CalibratedTimeScale { get; set; }
        public string CalibrateOffset { get; set; }
        public string RecordPath { get; set; }
        public string SaveCurve { get; set; }
        public string SaveImage { get; set; }
        public string DielectricConstant { get; set; }
        public string DataPointCheck { get; set; }


    }//end class
}//end namespace
