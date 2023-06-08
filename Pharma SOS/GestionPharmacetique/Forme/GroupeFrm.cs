using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class GroupeFrm : Form
    {
        public GroupeFrm()
        {
            InitializeComponent();
        }

        int id;
       void  ListeGroupe()
        {
            try
            {
                dgvProduit.Rows.Clear();
                foreach (var g in AppCode.ConnectionClass.ListeDesGroupe())
                    dgvProduit.Rows.Add(g.CodeFamille, g.Groupe);
            }
            catch { }
        }
        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if(!string.IsNullOrEmpty(txtDescription.Text))
                    {
                       if( AppCode.ConnectionClass.AjouterGroupeProduit(id,txtDescription.Text))
                        {
                            txtDescription.Text = "";
                            id = 0;
                            ListeGroupe();
                        }
                    }
                }
            }
            catch { }
        }

        private void dgvProduit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ce groupe de produit?,","Confirmation", "confirmation.png") == "1")
                {
                    AppCode.ConnectionClass.SupprimerGroupeProduit(Convert.ToInt32(dgvProduit.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    ListeGroupe();
                }
            }
            else
            {
                txtDescription.Text = dgvProduit.Rows[e.RowIndex].Cells[1].Value.ToString();
                id  =Convert.ToInt32( dgvProduit.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void GroupeFrm_Load(object sender, EventArgs e)
        {
            ListeGroupe();
        }
    }
}
