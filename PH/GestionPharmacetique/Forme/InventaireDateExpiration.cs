using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace GestionPharmacetique.Forme
{
    public partial class InventaireDateExpiration : Form
    {
        public InventaireDateExpiration()
        {
            InitializeComponent();
        }

        private void InventaireStockFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ScrollBar, 3);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                , SystemColors.ScrollBar, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void borderLabel2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkOrange, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                 , SystemColors.ScrollBar, LinearGradientMode.BackwardDiagonal);
            mGraphics.DrawRectangle(pen1, area1);
        }

       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        Bitmap _listeImpression;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(_listeImpression, 5, 20, _listeImpression.Width, _listeImpression.Height);
            e.HasMorePages = false;
        }

        List<Medicament> stockArrayList = ConnectionClass.ListeDesMedicamentsExpirees();
          
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {try
        {
             if (dataGridView1.Rows.Count > 0)
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

                    sfd.FileName = "stock_en_voie_expiration_imprimé_le_" + date + ".pdf";

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        var count = dataGridView1.Rows.Count;

                        var index = (dataGridView1.Rows.Count) / 45;

                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 45 < count)
                            {
                                var _listeImpression = AppCode.Impression.ImprimerInventaireStockExpires(i);

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
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("ff", ex);
            }
        }

        private void InventaireStockFrm_Load(object sender, EventArgs e)
        {

        }    
    }
}
