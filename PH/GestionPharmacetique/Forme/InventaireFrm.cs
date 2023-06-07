using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;
using Excel = Microsoft.Office.Interop.Excel; 

namespace GestionPharmacetique.Forme
{
    public partial class InventaireFrm : Form
    {
        public InventaireFrm()
        {
            InitializeComponent();
        }

        private void InventaireFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkOrange, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CadetBlue, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.CadetBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.CadetBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        private void InventaireFrm_Load(object sender, EventArgs e)
        {
            try
            {  
                button3.Location = new Point(Width - 45, button3.Location.Y);
                //radioButton1.Checked = true;
                label1.Text = "Inventaire du " + DateTime.Now.ToString();
                comboBox1.Items.Add("<TOUTES>");
                var  listeFamille = ConnectionClass.ListeDesFamille();
                foreach (Medicament famille in listeFamille)
                {
                    comboBox1.Items.Add(famille.Designation.ToUpper());
                }

                listView1.Columns.Clear();
                listView1.Columns.Add("DESIGNATION", listView1.Width / 4);
                listView1.Columns.Add("CODE BARRE", 0);
                listView1.Columns.Add("QTE", listView1.Width / 8);
                listView1.Columns.Add("Stock depôt".ToUpper(), listView1.Width / 8);
                listView1.Columns.Add("Px ACHAT", listView1.Width / 9);
                listView1.Columns.Add("Px VENTE", listView1.Width / 8);
                listView1.Columns.Add("Px ACHAT TOT", listView1.Width / 8);
                listView1.Columns.Add("Px. TOTAL", listView1.Width / 8);
                listView1.Columns.Add("RESULTAT NET", 0);
                listView1.Items.Clear();
                label3.Location = new Point(6*listView1.Width/8-20, this.label3.Location.Y);
                label4.Location = new Point(7 * listView1.Width / 8-20, this.label4.Location.Y);

                comboBox2.Text = "Stock Pharmacie";
                decimal prixCT = 0;
                decimal prixPT = 0;
                var resultatNet = 0m;
                if (dtp2.Value.Date == DateTime.Now.Date)
                {
                    #region DATE
                    List<Medicament> listeArrayList = ConnectionClass.ListeDesMedicamentsRechercherParNom("");
                    foreach (Medicament medicament in listeArrayList)
                    {
                        int qte;
                        decimal ppt, pct;
                        if (comboBox2.Text == "Stock Pharmacie")
                        {
                            qte = medicament.Quantite;
                        }
                        else if (comboBox2.Text == "Stock depôt")
                        {
                            qte = medicament.GrandStock;
                        }
                        else if (comboBox2.Text == "Stock Total")
                        {
                            qte = medicament.GrandStock + medicament.Quantite;
                        }
                        else
                        {
                            qte = medicament.Quantite;
                        }
                        ppt = qte * medicament.PrixVente;
                        pct = qte * medicament.PrixAchat;
                        prixCT += qte * medicament.PrixAchat;
                        prixPT += qte * medicament.PrixVente;;
                        resultatNet += (qte * medicament.PrixVente- qte * medicament.PrixAchat);
                        var items = new string[]
                    {
                     medicament.NomMedicament, 
                     medicament.NumeroMedicament ,                    
                     medicament.Quantite.ToString(),
                     medicament.GrandStock.ToString(),
                     medicament.PrixAchat.ToString(),
                     medicament.PrixVente.ToString(),
                    pct.ToString(),
                     ppt.ToString(),
                     (ppt-pct).ToString()
                    };
                        ListViewItem lstListViewItem = new ListViewItem(items);
                        listView1.Items.Add(lstListViewItem);
                    }
                    label2.Text = "Nombre de produits inventorié : " + listView1.Items.Count;
                    label3.Text = String.Format(elGR, "{0:0,0}", prixCT) + " F.CFA";
                    label4.Text = String.Format(elGR, "{0:0,0}", prixPT) + " F.CFA";
                    lblResultatNet.Text = String.Format(elGR, "{0:0,0}", resultatNet) + " F.CFA"; 
                    #endregion
                }
             
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste inventaire", exception);
            }
        }


        private void borderLabel2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

     

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }

        private Bitmap _inventaireStock;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(_inventaireStock, 0, 25, _inventaireStock.Width, _inventaireStock.Height);
            e.HasMorePages = false;
        }
        
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

                var pathFolder = "C:\\Dossier Pharmacie";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                pathFolder = pathFolder + "\\Rapport";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                sfd.FileName = "Impression_listing_du_" + date + ".pdf";

                var titre = "Inventaire de stock du " + DateTime.Now;
                if (!string.IsNullOrEmpty(comboBox1.Text))
                {
                    titre = "Inventaire de stock de la famille de " +comboBox1.Text +" du " + DateTime.Now;
                }
                if (listView1.Items.Count > 0)
                {
                    var Count = listView1.Items.Count / 45;
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        for (var i = 0; i <= Count; i++)
                        {
                            if (i * 45 < listView1.Items.Count)
                            {
                                _inventaireStock =  AppCode.Imprimer.ImprimerInventaireStockAnnuel(titre,listView1,i);

                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage pageIndex = document.addPage();

                                document.addImageReference(_inventaireStock, inputImage);
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

                    //if (printDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    printDocument1.PrinterSettings = printDialog1.PrinterSettings;

                    //}
                    //for (var i = 0; i <= Count; i++)
                    //{
                    //    if (i * 33 < listView1.Items.Count)
                    //    {
                    //        _inventaireStock = AppCode.Imprimer.ImprimerInventaireStockAnnuel(titre,listView1,i);
                            
                    //    }
                    //}
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            List<Medicament> listeMedicament = ConnectionClass.ListeDesMedicamentsAStockBas();
            if (listeMedicament.Count > 0)
            {
                AlerteStockFrm frm = new AlerteStockFrm();
                frm.Location = new Point(168, 172);
                foreach (Medicament medicament in listeMedicament)
                {
frm.dataGridView1.Rows.Add(
                        medicament.NumeroMedicament,
                        medicament.NomMedicament,
                        medicament.Quantite.ToString(),
                        medicament.QuantiteAlerte.ToString()
                    );                 
                }
                frm.ShowDialog();
            }
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                decimal prixCT = 0;
                decimal prixPT = 0;
                var resultatNet = 0m;
                if (dtp2.Value.Date == DateTime.Now.Date)
                {
                    #region DATE
                    List<Medicament> listeArrayList = ConnectionClass.ListeDesMedicamentsRechercherParNom("");
                    foreach (Medicament medicament in listeArrayList)
                    {
                        int qte;
                        decimal ppt, pct;
                        if (comboBox2.Text == "Stock Pharmacie")
                        {
                            qte = medicament.Quantite;
                       
                        }
                        else if (comboBox2.Text == "Stock depôt")
                        {
                            qte = medicament.GrandStock;
                        }
                        else if (comboBox2.Text == "Stock Total")
                        {
                            qte = medicament.GrandStock + medicament.Quantite;
                        }
                        else
                        {
                            qte = medicament.Quantite;
                        }
                        ppt = qte * medicament.PrixVente;
                        pct = qte * medicament.PrixAchat;
                        prixCT += qte * medicament.PrixAchat;
                        prixPT += qte * medicament.PrixVente; ;
                        resultatNet += (qte * medicament.PrixVente - qte * medicament.PrixAchat);
                        var items = new string[]
                    {
                     medicament.NomMedicament, 
                     medicament.NumeroMedicament ,                    
                     medicament.Quantite.ToString(),
                     medicament.GrandStock.ToString(),
                     medicament.PrixAchat.ToString(),
                     medicament.PrixVente.ToString(),
                    pct.ToString(),
                     ppt.ToString(),
                     (ppt-pct).ToString()
                    };
                        ListViewItem lstListViewItem = new ListViewItem(items);
                        listView1.Items.Add(lstListViewItem);
                    }
                    label2.Text = "Nombre de produits inventorié : " + listView1.Items.Count;
                    label3.Text = String.Format(elGR, "{0:0,0}", prixCT) + " F.CFA";
                    label4.Text = String.Format(elGR, "{0:0,0}", prixPT) + " F.CFA";
                    lblResultatNet.Text = String.Format(elGR, "{0:0,0}", resultatNet) + " F.CFA";
                    #endregion
                }
                else
                {
                    var listeArrayList = from l in AppCode.ConnectionClass.ListeEtatStock(dtp2.Value.Date)
                                         join m in AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                                         on l.NumeroMedicament equals m.NumeroMedicament
                                         select new { l.DateExpiration, l.GrandStock, l.Quantite, m.NomMedicament, m.PrixAchat, m.PrixVente, m.NumeroMedicament };
                    foreach (var medicament in listeArrayList)
                    {
                        int qte;
                        decimal ppt, pct;
                        if (comboBox2.Text == "Stock Pharmacie")
                        {    
                            qte = medicament.Quantite;
                            //if (ConnectionClass.ListeDesVentesParNumeroProduit(medicament.NumeroMedicament, dtp2.Value, dtp2.Value.Date.AddHours(24)).Rows.Count > 0)
                            //    qte += Convert.ToInt32(ConnectionClass.ListeDesVentesParNumeroProduit(medicament.NumeroMedicament, dtp2.Value, dtp2.Value.Date.AddHours(24)).Rows[0].ItemArray[2].ToString());
                        }
                        else if (comboBox2.Text == "Stock depôt")
                        {
                            qte = medicament.GrandStock;
                        }
                        else if (comboBox2.Text == "Stock Total")
                        {
                            qte = medicament.GrandStock + medicament.Quantite;
                        }
                        else
                        {
                            qte = medicament.Quantite;
                        }
                        ppt = qte * medicament.PrixVente;
                        pct = qte * medicament.PrixAchat;
                        var items = new string[]
                    {
                     medicament.NomMedicament, 
                     medicament.NumeroMedicament ,                    
                     medicament.Quantite.ToString(),
                     medicament.GrandStock.ToString(),
                     medicament.PrixAchat.ToString(),
                     medicament.PrixVente.ToString(),
                    pct.ToString(),
                     ppt.ToString(),
                     (ppt-pct).ToString()
                    };
                        ListViewItem lstListViewItem = new ListViewItem(items);
                        listView1.Items.Add(lstListViewItem);
                        prixCT += pct;
                        prixPT += ppt;
                        resultatNet += (ppt - pct);

                    }
                    label2.Text = "Nombre de produits inventorié : " + listView1.Items.Count;
                    label3.Text = String.Format(elGR, "{0:0,0}", prixCT) + " F.CFA";
                    label4.Text = String.Format(elGR, "{0:0,0}", prixPT) + " F.CFA";
                    lblResultatNet.Text = String.Format(elGR, "{0:0,0}", resultatNet) + " F.CFA";
                }   
            }
            catch (Exception ex ){ MessageBox.Show(ex.ToString());}
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string data = null;
                int i = 0;
                int j = 0;

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[1, 2] = "INVENTAIRE DES PRODUITS DU " + DateTime.Now;
                xlWorkSheet.Cells[3, 2] = "DESIGNATION";
                xlWorkSheet.Cells[3, 3] = "PRIX ACHAT";
                xlWorkSheet.Cells[3, 4] = "PRIX VENTE";
                xlWorkSheet.Cells[3, 5] = "QTE";
                xlWorkSheet.Cells[3, 6] = "GRD STOCK";
                 xlWorkSheet.Cells[3, 7] = "STOCK TOTAL";
                var ds = GestionPharmacetique.AppCode.ConnectionClass.Inventaire();
                var total = 0.0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet.Cells[i + 4, j + 2] = data;
                    }
                    total += Double.Parse(ds.Tables[0].Rows[i].ItemArray[3].ToString());
                }

                xlWorkSheet.Cells[ds.Tables[0].Rows.Count + 4, 2] = "TOTAL";
                xlWorkSheet.Cells[ds.Tables[0].Rows.Count + 4, 5] = total.ToString();
                var path = @"Inventaire_du_" + DateTime.Today.ToShortDateString().Trim().Replace("/", "-") + "_" + DateTime.Now.Hour + "h_" + DateTime.Now.Minute;

                xlWorkBook.SaveAs( path+ ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MonMessageBox.ShowBox("Les données ont été exportées vers le fichier Excel " + path +" sur mes documents" , "Info", "affirmation.png");
                System.Diagnostics.Process.Start(@"Documents//" + path + ".xls");
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Export", ex); }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MonMessageBox.ShowBox("Exception Occured while releasing object ", ex);
            }
            finally
            {
                GC.Collect();
            }
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (checkBox1.Checked)
            {
                try
                {
                    decimal prixCT = 0;
                    decimal prixPT = 0;
                    var resultatNet = 0m;
                    var Count = 0;
                    if (dtp2.Value.Date == DateTime.Now.Date)
                    {
                        #region DATE
                        List<Medicament> listeArrayList = ConnectionClass.ListeDesMedicamentsRechercherParNom("");
                        foreach (Medicament medicament in listeArrayList)
                        {
                            int qte;
                            decimal ppt, pct;
                            if (comboBox2.Text == "Stock Pharmacie")
                            {
                                qte = medicament.Quantite;
                            }
                            else if (comboBox2.Text == "Stock depôt")
                            {
                                qte = medicament.GrandStock;
                            }
                            else if (comboBox2.Text == "Stock Total")
                            {
                                qte = medicament.GrandStock + medicament.Quantite;
                            }
                            else
                            {
                                qte = medicament.Quantite;
                            }
                            if (qte > 0)
                            {
                                ppt = qte * medicament.PrixVente;
                                pct = qte * medicament.PrixAchat;
                                prixCT += qte * medicament.PrixAchat;
                                prixPT += qte * medicament.PrixVente; ;
                                resultatNet += (qte * medicament.PrixVente - qte * medicament.PrixAchat);
                                var items = new string[]
                    {
                     medicament.NomMedicament, 
                     medicament.NumeroMedicament ,                    
                     medicament.Quantite.ToString(),
                     medicament.GrandStock.ToString(),
                     medicament.PrixAchat.ToString(),
                     medicament.PrixVente.ToString(),
                    pct.ToString(),
                     ppt.ToString(),
                     (ppt-pct).ToString()
                    };
                                ListViewItem lstListViewItem = new ListViewItem(items);
                                listView1.Items.Add(lstListViewItem);
                            }
                            Count++;
                        }
                        label2.Text = "Nombre de produits inventorié : " + Count;
                        label3.Text = String.Format(elGR, "{0:0,0}", prixCT) + " F.CFA";
                        label4.Text = String.Format(elGR, "{0:0,0}", prixPT) + " F.CFA";
                        lblResultatNet.Text = String.Format(elGR, "{0:0,0}", resultatNet) + " F.CFA";
                        #endregion
                    }
                    else
                    {
                        var listeArrayList = from l in AppCode.ConnectionClass.ListeEtatStock(dtp2.Value.Date)
                                             join m in AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                                             on l.NumeroMedicament equals m.NumeroMedicament
                                             select new { l.DateExpiration, l.GrandStock, l.Quantite, m.NomMedicament, m.PrixAchat, m.PrixVente, m.NumeroMedicament };
                        foreach (var medicament in listeArrayList)
                        {
                            int qte;
                            decimal ppt, pct;
                            if (comboBox2.Text == "Stock Pharmacie")
                            {
                                qte = medicament.Quantite;
                            }
                            else if (comboBox2.Text == "Stock depôt")
                            {
                                qte = medicament.GrandStock;
                            }
                            else if (comboBox2.Text == "Stock Total")
                            {
                                qte = medicament.GrandStock + medicament.Quantite;
                            }
                            else
                            {
                                qte = medicament.Quantite;
                            }
                            ppt = qte * medicament.PrixVente;
                            pct = qte * medicament.PrixAchat;
                            var items = new string[]
                    {
                     medicament.NomMedicament, 
                     medicament.NumeroMedicament ,                    
                     medicament.Quantite.ToString(),
                     medicament.GrandStock.ToString(),
                     medicament.PrixAchat.ToString(),
                     medicament.PrixVente.ToString(),
                    pct.ToString(),
                     ppt.ToString(),
                     (ppt-pct).ToString()
                    };
                            ListViewItem lstListViewItem = new ListViewItem(items);
                            listView1.Items.Add(lstListViewItem);
                            prixCT += pct;
                            prixPT += ppt;
                            resultatNet += (ppt - pct);

                        }
                        label2.Text = "Nombre de produits inventorié : " + listView1.Items.Count;
                        label3.Text = String.Format(elGR, "{0:0,0}", prixCT) + " F.CFA";
                        label4.Text = String.Format(elGR, "{0:0,0}", prixPT) + " F.CFA";
                        lblResultatNet.Text = String.Format(elGR, "{0:0,0}", resultatNet) + " F.CFA";
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
            else
            {
                comboBox1_SelectedIndexChanged(null, null);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(null, null);
        }

   
    }
}
