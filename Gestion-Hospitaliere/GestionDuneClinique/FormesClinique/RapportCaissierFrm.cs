using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.Formes
{
    public partial class RapportCaissierFrm : Form
    {
        public RapportCaissierFrm()
        {
            InitializeComponent();
        }

        private void RapportCaissierFrm_Load(object sender, EventArgs e)
        {
          
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Blue, 3);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void RapportCaissierFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        void AfficherRapport()
        {
            try
            {

                if (checkBox1.Checked)
                {
                    RapportCaisse(DateTime.Now.Date, DateTime.Now.Date.AddHours(24));
                }
                else if (checkBox2.Checked)
                {
                    RapportCaisse(dtp1.Value.Date, dtp1.Value.Date.AddHours(24));
                }
                else if (checkBox3.Checked)
                {
                    RapportCaisse(dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                }
                else if (checkBox4.Checked)
                {
                    if (!string.IsNullOrEmpty(cmbEmploye.Text))
                    {
                        var listeEmploye = ConnectionClassClinique.ListeDesEmployees(cmbEmploye.Text);
                        var idEmpl = listeEmploye[0].NumMatricule;
                        RapportCaisse(idEmpl, DateTime.Now.Date, DateTime.Now.Date.AddHours(24));
                    }
                }
                else if (checkBox5.Checked)
                {
                    if (!string.IsNullOrEmpty(cmbEmploye.Text))
                    {
                        var listeEmploye = ConnectionClassClinique.ListeDesEmployees(cmbEmploye.Text);
                        var idEmpl = listeEmploye[0].NumMatricule;
                        RapportCaisse(idEmpl, dtp1.Value.Date, dtp1.Value.Date.AddHours(24));
                    }
                }
                else if (checkBox6.Checked)
                {
                    if (!string.IsNullOrEmpty(cmbEmploye.Text))
                    {
                        var listeEmploye = ConnectionClassClinique.ListeDesEmployees(cmbEmploye.Text);
                        var idEmpl = listeEmploye[0].NumMatricule;
                        RapportCaisse(idEmpl, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    }
                }

                var montant = 0.0;
                foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                {
                    montant += Double.Parse(dtRow.Cells[2].Value.ToString());
                }
                label3.Text = "Total : " + montant.ToString();
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (GestionAcademique.LoginFrm.typeUtilisateur.Contains("admin"))
            {
                AfficherRapport();
            }
            else if (cmbEmploye.Text.ToUpper() == GestionAcademique.LoginFrm.nom.ToUpper())
            {
                AfficherRapport();
            }
            else
            {
                MonMessageBox.ShowBox("Vous n'etes pas autorisés à voir le rapport de la caisse de " + cmbEmploye.Text, "Erreur", "erreur.png");
            }
        }

        void RapportCaisse(string idEmpl, DateTime dtm1 , DateTime dtm2)
        {
            var requete = "SELECT det_paie_tbl.date_paie, det_paie_tbl.montant, facture_tbl.num_empl,facture_tbl.num_patient,facture_tbl.id_fact" +
                         ",facture_tbl.sub FROM facture_tbl INNER JOIN det_paie_tbl ON facture_tbl.id_fact = det_paie_tbl.id_paie " +
                         " WHERE (facture_tbl.num_empl = '" + idEmpl + "' ) AND " +
                         " (det_paie_tbl.date_paie >= @date1 AND det_paie_tbl.date_paie < @date2)";
            var dt = ConnectionClassClinique.TableFacture(requete, dtm1,dtm2);
            dataGridView1.Rows.Clear();

            foreach (DataRow dtRow in dt.Rows)
            {
                var listePatient = from p in AppCode.ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient ==Int32.Parse(dtRow.ItemArray[3].ToString())
                                   select p;

                var patient = new AppCode.Patient();
                foreach (var p in listePatient)
                    patient = p; 
                var nomPatient = patient.Nom + " " + patient.Prenom;
                var sub = "";
                if(!string.IsNullOrEmpty( dtRow.ItemArray[5].ToString()))
                {
                   sub = dtRow.ItemArray[5].ToString();
                }
                var montant = Convert.ToDouble(dtRow.ItemArray[1].ToString());
                if (montant > 0 || montant < 0)
                {
                    if (string.IsNullOrEmpty(sub))
                    {
                        dataGridView1.Rows.Add(
                            dtRow.ItemArray[0].ToString(),
                            nomPatient.ToUpper(),
                            dtRow.ItemArray[1].ToString(),
                             dtRow.ItemArray[4].ToString()
                            );
                    }
                }
            }
        }

        void RapportCaisse(DateTime dtm1, DateTime dtm2)
        {
            var requete = "SELECT det_paie_tbl.date_paie, det_paie_tbl.montant, facture_tbl.num_empl,facture_tbl.num_patient,facture_tbl.id_fact "+
                         ",facture_tbl.sub FROM facture_tbl INNER JOIN det_paie_tbl ON facture_tbl.id_fact = det_paie_tbl.id_paie " +
                         " WHERE  (det_paie_tbl.date_paie >= @date1 AND det_paie_tbl.date_paie < @date2   )";
            var dt = ConnectionClassClinique.TableFacture(requete, dtm1, dtm2);

            dataGridView1.Rows.Clear();
            foreach (DataRow dtRow in dt.Rows)
            {
                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == Int32.Parse(dtRow.ItemArray[3].ToString())
                                   select p;

                var nomPatient = "";

                foreach (var p in listePatient)
                {
                    nomPatient = p.Nom + " " + p.Prenom;
                } 
                var montant = Convert.ToDouble(dtRow.ItemArray[1].ToString());
                var sub = "";
                if (!string.IsNullOrEmpty(dtRow.ItemArray[5].ToString()))
                {
                    sub = dtRow.ItemArray[5].ToString();
                }

                if (montant > 0 || montant < 0)
                {
                    if (string.IsNullOrEmpty(sub))
                    {
                        dataGridView1.Rows.Add(
                            dtRow.ItemArray[0].ToString(),
                            nomPatient.ToUpper(),
                            dtRow.ItemArray[1].ToString(),
                             dtRow.ItemArray[4].ToString()
                            );
                    }
                }
            }
        }

        Bitmap rapportCaissier;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportCaissier, -10, 0, rapportCaissier.Width, rapportCaissier.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void btnApercu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbEmploye.Text))
            {
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("nom_empl", cmbEmploye.Text);
                var employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                    listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                var idEmpl = listeEmploye[0].NumMatricule;
                var titre = "";
                if (checkBox1.Checked)
                {
                    titre = "Rapport du " + DateTime.Now.Date.ToShortDateString();
                    rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox2.Checked)
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString();
                    rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox3.Checked)
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                    rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox4.Checked)
                {
                    titre = "Rapport de  " + employe.NomEmployee + " du " + DateTime.Now.Date.ToShortDateString();
                    rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox5.Checked)
                {
                    titre = "Rapport de  " + employe.NomEmployee + " du " + dtp1.Value.Date.ToShortDateString() ;
                    rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox6.Checked)
                {
                    titre = "Rapport de  " + employe.NomEmployee + " du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                    rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
             printPreviewDialog1.ShowDialog();
                
            }
           
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox1.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox1.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox1.Checked = false;
            checkBox6.Checked = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox1.Checked = false;
        }

        //imprimer
        private void btnImprimer_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(cmbEmploye.Text))
            //{
                //var listeEmploye = ConnectionClassClinique.ListeDesEmployees("nom_empl",GestionAcademique.LoginFrm.nom);
                //var employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                //    listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                //var idEmpl =listeEmploye[0].NumMatricule;
                var titre = "";
                if (checkBox1.Checked)
                {
                    titre = "Rapport du " + DateTime.Now.Date.ToShortDateString();
                    //rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox2.Checked)
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString();
                    //rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox3.Checked)
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                    //rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox4.Checked)
                {
                    titre = "Rapport de  " + cmbEmploye.Text + " du " + DateTime.Now.Date.ToShortDateString();
                    //rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox5.Checked)
                {
                    titre = "Rapport de  " + cmbEmploye.Text + " du " + dtp1.Value.Date.ToShortDateString();
                    //rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }
                else if (checkBox6.Checked)
                {
                    titre = "Rapport de  " + cmbEmploye.Text + " du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                    //rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                }

                ImprimerRapport(titre);
            
        }
    
        void ImprimerRapport(string titre)
        {
            try
            {
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("nom_empl", cmbEmploye.Text);
                var employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                var idEmpl = listeEmploye[0].NumMatricule;
                var rowCount = dataGridView1.Rows.Count;
                rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printDocument1.Print();
                    if (rowCount > 30)
                    {
                        rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre, 1, 45, 0);
                        printDocument1.Print();
                        if (rowCount > 75)
                        {
                            rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre, 2, 45, 1);
                            printDocument1.Print();
                            if (rowCount > 130)
                            {
                                rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre, 3, 45, 2);
                                printDocument1.Print();
                                if (rowCount > 175)
                                {
                                    rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre, 4, 45, 3);
                                    printDocument1.Print();
                                    if (rowCount > 220)
                                    {
                                        rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre, 5, 45, 4);
                                        printDocument1.Print();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees(textBox4.Text);
                if (listeEmploye.Count > 0)
                {
                    var list = from l in listeEmploye
                               where !l.NomEmployee.ToUpper().Contains("EXTERNE")
                               select l.NomEmployee;
                    foreach (var empl in list)
                    {
                        cmbEmploye.Items.Add(empl);
                    }
                    cmbEmploye.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmbEmploye.DroppedDown = true;
                     
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {    
                var frm = new GestionDuneClinique.FormesClinique.DetailRapportFrm();

                if (checkBox1.Checked)
                {
                     frm.titre = "Rapport du " + DateTime.Now.Date.ToShortDateString();
                }
                else if (checkBox2.Checked)
                {
                     frm.titre = "Rapport du " + dtp1.Value.Date.ToShortDateString();
                }
                else if (checkBox3.Checked)
                {
                    frm.titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                }
                else if (checkBox4.Checked)
                {
                    frm.titre = "Rapport de  " + cmbEmploye.Text + " du " + DateTime.Now.Date.ToShortDateString();
                }
                else if (checkBox5.Checked)
                {
                    frm.titre = "Rapport de  " + cmbEmploye.Text + " du " + dtp1.Value.Date.ToShortDateString();
                }
                else if (checkBox6.Checked)
                {
                    frm.titre = "Rapport de  " + cmbEmploye.Text + " du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                }

                frm.dataGridView1.Rows.Clear();
                var  listFacture = new List<int>();
                foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                {
                    listFacture.Add(Int32.Parse(dtRow.Cells[3].Value.ToString()));
                }
                var groupesFact = from element in listFacture
                                  group element by element into grp
                                  select new { key = grp.Key };

                foreach (var   id in groupesFact)
                {
                    var detailListe = ConnectionClassClinique.DetailsDesFactures(id.key);
                    var listeFacture = ConnectionClassClinique.ListeDesFactures(id.key);
                    var montant = 0.0;
                    foreach (var facture in detailListe)
                    {
                        montant += facture.PrixTotal;
                    }
                    if (detailListe.Count > 1)
                    {
                        frm.dataGridView1.Rows.Add(listeFacture[0].DateFacture, listeFacture[0].Patient.ToUpper(),  detailListe[0].Designation, detailListe[0].PrixTotal);
                        
                        for (var i = 1; i < detailListe.Count; i++)
                        {
                            frm.dataGridView1.Rows.Add(" ",  " ", detailListe[i].Designation, detailListe[i].PrixTotal);
                                                  }
                    }
                    else
                    {
                        foreach (Facture facture in detailListe)
                        {
                            frm.dataGridView1.Rows.Add(listeFacture[0].DateFacture, listeFacture[0].Patient.ToUpper(), facture.Designation, facture.PrixTotal);
                            var items1 = new string[]
                            {
                                listeFacture[0].DateFacture.ToString(), listeFacture[0].Patient.ToUpper(), facture.Designation, facture.PrixTotal.ToString()
                            };
                            var lstItems1 = new ListViewItem(items1);
                        }
                    }
                    frm.dataGridView1.Rows.Add(" ", " ", "SOUS TOTAL", montant);
                    frm.dataGridView1.Rows.Add(" ", " ", " ", " ");
                     var  items2 = new string[]
                            {
                                " ", " ", "SOUS TOTAL", montant.ToString()
                            };
                     var lstItems2 = new ListViewItem(items2);
                }
                
                if (dataGridView1.Rows.Count > 0)
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("detail facture",ex );
            }
        }
    }
}
