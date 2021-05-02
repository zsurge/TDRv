using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TDRv
{
    class INI
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        //section: 要写入的段落名
        //key: 要写入的键，如果该key存在则覆盖写入
        //val: key所对应的值
        //filePath: INI文件的完整路径和文件名
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        //section：要读取的段落名
        // key: 要读取的键
        //def: 读取异常的情况下的缺省值
        //retVal: key所对应的值，如果该key不存在则返回空值
        //size: 值允许的大小
        //filePath: INI文件的完整路径和文件名
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

        private string sPath = null;
        //读取配置文件


        public INI()
        {
            this.sPath = Directory.GetCurrentDirectory()+ "\\Config.ini";
        }

        public void WriteValueToIniFile(string section, string key, string value)
        {        
            // section=配置节，key=键名，value=键值，path=路径
            WritePrivateProfileString(section, key, value, sPath);
        }

        public string GetValueFromIniFile(string section, string key)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(1024);
 
            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 1024, sPath);

            return temp.ToString();
        }
    }
}
