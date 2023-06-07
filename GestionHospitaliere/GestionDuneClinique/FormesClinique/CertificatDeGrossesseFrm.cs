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
               Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
               Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
               Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
               Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
    
        private void btnRetirer_Click(object sender, EventArgs e)
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
                            if (ConnectionClassClinique.SupprimerUnCertficatDeGrossesse(id))
                            {
                                txtPeriode.Text = "";
                                dataGridView1.Rows.Clear();
                                txtClient.Text = "";
                                txtSageFemme.Text = "";
                                txtEpoux.Text = "";
                                dgvCertificat.Rows.Clear();
                            }
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données à supprimer", "Erreur", "erreur.png");
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnApercc_Click(object sender, EventArgs e)
        {
            try
            {
                var certificat = CreerUnCertificat();
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
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Apercu", ex);
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                var certificat = CreerUnCertificat();
                if (certificat != null)
                {
                    if (ConnectionClassClinique.AjouterUnCertifiactDeGrossesse(certificat))
                    {
                        txtPeriode.Text = "";
                        dataGridView1.Rows.Clear();
                        txtClient.Text = "";
                       txtSageFemme.Text = "";
                        txtEpoux.Text = "";
                        dgvCertificat.Rows.Clear();
                    }
                }
            }
            catch(Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrer certificat", ex);
            }
        }
        int id, idPatient; string idEmploye;
        private void dgvVente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvCertificat.SelectedRows.Count > 0)
                {
                    id = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[0].Value.ToString());
                    idPatient = Convert.ToInt32(dgvCertificat.SelectedRows[0].Cells[1].Value.ToString());
                    txtEpoux.Text =  dgvCertificat.SelectedRows[0].Cells[3].Value.ToString();
                    txtPeriode.Text = dgvCertificat.SelectedRows[0].Cells[5].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dgvCertificat.SelectedRows[0].Cells[6].Value.ToString());
                    txtClient.Text = dgvCertificat.SelectedRows[0].Cells[2].Value.ToString();
                    txtSageFemme.Text = dgvCertificat.SelectedRows[0].Cells[4].Value.ToString();
                    txtFrais.Text = dgvCertificat.SelectedRows[0].Cells[7].Value.ToString();
                }
            }
            catch { }
        }

        CertificatDeGrossesse CreerUnCertificat()
        {


            if (!string.IsNullOrEmpty(txtEpoux.Text))
            {
                int periode;
                if (Int32.TryParse(txtPeriode.Text, out periode))
                {
                    if (!string.IsNullOrEmpty(txtSageFemme.Text))
                    {
                        var certificat = new CertificatDeGrossesse();
                         if (dataGridView1.SelectedRows.Count > 0)
                         {
                             var listPatient = ConnectionClassClinique.ListeDesPatients(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                             //certificat.ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                             certificat.IDPatiente = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                         }
                        else  if (dgvCertificat.SelectedRows.Count > 0)
                        {
                            certificat.ID = id;
                            certificat.IDPatiente = idPatient;
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
                         certificat.SageFemme = txtSageFemme.Text; ;                        
                        certificat.Epoux = txtEpoux.Text;
                        certificat.Periode = periode;
                        certificat.DateAccouchement = dateTimePicker1.Value.Date;

                        return certificat;

                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer le nom de la sage-femme ", "Erreur", "erreur.png");
                        return null;
                    }

                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour la période de grossesse", "Erreur", "erreur.png");
                    return null;
                }

            }
            else
            {
                MonMessageBox.ShowBox("Veuillez entrer le nom de l'epoux", "Erreur", "erreur.png");
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
                               on lc.IDPatiente equals lp.NumeroPatient
                               where lp.Nom.StartsWith(txtRecherche.Text, StringComparison.CurrentCultureIgnoreCase)
                               select new
                               {
                                   lc.ID,
                                   lc.IDPatiente,
                                   lc.Epoux,
                                   lc.Periode,
                                   lc.DateAccouchement,
                                   sageFemme = lc.SageFemme,
                                   lp.Nom,
                                   lp.Prenom,
                                   lc.FraisCertificat
                               };

                    dgvCertificat.Rows.Clear();
                    foreach (var p in list)
                    {
                        dgvCertificat.Rows.Add(p.ID, p.IDPatiente, p.Nom + " " + p.Prenom, p.Epoux, p.sageFemme,  p.Periode, p.DateAccouchement,p.FraisCertificat);
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox(" txtRecherche_KeyDown", ex);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtClient.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void txtPeriode_TextChanged(object sender, EventArgs e)
        {
            int periode;
            if (Int32.TryParse(txtPeriode.Text, out periode))
            {
                var month = 9 - periode;
                dateTimePicker1.Value = DateTime.Now.AddMonths(month);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCertificat.Rows.Count > 0)
                {
                    var certificat = CreerUnCertificat();
                    if (certificat != null)
                    {
                        certificat.ID = id;
                        if (MonMessageBox.ShowBox("Voulez vous vous modifier les données du certificat", "Confirmation", "confirmation.png") == "1")
                        {
                            if (ConnectionClassClinique.ModifierUnCertificatDeGrossesse(certificat))
                            {
                                txtPeriode.Text = "";
                                dataGridView1.Rows.Clear();
                                txtClient.Text = "";
                                txtSageFemme.Text = "";
                                txtEpoux.Text = "";
                                dgvCertificat.Rows.Clear();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new GestionDuneClinique.Formes.PatientFrm();
            frm.state = 1;
            frm.ShowDialog();
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
           
        }
      
    }
}
