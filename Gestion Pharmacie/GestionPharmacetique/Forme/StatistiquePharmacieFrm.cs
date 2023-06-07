using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel; 

namespace GestionPharmacetique.Forme
{
    public partial class StatistiquePharmacieFrm : Form
    {
        public StatistiquePharmacieFrm()
        {
            InitializeComponent();
        }

        public DateTime dateDebut, dateFin;
        private void StatistiqueFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
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
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void StatistiqueFrm_Load(object sender, EventArgs e)
        {
            try
            {
                Column2.Width = dataGridView1.Width / 2;
              comboBox2.Text = "Pharmacie";
                comboBox2_SelectedIndexChanged(null, null);
                button4.Location = new Point(Width - 120, 7);
                btnsupexamen.Location = new Point(Width - 80, 7);
                button2.Location = new Point(Width - 40, 7);

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
            }
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
      
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (comboBox2.Text == "Pharmacie")
                {
                    if (checkBox3.Checked)
                    {
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                        var liste = from p in listeProduit()
                                    orderby p.Designation ascending
                                    select p;
                        dataGridView1.Rows.Clear();
                        foreach (var p in liste)
                        {
                            dataGridView1.Rows.Add(
                           p.Designation,
                               p.Quantite.ToString(),
                                p.NombreVente.ToString(),
                                p.PrixVente.ToString(),
                               p.PrixTotal.ToString(),
                               p.NumeroMedicament.ToString());
                        }
                    }
                }
                else
                {
                    if (checkBox2.Checked)
                    {
                        checkBox1.Checked = false;
                        checkBox3.Checked = false;
                        var produit = from l in ListeDepot()
                                      orderby l.Designation ascending
                                      select l;
                        dataGridView1.Rows.Clear();
                        foreach (var l in produit)
                        {
                            dataGridView1.Rows.Add(
                                l.Designation.ToUpper(),
                               l.GrandStock.ToString(),
                                l.DifferenceStock.ToString(),
                                l.PrixAchat.ToString(),
                               (l.PrixAchat * l.DifferenceStock).ToString(),
                               l.Description);
                        }
                    }
                }
            }
            catch { }
         
        }

        List<AppCode.Vente> listeProduit()
        {
            var listeItems = new List<AppCode.Vente>();
            foreach (DataGridViewRow dgvRows in dataGridView1.Rows)
            {
                var produit = new AppCode.Vente();
                produit.Designation = dgvRows.Cells[0].Value.ToString();
                produit.Quantite = Convert.ToInt32(dgvRows.Cells[1].Value.ToString());
                produit.PrixVente = Convert.ToDecimal(dgvRows.Cells[3].Value.ToString());
                produit.NombreVente = Convert.ToInt32(dgvRows.Cells[2].Value.ToString());
                produit.PrixTotal = Convert.ToDecimal(dgvRows.Cells[4].Value.ToString());
                produit.NumeroMedicament = dgvRows.Cells[5].Value.ToString();
                listeItems.Add(produit);
            }
            return listeItems;
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
 
                if (comboBox2.Text == "Pharmacie")
                {
                    if (checkBox2.Checked)
                    {
                        checkBox1.Checked = false;
                        checkBox3.Checked = false;
                        var liste = from p in listeProduit()
                                    orderby p.PrixTotal descending
                                    select p;
                        dataGridView1.Rows.Clear();
                        foreach (var p in liste)
                        {
                            dataGridView1.Rows.Add(
                           p.Designation,
                               p.Quantite.ToString(),
                                p.NombreVente.ToString(),
                                p.PrixVente.ToString(),
                               p.PrixTotal.ToString(),
                               p.NumeroMedicament.ToString());                
                        }
                    }
                }
                else
                {
                    var produit = from l in ListeDepot()
                                  orderby l.TotalAchat descending
                                  select l;
                    dataGridView1.Rows.Clear();
                    foreach (var l in produit)
                    {
                        dataGridView1.Rows.Add(
                            l.Designation.ToUpper(),
                           l.GrandStock.ToString(),
                            l.DifferenceStock.ToString(),
                            l.PrixAchat.ToString(),
                           (l.PrixAchat * l.DifferenceStock).ToString(),
                           l.Description);
                    }
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
     
                if (comboBox2.Text == "Pharmacie")
                {
                    if (checkBox1.Checked)
                    {
                        checkBox3.Checked = false;
                        checkBox2.Checked = false;
                        var liste = from p in listeProduit()
                                    orderby p.NombreVente descending
                                    select p;
                        dataGridView1.Rows.Clear();
                        foreach (var p in liste)
                        {
                            dataGridView1.Rows.Add(
                   p.Designation,
                       p.Quantite.ToString(),
                        p.NombreVente.ToString(),
                        p.PrixVente.ToString(),
                       p.PrixTotal.ToString(),
                       p.NumeroMedicament.ToString()
                );
                        }
                    }
                }
                else
                {

                    var produit = from l in ListeDepot()
                                  orderby l.DifferenceStock descending
                                  select l;
                    dataGridView1.Rows.Clear();
                    foreach (var l in produit)
                    {
                        dataGridView1.Rows.Add(l.Designation.ToUpper(),
                           l.GrandStock.ToString(),
                            l.DifferenceStock.ToString(),
                            l.PrixAchat.ToString(),
                           (l.PrixAchat * l.DifferenceStock).ToString(),
                           l.Description);
                    }
                }
            }
            catch { }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                if (comboBox2.Text == "Pharmacie")
                {
            var liste = AppCode.ConnectionClass.RapportStatistiqueParMontant(dateDebut, dateFin,txtNomProduit.Text);
                    decimal pourcent = 0;
                    decimal montantTotal = 0;
                    foreach (var l in liste)
                    {
                        montantTotal += l.PrixTotal;
                    }
                    foreach (var li in liste)
                    {
                        pourcent = li.PrixTotal * 100 / montantTotal;
                        string pourcentage = null;
                        if (pourcent.ToString().Length > 4)
                        {
                            pourcentage = pourcent.ToString().Substring(0, 4) + "%";
                        }
                        else
                        {
                            pourcentage = pourcent.ToString();
                        }
                        var produit = AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom(li.NomMedicament);
                        dataGridView1.Rows.Add(
                                  li.NomMedicament,
                                    produit[0].Quantite.ToString(),
                                   li.Quantite.ToString(),
                                   li.PrixVente.ToString(),
                                   li.PrixTotal.ToString(),
                                   pourcentage
                                );
                    }
                }
                else
                {

                    var produit = from l in ListeDepot()
                                  orderby l.TotalAchat descending
                                  select l;
                    foreach (var l in produit)
                    {
                        dataGridView1.Rows.Add(
                           l.Designation.ToUpper(),
                           l.GrandStock.ToString(),
                            l.DifferenceStock.ToString(),
                            l.PrixAchat.ToString(),
                           ( l.PrixAchat * l.DifferenceStock).ToString(),     
                           l.Description);
                    }
                }
            }
            catch { }
        }

        List<AppCode.Produit> ListeDepot()
        {
            var liste = AppCode.ConnectionClass.ListeMouvementStockGroupeParQuantite(1, 2, dateDebut, dateFin);

            decimal pourcent = 0;
            decimal montantTotal = 0;
            foreach (var l in liste)
            {
                montantTotal += l.PrixAchat * l.DifferenceStock;
            }
            var listeP = new  List<AppCode.Produit>();
            foreach (var l in liste)
            {
                pourcent = l.PrixAchat * l.DifferenceStock * 100 / montantTotal;
                string pourcentage = null;
                if (pourcent.ToString().Length > 4)
                {
                    pourcentage = pourcent.ToString().Substring(0, 4) + "%";
                }
                else
                {
                    pourcentage = pourcent.ToString();
                }
                var produit = AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom(l.Designation);
                var p = new AppCode.Produit();

                p.Designation = l.Designation.ToUpper();
                p.GrandStock = produit[0].GrandStock;
                p.DifferenceStock = l.DifferenceStock;
                p.PrixAchat = l.PrixAchat;
                p.TotalAchat = (double)l.PrixAchat * l.DifferenceStock;
                p.Description = pourcentage;
                p.PrixCession = pourcent;
                listeP.Add(p);
            }
            return listeP;
        }

        private void btnsupexamen_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        var titre = "Statistique de vente du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
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
                                    var _listeImpression = AppCode.ImprimerRaportVente.ImprimerStatistique(dataGridView1, titre, i);

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
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private void ToCsV1(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count ; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count ; j++)
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
                sfd.FileName = "Statistique vente " + date + ".xls";

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

        private void txtNomProduit_TextChanged(object sender, EventArgs e)
        {
            comboBox2_SelectedIndexChanged(null, null);
        }

    }
}
