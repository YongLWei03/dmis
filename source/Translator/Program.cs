using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace Translator
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

            //判断access数据文件是否存在
            if (!File.Exists("...\\data\\dbs1.mdb"))
            {
                MessageBox.Show("数据库文件不存在，无法运行!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Application.Run(new Translator());
        }
    }
}