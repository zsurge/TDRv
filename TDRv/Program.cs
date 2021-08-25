using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TDRv
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            /// 界面转换
            //LoginForm loginForm = new LoginForm();
            //loginForm.ShowDialog();

            //if (loginForm.DialogResult == DialogResult.OK)
            //{
            //    loginForm.Dispose();
            //    Application.Run(new Form1());
            //}
            //else if (loginForm.DialogResult == DialogResult.Cancel)
            //{
            //    loginForm.Dispose();
            //    return;
            //}
        }
    }
}
