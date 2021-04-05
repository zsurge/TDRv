using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    class TestResult
    {
        private string layer = string.Empty; //这个是配方上显示的layer，跟current_index不是同一个概念
        private string upper_limit = string.Empty;
        private string low_limit = string.Empty;
        private string spec = string.Empty;        
        private string average = string.Empty;
        private string max = string.Empty;
        private string min = string.Empty;
        private string serial = string.Empty;
        private string result = string.Empty;
        private string date = string.Empty;
        private string time = string.Empty;
        private string mode = string.Empty;
        private string std = string.Empty;
        private string curve_data = string.Empty;
        private string curve_image = string.Empty;
        private string valid_begin = string.Empty; //有效的起始量测位置
        private string valid_end = string.Empty;    //有效的结束量测位置
        private int devmode = 0;        //为区分当前测试模式是单端还是差分
        private int current_index = 0;   //当前配方的索引,配方有几层，每块板子都要测试几条数据，由该参数来决定当前层的测试要求和测试结果

        public string Layer
        {
            get { return layer; }
            set { layer = value; }
        }
        public string Upper_limit
        {
            get { return upper_limit; }
            set { upper_limit = value; }
        }
        public string Low_limit
        {
            get { return low_limit; }
            set { low_limit = value; }
        }

        public string Spec
        {
            get { return spec; }
            set { spec = value; }
        }
        public string Average
        {
            get { return average; }
            set { average = value; }
        }
        public string Max
        {
            get { return max; }
            set { max = value; }
        }

        public string Min
        {
            get { return min; }
            set { min = value; }
        }
        public string Valid_Begin
        {
            get { return valid_begin; }
            set { valid_begin = value; }
        }

        public string Valid_End
        {
            get { return valid_end; }
            set { valid_end = value; }
        }
        public string Serial
        {
            get { return serial; }
            set { serial = value; }
        }
        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        public string Mode
        {
            get { return mode; }
            set { mode = value; }
        }
        public string Std
        {
            get { return std; }
            set { std = value; }
        }
        public int DevMode
        {
            get { return devmode; }
            set { devmode = value; }
        }
        public string Curve_data
        {
            get { return curve_data; }
            set { curve_data = value; }
        }
        public string Curve_image
        {
            get { return curve_image; }
            set { curve_image = value; }
        }
        public int Current_Index
        {
            get { return current_index; }
            set { current_index = value; }
        }
    }
}
