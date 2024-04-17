namespace TDRv
{
    partial class LoginConfig
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
            this.tx_PassWord = new System.Windows.Forms.TextBox();
            this.tx_UserName = new System.Windows.Forms.TextBox();
            this.label_PassWord = new System.Windows.Forms.Label();
            this.label_UserName = new System.Windows.Forms.Label();
            this.btn_Login_Close = new System.Windows.Forms.Button();
            this.btn_Login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tx_PassWord
            // 
            this.tx_PassWord.Location = new System.Drawing.Point(72, 42);
            this.tx_PassWord.Name = "tx_PassWord";
            this.tx_PassWord.PasswordChar = '*';
            this.tx_PassWord.Size = new System.Drawing.Size(173, 21);
            this.tx_PassWord.TabIndex = 15;
            this.tx_PassWord.Text = "1234";
            // 
            // tx_UserName
            // 
            this.tx_UserName.Location = new System.Drawing.Point(72, 15);
            this.tx_UserName.Name = "tx_UserName";
            this.tx_UserName.Size = new System.Drawing.Size(173, 21);
            this.tx_UserName.TabIndex = 14;
            this.tx_UserName.Text = "user";
            // 
            // label_PassWord
            // 
            this.label_PassWord.AutoSize = true;
            this.label_PassWord.Location = new System.Drawing.Point(13, 50);
            this.label_PassWord.Name = "label_PassWord";
            this.label_PassWord.Size = new System.Drawing.Size(29, 12);
            this.label_PassWord.TabIndex = 19;
            this.label_PassWord.Text = "密码";
            // 
            // label_UserName
            // 
            this.label_UserName.AutoSize = true;
            this.label_UserName.Location = new System.Drawing.Point(13, 18);
            this.label_UserName.Name = "label_UserName";
            this.label_UserName.Size = new System.Drawing.Size(53, 12);
            this.label_UserName.TabIndex = 18;
            this.label_UserName.Text = "用户名：";
            // 
            // btn_Login_Close
            // 
            this.btn_Login_Close.Location = new System.Drawing.Point(72, 79);
            this.btn_Login_Close.Name = "btn_Login_Close";
            this.btn_Login_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Login_Close.TabIndex = 17;
            this.btn_Login_Close.Text = "关闭";
            this.btn_Login_Close.UseVisualStyleBackColor = true;
            this.btn_Login_Close.Click += new System.EventHandler(this.btn_Login_Close_Click);
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(170, 79);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 16;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // LoginConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 115);
            this.Controls.Add(this.tx_PassWord);
            this.Controls.Add(this.tx_UserName);
            this.Controls.Add(this.label_PassWord);
            this.Controls.Add(this.label_UserName);
            this.Controls.Add(this.btn_Login_Close);
            this.Controls.Add(this.btn_Login);
            this.Name = "LoginConfig";
            this.Text = "LoginConfig";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tx_PassWord;
        private System.Windows.Forms.TextBox tx_UserName;
        private System.Windows.Forms.Label label_PassWord;
        private System.Windows.Forms.Label label_UserName;
        private System.Windows.Forms.Button btn_Login_Close;
        private System.Windows.Forms.Button btn_Login;
    }
}