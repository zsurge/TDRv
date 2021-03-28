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
    public partial class DevParamSet : Form
    {
        public DevParamSet()
        {
            InitializeComponent();
        }
        public string xmlFilePath = string.Empty;

        private void getXmlInfo(string filePath)
        {
            DataSet myds = new DataSet();
            if (filePath.Length != 0)
            {
                myds.ReadXml(filePath);
                dgv_param.DataSource = myds.Tables[0];
            }
            else
            {
                MessageBox.Show("未正确装载配方文件");
            }
        }


        private void tsb_measure_loadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();

            //设置对话框标题
            pOpenFileDialog.Title = "载入XML文件";
            pOpenFileDialog.Filter = "XML文件|*.xml";
            //监测文件是否存在
            pOpenFileDialog.CheckFileExists = true;
            if (pOpenFileDialog.ShowDialog() == DialogResult.OK)  //如果点击的是打开文件
            {
                xmlFilePath = pOpenFileDialog.FileName;  //获取全路径文件名        
                getXmlInfo(xmlFilePath);
            }
        }

        private void dgv_param_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                tx_p_testSn.Text = (e.RowIndex+1).ToString();
                tx_p_Description.Text = dgv_param.Rows[e.RowIndex].Cells["pDescription"].Value.ToString();
                tx_p_Layer.Text = dgv_param.Rows[e.RowIndex].Cells["pLayer"].Value.ToString();
                tx_p_Remark.Text = dgv_param.Rows[e.RowIndex].Cells["pRemark"].Value.ToString();
                tx_p_TargetValue.Text = dgv_param.Rows[e.RowIndex].Cells["pImpedanceDefine"].Value.ToString();
                tx_p_lowLimit.Text = dgv_param.Rows[e.RowIndex].Cells["pImpedanceLimitLower"].Value.ToString();
                tx_p_highLimit.Text = dgv_param.Rows[e.RowIndex].Cells["pImpedanceLimitUpper"].Value.ToString();

                string units = dgv_param.Rows[e.RowIndex].Cells["pImpedanceLimitUnit"].Value.ToString();
                if (string.Compare(units, "ohms", true) == 0)
                {
                    radio_units_ohm.Checked = true;
                    lab_highlimit_unit.Text = "欧姆";
                    lab_lowlimit_unit.Text  = "欧姆";
                }
                else
                {
                    radio_units_percent.Checked = true;
                    lab_highlimit_unit.Text = "%";
                    lab_lowlimit_unit.Text = "%";
                }

                string testMode = dgv_param.Rows[e.RowIndex].Cells["pInputMode"].Value.ToString();
                if (string.Compare(testMode, "Differential", true) == 0)
                {
                    radio_p_diff.Checked = true;
                }
                else
                {
                    radio_p_single.Checked = true;
                }

                tx_p_begin.Text = dgv_param.Rows[e.RowIndex].Cells["pTestFromThreshold"].Value.ToString();
                tx_p_end.Text = dgv_param.Rows[e.RowIndex].Cells["pTestToThreshold"].Value.ToString();
                tx_p_Index.Text = dgv_param.Rows[e.RowIndex].Cells["pOpenThreshold"].Value.ToString();
                tx_p_yOffset.Text = dgv_param.Rows[e.RowIndex].Cells["pCalibrateOffset"].Value.ToString();
                tx_p_savePath.Text = dgv_param.Rows[e.RowIndex].Cells["pRecordPath"].Value.ToString();


                string isSaveCsv = dgv_param.Rows[e.RowIndex].Cells["pSaveCurve"].Value.ToString();
                if (string.Compare(isSaveCsv, "Enable", true) == 0)
                {
                    radio_p_data_open.Checked = true;
                }
                else
                {
                    radio_p_data_close.Checked = true;
                }

                string isSaveImage = dgv_param.Rows[e.RowIndex].Cells["pSaveImage"].Value.ToString();
                if (string.Compare(isSaveImage, "Enable", true) == 0)
                {
                    radio_p_image_open.Checked = true;
                }
                else
                {
                    radio_p_image_close.Checked = true;
                }

                string judgMode = dgv_param.Rows[e.RowIndex].Cells["pDataPointCheck"].Value.ToString();
                if (string.Compare(judgMode, "DataPoints", true) == 0)
                {
                    radio_p_tag_point.Checked = true;
                }
                else
                {
                    radio_p_tag_avg.Checked = true;
                }
            }
        }


    }//end class
}//end namespace
