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
    public partial class DevOptSet : Form
    {
        INI optIni = new INI();
        public DevOptSet()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        public string historyFile = "TDR_" + DateTime.Now.ToString("yyyyMMdd") + "_History.csv";
        public string exportFile = "TDR_" + DateTime.Now.ToString("yyyyMMdd") + "_Export.csv";

        private void DevOptSet_Load(object sender, EventArgs e)
        {
            tx_sn_prefix.Text = optIni.GetValueFromIniFile("TDR", "SN_Head");
            tx_sn_begin.Text = optIni.GetValueFromIniFile("TDR", "SerialNumber");
            tx_history_report.Text = optIni.GetValueFromIniFile("TDR", "HistoryFile");
            tx_export_report.Text = optIni.GetValueFromIniFile("TDR", "ExportFile");

            if (string.Compare(optIni.GetValueFromIniFile("TDR", "TestStep"), "Pass") == 0)
            {
                radio_pro_pass.Checked = true;
            }
            else if (string.Compare(optIni.GetValueFromIniFile("TDR", "TestStep"), "Manual") == 0)
            {
                radio_pro_manual.Checked = true;
            }
            else if (string.Compare(optIni.GetValueFromIniFile("TDR", "TestStep"), "Next") == 0)
            {
                radio_pro_next.Checked = true;
            }
            else if (string.Compare(optIni.GetValueFromIniFile("TDR", "TestStep"), "PassRecord") == 0)
            {
                radio_pro_only_pass.Checked = true;
            }

            if (string.Compare(optIni.GetValueFromIniFile("TDR", "Keyboard"), "Disable") == 0)
            {
                radio_key_close.Checked = true;
            }
            else
            {
                radio_key_space.Checked = true;
            }

            if (string.Compare(optIni.GetValueFromIniFile("TDR", "Keyboard"), "Disable") == 0)
            {
                radio_key_close.Checked = true;
            }
            else
            {
                radio_key_space.Checked = true;
            }

            if (string.Compare(optIni.GetValueFromIniFile("TDR", "SN Method"), "Manual") == 0)
            {
                radio_sn_manual.Checked = true;
            }
            else
            {
                radio_sn_auto.Checked = true;
            }


        }

        private void btn_opt_ok_Click(object sender, EventArgs e)
        {
            //测试流程
            if (radio_pro_pass.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "TestStep", "Pass");
            }
            else if (radio_pro_manual.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "TestStep", "Manual");
            }
            else if (radio_pro_next.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "TestStep", "Next");
            }
            else if (radio_pro_only_pass.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "TestStep", "PassRecord");
            }
            else
            {
                optIni.WriteValueToIniFile("TDR", "TestStep","Next");
            }

            //键盘 
            if (radio_key_close.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "Keyboard", "Disable");
            }
            else
            {
                optIni.WriteValueToIniFile("TDR", "Keyboard", "Spec");
            }

            //流水号
            if (radio_sn_manual.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "SN Method", "Manual");                
            }
            else
            {
                optIni.WriteValueToIniFile("TDR", "SN Method", "Auto");                
            }

            optIni.WriteValueToIniFile("TDR", "SN_Head", tx_sn_prefix.Text);
            optIni.WriteValueToIniFile("TDR", "SerialNumber", tx_sn_begin.Text);

            //sava mode
            if (radio_save_date.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "Naming Method", "ByDate");
                optIni.WriteValueToIniFile("TDR", "HistoryFile", historyFile);
                optIni.WriteValueToIniFile("TDR", "ExportFile", exportFile);
            }            
            else
            {
                optIni.WriteValueToIniFile("TDR", "Naming Method", "ByProject");
                optIni.WriteValueToIniFile("TDR", "HistoryFile", "TDR_Project_History.csv");
                optIni.WriteValueToIniFile("TDR", "ExportFile", "TDR_Project_Export.csv");
            }

            this.Close();
        }

        private void btn_opt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radio_save_date_CheckedChanged(object sender, EventArgs e)
        {
            tx_history_report.Text = historyFile;
            tx_export_report.Text = exportFile;
        }

        private void radio_save_param_CheckedChanged(object sender, EventArgs e)
        {
            tx_history_report.Text = "TDR_Project_History.csv";
            tx_export_report.Text = "TDR_Project_Export.csv";
        }
    }
}
