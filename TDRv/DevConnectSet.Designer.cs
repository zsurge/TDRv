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
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tx_server_ip = new System.Windows.Forms.TextBox();
            this.tx_server_port = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_UserName = new System.Windows.Forms.Label();
            this.btn_setServerIP = new System.Windows.Forms.Button();
            this.btn_SetControlMode = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comb_control_mode = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.combDevType.Location = new System.Drawing.Point(68, 22);
            this.combDevType.Name = "combDevType";
            this.combDevType.Size = new System.Drawing.Size(72, 20);
            this.combDevType.TabIndex = 0;
            this.combDevType.Text = "E5080B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "VISA位置:";
            // 
            // btn_ConnectDev
            // 
            this.btn_ConnectDev.Location = new System.Drawing.Point(575, 20);
            this.btn_ConnectDev.Name = "btn_ConnectDev";
            this.btn_ConnectDev.Size = new System.Drawing.Size(75, 23);
            this.btn_ConnectDev.TabIndex = 3;
            this.btn_ConnectDev.Text = "连接仪器";
            this.btn_ConnectDev.UseVisualStyleBackColor = true;
            this.btn_ConnectDev.Click += new System.EventHandler(this.btn_ConnectDev_Click);
            // 
            // combDevString
            // 
            this.combDevString.FormattingEnabled = true;
            this.combDevString.Location = new System.Drawing.Point(222, 22);
            this.combDevString.Name = "combDevString";
            this.combDevString.Size = new System.Drawing.Size(347, 20);
            this.combDevString.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "设备型号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "料号：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(68, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(247, 21);
            this.textBox1.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "服务器：";
            // 
            // tx_server_ip
            // 
            this.tx_server_ip.Location = new System.Drawing.Point(68, 17);
            this.tx_server_ip.Name = "tx_server_ip";
            this.tx_server_ip.Size = new System.Drawing.Size(109, 21);
            this.tx_server_ip.TabIndex = 9;
            this.tx_server_ip.Text = "192.168.100.101";
            // 
            // tx_server_port
            // 
            this.tx_server_port.Location = new System.Drawing.Point(184, 17);
            this.tx_server_port.Name = "tx_server_port";
            this.tx_server_port.Size = new System.Drawing.Size(57, 21);
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
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(656, 60);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comb_control_mode);
            this.groupBox2.Controls.Add(this.btn_SetControlMode);
            this.groupBox2.Controls.Add(this.btn_setServerIP);
            this.groupBox2.Controls.Add(this.label_UserName);
            this.groupBox2.Controls.Add(this.tx_server_port);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tx_server_ip);
            this.groupBox2.Location = new System.Drawing.Point(5, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 81);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(9, 47);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(53, 12);
            this.label_UserName.TabIndex = 15;
            this.label_UserName.Text = "控制模式";
            // 
            // btn_setServerIP
            // 
            this.btn_setServerIP.Location = new System.Drawing.Point(257, 15);
            this.btn_setServerIP.Name = "btn_setServerIP";
            this.btn_setServerIP.Size = new System.Drawing.Size(75, 23);
            this.btn_setServerIP.TabIndex = 16;
            this.btn_setServerIP.Text = "设置";
            this.btn_setServerIP.UseVisualStyleBackColor = true;
            this.btn_setServerIP.Click += new System.EventHandler(this.btn_setServerIP_Click);
            // 
            // btn_SetControlMode
            // 
            this.btn_SetControlMode.Location = new System.Drawing.Point(257, 42);
            this.btn_SetControlMode.Name = "btn_SetControlMode";
            this.btn_SetControlMode.Size = new System.Drawing.Size(75, 23);
            this.btn_SetControlMode.TabIndex = 17;
            this.btn_SetControlMode.Text = "设置";
            this.btn_SetControlMode.UseVisualStyleBackColor = true;
            this.btn_SetControlMode.Click += new System.EventHandler(this.btn_SetControlMode_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(5, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 139);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(359, 66);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(302, 226);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            // 
            // comb_control_mode
            // 
            this.comb_control_mode.FormattingEnabled = true;
            this.comb_control_mode.Items.AddRange(new object[] {
            "OnLine",
            "OffLine"});
            this.comb_control_mode.Location = new System.Drawing.Point(68, 42);
            this.comb_control_mode.Name = "comb_control_mode";
            this.comb_control_mode.Size = new System.Drawing.Size(173, 20);
            this.comb_control_mode.TabIndex = 18;
            this.comb_control_mode.Text = "OnLine";
            // 
            // DevConnectSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 304);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DevConnectSet";
            this.Text = "仪器设定";
            this.Load += new System.EventHandler(this.DevConnectSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox combDevType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ConnectDev;
        private System.Windows.Forms.ComboBox combDevString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tx_server_ip;
        private System.Windows.Forms.TextBox tx_server_port;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_UserName;
        private System.Windows.Forms.Button btn_SetControlMode;
        private System.Windows.Forms.Button btn_setServerIP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox comb_control_mode;
    }
}