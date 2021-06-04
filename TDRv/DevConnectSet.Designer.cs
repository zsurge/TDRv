namespace TDRv
{
    partial class DevConnectSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevConnectSet));
            this.combDevType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ConnectDev = new System.Windows.Forms.Button();
            this.combDevString = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tx_server_ip = new System.Windows.Forms.TextBox();
            this.tx_server_port = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comb_control_mode = new System.Windows.Forms.ComboBox();
            this.btn_SetControlMode = new System.Windows.Forms.Button();
            this.btn_setServerIP = new System.Windows.Forms.Button();
            this.label_UserName = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tx_sn = new System.Windows.Forms.TextBox();
            this.btn_SetSn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // combDevType
            // 
            this.combDevType.FormattingEnabled = true;
            this.combDevType.Items.AddRange(new object[] {
            "E5080B",
            "E5071C",
            "PNA",
            "USB-ENA"});
            this.combDevType.Location = new System.Drawing.Point(136, 44);
            this.combDevType.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.combDevType.Name = "combDevType";
            this.combDevType.Size = new System.Drawing.Size(140, 32);
            this.combDevType.TabIndex = 0;
            this.combDevType.Text = "E5080B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "VISA位置:";
            // 
            // btn_ConnectDev
            // 
            this.btn_ConnectDev.Location = new System.Drawing.Point(1150, 40);
            this.btn_ConnectDev.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_ConnectDev.Name = "btn_ConnectDev";
            this.btn_ConnectDev.Size = new System.Drawing.Size(150, 46);
            this.btn_ConnectDev.TabIndex = 3;
            this.btn_ConnectDev.Text = "连接仪器";
            this.btn_ConnectDev.UseVisualStyleBackColor = true;
            this.btn_ConnectDev.Click += new System.EventHandler(this.btn_ConnectDev_Click);
            // 
            // combDevString
            // 
            this.combDevString.FormattingEnabled = true;
            this.combDevString.Location = new System.Drawing.Point(444, 44);
            this.combDevString.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.combDevString.Name = "combDevString";
            this.combDevString.Size = new System.Drawing.Size(690, 32);
            this.combDevString.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "设备型号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "服务器:";
            // 
            // tx_server_ip
            // 
            this.tx_server_ip.Location = new System.Drawing.Point(130, 84);
            this.tx_server_ip.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tx_server_ip.Name = "tx_server_ip";
            this.tx_server_ip.Size = new System.Drawing.Size(214, 35);
            this.tx_server_ip.TabIndex = 9;
            this.tx_server_ip.Text = "192.168.100.101";
            // 
            // tx_server_port
            // 
            this.tx_server_port.Location = new System.Drawing.Point(362, 84);
            this.tx_server_port.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tx_server_port.Name = "tx_server_port";
            this.tx_server_port.Size = new System.Drawing.Size(110, 35);
            this.tx_server_port.TabIndex = 11;
            this.tx_server_port.Text = "5200";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_ConnectDev);
            this.groupBox1.Controls.Add(this.combDevType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.combDevString);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(1312, 120);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_SetSn);
            this.groupBox2.Controls.Add(this.tx_sn);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comb_control_mode);
            this.groupBox2.Controls.Add(this.btn_SetControlMode);
            this.groupBox2.Controls.Add(this.btn_setServerIP);
            this.groupBox2.Controls.Add(this.label_UserName);
            this.groupBox2.Controls.Add(this.tx_server_port);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tx_server_ip);
            this.groupBox2.Location = new System.Drawing.Point(10, 132);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(696, 248);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // comb_control_mode
            // 
            this.comb_control_mode.FormattingEnabled = true;
            this.comb_control_mode.Items.AddRange(new object[] {
            "OnLine",
            "OffLine"});
            this.comb_control_mode.Location = new System.Drawing.Point(130, 137);
            this.comb_control_mode.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comb_control_mode.Name = "comb_control_mode";
            this.comb_control_mode.Size = new System.Drawing.Size(342, 32);
            this.comb_control_mode.TabIndex = 18;
            this.comb_control_mode.Text = "OnLine";
            // 
            // btn_SetControlMode
            // 
            this.btn_SetControlMode.Location = new System.Drawing.Point(508, 129);
            this.btn_SetControlMode.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_SetControlMode.Name = "btn_SetControlMode";
            this.btn_SetControlMode.Size = new System.Drawing.Size(150, 46);
            this.btn_SetControlMode.TabIndex = 17;
            this.btn_SetControlMode.Text = "设置";
            this.btn_SetControlMode.UseVisualStyleBackColor = true;
            this.btn_SetControlMode.Click += new System.EventHandler(this.btn_SetControlMode_Click);
            // 
            // btn_setServerIP
            // 
            this.btn_setServerIP.Location = new System.Drawing.Point(508, 76);
            this.btn_setServerIP.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_setServerIP.Name = "btn_setServerIP";
            this.btn_setServerIP.Size = new System.Drawing.Size(150, 46);
            this.btn_setServerIP.TabIndex = 16;
            this.btn_setServerIP.Text = "设置";
            this.btn_setServerIP.UseVisualStyleBackColor = true;
            this.btn_setServerIP.Click += new System.EventHandler(this.btn_setServerIP_Click);
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(12, 146);
            this.label_UserName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(118, 24);
            this.label_UserName.TabIndex = 15;
            this.label_UserName.Text = "控制模式:";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(718, 132);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Size = new System.Drawing.Size(604, 452);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "EQP_ID:";
            // 
            // tx_sn
            // 
            this.tx_sn.Location = new System.Drawing.Point(130, 31);
            this.tx_sn.Name = "tx_sn";
            this.tx_sn.Size = new System.Drawing.Size(342, 35);
            this.tx_sn.TabIndex = 20;
            // 
            // btn_SetSn
            // 
            this.btn_SetSn.Location = new System.Drawing.Point(508, 23);
            this.btn_SetSn.Name = "btn_SetSn";
            this.btn_SetSn.Size = new System.Drawing.Size(150, 46);
            this.btn_SetSn.TabIndex = 21;
            this.btn_SetSn.Text = "设置";
            this.btn_SetSn.UseVisualStyleBackColor = true;
            this.btn_SetSn.Click += new System.EventHandler(this.btn_SetSn_Click);
            // 
            // DevConnectSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 608);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "DevConnectSet";
            this.Text = "仪器设定";
            this.Load += new System.EventHandler(this.DevConnectSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox combDevType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ConnectDev;
        private System.Windows.Forms.ComboBox combDevString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tx_server_ip;
        private System.Windows.Forms.TextBox tx_server_port;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_UserName;
        private System.Windows.Forms.Button btn_SetControlMode;
        private System.Windows.Forms.Button btn_setServerIP;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comb_control_mode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_SetSn;
        private System.Windows.Forms.TextBox tx_sn;
    }
}