using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GestionPharmacetique.AppCode;
using GestionPharmacetique.Forme;

namespace SGDP.Formes
{
    public partial class ModifTsockFrm : Form
    {
        public ModifTsockFrm()
        {
            InitializeComponent();
        }


        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Gainsboro
                , SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Gainsboro
                , SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        //txtPrixTotal.Text = String.Format(elGR, "{0:0,0}", montantTh);
        private void ModifTsockFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dgvProduit.Rows.Clear();

                var listeReference = from r in ConnectionClass.ListeDocumentStock()
                                     where r.ID == numeroPiece
                                     select r;
                foreach (var r in listeReference)
                {
                    lRef.Text = r.Reference;
                    lPi.Text = r.ID.ToString();
                    lDep.Text = r.Destination;
                    comboBox2.Text = r.Date.ToShortDateString();
                }

                var liste = from f in ConnectionClass.LogStock(etat)
                            join p in ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                            on f.NumeroProduit equals p.NumeroMedicament
                            where f.NombreSortie == numeroPiece
                            orderby f.DateModification descending
                            select new
                            {

                                p.NumeroMedicament,
                                p.NomMedicament,
                                f.DifferenceStock,
                                f.PrixAchat,
                                f.Motif,
                                f.DateModification,
                                f.GroupeID
                            };
                foreach (var p in liste)
                {
                    dgvProduit.Rows.Add(p.NumeroMedicament,
                        p.DateModification.ToShortDateString(),
                        p.NomMedicament,
                         String.Format(elGR, "{0:0,0}", p.PrixAchat),
                          String.Format(elGR, "{0:0,0}", p.DifferenceStock),
                           String.Format(elGR, "{0:0,0}", p.PrixAchat * p.DifferenceStock), p.Motif, p.GroupeID);
                }

                if (etat == 1)
                {
                    label1.Text = "Mouvement d'entrée N° " + numeroPiece + " des articles";
                }
                else if (etat == 2)
                {
                    label1.Text = " Mouvement de sortie N° " + numeroPiece + " des articles";
                }



            clDesidgnation.Width = dgvProduit.Width / 3 + dgvProduit.Width / 9;

            btnExit.Location = new Point(Width - 43, btnExit.Location.Y);
            Column1.Width = dgvProduit.Width / 9;        
            clPrixAchat.Width = dgvProduit.Width / 9;
            clStock.Width= dgvProduit.Width / 9;
            clDescription.Width = dgvProduit.Width / 9;
            Column3.Width = dgvProduit.Width / 9; 
                
            lblDesignation.Width = dgvProduit.Width / 3 - 4 + 2 * dgvProduit.Width / 9 - 4;
            lblPrixAchat.Width = dgvProduit.Width / 9-2;
            txtStock.Width = dgvProduit.Width / 9-3;
            comboBox1.Width = 2*dgvProduit.Width / 9 +5;

            var width = 2 * dgvProduit.Width / 9 - 4;
            lblDesignation.Location = new Point(10, lblDesignation.Location.Y);
            lblPrixAchat.Location = new Point(lblDesignation.Width+ 13, lblPrixAchat.Location.Y);
            txtStock.Location = new Point( lblDesignation.Width+ lblPrixAchat.Width+ 16, txtStock.Location.Y);
            comboBox1.Location = new Point(lblDesignation.Width + lblPrixAchat.Width *2 + 18, comboBox1.Location.Y - 2);
            lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal()) + " .F";
            }
            catch { }
        }

        public static string numeroProduit, btnClick, designation, reference;
        public static int  grandStock, stock, prix, etat, numeroPiece;

       public  static ModifTsockFrm frm;

        public static string ShowBox()
        {
            frm = new ModifTsockFrm();
            frm.Size = new System.Drawing.Size(GestionPharmacetique.Form1.width1, GestionPharmacetique.Form1.height1+4);
            frm.Location = new Point(205, 183);
            frm.ShowDialog();
            return btnClick;
        }

        private void txtStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (Int32.TryParse(txtStock.Text, out stock))
                    {
                        var produit = new Produit();
                        produit.NumeroProduit = numeroProduit;
                        produit.NombreSortie = numeroPiece;
                        produit.Motif = comboBox1.Text;
                        produit.DateModification = DateTime.Now;
                        produit.IDUser = GestionPharmacetique.Form1.numEmploye;
                        produit.PrixAchat = prix;                  
                        produit.DifferenceStock = stock;

                        if (!string.IsNullOrEmpty(numeroProduit))
                        {
                            if (ConnectionClass.ModifierStock(produit, etat,lRef.Text))
                            {
                                dgvProduit.Rows.Clear();

                                var liste = from f in ConnectionClass.LogStock(etat)
                                            join p in ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                                            on f.NumeroProduit equals p.NumeroMedicament
                                            where f.NombreSortie == numeroPiece
                                            orderby f.DateModification descending 
                                            select new
                                            {
                                                p.NumeroMedicament,
                                                p.NomMedicament,
                                                f.DifferenceStock,
                                                f.PrixAchat,
                                                f.Motif,
                                                f.DateModification,
                                                f.GroupeID
                                            };
                                foreach (var p in liste)
                                {
                                    dgvProduit.Rows.Add(p.NumeroMedicament,
                                        p.DateModification.ToShortDateString(),
                                        p.NomMedicament,
                                        String.Format(elGR, "{0:0,0}", p.PrixAchat),
                                        String.Format(elGR, "{0:0,0}", p.DifferenceStock),
                                        String.Format(elGR, "{0:0,0}", p.PrixAchat * p.DifferenceStock), p.Motif, p.GroupeID);
                                }

                                btnClick = "1";
                                lblDesignation.Focus();
                                txtStock.Text = "";
                                lblDesignation.Text = "";
                                lblPrixAchat.Text = "";
                            }
                        }
                        else
                        {
                            produit.NumeroProduit = numeroProduit ;
                            var ancienStock = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[5].Value.ToString());
                            produit.NombreJour = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[8].Value.ToString());
                            if (ConnectionClass.MettreAJourModificationStock(produit, ancienStock, etat,lRef.Text))
                            {
                                dgvProduit.SelectedRows[0].Cells[5].Value =   String.Format(elGR, "{0:0,0}",Convert.ToInt32(txtStock.Text));
                                prix = (int)(Convert.ToDouble( dgvProduit.SelectedRows[0].Cells[5].Value.ToString())*
                                   Convert.ToDouble(dgvProduit.SelectedRows[0].Cells[4].Value.ToString()));
                                dgvProduit.SelectedRows[0].Cells[6].Value =   String.Format(elGR, "{0:0,0}",prix);
                                numeroProduit = null; grandStock = 0;
                                btnClick = "1";
                                lblDesignation.Focus();
                                txtStock.Text = "";
                                lblDesignation.Text = "";
                                lblPrixAchat.Text = "";
                            }
                        }
                        lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal()) + " .F";
                    }
                }
                catch { }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnClick = "2";
            Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                dgvProduit.Rows.Clear();
                
                    var liste = from f in ConnectionClass.LogStock(etat )
                                join p in ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                                on f.NumeroProduit equals p.NumeroMedicament
                                where f.DateModification >= dateTimePicker1.Value.Date
                                where f.DateModification < dateTimePicker2.Value.Date.AddHours(24)
                                where f.NombreSortie == numeroPiece
                                orderby f.DateModification
                                select new
                                {f.GroupeID,
                                    p.NumeroMedicament,
                                    p.NomMedicament,
                                    f.DifferenceStock,
                                    f.PrixAchat,
                                    f.Motif,
                                    f.DateModification
                                };
                    foreach (var p in liste)
                    {
                        dgvProduit.Rows.Add(
                            p.NumeroMedicament ,
                            p.DateModification.ToShortDateString(),
                            p.NomMedicament,
                            String.Format(elGR, "{0:0,0}", p.PrixAchat),
                            String.Format(elGR, "{0:0,0}", p.DifferenceStock),
                            String.Format(elGR, "{0:0,0}", p.PrixAchat * p.DifferenceStock), p.Motif, p.GroupeID);
                    }
                    lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal()) + " .F";
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                dgvProduit.Rows.Clear();

                var liste = from f in ConnectionClass.LogStock(etat)
                            join p in ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                            on f.NumeroProduit equals p.NumeroMedicament 
                            where f.DateModification >= dateTimePicker1.Value.Date
                            where f.DateModification < dateTimePicker2.Value.Date.AddHours(24)
                            where p.Designation.ToUpper()==lblDesignation.Text.ToUpper()
                            where f.NombreSortie == numeroPiece
                            orderby f.DateModification
                            select new
                            {
                                f.GroupeID,
                                p.NumeroMedicament,
                                p.Designation,
                                f.DifferenceStock,
                                f.PrixAchat,
                                f.Motif,
                                f.DateModification
                            };
                foreach (var p in liste)
                {
                    dgvProduit.Rows.Add(p.NumeroMedicament,
                        p.DateModification.ToShortDateString(),
                        p.Designation,
                        String.Format(elGR, "{0:0,0}", p.PrixAchat),
                        String.Format(elGR, "{0:0,0}", p.DifferenceStock),
                        String.Format(elGR, "{0:0,0}", p.PrixAchat * p.DifferenceStock), 
                        p.Motif, p.GroupeID);
                }

                lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal()) + " .F";
            }
            catch { }
        }

        private void dgvProduit_DoubleClick(object sender, EventArgs e)
        {
            if (dgvProduit.SelectedRows.Count > 0)
            {
                   numeroProduit = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                lblDesignation.Text = dgvProduit.SelectedRows[0].Cells[2].Value.ToString();
                lblPrixAchat.Text = dgvProduit.SelectedRows[0].Cells[3].Value.ToString();
                txtStock.Text = dgvProduit.SelectedRows[0].Cells[4].Value.ToString();
                comboBox1.Text = dgvProduit.SelectedRows[0].Cells[6].Value.ToString();
                grandStock = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[4].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.Rows.Count > 0)
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous retirer cet element de la liste?", "Confirmation", "confirmation.png") == "1")
                    {
                        var p = new Produit();
                        p.DifferenceStock = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[7].Value.ToString());
                        p.Stock = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[4].Value.ToString());
                        p.NumeroProduit = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                        if (ConnectionClass.RetirerStock(p, etat,lRef.Text))
                        {
                            dgvProduit.Rows.Remove(dgvProduit.SelectedRows[0]);
                            lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }

        }

        double  CalculerPrixTotal()
        {
            try{
            var totalHT=0.0;
            for(var i=0; i< dgvProduit.Rows.Count ;i++)
            {
                totalHT += Convert.ToDouble(dgvProduit.Rows[i].Cells[5].Value.ToString());
              
            }
            return totalHT;
            }catch
            {
                return 0.0;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            button6_Click(null, null);
            lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var docStcok = new DocumentStock();
                docStcok.ID = Convert.ToInt16(lPi.Text);
                docStcok.Destination = lDep.Text;
                docStcok.Reference = lRef.Text;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";
                var dateInv = DateTime.Now;               

                sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = label1.Text +"_imprimé_le_" + date + ".pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                { 
                    //string inputImage1 = @"cdali";
                    var index = dgvProduit.Rows.Count / 40;
                    for (var i = 0; i <= index; i++)
                    {
                        if (i * 40 < dgvProduit.Rows.Count)
                        {
                            var _inventaireStock = Impression.ImprimerMouvementStock(dgvProduit,label1.Text,docStcok ,i, dateInv,lblTotalHT.Text);

                            var inputImage = @"cdali" + i;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(_inventaireStock, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                        }
                    }
                }
                document.createPDF(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }

            catch { }
        }

        private void lblDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListeProduitFrm.state = "0";
                    GestionPharmacetique.Forme.ListeProduitFrm.indexRecherche = lblDesignation.Text;
                    if (GestionPharmacetique.Forme.ListeProduitFrm.ShowBox() == "1")
                    {
                        numeroProduit = GestionPharmacetique.Forme.ListeProduitFrm.numeroProduit;
                        lblDesignation.Text = GestionPharmacetique.Forme.ListeProduitFrm.designation;
                        prix = (int)GestionPharmacetique.Forme.ListeProduitFrm.prixCession;
                        lblPrixAchat.Text = prix.ToString();
                        txtStock.Focus();
                    }
                }
            }
            catch { }
        }

        private void dgvProduit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
