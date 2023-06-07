using System;
using System.Windows.Forms;
using GestionPharmacetique.Forme;

namespace GestionPharmacetique
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
                Application.Run(new SplashScreen());
            //}
            //else
            //{GestionDuneClinique.Formes.RapportEntrepriseFrm
            //    MessageBox.Show("Le delai d'utilisation est arrive a terme. Veuillez contacter l'administrateur");
            //}
        }
    }
}
