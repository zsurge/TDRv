using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TDRv
{
    public partial class LoginConfig : Form
    {

        public event EventHandler<LoginEventArgs> LoginSuccess;

        public LoginConfig()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_Login_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            string stohbuff = string.Empty;


            if (tx_UserName.Text.Equals("") || tx_PassWord.Text.Equals(""))//用户名或密码为空
            {
                MessageBox.Show("服务器地址或用户名密码不能为空");
            }
            else//用户名或密码不为空
            {
                if (tx_UserName.Text == "user" && tx_PassWord.Text == "1234")
                {
                    // 跳转参数设置界面
                    OnLoginSuccess(tx_UserName.Text);
                    this.Close();
                    

                }
                else if (tx_UserName.Text == "admin" && tx_PassWord.Text =="tsj")
                {
                    // 跳转参数设置
                    OnLoginSuccess(tx_UserName.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误，请重新输入");
                    tx_PassWord.Text = string.Empty;
                    tx_PassWord.Focus();
                }
            }
        }

        protected virtual void OnLoginSuccess(string username)
        {
            LoginSuccess?.Invoke(this, new LoginEventArgs(username));
        }

        public class LoginEventArgs : EventArgs
        {
            public string Username { get; }

            public LoginEventArgs(string username)
            {
                Username = username;
            }
        }


    }//end form
}
