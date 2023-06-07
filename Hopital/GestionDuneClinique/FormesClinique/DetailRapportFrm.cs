using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.FormesClinique
{
    public partial class DetailRapportFrm : Form
    {
        public DetailRapportFrm()
        {
            InitializeComponent();
        }

        private void DetailRapportFrm_Load(object sender, EventArgs e)
        {
            button7.Location = new Point(Width - 35, 4);
            button1.Location = new Point(Width - 90, 4);
            cl1.Width = dataGridView1.Width / 7;
            cl2.Width = dataGridView1.Width / 3;
            cl3.Width = dataGridView1.Width / 2-50;
            //cl4.Width = dataGridView1.Width / 8;
            cl5.Width = dataGridView1.Width / 8;
            var montant = 0.0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                double p;
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() != "SOUS TOTAL")
                {
                    if(Double.TryParse(dataGridView1.Rows[i].Cells[3].Value.ToString(), out p))
                    {
                        montant+=p;
                    }
                }
            }
            label1.Text = montant.ToString();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void DetailRapportFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 0);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        Bitmap rapportCaissier;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportCaissier, -10, 10, rapportCaissier.Width, rapportCaissier.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        public string  titre;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                //if (dataGridView1.Rows.Count > 0)
                //{
                //    bitmap = AppCode.Impression.DetailRapportDeLaCaisse( dataGridView1,titre ,Double.Parse(label1.Text));

                //    #region MyRegion
                //    var rowCount = dataGridView1.Rows.Count;
                //    if (printDialog1.ShowDialog() == DialogResult.OK)
                //    {
                //        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                //        printDocument1.Print();
                //        if (rowCount > 35)
                //        {
                //            var Count = (dataGridView1.Rows.Count - 35) / 45;

                //            for (var i = 0; i <= Count; i++)
                //            {
                //                if (i * 45 < dataGridView1.Rows.Count)
                //                {
                //                    bitmap = AppCode.Impression.DetailRapportDeLaCaisseParPage(dataGridView1,Double.Parse(label1.Text),i);
                //                    printDocument1.Print();
                //                }
                //            }

                //        }
                //    }
                    //#endregion
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
                    sfd.FileName =  "Rapport detaillé de la caisse  _imprimé_le_" + date + ".pdf";

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        rapportCaissier =
                            AppCode.Impression.DetailRapportDeLaCaisse(dataGridView1, titre, Double.Parse(label1.Text));
                        var inputImage = @"cdali" ;
                        // Create an empty page
                        sharpPDF.pdfPage pageIndex = document.addPage();

                        document.addImageReference(rapportCaissier, inputImage);
                        sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                        pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                        if (rowCount > 30)
                        {
                            var Count = (dataGridView1.Rows.Count - 30) / 45;

                            for (var i = 0; i <= Count; i++)
                            {
                                if (i * 45 < dataGridView1.Rows.Count)
                                {
                                    rapportCaissier = AppCode.Impression.DetailRapportDeLaCaisseParPage(dataGridView1,Double.Parse(label1.Text),i);
                                          
                                    inputImage = @"cdali" + i;
                                    // Create an empty page
                                    pageIndex = document.addPage();

                                    document.addImageReference(rapportCaissier, inputImage);
                                    img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                                }
                            }

                        }
                    }
                    document.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);

                }

            
            catch (Exception ex) { MonMessageBox.ShowBox("Imprimer detail rapport ", ex); }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
