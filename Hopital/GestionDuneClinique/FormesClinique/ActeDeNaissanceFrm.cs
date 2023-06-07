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
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 1);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox7.Width - 1, this.groupBox7.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void txtClient_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var listePatient = ConnectionClassClinique.ListeDesPatients(txtClient.Text);
                    dataGridView1.Rows.Clear();
                    foreach (var p in listePatient)
                    {
                        dataGridView1.Rows.Add(p.NumeroPatient, p.Nom + " " + p.Prenom);
                    }
                    dataGridView1.Focus();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("txtClient_KeyDown", ex);
            }
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
            groupBox4.Visible = false;
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
            //timer1.Start();
            clMere.Width = dgvCertificat.Width * 3 / 5;
            clBebe.Width = dgvCertificat.Width * 3 / 5;
            clSexe.Width = dgvCertificat.Width/ 7;
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
            double frais;
            if (!string.IsNullOrEmpty(txtNomBebe.Text) && !string.IsNullOrEmpty(txtPrenomBebe.Text))
            {
                if (Int32.TryParse(dUDDateMer.Text, out jour1) && Int32.TryParse(dUDMoisMere.Text, out mois1)
                    && Int32.TryParse(dUDDatePere.Text, out jour2) && Int32.TryParse(dUDMoisPere.Text, out mois2)
                    && Int32.TryParse(txtAnneeMere.Text, out annee1) && Int32.TryParse(txtAnneePere.Text, out annee2)
                    && Int32.TryParse(dUDHeureEnfant.Text, out heure) && Int32.TryParse(dUDMinutesEnfant.Text, out minute))
                {
                    if (!string.IsNullOrEmpty(txtNomEpoux.Text))
                    {
                        if (float.TryParse(txtPoidsBebe.Text, out poids ))
                        {
                            if (chkFemelle.Checked || chkMale.Checked)
                            {
                                if (!string.IsNullOrEmpty(txtSageFemme.Text))
                                {
                                    var certificat = new CertificatNaissance();
                                    if (dataGridView1.SelectedRows.Count > 0)
                                    {
                                        certificat.IDPatient = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                                    }
                                    else 
                                    {
                                        certificat.ID = id;
                                        certificat.IDPatient = idPatient;
                                    }

                                    if (double.TryParse(txtFrais.Text, out  frais))
                                    {
                                    }
                                    else
                                    {
                                        frais = 0.0;
                                    }
                                    certificat.SageFemme = txtSageFemme.Text ;                                    
                                    certificat.BeBe = txtNomBebe.Text + " " + txtPrenomBebe.Text;
                                    certificat.DomicileEpoux = txtDomicilePere.Text;
                                    certificat.DomicileMere = txtDomicileMere.Text;
                                    certificat.Epoux = txtNomEpoux.Text;
                                    certificat.NaissanceEnfant = DateTime.Parse(dtpNaissance.Value.ToShortDateString() + " " + dUDHeureEnfant.Text + ":" + dUDMinutesEnfant.Text + ":" + "00");
                                    certificat.NaissanceEpoux = DateTime.Parse(dUDDatePere.Text + "-" + dUDMoisPere.Text + "-" + txtAnneePere.Text);
                                    certificat.NaissanceMere = DateTime.Parse(dUDDateMer.Text + "-" + dUDMoisPere.Text + "-" + txtAnneeMere.Text);
                                    certificat.Poids = poids;
                                    certificat.ProfessionEpoux = txtProfessionPere.Text;
                                    certificat.ProfesssionMere = txtProfessionMere.Text;
                                    certificat.DomicileMere = txtDomicileMere.Text;
                                    certificat.DomicileEpoux = txtDomicilePere.Text;
                                    certificat.Frais = frais;
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
                                    MonMessageBox.ShowBox("Veuillez selectionner le nom de la sage-femme.", "Erreur", "erreur.png");
                                    return null;
                                }
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
            txtSageFemme.Text = "";
            txtClient.Text = "";
            txtDomicileMere.Text = "";
            txtDomicilePere.Text = "";
            txtNomBebe.Text = "";
            txtNomEpoux.Text = "";
            txtPoidsBebe.Text = "";
            txtPrenomBebe.Text = "";
            txtProfessionMere.Text = "";
            txtProfessionPere.Text = "";
            dataGridView1.Rows.Clear();
            txtFrais.Text = "";
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
                        ViderLesChamps();
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrer un certficat", ex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                txtClient.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }

        }

        private void txtRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgvCertificat.Rows.Clear();
                    var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeNaissance();
                    var listePatient = ConnectionClassClinique.ListeDesPatients();

                    if (comboBox2.Text == "Nom du bébé")
                    {
                        var list = from lc in listeCertificat
                                   join lp in listePatient
                                   on lc.IDPatient equals lp.NumeroPatient
                                   where lc.BeBe.StartsWith(txtRecherche.Text, StringComparison.CurrentCultureIgnoreCase)
                                   select new
                                   {
                                       lc.ID,
                                       lc.IDPatient,
                                       patient = lp.Nom + " " + lp.Prenom,
                                       lc.NaissanceMere,
                                       lc.NaissanceEnfant,
                                       lc.NaissanceEpoux,
                                       sageFemme = lc.SageFemme,
                                       lc.Epoux,
                                       lc.Poids,
                                       lc.ProfessionEpoux,
                                       lc.ProfesssionMere,
                                       lc.DomicileEpoux,
                                       lc.BeBe,
                                       lc.DomicileMere,
                                       lc.Sexe,
                                       lc.Frais
                                   };

                        dgvCertificat.Rows.Clear();
                        foreach (var p in list)
                        {
                            dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.sageFemme, p.NaissanceEnfant, p.BeBe, p.Sexe,
                                p.Poids, p.ProfesssionMere, p.DomicileMere, p.NaissanceMere, p.Epoux, p.ProfessionEpoux, p.DomicileEpoux, p.NaissanceEpoux, p.Frais);
                        }
                    }
                    else if (comboBox2.Text == "Nom de la mère")
                    {
                        var list = from lc in listeCertificat
                                   join lp in listePatient
                                   on lc.IDPatient equals lp.NumeroPatient
                                   where lp.Nom.StartsWith(txtRecherche.Text, StringComparison.CurrentCultureIgnoreCase)
                                   select new
                                   {
                                       lc.ID,
                                       lc.IDPatient,
                                       patient = lp.Nom + " " + lp.Prenom,
                                       lc.NaissanceMere,
                                       lc.NaissanceEnfant,
                                       lc.NaissanceEpoux,
                                       sageFemme = lc.SageFemme,
                                       lc.Epoux,
                                       lc.Poids,
                                       lc.ProfessionEpoux,
                                       lc.ProfesssionMere,
                                       lc.DomicileEpoux,
                                       lc.BeBe,
                                       lc.DomicileMere,
                                       lc.Sexe,
                                       lc.Frais
                                   };

                        dgvCertificat.Rows.Clear();
                        foreach (var p in list)
                        {
                            dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.sageFemme,  p.NaissanceEnfant, p.BeBe, p.Sexe,
                                p.Poids, p.ProfesssionMere, p.DomicileMere, p.NaissanceMere, p.Epoux, p.ProfessionEpoux, p.DomicileEpoux, p.NaissanceEpoux,p.Frais);
                        }
                    }
                    else if (comboBox2.Text == "Sexe")
                    {
                        var list = from lc in listeCertificat
                                   join lp in listePatient
                                   on lc.IDPatient equals lp.NumeroPatient
                                   where lc.Sexe.StartsWith(txtRecherche.Text, StringComparison.CurrentCultureIgnoreCase)
                                   select new
                                   {
                                       lc.ID,
                                       lc.IDPatient,
                                       patient = lp.Nom + " " + lp.Prenom,
                                       lc.NaissanceMere,
                                       lc.NaissanceEnfant,
                                       lc.NaissanceEpoux,
                                       sageFemme = lc.SageFemme,
                                       lc.Epoux,
                                       lc.Poids,
                                       lc.ProfessionEpoux,
                                       lc.ProfesssionMere,
                                       lc.DomicileEpoux,
                                       lc.BeBe,
                                       lc.DomicileMere,
                                       lc.Sexe,
                                       lc.Frais
                                   };

                        dgvCertificat.Rows.Clear();
                        foreach (var p in list)
                        {
                            dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.sageFemme, p.NaissanceEnfant, p.BeBe, p.Sexe,
                                p.Poids, p.ProfesssionMere, p.DomicileMere, p.NaissanceMere, p.Epoux, p.ProfessionEpoux, p.DomicileEpoux, p.NaissanceEpoux,p.Frais);
                        }
                    }
                    label13.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;
                    dgvCertificat.Focus();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("txtRechercher", ex);
            }
        }

        private void dgvCertificat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvCertificat_DoubleClick(null, null);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == ">>")
            {
                groupBox4.Visible = true;
                dgvCertificat.Rows.Clear();
                button6.Text = "<<";
            }
            else if(button6.Text == "<<")
            {
                button6.Text = ">>";
                groupBox4.Visible = false;
                dgvCertificat.Rows.Clear();
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
                var certificat = CreerUnCertificat();
                if (certificat != null)
                {
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                
                    var certificat = CreerUnCertificat();
                    if (certificat != null)
                    {
                        certificat.ID = id;
                        if (MonMessageBox.ShowBox("Voulez vous vous modifier les données du certificat", "Confirmation", "confirmation.png") == "1")
                        {
                            if (ConnectionClassClinique.ModifierUnCertificatDeNaissance(certificat))
                            {
                                ViderLesChamps();
                            }
                        }
                    }
             
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrer certificat", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCertificat.Rows.Count > 0)
                {
                    var certificat = CreerUnCertificat();
                    if (certificat != null)
                    {
                        certificat.ID = id;
                        if (MonMessageBox.ShowBox("Voulez vous vous supprimer les données du certificat", "Confirmation", "confirmation.png") == "1")
                        {
                            if (ConnectionClassClinique.SupprimerUnCertficatDeNaissance(certificat.ID))
                            {
                                ViderLesChamps();
                            }
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
                dgvCertificat.Rows.Clear();
                    var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeNaissance();
                    var listePatient = ConnectionClassClinique.ListeDesPatients();
                    if (comboBox2.Text == "<Toutes>")
                    {
                        var list = from lc in listeCertificat
                                   join lp in listePatient
                                   on lc.IDPatient equals lp.NumeroPatient
                                   select new
                                   {
                                       lc.ID,
                                       lc.IDPatient,
                                       patient = lp.Nom + " " + lp.Prenom,
                                       lc.NaissanceMere,
                                       lc.NaissanceEnfant,
                                       lc.NaissanceEpoux,
                                       NumeroEmploye = lc.SageFemme,
                                       lc.Epoux,
                                       lc.Poids,
                                       lc.ProfessionEpoux,
                                       lc.ProfesssionMere,
                                       lc.DomicileEpoux,
                                       lc.BeBe,
                                       lc.DomicileMere,
                                       lc.Sexe,lc.Frais
                                   };

                        dgvCertificat.Rows.Clear();
                        foreach (var p in list)
                        {
                            dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.NumeroEmploye,  p.NaissanceEnfant, p.BeBe, p.Sexe,
                                p.Poids, p.ProfesssionMere, p.DomicileMere, p.NaissanceMere, p.Epoux, p.ProfessionEpoux, p.DomicileEpoux, p.NaissanceEpoux,p.Frais);
                        }
                        label13.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;
                    }
                    else if (comboBox2.Text == "Nom du bébé")
                    {
                        dateTimePicker2.Visible = false;
                        dateTimePicker1.Visible = false;
                        txtRecherche.Visible = true;
                    }
                    else if (comboBox2.Text == "Nom de la mère")
                    {
                        dateTimePicker2.Visible = false;
                        dateTimePicker1.Visible = false;
                        txtRecherche.Visible = true;
                    }
                    else if (comboBox2.Text == "Sexe")
                    {
                        dateTimePicker2.Visible = false;
                        dateTimePicker1.Visible = false;
                        txtRecherche.Visible = true;
                    }
                    else if (comboBox2.Text == "Date de naissance")
                    {
                        dateTimePicker1.Visible = true;
                        dateTimePicker2.Visible = false; ;
                        txtRecherche.Visible = false;
                    }
                    else if (comboBox2.Text == "Naissance entre deux dates")
                    {
                        dateTimePicker2.Visible = true;
                        dateTimePicker1.Visible = true;
                        txtRecherche.Visible = false;
                    }
                    
                   txtRecherche.Focus();
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
                dgvCertificat.Rows.Clear();
                    var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeNaissance();
                    var listePatient = ConnectionClassClinique.ListeDesPatients();
              if (comboBox2.Text == "Date de naissance")
                    {
                       
                        var list = from lc in listeCertificat
                                   join lp in listePatient
                                   on lc.IDPatient equals lp.NumeroPatient
                                   where lc.NaissanceEnfant>=dateTimePicker1.Value.Date
                                   where lc.NaissanceEnfant < dateTimePicker1.Value.Date.AddHours(24)
                                   select new
                                   {
                                       lc.ID,
                                       lc.IDPatient,
                                       patient = lp.Nom + " " + lp.Prenom,
                                       lc.NaissanceMere,
                                       lc.NaissanceEnfant,
                                       lc.NaissanceEpoux,
                                       NumeroEmploye = lc.SageFemme,
                                       lc.Epoux,
                                       lc.Poids,
                                       lc.ProfessionEpoux,
                                       lc.ProfesssionMere,
                                       lc.DomicileEpoux,
                                       lc.BeBe,
                                       lc.DomicileMere,
                                       lc.Sexe,
                                       lc.Frais
                                   };

                        dgvCertificat.Rows.Clear();
                        foreach (var p in list)
                        {
                            dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.NumeroEmploye, p.NaissanceEnfant, p.BeBe, p.Sexe,
                                p.Poids, p.ProfesssionMere, p.DomicileMere, p.NaissanceMere, p.Epoux, p.ProfessionEpoux, p.DomicileEpoux, p.NaissanceEpoux, p.Frais);
                        }
                    }
              label13.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;
           }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dgvCertificat.Rows.Clear();
            var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeNaissance();
            var listePatient = ConnectionClassClinique.ListeDesPatients();
            if (comboBox2.Text == "Naissance entre deux dates")
            {
                var list = from lc in listeCertificat
                           join lp in listePatient
                           on lc.IDPatient equals lp.NumeroPatient
                           where lc.NaissanceEnfant >= dateTimePicker1.Value.Date
                           where lc.NaissanceEnfant < dateTimePicker2.Value.Date.AddHours(24)
                           select new
                           {
                               lc.ID,
                               lc.IDPatient,
                               patient = lp.Nom + " " + lp.Prenom,
                               lc.NaissanceMere,
                               lc.NaissanceEnfant,
                               lc.NaissanceEpoux,
                               sageFemme = lc.SageFemme,
                               lc.Epoux,
                               lc.Poids,
                               lc.ProfessionEpoux,
                               lc.ProfesssionMere,
                               lc.DomicileEpoux,
                               lc.BeBe,
                               lc.DomicileMere,
                               lc.Sexe,
                               lc.Frais
                           };

                dgvCertificat.Rows.Clear();
                foreach (var p in list)
                {
                    dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.sageFemme, p.NaissanceEnfant, p.BeBe, p.Sexe,
                        p.Poids, p.ProfesssionMere, p.DomicileMere, p.NaissanceMere, p.Epoux, p.ProfessionEpoux, p.DomicileEpoux, p.NaissanceEpoux, p.Frais);
                }
            }
            label13.Text = "Nombre de naissance : " + dgvCertificat.Rows.Count;
        }

        private void dgvCertificat_DoubleClick(object sender, EventArgs e)
        {


            if (dgvCertificat.SelectedRows.Count > 0)
            {
               
                id = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[0].Value.ToString());
                idPatient = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[1].Value.ToString());
                txtClient.Text = dgvCertificat.SelectedRows[0].Cells[2].Value.ToString();
                txtSageFemme.Text = dgvCertificat.SelectedRows[0].Cells[3].Value.ToString();
                var dateNaissance = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[4].Value.ToString());
                dtpNaissance.Value = dateNaissance.Date;
                dUDHeureEnfant.Text = dateNaissance.Hour.ToString();
                dUDMinutesEnfant.Text = dateNaissance.Minute.ToString();
                var nomBebe = dgvCertificat.SelectedRows[0].Cells[5].Value.ToString();

                txtNomBebe.Text = nomBebe.Substring(0, nomBebe.IndexOf(" "));
                txtPrenomBebe.Text = nomBebe.Substring(nomBebe.IndexOf(" ") + 1);
                if (dgvCertificat.SelectedRows[0].Cells[6].Value.ToString() == "M")
                {
                    chkFemelle.Checked = false;
                    chkMale.Checked = true;
                }
                else if (dgvCertificat.SelectedRows[0].Cells[6].Value.ToString() == "F")
                {
                    chkFemelle.Checked = true;
                    chkMale.Checked = false;
                }
                txtPoidsBebe.Text = dgvCertificat.SelectedRows[0].Cells[7].Value.ToString();
                txtProfessionMere.Text = dgvCertificat.SelectedRows[0].Cells[8].Value.ToString();
                txtDomicileMere.Text = dgvCertificat.SelectedRows[0].Cells[9].Value.ToString();
                var naissanceMere = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[10].Value.ToString());
                txtNomEpoux.Text = dgvCertificat.SelectedRows[0].Cells[11].Value.ToString();
                dUDDateMer.Text = naissanceMere.Day.ToString();
                dUDMoisMere.Text = naissanceMere.Month.ToString();
                txtAnneeMere.Text = naissanceMere.Year.ToString();
                txtProfessionPere.Text = dgvCertificat.SelectedRows[0].Cells[12].Value.ToString();
                txtDomicilePere.Text = dgvCertificat.SelectedRows[0].Cells[13].Value.ToString();
                var naissanceEpoux = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[14].Value.ToString());
                dUDDatePere.Text = naissanceEpoux.Day.ToString();
                dUDMoisPere.Text = naissanceEpoux.Month.ToString();
                txtAnneePere.Text = naissanceEpoux.Year.ToString();
                txtFrais.Text = dgvCertificat.SelectedRows[0].Cells[15].Value.ToString();
                button6.Text = ">>";
            }
            groupBox4.Visible = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new GestionDuneClinique.Formes.PatientFrm();
            frm.state = 1;
            frm.ShowDialog();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            groupBox4.Visible = false;
            dgvCertificat.Rows.Clear();
        }

    }
}
