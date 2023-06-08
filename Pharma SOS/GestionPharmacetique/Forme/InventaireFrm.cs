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

        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        private void InventaireFrm_Load(object sender, EventArgs e)
        {
            try
            {  
                button3.Location = new Point(Width - 40, button3.Location.Y);
                //radioButton1.Checked = true;
                label1.Text = "Inventaire du " + DateTime.Now.ToString();
                Column2.Width = dgvProduit.Width / 3;
                button4.Location = new Point(Width - 120, 7);
                btnsupexamen.Location = new Point(Width - 80, 7);
        
                ListeInventaire();
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

        void ListeInventaire()
        {
            try
            {
                dgvProduit.Rows.Clear();
                var listeGroupe = ConnectionClass.ListeGroupe();
                var totalAchatGeneral = 0m;
                var totalVenteGeneral = 0m;
                if (checkBox1.Checked)
                {
                    foreach (var g in listeGroupe)
                    {

                        var totalAchatParGroupe = 0m;
                        var totalVenteParGroupe = 0m;
                        var listeFamille = from f in ConnectionClass.ListeDesFamille()
                                           join gr in ConnectionClass.ListeGroupe()
                                           on f.NombreDetail equals g.CodeFamille
                                           where gr.Groupe.ToUpper() == g.Groupe.ToUpper()
                                           select new
                                           {
                                               f.Designation
                                           };
                        var l = ConnectionClass.ListeDesMedicamentsRechercherParGroupeQuantiteSupZero(g.Groupe);
                        if (l.Count > 0)
                        {
                            dgvProduit.Rows.Add(
                                         g.Groupe, "", "", "", "", "", "", ""
                                         );
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;

                            foreach (var p in listeFamille)
                            {

                                var ll = ConnectionClass.ListeDesMedicamentsRechercherParFamilleQuantiteSupZero(p.Designation);
                                if (ll.Count > 0)
                                {
                                    dgvProduit.Rows.Add(
                                                p.Designation, "", "", "", "", "", "", ""
                                                );
                                    dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                                    dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                                    var totalAchat = 0m;
                                    var totalVente = 0m;
                                    foreach (var produit in ll)
                                    {
                                        var quantite = 0;
                                        if (comboBox2.Text == "Pharmacie de cession")
                                        {
                                            quantite = produit.Quantite;
                                            Column3.Visible = true;
                                            Column4.Visible = false;
                                            Column5.Visible = false;
                                        }
                                        else if (comboBox2.Text == "Grand depot")
                                        {
                                            quantite = produit.GrandStock;
                                            Column3.Visible = false;
                                            Column4.Visible = true;
                                            Column5.Visible = false;
                                        }
                                        else
                                        {
                                            Column3.Visible = true;
                                            Column4.Visible = true;
                                            Column5.Visible = true;
                                            quantite = produit.GrandStock + produit.Quantite;
                                        }
                                        totalAchat += quantite * produit.PrixAchat;
                                        totalVente += quantite * produit.PrixVente;
                                        totalAchatGeneral += quantite * produit.PrixAchat;
                                        totalVenteGeneral += quantite * produit.PrixVente;
                                        totalAchatParGroupe += quantite * produit.PrixAchat; ;
                                        totalVenteParGroupe += quantite * produit.PrixVente; ;
                                        if (quantite > 0)
                                            dgvProduit.Rows.Add(
                                           produit.NomMedicament.ToUpper(), produit.Quantite,
                                            produit.GrandStock, (produit.Quantite + produit.GrandStock), produit.PrixAchat, produit.PrixVente,
                                         produit.PrixAchat * quantite, produit.PrixVente * quantite
                                           );
                                    }
                                    dgvProduit.Rows.Add("TOTAL " + p.Designation, "", "", "", "", "", string.Format(elGR, "{0:0,0}", totalAchat), string.Format(elGR, "{0:0,0}", totalVente));
                                }

                            }
                            dgvProduit.Rows.Add("TOTAL " + g.Groupe, "", "", "", "", "", string.Format(elGR, "{0:0,0}", totalAchatParGroupe), string.Format(elGR, "{0:0,0}", totalVenteParGroupe));
                            //dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                            //dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                    dgvProduit.Rows.Add("TOTAL GENERAL", "", "", "", "", "", string.Format(elGR, "{0:0,0}", totalAchatGeneral), string.Format(elGR, "{0:0,0}", totalVenteGeneral));
                }
                else
                {
                    foreach (var g in listeGroupe)
                    {

                        var totalAchatParGroupe = 0m;
                        var totalVenteParGroupe = 0m;
                        var listeFamille = from f in ConnectionClass.ListeDesFamille()
                                           join gr in ConnectionClass.ListeGroupe()
                                           on f.NombreDetail equals g.CodeFamille
                                           where gr.Groupe.ToUpper() == g.Groupe.ToUpper()
                                           select new
                                           {
                                               f.Designation
                                           };
                        var l = ConnectionClass.ListeDesMedicamentsRechercherParGroupe(g.Groupe);
                        if (l.Count > 0)
                        {
                            dgvProduit.Rows.Add(
                                         g.Groupe, "", "", "", "", "", "", ""
                                         );
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                            foreach (var p in listeFamille)
                            {

                                var ll = ConnectionClass.ListeDesMedicamentsRechercherParFamille(p.Designation);
                                if (ll.Count() > 0)
                                {
                                    dgvProduit.Rows.Add(
                                                p.Designation, "", "", "", "", "", "", ""
                                                );
                                    dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                                    dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                                    var totalAchat = 0m;
                                    var totalVente = 0m;
                                    foreach (var produit in ll)
                                    {
                                        var quantite = 0;
                                        if (comboBox2.Text == "Pharmacie de cession")
                                        {
                                            quantite = produit.Quantite;
                                            Column3.Visible = true;
                                            Column4.Visible = false;
                                            Column5.Visible = false;
                                        }
                                        else if (comboBox2.Text == "Grand depot")
                                        {
                                            quantite = produit.GrandStock;
                                            Column3.Visible = false;
                                            Column4.Visible = true;
                                            Column5.Visible = false;
                                        }
                                        else
                                        {
                                            Column3.Visible = true;
                                            Column4.Visible = true;
                                            Column5.Visible = true;
                                            quantite = produit.GrandStock + produit.Quantite;
                                        }
                                        totalAchat += quantite * produit.PrixAchat;
                                        totalVente += quantite * produit.PrixVente;
                                        totalAchatGeneral += quantite * produit.PrixAchat;
                                        totalVenteGeneral += quantite * produit.PrixVente;
                                        totalAchatParGroupe += quantite * produit.PrixAchat;
                                        totalVenteParGroupe += quantite * produit.PrixVente;
                                        dgvProduit.Rows.Add(
                                       produit.NomMedicament.ToUpper(), produit.Quantite,
                                        produit.GrandStock, (produit.Quantite + produit.GrandStock), produit.PrixAchat, produit.PrixVente,
                                     produit.PrixAchat * quantite, produit.PrixVente * quantite
                                       );
                                    }
                                    dgvProduit.Rows.Add("TOTAL " + p.Designation, "", "", "", "", "", string.Format(elGR, "{0:0,0}", totalAchat), string.Format(elGR, "{0:0,0}", totalVente));
                                }

                            }
                            dgvProduit.Rows.Add("TOTAL " + g.Groupe, "", "", "", "", "", string.Format(elGR, "{0:0,0}", totalAchatParGroupe), string.Format(elGR, "{0:0,0}", totalVenteParGroupe));
                            //dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                            //dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                    dgvProduit.Rows.Add("TOTAL GENERAL", "", "", "", "", "", string.Format(elGR, "{0:0,0}",totalAchatGeneral),string.Format(elGR, "{0:0,0}", totalVenteGeneral));
                }
               var ppp= dgvProduit.Rows.Count;
            }
            catch (Exception)
            {
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ListeInventaire();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeInventaire();
        }

        private void btnsupexamen_Click(object sender, EventArgs e)
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
            
                var titre = "Inventaire de stock ";
                if (comboBox2.Text == "Pharmacie de cession")
                {
                     titre = "Inventaire de stock de la pharmacie de cession du ";
                }
                else  if (comboBox2.Text == "Pharmacie de cession")
                {
                    titre = "Inventaire de stock du grand depot ";
                
                }
                sfd.FileName = titre +" " + date + ".pdf";
          titre = titre + " " +DateTime.Now.Date.ToShortDateString();
                if (dgvProduit.Rows.Count > 0)
                {
                    var Count = dgvProduit.Rows.Count / 45;
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        for (var i = 0; i <= Count; i++)
                        {
                            if (i * 45 < dgvProduit.Rows.Count)
                            {
                _inventaireStock = AppCode.Imprimer.ImprimerInventaireStockAnnuel(titre, dgvProduit, i,comboBox2.Text);

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
    
        private void button4_Click(object sender, EventArgs e)
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
                    ToCsV1(dgvProduit, sfd.FileName); // Here dataGridview1 is your grid view name
                }
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception)
            {
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

   
    }
}
