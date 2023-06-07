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

                Column6.Width = dgvRapport.Width / 2;
                dgvRapport.Rows.Clear();
                button1.Location = new Point(Width - 270, button1.Location.Y);
                button2.Location = new Point(Width - 123, button2.Location.Y);
                button3.Location = new Point(Width - 40, button3.Location.Y);
                dataGridView1.Visible = false;

                dataGridViewTextBoxColumn3.Width = dataGridView1.Width / 3;
                Column8.Width = dataGridView1.Width / 3;
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
                                   string.Format(elGR, "{0:0,0}", montant)
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
                                   string.Format(elGR, "{0:0,0}", montant)
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
                                   string.Format(elGR, "{0:0,0}", montant)
                                );
                           
                            }
                        }

                        if (listeLivraison.Rows.Count > 0)
                        {
                            dgvRapport.Rows.Add
                            (
                            "SOUS TOTAL",
                            "", "",
                             String.Format(elGR, "{0:0,0}", sousTotalNetAPayer)
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
                               string.Format( elGR, "{0:0,0}",montant)
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
                               string.Format( elGR, "{0:0,0}",montant)
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
                               string.Format( elGR, "{0:0,0}",montant)
                            );
                        }
                    }

                    if (listeLivraison.Rows.Count > 0)
                    {
                        dgvRapport.Rows.Add
                        (
                        "SOUS TOTAL",
                        "", "",
                         String.Format(elGR, "{0:0,0}", sousTotalNetAPayer)
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

                if (dgvRapport.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    string  titre;
                    if (comboBox1.Text == "<TOUS LES FOURNISSEURS>")
                    {
                        titre = "Etat des livraisons des fournisseurs du" + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                    }
                    else
                    {
                        titre = "Etat des livraisons du fournisseur" +comboBox1.Text +" du "  + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
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
                                var _listeImpression = ImprimerRaportVente.ImprimerJournalDesLivraisons(dgvRapport,titre, label5.Text, i);

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
            catch { }
        }

        void RapportDetaille()
        {
            try
            {

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
                            sousTotalNetAPayer += montant;
                            dgvRapport.Rows.Add
                                (
                                   fournisseur,
                                   listeLivraison.Rows[0].ItemArray[0].ToString(),
                                   DateTime.Parse(listeLivraison.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                   string.Format(elGR, "{0:0,0}", montant)
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
                            sousTotalNetAPayer += montant;
                            dgvRapport.Rows.Add
                                (
                                   fournisseur,
                                   listeLivraison.Rows[0].ItemArray[0].ToString(),
                                   DateTime.Parse(listeLivraison.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                   string.Format(elGR, "{0:0,0}", montant)
                                );

                            for (var i = 1; i < listeLivraison.Rows.Count; i++)
                            {
                                detailLivraison = ConnectionClass.RapportDeLivraison(listeLivraison.Rows[i].ItemArray[0].ToString());
                                montant = 0.0;
                                for (var j = 0; j < detailLivraison.Rows.Count; j++)
                                {
                                    montant += double.Parse(detailLivraison.Rows[j].ItemArray[3].ToString());
                                }
                                sousTotalNetAPayer += montant;
                                dgvRapport.Rows.Add
                                (
                                   "",
                                   listeLivraison.Rows[i].ItemArray[0].ToString(),
                                   DateTime.Parse(listeLivraison.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                                   string.Format(elGR, "{0:0,0}", montant)
                                );

                            }
                        }

                        if (listeLivraison.Rows.Count > 0)
                        {
                            dgvRapport.Rows.Add
                            (
                            "SOUS TOTAL",
                            "", "",
                             String.Format(elGR, "{0:0,0}", sousTotalNetAPayer)
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
                    #region ClientSelectionne
                    
                    var sousTotalNetAPayer = 0.0;
                    nomFournisseur = comboBox1.Text;
                    var listeLivraison = ConnectionClass.RapportDesLivraisonsParMedicament(nomFournisseur, dateDebut, dateFin);

                    if (listeLivraison.Rows.Count == 1)
                    {
                        //var montant = 0.0;

                        foreach (DataRow dtRow in listeLivraison.Rows)
                        {
                            dataGridView1.Rows.Add
                                (
                                   nomFournisseur,
                                   dtRow.ItemArray[0].ToString(),
                                   dtRow.ItemArray[1].ToString(),
                                   string.Format(elGR, "{0:0,0}", double.Parse(dtRow.ItemArray[2].ToString())),
                                   string.Format(elGR, "{0:0,0}", double.Parse(dtRow.ItemArray[3].ToString())),
                                   string.Format(elGR, "{0:0,0}", double.Parse(dtRow.ItemArray[4].ToString()))
                           
                                );
                        }
                    }
                    else if (listeLivraison.Rows.Count > 1)
                    {
                        
                        //sousTotalNetAPayer += montant;
                        dataGridView1.Rows.Add
                            (
                                 nomFournisseur,
                                   listeLivraison.Rows[0].ItemArray[0].ToString(),
                                   listeLivraison.Rows[0].ItemArray[1].ToString(),
                                   string.Format(elGR, "{0:0,0}", double.Parse(listeLivraison.Rows[0].ItemArray[2].ToString())),
                                   string.Format(elGR, "{0:0,0}", double.Parse(listeLivraison.Rows[0].ItemArray[3].ToString())),
                                   string.Format(elGR, "{0:0,0}", double.Parse(listeLivraison.Rows[0].ItemArray[4].ToString()))
                           
                            );

                        for (var i = 1; i < listeLivraison.Rows.Count; i++)
                        {
                            listeLivraison = ConnectionClass.RapportDeLivraison(listeLivraison.Rows[i].ItemArray[0].ToString());
                            var montant = 0.0;
                            for (var j = 0; j < listeLivraison.Rows.Count; j++)
                            {
                                montant += double.Parse(listeLivraison.Rows[j].ItemArray[3].ToString());
                            }
                            
                            sousTotalNetAPayer += montant;
                            dataGridView1.Rows.Add
                            (
                               "",
                                   listeLivraison.Rows[i].ItemArray[0].ToString(),
                                   listeLivraison.Rows[i].ItemArray[1].ToString(),
                                   string.Format(elGR, "{0:0,0}", double.Parse(listeLivraison.Rows[i].ItemArray[2].ToString())),
                                   string.Format(elGR, "{0:0,0}", double.Parse(listeLivraison.Rows[i].ItemArray[3].ToString())),
                           string.Format(elGR, "{0:0,0}", double.Parse(listeLivraison.Rows[i].ItemArray[4].ToString()))
                           
                            );
                        }
                    }

                    if (listeLivraison.Rows.Count > 0)
                    {
                        dgvRapport.Rows.Add
                        (
                        "SOUS TOTAL",
                        "", "",
                         String.Format(elGR, "{0:0,0}", sousTotalNetAPayer)
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
                    #endregion

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
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("Rapprot detaille", ex); }
        }
    }
}
