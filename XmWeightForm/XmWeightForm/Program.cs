﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XmWeightForm.SystemManage;

namespace XmWeightForm
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
            //Application.Run(new SysManageForm());
            //Application.Run(new MainForm());
            //Application.Run(new SysManageNewForm());
            LoginForm fl = new LoginForm();
            fl.ShowDialog();
            if (fl.DialogResult == DialogResult.OK)
            {
                if (fl.IsAdmin)
                {
                    Application.Run(new SysManageNewForm());
                }
                else
                {
                    Application.Run(new MainForm());
                }
                
            }
            else
            {
                return;
            }
          
        }
    }
}
