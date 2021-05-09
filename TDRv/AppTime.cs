using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDRv
{
    class AppTime
    {
        public static int InitRegedit()
        {
            /*检查注册表*/
            string SericalNumber = ReadSetting("", "SerialNumber", "-1");    // 读取注册表， 检查是否注册 -1为未注册
            if (SericalNumber == "-1")
            {
                return 1;
            }

            /* 比较CPUid */
            //string CpuId = GetSoftEndDateAllCpuId(1, SericalNumber);   //从注册表读取CPUid
            //string CpuIdThis = GetCpuId();           //获取本机CPUId         
            //if (CpuId != CpuIdThis)
            //{
            //    return 2;
            //}

            /* 比较时间 */
            string NowDate = AppTime.GetNowDate();
            string EndDate = AppTime.GetSoftEndDateAllCpuId(0, SericalNumber);
            if (Convert.ToInt32(EndDate) - Convert.ToInt32(NowDate) < 0)
            {
                return 3;
            }

            return 0;
        }

        public static string GetNowDate()
        {
            string NowDate = DateTime.Now.ToString("yyyyMMdd"); //.Year + DateTime.Now.Month + DateTime.Now.Day).ToString();  
            return NowDate;
        }

        public static string CreatSerialNumber()
        {
            //string SerialNumber = GetCpuId() + "-" + DateTime.Now.ToString("yyyyMMdd");
            string SerialNumber = DateTime.Now.ToString("yyyyMMdd");
            return SerialNumber;
        }


        /* 
         * i=1 得到 CUP 的id 
         * i=0 得到上次或者 开始时间 
         */
        public static string GetSoftEndDateAllCpuId(int i, string SerialNumber)
        {
            if (i == 1)
            {
                string cupId = SerialNumber.Substring(0, SerialNumber.LastIndexOf("-"));

                return cupId;
            }
            if (i == 0)
            {
                //string dateTime = SerialNumber.Substring(SerialNumber.LastIndexOf("-") + 1);           
                return SerialNumber;
            }
            else
            {
                return string.Empty;
            }
        }

        /*写入注册表*/
        public static void WriteSetting(string Section, string Key, string Setting)  // name = key  value=setting  Section= path
        {
            string text1 = Section;
            RegistryKey key1 = Registry.CurrentUser.CreateSubKey("Software\\SZ_TSJ\\TDRvTest");
            if (key1 == null)
            {
                return;
            }
            try
            {
                key1.SetValue(Key, Setting);
            }
            catch (Exception exception1)
            {
                return;
            }
            finally
            {
                key1.Close();
            }
        }

        /*读取注册表*/
        public static string ReadSetting(string Section, string Key, string Default)
        {
            if (Default == null)
            {
                Default = "-1";
            }
            string text2 = Section;
            RegistryKey key1 = Registry.CurrentUser.OpenSubKey("Software\\SZ_TSJ\\TDRvTest");
            if (key1 != null)
            {
                object obj1 = key1.GetValue(Key, Default);
                key1.Close();
                if (obj1 != null)
                {
                    if (!(obj1 is string))
                    {
                        return "-1";
                    }
                    string obj2 = obj1.ToString();
                    return obj2;
                }
                return "-1";
            }


            return Default;
        }

    }//end Class
}
