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
            this.combDevType.Location = new System.Drawing.Point(161, 20);
            this.combDevType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.combDevType.Name = "combDevType";
            this.combDevType.Size = new System.Drawing.Size(118, 23);
            this.combDevType.TabIndex = 0;
            this.combDevType.Text = "E5063A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(293, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "VISA Address:";
            // 
            // btn_ConnectDev
            // 
            this.btn_ConnectDev.Location = new System.Drawing.Point(709, 18);
            this.btn_ConnectDev.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_ConnectDev.Name = "btn_ConnectDev";
            this.btn_ConnectDev.Size = new System.Drawing.Size(100, 28);
            this.btn_ConnectDev.TabIndex = 3;
            this.btn_ConnectDev.Text = "Connection";
            this.btn_ConnectDev.UseVisualStyleBackColor = true;
            this.btn_ConnectDev.Click += new System.EventHandler(this.btn_ConnectDev_Click);
            // 
            // combDevString
            // 
            this.combDevString.FormattingEnabled = true;
            this.combDevString.Location = new System.Drawing.Point(410, 20);
            this.combDevString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.combDevString.Name = "combDevString";
            this.combDevString.Size = new System.Drawing.Size(275, 23);
            this.combDevString.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Instrument Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 62);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Material Number：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(161, 58);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(522, 25);
            this.textBox1.TabIndex = 7;
            // 
            // DevConnectSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 380);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combDevString);
            this.Controls.Add(this.btn_ConnectDev);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combDevType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DevConnectSet";
            this.Text = "Instrument settings";
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