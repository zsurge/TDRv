﻿namespace TDRv
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tx_PassWord = new System.Windows.Forms.TextBox();
            this.tx_UserName = new System.Windows.Forms.TextBox();
            this.label_PassWord = new System.Windows.Forms.Label();
            this.label_UserName = new System.Windows.Forms.Label();
            this.btn_Login_Close = new System.Windows.Forms.Button();
            this.btn_Login = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tx_server_ip = new System.Windows.Forms.TextBox();
            this.tx_server_port = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(294, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(668, 90);
            this.label1.TabIndex = 11;
            this.label1.Text = "泰仕捷自动测试系统";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tx_PassWord
            // 
            this.tx_PassWord.Location = new System.Drawing.Point(474, 410);
            this.tx_PassWord.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tx_PassWord.Name = "tx_PassWord";
            this.tx_PassWord.PasswordChar = '*';
            this.tx_PassWord.Size = new System.Drawing.Size(342, 35);
            this.tx_PassWord.TabIndex = 8;
            this.tx_PassWord.Text = "123456";
            // 
            // tx_UserName
            // 
            this.tx_UserName.Location = new System.Drawing.Point(474, 356);
            this.tx_UserName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tx_UserName.Name = "tx_UserName";
            this.tx_UserName.Size = new System.Drawing.Size(342, 35);
            this.tx_UserName.TabIndex = 7;
            this.tx_UserName.Text = "tsj";
            // 
            // label_PassWord
            // 
            this.label_PassWord.AutoSize = true;
            this.label_PassWord.Location = new System.Drawing.Point(356, 426);
            this.label_PassWord.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_PassWord.Name = "label_PassWord";
            this.label_PassWord.Size = new System.Drawing.Size(58, 24);
            this.label_PassWord.TabIndex = 13;
            this.label_PassWord.Text = "密码";
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(356, 362);
            this.label_UserName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(106, 24);
            this.label_UserName.TabIndex = 12;
            this.label_UserName.Text = "用户名：";
            // 
            // btn_Login_Close
            // 
            this.btn_Login_Close.Location = new System.Drawing.Point(402, 506);
            this.btn_Login_Close.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_Login_Close.Name = "btn_Login_Close";
            this.btn_Login_Close.Size = new System.Drawing.Size(150, 46);
            this.btn_Login_Close.TabIndex = 10;
            this.btn_Login_Close.Text = "关闭";
            this.btn_Login_Close.UseVisualStyleBackColor = true;
            this.btn_Login_Close.Click += new System.EventHandler(this.btn_Login_Close_Click);
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(646, 506);
            this.btn_Login.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(150, 46);
            this.btn_Login.TabIndex = 9;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(356, 300);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "服务器：";
            // 
            // tx_server_ip
            // 
            this.tx_server_ip.Location = new System.Drawing.Point(474, 294);
            this.tx_server_ip.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tx_server_ip.Name = "tx_server_ip";
            this.tx_server_ip.Size = new System.Drawing.Size(236, 35);
            this.tx_server_ip.TabIndex = 15;
            this.tx_server_ip.Text = "192.168.1.1";
            // 
            // tx_server_port
            // 
            this.tx_server_port.Location = new System.Drawing.Point(726, 294);
            this.tx_server_port.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tx_server_port.Name = "tx_server_port";
            this.tx_server_port.Size = new System.Drawing.Size(90, 35);
            this.tx_server_port.TabIndex = 16;
            this.tx_server_port.Text = "6200";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 822);
            this.Controls.Add(this.tx_server_port);
            this.Controls.Add(this.tx_server_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tx_PassWord);
            this.Controls.Add(this.tx_UserName);
            this.Controls.Add(this.label_PassWord);
            this.Controls.Add(this.label_UserName);
            this.Controls.Add(this.btn_Login_Close);
            this.Controls.Add(this.btn_Login);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tx_PassWord;
        private System.Windows.Forms.TextBox tx_UserName;
        private System.Windows.Forms.Label label_PassWord;
        private System.Windows.Forms.Label label_UserName;
        private System.Windows.Forms.Button btn_Login_Close;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tx_server_ip;
        private System.Windows.Forms.TextBox tx_server_port;
    }
}