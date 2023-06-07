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
            //clFrais1.Width = dataGridView1.Width / 9;
            
            clMontant.Visible = true;
            clMontant1.Visible = true;
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
               ListeDesExamens();
               ListeDesRapportsVentesMedicaments();
               ListeDesConsultation();
                var grandTotal = .0;
               if(dataGridView1.Rows.Count>0)
                    grandTotal += double.Parse(dataGridView1.Rows[dataGridView1.Rows.Count-1].Cells[2].Value.ToString());
                if (dataGridView2.Rows.Count > 0)
                    grandTotal += double.Parse(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value.ToString());
                if (dataGridView3.Rows.Count > 0)
                    grandTotal += double.Parse(dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[2].Value.ToString());
                label10.Text = string.Format(elGR, "{0:0,0}", grandTotal);
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
                    RecettesDesActesDesPatients();
                }
                else if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                {
                    RecettesDesActesDesParticuliers();
                }else if(cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                {
                    RecettesDesActesTousConventionne();
                }
                else
                {
                    RecettesDesActesParConventionne();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("rapport examen", ex);
            }
        }

        //rapport des consultations
        void ListeDesConsultation()
        {
            try
            {
                var dtConsultation = new DataTable();
                if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                {
                    var requete = "SELECT  det_fact.design, COUNT(det_fact.qte) , SUM(det_fact.prix_total) FROM det_fact INNER JOIN" +
                               " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE det_fact.groupage   = 'Consultation' AND " +
                               "  facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2  AND facture_tbl.sub IS NULL   GROUP BY det_fact.design";
                    dtConsultation = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(
                  "Acte Consultation", "", "", "", "");
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    var Montant = .0;
                    for (int i = 0; i < dtConsultation.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(
                            dtConsultation.Rows[i].ItemArray[0].ToString().ToUpper(),
                            string.Format(elGR, "{0:0,0}", double.Parse(dtConsultation.Rows[i].ItemArray[1].ToString())),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtConsultation.Rows[i].ItemArray[2].ToString())), "", ""
                        );
                        Montant += double.Parse(dtConsultation.Rows[i].ItemArray[2].ToString());
                    }
                    dataGridView1.Rows.Add(
                    "Total Consultation", "",
                     string.Format(elGR, "{0:0,0}", Montant), "", "");
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else if (cmbConventionne.Text == "<<TOUS LES PATIENTS>>" || string.IsNullOrWhiteSpace(cmbConventionne.Text))
                {
                  var   requete = "SELECT  det_fact.design, COUNT(det_fact.qte) , SUM(det_fact.prix_total) FROM det_fact INNER JOIN" +
                                 " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE det_fact.groupage   = 'Consultation' AND " +
                                 "  facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2  AND facture_tbl.sub IS NULL   GROUP BY det_fact.design";
                    dtConsultation = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                     dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(
                   "Totale Consultation", "", "", "", "");
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    var Montant = .0;
                    for (int i = 0; i < dtConsultation.Rows.Count; i++)
                    {
                        var count = double.Parse(dtConsultation.Rows[i].ItemArray[1].ToString());
                        var total = double.Parse(dtConsultation.Rows[i].ItemArray[2].ToString());
                        requete = "SELECT  pro_det_fact.design, COUNT(pro_det_fact.qte) , SUM(pro_det_fact.prix_total) FROM pro_det_fact INNER JOIN " +
              " pro_facture_tbl ON pro_det_fact.id_fact = pro_facture_tbl.id_fact INNER JOIN patient_tbl ON patient_tbl.id = pro_facture_tbl.num_patient" +
              " WHERE pro_det_fact.groupage = 'Consultation' AND patient_tbl.entrep NOT LIKE '' AND pro_det_fact.design='"+
               dtConsultation.Rows[i].ItemArray[0].ToString() +"' group by  pro_det_fact.design";
                        var dtConsultationPro = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                         if(dtConsultationPro.Rows.Count>0)
                            {
                                count += double.Parse(dtConsultationPro.Rows[0].ItemArray[1].ToString());
                                total += double.Parse(dtConsultationPro.Rows[0].ItemArray[2].ToString());
                            }
                        
                        dataGridView1.Rows.Add(
                            dtConsultation.Rows[i].ItemArray[0].ToString().ToUpper(),
                            string.Format(elGR, "{0:0,0}", count),
                             string.Format(elGR, "{0:0,0}", total), "", ""
                        );
                        Montant +=total;
                    }
                    dataGridView1.Rows.Add(
                    "Totale Consultation", "",
                     string.Format(elGR, "{0:0,0}", Montant), "", "");
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                {
                    var requete = "SELECT  pro_det_fact.design, COUNT(pro_det_fact.qte) , SUM(pro_det_fact.prix_total) FROM pro_det_fact INNER JOIN " +
                     " pro_facture_tbl ON pro_det_fact.id_fact = pro_facture_tbl.id_fact INNER JOIN patient_tbl ON patient_tbl.id = pro_facture_tbl.num_patient" +
                     " WHERE pro_det_fact.groupage = 'Consultation' AND patient_tbl.entrep NOT LIKE '' group by  pro_det_fact.design";
                    dtConsultation = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(
                   "Totale Consultation", "", "", "", "");
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    var Montant = .0;
                    for (int i = 0; i < dtConsultation.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(
                            dtConsultation.Rows[i].ItemArray[0].ToString().ToUpper(),
                            string.Format(elGR, "{0:0,0}", double.Parse(dtConsultation.Rows[i].ItemArray[1].ToString())),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtConsultation.Rows[i].ItemArray[2].ToString())), "", ""
                        );
                        Montant += double.Parse(dtConsultation.Rows[i].ItemArray[2].ToString());
                    }
                    dataGridView1.Rows.Add(
                    "Acte Consultation", "",
                     string.Format(elGR, "{0:0,0}", Montant), "", "");
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else
                {
                    var requete = "SELECT  pro_det_fact.design, COUNT(pro_det_fact.qte) , SUM(pro_det_fact.prix_total) FROM pro_det_fact INNER JOIN " +
                               " pro_facture_tbl ON pro_det_fact.id_fact = pro_facture_tbl.id_fact INNER JOIN patient_tbl ON patient_tbl.id = pro_facture_tbl.num_patient" +
                               " WHERE pro_det_fact.groupage = 'Consultation' AND patient_tbl.entrep LIKE '" + cmbConventionne.Text + "' group by  pro_det_fact.design";
                    dtConsultation = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(
                   "Acte Consultation", "", "", "", "");
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    var Montant = .0;
                    for (int i = 0; i < dtConsultation.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(
                            dtConsultation.Rows[i].ItemArray[0].ToString().ToUpper(),
                            string.Format(elGR, "{0:0,0}", double.Parse(dtConsultation.Rows[i].ItemArray[1].ToString())),
                             string.Format(elGR, "{0:0,0}", double.Parse(dtConsultation.Rows[i].ItemArray[2].ToString())), "", ""
                        );
                        Montant += double.Parse(dtConsultation.Rows[i].ItemArray[2].ToString());
                    }
                    dataGridView1.Rows.Add(
                    "Totale Consultation", "",
                     string.Format(elGR, "{0:0,0}", Montant), "", "");
                    dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
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
           
                   var  titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString();
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

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                //rapportMedecin = Impression.RapportDesAnalysesDunMedecin(dataGridView2, employe, titre, 0, etat);
                printPreviewDialog1.ShowDialog();
            }
            
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
                textBox1.Visible = true;
                label4.Visible = true ;
                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
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
                //clFrais1.Width = dataGridView1.Width / 9;
                clPart1.Visible = true;
                clPartT1.Visible = true;
                clMontant1.Visible = true;
                //clFrais1.Visible = true;
                textBox2.Visible = true;
                textBox2.Text = "";
                label5.Visible = true;
                textBox2.Focus();
            }
            else
            {
                clMontant1.Visible = false;
                //clFrais1.Visible = false;
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
                    titre = "Rapport des actes de " + cmbNomEmpoye.Text + " du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                }
                else
                {
                    titre = "Rapport des actes du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString();
                    if (cmbConventionne.Text == "" || cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                    {
                    }
                    else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                    {
                        titre = "Rapport des actes du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des conventionnés";
                    }
                    else if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                    {
                        titre = "Rapport des actes du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des particuliers";
                    }
                    else
                    {
                        titre = "Rapport des actes du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " de " + cmbConventionne.Text;
                    }

                }
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
                pathFolder = pathFolder + "\\Rapport des actes";
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
                        if (i * 47 < dataGridView2.Rows.Count)
                        {
                            var rapportMedecin = Impression.ImprimerRapportDesActes(dataGridView2, titre, i);

                            var inputImage = @"cdali" + i;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(rapportMedecin, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                        }
                    }

                }

                document.createPDF(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch { }
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

           //rapportMedecin = Impression.RapportDesAnalysesDunMedecin(dataGridView1, employe, titre, 0, etat);
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
            try
            {
                ListeDesExamens();
                ListeDesRapportsVentesMedicaments();
                ListeDesConsultation();
                var grandTotal = .0;
                if (dataGridView1.Rows.Count > 0)
                    grandTotal += double.Parse(dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value.ToString());
                if (dataGridView2.Rows.Count > 0)
                    grandTotal += double.Parse(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value.ToString());
                if (dataGridView3.Rows.Count > 0)
                    grandTotal += double.Parse(dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[2].Value.ToString());
                label10.Text = string.Format(elGR, "{0:0,0}", grandTotal);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport medecin", ex);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            groupBox5.Visible = false;
            groupBox2.Visible = true ;
        }

        private void ListeDesRapportsVentesMedicaments()
        {
            try
            {
                double solde = 0;
                var dtPharmacie = new DataTable();
                if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                {
                    dtPharmacie = ConnectionClassPharmacie.ListeDesVentesDataTableNonHospitalisation(dtp1.Value.Date, dtp2.Value.Date);
                    var dt = AppCode.ConnectionClassPharmacie.ListeDesFactures("", dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    for (var l = 0; l < dt.Rows.Count; l++)
                    {
                        solde += double.Parse(dt.Rows[l].ItemArray[3].ToString());
                    }
                   
                }
                else if (cmbConventionne.Text == "<<TOUS LES PATIENTS>>" || string.IsNullOrWhiteSpace(cmbConventionne.Text))
                {
                    dtPharmacie = ConnectionClassPharmacie.ListeDesVentesPharmacieTousLesPatients(dtp1.Value.Date, dtp2.Value.Date);
                    
                    var dt = AppCode.ConnectionClassPharmacie.ListeDesFactures("", dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    for (var l = 0; l < dt.Rows.Count; l++)
                    {
                        solde += double.Parse(dt.Rows[l].ItemArray[3].ToString());
                    }

                }
                else if (cmbConventionne.Text=="<<TOUS LES CONVENTIONNES>>")
                {
                    dtPharmacie = ConnectionClassPharmacie.ListeDesVentesDeTousLesConventionnesNonHospitalise(dtp1.Value.Date, dtp2.Value.Date);
                }
                else
                {
                    dtPharmacie = ConnectionClassPharmacie.ListeDesVentesPharmacieParConventionNonHospitalisee(cmbConventionne.Text, dtp1.Value.Date, dtp2.Value.Date);
                }

                var Montant = 0.0;
                dataGridView3.Rows.Clear();
                for (int i = 0; i < dtPharmacie.Rows.Count; i++)
                {
                    var total = double.Parse(dtPharmacie.Rows[i].ItemArray[1].ToString()) *
                        Int32.Parse(dtPharmacie.Rows[i].ItemArray[2].ToString());
                    dataGridView3.Rows.Add(
                        dtPharmacie.Rows[i].ItemArray[0].ToString().ToUpper(),
                        string.Format(elGR, "{0:0,0}", double.Parse(dtPharmacie.Rows[i].ItemArray[2].ToString())),
                         string.Format(elGR, "{0:0,0}", double.Parse(dtPharmacie.Rows[i].ItemArray[1].ToString()))
                    );
                    Montant += double.Parse(dtPharmacie.Rows[i].ItemArray[1].ToString());
                }
                Montant = Montant - solde;
                dataGridView3.Rows.Add(
                "Totale Pharmacie", "",
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

                        var index = (dataGridView3.Rows.Count) / 47;

                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 47 < count)
                            {
                                var _listeImpression = Impression.ImprimerRapportDesActes(dataGridView3,titre, i);

                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage pageIndex = document.addPage();

                                document.addImageReference(_listeImpression, inputImage);
                                sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch { }
        }

        void RecettesDesActesDesParticuliers()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView1.Rows.Clear();
                var totalRecettesCaisse = .0;

                var totalRecettesCaisseduJour = .0;
                var listeLibelle =from f in  AppCode.ConnectionClassClinique.ListeDesLibellesDistingues()
                                   where !f.Designation.ToUpper().Contains("CONSULTATION")
                                  where !f.Designation.ToUpper().Contains("Vente médicaments".ToUpper())
                                  select f;
                foreach (var libelle in listeLibelle)
                {
                    var montantRecettesCaisse = 0.0;
                                    
                    if (libelle.Designation.Contains("Laboratoire"))
                    {
                        var sousTotalRecettesCaisse = .0;


                        var listeLib = from lib in AppCode.ConnectionClassClinique.ListeDesLibelles()
                                       where lib.Designation == libelle.Designation
                                       select lib.Sub;

                        foreach (var li in listeLib)
                        {
                            dataGridView2.Rows.Add(
                                     "Acte " + li, " ", " ", " ", " ", ""
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
                                      string.Format(elGR, "{0:0,0}", re.PrixTotal), "", "", ""
                                       );
                                montantRecettesCaisse += re.PrixTotal;
                            }
                            //montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[2].ToString());
                            dataGridView2.Rows.Add(
                                       "Total " + li, "", 
                                        string.Format(elGR, "{0:0,0}", montantRecettesCaisse), "", "", ""
                                         );
                            dataGridView2.Rows.Add(
                                         "", "", "", "", "", "");
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            totalRecettesCaisseduJour += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation, "", 
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "",""
                                        );

                    }
                    else if (libelle.Designation.Contains("Hospitalisation"))
                    {
                        dataGridView2.Rows.Add(
                         libelle.Designation, "","", "", "", ""
                            );
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                        var sousTotalRecettesCaisse = .0;
                        var dtHos = AppCode.ConnectionClassPharmacie.ListeDesVentesDataTable(dtp1.Value.Date, dtp2.Value.Date);

                        foreach (DataRow dtRow in dtHos.Rows)
                        {

                            montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[1].ToString());

                            dataGridView2.Rows.Add(
                                          libelle.Designation,
                                        string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[2].ToString())),
                                  string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[1].ToString()),"","","")
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            totalRecettesCaisseduJour += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation, "", 
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse),"", "",""
                                        );

                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                            dataGridView2.Rows.Add(
                                      "Acte " + libelle.Designation, "", "", "", "", ""
                                   );
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                        
                        var sousTotalRecettesCaisse = .0;
                        var requete = "SELECT  det_fact.design, COUNT(det_fact.design), SUM(det_fact.prix_total* det_fact.pourcentage/100) FROM det_fact INNER JOIN" +
                                                " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE(det_fact.groupage = '" + libelle.Designation + "') " +
                                                "AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2  AND facture_tbl.sub IS NULL   GROUP BY   det_fact.design";
                        var dtCaisse = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                        foreach (DataRow dtRow in dtCaisse.Rows)
                        {

                            montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[2].ToString());
                            dataGridView2.Rows.Add(
                                        dtRow.ItemArray[0].ToString(),
                                               string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[1].ToString())),
                                        string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[2].ToString())),"", "", ""
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            totalRecettesCaisseduJour += montantRecettesCaisse;
                        }  
dataGridView2.Rows.Add(
                                            "Total " + libelle.Designation, "", 
                                           string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "",""
                                            );
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                        
                    }

                    dataGridView2.Rows.Add(
                                    "",  "", "", "","",  "");
                }
                dataGridView2.Rows.Add(
                                  "Total Général ","",
                                  string.Format(elGR, "{0:0,0}", totalRecettesCaisse), "","",""
                                   );
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Recettes caisse", ex);
            }
        }
        void RecettesDesActesParConventionne()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var totalRecettesCaisse = .0;
                
                var listeLibelle = from f in AppCode.ConnectionClassClinique.ListeDesLibellesDistingues()
                                   where !f.Designation.ToUpper().Contains("CONSULTATION")
                                   where !f.Designation.ToUpper().Contains("Vente médicaments".ToUpper())
                                   select f;
                foreach (var libelle in listeLibelle)
                {
                    var montantRecettesCaisse = 0.0;

                    if (libelle.Designation.Contains("Laboratoire"))
                    {
                        var sousTotalRecettesCaisse = .0;


                        var listeLib = from lib in AppCode.ConnectionClassClinique.ListeDesLibelles()
                                       where lib.Designation == libelle.Designation
                                       select lib.Sub;

                        foreach (var li in listeLib)
                        {
                            dataGridView2.Rows.Add(
                                     "Acte " + li, " ", " ", " ", " ", ""
                                  );
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            montantRecettesCaisse = 0;
                            var liste = from re in AppCode.ConnectionClassClinique.RecettesLaboratoireConventionne( dtp1.Value.Date, dtp2.Value.Date, cmbConventionne.Text)
                                        where re.Sub.ToLower().Contains(li.ToLower())
                                        select re;
                            foreach (var re in liste)
                            {
                                dataGridView2.Rows.Add(

                                     re.Designation,
                                      string.Format(elGR, "{0:0,0}", re.Quantite),
                                      string.Format(elGR, "{0:0,0}", re.PrixTotal), "", "", ""
                                       );
                                montantRecettesCaisse += re.PrixTotal;
                            }
                            dataGridView2.Rows.Add(
                                       "Total " + li, "",
                                        string.Format(elGR, "{0:0,0}", montantRecettesCaisse), "", "", ""
                                         );

                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;

                            dataGridView2.Rows.Add(
                                            "", "", "", "", "", "");
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation, "",
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                        );

                    }
                    else if (libelle.Designation.Contains("Hospitalisation"))
                    {
                        dataGridView2.Rows.Add(
                                           libelle.Designation,
                                          "","","","",""      );
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                        var sousTotalRecettesCaisse = .0;
                        var dtHos = AppCode.ConnectionClassPharmacie.ListeDesVentesPharmacieParConventionHospitalisee(cmbConventionne.Text, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                        foreach (DataRow dtRow in dtHos.Rows)
                        {

                            montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[1].ToString());

                            dataGridView2.Rows.Add(
                                         dtRow.ItemArray[0].ToString(),
                                        string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[2].ToString())),
                                  string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[1].ToString()), "", "", "")
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation, "",
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                        );

                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        dataGridView2.Rows.Add(
                                  "Acte " + libelle.Designation, "", "", "", "", ""
                               );
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;

                        var sousTotalRecettesCaisse = .0;
                          var dtCaisse = AppCode.ConnectionClassClinique.RecettesAutresActesPourConventionne(libelle.Designation, cmbConventionne.Text, dtp1.Value.Date, dtp2.Value.Date);

                        foreach (var re in dtCaisse)
                        {

                            montantRecettesCaisse = re.PrixTotal;
                            dataGridView2.Rows.Add(
                                        re.Designation,
                                               string.Format(elGR, "{0:0,0}", re.Quantite),
                                        string.Format(elGR, "{0:0,0}", re.PrixTotal), "", "", ""
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                                                    "Total " + libelle.Designation, "",
                                                                   string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                                                    );
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;

                    }

                    dataGridView2.Rows.Add(
                                    "", "", "", "", "", "");
                }
                dataGridView2.Rows.Add(
                                  "Total Général ", "",
                                  string.Format(elGR, "{0:0,0}", totalRecettesCaisse), "", "", ""
                                   );
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Recettes caisse", ex);
            }
        }
        void RecettesDesActesTousConventionne()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var totalRecettesCaisse = .0;

                var listeLibelle = from f in AppCode.ConnectionClassClinique.ListeDesLibellesDistingues()
                                   where !f.Designation.ToUpper().Contains("CONSULTATION")
                                   where !f.Designation.ToUpper().Contains("Vente médicaments".ToUpper())
                                   select f;
                foreach (var libelle in listeLibelle)
                {
                    var montantRecettesCaisse = 0.0;

                    if (libelle.Designation.Contains("Laboratoire"))
                    {
                        var sousTotalRecettesCaisse = .0;


                        var listeLib = from lib in AppCode.ConnectionClassClinique.ListeDesLibelles()
                                       where lib.Designation == libelle.Designation
                                       select lib.Sub;

                        foreach (var li in listeLib)
                        {
                            dataGridView2.Rows.Add(
                                     "Acte " + li, " ", " ", " ", " ", ""
                                  );
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            montantRecettesCaisse = 0;
                            var liste = from re in AppCode.ConnectionClassClinique.RecettesLaboratoirePourTousConventionnes(dtp1.Value.Date, dtp2.Value.Date)
                                        where re.Sub.ToLower().Contains(li.ToLower())
                                        select re;
                            foreach (var re in liste)
                            {
                                dataGridView2.Rows.Add(

                                     re.Designation,
                                      string.Format(elGR, "{0:0,0}", re.Quantite),
                                      string.Format(elGR, "{0:0,0}", re.PrixTotal), "", "", ""
                                       );
                                montantRecettesCaisse += re.PrixTotal;
                            }
                            dataGridView2.Rows.Add(
                                       "Total " + li, "",
                                        string.Format(elGR, "{0:0,0}", montantRecettesCaisse), "", "", ""
                                         );

                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;

                            dataGridView2.Rows.Add(
                                            "", "", "", "", "", "");
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation, "",
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                        );

                    }
                    else if (libelle.Designation.Contains("Hospitalisation"))
                    {
                        dataGridView2.Rows.Add(
                                           libelle.Designation,
                                          "", "", "", "", "");
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                        var sousTotalRecettesCaisse = .0;
                        var dtHos = AppCode.ConnectionClassPharmacie.ListeDesVentesPharmacieTousConventionsHospitalisees( dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                        foreach (DataRow dtRow in dtHos.Rows)
                        {

                            montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[1].ToString());

                            dataGridView2.Rows.Add(
                                         dtRow.ItemArray[0].ToString(),
                                        string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[2].ToString())),
                                  string.Format(elGR, "{0:0,0}", Convert.ToDouble(dtRow.ItemArray[1].ToString()), "", "", "")
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation, "",
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                        );

                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        dataGridView2.Rows.Add(
                                  "Acte " + libelle.Designation, "", "", "", "", ""
                               );
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;

                        var sousTotalRecettesCaisse = .0;
                        var dtCaisse = AppCode.ConnectionClassClinique.RecettesAutresActesTousConventionnes(libelle.Designation, dtp1.Value.Date, dtp2.Value.Date);

                        foreach (var re in dtCaisse)
                        {

                            montantRecettesCaisse = re.PrixTotal;
                            dataGridView2.Rows.Add(
                                        re.Designation,
                                               string.Format(elGR, "{0:0,0}", re.Quantite),
                                        string.Format(elGR, "{0:0,0}", re.PrixTotal), "", "", ""
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                                                    "Total " + libelle.Designation, "",
                                                                   string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                                                    );
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;

                    }

                    dataGridView2.Rows.Add(
                                    "", "", "", "", "", "");
                }
                dataGridView2.Rows.Add(
                                  "Total Général ", "",
                                  string.Format(elGR, "{0:0,0}", totalRecettesCaisse), "", "", ""
                                   );
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Recettes caisse", ex);
            }
        }
        void RecettesDesActesDesPatients()
        {
            try
            {
                dataGridView2.Rows.Clear();
                dataGridView1.Rows.Clear();
                var totalRecettesCaisse = .0;

                var totalRecettesCaisseduJour = .0;
                var listeLibelle = from f in AppCode.ConnectionClassClinique.ListeDesLibellesDistingues()
                                   where !f.Designation.ToUpper().Contains("CONSULTATION")
                                   where !f.Designation.ToUpper().Contains("Vente médicaments".ToUpper())
                                   select f;
                foreach (var libelle in listeLibelle)
                {
                    var montantRecettesCaisse = 0.0;

                    if (libelle.Designation.Contains("Laboratoire"))
                    {
                        var sousTotalRecettesCaisse = .0;


                        var listeLib = from lib in AppCode.ConnectionClassClinique.ListeDesLibelles()
                                       where lib.Designation == libelle.Designation
                                       select lib.Sub;

                        foreach (var li in listeLib)
                        {
                            dataGridView2.Rows.Add(
                                     "Acte " + li, " ", " ", " ", " ", ""
                                  );
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            montantRecettesCaisse = 0;
                            var liste = from re in AppCode.ConnectionClassClinique.RecettesLaboratoire(dtp1.Value.Date, dtp2.Value.Date)
                                        where re.Sub.ToLower().Contains(li.ToLower())
                                        select re;
                            foreach (var re in liste)
                            {
                                var listePro = from rep in AppCode.ConnectionClassClinique.RecettesLaboratoirePourTousConventionnes(dtp1.Value.Date, dtp2.Value.Date)
                                            where rep.Sub.ToLower().Contains(li.ToLower())
                                            where rep.Designation==re.Designation
                                            select rep;
                                var count = re.Quantite;
                                var total = re.PrixTotal;
                                foreach(var pr in listePro )
                                {
                                    count += pr.Quantite;
                                    total += pr.PrixTotal;
                                }
                                dataGridView2.Rows.Add(

                                     re.Designation,
                                      string.Format(elGR, "{0:0,0}", count ),
                                      string.Format(elGR, "{0:0,0}", total), "", "", ""
                                       );
                                montantRecettesCaisse += total;
                            }
                            //montantRecettesCaisse = Convert.ToDouble(dtRow.ItemArray[2].ToString());
                            dataGridView2.Rows.Add(
                                       "Total " + li, "",
                                        string.Format(elGR, "{0:0,0}", montantRecettesCaisse), "", "", ""
                                         );
                            dataGridView2.Rows.Add(
                                         "", "", "", "", "", "");
                            dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            totalRecettesCaisseduJour += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation, "",
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                        );

                    }
                    else if (libelle.Designation.Contains("Hospitalisation"))
                    {

                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;
                        var sousTotalRecettesCaisse = .0;
                        var dtHos = AppCode.ConnectionClassPharmacie.ListeDesVentesDataTable(dtp1.Value.Date, dtp2.Value.Date);

                        foreach (DataRow dtRow in dtHos.Rows)
                        {
                            var listePro = AppCode.ConnectionClassPharmacie.ListeDesVentesPharmacieTousConventionsHospitalisees(dtp1.Value.Date, dtp2.Value.Date);
                                         
                            var count = Convert.ToDouble(dtRow.ItemArray[2].ToString());
                            var total = Convert.ToDouble(dtRow.ItemArray[1].ToString());
                            foreach (DataRow pr in listePro.Rows)
                            {
                                if (dtRow.ItemArray[0].ToString() == pr.ItemArray[0].ToString())
                                {
                                     count += Convert.ToDouble(dtRow.ItemArray[2].ToString());
                                     total += Convert.ToDouble(dtRow.ItemArray[1].ToString());
                                }
                            }
                            montantRecettesCaisse = total;

                            dataGridView2.Rows.Add(
                                          libelle.Designation,
                                        string.Format(elGR, "{0:0,0}", count),
                                  string.Format(elGR, "{0:0,0}", total, "", "", "")
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            totalRecettesCaisseduJour += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add(
                                        "Total " + libelle.Designation, "",
                                       string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                        );

                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        dataGridView2.Rows.Add(
                                  "Acte " + libelle.Designation, "", "", "", "", ""
                               );
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.Yellow;

                        var sousTotalRecettesCaisse = .0;
                        var requete = "SELECT  det_fact.design, COUNT(det_fact.design), SUM(det_fact.prix_total* det_fact.pourcentage/100) FROM det_fact INNER JOIN" +
                                                " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE(det_fact.groupage = '" + libelle.Designation + "') " +
                                                "AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2  AND facture_tbl.sub IS NULL   GROUP BY   det_fact.design";
                        var dtCaisse = AppCode.ConnectionClassClinique.TableFacture(requete, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                        
                        foreach (DataRow dtRow in dtCaisse.Rows)
                        {
                            var count = Convert.ToDouble(dtRow.ItemArray[1].ToString());
                            var total = Convert.ToDouble(dtRow.ItemArray[2].ToString());
                            var listePro = AppCode.ConnectionClassClinique.RecettesLaboratoirePourTousConventionnes(dtp1.Value.Date, dtp2.Value.Date);
                            foreach (var  pr in listePro)
                            {
                                if (dtRow.ItemArray[0].ToString() == pr.Designation)
                                {
                                    count += pr.Quantite;
                                    total += pr.PrixTotal;
                                }
                            }
                            montantRecettesCaisse = total;
                            dataGridView2.Rows.Add(
                                        dtRow.ItemArray[0].ToString(),
                                               string.Format(elGR, "{0:0,0}",count ),
                                        string.Format(elGR, "{0:0,0}", total ), "", "", ""
                                         );
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                            totalRecettesCaisseduJour += montantRecettesCaisse;
                        }
                        dataGridView2.Rows.Add( "Total " + libelle.Designation, "",
                                                 string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse), "", "", ""
                                                                    );
                        dataGridView2.Rows[dataGridView2.RowCount - 1].DefaultCellStyle.BackColor = Color.GreenYellow;

                    }

                    dataGridView2.Rows.Add(
                                    "", "", "", "", "", "");
                }
                dataGridView2.Rows.Add(
                                  "Total Général ", "",
                                  string.Format(elGR, "{0:0,0}", totalRecettesCaisse), "", "", ""
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

        private void button8_Click(object sender, EventArgs e)
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
                    titre = "Rapport des consultations de " + cmbNomEmpoye.Text + " du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                }
                else
                {
                    titre = "Rapport des consultations du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString();
                    if (cmbConventionne.Text == "" || cmbConventionne.Text == "<<TOUS LES PATIENTS>>")
                    {
                    }
                    else if (cmbConventionne.Text == "<<TOUS LES CONVENTIONNES>>")
                    {
                        titre = "Rapport des consultations du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des conventionnés";
                    }
                    else if (cmbConventionne.Text == "<<TOUS LES PARTICULIERS>>")
                    {
                        titre = "Rapport des consultations du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " des particuliers";
                    }
                    else
                    {
                        titre = "Rapport des consultations du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString() + " de " + cmbConventionne.Text;
                    }

                }
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
                pathFolder = pathFolder + "\\Rapport des actes";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                sfd.FileName = " Rapport des actes " + date + ".pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //sharpPDF.pdfPage pageIndex = document.addPage();
                    var Count = dataGridView1.Rows.Count / 31;

                    for (var i = 0; i <= Count; i++)
                    {
                        if (i * 47 < dataGridView1.Rows.Count)
                        {
                            var rapportMedecin = Impression.ImprimerRapportDesActes(dataGridView1, titre, i);

                            var inputImage = @"cdali" + i;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(rapportMedecin, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                        }
                    }

                }

                document.createPDF(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch { }
        }
    }
}
