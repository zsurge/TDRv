using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TDRv 
{
    public partial class DevConnectSet : Form
    {
        public DevConnectSet()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        public delegate void ChangeTsbHandler(bool flag);  //定义委托
        public event ChangeTsbHandler ChangeValue;  //定义事件

        private void SetParentFormTsbControl()
        {
            if (ChangeValue != null)
            {
                ChangeValue(true);
            }
        }

        private void btn_ConnectDev_Click(object sender, EventArgs e)
        {
            int ret = OptDev.Instance.OpenDev(combDevString.Text);

            if (ret != 0)
            {                
                MessageBox.Show("error!");
            }
            else
            {
                SetParentFormTsbControl();
                this.Close();
            }
        }
    }
}
