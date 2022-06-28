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


            if (combDevType.Text.Contains("E5080B"))
            {
                CGloabal.g_InstrE5080BModule.adress = combDevString.Text;

                INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
                INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

                CGloabal.g_curInstrument = CGloabal.g_InstrE5080BModule;

                int ret = E5080B.Open(CGloabal.g_curInstrument.adress, ref CGloabal.g_curInstrument.nHandle);

                E5080B.GetInstrumentIdentifier(CGloabal.g_curInstrument.nHandle, out sn);

                if (sn.Contains("MY59101265") || sn.Contains("MY59101017") || sn.Contains("MY60213234"))
                {

                    if (20220210 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59101009") || sn.Contains("MY59100173"))
                {

                    if (20211007 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59100856") || sn.Contains("MY59100857")) //add 2022.04.02 to 2022.04.12
                {

                    if (20220412 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59201567"))
                {

                    if (20220910 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY58100210"))//临时ID，试用一个月
                {
                    if (20220522 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY59100164"))//临时ID，试用15天
                {
                    if (20220615 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY59101203")) //add 2022.05.27 临时增加测试使用
                {

                    if (20220630 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59202202")) //add 2022.05.27 临时增加测试使用
                {

                    if (20220902 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59202302")) //add 2022.05.27 临时增加测试使用
                {

                    if (20220802 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59201572"))
                {

                    if (20211107 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59201793"))
                {

                    if (20221015 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59201890"))//modify 0427
                {

                    if (20220830 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59201701")) //永久
                {

                    if (20991230 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY59100175")) //add 2021.11.22 测试样机使用
                {

                    if (20220122 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
            }//end 5080B
            else if (combDevType.Text.Contains("E5063A"))
            {
                CGloabal.g_InstrE5063AModule.adress = combDevString.Text;

                INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
                INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

                CGloabal.g_curInstrument = CGloabal.g_InstrE5063AModule;

                int ret = E5063A.Open(CGloabal.g_curInstrument.adress, ref CGloabal.g_curInstrument.nHandle);

                LoggerHelper.mlog.Debug("E5063A.Open ret = " + ret.ToString());

                LoggerHelper.mlog.Debug("nInstrumentHandle = " + CGloabal.g_curInstrument.nHandle.ToString());

                E5063A.GetInstrumentIdentifier(CGloabal.g_curInstrument.nHandle, out sn);

                E5063A.GetThreePortIdentifier(CGloabal.g_curInstrument.nHandle);

                E5063A.ClearAllErrorQueue(CGloabal.g_curInstrument.nHandle);

                //深圳超能 改为20210130
                if (sn.Contains("MY54504547"))
                {
                    //已付款
                    if (20990301 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY54605417"))//unlock
                {
                    if (21000101 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                //add 2022.01.15
                else if (sn.Contains("MY54705656"))
                {
                    if (20220820 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                //add 2022.01.18
                else if (sn.Contains("MY54705720"))
                {
                    if (20220718 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                //add 2022.03.23
                else if (sn.Contains("MY54705796"))
                {
                    if (20991230 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                //modify 2021.11.30 delay 2021.12.30
                else if (sn.Contains("MY54605475") || sn.Contains("MY54605474") || sn.Contains("MY54605473"))
                {
                    if (20220630 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
            }//end E5063A
            else if (combDevType.Text.Contains("E5071C"))
            {
                CGloabal.g_InstrE5071CModule.adress = combDevString.Text;

                INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
                INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

                CGloabal.g_curInstrument = CGloabal.g_InstrE5071CModule;

                int ret = E5071C.Open(CGloabal.g_curInstrument.adress, ref CGloabal.g_curInstrument.nHandle);
                E5071C.GetInstrumentIdentifier(CGloabal.g_curInstrument.nHandle, out sn);
                E5071C.ClearAllErrorQueue(CGloabal.g_curInstrument.nHandle);
                E5071C.setAttribute(CGloabal.g_curInstrument.nHandle);
                
                if (sn.Contains("MY46734604"))
                {

                    if (20220201 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }


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
                else if (sn.Contains("MY46524686")) //add 2022.05.11 样机 三个月 09.01
                {
                    if (20220901 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY46419308"))
                {
                    if (20220306 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY47002100")) //样机 到4月18号
                {
                    if (20220418 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY46528681")) //样机 到9月10号
                {
                    if (20220910 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY46522885")) //样机 到7月14号
                {
                    if (20220714 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY46103317")) //add 2022.03.29 样机 到06.10
                {
                    if (20220610 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY46520437")) //add 2022.03.30 样机 到永久
                {
                    if (20991230 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
                else if (sn.Contains("MY46901713"))  //modify 20220516
                {
                    if (20220716 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
                    {
                        optStatus.isConnect = false;
                        combDevString.BackColor = Color.Red;
                        return;
                    }

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
            }//end E5071C


        }

        private void DevConnectSet_Load(object sender, EventArgs e)
        {            
            combDevString.BackColor = SystemColors.Window;
            combDevString.Text = INI.GetValueFromIniFile("Instrument", "AddressNA");
            CGloabal.g_InstrE5080BModule.adress = INI.GetValueFromIniFile("Instrument", "AddressNA");

            if (INI.GetValueFromIniFile("Instrument", "NA").Length > 0)
            {
                combDevType.Text = INI.GetValueFromIniFile("Instrument", "NA");
            }
        }
    }
}
