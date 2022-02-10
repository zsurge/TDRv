using System;
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
                else if (sn.Contains("MY59201567"))
                {

                    if (20220308 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
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

                    if (20220301 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
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
                else if (sn.Contains("MY59201890"))
                {

                    if (20220301 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
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
                else if (sn.Contains("MY59201701"))
                {

                    if (20220411 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
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
                    if (20220301 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
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
                    if (20220415 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
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
                //modify 2021.11.30 delay 2021.12.30
                else if (sn.Contains("MY54605475") || sn.Contains("MY54605474") || sn.Contains("MY54605473"))
                {
                    if (20220228 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
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
                else if (sn.Contains("MY46524686"))
                {
                    if (20220511 - Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 0)
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
