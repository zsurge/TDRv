﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDRv.Driver;

namespace TDRv 
{
    public partial class DevConnectSet : Form
    {
        public DevConnectSet()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        public delegate void ChangeTsbHandler(string addr);  //定义委托
        public event ChangeTsbHandler ChangeValue;  //定义事件

     
        private void SetParentFormTsbControl()
        {
            if (ChangeValue != null)
            {
                ChangeValue(combDevString.Text);
            }
        }

        private void btn_ConnectDev_Click(object sender, EventArgs e)
        {
            string sn = string.Empty;

            string NowDate = DateTime.Now.ToString("yyyyMMdd");

            if (20210817 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
            {
                optStatus.isConnect = false;
                combDevString.BackColor = Color.Red;
                return;
            }

            CGloabal.g_InstrE5080BModule.adress = combDevString.Text;

            INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
            INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

            CGloabal.g_curInstrument = CGloabal.g_InstrE5080BModule;

            int ret = E5080B.Open(CGloabal.g_InstrE5080BModule.adress, ref CGloabal.g_InstrE5080BModule.nHandle);

            E5080B.GetInstrumentIdentifier(CGloabal.g_InstrE5080BModule.nHandle, out sn);

            if (sn.Contains("MY59101265") || sn.Contains("MY59101017"))
            {               

                if (ret != 0)
                {
                    optStatus.isConnect = false;
                    combDevString.BackColor = Color.Red;
                    MessageBox.Show("error!");
                }
                else
                {
                    optStatus.isConnect = true;
                    combDevString.BackColor = Color.Green;
                }
            }
            else
            {
                optStatus.isConnect = false;
                combDevString.BackColor = Color.Red;      
            }
        }

        private void DevConnectSet_Load(object sender, EventArgs e)
        {            
            combDevString.BackColor = SystemColors.Window;
            combDevString.Text = INI.GetValueFromIniFile("Instrument", "AddressNA");
            CGloabal.g_InstrE5080BModule.adress = INI.GetValueFromIniFile("Instrument", "AddressNA");
        }
    }
}
