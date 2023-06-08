using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GestionDuneClinique
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //if (DateTime.Now.Date < DateTime.Parse("05/05/2017"))
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GestionAcademique.Form1());
            //}
        }
    }
}
