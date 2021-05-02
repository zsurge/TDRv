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
            INI ni = new INI();

            CGloabal.g_InstrE5080BModule.adress = combDevString.Text;

            ni.WriteValueToIniFile("Instrument", "AddressNA", combDevString.Text);
            ni.WriteValueToIniFile("Instrument", "NA", combDevType.Text);

            CGloabal.g_curInstrument = CGloabal.g_InstrE5080BModule;

            int ret = E5080B.Open(CGloabal.g_InstrE5080BModule.adress, ref CGloabal.g_InstrE5080BModule.nHandle);

            if (ret != 0)
            {
                combDevString.BackColor = Color.Red;
                MessageBox.Show("error!");
            }
            else
            {
                optStatus.isConnect = true;
                combDevString.BackColor = Color.Green;
            }
        }

        private void DevConnectSet_Load(object sender, EventArgs e)
        {
            INI ni = new INI();
            combDevString.BackColor = SystemColors.Window;
            combDevString.Text = ni.GetValueFromIniFile("Instrument", "AddressNA");
            CGloabal.g_InstrE5080BModule.adress = ni.GetValueFromIniFile("Instrument", "AddressNA");
        }
    }
}
