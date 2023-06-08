using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace SGDP.Formes
{
    public partial class JournalDesLivraisonsFrm : Form
    {
        public JournalDesLivraisonsFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue
                , Color.SlateGray, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue
                , Color.SlateGray, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void JournalDesLivraisonsFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.White
                , SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        public DateTime dateDebut, dateFin;
        public string nomFournisseur;
        public bool siTousLesFournisseur;
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        private void JournalDesLivraisonsFrm_Load(object sender, EventArgs e)
        {
            try
            {

                var listeFournisseurs = from l in ConnectionClass.ListeFournisseur()
                                        orderby l.NomFournisseur
                                        select l;
                comboBox1.Items.Add("<Tous les fournisseurs>".ToUpper());
                foreach (var fournisseur in listeFournisseurs)
                {
                    comboBox1.Items.Add(fournisseur.NomFournisseur.ToUpper());
                }
                button4.Location = new Point(Width - 80, 4);
                Column6.Width = dgvRapport.Width / 2;
                dgvRapport.Rows.Clear();
                button1.Location = new Point(Width - 270, button1.Location.Y);
                button2.Location = new Point(Width - 123, button2.Location.Y);
                button3.Location = new Point(Width - 40, button3.Location.Y);
                dataGridView1.Visible = false;

                dataGridViewTextBoxColumn3.Width = dataGridView1.Width / 3;
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Plus detaillé")
                {
                    RapportDetaille();
                    dataGridView1.Visible = true;
                    dgvRapport.Visible = false;
                    button1.Text = "Moins detaillé";
                }
                else if (button1.Text == "Moins detaillé")
                {

                    dgvRapport.Visible = true;
                    dataGridView1.Visible = false;
                    button1.Text = "Plus detaillé";
                }
            }
            catch (Exception )
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
try{
   
    dgvRapport.Rows.Clear();
                    
                    #region TousLesFournisseurs
                    if (comboBox1.Text == "<Tous les fournisseurs>".ToUpper())
                    {
                        var listeFournisseur = from cl in ConnectionClass.ListeFournisseur()
                                               orderby cl.NomFournisseur
                                               select cl.NomFournisseur.ToUpper();
                    foreach (var fournisseur in listeFournisseur)
                    {
                        var sousTotalNetAPayer = 0.0;
                        nomFournisseur = comboBox1.Text;
                        var listeLivraison = ConnectionClass.JournalDesLivraisons(dateDebut, dateFin, fournisseur);

                        if (listeLivraison.Rows.Count == 1)
                        {
                            var detailLivraison = ConnectionClass.RapportDeLivraison(listeLivraison.Rows[0].ItemArray[0].ToString());
                            var montant = 0.0;
                            for (var j = 0; j < detailLivraison.Rows.Count; j++)
                            {
                                montant += double.Parse(detailLivraison.Rows[j].ItemArray[3].ToString());
                            }
                            double autre;
                            if (double.TryParse(listeLivraison.Rows[0].ItemArray[4].ToString(), out autre))
                            {
                                montant = montant + double.Parse(listeLivraison.Rows[0].ItemArray[4].ToString());
                            }
                            sousTotalNetAPayer += montant;
                            dgvRapport.Rows.Add
                                (
                                   fournisseur,
                                   DateTime.Parse(listeLivraison.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                   listeLivraison.Rows[0].ItemArray[0].ToString(),
                                    montant
                                );
                        }
                        else if (listeLivraison.Rows.Count > 1)
                        {
                            var detailLivraison = ConnectionClass.RapportDeLivraison(listeLivraison.Rows[0].ItemArray[0].ToString());
                            var montant = 0.0;
                            for (var j = 0; j < detailLivraison.Rows.Count; j++)
                            {
                                montant += double.Parse(detailLivraison.Rows[j].ItemArray[3].ToString());
                            }
                           montant = montant  +double.Parse(listeLivraison.Rows[0].ItemArray[3].ToString());
                            sousTotalNetAPayer += montant;
                            dgvRapport.Rows.Add
                                (
                                   fournisseur,
                                    DateTime.Parse(listeLivraison.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                  listeLivraison.Rows[0].ItemArray[0].ToString(),
                                    montant
                                );

                            for (var i = 1; i < listeLivraison.Rows.Count; i++)
                            {
                                detailLivraison = ConnectionClass.RapportDeLivraison(listeLivraison.Rows[i].ItemArray[0].ToString());
                                montant = 0.0;
                                for (var j = 0; j < detailLivraison.Rows.Count; j++)
                                {
                                    montant += double.Parse(detailLivraison.Rows[j].ItemArray[3].ToString());
                                }
                                  double autre;
                                  if (double.TryParse(listeLivraison.Rows[0].ItemArray[4].ToString(), out autre))
                                  {
                                      montant = montant + double.Parse(listeLivraison.Rows[i].ItemArray[3].ToString());
                                  }
                                sousTotalNetAPayer += montant;
                                dgvRapport.Rows.Add
                                (
                                   "",
                                    DateTime.Parse(listeLivraison.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                                  listeLivraison.Rows[i].ItemArray[0].ToString(),
                                    montant
                                );
                           
                            }
                        }

                        if (listeLivraison.Rows.Count > 0)
                        {
                            dgvRapport.Rows.Add
                            (
                            "SOUS TOTAL",
                            "", "",
                             sousTotalNetAPayer
                            );
                            dgvRapport.Rows.Add
                            (
                            "",
                            "",
                            "",
                            ""
                            );
                        }


                    }
                    dgvRapport.Rows.Remove(dgvRapport.Rows[dgvRapport.Rows.Count - 1]);
                        /***Fin tous les fournisseur****/
#endregion
                }
                else //s'il nya qun seul client selectionne
                {
                    var sousTotalNetAPayer = 0.0;
                    nomFournisseur = comboBox1.Text;
                    var listeLivraison = ConnectionClass.JournalDesLivraisons(dateDebut, dateFin, nomFournisseur);

                    if (listeLivraison.Rows.Count == 1)
                    {
                        var detailLivraison = ConnectionClass.RapportDeLivraison(listeLivraison.Rows[0].ItemArray[0].ToString());
                        var montant = 0.0;
                        for (var j = 0; j < detailLivraison.Rows.Count; j++)
                        {
                            montant += double.Parse(detailLivraison.Rows[j].ItemArray[3].ToString());
                        }
                        montant = montant + double.Parse(listeLivraison.Rows[0].ItemArray[4].ToString());
                        sousTotalNetAPayer += montant;
                        dgvRapport.Rows.Add
                            (
                               nomFournisseur,
                                DateTime.Parse(listeLivraison.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                              listeLivraison.Rows[0].ItemArray[0].ToString(),
                               montant
                            );
                    }
                    else if (listeLivraison.Rows.Count > 1)
                    {
                       var detailLivraison = ConnectionClass.RapportDeLivraison(listeLivraison.Rows[0].ItemArray[0].ToString());
                       var montant = 0.0;
                       for (var j = 0; j < detailLivraison.Rows.Count; j++)
                       {
                           montant += double.Parse(detailLivraison.Rows[j].ItemArray[3].ToString());
                       }
                          double autre;
                          if (double.TryParse(listeLivraison.Rows[0].ItemArray[4].ToString(), out autre))
                          {
                              montant = montant + double.Parse(listeLivraison.Rows[0].ItemArray[4].ToString());
                          }
                       sousTotalNetAPayer += montant;
                        dgvRapport.Rows.Add
                            (
                               nomFournisseur,
                                 DateTime.Parse(listeLivraison.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                             listeLivraison.Rows[0].ItemArray[0].ToString(),
                               montant
                            );

                        for (var i = 1; i < listeLivraison.Rows.Count; i++)
                        {
                            detailLivraison = ConnectionClass.RapportDeLivraison(listeLivraison.Rows[i].ItemArray[0].ToString());
                             montant = 0.0;
                            for (var j = 0; j < detailLivraison.Rows.Count; j++)
                            {
                                montant += double.Parse(detailLivraison.Rows[j].ItemArray[3].ToString());                               
                            }
                               
                              if (double.TryParse(listeLivraison.Rows[0].ItemArray[4].ToString(), out autre))
                              {
                                  montant = montant + double.Parse(listeLivraison.Rows[i].ItemArray[4].ToString());
                              }
                            sousTotalNetAPayer += montant;
                            dgvRapport.Rows.Add
                            (
                               "",
                              DateTime.Parse(listeLivraison.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                                listeLivraison.Rows[i].ItemArray[0].ToString(),
                               montant
                            );
                        }
                    }

                    if (listeLivraison.Rows.Count > 0)
                    {
                        dgvRapport.Rows.Add
                        (
                        "SOUS TOTAL",
                        "", "",
                          sousTotalNetAPayer
                        );
                        dgvRapport.Rows.Add
                        (
                        "",
                        "",
                        "",
                        ""
                        );
                    }
                    dgvRapport.Rows.Remove(dgvRapport.Rows[dgvRapport.Rows.Count - 1]);
                }


                var totalRapport = 0.0;

                foreach (DataGridViewRow row in dgvRapport.Rows)
                {
                    double total;
                    


                    if (row.Cells[0].Value.ToString() == "SOUS TOTAL")
                    {
                        if (double.TryParse(row.Cells[3].Value.ToString(), out total))
                            totalRapport += total;
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }

                label5.Text = String.Format(elGR, "{0:0,0}", (totalRapport));
            }
            catch (Exception ex) {GestionPharmacetique. MonMessageBox.ShowBox("Form load", ex); }
        }

        Bitmap _listeImpression;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(_listeImpression, 0, 20, _listeImpression.Width, _listeImpression.Height);
                e.HasMorePages = false;
            }
            catch { }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRapport.Visible)
                {
                    if (dgvRapport.Rows.Count > 0)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        string titre;
                        if (comboBox1.Text == "<TOUS LES FOURNISSEURS>")
                        {
                            titre = "Etat des livraisons des fournisseurs du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                        }
                        else
                        {
                            titre = "Etat des livraisons du fournisseur " + comboBox1.Text + " du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                        }

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
                        var s = titre;
                        if (titre.Contains("/"))
                        { s = s.Replace("/", "_"); }
                        sfd.FileName = s + "_imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                            var count = dgvRapport.Rows.Count;

                            var index = (dgvRapport.Rows.Count) / 45;

                            for (var i = 0; i <= index; i++)
                            {
                                if (i * 45 < count)
                                {
                                    var _listeImpression = ImprimerRaportVente.ImprimerJournalDesLivraisons(dgvRapport, titre, label5.Text, i);

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
                else if (dataGridView1.Visible)
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
                        string titre;
                        if (comboBox1.Text == "<TOUS LES FOURNISSEURS>")
                        {
                            titre = "Details des livraisons des fournisseurs du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                        }
                        else
                        {
                            titre = "Details des livraisons du fournisseur " + comboBox1.Text + " du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                        }
                        var s = titre;
                        if (titre.Contains("/"))
                        { s = s.Replace("/", "_"); }
                        sfd.FileName = s + "_imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                            var count = dataGridView1.Rows.Count;

                            var index = (dataGridView1.Rows.Count) / 45;

                            for (var i = 0; i <= index; i++)
                            {
                                if (i * 45 < count)
                                {
                                    var _listeImpression = ImprimerRaportVente.ImprimerRapportDesVentes(titre, dataGridView1, i, double.Parse(label5.Text));

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
            catch { }
        }

        void RapportDetaille()
        {
            try
            {

                dgvRapport.Rows.Clear();

                
                var totalRapport = .0;
                if (comboBox1.Text == "<Tous les fournisseurs>".ToUpper())
                {
                    var listeFournisseurs = from l in ConnectionClass.ListeFournisseur()
                                            orderby l.NomFournisseur
                                            select l;
                    foreach (var fournisseur in listeFournisseurs)
                    {
                        var totalParFournissuer = .0;
                        var dt = ConnectionClass.RapportDeLivraisonGroupe(fournisseur.NomFournisseur);
                        if (dt.Rows.Count > 0)
                        {
                            dataGridView1.Rows.Add(fournisseur.NomFournisseur,
                                    "", "", "");
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            for (var i = 0; i < dt.Rows.Count; i++)
                            {
                                dataGridView1.Rows.Add(dt.Rows[i].ItemArray[0].ToString(),
                                    dt.Rows[i].ItemArray[1].ToString(),
                                    dt.Rows[i].ItemArray[2].ToString(),
                                    dt.Rows[i].ItemArray[3].ToString());
                                totalRapport += double.Parse(dt.Rows[i].ItemArray[3].ToString());
                                totalParFournissuer += double.Parse(dt.Rows[i].ItemArray[3].ToString());
                            }
                            dataGridView1.Rows.Add("TOTAL " + fournisseur.NomFournisseur, "", "", totalParFournissuer);
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                           
                        }
                    }
                }
                else
                {
                    
                    var dt = ConnectionClass.RapportDeLivraisonGroupe(comboBox1.Text);
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(dt.Rows[i].ItemArray[0].ToString(),
                            dt.Rows[i].ItemArray[1].ToString(),
                            dt.Rows[i].ItemArray[2].ToString(),
                            dt.Rows[i].ItemArray[3].ToString());
                        totalRapport += double.Parse(dt.Rows[i].ItemArray[3].ToString());
                    }
                }
                
                label5.Text = String.Format(elGR, "{0:0,0}", (totalRapport));
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("Rapprot detaille", ex); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvRapport.Visible)
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
                    sfd.FileName = "Inventaire de stock  " + date + ".xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ToCsV1(dgvRapport, sfd.FileName); // Here dataGridview1 is your grid view name
                    }
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
            else if (dataGridView1.Visible)
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
                    sfd.FileName = "Inventaire de stock  " + date + ".xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ToCsV1(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                    }
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception)
                {
                }
            }
        }

        
        
        private void ToCsV1(DataGridView dGV, string filename)
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
    }
}
