using Doamin.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using FarsiLibrary;

namespace WindowsFormsApp1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Thread.CurrentThread.CurrentUICulture = FALocalizeManager.FarsiCulture;


            CultureInfo persianCulture = new CultureInfo("fa-IR", false);
            Thread.CurrentThread.CurrentUICulture = persianCulture; 
           //Thread.CurrentThread.de = persianCulture;


            Application.Run(new MainForm());
        }
    }

}
