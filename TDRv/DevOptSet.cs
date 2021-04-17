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

            optParam.snPrefix = tx_sn_prefix.Text;
            optParam.snBegin = tx_sn_begin.Text;
            optParam.historyExportFileName = tx_history_report.Text;
            optParam.outputExportFileName = tx_export_report.Text;

            if (string.Compare(optIni.GetValueFromIniFile("TDR", "TestStep"), "Pass") == 0)
            {
                radio_pro_pass.Checked = true;
                optParam.testMode = 1;
            }
            else if (string.Compare(optIni.GetValueFromIniFile("TDR", "TestStep"), "Manual") == 0)
            {
                radio_pro_manual.Checked = true;
                optParam.testMode = 2;
            }
            else if (string.Compare(optIni.GetValueFromIniFile("TDR", "TestStep"), "Next") == 0)
            {
                radio_pro_next.Checked = true;
                optParam.testMode = 3;
            }
            else if (string.Compare(optIni.GetValueFromIniFile("TDR", "TestStep"), "PassRecord") == 0)
            {
                radio_pro_only_pass.Checked = true;
                optParam.testMode = 4;
            }

            if (string.Compare(optIni.GetValueFromIniFile("TDR", "Keyboard"), "Disable") == 0)
            {
                radio_key_close.Checked = true;
                optParam.keyMode = 0;
            }
            else
            {
                radio_key_space.Checked = true;
                optParam.keyMode = 1;
            }

            
            if (string.Compare(optIni.GetValueFromIniFile("TDR", "Naming Method"), "ByDate") == 0)
            {
                optParam.exportMode = 1;
                radio_sn_manual.Checked = true;
            }
            else
            {
                optParam.exportMode = 2;
                radio_sn_auto.Checked = true;
            }


        }

        private void btn_opt_ok_Click(object sender, EventArgs e)
        {
            //测试流程
            if (radio_pro_pass.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "TestStep", "Pass");
                optParam.testMode = 1;
            }
            else if (radio_pro_manual.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "TestStep", "Manual");
                optParam.testMode = 2;
            }
            else if (radio_pro_next.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "TestStep", "Next");
                optParam.testMode = 3;
            }
            else if (radio_pro_only_pass.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "TestStep", "PassRecord");
                optParam.testMode = 4;
            }
            else
            {
                optIni.WriteValueToIniFile("TDR", "TestStep","Next");
                optParam.testMode = 3;
            }

            //键盘 
            if (radio_key_close.Checked)
            {
                optIni.WriteValueToIniFile("TDR", "Keyboard", "Disable");
                optParam.keyMode = 0;
            }
            else
            {
                optIni.WriteValueToIniFile("TDR", "Keyboard", "Spec");
                optParam.keyMode = 1;
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
            optParam.snPrefix = tx_sn_prefix.Text;

            optIni.WriteValueToIniFile("TDR", "SerialNumber", tx_sn_begin.Text);
            optParam.snBegin = tx_sn_begin.Text;

            //sava mode
            if (radio_save_date.Checked)
            {
                optParam.exportMode = 1;
                optIni.WriteValueToIniFile("TDR", "Naming Method", "ByDate");

                optIni.WriteValueToIniFile("TDR", "HistoryFile", historyFile);
                optParam.historyExportFileName = historyFile;

                optIni.WriteValueToIniFile("TDR", "ExportFile", exportFile);
                optParam.outputExportFileName = exportFile;

            }            
            else
            {
                optParam.exportMode = 2;
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
