using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDRv
{
    public partial class DevParamSet : Form
    {
        public DevParamSet(DataTable tmp)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//设置form1的开始位置为屏幕的中央
            this.tmpDt = tmp;
        }

        public bool clickFlag = false;

        public bool single_click_flag = false;
        public bool diff_click_flag = false;
        public bool units_percent_click_flag = false;
        public bool units_ohm_click_flag = false;
        public bool units_custom_click_flag = false;
        public bool limit_value_click_flag = false;

        public bool image_enable_click_flag = false;
        public bool image_disable_click_flag = false;

        public string last_units = "ohm";
        DataTable tmpDt;

        public delegate void ChangeDgvHandler(DataGridView dgv,string xmlFilePath);  //定义委托
        public event ChangeDgvHandler ChangeDgv;  //定义事件

        public static string xmlFilePath = string.Empty;
        public int selectionIdx = 0;

        // private Regex _regex = new Regex("^[0-9]*\\.?[0-9]+$");
        //private Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$|\bBackspace\b");
        private string allowedCharsPattern = @"[0-9\b\.\,]";

        //添加是否存储XML标志位，TRUE= 已保存；FALSE = 未保存；
        public bool isSaveXml  = true;

        private static string sPath = Directory.GetCurrentDirectory() + "\\Impedance_Config.ini";
        IniFile optIni = new IniFile(sPath);

        devParam dp = new devParam();

        /// <summary>
        /// 获取XML文件数据到datagrid
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        private void getXmlInfo(string filePath)
        {
            try
            {
                DataSet myds = new DataSet();
                if (filePath.Length != 0)
                {
                    myds.ReadXml(filePath);
                    dgv_param.DataSource = myds.Tables[0];
                    dgv_param.Tag = Path.GetFileNameWithoutExtension(filePath);
                }
                else
                {
                    MessageBox.Show("未正确装载配方文件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("配方文件格式错误\r\n" + ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TranToParentForm()
        {
            if (ChangeDgv != null)
            {
                string filePath = xmlFilePath;
                ChangeDgv(dgv_param, filePath);          
            }
        }



        private void tsb_measure_loadXml_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();

            //设置对话框标题
            pOpenFileDialog.Title = "载入XML文件";
            pOpenFileDialog.Filter = "XML文件|*.xml";
            pOpenFileDialog.InitialDirectory = Environment.CurrentDirectory + "\\Config";
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
            isSaveXml = false;

            ctrIsEnable(true);

            if (dgv_param.DataSource == null)
            {
                //清空参数表格            
                if (dgv_param.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_param.Rows.Count; i++)
                    {
                        dgv_param.Rows.Clear();
                    }
                }
            }
            else
            {
                DataTable dt = (DataTable)dgv_param.DataSource;
                dt.Rows.Clear();
                dgv_param.DataSource = dt;
            }

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
            if (dgv_param.Rows.Count == 0)
            {
                MessageBox.Show("请新建配方", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存XML文件";
            sfd.InitialDirectory = Environment.CurrentDirectory + "\\Config";
            sfd.Filter = "XML文件|*.xml";



            if (sfd.ShowDialog() == DialogResult.OK)
            {

                if (File.Exists(sfd.FileName))
                    File.Delete(sfd.FileName);

                DataTable dT = GetDataTableFromDGV(dgv_param);
                DataSet dS = new DataSet();
                dS.Tables.Add(dT);
                dS.WriteXml(File.OpenWrite(sfd.FileName));
                dgv_param.Tag = Path.GetFileNameWithoutExtension(sfd.FileName);
                xmlFilePath = sfd.FileName;
                isSaveXml = true;
            }

        }

        //新增或者是新添加一行
        private void CreateOrAddRow()
        {
            isSaveXml = false;
            if (dgv_param.Rows.Count == 0) 
            {
                if (dgv_param.DataSource == null)
                {
                    int index = this.dgv_param.Rows.Add();
                    this.dgv_param.Rows[index].Cells[0].Value = dp.Id;
                    //this.dgv_param.Rows[index].Cells[1].Value = (dp.TestStep++).ToString();          
                    this.dgv_param.Rows[index].Cells[1].Value = dgv_param.Rows.Count;
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
                else
                {
                    string[] rowVals = new string[24];
                    rowVals[0] = dp.Id;
                    rowVals[1] = (dgv_param.Rows.Count+1).ToString(); //(dp.TestStep++).ToString();
                    rowVals[2] = dp.Description;
                    rowVals[3] = dp.Layer;
                    rowVals[4] = dp.Remark;
                    rowVals[5] = dp.ImpedanceDefine;
                    rowVals[6] = dp.ImpedanceLimitLower;
                    rowVals[7] = dp.ImpedanceLimitUpper;
                    rowVals[8] = dp.ImpedanceLimitUnit;
                    rowVals[9] = dp.InputChannel;
                    rowVals[10] = dp.InputMode;
                    rowVals[11] = dp.TestMethod;
                    rowVals[12] = dp.TestFromThreshold;
                    rowVals[13] = dp.TestToThreshold;
                    rowVals[14] = dp.OpenThreshold;
                    rowVals[15] = dp.TraceStartPosition;
                    rowVals[16] = dp.TraceEndPosition;
                    rowVals[17] = dp.CalibratedTimeScale;
                    rowVals[18] = dp.CalibrateOffset;
                    rowVals[19] = dp.RecordPath;
                    rowVals[20] = dp.SaveCurve;
                    rowVals[21] = dp.SaveImage;
                    rowVals[22] = dp.DielectricConstant;
                    rowVals[23] = dp.DataPointCheck;
                    ((DataTable)dgv_param.DataSource).Rows.Add(rowVals);
                }
            }
            else
            {
                if (dgv_param.DataSource == null)
                {
                    int index = dgv_param.Rows.Add();//添加一行

                    DataGridViewRow row = dgv_param.Rows[dgv_param.CurrentRow.Index]; //获取当前行数据

                    //添加一新行，并把数据赋值给新行
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dgv_param.Rows[index].Cells[i].Value = row.Cells[i].Value;
                    }


                    //更新STEP
                    //dp.TestStep = dgv_param.Rows.Count;
                    //dgv_param.Rows[index].Cells[1].Value = (dp.TestStep++).ToString();      
                    
                    dgv_param.Rows[index].Cells[1].Value = dgv_param.Rows.Count.ToString();
                }
                else
                {
                    DataGridViewRow row = dgv_param.Rows[dgv_param.CurrentRow.Index]; //获取当前行数据
                    string[] rowVals = new string[24];

                    //添加一新行，并把数据赋值给新行
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        rowVals[i] = row.Cells[i].Value.ToString();
                    }
                    //更新STEP
                    //dp.TestStep = dgv_param.Rows.Count+1;
                    //rowVals[1] = (dp.TestStep++).ToString();

                    rowVals[1] = (dgv_param.Rows.Count + 1).ToString();

                    ((DataTable)dgv_param.DataSource).Rows.Add(rowVals);
                }
            }//end dgv_param.Rows.Count == 0

            dgv_param.CurrentCell = dgv_param.Rows[this.dgv_param.Rows.Count - 1].Cells[0];
        }//end CreateOrAddRow

        //增加一行
        private void tsb_add_param_Click(object sender, EventArgs e)
        {
            ctrIsEnable(true);
            CreateOrAddRow();
        }

        //复制选中行
        private void tsb_copy_param_Click(object sender, EventArgs e)
        {
            isSaveXml = false;

            if (dgv_param.Rows.Count == 0)
            {
                MessageBox.Show("请先新建一条配方", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ctrIsEnable(true);

            if (dgv_param.DataSource == null)
            {
                int index = dgv_param.Rows.Add();//添加一行

                DataGridViewRow row = dgv_param.Rows[dgv_param.CurrentRow.Index]; //获取当前行数据
                //添加一新行，并把数据赋值给新行
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dgv_param.Rows[index].Cells[i].Value = row.Cells[i].Value;
                }

                //更新STEP
                //dp.TestStep = dgv_param.Rows.Count;
                //dgv_param.Rows[index].Cells[1].Value = (dp.TestStep++).ToString();

                dgv_param.Rows[index].Cells[1].Value = (dgv_param.Rows.Count).ToString();
            }
            else
            {
                DataGridViewRow row = dgv_param.Rows[dgv_param.CurrentRow.Index]; //获取当前行数据
                string[] rowVals = new string[24];

                //添加一新行，并把数据赋值给新行
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    rowVals[i] = row.Cells[i].Value.ToString();
                }

                //更新STEP
                //dp.TestStep = dgv_param.Rows.Count+1 ;
                //rowVals[1] = (dp.TestStep++).ToString();
                
                rowVals[1] = (dgv_param.Rows.Count + 1).ToString();
                ((DataTable)dgv_param.DataSource).Rows.Add(rowVals);
            }

            dgv_param.CurrentCell = dgv_param.Rows[this.dgv_param.Rows.Count - 1].Cells[0];
        }

        //删除选中行
        private void tsb_del_param_Click(object sender, EventArgs e)
        {
            isSaveXml = false;
            ctrIsEnable(true);

            if (dgv_param.Rows.Count > 0)
            {
                dgv_param.Rows.Remove(dgv_param.CurrentRow);                

                for (int i = 0; i < dgv_param.RowCount; i++)
                {                    
                    dgv_param.Rows[i].Cells["TestStep"].Value = i + 1;
                }
            }
        }

        private void dgv_param_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    initControl(true);
                    ctrIsEnable(true);
                
                    tx_p_Description.Text = dgv_param.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                    tx_p_Layer.Text = dgv_param.Rows[e.RowIndex].Cells["Layer"].Value.ToString();
                    tx_p_Remark.Text = dgv_param.Rows[e.RowIndex].Cells["Remark"].Value.ToString();
                    tx_p_TargetValue.Text = dgv_param.Rows[e.RowIndex].Cells["ImpedanceDefine"].Value.ToString();
                    tx_p_lowLimit.Text = dgv_param.Rows[e.RowIndex].Cells["ImpedanceLimitLower"].Value.ToString();
                    tx_p_highLimit.Text = dgv_param.Rows[e.RowIndex].Cells["ImpedanceLimitUpper"].Value.ToString();

                    limit_value_click_flag = true;

                    string units = dgv_param.Rows[e.RowIndex].Cells["ImpedanceLimitUnit"].Value.ToString();

                        //clickFlag = true;

                    if (string.Compare(units, "%", true) == 0)
                    {
                        last_units = "%";
                        units_percent_click_flag = true;
                        radio_units_percent.Checked = true;
                        lab_highlimit_unit.Text = "%";
                        lab_lowlimit_unit.Text = "%";
                        lab_offsetlimit_unit.Text = "ohm";
                        tx_limit_offset.Text = (float.Parse(tx_p_highLimit.Text)*float.Parse(tx_p_TargetValue.Text)/100).ToString();   
                    }
                    else
                    {
                        last_units = "ohm";
                        units_ohm_click_flag = true;
                        radio_units_ohm.Checked = true;
                        lab_highlimit_unit.Text = "ohm";
                        lab_lowlimit_unit.Text = "ohm";
                        lab_offsetlimit_unit.Text = "ohm";
                        tx_limit_offset.Text = Math.Abs(Math.Round((float.Parse(tx_p_highLimit.Text) - float.Parse(tx_p_TargetValue.Text)),2)).ToString();
                    }


                    string testMode = dgv_param.Rows[e.RowIndex].Cells["InputMode"].Value.ToString();
                    if (string.Compare(testMode, "Differential", true) == 0)
                    {
                        diff_click_flag = true;
                        radio_p_diff.Checked = true;
                    }
                    else
                    {
                        single_click_flag = true;
                        radio_p_single.Checked = true;
                    }
                

                    tx_p_begin.Text = dgv_param.Rows[e.RowIndex].Cells["TestFromThreshold"].Value.ToString();
                    tx_p_end.Text = dgv_param.Rows[e.RowIndex].Cells["TestToThreshold"].Value.ToString();
                    tx_p_Index.Text = dgv_param.Rows[e.RowIndex].Cells["OpenThreshold"].Value.ToString();
                    tx_p_yOffset.Text = dgv_param.Rows[e.RowIndex].Cells["CalibrateOffset"].Value.ToString();
                    tx_p_savePath.Text = dgv_param.Rows[e.RowIndex].Cells["RecordPath"].Value.ToString();


                    string isSaveCsv = dgv_param.Rows[e.RowIndex].Cells["SaveCurve"].Value.ToString();
                    if (string.Compare(isSaveCsv, "Enable", true) == 0)
                    {
                        radio_p_data_open.Checked = true;
                    }
                    else
                    {
                        radio_p_data_close.Checked = true;
                    }

                    string isSaveImage = dgv_param.Rows[e.RowIndex].Cells["SaveImage"].Value.ToString();
                    if ((string.Compare(isSaveImage, "Enable", true) == 0) && tx_p_savePath.Text.Length != 0)
                    {
                        image_enable_click_flag = true;
                        radio_p_image_open.Checked = true;
                    }
                    else
                    {
                        image_disable_click_flag = true;
                        radio_p_image_close.Checked = true;
                    }

                    string judgMode = dgv_param.Rows[e.RowIndex].Cells["DataPointCheck"].Value.ToString();
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
            catch (IOException ex)
            {
                    MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //TranToParentForm();
        }

        private void save_image_path()
        {
            FolderBrowserDialog sOpt = new FolderBrowserDialog();
            sOpt.Description = "请选择文件路径";

            DialogResult result = sOpt.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                tx_p_savePath.Text = "";
                radio_p_image_close.Checked = true;
                //dgv_param.Rows[dgv_param.CurrentRow.Index].Cells[21].Value = "Disable";
                return;
            }
            string folderPath = sOpt.SelectedPath.Trim();
            DirectoryInfo theFolder = new DirectoryInfo(folderPath);
            if (theFolder.Exists)
            {
                tx_p_savePath.Text = folderPath;
                //dgv_param.Rows[dgv_param.CurrentRow.Index].Cells[21].Value = "Enable";
            }
        }

        //选择要保存文件的地址
        private void btn_p_SavePath_Click(object sender, EventArgs e)
        {
            save_image_path();
        }

        private bool is_check_limit_value_empty()
        {
            if (tx_p_TargetValue.Text.Length == 0 || tx_p_highLimit.Text.Length==0 || tx_p_lowLimit.Text.Length == 0)
                return true;

            return false;
        }

        private void radio_units_ohm_CheckedChanged(object sender, EventArgs e)
        {
            if (units_ohm_click_flag)
            {
                units_ohm_click_flag = false;
                return;
            }

            if (radio_units_ohm.Checked)
            {
       
                lab_highlimit_unit.Text = "ohm";
                lab_lowlimit_unit.Text = "ohm";

                if (is_check_limit_value_empty())
                {
                    MessageBox.Show("limit值不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.Compare(last_units, "%", true) == 0)
                {
                    last_units = "ohm";
                    tx_p_highLimit.Text = Math.Round(((Convert.ToSingle(tx_p_highLimit.Text) / 100 + 1) * Convert.ToSingle(tx_p_TargetValue.Text)),2).ToString();
                    tx_p_lowLimit.Text = Math.Round(((1-Math.Abs(Convert.ToSingle(tx_p_lowLimit.Text)) / 100 ) * Convert.ToSingle(tx_p_TargetValue.Text)),2).ToString();
                    tx_limit_offset.Text = Math.Abs(Math.Round((float.Parse(tx_p_highLimit.Text) - float.Parse(tx_p_TargetValue.Text)),2)).ToString();
                }
         
            }
        }

        private void radio_units_percent_CheckedChanged(object sender, EventArgs e)
        {

            if (units_percent_click_flag)
            {
                units_percent_click_flag = false;
                return;
            }

            if (radio_units_percent.Checked)
            {     
                lab_highlimit_unit.Text = "%";
                lab_lowlimit_unit.Text = "%";

                if (is_check_limit_value_empty())
                {
                    MessageBox.Show("limit值不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.Compare(last_units, "ohm", true) == 0)
                {
                    last_units = "%";
                    tx_p_highLimit.Text = Math.Round(((Convert.ToSingle(tx_p_highLimit.Text) / Convert.ToSingle(tx_p_TargetValue.Text) - 1) * 100),2).ToString();
                    tx_p_lowLimit.Text = Math.Round(((Convert.ToSingle(tx_p_lowLimit.Text) / Convert.ToSingle(tx_p_TargetValue.Text) - 1) * 100),2).ToString();
                    tx_limit_offset.Text = Math.Round((float.Parse(tx_p_highLimit.Text) * float.Parse(tx_p_TargetValue.Text) / 100), 2).ToString();
                }
            }
        }

        private void radio_units_custom_CheckedChanged(object sender, EventArgs e)
        {
            if (units_custom_click_flag)
            {
                units_custom_click_flag = false;
                return;
            }

            if (radio_units_custom.Checked)
            {
                tx_limit_offset.Enabled = true;
                lab_highlimit_unit.Text = "ohm";
                lab_lowlimit_unit.Text = "ohm";
                lab_offsetlimit_unit.Text = "ohm";
                last_units = "ohm";

                tx_limit_offset.Text = String.Empty;
                tx_p_highLimit.Text = String.Empty;
                tx_p_lowLimit.Text = String.Empty;


                //if (string.Compare(last_units, "%", true) == 0)
                //{
                //    last_units = "ohm";
                //    tx_p_highLimit.Text = ((Convert.ToSingle(tx_p_highLimit.Text) / 100 + 1) * Convert.ToSingle(tx_p_TargetValue.Text)).ToString();
                //    tx_p_lowLimit.Text = ((1 - Math.Abs(Convert.ToSingle(tx_p_lowLimit.Text)) / 100) * Convert.ToSingle(tx_p_TargetValue.Text)).ToString();
                //    tx_limit_offset.Text = (float.Parse(tx_p_highLimit.Text) - float.Parse(tx_p_TargetValue.Text)).ToString();
                //}

            }
        }

        private void DevParamSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isSaveXml == false)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "保存XML文件";
                sfd.InitialDirectory = Environment.CurrentDirectory + "\\Config";
                sfd.Filter = "XML文件|*.xml";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DataTable dT = GetDataTableFromDGV(dgv_param);
                    DataSet dS = new DataSet();
                    dS.Tables.Add(dT);
                    dS.WriteXml(File.OpenWrite(sfd.FileName));
                    dgv_param.Tag = Path.GetFileNameWithoutExtension(sfd.FileName);
                    xmlFilePath = sfd.FileName;
                    isSaveXml = true;
                }
                else
                {
                    DataTable dT = GetDataTableFromDGV(dgv_param);
                    DataSet dS = new DataSet();
                    dS.Tables.Add(dT);
                    sfd.FileName = Environment.CurrentDirectory + "\\Config\\_temp.xml";
                    dS.WriteXml(File.OpenWrite(sfd.FileName));
                    dgv_param.Tag = Path.GetFileNameWithoutExtension(sfd.FileName);
                    xmlFilePath = sfd.FileName;
                    return;
                }
            }

            TranToParentForm();
            save_xmlfilename_config();
        }

        public void ctrIsEnable(bool isEnable)
        {
            groupBox1.Enabled = isEnable;
            groupBox2.Enabled = isEnable;
            groupBox4.Enabled = isEnable;
            groupBox5.Enabled = isEnable;
            groupBox6.Enabled = isEnable;
            groupBox7.Enabled = isEnable;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            int index = dgv_param.CurrentRow.Index;


            ctrIsEnable(false);

            this.dgv_param.Rows[index].Cells[0].Value = dp.Id;
            this.dgv_param.Rows[index].Cells[1].Value = index+1;            
            this.dgv_param.Rows[index].Cells[2].Value = tx_p_Description.Text;
            this.dgv_param.Rows[index].Cells[3].Value = tx_p_Layer.Text;
            this.dgv_param.Rows[index].Cells[4].Value = tx_p_Remark.Text;
            this.dgv_param.Rows[index].Cells[5].Value = tx_p_TargetValue.Text;
            this.dgv_param.Rows[index].Cells[6].Value = tx_p_lowLimit.Text;
            this.dgv_param.Rows[index].Cells[7].Value = tx_p_highLimit.Text;

            if (radio_units_ohm.Checked)
            {
                this.dgv_param.Rows[index].Cells[8].Value = "ohms";
            }
            if (radio_units_percent.Checked)
            {
                this.dgv_param.Rows[index].Cells[8].Value = "%";
            }

            if (radio_units_custom.Checked)
            {
                this.dgv_param.Rows[index].Cells[8].Value = "ohms";
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

            this.dgv_param.Rows[index].Cells[12].Value = tx_p_begin.Text;

            this.dgv_param.Rows[index].Cells[13].Value = tx_p_end.Text;
            this.dgv_param.Rows[index].Cells[14].Value = tx_p_Index.Text;
            this.dgv_param.Rows[index].Cells[15].Value = "0";
            this.dgv_param.Rows[index].Cells[16].Value = "0";
            this.dgv_param.Rows[index].Cells[17].Value = "0";
            this.dgv_param.Rows[index].Cells[18].Value = tx_p_yOffset.Text;
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

        private void initControl(bool en_disable)
        {
            btn_cancel.Enabled = en_disable;
            btn_update.Enabled = en_disable;
            radio_units_ohm.Enabled = en_disable;
            radio_units_percent.Enabled = en_disable;
            radio_units_custom.Enabled = en_disable;
            tx_limit_offset.Enabled = false;
        }

        private void DevParamSet_Load(object sender, EventArgs e)
        {
            initControl(false);
            //禁止列排序
            for (int i = 0; i < dgv_param.Columns.Count; i++)
            {
                dgv_param.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv_param.DataSource = tmpDt;

            optParam.offsetValue = tx_p_yOffset.Text;

        }
        #region -支持鼠标拖拽
        //控制移动时鼠标的图形，否则是一个禁止移动的标识
        private void dgv_param_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //控制拖动的条件
        //鼠标在单元格里移动时激活拖放功能，我这里判断了如果是只有单击才执行拖放，双击我要执行其他功能，而且只有点在每行的表头那一格拖动才行，否则会影响编辑其他单元格的值
        private void dgv_param_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
            {
                if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                    dgv_param.DoDragDrop(dgv_param.Rows[e.RowIndex], DragDropEffects.Move);
            }
        }

        private void dgv_param_DragDrop(object sender, DragEventArgs e)
        {
            int idx = GetRowFromPoint(e.X, e.Y);

            if (idx < 0) return;

            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.dgv_param.DataSource;

                DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                dgv_param.Rows.Remove(row);
                selectionIdx = idx;
                //dgv_param.Rows.Insert(idx, row);

                DataRow dataRow = (row.DataBoundItem as DataRowView).Row;

                //DataRow dataRow = (row.DataBoundItem as DataRowView).Row;
                //selectionIdx = idx;

                dt.Rows.InsertAt(dataRow, idx);

            }
        }
        private int GetRowFromPoint(int x, int y)
        {
            for (int i = 0; i < dgv_param.RowCount; i++)
            {
                Rectangle rec = dgv_param.GetRowDisplayRectangle(i, false);

                if (dgv_param.RectangleToScreen(rec).Contains(x, y))
                    return i;
            }

            return -1;
        }

        private void dgv_param_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
                selectionIdx = e.RowIndex;
        }

        private void dgv_param_SizeChanged(object sender, EventArgs e)
        {
            if ((dgv_param.Rows.Count > 0) && (dgv_param.SelectedRows.Count > 0) && (dgv_param.SelectedRows[0].Index != selectionIdx))
            {
                if (dgv_param.Rows.Count <= selectionIdx)
                    selectionIdx = dgv_param.Rows.Count - 1;
                dgv_param.Rows[selectionIdx].Selected = true;
                dgv_param.CurrentCell = dgv_param.Rows[selectionIdx].Cells[0];
            }
        }
        #endregion

        private void radio_p_single_CheckedChanged(object sender, EventArgs e)
        {
            if (single_click_flag)
            {
                single_click_flag = false;
                return;
            }


            if (radio_p_single.Checked)
            {
                limit_value_click_flag = true;

                tx_p_Description.Text = "50";
                tx_p_Index.Text = "125";
                tx_p_TargetValue.Text = "50";

                if (radio_units_ohm.Checked)
                {
                    tx_p_highLimit.Text = "55";
                    tx_p_lowLimit.Text = "45";
                    tx_limit_offset.Text = "5";
                    lab_offsetlimit_unit.Text = "ohm";
                }
                else
                {
                    tx_p_highLimit.Text = "10";
                    tx_p_lowLimit.Text = "-10";
                    tx_limit_offset.Text = "5";
                    lab_offsetlimit_unit.Text = "ohm";
                }


            }
        }

        private void radio_p_diff_CheckedChanged(object sender, EventArgs e)
        {
            //public bool single_click_flag = false;
            //public bool diff_click_flag = false;
            //public bool units_percent_click_flag = false;
            //public bool units_ohm_click_flag = false;
            //public bool units_custom_click_flag = false;

            if (diff_click_flag)
            {
                diff_click_flag = false;
                return;
            }

            if (radio_p_diff.Checked)
            {
                limit_value_click_flag = true;

                tx_p_Description.Text = "100";
                tx_p_Index.Text = "200";
                tx_p_TargetValue.Text = "100";
                if (radio_units_ohm.Checked)
                {
                    tx_p_highLimit.Text = "110";
                    tx_p_lowLimit.Text = "90";
                    tx_limit_offset.Text = "10";
                    lab_offsetlimit_unit.Text = "ohm";
                }
                else
                {
                    tx_p_highLimit.Text = "10";
                    tx_p_lowLimit.Text = "-10";
                    tx_limit_offset.Text = "10";
                    lab_offsetlimit_unit.Text = "ohm";
                }
            }
        }



        private void tx_p_yOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            //数字、小数点（最大到2位）、退格键、负号
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('-'))
            {
                if ((sender as TextBox).Text != "")
                {
                    e.Handled = true;
                }
            }
            //第1位是负号时候、第2位小数点不可
            if (((TextBox)sender).Text == "-" && e.KeyChar == (char)('.'))
            {
                e.Handled = true;
            }
            //负号只能1次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            //小数点（最大到2位）   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //光标在小数点右侧时候判断  
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
            }
          */ 
        }


        private void save_xmlfilename_config()
        {
            string historyFile_bypro = Environment.CurrentDirectory + "\\MeasureData\\History\\";
            string exportFile_bypro = Environment.CurrentDirectory + "\\MeasureData\\History\\";

            if (xmlFilePath == null)
            {
                historyFile_bypro += "TDR_Project_History.csv";
                exportFile_bypro += "TDR_Project_Export.csv";

            }
            else
            {
                historyFile_bypro += (Path.GetFileNameWithoutExtension(xmlFilePath) + "_history.csv");
                exportFile_bypro += (Path.GetFileNameWithoutExtension(xmlFilePath) + "_Export.csv");
            }

            INI.WriteValueToIniFile("TDR", "Naming Method", "ByProject");
            INI.WriteValueToIniFile("TDR", "HistoryFile", historyFile_bypro);
            INI.WriteValueToIniFile("TDR", "ExportFile", exportFile_bypro);
          
        }

        private void radio_p_image_close_CheckedChanged(object sender, EventArgs e)
        {
            if (image_disable_click_flag)
            {
                image_disable_click_flag = false;
                return;
            }

            if (radio_p_image_close.Checked)
            {
                tx_p_savePath.Text = "";
            }
        }

        private void radio_p_image_open_CheckedChanged(object sender, EventArgs e)
        {
            if (image_enable_click_flag)
            {
                image_enable_click_flag = false;
                return;
            }

            if (radio_p_image_open.Checked)
            {
                save_image_path();
            }
        }

        private void tx_limit_offset_TextChanged(object sender, EventArgs e)
        {
            if (tx_limit_offset.Text.Length > 0)
            {

                if (limit_value_click_flag)
                {
                    limit_value_click_flag = false;
                    return;
                }
                tx_p_highLimit.Text = (float.Parse(tx_p_TargetValue.Text) + float.Parse(tx_limit_offset.Text)).ToString();
                tx_p_lowLimit.Text = (float.Parse(tx_p_TargetValue.Text) - float.Parse(tx_limit_offset.Text)).ToString();


                //tx_p_highLimit.Text = Math.Round(((Convert.ToSingle(tx_p_highLimit.Text) / Convert.ToSingle(tx_p_TargetValue.Text) - 1) * 100), 2).ToString();
                //tx_p_lowLimit.Text = Math.Round(((Convert.ToSingle(tx_p_lowLimit.Text) / Convert.ToSingle(tx_p_TargetValue.Text) - 1) * 100), 2).ToString();


            }
        }

        private void tx_limit_offset_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!regex.IsMatch(e.KeyChar.ToString()))
            if (!Regex.IsMatch(e.KeyChar.ToString(), allowedCharsPattern))
            {
                e.Handled = true;
                MessageBox.Show("只能输入数字和小数点", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }//end class
}//end namespace
