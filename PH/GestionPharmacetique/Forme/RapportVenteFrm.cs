using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;
using System.Linq;

namespace GestionPharmacetique.Forme
{
    public partial class RapportVenteFrm : Form
    {
        public RapportVenteFrm()
        {
            InitializeComponent();
        }

        private void RapportVenteFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkOrange, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public DateTime dateDebut, dateFin; public bool siVenteTotal;
        private void RapportVenteFrm_Load(object sender, EventArgs e)
        {
            try
            {
                button3.Location = new Point(Width - 43, 4);
                cl1.Width = dataGridView1.Width / 2 ;
                cl2.Width = dataGridView1.Width / 6;
                cl3.Width = dataGridView1.Width / 6;
                cl4.Width = dataGridView1.Width / 6;
            var  listeEMploye = from f in ConnectionClass.ListeDesEmployees()
                                                 orderby f.NomEmployee
                                                 select f;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("<Tous les caissiers>");
                foreach (Employe empl in listeEMploye)
                {
                    comboBox2.Items.Add(empl.NomEmployee.ToUpper());
                }
                button1_Click(null, null);
            }
            catch { }
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

                //textBox2.Text = string.Format(elGR, "{0:0,0}", 
        private void ListeDesRapportsVentes(DataTable dataTable)
        {
            try
            {
                var Montant = 0.0;
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var total = double.Parse(dataTable.Rows[i].ItemArray[1].ToString()) *
                        Int32.Parse(dataTable.Rows[i].ItemArray[2].ToString());
                    dataGridView1.Rows.Add(
                        dataTable.Rows[i].ItemArray[0].ToString().ToUpper(),
                        string.Format(elGR, "{0:0,0}", double.Parse(dataTable.Rows[i].ItemArray[1].ToString())),
                         string.Format(elGR, "{0:0,0}", double.Parse(dataTable.Rows[i].ItemArray[2].ToString())),
                        string.Format(elGR, "{0:0,0}",  total)
                    );

                   
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    Montant += double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                }
                borderLabel2.Text = "Montant total de " +  string.Format(elGR, "{0:0,0}",Montant) + " F CFA";
                
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (siVenteTotal)
                {
                    cl1.HeaderText = "DATE";
                    cl2.HeaderText = "MONTANT TOTAL";
                    cl3.Visible = false;
                    cl4.Visible = false;
                    dataGridView1.Rows.Clear();
                    var listeJour = new List<DateTime>();
                    var day = dateFin.DayOfYear - dateDebut.DayOfYear;
                    for (var i = 0; i <= day; i++)
                    {
                        listeJour.Add(dateDebut.AddDays(i));
                    }

                    var Montant = 0.0;
                    foreach (var date in listeJour)
                    {
                        var total = 0.0;
                        DataTable dt = ConnectionClass.ListeDesVentes("", date, date.AddHours(24));

                        foreach (DataRow dtRow in dt.Rows)
                        {
                            total += double.Parse(dtRow.ItemArray[1].ToString()) *
                        Int32.Parse(dtRow.ItemArray[2].ToString());
                        }
                        dataGridView1.Rows.Add(date.ToShortDateString(),  string.Format(elGR, "{0:0,0}",total));
                    }
                    for (var i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        Montant += double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()); ;
                    }
                    borderLabel2.Text = "Montant total de " +  string.Format(elGR, "{0:0,0}",Montant) + " CFA";
                }
                else
                {
                    string caissier;
                    if (comboBox2.Text=="<Tous les caissiers>")
                    {
                        caissier="";
                    }
                    else
                    {
                        caissier = comboBox2.Text;
                    }
                    cl1.HeaderText = "DESIGNATION";
                    cl2.HeaderText = "PRIX VENTE";
                    cl3.Visible = true ;
                    cl4.Visible = true;
                    DataTable dataTable = ConnectionClass.ListeDesVentes(caissier, dateDebut, dateFin.Date.AddHours(24));
                    ListeDesRapportsVentes(dataTable);
                }
            }

            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
            }

              
            
        }

        private Bitmap RapportVente;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(RapportVente, 5, 10, RapportVente.Width, RapportVente.Height);
            e.HasMorePages = false;
        }
        
        //imprimer rapport journalier
        private void ImprimerRapportJournalier()
        {
            try
            {
                //var titre = "Inventaire des stocks de produit " + DateTime.Now;
                //if (!string.IsNullOrEmpty(comboBox1.Text))
                //{
                //    titre =  comboBox1.Text + DateTime.Now;
                //}
                ////les dimension de la facture
                //if (dataGridView1.Rows.Count > 0)
                //{
                //    var Count = dataGridView1.Rows.Count / 40;

                //    if (printDialog1.ShowDialog() == DialogResult.OK)
                //    {
                //        printDocument1.PrinterSettings = printDialog1.PrinterSettings;

                //    }
                //    for (var i = 0; i <= Count; i++)
                //    {
                //        if (i * 40 < dataGridView1.Rows.Count)
                //        {
                //            RapportVente = ImprimerRaportVente.ImprimerRapportDesVentes(titre, dataGridView1, i);
                //            printDocument1.Print();
                //        }
                //    }
                //} 
                
            }
            catch { }
        }

        //impression
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(siVenteTotal)
                {
                    if (dataGridView1.Rows.Count > 0)
                    {   SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";
                                               
                        var titre = "Vente totale journalier du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
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
                        { s=s.Replace("/","_");}
                        sfd.FileName =s+ "_imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                            var count = dataGridView1.Rows.Count;
                            
                                var index = (dataGridView1.Rows.Count) / 45;

                                for (var i = 0; i <= index; i++)
                                {
                                    if (i * 45 < count)
                                    {
                                        var _listeImpression = ImprimerRaportVente.ImprimerRapportDesVentesParJour(titre,dataGridView1, i);

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
                else
                {
                    var titre = "";
                    if (comboBox2.Text=="<Tous les caissiers>" && dateFin ==dateDebut)
                    {
                        titre = "Rapport de vente journaliere du " + dateDebut.ToShortDateString();
                    }
                    else if (comboBox2.Text == "<Tous les caissiers>" && dateFin > dateDebut)
                    {
                        titre = "Rapport de vente du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                    }
                    else 
                    {
                        if (dateDebut == dateFin)
                        {
                            titre = "Rapport de vente journaliere de " + comboBox2.Text + " du " + dateDebut.ToShortDateString();
                        }
                        else
                        {
                            titre = "Rapport de vente de " + comboBox2.Text + " du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                        }
                    }
                   

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
                        { s=s.Replace("/","_");}
                        sfd.FileName =s+ "_imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                            var count = dataGridView1.Rows.Count;
                            
                                var index = (dataGridView1.Rows.Count) / 45;

                                for (var i = 0; i <= index; i++)
                                {
                                    if (i * 45 < count)
                                    {
                                        var _listeImpression = ImprimerRaportVente.ImprimerRapportDesVentes(titre,dataGridView1, i);

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
            }
            catch (Exception )
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (siVenteTotal)
            {
                cl1.Width = dataGridView1.Width / 2;
                cl2.Width = dataGridView1.Width / 2;
                cl1.HeaderText = "DATE";
                cl2.HeaderText = "TOTAL";
                cl3.Visible = false;
                cl4.Visible = false;
            }
            else
            {
                cl1.Width = dataGridView1.Width / 2;
                cl2.Width = dataGridView1.Width / 6;
                cl3.Width = dataGridView1.Width / 6;
                cl4.Width = dataGridView1.Width / 6;
                cl3.Visible = true;
                cl4.Visible = true;
                cl1.HeaderText = "DESIGNATION";
                cl2.HeaderText = "PRIX VENTE";
                cl3.HeaderText = "QUANTITE";
                cl4.HeaderText = "PRIX TOTAL";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SGDP.Formes.DateFrm.state = true;
                if (SGDP.Formes.DateFrm.ShowBox())
                {    dateDebut = SGDP.Formes.DateFrm.dateDebut;
                     dateFin = SGDP.Formes.DateFrm.dateFin;
                    siVenteTotal = SGDP.Formes.DateFrm.checkVenteTotal;
                    button1_Click(null, null);
                }
            }
            catch { }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
       
    }
}
