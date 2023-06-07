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

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
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
                label1.Text = "Mouvement d'entrée et sortie des articles " + depot;
                var listeReference = from r in ConnectionClass.ListeDocumentStock(etat)
                                     where r.IDReference == numeroPiece
                                     select r;
                foreach (var r in listeReference)
                {
                    lRef.Text = r.Reference;
                    lPi.Text = r.IDReference.ToString();
                    lDep.Text = r.Source;
                    comboBox2.Text = r.Date.ToShortDateString();
                    lblDestination.Text = r.Destination;
                }
                if (GestionPharmacetique.Form1.typeUtilisateur == "caissier")
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                }
                comboBox1.Text ="<<Type d'impression>>";
                btnExit.Location = new Point(Width - 43, btnExit.Location.Y);
               
                  var listeReference1 = from r in ConnectionClass.ListeDocumentStock(etat)
                                        select r;
                comboBox3.Items.Add("");
                  foreach (var r in listeReference1)
                  {
                      comboBox3.Items.Add(r.Reference);
                  }
                if (etat == 1)
                {
                    label1.Text = "Mouvement d'entrée N° " + numeroPiece + " des articles "+typeDepot;
                }
                else if (etat == 2)
                {
                    label1.Text = " Mouvement de sortie N° " + numeroPiece + " des articles" + typeDepot;
                }

             btnExit.Location = new Point(Width - 43, btnExit.Location.Y); 
            clDesidgnation.Width = dgvProduit.Width / 2-100;
            clLot.Width = 100;
            clPrixAchat.Width = dgvProduit.Width / 6-30;
            clStock.Width= dgvProduit.Width / 6-60;
            clMontantHT.Width = dgvProduit.Width / 6-50;
            lblLot.Location = new Point(lblDesignation.Width + 10, lblDesignation.Location.Y);
            lblDesignation.Width = dgvProduit.Width / 2-102;
            lblPrixAchat.Width = dgvProduit.Width / 6-32;
            txtStock.Width = dgvProduit.Width / 6-62;
            lblMontantHT.Width = dgvProduit.Width / 6 -42;
            dateTimePicker4.Width = dgvProduit.Width / 6 - 42;
            //lblDesignation.Location = new Point(10, lblDesignation.Location.Y);
            lblPrixAchat.Location = new Point(lblDesignation.Width + 108, lblDesignation.Location.Y);
            txtStock.Location = new Point(lblDesignation.Width + lblPrixAchat.Width + 110, lblDesignation.Location.Y);
            lblMontantHT.Location = new Point(lblDesignation.Width + lblPrixAchat.Width + txtStock.Width+ 110, lblDesignation.Location.Y);
        dateTimePicker4.Location = new Point(lblMontantHT.Width+ lblDesignation.Width + lblPrixAchat.Width + txtStock.Width + 110, lblDesignation.Location.Y);
            lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal()) + " .F";
            }
            catch { }
        }

        public static string numeroProduit, btnClick, designation, reference, depot, typePrix;
        public static int  grandStock, stock, etat,typeDepot, numeroPiece;
        public static decimal prix;
       public  static ModifTsockFrm frm;

        public static string ShowBox()
        {
            frm = new ModifTsockFrm();
            frm.Size = new System.Drawing.Size(GestionPharmacetique.Form1.width1, GestionPharmacetique.Form1.height1+4);
            frm.Location = new Point(205, 183);
            frm.ShowDialog();
            return btnClick;
        }
        void ListeDesDetailsMouvements()
        {
            try
            {
                dgvProduit.Rows.Clear();

                var liste = from f in ConnectionClass.LogStock(idMouvement)
                            join p in ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                            on f.NumeroProduit equals p.NumeroMedicament
                            orderby p.NomMedicament 
                            select new
                            {
                                p.NumeroMedicament,
                                p.NomMedicament,
                                f.DifferenceStock,
                                f.PrixAchat,f.Description,f.DateExpiration
                            };
                foreach (var p in liste)
                {
                    dgvProduit.Rows.Add(
                        p.NumeroMedicament,
                        p.NomMedicament,p.Description,
                        String.Format(elGR, "{0:0,0}", p.PrixAchat),
                        String.Format(elGR, "{0:0,0}", p.DifferenceStock),
                        String.Format(elGR, "{0:0,0}", p.PrixAchat * p.DifferenceStock),
                        p.DateExpiration.ToShortDateString()
                        );
                }
                lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal());
            }
            catch { }
        }
        private void txtStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (idMouvement > 0)
                    {
                        if (Int32.TryParse(txtStock.Text, out stock) && decimal.TryParse(lblPrixAchat.Text, out prix))
                        {
                            var produit = new Produit();
                            produit.DateExpiration = dateTimePicker4.Value.Date;
                            produit.NumeroProduit = numeroProduit;
                            produit.PrixAchat = prix;
                            produit.DifferenceStock = stock;
                            produit.Description = lblLot.Text;
                            var doc = new DocumentStock();
                            doc.Reference = lRef.Text;
                            doc.Destination = lblDestination.Text;
                            //if (typeDepot == 1 && !ConnectionClass.ValiderQuantitePharmacie(numeroProduit, stock))
                            //{
                            //    MonMessageBox.ShowBox("La quantité n'est pas assez grande (" + stock + ") pour transferer", "Erreur", "erreur.png");
                            //    return;
                            //}
                            //else if (typeDepot == 2 && !ConnectionClass.ValiderQuantiteDepot(numeroProduit, stock))
                            //{
                            //    return;
                            //}

                            if (!string.IsNullOrEmpty(numeroProduit))
                            {
                                var flag = false;
                                if (dgvProduit.Rows.Count > 0)
                                {
                                    for (var i = 0; i < dgvProduit.Rows.Count; i++)
                                    {
                                        if (produit.NumeroProduit == dgvProduit.Rows[i].Cells[0].Value.ToString()  
                                           && produit.Description==dgvProduit.Rows[i].Cells[0].Value.ToString() && indexRow !="1" )
                                        {
                                            GestionPharmacetique.MonMessageBox.ShowBox("Ce produit est deja ajouté sur la liste", "Erreur", "erreur.png");
                                            flag = true;
                                        }
                                    }
                                    if (!flag)
                                    {
                                        if (ConnectionClass.ModifierStock(produit, idMouvement, etat, doc, typeDepot, indexRow))
                                        {
                                            indexRow = null;
                                            ListeDesDetailsMouvements();
                                            btnClick = "1";
                                            lblDesignation.Focus();
                                            txtStock.Text = "";
                                            lblDesignation.Text = "";
                                            lblPrixAchat.Text = "";
                                            lblMontantHT.Text = "";
                                            lblLot.Text = "";
                                        }
                                    }
                                }
                                else
                                {
                                    if (ConnectionClass.ModifierStock(produit, idMouvement, etat, doc,typeDepot, indexRow))
                                    {
                                        ListeDesDetailsMouvements();
                                        btnClick = "1";
                                        indexRow = null;
                                        lblDesignation.Focus();
                                        txtStock.Text = "";
                                        lblDesignation.Text = "";
                                        lblPrixAchat.Text = "";
                                        lblMontantHT.Text = "";
                                        lblLot.Text = "";
                                    }
                                }
                            }
                            else
                            {
                                //        produit.NumeroProduit = numeroProduit;
                                //        var ancienStock = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[5].Value.ToString());
                                //        produit.NombreJour = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[8].Value.ToString());
                                //        if (ConnectionClass.MettreAJourModificationStock(produit, ancienStock, etat, lRef.Text))
                                //        {
                                //            dgvProduit.SelectedRows[0].Cells[5].Value = String.Format(elGR, "{0:0,0}", Convert.ToInt32(txtStock.Text));
                                //            prix = (int)(Convert.ToDouble(dgvProduit.SelectedRows[0].Cells[5].Value.ToString()) *
                                //               Convert.ToDouble(dgvProduit.SelectedRows[0].Cells[4].Value.ToString()));
                                //            dgvProduit.SelectedRows[0].Cells[6].Value = String.Format(elGR, "{0:0,0}", prix);
                                //            numeroProduit = null; grandStock = 0;
                                //            btnClick = "1";
                                //            lblDesignation.Focus();
                                //            txtStock.Text = "";
                                //            lblDesignation.Text = "";
                                //            lblPrixAchat.Text = "";
                                //        }
                                //    }
                                //    lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal()) + " .F";
                            }
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez créer un nouveau mouvement ou selectinonner sur la liste existante", "Erreur", "erreur.png");
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

        string  indexRow;
        private void dgvProduit_DoubleClick(object sender, EventArgs e)
        {
            if (dgvProduit.SelectedRows.Count > 0)
            {
                numeroProduit = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                lblDesignation.Text = dgvProduit.SelectedRows[0].Cells[1].Value.ToString();
                lblPrixAchat.Text = dgvProduit.SelectedRows[0].Cells[3].Value.ToString();
                txtStock.Text = dgvProduit.SelectedRows[0].Cells[4].Value.ToString();
                lblLot.Text = dgvProduit.SelectedRows[0].Cells[2].Value.ToString();
                txtStock.Focus();
                indexRow = "1";
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var docStock = new DocumentStock();
                docStock.IDReference = Convert.ToInt16(lPi.Text);
                docStock.Destination = lblDestination.Text;
                docStock.Reference = lRef.Text;
                docStock.NumeroMatricule = lblOperateur.Text;
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
                    var index = dgvProduit.Rows.Count / 40;
                    var titre = "";
                    if (comboBox1.Text == "Facture")
                    {
                        titre = "FACTURE N°: " + DateTime.Now.Year + "/" + idMouvement;
                    }
                    else if (comboBox1.Text == "Mouvement")
                    {
                        if (etat == 1)
                        {
                            titre = "MOUVEMENT D'ENTREE N°: " + DateTime.Now.Year + "/" + idMouvement;
                        }
                        else 
                        {
                            titre = "MOUVEMENT DE SORTIE N°: " + DateTime.Now.Year + "/" + idMouvement;
                        }
                    }
                    else if (comboBox1.Text == "Bon livraison")
                    {
                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 40 < dgvProduit.Rows.Count)
                            {
                                titre = "Bon livraison N°: " + DateTime.Now.Year + "/" + idMouvement;
                                var _inventaireStock = Impression.ImprimerBonLivraison(dgvProduit, titre, docStock, i, dateInv, lblTotalHT.Text);
                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage pageIndex = document.addPage();

                                document.addImageReference(_inventaireStock, inputImage);
                                sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                        return;
                    }
                    else if (comboBox1.Text == "Rapport")
                    {
                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 41< dgvProduit.Rows.Count)
                            {
                                if (etat == 1)
                                {
                                    titre = "Rapport de mouvement d'entrée " +comboBox3.Text+ " du " +
                                        dateTimePicker2.Value.ToShortDateString() + " au " + dateTimePicker3.Value.ToShortDateString();
                                }
                                else
                                {   
                                    titre = "Rapport de mouvement de sortie " +comboBox3.Text +" du " +
                                        dateTimePicker2.Value.ToShortDateString() + " au " + dateTimePicker3.Value.ToShortDateString();
                                }
                                var _inventaireStock = Impression.ImprimerRapportMouvementStock(dgvProduit, titre, i, lblTotalHT.Text);
                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage pageIndex = document.addPage();

                                document.addImageReference(_inventaireStock, inputImage);
                                sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                        return;
                    }
                    else if (comboBox1.Text == "<<Type d'impression>>" || string.IsNullOrEmpty(comboBox1.Text))
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le type d'impression avant de continuer", "Erreur", "erreur.png");
                        return;
                    }
                    //string inputImage1 = @"cdali";
                    for (var i = 0; i <= index; i++)
                    {
                        if (i * 40 < dgvProduit.Rows.Count)
                        {
                            var _inventaireStock = Impression.ImprimerMouvementStock(dgvProduit, titre, docStock, i, dateInv, lblTotalHT.Text);
                            var inputImage = @"cdali" + i;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(_inventaireStock, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -0, 0, pageIndex.height, pageIndex.width);
                        }
                    }
                }
                document.createPDF(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }

            catch { }
        }
        DateTime date;
        private void lblDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListeProduitAvecLot.state = "0";
                    GestionPharmacetique.Forme.ListeProduitAvecLot.indexRecherche = lblDesignation.Text;
                    if (lblDesignation.Text.Length > 2)
                    if (GestionPharmacetique.Forme.ListeProduitAvecLot.ShowBox() == "1")
                    {
                        numeroProduit = GestionPharmacetique.Forme.ListeProduitAvecLot.numeroProduit;
                        lblDesignation.Text = GestionPharmacetique.Forme.ListeProduitAvecLot.designation;
                        lblLot.Text = GestionPharmacetique.Forme.ListeProduitAvecLot.NoLot;
                        dateTimePicker4.Value = GestionPharmacetique.Forme.ListeProduitAvecLot.dateExpiration;
                        if(typePrix=="prix achat")
                        {
                        prix = GestionPharmacetique.Forme.ListeProduitAvecLot.prixCession;
                        }else  if(typePrix=="prix vente")
                        {
                        prix = GestionPharmacetique.Forme.ListeProduitAvecLot.prixPublic;
                        }else{
                            prix = GestionPharmacetique.Forme.ListeProduitAvecLot.prixCession;
                        }
                        date = GestionPharmacetique.Forme.ListeProduitAvecLot.dateExpiration;
                        lblPrixAchat.Text = prix.ToString();
                       lblLot.Focus();
                    }
                }
            }
            catch { }
        }
        int idMouvement;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var doc = new GestionPharmacetique.AppCode.DocumentStock();
                doc.IDReference = numeroPiece;
                doc.Date = dateTimePicker1.Value.Date;
                doc.ID = idMouvement;
                doc.Etat = etat;
                doc.EtatDepot =typeDepot;
                if (GestionPharmacetique.Form1.typeUtilisateur == "caissier")
                {
                    button2.Enabled = false;
                    return;
                }
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous créer un nouvement?", "Confirmation", "confirmation.png") == "1")
                {
                    if (GestionPharmacetique.AppCode.ConnectionClass.CreerUnNouveauMouvement(doc))
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Nouveau document crée avec succés", "Affirmation", "affirmation.png");
                        idMouvement = GestionPharmacetique.AppCode.ConnectionClass.DernierNouveauMouvement();
                        dgvProduit.Rows.Clear();
                        txtStock.Text = "";
                        lblDesignation.Text = "";
                        lblDesignation.Focus();
                        lblPrixAchat.Text = "";
                        lblMontantHT.Text = "";
                        lblLot.Text="";lblTotalHT.Text="";
                        ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Création du mouvement de réference : " + lRef.Text, this.Name);
                    }
                }
            }
            catch
            {
            }
        }

        private void dgvProduit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex ==7)
            {
                try
                {
                    if (dgvProduit.Rows.Count > 0)
                    {
                        if (ConnectionClass.SiMouvementValider(idMouvement) == 0)
                        {
                            if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous retirer cet element de la liste?", "Confirmation", "confirmation.png") == "1")
                            {
                                var p = new Produit();
                                p.DifferenceStock = (int)Convert.ToDouble(dgvProduit.SelectedRows[0].Cells[4].Value.ToString());
                                p.Stock = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[4].Value.ToString());
                                p.NumeroProduit = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                                var docu = new DocumentStock();
                                docu.Reference = lRef.Text; docu.Destination = lblDestination.Text;
                                docu.ID = idMouvement;
                                if (ConnectionClass.RetirerStock(p, etat, docu))
                                {
                                    dgvProduit.Rows.Remove(dgvProduit.SelectedRows[0]);
                                    lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal());
                                }
                            }
                        }
                        else { GestionPharmacetique.MonMessageBox.ShowBox("Données deja valider ne peuvent être modifiées", "Erreur", "erreur.png"); }
                    }
                }
                catch (Exception ex)
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("", ex);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListeModFrm.etat = etat;
            ListeModFrm.typeDepot = typeDepot;
            if (ListeModFrm.ShowBox())
            {
                idMouvement = ListeModFrm.doc.ID;

                var listeReference = from r in ConnectionClass.ListeDocumentStock(etat)
                                     join m in ConnectionClass.ListeMouvementStock(typeDepot)
                                   on r.IDReference equals m.IDReference
                                   where m.ID==idMouvement
                                     select r;
                foreach (var r in listeReference)
                {
                    lRef.Text = r.Reference;
                    lPi.Text = r.IDReference.ToString();
                    lDep.Text = r.Source;
                    comboBox2.Text = r.Date.ToShortDateString();
                    lblDestination.Text = r.Destination;
                }
                idMouvement = ListeModFrm.doc.ID;
                lblOperateur.Text = ListeModFrm.doc.NumeroMatricule;
                ListeDesDetailsMouvements();
            }
        }

        private void lblPrixAchat_TextChanged(object sender, EventArgs e)
        {
            double qte, prixAchat;
            if (double.TryParse(txtStock.Text, out qte) && double.TryParse(lblPrixAchat.Text, out prixAchat))
            {
                var prixTotal = qte * prixAchat;
                lblMontantHT.Text = prixTotal.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.Rows.Count > 0) 
                    if (GestionPharmacetique.Form1.typeUtilisateur == "caissier")
                    {
                        button1.Enabled = false;
                        return;
                    }
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous valider ces données?", "Confirmation", "confirmation.png") == "1")
                {
                    if (ConnectionClass.SiMouvementValider(idMouvement) == 0)
                    {
                        var doc = new DocumentStock();
                        doc.Reference = lRef.Text;
                        doc.Destination = lblDestination.Text;
                        if (GestionPharmacetique.AppCode.ConnectionClass.ValidererMouvementStock(dgvProduit, idMouvement, etat, doc, typeDepot))
                        {
                            dgvProduit.Rows.Clear();
                            ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Validation du mouvement de réference : " + lRef.Text, this.Name);
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Ce mouvement a été deja validé", "Erreur.png", "erreur.png");
                    }
                }
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
             dgvProduit.Rows.Clear();
                        txtStock.Text = "";
                        lblDesignation.Text = "";
                        lblPrixAchat.Text = "";
                        lblMontantHT.Text = "";
                        lblLot.Text="";lblTotalHT.Text="";
        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                dgvProduit.Rows.Clear();
                
                    if (string.IsNullOrEmpty(comboBox3.Text))
                    {
                        var liste = ConnectionClass.ListeMouvementStockGroupeParQuantiteExclu(typeDepot, etat, dateTimePicker2.Value.Date, dateTimePicker3.Value.Date);

                        foreach (var p in liste)
                        {
                            dgvProduit.Rows.Add(
                                p.NumeroProduit,
                                p.Designation, "",
                                String.Format(elGR, "{0:0,0}", p.PrixAchat),
                                String.Format(elGR, "{0:0,0}", p.DifferenceStock),
                                 String.Format(elGR, "{0:0,0}", p.PrixAchat * p.DifferenceStock),
                                ""
                                );
                        }
                    }
                    else
                    {                        var liste = ConnectionClass.ListeMouvementStockGroupeParQuantite(typeDepot, etat, dateTimePicker2.Value.Date, dateTimePicker3.Value.Date, comboBox3.Text);

                        foreach (var p in liste)
                        {
                            dgvProduit.Rows.Add(
                                p.NumeroProduit,
                                p.Designation, "",
                                String.Format(elGR, "{0:0,0}", p.PrixAchat),
                                String.Format(elGR, "{0:0,0}", p.DifferenceStock),
                                 String.Format(elGR, "{0:0,0}",p.PrixAchat * p.DifferenceStock),
                                ""
                                );
                        }
                    }
                lblTotalHT.Text = String.Format(elGR, "{0:0,0}", CalculerPrixTotal());
            }
            catch { }
        }

        private void lblPrixAchat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtStock.Focus();
            }
        }

        private void lblLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                lblPrixAchat.Focus();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new GestionPharmacetique.Forme.ConsoService();
            frm.etat = etat;
            frm.typeDepot = typeDepot;
                frm.ShowDialog();
        }
  
    }
}
