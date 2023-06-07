using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using GestionPharmacetique.AppCode;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique;
using System.Linq;

namespace GestionDuneClinique.Formes
{
    public partial class EntrepriseFrm : Form
    {
        public EntrepriseFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void EntrepriseFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Blue, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        string nomEntrep, tele1, tele2, email, adresse, etat;
        int id;
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEntreprise.Text != "" && txtTele1.Text != "" && txtAdresse.Text != "")
            {
                
                nomEntrep = txtEntreprise.Text;
                tele1 = txtTele1.Text;
                tele2 = txtTele2.Text;
                email = txtEmail.Text;
                adresse = txtAdresse.Text;
                var entreprise = new Entreprise(id, nomEntrep, tele1, tele2, email, adresse);
                if (etat == "1")
                {
                    if (ConnectionClass.AjouterUneEntreprise(entreprise))
                    {
                        DesActiverLesControles();
                        var listeEntrep = ConnectionClass.ListeDesEntreprises();
                        ListeDesEntreprises();
                        button1.Enabled = false;
                    }
                }
                else if (etat == "2")
                {
                     if (MonMessageBox.ShowBox("Voulez vous modifier les données de l' entreprise?", "Confirmation", "confirmation") == "1")
                     {
                         
                         if (ConnectionClass.ModifierUneEntreprise(entreprise, nomEntrep))
                         {
                             DesActiverLesControles();
                             ListeDesEntreprises();
                             button1.Enabled = false;
                         }
                     }
                }
          
        }
            else
            {
                MonMessageBox.ShowBox("les champs entreprise, telephone 1 et adresse sont à remplir", "Erreur ", "erreur.png");
            }
        }

        private void EntrepriseFrm_Load(object sender, EventArgs e)
        {
            ListeDesEntreprises();
            button3.Location = new Point(Width - 43, 3);
        }

        void ListeDesEntreprises()
        {  
            dgvEntrp.Rows.Clear();
            var list = ConnectionClass.ListeDesEntreprises();
            foreach (Entreprise entrep in list)
            {
                dgvEntrp.Rows.Add(entrep.NumeroEntreprise,
                             entrep.NomEntreprise,
                             entrep.Telephone1,
                             entrep.Telephone1, 
                             entrep.Email,
                             entrep.Adresse
                             );
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvEntrp.SelectedRows.Count > 0)
            {
                ActiverLesControles();
                etat = "2";
                button1.Enabled = true;
                id = Int32.Parse(dgvEntrp.SelectedRows[0].Cells[0].Value.ToString());
                txtEntreprise.Text = dgvEntrp.SelectedRows[0].Cells[1].Value.ToString();
                txtTele1.Text = dgvEntrp.SelectedRows[0].Cells[2].Value.ToString();
                txtTele2.Text = dgvEntrp.SelectedRows[0].Cells[3].Value.ToString();
                txtEmail.Text = dgvEntrp.SelectedRows[0].Cells[4].Value.ToString();
                txtAdresse.Text = dgvEntrp.SelectedRows[0].Cells[5].Value.ToString();
                nomEntrep = dgvEntrp.SelectedRows[0].Cells[1].Value.ToString();
            }

        }

        private void dgvEntrp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEntrp.SelectedRows.Count > 0)
            {
                txtNomEntrep.Text = dgvEntrp.SelectedRows[0].Cells[1].Value.ToString();
                id =Int32.Parse( dgvEntrp.SelectedRows[0].Cells[0].Value.ToString());
                txtEntreprise.Text = dgvEntrp.SelectedRows[0].Cells[1].Value.ToString();
                txtTele1.Text = dgvEntrp.SelectedRows[0].Cells[2].Value.ToString();
                txtTele2.Text = dgvEntrp.SelectedRows[0].Cells[3].Value.ToString();
                txtEmail.Text = dgvEntrp.SelectedRows[0].Cells[4].Value.ToString();
                txtAdresse.Text = dgvEntrp.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvEntrp.SelectedRows.Count > 0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de l' entreprise?", "Confirmation", "confirmation") == "1")
                {
                    id = Int32.Parse(dgvEntrp.SelectedRows[0].Cells[0].Value.ToString());
                    var entrep = dgvEntrp.SelectedRows[0].Cells[1].Value.ToString();
                    ConnectionClass.SupprimerUneEntreprise(id,entrep);
                    {
                        txtEntreprise.Text = "";
                        txtTele1.Text = "";
                        txtTele2.Text = "";
                        txtEmail.Text = "";
                        txtAdresse.Text = "";
                        var listeEntrep = ConnectionClass.ListeDesEntreprises();
                        ListeDesEntreprises();
                    }
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez selectionner les données à modifier", "Erreur ", "erreur.png");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var frm = new GestionPharmacetique.Forme.RapportDesEntreprises();
            frm.ShowDialog();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            ActiverLesControles();
            etat = "1";
            button1.Enabled = true;
        }

        void DesActiverLesControles()
        {
            txtEntreprise.Enabled = false;
            txtTele1.Enabled = false;
            txtTele2.Enabled = false;
            txtEmail.Enabled = false;
            txtAdresse.Enabled = false;
            txtEntreprise.Text = "";
            txtTele1.Text = "";
            txtTele2.Text = "";
            txtEmail.Text = "";
            txtAdresse.Text = "";
        }

        void ActiverLesControles()
        {
            txtEntreprise.Enabled = true;
            txtTele1.Enabled = true;
            txtTele2.Enabled = true;
            txtEmail.Enabled = true;
            txtAdresse.Enabled = true; 
            txtEntreprise.Text = "";
            txtTele1.Text = "";
            txtTele2.Text = "";
            txtEmail.Text = "";
            txtAdresse.Text = "";
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNomCLient.Text ) && !string.IsNullOrWhiteSpace(txtNomEntrep.Text))
                {
                    //var client = new Client(0,txtNomCLient.Text, txtTeleEmploye.Text, txtNomEntrep.Text,txtMatricule.Text, comboBox2.Text);
                    //if (ConnectionClass.AjouterClient(client))
                    //{
                    //    dgvEntrp_DoubleClick(null, null);
                    //    txtTeleEmploye.Text = "";
                    //    txtNomCLient.Text = "";
                    //    txtMatricule.Text = "";
                    //    comboBox2.Text = "";
                    //}
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer le nom du client et selectionner l'entreprise sur la liste", "Erreur", "erreur.png");
                }
            }
            catch (Exception)
            {
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de ce client?", "Confirmation", "confirmtion.png") == "1")
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ConnectionClass.SupprimerClient(id);
                    dgvEntrp_DoubleClick(null, null);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    txtNomCLient.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    txtTeleEmploye.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    txtNomEntrep.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    txtMatricule.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                    comboBox2.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNomCLient.Text) && !string.IsNullOrWhiteSpace(txtNomEntrep.Text))
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    var client = new Client();
                    if (MonMessageBox.ShowBox("Voulez vous modifier les données de ce client?", "Confirmation", "confirmtion.png") == "1")
                    {
                        if (ConnectionClass.ModifierClient(client))
                        {
                            dgvEntrp_DoubleClick(null, null);
                            txtTeleEmploye.Text = "";
                            txtNomCLient.Text = ""; txtMatricule.Text = "";
                            comboBox2.Text = "";
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer le nom du client et selectionner l'entreprise sur la liste", "Erreur", "erreur.png");
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvEntrp_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                if (!string.IsNullOrWhiteSpace(txtNomEntrep.Text))
                {
                    var listeClient = ConnectionClass.ListeDesClient();
                    var list = from cl in listeClient
                               where cl.Entreprise == txtNomEntrep.Text
                               orderby cl.NomClient
                               select cl;
                    foreach (var q in list)
                    {
                        dataGridView1.Rows.Add
                            (
                            q.Id, q.NomClient, q.Telephone, q.Entreprise,q.Matricule,q.SousCouvert
                             );
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvEntrp_Click(object sender, EventArgs e)
        {
            dgvEntrp_CellContentClick(null, null);
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox2.Items.Clear();
                var liste = from c in ConnectionClass.ListeDesClient()
                            where c.NomClient.StartsWith(comboBox2.Text, StringComparison.CurrentCultureIgnoreCase)
                            where c.Entreprise.StartsWith(txtNomEntrep.Text , StringComparison.CurrentCultureIgnoreCase)
                            select c.NomClient;
                foreach (var c in liste)
                    comboBox2.Items.Add(c);
                comboBox2.Text = "";
                comboBox2.DroppedDown = true;

            }
        }
    
    }
}
