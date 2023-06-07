using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class DetailsPaiementFrm : Form
    {
        public DetailsPaiementFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void DetailsPaiementFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SlateBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        public int numeroPaiement, exercice; public string moisEtat;
        private void DetailsPaiementFrm_Load(object sender, EventArgs e)
        {
            label1.Text = "ETAT DE SALAIRE DU MOIS DE " + moisEtat.ToUpper();
            Column2.Width = dataGridView1.Width / 4-80;
            Column3.Width = dataGridView1.Width / 5-30;
            button5.Location = new Point(Width - 45, button5.Location.Y);
            button10.Location = new Point(Width - 90, button5.Location.Y);
            ListeDesEtatsPaiements();
               MontantTotal();
               var typeContrat = new string[]
            {
                "Decisionaire",
                "Decreté",
                "Detaché",
                "Journalier",
                 "Prestataire",
                "Stage"
            };
            foreach (DataRow b in ConnectionClass.ListeBanques().Rows)
            {
                cmbBank.Items.Add(b.ItemArray[1].ToString());
            }
            cmbTypeContrat.Items.AddRange(typeContrat);
        }

        void ListeDesEtatsPaiements()
        {
            try
            {
                dataGridView1.Rows.Clear();
                  var listeDep = ConnectionClass.ListeDepartement();
                  double totalGain = 0.0 , totalSalaireBase = 0.0, totalGainSalarial = 0.0, totalPrimes = 0.0, totalIRPP = 0.0, totalONASA = 0.0, totalConge = 0.0,
                  totalChargePatronal = 0.0, totalDeductif = 0.0, totalCoutSalarial = 0.0,
                  totalSalaireBrut = 0.0, totalAcompte = 0.0, totalCNPS = 0.0, totalSalaireNet = 0.0, totalAvanceSurSalaire = 0.0, totalCoutAbscence = 0.0;
                  var i = 1;
                  for (var k = 0; k < listeDep.Rows.Count; k++)
                  {
                      double sousTotalSalaireBase = 0.0, sousTotalGainSalarial = 0.0, sousTotalPrimes = 0.0, sousTotalIRPP = 0.0, sousTotalONASA = 0.0, sousTotalConge = 0.0,
                          sousTotalChargePatronal  = 0.0, sousTotalDeductif = 0.0, sousTotalCoutSalarial = 0.0,
                      sousTotalSalaireBrut = 0.0,sousTotalGain=0.0, sousTotalAcompte = 0.0, sousTotalCNPS = 0.0, sousTotalSalaireNet = 0.0, sousTotalAvanceSurSalaire = 0.0, sousTotalCoutAbscence = 0.0;

                      var listeMatricule = ConnectionClass.ListeMatriculePaye(numeroPaiement, listeDep.Rows[k].ItemArray[1].ToString(), "CDD", "CDI");
                      if (listeMatricule.Count() > 0)
                      {
                          dataGridView1.Rows.Add("", listeDep.Rows[k].ItemArray[1].ToString(), "","",
                             "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","","100","");
                          dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                          dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 12.25F, System.Drawing.FontStyle.Bold);
                  
                          foreach (var personnel in listeMatricule)
                          {
                              var p = ConnectionClass.PaiementParMatricule(numeroPaiement, personnel.NumeroMatricule);
                              var listeEmpl = ConnectionClass.ListeDesPersonnelParNumeroMatricule(personnel.NumeroMatricule);
                              var dtSal = ConnectionClass.ListeSalaire(p.NumeroMatricule);
                              var anciennete = listeEmpl.Rows[0].ItemArray[19].ToString();
                              if (anciennete.Contains("%"))
                              {
                                  anciennete = anciennete.Remove(anciennete.IndexOf("%"));
                              }
                              double categorie;
                              if (double.TryParse(anciennete, out categorie))
                              {
                              }
                              else
                              {
                                  categorie = 0;
                              }
                              p.SalaireBase = (double)dtSal[0].SalaireBase;
                              var gain = Math.Round(p.SalaireBase *  categorie / 100);
                              var gainSalariale =  Math.Round(p.SalaireBase * (1 + categorie / 100));
                              var employe = listeEmpl.Rows[0].ItemArray[1].ToString() + " " + listeEmpl.Rows[0].ItemArray[2].ToString();
                              var qualification = listeEmpl.Rows[0].ItemArray[12].ToString();
                              var chargeTotal = p.CNPS + p.IRPP + p.ONASA;
                               var deduction = p.AvanceSurSalaire + p.AcomptePaye;
                               var primes = p.Transport + p.HeureSupplementaire + p.PrimeGarde + p.PrimeLogement + p.PrimeResponsabilite;
                               var deductif = 0 + p.AcomptePaye + p.AvanceSurSalaire + p.CNPS + p.ONASA + p.IRPP;
                      
                              dataGridView1.Rows.Add(personnel.NumeroMatricule, employe, qualification,
                                  listeEmpl.Rows[0].ItemArray[14].ToString() + "-" + listeEmpl.Rows[0].ItemArray[13].ToString() ,
                                    String.Format(elGR, "{0:0,0}", p.SalaireBase),
                                      String.Format(elGR, "{0:0,0}", gain),
                                    String.Format(elGR, "{0:0,0}", gainSalariale),
                                    String.Format(elGR, "{0:0,0}", p.CongeAnnuel),
                                    String.Format(elGR, "{0:0,0}", p.CoutAbsence), 
                                    String.Format(elGR, "{0:0,0}", p.SalaireBrut),
                                    String.Format(elGR, "{0:0,0}", p.CNPS),
                                    String.Format(elGR, "{0:0,0}", p.IRPP),
                                    String.Format(elGR, "{0:0,0}", p.ONASA),
                                           String.Format(elGR, "{0:0,0}", p.AvanceSurSalaire),
                                    String.Format(elGR, "{0:0,0}", p.AcomptePaye),
                                      String.Format(elGR, "{0:0,0}", deductif), 
                                    String.Format(elGR, "{0:0,0}", primes),
                                    String.Format(elGR, "{0:0,0}", p.SalaireNet),
                                    String.Format(elGR, "{0:0,0}", p.ChargePatronale),
                                    String.Format(elGR, "{0:0,0}", p.CoutDuSalarie),
                                    i,p.Banque
                                  );
                              i++;
                              totalChargePatronal += p.ChargePatronale;
                              totalSalaireBase += p.SalaireBase;
                              totalGainSalarial += gainSalariale;
                              totalPrimes += primes;
                              totalSalaireBrut += p.SalaireBrut;
                              totalCNPS += p.CNPS;
                              totalSalaireNet += p.SalaireNet;
                              totalIRPP += p.IRPP;
                              totalONASA += p.ONASA;
                              totalAcompte += p.AcomptePaye;
                              totalAvanceSurSalaire += p.AvanceSurSalaire;
                              totalDeductif += deductif;
                              totalConge += p.CongeAnnuel;
                              totalCoutAbscence += p.CoutAbsence;
                              totalCoutSalarial += p.CoutDuSalarie;
                              totalGain += gain;
                              sousTotalGain += gain;
                              sousTotalChargePatronal += p.ChargePatronale;
                              sousTotalSalaireBase += p.SalaireBase;
                              sousTotalGainSalarial += gainSalariale;
                              sousTotalPrimes += primes;
                              sousTotalSalaireBrut += p.SalaireBrut;
                              sousTotalCNPS += p.CNPS;
                              sousTotalSalaireNet += p.SalaireNet;
                              sousTotalIRPP += p.IRPP;
                              sousTotalONASA += p.ONASA;
                              sousTotalAcompte += p.AcomptePaye;
                              sousTotalAvanceSurSalaire += p.AvanceSurSalaire;
                              sousTotalDeductif += deductif;
                              sousTotalConge += p.CongeAnnuel;
                              sousTotalCoutAbscence += p.CoutAbsence;
                              sousTotalCoutSalarial += p.CoutDuSalarie;

                          }
                          dataGridView1.Rows.Add("", "TOTAL", "", "",
                                        String.Format(elGR, "{0:0,0}", sousTotalSalaireBase),
                                        String.Format(elGR, "{0:0,0}", sousTotalGain),
                                          String.Format(elGR, "{0:0,0}", sousTotalGainSalarial),
                                        String.Format(elGR, "{0:0,0}", sousTotalConge),
                                        String.Format(elGR, "{0:0,0}", sousTotalCoutAbscence),
                                        String.Format(elGR, "{0:0,0}", sousTotalSalaireBrut),
                                        String.Format(elGR, "{0:0,0}", sousTotalCNPS),
                                        String.Format(elGR, "{0:0,0}", sousTotalIRPP),
                                        String.Format(elGR, "{0:0,0}", sousTotalONASA),
                                        String.Format(elGR, "{0:0,0}", sousTotalAvanceSurSalaire),
                                        String.Format(elGR, "{0:0,0}", sousTotalAcompte),
                                          String.Format(elGR, "{0:0,0}", sousTotalDeductif),
                                        String.Format(elGR, "{0:0,0}", sousTotalPrimes),
                                        String.Format(elGR, "{0:0,0}", sousTotalSalaireNet),
                                        String.Format(elGR, "{0:0,0}", sousTotalChargePatronal),
                                        String.Format(elGR, "{0:0,0}", sousTotalCoutSalarial),"",""
                                      );
                          dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                          dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 12.2F, System.Drawing.FontStyle.Bold);

                      }

                  }

                  dataGridView1.Rows.Add("", "TOTAL GENERAL", "", "",
                                String.Format(elGR, "{0:0,0}", totalSalaireBase),
                                        String.Format(elGR, "{0:0,0}", totalGain),
                                String.Format(elGR, "{0:0,0}", totalGainSalarial),
                                String.Format(elGR, "{0:0,0}", totalConge),
                                String.Format(elGR, "{0:0,0}", totalCoutAbscence),
                                String.Format(elGR, "{0:0,0}", totalSalaireBrut),
                                String.Format(elGR, "{0:0,0}", totalCNPS),
                                String.Format(elGR, "{0:0,0}", totalIRPP),
                                String.Format(elGR, "{0:0,0}", totalONASA),
                                String.Format(elGR, "{0:0,0}", totalAvanceSurSalaire),
                                String.Format(elGR, "{0:0,0}", totalAcompte),
                                 String.Format(elGR, "{0:0,0}", totalDeductif),
                                String.Format(elGR, "{0:0,0}",totalPrimes),
                                String.Format(elGR, "{0:0,0}", totalSalaireNet),
                                String.Format(elGR, "{0:0,0}",totalChargePatronal),
                                String.Format(elGR, "{0:0,0}", totalCoutSalarial),"",""
                              );
                  dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.SteelBlue;
                  dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                  dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 12.2F, System.Drawing.FontStyle.Bold);
                  MontantTotal();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Autres")
            {
                ListeDesEtatsPaiementsParContrat();
            }
        }

        void MontantTotal()
        {
            try
            {
                var total = 0.0;
                var count = 0;
                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!dataGridView1.Rows[i].Cells[1].Value.ToString().Contains("TOTAL") && ! string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                    {
                        total += double.Parse(dataGridView1.Rows[i].Cells[17].Value.ToString());
                        count++;
                    }
                }

                txtTotal.Text = "Nombre salarié traité  : " + count + "        Cout total : " + String.Format(elGR, "{0:0,0}", total) + "   ";
            }
            catch { }
        }
        void ListeDesEtatsPaiementsParContrat()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var listeDep = ConnectionClass.ListeDepartement();
                double totalSalaireBase = 0.0, totalGainSalarial = 0.0, totalPrimes = 0.0, totalIRPP = 0.0, totalONASA = 0.0, totalConge = 0.0,
                totalChargePatronal = 0.0, totalDeductif = 0.0, totalCoutSalarial = 0.0,
               totalSalaireBrut = 0.0, totalAcompte = 0.0, totalCNPS = 0.0, totalSalaireNet = 0.0, totalAvanceSurSalaire = 0.0, totalCoutAbscence = 0.0;

                for (var k = 0; k < listeDep.Rows.Count; k++)
                {
                    var listeMatricule = ConnectionClass.ListeMatriculePaye(numeroPaiement, listeDep.Rows[k].ItemArray[1].ToString(), cmbTypeContrat.Text ,cmbTypeContrat.Text);
                    if (listeMatricule.Count() > 0)
                    {
                       
                        foreach (var personnel in listeMatricule)
                        {
                            var p = ConnectionClass.PaiementParMatricule(numeroPaiement, personnel.NumeroMatricule);
                            var listeEmpl = ConnectionClass.ListeDesPersonnelParNumeroMatricule(personnel.NumeroMatricule);
                            var dtService = ConnectionClass.ListeDesServices(personnel.NumeroMatricule);
                            //var dtSal = ConnectionClass.ListeSalaire(p.NumeroMatricule);
                            var typeCont = ""; 
                            
                            #region MyRegion
                            {
                            
                                //p.SalaireBase = Convert.ToDouble(dtSal.Rows[0].ItemArray[0].ToString());
                                var gainSalariale =p.SalaireBase;
                                var employe = listeEmpl.Rows[0].ItemArray[1].ToString() + " " + listeEmpl.Rows[0].ItemArray[2].ToString();
                                var qualification = listeEmpl.Rows[0].ItemArray[12].ToString();
                                var chargeTotal = p.CNPS + p.IRPP + p.ONASA;
                                var primes = p.Transport + p.HeureSupplementaire + p.PrimeGarde + p.PrimeLogement + p.PrimeResponsabilite;
                                var deductif = p.AcomptePaye + 0 + p.AvanceSurSalaire + p.CNPS + p.ONASA + p.IRPP;

                                dataGridView1.Rows.Add(personnel.NumeroMatricule, employe, qualification,
                                    listeEmpl.Rows[0].ItemArray[14].ToString() + "-" + listeEmpl.Rows[0].ItemArray[13].ToString() ,
                                      String.Format(elGR, "{0:0,0}", p.SalaireBase),
                                       String.Format(elGR, "{0:0,0}", 0),
                                      String.Format(elGR, "{0:0,0}", gainSalariale),
                                      String.Format(elGR, "{0:0,0}", p.CongeAnnuel),
                                      String.Format(elGR, "{0:0,0}", p.CoutAbsence),
                                      String.Format(elGR, "{0:0,0}", p.SalaireBrut),
                                      String.Format(elGR, "{0:0,0}", p.CNPS),
                                      String.Format(elGR, "{0:0,0}", p.IRPP),
                                      String.Format(elGR, "{0:0,0}", p.ONASA),
                                                 String.Format(elGR, "{0:0,0}", p.AvanceSurSalaire),
                                      String.Format(elGR, "{0:0,0}", p.AcomptePaye),
                                        String.Format(elGR, "{0:0,0}", deductif),
                                      String.Format(elGR, "{0:0,0}", primes),
                                      String.Format(elGR, "{0:0,0}", p.SalaireNet),
                                      String.Format(elGR, "{0:0,0}", p.ChargePatronale),
                                      String.Format(elGR, "{0:0,0}", p.CoutDuSalarie),"",p.Banque
                                    );

                                totalChargePatronal += p.ChargePatronale;
                                totalSalaireBase += p.SalaireBase;
                                totalGainSalarial += gainSalariale;
                                totalPrimes += primes;
                                totalSalaireBrut += p.SalaireBrut;
                                totalCNPS += p.CNPS;
                                totalSalaireNet += p.SalaireNet;
                                totalIRPP += p.IRPP;
                                totalONASA += p.ONASA;
                                totalAvanceSurSalaire += p.AvanceSurSalaire;
                                totalAcompte += p.AcomptePaye;
                                totalDeductif += deductif;
                                totalConge += p.CongeAnnuel;
                                totalCoutAbscence += p.CoutAbsence;
                                totalCoutSalarial += p.CoutDuSalarie;
                                //totalChargeMedical += p.ChargeSoinFamille;
                            } 
                            #endregion
                        }
                    }

                }

                dataGridView1.Rows.Add("", "TOTAL", "", "",
                              String.Format(elGR, "{0:0,0}", totalSalaireBase),
                                     String.Format(elGR, "{0:0,0}", 0),
                              String.Format(elGR, "{0:0,0}", totalGainSalarial),
                              String.Format(elGR, "{0:0,0}", totalConge),
                              String.Format(elGR, "{0:0,0}", totalCoutAbscence),
                              String.Format(elGR, "{0:0,0}", totalSalaireBrut),
                              String.Format(elGR, "{0:0,0}", totalCNPS),
                              String.Format(elGR, "{0:0,0}", totalIRPP),
                              String.Format(elGR, "{0:0,0}", totalONASA),
            String.Format(elGR, "{0:0,0}", totalAvanceSurSalaire),
                              String.Format(elGR, "{0:0,0}", totalAcompte),
                                String.Format(elGR, "{0:0,0}", totalDeductif),
                              String.Format(elGR, "{0:0,0}", totalPrimes),
                              String.Format(elGR, "{0:0,0}", totalSalaireNet),
                              String.Format(elGR, "{0:0,0}", totalChargePatronal),
                              String.Format(elGR, "{0:0,0}", totalCoutSalarial),"",""
                            );
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.SteelBlue;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 12.2F, System.Drawing.FontStyle.Bold);
                MontantTotal();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text =="Contractuel")
            {
                ListeDesEtatsPaiements();
            }
            else if (comboBox1.Text == "Autres")
            {
                cmbTypeContrat_SelectedIndexChanged(null, null);
            }
            MontantTotal();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf";
                    //string pathFile = "";
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (comboBox1.Text == "Autres")
                        {
                            var count = dataGridView1.Rows.Count;
                            var div = (count) / 14;
                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 14 < count)
                                {
                                    var bitmap = AppCode.Impression.ImprimerOrdreDePaiementPourJournalierEtStagiaire(numeroPaiement, dataGridView1, cmbTypeContrat.Text, exercice, moisEtat, i);
                                    string inputImage1 = @"cdali" + i;
                                    // Create an empty page
                                    sharpPDF.pdfPage page = document.addPage(500, 700);
                                    
                                    document.addImageReference(bitmap, inputImage1);
                                    sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                                    page.addImage(img, 10, 0, page.height, page.width);
                                }

                            }
                        }
                        else if (comboBox1.Text == "Contractuel" || comboBox1.Text=="<Trier par>")
                        {  
                            var bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, dataGridView1, exercice, moisEtat);
                                       
                            var inputImage = @"cdali" ;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage(500,700);
                            document.addImageReference(bitmap, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                            if (dataGridView1.Rows.Count > 25)
                            {
                                var div = (dataGridView1.Rows.Count - 25) / 31;

                                for (var i = 0; i <= div; i++)
                                {
                                    if (i * 31 < dataGridView1.Rows.Count)
                                    {
                                        bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, dataGridView1, exercice, moisEtat, i);
                                        inputImage = @"cdali" + i;
                                        // Create an empty page
                                        pageIndex = document.addPage(500, 700);

                                        document.addImageReference(bitmap, inputImage);
                                        img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Imprimer paiement", ex);
            }
        }
        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 0; j < dGV.Columns.Count ; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 0; j < dGV.Rows[i].Cells.Count ; j++)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(1254);
                byte[] output = utf16.GetBytes(stOutput);
                System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();
                    var montant = 0.0; var montantCNPS = 0.0;
                    for (var p = 0; p < dataGridView1.Rows.Count; p++)
                    {
                        if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                        {
                            var paiement = new Paiement();
                            paiement.GainSalarial = double.Parse(dataGridView1.Rows[p].Cells[6].Value.ToString());
                            paiement.CNPS =Math.Round( double.Parse(dataGridView1.Rows[p].Cells[6].Value.ToString()) * 20 / 100);
                            paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString();
                            paiement.NumeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                            lstPaiement.Add(paiement);

                            montantCNPS += paiement.CNPS;
                            montant += paiement.GainSalarial;
                        }
                    }
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = "CNPS du mois " +moisEtat +"_imprimé_le_" + datTe + ".pdf";
                    var l = from li in lstPaiement
                            orderby li.Employe
                            select li;
                    var listePaiement = new List<Paiement>();
                    
                    foreach (var li in lstPaiement)
                    {
                        var p = new Paiement();
                        p.CNPS = li.CNPS;
                        p.GainSalarial = li.GainSalarial;
                        p.Employe = li.Employe;
                        p.NumeroMatricule = li.NumeroMatricule;
                        listePaiement.Add(p);
                    }
                    //string pathFile = "";
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (comboBox1.Text != "Autres")
                        {
                            var count = listePaiement.Count;
                            var div = (count) / 40;
                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 40 < count)
                                {
                                    var bitmap = AppCode.Impression.ImprimerListeDesCNPS(listePaiement,montant,montantCNPS, moisEtat, exercice, i);
                                    string inputImage1 = @"cdali" + i;
                                    // Create an empty page
                                    sharpPDF.pdfPage page = document.addPage();

                                    document.addImageReference(bitmap, inputImage1);
                                    sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                                    page.addImage(img, 10, 0, page.height, page.width);
                                }
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch { }
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
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
                sfd.FileName = "Paiement_Impriméé_le_" + date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToCsV(dataGridView1, sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            try
            {
                var lstPaiement = new List<Paiement>();
                var montant = 0.0; var montantIRPP = 0.0;
                var totalONASA = 0.0;
                for (var p = 0; p < dataGridView1.Rows.Count; p++)
                {
                    if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                    {
                        var salaireImpos = Math.Round(double.Parse(dataGridView1.Rows[p].Cells[6].Value.ToString()) -
                                double.Parse(dataGridView1.Rows[p].Cells[6].Value.ToString()) * 3.5 / 100);
                        if (salaireImpos*12 > 800000)
                        {

                            var paiement = new Paiement(); ;
                            paiement.GainSalarial = salaireImpos;
                            paiement.IRPP = Math.Round(paiement.GainSalarial * 10.5 / 100);
                            paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString();
                            paiement.NumeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                            paiement.ONASA = double.Parse(dataGridView1.Rows[p].Cells[12].Value.ToString());
                            lstPaiement.Add(paiement);

                            totalONASA += double.Parse(dataGridView1.Rows[p].Cells[12].Value.ToString());
                            montantIRPP += paiement.IRPP;
                            montant += paiement.GainSalarial;
                        }
                    }
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                
                var jour = DateTime.Now.Day;
                var moiSs = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "IRPP du mois " + moisEtat + "_imprimé_le_" + datTe + ".xls";
                var l = from li in lstPaiement
                        orderby li.Employe
                        select li;
                var listePaiement = new List<Paiement>();

                foreach (var li in lstPaiement)
                {
                    var p = new Paiement();
                    p.IRPP = li.IRPP;
                    p.GainSalarial = li.GainSalarial;
                    p.Employe = li.Employe;
                    p.NumeroMatricule = li.NumeroMatricule;
                    p.ONASA = li.ONASA;
                    listePaiement.Add(p);
                }
                var dgvView = new DataGridView();
                dgvView.Columns.Add("no", "N°");
                dgvView.Columns.Add("nom", "Nom & Prenom");
                dgvView.Columns.Add("nomt", "Montant");
                dgvView.Columns.Add("nonta", "Taux");
                dgvView.Columns.Add("nocnp", "IRPP");
                dgvView.Columns.Add("noas", "FIR");
                dgvView.Columns.Add("noas", "Total");

                var k = 1;
                foreach (var pai in lstPaiement)
                {
                    dgvView.Rows.Add(k, pai.Employe, pai.GainSalarial, "10,5%", pai.IRPP , pai.ONASA, pai.ONASA+pai.IRPP);
                    k++;
                }
                dgvView.Rows.Add("", "Total",montant , "", Math.Round(montant*10.5/100), totalONASA, Math.Round(montant * 10.5 / 100) + totalONASA);
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    string stOutput = "";
                    // Export titles:
                    string sHeaders = "";

                    for (int j = 0; j < dgvView.Columns.Count; j++)
                        sHeaders = sHeaders.ToString() + Convert.ToString(dgvView.Columns[j].HeaderText) + "\t";
                    stOutput += sHeaders + "\r\n";
                    // Export data.
                    for (int i = 0; i < dgvView.RowCount; i++)
                    {
                        string stLine = "";
                        for (int j = 0; j < dgvView.Rows[i].Cells.Count; j++)
                            stLine = stLine.ToString() + Convert.ToString(dgvView.Rows[i].Cells[j].Value) + "\t";
                        stOutput += stLine + "\r\n";
                    }
                    Encoding utf16 = Encoding.GetEncoding(1254);
                    byte[] output = utf16.GetBytes(stOutput);
                    System.IO.FileStream fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create);
                    System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                    bw.Write(output, 0, output.Length); //write the encoded file
                    bw.Flush();
                    bw.Close();
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void entréeStcokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var lstPaiement = new List<Paiement>();
                var montant = 0.0; var montantCNPS = 0.0;
                for (var p = 0; p < dataGridView1.Rows.Count; p++)
                {
                    if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                    {
                        var paiement = new Paiement();
                        paiement.GainSalarial = double.Parse(dataGridView1.Rows[p].Cells[6].Value.ToString());
                        paiement.CNPS = Math.Round(double.Parse(dataGridView1.Rows[p].Cells[6].Value.ToString()) * 20 / 100);
                        paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString();
                        paiement.NumeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                        lstPaiement.Add(paiement);

                        montantCNPS += paiement.CNPS;
                        montant += paiement.GainSalarial;
                    }
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                var jour = DateTime.Now.Day;
                var moiSs = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "CNPS du mois " + moisEtat + "_imprimé_le_" + datTe + ".xls";
                var l = from li in lstPaiement
                        orderby li.Employe
                        select li;
                var listePaiement = new List<Paiement>();

                foreach (var li in lstPaiement)
                {
                    var p = new Paiement();
                    p.CNPS = li.CNPS;
                    p.GainSalarial = li.GainSalarial;
                    p.Employe = li.Employe;
                    p.NumeroMatricule = li.NumeroMatricule;
                    listePaiement.Add(p);
                }
                var dgvView = new DataGridView();
                dgvView.Columns.Add("no","N°");
                dgvView.Columns.Add("nom", "Nom & Prenom");
                dgvView.Columns.Add("nomt", "Montant");
                dgvView.Columns.Add("nonta", "Taux");
                dgvView.Columns.Add("nocnp", "CNPS à payer");
                dgvView.Columns.Add("noas", "Assurance");
                var k= 1;
                foreach (var pai in lstPaiement)
                {
                    var dt = ConnectionClass.ListeDesPersonnelParNumeroMatricule(pai.NumeroMatricule);
                    var cnps = "";
                    if (dt.Rows.Count > 0)
                    {
                        cnps = dt.Rows[0].ItemArray[15].ToString();
                    }
                    dgvView.Rows.Add(k,pai.Employe,pai.GainSalarial,"20%", pai.CNPS,cnps );
                    k++;
                }
                dgvView.Rows.Add("","Total", montant, "",Math.Round( montant*20.5/100), "");
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    string stOutput = "";
                    // Export titles:
                    string sHeaders = "";

                    for (int j = 0; j < dgvView.Columns.Count; j++)
                        sHeaders = sHeaders.ToString() + Convert.ToString(dgvView.Columns[j].HeaderText) + "\t";
                    stOutput += sHeaders + "\r\n";
                    // Export data.
                    for (int i = 0; i < dgvView.RowCount; i++)
                    {
                        string stLine = "";
                        for (int j = 0; j < dgvView.Rows[i].Cells.Count; j++)
                            stLine = stLine.ToString() + Convert.ToString(dgvView.Rows[i].Cells[j].Value) + "\t";
                        stOutput += stLine + "\r\n";
                    }
                    Encoding utf16 = Encoding.GetEncoding(1254);
                    byte[] output = utf16.GetBytes(stOutput);
                    System.IO.FileStream fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create);
                    System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                    bw.Write(output, 0, output.Length); //write the encoded file
                    bw.Flush();
                    bw.Close();
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                button10.ContextMenuStrip = ctxInventaire ;
                button10.ContextMenuStrip.Show(button10, e.Location);
            }
        }

        private void btnImprimer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                button2.ContextMenuStrip = contextMenuStrip1;
                button2.ContextMenuStrip.Show(button2, e.Location);
            }
        }

        private void CmbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = new List<Personnel>();
                var total = .0;
                foreach(DataGridViewRow dgRows in dataGridView1.Rows)
                {
                    if (dgRows.Cells[21].Value.ToString().ToUpper() == cmbBank.Text.ToUpper())
                    {
                        var personnel = new Personnel();
                        personnel.Nom = dgRows.Cells[1].Value.ToString();
                        var dtPersonnel = ConnectionClass.ListeDesPersonnelParNumeroMatricule(dgRows.Cells[0].Value.ToString());
                        personnel.NumeroCompte = dtPersonnel.Rows[0].ItemArray[18].ToString();
                        personnel.CodeBanque= dtPersonnel.Rows[0].ItemArray[23].ToString();
                        personnel.CodeGuichet= dtPersonnel.Rows[0].ItemArray[24].ToString();
                        personnel.Cle = dtPersonnel.Rows[0].ItemArray[25].ToString();
                        personnel.Telephone1 = dgRows.Cells[17].Value.ToString();
                        liste.Add(personnel);
                        total += double.Parse(dgRows.Cells[17].Value.ToString());
                    }
                }
                var personnel1 = new Personnel();
                personnel1.Nom = "Total";
                personnel1.Telephone1 = total.ToString();
                liste.Add(personnel1);
                if (liste.Count > 0)
                {

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var date = DateTime.Now.ToString().Replace("/", "_");
                        date = date.Replace(":","_");
                    sfd.FileName = "Liste des employes "+cmbBank.Text.Replace("/", "_") + "_imprimé_le_" + date + ".pdf";
                    //string pathFile = "";
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var count = liste.Count;
                        var bitmap = Impression.ImprimerListeDesBancaries(liste,cmbBank.Text);
                        string inputImage1 = @"cdali";
                        // Create an empty page
                        sharpPDF.pdfPage page = document.addPage();
                        document.addImageReference(bitmap, inputImage1);
                        sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                        page.addImage(img, 10, 0, page.height, page.width);
                        var div = (count - 30) / 45;
                        for (var i = 0; i <= div; i++)
                        {
                            if (i * 45 < count-30)
                            {
                                bitmap = Impression.ImprimerListeDesBancaries(liste, i);
                                inputImage1 = @"cdali" + i;
                                // Create an empty page
                                page = document.addPage();

                                document.addImageReference(bitmap, inputImage1);
                                img = document.getImageReference(inputImage1);
                                page.addImage(img, 10, 0, page.height, page.width);
                            }

                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            { }
            }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            cmbBank.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();
                    var montant = 0.0; var totalFIR=0.0;
                    for (var p = 0; p < dataGridView1.Rows.Count; p++)
                    {
                        if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                        {
                            if (double.Parse(dataGridView1.Rows[p].Cells[11].Value.ToString()) > 0)
                            {
                                var paiement = new Paiement();
                                paiement.GainSalarial = double.Parse(dataGridView1.Rows[p].Cells[6].Value.ToString());
                                paiement.ONASA = double.Parse(dataGridView1.Rows[p].Cells[12].Value.ToString());
                                paiement.IRPP = double.Parse(dataGridView1.Rows[p].Cells[11].Value.ToString());
                                paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString();
                                paiement.NumeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                                lstPaiement.Add(paiement);
                                montant += paiement.GainSalarial;
                                totalFIR += paiement.ONASA;
                            }
                        }
                    }
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = "IRPP du mois " + moisEtat + "_imprimé_le_" + datTe + ".pdf";
                    var l = from li in lstPaiement
                            orderby li.Employe
                            select li;
                    var listePaiement = new List<Paiement>();

                    foreach (var li in lstPaiement)
                    {
                        var p = new Paiement();
                        p.IRPP = li.IRPP;
                        p.GainSalarial = li.GainSalarial;
                        p.ONASA = li.ONASA;
                        p.Employe = li.Employe;
                        p.NumeroMatricule = li.NumeroMatricule;
                        listePaiement.Add(p);
                    }

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (comboBox1.Text != "Autres")
                        {
                            var count = listePaiement.Count;
                            var div = (count) / 40;
                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 40 < count)
                                {
                                    var bitmap = AppCode.Impression.ImprimerListeDesIRPPFIR(listePaiement, montant, totalFIR, moisEtat, exercice, i);
                                    string inputImage1 = @"cdali" + i;
                                    // Create an empty page
                                    sharpPDF.pdfPage page = document.addPage();

                                    document.addImageReference(bitmap, inputImage1);
                                    sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                                    page.addImage(img, 10, 0, page.height, page.width);
                                }
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch { }
        }
    }
}
