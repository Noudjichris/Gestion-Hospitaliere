using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class RapportDesEntreprises : Form
    {
        public RapportDesEntreprises()
        {
            InitializeComponent();
        }

        private void RapportDesEntreprises_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkOrange, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void RapportDesEntreprises_Load(object sender, EventArgs e)
        {
            dataGridViewTextBoxColumn3.Width = Column6.Width = dataGridView2.Width / 3;
            var dtEntrep = AppCode.ConnectionClass.ListeDesEntreprises();
            foreach (var  entrep in dtEntrep)
            { 
                comboBox2.Items.Add(entrep.NomEntreprise);
            }
            button3.Location = new Point(Width - 43, 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Visible = true;
                    dataGridView1.Visible = false;

                    var listeDesEntreprises = AppCode.ConnectionClass.ListeDesEntreprises();
                    var totalFacture = 0.0;

                    if (comboBox2.Text != "")
                    {
                        var listeClient = AppCode.ConnectionClass.ListeDesClient();
                        var list = from cl in listeClient
                                   where cl.Entreprise == comboBox2.Text
                                   orderby cl.NomClient
                                   select cl;

                        if (list.Count() > 0)
                        {
                            foreach (var q in list)
                            {
                                var sousTotal = 0.0;
                                var listeDetaaillesCredit = AppCode.ConnectionClass.ListeDesVentesParClient(q.Id, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                                if (listeDetaaillesCredit.Rows.Count > 1)
                                {
                                    if (listeDetaaillesCredit.Rows[0].ItemArray[9].ToString() == "0")
                                    {
                                    sousTotal += Double.Parse(listeDetaaillesCredit.Rows[0].ItemArray[7].ToString());
                                    totalFacture += Double.Parse(listeDetaaillesCredit.Rows[0].ItemArray[7].ToString());
                                    
                                        dataGridView2.Rows.Add
                                              (
                                                  q.NomClient.ToUpper(),
                                                  DateTime.Parse(listeDetaaillesCredit.Rows[0].ItemArray[0].ToString()).ToShortDateString(),
                                                  listeDetaaillesCredit.Rows[0].ItemArray[8].ToString(),
                                                   listeDetaaillesCredit.Rows[0].ItemArray[6].ToString(),
                                                   listeDetaaillesCredit.Rows[0].ItemArray[5].ToString(),
                                                   listeDetaaillesCredit.Rows[0].ItemArray[7].ToString()
                                              );
                                    }
                                    for (int j = 1; j < listeDetaaillesCredit.Rows.Count; j++)
                                    {
                                        if (listeDetaaillesCredit.Rows[j].ItemArray[9].ToString() == "0")
                                        {
                                            sousTotal += Double.Parse(listeDetaaillesCredit.Rows[j].ItemArray[7].ToString());
                                            totalFacture += Double.Parse(listeDetaaillesCredit.Rows[j].ItemArray[7].ToString());

                                            dataGridView2.Rows.Add
                                              (
                                                  "",
                                                  DateTime.Parse(listeDetaaillesCredit.Rows[j].ItemArray[0].ToString()).ToShortDateString(),
                                                  listeDetaaillesCredit.Rows[j].ItemArray[8].ToString(),
                                                   listeDetaaillesCredit.Rows[j].ItemArray[6].ToString(),
                                                   listeDetaaillesCredit.Rows[j].ItemArray[5].ToString(),
                                                   listeDetaaillesCredit.Rows[j].ItemArray[7].ToString()
                                              );
                                        }
                                    }
                                }
                                else if (listeDetaaillesCredit.Rows.Count == 1)
                                {
                                    if (listeDetaaillesCredit.Rows[0].ItemArray[9].ToString() == "0")
                                    {
                                    sousTotal += Double.Parse(listeDetaaillesCredit.Rows[0].ItemArray[7].ToString());
                                    totalFacture += Double.Parse(listeDetaaillesCredit.Rows[0].ItemArray[7].ToString());
                                    
                                        dataGridView2.Rows.Add
                                              (
                                                  q.NomClient.ToUpper(),
                                                   DateTime.Parse(listeDetaaillesCredit.Rows[0].ItemArray[0].ToString()).ToShortDateString(),
                                                  listeDetaaillesCredit.Rows[0].ItemArray[8].ToString(),
                                                   listeDetaaillesCredit.Rows[0].ItemArray[6].ToString(),
                                                   listeDetaaillesCredit.Rows[0].ItemArray[5].ToString(),
                                                   listeDetaaillesCredit.Rows[0].ItemArray[7].ToString()
                                              );
                                    }
                                }
                                if (listeDetaaillesCredit.Rows.Count > 0)
                                {
                                    dataGridView2.Rows.Add
                                                     (
                                                         "", "", "SOUS TOTAL", "", "", sousTotal
                                                     );
                                    dataGridView2.Rows.Add
                                          (
                                              "", "", "", "", "", ""
                                          );
                                }
                            }
                        }
                    }
                    borderLabel2.Text = totalFacture.ToString();
                    dataGridView2.Rows.Remove(dataGridView2.Rows[dataGridView2.Rows.Count - 1]);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    
                    var listeClient = AppCode.ConnectionClass.ListeDesClient();
                    var list = from cl in listeClient
                               where cl.Entreprise == comboBox2.Text
                               orderby cl.NomClient
                               select cl;
                    var totalFacture = 0.0;
                    foreach (var q in list)
                    {
                        var sousTotal = 0.0;
                        var dt = AppCode.ConnectionClass.ListeDesVentes(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, q.NomClient);
                               
                        if (dt.Rows.Count > 1)
                        {
                            if (dt.Rows[0].ItemArray[6].ToString() == "0")
                            {
                                sousTotal += Double.Parse(dt.Rows[0].ItemArray[3].ToString());
                                totalFacture += Double.Parse(dt.Rows[0].ItemArray[3].ToString());

                                dataGridView1.Rows.Add
                                        (
                                            q.NomClient.ToUpper(), DateTime.Parse(dt.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                            dt.Rows[0].ItemArray[3].ToString()
                                        );
                            }
                                for (var i = 1; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i].ItemArray[6].ToString() == "0")
                                    {
                                        sousTotal += Double.Parse(dt.Rows[i].ItemArray[3].ToString());
                                        totalFacture += Double.Parse(dt.Rows[i].ItemArray[3].ToString());

                                        dataGridView1.Rows.Add
                                            (
                                                "", DateTime.Parse(dt.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                                                dt.Rows[i].ItemArray[3].ToString()
                                            );
                                    }
                                }
                            
                        }
                        else if (dt.Rows.Count == 1)
                        {
                            if (dt.Rows[0].ItemArray[6].ToString() == "0")
                            {
                                sousTotal += Double.Parse(dt.Rows[0].ItemArray[3].ToString());
                                totalFacture += Double.Parse(dt.Rows[0].ItemArray[3].ToString());

                                dataGridView1.Rows.Add
                                       (
                                           q.NomClient.ToUpper(), DateTime.Parse(dt.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                           dt.Rows[0].ItemArray[3].ToString()
                                       );
                            }
                        }

                        if (dt.Rows.Count > 0)
                        {
                            dataGridView1.Rows.Add
                                 (
                                     "SOUS TOTAL", "", sousTotal
                                 );
                            dataGridView1.Rows.Add
                                  (
                                      "", "", ""
                                  );

                        }
                    }
                    borderLabel2.Text = totalFacture.ToString();
                    dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count - 1]);
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("rapprot conventionne", ex); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
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
                sfd.FileName = "Rapport_imprimé_le_" + date + ".pdf";

                if (checkBox1.Checked)
                {
                    if (dataGridView2.Rows.Count > 0)
                    {

                        var bitmap = AppCode.ImprimerRaportVente.RapportDuneEntrepriseDtetailles(dataGridView2, comboBox2.Text, dateTimePicker2.Value.Date, double.Parse(borderLabel2.Text));
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string inputImage1 = @"cdali";
                            // Create an empty page
                            sharpPDF.pdfPage page = document.addPage();

                            document.addImageReference(bitmap, inputImage1);
                            sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                            page.addImage(img, -10, 0, page.height, page.width);

                            var count = dataGridView2.Rows.Count;
                            if (count > 36)
                            {
                                var index = (dataGridView2.Rows.Count - 36) / 45;

                                for (var i = 0; i <= index; i++)
                                {
                                    if (i * 45 < count)
                                    {
                                        bitmap = AppCode.ImprimerRaportVente.RapportDuneEntrepriseDtetailles(dataGridView2, double.Parse(borderLabel2.Text), i);
                                        var inputImage = @"cdali" + i;
                                        // Create an empty page
                                        sharpPDF.pdfPage pageIndex = document.addPage();

                                        document.addImageReference(bitmap, inputImage);
                                        sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                            }
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);

                        }
                    }
                }
                else
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        var liste = AppCode.ConnectionClass.ListeDesEntreprises();

                        AppCode.Entreprise entreprise = new AppCode.Entreprise(liste[0].NumeroEntreprise, liste[0].NomEntreprise,
                            liste[0].Telephone1, liste[0].Telephone2, liste[0].Email, liste[0].Adresse);
                        var bitmap = AppCode.ImprimerRaportVente.RapportDuneEntreprise(dataGridView1, entreprise, dateTimePicker2.Value.Date, double.Parse(borderLabel2.Text));

                        var rowCount = dataGridView1.Rows.Count;

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            string inputImage1 = @"cdali";
                            // Create an empty page
                            sharpPDF.pdfPage page = document.addPage();

                            document.addImageReference(bitmap, inputImage1);
                            sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                            page.addImage(img, -10, 0, page.height, page.width);

                            var count = dataGridView1.Rows.Count;
                            if (count > 30)
                            {
                                var index = (dataGridView1.Rows.Count - 30) / 45;

                                for (var i = 0; i <= index; i++)
                                {
                                    if (i * 45 < count)
                                    {
                                        bitmap = AppCode.ImprimerRaportVente.RapportDuneEntreprise(dataGridView1, double.Parse(borderLabel2.Text), i);
                                        var inputImage = @"cdali" + i;
                                        // Create an empty page
                                        sharpPDF.pdfPage pageIndex = document.addPage();

                                        document.addImageReference(bitmap, inputImage);
                                        sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                            }
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);

                        }
                    }
                }
                    
                
            }
            catch { }
        }

      
    }
}
