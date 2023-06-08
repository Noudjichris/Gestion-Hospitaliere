using System;
using System.Collections.Generic;
using GestionPharmacetique.AppCode;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GestionPharmacetique.Forme
{
    public partial class InventaireDesProduitsFrm : Form
    {
        public InventaireDesProduitsFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CornflowerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CornflowerBlue, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void InventaireDesProduitsFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CornflowerBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void InventaireDesProduitsFrm_Load(object sender, EventArgs e)
        {
            try

            {
                if (Form1.typeUtilisateur == "caissier")
                {
                    validerLinventaireToolStripMenuItem.Enabled = false;
                    nouvelleInventaireToolStripMenuItem.Enabled = false;
                }
                comboBox2.Text = "<Inventaire stock de :>";
                button3.Location = new Point(Width - 43, 4);
                //cl1.Width = dataGridView1.Width / 7;
                cl2.Width = dgvProduit.Width / 3;
                var xSize1 = dgvProduit.Width / 3+2;
                var xSize2 =( dgvProduit.Width - dgvProduit.Width/3)/7;

                txtNomProduit.Width = xSize1;
                txtAncien.Width = txtEcart.Width = txtEmplacement.Width = txtPrixAchat.Width
                   = txtPrixVente.Width = dateTimePicker1.Width = txtStock.Width = xSize2;

                txtNomProduit.Location = new Point(0, 9);
                txtPrixAchat.Location = new Point(xSize1, 9);
                txtPrixVente.Location = new Point(xSize1 + xSize2 , 9);
                txtStock.Location = new Point(xSize1 + xSize2 *2, 9);
                txtAncien.Location = new Point(xSize1 + xSize2*3, 9);
                txtEcart.Location = new Point(xSize1 + xSize2*4, 9);
                dateTimePicker1.Location = new Point(xSize1 + xSize2*5, 9);
                txtEmplacement.Location = new Point(xSize1 + xSize2*6, 9);
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste inventaire", exception);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Next)
            {
                //var indexCell = dataGridView1.CurrentCell.ColumnIndex;
                //var indexRow = dataGridView1.CurrentRow.Index;
                //dataGridView1.CurrentCell = dataGridView1.Rows[indexRow].Cells[indexCell + 1];
                //dataGridView1.BeginEdit(true);
            }
           
        }
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox3.Text = "Nbre produit   " + dgvProduit.Rows.Count;
            }
        }

        string numeroProduit = null ;
        string indexDetail = "0";
        int id;
       

       private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dgvProduit.ContextMenuStrip = contextMenuStrip1;
                dgvProduit.ContextMenuStrip.Show(dgvProduit, e.Location);
            }
        }
        bool inventaire = false;
        private void viderLaListeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text == "<Inventaire stock de :>")
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le depot", "Erreur", "erreur.png");
                    return ;
                }
                if (MonMessageBox.ShowBox("Voulez vous commencer une nouvelle inventaire?", "Confirmation", "confirmation.png") == "1")
                {
                    var listeProduit = ConnectionClass.ListeDesMedicamentsRechercherParNom("");
                    txtNoInv.Text = ConnectionClass.EnregistrerInventaire(listeProduit,comboBox2.Text).ToString();
                    ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Creation d'un nouveau inventaire pour le " + comboBox2.Text, this.Name);
                    if(Int32.Parse(txtNoInv.Text)>0)
                    {
                        var listeGroupe = ConnectionClass.ListeGroupe();
                        foreach(var gr in listeGroupe  )
                        {
                            dgvProduit.Rows.Add(
                                       "", gr.Groupe, "", "", "", "", "", "", "", ""
                                       );
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;

                            var listeFamille = ConnectionClass.ListeDesFamille().Where(f=> f.NombreDetail==gr.CodeFamille);
                            foreach (var p in listeFamille)
                            {
                                listeProduit = ConnectionClass.ListeDesMedicamentsRechercherParFamille(p.Designation);
                                if (listeProduit.Count() > 0)
                                {
                                    dgvProduit.Rows.Add(
                                            "", p.Designation, "", "", "", "", "", "", "", ""
                                            );

                                    foreach (var produit in listeProduit)
                                    {
                                        int quantite;
                                        if (comboBox2.Text == "Pharmacie")
                                        {
                                            quantite = produit.Quantite;
                                        }
                                        else
                                        {
                                            quantite = produit.GrandStock;
                                        }
                                        dgvProduit.Rows.Add(
                                            produit.NumeroMedicament,
                                            produit.NomMedicament.ToUpper(),
                                             produit.PrixAchat,
                                            produit.PrixVente, "",
                                            quantite, "",
                                            produit.DateExpiration.ToShortDateString(), "", ""
                                            );

                                    }
                                }
                            }
                        }
                }
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
                    sfd.FileName = "Inventaire_imprimé_le_" + date + ".pdf";
                    //_inventaireStock = AppCode.Impression.ImprimerInventaireStockPage(dataGridView1, 0, dateInv);
                    //printPreviewDialog1.ShowDialog();

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //string inputImage1 = @"cdali";
                        var index = dgvProduit.Rows.Count / 45;
                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 45 < dgvProduit.Rows.Count)
                            {
                                var _inventaireStock = AppCode.Imprimer.ImprimerInventaireStockPage(dgvProduit,comboBox2.Text, i, DateTime.Now,0,false);

                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage pageIndex = document.addPage();

                                document.addImageReference(_inventaireStock, inputImage);
                                sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                            }
                        }
                    }
                    document.createPDF(sfd.FileName); dgvProduit.Rows.Clear();
                    System.Diagnostics.Process.Start(sfd.FileName);
                    dgvProduit.ReadOnly = false;
                    inventaire = true;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void continuerInentaireToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                txtNoInv.Text = ConnectionClass.DerniereInventaire().ToString();
            dgvProduit.Rows.Clear();
            dgvProduit.ReadOnly = false;
            inventaire = true;
            if (txtNoInv.Text != "")
            {
                ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Continuer  inventaire pour le " +txtNoInv.Text , this.Name);
                  
                var listInv = from inv in ConnectionClass.ListeInventaire()
                              where inv.IDInventaire == Convert.ToInt32(txtNoInv.Text)
                              select inv;
                foreach (var inv in listInv)
                    comboBox2.Text = inv.Emplacement;
                var listeProduit = from i in ConnectionClass.ListeDetailsInventaire()
                                   join p in ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                                   on i.IDProduit equals p.NumeroMedicament
                                   where i.IDInventaire == Convert.ToInt32(txtNoInv.Text)
                                   where i.Quantite > 0
                                   orderby i.Ordre
                                   select new
                                   {
                                       p.NumeroMedicament,
                                       p.NomMedicament,
                                       i.Quantite,
                                       i.PrixPublic,
                                       i.PrixCession,
                                       i.IDInventaire,i.Ordre,
                                     i.QuantiteAvant,i.ID,
                                       i.Emplacement,p.DateExpiration,i.IndexDetail
                                   };

                dgvProduit.Rows.Clear();
                var lstIventaire = from i in ConnectionClass.ListeDetailsInventaire()
                                   where i.IDProduit ==i.IDProduit
                                   select i.QuantiteAvant;
                
                foreach (var p in listeProduit)
                {
                    var ecart = "";
                    if (p.Quantite - p.QuantiteAvant >= 0)
                    {
                        ecart = "+" + (p.Quantite - p.QuantiteAvant);
                    }
                    else
                    {
                        ecart = (p.Quantite - p.QuantiteAvant).ToString();
                    }
                    var designation = p.NomMedicament;
                    if (p.IndexDetail == "1")
                    {
                        designation = p.NomMedicament + " DETAIL";
                    }
                    dgvProduit.Rows.Add(
                        
                                        p.NumeroMedicament,
                                        designation,
                                        p.PrixCession,
                                        p.PrixPublic,
                                        p.Quantite,
                                        p.QuantiteAvant,
                                        ecart,
                                        p.DateExpiration.ToShortDateString(),
                                         p.Emplacement, p.IndexDetail,p.Ordre

                                    );
                }
                label6.Text = listeProduit.Count().ToString();
            }
            }
            catch (Exception Exception) { MonMessageBox.ShowBox("", Exception); }
        }

        private void listeDesInventairesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox6.Visible = true;
            try
            {
                var liste = from i in ConnectionClass.ListeInventaire()
                            orderby i.DateInventaire descending
                            select i;

                dataGridView2.Rows.Clear();
                foreach (var i in liste)
                {
                    var etat = "Non validé";
                    if (i.Etat)
                    {
                        etat = "Validé";
                    }
                    dataGridView2.Rows.Add(i.IDInventaire, i.DateInventaire, i.Emplacement, etat, i.Employe);
                }
            }
            catch
            {
            }
        }

        private void remettreLesDonnéesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (Int32.TryParse(txtNoInv.Text, out id))
                {
                    if (dgvProduit.Rows.Count > 0)
                    {

                        //if (ConnectionClass.RemettreLeStock(dataGridView1))
                        {
                            MonMessageBox.ShowBox("Stock remis avec succés", "Affirmation", "affirmation");
                        }
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données?", "Confirmation", "confirmation.png") == "1")
                {
                    if (ConnectionClass.SupprimerInventaire(Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString())))
                    {
                        ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Suppression de l' inventaire numero" +dataGridView2.SelectedRows[0].Cells[0].Value.ToString(), this.Name);
                        dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
                    }
                }
            }

        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    groupBox6.Visible = false;
                    txtNoInv.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    dgvProduit.Rows.Clear();
                    dgvProduit.ReadOnly = false;
                    comboBox2.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                    inventaire = true;
                    if (txtNoInv.Text != "")
                    {
                        var listeGroupe = ConnectionClass.ListeGroupe();
                        foreach (var gr in listeGroupe)
                        {
                            dgvProduit.Rows.Add(
                                       "", gr.Groupe, "", "", "", "", "", "", "", ""
                                       );
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                            var listeFamille = ConnectionClass.ListeDesFamille().Where(f => f.NombreDetail == gr.CodeFamille);

                            foreach (var pp in listeFamille)
                            {

                                var listeProduit = from i in ConnectionClass.ListeDetailsInventaire()
                                                   join p in ConnectionClass.ListeDesMedicamentsRechercherParFamille(pp.Designation)
                                                   on i.IDProduit equals p.NumeroMedicament
                                                   where i.IDInventaire == Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString())
                                                   //where i.Quantite > 0
                                                   orderby p.NomMedicament
                                                   select new
                                                   {
                                                       p.NumeroMedicament,
                                                       p.NomMedicament,
                                                       i.Quantite,
                                                       i.PrixPublic,
                                                       i.PrixCession,
                                                       ID = i.IDInventaire,
                                                       p.QuantiteAlerte,
                                                       i.QuantiteAvant,
                                                       i.Emplacement,
                                                       p.DateExpiration,
                                                       i.Ordre
                                                   };

                                if (listeProduit.Count() > 0)
                                {
                                    dgvProduit.Rows.Add(
                                            "", pp.Designation, "", "", "", "", "", "", "", ""
                                            );

                                    foreach (var p in listeProduit)
                                    {
                                        var ecart = "";
                                        if (p.Quantite - p.QuantiteAvant >= 0)
                                        {
                                            ecart = "+" + (p.Quantite - p.QuantiteAvant);
                                        }
                                        else
                                        {
                                            ecart = (p.Quantite - p.QuantiteAvant).ToString();
                                        }
                                        var designation = p.NomMedicament;

                                        dgvProduit.Rows.Add(
                                                            p.NumeroMedicament,
                                                            designation.ToUpper(),
                                                            p.PrixCession,
                                                            p.PrixPublic,
                                                            p.Quantite,
                                                              p.QuantiteAvant,
                                                             ecart,
                                                           p.DateExpiration.ToShortDateString(),
                                                           p.Emplacement,
                                                           p.Ordre
                                                        );
                                    }
                                }
                            }
                        }
                        label6.Text = dgvProduit.Rows.Count.ToString();
                    }
                }
            }
            catch (Exception Exception) { MonMessageBox.ShowBox("", Exception); }
        }

        private void cmbTypeRecherche_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbTypeRecherche.Text))
                {
                    var dateDebut = DateTime.Parse("01-01-" + cmbTypeRecherche.Text);
                    var dateFin = DateTime.Parse("31-12-" + cmbTypeRecherche.Text);

                    var liste = from i in ConnectionClass.ListeInventaire()
                                where i.DateInventaire >=dateDebut 
                                where i.DateInventaire < dateFin
                                select i;

                    dataGridView2.Rows.Clear();
                    foreach (var i in liste)
                    {
                        dataGridView2.Rows.Add(i.IDInventaire, i.DateInventaire);
                    }
                }
            }
            catch { }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            groupBox6.Visible = false;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {groupBox6.Visible = false;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            dgvProduit.Rows.Clear();
        }

        private void retirerUnProduitDeLaListeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvProduit.SelectedRows.Count > 0)
            {
                dgvProduit.Rows.Remove(dgvProduit.SelectedRows[0]);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.SelectedRows.Count > 0)
                {
                    if (SGDP.Formes.TauxFrm.ShowBox())
                    {
                        var taux = SGDP.Formes.TauxFrm.taux;
                        for (var i = 0; i < dgvProduit.SelectedRows.Count; i++)
                        {
                            //prixAchat = prixVente / (1 + taux / 100);
                            var prixCession= Convert.ToDouble(dgvProduit.SelectedRows[i].Cells[4].Value.ToString()) / (1 + taux / 100);

                            dgvProduit.SelectedRows[i].Cells[3].Value = System.Math.Round(prixCession);
                        }
                    }
                }
            }
            catch { }
        }

        private void validerLinventaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text == "<Inventaire stock de :>")
                {
                    return;
                }
                int idInventaire;
                if (Int32.TryParse(txtNoInv.Text, out idInventaire))
                {
                }
                else
                {
                    return;
                }
                if (dgvProduit.Rows.Count > 0)
                    if (MonMessageBox.ShowBox("Voulez vous valider ces données?", "Confirmation", "confirmation.png") == "1")
                    {
                        if (ConnectionClass.ValiderInventaire(dgvProduit, comboBox2.Text,idInventaire))
                        {
                            ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Validation de l'inventaire  " +idInventaire, this.Name);
                            MonMessageBox.ShowBox("Données validés avec succés", "Affirmation", "affirmation.png");
                            dgvProduit.Rows.Clear();
                            dgvProduit.ReadOnly = true;
                        }
                    }
            }
            catch { }
        }

        private void ajouterUnProduitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MedicamentFrm.ShowBox() == "1")
            {
                //dataGridView1.Rows.Add(
                //            MedicamentFrm.codeBarre,
                //            MedicamentFrm.designation,
                //            0,
                //           MedicamentFrm.prixAchat,
                //           MedicamentFrm.prixVente,
                //           MedicamentFrm.quantiteAlerte,
                //           txtEmplacement.Text,
                //           0,
                //           MedicamentFrm.datePeremption
                //                           ); 
                label6.Text = dgvProduit.Rows.Count.ToString();
            }
        }

        private void voirDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvProduit.SelectedRows.Count > 0)
            {
                MedicamentFrm.codeBarre = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                if (MedicamentFrm.ShowBox()=="1")
                {
                    //dataGridView1.SelectedRows[0].Cells[1].Value = MedicamentFrm.designation;
                    //dataGridView1.SelectedRows[0].Cells[3].Value = MedicamentFrm.prixAchat;
                    //dataGridView1.SelectedRows[0].Cells[4].Value = MedicamentFrm.prixVente;
                    //dataGridView1.SelectedRows[0].Cells[5].Value = MedicamentFrm.quantiteAlerte;
                    //dataGridView1.SelectedRows[0].Cells[8].Value = MedicamentFrm.datePeremption;
                }
            }
        }

        private void imprimerInventaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";
                var dateInv = DateTime.Now; int id;
                if (Int32.TryParse(txtNoInv.Text, out id))
                {
                }
                var liste = from l in ConnectionClass.ListeInventaire()
                            where l.IDInventaire == id
                            select l.DateInventaire;
                foreach (var d in liste)
                    dateInv = d;

                sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "Inventaire_imprimé_le_" + date + ".pdf";
                //_inventaireStock = AppCode.Impression.ImprimerInventaireStockPage(dataGridView1, 0, dateInv);
                //printPreviewDialog1.ShowDialog();
                var total = 0.0;
                for (var p = 0; p < dgvProduit.Rows.Count; p++)
                {
                  if(!string.IsNullOrEmpty(dgvProduit.Rows[p].Cells[0].Value.ToString()))
                    total += double.Parse(dgvProduit.Rows[p].Cells[2].Value.ToString()) *
                         double.Parse(dgvProduit.Rows[p].Cells[4].Value.ToString());
                }
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //string inputImage1 = @"cdali";
                    var index = dgvProduit.Rows.Count / 45;
                    for (var i = 0; i <= index; i++)
                    {
                        if (i * 45 < dgvProduit.Rows.Count)
                        {
                            var _inventaireStock = AppCode.Imprimer.ImprimerInventaireStockPage(dgvProduit,comboBox2.Text, i, dateInv,total, true);

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

            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtEmplacement_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtNoInv.Text != "")
                {
                    var listeProduit = from i in ConnectionClass.ListeDetailsInventaire()
                                       join p in ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                                       on i.IDProduit equals p.NumeroMedicament
                                       where i.IDInventaire == Convert.ToInt32(txtNoInv.Text)
                                       where i.Quantite > 0
                                       where i.Emplacement.StartsWith(txtEmplacement.Text , StringComparison.CurrentCultureIgnoreCase)
                                       orderby p.Designation
                                       select new
                                       {i.IndexDetail,
                                           p.NumeroMedicament,
                                           p.NomMedicament,
                                           i.Quantite,
                                           i.PrixPublic,
                                           i.PrixCession,
                                            i.IDInventaire,
                                           i.QuantiteAvant,
                                           i.Emplacement,
                                           p.DateExpiration,
                                           i.ID
                                       };

                    dgvProduit.Rows.Clear();

                    foreach (var p in listeProduit)
                    {
                        var ecart = "";
                        if (p.Quantite - p.QuantiteAvant >= 0)
                        {
                            ecart = "+" + (p.Quantite - p.QuantiteAvant);
                        }
                        else
                        {
                            ecart = (p.Quantite - p.QuantiteAvant).ToString();
                        }

                        dgvProduit.Rows.Add(
                                            p.NumeroMedicament,
                                            p.NomMedicament.ToUpper(),
                                            p.PrixCession,
                                            p.PrixPublic,
                                            p.Quantite,
                                            p.QuantiteAvant,
                                            ecart,
                                            p.DateExpiration.ToShortDateString(),
                                             p.Emplacement,  p.ID

                                        );
                    }
                    label6.Text = dgvProduit.Rows.Count.ToString();
                }
            }
            catch (Exception Exception) { MonMessageBox.ShowBox("", Exception); }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(txtNoInv.Text))
                    {
                        ListeProduitFrm.indexRecherche = txtNomProduit.Text;
                        txtNomProduit.Text = "";
                        var emplacement = " ";
                        if (!string.IsNullOrEmpty(txtEmplacement.Text))
                        {
                            emplacement = txtEmplacement.Text;
                        }
                        ListeProduitFrm.state = "0";
                        if (ListeProduitFrm.ShowBox() == "1")
                        {
                            var listeMedicament = ConnectionClass.ListeDesMedicamentParCode(ListeProduitFrm.numeroProduit);
                            foreach (var p in listeMedicament)
                            {
                                if (ListeProduitFrm.indexDetail == "1")
                                {
                                    indexDetail = "1";
                                    numeroProduit = p.NumeroMedicament;
                                    txtNomProduit.Text = p.NomMedicament.ToUpper() + " DETAIL";
                                    txtStock.Text = "0";
                                    txtPrixAchat.Text = ((int)p.PrixAchat / p.NombreBoite).ToString();
                                    txtPrixVente.Text = p.PrixVenteDetail.ToString();
                                    dateTimePicker1.Value = p.DateExpiration;
                                    txtAncien.Text = p.NombreDetail.ToString();
                                    if (p.NombreDetail >= 0)
                                    {
                                        txtEcart.Text = "+" + p.NombreDetail;
                                    }
                                    txtPrixVente.Focus();
                                }
                                else
                                {
                                    indexDetail = "0";
                                    numeroProduit = p.NumeroMedicament;
                                    txtNomProduit.Text = p.NomMedicament.ToUpper();
                                    txtStock.Text = "0";
                                    txtPrixAchat.Text = p.PrixAchat.ToString();
                                    txtPrixVente.Text = p.PrixVente.ToString();
                                    dateTimePicker1.Value = p.DateExpiration;
                                    if (comboBox2.Text == "Pharmacie")
                                    {
                                        txtAncien.Text = p.Quantite.ToString();
                                    }
                                    else
                                    {
                                        txtAncien.Text = p.GrandStock.ToString();
                                    }
                                    if (p.Quantite >= 0)
                                    {
                                        txtEcart.Text = "+" + p.Quantite;
                                    }
                                    txtPrixVente.Focus();
                                }
                            }

                        }
                        else if (e.KeyCode == Keys.Right)
                        {
                            txtPrixVente.Focus();
                        }
                    }
                }                
            }
            catch { }
        }
        private void txtPrixVente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal prixVente, taux;
                if (decimal.TryParse(txtPrixVente.Text, out prixVente))
                {
                    if (decimal.TryParse(txtTaux.Text, out taux) && checkBox1.Checked)
                    {
                        var prixAchat = prixVente / (1 + taux / 100);
                        txtPrixAchat.Text = Math.Round(prixAchat).ToString();
                        txtStock.Focus();
                    }
                    else
                    {
                        txtStock.Focus();
                    }
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtNomProduit.Focus();
            }
        }

        private void txtPrixAchat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {   decimal prixAchat;
                if (decimal.TryParse(txtPrixAchat.Text, out prixAchat))
                {
                    txtStock.Focus();
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtPrixVente.Focus();
            }
        }

        private void txtStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decimal prixAchat;
                if (decimal.TryParse(txtStock.Text, out prixAchat))
                {
                    dateTimePicker1.Focus();
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtPrixVente.Focus();
            }
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int qte, ancienQte;
                if (Int32.TryParse(txtStock.Text, out qte) && Int32.TryParse(txtAncien.Text, out ancienQte))
                {
                    var ecart = qte - ancienQte;
                    if (ecart >= 0)
                    {
                        txtEcart.Text = "+" + ecart;
                    }
                    else
                    {
                        txtEcart.Text = ecart.ToString(); ;
                    }
                }
            }
            catch { }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    double prixAchat, prixVente; int qte, idInventaire;
                    if (!string.IsNullOrEmpty(txtNomProduit.Text))
                    {
                        if (double.TryParse(txtPrixAchat.Text, out prixAchat))
                        {
                            if (double.TryParse(txtPrixVente.Text, out prixVente))
                            {
                                if (Int32.TryParse(txtStock.Text, out qte))
                                {
                                    if (Int32.TryParse(txtNoInv.Text, out idInventaire) && idInventaire > 0)
                                    {
                                        var flag = false;

                                        var produit = new Inventaire();
                                        produit.IDProduit = numeroProduit;
                                        produit.PrixCession = prixAchat;
                                        produit.PrixPublic = prixVente;
                                        produit.Quantite = qte;
                                        produit.Emplacement = txtEmplacement.Text;
                                        produit.IndexDetail = indexDetail;
                                        produit.Ordre = dgvProduit.Rows.Count + 1;
                                        produit.IDInventaire = idInventaire;

                                        if (dgvProduit.Rows.Count > 0)
                                        {
                                            foreach (DataGridViewRow dgvRow in dgvProduit.Rows)
                                            {
                                                if (dgvRow.Cells[0].Value.ToString().Equals(numeroProduit) && dgvRow.Cells[1].Value.ToString().Equals(txtNomProduit.Text))
                                                {
                                                    if (ConnectionClass.EnregistrerInventaire(produit))
                                                    {
                                                        flag = true;
                                                        dgvRow.Cells[2].Value = txtPrixAchat.Text;
                                                        dgvRow.Cells[3].Value = txtPrixVente.Text;
                                                        dgvRow.Cells[4].Value = txtStock.Text;
                                                        dgvRow.Cells[6].Value = txtEcart.Text;
                                                        dgvRow.Cells[5].Value = txtAncien.Text;
                                                        dgvRow.Cells[8].Value = txtEmplacement.Text;
                                                        dgvRow.Cells[7].Value = dateTimePicker1.Value.ToShortDateString();


                                                        txtNomProduit.Text = "";
                                                        txtPrixAchat.Text = "";
                                                        txtPrixVente.Text = "";
                                                        txtStock.Text = "";
                                                        txtEcart.Text = "";
                                                        txtAncien.Text = "";
                                                        txtNomProduit.Focus();
                                                        ConnectionClass.ModifierLadateMedicament(produit.IDProduit, dateTimePicker1.Value);
                                                        dateTimePicker1.Value = DateTime.Now;
                                                        txtNomProduit.Focus();

                                                    }
                                                }
                                            }
                                                if (!flag)
                                                {
                                                    if (ConnectionClass.EnregistrerInventaire(produit))
                                                    {
                                                        dgvProduit.Rows.Add
                                                            (
                                                                produit.IDProduit,
                                                                txtNomProduit.Text,
                                                                produit.PrixCession,
                                                                produit.PrixPublic,
                                                                produit.Quantite,
                                                                txtAncien.Text,
                                                                txtEcart.Text,
                                                                dateTimePicker1.Value.ToShortDateString(),
                                                                txtEmplacement.Text, indexDetail, produit.Ordre
                                                            );

                                                        txtNomProduit.Text = "";
                                                        txtPrixAchat.Text = "";
                                                        txtPrixVente.Text = "";
                                                        txtStock.Text = "";
                                                        txtEcart.Text = "";
                                                        txtAncien.Text = "";
                                                        txtNomProduit.Focus();
                                                        ConnectionClass.ModifierLadateMedicament(produit.IDProduit, dateTimePicker1.Value);
                                                        dateTimePicker1.Value = DateTime.Now;
                                                    }
                                                }
                                            
                                        }
                                        else
                                        {
                                            if (ConnectionClass.EnregistrerInventaire(produit))
                                            {
                                                dgvProduit.Rows.Add
                                                          (
                                                              produit.IDProduit,
                                                              txtNomProduit.Text,
                                                              produit.PrixCession,
                                                              produit.PrixPublic,
                                                              produit.Quantite,
                                                              txtAncien.Text,
                                                              txtEcart.Text,
                                                              dateTimePicker1.Value.ToShortDateString(),
                                                              txtEmplacement.Text, indexDetail
                                                          );
                                                txtNomProduit.Text = "";
                                                txtPrixAchat.Text = "";
                                                txtPrixVente.Text = "";
                                                txtStock.Text = "";
                                                txtEcart.Text = "";
                                                txtAncien.Text = "";
                                                txtNomProduit.Focus();
                                                ConnectionClass.ModifierLadateMedicament(produit.IDProduit, dateTimePicker1.Value);
                                                dateTimePicker1.Value = DateTime.Now;
                                            }
                                        }
                                        textBox3.Text = "Nbre produit  : " + dgvProduit.Rows.Count;
                                    }
                                }
                                else
                                {
                                    txtStock.Focus();
                                    txtStock.BackColor = Color.Red;
                                }
                            }
                            else
                            {
                                txtPrixVente.Focus();
                                txtPrixVente.BackColor = Color.Red;
                            }
                        }
                        else
                        {
                            txtPrixAchat.Focus();
                            txtPrixAchat.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        txtNomProduit.Focus();
                    }


                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        
    }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {

                    groupBox6.Visible = false;
                    txtNoInv.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    dgvProduit.Rows.Clear();
                    dgvProduit.ReadOnly = false;
                    comboBox2.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                    inventaire = true;
                    if (txtNoInv.Text != "")
                    {
                        var listeGroupe = ConnectionClass.ListeGroupe();
                        foreach (var gr in listeGroupe)
                        {
                            dgvProduit.Rows.Add(
                                       "", gr.Groupe, "", "", "", "", "", "", "", ""
                                       );
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                            var listeFamille = ConnectionClass.ListeDesFamille().Where(f => f.NombreDetail == gr.CodeFamille);

                            foreach (var pp in listeFamille)
                            {

                                var listeProduit = from i in ConnectionClass.ListeDetailsInventaire()
                                                   join p in ConnectionClass.ListeDesMedicamentsRechercherParFamille(pp.Designation)
                                                   on i.IDProduit equals p.NumeroMedicament
                                                   where i.IDInventaire == Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString())
                                                   //where i.Quantite > 0
                                                   orderby p.NomMedicament
                                                   select new
                                                   {
                                                       p.NumeroMedicament,
                                                       p.NomMedicament,
                                                       i.Quantite,
                                                       i.PrixPublic,
                                                       i.PrixCession,
                                                       ID = i.IDInventaire,
                                                       p.QuantiteAlerte,
                                                       i.QuantiteAvant,
                                                       i.Emplacement,
                                                       p.DateExpiration,
                                                       i.Ordre
                                                   };

                                if (listeProduit.Count() > 0)
                                {
                                    dgvProduit.Rows.Add(
                                            "", pp.Designation, "", "", "", "", "", "", "", ""
                                            );

                                    foreach (var p in listeProduit)
                                    {
                                        var ecart = "";
                                        if (p.Quantite - p.QuantiteAvant >= 0)
                                        {
                                            ecart = "+" + (p.Quantite - p.QuantiteAvant);
                                        }
                                        else
                                        {
                                            ecart = (p.Quantite - p.QuantiteAvant).ToString();
                                        }
                                        var designation = p.NomMedicament;

                                        if (p.Quantite > 0 || p.QuantiteAvant > 0)
                                        {
                                            dgvProduit.Rows.Add(
                                                            p.NumeroMedicament,
                                                            designation.ToUpper(),
                                                            p.PrixCession,
                                                            p.PrixPublic,
                                                            p.Quantite,
                                                              p.QuantiteAvant,
                                                             ecart,
                                                           p.DateExpiration.ToShortDateString(),
                                                           p.Emplacement,
                                                           p.Ordre
                                                        );
                                        }
                                    }
                                }
                            }
                        }
                        label6.Text = dgvProduit.Rows.Count.ToString();
                    }
                }
                else
                {
                    groupBox6.Visible = false;
                    txtNoInv.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    dgvProduit.Rows.Clear();
                    dgvProduit.ReadOnly = false;
                    comboBox2.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                    inventaire = true;
                    if (txtNoInv.Text != "")
                    {
                        var listeGroupe = ConnectionClass.ListeGroupe();
                        foreach (var gr in listeGroupe)
                        {
                            dgvProduit.Rows.Add(
                                       "", gr.Groupe, "", "", "", "", "", "", "", ""
                                       );
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.AliceBlue;
                            var listeFamille = ConnectionClass.ListeDesFamille().Where(f => f.NombreDetail == gr.CodeFamille);

                            foreach (var pp in listeFamille)
                            {

                                var listeProduit = from i in ConnectionClass.ListeDetailsInventaire()
                                                   join p in ConnectionClass.ListeDesMedicamentsRechercherParFamille(pp.Designation)
                                                   on i.IDProduit equals p.NumeroMedicament
                                                   where i.IDInventaire == Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString())
                                                   //where i.Quantite > 0
                                                   orderby p.NomMedicament
                                                   select new
                                                   {
                                                       p.NumeroMedicament,
                                                       p.NomMedicament,
                                                       i.Quantite,
                                                       i.PrixPublic,
                                                       i.PrixCession,
                                                       ID = i.IDInventaire,
                                                       p.QuantiteAlerte,
                                                       i.QuantiteAvant,
                                                       i.Emplacement,
                                                       p.DateExpiration,
                                                       i.Ordre
                                                   };

                                if (listeProduit.Count() > 0)
                                {
                                    dgvProduit.Rows.Add(
                                            "", pp.Designation, "", "", "", "", "", "", "", ""
                                            );

                                    foreach (var p in listeProduit)
                                    {
                                        var ecart = "";
                                        if (p.Quantite - p.QuantiteAvant >= 0)
                                        {
                                            ecart = "+" + (p.Quantite - p.QuantiteAvant);
                                        }
                                        else
                                        {
                                            ecart = (p.Quantite - p.QuantiteAvant).ToString();
                                        }
                                        var designation = p.NomMedicament;

                                          dgvProduit.Rows.Add(
                                                            p.NumeroMedicament,
                                                            designation.ToUpper(),
                                                            p.PrixCession,
                                                            p.PrixPublic,
                                                            p.Quantite,
                                                              p.QuantiteAvant,
                                                             ecart,
                                                           p.DateExpiration.ToShortDateString(),
                                                           p.Emplacement,
                                                           p.Ordre
                                                        );
                                        
                                    }
                                }
                            }
                        }
                        label6.Text = dgvProduit.Rows.Count.ToString();
                    }
                }
                   
            }
                    
            catch (Exception Exception) { MonMessageBox.ShowBox("", Exception); }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.SelectedRows.Count > 0)
                {
                    //indexDetail = dgvProduit.SelectedRows[0].Cells[9].Value.ToString();
                    //id = Int32.Parse(dgvProduit.SelectedRows[0].Cells[10].Value.ToString());
                    numeroProduit = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                    txtNomProduit.Text = dgvProduit.SelectedRows[0].Cells[1].Value.ToString();
                    txtPrixAchat.Text = dgvProduit.SelectedRows[0].Cells[2].Value.ToString();
                    txtPrixVente.Text = dgvProduit.SelectedRows[0].Cells[3].Value.ToString();
                    txtStock.Text = dgvProduit.SelectedRows[0].Cells[4].Value.ToString();
                    txtEcart.Text = dgvProduit.SelectedRows[0].Cells[6].Value.ToString();
                    txtAncien.Text = dgvProduit.SelectedRows[0].Cells[5].Value.ToString();
                    txtEmplacement.Text = dgvProduit.SelectedRows[0].Cells[8].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dgvProduit.SelectedRows[0].Cells[7].Value.ToString());
                    txtStock.Focus();
                }
            }
            catch { }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void nouvelleInventaireToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void evaluerLeResultatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";
                var dateInv = DateTime.Now; int id;
                if (Int32.TryParse(txtNoInv.Text, out id))
                {
                }
                var liste = from l in ConnectionClass.ListeInventaire()
                            where l.IDInventaire == id
                            select l.DateInventaire;
                foreach (var d in liste)
                    dateInv = d;

                sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "Inventaire_imprimé_le_" + date + ".pdf";
                //_inventaireStock = AppCode.Impression.ImprimerInventaireStockPage(dataGridView1, 0, dateInv);
                //printPreviewDialog1.ShowDialog();
                var total = 0.0;
                for (var p = 0; p < dgvProduit.Rows.Count; p++)
                {
                    if (!string.IsNullOrEmpty(dgvProduit.Rows[p].Cells[0].Value.ToString()))
                        total += double.Parse(dgvProduit.Rows[p].Cells[3].Value.ToString()) *
                             double.Parse(dgvProduit.Rows[p].Cells[4].Value.ToString());
                }
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //string inputImage1 = @"cdali";
                    var index = dgvProduit.Rows.Count / 45;
                    for (var i = 0; i <= index; i++)
                    {
                        if (i * 45 < dgvProduit.Rows.Count)
                        {
                            var _inventaireStock = AppCode.Imprimer.ImprimerEvaluationInventaireStockPage(dgvProduit, comboBox2.Text, i, dateInv, total, true);

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

            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void imprimerToolStripMenuItem_Click(object sender, EventArgs e)
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
