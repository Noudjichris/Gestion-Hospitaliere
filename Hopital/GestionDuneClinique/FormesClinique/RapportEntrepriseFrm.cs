using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using System.Linq;

namespace GestionDuneClinique.Formes
{
    public partial class RapportEntrepriseFrm : Form
    {
        public RapportEntrepriseFrm()
        {
            InitializeComponent();
        }

        private void RapportEntreprise_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Blue, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
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
        private void RapportEntreprise_Load(object sender, EventArgs e)
        {
            
            //Column2.Width = 3 * dgvPatient.Width / 6;
            //Column4.Width =  dgvPatient.Width / 7;
            //cl1.Width = dataGridView1.Width / 6;
            //cl2.Width = dataGridView1.Width / 3;
            //cl3.Width = dataGridView1.Width / 3;
            //cl4.Width = dataGridView1.Width / 8;
            //cl5.Width = dataGridView1.Width / 8;
            button3.Location = new Point(Width - 50, 3);
            cll1.Width = dataGridView1.Width / 12;
            cll2.Width = dataGridView1.Width / 4;
            cll3.Width = dataGridView1.Width / 5;
            cll4.Width = dataGridView1.Width / 20;
            cll5.Width = dataGridView1.Width / 15;
            cll6.Width = dataGridView1.Width / 15;
            cll7.Width = dataGridView1.Width / 13;
            cll8.Width = dataGridView1.Width / 10;

            cl3.Width = cl3.Width - 30;
            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
            cmbEntreprise.Items.Clear();
            cmbEntreprise.Items.Add("<<TOUTES LES CONVENTIONNEES>>");
            foreach (Entreprise entreprise in listeEntrep)
            {
                cmbEntreprise.Items.Add(entreprise.NomEntreprise);
            }
        }

        private void cmbEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dgvPatient.Rows.Clear();
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises().Where(r => r.Rattache == cmbEntreprise.Text).Select(en=>en);
                foreach (var entrep in listeEntrep)
                {
                    dgvPatient.Rows.Add(
                             entrep.NomEntreprise,
                             "",
                             "",
                             "",
                             "", ""
                             );

                    var listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise("", entrep.NomEntreprise);
                    foreach (Patient patient in listePatient)
                    {
                        dgvPatient.Rows.Add(
                            patient.NumeroPatient,
                            patient.Nom + " " + patient.Prenom,
                            patient.Sexe,
                            patient.An,
                            patient.SousCouvert, patient.NomEntreprise
                            );

                    }
                }
            }
            else
            {
                var listePatient = new List<Patient>();
                if (cmbEntreprise.Text == "PCP MEMBRE FAMILLE PERSONNEL")
                {
                    dgvPatient.Rows.Clear();
                    var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);
                    var idEntrep = listeEntrep[0].NumeroEntreprise;
                    var listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep);
                    foreach (var ep in listeempl)
                    {
                        dgvPatient.Rows.Add(
                              ep.Matricule,
                              ep.Nom,
                              ep.Sexe,
                              ep.Age,
                              "", cmbEntreprise.Text
                              );
                    }
                }
                else
                {
                    if (cmbEntreprise.Text == "<<TOUTES LES CONVENTIONNEES>>")
                    {
                        listePatient = ConnectionClassClinique.ListeDesPatientsEntreprise();
                    }
                    else
                    {
                        listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise("", cmbEntreprise.Text);
                    }

                    ListeDespatients(listePatient);
                }
            }
        }
        

        void ListeDespatients(List<Patient> listePatient)
        {
            dgvPatient.Rows.Clear();

            foreach (Patient patient in listePatient)
            {
               // var listeEmp = ConnectionClassClinique.ListeDesEmployeesEntreprise(patient.Nom + " " + patient.Prenom);
               //var td ="";
               //if (listeEmp.Count > 0)
               //{
               //    if (!string.IsNullOrWhiteSpace(listeEmp[0].Telephone))
               //    {
               //        td = " / " + listeEmp[0].Telephone;
               //    }
               //}

                dgvPatient.Rows.Add(
                    patient.NumeroPatient,
                    patient.Nom+" "+ patient.Prenom ,
                    patient.Sexe, 
                    patient.An,
                    patient.SousCouvert,patient.NomEntreprise
                    );

            }
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        string etatImpression;
        private void button1_Click(object sender, EventArgs e)
        {
                label12.Text = "";
                label13.Visible = false;
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                dataGridView2.Rows.Clear();
                dataGridView1.Rows.Clear();
                try
                {
                    if (checkBox1.Checked)
                    {
                        var montantTotal = 0.0;
                        var totalPharmacie = .0;
                        var totalConsulation = .0;
                        var totalActe = .0;
                        var totalParConventionne = .0;
                        dataGridView1.Rows.Clear();
                        var rowCount = 0;
                        var indexConventionne = 0;
                        foreach (DataGridViewRow dtGrid in dgvPatient.Rows)
                        {
                            var nomPatient = "";
                            int idPatient;
                            if (dtGrid.Cells[4].Value.ToString() != "")
                            {
                                nomPatient = dtGrid.Cells[1].Value.ToString() + " s/c " + dtGrid.Cells[4].Value.ToString();
                            }
                            else
                            {
                                nomPatient = dtGrid.Cells[1].Value.ToString();
                            }

                            if (Int32.TryParse(dtGrid.Cells[0].Value.ToString(), out idPatient))
                            {
                                var dtFacture = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                var entreprise = dtGrid.Cells[5].Value.ToString();

                                var dtCredit = ConnectionClassPharmacie.ListeDesCredit(idPatient, dtGrid.Cells[1].Value.ToString(), entreprise, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                var total = 0.0;
                                foreach (DataRow dtRow in dtFacture.Rows)
                                {
                                    total += Double.Parse(dtRow.ItemArray[6].ToString());
                                    montantTotal += Double.Parse(dtRow.ItemArray[6].ToString());
                                    totalParConventionne += Double.Parse(dtRow.ItemArray[6].ToString());
                                    if (dtRow.ItemArray[7].ToString() == "EXAMEN")
                                    {
                                        totalActe += Double.Parse(dtRow.ItemArray[6].ToString());
                                    }
                                    else
                                    {
                                        totalConsulation += Double.Parse(dtRow.ItemArray[6].ToString());
                                    }
                                }
                                foreach (DataRow dtRow in dtCredit.Rows)
                                {
                                    total += Double.Parse(dtRow.ItemArray[7].ToString());
                                    totalPharmacie += Double.Parse(dtRow.ItemArray[7].ToString());
                                    montantTotal += Double.Parse(dtRow.ItemArray[7].ToString());
                                    totalParConventionne += Double.Parse(dtRow.ItemArray[7].ToString());
                                }
                                RapportTotalGrouperParConventionne(dtFacture, dtCredit, total, nomPatient);
                                var count = dataGridView3.Rows.Count;

                                if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value.ToString().ToUpper() != "SOUS TOTAL")
                                { }
                                else
                                {

                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                                }
                            }
                            else
                            {
                                if (dataGridView1.Rows.Count == 0)
                                {
                                    dataGridView1.Rows.Add("", dtGrid.Cells[0].Value.ToString(),"", "",  "", "","", "");
                                }
                                else
                                {
                                    dataGridView1.Rows.Add("", "TOTAL " + dgvPatient.Rows[rowCount - indexConventionne].Cells[0].Value.ToString(), "", "", "", "", string.Format(elGR, "{0:0,0}", totalParConventionne), "");
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                                    dataGridView1.Rows.Add("", dtGrid.Cells[0].Value.ToString(), "", "", "", "", "", "");
                                    totalParConventionne = 0;
                                    indexConventionne = 0;
                               }
                               
                            }
                            if (rowCount == dgvPatient.Rows.Count - 1)
                            {
                                dataGridView1.Rows.Add("", "TOTAL " + dgvPatient.Rows[rowCount - indexConventionne].Cells[0].Value.ToString(), "", "", "", "", string.Format(elGR, "{0:0,0}", totalParConventionne), "");
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            }
                            rowCount++;
                            indexConventionne++;
                        }
                        lblTotal.Text = string.Format(elGR, "{0:0,0}", montantTotal);
                        lblTotalConsultation.Text = string.Format(elGR, "{0:0,0}", totalConsulation);
                        lblTotalActes.Text = string.Format(elGR, "{0:0,0}", totalActe);
                        lblTotalPharmacie.Text = string.Format(elGR, "{0:0,0}", totalPharmacie);
                        dataGridView3.Rows.Add("TOTAL", montantTotal);
                        etatImpression = "1";
                        dataGridView1.Rows.Add(
                                  "",
                                  "TOTAL GENERAL",
                                 "",
                                 "",
                                  "",
                                  "",
                                string.Format(elGR, "{0:0,0}", montantTotal),
                                ""
                                  );

                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                    else
                    {
                        if (cmbEntreprise.Text == "PCP MEMBRE FAMILLE PERSONNEL")
                        {
                            RapportTotalGrouperParPatientPCP();
                        }
                        else
                        {
                            var montantTotal = 0.0;
                            var totalPharmacie = .0;
                            var totalConsulation = .0;
                            var totalActe = .0;
                            dataGridView1.Rows.Clear();
                            foreach (DataGridViewRow dtGrid in dgvPatient.Rows)
                            {
                                var nomPatient = "";
                                int idPatient;
                                if (dtGrid.Cells[4].Value.ToString() != "")
                                {
                                    nomPatient = dtGrid.Cells[1].Value.ToString() + " s/c " + dtGrid.Cells[4].Value.ToString();
                                }
                                else
                                {
                                    nomPatient = dtGrid.Cells[1].Value.ToString();
                                }

                                if (Int32.TryParse(dtGrid.Cells[0].Value.ToString(), out idPatient))
                                {
                                    var dtFacture = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                    var entreprise = cmbEntreprise.Text;
                                    if (entreprise == "<<TOUTES LES CONVENTIONNEES>>")
                                    {
                                        entreprise = dtGrid.Cells[5].Value.ToString();
                                    }
                                    var dtCredit = ConnectionClassPharmacie.ListeDesCredit(idPatient, dtGrid.Cells[1].Value.ToString(), entreprise, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                    var total = 0.0;
                                    foreach (DataRow dtRow in dtFacture.Rows)
                                    {
                                        total += Double.Parse(dtRow.ItemArray[6].ToString());
                                        montantTotal += Double.Parse(dtRow.ItemArray[6].ToString());
                                        if (dtRow.ItemArray[7].ToString() == "EXAMEN")
                                        {
                                            totalActe += Double.Parse(dtRow.ItemArray[6].ToString());
                                        }
                                        else
                                        {
                                            totalConsulation += Double.Parse(dtRow.ItemArray[6].ToString());
                                        }
                                    }
                                    foreach (DataRow dtRow in dtCredit.Rows)
                                    {
                                        total += Double.Parse(dtRow.ItemArray[7].ToString());
                                        totalPharmacie += Double.Parse(dtRow.ItemArray[7].ToString());
                                        montantTotal += Double.Parse(dtRow.ItemArray[7].ToString());
                                    }
                                    RapportTotalGrouperParPatient(dtFacture, dtCredit, total, nomPatient);
                                    var count = dataGridView3.Rows.Count;
                                    if (dataGridView1.Rows.Count > 0)
                                    {
                                        if (dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value.ToString().ToUpper() != "SOUS TOTAL")
                                        { }
                                        else
                                        {

                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                                        }
                                    }
                                }
                            }
                            lblTotal.Text = string.Format(elGR, "{0:0,0}", montantTotal);
                            lblTotalConsultation.Text = string.Format(elGR, "{0:0,0}", totalConsulation);
                            lblTotalActes.Text = string.Format(elGR, "{0:0,0}", totalActe);
                            lblTotalPharmacie.Text = string.Format(elGR, "{0:0,0}", totalPharmacie);
                            dataGridView3.Rows.Add("TOTAL", montantTotal);
                            dataGridView1.Rows.Add(
                                      "",
                                      "TOTAL",
                                     "",
                                     "",
                                      "",
                                      "",
                                    string.Format(elGR, "{0:0,0}", montantTotal),
                                    ""
                                      );

                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;
                        }
                        etatImpression = "1";
                    }
                }
                catch (Exception ex)
                {
                    MonMessageBox.ShowBox("Rapport entreprise", ex);
                }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        Bitmap rapportBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportBitmap, -10, 20, rapportBitmap.Width, rapportBitmap.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        int j= 1;
        private void btnApercu_Click(object sender, EventArgs e)
        {
            try
            {
                if (etatImpression == "1")
                {
                    var reduction = 0.0;
                    if (double.TryParse(label12.Text, out reduction))
                    {
                    }

                    if (dataGridView1.Rows.Count > 0 && cmbEntreprise.Text != "")
                    {
                        var listeEntreprise = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);

                        var entreprise = new Entreprise(listeEntreprise[0].NumeroEntreprise, listeEntreprise[0].NomEntreprise, listeEntreprise[0].Telephone1,
                            listeEntreprise[0].Telephone2, listeEntreprise[0].Email, listeEntreprise[0].Adresse, listeEntreprise[0].DateAbonnement, listeEntreprise[0].PrixHonoraire, listeEntreprise[0].Rattache);
                        var rowCount = dataGridView1.Rows.Count;
                      
                        #region MyRegion

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                        var jour = DateTime.Now.Day;
                        var mois = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                        var pathFolder = "C:\\Dossier Clinique";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        pathFolder = pathFolder + "\\Rapport des conventionnees";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        sfd.InitialDirectory = pathFolder;
                        sfd.FileName =  "Rapport de "+cmbEntreprise.Text +" _imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            rapportBitmap = Impression.RapportDuneEntreprise(dataGridView1, entreprise, dtp2.Value, Double.Parse(lblTotal.Text),
                                Convert.ToDecimal(lblTotalConsultation.Text), Convert.ToDecimal(lblTotalActes.Text),Convert.ToDecimal(lblTotalPharmacie.Text), reduction);

                            var inputImage = @"cdali" ;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(rapportBitmap, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -5, 0, pageIndex.height, pageIndex.width);
                            if (rowCount > 36)
                            {
                                var Count = (dataGridView1.Rows.Count - 36) / 45;

                                for (var i = 0; i <= Count; i++)
                                {
                                    if (i * 45 < dataGridView1.Rows.Count)
                                    {
                                        rapportBitmap = Impression.RapportDuneEntrepriseParPage(dataGridView1, entreprise, dtp2.Value, Double.Parse(lblTotal.Text),
                                            Convert.ToDecimal(lblTotalConsultation.Text), Convert.ToDecimal(lblTotalActes.Text),Convert.ToDecimal(lblTotalPharmacie.Text), reduction, i);

                                         inputImage = @"cdali" + i;
                                        // Create an empty page
                                         pageIndex = document.addPage();

                                        document.addImageReference(rapportBitmap, inputImage);
                                         img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -5, 0, pageIndex.height, pageIndex.width);
                                    }
                                }

                            }
                        }
                        #endregion
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    
                }
                else if (etatImpression == "2")
                {
                    button2_Click(null, null);
                }
                else if (etatImpression == "3")
                {
                    ImprimerRapportAvecCharge();
                }
                else if (etatImpression == "4")
                {
                    ImprimerRapportParPersonnelConventionne();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Impression", ex);
            }
            
        }

        private void printPreviewDialog1_Load_1(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void ImprimerRapportParPersonnelConventionne()
        {
            try
            {
                if (cmbEntreprise.Text != "")
                {
                    if (dgvPatient.SelectedRows.Count > 0)
                    {
                        var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                           where p.NumeroPatient == (Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString()))
                                           select p;
                        var patient = new Patient();
                        foreach (var p in listePatient)
                            patient = p;
                        if (dataGridView1.Rows.Count > 0)
                        {
                            #region MyRegion

                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                            sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                            var jour = DateTime.Now.Day;
                            var mois = DateTime.Now.Month;
                            var year = DateTime.Now.Year;
                            var hour = DateTime.Now.Hour;
                            var min = DateTime.Now.Minute;
                            var sec = DateTime.Now.Second;
                            var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                            var pathFolder = "C:\\Dossier Clinique";
                            if (!System.IO.Directory.Exists(pathFolder))
                            {
                                System.IO.Directory.CreateDirectory(pathFolder);
                            }
                            pathFolder = pathFolder + "\\Rapport des conventionnees";
                            if (!System.IO.Directory.Exists(pathFolder))
                            {
                                System.IO.Directory.CreateDirectory(pathFolder);
                            }
                            sfd.InitialDirectory = pathFolder;
                            sfd.FileName = "Rapport de " + cmbEntreprise.Text + " _imprimé_le_" + date + ".pdf";

                            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                if (dataGridView1.Rows.Count > 0)
                                {
                                    var Count = (dataGridView1.Rows.Count) / 28;

                                    for (var i = 0; i <= Count; i++)
                                    {
                                        if (i * 28 < dataGridView1.Rows.Count)
                                        {
                                            rapportBitmap = Impression.FactureDependantsEmployeDuneEntreprise("FACTURE", patient, cmbEntreprise.Text, Convert.ToDecimal(lblTotal.Text),
                                                Convert.ToDecimal(lblTotalConsultation.Text), Convert.ToDecimal(lblTotalActes.Text), Convert.ToDecimal(lblTotalPharmacie.Text), GestionAcademique.LoginFrm.nom, dataGridView1, i);

                                            var inputImage = @"cdali" + i;
                                            // Create an empty page
                                            sharpPDF.pdfPage pageIndex = document.addPage();

                                            document.addImageReference(rapportBitmap, inputImage);
                                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                            pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                        }
                                    }

                                }
                            }
                            #endregion
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture employe ", ex);
            }
        }   
        //static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
      
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEntreprise.Text != "")
                {
                    if (dgvPatient.SelectedRows.Count > 0)
                    {
                        var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                           where p.NumeroPatient == (Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString()))
                                           select p;
                        var patient = new Patient();
                        foreach (var p in listePatient)
                            patient = p;
                        if (dataGridView1.Rows.Count > 0)
                        {
                        #region MyRegion

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                        var jour = DateTime.Now.Day;
                        var mois = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                        var pathFolder = "C:\\Dossier Clinique";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        pathFolder = pathFolder + "\\Rapport des conventionnees";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        sfd.InitialDirectory = pathFolder;
                        sfd.FileName =  "Rapport de "+cmbEntreprise.Text +" _imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (dataGridView1.Rows.Count > 0)
                            {
                                var Count = (dataGridView1.Rows.Count ) / 28;

                                for (var i = 0; i <= Count; i++)
                                {
                                    if (i * 28 < dataGridView1.Rows.Count)
                                    {
                                        rapportBitmap = Impression.FactureEmployeDuneEntreprise("FACTURE", patient, cmbEntreprise.Text, Convert.ToDecimal(lblTotal.Text),
                                              Convert.ToDecimal(lblTotalConsultation.Text), Convert.ToDecimal(lblTotalActes.Text), Convert.ToDecimal(lblTotalPharmacie.Text),GestionAcademique.LoginFrm.nom, dataGridView1, i);
                                   
                                        var inputImage = @"cdali" + i;
                                        // Create an empty page
                                        sharpPDF.pdfPage pageIndex = document.addPage();

                                        document.addImageReference(rapportBitmap, inputImage);
                                        sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                    }
                                }

                            }
                        }
                        #endregion
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture employe ", ex);
            }
        }   
        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
      //facture honraire
        private void button4_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            label13.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();
            try
            {
                var totalPharmacie = .0;
                var totalConsulation = .0;
                var totalActe = .0;
                dataGridView1.Rows.Clear();
                foreach (DataGridViewRow dtGrid in dgvPatient.SelectedRows)
                {
                    var nomPatient = "";
                    int idPatient;
                    if (dtGrid.Cells[4].Value.ToString() != "")
                    {
                        nomPatient = dtGrid.Cells[1].Value.ToString() + " s/c " + dtGrid.Cells[4].Value.ToString();
                    }
                    else
                    {
                        nomPatient = dtGrid.Cells[1].Value.ToString();
                    }

                    if (Int32.TryParse(dtGrid.Cells[0].Value.ToString(), out idPatient))
                    {
                        var dtFacture = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                        var dtCredit = ConnectionClassPharmacie.ListeDesCredit(idPatient,dtGrid.Cells[1].Value.ToString(), cmbEntreprise.Text, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                        var total = 0.0;
                        foreach (DataRow dtRow in dtFacture.Rows)
                        {
                            total += Double.Parse(dtRow.ItemArray[6].ToString());
                            if (dtRow.ItemArray[7].ToString() == "EXAMEN")
                            {
                                totalActe += Double.Parse(dtRow.ItemArray[6].ToString());
                            }
                            else
                            {
                                totalConsulation += Double.Parse(dtRow.ItemArray[6].ToString());
                            }
                        }
                        foreach (DataRow dtRow in dtCredit.Rows)
                        {
                            totalPharmacie += Double.Parse(dtRow.ItemArray[7].ToString());
                            total += Double.Parse(dtRow.ItemArray[7].ToString());
                        }
                        RapportTotalGrouperParPatient(dtFacture, dtCredit, total, nomPatient);
                        var count = dataGridView3.Rows.Count;
                    }
                }

                dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count - 1]);
                var montantTotal = 0.0;

                foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                {
                    double total;
                     if (Double.TryParse(dtRow.Cells[6].Value.ToString(), out total))
                        {
                            montantTotal += total;
                        }
                }
                lblTotal.Text = string.Format(elGR, "{0:0,0}", montantTotal);
                lblTotalConsultation.Text = string.Format(elGR, "{0:0,0}", totalConsulation);
                lblTotalActes.Text = string.Format(elGR, "{0:0,0}", totalActe);
                lblTotalPharmacie.Text = string.Format(elGR, "{0:0,0}", totalPharmacie);
                etatImpression = "2";
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (cmbEntreprise.Text == "PCP MEMBRE FAMILLE PERSONNEL")
            {
                dgvPatient.Rows.Clear();
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);
                var idEntrep = listeEntrep[0].NumeroEntreprise;
                var listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep,textBox1.Text);
                foreach (var ep in listeempl)
                {
                    dgvPatient.Rows.Add(
                          ep.Matricule,
                          ep.Nom,
                          ep.Sexe,
                          ep.Age,
                          "", cmbEntreprise.Text
                          );
                }
            }
            else
            {
                var listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise(textBox1.Text, cmbEntreprise.Text);
                ListeDespatients(listePatient);
            }
        }

        private void ImprimerRapportAvecCharge()
        {
            try
            {
                double charge;
                if(double.TryParse(textBox2.Text, out charge))
                {
                if (dataGridView2.Rows.Count > 0 && cmbEntreprise.Text != "")
                {
                    var listeEntreprise = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);

                    var entreprise = new Entreprise(listeEntreprise[0].NumeroEntreprise, listeEntreprise[0].NomEntreprise, listeEntreprise[0].Telephone1,
                        listeEntreprise[0].Telephone2, listeEntreprise[0].Email, listeEntreprise[0].Adresse, listeEntreprise[0].DateAbonnement, listeEntreprise[0].PrixHonoraire, listeEntreprise[0].Rattache);
                    var rowCount = dataGridView1.Rows.Count;

                    #region MyRegion
                     SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                        var jour = DateTime.Now.Day;
                        var mois = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                        var pathFolder = "C:\\Dossier Clinique";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        pathFolder = pathFolder + "\\Rapport des conventionnees";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        sfd.InitialDirectory = pathFolder;
                        sfd.FileName =  "Rapport de "+cmbEntreprise.Text +" _imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            rapportBitmap = Impression.RapportDuneEntrepriseAvecCharge(dataGridView2, entreprise, dtp2.Value, Double.Parse(lblTotal.Text), charge);
                            var inputImage = @"cdali";
                            // Create an empty page
                            sharpPDF.pdfPage page = document.addPage(450, 700);

                            document.addImageReference(rapportBitmap, inputImage);
                            sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage);
                            page.addImage(img, 0, 0, page.height, page.width);
                            if (rowCount > 13)
                            {
                                var Count = (dataGridView1.Rows.Count - 13) / 25;

                                for (var i = 0; i <= Count; i++)
                                {
                                    if (i * 25 < dataGridView1.Rows.Count)
                                    {
                                        rapportBitmap = Impression.RapportDuneEntrepriseParPageAvecCharge(dataGridView2,  Double.Parse(lblTotal.Text), i,charge);
                          
                                         inputImage = @"cdali" + i;
                                        // Create an empty page
                                         sharpPDF.pdfPage page1 = document.addPage(450, 700);
                                         document.addImageReference(rapportBitmap, inputImage);
                                          img = document.getImageReference(inputImage);
                                         page1.addImage(img, 0, 0, page1.height, page1.width);
                                    }
                                }

                            }
                        }
                        #endregion
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Impression", ex);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(null, null);
            }
        }

        void RapportTotalGrouperParPatient(DataTable dtFacture, DataTable dtPharm, double total,string nomPatient)
        {
            try
            {
                #region Facture
                if (dtFacture.Rows.Count == 1)
                {
                    dataGridView1.Rows.Add(
                                dtFacture.Rows[0].ItemArray[0].ToString(),
                                nomPatient.ToUpper(),
                                DateTime.Parse(dtFacture.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                dtFacture.Rows[0].ItemArray[3].ToString().ToUpper(),
                             dtFacture.Rows[0].ItemArray[5].ToString(),
                              string.Format(elGR, "{0:0,0}",  double.Parse(dtFacture.Rows[0].ItemArray[4].ToString())),
                                           string.Format(elGR, "{0:0,0}",  double.Parse(dtFacture.Rows[0].ItemArray[6].ToString())),
                                           dtFacture.Rows[0].ItemArray[2].ToString()
                                );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
                else if (dtFacture.Rows.Count >1)
                {
                    dataGridView1.Rows.Add(
                              dtFacture.Rows[0].ItemArray[0].ToString(),
                              nomPatient.ToUpper(),
                              DateTime.Parse(dtFacture.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                              dtFacture.Rows[0].ItemArray[3].ToString().ToUpper(),
                                   dtFacture.Rows[0].ItemArray[5].ToString(),
     string.Format(elGR, "{0:0,0}",  double.Parse(dtFacture.Rows[0].ItemArray[4].ToString())),
                                   string.Format(elGR, "{0:0,0}",  double.Parse(dtFacture.Rows[0].ItemArray[6].ToString())),
                              dtFacture.Rows[0].ItemArray[2].ToString()
                              );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    for (var i = 1; i < dtFacture.Rows.Count; i++)
                    {

                        dataGridView1.Rows.Add(
                               dtFacture.Rows[i].ItemArray[0].ToString(),
                               "",
                               DateTime.Parse(dtFacture.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                               dtFacture.Rows[i].ItemArray[3].ToString().ToUpper(),
                                  dtFacture.Rows[i].ItemArray[5].ToString(),
                                    string.Format(elGR, "{0:0,0}",  double.Parse(dtFacture.Rows[i].ItemArray[4].ToString())),
                                    string.Format(elGR, "{0:0,0}",  double.Parse(dtFacture.Rows[i].ItemArray[6].ToString())),
                                    dtFacture.Rows[i].ItemArray[2].ToString()
                               );
                        Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    }
                }
                #endregion
                #region Pharmacie
                if (dtPharm.Rows.Count == 1)
                {

                    if (dtFacture.Rows.Count > 0)
                    {
                        nomPatient = "";
                    }
                    else
                    {
                        nomPatient = dtPharm.Rows[0].ItemArray[8].ToString();
                    }

                    dataGridView1.Rows.Add(
                        dtPharm.Rows[0].ItemArray[0].ToString(),
                        nomPatient.ToUpper(),
                         DateTime.Parse(dtPharm.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                        dtPharm.Rows[0].ItemArray[4].ToString(),
                           dtPharm.Rows[0].ItemArray[5].ToString(),
                             string.Format(elGR, "{0:0,0}",  double.Parse(dtPharm.Rows[0].ItemArray[6].ToString())),
                             string.Format(elGR, "{0:0,0}",  double.Parse(dtPharm.Rows[0].ItemArray[7].ToString())),
                        dtPharm.Rows[0].ItemArray[3].ToString()

                        );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
                else if (dtPharm.Rows.Count > 1)
                {
                    if (dtFacture.Rows.Count > 0)
                    {
                        nomPatient = "";
                    }
                    else
                    {
                        nomPatient = dtPharm.Rows[0].ItemArray[8].ToString();
                    }
                    dataGridView1.Rows.Add(
                        dtPharm.Rows[0].ItemArray[0].ToString(),
                        nomPatient.ToUpper(),
                         DateTime.Parse(dtPharm.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                        dtPharm.Rows[0].ItemArray[4].ToString(),
                        dtPharm.Rows[0].ItemArray[5].ToString(),
                             string.Format(elGR, "{0:0,0}",  double.Parse(dtPharm.Rows[0].ItemArray[6].ToString())),
                             string.Format(elGR, "{0:0,0}",  double.Parse(dtPharm.Rows[0].ItemArray[7].ToString())),
                        dtPharm.Rows[0].ItemArray[3].ToString()

                        );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;

                    for (var m = 1; m < dtPharm.Rows.Count; m++)
                    {
                        dataGridView1.Rows.Add(
                        dtPharm.Rows[m].ItemArray[0].ToString(),
                        "",
                         DateTime.Parse(dtPharm.Rows[m].ItemArray[1].ToString()).ToShortDateString(),
                        dtPharm.Rows[m].ItemArray[4].ToString(),
                        dtPharm.Rows[m].ItemArray[5].ToString(),
                             string.Format(elGR, "{0:0,0}",  double.Parse(dtPharm.Rows[m].ItemArray[6].ToString())),
                             string.Format(elGR, "{0:0,0}",  double.Parse(dtPharm.Rows[m].ItemArray[7].ToString())),
                        dtPharm.Rows[m].ItemArray[3].ToString()

                        );
                        Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
                #endregion
                }
                var count = dtFacture.Rows.Count + dtPharm.Rows.Count;

                if (count > 0)
                {
                    dataGridView3.Rows.Add(nomPatient.ToUpper(), total);
                    //dataGridView1.Rows.Add(
                    //         "",
                    //        "",
                    //         "",
                    //        "SOUS ",
                    //         "",
                    //        total
                    //         );
                    dataGridView1.Rows.Add(
                             "",
                            "SOUS TOTAL", "",
                             "",
                             "",
                            "",
                                string.Format(elGR, "{0:0,0}",  total),
                    ""
                             );
                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }
        }

        void RapportTotalGrouperParPatientPCP()
        {
            try
            {
                var Total = .0;
                var totalPharmacie = .0;
                var totalConsulation = .0;
                var totalActe = .0;
                for (var i = 0; i < dgvPatient.Rows.Count; i++)
                {
                    var sousTotalEmploye = .0; var countE = 0;
                    var dtPatient = ConnectionClassClinique.ListeDesPatientsParEntreprise(dgvPatient.Rows[i].Cells[1].Value.ToString());
                    if (dtPatient.Rows.Count > 0)
                    {

                        var dtG = ConnectionClassClinique.TableDesDetailsFacturesProformaPCP(dgvPatient.Rows[i].Cells[0].Value.ToString(), dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                        var count = dtG.Rows.Count;
                       
                        for (var w = 0; w < dtPatient.Rows.Count; w++)
                        {
                            var dtPharm = AppCode.ConnectionClassPharmacie.ListeDesCredit(dtPatient.Rows[w].ItemArray[1].ToString() + " " +
                                dtPatient.Rows[w].ItemArray[2].ToString(), dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                            count += dtPharm.Rows.Count; 
                        }

                        if (count > 0)
                        {
                            dataGridView1.Rows.Add("",
                               dgvPatient.Rows[i].Cells[1].Value.ToString() + " /  " + dgvPatient.Rows[i].Cells[0].Value.ToString(),
                              "",
                               "",
                               "",
                              "",
                              "",
                                  "", "");
                            for (var ii = 0; ii < dtPatient.Rows.Count; ii++)
                            {
                                var sousTotal = .0;
                                var nomPatient = dtPatient.Rows[ii].ItemArray[1].ToString() + " " + dtPatient.Rows[ii].ItemArray[2].ToString();
                                var nomEntreprise = dtPatient.Rows[ii].ItemArray[6].ToString();
                                var dtProCaisse = ConnectionClassClinique.TableDesDetailsFacturesProformaPCP(Int32.Parse(dtPatient.Rows[ii].ItemArray[0].ToString()), dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                var dtPharm = AppCode.ConnectionClassPharmacie.ListeDesCredit(nomPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                           
                                if (nomEntreprise == "PCP MEMBRE FAMILLE PERSONNEL")
                                {
                                    for (var j = 0; j < dtProCaisse.Rows.Count; j++)
                                    {
                                        dataGridView1.Rows.Add(
                       dtProCaisse.Rows[j].ItemArray[0].ToString(),
                       nomPatient.ToUpper(),
                       DateTime.Parse(dtProCaisse.Rows[j].ItemArray[1].ToString()).ToShortDateString(),
                       dtProCaisse.Rows[j].ItemArray[3].ToString().ToUpper(),
                            dtProCaisse.Rows[j].ItemArray[5].ToString(),
                            string.Format(elGR, "{0:0,0}", double.Parse(dtProCaisse.Rows[j].ItemArray[4].ToString())),
                                                        string.Format(elGR, "{0:0,0}", double.Parse(dtProCaisse.Rows[j].ItemArray[6].ToString())),
                                                   dtProCaisse.Rows[j].ItemArray[2].ToString());
                                        sousTotal += double.Parse(dtProCaisse.Rows[j].ItemArray[6].ToString());
                                        sousTotalEmploye += double.Parse(dtProCaisse.Rows[j].ItemArray[6].ToString());
                                        Total += double.Parse(dtProCaisse.Rows[j].ItemArray[6].ToString());
                                        if (dtProCaisse.Rows[j].ItemArray[7].ToString() == "EXAMEN")
                                        {
                                            totalActe += Double.Parse(dtProCaisse.Rows[j].ItemArray[6].ToString());
                                        }
                                        else
                                        {
                                            totalConsulation += Double.Parse(dtProCaisse.Rows[j].ItemArray[6].ToString());
                                        }
                                    }

                                    for (var k = 0; k < dtPharm.Rows.Count; k++)
                                    {
                                        dataGridView1.Rows.Add(
               dtPharm.Rows[k].ItemArray[0].ToString(),
               nomPatient.ToUpper(),
                DateTime.Parse(dtPharm.Rows[k].ItemArray[1].ToString()).ToShortDateString(),
               dtPharm.Rows[k].ItemArray[4].ToString(),
               dtPharm.Rows[k].ItemArray[5].ToString(),
                    string.Format(elGR, "{0:0,0}", double.Parse(dtPharm.Rows[k].ItemArray[6].ToString())),
                    string.Format(elGR, "{0:0,0}", double.Parse(dtPharm.Rows[k].ItemArray[7].ToString())),
               dtPharm.Rows[k].ItemArray[3].ToString());
                                        sousTotal += double.Parse(dtPharm.Rows[k].ItemArray[7].ToString());
                                        sousTotalEmploye += double.Parse(dtPharm.Rows[k].ItemArray[7].ToString());
                                        Total += double.Parse(dtPharm.Rows[k].ItemArray[7].ToString());
                                        totalPharmacie += double.Parse(dtPharm.Rows[k].ItemArray[7].ToString());
                                    }

                                    countE += dtProCaisse.Rows.Count + dtPharm.Rows.Count;
                                }
                                if (dtProCaisse.Rows.Count + dtPharm.Rows.Count  > 0)
                                    dataGridView1.Rows.Add("",
                                                            "SOUS TOTAL",
                                                            "",
                                                             "",
                                                             "",
                                                            "",

                                                              string.Format(elGR, "{0:0,0}", sousTotal), "");  //dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            }
                        }
                    } 
                    if (countE > 0)
                        dataGridView1.Rows.Add("",
                                           "TOTAL " + dgvPatient.Rows[i].Cells[1].Value.ToString() + " /  " + dgvPatient.Rows[i].Cells[0].Value.ToString(),
                                           "",
                                            "",
                                            "",
                                           "",

                                             string.Format(elGR, "{0:0,0}", sousTotalEmploye), "");
                }
               
                dataGridView1.Rows.Add("",
                                   "TOTAL",
                                   "",
                                    "",
                                    "",
                                   "",

                                     string.Format(elGR, "{0:0,0}", Total), "");
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.YellowGreen;
                lblTotal.Text = string.Format(elGR, "{0:0,0}", Total);
                lblTotalConsultation.Text = string.Format(elGR, "{0:0,0}", totalConsulation);
                lblTotalActes.Text = string.Format(elGR, "{0:0,0}", totalActe);
                lblTotalPharmacie.Text = string.Format(elGR, "{0:0,0}", totalPharmacie);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }
        }

        void RapportTotalGrouperParConventionne(DataTable dtFacture, DataTable dtPharm, double total, string nomPatient)
        {
            try
            {
                #region Facture
                if (dtFacture.Rows.Count == 1)
                {
                    dataGridView1.Rows.Add(
                                dtFacture.Rows[0].ItemArray[0].ToString(),
                                nomPatient.ToUpper(),
                                DateTime.Parse(dtFacture.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                dtFacture.Rows[0].ItemArray[3].ToString().ToUpper(),
                             dtFacture.Rows[0].ItemArray[5].ToString(),
                              string.Format(elGR, "{0:0,0}", double.Parse(dtFacture.Rows[0].ItemArray[4].ToString())),
                                           string.Format(elGR, "{0:0,0}", double.Parse(dtFacture.Rows[0].ItemArray[6].ToString())),
                                           dtFacture.Rows[0].ItemArray[2].ToString()
                                );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
                else if (dtFacture.Rows.Count > 1)
                {
                    dataGridView1.Rows.Add(
                              dtFacture.Rows[0].ItemArray[0].ToString(),
                              nomPatient.ToUpper(),
                              DateTime.Parse(dtFacture.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                              dtFacture.Rows[0].ItemArray[3].ToString().ToUpper(),
                                   dtFacture.Rows[0].ItemArray[5].ToString(),
     string.Format(elGR, "{0:0,0}", double.Parse(dtFacture.Rows[0].ItemArray[4].ToString())),
                                   string.Format(elGR, "{0:0,0}", double.Parse(dtFacture.Rows[0].ItemArray[6].ToString())),
                              dtFacture.Rows[0].ItemArray[2].ToString()
                              );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    for (var i = 1; i < dtFacture.Rows.Count; i++)
                    {

                        dataGridView1.Rows.Add(
                               dtFacture.Rows[i].ItemArray[0].ToString(),
                               "",
                               DateTime.Parse(dtFacture.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                               dtFacture.Rows[i].ItemArray[3].ToString().ToUpper(),
                                  dtFacture.Rows[i].ItemArray[5].ToString(),
                                    string.Format(elGR, "{0:0,0}", double.Parse(dtFacture.Rows[i].ItemArray[4].ToString())),
                                    string.Format(elGR, "{0:0,0}", double.Parse(dtFacture.Rows[i].ItemArray[6].ToString())),
                                    dtFacture.Rows[i].ItemArray[2].ToString()
                               );
                        Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    }
                }
                #endregion
                #region Pharmacie
                if (dtPharm.Rows.Count == 1)
                {

                    if (dtFacture.Rows.Count > 0)
                    {
                        nomPatient = "";
                    }
                    else
                    {
                        nomPatient = dtPharm.Rows[0].ItemArray[8].ToString();
                    }

                    dataGridView1.Rows.Add(
                        dtPharm.Rows[0].ItemArray[0].ToString(),
                        nomPatient.ToUpper(),
                         DateTime.Parse(dtPharm.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                        dtPharm.Rows[0].ItemArray[4].ToString(),
                           dtPharm.Rows[0].ItemArray[5].ToString(),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtPharm.Rows[0].ItemArray[6].ToString())),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtPharm.Rows[0].ItemArray[7].ToString())),
                        dtPharm.Rows[0].ItemArray[3].ToString()

                        );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
                else if (dtPharm.Rows.Count > 1)
                {
                    if (dtFacture.Rows.Count > 0)
                    {
                        nomPatient = "";
                    }
                    else
                    {
                        nomPatient = dtPharm.Rows[0].ItemArray[8].ToString();
                    }
                    dataGridView1.Rows.Add(
                        dtPharm.Rows[0].ItemArray[0].ToString(),
                        nomPatient.ToUpper(),
                         DateTime.Parse(dtPharm.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                        dtPharm.Rows[0].ItemArray[4].ToString(),
                        dtPharm.Rows[0].ItemArray[5].ToString(),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtPharm.Rows[0].ItemArray[6].ToString())),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtPharm.Rows[0].ItemArray[7].ToString())),
                        dtPharm.Rows[0].ItemArray[3].ToString()

                        );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;

                    for (var m = 1; m < dtPharm.Rows.Count; m++)
                    {
                        dataGridView1.Rows.Add(
                        dtPharm.Rows[m].ItemArray[0].ToString(),
                        "",
                         DateTime.Parse(dtPharm.Rows[m].ItemArray[1].ToString()).ToShortDateString(),
                        dtPharm.Rows[m].ItemArray[4].ToString(),
                        dtPharm.Rows[m].ItemArray[5].ToString(),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtPharm.Rows[m].ItemArray[6].ToString())),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtPharm.Rows[m].ItemArray[7].ToString())),
                        dtPharm.Rows[m].ItemArray[3].ToString()

                        );
                        Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    }
                #endregion
                }
                var count = dtFacture.Rows.Count + dtPharm.Rows.Count;

                if (count > 0)
                {
                    dataGridView3.Rows.Add(nomPatient.ToUpper(), total);
                    dataGridView1.Rows.Add(
                             "",
                            "SOUS TOTAL", "",
                             "",
                             "",
                            "",
                                string.Format(elGR, "{0:0,0}", total),
                    ""
                             );
                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }
            
        }
   
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode==Keys.Enter)
                {
                    double remise;
                    if (double.TryParse(textBox3.Text, out remise))
                    {
                        if (lblTotal.Text != "")
                        {
                            var totalRemise = remise * double.Parse(lblTotal.Text) / 100;
                            label12.Text = totalRemise.ToString();
                            label12.Visible = true;
                            label13.Visible = true;
                        }
                    }
                    else
                    {
                        label12.Visible = false;
                        label13.Visible = false;
                        label13.Text = "";
                    }
                }

            }
            catch { }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dGV.Columns.Count-2; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dGV.Rows[i].Cells.Count-2; j++)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(1254);
                byte[] output = utf16.GetBytes(stOutput);
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void ToCsV1(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount ; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }
      
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "Facture de " + cmbEntreprise.Text + "_Impriméé_le_" + date + ".xls";
                var pathFolder = "C:\\Dossier Clinique";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                pathFolder = pathFolder + "\\Rapport des Conventionnes";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                if (checkBox5.Checked)
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ToCsV1(dataGridView3, sfd.FileName); // Here dataGridview1 is your grid view name
                    }
                }
                else
                {


                    if (dataGridView1.Visible == true || dataGridView2.Visible == false)
                    {
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                        }
                    }
                    else if (dataGridView2.Visible == true || dataGridView1.Visible == false)
                    {
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            ToCsV1(dataGridView2, sfd.FileName); // Here dataGridview1 is your grid view name
                        }
                    }
                }
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception)
            {
            }
        }

        private void dgvPatient_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dgvPatient.ContextMenuStrip = contextMenuStripLivraison;
                dgvPatient.ContextMenuStrip.Show(dgvPatient, e.Location);
            }
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbEntreprise.Text) && dgvPatient.SelectedRows.Count > 0)
                {

                    //var numEmpl = GestionAcademique.LoginFrm.matricule;
                    //var facture = new Facture(numFacture, dateFacture, montantTotal, idPatient, numEmpl, reste);
                    var frm = new GestionDuneClinique.FormesClinique.MontantFrm();
                    var total = 0.0;
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        frm.dataGridView1.Rows.Add(
                            dataGridView1.Rows[i].Cells[3].Value.ToString(),
                            dataGridView1.Rows[i].Cells[4].Value.ToString(),
                            double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())
                            / double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                            dataGridView1.Rows[i].Cells[5].Value.ToString(),
                             dataGridView1.Rows[i].Cells[6].Value.ToString(),
                              dataGridView1.Rows[i].Cells[0].Value.ToString()
                              );
                        total += double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    }

                    frm.numeroPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    frm.txtPatient.Text = dgvPatient.SelectedRows[0].Cells[1].Value.ToString() + " "
                        + dgvPatient.SelectedRows[0].Cells[2].Value.ToString();
                    frm.txtTotal.Text = total.ToString();
                    frm.lblNetAPayer.Text = total.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void bonDeRetourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbEntreprise.Text) && dgvPatient.SelectedRows.Count > 0)
                {
                    var frm = new GestionDuneClinique.FormesClinique.MontantFrm();
                   
                    var listeFacture = from f in ConnectionClassClinique.ListeDesFactures()
                                       where f.DateFacture >= dtp1.Value.Date
                                       where f.DateFacture < dtp2.Value.Date.AddHours(24)
                                       where f.IdPatient == Convert.ToInt32(dgvPatient.SelectedRows[0].Cells[0].Value.ToString())
                                       orderby f.NumeroFacture
                                       select f;
                    var montant = 0.0;var totalPaye = 0.0;
                    frm.numeroPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    foreach (var facture in listeFacture)
                    {
                        var sousTotalFacture = 0.0 ;
                        var detailListe = ConnectionClassClinique.DetailsDesFactures(facture.NumeroFacture);
                        var dtPaie = ConnectionClassClinique.TablePaiement(facture.NumeroFacture);
                        frm.dataGridView1.Rows.Add(" FACTURE N° " + facture.NumeroFacture + " du " +facture.DateFacture.ToShortDateString(), "", "", "");
                     
                        foreach (var f in detailListe)
                        {
                            frm. dataGridView1.Rows.Add(f.Designation, f.Prix, f.Quantite, f.PrixTotal);
                            montant += f.Prix * f.Quantite;
                            sousTotalFacture += f.Prix * f.Quantite;
                        }

                        frm.dataGridView1.Rows.Add("Total    ".ToUpper(), "", "", sousTotalFacture);
                     
                        for(var i=0; i<dtPaie.Rows.Count ;i++)
                        {
                             totalPaye += Double.Parse(dtPaie.Rows[i].ItemArray[0].ToString());
                        }
                    }
                    foreach (DataGridViewRow row in frm.dataGridView1.Rows)
                    {
                        
                        if (row.Cells[0].Value.ToString().Contains ("TOTAL    "))
                        {
                            row.DefaultCellStyle.BackColor = Color.Blue;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                    frm.txtPatient.Text = dgvPatient.SelectedRows[0].Cells[1].Value.ToString() + " "
                        + dgvPatient.SelectedRows[0].Cells[2].Value.ToString();
                    frm.txtTotal.Text = montant.ToString();
                    frm.lblNetAPayer.Text = montant.ToString();
                    frm.txtPaye.Text =totalPaye.ToString();
                    frm.txtReduction.Enabled = false;
                    frm.txtPaye.Enabled = false;
                    frm.btnApercu.Enabled = true;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var charge = 0.0;
                dataGridView2.Rows.Clear();
                if (double.TryParse(textBox2.Text, out charge))
                {

                    dataGridView1.Visible = false;
                    dataGridView2.Visible = true;
                    foreach (DataGridViewRow dtGrid in dataGridView1.Rows)
                    {
                        string nomPatient = "", libelle = "", date = "", prixUnit = "", quantite = "", prixTotal = "", chargeAssure = "", chargeAssureur = "";
                        double montantTotal, montantUnit;
                        int qte;

                        nomPatient = dtGrid.Cells[1].Value.ToString();
                        date = dtGrid.Cells[2].Value.ToString();
                        libelle = dtGrid.Cells[3].Value.ToString();
                        if (Double.TryParse(dtGrid.Cells[5].Value.ToString(), out montantUnit))
                        {
                            prixUnit = montantUnit.ToString();
                        }
                        if (Double.TryParse(dtGrid.Cells[6].Value.ToString(), out montantTotal))
                        {
                            prixTotal = montantTotal.ToString();

                            chargeAssure = (montantTotal * charge / 100).ToString();
                            chargeAssureur = (montantTotal - (montantTotal * charge / 100)).ToString();
                        }
                        if (Int32.TryParse(dtGrid.Cells[4].Value.ToString(), out qte))
                        {
                            quantite = qte.ToString();
                        }
                        dataGridView2.Rows.Add(nomPatient, date, libelle,
                                quantite, 
                                prixUnit,
                                montantTotal,
                                string.Format(elGR, "{0:0,0}", double.Parse(chargeAssure)),
                                string.Format(elGR, "{0:0,0}",double.Parse(chargeAssureur)));
                        //}
                        //else
                        //{
                        //    dataGridView2.Rows.Add("", "", "",
                        //  "", "", "", "", "");
                        //}
                    }
                    var TotalCharge = 0.0;
                    double montantTotaux;
                    foreach (DataGridViewRow dtGridView in dataGridView2.Rows)
                    {
                        if (!dtGridView.Cells[2].Value.ToString().Contains("TOTAL"))
                        {
                            if (double.TryParse(dtGridView.Cells[7].Value.ToString(), out montantTotaux))
                            {
                                TotalCharge += montantTotaux;
                            }
                        }
                    }
                    lblTotal.Text =string.Format(elGR, "{0:0,0}", TotalCharge);
                   
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells[2].Value.ToString() == "SOUS TOTAL")
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (row.Cells[2].Value.ToString() == "TOTAL")
                        {
                            row.DefaultCellStyle.BackColor = Color.OrangeRed;
                        }
                    }
                    etatImpression = "3";
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Calculer la charge", ex);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cmbEntreprise.Text != "")
                {
                    var listeEntreprise = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);

                    var entreprise = new Entreprise(listeEntreprise[0].NumeroEntreprise, listeEntreprise[0].NomEntreprise, listeEntreprise[0].Telephone1,
                        listeEntreprise[0].Telephone2, listeEntreprise[0].Email, listeEntreprise[0].Adresse, listeEntreprise[0].DateAbonnement, listeEntreprise[0].PrixHonoraire, listeEntreprise[0].Rattache);
                    dtp2.Format = DateTimePickerFormat.Long;
                    var mois = dtp2.Text.Substring(dtp2.Text.IndexOf(" ") + 1);
                    var mois1 = mois.Substring(mois.IndexOf(" "));
                    var date = dtp2.Value.ToShortDateString();
                    if (listeEntreprise[0].PrixHonoraire > 0)
                    {
                        rapportBitmap = Impression.FactureHonoraire(entreprise, date, mois1);
                        if (printDialog1.ShowDialog() == DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printPreviewDialog1.ShowDialog();
                        }
                    }
                    dtp2.Format = DateTimePickerFormat.Short;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture honoraire ", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            label13.Visible = false;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();
            try
            {
                var totalPharmacie = .0;
                var totalConsulation = .0;
                var totalActe = .0;
                dataGridView1.Rows.Clear();
                if (dgvPatient.SelectedRows.Count > 0)
                {
                    var nomPatient = "";
                    int idPatient;
                    if (dgvPatient.SelectedRows[0].Cells[4].Value.ToString() != "")
                    {
                        nomPatient = dgvPatient.SelectedRows[0].Cells[1].Value.ToString() + " s/c " + dgvPatient.SelectedRows[0].Cells[4].Value.ToString();
                    }
                    else
                    {
                        nomPatient = dgvPatient.SelectedRows[0].Cells[1].Value.ToString();
                    }

                    var listeDependant = from p in ConnectionClassClinique.ListeDesPatients()
                                         where p.SousCouvert.StartsWith(nomPatient, StringComparison.CurrentCultureIgnoreCase)
                                         select p;
                    foreach (var p in listeDependant)
                    {
                        var sousTotal = .0;
                        var dtFacture = ConnectionClassClinique.TableDesDetailsFacturesProforma(p.NumeroPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                        var dtCredit = ConnectionClassPharmacie.ListeDesCredit(p.NumeroPatient, p.Nom + " "+p.Prenom, cmbEntreprise.Text, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                            var total = 0.0;
                            foreach (DataRow dtRow in dtFacture.Rows)
                            {
                                if (dtRow.ItemArray[7].ToString() == "EXAMEN")
                                {
                                    totalActe += Double.Parse(dtRow.ItemArray[6].ToString());
                                }
                                else
                                {
                                    totalConsulation += Double.Parse(dtRow.ItemArray[6].ToString());
                                }
                                total += Double.Parse(dtRow.ItemArray[6].ToString());
                                sousTotal +=Double.Parse(dtRow.ItemArray[6].ToString());
                            }
                            foreach (DataRow dtRow in dtCredit.Rows)
                            {
                                totalPharmacie += Double.Parse(dtRow.ItemArray[7].ToString());
                                total += Double.Parse(dtRow.ItemArray[7].ToString());
                                sousTotal +=Double.Parse(dtRow.ItemArray[7].ToString());
                            }
                            RapportTotalGrouperParPatient(dtFacture, dtCredit, total, p.Nom + " " + p.Prenom);
                            var count = dataGridView3.Rows.Count;
                        }
                }

                //dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count - 1]);
                var montantTotal = 0.0;

                foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                {
                    double total;
                    if (dtRow.Cells[3].Value.ToString() == "SOUS TOTAL")
                    {
                        dtRow.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        if (Double.TryParse(dtRow.Cells[6].Value.ToString(), out total))
                        {
                            montantTotal += total;
                        }
                    }
                }
                lblTotal.Text = montantTotal.ToString();
                lblTotalConsultation.Text = string.Format(elGR, "{0:0,0}", totalConsulation);
                lblTotalActes.Text = string.Format(elGR, "{0:0,0}", totalActe);
                lblTotalPharmacie.Text = string.Format(elGR, "{0:0,0}", totalPharmacie);
                etatImpression = "4";
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            cmbEntreprise.Items.Clear();
            var listeEntrep = from en in ConnectionClassClinique.ListeDesEntreprises()
                              where en.NomEntreprise.ToUpper().Contains(textBox4.Text.ToUpper())
                              select en;
            cmbEntreprise.Items.Add("<<TOUTES LES CONVENTIONNEES>>");
            foreach (Entreprise entreprise in listeEntrep)
            {
                cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cmbEntreprise.Items.Clear();
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises().Select(en=>en.Rattache).Distinct().ToList();
                foreach (var  groupeConventionnne in listeEntrep)
                {
                    cmbEntreprise.Items.Add(groupeConventionnne.ToUpper());
                }
            }
            else
            {
                cmbEntreprise.Items.Clear();
                var listeEntrep = from en in ConnectionClassClinique.ListeDesEntreprises()
                                  where en.NomEntreprise.ToUpper().Contains(textBox4.Text.ToUpper())
                                  select en;
                cmbEntreprise.Items.Add("<<TOUTES LES CONVENTIONNEES>>");
                //cmbEntreprise.Text = "<<TOUTES LES CONVENTIONNEES>>";
                foreach (Entreprise entreprise in listeEntrep)
                {
                    cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
                }
                dgvPatient.Rows.Clear();
            }
        }
    }
}
