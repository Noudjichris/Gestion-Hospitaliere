using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class RechercheMedicamentIndiFrm : Form
    {
        public RechercheMedicamentIndiFrm()
        {
            InitializeComponent();
        }

        private void RechercheMedicamentIndiFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 192, 128), 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(64, 64, 64),
                Color.FromArgb(64, 64, 64), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 192, 128), 5);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Green,
                Color.Green, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string designation; public DateTime dateDebut, dateFin; public double prix; public int nbreDetaille;
        private void RechercheMedicamentIndiFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var listeJour = new List<DateTime>();
                var day = dateFin.Subtract( dateDebut).Days;
                //if (day == 0)
                //{
                //    day = day + 365;
                //}
                for (var i = 0; i <= day; i++)
                {
                    listeJour.Add(dateDebut.AddDays(i));
                }

                int qteTotaleVendue = 0, qteTotaleRetournee = 0, qteTotaleLivree = 0;
                foreach (var date in listeJour)
                {
                    
                    var dtVente = AppCode.ConnectionClass.ListeDesVentesParNom(date, date, designation);
                    var dtRetour = AppCode.ConnectionClass.ListeRetourLivraison(designation, date, date);
                    var dtLivraison = AppCode.ConnectionClass.ListeDesDetailsLivraisons(designation, date, date);

                    var count = dtVente.Rows.Count + dtRetour.Rows.Count + dtLivraison.Rows.Count;

                    var qteLivree = "";
                    var qteRetourne = "";
                    var qte = 0;
                            var detaille = 0;
                    if (dtLivraison.Rows.Count > 0)
                    {
                        qteLivree = dtLivraison.Rows[0].ItemArray[2].ToString();
                        qteTotaleLivree += Int32.Parse(dtLivraison.Rows[0].ItemArray[2].ToString());
                    }

                    if (dtRetour.Rows.Count > 0)
                    {
                        qteRetourne = dtRetour.Rows[0].ItemArray[2].ToString();
                        qteTotaleRetournee +=Int32.Parse(dtRetour.Rows[0].ItemArray[2].ToString());
                    }
                    if (count > 0)
                    {
                        if (dtVente.Rows.Count == 0)
                        {
                            dgvVente.Rows.Add( date.ToShortDateString(), qteLivree , qteRetourne , "","");
                        }
                        else if (dtVente.Rows.Count ==1)
                        {
                            var quantite = dtVente.Rows[0].ItemArray[6].ToString();
                            var prixVente = double.Parse(dtVente.Rows[0].ItemArray[5].ToString());
                            if (prixVente * 2 <= prix)
                            {
                                quantite = quantite + " en detail";
                                detaille +=Int32.Parse(dtVente.Rows[0].ItemArray[6].ToString());
                            }
                            else
                            {
                                qteTotaleVendue += Int32.Parse(dtVente.Rows[0].ItemArray[6].ToString());
                                qte += Int32.Parse(dtVente.Rows[0].ItemArray[6].ToString());
                            }
                            dgvVente.Rows.Add(date.ToShortDateString(), qteLivree, qteRetourne, 
                                dtVente.Rows[0].ItemArray[8].ToString(), quantite);
                        }
                        else if (dtVente.Rows.Count>1)
                        {
                            var quantite = dtVente.Rows[0].ItemArray[6].ToString();
                            var prixVente = double.Parse(dtVente.Rows[0].ItemArray[5].ToString());
                            if (prixVente * 2 <= prix)
                            {
                                quantite = quantite + " en detail";

                                detaille += Int32.Parse(dtVente.Rows[0].ItemArray[6].ToString());
                            }
                            else
                            {
                                qteTotaleVendue += Int32.Parse(dtVente.Rows[0].ItemArray[6].ToString());
                                qte += Int32.Parse(dtVente.Rows[0].ItemArray[6].ToString());
                            }
                            dgvVente.Rows.Add(date.ToShortDateString(), qteLivree, qteRetourne,
                                dtVente.Rows[0].ItemArray[8].ToString(), quantite);

                           
                            for (var i = 1; i < dtVente.Rows.Count; i++)
                            {
                                //qte += Int32.Parse(dtVente.Rows[i].ItemArray[6].ToString());
                                //qteTotaleVendue += Int32.Parse(dtVente.Rows[i].ItemArray[6].ToString());
                                 quantite = dtVente.Rows[i].ItemArray[6].ToString();
                                 prixVente = double.Parse(dtVente.Rows[i].ItemArray[5].ToString());
                                 if (prixVente * 2 <= prix)
                                 {
                                     quantite = quantite + " en detail";
                                     detaille += Int32.Parse(dtVente.Rows[i].ItemArray[6].ToString()) ;
                                 }
                                 else
                                 {
                                     qteTotaleVendue += Int32.Parse(dtVente.Rows[i].ItemArray[6].ToString());
                                     qte += Int32.Parse(dtVente.Rows[i].ItemArray[6].ToString());
                                 }
                                dgvVente.Rows.Add("", "", "",
                                   dtVente.Rows[i].ItemArray[8].ToString(), quantite);
                            }
                        }
                        if (nbreDetaille>0)
                        {
                            qte += (int)detaille / nbreDetaille;
                            qteTotaleVendue += (int)detaille / nbreDetaille;
                        }
                        dgvVente.Rows.Add("", "", "","Total",qte);
                        dgvVente.Rows.Add("", "", "", "", "");
                    }


                } dgvVente.Rows.Add("Nbres totaux", qteTotaleLivree, qteTotaleRetournee, "", qteTotaleVendue);
                //for (var i = 0; i < dt.Rows.Count; i++)
                //{
                //    var date = dt.Rows[i].ItemArray[1].ToString();
                //    var heure = "00:00::00";

                //    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[8].ToString()))
                //    {
                //        heure = dt.Rows[i].ItemArray[8].ToString();
                //    }
                //    var dateVente = date.Substring(0, 10) + " " + heure;
                //    //dgvVente.Rows.Add(
                //    //    dt.Rows[i].ItemArray[0].ToString(),
                //    //    dateVente,
                //    //    dt.Rows[i].ItemArray[6].ToString(),
                //    //    dt.Rows[i].ItemArray[5].ToString(),
                //    //    dt.Rows[i].ItemArray[7].ToString()
                //    //);
                //}
                label8.Text = "Chronologie de mouvement du produit " + designation + " du " + dateDebut.ToShortDateString()+ " au "+dateFin.ToShortDateString();
                //lblNreVendue.Text = dgvVente.Rows.Count.ToString();
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void btnImprimer_Click(object sender, EventArgs e)
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
                pathFolder = pathFolder + "\\Rapport vente";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                sfd.FileName = "Chronologie_du_" + date + ".pdf";

                if (dgvVente.Rows.Count > 0)
                {
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var Count = dgvVente.Rows.Count / 45;
                        var titre = label8.Text;
                        for (var i = 0; i <= Count; i++)
                        {
                            if (i * 45 < dgvVente.Rows.Count)
                            {
                                var _listeImpression = AppCode.ImprimerRaportVente.ImprimerChronologieProduit(dgvVente,titre, i);

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

    }
}
