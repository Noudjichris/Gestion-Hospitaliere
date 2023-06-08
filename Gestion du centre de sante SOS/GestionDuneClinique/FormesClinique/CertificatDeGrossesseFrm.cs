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
    public partial class CertificatDeGrossesseFrm : Form
    {
        public CertificatDeGrossesseFrm()
        {
            InitializeComponent();
        }

        private void CertificatDeGrossesse_Paint(object sender, PaintEventArgs e)
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
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
               Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
     
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
               SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
               SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
    
        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        int idCertificat, idPatient;
        private void dgvVente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9)
                {
                    if (dgvCertificat.SelectedRows.Count > 0)
                    {
                        idCertificat = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[0].Value.ToString());
                        txtClient.Text = dgvCertificat.SelectedRows[0].Cells[3].Value.ToString();
                        cmbMedecin.Text = dgvCertificat.SelectedRows[0].Cells[2].Value.ToString();
                        txtPeriode.Text = dgvCertificat.SelectedRows[0].Cells[8].Value.ToString();
                        dtpDDR.Value = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[5].Value.ToString());
                        dtpAccouchement.Value = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[6].Value.ToString());
                        dtpDateConge.Value = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[7].Value.ToString());
                        EnabledControl();
                    }
                }
                else if (e.ColumnIndex == 10)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données ?", "confirmation", "confirmation.png") == "1")
                        if (ConnectionClassClinique.SupprimerUnCertficatDeGrossesse(Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[0].Value.ToString())))
                        {
                            cmbMedecin.Text = "";
                            txtClient.Text = "";
                            dataGridView1.Rows.Clear();
                            txtClient.Text = "";
                            dtpAccouchement.Value = DateTime.Now;
                            dtpDateConge.Value = DateTime.Now;
                            dtpDDR.Value = DateTime.Now;
                            txtPeriode.Text = "";
                            ListeDesCertificats();
                        }
                }
                else if (e.ColumnIndex == 11)
                {

                    var certificat = new CertificatDeGrossesse();
                    var listeCertificat = from c in ConnectionClassClinique.ListeDesCertificatDeGrossesse()
                                          join p in ConnectionClassClinique.ListeDesPatients()
                                          on c.IDPatient equals p.NumeroPatient
                                          join em in ConnectionClassClinique.ListeDesEmployees()
                                          on c.Matricule equals em.NumMatricule
                                          select new
                                          {
                                              c.Matricule,
                                              c.DateAccouchement,
                                              c.DateConge,
                                              c.DateDDR,
                                              c.ID,
                                              c.IDPatient,
                                              c.PeriodeGrossesse,
                                              p.Nom,
                                              p.Prenom,
                                              em.NomEmployee
                                          };
                 
                    foreach (var c in listeCertificat)
                    {
                        certificat.DateAccouchement = c.DateAccouchement;
                        certificat.DateConge = c.DateConge;
                        certificat.DateDDR = c.DateDDR;
                        certificat.Matricule = c.Matricule;
                        certificat.NomDocteur = c.NomEmployee ;
                        certificat.NomPatient = c.Nom + " " + c.Prenom;
                        certificat.PeriodeGrossesse = c.PeriodeGrossesse;
                    }
                    if (certificat != null)
                    {
                        document = Impression.CertificatDeGrossesse(certificat);
                        if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printPreviewDialog1.ShowDialog();
                        }
                    }
                }
            }
            catch { }
        }

        CertificatDeGrossesse CreerUnCertificat()
        {

            if (!string.IsNullOrEmpty(cmbMedecin.Text))
            {
                int periode;
                if (Int32.TryParse(txtPeriode.Text, out periode))
                {
                        var certificat = new CertificatDeGrossesse();
                         if (dataGridView1.SelectedRows.Count > 0)
                         {
                             certificat.IDPatient = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                         }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez selectionner le nom du patient sur la liste", "Erreur", "erreur.png");
                            return null;
                        }

                    certificat.PeriodeGrossesse = periode;
                    certificat.Matricule = ConnectionClassClinique.ListeDesEmployees("nom_empl", cmbMedecin.Text)[0].NumMatricule;
                    certificat.DateAccouchement = dtpAccouchement.Value.Date;
                    certificat.DateConge = dtpDateConge.Value.Date;
                    certificat.DateDDR = dtpDDR.Value.Date;
                    certificat.ID = idCertificat;
                        return certificat;

                  

                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour la période de grossesse", "Erreur", "erreur.png");
                    return null;
                }

            }
            else
            {
                MonMessageBox.ShowBox("Veuillez selectionner le nom du docteur sur la liste", "Erreur", "erreur.png");
                return null;
            }
        }

        private void txtClient_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var listePatient = ConnectionClassClinique.ListeDesPatients( txtClient.Text);
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
                MonMessageBox.ShowBox("cmbPatient_KeyDown", ex);
            }
        }

        private void txtRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var listeCertificat = ConnectionClassClinique.ListeDesCertificatDeGrossesse();
                    var listePatient = ConnectionClassClinique.ListeDesPatients();
                    var list = from lc in listeCertificat
                               join lp in listePatient
                               on lc.IDPatient equals lp.NumeroPatient
                               where lp.Nom.StartsWith(txtRecherche.Text, StringComparison.CurrentCultureIgnoreCase)
                               select new
                               {
                                   lc.ID,
                                   lc.IDPatient,
                                   lc.DateAccouchement,
                                   //sageFemme = lc.SageFemme,
                                   lp.Nom,
                                   lp.Prenom,
                                   //lc.FraisCertificat
                               };

                    dgvCertificat.Rows.Clear();
                    foreach (var p in list)
                    {
                        dgvCertificat.Rows.Add(p.ID, p.IDPatient, p.Nom + " " , p.DateAccouchement);
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox(" txtRecherche_KeyDown", ex);
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


        public static int idPatiente;
        public static string nomPatiente, btnClick, state = "2";
        public static double fraisCertif;
        private static CertificatDeGrossesseFrm frmActe;
        public static string ShowBox()
        {
            try
            {
                frmActe = new CertificatDeGrossesseFrm();
                frmActe.ShowDialog();
                return btnClick;
            }
            catch { return btnClick; }
        }

        private void CertificatDeGrossesseFrm_Load(object sender, EventArgs e)
        {
            try
            {
                cmbMedecin.Items.Add("");
               var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
                var list = from p in listeEmploye
                           where !p.Titre.ToUpper().Contains("CAISS")
                           select p.NomEmployee;

                foreach (var empl in list)
                {
                    cmbMedecin.Items.Add(empl.ToUpper());
                }


                button7.Location = new Point(Width - 33, 2);
                button2.Location = new Point(groupBox1.Width - 45, 10);
                DisabledControl();

                Column8.Width = 35;
                Column9.Width = 35;
                Column10.Width = 40;
                ListeDesCertificats();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var certificat = CreerUnCertificat();
            if (certificat != null)
            {
                if(ConnectionClassClinique.EnregistrerUnCertifiactDeGrossesse(certificat))
                {
                    idCertificat = 0;
                    cmbMedecin.Text = "";
                    txtClient.Text = "";
                    dataGridView1.Rows.Clear();
                    txtClient.Text = "";
                    dtpAccouchement.Value = DateTime.Now;
                    dtpDateConge.Value = DateTime.Now;
                    dtpDDR.Value = DateTime.Now;
                    txtPeriode.Text = "";
                    ListeDesCertificats();
                }
            }
        }

        void ListeDesCertificats()
        {
            try
            {
                var listeCertificat = from c in ConnectionClassClinique.ListeDesCertificatDeGrossesse()
                                      join p in ConnectionClassClinique.ListeDesPatients()
                                      on c.IDPatient equals p.NumeroPatient
                                      join e in ConnectionClassClinique.ListeDesEmployees()
                                      on c.Matricule equals e.NumMatricule
                                      select new
                                      {
                                          c.Matricule,c.DateAccouchement,c.DateConge,c.DateDDR,c.ID,c.IDPatient,c.PeriodeGrossesse,p.Nom,p.Prenom,e.NomEmployee
                                      };
                dgvCertificat.Rows.Clear();
                foreach (var c in listeCertificat)
                    dgvCertificat.Rows.Add(c.ID,c.Matricule,c.NomEmployee,c.IDPatient,c.Nom+" "+c.Prenom,c.DateDDR.ToShortDateString(), c.DateAccouchement.ToShortDateString(), c.DateConge.ToShortDateString(),c.PeriodeGrossesse);
            }
            catch { }
        }
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            EnabledControl();
        }
        void EnabledControl()
        {
            dtpDDR.Enabled = true;
            dtpDateConge.Enabled = true;
            dtpAccouchement.Enabled = true;
            txtPeriode.Enabled = true;
            cmbMedecin.Enabled = true;
            button3.Enabled = true;
        }
        void DisabledControl()
        {
            button3.Enabled = false;
            dtpDDR.Enabled = false;
            dtpDateConge.Enabled = false;
            dtpAccouchement.Enabled = false;
            txtPeriode.Enabled = false;
            cmbMedecin.Enabled = false;
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
                        fraisCertif = Convert.ToDouble(dgvCertificat.SelectedRows[0].Cells[7].Value.ToString());
                        btnClick = "1";
                        Dispose();
                    }
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int idPat;
                GestionDuneClinique.Formes.PatientFrm.btnClick = "1";
                if (Int32.TryParse(txtClient.Text, out idPat))
                    GestionDuneClinique.Formes.PatientFrm.numeroPatient = idPat;
                if (GestionDuneClinique.Formes.PatientFrm.ShowBox() == "1")
                {
                    txtClient.Text = GestionDuneClinique.Formes.PatientFrm.numeroPatient > 0 ? GestionDuneClinique.Formes.PatientFrm.numeroPatient.ToString() : "";
                    txtClient_TextChanged(null, null);
                    btnEnregistrer.Focus();
                }
            }
            catch { }
        }

        private void dtpDDR_ValueChanged(object sender, EventArgs e)
        {
            dtpAccouchement.Value = dtpDDR.Value.Date.AddDays(7 * 40);
            dtpDateConge.Value = dtpAccouchement.Value.Date.AddDays(-7 * 6);
            txtPeriode.Text = (DateTime.Now.Date.Subtract(dtpDDR.Value.Date).Days / 7).ToString();
        }

        private void dgvCertificat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvVente_CellContentClick(null, null);
                dgvCertificat.Focus();
            }
        }

        private void txtClient_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var listePatient = new List<Patient>();
                if (Int32.TryParse(txtClient.Text, out idPatient))
                {
                    listePatient = ConnectionClassClinique.ListeDesPatients(idPatient);
                }
                else
                {
                    if (txtClient.Text.Length > 2)
                    {
                        listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise(txtClient.Text, "");
                    }
                }
                if (listePatient.Count() > 0)
                {
                    dataGridView1.Rows.Clear();
                    foreach (var patient in listePatient)
                    {
                        var age = AfficherAge(patient.An, patient.Mois);
                        dataGridView1.Rows.Add(
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

    }
}
