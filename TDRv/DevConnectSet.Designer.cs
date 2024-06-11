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
            this.SuspendLayout();
            // 
            // combDevType
            // 
            this.combDevType.FormattingEnabled = true;
            this.combDevType.Items.AddRange(new object[] {
            "E5080B 4-port",
            "E5080B 2-port",
            "E5071C 4-port",
            "E5071C 2-port",
            "E5063A",
            "PNA",
            "USB-ENA"});
            this.combDevType.Location = new System.Drawing.Point(106, 24);
            this.combDevType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.combDevType.Name = "combDevType";
            this.combDevType.Size = new System.Drawing.Size(151, 26);
            this.combDevType.TabIndex = 0;
            this.combDevType.Text = "E5063A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(288, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "VISA位置:";
            // 
            // btn_ConnectDev
            // 
            this.btn_ConnectDev.Location = new System.Drawing.Point(798, 21);
            this.btn_ConnectDev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_ConnectDev.Name = "btn_ConnectDev";
            this.btn_ConnectDev.Size = new System.Drawing.Size(112, 34);
            this.btn_ConnectDev.TabIndex = 3;
            this.btn_ConnectDev.Text = "连接仪器";
            this.btn_ConnectDev.UseVisualStyleBackColor = true;
            this.btn_ConnectDev.Click += new System.EventHandler(this.btn_ConnectDev_Click);
            // 
            // combDevString
            // 
            this.combDevString.FormattingEnabled = true;
            this.combDevString.Location = new System.Drawing.Point(386, 24);
            this.combDevString.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.combDevString.Name = "combDevString";
            this.combDevString.Size = new System.Drawing.Size(384, 26);
            this.combDevString.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "设备型号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "料号：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 69);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(662, 28);
            this.textBox1.TabIndex = 7;
            // 
            // DevConnectSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 456);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combDevString);
            this.Controls.Add(this.btn_ConnectDev);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combDevType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DevConnectSet";
            this.Text = "仪器设定";
            this.Load += new System.EventHandler(this.DevConnectSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combDevType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ConnectDev;
        private System.Windows.Forms.ComboBox combDevString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}