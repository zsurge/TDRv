using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDRv
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (tx_UserName.Text.Equals("") || tx_PassWord.Text.Equals(""))//用户名或密码为空
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            else//用户名或密码不为空
            {
                if (String.Compare(tx_UserName.Text, "tsj") == 0 && String.Compare(tx_PassWord.Text, "123456") == 0)
                {
                    //写日志，用户名及登录时间

                    // 跳转主界面
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误");
                }

            }
        }

        private void btn_Login_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
