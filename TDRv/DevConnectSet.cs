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
                this.Close();
            }
        }
    }
}
