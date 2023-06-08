using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.FormesClinique
{
    public partial class ActeDeNaissanceFrm : Form
    {
        public ActeDeNaissanceFrm()
        {
            InitializeComponent();
        }

        private void ActeDeNaissanceFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
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
        public static int idPatiente;
        public static string nomPatiente, btnClick, state="2";
        public static double fraisCertif;
        private static ActeDeNaissanceFrm frmActe;
        public static string ShowBox()
        {
            try
            {
                frmActe = new ActeDeNaissanceFrm();
                frmActe.ShowDialog();
                return btnClick;
            }
            catch { return btnClick; }
        }
        private void ActeDeNaissanceFrm_Load(object sender, EventArgs e)
        {
            button1.Location = new Point(Width - 32, 3);
            lblNbre.Location = new Point(Width - 250, 8);
            Column4.Width = 50;
            clSexe.Width = 50;
            cmbNumeroEmploye.Items.Add("");
            var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
            var list = from p in listeEmploye
                       where !p.Titre.ToUpper().Contains("CAISS")
                       select p.NomEmployee;

            foreach (var empl in list)
            {
                cmbNumeroEmploye.Items.Add(empl.ToUpper());
            }

            for (var i = 1; i <= 59; i++)
            {
                dUDMinutesEnfant.Items.Add(i);
            } 
            for (var i = 1; i <= 23; i++)
            {
                dUDHeureEnfant.Items.Add(i);
            }
            dUDDateMer.Text = dUDDatePere.Text = DateTime.Now.Day.ToString();
            dUDMoisMere.Text = dUDMoisPere.Text = DateTime.Now.Month.ToString();
            txtAnneeMere.Text = txtAnneePere.Text = DateTime.Now.Year.ToString();
            ListeCertificat();
            //timer1.Start();
            //clMere.Width = dgvCertificat.Width * 3 / 5;
            //clBebe.Width = dgvCertificat.Width * 3 / 5;
            //clSexe.Width = dgvCertificat.Width/ 7;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dUDHeureEnfant.Text = DateTime.Now.Hour.ToString();
            dUDMinutesEnfant.Text = DateTime.Now.Minute.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        int id, idPatient;
        float poids; string idEmploye;
        CertificatNaissance CreerUnCertificat()
        {
            int annee1, jour1, mois1, annee2, jour2, mois2, heure,minute;
            if (!string.IsNullOrEmpty(txtNomBebe.Text))
            {
                if (!string.IsNullOrEmpty(txtLieuNaissance.Text))
                {
                    if (Int32.TryParse(dUDDateMer.Text, out jour1) && Int32.TryParse(dUDMoisMere.Text, out mois1)
                        && Int32.TryParse(dUDDatePere.Text, out jour2) && Int32.TryParse(dUDMoisPere.Text, out mois2)
                        && Int32.TryParse(txtAnneeMere.Text, out annee1) && Int32.TryParse(txtAnneePere.Text, out annee2)
                        && Int32.TryParse(dUDHeureEnfant.Text, out heure) && Int32.TryParse(dUDMinutesEnfant.Text, out minute))
                    {
                        if (!string.IsNullOrEmpty(txtNomEpoux.Text))
                        {
                            if (float.TryParse(txtPoidsBebe.Text, out poids))
                            {
                                if (chkFemelle.Checked || chkMale.Checked)
                                {
                                    if (dataGridView2.SelectedRows.Count <= 0)
                                    {
                                        MonMessageBox.ShowBox("Veuillez selecteionner le nom de la mère sur la liste", "Erreur", "erreur.png");
                                        return null;
                                    }
                                    if (!chkFemelle.Checked && !chkMale.Checked)

                                    {
                                        MonMessageBox.ShowBox("Veuillez cocher pour le sexe", "Erreur", "Erreur.png");
                                        return null;
                                    }
                                    if (string.IsNullOrEmpty(cmbNumeroEmploye.Text))
                                    {
                                        MonMessageBox.ShowBox("Veuillez selectionner le nom du docteur sur la liste", "Erreur", "erreur.png");
                                        return null;
                                    }
                                    var certificat = new CertificatNaissance();
                                    certificat.IDPatient = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                                    certificat.NumeroEmplye = ConnectionClassClinique.ListeDesEmployees("nom_empl", cmbNumeroEmploye.Text)[0].NumMatricule ;                                    
                                        certificat.NomEnfant = txtNomBebe.Text ;
                                    certificat.LieuNaissancePere = txtDomicilePere.Text;
                                    certificat.LieuNaissanceMere = txtLieuNaissanceMere.Text;
                                    certificat.LieuNaissanceEnfant = txtLieuNaissance.Text;
                                    certificat.NomPere = txtNomEpoux.Text;
                                    certificat.DateNaissanceEnfant = DateTime.Parse(dtpNaissanceEnfant.Value.ToShortDateString() + " " + dUDHeureEnfant.Text + ":" + dUDMinutesEnfant.Text + ":" + "00");
                                    certificat.DateNaissancePere = DateTime.Parse(dUDDatePere.Text + "-" + dUDMoisPere.Text + "-" + txtAnneePere.Text);
                                    certificat.DateNaissanceMere = DateTime.Parse(dUDDateMer.Text + "-" + dUDMoisPere.Text + "-" + txtAnneeMere.Text);
                                    certificat.Poids = poids;
                                    certificat.ProfessionPere = txtProfessionPere.Text;
                                    certificat.ProfesssionMere = txtProfessionMere.Text;
                                    certificat.LieuNaissanceMere = txtLieuNaissanceMere.Text;
                                    certificat.LieuNaissancePere = txtDomicilePere.Text;
                                    certificat.ID = id;
                                    if (chkMale.Checked)
                                    {
                                        certificat.Sexe = "M";
                                    }
                                    else if (chkFemelle.Checked)
                                    {
                                        certificat.Sexe = "F";
                                    }

                                    return certificat;

                                }
                                else
                                {
                                    MonMessageBox.ShowBox("Veuillez cocher pour le sexe du bébé.", "Erreur", "erreur.png");
                                    return null;
                                }
                            }
                            else
                            {
                                MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le poids du bébé.", "Erreur", "erreur.png");
                                return null;
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez entrer le nom du père du bébé.", "Erreur", "erreur.png");
                            return null;
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer des chiffres valides pour les jours, mois et années.", "Erreur", "erreur.png");
                        return null;
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer le lieu de naissance.", "Erreur", "erreur.png");
                    return null;
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez entrer le nom et prenom du bébé.", "Erreur", "erreur.png");
                return null;
            }
        }

        void ViderLesChamps()
        {
            for (var i = 1; i <= 59; i++)
            {
                dUDHeureEnfant.Items.Add(i);
                dUDMinutesEnfant.Items.Add(i);
            }
            dUDDateMer.Text = dUDDatePere.Text = DateTime.Now.Day.ToString();
            dUDMoisMere.Text = dUDMoisPere.Text = DateTime.Now.Month.ToString();
            txtAnneeMere.Text = txtAnneePere.Text = DateTime.Now.Year.ToString();
            timer1.Start();
          txtRecherche.Text= "";
            txtLieuNaissanceMere.Text = "";
            txtDomicilePere.Text = "";
            txtNomBebe.Text = "";
            txtNomEpoux.Text = "";
            txtPoidsBebe.Text = "";
            txtLieuNaissance.Text = "";
            txtProfessionMere.Text = "";
            txtProfessionPere.Text = "";
            cmbNumeroEmploye.Text = "";
            dataGridView2.Rows.Clear();
            txtRecherche.Text = "";
            id = 0;
        }
     
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var certificat = CreerUnCertificat();
                if (certificat != null)
                {
                    if (ConnectionClassClinique.AjouterUnCertificatNaissance(certificat))
                    {
                        ListeCertificat();
                        ViderLesChamps();
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrer un certficat", ex);
            }
        }



        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }
        Bitmap document;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 10, 10, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var certificat =new  CertificatNaissance();
                if(dgvCertificat.SelectedRows.Count > 0)
                {
                    textBox1.Text = "";
                    id = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[0].Value.ToString());
                    var liste = ConnectionClassClinique.ListeDesCertificatDeNaissance().Where(c => c.ID == id);
                    foreach (var c in liste)
                    {
                        certificat.Sexe = c.Sexe;
                        certificat.ProfessionMere = ConnectionClassClinique.ListeDesPatients(c.IDPatient)[0].Fonction;
                        textBox1.Text = c.IDPatient.ToString();
                        certificat.DateNaissanceMere = c.DateNaissanceMere;
                        certificat.DateNaissancePere = c.DateNaissancePere;
                        certificat.LieuNaissancePere = c.LieuNaissancePere;
                        certificat.LieuNaissanceEnfant = c.LieuNaissanceEnfant;
                        certificat.DateNaissanceEnfant = c.DateNaissanceEnfant;
                        certificat.LieuNaissanceMere= c.LieuNaissanceMere;
                        certificat.NomEnfant = c.NomEnfant;
                        certificat.NomPere = c.NomPere;
                        certificat.Poids = c.Poids;
                        certificat.ProfessionMere= c.ProfessionMere;
                        certificat.ProfessionPere= c.ProfessionPere;
                        certificat.NumeroEmplye = c.NumeroEmplye;
                        certificat.IDPatient = c.IDPatient;
                        //certificat.nomEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", c.NumeroEmplye)[0].NomEmployee;
                    }

                    document = Impression.CertificatDeNaissance(certificat);
                    if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Apercu", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCertificat.Rows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous vous supprimer les données du certificat", "Confirmation", "confirmation.png") == "1")
                    {
                        if (ConnectionClassClinique.SupprimerUnCertficatDeNaissance(Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[0].Value.ToString())))
                        {
                            ListeCertificat();
                            ViderLesChamps();
                        }
                    }
                }
                
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données à supprimer", "Erreur", "erreur.png");
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrer certificat", ex);
            }
        
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
               
        }

        void ListeCertificat()
        {
            try
            {
                dgvCertificat.Rows.Clear();
                var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeNaissance();

                   dgvCertificat.Rows.Clear();
                    foreach (var p in listeCertificat)
                    {
                    var nomMere = ConnectionClassClinique.ListeDesPatients(p.IDPatient)[0].Nom+" "+ConnectionClassClinique.ListeDesPatients(p.IDPatient)[0].Prenom;
                    var nomEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", p.NumeroEmplye)[0].NomEmployee;
                    dgvCertificat.Rows.Add(p.ID, nomEmploye, p.NomEnfant, p.DateNaissanceEnfant, p.LieuNaissanceEnfant, p.Sexe, p.Poids,p.IDPatient,nomMere,p.NomPere ); 
                }
                lblNbre.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;
                
            }
            catch { }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCertificat.Rows.Clear();
                var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeNaissance().Where
                    (c => c.DateNaissanceEnfant >=dateTimePicker1.Value.Date && c.DateNaissanceEnfant<= dateTimePicker2.Value.Date);

                dgvCertificat.Rows.Clear();
                foreach (var p in listeCertificat)
                {
                    var nomMere = ConnectionClassClinique.ListeDesPatients(p.IDPatient)[0].Nom + " " + ConnectionClassClinique.ListeDesPatients(p.IDPatient)[0].Prenom;
                    var nomEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", p.NumeroEmplye)[0].NomEmployee;
                    dgvCertificat.Rows.Add(p.ID, nomEmploye, p.NomEnfant, p.DateNaissanceEnfant, p.LieuNaissanceEnfant, p.Sexe, p.Poids, p.IDPatient, nomMere, p.NomPere);
                }
                lblNbre.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;

            }
            catch { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listePatient = new List<Patient>();
                if (Int32.TryParse(textBox1.Text, out idPatient))
                {
                    listePatient = ConnectionClassClinique.ListeDesPatients(idPatient);
                }
                else
                {
                    if (textBox1.Text.Length > 2)
                    {
                        listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise(textBox1.Text, "");
                    }
                }
                if (listePatient.Count() > 0)
                {
                    dataGridView2.Rows.Clear();
                    foreach (var patient in listePatient)
                    {
                        var age = AfficherAge(patient.An, patient.Mois);
                        dataGridView2.Rows.Add(
                            patient.NumeroPatient.ToString(),
                            patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                         age.ToLower(), patient.Sexe,
                            patient.NomEntreprise,
                            patient.Couvert,
                      patient.SousCouvert
                        );
                    }
                }

            }
            catch { }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvCertificat.SelectedRows.Count>0)
                {
                    textBox1.Text = "";
                id = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[0].Value.ToString());
                var liste = ConnectionClassClinique.ListeDesCertificatDeNaissance().Where(c => c.ID == id);
                    foreach (var c in liste)
                    {
                        if (c.Sexe == "M")
                            chkMale.Checked = true;
                        else
                            chkFemelle.Checked = true;
                        c.ProfessionMere = ConnectionClassClinique.ListeDesPatients(c.IDPatient)[0].Fonction;
                        textBox1.Text = c.IDPatient.ToString();
                        txtAnneeMere.Text = c.DateNaissanceMere.Year.ToString();
                        txtAnneePere.Text = c.DateNaissancePere.Year.ToString();
                        txtDomicilePere.Text = c.LieuNaissancePere;
                        txtLieuNaissance.Text = c.LieuNaissanceEnfant;
                        txtLieuNaissanceMere.Text = c.LieuNaissanceMere;
                        txtNomBebe.Text = c.NomEnfant;
                        txtNomEpoux.Text = c.NomPere;
                        txtPoidsBebe.Text = c.Poids.ToString();
                        txtProfessionMere.Text = c.ProfessionMere;
                        txtProfessionPere.Text = c.ProfessionPere;
                        dUDDateMer.Text = c.DateNaissanceMere.Day.ToString();
                        dUDDatePere.Text = c.DateNaissancePere.Day.ToString();
                        dUDHeureEnfant.Text = c.DateNaissanceEnfant.Hour.ToString();
                        dtpNaissanceEnfant.Value = c.DateNaissanceEnfant.Date;
                        dUDMinutesEnfant.Text = c.DateNaissanceEnfant.Minute.ToString();
                        dUDMoisMere.Text = c.DateNaissanceMere.Month.ToString();
                        dUDMoisPere.Text = c.DateNaissancePere.Month.ToString();
                        cmbNumeroEmploye.Text = ConnectionClassClinique.ListeDesEmployees("num_empl", c.NumeroEmplye)[0].NomEmployee;
                    }
                }
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCertificat.Rows.Clear();
                var listeCertificat = from c in ConnectionClassClinique.ListeDesCertificatDeNaissance()
                                      join em in ConnectionClassClinique.ListeDesEmployees()
                                      on c.NumeroEmplye equals em.NumMatricule
                                      join p in ConnectionClassClinique.ListeDesPatients()
                                      on c.IDPatient equals p.NumeroPatient
                                      where p.Nom.StartsWith(txtRecherche.Text, StringComparison.CurrentCultureIgnoreCase)
                                      select new
                                      {
                                          c.IDPatient,c.ID,c.NomEnfant,c.NomPere,c.DateNaissanceEnfant,
                                          c.LieuNaissanceEnfant,c.Sexe,c.Poids  , em.NomEmployee , p.Nom,p.Prenom
                                      };
                dgvCertificat.Rows.Clear();
                foreach (var p in listeCertificat)
                {
                    var nomMere = p.Nom + " " + p.Prenom;
                    dgvCertificat.Rows.Add(p.ID, p.NomEmployee, p.NomEnfant, p.DateNaissanceEnfant, p.LieuNaissanceEnfant, p.Sexe, p.Poids, p.IDPatient, nomMere, p.NomPere);
                }
                lblNbre.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;

            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    checkBox2.Checked = false;
                    dgvCertificat.Rows.Clear();
                    var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeNaissance().Where(c => c.Sexe == "M");

                    dgvCertificat.Rows.Clear();
                    foreach (var p in listeCertificat)
                    {
                        var nomMere = ConnectionClassClinique.ListeDesPatients(p.IDPatient)[0].Nom + " " + ConnectionClassClinique.ListeDesPatients(p.IDPatient)[0].Prenom;
                        var nomEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", p.NumeroEmplye)[0].NomEmployee;
                        dgvCertificat.Rows.Add(p.ID, nomEmploye, p.NomEnfant, p.DateNaissanceEnfant, p.LieuNaissanceEnfant, p.Sexe, p.Poids, p.IDPatient, nomMere, p.NomPere);
                    }
                    lblNbre.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;
                }
            }
            catch { }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox2.Checked)
                {
                    checkBox1.Checked = false;
                    dgvCertificat.Rows.Clear();
                    var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeNaissance().Where(c => c.Sexe == "F");

                    dgvCertificat.Rows.Clear();
                    foreach (var p in listeCertificat)
                    {
                        var nomMere = ConnectionClassClinique.ListeDesPatients(p.IDPatient)[0].Nom + " " + ConnectionClassClinique.ListeDesPatients(p.IDPatient)[0].Prenom;
                        var nomEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", p.NumeroEmplye)[0].NomEmployee;
                        dgvCertificat.Rows.Add(p.ID, nomEmploye, p.NomEnfant, p.DateNaissanceEnfant, p.LieuNaissanceEnfant, p.Sexe, p.Poids, p.IDPatient, nomMere, p.NomPere);
                    }
                    lblNbre.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;
                }
            }
            catch { }
        }

        public static string AfficherAge(string an, string mois)
        {
            int annee;
            if (!string.IsNullOrEmpty(an))
            {
                if (Int32.TryParse(an, out annee))
                {
                    if (!string.IsNullOrEmpty(mois))
                    {
                        return an + " ans et " + mois + " mois ";
                    }
                    else
                    {
                        return an + " ans ";
                    }
                }
                else
                {
                    return an;
                }
            }
            else
            {
                return mois + " mois";
            }
        }


        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                int idPat;
               GestionDuneClinique.Formes. PatientFrm.btnClick = "1";
                if (Int32.TryParse(textBox1.Text, out idPat))
                    GestionDuneClinique.Formes.PatientFrm.numeroPatient = idPat;
                if (GestionDuneClinique.Formes.PatientFrm.ShowBox() == "1")
                {
                    textBox1.Text = GestionDuneClinique.Formes.PatientFrm.numeroPatient > 0 ? GestionDuneClinique.Formes.PatientFrm.numeroPatient.ToString() : "";
                    textBox1_TextChanged(null, null);
                }
            }
            catch { }
        }


        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCertificat.SelectedRows.Count > 0)
                {
                    if (state == "1")
                    {
                        idPatiente = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[1].Value.ToString());
                        nomPatiente = dgvCertificat.SelectedRows[0].Cells[2].Value.ToString();
                        fraisCertif = Convert.ToDouble(dgvCertificat.SelectedRows[0].Cells[15].Value.ToString());
                        btnClick = "1";
                        Dispose();
                    }
                }
            }
            catch { }
        }

    }
}
