﻿using System;
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
        public DevOptSet()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
        }

        public delegate void ChangeSnHandler(string sn);  //定义委托
        public event ChangeSnHandler ChangeSn;  //定义事件

        public string historyFile = Environment.CurrentDirectory + "\\MeasureData\\History\\" + "TDR_" + DateTime.Now.ToString("yyyyMMdd") + "_History.csv";
        public string exportFile = Environment.CurrentDirectory + "\\MeasureData\\Report\\" + "TDR_" + DateTime.Now.ToString("yyyyMMdd") + "_Export.csv";

        private void DevOptSet_Load(object sender, EventArgs e)
        {
            tx_sn_prefix.Text = INI.GetValueFromIniFile("TDR", "SN_Head");
            tx_sn_begin.Text = INI.GetValueFromIniFile("TDR", "SerialNumber");
            tx_history_report.Text = INI.GetValueFromIniFile("TDR", "HistoryFile");
            tx_export_report.Text = INI.GetValueFromIniFile("TDR", "ExportFile");

            if(tx_sn_prefix.Text.Length == 0)
            {
                tx_sn_prefix.Text = "SN";
                INI.WriteValueToIniFile("TDR", "SN_Head", "SN");
            }

            if (tx_sn_begin.Text.Length == 0)
            {
                tx_sn_begin.Text = "0001";
                INI.WriteValueToIniFile("TDR", "SerialNumber", "0001");
            }

            if (tx_history_report.Text.Length == 0)
            {
                tx_history_report.Text = "TDR_HistoryFile.CSV";
                INI.WriteValueToIniFile("TDR", "HistoryFile", "TDR_HistoryFile.CSV");
            }

            if (tx_export_report.Text.Length == 0)
            {
                tx_export_report.Text = "SN";
                INI.WriteValueToIniFile("TDR", "ExportFile", "TDR_Export_report.CSV");
            }

            optParam.snPrefix = tx_sn_prefix.Text;
            optParam.snBegin = tx_sn_begin.Text;
            optParam.historyExportFileName = tx_history_report.Text;
            optParam.outputExportFileName = tx_export_report.Text;

            if (string.Compare(INI.GetValueFromIniFile("TDR", "TestStep"), "Pass") == 0)
            {
                radio_pro_pass.Checked = true;
                optParam.testMode = 1;
            }
            else if (string.Compare(INI.GetValueFromIniFile("TDR", "TestStep"), "Manual") == 0)
            {
                radio_pro_manual.Checked = true;
                optParam.testMode = 2;
            }
            else if (string.Compare(INI.GetValueFromIniFile("TDR", "TestStep"), "Next") == 0)
            {
                radio_pro_next.Checked = true;
                optParam.testMode = 3;
            }
            else if (string.Compare(INI.GetValueFromIniFile("TDR", "TestStep"), "PassRecord") == 0)
            {
                radio_pro_only_pass.Checked = true;
                optParam.testMode = 4;
            }

            if (string.Compare(INI.GetValueFromIniFile("TDR", "Keyboard"), "Disable") == 0)
            {
                radio_key_close.Checked = true;
                optParam.keyMode = 0;
            }
            else
            {
                radio_key_space.Checked = true;
                optParam.keyMode = 1;
            }

            
            if (string.Compare(INI.GetValueFromIniFile("TDR", "Naming Method"), "ByDate") == 0)
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

        public void OnSnChanged()
        {
            if (ChangeSn != null)
            {
                ChangeSn(optParam.snBegin); /* 事件被触发 */
            }
        }

        private void btn_opt_ok_Click(object sender, EventArgs e)
        {
            //测试流程
            if (radio_pro_pass.Checked)
            {
                INI.WriteValueToIniFile("TDR", "TestStep", "Pass");
                optParam.testMode = 1;
            }
            else if (radio_pro_manual.Checked)
            {
                INI.WriteValueToIniFile("TDR", "TestStep", "Manual");
                optParam.testMode = 2;
            }
            else if (radio_pro_next.Checked)
            {
                INI.WriteValueToIniFile("TDR", "TestStep", "Next");
                optParam.testMode = 3;
            }
            else if (radio_pro_only_pass.Checked)
            {
                INI.WriteValueToIniFile("TDR", "TestStep", "PassRecord");
                optParam.testMode = 4;
            }
            else
            {
                INI.WriteValueToIniFile("TDR", "TestStep","Next");
                optParam.testMode = 3;
            }

            //键盘 
            if (radio_key_close.Checked)
            {
                INI.WriteValueToIniFile("TDR", "Keyboard", "Disable");
                optParam.keyMode = 0;
            }
            else
            {
                INI.WriteValueToIniFile("TDR", "Keyboard", "Spec");
                optParam.keyMode = 1;
            }

            //流水号
            if (radio_sn_manual.Checked)
            {
                INI.WriteValueToIniFile("TDR", "SN Method", "Manual");                
            }
            else
            {
                INI.WriteValueToIniFile("TDR", "SN Method", "Auto");                
            }

            INI.WriteValueToIniFile("TDR", "SN_Head", tx_sn_prefix.Text);
            optParam.snPrefix = tx_sn_prefix.Text;

            INI.WriteValueToIniFile("TDR", "SerialNumber", tx_sn_begin.Text);
            optParam.snBegin = tx_sn_begin.Text;

            //sava mode
            if (radio_save_date.Checked)
            {

                optParam.exportMode = 1;
                INI.WriteValueToIniFile("TDR", "Naming Method", "ByDate");

                INI.WriteValueToIniFile("TDR", "HistoryFile", tx_history_report.Text);
                optParam.historyExportFileName = tx_history_report.Text;

                INI.WriteValueToIniFile("TDR", "ExportFile", tx_export_report.Text);
                optParam.outputExportFileName = tx_export_report.Text;

            }            
            else
            {
                optParam.exportMode = 2;
                INI.WriteValueToIniFile("TDR", "Naming Method", "ByProject");

                INI.WriteValueToIniFile("TDR", "HistoryFile", tx_history_report.Text);

                INI.WriteValueToIniFile("TDR", "ExportFile", tx_export_report.Text);
            }

            OnSnChanged();

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
            tx_history_report.Text = INI.GetValueFromIniFile("TDR", "HistoryFile");
            tx_export_report.Text = INI.GetValueFromIniFile("TDR", "ExportFile");
        }
    }
}
