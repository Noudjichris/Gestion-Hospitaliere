using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using GestionPharmacetique.AppCode;

namespace GestionDuneClinique.Formes
{
    public partial class RapportMedecinFrm : Form
    {
        public RapportMedecinFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void RapportMedecin_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, groupBox4.Width - 1, groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
               SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, groupBox3.Width - 1, groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
               SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void RapportMedecinFrm_Load(object sender, EventArgs e)
        {
            dataGridViewTextBoxColumn3.Width = dataGridView1.Width / 9;
            dataGridViewTextBoxColumn4.Width = dataGridView1.Width / 9;
            clPart.Width = dataGridView1.Width / 9;
            dataGridViewTextBoxColumn3.Width = dataGridView1.Width / 2 - 130;
            clPartT.Width = dataGridView1.Width / 9;
            clMontant.Width = dataGridView1.Width / 9;

            clPart1.Width = dataGridView1.Width / 9;
            clMontant1.Width = dataGridView1.Width / 9;
            Column2.Width = dataGridView1.Width / 9;
            Column1.Width = dataGridView1.Width / 2 - 130;
            clPartT1.Width = dataGridView1.Width / 9;
            clFrais1.Width = dataGridView1.Width / 9;

            clFrais.Visible = true;
            clFrais1.Visible = true;
            clMontant.Visible = true;
            clMontant1.Visible = true;
            //clPart.Visible = true;
            //clPart1.Visible = true;
            //clPartT.Visible = true;
            //clPartT1.Visible = true;
            etat = true;
            radioButton1.Checked = true;
            button3.Location = new Point(Width - 50, 3);
            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
            cmbConventionne.Items.Clear();
            foreach (Entreprise entreprise in listeEntrep)
            {
                cmbConventionne.Items.Add(entreprise.NomEntreprise);
            }
            cmbConventionne.Items.Add("<<TOUS LES CONVENTIONNES>>");
            cmbConventionne.Items.Add("<<TOUS LES PARTICULIERS>>");
            cmbConventionne.Items.Add("<<TOUS LES PATIENTS>>");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbNomEmpoye.Text))
                {
                        ListeDesExamens();

                        var dtConsultation = ConnectionClassClinique.ListeConsultationChiffre(cmbNomEmpoye.Text,
                            dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                        ListeDesConsultation(dtConsultation, dtp1.Value.Date, dtp2.Value.Date.AddHours(24),1);
             
                }
                else
                {
                        ListeDesExamens();

                        var dtConsultation = ConnectionClassClinique.ListeConsultationChiffre(cmbNomEmpoye.Text,
                            dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                        ListeDesConsultation(dtConsultation, dtp1.Value.Date, dtp2.Value.Date.AddHours(24),1);
                }
                var dtPharmacie = ConnectionClassPharmacie.ListeDesVentesPharmacie(dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                ListeDesRapportsVentes(dtPharmacie);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport medecin", ex);
            }
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        //rapport des examens
        void ListeDesExamens()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var total = 0.0;
                if (cmbConventionne.Text == "" || cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                {
                    var requete = "SELECT  det_fact.design, COUNT(det_fact.qte) ,det_fact.prix, SUM(det_fact.prix_total) FROM det_fact INNER JOIN" +
                             " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE det_fact.groupage   not like 'Consultation' AND " +
                             "  facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 GROUP BY   det_fact.design,det_fact.prix";
                    var dtCaisse = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                    foreach (DataRow dtRow in dtCaisse.Rows)
                    {
                        total += Double.Parse(dtRow.ItemArray[3].ToString());
                    }
     
                }
                 if(cmbConventionne.Text=="<<TOUS LES PARTICULIERS>>")
                 {
                                        RecettesMensuellesDeLaCaisseEtPharmacie();
                  }else{
                      foreach (var gr in AppCode.ConnectionClassClinique.ListeDesGroupes())
                      {
                          if (!gr.Groupe.ToUpper().Contains("CONSULTATION") && !gr.Groupe.ToUpper().Contains("PHARMACIE"))
                          {
                              var listeAnalyse = from a in AppCode.ConnectionClassClinique.ListeDesAnalyses()
                                                 join g in AppCode.ConnectionClassClinique.ListeDesGroupes()
                                                 on a.NumeroGroupe equals g.NumeroGroupe
                                                 where g.Groupe.StartsWith(gr.Groupe, StringComparison.CurrentCultureIgnoreCase)
                                                 where g.Libelle == gr.Libelle
                                                 select new
                                         {
                                             a.NumeroListeAnalyse,
                                             a.TypeAnalyse,
                                             g.Groupe,
                                             g.Libelle
                                         };
                              //if (checkBox2.Checked)
                              //{
                     
                                  if (listeAnalyse.Count() > 0)
                                  {
                                      dataGridView2.Rows.Add(
                                         "Acte " + gr.Groupe + " " + gr.Libelle, " ", " ", " ", " ", " ",""
                                      );
                                      dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                                      var sousTotal = 0.0;
                                      foreach (var analyse in listeAnalyse)
                                      {
                                          var dtAnalyse = new DataTable();
                                          if (cmbConventionne.Text == "" || cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                                          {
                                              dtAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectuesChiffresParMedecin(cmbNomEmpoye.Text, analyse.TypeAnalyse,
                                                                                          dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                              foreach (DataRow dtRow in dtAnalyse.Rows)
                                              {
                                                      dataGridView2.Rows.Add(
                                                      dtRow.ItemArray[2].ToString(),
                                                        string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[4].ToString())),
                                                        string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[3].ToString())),
                                                       string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[3].ToString()) * Double.Parse(dtRow.ItemArray[4].ToString()),"")
                                                       , 0, 0
                                                          );
                                                  
                                                  
                                                      sousTotal += Double.Parse(dtRow.ItemArray[3].ToString()) * Double.Parse(dtRow.ItemArray[4].ToString());
                                                  
                                              }
                                             
                                              dtAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectuesChiffresTousLesConventionnes(analyse.TypeAnalyse,
                                                                                   dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                              foreach (DataRow dtRows in dtAnalyse.Rows)
                                              {
                                                  var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("EXAMEN", Int32.Parse(dtRows.ItemArray[0].ToString()));
                                                  if (flag)
                                                  {
                                                      total += Double.Parse(dtRows.ItemArray[3].ToString()) * Double.Parse(dtRows.ItemArray[4].ToString());                                                  
                                                  }
                                              }
                                          }
                                          else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                                          {
                                              dtAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectuesChiffresTousLesConventionnes(analyse.TypeAnalyse,
                                                                                           dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                              foreach (DataRow dtRow in dtAnalyse.Rows)
                                              {
                                                  var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("EXAMEN", Int32.Parse(dtRow.ItemArray[0].ToString()));
                                                  if (flag)
                                                  {
                                                      dataGridView2.Rows.Add(
                                                      dtRow.ItemArray[2].ToString(),
                                                        string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[4].ToString())),
                                                        string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[3].ToString())),
                                                       string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[3].ToString()) * Double.Parse(dtRow.ItemArray[4].ToString()))
                                                       , 0, 0,""
                                                          );
                                                      total += Double.Parse(dtRow.ItemArray[3].ToString()) * Double.Parse(dtRow.ItemArray[4].ToString());
                                                      sousTotal += Double.Parse(dtRow.ItemArray[3].ToString()) * Double.Parse(dtRow.ItemArray[4].ToString());
                                                  }
                                              }
                                          }
                                          else
                                          {
                                              dtAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectuesChiffresParCOnvention(cmbConventionne.Text, analyse.TypeAnalyse,
                                                                                           dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                              foreach (DataRow dtRow in dtAnalyse.Rows)
                                              {
                                                  var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("EXAMEN", Int32.Parse(dtRow.ItemArray[0].ToString()));
                                                  if (flag)
                                                  {
                                                      dataGridView2.Rows.Add(
                                                      dtRow.ItemArray[2].ToString(),
                                                        string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[4].ToString())),
                                                        string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[3].ToString())),
                                                       string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[3].ToString()) * Double.Parse(dtRow.ItemArray[4].ToString()))
                                                       , 0, 0,""
                                                          );
                                                      total += Double.Parse(dtRow.ItemArray[3].ToString()) * Double.Parse(dtRow.ItemArray[4].ToString());
                                                      sousTotal += Double.Parse(dtRow.ItemArray[3].ToString()) * Double.Parse(dtRow.ItemArray[4].ToString());
                                                  }
                                              }
                                          }
                                         
                                      }
                                      dataGridView2.Rows.Add(
                                       "Sous Total  ", " ", " ", string.Format(elGR, "{0:0,0}", sousTotal), " ", " ",""
                                    );
                                      dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                                      dataGridView2.Rows.Add(
                                         "", " ", " ", " ", " ", " ",""
                                      );
                                  }

                          }
                      }
                      dataGridView2.Rows.Add(
                       "Total  ", " ", " ", string.Format(elGR, "{0:0,0}", total), " ", " "
                    );
                      dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                 //RapportParConventionnee();
                 //ListeDesExamens();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("rapport examen", ex);
            }
        }

        //rapport des consultations
        void ListeDesConsultation(DataTable dtConsultation, DateTime date1, DateTime date2,int indexConventionne)
        {
            try
            {
                dataGridView1.Rows.Clear();
                label4.Text = "";
                textBox1.Text = "";
                var total =0.0;
                foreach (DataRow dtRow in dtConsultation.Rows)
                {
                        var consultation = dtRow.ItemArray[0].ToString().ToUpper();
                        if (consultation.ToUpper().Contains("Specialit".ToUpper()))
                        {
                            consultation = "CONSULTATION EN " + dtRow.ItemArray[4].ToString().ToUpper();
                        }
                        if (indexConventionne == 1)
                        {
                            dataGridView1.Rows.Add(
                                 consultation.ToUpper(),
                                   string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[1].ToString())),
                                   string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[2].ToString())),
                                   string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[3].ToString())), 0, 0
                             );
                            total += Double.Parse(dtRow.ItemArray[3].ToString());
                        }
                        else
                        {
                            var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("CONSULTATION", Int32.Parse(dtRow.ItemArray[5].ToString()));
                            if (flag)
                            {
                                dataGridView1.Rows.Add(
                                    consultation.ToUpper(),
                                      string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[1].ToString())),
                                      string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[2].ToString())),
                                      string.Format(elGR, "{0:0,0}", Double.Parse(dtRow.ItemArray[3].ToString())), 0, 0
                                );
                                total += Double.Parse(dtRow.ItemArray[3].ToString());
                            }
                        }
                }
                if (cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                {
                    total = 0;
                    var requete = "SELECT  det_fact.design, COUNT(det_fact.qte) ,det_fact.prix, SUM(det_fact.prix_total) FROM det_fact INNER JOIN" +
                             " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE det_fact.groupage   = 'Consultation' AND " +
                             "  facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 GROUP BY   det_fact.design,det_fact.prix";
                    var dtCaisse = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                    foreach (DataRow dtRow in dtCaisse.Rows)
                    {
                        total += Double.Parse(dtRow.ItemArray[3].ToString());
                    }
                    dtConsultation = ConnectionClassClinique.ListeConsultationChiffreDeTousLesConvention(dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    foreach (DataRow dtRow in dtConsultation.Rows)
                    {
                        var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("CONSULTATION", Int32.Parse(dtRow.ItemArray[5].ToString()));
                        if (flag)
                        {
                            total += Double.Parse(dtRow.ItemArray[3].ToString());
                        }
                    }
                }
                dataGridView1.Rows.Add(
                                "Totale consultation","",  "",string.Format(elGR, "{0:0,0}",total), 0, 0
                            );
                dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("rapport consultation", ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        Bitmap rapportMedecin;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(rapportMedecin, -10, 0, rapportMedecin.Width, rapportMedecin.Height);
                e.HasMorePages = false;
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void cmbNomEmpoye_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbNomEmpoye.Items.Clear();
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                if (listeEmploye.Count > 0)
                {
                    var list = from l in listeEmploye
                               where !l.NomEmployee.ToUpper().Contains("EXTERNE")
                               select l.NomEmployee;
                    foreach (var empl in list )
                    {
                        cmbNomEmpoye.Items.Add(empl);
                    }
                    cmbNomEmpoye.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmbNomEmpoye.DroppedDown = true;

                }
            }
        }

        private void cmbNomEmpoye_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNomEmpoye.Text = cmbNomEmpoye.SelectedText;
            cmbNomEmpoye.DropDownStyle = ComboBoxStyle.Simple;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox5.Visible = false;
            groupBox2.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = false ;
            groupBox5.Visible = true ;
            groupBox2.Visible = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                dataGridViewTextBoxColumn3.Width = dataGridView1.Width / 9;
                dataGridViewTextBoxColumn4.Width = dataGridView1.Width / 9;
                clPart.Width = dataGridView1.Width / 9;
                dataGridViewTextBoxColumn3.Width = dataGridView1.Width / 2 - 130;
                clPartT.Width = dataGridView1.Width / 9;
                clMontant.Width = dataGridView1.Width / 9;
 
                clPart.Visible = true;
                clPartT.Visible = true;
                clMontant.Visible = true;
                clFrais.Visible = true;
                textBox1.Visible = true;
                label4.Visible = true ;
                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
                clFrais.Visible = false;
                clMontant.Visible = false;
                textBox1.Visible = false ;
                label4.Visible = false;  
                clPart.Visible = false;
                clPartT.Visible = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            { 
                clPart1.Width = dataGridView1.Width /9;
                clMontant1.Width = dataGridView1.Width / 9;
                Column2.Width = dataGridView1.Width / 9;
                Column1.Width = dataGridView1.Width / 2-130;
                clPartT1.Width = dataGridView1.Width /9;
                clFrais1.Width = dataGridView1.Width / 9;
                clPart1.Visible = true;
                clPartT1.Visible = true;
                clMontant1.Visible = true;
                clFrais1.Visible = true;
                textBox2.Visible = true;
                textBox2.Text = "";
                label5.Visible = true;
                textBox2.Focus();
            }
            else
            {
                clMontant1.Visible = false;
                clFrais1.Visible = false;
                clPart1.Visible = false ;
                clPartT1.Visible = false;
                textBox2.Visible = false;
                label5.Visible = false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    double part,montant;
                    var total = 0.0;
                    if (Double.TryParse(textBox1.Text, out part))
                    {
                        foreach (DataGridViewRow dgRow in dataGridView2.Rows)
                        {
                            if (Double.TryParse(dgRow.Cells[3].Value.ToString(), out montant))
                            {
                                if (!dgRow.Cells[0].Value.ToString().Contains("SOUS TOTAL"))
                                {
                                    var partMedecin = montant * part / 100;
                                    dgRow.Cells[4].Value = string.Format(elGR, "{0:0,0}", part);
                                    dgRow.Cells[5].Value = string.Format(elGR, "{0:0,0}", partMedecin);
                                    total += partMedecin;
                                }
                            }
                        }
                        label4.Text =string.Format(elGR, "{0:0,0}",  total);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    double part, total = 0.0;
                    if (Double.TryParse(textBox2.Text, out part))
                    {
                        foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                        {
                            var montant = Double.Parse(dgRow.Cells[3].Value.ToString());
                            var partMedecin = montant * part / 100;
                            dgRow.Cells[4].Value =string.Format(elGR, "{0:0,0}",  part);
                            dgRow.Cells[5].Value = string.Format(elGR, "{0:0,0}", partMedecin);
                            total += partMedecin;
                        }
                        label5.Text = string.Format(elGR, "{0:0,0}", total);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double part, total = 0.0;
                bool state = true ;
                    foreach (DataGridViewRow dgRow in dataGridView2.Rows)
                    {
                        if (Double.TryParse(dgRow.Cells[4].Value.ToString(), out part))
                        {
                            var montant = Double.Parse(dgRow.Cells[3].Value.ToString());
                            var partMedecin = montant * part / 100;
                            dgRow.Cells[4].Value = part;
                            dgRow.Cells[5].Value = partMedecin;
                            total += partMedecin;
                        }
                        else
                        {
                            state = false;
                        }
                        label4.Text = total.ToString();
                    }
                    if (!state)
                    {
                        MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour la part du medecin", "Erreur", "erreur.png");
                    }
                
                
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double part, total = 0.0;
                bool state = true;
                foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                {
                    if (Double.TryParse(dgRow.Cells[4].Value.ToString(), out part))
                    {
                        CalulerTotalConsultation();
                    }
                    else
                    {
                        state = false;
                    }
                    //label5.Text = total.ToString();
                }
                if (!state)
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour la part du medecin", "Erreur", "erreur.png");
                }


            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                GestionPharmacetique.AppCode.Employe employe = null;
                string titre = "";

                if (!string.IsNullOrWhiteSpace(cmbNomEmpoye.Text))
                {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                    foreach (var empl in listeEmploye)
                        employe = empl;
                    titre = "Rapport de " + cmbNomEmpoye.Text + " du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                }
                else
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString();
                    if (cmbConventionne.Text == "" || cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                    {
                    }
                    else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                    {
                        titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des conventionnés";
                    }
                    else if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                    {
                        titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des particuliers";
                    }
                    else
                    {
                        titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " de " + cmbConventionne.Text;
                    }

                }

                var rowCount = dataGridView1.Rows.Count;

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
                pathFolder = pathFolder + "\\Rapport de la medecin";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                sfd.FileName = " Rapport des actes " + date + ".pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //sharpPDF.pdfPage pageIndex = document.addPage();
                    var Count = dataGridView2.Rows.Count / 31;

                    for (var i = 0; i <= Count; i++)
                    {
                        if (i * 37 < dataGridView2.Rows.Count)
                        {
                            var rapportMedecin = Impression.RapportDesAnalysesDunMedecin(dataGridView2, employe, titre, i, etat);

                            var inputImage = @"cdali" + i;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(rapportMedecin, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);

                           // rapportMedecin = Impression.RapportDesAnalysesDunMedecin(dataGridView2, employe, titre, i,etat);
                           //var  inputImage = @"cdali" + i;
                           // // Create an empty page
                           // pageIndex = document.addPage();

                           // document.addImageReference(rapportMedecin, inputImage);
                           // sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                           // pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                        }
                    }

                }

                document.createPDF(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GestionPharmacetique.AppCode.Employe employe= new Employe();
            string titre="";
           if (cmbNomEmpoye.Text != null)
            {
                var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                var id = listeEmpl[0].NumMatricule;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                foreach (var empl in listeEmploye)
                    employe = empl; titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();

            }
            else
            {
                employe = null;
                ; titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                if (cmbConventionne.Text == "" || cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                {
                }
                else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des conventionnés";
                }
                else if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des particuliers";
                }
                else
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " de " + cmbConventionne.Text;
                }

            }
          
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                var Count = (dataGridView1.Rows.Count) / 31;
                for (var i = 0; i <= Count; i++)
                {
                    if (i * 31 < dataGridView1.Rows.Count)
                    {
                        rapportMedecin = Impression.RapportDesAnalysesDunMedecin(dataGridView1, employe, titre, i, etat);
                        printDocument1.Print();
                    }
                }

            }
        
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GestionPharmacetique.AppCode.Employe employe=new  GestionPharmacetique.AppCode.Employe();
            string titre = "";
           if (cmbNomEmpoye.Text != "")
            {
                var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                var id = listeEmpl[0].NumMatricule;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                foreach (var empl in listeEmploye)
                    employe = empl;
                titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();

            }
            else
            {
                employe = null;
                ; titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                if (cmbConventionne.Text == "" || cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                {
                }
                else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des conventionnés";
                }
                else if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des particuliers";
                }
                else
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " de " + cmbConventionne.Text;
                }
            }

           rapportMedecin = Impression.RapportDesAnalysesDunMedecin(dataGridView1, employe, titre, 0, etat);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
            //}
        }
        //System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR"); 

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    for (var i = 0; i < dataGridView2.SelectedRows.Count; i++)
                    {
                        dataGridView2.Rows.Remove(dataGridView2.SelectedRows[i]);
                        CalulerTotalExamen();
                    }
                }
            }
            catch { }
        }
        void CalulerTotalExamen()
        {
            try
            {
                double prix, nombre, pourcentage, montantTotal=0.0, partMedecinTotal = 0.0;
                for (var i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if(double.TryParse(dataGridView2.Rows[i].Cells[1].Value.ToString(), out nombre ))
                    {
                        if (double.TryParse(dataGridView2.Rows[i].Cells[2].Value.ToString(), out prix))
                        {
                            if (double.TryParse(dataGridView2.Rows[i].Cells[4].Value.ToString(), out pourcentage ))
                            {
                                if (dataGridView2.Rows[i].Cells[0].Value.ToString().Equals("SOUS TOTAL"))
                                {
                                    var total = nombre * prix;
                                    var partMedecin = nombre * prix * pourcentage / 100;
                                    dataGridView2.Rows[i].Cells[3].Value = total;
                                    dataGridView2.Rows[i].Cells[5].Value = partMedecin;
                                    montantTotal += total;
                                    partMedecinTotal += partMedecin;
                                }
                            }
                        }
                    }

                }

                //label11.Text =   string.Format(elGR, "{0:0,0}", montantTotal);
                label4.Text =   string.Format(elGR, "{0:0,0}", partMedecinTotal);
            }
            catch { }

        }
        void CalulerTotalConsultation()
        {
            try
            {
                double prix, nombre, pourcentage, montantTotal = 0.0, partMedecinTotal = 0.0;
                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (double.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out nombre))
                    {
                        if (double.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out prix))
                        {
                            if (double.TryParse(dataGridView1.Rows[i].Cells[4].Value.ToString(), out pourcentage))
                            {
                                var total = nombre * prix;
                                var partMedecin = nombre * prix * pourcentage / 100;
                                dataGridView1.Rows[i].Cells[3].Value = total;
                                dataGridView1.Rows[i].Cells[5].Value = partMedecin;
                                montantTotal += total;
                                partMedecinTotal += partMedecin;
                            }
                        }
                    }

                }

                //label10.Text =   string.Format(elGR, "{0:0,0}",  montantTotal);
                label5.Text =   string.Format(elGR, "{0:0,0}",  partMedecinTotal);
            }
            catch { }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    for (var i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                        CalulerTotalConsultation();
                    }
                }
            }
            catch { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    rapportMedecin = Impression.RapportRecuPaiementDunMedecin
                        (dataGridView1,dataGridView2, cmbNomEmpoye.Text, dtp1.Value, dtp2.Value);
                    printPreviewDialog1.ShowDialog();
                }
            }
            catch { }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
        bool etat;
   
        private void cmbConventionne_SelectedIndexChanged(object sender, EventArgs e)
        {
                RapportParConventionnee();
                ListeDesExamens();
        }

        void RapportParConventionnee()
        {
            try
            {
                var nomConventionne = "";
                var dtConsultation = new DataTable();
                var dtPharmacie = new DataTable();
                int indesx = 0;
                if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                {
                    dtPharmacie = ConnectionClassPharmacie.ListeDesVentesDesParticuliers(dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    RecettesMensuellesDeLaCaisseEtPharmacie();
                }
                else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                {    
                    dtPharmacie = ConnectionClassPharmacie.ListeDesVentesDeTousLesConventionnes(dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                 
                        dtConsultation = ConnectionClassClinique.ListeConsultationChiffreDeTousLesConvention(dtp1.Value.Date, dtp2.Value.Date.AddHours(24));                     
                
                }
                else if (cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                {    
                    dtPharmacie = ConnectionClassPharmacie.ListeDesVentesPharmacie(dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    nomConventionne = "";
                    indesx = 1;
                        dtConsultation = ConnectionClassClinique.ListeConsultationChiffreParConvention(nomConventionne, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                   
                }
                else
                {
                    nomConventionne = cmbConventionne.Text;
                    dtPharmacie = ConnectionClassPharmacie.ListeDesVentesPharmacieParConvention(nomConventionne, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                 
                        dtConsultation = ConnectionClassClinique.ListeConsultationChiffreParConvention(nomConventionne, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                 
                }
                ListeDesConsultation(dtConsultation, dtp1.Value.Date, dtp2.Value.Date.AddHours(24), indesx);
                ListeDesRapportsVentes(dtPharmacie);
            }
            catch { }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            groupBox2.Visible = true ;
        }

        private void ListeDesRapportsVentes(DataTable dataTable)
        {
            try
            {
                var Montant = 0.0;
                dataGridView3.Rows.Clear();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var total = double.Parse(dataTable.Rows[i].ItemArray[1].ToString()) *
                        Int32.Parse(dataTable.Rows[i].ItemArray[2].ToString());
                    dataGridView3.Rows.Add(
                        dataTable.Rows[i].ItemArray[0].ToString().ToUpper(),
                        string.Format(elGR, "{0:0,0}", double.Parse(dataTable.Rows[i].ItemArray[2].ToString())),
                         string.Format(elGR, "{0:0,0}", double.Parse(dataTable.Rows[i].ItemArray[1].ToString())),
                        string.Format(elGR, "{0:0,0}", total)
                    );


                }
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    Montant += double.Parse(dataGridView3.Rows[i].Cells[3].Value.ToString());
                }
                dataGridView3.Rows.Add(
                "Totale Pharmacie","","",
                 string.Format(elGR, "{0:0,0}", Montant)
             );

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                  var  titre = "Rapport de la pharmacie " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                    if (cmbConventionne.Text == "" || cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                    {
                    }
                    else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                    {
                        titre = "Rapport de la pharmacie " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des conventionnés";
                    }
                    else if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                    {
                        titre = "Rapport de la pharmacie " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des particuliers";
                    }
                    else
                    {
                        titre = "Rapport de la pharmacie " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " de " + cmbConventionne.Text;
                    }
                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var mois = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                    var pathFolder = "C:\\Dossier Pharmacie";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    pathFolder = pathFolder + "\\Rapport vente";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    sfd.InitialDirectory = pathFolder;
                    var s = titre;
                    if (titre.Contains("/"))
                    { s = s.Replace("/", "_"); }
                    sfd.FileName = s + "_imprimé_le_" + date + ".pdf";

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        var count = dataGridView3.Rows.Count;

                        var index = (dataGridView3.Rows.Count) / 50;

                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 50 < count)
                            {
                                var _listeImpression = Impression.ImprimerRapportDesVentes(titre, dataGridView3, i);

                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage pageIndex = document.addPage();

                                document.addImageReference(_listeImpression, inputImage);
                                sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch { }
        }

        void RecettesMensuellesDeLaCaisseEtPharmacie()
        {
            try
            {


                dataGridView2.Rows.Clear();
                dataGridView1.Rows.Clear();
                var totalRecettesCaisse = .0;

                var totalRecettesCaisseduJour = .0;
                //dataGridView2.Rows.Add(
                //   dtp1.Value .ToShortDateString() + " au " + dtp2.Value.ToShortDateString(),
                //             "", " ", ""
                //              );

                var listeLibelle =from f in  AppCode.ConnectionClassClinique.ListeDesLibellesDistingues()
                                      //where f.Designation !="Vente médicaments"
                                      select f;
                foreach (var libelle in listeLibelle)
                {
                    var montantRecettesCaisse = 0.0;
                    if (!string.IsNullOrWhiteSpace(libelle.Sub))
                    {
                        dataGridView2.Rows.Add(
                                      "Acte " + libelle.Sub, "", "", "", "", "");
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        if (libelle.Designation == "Consultation")
                        {
                            dataGridView1.Rows.Add(
                                  "Acte " + libelle.Designation, "", "", "", "", "");
                        }
                        else if (libelle.Designation != "Consultation" && libelle.Designation.ToUpper().Contains("medicament".ToUpper()))
                        {
                            dataGridView2.Rows.Add(
                                "Acte " + libelle.Designation, "", "", "", "", "");
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                        }
                    }
                    if (libelle.Designation == "Consultation")
                    {
                        var sousTotalRecettesCaisse = .0;
                        var requete = "SELECT  det_fact.design, COUNT(det_fact.qte) ,det_fact.prix, SUM(det_fact.prix_total) FROM det_fact INNER JOIN" +
                                                " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE(det_fact.groupage = 'Consultation') " +
                                                "AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 GROUP BY   det_fact.design,det_fact.prix";
                        var dtCaisse = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                        foreach (DataRow dtRow in dtCaisse.Rows)
                        {

                            montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[3].ToString());
                            dataGridView1.Rows.Add(
                                        
                                         dtRow.ItemArray[0].ToString() ,
                                        string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[1].ToString())),
                                        string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[2].ToString())),
                                        string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[3].ToString())),"",""
                                         );
                            //totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            //totalRecettesCaisseduJour += montantRecettesCaisse;
                        }
                        dataGridView1.Rows.Add(
                                       
                                        "Total " + libelle.Designation, "","",
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "",    ""
                                        );

                        dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;

                    }

                    else if (libelle.Designation.Contains("Laboratoire"))
                    {
                        var sousTotalRecettesCaisse = .0;


                        var listeLib = from lib in AppCode.ConnectionClassClinique.ListeDesLibelles()
                                       where lib.Designation == libelle.Designation
                                       select lib.Sub;

                        foreach (var li in listeLib)
                        {
                            dataGridView2.Rows.Add(
                                     "Acte " + li , " ", " ", " ", " ", " ",""
                                  );
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            montantRecettesCaisse = 0;
                            var liste = from re in AppCode.ConnectionClassClinique.RecettesLaboratoire(dtp1.Value.Date, dtp2.Value.Date)
                                        where re.Sub.ToLower().Contains(li.ToLower())
                                        select re;
                            foreach (var re in liste)
                            {
                                dataGridView2.Rows.Add(

                                     re.Designation,
                                      string.Format(elGR, "{0:0,0}", re.Quantite),
                                      string.Format(elGR, "{0:0,0}",re.Prix),
                                      string.Format(elGR, "{0:0,0}", re.PrixTotal), "", "","VOIR PRODUIT"
                                       );
                                montantRecettesCaisse += re.PrixTotal;
                            }
                            //montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[2].ToString());
                            dataGridView2.Rows.Add(
                                       "Total " + li,"","",
                                        string.Format(elGR, "{0:0,0}", montantRecettesCaisse), "","",""
                                         );

                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            totalRecettesCaisseduJour += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation,"","",
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse),"","",""
                                        );

                    }
                    else
                    {
                        if (libelle.Designation == "Vente médicaments")
                        {
                            dataGridView2.Rows.Add(
                                          "Acte therapie", "", "", "", "", "");
                        }
                        else
                        {
                            dataGridView2.Rows.Add(
                                      "Acte " + libelle.Designation, " ", " ", " ", " ", " "
                                   );
                        }
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                        var sousTotalRecettesCaisse = .0;
                        var requete = "SELECT  det_fact.design, COUNT(det_fact.design),det_fact.prix, SUM(det_fact.prix_total) FROM det_fact INNER JOIN" +
                                                " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE(det_fact.groupage = '" + libelle.Designation + "') " +
                                                "AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 GROUP BY   det_fact.design,det_fact.prix";
                        var dtCaisse = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                        foreach (DataRow dtRow in dtCaisse.Rows)
                        {

                            montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[3].ToString());
                            dataGridView2.Rows.Add(
                                        dtRow.ItemArray[0].ToString(),
                                               string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[1].ToString())),
                                                   string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[2].ToString())),
                                        string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[3].ToString())), "",""
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            totalRecettesCaisseduJour += montantRecettesCaisse;
                        }
                        if (libelle.Designation == "Vente médicaments")
                        {
                            dataGridView2.Rows.Add(
                                          "Total therapie", "", "",
                                           string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "");
                        }
                        else
                        {
                            dataGridView2.Rows.Add(
                                            "Total " + libelle.Designation, "", "",
                                           string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", ""
                                            );
                        }
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }

                    dataGridView2.Rows.Add(
                                    "",  "", "", "", "", "");
                }
                dataGridView2.Rows.Add(
                                  "Total Général ","","",
                                  string.Format(elGR, "{0:0,0}", totalRecettesCaisse), "",""
                                   );
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Recettes caisse", ex);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex ==6)
            {
                var frm = new GestionDuneClinique.FormesClinique.ProduitFrm();
                frm.nomProduit = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                frm.nombreExamen = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                frm.dateDebut = dtp1.Value.Date;
                frm.dateFin = dtp2.Value.Date;
                frm.ShowDialog();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
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
                sfd.FileName = "Liste des actes de _Impriméé_le_" + date + ".xls";
              
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToCsV1(dataGridView3, sfd.FileName); // Here dataGridview1 is your grid view name
                }
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception)
            {
            }
        }

        private void ToCsV1(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 0; j < dGV.Columns.Count; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
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
            catch (Exception)
            {
            }

        }


        private void button5_Click_1(object sender, EventArgs e)
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
                sfd.FileName = "Liste des actes de _Impriméé_le_" + date + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToCsV1(dataGridView2, sfd.FileName); // Here dataGridview1 is your grid view name
                }
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception)
            {
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
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
                sfd.FileName = "Liste des actes de _Impriméé_le_" + date + ".xls";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToCsV1(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                }
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception)
            {
            }
        }
    }
}
