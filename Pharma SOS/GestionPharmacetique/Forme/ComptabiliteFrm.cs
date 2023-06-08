using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class ComptabiliteFrm : Form
    {
        public ComptabiliteFrm()
        {
            InitializeComponent();
        }
        public DateTime dateDebut, dateFin;
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        public string index;


        
        private void ComptabiliteFrm_Load(object sender, EventArgs e)
        {
            try
            {
                cl2.Width = dgvProduit.Width / 6 - 10;
                cl3.Width = dgvProduit.Width / 6 ;
                cl5.Width = dgvProduit.Width / 6 ;
                cl4.Width = 2 * dgvProduit.Width / 6 ;
                Column1.Width = dgvProduit.Width / 6;
                if (index == "j")
                {
                    ComptabiliteJournaliere();
                }
                else if (index == "m")
                {
                    ComptabiliteMensuelle();
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception);
            }
           
            //for(var i = 0; i
        }

        void ComptabiliteJournaliere()
        {
            try
            {
                var listeJour = new List<DateTime>();
                var day = dateFin.DayOfYear - dateDebut.DayOfYear;
                if (day < 0)
                {
                    day = day + 365;
                }
                for (var i = 0; i <= day; i++)
                {
                    listeJour.Add(dateDebut.AddDays(i));
                }
                var depenseTotal = 0.0;
                var caisseTotal = 0.0;
                foreach (var date in listeJour)
                {
                    var depenseJournalier = 0.0;
                    var dtCaisse = AppCode.ConnectionClass.ListeDesPaiements(date, date, "");
                    //var dtDepense = AppCode.ConnectionClass.ListeDesDepenses(dateDebut, dateFin);
                    var listeDepense = AppCode.ConnectionClass.DataTableDesDepenses(date, date);
                    var caisseJournalier = 0.0;
                    foreach (DataRow rowCaisse in dtCaisse.Rows)
                    {
                        caisseTotal += double.Parse(rowCaisse.ItemArray[2].ToString());
                        caisseJournalier += double.Parse(rowCaisse.ItemArray[2].ToString());
                    }

                    var count = dtCaisse.Rows.Count + listeDepense.Rows.Count;
                    if (count > 0)
                    {
                        if (listeDepense.Rows.Count == 0)
                        {
                            dgvProduit.Rows.Add(date.ToShortDateString(), string.Format(elGR, "{0:0,0}", caisseJournalier), "-", "0", "");
                        }
                        else if (listeDepense.Rows.Count == 1)
                        {
                            var libelle = listeDepense.Rows[0].ItemArray[0].ToString();
                            if (libelle.Length > 38)
                            {
                                libelle = libelle.Substring(0, 38);
                            }

                            depenseJournalier += double.Parse(listeDepense.Rows[0].ItemArray[1].ToString());
                            depenseTotal += double.Parse(listeDepense.Rows[0].ItemArray[1].ToString());
                            dgvProduit.Rows.Add(date.ToShortDateString(), string.Format(elGR, "{0:0,0}", caisseJournalier),
                               libelle,
                               string.Format(elGR, "{0:0,0}", double.Parse(listeDepense.Rows[0].ItemArray[1].ToString())), "");
                        }
                        else if (listeDepense.Rows.Count > 1)
                        {
                            var libelle = listeDepense.Rows[0].ItemArray[0].ToString();
                            if (libelle.Length > 38)
                            {
                                libelle = libelle.Substring(0, 38);
                            }
                            depenseJournalier += double.Parse(listeDepense.Rows[0].ItemArray[1].ToString());
                            depenseTotal += double.Parse(listeDepense.Rows[0].ItemArray[1].ToString());

                            dgvProduit.Rows.Add(date.ToShortDateString(), string.Format(elGR, "{0:0,0}", caisseJournalier),
                               libelle,
                                    string.Format(elGR, "{0:0,0}", double.Parse(listeDepense.Rows[0].ItemArray[1].ToString())), "");
                            for (var i = 1; i < listeDepense.Rows.Count; i++)
                            {
                                libelle = listeDepense.Rows[i].ItemArray[0].ToString();
                                if (libelle.Length > 38)
                                {
                                    libelle = libelle.Substring(0, 38);
                                }
                                depenseJournalier += double.Parse(listeDepense.Rows[i].ItemArray[1].ToString());
                                depenseTotal += double.Parse(listeDepense.Rows[i].ItemArray[1].ToString());

                                dgvProduit.Rows.Add("", "",
                                       libelle, string.Format(elGR, "{0:0,0}",
                                       double.Parse(listeDepense.Rows[i].ItemArray[1].ToString())), "");

                            }
                        }

                        var solde = caisseJournalier - depenseJournalier;
                        dgvProduit.Rows.Add("",
                            string.Format(elGR, "{0:0,0}", ""),
                            "Total depenses", string.Format(elGR, "{0:0,0}", depenseJournalier),
                            string.Format(elGR, "{0:0,0}", solde));
                    }
                }

                dgvProduit.Rows.Add("Totaux",
                       string.Format(elGR, "{0:0,0}", caisseTotal),
                       "", string.Format(elGR, "{0:0,0}", depenseTotal),
                       string.Format(elGR, "{0:0,0}", caisseTotal - depenseTotal));

                foreach (DataGridViewRow row in dgvProduit.Rows)
                {
                    if (row.Cells[0].Value.ToString() == "    ")
                    {
                        //row.DefaultCellStyle.BackColor = Color.SaddleBrown;
                        //row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else if (row.Cells[0].Value.ToString() == "Totaux")
                    {
                        row.DefaultCellStyle.BackColor = Color.SaddleBrown;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch { }
        }

        void ComptabiliteMensuelle()
        {
            try
            {
                var listeMois = new List<int>();
                var mois = dateFin.Month - dateDebut.Month;
              
                for (var i = 0; i <= mois; i++)
                {
                    listeMois.Add(dateDebut.Month+i);
                }
                var depenseTotal = 0.0;
                var caisseTotal = 0.0;
                foreach (var Mois in listeMois)
                {
                    var depenseMensuel = 0.0;
                    var dtCaisse = AppCode.ConnectionClass.ListeDesPaiements(ObtenirDebutJour(Mois), ObtenirFinJour(Mois), "");
                    var listeDepense = AppCode.ConnectionClass.DataTableDesDepenses(ObtenirDebutJour(Mois), ObtenirFinJour(Mois));
                    var caisseMensuel = 0.0;
                    foreach (DataRow rowCaisse in dtCaisse.Rows)
                    {
                        caisseMensuel += double.Parse(rowCaisse.ItemArray[2].ToString());
                        caisseTotal += double.Parse(rowCaisse.ItemArray[2].ToString());
                    }
                    foreach (DataRow rowDepense in listeDepense.Rows)
                    {
                        depenseMensuel += double.Parse(rowDepense.ItemArray[1].ToString());
                        depenseTotal += double.Parse(rowDepense.ItemArray[1].ToString());
                    }
                    var solde = caisseMensuel - depenseMensuel;
                    dgvProduit.Rows.Add(ObtenirMois(Mois), string.Format(elGR, "{0:0,0}", caisseMensuel),
                           "",
                           string.Format(elGR, "{0:0,0}", depenseTotal), string.Format(elGR, "{0:0,0}", solde));
                   
                   
                }

                dgvProduit.Rows.Add("Totaux",
                       string.Format(elGR, "{0:0,0}", caisseTotal),
                       "", string.Format(elGR, "{0:0,0}", depenseTotal),
                       string.Format(elGR, "{0:0,0}", caisseTotal - depenseTotal));

                foreach (DataGridViewRow row in dgvProduit.Rows)
                {
                    if (row.Cells[0].Value.ToString() == "Totaux")
                    {
                        row.DefaultCellStyle.BackColor = Color.SaddleBrown;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }
        private void button5_Click(object sender, EventArgs e)
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

                    sfd.FileName = "Journal_Des_Depenses_detaillées_Impriméé_le_" + date + ".xls";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {

                        ToCsV(dgvProduit, sfd.FileName); // Here dataGridview1 is your grid view name
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
               
                
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dGV.Columns.Count; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dGV.Rows[i].Cells.Count; j++)
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

        private void button3_Click(object sender, EventArgs e)
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

                sfd.FileName = "Comptabilite_du_" + date + ".pdf";

                if (dgvProduit.Rows.Count > 0)
                {
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var Count = dgvProduit.Rows.Count / 45;
                        var titre = "Comptabilité journalière du " + dateDebut.ToShortDateString() +
                            " au " + dateFin.ToShortDateString();
                        if (index == "m")
                        {
                            titre = "Comptabilité mensuèlle de " + dateDebut.Year;
                        }
                            for (var i = 0; i <= Count; i++)
                            {
                                if (i * 45 < dgvProduit.Rows.Count)
                                {
                                    var _listeImpression = AppCode.ImprimerRaportVente.ImprimerRapportComptabilite(titre, dgvProduit, i);

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

        string  ObtenirMois(int mois)
        {
            switch (mois)
            {
                case 1:
                    return "Mois de Janvier";
                case 2:
                    return "Mois de Fevrier";
                case 3:
                    return "Mois de Mars";
                case 4:
                    return "Mois de Avril";
                case 5:
                    return "Mois de Mai";
                case 6:
                    return "Mois de Juin";
                case 7:
                    return "Mois de Juillet";
                case 8:
                    return "Mois de Août";
                case 9:
                    return "Mois de Septembre";
                case 10:
                    return "Mois de Octobre";
                case 11:
                    return "Mois de Novembre";
                case 12:
                    return "Mois de Decembre";
                default:
                    return "";
            };
        }

        DateTime ObtenirDebutJour(int  mois)
        {
            return DateTime.Parse("01/"+mois +"/" + dateDebut.Year); 
        }

        DateTime ObtenirFinJour(int mois)
        {
            if (mois == 1)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 2)
            {
                if(DateTime.IsLeapYear(dateDebut.Year))
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
    }
}
