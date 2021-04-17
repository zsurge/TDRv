namespace TDRv
{
    partial class DevParamSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevParamSet));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_create_xml = new System.Windows.Forms.ToolStripButton();
            this.tsb_measure_loadXml = new System.Windows.Forms.ToolStripButton();
            this.tsb_save_xml = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_add_param = new System.Windows.Forms.ToolStripButton();
            this.tsb_copy_param = new System.Windows.Forms.ToolStripButton();
            this.tsb_del_param = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_param = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpedanceDefine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpedanceLimitLower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpedanceLimitUpper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpedanceLimitUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestFromThreshold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestToThreshold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenThreshold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TraceStartPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TraceEndPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalibratedTimeScale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalibrateOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecordPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveCurve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DielectricConstant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataPointCheck = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tx_p_Description = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tx_p_Remark = new System.Windows.Forms.TextBox();
            this.tx_p_Layer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tx_p_testSn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.radio_p_image_close = new System.Windows.Forms.RadioButton();
            this.radio_p_image_open = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radio_p_data_close = new System.Windows.Forms.RadioButton();
            this.radio_p_data_open = new System.Windows.Forms.RadioButton();
            this.btn_p_SavePath = new System.Windows.Forms.Button();
            this.tx_p_savePath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radio_p_diff = new System.Windows.Forms.RadioButton();
            this.radio_p_single = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tx_p_end = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tx_p_begin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tx_p_Index = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lab_lowlimit_unit = new System.Windows.Forms.Label();
            this.radio_units_percent = new System.Windows.Forms.RadioButton();
            this.lab_highlimit_unit = new System.Windows.Forms.Label();
            this.radio_units_ohm = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tx_p_lowLimit = new System.Windows.Forms.TextBox();
            this.tx_p_TargetValue = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tx_p_highLimit = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tx_p_yOffset = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radio_p_tag_point = new System.Windows.Forms.RadioButton();
            this.radio_p_tag_avg = new System.Windows.Forms.RadioButton();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_param)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_create_xml,
            this.tsb_measure_loadXml,
            this.tsb_save_xml,
            this.toolStripSeparator1,
            this.tsb_add_param,
            this.tsb_copy_param,
            this.tsb_del_param});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(813, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_create_xml
            // 
            this.tsb_create_xml.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_create_xml.Image = ((System.Drawing.Image)(resources.GetObject("tsb_create_xml.Image")));
            this.tsb_create_xml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_create_xml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_create_xml.Name = "tsb_create_xml";
            this.tsb_create_xml.Size = new System.Drawing.Size(110, 36);
            this.tsb_create_xml.Text = "新建档案";
            this.tsb_create_xml.Click += new System.EventHandler(this.tsb_create_xml_Click);
            // 
            // tsb_measure_loadXml
            // 
            this.tsb_measure_loadXml.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_measure_loadXml.Image = ((System.Drawing.Image)(resources.GetObject("tsb_measure_loadXml.Image")));
            this.tsb_measure_loadXml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_measure_loadXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_measure_loadXml.Name = "tsb_measure_loadXml";
            this.tsb_measure_loadXml.Size = new System.Drawing.Size(78, 36);
            this.tsb_measure_loadXml.Text = "载入";
            this.tsb_measure_loadXml.Click += new System.EventHandler(this.tsb_measure_loadXml_Click);
            // 
            // tsb_save_xml
            // 
            this.tsb_save_xml.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_save_xml.Image = ((System.Drawing.Image)(resources.GetObject("tsb_save_xml.Image")));
            this.tsb_save_xml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_save_xml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_save_xml.Name = "tsb_save_xml";
            this.tsb_save_xml.Size = new System.Drawing.Size(78, 36);
            this.tsb_save_xml.Text = "保存";
            this.tsb_save_xml.Click += new System.EventHandler(this.tsb_save_xml_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsb_add_param
            // 
            this.tsb_add_param.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_add_param.Image = ((System.Drawing.Image)(resources.GetObject("tsb_add_param.Image")));
            this.tsb_add_param.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_add_param.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_add_param.Name = "tsb_add_param";
            this.tsb_add_param.Size = new System.Drawing.Size(78, 36);
            this.tsb_add_param.Text = "新增";
            this.tsb_add_param.Click += new System.EventHandler(this.tsb_add_param_Click);
            // 
            // tsb_copy_param
            // 
            this.tsb_copy_param.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_copy_param.Image = ((System.Drawing.Image)(resources.GetObject("tsb_copy_param.Image")));
            this.tsb_copy_param.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_copy_param.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_copy_param.Name = "tsb_copy_param";
            this.tsb_copy_param.Size = new System.Drawing.Size(78, 36);
            this.tsb_copy_param.Text = "复制";
            this.tsb_copy_param.Click += new System.EventHandler(this.tsb_copy_param_Click);
            // 
            // tsb_del_param
            // 
            this.tsb_del_param.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsb_del_param.Image = ((System.Drawing.Image)(resources.GetObject("tsb_del_param.Image")));
            this.tsb_del_param.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_del_param.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_del_param.Name = "tsb_del_param";
            this.tsb_del_param.Size = new System.Drawing.Size(78, 36);
            this.tsb_del_param.Text = "删除";
            this.tsb_del_param.Click += new System.EventHandler(this.tsb_del_param_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgv_param);
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 193);
            this.panel1.TabIndex = 1;
            // 
            // dgv_param
            // 
            this.dgv_param.AllowDrop = true;
            this.dgv_param.AllowUserToAddRows = false;
            this.dgv_param.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_param.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_param.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.TestStep,
            this.Description,
            this.Layer,
            this.Remark,
            this.ImpedanceDefine,
            this.ImpedanceLimitLower,
            this.ImpedanceLimitUpper,
            this.ImpedanceLimitUnit,
            this.InputChannel,
            this.InputMode,
            this.TestMethod,
            this.TestFromThreshold,
            this.TestToThreshold,
            this.OpenThreshold,
            this.TraceStartPosition,
            this.TraceEndPosition,
            this.CalibratedTimeScale,
            this.CalibrateOffset,
            this.RecordPath,
            this.SaveCurve,
            this.SaveImage,
            this.DielectricConstant,
            this.DataPointCheck});
            this.dgv_param.Location = new System.Drawing.Point(0, 3);
            this.dgv_param.MultiSelect = false;
            this.dgv_param.Name = "dgv_param";
            this.dgv_param.ReadOnly = true;
            this.dgv_param.RowTemplate.Height = 23;
            this.dgv_param.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_param.Size = new System.Drawing.Size(813, 187);
            this.dgv_param.TabIndex = 5;
            this.dgv_param.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_param_CellClick);
            this.dgv_param.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_param_CellMouseDown);
            this.dgv_param.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_param_CellMouseMove);
            this.dgv_param.SizeChanged += new System.EventHandler(this.dgv_param_SizeChanged);
            this.dgv_param.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_param_DragDrop);
            this.dgv_param.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgv_param_DragEnter);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.id.Width = 23;
            // 
            // TestStep
            // 
            this.TestStep.DataPropertyName = "TestStep";
            this.TestStep.HeaderText = "TestStep";
            this.TestStep.Name = "TestStep";
            this.TestStep.ReadOnly = true;
            this.TestStep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TestStep.Width = 59;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Description.Width = 77;
            // 
            // Layer
            // 
            this.Layer.DataPropertyName = "Layer";
            this.Layer.HeaderText = "Layer";
            this.Layer.Name = "Layer";
            this.Layer.ReadOnly = true;
            this.Layer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Layer.Width = 41;
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "Remark";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            this.Remark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Remark.Width = 47;
            // 
            // ImpedanceDefine
            // 
            this.ImpedanceDefine.DataPropertyName = "ImpedanceDefine";
            this.ImpedanceDefine.HeaderText = "ImpedanceDefine";
            this.ImpedanceDefine.Name = "ImpedanceDefine";
            this.ImpedanceDefine.ReadOnly = true;
            this.ImpedanceDefine.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ImpedanceDefine.Width = 101;
            // 
            // ImpedanceLimitLower
            // 
            this.ImpedanceLimitLower.DataPropertyName = "ImpedanceLimitLower";
            this.ImpedanceLimitLower.HeaderText = "ImpedanceLimitLower";
            this.ImpedanceLimitLower.Name = "ImpedanceLimitLower";
            this.ImpedanceLimitLower.ReadOnly = true;
            this.ImpedanceLimitLower.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ImpedanceLimitLower.Width = 125;
            // 
            // ImpedanceLimitUpper
            // 
            this.ImpedanceLimitUpper.DataPropertyName = "ImpedanceLimitUpper";
            this.ImpedanceLimitUpper.HeaderText = "ImpedanceLimitUpper";
            this.ImpedanceLimitUpper.Name = "ImpedanceLimitUpper";
            this.ImpedanceLimitUpper.ReadOnly = true;
            this.ImpedanceLimitUpper.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ImpedanceLimitUpper.Width = 125;
            // 
            // ImpedanceLimitUnit
            // 
            this.ImpedanceLimitUnit.DataPropertyName = "ImpedanceLimitUnit";
            this.ImpedanceLimitUnit.HeaderText = "ImpedanceLimitUnit";
            this.ImpedanceLimitUnit.Name = "ImpedanceLimitUnit";
            this.ImpedanceLimitUnit.ReadOnly = true;
            this.ImpedanceLimitUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ImpedanceLimitUnit.Width = 119;
            // 
            // InputChannel
            // 
            this.InputChannel.DataPropertyName = "InputChannel";
            this.InputChannel.HeaderText = "InputChannel";
            this.InputChannel.Name = "InputChannel";
            this.InputChannel.ReadOnly = true;
            this.InputChannel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputChannel.Width = 83;
            // 
            // InputMode
            // 
            this.InputMode.DataPropertyName = "InputMode";
            this.InputMode.HeaderText = "InputMode";
            this.InputMode.Name = "InputMode";
            this.InputMode.ReadOnly = true;
            this.InputMode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InputMode.Width = 65;
            // 
            // TestMethod
            // 
            this.TestMethod.DataPropertyName = "TestMethod";
            this.TestMethod.HeaderText = "TestMethod";
            this.TestMethod.Name = "TestMethod";
            this.TestMethod.ReadOnly = true;
            this.TestMethod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TestMethod.Width = 71;
            // 
            // TestFromThreshold
            // 
            this.TestFromThreshold.DataPropertyName = "TestFromThreshold";
            this.TestFromThreshold.HeaderText = "TestFromThreshold";
            this.TestFromThreshold.Name = "TestFromThreshold";
            this.TestFromThreshold.ReadOnly = true;
            this.TestFromThreshold.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TestFromThreshold.Width = 113;
            // 
            // TestToThreshold
            // 
            this.TestToThreshold.DataPropertyName = "TestToThreshold";
            this.TestToThreshold.HeaderText = "TestToThreshold";
            this.TestToThreshold.Name = "TestToThreshold";
            this.TestToThreshold.ReadOnly = true;
            this.TestToThreshold.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TestToThreshold.Width = 101;
            // 
            // OpenThreshold
            // 
            this.OpenThreshold.DataPropertyName = "OpenThreshold";
            this.OpenThreshold.HeaderText = "OpenThreshold";
            this.OpenThreshold.Name = "OpenThreshold";
            this.OpenThreshold.ReadOnly = true;
            this.OpenThreshold.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OpenThreshold.Width = 89;
            // 
            // TraceStartPosition
            // 
            this.TraceStartPosition.DataPropertyName = "TraceStartPosition";
            this.TraceStartPosition.HeaderText = "TraceStartPosition";
            this.TraceStartPosition.Name = "TraceStartPosition";
            this.TraceStartPosition.ReadOnly = true;
            this.TraceStartPosition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TraceStartPosition.Width = 119;
            // 
            // TraceEndPosition
            // 
            this.TraceEndPosition.DataPropertyName = "TraceEndPosition";
            this.TraceEndPosition.HeaderText = "TraceEndPosition";
            this.TraceEndPosition.Name = "TraceEndPosition";
            this.TraceEndPosition.ReadOnly = true;
            this.TraceEndPosition.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TraceEndPosition.Width = 107;
            // 
            // CalibratedTimeScale
            // 
            this.CalibratedTimeScale.DataPropertyName = "CalibratedTimeScale";
            this.CalibratedTimeScale.HeaderText = "CalibratedTimeScale";
            this.CalibratedTimeScale.Name = "CalibratedTimeScale";
            this.CalibratedTimeScale.ReadOnly = true;
            this.CalibratedTimeScale.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CalibratedTimeScale.Width = 125;
            // 
            // CalibrateOffset
            // 
            this.CalibrateOffset.DataPropertyName = "CalibrateOffset";
            this.CalibrateOffset.HeaderText = "CalibrateOffset";
            this.CalibrateOffset.Name = "CalibrateOffset";
            this.CalibrateOffset.ReadOnly = true;
            this.CalibrateOffset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CalibrateOffset.Width = 101;
            // 
            // RecordPath
            // 
            this.RecordPath.DataPropertyName = "RecordPath";
            this.RecordPath.HeaderText = "RecordPath";
            this.RecordPath.Name = "RecordPath";
            this.RecordPath.ReadOnly = true;
            this.RecordPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RecordPath.Width = 71;
            // 
            // SaveCurve
            // 
            this.SaveCurve.DataPropertyName = "SaveCurve";
            this.SaveCurve.HeaderText = "SaveCurve";
            this.SaveCurve.Name = "SaveCurve";
            this.SaveCurve.ReadOnly = true;
            this.SaveCurve.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SaveCurve.Width = 65;
            // 
            // SaveImage
            // 
            this.SaveImage.DataPropertyName = "SaveImage";
            this.SaveImage.HeaderText = "SaveImage";
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.ReadOnly = true;
            this.SaveImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SaveImage.Width = 65;
            // 
            // DielectricConstant
            // 
            this.DielectricConstant.DataPropertyName = "DielectricConstant";
            this.DielectricConstant.HeaderText = "DielectricConstant";
            this.DielectricConstant.Name = "DielectricConstant";
            this.DielectricConstant.ReadOnly = true;
            this.DielectricConstant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DielectricConstant.Width = 119;
            // 
            // DataPointCheck
            // 
            this.DataPointCheck.DataPropertyName = "DataPointCheck";
            this.DataPointCheck.HeaderText = "DataPointCheck";
            this.DataPointCheck.Name = "DataPointCheck";
            this.DataPointCheck.ReadOnly = true;
            this.DataPointCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataPointCheck.Width = 95;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tx_p_Description);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.tx_p_Remark);
            this.groupBox1.Controls.Add(this.tx_p_Layer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tx_p_testSn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(0, 241);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 138);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待测物描述";
            // 
            // tx_p_Description
            // 
            this.tx_p_Description.Location = new System.Drawing.Point(71, 50);
            this.tx_p_Description.Name = "tx_p_Description";
            this.tx_p_Description.Size = new System.Drawing.Size(245, 21);
            this.tx_p_Description.TabIndex = 8;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 55);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 7;
            this.label21.Text = "描述";
            // 
            // tx_p_Remark
            // 
            this.tx_p_Remark.Location = new System.Drawing.Point(71, 104);
            this.tx_p_Remark.Name = "tx_p_Remark";
            this.tx_p_Remark.Size = new System.Drawing.Size(245, 21);
            this.tx_p_Remark.TabIndex = 6;
            // 
            // tx_p_Layer
            // 
            this.tx_p_Layer.Location = new System.Drawing.Point(71, 77);
            this.tx_p_Layer.Name = "tx_p_Layer";
            this.tx_p_Layer.Size = new System.Drawing.Size(245, 21);
            this.tx_p_Layer.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "备注";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "层";
            // 
            // tx_p_testSn
            // 
            this.tx_p_testSn.Location = new System.Drawing.Point(71, 23);
            this.tx_p_testSn.Name = "tx_p_testSn";
            this.tx_p_testSn.ReadOnly = true;
            this.tx_p_testSn.Size = new System.Drawing.Size(100, 21);
            this.tx_p_testSn.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "测试编号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox9);
            this.groupBox2.Controls.Add(this.groupBox8);
            this.groupBox2.Controls.Add(this.btn_p_SavePath);
            this.groupBox2.Controls.Add(this.tx_p_savePath);
            this.groupBox2.Location = new System.Drawing.Point(0, 385);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(335, 103);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试结果存储设定";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.radio_p_image_close);
            this.groupBox9.Controls.Add(this.radio_p_image_open);
            this.groupBox9.Location = new System.Drawing.Point(167, 47);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(155, 52);
            this.groupBox9.TabIndex = 3;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "储存图像";
            // 
            // radio_p_image_close
            // 
            this.radio_p_image_close.AutoSize = true;
            this.radio_p_image_close.Checked = true;
            this.radio_p_image_close.Location = new System.Drawing.Point(80, 25);
            this.radio_p_image_close.Name = "radio_p_image_close";
            this.radio_p_image_close.Size = new System.Drawing.Size(47, 16);
            this.radio_p_image_close.TabIndex = 3;
            this.radio_p_image_close.TabStop = true;
            this.radio_p_image_close.Text = "关闭";
            this.radio_p_image_close.UseVisualStyleBackColor = true;
            // 
            // radio_p_image_open
            // 
            this.radio_p_image_open.AutoSize = true;
            this.radio_p_image_open.Location = new System.Drawing.Point(16, 25);
            this.radio_p_image_open.Name = "radio_p_image_open";
            this.radio_p_image_open.Size = new System.Drawing.Size(47, 16);
            this.radio_p_image_open.TabIndex = 2;
            this.radio_p_image_open.Text = "开启";
            this.radio_p_image_open.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radio_p_data_close);
            this.groupBox8.Controls.Add(this.radio_p_data_open);
            this.groupBox8.Location = new System.Drawing.Point(6, 47);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(155, 52);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "储存数据";
            // 
            // radio_p_data_close
            // 
            this.radio_p_data_close.AutoSize = true;
            this.radio_p_data_close.Location = new System.Drawing.Point(83, 25);
            this.radio_p_data_close.Name = "radio_p_data_close";
            this.radio_p_data_close.Size = new System.Drawing.Size(47, 16);
            this.radio_p_data_close.TabIndex = 1;
            this.radio_p_data_close.Text = "关闭";
            this.radio_p_data_close.UseVisualStyleBackColor = true;
            // 
            // radio_p_data_open
            // 
            this.radio_p_data_open.AutoSize = true;
            this.radio_p_data_open.Checked = true;
            this.radio_p_data_open.Location = new System.Drawing.Point(12, 25);
            this.radio_p_data_open.Name = "radio_p_data_open";
            this.radio_p_data_open.Size = new System.Drawing.Size(47, 16);
            this.radio_p_data_open.TabIndex = 0;
            this.radio_p_data_open.TabStop = true;
            this.radio_p_data_open.Text = "开启";
            this.radio_p_data_open.UseVisualStyleBackColor = true;
            // 
            // btn_p_SavePath
            // 
            this.btn_p_SavePath.Location = new System.Drawing.Point(268, 18);
            this.btn_p_SavePath.Name = "btn_p_SavePath";
            this.btn_p_SavePath.Size = new System.Drawing.Size(48, 23);
            this.btn_p_SavePath.TabIndex = 1;
            this.btn_p_SavePath.Text = "...";
            this.btn_p_SavePath.UseVisualStyleBackColor = true;
            this.btn_p_SavePath.Click += new System.EventHandler(this.btn_p_SavePath_Click);
            // 
            // tx_p_savePath
            // 
            this.tx_p_savePath.Location = new System.Drawing.Point(6, 20);
            this.tx_p_savePath.Name = "tx_p_savePath";
            this.tx_p_savePath.Size = new System.Drawing.Size(256, 21);
            this.tx_p_savePath.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radio_p_diff);
            this.groupBox3.Controls.Add(this.radio_p_single);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(596, 241);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(205, 66);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "量测通道";
            // 
            // radio_p_diff
            // 
            this.radio_p_diff.AutoSize = true;
            this.radio_p_diff.Checked = true;
            this.radio_p_diff.Location = new System.Drawing.Point(83, 44);
            this.radio_p_diff.Name = "radio_p_diff";
            this.radio_p_diff.Size = new System.Drawing.Size(47, 16);
            this.radio_p_diff.TabIndex = 8;
            this.radio_p_diff.TabStop = true;
            this.radio_p_diff.Text = "差分";
            this.radio_p_diff.UseVisualStyleBackColor = true;
            this.radio_p_diff.CheckedChanged += new System.EventHandler(this.radio_p_diff_CheckedChanged);
            // 
            // radio_p_single
            // 
            this.radio_p_single.AutoSize = true;
            this.radio_p_single.Location = new System.Drawing.Point(83, 23);
            this.radio_p_single.Name = "radio_p_single";
            this.radio_p_single.Size = new System.Drawing.Size(47, 16);
            this.radio_p_single.TabIndex = 2;
            this.radio_p_single.Text = "单端";
            this.radio_p_single.UseVisualStyleBackColor = true;
            this.radio_p_single.CheckedChanged += new System.EventHandler(this.radio_p_single_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "测试模式";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.tx_p_end);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tx_p_begin);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tx_p_Index);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(353, 241);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(231, 112);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "X轴量测范围";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(165, 82);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 16);
            this.label11.TabIndex = 15;
            this.label11.Text = "%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(166, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(166, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "欧姆";
            // 
            // tx_p_end
            // 
            this.tx_p_end.Location = new System.Drawing.Point(74, 79);
            this.tx_p_end.Name = "tx_p_end";
            this.tx_p_end.Size = new System.Drawing.Size(86, 21);
            this.tx_p_end.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "终止位置";
            // 
            // tx_p_begin
            // 
            this.tx_p_begin.Location = new System.Drawing.Point(74, 53);
            this.tx_p_begin.Name = "tx_p_begin";
            this.tx_p_begin.Size = new System.Drawing.Size(86, 21);
            this.tx_p_begin.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "起始位置";
            // 
            // tx_p_Index
            // 
            this.tx_p_Index.Location = new System.Drawing.Point(74, 26);
            this.tx_p_Index.Name = "tx_p_Index";
            this.tx_p_Index.Size = new System.Drawing.Size(86, 21);
            this.tx_p_Index.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "开路位置";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lab_lowlimit_unit);
            this.groupBox5.Controls.Add(this.radio_units_percent);
            this.groupBox5.Controls.Add(this.lab_highlimit_unit);
            this.groupBox5.Controls.Add(this.radio_units_ohm);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.tx_p_lowLimit);
            this.groupBox5.Controls.Add(this.tx_p_TargetValue);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.tx_p_highLimit);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Location = new System.Drawing.Point(353, 359);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(231, 129);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "边界设定";
            // 
            // lab_lowlimit_unit
            // 
            this.lab_lowlimit_unit.AutoSize = true;
            this.lab_lowlimit_unit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_lowlimit_unit.Location = new System.Drawing.Point(168, 102);
            this.lab_lowlimit_unit.Name = "lab_lowlimit_unit";
            this.lab_lowlimit_unit.Size = new System.Drawing.Size(16, 16);
            this.lab_lowlimit_unit.TabIndex = 24;
            this.lab_lowlimit_unit.Text = "%";
            // 
            // radio_units_percent
            // 
            this.radio_units_percent.AutoSize = true;
            this.radio_units_percent.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radio_units_percent.Location = new System.Drawing.Point(148, 24);
            this.radio_units_percent.Name = "radio_units_percent";
            this.radio_units_percent.Size = new System.Drawing.Size(34, 20);
            this.radio_units_percent.TabIndex = 3;
            this.radio_units_percent.Text = "%";
            this.radio_units_percent.UseVisualStyleBackColor = true;
            this.radio_units_percent.CheckedChanged += new System.EventHandler(this.radio_units_percent_CheckedChanged);
            // 
            // lab_highlimit_unit
            // 
            this.lab_highlimit_unit.AutoSize = true;
            this.lab_highlimit_unit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_highlimit_unit.Location = new System.Drawing.Point(169, 77);
            this.lab_highlimit_unit.Name = "lab_highlimit_unit";
            this.lab_highlimit_unit.Size = new System.Drawing.Size(16, 16);
            this.lab_highlimit_unit.TabIndex = 23;
            this.lab_highlimit_unit.Text = "%";
            // 
            // radio_units_ohm
            // 
            this.radio_units_ohm.AutoSize = true;
            this.radio_units_ohm.Checked = true;
            this.radio_units_ohm.Location = new System.Drawing.Point(77, 24);
            this.radio_units_ohm.Name = "radio_units_ohm";
            this.radio_units_ohm.Size = new System.Drawing.Size(47, 16);
            this.radio_units_ohm.TabIndex = 2;
            this.radio_units_ohm.TabStop = true;
            this.radio_units_ohm.Text = "欧姆";
            this.radio_units_ohm.UseVisualStyleBackColor = true;
            this.radio_units_ohm.CheckedChanged += new System.EventHandler(this.radio_units_ohm_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(169, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 22;
            this.label15.Text = "欧姆";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "单位";
            // 
            // tx_p_lowLimit
            // 
            this.tx_p_lowLimit.Location = new System.Drawing.Point(77, 99);
            this.tx_p_lowLimit.Name = "tx_p_lowLimit";
            this.tx_p_lowLimit.Size = new System.Drawing.Size(86, 21);
            this.tx_p_lowLimit.TabIndex = 21;
            // 
            // tx_p_TargetValue
            // 
            this.tx_p_TargetValue.Location = new System.Drawing.Point(77, 46);
            this.tx_p_TargetValue.Name = "tx_p_TargetValue";
            this.tx_p_TargetValue.Size = new System.Drawing.Size(86, 21);
            this.tx_p_TargetValue.TabIndex = 17;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 103);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 20;
            this.label16.Text = "下限";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(18, 50);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 16;
            this.label18.Text = "阻抗";
            // 
            // tx_p_highLimit
            // 
            this.tx_p_highLimit.Location = new System.Drawing.Point(77, 73);
            this.tx_p_highLimit.Name = "tx_p_highLimit";
            this.tx_p_highLimit.Size = new System.Drawing.Size(86, 21);
            this.tx_p_highLimit.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 77);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 18;
            this.label17.Text = "上限";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.tx_p_yOffset);
            this.groupBox6.Location = new System.Drawing.Point(597, 384);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(205, 70);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Y轴 自定补偿";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "欧姆";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(167, 28);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(0, 12);
            this.label20.TabIndex = 17;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(17, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 16;
            this.label19.Text = "补偿值";
            // 
            // tx_p_yOffset
            // 
            this.tx_p_yOffset.Location = new System.Drawing.Point(76, 22);
            this.tx_p_yOffset.Name = "tx_p_yOffset";
            this.tx_p_yOffset.Size = new System.Drawing.Size(70, 21);
            this.tx_p_yOffset.TabIndex = 5;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.radio_p_tag_point);
            this.groupBox7.Controls.Add(this.radio_p_tag_avg);
            this.groupBox7.Location = new System.Drawing.Point(597, 326);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(205, 39);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "待测物判定";
            // 
            // radio_p_tag_point
            // 
            this.radio_p_tag_point.AutoSize = true;
            this.radio_p_tag_point.Checked = true;
            this.radio_p_tag_point.Location = new System.Drawing.Point(135, 17);
            this.radio_p_tag_point.Name = "radio_p_tag_point";
            this.radio_p_tag_point.Size = new System.Drawing.Size(47, 16);
            this.radio_p_tag_point.TabIndex = 5;
            this.radio_p_tag_point.TabStop = true;
            this.radio_p_tag_point.Text = "点数";
            this.radio_p_tag_point.UseVisualStyleBackColor = true;
            // 
            // radio_p_tag_avg
            // 
            this.radio_p_tag_avg.AutoSize = true;
            this.radio_p_tag_avg.Location = new System.Drawing.Point(36, 17);
            this.radio_p_tag_avg.Name = "radio_p_tag_avg";
            this.radio_p_tag_avg.Size = new System.Drawing.Size(59, 16);
            this.radio_p_tag_avg.TabIndex = 4;
            this.radio_p_tag_avg.Text = "平均值";
            this.radio_p_tag_avg.UseVisualStyleBackColor = true;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(726, 461);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 23);
            this.btn_update.TabIndex = 7;
            this.btn_update.Text = "确定";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(597, 461);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // DevParamSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 503);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DevParamSet";
            this.Text = "TDR 量测试参数";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DevParamSet_FormClosed);
            this.Load += new System.EventHandler(this.DevParamSet_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_param)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_create_xml;
        private System.Windows.Forms.ToolStripButton tsb_measure_loadXml;
        private System.Windows.Forms.ToolStripButton tsb_save_xml;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsb_add_param;
        private System.Windows.Forms.ToolStripButton tsb_copy_param;
        private System.Windows.Forms.ToolStripButton tsb_del_param;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tx_p_Remark;
        private System.Windows.Forms.TextBox tx_p_Layer;
        private System.Windows.Forms.TextBox tx_p_yOffset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tx_p_testSn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton radio_p_image_close;
        private System.Windows.Forms.RadioButton radio_p_image_open;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radio_p_data_close;
        private System.Windows.Forms.RadioButton radio_p_data_open;
        private System.Windows.Forms.Button btn_p_SavePath;
        private System.Windows.Forms.TextBox tx_p_savePath;
        private System.Windows.Forms.RadioButton radio_p_diff;
        private System.Windows.Forms.RadioButton radio_p_single;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tx_p_end;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tx_p_begin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tx_p_Index;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radio_p_tag_point;
        private System.Windows.Forms.RadioButton radio_p_tag_avg;
        private System.Windows.Forms.Label lab_lowlimit_unit;
        private System.Windows.Forms.RadioButton radio_units_percent;
        private System.Windows.Forms.Label lab_highlimit_unit;
        private System.Windows.Forms.RadioButton radio_units_ohm;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tx_p_lowLimit;
        private System.Windows.Forms.TextBox tx_p_TargetValue;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tx_p_highLimit;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox tx_p_Description;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DataGridView dgv_param;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Layer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImpedanceDefine;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImpedanceLimitLower;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImpedanceLimitUpper;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImpedanceLimitUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestFromThreshold;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestToThreshold;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenThreshold;
        private System.Windows.Forms.DataGridViewTextBoxColumn TraceStartPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn TraceEndPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalibratedTimeScale;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalibrateOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaveCurve;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaveImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn DielectricConstant;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataPointCheck;
    }
}