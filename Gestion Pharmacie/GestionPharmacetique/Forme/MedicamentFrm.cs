using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;
using System.Linq;

namespace GestionPharmacetique.Forme
{
    public partial class MedicamentFrm : Form
    {
        public MedicamentFrm()
        {
            InitializeComponent();
        }

      
       private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void MedicamentFrm_Paint(object sender, PaintEventArgs e)
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
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        #region ProprieteMedicament

        private string  _codeMedicament;
        private string _nomMedicament;
        private int _codeFamille;
        private decimal _prixAchat;
        private decimal _prixVente;
        private DateTime _dateExpiration;
        private int  _quantite,grand_stock ;
        int _qteAlerte;
        #endregion
        //form load
        private void MedicamentFrm_Load(object sender, EventArgs e)
        {
            try
            {
                txtNumeroMedicament.Text = codeBarre;
                btnFermer.Location = new Point(Width - 43, 2);
                clDesidgnation.Width = dgvProduit.Width / 3;
                groupBox2.Location = new Point(dgvProduit.Width + 12, groupBox2.Location.Y);
                ListeDesMedicaments();
                //charge le combobox des donnees famille produit
                comboBox1.Items.Clear();
                var  listeFamille = ConnectionClass.ListeDesFamille();
                cmbTrie.Items.Add("<<Toutes les données>>");
                foreach (var famille in listeFamille)
                {
                    cmbTrie.Items.Add(famille.Designation);
                    comboBox1.Items.Add(famille.Designation.ToUpper());
                }
                if (Form1.typeUtilisateur != "admin")
                {
                    //txtQte.Enabled = false;
                    //txtGrdStock.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }
      
        //ajouter un nouveau produit
        private void btnAJouterMedicament_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValiderLesChamps())
                {

                    var listeFamille = from f in ConnectionClass.ListeDesFamille()
                                       where f.Designation.ToUpper() == comboBox1.Text.ToUpper()
                                       select f.CodeFamille;
                    foreach (var  f in listeFamille)
                        _codeFamille = f;
                    var  produit = new Medicament();
                    produit.NomMedicament = txtNomMedicament.Text;
                    produit.DateExpiration = dateTimePicker1.Value.Date;
                    produit.NumeroMedicament = txtNumeroMedicament.Text;
                    produit.PrixAchat = _prixAchat;
                    produit.PrixVente = _prixVente;
                    produit.QuantiteAlerte = _qteAlerte;
                    produit.Quantite = _quantite;
                    produit.CodeFamille = _codeFamille;
                    produit.GrandStock = grand_stock;
                    if( ConnectionClass.EnregistrerMedicament(produit))
                    {
                        ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, 
                            "enregistrement produit  medicament  Stock pharmacie " +produit.Quantite + " stock depot "+produit.GrandStock, this.Name);
                  
                        if (etat != "1")
                        {
                            designation = _nomMedicament;
                            codeBarre = _codeMedicament;
                            prixAchat = _prixAchat;
                            prixVente = _prixVente;
                            quantite = _quantite;
                            quantiteAlerte = _qteAlerte;
                            codeFamille = _codeFamille;
                            datePeremption = _dateExpiration;
                            btnClick = "1";
                          
                            Dispose();
                        }
                        ViderLEsChamps();
                        ListeDesMedicaments();
                        //txtNumeroMedicament.Focus();
                        txtRecherche.Focus();
                        txtRecherche.SelectAll();
                    }
                  
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Insertion produit", exception);
            }
        }

        //vider les champs
        private void ViderLEsChamps()
        {
            txtGrdStock.Text = "";
            dataGridView1.Rows.Clear();
            txtNomMedicament.Text = "";
            txtNumeroMedicament.Text = "";
            txtPrixAchat.Text = "";
            txtPrVente.Text = "";
            txtQte.Text = "";
            txtSeuil.Text = "";
            
        }

        //valider les champs
        private bool ValiderLesChamps()
        {
           if (!string.IsNullOrEmpty(txtNomMedicament.Text))
            {
                if (!string.IsNullOrEmpty(txtNumeroMedicament.Text ))
                {
                    if ( decimal.TryParse(txtPrVente.Text, out _prixVente) && _prixAchat >= 0 && _prixVente >=0)
                    {
                        
                            if (!string.IsNullOrEmpty(comboBox1.Text))
                            {
                                if (Int32.TryParse(txtQte.Text, out _quantite) )
                                {
                                }else
                                {
                                    _quantite=0;
                                }

                                if (Int32.TryParse(txtGrdStock.Text, out grand_stock))
                                {
                                }
                                else
                                {
                                    grand_stock = 0;
                                }
                                  
                                    if (Int32.TryParse(txtSeuil.Text, out _qteAlerte) &&_qteAlerte >=0)
                                    {
                                        if (decimal.TryParse(txtPrixAchat.Text, out _prixAchat))
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            _prixAchat = 0;
                                            return true;
                                        }
                                    }
                                    else
                                    {
                                        _qteAlerte = 0;
                                        return true;
                                    }
                                    
                            }
                            else
                            {
                                MonMessageBox.ShowBox("Veuillez selectionner la categorie du materiel puis reéssayer.", "Erreur saisie",
           "erreur.png");
                                return false;
                            }
                       
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer des chiffres valides pour les prix d'achat et de vente puis reéssayer.", "Erreur saisie",
           "erreur.png");
                        return false;
                    }
                } 
                else 
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le code du produit puis reéssayer.", "Erreur saisie",
            "erreur.png");
                    return  false;
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez entrer le nom du produit puis reéssayer.", "Erreur saisie",
                    "erreur.png");
                return  false;
            }
        }

        
        //supprimer les donnees de produit
        private void button2_Click(object sender, EventArgs e)
        {
            try
            { string codeMedic = txtNumeroMedicament.Text;
               if( MonMessageBox.ShowBox("Voulez vous supprimer ls données du produit numéro " + codeMedic
                    + " ?", "Confirmation" ,"confirmation.png" )== "1")
                   if (ConnectionClass.SupprimerMedicament(codeMedic))
                   {                             
                       ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Suppression  du produit" +txtNomMedicament.Text +" de la liste", this.Name);
                  
                       MonMessageBox.ShowBox("La suppresion des données a été effectuée avec succés",
                       "Information suppression", "affirmation.png");
                       ViderLEsChamps();
                       ListeDesMedicaments();
                   }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("suppression produit", exception);
            }
        }
        //liste des produit
        private void ListeDesMedicaments()
        {
            try
            {
                dgvProduit.Rows.Clear();
                List<Medicament> listeMedicament = new List<Medicament>();
                //if (cmbTrie.Text == "Nom" || string.IsNullOrEmpty(cmbTrie.Text))
                {

                    listeMedicament = ConnectionClass.ListeDesMedicamentsRechercherParNom(txtRecherche.Text);
                    label11.Text = "Nombre de produit : " + listeMedicament.Count;
                    foreach (Medicament produit in listeMedicament)
                    {
                        
                        dgvProduit.Rows.Add(
                            produit.NumeroMedicament.ToUpper(), produit.Designation.ToUpper(),
                            produit.NomMedicament.ToUpper(), produit.PrixAchat.ToString().ToUpper(),
                            produit.PrixVente.ToString(), produit.Quantite,produit.GrandStock,
                            produit.QuantiteAlerte,
                            produit.DateExpiration.ToShortDateString(),
                            produit.Photo,produit.NombreBoite
                        );

                        if (produit.NombreBoite > 0 && produit.PrixVenteDetail>0)
                        {
                            dgvProduit.Rows.Add(
                            produit.NumeroMedicament.ToUpper(), produit.Designation.ToUpper(),
                            produit.NomMedicament.ToUpper() + " DETAIL",(int) produit.PrixAchat/produit.NombreBoite,0,
                            produit.PrixVenteDetail.ToString(), produit.NombreDetail,
                            0,
                            produit.DateExpiration.ToShortDateString(),
                            produit.Photo, produit.NombreBoite
                        );
                        }
                    }
                    foreach (DataGridViewRow row in dgvProduit.Rows)
                    {
                        var stock = Convert.ToDouble(row.Cells[5].Value.ToString()) + Convert.ToDouble(row.Cells[6].Value.ToString());
                        var stockMinimal = Convert.ToDouble(row.Cells[7].Value.ToString());
                        //var nbreEnBoite = Convert.ToDouble(row.Cells[9].Value.ToString());

                        if (stock <= 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (stock > 0 && stock < stockMinimal)
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }
         
                    }
                }  
               
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
            }
        }

        private void btnInsererImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNomMedicament.Text) && !txtNomMedicament.Text.ToUpper().Contains("DETAIL"))
                {
                    DetailProFrm.quantiteInitiale = Int32.Parse(txtQte.Text);
                    DetailProFrm.numeroProduit = txtNumeroMedicament.Text;
                    DetailProFrm.medicament = txtNomMedicament.Text;
                    if (DetailProFrm.ShowBox())
                    {
                        txtRecherche.Text = DetailProFrm.medicament.Substring(0,3);
                        ListeDesMedicaments();
                    }
                }
            }

            catch (Exception ex) { MonMessageBox.ShowBox("Transfert de l'image", ex); }

        }

       private void button5_Click(object sender, EventArgs e)
        {
            FamilleMedicamentFrm frm = new FamilleMedicamentFrm();
            frm.ShowDialog();
            

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            var   listeFamille = ConnectionClass.ListeDesFamille();
            comboBox1.Items.Clear();
            foreach (var  famille in listeFamille)
            {
                comboBox1.Items.Add(famille.Designation);
            }
        }
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ListeDesMedicaments();
            dgvVente_CellContentClick(null, null);
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                comboBox1.Items.Clear();
                var  listeFamille = ConnectionClass.ListeDesFamille();
                foreach (Medicament famille in listeFamille)
                {
                    comboBox1.Items.Add(famille.Designation.ToUpper());
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtNumeroMedicament_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNumeroMedicament.Text))
                {
                    var listeMedicament = ConnectionClass.ListeDesMedicamentParCode(txtNumeroMedicament.Text);
                    if (listeMedicament.Count > 0)
                    {
                        foreach (Medicament produit in listeMedicament)
                        {
                            var dt = ConnectionClass.ListeStock(txtNumeroMedicament.Text);
                            var liste = from f in ConnectionClass.ListeDesFamille()
                                        where f.CodeFamille == produit.CodeFamille
                                        select f.Designation;
                            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                            foreach (var p in liste)
                            {
                                comboBox1.Text = p.ToUpper();
                            }
                            if (dt.Rows.Count > 0)
                            {
                                txtQte.Text = dt.Rows[0].ItemArray[2].ToString();
                            }
                            else
                            {
                                txtQte.Text = "0";
                            }

                            txtNomMedicament.Text = produit.NomMedicament.ToUpper();
                            var listeLot = ConnectionClass.ListeDesLotsProduitsParCode(produit.NumeroMedicament);
                            dataGridView1.Rows.Clear();
                            foreach (Medicament p in listeLot)
                            {
                                dataGridView1.Rows.Add(p.CodeFamille, p.NoLot, p.DateExpiration.ToShortDateString(), p.GrandStock);
                            }
                            if (listeLot.Count > 0)
                            {
                                dateTimePicker1.Value = listeLot[0].DateExpiration;
                            }
                            dateTimePicker1.Value = produit.DateExpiration;
                            txtPrixAchat.Text = produit.PrixAchat.ToString().ToUpper();
                            txtPrVente.Text = produit.PrixVente.ToString().ToUpper();
                            txtSeuil.Text = produit.QuantiteAlerte.ToString().ToUpper();

                        }
                    }
                    else
                    {
                        dateTimePicker1.Value = DateTime.Now;
                        txtNomMedicament.Text = ""; ;
                        dataGridView1.Rows.Clear();
                        txtPrixAchat.Text = "";
                        txtPrVente.Text = "";
                        txtQte.Text = "";
                        comboBox1.Text = "";
                        txtSeuil.Text = "";
                        txtDosage.Text = "";
                        txtGrdStock.Text = "";
                        dataGridView1.Rows.Clear();
                        txtNoLot.Text = "";
                        txtQteLot.Text = "";
                    }
                }
            }
            catch { }
        }
        private void MedicamentFrm_Resize(object sender, EventArgs e)
        {
            groupBox2.Location = new Point((Width-groupBox2.Width)/2, groupBox2.Location.Y);
        }


        private void dgvVente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvProduit.SelectedRows.Count > 0)
                {
                    txtNumeroMedicament.Text = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                    txtNomMedicament.Text = dgvProduit.SelectedRows[0].Cells[2].Value.ToString();
                    //txtDescription.Text = dgvVente.SelectedRows[0].Cells[5].Value.ToString();
                    txtPrVente.Text = dgvProduit.SelectedRows[0].Cells[4].Value.ToString();
                    txtPrixAchat.Text = dgvProduit.SelectedRows[0].Cells[3].Value.ToString();
                    txtQte.Text = dgvProduit.SelectedRows[0].Cells[5].Value.ToString();
                    txtGrdStock.Text = dgvProduit.SelectedRows[0].Cells[6].Value.ToString();
                    comboBox1.Text = dgvProduit.SelectedRows[0].Cells[1].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dgvProduit.SelectedRows[0].Cells[8].Value.ToString());
                    txtSeuil.Text = dgvProduit.SelectedRows[0].Cells[7].Value.ToString();
                    if (ConnectionClass.DoseParUnite(txtNumeroMedicament.Text) > 0)
                    {
                        txtDosage.Visible = true;
                        txtDosage.Text = ConnectionClass.DoseParUnite(txtNumeroMedicament.Text).ToString();
                    }
                    else { txtDosage.Visible = false; }
                }
            }
            catch (Exception)
            {
            }
        }

        private void cmbTrie_SelectedIndexChanged(object sender, EventArgs e)
        {
            try{
            dgvProduit.Rows.Clear();
                List<Medicament> listeMedicament = new List<Medicament>();
                if (cmbTrie.Text == "<<Toutes les données>>")
                {
                    listeMedicament = ConnectionClass.ListeDesMedicamentsRechercherParFamille("");
                }
                else
                {
                    listeMedicament = ConnectionClass.ListeDesMedicamentsRechercherParFamille(cmbTrie.Text);
                }
                    label11.Text = "Nombre de produit : " + listeMedicament.Count;
                    foreach (Medicament produit in listeMedicament)
                    {
                        
                        dgvProduit.Rows.Add(
                            produit.NumeroMedicament.ToUpper(), produit.Designation.ToUpper(),
                            produit.NomMedicament.ToUpper(), produit.PrixAchat.ToString().ToUpper(),
                            produit.PrixVente.ToString(), produit.Quantite,produit.GrandStock,
                            produit.QuantiteAlerte,
                            produit.DateExpiration.ToShortDateString(),
                            produit.Photo,produit.NombreBoite
                        );
                        
                    }
                    foreach (DataGridViewRow row in dgvProduit.Rows)
                    {
                        var stock = Convert.ToDouble(row.Cells[5].Value.ToString()) + Convert.ToDouble(row.Cells[6].Value.ToString());
                        var stockMinimal = Convert.ToDouble(row.Cells[7].Value.ToString());
                        //var nbreEnBoite = Convert.ToDouble(row.Cells[9].Value.ToString());

                        if (stock <= 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (stock > 0 && stock < stockMinimal)
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }
         
                    }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
            }
        }

        private void txtRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvProduit.Focus();
            }
        }

        private void dgvVente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvVente_CellContentClick(null,null);

                txtNumeroMedicament.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                dgvVente_CellContentClick(null, null);
            }
        }

        private void txtNumeroMedicament_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                comboBox1.Focus();
                comboBox1.DroppedDown = true;
            } else if (e.KeyCode == Keys.Enter)
            {
                txtNomMedicament.Focus();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtNomMedicament.Focus();
        }

        private void btnCalculer_Click(object sender, EventArgs e)
        {
            indexCalcul = 1;
            txtTaux.Visible = true;
            txtTaux.Focus();
        }

        private void txtTaux_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    double taux, prixVente, prixAchat;
                    if (indexCalcul == 1)
                    {
                        if (Double.TryParse(txtPrVente.Text, out prixVente) && Double.TryParse(txtTaux.Text, out taux))
                        {

                            prixAchat = prixVente / (1 + taux / 100);
                            txtPrixAchat.Text = Math.Round(prixAchat).ToString();
                            txtTaux.Text = "";
                            txtTaux.Visible = false;
                            btnAjouter.Focus();
                        }
                        else
                        {
                            txtPrixAchat.Text = "";
                        }
                    }
                    else
                    {
                        if (Double.TryParse(txtPrixAchat.Text, out prixAchat) && Double.TryParse(txtTaux.Text, out taux))
                        {
                            prixVente = prixAchat * (1 + taux / 100);
                            txtPrVente.Text = Math.Round(prixVente).ToString();
                            txtTaux.Text = "";
                            txtTaux.Visible = false;
                            btnAjouter.Focus();
                        }
                        else
                        {
                            txtPrVente.Text = "";
                        }
                    }
                }
            }
            catch { }
        }

        public static MedicamentFrm frmMedic;
        public static string designation, btnClick, codeBarre;
        public static decimal prixAchat, prixVente;
        public static int quantite, quantiteAlerte, codeFamille;
        public static  DateTime datePeremption;
        public  string etat;
       
        public static string ShowBox()
        {
            frmMedic = new MedicamentFrm();
            frmMedic.ShowDialog();
            return btnClick;

        }
        int indexCalcul;
        private void button4_Click(object sender, EventArgs e)
        {
            indexCalcul = 2;
            txtTaux.Visible = true;
            txtTaux.Focus();
            //}
            //ConnectionClass.ModifierMedicament(dgvProduit);
        }

        private void txtRecherche_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Down)
            {
                dgvProduit.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNomMedicament.Focus();
            }
        }

        private void txtNomMedicament_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPrVente.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                comboBox1.Focus();
                comboBox1.DroppedDown = true;
            }
        }

        private void txtPrVente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(string.IsNullOrEmpty(txtPrixAchat.Text))
                {
                    txtPrixAchat.Text = "0";
                }
                if (!txtQte.Enabled)
                {
                    txtQte.Text = "0";
                    txtPrixAchat.Focus();
                }
                else
                {
                    txtPrixAchat.Text = "0";
                    txtQte.Focus();
                }
            }
          
        }

        private void txtPrixAchat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSeuil.Focus();
            }
        }

        private void txtQte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSeuil.Text = "0";
                txtSeuil.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                btnAjouter.Focus();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePicker1.Focus();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //if (Convert.ToDouble(txtPrixAchat.Text) > 0)
                    //{
                        btnAjouter.Focus();
                    //}
                    //else
                    //{
                    //    btnCalculer_Click(null, null);
                    //    txtTaux.Focus();
                    //}
                }
            }
            catch { }
        }

        private void dgvVente_Click(object sender, EventArgs e)
        {
            dgvVente_CellContentClick(null, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (SGDP.Formes.DateFrm.ShowBox())
                {
                    if(dgvProduit.SelectedRows.Count >0)
                    {
                        var designation = dgvProduit.SelectedRows[0].Cells[2].Value.ToString();
                        var listeMedicament = ConnectionClass.ListeParNomMedicaments(designation);

                        RechercheMedicamentIndiFrm frm = new RechercheMedicamentIndiFrm();
                        foreach (Medicament medicament in listeMedicament)
                        {
                            frm.lblCodeMed.Text = medicament.NumeroMedicament;
                            frm.lblCodeFamille.Text = medicament.CodeFamille.ToString();
                            frm.lblNomMedicament.Text = medicament.NomMedicament;
                            frm.lblPrixAchat.Text = medicament.PrixAchat.ToString();
                            frm.lblPrixVente.Text = medicament.PrixVente.ToString();
                            frm.lblDateExpiration.Text = medicament.Description;
                            frm.lblDateExpiration.Text = medicament.DateExpiration.ToShortDateString();
                            frm.lblQteStock.Text = medicament.Quantite.ToString();


                            System.Data.DataTable dt = ConnectionClass.ImageMedicament(frm.lblCodeMed.Text);
                            if (dt.Rows.Count > 0)
                            {
                                string imagePath = @"C:\\Dossier Pharmacie\\Image Medicament\\" + dt.Rows[0].ItemArray[0].ToString();

                                frm.pictureBox1.Image = (System.IO.File.Exists(imagePath))
                                    ? Image.FromFile(imagePath)
                                    : null;
                            }
                            var dataTableVente = ConnectionClass.MontantTotalDeVente("medicament.nom_medi", medicament.NomMedicament,SGDP.Formes.DateFrm.dateDebut,SGDP.Formes.DateFrm.dateFin);
                            var dataTableLivraison = ConnectionClass.MontantTotalDeLivraison("medicament.nom_medi", medicament.NomMedicament, SGDP.Formes.DateFrm.dateDebut, SGDP.Formes.DateFrm.dateFin);
                            if (dataTableLivraison.Rows.Count > 0)
                            {
                                frm.lblQteTotaleLivree.Text = dataTableLivraison.Rows[0].ItemArray[2].ToString();
                                frm.lblMotantTotalLivraison.Text = dataTableLivraison.Rows[0].ItemArray[3].ToString();
                            }
                            else
                            {
                                frm.lblQteTotaleLivree.Text = "0";
                                frm.lblMotantTotalLivraison.Text = "0";
                            }
                            if (dataTableVente.Rows.Count > 0)
                            {
                                frm.lblNreVendue.Text = dataTableVente.Rows[0].ItemArray[0].ToString();
                                frm.lblQteTotalVendue.Text = dataTableVente.Rows[0].ItemArray[2].ToString();
                                frm.lblMontantTotalVente.Text = dataTableVente.Rows[0].ItemArray[3].ToString();
                            }
                            else
                            {
                                frm.lblQteTotalVendue.Text = "0";
                                frm.lblMontantTotalVente.Text = "0";
                            }
                        }
                        if (listeMedicament.Count > 0)
                        {
                            frm.designation = designation;
                            frm.prix = double.Parse(dgvProduit.SelectedRows[0].Cells[4].Value.ToString());
                            frm.nbreDetaille = 0;// Int32.Parse(dgvProduit.SelectedRows[0].Cells[9].Value.ToString());
                            frm.dateDebut = SGDP.Formes.DateFrm.dateDebut;
                            frm.dateFin = SGDP.Formes.DateFrm.dateFin;
                            frm.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new FrmLabo();
            frm.numeroProduit = txtNumeroMedicament.Text;
            frm.ShowDialog();
            if (ConnectionClass.DoseParUnite(txtNumeroMedicament.Text) > 0)
            {
                txtDosage.Visible = true;
                txtDosage.Text = ConnectionClass.DoseParUnite(txtNumeroMedicament.Text).ToString();
            }
            else { txtDosage.Visible = false; }
        }

        private void button1_Click(object sender, EventArgs e)
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
                sfd.FileName = "Impression_listing_du_" + date + ".pdf";

                if (dgvProduit.Rows.Count > 0)
                {
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var Count = dgvProduit.Rows.Count / 45;
                        var titre = label8.Text;
                        for (var i = 0; i <= Count; i++)
                        {
                            if (i * 45 < dgvProduit.Rows.Count)
                            {
                                var _listeImpression = AppCode.ImprimerRaportVente.ImprimerListingProduit(dgvProduit, i);

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
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }
        

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        int idLot,index;
        private void button3_Click(object sender, EventArgs e)
        {
           try 
           {
               var totalStock = 0;
               for (var i = 0; i < dataGridView1.Rows.Count; i++)
               {
                   totalStock +=Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
               }
               if (idLot == 0)
               {
                   if (totalStock + Convert.ToInt32(txtQteLot.Text) > Convert.ToInt32(txtGrdStock.Text))
                   {
                       MonMessageBox.ShowBox("le stock des lots ne doit pas depasser le stock du depôt", "Erreur", "erreur.png");
                       return;
                   }
               }
               else
               {
                   if(dataGridView1.Rows.Count>0)
                   if ((totalStock + Convert.ToInt32(txtQteLot.Text) - Convert.ToInt32(dataGridView1.Rows[index].Cells[3].Value.ToString())) > Convert.ToInt32(txtGrdStock.Text))
                   {
                            MonMessageBox.ShowBox("le stock des lots ne doit pas depasser le stock du depôt", "Erreur", "erreur.png");
                       return;
                   }
               }
               if (!string.IsNullOrEmpty(txtNumeroMedicament.Text) && !string.IsNullOrEmpty(txtNoLot.Text))
               {
                   int qte;
                   if (Int32.TryParse(txtQteLot.Text, out qte))
                   {
                   }
                   else
                   {
                       MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour la quantité pour ce lot", "Erreur", "erreur.png");
                   }
                   var medicament = new Medicament();
                   medicament.NumeroMedicament = txtNumeroMedicament.Text;
                   medicament.DateExpiration = dateTimePicker1.Value.Date;
                   medicament.CodeFamille = idLot;
                   medicament.Quantite = qte;
                   medicament.NoLot = txtNoLot.Text;
                   dataGridView1.Rows.Clear();
                   if (ConnectionClass.EnregistrerLotDeProduit(medicament))
                   {
                       idLot = 0;
                       var listeLot = ConnectionClass.ListeDesLotsProduitsParCode(txtNumeroMedicament.Text);
                       foreach (Medicament p in listeLot)
                       {
                           txtQteLot.Text = "";
                           txtNoLot.Text = "";
                           dataGridView1.Rows.Add(p.CodeFamille, p.NoLot, p.DateExpiration.ToShortDateString(), p.GrandStock);
                       }
                       if (listeLot.Count > 0)
                       {
                           dateTimePicker1.Value = listeLot[0].DateExpiration;
                       }
                       ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Enregistrement du lot numero " + txtNoLot.Text + " du " + txtNomMedicament.Text, this.Name);
                   }
               }
	}
	catch (Exception)
	{
	}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==4)
            {
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                txtNoLot.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtQteLot.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                 idLot  = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                 index = dataGridView1.CurrentRow.Index;
            }
            else if (e.ColumnIndex ==5)
            {
                idLot = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                ConnectionClass.SupprimeDateExpiration(idLot);
               ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Suppression du lot " +dataGridView1.CurrentRow.Cells[1].Value.ToString()+ " du produit "+txtNomMedicament.Text, this.Name);
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                 
            }
        }
        
    }
}
