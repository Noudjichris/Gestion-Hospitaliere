using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.FormesClinique
{
    public partial class JournalDesRecettesFrm : Form
    {
        public JournalDesRecettesFrm()
        {
            InitializeComponent();
        }
      
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        private void JournalDesRecettesFrm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        DateTime dateDebut, dateFin;
        string ObtenirMois(int mois)
        {
            switch (mois)
            {
                case 1:
                    return "Janvier";
                case 2:
                    return "Fevrier";
                case 3:
                    return "Mars";
                case 4:
                    return "Avril";
                case 5:
                    return "Mai";
                case 6:
                    return "Juin";
                case 7:
                    return "Juillet";
                case 8:
                    return "Août";
                case 9:
                    return "Septembre";
                case 10:
                    return "Octobre";
                case 11:
                    return "Novembre";
                case 12:
                    return "Decembre";
                default:
                    return "";
            };
        }

        DateTime ObtenirDebutJour(int mois)
        {
            return DateTime.Parse("01/" + mois + "/" + dateDebut.Year);
        }

        DateTime ObtenirFinJour(int mois)
        {
            if (mois == 1)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 2)
            {
                if (DateTime.IsLeapYear(dateDebut.Year))
                    return DateTime.Parse("29/" + mois + "/" + dateDebut.Year);
                else
                    return DateTime.Parse("28/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 3)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 4)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 5)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 6)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 7)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 8)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 9)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 10)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 11)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 12)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else
            {
                return dateDebut;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateDebut = dtp1.Value.Date;
            dateFin = dtp2.Value.Date;

            if (checkBox1.Checked)
            {
                RecettesJournalieresDeLaCaisseEtPharmacie();
            }
            else if (checkBox2.Checked)
            {
                RecettesMensuellesDeLaCaisseEtPharmacie();
            }
        }

        void RecettesMensuellesDeLaCaisse()
        {
            try
            {
                var listeMois = new List<int>();
                var mois = dateFin.Month - dateDebut.Month;

                for (var i = 0; i <= mois; i++)
                {
                    listeMois.Add(dateDebut.Month + i);
                }
               
                dgvRecettes.Rows.Clear();
                var totalRecettes = .0;
                foreach (var Mois in listeMois)
                {
                    var requete = "SELECT det_paie_tbl.date_paie, det_paie_tbl.montant, facture_tbl.num_empl,facture_tbl.num_patient,facture_tbl.id_fact" +
                        ",facture_tbl.sub FROM facture_tbl INNER JOIN det_paie_tbl ON facture_tbl.id_fact = det_paie_tbl.id_paie " +
                        " WHERE (det_paie_tbl.date_paie >= @date1 AND det_paie_tbl.date_paie < @date2)";
                    var dt = AppCode.ConnectionClassClinique.TableFacture(requete, ObtenirDebutJour(Mois), ObtenirFinJour(Mois).AddHours(24));
                    var montantRecettes = .0;
                    foreach (DataRow dtRow in dt.Rows)
                    {                       
                        var sub = "";
                        if (!string.IsNullOrEmpty(dtRow.ItemArray[5].ToString()))
                        {
                            sub = dtRow.ItemArray[5].ToString();
                        }
                        var montant = Convert.ToDouble(dtRow.ItemArray[1].ToString());
                        if (montant > 0 || montant < 0)
                        {
                            if (string.IsNullOrEmpty(sub))
                            {
                                montantRecettes += montant;
                                totalRecettes += montant;
                            }
                        }
                    }
                    dgvRecettes.Rows.Add(
                                    ObtenirMois(Mois),
                                   string.Format(elGR, "{0:0,0}", montantRecettes," ")
                                    );
                }
                  dgvRecettes.Rows.Add(
                                    "TOTAL",
                                   string.Format(elGR, "{0:0,0}", totalRecettes," ")
                                    );
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Recettes caisse", ex);
            }
        }

        void RecettesJournalieresDeLaCaisseEtPharmacie()
        {
            try
            {
                var listeDesJour = new List<DateTime>();
                var mois = dateFin.Subtract(dateDebut);

                for (var i = 0; i <= mois.Days; i++)
                {
                    listeDesJour.Add(dateDebut.AddDays(i));
                }

                dgvRecettes.Rows.Clear();
                var totalRecettesCaisse = .0;
                foreach (var jour in listeDesJour)
                {
                    var sousTotalRecettesCaisse = .0;
                     var sousTotalRecettesPharmacie = .0;
                    dgvRecettes.Rows.Add(
                                  jour.ToShortDateString(),
                                 "", " "
                                  );

                    var listeLibelle = AppCode.ConnectionClassClinique.ListeDesLibelles();
                    foreach (var libelle in listeLibelle)
                    {
                        var requete = "SELECT det_fact.groupage ,count(det_fact.prix_total), sum(det_fact.prix_total) FROM det_fact" +
                            " INNER JOIN facture_tbl  ON det_fact.id_fact =facture_tbl.id_fact  WHERE det_fact.groupage ='" + libelle.Designation +
                            "' AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 GROUP BY   det_fact.groupage";
                        var dtCaisse = AppCode.ConnectionClassClinique.TableFacture(requete, jour, jour.AddHours(24));
                        var montantRecettesCaisse = 0.0;
                        foreach (DataRow dtRow in dtCaisse.Rows)
                        {

                            montantRecettesCaisse += Convert.ToDouble(dtRow.ItemArray[2].ToString());
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                        }


                        if (libelle.Designation == "PHARMACIE")
                        {
                            var montantPharmacie = AppCode.ConnectionClassPharmacie.ListeDesVentes(jour, jour.AddHours(24));
                            if (montantPharmacie > 0)
                            {
                                montantRecettesCaisse += montantPharmacie;
                                sousTotalRecettesCaisse += montantRecettesCaisse;
                                totalRecettesCaisse += montantRecettesCaisse;
                                sousTotalRecettesPharmacie +=montantPharmacie;
                            }
                        }
                        dgvRecettes.Rows.Add(
                                       "", libelle.Designation, string.Format(elGR, "{0:0,0}", montantRecettesCaisse)
                                       );
                    }
                    var requete1 = "SELECT sum(montant) FROM det_paie_tbl WHERE date_paie >=@date1 AND date_paie<@date2";
                    var dtCaisse1 = AppCode.ConnectionClassClinique.TableFacture(requete1, jour, jour.AddHours(24));
                    var montantRecettesCaisse1 = 0.0;
                    if (dtCaisse1.Rows.Count > 0)
                    {
                        montantRecettesCaisse1 = double.Parse(dtCaisse1.Rows[0].ItemArray[0].ToString());
                    }
                    if (sousTotalRecettesCaisse - sousTotalRecettesPharmacie < montantRecettesCaisse1)
                    {

                    }
                    dgvRecettes.Rows.Add(
                                "SOUS TOTAL",
                                 "",
                                string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse)
                                 );
                }
                dgvRecettes.Rows.Add(
                                  "",
                                  "",
                                  ""
                                   );
                dgvRecettes.Rows.Add(
                                  "TOTAL",
                                   "",
                                  string.Format(elGR, "{0:0,0}", totalRecettesCaisse)
                                   );
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Recettes caisse", ex);
            }
        }

        void RecettesMensuellesDeLaCaisseEtPharmacie()
        {
            try
            {
                var listeMois = new List<int>();
                var mois = dateFin.Month - dateDebut.Month;

                for (var i = 0; i <= mois; i++)
                {
                    listeMois.Add(dateDebut.Month + i);
                }

                dgvRecettes.Rows.Clear();
               var totalRecettesCaisse = .0;
                foreach (var Mois in listeMois)
                {
                    //var montantRecettesPharmacie = .0;
                    var sousTotalRecettesCaisse = .0;
                     dgvRecettes.Rows.Add(
                                   ObtenirMois(Mois).ToUpper(),
                                  ""," "                            
                                   );
                      
                    var listeLibelle = AppCode.ConnectionClassClinique.ListeDesLibelles();
                    foreach (var libelle in listeLibelle)
                    {
                        var requete = "SELECT det_fact.groupage ,count(det_fact.prix_total), sum(det_fact.prix_total) FROM det_fact" +
                            " INNER JOIN facture_tbl  ON det_fact.id_fact =facture_tbl.id_fact  WHERE det_fact.groupage ='"+libelle.Designation +
                            "' AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 GROUP BY   det_fact.groupage";
                        var dtCaisse = AppCode.ConnectionClassClinique.TableFacture(requete, ObtenirDebutJour(Mois), ObtenirFinJour(Mois).AddHours(24));
                         var montantRecettesCaisse=0.0;
                        foreach (DataRow dtRow in dtCaisse.Rows)
                        {
                           
                            montantRecettesCaisse += Convert.ToDouble(dtRow.ItemArray[2].ToString());
                            totalRecettesCaisse += montantRecettesCaisse;
                            sousTotalRecettesCaisse += montantRecettesCaisse;
                        } 
                        if (libelle.Designation == "PHARMACIE")
                        {
                            var montantPharmacie = AppCode.ConnectionClassPharmacie.ListeDesVentes(ObtenirDebutJour(Mois), ObtenirFinJour(Mois).AddHours(24));
                            if (montantPharmacie > 0)
                            {
                                montantRecettesCaisse += montantPharmacie;
                                    sousTotalRecettesCaisse += montantRecettesCaisse;
                                    totalRecettesCaisse += montantRecettesCaisse;
                                }
                        }
                        dgvRecettes.Rows.Add(
                                       "", libelle.Designation, string.Format(elGR, "{0:0,0}", montantRecettesCaisse)
                                       );
                    }
                    dgvRecettes.Rows.Add(
                                "SOUS TOTAL",
                                 "",
                                string.Format(elGR, "{0:0,0}", sousTotalRecettesCaisse)
                                 );
                }
                dgvRecettes.Rows.Add(
                                  "",
                                  "",
                                  ""
                                   );
                dgvRecettes.Rows.Add(
                                  "TOTAL",
                                   "",
                                  string.Format(elGR, "{0:0,0}", totalRecettesCaisse)
                                   );
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Recettes caisse", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
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
            if (checkBox1.Checked && !checkBox2.Checked)
            {
                sfd.FileName = "Journal_Des_Recettes_de_la_Caisse_Impriméé_le_" + date + ".xls";
            }
            else if (checkBox2.Checked && !checkBox1.Checked)
            {
                sfd.FileName = "Journal_Des_Recettes_de_la_Pharmacie_Impriméé_le_" + date + ".xls";
            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                sfd.FileName = "Journal_Des_Recettes_de_la_Caisse_et_de_la_Pharmacie_Impriméé_le_" + date + ".xls";
            }
            
            if (sfd.ShowDialog() == DialogResult.OK)
            {

                ToCsV(dgvRecettes , sfd.FileName); // Here dataGridview1 is your grid view name
                System.Diagnostics.Process.Start(sfd.FileName);
            }
               
        }

        private void ToCsV(DataGridView dGV, string filename)
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
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
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

                

                if (dgvRecettes.Rows.Count > 0)
                {
                    var titre = "";
                    if (checkBox1.Checked)
                    {
                        if (dateDebut == dateFin)
                        {
                            titre = "Journal des Recettes journalieres du ".ToUpper() + dateDebut.ToShortDateString();
                        }
                        else
                        {
                            titre = "Journal des Recettes journalieres du ".ToUpper() + dateDebut.ToShortDateString() + " AU " + dateFin.ToShortDateString();
                        }
                    }
                    else if (checkBox2.Checked)
                    {
                        titre = "Journal des recettes mensuelles du ".ToUpper() + dateDebut.ToShortDateString() +" AU "+dateFin.ToShortDateString();
                    }

                    sfd.FileName =  "jounal des recettes " + date + ".pdf";
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var Count = dgvRecettes.Rows.Count / 45;
                       
                        for (var i = 0; i <= Count; i++)
                        {
                            if (i * 45 < dgvRecettes.Rows.Count)
                            {
                                var _listeImpression = AppCode.Impression.ImprimerRapportComptabilite(titre, dgvRecettes, i);

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
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
            //checkBox1.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //checkBox2.Checked = true;
            checkBox1.Checked = false;
        }

    }
}
