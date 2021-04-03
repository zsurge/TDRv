﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public delegate void ChangeDgvHandler(DataGridView dgv);  //定义委托
        public event ChangeDgvHandler ChangeDgv;  //定义事件

        public string xmlFilePath = string.Empty;

        devParam dp = new devParam();

        /// <summary>
        /// 获取XML文件数据到datagrid
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
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

        private void TranToParentForm()
        {
            if (ChangeDgv != null)
            {                
                ChangeDgv(dgv_param);          
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

        //新建一个新的配置文件
        private void tsb_create_xml_Click(object sender, EventArgs e)
        {
            //清空参数表格
            dgv_param.Rows.Clear();

            //新建一默认行
            CreateOrAddRow();
        }

        /// <summary>
        /// Converts data grid view to a data table
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    // You could potentially name the column based on the DGV column name (beware of dupes)
                    // or assign a type based on the data type of the data bound to this DGV column.
                    dt.Columns.Add(column.Name);
                }
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }

            return dt;
        }

        //保存新的配置文件
        private void tsb_save_xml_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存XML文件";
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            sfd.Filter = "XML文件|*.xml";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DataTable dT = GetDataTableFromDGV(dgv_param);
                DataSet dS = new DataSet();
                dS.Tables.Add(dT);
                dS.WriteXml(File.OpenWrite(sfd.FileName));
            }

        }

        //新增或者是新添加一行
        private void CreateOrAddRow()
        {
            int index = this.dgv_param.Rows.Add();
            this.dgv_param.Rows[index].Cells[0].Value = dp.Id;
            this.dgv_param.Rows[index].Cells[1].Value = dp.TestStep.ToString();
            dp.TestStep += 1;
            this.dgv_param.Rows[index].Cells[2].Value = dp.Description;
            this.dgv_param.Rows[index].Cells[3].Value = dp.Layer;
            this.dgv_param.Rows[index].Cells[4].Value = dp.Remark;
            this.dgv_param.Rows[index].Cells[5].Value = dp.ImpedanceDefine;
            this.dgv_param.Rows[index].Cells[6].Value = dp.ImpedanceLimitLower;
            this.dgv_param.Rows[index].Cells[7].Value = dp.ImpedanceLimitUpper;
            this.dgv_param.Rows[index].Cells[8].Value = dp.ImpedanceLimitUnit;
            this.dgv_param.Rows[index].Cells[9].Value = dp.InputChannel;
            this.dgv_param.Rows[index].Cells[10].Value = dp.InputMode;
            this.dgv_param.Rows[index].Cells[11].Value = dp.TestMethod;
            this.dgv_param.Rows[index].Cells[12].Value = dp.TestFromThreshold;
            this.dgv_param.Rows[index].Cells[13].Value = dp.TestToThreshold;
            this.dgv_param.Rows[index].Cells[14].Value = dp.OpenThreshold;
            this.dgv_param.Rows[index].Cells[15].Value = dp.TraceStartPosition;
            this.dgv_param.Rows[index].Cells[16].Value = dp.TraceEndPosition;
            this.dgv_param.Rows[index].Cells[17].Value = dp.CalibratedTimeScale;
            this.dgv_param.Rows[index].Cells[18].Value = dp.CalibrateOffset;
            this.dgv_param.Rows[index].Cells[19].Value = dp.RecordPath;
            this.dgv_param.Rows[index].Cells[20].Value = dp.SaveCurve;
            this.dgv_param.Rows[index].Cells[21].Value = dp.SaveImage;
            this.dgv_param.Rows[index].Cells[22].Value = dp.DielectricConstant;
            this.dgv_param.Rows[index].Cells[23].Value = dp.DataPointCheck;
        }
        //增加一行
        private void tsb_add_param_Click(object sender, EventArgs e)
        {
            // int index = dgv_param.Rows.Add();

            //string[] defaultValue = { "20", "20", "20", "20", "20", "20", "20", "20", "20", "20", "20", "20", "20", "20", "20", "20", "20" ,"20", "20", "20", "20", "20", "20", "20" };

            //dgv_param.Rows.Add(defaultValue); 

            CreateOrAddRow();
        }

        //复制选中行
        private void tsb_copy_param_Click(object sender, EventArgs e)
        {
            int index = dgv_param.Rows.Add();
            DataGridViewRow row = dgv_param.Rows[dgv_param.CurrentRow.Index];    
            //DataRow dr = ((DataTable)dgv_param.DataSource).NewRow();
            //((DataTable)dgv_param.DataSource).Rows.Add(row);

            for(int i=0;i<row.Cells.Count;i++)
            {
                dgv_param.Rows[index].Cells[i].Value = row.Cells[i].Value;
            }
        }

        //删除选中行
        private void tsb_del_param_Click(object sender, EventArgs e)
        {
            dgv_param.Rows.Remove(dgv_param.CurrentRow);
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            TranToParentForm();
        }

        //选择要保存文件的地址
        private void btn_p_SavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog sOpt = new FolderBrowserDialog();
            sOpt.Description = "请选择文件路径";
            DialogResult result = sOpt.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return;
            }
            string folderPath = sOpt.SelectedPath.Trim();
            DirectoryInfo theFolder = new DirectoryInfo(folderPath);
            if (theFolder.Exists)
            {
                tx_p_savePath.Text = folderPath;
            }
        }

        private void radio_units_ohm_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_units_ohm.Checked)
            {
                label15.Text = "欧姆";
                lab_highlimit_unit.Text = "欧姆";
                lab_lowlimit_unit.Text = "欧姆";

                tx_p_highLimit.Text = ((Convert.ToSingle(tx_p_highLimit.Text) / 100 + 1) * Convert.ToSingle(tx_p_TargetValue.Text)).ToString();
                tx_p_lowLimit.Text = ((Convert.ToSingle(tx_p_lowLimit.Text) / 100 + 1) * Convert.ToSingle(tx_p_TargetValue.Text)).ToString();
            }
        }

        private void radio_units_percent_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_units_percent.Checked)
            {
                label15.Text = "%";
                lab_highlimit_unit.Text = "%";
                lab_lowlimit_unit.Text = "%";
                tx_p_highLimit.Text = ((Convert.ToSingle(tx_p_highLimit.Text) / Convert.ToSingle(tx_p_TargetValue.Text) - 1) * 100).ToString();
                tx_p_lowLimit.Text = ((Convert.ToSingle(tx_p_lowLimit.Text) / Convert.ToSingle(tx_p_TargetValue.Text) - 1) * 100).ToString();
            }
        }

        private void DevParamSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            TranToParentForm();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            int index = dgv_param.CurrentRow.Index;

            this.dgv_param.Rows[index].Cells[0].Value = dp.Id;
            this.dgv_param.Rows[index].Cells[1].Value = index+1;            
            this.dgv_param.Rows[index].Cells[2].Value = tx_p_Description.Text;
            this.dgv_param.Rows[index].Cells[3].Value = tx_p_Layer.Text;
            this.dgv_param.Rows[index].Cells[4].Value = tx_p_Remark.Text;
            this.dgv_param.Rows[index].Cells[5].Value = tx_p_TargetValue.Text;
            this.dgv_param.Rows[index].Cells[6].Value = tx_p_highLimit.Text;
            this.dgv_param.Rows[index].Cells[7].Value = tx_p_lowLimit.Text;

            if (radio_units_ohm.Checked)
            {
                this.dgv_param.Rows[index].Cells[8].Value = "ohms";
            }
            if (radio_units_percent.Checked)
            {
                this.dgv_param.Rows[index].Cells[8].Value = "%";
            }

            this.dgv_param.Rows[index].Cells[9].Value = dp.InputChannel;

            if (radio_p_diff.Checked)
            {
                this.dgv_param.Rows[index].Cells[10].Value = "Differential";
            }
            if (radio_p_single.Checked)
            {
                this.dgv_param.Rows[index].Cells[10].Value = "SingleEnded";
            }

            this.dgv_param.Rows[index].Cells[11].Value = dp.TestMethod;

            this.dgv_param.Rows[index].Cells[12].Value = tx_p_highLimit.Text;

            this.dgv_param.Rows[index].Cells[13].Value = tx_p_lowLimit.Text;
            this.dgv_param.Rows[index].Cells[14].Value = tx_p_Index.Text;
            this.dgv_param.Rows[index].Cells[15].Value = tx_p_begin.Text;
            this.dgv_param.Rows[index].Cells[16].Value = tx_p_end.Text;
            this.dgv_param.Rows[index].Cells[17].Value = "0";
            this.dgv_param.Rows[index].Cells[18].Value = "0";
            this.dgv_param.Rows[index].Cells[19].Value = tx_p_savePath.Text;

            if (radio_p_data_open.Checked)
            {
                this.dgv_param.Rows[index].Cells[20].Value = "Enable";
            }
            if (radio_p_data_close.Checked)
            {
                this.dgv_param.Rows[index].Cells[20].Value = "Disable";
            }

            if (radio_p_image_open.Checked)
            {
                this.dgv_param.Rows[index].Cells[21].Value = "Enable";
            }
            if (radio_p_image_close.Checked)
            {
                this.dgv_param.Rows[index].Cells[21].Value = "Disable";
            }


            this.dgv_param.Rows[index].Cells[22].Value = "4.2";


            if (radio_p_tag_avg.Checked)
            {
                this.dgv_param.Rows[index].Cells[23].Value = "AverageValue";
            }
            if (radio_p_tag_point.Checked)
            {
                this.dgv_param.Rows[index].Cells[23].Value = "DataPoints";
            }
        
        }
    }//end class
}//end namespace
