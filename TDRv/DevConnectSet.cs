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


            if (combDevType.Text.Contains("E5080B") || combDevType.Text.Contains("PNA"))
            {
                CGloabal.g_InstrE5080BModule.adress = combDevString.Text;

                INI.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
                INI.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

                CGloabal.g_curInstrument = CGloabal.g_InstrE5080BModule;

                int ret = E5080B.Open(CGloabal.g_curInstrument.adress, ref CGloabal.g_curInstrument.nHandle);

                E5080B.GetInstrumentIdentifier(CGloabal.g_curInstrument.nHandle, out sn);
                LoggerHelper.mlog.Debug("SN = " + sn);

                if (sn.Contains("MY59101265") || sn.Contains("MY59101017") || sn.Contains("MY60213234"))
                {

                    if (202202101400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                    if (202110071400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                    if (202204121400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59202434"))
                {

                    if (209912311400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59400238")) //参展样机，一个月 add 2024.03.14
                {

                    if (202404141400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59101296"))
                {

                    if (209912311400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY61100183"))
                {

                    if (202402201400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59100527"))
                {

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY63057096")) //add 2023.09.09
                {

                    if (202404091400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59400117")) //add 2024.01.30
                {

                    if (202404301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59203255")) //2024.02.04 修改到永久版
                {

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59202618"))
                {

                    if (202404181400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59202852"))
                {

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                    if (202209101400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (202205221400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (202206151400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                    if (202307271400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59202302")) //add 2022.05.27 临时增加测试使用,2022.11.03 delay 2023.02.03//modify 20230128 delay 2099
                {

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY59201793")) //modify 2022.11.15,enable
                {

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //add 2022.12.15
                else if (sn.Contains("MY59201701") || sn.Contains("MY59202460") || sn.Contains("MY59202462")) //永久
                {

                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                    if (202201221400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY63057093")) //add 2023.09.26 到永久
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("US63055408")) //add 2024.01.23 到永久
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //////////////////////////////PNA/////////////////////////////////////////////////
                else if (sn.Contains("MY63056301")) //add 2023.04.02
                {

                    if (202405071400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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

                LoggerHelper.mlog.Debug("SN = " + sn);

                E5063A.GetThreePortIdentifier(CGloabal.g_curInstrument.nHandle);

                E5063A.ClearAllErrorQueue(CGloabal.g_curInstrument.nHandle);

                //深圳超能 改为20210130
                if (sn.Contains("MY54504547"))
                {
                    //已付款
                    if (209903011400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //2023.08.15  delay2099 12 30
                else if (sn.Contains("MY54100391"))
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (210001011400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //modify 2022.10.21 delay 2022.12.20
                else if (sn.Contains("MY54705656"))
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY54504144")) //add 2023.10.16
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //add 2023.01.11
                else if (sn.Contains("MY54503996") || sn.Contains("MY54503997") || sn.Contains("MY54503998"))
                {
                    if (202301211400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //add 2022.12.26 delay 2023.0508
                else if (sn.Contains("MY54706015"))
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //add 2022.04.19
                else if (sn.Contains("MY54706016"))
                {
                    if (202404221400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //add 2022.07.25 modify 2022.10.25,delay 2023.01.25 delay 2023.05.28 2023.01.25 delay 2023.06.28 
                else if (sn.Contains("MY54705930") ) //2023.07.06 add to 2099
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY54705910"))
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY54706013"))
                {
                    if (202404131400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //add 2022.10.21 永久
                else if (sn.Contains("MY54706014"))
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (209912301500 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                LoggerHelper.mlog.Debug("SN = " + sn);
                E5071C.ClearAllErrorQueue(CGloabal.g_curInstrument.nHandle);
                E5071C.setAttribute(CGloabal.g_curInstrument.nHandle);

                if (sn.Contains("MY46734604"))
                {

                    if (202202011400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                if (sn.Contains("MY46733941")) //龙宇，add 2024.02.28 30天
                {

                    if (202403281400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (202209011400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY47002573")) //add 2022.05.11 样机 三个月 09.01
                {
                    if (202402101400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY47002760")) //add 2023.12.17 样机 2个月 02.17
                {
                    if (202402171400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY47003740")) //add 2023.12.12 样机 三个月2024.05.12
                {
                    if (202405141400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY47100345")) //add 2024.02.28 三个月2024.05.28
                {
                    if (202405281400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY47002764")) //add 2023.12.06 样机 三个月2024.03.06
                {
                    if (202406191400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY47002948")) //add 20240210
                {
                    if (202404101400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                //add 2023.03.24 样机 
                else if (sn.Contains("MY46525726") || sn.Contains("MY46528909") || sn.Contains("MY46630570"))
                {
                    if (202306241400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY46630895")) //UNLOCK 2023.03.26
                {
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY47003065")) //
                {
                    if (209912311400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY46419308")) //add 2022.11.28 样机 三个月 2023.02.28
                {
                    if (202302281400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (202203061400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY46632852")) //样机 到8月22号
                {
                    if (202208291400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (202204181400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (202209101400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (202207141400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (202206101400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                else if (sn.Contains("MY46524427")) //add 2022.09.25 样机 到09.29
                {
                    if (202309291400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
                    if (209912301400 - Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmm")) <= 0)
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
            else
            {
                optStatus.isConnect = false;
                combDevString.BackColor = Color.Red;
                MessageBox.Show("暂不支持，请联系支持人员!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


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
