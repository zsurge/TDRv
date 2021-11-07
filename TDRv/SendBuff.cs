using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    public class SendData
    {

        public static string layer { get; set; }
        public static string upper_limit { get; set; }
        public static string low_limit { get; set; }
        public static string spec { get; set; }
        public static string average { get; set; }
        public static string max { get; set; }
        public static string min { get; set; }
        public static string mode { get; set; }
        public static string result { get; set; }
        public static string pannelid { get; set; }
        public static string setid { get; set; }
        public static string serialNumber { get; set; }
    }


    public class SendToHost
    {

        public string layer { get; set; }
        public string upper_limit { get; set; }
        public string low_limit { get; set; }
        public string spec { get; set; }
        public string average { get; set; }
        public string max { get; set; }
        public string min { get; set; }
        public string mode { get; set; }
        public string result { get; set; }
        public string pannelid { get; set; }
        public string setid { get; set; }
        public string test_date { get; set; }
        public string test_time { get; set; }
    }
}



