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
        double fraisHonoraire;
        DateTime dateAbonn;
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEntreprise.Text != "" && txtTele1.Text != "" && txtAdresse.Text != "")
            {
                if (string.IsNullOrEmpty(txtFraisHon.Text) || Double.TryParse(txtFraisHon.Text, out fraisHonoraire))
                {
                nomEntrep = txtEntreprise.Text;
                tele1 = txtTele1.Text;
                tele2 = txtTele2.Text;
                email = txtEmail.Text;
                adresse = txtAdresse.Text;
                dateAbonn = dtpAbon.Value;
                var entreprise = new Entreprise(id, nomEntrep, tele1, tele2, email, adresse,dateAbonn,fraisHonoraire);
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

            var listePaiement = ConnectionClassClinique.ListeDesPaiements();
            ListePaiement(listePaiement);

            radioButton2.Checked = true;

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
                             entrep.PrixHonoraire
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(dateTimePicker1.Value.Date);
            ListeDesEntreprises(listeEntrep);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var frm = new RapportEntrepriseFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(205, 120);
            frm.ShowDialog();
        }

        void ListePaiement(List<Entreprise> listeEntreprise)
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (Entreprise entreprise in listeEntreprise)
                {
                    dataGridView1.Rows.Add(
                        entreprise.IdPaiement,
                        entreprise.NumeroEntreprise,
                        entreprise.NomEntreprise,
                        entreprise.DateAbonnement,
                        entreprise.Montant,
                        entreprise.ModePaiement,
                        entreprise.Cheque);
                }
            }
            catch(Exception ex)
            {
                MonMessageBox.ShowBox("Liste paiement",ex);
            }
        }
    
        private void button8_Click(object sender, EventArgs e)
        {
            double montant;
            if (dgvEntrp.SelectedRows.Count > 0)
            {
                if (Double.TryParse(txtMontant.Text, out montant))
                {
                    var idEntrep = Int32.Parse(dgvEntrp.SelectedRows[0].Cells[0].Value.ToString());
                    var entrep = dgvEntrp.SelectedRows[0].Cells[1].Value.ToString();
                    var id =0;
                    var numCheque = textBox2.Text;
                    var entreprise = new Entreprise(id, idEntrep, dateTimePicker2.Value, montant, entrep, modePaiement, numCheque);
                    if(ConnectionClassClinique.EnregistrerPaiement(entreprise))
                    {
                          var listePaiement = ConnectionClassClinique.ListeDesPaiements();
                          ListePaiement(listePaiement);
                          txtMontant.Text = "";
                          textBox2.Text = "";
                          dateTimePicker2.Value = DateTime.Now;
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données de l'entreprise puis continuer.", "Erreur", "erreur.png");
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez selectionner les données de l'entreprise puis continuer.", "Erreur", "erreur.png");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            double montant;
            
                if (Double.TryParse(txtMontant.Text, out montant))
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        var idEntrep = Int32.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                        var entrep = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                        var id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        var numCheque = textBox2.Text;
                        var entreprise = new Entreprise(id, idEntrep, dateTimePicker2.Value, montant, entrep, modePaiement, numCheque);
                        if (MonMessageBox.ShowBox("Voulez vous modifier les données de paiement?", "Confirmation", "confirmation") == "1")
                        {
                            if (ConnectionClassClinique.ModifierPaiement(entreprise))
                            {
                                txtMontant.Text = "";
                                dateTimePicker2.Value = DateTime.Now;
                                textBox2.Text = "";
                                var listePaiement = ConnectionClassClinique.ListeDesPaiements();
                                ListePaiement(listePaiement);
                            }
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner les données de la table paiement à modifer puis continuer.", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données de l'entreprise puis continuer.", "Erreur", "erreur.png");
                }
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                var id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de paiement?", "Confirmation", "confirmation") == "1")
                {
                    ConnectionClassClinique.SupprimerUnPaiement(id);
                    txtMontant.Text = "";
                        dateTimePicker2.Value = DateTime.Now;
                        var listePaiement = ConnectionClassClinique.ListeDesPaiements();
                        ListePaiement(listePaiement);                        
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez selectionner les données de la table paiement à modifer puis continuer.", "Erreur", "erreur.png");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtMontant.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                dateTimePicker2.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString() == "Par chèques")
                {
                    radioButton1.Checked = true;
                }
                else if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString() == "En espèces")
                {
                    radioButton2.Checked = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvEntrp.SelectedRows.Count > 0)
            {
                var entrep = dgvEntrp.SelectedRows[0].Cells[1].Value.ToString();

                var listePaie = ConnectionClassClinique.ListeDesPaiements(entrep);
                ListePaiement(listePaie);                
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var listePaiement = ConnectionClassClinique.ListeDesPaiements();
            ListePaiement(listePaiement);
        }

       string  modePaiement;
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            modePaiement = "En espèces";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            modePaiement = "Par chèques";
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

        //impimer avec apercu
        private void button11_Click(object sender, EventArgs e)
        {
            var montant = 0.0;
            if (Double.TryParse(txtMontant.Text, out montant))
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var idEntrep = Int32.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    var entrep = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    var id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    var numCheque = textBox2.Text;
                    var entrepr = new Entreprise(id, idEntrep, dateTimePicker2.Value, montant, entrep, modePaiement, numCheque);
                    var listeEntreprise = ConnectionClassClinique.ListeDesEntreprises(entrep);
                    var entreprise = new Entreprise(listeEntreprise[0].NumeroEntreprise, listeEntreprise[0].NomEntreprise, listeEntreprise[0].Telephone1,
                        listeEntreprise[0].Telephone2, listeEntreprise[0].Email, listeEntreprise[0].Adresse, listeEntreprise[0].DateAbonnement, listeEntreprise[0].PrixHonoraire);

                    rapportBitmap = Impression.FactureOfficielleDuneEntreprise(entrepr, entreprise);
                    printPreviewDialog1.ShowDialog();
                     
                    //if (printDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    //    printDocument1.Print();
                    //}
                }
            }
           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
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
            txtFraisHon.Enabled = false;
            txtAdresse.Enabled = false;;
            dtpAbon.Enabled = false;
            txtEntreprise.Text = "";
            txtTele1.Text = "";
            txtTele2.Text = "";
            txtEmail.Text = "";
            txtFraisHon.Text = "";
            txtAdresse.Text = "";
            dtpAbon.Value = DateTime.Now;
        }

        void ActiverLesControles()
        {
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
