using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SystemeGestionAgenceVoyage.GUI;
using GestionPharmacetique.AppCode;
using GestionPharmacetique.Forme;
using Excel = Microsoft.Office.Interop.Excel;
using System.Speech.Synthesis;


namespace GestionPharmacetique
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        LoginForm loginFrm = new LoginForm();
   
        private void grp_banniere_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.WhiteSmoke, 0);
            Rectangle area1 = new Rectangle(0, 0, this.grp_banniere.Width - 1, this.grp_banniere.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.WhiteSmoke, Color.White, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void grp_cote_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.grp_cote.Width - 1, this.grp_cote.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DarkGray, Color.CadetBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void grp_principal_Paint(object sender, PaintEventArgs e)
        {
            //Graphics mGraphics = e.Graphics;
            //Pen pen1 = new Pen(SystemColors.Control, 2);
            //Rectangle area1 = new Rectangle(0, 0, this.grp_principal.Width - 1, this.grp_principal.Height - 1);
            //LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.White, Color.White, LinearGradientMode.ForwardDiagonal);
            //mGraphics.FillRectangle(linearGradientBrush, area1);
            //mGraphics.DrawRectangle(pen1, area1);
        }

        private void menuStrip1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.menuStrip1.Width - 1, this.menuStrip1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.CadetBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }



        private void contextMenuStripParametre_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.ctnJournal.Width - 1, this.ctnJournal.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.AliceBlue
                , Color.AliceBlue, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblDate.Text = DateTime.Now.ToLongTimeString();
        }

        //inserer les donnees deans un fichier log copmte
        private void InsererDansLog( string nomEmployee, string typeEmpl)
        {
            try
            {
                //string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                //string filePath = global::GestionPharmacetique.Properties.Resources.log_account; ;
                //if (System.IO.File.Exists(filePath))
                //{
                //    System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true);
                //    sw.WriteLine("*****************Date et Heure de connection " + DateTime.Now.ToString() + "  **********************");
                //    sw.WriteLine("  {0} s'est connecté comme un {1}",  nomEmployee, typeEmpl);
                //    sw.WriteLine();
                //    sw.Close();
                //}
            }
            catch { }
        }

        public static int width1, width2, height1, height2;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                
                timer1.Start();
                timerAnimation.Start();
                SeDeconnecter();
                width1 = grp_principal.Width;
                width2 = grp_cote.Width;
                height2 = menuStrip1.Height;
                height1 = grp_principal.Height;
                w = this.grp_principal.Width + this.grp_cote.Width + 10;
                h = this.grp_principal.Height + this.menuStrip1.Height + 10;
                //pictureBox3.Location = new Point((width1 - pictureBox3.Width) / 2, pictureBox3.Location.Y);

                var depot = AppCode.ConnectionClass.ListeDesDepots();
                if (depot[0].NomDepot.ToUpper().Contains("DEPOT"))
                {
                    label1.Text = "Systeme de Gestion du " .ToUpper()+ depot[0].NomDepot.ToUpper();
                }
                else
                {
                    label1.Text = "Systeme de Gestion de la ".ToUpper() + depot[0].NomDepot.ToUpper();
                }

                if (AppCode.ConnectionClass.ListeEtatStock(DateTime.Now.Date).Count <= 0)
                {
                    var liste = AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom("");
                    AppCode.ConnectionClass.EnregistrerEtatStock(liste);
                }
            }
            catch { }
        }

        //deconnecter les menu strip et les boutons
        private void SeDeconnecter()
        {
            this.grp_principal.Controls.Clear();
            gestionDsLivraisonsToolStripMenuItem.Visible = false;
            administrerToolStripMenuItem.Visible = false;
            gestionDsLivraisonsToolStripMenuItem.Visible = false;
            toolStripMenuItem1.Visible = false;
            gestionDeFamilleDeMedicamentToolStripMenuItem.Visible = false;
            seConnecterToolStripMenuItem.Text = "Se connecter";
            btnInventaire.Enabled = false;
            btnJournal.Enabled = false;
            btnTache.Enabled = false;
            btnParametre.Enabled = false;
            btnTiers.Enabled = false;
            lblHeureConnecter.Text = "";
            label2.Text = "";
            lblUtilisateur.Text = "";
            typeUtilisateur = "";
            pictureBox1.Image = null;
        }

        //activer les menus et les boutons
        public static string typeUtilisateur;
        private void SeConnecter()
        {
            if (typeUtilisateur == "caissier")
            {
                toolStripMenuItem1.Visible = true;
                gestionDsLivraisonsToolStripMenuItem.Visible = true;
                btnTiers.Enabled = true;
                btnJournal.Enabled = true;
                btnTache.Enabled = true;
                gestionDeFamilleDeMedicamentToolStripMenuItem.Visible = true;
                gestionDeFamilleDeMedicamentToolStripMenuItem.Enabled = true;
                sortieStockToolStripMenuItem.Visible = true;
                sortieStockToolStripMenuItem.Enabled = true;
                produitToolStripMenuItem.Visible = false;
                entréeStockToolStripMenuItem.Visible = false;
                toolStripSeparator13.Visible = false;
                toolStripSeparator4.Visible = false;
            }
            else if (typeUtilisateur == "admin")
            {
                gestionDsLivraisonsToolStripMenuItem.Visible = true;
                administrerToolStripMenuItem.Visible = true;
                toolStripMenuItem1.Visible = true;
                gestionDeFamilleDeMedicamentToolStripMenuItem.Visible = true;
                btnTiers.Enabled = true;
                btnJournal.Enabled = true;
                btnTache.Enabled = true;
                btnParametre.Enabled = true;
                btnInventaire.Enabled = true;
            }
        }

        private void administrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new EmployeFrm();
                frm.Location = new Point(grp_principal.Location.X, grp_principal.Location.Y + 22);
                frm.Size = new System.Drawing.Size(this.grp_principal.Width, this.grp_principal.Height + 3);
                frm.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void timerProgressBar1_Tick(object sender, EventArgs e)
        {
            if (loginFrm.progressBar1.Value < 100)
            {
                loginFrm.progressBar1.Value++;
                if (loginFrm.progressBar1.Value == 100)
                {
                    loginFrm.progressBar1.Visible = false;
                }
            }
            else
            {
                loginFrm.lblEtat.Text = "Connection établie  ";
                this.timerProgressBar1.Enabled = false;
                //this.timerProgressBar1.Dispose();
                loginFrm.progressBar1.Value = 0;
               loginFrm. progressBar2.Visible = true;
               loginFrm.progressBar2.Location = new Point(loginFrm.progressBar1.Location.X,
                   loginFrm.progressBar1.Location.Y);
               loginFrm.lblEtat.Text = "Vérification du mot de passe...";
                this.timerProgressBar2.Start();
            }
        }

        //code pour progress bar 2
      public  static string nomEmploye = null;
      public static string numEmploye = null;
      public SpeechSynthesizer _synthesizer;
        private void timerProgressBar2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (loginFrm.progressBar2.Value < 100)
                {
                    loginFrm.progressBar2.Value++;
                    if (loginFrm.progressBar2.Value == 100)
                    {
                        loginFrm.progressBar1.Visible = false;
                    }
                }
                else
                {
                    this.timerProgressBar2.Dispose();
                    loginFrm.progressBar2.Visible = false;
                    loginFrm.progressBar2.Value = 0;
                    loginFrm.lblEtat.Visible = false;

                    string nom_util = loginFrm.comboBox1.Text.Trim();
                    string mot_passe = loginFrm.txtPassword.Text.GetHashCode().ToString();
                    Utilisateur utilisateur = ConnectionClass.SeConnecter(nom_util, mot_passe);
                    if (utilisateur != null)
                    {
                        loginFrm.txtPassword.Text = "";
                        loginFrm.Close();
                        var imagePath = "C:\\Image Pharmacie\\Photo Employe\\" + utilisateur.Image;

                        pictureBox1.Image = System.IO.File.Exists(imagePath) ? Image.FromFile(imagePath) : pictureBox1.Image = null;

                        //_synthesizer = new SpeechSynthesizer();
                        //var words = "Bonjour " + nomEmploye +" Bienvenue au système de gestion pharmaceutique érré";
                        //_synthesizer.Speak(words);


                        typeUtilisateur = utilisateur.TypeUtilisateur;
                        lblUtilisateur.Text = utilisateur.NomUtilisateur;
                        nomEmploye = utilisateur.NomEmploye;
                        numEmploye = utilisateur.NumEmploye;
                        lblHeureConnecter.Text = DateTime.Now.ToLongTimeString();
                        label2.Text = "Connecter à ";
                        InsererDansLog(utilisateur.NomEmploye, typeUtilisateur);
                        SeConnecter();
                        seConnecterToolStripMenuItem.Text = "Se deconnecter";

                        timerClignotant1.Dispose();
                        timerClignotant2.Dispose();
                        List<Medicament> listeArrayList = ConnectionClass.ListeDesMedicamentsExpirees();
                        if (listeArrayList.Count > 0)
                        {
                            lblexpiration.Text =
                                "Il y' a de medicaments en voie d' expiration dans le stock. Veuillez cliquer ici pou voir les details";
                            lblexpiration.Visible = true;
                            timerExpiration1.Start();
                            timerExpiration2.Stop();
                            lblexpiration.ForeColor = Color.Red;
                            timerExpiration2.Start();
                            timerExpiration1.Stop();
                        }
                        VerificationStockAlerte();

                       

                        string etat = "1";
                        if (etat == "1")
                        {
                             timerAnim1.Start();
                            etat = "2";

                        }
                        else if (etat == "2")
                        {
                            etat = "1";
                            timerAnim1.Stop();
                            BS.MoveFirst();
                        }
                    }
                    else
                    {
                        //login_user.label4.Visible = false;
                        timerClignotant1.Start();
                        timerClignotant2.Stop();
                        loginFrm.lblResultat.ForeColor = Color.Red;
                        //label4.Text = "Accés refusé. Mot de passe incorrect";
                        timerClignotant2.Start();
                        timerClignotant1.Stop();
                        loginFrm.lblResultat.Visible = false;
                    }
                }
            }
            catch { }
        }

        //authenfier
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(loginFrm.comboBox1.Text))
            {
                loginFrm.progressBar1.Visible = true;
                loginFrm.lblEtat.Text = "Connection au serveur de la base de données...  ";
                loginFrm.lblEtat.Visible = true;
                timerProgressBar1.Start();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loginFrm.lblResultat.Visible = false;
            this.loginFrm.Close  ();
        }
        // seconnecter pour s'authentifier
        private void seConnecterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (seConnecterToolStripMenuItem.Text == "Se connecter")
            {
                loginFrm.btnLogin.Click += new EventHandler(btnLogin_Click);
                loginFrm.button1.Click += new EventHandler(button1_Click);
                loginFrm.lblResultat.Visible = false;
                loginFrm.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
                loginFrm.Location = new Point((this.grp_principal.Width - loginFrm.Width) / 2
                + this.grp_principal.Location.X, (this.grp_principal.Height - this.loginFrm.Height) / 2 +
                this.grp_principal.Location.Y);
                loginFrm.ShowDialog();
                timerClignotant2.Dispose();
                timerClignotant1.Dispose();
               
            }
            else if (seConnecterToolStripMenuItem.Text == "Se deconnecter")
            {
                if(MonMessageBox.ShowBox("Voulez vous deconnecter?","Confirmation","confirmation.png")=="1")
                SeDeconnecter();
                timerClignotant2.Dispose();
                timerClignotant1.Dispose();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(null, null);

            }
        }

        //fqire clignoter le label resultat
        private void timerCligontant1_Tick(object sender, EventArgs e)
        {
            loginFrm.lblResultat.Visible = true;
            loginFrm.lblResultat.ForeColor = Color.Red;
            timerClignotant2.Start();
            timerClignotant1.Stop();
        }

        private void timerClignotant2_Tick(object sender, EventArgs e)
        {
            loginFrm.lblResultat.Visible = true;
            loginFrm.lblResultat.ForeColor = Color.CadetBlue;
            timerClignotant1.Start();
            timerClignotant2.Stop();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //this.pictureBox1.Location = new Point(this.pictureBox1.Location.X, this.grp_cote.Size.Height - 10 - this.pictureBox1.Height);
            //this.lblUtilisateur.Location = new Point(this.pictureBox1.Location.X, this.grp_cote.Size.Height - 20 - this.pictureBox1.Height - this.lblUtilisateur.Height);
        }

        private void gestionDsLivraisonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VenteFrm frmVente = new VenteFrm();
            frmVente.Location = new Point(grp_cote.Location.X, grp_cote.Location.Y-22);
            frmVente.Size = new System.Drawing.Size(width1 + width2+12 , this.grp_principal.Height + 50);            
            frmVente.ShowDialog();
        }
        static int h, w;
        private void btnConnection_Click(object sender, EventArgs e)
        {
            InventaireDesProduitsFrm frm = new InventaireDesProduitsFrm();
            frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height + this.menuStrip1.Height+10);
            frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y -18);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm= new LivraisonFournisseurFrm();
            frm.Location = new Point(grp_cote.Location.X, grp_cote.Location.Y + 23);
            frm.Size = new System.Drawing.Size(width1 + width2 + 10, this.grp_principal.Height + 3);
            frm.ShowDialog();
        }

        private void btnRapportVente_Click(object sender, EventArgs e)
        {

            try
            {
                RapportVenteFrm frm = new RapportVenteFrm();
                frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height + this.menuStrip1.Height + 10);
                frm.Location = new Point(this.grp_cote.Location.X-3, this.grp_cote.Location.Y - 18);
                frm.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

        private void btnRapportLivraison_Click(object sender, EventArgs e)
        {
            try
            {
                var  frm = new StatistiquePharmacieFrm();
                frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                frm.ShowDialog();
            }
            catch (Exception)
            {}
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var btnClick = MonMessageBox.ShowBox("Voulez vous quitter l'application ?", "Confirmation", "confirmation.png");
            if (btnClick == "1")
            {
                if (MonMessageBox.ShowBox("Voulez vous quitter l'application en exportant les données?", "Confirmation", "confirmation.png") == "1")
                {
                    FolderBrowserDialog fbg = new FolderBrowserDialog();
                    if (fbg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                       
                        var path =fbg.SelectedPath + "\\Donnees_du_" + DateTime.Now.ToShortDateString().Trim().Replace("/", "-");
                        ConnectionClass.Backup(path);
                        ExporterLesDonnees();
                    }
                }
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lblUtilisateur.Text))
            {
                ChangerMotPasseFrm frm = new ChangerMotPasseFrm();
                frm.label1.Text = lblUtilisateur.Text;
                if (pictureBox1.Image != null)
                {
                    frm.pictureBox2.Image = pictureBox1.Image;
                }
                frm.ShowDialog();
            }
        }

        private void timerExpiration1_Tick(object sender, EventArgs e)
        {
            lblexpiration.Visible = true;
            lblexpiration.ForeColor = Color.Red;
            timerExpiration2.Start();
            timerExpiration1.Stop();
        }

        private void timerExpiration2_Tick(object sender, EventArgs e)
        {
            lblexpiration.Visible = true;
            lblexpiration.ForeColor = Color.White;
            timerExpiration1.Start();
            timerExpiration2.Stop();
        }

        private void lblexpiration_Click(object sender, EventArgs e)
        {
            try
            {
                InventaireDateExpiration frm = new InventaireDateExpiration();
                decimal montant = 0;
                decimal totalTMontant = 0;
                List<Medicament> listeArrayList = ConnectionClass.ListeDesMedicamentsExpirees();
                frm.dataGridView1.Rows.Add(
                   "PHARMACIE DE CESSION",
                      "", "", "", "");
                foreach (Medicament medicament in listeArrayList)
                {
                    frm.dataGridView1.Rows.Add(
                      medicament.NomMedicament.ToUpper(),
                     medicament.DateExpiration.ToShortDateString(),
                      medicament.Quantite.ToString(),
                      medicament.PrixAchat.ToString(),
                      (medicament.Quantite * medicament.PrixAchat).ToString()
                     );
                    montant += medicament.Quantite * medicament.PrixAchat;
                    totalTMontant += medicament.Quantite * medicament.PrixAchat;
                }
                frm.dataGridView1.Rows.Add(
              "TOTAL PHARMACIE DE CESSION",
                 "", "", "", montant);

                frm.dataGridView1.Rows.Add("", "", "", "", "");

                frm.dataGridView1.Rows.Add("PHARMACIE DEPOT",
             "", "", "", "");

                var liste = ConnectionClass.ListeDesLotsProduitsParCodeExpires();
                montant = 0;
                foreach (Medicament medicament in liste)
                {
                    frm.dataGridView1.Rows.Add(
                      medicament.NomMedicament.ToUpper() + " - " + medicament.NoLot,
                     medicament.DateExpiration.ToShortDateString(),
                      medicament.GrandStock.ToString(),
                      medicament.PrixAchat.ToString(),
                      (medicament.GrandStock * medicament.PrixAchat).ToString()
                     );
                    montant += medicament.GrandStock * medicament.PrixAchat;
                    totalTMontant += medicament.GrandStock * medicament.PrixAchat;
                }
                frm.dataGridView1.Rows.Add("TOTAL PHARMACIE DEPOT",
               "", "", "", montant);

                frm.dataGridView1.Rows.Add("TOTAL",
               "", "", "", totalTMontant);
                var nbre = liste.Count+ listeArrayList.Count;
                frm.label1.Text = "Nombre de medicament expiré ou en voie d'expiration : " + nbre;
                timerExpiration1.Stop();
                timerExpiration2.Stop();
                lblexpiration.Visible = false;
                frm.Location = new Point(grp_principal.Location.X, grp_principal.Location.Y + 22);
                frm.Size = new System.Drawing.Size(this.grp_principal.Width, this.grp_principal.Height + 3);
                frm.ShowDialog();

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste inventaire", exception);

            }
        }
   
        //verifier si le ya stock alerte
        private void VerificationStockAlerte()
        {

            List<Medicament> listeMedicament = ConnectionClass.ListeDesMedicamentsAStockBas();
            if (listeMedicament.Count > 0)
            {
                AlerteStockFrm frm = new AlerteStockFrm();
                frm.Location = new Point(168, 172);
                frm.dataGridView1.Rows.Clear();
                foreach (Medicament medicament in listeMedicament)
                {
                    frm.dataGridView1.Rows.Add
                    (
                        medicament.NomMedicament,
                        medicament.Quantite.ToString(),
                        medicament.QuantiteAlerte.ToString()
                    );

                }
                frm.Location = new Point(grp_principal.Location.X, grp_principal.Location.Y + 22);
                frm.Size = new System.Drawing.Size(this.grp_principal.Width, this.grp_principal.Height + 3);
                frm.ShowDialog();
            }
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            label1.Location = new Point(label1.Location.X + 5, label1.Location.Y);
            if (label1.Location.X > this.Size.Width)
            {
                label1.Location = new Point(190, label1.Location.Y);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DepenseFrm frm = new DepenseFrm();
                frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                InventaireFrm frm = new InventaireFrm();
                frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 15, this.grp_principal.Height);
                frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                frm.ShowDialog();
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            imprimerTable = Imprimer.ImprimerTableJournaliere(DateTime.Now);
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                printPreviewDialog1.ShowDialog();
            }  
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EnCaissementFrm frm = new EnCaissementFrm();
            frm.ShowDialog();
        }
        Bitmap imprimerTable;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(imprimerTable, 0, 0, imprimerTable.Width, imprimerTable.Height);
            e.HasMorePages = false;

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MonMessageBox.ShowBox("Exception Occured while releasing object ", ex);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void ExporterLesDonnees()
        {

            try
            {
                string data = null;
                int i = 0;
                int j = 0;

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet1;
                Excel.Worksheet xlWorkSheet2;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet1 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet2 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);

                // donnees de stocks
                xlWorkSheet1.Cells[3, 2] = "CODE FAMILLE";
                xlWorkSheet1.Cells[3, 3] = "CODE BARRE";
                xlWorkSheet1.Cells[3, 4] = "DESIGNATION";
                xlWorkSheet1.Cells[3, 5] = "PRIX ACHAT";
                xlWorkSheet1.Cells[3, 6] = "PRIX VENTE";
                xlWorkSheet1.Cells[3, 7] = "QTE";
                xlWorkSheet1.Cells[3, 8] = "QTE ALERTE";
                xlWorkSheet1.Cells[3, 9] = "DATE EXPIRATION";
                var ds = GestionPharmacetique.AppCode.ConnectionClass.InventaireGlobal();
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet1.Cells[i + 4, j + 2] = data;
                    }
                }

                // donnees de la famille
                xlWorkSheet2.Cells[3, 2] = "CODE FAMILLE";
                xlWorkSheet2.Cells[3, 3] = "FAMILLE";
                var dsFamille = GestionPharmacetique.AppCode.ConnectionClass.ExporterFamille();
                for (i = 0; i <= dsFamille.Tables[0].Rows.Count - 1; i++)
                {
                    for (j = 0; j <= dsFamille.Tables[0].Columns.Count - 1; j++)
                    {
                        data = dsFamille.Tables[0].Rows[i].ItemArray[j].ToString();
                        xlWorkSheet2.Cells[i + 4, j + 2] = data;
                    }
                }
                var path = @"Donnees_du_" + DateTime.Today.ToShortDateString().Trim().Replace("/", "-") + "_" + DateTime.Now.Hour + "h_" + DateTime.Now.Minute;

                xlWorkBook.SaveAs(path + ".xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                releaseObject(xlWorkSheet1);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);

                MonMessageBox.ShowBox("Les données ont été exportées vers le fichier Excel " + path + " sur mes documents", "Info", "affirmation.png");
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ImpoterLesDonneesExcels.Form1 frm = new ImpoterLesDonneesExcels.Form1();
            frm.ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Application.Exit();
        }

        private void conventionnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionDuneClinique.Formes.EntrepriseFrm();
            frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 15, this.grp_principal.Height);
            frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
            frm.ShowDialog();
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            var frm = new DepenseFrm();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (SGDP.Formes.DateFrm.ShowBox())
                {

                    var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    var dateFin = SGDP.Formes.DateFrm.dateFin;

                    var frm = new SGDP.Formes.JournalDesLivraisonsFrm();
                    frm.dateFin = dateFin;
                    frm.dateDebut = dateDebut;
                    frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                    frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);

                    frm.ShowDialog();

                }
            }
            catch { }
        }

        private void btnJournal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                btnTache.ContextMenuStrip = ctnJournal;
                btnTache.ContextMenuStrip.Show(btnJournal, e.Location);
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                SGDP.Formes.DateFrm.state = true;
                if (SGDP.Formes.DateFrm.ShowBox())
                {

                    var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    var dateFin = SGDP.Formes.DateFrm.dateFin;

                    var frm = new  RapportVenteFrm();
                    frm.dateFin = dateFin;
                    frm.dateDebut = dateDebut;
                    frm.siVenteTotal = SGDP.Formes.DateFrm.checkVenteTotal;
                    frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                    frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                    frm.ShowDialog();
                }
            }
            catch { }

        }
        BindingSource BS; string nomProduit;
        private void timerAnim1_Tick(object sender, EventArgs e)
        {
            try
            {

               // //BS.CurrentChanged += new System.EventHandler(this.ShowPoint);
               // BS.MoveNext();
               //var  imagePath = "C:\\Dossier Pharmacie\\Image Medicament\\" + lblImage.Text;
               // if (System.IO.File.Exists(imagePath))
               // {
               //     pictureBox3.Image = Image.FromFile(imagePath);
               // }
               // else
               // {
               //     pictureBox3.Image = null ;
               // }

                //var currentPosition = BS.CurrencyManager.Position + 1;
                //if (currentPosition == BS.Count)
                //{
                //    timerAnim1.Stop();
                //    BS.MoveLast();
                //    timerAnim2.Start();
                //}
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Rexhercher", ex); }
        }

        private void timerAnim2_Tick(object sender, EventArgs e)
        {
            //BS.MoveFirst();
            //timerAnim1.Start();
            //timerAnim2.Stop();
        }

        private void DetailArticleFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new ListeClientFrm();
                frm.Location = new Point(grp_cote.Location.X, grp_principal.Location.Y + 22);
                frm.Size = new System.Drawing.Size(this.grp_principal.Width + grp_cote.Width + 10, this.grp_principal.Height);
                frm.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                btnTache.ContextMenuStrip = ctxtTiers;
                btnTache.ContextMenuStrip.Show(btnTiers, e.Location);
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new SGDP.Formes.DepotFrm();
                frm.ShowDialog();
                
            }
            catch (Exception)
            { }
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                btnTache.ContextMenuStrip = ctxtParametres;
                btnTache.ContextMenuStrip.Show(btnParametre, e.Location);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                SGDP.Formes.FournisseurCtrl ctrl = new SGDP.Formes.FournisseurCtrl();
                ctrl.Location = new Point(0, 0);
                ctrl.Size = new System.Drawing.Size(grp_principal.Width, this.grp_principal.Height);
                grp_principal.Controls.Add(ctrl);
            }
            catch (Exception)
            { }
        }

        private void btnInventaire_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                btnInventaire.ContextMenuStrip = ctxInventaire;
                btnInventaire.ContextMenuStrip.Show(btnParametre, e.Location);
            }
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            InventaireFrm frmVente = new InventaireFrm();
            frmVente.Location = new Point(grp_cote.Location.X, grp_cote.Location.Y + 23);
            frmVente.Size = new System.Drawing.Size(width1 + width2 + 10, this.grp_principal.Height +3);
            frmVente.ShowDialog();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            InventaireDesProduitsFrm frmVente = new InventaireDesProduitsFrm();
            frmVente.Location = new Point(grp_cote.Location.X, grp_cote.Location.Y + 23);
            frmVente.Size = new System.Drawing.Size(width1 + width2 + 10, this.grp_principal.Height + 3);
            frmVente.ShowDialog();
        }
        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            DepenseFrm frmVente = new DepenseFrm();
            frmVente.Location = new Point(grp_cote.Location.X, grp_cote.Location.Y -25);
            frmVente.Size = new System.Drawing.Size(width1 + width2 + 10, this.grp_principal.Height + 48);
            frmVente.ShowDialog();
        }

        private void btnTache_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                btnTache.ContextMenuStrip = ctxtTaches;
                btnTache.ContextMenuStrip.Show(btnTache, e.Location);
            }
        }

        private void chronologieVenteParPrpduitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (SGDP.Formes.DateFrm.ShowBox())
                {
                    if (ListeProduitFrm.ShowBox() == "1")
                    {
                        var designation = ListeProduitFrm.designation;
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

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                if (SGDP.Formes.DateFrm.ShowBox())
                {

                    var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    var dateFin = SGDP.Formes.DateFrm.dateFin;

                    var frm = new SGDP.Formes.JournalDesLivraisonsFrm();
                    frm.dateFin = dateFin;
                    frm.dateDebut = dateDebut;
                    frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                    frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                    frm.ShowDialog();
                }
            }
            catch { }
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            var frm = new EnCaissementFrm();
            frm.ShowDialog();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            try
            {
                SGDP.Formes.DateFrm.state = true;
                if (SGDP.Formes.DateFrm.ShowBox())
                {

                    var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    var dateFin = SGDP.Formes.DateFrm.dateFin;

                    var frm = new  StatistiquePharmacieFrm ();
                    frm.dateFin = dateFin;
                    frm.dateDebut = dateDebut;
                    frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                    frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                    frm.ShowDialog();
                }
            }
            catch { }
        }

        private void etatDesConventionnésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new RapportDesEntreprises();
            frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
            frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25); 
            frm.ShowDialog();
        }

        private void conventionnésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new  GestionDuneClinique.Formes.EntrepriseFrm();
            frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
            frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
            frm.ShowDialog();
        }

        private void produitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MedicamentFrm medicamentFrm = new MedicamentFrm();
                medicamentFrm.etat = "1";
                medicamentFrm.Location = new Point(grp_cote.Location.X, grp_principal.Location.Y + 18);
                medicamentFrm.Size = new System.Drawing.Size(this.grp_principal.Width + grp_cote.Width + 10, this.grp_principal.Height+5);
                medicamentFrm.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void entréeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var ctrl = new SGDP.Formes. DocStock();
                ctrl.etat = 1;
                this.grp_principal.Controls.Clear();
                ctrl.Location = new Point(0, 0);
                ctrl.Size = new Size(this.grp_principal.Size.Width , this.grp_principal.Size.Height);
                this.grp_principal.Controls.Add(ctrl);
            }
            catch (Exception)
            { }
        }

        private void sortieStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var ctrl = new SGDP.Formes.DocStock();
                ctrl.etat = 2;
                this.grp_principal.Controls.Clear();
                ctrl.Location = new Point(0, 0);
                ctrl.Size = new Size(this.grp_principal.Size.Width, this.grp_principal.Size.Height);
                this.grp_principal.Controls.Add(ctrl);
            }
            catch (Exception)
            { }
        }

        private void jouralDeLaCaisseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SGDP.Formes.DateFrm.state = true;
                if (SGDP.Formes.DateFrm.ShowBox())
                {

                    var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    var dateFin = SGDP.Formes.DateFrm.dateFin;

                    var frm = new RapportVenteFrm();
                    frm.dateFin = dateFin;
                    frm.dateDebut = dateDebut;
                    frm.siVenteTotal = SGDP.Formes.DateFrm.checkVenteTotal;
                    frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                    frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                    frm.ShowDialog();
                }
            }
            catch { }

        }

        private void journalDesPaiementsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                SGDP.Formes.DateFrm.state = true;
                if (SGDP.Formes.DateFrm.ShowBox())
                {

                    var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    var dateFin = SGDP.Formes.DateFrm.dateFin;
                    var frm = new CaisseFrm();
                    frm.dateFin = dateFin;
                    frm.dateDebut = dateDebut;
                    //frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                    //frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                    frm.siVenteTotal = SGDP.Formes.DateFrm.checkVenteTotal;
                    frm.ShowDialog();
                }
            }
            catch { }

        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            
        }

        private void etatJournalierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SGDP.Formes.DateFrm.state = true;
                if (SGDP.Formes.DateFrm.ShowBox())
                {

                    var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    var dateFin = SGDP.Formes.DateFrm.dateFin;
                    var frm = new ComptabiliteFrm();
                    frm.index = "j";
                    frm.dateFin = dateFin;
                    frm.dateDebut = dateDebut;
                    frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                    frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                    frm.ShowDialog();
                }
            }
            catch { }
        }

        private void etatMensuelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SGDP.Formes.DateFrm.state = true;
                if (SGDP.Formes.DateFrm.ShowBox())
                {

                    var dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    var dateFin = SGDP.Formes.DateFrm.dateFin;
                    var frm = new ComptabiliteFrm();
                    frm.index = "m";
                    frm.dateFin = dateFin;
                    frm.dateDebut = dateDebut;
                    frm.Size = new System.Drawing.Size(this.grp_principal.Width + this.grp_cote.Width + 10, this.grp_principal.Height);
                    frm.Location = new Point(this.grp_cote.Location.X, this.grp_cote.Location.Y + 25);
                    frm.ShowDialog();
                }
            }
            catch { }
        }

        private void etatStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var medicamentFrm = new EtatStockFrm();
            medicamentFrm.Location = new Point(grp_cote.Location.X, grp_cote.Location.Y + 25);
            medicamentFrm.Size = new System.Drawing.Size(width1 + width2 + 10, this.grp_principal.Height + 6);
            medicamentFrm.ShowDialog();
        }

        

    }
}
