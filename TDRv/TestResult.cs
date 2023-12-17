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
        private string upper_limit = string.Empty; //最大上限比例 
        private string low_limit = string.Empty;  //最小下限比例
        private string impedanceLimit_Unit = string.Empty; //阻抗单位
        private string spec = string.Empty;        //标准值
        private string average = string.Empty;   //设备计算出的平均值
        private string max = string.Empty;      //设备计算出的最大值
        private string min = string.Empty;      //设备计算出的最小值
        private string serial = string.Empty;   //序列号
        private string result = string.Empty;   //测试结果 
        private string date = string.Empty;     //测试日期
        private string time = string.Empty;     //测试时间
        private string mode = string.Empty;    //为区分当前测试模式是单端还是差分
        private string std = string.Empty;      //区分是测试模式是平均还是每个点
        private string curve_data = string.Empty;   //报告存储位置
        private string curve_image = string.Empty;  //截图存储位置
        private string valid_begin = string.Empty; //有效的起始量测位置
        private string valid_end = string.Empty;    //有效的结束量测位置
        private float offset = 0;                     //测试结果偏移量
        private int devmode = 0;        //为区分当前测试模式是单端还是差分
        private int total_item = 0;   //当前配方的总的条数
        private int open_threshold = 0;  //开路位置

        //新增标准最大值和标准最小值，判定时，需要同时判定最大值和最小值，平均值是否全部通过，才算是PASS
        //标准值，          上限，             下限
        //spec             upper_limit      low_limit  
        //最大值,           上限，             下限
        //spec_max         spec_max_upper    spec_max_low
        //最小值，          上限，             下限
        //spec_min         spec_min_upper    spec_min_low
        private string spec_max = string.Empty;         //最大值
        private string spec_max_upper = string.Empty;   //最大值的上限比例 
        private string spec_max_low = string.Empty;     //最小值的下限比例
        private string spec_min = string.Empty;     //最小值
        private string spec_min_upper = string.Empty;     //最小值的上限比例 
        private string spec_min_low = string.Empty;     //最小值的下限比例


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
        
        public string ImpedanceLimit_Unit
        {
            get { return impedanceLimit_Unit; }
            set { impedanceLimit_Unit = value; }
        }

        public string Spec
        {
            get { return spec; }
            set { spec = value; }
        }

        public string Spec_max
        {
            get { return spec_max; }
            set { spec_max = value; }
        }
        public string Spec_min
        {
            get { return spec_min; }
            set { spec_min = value; }
        }

        public string Max_hi_limit
        {
            get { return spec_max_upper; }
            set { spec_max_upper = value; }
        }
        public string Max_low_limit
        {
            get { return low_limit; }
            set { low_limit = value; }
        }

        public string Min_hi_limit
        {
            get { return spec_min_upper; }
            set { spec_min_upper = value; }
        }
        public string Min_low_limit
        {
            get { return spec_min_low; }
            set { spec_min_low = value; }
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

        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public int Total_Item
        {
            get { return total_item; }
            set { total_item = value; }
        }
        public int Open_hreshold
        {
            get { return open_threshold; }
            set { open_threshold = value; }
        }
    }
}
