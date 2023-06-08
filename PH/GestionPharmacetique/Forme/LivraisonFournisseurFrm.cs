using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace GestionPharmacetique.Forme
{
    public partial class LivraisonFournisseurFrm : Form
    {
        public LivraisonFournisseurFrm()
        {
            InitializeComponent();
        }

        private void FournisseurFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ActiveCaption, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
            
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CornflowerBlue, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CornflowerBlue, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CornflowerBlue, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
    
        private void groupBox13_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox13.Width - 1, this.groupBox13.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ActiveCaption, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
         SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FournisseurFrm_Load(object sender, EventArgs e)
        {
            try
            {
                cl1.Width = dgvVente.Width / 2;

                txtRechercherProduit.Width = cl1.Width - 1;
                var yLocation = txtRechercherProduit.Location.Y;
                var xSize = (dgvVente.Width - dgvVente.Width / 3) / 5 -12;
                txtPrixCession.Width = txtPrixPublic.Width = txtQte.Width = txtPrixTotal.Width = xSize;
                //dateTimePicker2.Width = xSize+2;
                txtPrixCession.Location = new Point(txtRechercherProduit.Width + 2, yLocation);
                txtPrixPublic.Location = new Point(txtRechercherProduit.Width + xSize + 2, yLocation);
                txtQte.Location = new Point(txtRechercherProduit.Width + 2 * xSize + 2, yLocation);
                txtPrixTotal.Location = new Point(txtRechercherProduit.Width + 3 * xSize + 3, yLocation);
                var ListeFournisseur = from f in ConnectionClass.ListeFournisseur()
                                       orderby f.NomFournisseur
                                       select f;
                foreach (var f in ListeFournisseur)
                {
                    cmbFournisseur.Items.Add(f.NomFournisseur);
                }
            }
            catch (Exception)
            {}
        }
             
        //rechercher par produit
        private void txtRechercherProduit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                     ListeProduitFrm.indexRecherche = txtRechercherProduit.Text;
                     ListeProduitFrm.state = "0";
                     if (ListeProduitFrm.ShowBox() == "1")
                     {
                         var montant = 0m;
                         txtRechercherProduit.Text = ListeProduitFrm.designation;
                         txtPrixCession.Text = String.Format(elGR, "{0:0,0}", ListeProduitFrm.prixCession);
                         txtPrixPublic.Text = String.Format(elGR, "{0:0,0}", ListeProduitFrm.prixPublic);
                         txtPrixTotal.Text = String.Format(elGR, "{0:0,0}", montant);
                         numeroProduit = ListeProduitFrm.numeroProduit;
                         //dateTimePicker2.Value = ListeProduitFrm.dateExpiration;
                         txtPrixCession.Focus();
                     }
                }

            }
            catch (Exception)
            {
            }
        }

       
        private void dgvVente_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var montantFactural=0.0;
                foreach (DataGridViewRow dgvRow in dgvVente.Rows)
                {
                    var prixVente = Double.Parse(dgvRow.Cells[2].Value.ToString());
                    var qte = Int32.Parse(dgvRow.Cells[4].Value.ToString());
                    var prixTotal = prixVente * qte;
                    dgvRow.Cells[5].Value = prixTotal;
                   

                    montantFactural +=prixTotal;
                }
                txtMntFact.Text = montantFactural.ToString();               
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Cell end edit", ex);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                 decimal prixAchat, prixVente;
                    int id, qte;

                    if (etat == "2")
                    {
                        id = Convert.ToInt32(dgvVente.Rows[rowIndex].Cells[0].Value.ToString());
                    }
                    else
                    {
                        id = 0;
                    }
                    if (!string.IsNullOrEmpty(txtRechercherProduit.Text))
                    {
                        if (decimal.TryParse(txtPrixCession.Text, out prixAchat))
                        {
                            if (decimal.TryParse(txtPrixPublic.Text, out prixVente))
                            {
                                if (Int32.TryParse(txtQte.Text, out qte))
                                {
                                    if (!string.IsNullOrEmpty(txtNoLivraisom.Text))
                                    {
                                        var livraison = new Livraison(id, txtNoLivraisom.Text, numeroProduit, txtRechercherProduit.Text, prixAchat, prixVente, qte);
                                        if (ConnectionClass.EnregistrerLivraison(livraison, dgvVente, etat,  rowIndex))
                                        {
                                            AppCode.ConnectionClass.MisAjourAutomatique(numeroProduit);
                                            txtRechercherProduit.Text = "";
                                            txtQte.Text = "";
                                            txtPrixTotal.Text = "";
                                            txtPrixCession.Text = "";
                                            txtPrixPublic.Text = "";
                                            txtRechercherProduit.Focus();
                                            etat = "1";
                                            var prixTotal = 0.0;
                                            for (var i = 0; i < dgvVente.Rows.Count; i++)
                                            {
                                                prixTotal += double.Parse(dgvVente.Rows[i].Cells[6].Value.ToString());
                                            }
                                            txtMntFact.Text = String.Format(elGR, "{0:0,0}", prixTotal);
                                            double autresCharges, montantTTC;
                                            if (double.TryParse(txtAutresCharges.Text, out autresCharges))
                                            {
                                                montantTTC = prixTotal + autresCharges;
                                            }
                                            else
                                            {
                                                montantTTC = prixTotal;
                                            }
                                            txtMontantTTC.Text = String.Format(elGR, "{0:0,0}", montantTTC);
                                        }
                                    }
                                }
                                else
                                {
                                    txtPrixPublic.BackColor = Color.Red;
                                }
                            }
                            else
                            {
                                txtPrixPublic.BackColor = Color.Red;
                            }
                        }
                        else
                        {
                            txtPrixCession.BackColor = Color.Red;
                        }
                    }
                }
            
            catch { }
        }

        //nouvelle commande
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string numLivraison;
                if (!string.IsNullOrEmpty(txtNoLivraisom.Text))
                    {
                        if (!string.IsNullOrEmpty(cmbFournisseur.Text))
                        {
                            var dateLivraison = dateTimePicker1.Value;
                            var listeFournisseur = from f in AppCode.ConnectionClass.ListeFournisseur()
                                                   where f.NomFournisseur.ToUpper() == cmbFournisseur.Text.ToUpper()
                                                   select f.ID;
                            int numFournisseur = 0;
                            foreach (var f in listeFournisseur)
                                numFournisseur = f;


                            var montantHT = 0m;
                            var autresCharges = 0m;
                            numLivraison = txtNoLivraisom.Text;
                            var livraison = new Livraison(numLivraison, dateLivraison, numFournisseur, "", autresCharges, montantHT);
                            if (MonMessageBox.ShowBox("Voulez vous enregistrer la facture?", "Confirmation", "confirmation.png") == "1")
                            {
                               if (ConnectionClass.CreerUneLivraison(livraison))
                                {
                                    txtRechercherProduit.Text = "";
                                    txtQte.Text = "";
                                    txtMntFact.Text = montantHT.ToString();
                                    txtAutresCharges.Text = autresCharges.ToString();
                                    txtMontantTTC.Text = "0";
                                    txtPrixCession.Text = "";
                                    txtPrixPublic.Text = "";
                                    btnEnregistreLivraison.Enabled = true;
                                    txtRechercherProduit.Enabled = true;
                                    txtQte.Enabled = true;
                                    txtPrixPublic.Enabled = true;
                                    txtPrixCession.Enabled = true;
                                    etat = "1";
                                    dgvVente.Rows.Clear();
                                    txtRechercherProduit.Focus();
                                }
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez selectionner le nom du fournisseur ", "Erreur", "erreur.png");
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le numero de la livraison. ", "Erreur", "erreur.png");
                    }
                
            }
            catch (Exception Exception) { MonMessageBox.ShowBox("", Exception); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtNoLivraisom.Text = comboBox1.Text.Substring(0, comboBox1.Text.IndexOf("&")-1);
                var livraisonArrayList = AppCode.ConnectionClass.ListeDesDetailsLivraisons(txtNoLivraisom.Text);
                var listeLivraison = AppCode.ConnectionClass.ListeDesLivraisons(txtNoLivraisom.Text);
                
                decimal montantTotal = 0;
                dgvVente.Rows.Clear();
                foreach (AppCode.Livraison livraison in livraisonArrayList)
                {
                    //var listeMedi = ConnectionClass.ListeDesMedicamentParCode(livraison.NumProduit);
                    dgvVente.Rows.Add( 
                        livraison.ID,
                        livraison.NumProduit, livraison.NomMedicament.ToUpper(),
                        String.Format(elGR, "{0:0,0}",livraison.PrixAchat),
                        String.Format(elGR, "{0:0,0}",livraison.PrixVente),
                        String.Format(elGR, "{0:0,0}",livraison.Quantite),
                        String.Format(elGR, "{0:0,0}",(livraison.PrixAchat * livraison.Quantite)),
                       "");
                    montantTotal += livraison.PrixAchat * livraison.Quantite;
                }
                txtMntFact.Text = String.Format(elGR, "{0:0,0}",montantTotal);
                btnEnregistreLivraison.Enabled = true;
                txtRechercherProduit.Enabled = true;
                txtQte.Enabled = true;
                txtPrixPublic.Enabled = true;
                txtPrixCession.Enabled = true;
                etat = "1";
                txtRechercherProduit.Focus();
                foreach (Livraison liv in listeLivraison)
                {
                    txtAutresCharges.Text = String.Format(elGR, "{0:0,0}", liv.AutresCharges);
                    txtMontantTTC.Text = String.Format(elGR, "{0:0,0}", liv.AutresCharges+ montantTotal);
                    dateTimePicker1.Value = liv.DateLivraison;
                }
            }
            catch (Exception Exception) { MonMessageBox.ShowBox("", Exception); }
        }

        //annuler une commande
        private void btnAnnuerCmd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNoLivraisom.Text))
                {
                    if (MonMessageBox.ShowBox("Voulez vous annuler la livraison N° " + comboBox1.Text + "?", "Confirmation", "confrimation.png") == "1")
                    {
                        ConnectionClass.SupprimerLivraison(txtNoLivraisom.Text);
                        txtRechercherProduit.Text = "";
                        txtQte.Text = "";
                        txtPrixTotal.Text = "";
                        txtPrixCession.Text = "";
                        txtPrixPublic.Text = "";
                        txtNoLivraisom.Text = "";
                        dgvVente.Rows.Clear();
                        comboBox1.Items.Clear();
                        var livraisonArrayList = ConnectionClass.ListeDesLivraisonsParFournisseur
                            (cmbFournisseur.Text);

                        foreach (Livraison livraison in livraisonArrayList)
                        {
                            comboBox1.Items.Add(livraison.NumLivraison + " & " + livraison.DateLivraison.ToShortDateString());
                        }
                    }
                }
            }
            catch { }
        }

        //retourde produit
        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new RetourLivrFrm();
            frm.Location = new Point(Location.X, Location.Y);
            frm.Size = new Size(Width, Height);
            if (dgvVente.SelectedRows.Count > 0)
            {
                frm.lblBonLivraison.Text = txtNoLivraisom.Text;
                frm.lblNomFournisseur.Text = cmbFournisseur.Text;
                frm.txtDesignation.Text = dgvVente.SelectedRows[0].Cells[2].Value.ToString();
                frm.numeroProduit = dgvVente.SelectedRows[0].Cells[1].Value.ToString();
                frm.txtPrixCession .Text= dgvVente.SelectedRows[0].Cells[3].Value.ToString();
            } frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new MedicamentFrm();
            frm.txtQte.Enabled = false;
            frm.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txtNoLivraisom.Text = comboBox1.Text.Substring(0, comboBox1.Text.IndexOf("&") - 1);
                var livraisonArrayList = AppCode.ConnectionClass.ListeDesDetailsLivraisons(txtNoLivraisom.Text);
                var listeLivraison = AppCode.ConnectionClass.ListeDesLivraisons(txtNoLivraisom.Text);

                decimal montantTotal = 0;
                dgvVente.Rows.Clear();
                foreach (AppCode.Livraison livraison in livraisonArrayList)
                {
                    //var listeMedi = ConnectionClass.ListeDesMedicamentParCode(livraison.NumProduit);
                    dgvVente.Rows.Add(
                        livraison.ID,
                        livraison.NumProduit, livraison.NomMedicament.ToUpper(),
                        String.Format(elGR, "{0:0,0}", livraison.PrixAchat),
                        String.Format(elGR, "{0:0,0}", livraison.PrixVente),
                        String.Format(elGR, "{0:0,0}", livraison.Quantite),
                        String.Format(elGR, "{0:0,0}", (livraison.PrixAchat * livraison.Quantite)),
                        "");
                    montantTotal += livraison.PrixAchat * livraison.Quantite;
                }
                txtMntFact.Text = String.Format(elGR, "{0:0,0}", montantTotal);
                btnEnregistreLivraison.Enabled = true;
                txtRechercherProduit.Enabled = true;
                txtQte.Enabled = true;
                txtPrixPublic.Enabled = true;
                txtPrixCession.Enabled = true;
                etat = "1";
                //txtRechercherProduit.Focus();
                foreach (Livraison liv in listeLivraison)
                {
                    txtAutresCharges.Text = String.Format(elGR, "{0:0,0}", liv.AutresCharges);
                    txtMontantTTC.Text = String.Format(elGR, "{0:0,0}", liv.AutresCharges + montantTotal);
                    dateTimePicker1.Value = liv.DateLivraison;
                }
            }
            catch (Exception Exception) { MonMessageBox.ShowBox("", Exception); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvVente.SelectedRows.Count > 0)
            {
                if(MonMessageBox.ShowBox("Voulez vous vous retirer ces données de la liste de livraison","Confirmation","confirmation.png")=="1")
                {
                    var  montantTotal =0.0;

                    var id = Int32.Parse(dgvVente.SelectedRows[0].Cells[0].Value.ToString());
                    numeroProduit = dgvVente.SelectedRows[0].Cells[1].Value.ToString();
                    var quantite =(int) double.Parse(dgvVente.SelectedRows[0].Cells[5].Value.ToString());
                    ConnectionClass.RetirerLivraison(id, numeroProduit,quantite,txtNoLivraisom.Text);
                    dgvVente.Rows.Remove(dgvVente.SelectedRows[0]);
                    for (int i = 0; i < dgvVente.Rows.Count; i++)
                    {
                        montantTotal += Double.Parse(dgvVente.Rows[i].Cells[6].Value.ToString());
                    }
                    txtMntFact.Text = montantTotal.ToString();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                var titre = "facture_livraison_de_" +cmbFournisseur.Text + "_du_"+dateTimePicker1.Value.ToShortDateString()+"_"+DateTime.Now.Second;
                sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
               
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
                { 
                    s = s.Replace("/", "_");
                }
                sfd.FileName = "Impression_le_" + s + ".pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    var count = dgvVente.Rows.Count;

                    var index = (dgvVente.Rows.Count) / 43;

                    for (var i = 0; i <= index; i++)
                    {
                        if (i * 43 < count)
                        {
                            var _listeImpression = Impression.ImprimerLivraison
                                (dgvVente,txtNoLivraisom.Text,cmbFournisseur.Text , dateTimePicker1.Value.ToShortDateString(),
                                txtMntFact.Text ,txtAutresCharges.Text, txtMontantTTC.Text, i);

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
            catch(Exception) { }
        }

     
        private void dgvVente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRechercherProduit.Focus();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (etat != "1")
            {
                if (dgvVente.SelectedRows.Count > 0)
                {
                    dgvVente.SelectedRows[0].Cells[7].Value = dateTimePicker1.Value.ToShortDateString();
                }
            }
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        //String.Format(elGR, "{0:0,0}", montantTh);
        private void txtPrixCession_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double prixCession, quantite;
                if (double.TryParse(txtPrixCession.Text, out  prixCession))
                {
                    if (double.TryParse(txtQte.Text, out quantite))
                    {
                        txtPrixTotal.Text = String.Format(elGR, "{0:0,0}", prixCession * quantite);
                    }
                    txtPrixPublic.Focus();
                }
                else
                {
                    txtPrixCession.Focus();
                }
            }
        }

        private void txtPrixPublic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double prixCession;
                if (double.TryParse(txtPrixPublic.Text, out  prixCession))
                {
                    txtQte.Focus();
                }
                else
                {
                    txtPrixPublic.Focus();
                }
            }
        }

        private void txtQte_TextChanged(object sender, EventArgs e)
        {
            double qte, prixCession, prixTotal=0;
            if (double.TryParse(txtQte.Text, out qte) && double.TryParse(txtPrixCession.Text, out prixCession))
            {
                txtQte.BackColor = txtPrixCession.BackColor = txtPrixPublic.BackColor = Color.White;

                prixTotal += prixCession * qte;
                txtPrixTotal.Text = String.Format(elGR, "{0:0,0}", prixTotal);
            }
            else
            {
                txtPrixTotal.Text = String.Format(elGR, "{0:0,0}", prixTotal);
            }
            //for (var i = 0; i < dgvVente.Rows.Count; i++)
            //{
            //    prixTotal += double.Parse(dgvVente.Rows[0].Cells[6].Value.ToString());
            //}
            //txtMntFact.Text = String.Format(elGR, "{0:0,0}", prixTotal);
        }

        private void cmbFournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                var livraisonArrayList = ConnectionClass.ListeDesLivraisonsParFournisseur
                    (cmbFournisseur.Text );

                foreach (Livraison livraison in livraisonArrayList)
                {
                    comboBox1.Items.Add(livraison.NumLivraison+ " & "+ livraison.DateLivraison.ToShortDateString());
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        string numeroProduit, etat;
        DateTime dateExpiration; int rowIndex;
        private void txtQte_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    button8_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void dgvVente_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvVente.SelectedRows.Count > 0)
                {
                    rowIndex = dgvVente.SelectedRows[0].Index;
                    txtRechercherProduit.Text = dgvVente.SelectedRows[0].Cells[2].Value.ToString();
                    txtPrixCession.Text = dgvVente.SelectedRows[0].Cells[3].Value.ToString();
                    txtPrixPublic.Text = dgvVente.SelectedRows[0].Cells[4].Value.ToString();
                    txtQte.Text = dgvVente.SelectedRows[0].Cells[5].Value.ToString();
                    numeroProduit = dgvVente.SelectedRows[0].Cells[1].Value.ToString();
                    txtPrixTotal.Text = dgvVente.SelectedRows[0].Cells[6].Value.ToString();
                    txtQte.Focus();
                    etat = "2";
                }
            }
            catch { }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if(comboBox2.Text=="Ordre de saisie")
            {
                try
                {
                    txtNoLivraisom.Text = comboBox1.Text;
                    var livraisonArrayList = AppCode.ConnectionClass.ListeDesDetailsLivraisonsParOrdreDeSaisie(comboBox1.Text);
                    var listeLivraison = AppCode.ConnectionClass.ListeDesLivraisons(comboBox1.Text);

                    decimal montantTotal = 0;
                    dgvVente.Rows.Clear();
                    foreach (AppCode.Livraison livraison in livraisonArrayList)
                    {
                        var listeMedi = ConnectionClass.ListeDesMedicamentParCode(livraison.NumProduit);
                        dgvVente.Rows.Add(
                            livraison.ID,
                            livraison.NumProduit, livraison.NomMedicament.ToUpper(),
                            String.Format(elGR, "{0:0,0}", livraison.PrixAchat),
                            String.Format(elGR, "{0:0,0}", livraison.PrixVente),
                            String.Format(elGR, "{0:0,0}", livraison.Quantite),
                            String.Format(elGR, "{0:0,0}", (livraison.PrixAchat * livraison.Quantite)),
                            listeMedi[0].DateExpiration.ToShortDateString());
                        montantTotal += livraison.PrixAchat * livraison.Quantite;
                    }
                    txtMntFact.Text = String.Format(elGR, "{0:0,0}", montantTotal);
                    btnEnregistreLivraison.Enabled = true;
                    txtRechercherProduit.Enabled = true;
                    txtQte.Enabled = true;
                    txtPrixPublic.Enabled = true;
                    txtPrixCession.Enabled = true;
                    etat = "1";
                    txtRechercherProduit.Focus();
                    foreach (Livraison liv in listeLivraison)
                    {
                        dateTimePicker1.Value = liv.DateLivraison;
                    }
                }
                catch (Exception Exception) { MonMessageBox.ShowBox("", Exception); }
            }
            else if (comboBox2.Text == "Désignation")
            {
                comboBox1_SelectedIndexChanged(null, null);
            }
        }

        private void txtAutresCharges_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal autresCharges, montantHT;
                if (decimal.TryParse(txtAutresCharges.Text,out  autresCharges) && decimal.TryParse(txtMntFact.Text , out montantHT))
                {
                    if (!string.IsNullOrEmpty(txtNoLivraisom.Text))
                    {
                        var montantTTC = autresCharges + montantHT;
                        txtMontantTTC.Text  = string.Format(elGR, "{0:0,0}", montantTTC);
                        var livraison = new Livraison(txtNoLivraisom.Text, dateTimePicker1.Value, 0,"",autresCharges,0);
                        ConnectionClass.MettreAJourUneLivraison(livraison);
                    }
                }
            }
            catch { }
        }
        
        
    }
}
