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
    public partial class GroupeFrm : Form
    {
        public GroupeFrm()
        {
            InitializeComponent();
        }

        private void GroupeFrm_Load(object sender, EventArgs e)
        {
            try
            {
                ListeGroupe();
            }
            catch { }
        }

        void ListeGroupe()
        {
            dgvConsult.Rows.Clear();
            foreach(var g in AppCode.ConnectionClassClinique.ListeDesGroupes())
            {
                dgvConsult.Rows.Add(g.NumeroGroupe,g.Groupe,g.Libelle);
            }
        }
        int idGr;
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGroupe.Text))
            {
                var gr = new AppCode.Analyse();
                gr.Groupe = txtGroupe.Text;
                if (string.IsNullOrEmpty(txtLibelle.Text))
                {
                    gr.Libelle = "";
                }
                else
                {
                    gr.Libelle = txtLibelle.Text;
                }
                gr.NumeroGroupe = idGr;
                if (AppCode.ConnectionClassClinique.EnregistrerGroupe(gr))
                {
                    txtLibelle.Text = ""; txtGroupe.Text = "";
                    ListeGroupe();
                    idGr = 0;
                }
            }
        }

        private void dgvConsult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                AppCode.ConnectionClassClinique.SupprimerUnGroupe(Convert.ToInt32(dgvConsult.Rows[e.RowIndex].Cells[0].Value.ToString()));
                {
                    ListeGroupe();
                }
            }
            else if(e.ColumnIndex==3)
            {
                idGr = Convert.ToInt32(dgvConsult.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtGroupe.Text = dgvConsult.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtLibelle.Text = dgvConsult.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
    }
}
