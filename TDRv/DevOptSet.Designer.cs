namespace TDRv
{
    partial class DevOptSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevOptSet));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_key_space = new System.Windows.Forms.RadioButton();
            this.radio_key_close = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radio_pro_only_pass = new System.Windows.Forms.RadioButton();
            this.radio_pro_next = new System.Windows.Forms.RadioButton();
            this.radio_pro_manual = new System.Windows.Forms.RadioButton();
            this.radio_pro_pass = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radio_save_param = new System.Windows.Forms.RadioButton();
            this.cmbo_format = new System.Windows.Forms.ComboBox();
            this.radio_save_date = new System.Windows.Forms.RadioButton();
            this.tx_export_report = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tx_history_report = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tx_sn_begin = new System.Windows.Forms.TextBox();
            this.tx_sn_prefix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radio_sn_auto = new System.Windows.Forms.RadioButton();
            this.radio_sn_manual = new System.Windows.Forms.RadioButton();
            this.btn_opt_ok = new System.Windows.Forms.Button();
            this.btn_opt_cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_key_space);
            this.groupBox1.Controls.Add(this.radio_key_close);
            this.groupBox1.Location = new System.Drawing.Point(18, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(194, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keyboard trigger";
            // 
            // radio_key_space
            // 
            this.radio_key_space.AutoSize = true;
            this.radio_key_space.Location = new System.Drawing.Point(24, 62);
            this.radio_key_space.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_key_space.Name = "radio_key_space";
            this.radio_key_space.Size = new System.Drawing.Size(114, 22);
            this.radio_key_space.TabIndex = 1;
            this.radio_key_space.Text = "Space Bar";
            this.radio_key_space.UseVisualStyleBackColor = true;
            // 
            // radio_key_close
            // 
            this.radio_key_close.AutoSize = true;
            this.radio_key_close.Checked = true;
            this.radio_key_close.Location = new System.Drawing.Point(24, 30);
            this.radio_key_close.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_key_close.Name = "radio_key_close";
            this.radio_key_close.Size = new System.Drawing.Size(78, 22);
            this.radio_key_close.TabIndex = 0;
            this.radio_key_close.TabStop = true;
            this.radio_key_close.Text = "Close";
            this.radio_key_close.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radio_pro_only_pass);
            this.groupBox2.Controls.Add(this.radio_pro_next);
            this.groupBox2.Controls.Add(this.radio_pro_manual);
            this.groupBox2.Controls.Add(this.radio_pro_pass);
            this.groupBox2.Location = new System.Drawing.Point(18, 163);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(194, 163);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test process";
            // 
            // radio_pro_only_pass
            // 
            this.radio_pro_only_pass.AutoSize = true;
            this.radio_pro_only_pass.Location = new System.Drawing.Point(9, 130);
            this.radio_pro_only_pass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_pro_only_pass.Name = "radio_pro_only_pass";
            this.radio_pro_only_pass.Size = new System.Drawing.Size(177, 22);
            this.radio_pro_only_pass.TabIndex = 3;
            this.radio_pro_only_pass.Text = "Record only pass";
            this.radio_pro_only_pass.UseVisualStyleBackColor = true;
            // 
            // radio_pro_next
            // 
            this.radio_pro_next.AutoSize = true;
            this.radio_pro_next.Checked = true;
            this.radio_pro_next.Location = new System.Drawing.Point(9, 96);
            this.radio_pro_next.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_pro_next.Name = "radio_pro_next";
            this.radio_pro_next.Size = new System.Drawing.Size(69, 22);
            this.radio_pro_next.TabIndex = 2;
            this.radio_pro_next.TabStop = true;
            this.radio_pro_next.Text = "Next";
            this.radio_pro_next.UseVisualStyleBackColor = true;
            // 
            // radio_pro_manual
            // 
            this.radio_pro_manual.AutoSize = true;
            this.radio_pro_manual.Location = new System.Drawing.Point(9, 62);
            this.radio_pro_manual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_pro_manual.Name = "radio_pro_manual";
            this.radio_pro_manual.Size = new System.Drawing.Size(78, 22);
            this.radio_pro_manual.TabIndex = 1;
            this.radio_pro_manual.Text = "Manul";
            this.radio_pro_manual.UseVisualStyleBackColor = true;
            // 
            // radio_pro_pass
            // 
            this.radio_pro_pass.AutoSize = true;
            this.radio_pro_pass.Location = new System.Drawing.Point(9, 30);
            this.radio_pro_pass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_pro_pass.Name = "radio_pro_pass";
            this.radio_pro_pass.Size = new System.Drawing.Size(69, 22);
            this.radio_pro_pass.TabIndex = 0;
            this.radio_pro_pass.Text = "Pass";
            this.radio_pro_pass.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radio_save_param);
            this.groupBox4.Controls.Add(this.cmbo_format);
            this.groupBox4.Controls.Add(this.radio_save_date);
            this.groupBox4.Controls.Add(this.tx_export_report);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.tx_history_report);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(256, 163);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(516, 163);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Storage Mode";
            // 
            // radio_save_param
            // 
            this.radio_save_param.AutoSize = true;
            this.radio_save_param.Checked = true;
            this.radio_save_param.Location = new System.Drawing.Point(123, 30);
            this.radio_save_param.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_save_param.Name = "radio_save_param";
            this.radio_save_param.Size = new System.Drawing.Size(222, 22);
            this.radio_save_param.TabIndex = 7;
            this.radio_save_param.TabStop = true;
            this.radio_save_param.Text = "Measurement Parameter";
            this.radio_save_param.UseVisualStyleBackColor = true;
            this.radio_save_param.CheckedChanged += new System.EventHandler(this.radio_save_param_CheckedChanged);
            // 
            // cmbo_format
            // 
            this.cmbo_format.Enabled = false;
            this.cmbo_format.FormattingEnabled = true;
            this.cmbo_format.Items.AddRange(new object[] {
            "CSV",
            "TXT"});
            this.cmbo_format.Location = new System.Drawing.Point(429, 102);
            this.cmbo_format.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbo_format.Name = "cmbo_format";
            this.cmbo_format.Size = new System.Drawing.Size(76, 26);
            this.cmbo_format.TabIndex = 12;
            this.cmbo_format.Text = "CSV";
            // 
            // radio_save_date
            // 
            this.radio_save_date.AutoSize = true;
            this.radio_save_date.Location = new System.Drawing.Point(26, 30);
            this.radio_save_date.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_save_date.Name = "radio_save_date";
            this.radio_save_date.Size = new System.Drawing.Size(69, 22);
            this.radio_save_date.TabIndex = 6;
            this.radio_save_date.Text = "Date";
            this.radio_save_date.UseVisualStyleBackColor = true;
            this.radio_save_date.CheckedChanged += new System.EventHandler(this.radio_save_date_CheckedChanged);
            // 
            // tx_export_report
            // 
            this.tx_export_report.Location = new System.Drawing.Point(174, 102);
            this.tx_export_report.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tx_export_report.Name = "tx_export_report";
            this.tx_export_report.ReadOnly = true;
            this.tx_export_report.Size = new System.Drawing.Size(244, 28);
            this.tx_export_report.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 105);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output Data";
            // 
            // tx_history_report
            // 
            this.tx_history_report.Location = new System.Drawing.Point(174, 62);
            this.tx_history_report.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tx_history_report.Name = "tx_history_report";
            this.tx_history_report.ReadOnly = true;
            this.tx_history_report.Size = new System.Drawing.Size(244, 28);
            this.tx_history_report.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 66);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "History Data";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tx_sn_begin);
            this.groupBox5.Controls.Add(this.tx_sn_prefix);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(256, 2);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(330, 151);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "EUT SN.";
            // 
            // tx_sn_begin
            // 
            this.tx_sn_begin.Location = new System.Drawing.Point(116, 94);
            this.tx_sn_begin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tx_sn_begin.Name = "tx_sn_begin";
            this.tx_sn_begin.Size = new System.Drawing.Size(203, 28);
            this.tx_sn_begin.TabIndex = 9;
            this.tx_sn_begin.Text = "0001";
            // 
            // tx_sn_prefix
            // 
            this.tx_sn_prefix.Location = new System.Drawing.Point(116, 42);
            this.tx_sn_prefix.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tx_sn_prefix.Name = "tx_sn_prefix";
            this.tx_sn_prefix.Size = new System.Drawing.Size(203, 28);
            this.tx_sn_prefix.TabIndex = 8;
            this.tx_sn_prefix.Text = "SN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Start SN.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "SN.Header";
            // 
            // radio_sn_auto
            // 
            this.radio_sn_auto.AutoSize = true;
            this.radio_sn_auto.Checked = true;
            this.radio_sn_auto.Location = new System.Drawing.Point(702, 18);
            this.radio_sn_auto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_sn_auto.Name = "radio_sn_auto";
            this.radio_sn_auto.Size = new System.Drawing.Size(69, 22);
            this.radio_sn_auto.TabIndex = 5;
            this.radio_sn_auto.TabStop = true;
            this.radio_sn_auto.Text = "Auto";
            this.radio_sn_auto.UseVisualStyleBackColor = true;
            this.radio_sn_auto.Visible = false;
            // 
            // radio_sn_manual
            // 
            this.radio_sn_manual.AutoSize = true;
            this.radio_sn_manual.Location = new System.Drawing.Point(622, 18);
            this.radio_sn_manual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radio_sn_manual.Name = "radio_sn_manual";
            this.radio_sn_manual.Size = new System.Drawing.Size(78, 22);
            this.radio_sn_manual.TabIndex = 4;
            this.radio_sn_manual.Text = "Manul";
            this.radio_sn_manual.UseVisualStyleBackColor = true;
            this.radio_sn_manual.Visible = false;
            // 
            // btn_opt_ok
            // 
            this.btn_opt_ok.Location = new System.Drawing.Point(522, 336);
            this.btn_opt_ok.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_opt_ok.Name = "btn_opt_ok";
            this.btn_opt_ok.Size = new System.Drawing.Size(112, 35);
            this.btn_opt_ok.TabIndex = 3;
            this.btn_opt_ok.Text = "Confirm";
            this.btn_opt_ok.UseVisualStyleBackColor = true;
            this.btn_opt_ok.Click += new System.EventHandler(this.btn_opt_ok_Click);
            // 
            // btn_opt_cancel
            // 
            this.btn_opt_cancel.Location = new System.Drawing.Point(660, 336);
            this.btn_opt_cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_opt_cancel.Name = "btn_opt_cancel";
            this.btn_opt_cancel.Size = new System.Drawing.Size(112, 35);
            this.btn_opt_cancel.TabIndex = 3;
            this.btn_opt_cancel.Text = "Cancel";
            this.btn_opt_cancel.UseVisualStyleBackColor = true;
            this.btn_opt_cancel.Click += new System.EventHandler(this.btn_opt_cancel_Click);
            // 
            // DevOptSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 385);
            this.Controls.Add(this.btn_opt_cancel);
            this.Controls.Add(this.btn_opt_ok);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.radio_sn_auto);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.radio_sn_manual);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DevOptSet";
            this.Text = "Operation Config Setup";
            this.Load += new System.EventHandler(this.DevOptSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radio_key_space;
        private System.Windows.Forms.RadioButton radio_key_close;
        private System.Windows.Forms.RadioButton radio_pro_only_pass;
        private System.Windows.Forms.RadioButton radio_pro_next;
        private System.Windows.Forms.RadioButton radio_pro_manual;
        private System.Windows.Forms.RadioButton radio_pro_pass;
        private System.Windows.Forms.ComboBox cmbo_format;
        private System.Windows.Forms.TextBox tx_export_report;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tx_history_report;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tx_sn_begin;
        private System.Windows.Forms.TextBox tx_sn_prefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radio_sn_auto;
        private System.Windows.Forms.RadioButton radio_sn_manual;
        private System.Windows.Forms.RadioButton radio_save_param;
        private System.Windows.Forms.RadioButton radio_save_date;
        private System.Windows.Forms.Button btn_opt_ok;
        private System.Windows.Forms.Button btn_opt_cancel;
    }
}