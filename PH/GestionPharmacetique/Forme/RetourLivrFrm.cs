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
    public partial class RetourLivrFrm : Form
    {
        public RetourLivrFrm()
        {
            InitializeComponent();
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CornflowerBlue, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        public string code,produit ;
        private void RetourLivrFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var ListeFournisseur = from f in AppCode. ConnectionClass.ListeFournisseur()
                                       orderby f.NomFournisseur
                                       select f;
                foreach (var f in ListeFournisseur)
                {
                    lblNomFournisseur.Items.Add(f.NomFournisseur);
                }
                ListeRetour();
                txtQte.Focus();
            }
            catch { }
        }

        void ListeRetour()
        {
            try
            {
                dgvVente.Rows.Clear();
                var dt = AppCode.ConnectionClass.ListeRetourLivraison(lblBonLivraison.Text);
                if (dt.Rows.Count > 0)
                {
                    lblBonLivraison.Text = dt.Rows[0].ItemArray[5].ToString();
                    lblNomFournisseur.Text = lblNomFournisseur.Text;
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        dgvVente.Rows.Add(
                            dtRow.ItemArray[0].ToString(),
                           DateTime.Parse(dtRow.ItemArray[4].ToString()).ToShortDateString(),
                           dtRow.ItemArray[1].ToString(),
                           dtRow.ItemArray[6].ToString(),
                           dtRow.ItemArray[2].ToString(),
                         double.Parse(dtRow.ItemArray[6].ToString()) *
                         double.Parse(dtRow.ItemArray[2].ToString()),
                          dtRow.ItemArray[3].ToString()
                       );
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        Bitmap _listeImpression;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(_listeImpression, 10, 10, _listeImpression.Width, _listeImpression.Height);
            e.HasMorePages = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVente.Rows.Count > 0)
                {
                    if (checkBox1.Checked)
                    {
                        var titre = "Chronologie des retours des produits du " + dateTimePicker1.Value.ToShortDateString() + " au " + dateTimePicker2.Value.ToShortDateString() + " de " + lblNomFournisseur.Text;
                        _listeImpression = GestionPharmacetique.AppCode.Imprimer.ImprimerRapportRetourCommande(dgvVente, titre);

                    }
                    else
                    {
                        _listeImpression = GestionPharmacetique.AppCode.Imprimer.ImprimerRetourCommande(dgvVente, lblNomFournisseur.Text);

                    } if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;

        }

        //enregistrer le retour
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                 int qte;double prix;
                    if (Int32.TryParse(txtQte.Text, out qte) && double.TryParse(txtPrixCession.Text , out prix))
                    {
                        var btnClick = MonMessageBox.ShowBox("Voulez vous retirer le produit " + produit +" de la liste commande ?", "Confirmation", "confirmation.png");
                        if (btnClick == "1")
                        {
                            var idLiv = lblBonLivraison.Text;

                            AppCode.ConnectionClass.InsererLeRetour(numeroProduit, txtDesignation.Text, prix, qte, txtMotif.Text, idLiv);
                            txtQte.Text = "";
                            txtMotif.Text = "";
                            txtDesignation.Text = "";
                            txtPrixCession.Text = "";
                            txtPrixTotal.Text = "";
                            txtDesignation.Focus();
                            ListeRetour();
                          
                        }
                    }
                
            }
            catch (Exception)
            {
            }
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
       public  string numeroProduit;
        private void txtDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListeProduitFrm.state = "0";
                    ListeProduitFrm.indexRecherche = txtDesignation.Text;
                    if (ListeProduitFrm.ShowBox() == "1")
                    {
                        var flag = false;
                        var livraisonArrayList = AppCode.ConnectionClass.ListeDesDetailsLivraisons(lblBonLivraison.Text);
                        foreach (AppCode.Livraison livraison in livraisonArrayList)
                        {
                            if (livraison.NomMedicament == ListeProduitFrm.designation)
                            {
                                flag = true;
                            }
                        }
                        if (flag)
                        {
                            var montant = 0m;
                            txtDesignation.Text = ListeProduitFrm.designation;
                            txtPrixCession.Text = String.Format(elGR, "{0:0,0}", ListeProduitFrm.prixCession);
                            txtPrixTotal.Text = String.Format(elGR, "{0:0,0}", montant);
                            numeroProduit = ListeProduitFrm.numeroProduit;
                            txtQte.Focus();
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Ce produit n'est pas sur la liste des produits livrés", "Erreur", "erreur.png");
                        }
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        private void txtQte_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double qte, prixCession;
                if (double.TryParse(txtQte.Text, out qte) && double.TryParse(txtPrixCession.Text, out prixCession))
                {
                    txtPrixTotal.Text = string.Format(elGR, "{0:0,0}", qte * prixCession);
                }
                else
                {
                    txtPrixTotal.Text = "";
                }
            }
            catch { }
        }

        private void txtQte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(null, null);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVente.SelectedRows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous retirer cette ligne du bon de retour?", "Confirmation", "confirmation.png") == "1")
                    {
                        var id = Int32.Parse(dgvVente.SelectedRows[0].Cells[0].Value.ToString());
                        var qte = Int32.Parse(dgvVente.SelectedRows[0].Cells[4].Value.ToString());
                        var designation = dgvVente.SelectedRows[0].Cells[2].Value.ToString();
                        var l = AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom(designation);
                        var numeroProduit = l[0].NumeroMedicament;
                        if (AppCode.ConnectionClass.RetirerLeRetour(id, numeroProduit, qte))
                        {
                            dgvVente.Rows.Remove(dgvVente.SelectedRows[0]);
                        }
                    }
                }
            }
            catch { }
            }

        private void lblBonLivraison_TextChanged(object sender, EventArgs e)
        {
            var listeLivraison = AppCode.ConnectionClass.ListeDesLivraisons(lblBonLivraison.Text);

            if (listeLivraison.Count > 0)
            {
                foreach (AppCode.Livraison l in listeLivraison)
                {
                    lblNomFournisseur.Text = l.NomFournisseur;
                }
                ListeRetour();
            }
            else
            {
                lblNomFournisseur.Text = "";
                dgvVente.Rows.Clear();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            try
            {
                if(!string.IsNullOrEmpty(lblNomFournisseur.Text))
                {
                    var n=0;
                    var liste = from f in AppCode.ConnectionClass.ListeFournisseur()
                                where f.NomFournisseur.ToUpper() == lblNomFournisseur.Text.ToUpper()
                                select f.ID;
                              foreach(var l in liste)
                                  n=l;
                dgvVente.Rows.Clear();
                var dt = AppCode.ConnectionClass.ListeRetourLivraison(n,dateTimePicker1.Value.Date,dateTimePicker2.Value.Date);
                if (dt.Rows.Count > 0)
                {
                    lblBonLivraison.Text = dt.Rows[0].ItemArray[5].ToString();
                    lblNomFournisseur.Text = lblNomFournisseur.Text;
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        dgvVente.Rows.Add(
                            dtRow.ItemArray[0].ToString(),
                           DateTime.Parse(dtRow.ItemArray[4].ToString()).ToShortDateString(),
                           dtRow.ItemArray[1].ToString(),
                           dtRow.ItemArray[6].ToString(),
                           dtRow.ItemArray[2].ToString(),
                         double.Parse(dtRow.ItemArray[6].ToString()) *
                         double.Parse(dtRow.ItemArray[2].ToString()),
                          dtRow.ItemArray[3].ToString()
                       );
                    }
                }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void dgvVente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
     
    }
}
