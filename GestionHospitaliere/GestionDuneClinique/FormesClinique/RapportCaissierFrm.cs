using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionAcademique;
using GestionDuneClinique.AppCode;
using GestionPharmacetique.AppCode;

namespace GestionDuneClinique.Formes
{
    public partial class RapportCaissierFrm : Form
    {
        public RapportCaissierFrm()
        {
            InitializeComponent();
        }

        public DateTime dateDebut, dateFin;
        private void RapportCaissierFrm_Load(object sender, EventArgs e)
        {
            try
            {
                cmbEmploye.Items.Add("");
                var listeFacture = ConnectionClassClinique.ListeDesFactures("", dateDebut, dateFin.Date.AddHours(24));
                var groupesFact = from element in listeFacture
                                  group element by element.NomEmploye into grp
                                  select new { key = grp.Key };
                foreach (var empl in groupesFact)
                {
                    cmbEmploye.Items.Add(empl.key);
                }
                button7.Location = new Point(Width - 35, 4);
                if (GestionAcademique.LoginFrm.typeUtilisateur.Contains("admin"))
                {
                    AfficherRapport();
                }
                else if (cmbEmploye.Text.ToUpper() == GestionAcademique.LoginFrm.nom.ToUpper())
                {
                    cmbEmploye.Text = LoginFrm.nom;
                    AfficherRapport();
                }
                //else
                //{
                //    //MonMessageBox.ShowBox("Vous n'etes pas autorisés à voir le rapport de la caisse de " + cmbEmploye.Text, "Erreur", "erreur.png");
                //}
            }
            catch
            {
            }
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 1);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
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
        public static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
    
        void AfficherRapport()
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbEmploye.Text))
                {
                 
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees(cmbEmploye.Text);
                    var idEmpl = listeEmploye[0].NumMatricule;
                    RapportCaisse(idEmpl, dateDebut, dateFin.Date.AddHours(24));
                }  
                else
                {
                    RapportCaisse("",dateDebut, dateFin.Date.AddHours(24));
                }
                  var montantTotal=.0;
                var montantPaye = 0.0;
                foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                {
                    montantTotal += Double.Parse(dtRow.Cells[2].Value.ToString());
                    montantPaye += Double.Parse(dtRow.Cells[3].Value.ToString());
                }
                var resteTotal = montantTotal - montantPaye;
                label3.Text = "Montant total :   " +   string.Format(elGR, "{0:0,0}",montantTotal )+ "    -     Total payé :   " + 
                     string.Format(elGR, "{0:0,0}",montantPaye) + "    -     Reste à payer :   " +  string.Format(elGR, "{0:0,0}",resteTotal);
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
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
                    MonMessageBox.ShowBox(
                        "Vous n'etes pas autorisés à voir le rapport de la caisse de " + cmbEmploye.Text, "Erreur",
                        "erreur.png");
                }
            }
            catch
            {
            }
        }

        void RapportCaisse(string idEmpl, DateTime dtm1 , DateTime dtm2)
        {
            try
            {

                var listeFacture = ConnectionClassClinique.ListeDesFactures(idEmpl, dateDebut, dateFin.Date.AddHours(24));
                dataGridView1.Rows.Clear();

                foreach (var facture in listeFacture)
                {
                    var requete = "SELECT det_paie_tbl.date_paie, det_paie_tbl.montant, facture_tbl.num_empl,facture_tbl.num_patient,facture_tbl.id_fact" +
                 ",facture_tbl.sub,facture_tbl.montant_fact FROM facture_tbl INNER JOIN det_paie_tbl ON facture_tbl.id_fact = det_paie_tbl.id_paie " +
                 " WHERE  det_paie_tbl.id_paie = " + facture.NumeroFacture;
                    var dt = ConnectionClassClinique.TableFacture(requete, dtm1, dtm2);
                    var montantPaye = .0;
                    var detPaiement = ConnectionClassClinique.TablePaiement(facture.NumeroFacture);
                    for (int i = 0; i < detPaiement.Rows.Count; i++)
                    {
                        montantPaye += Convert.ToDouble(detPaiement.Rows[i].ItemArray[0].ToString());
                    }
                        var sub = "";
                        if (!string.IsNullOrEmpty(facture.Sub))
                        {
                            sub = facture.Sub;
                        }
                        if (string.IsNullOrEmpty(sub))
                        {
                            dataGridView1.Rows.Add(
                                facture.DateFacture.ToShortDateString(),
                                facture.Patient.ToUpper(),
                                 string.Format(elGR, "{0:0,0}", facture.MontantFactural),
                                 string.Format(elGR, "{0:0,0}", montantPaye),
                                facture.NumeroFacture
                                );
                        }                        
                    }
                }
            catch (Exception)
            {
            }
        }

        void RapportCaisse(DateTime dtm1, DateTime dtm2)
        {
            //try
            //{
            //    var requete = "SELECT det_paie_tbl.date_paie, det_paie_tbl.montant, facture_tbl.num_empl,facture_tbl.num_patient,facture_tbl.id_fact " +
            //                   ",facture_tbl.sub, facture_tbl.montant_fact FROM facture_tbl INNER JOIN det_paie_tbl ON facture_tbl.id_fact = det_paie_tbl.id_paie " +
            //                   " WHERE  (det_paie_tbl.date_paie >= @date1 AND det_paie_tbl.date_paie < @date2   )";
            //    var dt = ConnectionClassClinique.TableFacture(requete, dtm1, dtm2);

            //    dataGridView1.Rows.Clear();
            //    foreach (DataRow dtRow in dt.Rows)
            //    {
            //        var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
            //                           where p.NumeroPatient == Int32.Parse(dtRow.ItemArray[3].ToString())
            //                           select p;

            //        var nomPatient = "";

            //        foreach (var p in listePatient)
            //        {
            //            nomPatient = p.Nom + " " + p.Prenom;
            //        }
            //        var montant = Convert.ToDouble(dtRow.ItemArray[1].ToString());
            //        var sub = "";
            //        if (!string.IsNullOrEmpty(dtRow.ItemArray[5].ToString()))
            //        {
            //            sub = dtRow.ItemArray[5].ToString();
            //        }

            //            if (string.IsNullOrEmpty(sub))
            //            {
            //                dataGridView1.Rows.Add(
            //                    dtRow.ItemArray[0].ToString(),
            //                    nomPatient.ToUpper(),
            //                     string.Format(elGR, "{0:0,0}", double.Parse(dtRow.ItemArray[6].ToString())),
            //                     string.Format(elGR, "{0:0,0}",double.Parse( dtRow.ItemArray[1].ToString())),
            //                     dtRow.ItemArray[4].ToString()
            //                    );
            //            }
            //    }
            //}
            //catch (Exception)
            //{
            //}
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

        //imprimer
        private void btnImprimer_Click(object sender, EventArgs e)
        {
                var titre = "";
            if (!string.IsNullOrEmpty(cmbEmploye.Text))
            {
                if (dateDebut == dateFin)
                {
                    titre = "Rapport de la caisse de " + cmbEmploye.Text + " du " + dateDebut.ToShortDateString();
                }
                else
                {
                    titre = "Rapport de la caisse de " + cmbEmploye.Text + " du " + dateDebut.ToShortDateString() + " au "+dateFin.ToShortDateString();
                }
            }
            else
            {
                if (dateDebut == dateFin)
                {
                    titre = "Rapport de la caisse  du " + dateDebut.ToShortDateString();
                }
                else
                {
                    titre = "Rapport de la caisse du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                }
            }
            
            ImprimerRapport(titre);
            
        }
    
        void ImprimerRapport(string titre)
        {
            try
            {
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("nom_empl", cmbEmploye.Text);
                GestionPharmacetique.AppCode.Employe employe=new Employe();
                if (listeEmploye.Count > 0)
                {
                    foreach (var empl in listeEmploye)
                        employe = empl;
                }
                else
                {
                    employe = null;
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
                pathFolder = pathFolder + "\\Rapport de la caisse";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                sfd.FileName =  "Rapport de la caisse  _imprimé_le_" + date + ".pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1, employe, titre);
                    var inputImage = @"cdali" ;
                    // Create an empty page
                    sharpPDF.pdfPage pageIndex = document.addPage();

                    document.addImageReference(rapportCaissier, inputImage);
                    sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                    pageIndex.addImage(img1, -5, 0, pageIndex.height, pageIndex.width);
                    if (rowCount > 38)
                    {
                        var Count = (dataGridView1.Rows.Count - 38) / 45;

                        for (var i = 0; i <= Count; i++)
                        {
                            if (i * 45 < dataGridView1.Rows.Count)
                            {
                                var numPage = 1 + i;
                                rapportCaissier = Impression.RapportDeLaCaisse(dataGridView1,employe,titre,numPage,45,i);

                                inputImage = @"cdali" + i;
                                // Create an empty page
                                pageIndex = document.addPage();

                                document.addImageReference(rapportCaissier, inputImage);
                                img1 = document.getImageReference(inputImage);
                                pageIndex.addImage(img1, -5, 0, pageIndex.height, pageIndex.width);
                            }
                        }

                    }
                }
                document.createPDF(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception)
            {
                
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbEmploye.Items.Clear();
                cmbEmploye.Items.Add("");
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
                if (!string.IsNullOrEmpty(cmbEmploye.Text))
                {
                    if (dateDebut == dateFin)
                    {
                        frm.titre = "Rapport de la caisse de " + cmbEmploye.Text + " du " + dateDebut.ToShortDateString();
                    }
                    else
                    {
                        frm.titre = "Rapport de la caisse de " + cmbEmploye.Text + " du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                    }
                }
                else
                {
                    if (dateDebut == dateFin)
                    {
                        frm.titre = "Rapport de la caisse  du " + dateDebut.ToShortDateString();
                    }
                    else
                    {
                        frm.titre = "Rapport de la caisse du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                    }
                }

                frm.dataGridView1.Rows.Clear();
                var  listFacture = new List<int>();
                foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                {
                    listFacture.Add(Int32.Parse(dtRow.Cells[4].Value.ToString()));
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
                    frm.Location= new Point(Location.X,Location.Y);
                    frm.Size= new Size(Width,Height);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("detail facture",ex );
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SGDP.Formes.DateFrm.ShowBox())
            {
                cmbEmploye.Items.Clear();
                cmbEmploye.Items.Add("");
                var listeFacture = ConnectionClassClinique.ListeDesFactures("", dateDebut, dateFin.Date.AddHours(24));
                var groupesFact = from element in listeFacture
                                  group element by element.NomEmploye into grp
                                  select new { key = grp.Key };
                foreach (var empl in groupesFact)
                {
                    cmbEmploye.Items.Add(empl.key);
                }
                dateDebut = SGDP.Formes.DateFrm.dateDebut;
                dateFin = SGDP.Formes.DateFrm.dateFin;
                button1_Click(null,null);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            cmbEmploye.Items.Clear();
            cmbEmploye.Items.Add("");
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
                //cmbEmploye.DropDownStyle = ComboBoxStyle.DropDownList;
                //cmbEmploye.DroppedDown = true;

            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void cmbEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}
