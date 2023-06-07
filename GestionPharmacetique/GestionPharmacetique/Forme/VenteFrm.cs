using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;
using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Linq;
namespace GestionPharmacetique.Forme
{
    public partial class VenteFrm : Form
    {
        public VenteFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.Black, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 1);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
      private void groupBox10_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ActiveCaption, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox10.Width - 1, this.groupBox10.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
         SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
         SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
        }
        private void VenteeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ActiveCaption, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ActiveCaption, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
 
         private void groupBox2_Paint_1(object sender, PaintEventArgs e)
        {
             Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, SystemColors.ControlLightLight, SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
       
        private void groupBox13_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 1);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox13.Width - 1, this.groupBox13.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, SystemColors.ControlLight, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

   
        public SpeechSynthesizer _synthesizer;

        private void VenteFrm_Load(object sender, EventArgs e)
        {
            //checkBox2.Checked = true;
            radioButton1.Checked = true;
            var widthControle = (groupBox3.Width - 180) / 4 - 3;
            groupBox4.Location = new Point(groupBox3.Width + 10, groupBox4.Location.Y);
            groupBox4.Width = 650;
            btnFermer.Location = new Point(Width - 40, 2);
            if (Form1.typeUtilisateur == "admin")
            {
                button3.Enabled = false;
                button2.Visible = true ;
                btnSolderCredit.Visible = true;
            }
            else
            {
                button2.Visible = false;
                btnSolderCredit.Visible = false;
            }
            clDesidgnation.Width = dgvVente.Width / 3 +60 +  2*dgvVente.Width / 8;
            clPrixTotal.Width = dgvVente.Width / 8;
            clPublic.Width = dgvVente.Width / 8;
            clQte.Width = dgvVente.Width / 8;
            clRemise.Visible=false;
          
            var dtEntrep = ConnectionClassClinique.ListeDesEntreprises();
             cmbConventione.Items.Add("");
            foreach (DataRow entrep in  dtEntrep.Rows)
            {
                cmbConventione.Items.Add(entrep.ItemArray[1].ToString().ToUpper());
            }
            txtRechercherProduit.Focus();
            timer1.Stop();
            timer1.Dispose();
        }
         int numVente;
        //enregistrer et imprimer
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
               
                //propriete du client
                string nomClient = null;

                var client = new Client();
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    client.NomClient = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                    client.Matricule = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    client.Telephone = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
                    client.Entreprise = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                    client.Age = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                    client.Sexe = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                    client.SousCouvert = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                }
                else if (dataGridView2.SelectedRows.Count == 0 && !string.IsNullOrEmpty(txtRecherche.Text))
                {
                    client.NomClient = txtRecherche.Text;
                    client.Matricule = "";
                    client.Entreprise = "";
                }
                else
                {
                    client.NomClient = "CLIENT COMPTANT";
                    client.Matricule = "";
                    client.Entreprise = "";
                }
                //client.NomClient = nomClient;
                decimal prixTotal = 0;
                //propriete ventetxt
                if (txtPrixTotal.Text.Contains(","))
                {
                    prixTotal += decimal.Parse(txtPrixTotal.Text.Trim().Substring(0, txtPrixTotal.Text.Trim().LastIndexOf(",")));
                }
                else
                {
                    prixTotal += decimal.Parse(txtPrixTotal.Text.Trim());
                }
               
                if (etat == "2")
                {
                    if (checkBox2.Checked)
                    {
                        _listeImpression = Impression.ImprimerFacturePetitFormat
                                    (numVente, dgvVente, client, dateTimePicker1.Value,
                                    txtPaye.Text, lblResteCredit.Text);
                    }
                    else
                    {
                        _listeImpression = Impression.ImprimerFacture(
                                    numVente, dgvVente, client, dateTimePicker1.Value,
                                    "FACTURE N° ", "Règlement en èspeces le : ");
                    }
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printPreviewDialog1.ShowDialog();
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    } txtTotalRemise.Text = "";
                    txtRecherche.Text = "";
                    txtPrixTotal.Text = "";
                    dgvVente.Rows.Clear();
                    btnImprimer.Enabled = false;
                    btnEnregistrer.Enabled = false; btnCrediter.Enabled = false;
                    txtPaye.Text = "";
                    lblResteCredit.Text = ""; chkRemise.Checked = false;
                    txtRechercherProduit.Text = "";
                    txtRechercherProduit.Focus(); dataGridView2.Rows.Clear();
                    numVente = 0;
                }
                else if (etat == "3")
                {
                    var totalAPayer  =double.Parse( txtPrixTotal.Text);
                    var totalPayer = double.Parse(txtPaye.Text);
                    var reste = double.Parse(lblResteCredit.Text);
                    var Count = dgvVente.Rows.Count / 25;
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;

                    }
                    for (var i = 0; i <= Count; i++)
                    {
                        if (i * 25 < dgvVente.Rows.Count)
                        {
                            _listeImpression = Impression.ImprimerFactureCredit
                                                    (dgvVente,client, totalAPayer, totalPayer, reste, i);
                            
                                printDocument1.Print();
                            
                        }
                    }
                    txtTotalRemise.Text = "";
                    txtRecherche.Text = "";
                    cmbConventione.Text = "";
                    txtPrixTotal.Text = "";
                    dgvVente.Rows.Clear();
                    btnImprimer.Enabled = false;
                    btnEnregistrer.Enabled = false;
                    txtPaye.Text = "";
                    lblResteCredit.Text = ""; chkRemise.Checked = false;
                    txtRechercherProduit.Text = "";
                    txtRechercherProduit.Focus(); dataGridView2.Rows.Clear();
                    numVente = 0;
                }
                else if (etat == "4")
                {
                   if (checkBox2.Checked)
                    {
                        _listeImpression = Impression.ImprimerFacturePetitFormat
                                    (numVente, dgvVente, client, dateTimePicker1.Value,
                                    txtPaye.Text, lblResteCredit.Text);
                    }
                    else
                    {
                        _listeImpression = Impression.ImprimerFacture
                                    (numVente, dgvVente, client, dateTimePicker1.Value,
                                    "FACTURE  N° ", "Règlement en èspeces le : ");
                    }
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                        chkRemise.Checked = false;
                    }
                }
                else
                {
                    List<Employe> listeEmploye = ConnectionClass.ListeDesEmployees("nom_empl", "'" + Form1.nomEmploye + "'");
                    string numEmploye = listeEmploye[0].NumMatricule;
                    var dateVente = DateTime.Now.Date ;
                        if (checkBox3.Checked)
                        {
                           dateVente= dateTimePicker1.Value.Date;
                        }
                    var heure = DateTime.Now;
                    var vente = new Vente();
                    vente.SiVente = true;
                    vente.DateVente = dateVente;
                    vente.Heure = heure;
                    vente.NumeroEmploye = numEmploye;
                    vente.PrixTotal = prixTotal;
                    if(ConnectionClass.EnregistrerVente(vente, dgvVente, client))                   
                    {
                        numVente = ConnectionClass.ObtenirNumeroVente();
                        ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Enregistrement du vente numero " + numVente + " du  " + txtRecherche.Text, this.Name);
                        if (checkBox2.Checked)
                        {
                            _listeImpression = Impression.ImprimerFacturePetitFormat
                                        (numVente, dgvVente, client, dateTimePicker1.Value,
                                        txtPaye.Text, lblResteCredit.Text);
                        }
                        else
                        {
                            _listeImpression = Impression.ImprimerFacture
                                (numVente, dgvVente, client, DateTime.Now,
                                "FACTURE N° ", "Règlement en èspeces le : ");
                        }
                        if (printDialog1.ShowDialog() == DialogResult.OK)
                        {
                            //printPreviewDialog1.ShowDialog();
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printDocument1.Print();
                        } txtTotalRemise.Text = "";
                        txtRecherche.Text = "";
                        txtPrixTotal.Text = "";
                        cmbConventione.Text = "";
                        dgvVente.Rows.Clear();
                        checkBox3.Checked = false;
                        btnImprimer.Enabled = false;
                        btnEnregistrer.Enabled = false;
                        txtPaye.Text = "";
                        lblResteCredit.Text = ""; chkRemise.Checked = false;
                        txtRechercherProduit.Text = "";
                        txtRechercherProduit.Focus();
                        numVente = 0; dataGridView2.Rows.Clear();
                    }

                }

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Enregistrement vente", exception);
            }
        }

        //retirer un element de la vente
        private void btnRetirer_Click(object sender, EventArgs e)
        {
            try
            {
                numVente = 0;
                txtRecherche.Text = "";
                txtTotalRemise.Text = "";
                lblResteCredit.Text = "";
               dgvVente.Rows.Clear();
                txtPrixTotal.Text = "";
                lblResteCredit.Text = "";
                txtPaye.Text = "";
                lblResteCredit.Text = "";
                cmbConventione.Text = "";
                chkRemise.Checked = false; dataGridView2.Rows.Clear();
               }
            catch (Exception )
            {
            }
        }
  
        Bitmap _listeImpression;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            //var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            e.Graphics.DrawImage(_listeImpression, -5, -10, _listeImpression.Width, _listeImpression.Height);
            e.HasMorePages = false;
        }
     
        private void txtRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dataGridView2.Rows.Clear();
                    var listePatient = ConnectionClassClinique.ListeDesPatients(txtRecherche.Text, cmbConventione.Text);
                    foreach (DataRow dtRow in listePatient.Rows)
                    {
                        dataGridView2.Rows.Add(
                         dtRow.ItemArray[0].ToString(),
                            dtRow.ItemArray[1].ToString().ToUpper() + " " + dtRow.ItemArray[2].ToString().ToUpper(),
                             dtRow.ItemArray[3].ToString(),
                             dtRow.ItemArray[4].ToString(),
                             dtRow.ItemArray[6].ToString(),
                             dtRow.ItemArray[7].ToString(),
                             dtRow.ItemArray[5].ToString(),
                             dtRow.ItemArray[16].ToString()
                      );
                    }
                }

            }
            catch (Exception)
            { }
        }

        private void txtTelephoneClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCrediter.Focus();
            }
        }

        //chercher par nom medicament et barcode
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                //if (cmbTrie.Text == "Code barre")
                //{
                //timer1.Start();
                if (checkBox1.Checked)
                {
                    if (dgvVente.Rows.Count > 0)
                    {

                        //propriete du client
                        string nomClient = null;
                        var client = new Client();
                        if (dataGridView2.SelectedRows.Count > 0)
                        {
                            nomClient = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                            client.Matricule = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                            client.Telephone = dataGridView2.SelectedRows[0].Cells[7].Value.ToString();
                            client.Entreprise = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                            client.Sexe = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                            client.Age = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                            client.SousCouvert = dataGridView2.SelectedRows[6].Cells[0].Value.ToString();
                        }
                        else if (dataGridView2.SelectedRows.Count == 0 && !string.IsNullOrEmpty(txtRecherche.Text))
                        {
                            nomClient = txtRecherche.Text;
                        }
                        else
                        {
                            nomClient = "CLIENT COMPTANT";
                        }
                    
                        List<Employe> listeEmploye = ConnectionClass.ListeDesEmployees("nom_empl", "'" + Form1.nomEmploye + "'");
                        string numEmploye = listeEmploye[0].NumMatricule;
                        var dateVente = DateTime.Now.Date;
                        var heure = DateTime.Now;
                        decimal prixTotal = decimal.Parse(dgvVente.Rows[0].Cells[3].Value.ToString());
                        var vente = new Vente();
                        vente.SiVente = true;
                        vente.DateVente = dateVente;
                        vente.Heure = heure;
                        vente.NumeroEmploye = numEmploye;
                        vente.PrixTotal = prixTotal;
                        if (ConnectionClass.EnregistrerVente(vente, dgvVente, client ))
                        {
                            timer1.Stop();
                            timer1.Dispose();
                            txtPrixTotal.Text = "";
                            dgvVente.Rows.Clear();
                            btnImprimer.Enabled = false;
                            btnEnregistrer.Enabled = false;
                            txtPaye.Text = "";
                            lblResteCredit.Text = "";
                            txtRechercherProduit.Text = "";
                            txtRechercherProduit.Focus();
                            chkRemise.Checked = false;
                        }
                    }
                }

                //}
            }
            catch { }
        }
        
        //enrgistrer une vente
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (dgvVente.Rows.Count > 0)
                {

                    //propriete du client
                    var passe = true;
                    var p = "";
                    foreach (DataGridViewRow dgv in dgvVente.Rows)
                    {
                        List<Medicament> listeMedicament = ConnectionClass.ListeDesMedicamentParCode(dgv.Cells[0].Value.ToString());
                        if (Int32.Parse(dgv.Cells[5].Value.ToString()) > listeMedicament[0].Quantite)
                        {
                            passe = false;
                            p = listeMedicament[0].Designation;
                        }
                    }
                    if (!passe)
                    {
                        //MonMessageBox.ShowBox("La quantité " + p + " démandée n'est pas disponible ", "Erreur", "erreur.png");
                        //return;
                    }
                    string nomClient = null;
                    var client = new Client();
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        nomClient = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                        client.Matricule = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                        client.Telephone = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
                        client.Entreprise = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                        client.Age = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                        client.Sexe = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                        client.SousCouvert = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                    }
                    else if ( dataGridView2.SelectedRows.Count  ==0 && !string.IsNullOrEmpty(txtRecherche.Text))
                    {
                        nomClient = txtRecherche.Text;
                    }
                    else
                    {
                        nomClient = "CLIENT COMPTANT";
                    }
                    client.NomClient = nomClient;
                    decimal prixTotal = 0;
                    if (txtPrixTotal.Text.Contains(","))
                    {
                        prixTotal += decimal.Parse(txtPrixTotal.Text.Substring(0, txtPrixTotal.Text.LastIndexOf(",")));
                    }
                    else
                    {
                        prixTotal += decimal.Parse(txtPrixTotal.Text);
                    }
                    List<Employe> listeEmploye = ConnectionClass.ListeDesEmployees("nom_empl", "'" + Form1.nomEmploye + "'");
                    string numEmploye = listeEmploye[0].NumMatricule;
                    var dateVente = DateTime.Now.Date;
                    var heure = DateTime.Now;
                    var vente = new Vente();
                    vente.SiVente = true;
                    vente.DateVente = dateVente;
                    vente.Heure = heure;
                    vente.NumeroEmploye = numEmploye;
                    vente.PrixTotal = prixTotal;

                    if (ConnectionClass.EnregistrerVente(vente, dgvVente, client))
                    {
                   
                        txtTotalRemise.Text = "";
                        txtRecherche.Text = "";
                        txtPrixTotal.Text = "";
                        cmbConventione.Text = "";
                        dgvVente.Rows.Clear();
                        btnImprimer.Enabled = false;
                        btnEnregistrer.Enabled = false;
                        btnCrediter.Enabled = false;
                        txtPaye.Text = "";
                        lblResteCredit.Text = "";
                        txtRechercherProduit.Text = "";
                        txtRechercherProduit.Focus();
                        chkRemise.Checked = false;
                        numVente = 0; dataGridView2.Rows.Clear();
                    }
                }               
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Enregistrement vente", exception);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        //facture pro forma
        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvVente.Rows.Count > 0)
                {
                    //propriete du client
                    var nomClient = txtRecherche.Text;
                    //propriete vente
                    var prixTotal = decimal.Parse(txtPrixTotal.Text);
                    int chiffre = (int)prixTotal;
                    string somme = Converti(chiffre);
                  
                        _listeImpression = Impression.ImprimerFactureProFormat
                 (dgvVente, nomClient.ToUpper(),somme);
                        if (printDialog1.ShowDialog() == DialogResult.OK)
                        {
                            printPreviewDialog1.ShowDialog();
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printDocument1.Print();
                        }   
                    
                    txtPrixTotal.Text = "";
                    dgvVente.Rows.Clear();
                    btnImprimer.Enabled = false;
                    btnEnregistrer.Enabled = false;
                    txtPaye.Text = "";
                    lblResteCredit.Text = "";
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Enregistrement vente", exception);
            }
        }

        public string Converti(int chiffre)
        {
            int centaine, dizaine, unite, reste, y;
            bool dix = false;
            string lettre = "";
            //strcpy(lettre, "");

            reste = chiffre / 1;

            for (int i = 1000000000; i >= 1; i /= 1000)
            {
                y = reste / i;
                if (y != 0)
                {
                    centaine = y / 100;
                    dizaine = (y - centaine * 100) / 10;
                    unite = y - (centaine * 100) - (dizaine * 10);
                    switch (centaine)
                    {
                        case 0:
                            break;
                        case 1:
                            lettre += "cent ";
                            break;
                        case 2:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "deux cents ";
                            }
                            else
                            {
                                lettre += "deux cent ";
                            }
                            break;
                        case 3:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "trois cents ";
                            }
                            else
                            {
                                lettre += "trois cent ";
                            }
                            break;
                        case 4:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "quatre cents ";
                            }
                            else { lettre += "quatre cent "; }
                            break;
                        case 5:
                            if ((dizaine == 0) && (unite == 0)) { lettre += "cinq cents "; }
                            else { lettre += "cinq cent "; }
                            break;
                        case 6:
                            if ((dizaine == 0) && (unite == 0)) { lettre += "six cents "; }
                            else { lettre += "six cent "; }
                            break;
                        case 7:
                            if ((dizaine == 0) && (unite == 0)) { lettre += "sept cents "; }
                            else { lettre += "sept cent "; }
                            break;
                        case 8:
                            if ((dizaine == 0) && (unite == 0)) { lettre += "huit cents "; }
                            else { lettre += "huit cent "; }
                            break;
                        case 9:
                            if ((dizaine == 0) && (unite == 0)) lettre += "neuf cents ";
                            else lettre += "neuf cent ";
                            break;
                    }// endSwitch(centaine)

                    switch (dizaine)
                    {
                        case 0:
                            break;
                        case 1:
                            dix = true;
                            break;
                        case 2:
                            lettre += "vingt ";
                            break;
                        case 3:
                            lettre += "trente ";
                            break;
                        case 4:
                            lettre += "quarante ";
                            break;
                        case 5:
                            lettre += "cinquante ";
                            break;
                        case 6:
                            lettre += "soixante ";
                            break;
                        case 7:
                            dix = true;
                            lettre += "soixante ";
                            break;
                        case 8:
                            lettre += "quatre-vingt ";
                            break;
                        case 9:
                            dix = true;
                            lettre += "quatre-vingt ";
                            break;
                    } // endSwitch(dizaine)

                    switch (unite)
                    {
                        case 0:
                            if (dix) lettre += "dix ";
                            break;
                        case 1:
                            if (dix) lettre += "onze ";
                            else lettre += "un ";
                            break;
                        case 2:
                            if (dix) lettre += "douze ";
                            else lettre += "deux ";
                            break;
                        case 3:
                            if (dix) lettre += "treize ";
                            else lettre += "trois ";
                            break;
                        case 4:
                            if (dix) lettre += "quatorze ";
                            else lettre += "quatre ";
                            break;
                        case 5:
                            if (dix) lettre += "quinze ";
                            else lettre += "cinq ";
                            break;
                        case 6:
                            if (dix) lettre += "seize ";
                            else lettre += "six ";
                            break;
                        case 7:
                            if (dix) lettre += "dix-sept ";
                            else lettre += "sept ";
                            break;
                        case 8:
                            if (dix) lettre += "dix-huit ";
                            else lettre += "huit ";
                            break;
                        case 9:
                            if (dix) lettre += "dix-neuf ";
                            else lettre += "neuf ";
                            break;
                    } // endSwitch(unite)

                    switch (i)
                    {
                        case 1000000000:
                            if (y > 1) lettre += "milliards ";
                            else lettre += "milliard ";
                            break;
                        case 1000000:
                            if (y > 1) lettre += "millions ";
                            else lettre += "million ";
                            break;
                        case 1000:
                            lettre += "mille ";
                            break;
                    }
                } // end if(y!=0)
                reste -= y * i;
                dix = false;
            } // end for
            if (lettre.Length == 0) lettre += "zero";

            return lettre;
        }
    
        //aouter par code barrre
        private void timer1_Tick(object sender, EventArgs e)
        {
              try
                {
                    #region MyRegion

                    var  listeMedicament = ConnectionClass.ListeDesMedicamentParCode(txtRechercherProduit.Text);
                    if (listeMedicament.Count > 0)
                    {
                        string codeMedicament = null;
                        string nomMedicament = null;
                        decimal prixAchat = 0;
                        decimal prixVente = 0;

                        foreach (Medicament medicament in listeMedicament)
                        {
                            codeMedicament = medicament.NumeroMedicament;
                            nomMedicament = medicament.NomMedicament;
                            prixAchat = medicament.PrixAchat;
                            prixVente = medicament.PrixVente;
                        }
                        bool found = false;
                        if (dgvVente.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow dg in dgvVente.Rows)
                            {
                                if (dg.Cells[0].Value.ToString().Equals(codeMedicament))
                                {
                                    int qte = Convert.ToInt32(dg.Cells[5].Value.ToString()) + 1;

                                    dg.Cells[5].Value = qte;
                                    var prixTotal = Convert.ToDouble(dg.Cells[3].Value.ToString()) * qte * (1 - Convert.ToDouble(dg.Cells[4].Value.ToString()) / 100);
                                    dg.Cells[6].Value = prixTotal;
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                dgvVente.Rows.Add(codeMedicament, nomMedicament, prixAchat.ToString(), prixVente.ToString(), 0, 1, prixVente);
                               
                            }

                        }
                        else
                        {
                            dgvVente.Rows.Add(codeMedicament, nomMedicament, prixAchat.ToString(), prixVente.ToString(), 0, 1, prixVente);
                        
                        }
                           
                            txtRechercherProduit.Text = "";
                            txtRechercherProduit.Focus();
                            btnImprimer.Enabled = true;
                            btnEnregistrer.Enabled = true;
                            timer1.Dispose();
                        
                    }
                        #endregion
                    
                }
              catch (Exception ex) { MonMessageBox.ShowBox("Enregistrement auto", ex); }
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVente.Rows.Count > 0)
                {
                    decimal montantTh = 0;
                    
                    //if (numVente > 0)
                    //{
                    //    if (GestionPharmacetique.Form1.typeUtilisateur == "admin")
                    //    {
                    //        if (MonMessageBox.ShowBox("Voulez vous supprimer cette vente?", "Confirmation", "confirmation.png") == "1")
                    //        {
                    //            string num_medi = dgvVente.SelectedRows[0].Cells[0].Value.ToString();
                    //            AppCode.ConnectionClass.SupprimerVentes(numVente, num_medi);
                    //            AppCode.ConnectionClass.SupprimerVentesComplet(numVente, num_medi);                              
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MonMessageBox.ShowBox("Vous n'etes pas autorisés à supprimer cette vente .", "Erreur", "erreur.png");
                    //    }
                    //}

                    dgvVente.Rows.Remove(dgvVente.SelectedRows[0]);
                    if (!string.IsNullOrWhiteSpace(txtPrixTotal.Text))
                    {
                        foreach (DataGridViewRow dgvRow in dgvVente.Rows)
                        {
                            decimal tauxRemise = decimal.Parse(dgvRow.Cells[4].Value.ToString());
                            decimal prixTotal = decimal.Parse(dgvRow.Cells[6].Value.ToString()) * (1 - tauxRemise / 100);

                            dgvRow.Cells[6].Value = prixTotal;
                            montantTh += decimal.Parse(dgvRow.Cells[6].Value.ToString());
                        }
                        txtPrixTotal.Text = montantTh.ToString();
                    }
                }
            }
            catch (Exception )
            {

            }
        }

    
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
           
        }
        
        //voir credit
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRecherche.Text))
                {
                    dgvVente.Rows.Clear();
                    Column7.Visible = true;
                    Column7.HeaderText = "DATE FACTURE";
                    clDesidgnation.Width = dgvVente.Width / 3 + 60 + dgvVente.Width / 8;
                   if (SGDP.Formes.DateFrm.ShowBox())
                    {
                        ArrayList listeClient = ConnectionClass.ListeClient(txtRecherche.Text);
                       foreach(Client  cl in listeClient)
                       {
                           numClient= cl.Id;
                       }
                        var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                        var dateFin = SGDP.Formes.DateFrm.dateFin;

                        var dt = AppCode.ConnectionClass.ListeDesVentesParClient(numClient, dateDebut, dateFin);
                       

                        double  montantPaye = 0;
                        var montantTotal = 0.0;
                        var dt1 = ConnectionClass.ListeDesPaiesDesCreditss(numClient, dateDebut, dateFin);
                        foreach (DataRow dtRow in dt1.Rows)
                        {
                            montantPaye += Double.Parse(dtRow.ItemArray[0].ToString());
                        }
                        numVente = Int32.Parse(dt.Rows[0].ItemArray[2].ToString()); 
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            montantTotal += double.Parse( dt.Rows[i].ItemArray[7].ToString());
                            dgvVente.Rows.Add
                                (
                                DateTime.Parse(dt.Rows[i].ItemArray[0].ToString()).ToShortDateString(),
                                dt.Rows[i].ItemArray[9].ToString(),
                                   dt.Rows[i].ItemArray[4].ToString(),
                                      dt.Rows[i].ItemArray[5].ToString(),0,
                                         dt.Rows[i].ItemArray[6].ToString(),
                                            dt.Rows[i].ItemArray[7].ToString()
                                );
                        }
                       paye = montantPaye;
                        var reste = montantTotal - montantPaye;
                        txtPaye.Text = string.Format(elGR, "{0:0,0}", montantPaye);
                        txtPrixTotal.Text = string.Format(elGR, "{0:0,0}", montantTotal);
                        lblResteCredit.Text = string.Format(elGR, "{0:0,0}", reste);
                    }
                }
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Voir credit", exception); }
        
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        double  paye;
        //solder
         private void btnSolderCredit_Click(object sender, EventArgs e)
        {
            try
            {
                decimal montantCredit;
                if (Decimal.TryParse(txtPaye.Text, out montantCredit))
                {
                    if (!string.IsNullOrEmpty(txtRecherche.Text))
                    {
                        if (MonMessageBox.ShowBox("Voulez vous solder cette facture?", "Confirmation", "confirmation.png") == "1")
                        {
                            var listeEmploye = AppCode.ConnectionClass.ListeDesEmployees("nom_empl", "'" + Form1.nomEmploye + "'");
                            string numEmploye = listeEmploye[0].NumMatricule;
                            ConnectionClass.SolderCredit(numClient, montantCredit, numEmploye, numVente);
                            ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Solder credit  numero " + numVente + " du  " + txtRecherche.Text, this.Name);
                            var reste = double.Parse(txtPrixTotal.Text) - (double)montantCredit - paye;
                            lblResteCredit.Text = string.Format(elGR, "{0:0,0}", reste);
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner le nom du client, puis reessayez.", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    txtPrixTotal.BackColor = Color.Red;
                    txtPrixTotal.Focus();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Solder credit", ex);
            }
        }

        //crediter
        private void btnCrediter_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvVente.Rows.Count > 0)
                {
                    var client = new Client();
                    if (!string.IsNullOrEmpty(txtRecherche.Text))
                    {
                        txtRecherche.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                        client.Matricule = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                        client.Telephone = dataGridView2.SelectedRows[0].Cells[6].Value.ToString();
                        client.Entreprise = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                        client.Age = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                        client.Sexe = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                        client.SousCouvert = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                        if (!string.IsNullOrEmpty(dataGridView2.SelectedRows[0].Cells[7].Value.ToString()))
                        {
                            if (  dataGridView2.SelectedRows[0].Cells[7].Value.ToString() == "1")
                            {
                                client.SiCouvert = true;
                            }
                            else
                            {
                                client.SiCouvert = false;
                            }
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner le nom du client sur la liste puis réessayez.", "Erreur ", "erreur.png");
                        return;
                    }
                    client.NomClient = txtRecherche.Text;
                    decimal prixTotal = 0;
                    if (txtPrixTotal.Text.Contains(","))
                    {
                        prixTotal += decimal.Parse(txtPrixTotal.Text.Substring(0, txtPrixTotal.Text.LastIndexOf(",")));
                    }
                    else
                    {
                        prixTotal += decimal.Parse(txtPrixTotal.Text);
                    }

                    List<Employe> listeEmploye = ConnectionClass.ListeDesEmployees("nom_empl", "'" + Form1.nomEmploye + "'");
                    string numEmploye =  listeEmploye[0].NumMatricule;
                       var dateVente = DateTime.Now.Date ;
                        if (checkBox3.Checked)
                        {
                           dateVente= dateTimePicker1.Value.Date;
                        }
                    var heure = DateTime.Now;
                    var vente = new Vente();
                    vente.SiVente = false;
                    vente.DateVente = dateVente;
                    vente.Heure = heure;
                    vente.PrixTotal = prixTotal;
                    vente.NumeroEmploye = numEmploye;

                    var montantMensuelParPatient = ConnectionClassClinique.TotalProformaDuPatient(Int32.Parse(dataGridView2.Rows[0].Cells[0].Value.ToString()), ConnectionClass.ObtenirDebutJour(DateTime.Now.Month), ConnectionClass.ObtenirFinJour(DateTime.Now.Month));
                    montantMensuelParPatient += ConnectionClass.MontantDesCredit(dataGridView2.Rows[0].Cells[1].Value.ToString(), dataGridView2.Rows[0].Cells[4].Value.ToString(), ConnectionClass.ObtenirDebutJour(DateTime.Now.Month), ConnectionClass.ObtenirFinJour(DateTime.Now.Month));
                    var entreprise = ConnectionClassClinique.ListeDesEntreprises(dataGridView2.Rows[0].Cells[4].Value.ToString());
                    var montantLimite = entreprise[0].MontantLimite;
                    var siLimit = entreprise[0].SiLimite;
                    if ((montantMensuelParPatient +(double) prixTotal >= montantLimite) && siLimit)
                    {
                        MonMessageBox.ShowBox("La prise en charge de la convention " + dataGridView2.Rows[0].Cells[4].Value.ToString() + " ne doit pas excéder " + montantLimite + " \nTotal prise en charge du mois en cours: " + montantMensuelParPatient, "Erreur", "erreur.png");
                        return;
                    }

                    if (client.SiCouvert)
                    {
                        if (MonMessageBox.ShowBox("Voulez vous enregistrer ce credit ?", "Demande confirmation", "confirmation.png") == "1")
                        {

                            if (ConnectionClass.EnregistrerVente(vente, dgvVente, client))
                            {  
                                  dataGridView2.Rows.Clear();
                                numVente = ConnectionClass.ObtenirNumeroVente();
                                if (checkBox2.Checked)
                                {
                                    ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Enregistrement du credit numero "+numVente +" du  " +txtRecherche.Text, this.Name);
                                    _listeImpression = Impression.ImprimerFacturePetitFormat
                                       (numVente, dgvVente, client, dateVente,
                                     txtPaye.Text, lblResteCredit.Text);
                                }
                                else
                                {
                                    _listeImpression = Impression.ImprimerFacture
                                           (numVente, dgvVente, client, dateVente,
                                           "CREDIT NO ", "Date : ");
                                }
                                if (printDialog1.ShowDialog() == DialogResult.OK)
                                {
                                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                                    printDocument1.Print();
                                }
                                txtTotalRemise.Text = "";
                                txtRecherche.Text = "";
                                txtPrixTotal.Text = "";
                                cmbConventione.Text = "";
                                dgvVente.Rows.Clear();
                                btnImprimer.Enabled = false;
                                checkBox3.Checked = false;
                                txtPaye.Text = "";
                                lblResteCredit.Text = "";
                                txtRechercherProduit.Text = "";
                                txtRechercherProduit.Focus();
                                btnCrediter.Enabled = false; dataGridView2.Rows.Clear();
                                dateTimePicker1.Value = DateTime.Now;
                            }
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Ce patient n'est pas couvert", "Erreur", "erreur.png");
                    }
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Enregistrement credit", exception);
            }

        }

            
        // rechercher medicament par designation
        private void txtRechercherProduit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ( e.KeyCode == Keys.End)
                {
                    if (dgvVente.Rows.Count > 0)
                    {
                        var montantTh = 0.0;
                        var totalRemise = 0.0;
                        for (int i = 0; i <= dgvVente.Rows.Count - 1; i++)
                        {
                            montantTh += Double.Parse(dgvVente.Rows[i].Cells[6].Value.ToString());
                            totalRemise += Double.Parse(dgvVente.Rows[i].Cells[2].Value.ToString()) *
                                Double.Parse(dgvVente.Rows[i].Cells[4].Value.ToString()) *
                                Double.Parse(dgvVente.Rows[i].Cells[5].Value.ToString()) / 100;

                        }
                        txtPrixTotal.Text = ((int)montantTh).ToString();

                        txtTotalRemise.Text = ((int)totalRemise).ToString();
                        btnEnregistrer.Enabled = true;
                        btnEnregistrer.Focus();
                        btnImprimer.Enabled = true;
                        btnCrediter.Enabled = true;
                        txtRecherche.Focus();
                    }
                }
              
            }
            catch (Exception EX)
            {
                MonMessageBox.ShowBox("",EX);
            }
        }

        private void dgvVente_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var rowIndex = dgvVente.SelectedRows[0].Index;

                var tauxRemise = Double.Parse(dgvVente.Rows[rowIndex].Cells[4].Value.ToString());
                    double prixVente;
                    var qte = Int32.Parse(dgvVente.Rows[rowIndex].Cells[5].Value.ToString());
                    
                    if (qte > ListeProduitFrm.stock && ListeProduitFrm.indexDetail =="0")
                    {
                        MonMessageBox.ShowBox("La quantité démandée n'est pas disponible ", "Erreur", "erreur.png");
                        dgvVente.Rows[rowIndex].Cells[5].Value= "0";
                        dgvVente.CurrentCell = dgvVente.Rows[rowIndex].Cells[5];
                        dgvVente.BeginEdit(true);
                        return;
                    }

                    if (tauxRemise > 0)
                    {
                        prixVente = Double.Parse(dgvVente.Rows[rowIndex].Cells[2].Value.ToString())* (1 - tauxRemise / 100);
                        dgvVente.Rows[rowIndex].Cells[3].Value = (int)prixVente;
                    }
                    else
                    {
                        prixVente = double.Parse(dgvVente.Rows[rowIndex].Cells[3].Value.ToString());
                        //dgvRow.Cells[2].Value = (int)prixVente;
                    }
                    var prixTotal = prixVente * qte;
                    dgvVente.Rows[rowIndex].Cells[6].Value = (int)prixTotal;
                    if (!string.IsNullOrEmpty(txtPrixTotal.Text))
                    {
                                var montantTh = 0.0;
                                var totalRemise = 0.0;
                                for (int i = 0; i <= dgvVente.Rows.Count - 1; i++)
                                {
                                    montantTh += Double.Parse(dgvVente.Rows[i].Cells[6].Value.ToString());
                                    totalRemise += Double.Parse(dgvVente.Rows[i].Cells[2].Value.ToString()) *
                                        Double.Parse(dgvVente.Rows[i].Cells[4].Value.ToString()) *
                                        Double.Parse(dgvVente.Rows[i].Cells[5].Value.ToString()) / 100;

                                }
                                txtPrixTotal.Text = ((int)montantTh).ToString();

                                txtTotalRemise.Text = ((int)totalRemise).ToString();
                               
                    }
                txtRechercherProduit.Focus();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Cell end edit", ex);
            }
        }

        private void cmbTrie_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRechercherProduit.Focus();
        }

        bool found ;
        private void txtRechercherProduit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    ListeProduitFrm.indexRecherche = txtRechercherProduit.Text;
                    ListeProduitFrm.state = "1";
                    txtRechercherProduit.Text = "";
                    if (ListeProduitFrm.ShowBox() == "1")
                    {
                        found = false;
                            var qte = ListeProduitFrm.stock;
                            var designation = ListeProduitFrm.designation;
                            var prixCession = ListeProduitFrm.prixCession;
                            var prixPublic = ListeProduitFrm.prixPublic;
                            var numeroProduit = ListeProduitFrm.numeroProduit;
                                if (qte > 0)
                                {
                                   
                                    if (dgvVente.Rows.Count > 0)
                                    {
                                        foreach (DataGridViewRow dg in dgvVente.Rows)
                                        {
                                            if (dg.Cells[0].Value.ToString().Equals(numeroProduit))
                                            {
                                                found = true;
                                                txtRechercherProduit.Focus();
                                            }
                                        }
                                        if (!found)
                                        {
                                            
                                                dgvVente.Rows.Add(
                                                   numeroProduit,
                                                   designation.ToUpper(),
                                                   prixPublic,
                                                    prixPublic,
                                                    "0",
                                                    "1",
                                                  prixPublic,
                                                  ListeProduitFrm.indexDetail
                                                );

                                        }

                                    }
                                    else
                                    {
                                        dgvVente.Rows.Add(
                                                   numeroProduit,
                                                   designation.ToUpper(),
                                                   prixPublic,
                                                    prixPublic,
                                                    "0",
                                                    "1",
                                                  prixPublic,
                                                  ListeProduitFrm.indexDetail
                                                  );
                                    }

                                                dgvVente.CurrentCell = dgvVente.Rows[dgvVente.Rows.Count - 1].Cells[5];
                                                dgvVente.BeginEdit(true);
                                }
                            }
                       

                        etat = "1";
                    } 
                
            }
            catch { }
        }

        //paiement credit
        private void txtMontantCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSolderCredit_Click(null, null);
            }
        }
        
        //calcul de remise
        private void txtRemise_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    
                    Double tauxRemise;
                    var totalRemise = 0.0; var  totalPrix = 0.0;
                    if (Double.TryParse(txtRemise.Text, out tauxRemise))
                    {

                        foreach (DataGridViewRow dgvRow in dgvVente.Rows)
                        {
                            var prixVente = Double.Parse(dgvRow.Cells[2].Value.ToString()) * (1 - tauxRemise / 100); ;
                            var qte = Int32.Parse(dgvRow.Cells[5].Value.ToString());
                            var prixTotal = prixVente * qte  ;
                            totalRemise += Double.Parse(dgvRow.Cells[2].Value.ToString()) *  tauxRemise / 100 * qte; 
                            dgvRow.Cells[3].Value = (int)prixVente;
                            dgvRow.Cells[4].Value = tauxRemise;
                            dgvRow.Cells[6].Value = (int)prixTotal;
                            totalPrix += (int)prixTotal;
                        }
                        txtRemise.Text = "";
                        txtPrixTotal.Text = totalPrix.ToString();
                        txtRechercherProduit.Focus();
                        txtTotalRemise.Text =totalRemise.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MonMessageBox.ShowBox("Cell end edit", ex);
                }
            }
        }

        //activer la remise
        private void chkRemise_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRemise.Checked)
            {
                clRemise.ReadOnly = false ;
                txtRemise.Visible = true;
                txtRemise.Focus();
                clDesidgnation.Width = dgvVente.Width / 3 + 60 + dgvVente.Width / 8;
                clRemise.Width = dgvVente.Width / 8;
                clRemise.Visible = true;
            }
            else
            {
                clRemise.ReadOnly = true;
                clRemise.Visible = false;
                txtRemise.Visible = false;
                txtRechercherProduit.Focus();
                clDesidgnation.Width = dgvVente.Width / 3 + 60 + 2*dgvVente.Width / 8;
            }
        }

        private void dgvVente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvVente_CellEndEdit(null, null);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var frm = new GestionDesVetements.Formes.ListePaiementFrm();
            frm.ShowDialog();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            if (MedicamentFrm.ShowBox() == "1")
            { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                clDesidgnation.Width = dgvVente.Width / 3 + 60 + 2 * dgvVente.Width / 8;
                Column7.Visible = false;
                if (listeDistributionFrm.ShowBox() == "1")
                {
                    dgvVente.Rows.Clear();
                    var total = 0.0;
                    numVente = listeDistributionFrm.numVente;
                    numClient = listeDistributionFrm.idClient;
                    var dt = ConnectionClass.ListeDesVentesParDateVente(numVente);
                    var dt1 = ConnectionClass.ListeDesVentesParNumeroVente(numVente);
                    var heure = "00:00::00";
                    
                    if (!string.IsNullOrEmpty(dt1.Rows[0].ItemArray[8].ToString()))
                    {
                        heure = dt1.Rows[0].ItemArray[8].ToString();
                    }
                    var dateVente = dt1.Rows[0].ItemArray[1].ToString();
                    dateTimePicker1.Value = DateTime.Parse(dateVente.Substring(0, 10) + " " + heure);
                    var totalRemise = .0;
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        var liste = ConnectionClass.ListeDesMedicamentsRechercherParNom(dtRow.ItemArray[1].ToString());
                        var designation = dtRow.ItemArray[1].ToString(); 
                       
                        var tauxRemise =100- (double.Parse(dtRow.ItemArray[3].ToString()) / double.Parse(dtRow.ItemArray[2].ToString()))*100;
                        dgvVente.Rows.Add(
                            dtRow.ItemArray[0].ToString(),
                            designation ,
                            dtRow.ItemArray[2].ToString(),
                            dtRow.ItemArray[3].ToString(), "0",
                            dtRow.ItemArray[4].ToString(),
                            dtRow.ItemArray[5].ToString());
                        total += double.Parse(dtRow.ItemArray[5].ToString());
                        totalRemise += (double.Parse(dtRow.ItemArray[4].ToString()) * double.Parse(dtRow.ItemArray[2].ToString()) * tauxRemise / 100);
                        txtRecherche.Text = listeDistributionFrm.client;
                        btnImprimer.Enabled = true;
                    }
                    txtPrixTotal.Text = total.ToString();
                    txtTotalRemise.Text = totalRemise.ToString();
                    if(listeDistributionFrm.typeVente=="CREDIT")
                    {
                        etat = "4";
                    }else if(listeDistributionFrm.typeVente=="COMPTANT")
                    {
                        etat = "2";
                    }
          
                    var dtt = ConnectionClassClinique.ListeDesPatients(listeDistributionFrm.idPatient);
                    if (dtt.Rows.Count > 0)
                    {
                        cmbConventione.Text = dtt.Rows[0].ItemArray[6].ToString();
                   
                        dataGridView2.Rows.Clear();
                        foreach (DataRow dtRow in dtt.Rows)
                        {
                            dataGridView2.Rows.Add(
                             dtRow.ItemArray[0].ToString(),
                                dtRow.ItemArray[1].ToString().ToUpper() + " " + dtRow.ItemArray[2].ToString().ToUpper(),
                                 dtRow.ItemArray[4].ToString(),
                                 dtRow.ItemArray[3].ToString(),
                                 dtRow.ItemArray[6].ToString(),
                                 dtRow.ItemArray[7].ToString(),
                                    dtRow.ItemArray[5].ToString()
                          );
                        }
                    }
                }
            }
            catch (Exception Exception)
            {
                MonMessageBox.ShowBox("erreur", Exception); 
            }
        }

        string etat;

     
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                dataGridView2.Rows.Clear();
                if (!string.IsNullOrEmpty(cmbConventione.Text ))
                {
                    var listePatient = ConnectionClassClinique.ListeDesPatients("", cmbConventione.Text);
                    foreach (DataRow dtRow in listePatient.Rows)
                    {
                        dataGridView2.Rows.Add(
                         dtRow.ItemArray[0].ToString(),
                            dtRow.ItemArray[1].ToString().ToUpper() + " " + dtRow.ItemArray[2].ToString().ToUpper(),
                             dtRow.ItemArray[3].ToString(),
                             dtRow.ItemArray[4].ToString(),
                             dtRow.ItemArray[6].ToString(),
                             dtRow.ItemArray[7].ToString(),
                                dtRow.ItemArray[5].ToString(),
                                   dtRow.ItemArray[16].ToString()
                      );
                    }
                }
            }
            catch 
            { }
        }

        private void printPreviewDialog1_Load_1(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        int numClient;
       
        private void txtRechercherProduit_Click(object sender, EventArgs e)
        {
            clDesidgnation.Width = dgvVente.Width / 3 + 60 + 2 * dgvVente.Width / 8;
            Column7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new ListVentFrm();
            frm.Location = new Point(Location.X, Location.Y);
            frm.Size = new System.Drawing.Size(Width, Height);
            frm.ShowDialog();
        }

        private void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            int numero;

            if (Int32.TryParse(txtRecherche.Text, out numero))
            {
         
                var dt = ConnectionClassClinique.ListeDesPatients(numero);
                if (dt.Rows.Count > 0)
                {
                    cmbConventione.Text = dt.Rows[0].ItemArray[6].ToString();
                    dataGridView2.Rows.Clear();
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        dataGridView2.Rows.Add(
                         dtRow.ItemArray[0].ToString(),
                            dtRow.ItemArray[1].ToString().ToUpper() + " " + dtRow.ItemArray[2].ToString().ToUpper(),
                             dtRow.ItemArray[4].ToString(),
                             dtRow.ItemArray[3].ToString(),
                             dtRow.ItemArray[6].ToString(),
                             dtRow.ItemArray[7].ToString(),
                                dtRow.ItemArray[5].ToString(),
                                   dtRow.ItemArray[16].ToString()
                      );
                    }
                }
                else
                {
                    dataGridView2.Rows.Clear();
                }
            }
            else
            {
               
            }
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    txtRecherche.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                }
                else
                {
                    txtRecherche.Text = "";
                }
            }
            catch { }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            dataGridView2_CellContentClick(null, null);
        }

        private void txtPaye_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvVente.Rows.Count > 0)
                {

                    var montantPaye = Decimal.Parse(txtPaye.Text);
                    var ttc = Decimal.Parse(txtPrixTotal.Text);
                    var reste = montantPaye - ttc;
                    if (reste >= 0)
                        lblResteCredit.Text = reste.ToString();
                    else
                        lblResteCredit.Text = "";
                }
                else
                {
                    //txtPaye.Text = "";
                }

            }
            catch { }
        }

        private void dgvVente_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvVente_CellEndEdit(null, null);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                dateTimePicker1.Visible = true;
            }
            else
            {
                dateTimePicker1.Visible = false ;
            }
        }

        private void txtPaye_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {       if (e.KeyCode == Keys.Right)
                {
                    double total;
                    if (double.TryParse(txtPaye.Text, out total))
                    {
                        //if(total<=2500)
                        if (AppCode.ConnectionClass.EnregistrerFacture(total, Form1.numEmploye, dateTimePicker1.Value.Date))
                        {
                            txtPaye.Text = "";
                        }
                    }
                }
                else if (e.KeyCode == Keys.End)
                {
                    var dt = ConnectionClass.ListeDesFactures(Form1.numEmploye, dateTimePicker1.Value.Date, dateTimePicker1.Value.Date.AddHours(24));
                    txtPaye.Text = dt.Rows.Count > 0 ? dt.Rows[0].ItemArray[3].ToString() : "";
                }
            }
            catch { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var dtEntrep = ConnectionClassClinique.ListeDesEntreprises();
                cmbConventione.Items.Add("");
                foreach (DataRow entrep in dtEntrep.Rows)
                {
                    cmbConventione.Items.Add(entrep.ItemArray[1].ToString().ToUpper());
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
