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
    public partial class ListeClientFrm : Form
    {
        public ListeClientFrm()
        {
            InitializeComponent();
        }

        private void ListeClientFrm_Load(object sender, EventArgs e)
        {

            var dtEntrep =AppCode. ConnectionClassClinique.ListeDesEntreprises();
            comboBox2.Items.Add("");
            foreach (DataRow entrep in dtEntrep.Rows)
            {comboBox2.Items.Add(entrep.ItemArray[1].ToString().ToUpper());
            }
            button2.Location = new Point(Width - 45, 3);
                   ListeClient();
        }
        void ListeClient()
        {
            try
            {
                dgvProduit.Rows.Clear();
                var liste =from c in AppCode.ConnectionClass.ListeDesClient()
                               where c.NomClient.StartsWith(txtRecherche.Text, StringComparison.CurrentCultureIgnoreCase)
                               select c;
                foreach (AppCode.Client client in liste)
                {
                    dgvProduit.Rows.Add(client.Id,client.NomClient,client.Telephone,client.Entreprise,client.Matricule ,client.SousCouvert);
                }

            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            if (txtRecherche.Text.Length >= 3)
            {
                ListeClient();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dgvProduit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var indexCurrentRow = dgvProduit.CurrentRow;
                var client = new AppCode.Client();
                client.Id = Int32.Parse(dgvProduit.CurrentRow.Cells[0].Value.ToString());
                client.NomClient = dgvProduit.CurrentRow.Cells[1].Value.ToString();
                client.Telephone = dgvProduit.CurrentRow.Cells[2].Value.ToString();
                client.Entreprise = dgvProduit.CurrentRow.Cells[3].Value.ToString();
                client.Matricule = dgvProduit.CurrentRow.Cells[4].Value.ToString();
                client.SousCouvert = dgvProduit.CurrentRow.Cells[5].Value.ToString();
                AppCode.ConnectionClass.ModifierClient(client);
            }
            catch { }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvProduit.Rows.Clear();
                var liste = from c in AppCode.ConnectionClass.ListeDesClient()
                            where c.Entreprise.StartsWith(comboBox2.Text, StringComparison.CurrentCultureIgnoreCase)
                            select c;
                foreach (AppCode.Client client in liste)
                {
                    dgvProduit.Rows.Add(client.Id, client.NomClient, client.Telephone, client.Entreprise, client.Matricule, client.SousCouvert);
                }

            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           var frm=new  GestionDuneClinique.Formes.EntrepriseFrm();
           frm.Size = new Size(Width, Height);
           frm.Location = new Point(Location.X, Location.Y);
           frm.ShowDialog();
        }
    }
}
