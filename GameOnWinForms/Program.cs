﻿using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading;
using System.Windows.Forms;

namespace GameOnWinForms
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new Form1();

            Application.Run(mainForm);
        }

        

    }
}