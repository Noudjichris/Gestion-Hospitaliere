using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using GestionDuneClinique.AppCode;
using System.Text;
using System.Windows.Forms;

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
            Pen pen1 = new Pen(Color.FromArgb(255, 128, 0), 3);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.BackwardDiagonal);
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
        double fraisHonoraire;
        DateTime dateAbonn;
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEntreprise.Text != "" && txtTele1.Text != "" && txtAdresse.Text != "")
            {
                if (string.IsNullOrEmpty(txtFraisHon.Text) || Double.TryParse(txtFraisHon.Text, out fraisHonoraire))
                {
                    var index = 0;
                    if (int.TryParse(textBox2.Text, out index))
                    {
                    }
                nomEntrep = txtEntreprise.Text;
                tele1 = txtTele1.Text;
                tele2 = txtTele2.Text;
                email = txtEmail.Text;
                adresse = txtAdresse.Text;
                dateAbonn = dtpAbon.Value;
                var entreprise = new Entreprise(id, nomEntrep, tele1, tele2, email, adresse,dateAbonn,fraisHonoraire,index);
                if (etat == "1")
                {
                    if (ConnectionClassClinique.AjouterUneEntreprise(entreprise))
                    {
                        DesActiverLesControles();
                        var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
                        ListeDesEntreprises(listeEntrep);
                        button1.Enabled = false;
                    }
                }
                else if (etat == "2")
                {
                     if (MonMessageBox.ShowBox("Voulez vous modifier les données de l' entreprise?", "Confirmation", "confirmation") == "1")
                     {
                         if (ConnectionClassClinique.ModifierUneEntreprise(entreprise))
                         {
                             DesActiverLesControles();
                             var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
                             ListeDesEntreprises(listeEntrep);
                             button1.Enabled = false;
                         }
                     }
                }
            }else
            {
               MonMessageBox.ShowBox("Si le frais d' honoration existe veuillez entrer un chiffre valide","Erreur","erreur.png");
            }
        }
            else
            {
                MonMessageBox.ShowBox("les champs entreprise, telephone 1 et adresse sont à remplir", "Erreur ", "erreur.png");
            }
        }

        private void EntrepriseFrm_Load(object sender, EventArgs e)
        {
            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
            ListeDesEntreprises(listeEntrep);
            Column2.Width = dgvEntrp.Width / 3;

        }

        void ListeDesEntreprises(List<Entreprise> listeEntrep)
        {  
            dgvEntrp.Rows.Clear();
            foreach (Entreprise entrep in listeEntrep)
            {
                dgvEntrp.Rows.Add(entrep.NumeroEntreprise,
                             entrep.NomEntreprise,
                             entrep.Telephone1,
                             entrep.Telephone1, 
                             entrep.Email,
                             entrep.Adresse,
                             entrep.DateAbonnement,
                             entrep.PrixHonoraire,entrep.IndexPrix
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
                dtpAbon.Value = DateTime.Parse(dgvEntrp.SelectedRows[0].Cells[6].Value.ToString());
                txtFraisHon.Text = dgvEntrp.SelectedRows[0].Cells[7].Value.ToString();
                textBox2.Text = dgvEntrp.SelectedRows[0].Cells[8].Value.ToString();
            }
            //if (txtEntreprise.Text != "" && txtTele1.Text != "" && txtAdresse.Text != "")
            //{
            //    if (string.IsNullOrEmpty(txtFraisHon.Text) || Double.TryParse(txtFraisHon.Text, out fraisHonoraire))
            //    {
            //        if (dgvEntrp.SelectedRows.Count > 0)
            //        {
            //            nomEntrep = txtEntreprise.Text;
            //            tele1 = txtTele1.Text;
            //            tele2 = txtTele2.Text;
            //            email = txtEmail.Text;
            //            adresse = txtAdresse.Text;
            //            dateAbonn = dtpAbon.Value;
            //            var entreprise = new Entreprise(id, nomEntrep, tele1, tele2, email, adresse, dateAbonn, fraisHonoraire);
            //            if (MonMessageBox.ShowBox("Voulez vous modifier les données de l' entreprise?", "Confirmation", "confirmation") == "1")
            //            {
            //                if (ConnectionClassClinique.ModifierUneEntreprise(entreprise))
            //                {
            //                    txtEntreprise.Text = "";
            //                    txtTele1.Text = "";
            //                    txtTele2.Text = "";
            //                    txtEmail.Text = "";
            //                    txtAdresse.Text = "";
            //                    dtpAbon.Value = DateTime.Now;
            //                    var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
            //                    ListeDesEntreprises(listeEntrep);
            //                }
            //            }

            //        }
            //        else
            //        {
            //            MonMessageBox.ShowBox("Veuillez selectionner les données à modifier", "Erreur ", "erreur.png");
            //        }
            //    }
            //    else
            //    {
            //        MonMessageBox.ShowBox("Si le frais d' honoration existe veuillez entrer un chiffre valide", "Erreur", "erreur.png");
            //    }
            //}
            //else
            //{
            //    MonMessageBox.ShowBox("les champs entreprise, telephone 1 et adresse sont à remplir", "Erreur ", "erreur.png");
            //}
        }

        private void dgvEntrp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEntrp.SelectedRows.Count > 0)
            {
                id =Int32.Parse( dgvEntrp.SelectedRows[0].Cells[0].Value.ToString());
                txtEntreprise.Text = dgvEntrp.SelectedRows[0].Cells[1].Value.ToString();
                txtTele1.Text = dgvEntrp.SelectedRows[0].Cells[2].Value.ToString();
                txtTele2.Text = dgvEntrp.SelectedRows[0].Cells[3].Value.ToString();
                txtEmail.Text = dgvEntrp.SelectedRows[0].Cells[4].Value.ToString();
                txtAdresse.Text = dgvEntrp.SelectedRows[0].Cells[5].Value.ToString();
                dtpAbon.Value = DateTime.Parse(dgvEntrp.SelectedRows[0].Cells[6].Value.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvEntrp.SelectedRows.Count > 0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de l' entreprise?", "Confirmation", "confirmation") == "1")
                {
                    id = Int32.Parse(dgvEntrp.SelectedRows[0].Cells[0].Value.ToString());
                    ConnectionClassClinique.SupprimerUneEntreprise(id);
                    {
                        txtEntreprise.Text = "";
                        txtTele1.Text = "";
                        txtTele2.Text = "";
                        txtEmail.Text = "";
                        txtAdresse.Text = "";
                        dtpAbon.Value = DateTime.Now;
                        var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
                        ListeDesEntreprises(listeEntrep);
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

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(textBox1.Text);
            ListeDesEntreprises(listeEntrep);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var frm = new RapportEntrepriseFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(205, 120);
            frm.ShowDialog();
        }

        Bitmap rapportBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportBitmap, 5, 20, rapportBitmap.Width, rapportBitmap.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            ActiverLesControles();
            etat = "1";
            button1.Enabled = true;
        }

        void DesActiverLesControles()
        {
            textBox2.Enabled = false;
            txtEntreprise.Enabled = false;
            txtTele1.Enabled = false;
            txtTele2.Enabled = false;
            txtEmail.Enabled = false;
            txtFraisHon.Enabled = false;
            txtAdresse.Enabled = false;;
            dtpAbon.Enabled = false;
            txtEntreprise.Text = "";
            txtTele1.Text = "";
            txtTele2.Text = "";
            txtEmail.Text = "";
            txtFraisHon.Text = "";
            txtAdresse.Text = "";
            textBox2.Text = "";
            dtpAbon.Value = DateTime.Now;
        }

        void ActiverLesControles()
        {
            textBox2.Enabled = true ;
            txtEntreprise.Enabled = true;
            txtTele1.Enabled = true;
            txtTele2.Enabled = true;
            txtEmail.Enabled = true;
            txtFraisHon.Enabled = true;
            txtAdresse.Enabled = true; ;
            dtpAbon.Enabled = true;
            txtEntreprise.Text = "";
            txtTele1.Text = "";
            txtTele2.Text = "";
            txtEmail.Text = "";
            txtFraisHon.Text = "";
            txtAdresse.Text = "";
            dtpAbon.Value = DateTime.Now;
        }
    
    }
}
