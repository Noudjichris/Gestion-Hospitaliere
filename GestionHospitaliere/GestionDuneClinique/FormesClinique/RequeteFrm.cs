using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.FormesClinique
{
    public partial class RequeteFrm : Form
    {
        public RequeteFrm()
        {
            InitializeComponent();
        }

        private void txtNom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var requete = txtNom.Text;
                if (requete.ToUpper().Contains("SELECT"))
                {
                    var dt =AppCode.ConnectionClassClinique.ExecuterUneRequeteSelect(requete);
                    dataGridView3.DataSource = dt;
                    //dataGridView3.DataBindings;
                }
                else
                {
                    AppCode.ConnectionClassClinique.ExecuterUneRequete(requete);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'opération a échoué", "Erreur", "Erreur");
            }
        }
    }
}
