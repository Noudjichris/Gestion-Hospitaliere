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
    public partial class CertificatDeDecesFrm : Form
    {
        public CertificatDeDecesFrm()
        {
            InitializeComponent();
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
               Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void CertificatDeDecesFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
               Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
               Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
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
                        dataGridView1.Rows.Add(p.NumeroPatient, p.Nom + " " + p.Prenom,p.An);
                    }
                    dataGridView1.Focus();

                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("cmbPatient_KeyDown", ex);
            }
        }

        private void cmbMedecin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var listePatient = ConnectionClassClinique.ListeDesEmployees(cmbMedecin.Text);

                    cmbMedecin.Items.Clear();
                    if (listePatient.Count() > 0)
                    {
                        cmbMedecin.DropDownStyle = ComboBoxStyle.DropDownList;
                        foreach (var p in listePatient)
                        {
                            cmbMedecin.Items.Add(p.NomEmployee);
                        }
                        cmbMedecin.DroppedDown = true;
                        cmbMedecin.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("cmbMedecin_KeyDown", ex);
            }

        }

        private void CertificatDeDecesFrm_Load(object sender, EventArgs e)
        {
            for (var i = 1; i <= 59; i++)
            {
                dUDMinute.Items.Add(i);duDMinutesHosp.Items.Add(i);
            }
                for (var i = 1; i <= 23; i++)
                {
                    dUDHeure.Items.Add(i);
                    duDHeureHosp.Items.Add(i);
                } 
            
        }
        int idPatient, id; string idEmploye;
        CertificatDeDeces CreerUnCerticficat()
        {

            if (!string.IsNullOrEmpty(txtCause.Text))
            {
                  var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbMedecin.Text);
                    if (listeEmpl.Count > 0)
                    {
                        var certificat = new  CertificatDeDeces();
                         if (dataGridView1.SelectedRows.Count > 0)
                         {
                             
                             certificat.IDPatient = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                         }
                        else  if (dgvCertificat.SelectedRows.Count > 0)
                        {
                            certificat.ID = id;
                            certificat.IDPatient = idPatient;
                        }
                         
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez selectionner le nom du patient sur la liste", "Erreur", "erreur.png");
                            return null;
                        }
                         double frais;
                         if (Double.TryParse(txtFrais.Text, out frais))
                         {
                         }
                         else
                         {
                             frais = 0.0;
                         }
                         certificat.FraisCertificat = frais;
                         certificat.IDEmploye = listeEmpl[0].NumMatricule;                        
                        certificat.CauseDeces = txtCause.Text;
                        certificat.DateHospitalisation = DateTime.Parse(dtpHosp.Value.ToShortDateString() + " " + duDMinutesHosp.Text + ":" + duDHeureHosp.Text + ":00");
                        certificat.Service = txtService.Text;
                        certificat.DateDeces =DateTime.Parse( dtpDeces.Value.ToShortDateString()+" "+dUDHeure.Text +":"+dUDMinute.Text+":00");
                        return certificat;

                    }
                    else
                    {
                        MonMessageBox.ShowBox("Le nom du medecin " + cmbMedecin.Text + " n'existe pas dans la base de données", "Erreur", "erreur.png");
                        return null;
                    }

            }
            else
            {
                MonMessageBox.ShowBox("Veuillez entrer la cause de decés du malade", "Erreur", "erreur.png");
                return null;
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            var certificat = CreerUnCerticficat();
            if (certificat != null)
            {
                if(ConnectionClassClinique.AjouterUnCertificatDeCes(certificat))
                {
                    cmbMedecin.Text = "";
                    dataGridView1.Rows.Clear();
                    txtCause.Text = "";
                    txtClient.Text = "";
                    dgvCertificat.Rows.Clear();
                    txtService.Text = "";
                }
            }
        }

        private void cmbMedecin_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMedecin.Text = cmbMedecin.SelectedText;
            cmbMedecin.DropDownStyle = ComboBoxStyle.Simple;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCertificat.Rows.Count > 0)
                {
                    var certificat = CreerUnCerticficat();
                    if (certificat != null)
                    {
                        certificat.ID = id;
                        if (MonMessageBox.ShowBox("Voulez vous vous modifier les données du certificat", "Confirmation", "confirmation.png") == "1")
                        {
                            if (ConnectionClassClinique.ModifierUnCertificatDeces(certificat))
                            {
                                cmbMedecin.Text = "";
                                dataGridView1.Rows.Clear();
                                txtCause.Text = "";
                                txtClient.Text = "";
                                dgvCertificat.Rows.Clear();
                                txtService.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données à modifier", "Erreur", "erreur.png");
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrer certificat", ex);
            }
        }

        private void btnRetirer_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCertificat.Rows.Count > 0)
                {
                    var certificat = CreerUnCerticficat();
                    if (certificat != null)
                    {
                        certificat.ID = id;
                        if (MonMessageBox.ShowBox("Voulez vous vous supprimer les données du certificat", "Confirmation", "confirmation.png") == "1")
                        {
                            if (ConnectionClassClinique.SupprimerUnCertficatDeDeces(certificat.ID))
                            {
                                cmbMedecin.Text = "";
                                dataGridView1.Rows.Clear();
                                txtCause.Text = "";
                                txtClient.Text = "";
                                dgvCertificat.Rows.Clear();
                                txtService.Text = "";
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

        private void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            dgvCertificat.Rows.Clear();
            var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeDeces();
                var listePatient = ConnectionClassClinique.ListeDesPatients();
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
            if (comboBox2.Text == "Nom patient")
            {
                
            var list = from lc in listeCertificat
                               join lp in listePatient
                               on lc.IDPatient equals lp.NumeroPatient
                               join le in listeEmploye on
                               lc.IDEmploye equals le.NumMatricule
                               where lp.Nom.StartsWith(txtRecherche.Text, StringComparison.CurrentCultureIgnoreCase)
                               select new
                               {
                                   lc.ID,
                                   lc.IDPatient,
                                   patient = lp.Nom + " " + lp.Prenom,
                                   lc.DateDeces,
                                   lc.CauseDeces,
                                   lc.DateHospitalisation,
                                   lc.IDEmploye,
                                   le.NomEmployee,
                                   lc.Service,
                                   lc.FraisCertificat
                               };

                    dgvCertificat.Rows.Clear();
                    foreach (var p in list)
                    {
                        dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.IDEmploye, p.NomEmployee, p.DateHospitalisation , p.DateDeces ,p.CauseDeces,p.Service,p.FraisCertificat );
                    }
                    lblNombre.Text = "Nombre de decés : " + dgvCertificat.Rows.Count;
                }
            else  if (comboBox2.Text == "Cause de decés")
            {
                
            var list = from lc in listeCertificat
                               join lp in listePatient
                               on lc.IDPatient equals lp.NumeroPatient
                               join le in listeEmploye on
                               lc.IDEmploye equals le.NumMatricule
                               where lc.CauseDeces.Contains(txtRecherche.Text)
                               select new
                               {
                                   lc.ID,
                                   lc.IDPatient,
                                   patient = lp.Nom + " " + lp.Prenom,
                                   lc.DateDeces,
                                   lc.CauseDeces,
                                   lc.DateHospitalisation,
                                   lc.IDEmploye,
                                   le.NomEmployee,
                                   lc.Service,
                                   lc.FraisCertificat 
                               };

                    dgvCertificat.Rows.Clear();
                    foreach (var p in list)
                    {
                        dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.IDEmploye, p.NomEmployee, p.DateHospitalisation , p.DateDeces ,p.CauseDeces,p.Service,p.FraisCertificat );
                    }
                    lblNombre.Text = "Nombre de decés : " + dgvCertificat.Rows.Count;
                }
            else if (comboBox2.Text == "<Toutes>")
            {
                var list = from lc in listeCertificat
                           join lp in listePatient
                           on lc.IDPatient equals lp.NumeroPatient
                           join le in listeEmploye on
                           lc.IDEmploye equals le.NumMatricule
                           select new
                           {
                               lc.ID,
                               lc.IDPatient,
                               patient = lp.Nom + " " + lp.Prenom,
                               lc.DateDeces,
                               lc.CauseDeces,
                               lc.DateHospitalisation,
                               lc.IDEmploye,
                               le.NomEmployee,
                               lc.Service,
                               lc.FraisCertificat 
                           };

                dgvCertificat.Rows.Clear();
                foreach (var p in list)
                {
                    dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.IDEmploye, p.NomEmployee, p.DateHospitalisation, p.DateDeces, p.CauseDeces, p.Service,p.FraisCertificat );
                }
                lblNombre.Text = "Nombre de decés : " + dgvCertificat.Rows.Count;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvCertificat.Rows.Clear();
            var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeDeces();
            var listePatient = ConnectionClassClinique.ListeDesPatients();
            var listeEmploye = ConnectionClassClinique.ListeDesEmployees();

            if (comboBox2.Text == "<Toutes>")
            {
                var list = from lc in listeCertificat
                           join lp in listePatient
                           on lc.IDPatient equals lp.NumeroPatient
                           join le in listeEmploye on
                           lc.IDEmploye equals le.NumMatricule
                           select new
                           {
                               lc.ID,
                               lc.IDPatient,
                               patient = lp.Nom + " " + lp.Prenom,
                               lc.DateDeces,
                               lc.CauseDeces,
                               lc.DateHospitalisation,
                               lc.IDEmploye,
                               le.NomEmployee,
                               lc.Service,
                               lc.FraisCertificat
                           };

                
                foreach (var p in list)
                {
                    dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.IDEmploye, p.NomEmployee, p.DateHospitalisation, p.DateDeces, p.CauseDeces, p.Service,p.FraisCertificat );
                }
                lblNombre.Text = "Nombre de decés : " + dgvCertificat.Rows.Count;
                dateTimePicker3.Visible = false;
                dateTimePicker4.Visible = false;
                txtRecherche.Visible = true;
            }
            else if (comboBox2.Text == "Nom patient" || comboBox2.Text =="Cause de decés")
            {
                dateTimePicker3.Visible = false;
                dateTimePicker4.Visible = false;
                txtRecherche.Visible = true;
            }
            else if (comboBox2.Text == "Decés entre deux dates")
            {
                dateTimePicker3.Visible = true;
                dateTimePicker4.Visible = true;
                txtRecherche.Visible = false;
            }
            else if (comboBox2.Text == "Date de decés")
            {
                dateTimePicker3.Visible = true;
                dateTimePicker4.Visible = false ;
                txtRecherche.Visible = false;
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Date de decés")
            {
                var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeDeces();
            var listePatient = ConnectionClassClinique.ListeDesPatients();
            var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
            var list = from lc in listeCertificat
                           join lp in listePatient
                           on lc.IDPatient equals lp.NumeroPatient
                           join le in listeEmploye on
                           lc.IDEmploye equals le.NumMatricule
                           where lc.DateDeces>= dateTimePicker3.Value.Date 
                           where lc.DateDeces<dateTimePicker3.Value.Date.AddDays(1)
                           select new
                           {
                               lc.ID,
                               lc.IDPatient,
                               patient = lp.Nom + " " + lp.Prenom,
                               lc.DateDeces,
                               lc.CauseDeces,
                               lc.DateHospitalisation,
                               lc.IDEmploye,
                               le.NomEmployee,
                               lc.Service,
                               lc.FraisCertificat
                           };

                dgvCertificat.Rows.Clear();
                foreach (var p in list)
                {
                    dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.IDEmploye, p.NomEmployee, p.DateHospitalisation, p.DateDeces, p.CauseDeces,p.Service,p.FraisCertificat );
                }
                lblNombre.Text = "Nombre de decés : " + dgvCertificat.Rows.Count;
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Decés entre deux dates")
            {
                var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeDeces();
                var listePatient = ConnectionClassClinique.ListeDesPatients();
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
                var list = from lc in listeCertificat
                           join lp in listePatient
                           on lc.IDPatient equals lp.NumeroPatient
                           join le in listeEmploye on
                           lc.IDEmploye equals le.NumMatricule
                           where lc.DateDeces >= dateTimePicker3.Value.Date
                           where lc.DateDeces < dateTimePicker4.Value.Date.AddDays(1)
                           select new
                           {
                               lc.ID,
                               lc.IDPatient,
                               patient = lp.Nom + " " + lp.Prenom,
                               lc.DateDeces,
                               lc.CauseDeces,
                               lc.DateHospitalisation,
                               lc.IDEmploye,
                               le.NomEmployee,
                               lc.Service,
                               lc.FraisCertificat
                           };

                dgvCertificat.Rows.Clear();
                foreach (var p in list)
                {
                    dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.patient, p.IDEmploye, p.NomEmployee, p.DateHospitalisation, p.DateDeces, p.CauseDeces,p.Service,p.FraisCertificat );
                }
                lblNombre.Text = "Nombre de decés : " + dgvCertificat.Rows.Count;
            }
        }

        private void dgvCertificat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCertificat.Rows.Count > 0)
            {
                id = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[0].Value.ToString());
                idPatient = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[1].Value.ToString());               
                txtClient.Text = dgvCertificat.SelectedRows[0].Cells[2].Value.ToString();
                cmbMedecin.Text = dgvCertificat.SelectedRows[0].Cells[4].Value.ToString();
                txtCause.Text = dgvCertificat.SelectedRows[0].Cells[7].Value.ToString();
                var dateHosp = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[5].Value.ToString());
                var dateDeces = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[6].Value.ToString());
                dtpDeces.Value = dateDeces.Date;
                dUDHeure.Text = dateDeces.Hour.ToString();
                dUDMinute.Text = dateDeces.Minute.ToString();
                dtpHosp.Value = dateHosp.Date;
                duDHeureHosp.Text = dateHosp.Hour.ToString();
                duDMinutesHosp.Text = dateHosp.Minute.ToString();
                txtService.Text = dgvCertificat.SelectedRows[0].Cells[8].Value.ToString();
                txtFrais.Text = dgvCertificat.SelectedRows[0].Cells[9].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            txtClient.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }


        Bitmap document;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 10, 10, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void btnApercc_Click(object sender, EventArgs e)
        {
            try
            {
                var certificat = CreerUnCerticficat();
                if (certificat != null)
                {
                    document = Impression.CertificatDeDeces(certificat);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new GestionDuneClinique.Formes.PatientFrm();
            frm.state = 1;
            frm.ShowDialog();

        }
        public static int idPatiente;
        public static string nomPatiente, btnClick, state = "2";
        public static double fraisCertif;
        private static CertificatDeDecesFrm frmActe;
        public static string ShowBox()
        {
            try
            {
                frmActe = new CertificatDeDecesFrm();
                frmActe.ShowDialog();
                return btnClick;
            }
            catch { return btnClick; }
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
                        fraisCertif = Convert.ToDouble(dgvCertificat.SelectedRows[0].Cells[9].Value.ToString());
                        btnClick = "1";
                        Dispose();
                    }
                }
            }
            catch { }
        }

        private void dgvCertificat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvCertificat_CellContentClick(null, null);
                dgvCertificat.Focus();
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
