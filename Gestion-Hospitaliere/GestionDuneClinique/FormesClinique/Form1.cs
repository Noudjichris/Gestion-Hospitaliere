using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.Formes;
using GestionDuneClinique;
namespace GestionAcademique
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var  mGraphics = e.Graphics;
            var  pen1 = new Pen(Color.FromArgb(255, 192, 128), 0);
            var  area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var  linearGradientBrush = new LinearGradientBrush(area1, Color.White,Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void menuStrip1_Paint(object sender, PaintEventArgs e)
        {
            var  mGraphics = e.Graphics;
            var  pen1 = new Pen(Color.AliceBlue, 0);
            var  area1 = new Rectangle(0, 0, this.menuStrip1.Width - 1, this.menuStrip1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var  mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(255, 128, 0), 0);
            var  area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var  mGraphics = e.Graphics;
            var  pen1 = new Pen(Color.WhiteSmoke,5);
            var  area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            var  linearGradientBrush = new LinearGradientBrush(area1, Color.White,
                Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var  mGraphics = e.Graphics;
            var  pen1 = new Pen(Color.FromArgb(255, 128, 0), 0);
            var  area1 = new Rectangle(0, 0, this.panel1.Width - 1, this.panel1.Height - 1);
            var  linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        //
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            height = Height - 125;
            width = Width - 228;
            timer1.Start();
            xLocation = 205;
            yLocation = 120;
            //SeDeconnecter();   
            //if(DateTime.Now >= DateTime.Parse("12/12/2019") && DateTime.Now < DateTime.Parse("13/12/2019"))
            //{
            //var mot = "001287".GetHashCode().ToString();

            ////GestionDuneClinique.AppCode.ConnectionClassClinique.ModifierMotDePasse("ADMINISTRATEUR", mot, "281218".GetHashCode().ToString());
            //}
        }

        void SeDeconnecter()
        {
            toolStripMenuItem2.Visible = false;
            factureToolStripMenuItem.Visible = false;
            entrepriseToolStripMenuItem.Visible = false;
            administrerToolStripMenuItem.Visible = false;
            rapportToolStripMenuItem.Visible = false;
            fichierToolStripMenuItem.Visible = false;
            employeToolStripMenuItem1.Visible = false;
            verssementToolStripMenuItem.Enabled = false;
            recettesToolStripMenuItem.Enabled = false;
        }

        void SeConnecter()
        {
            if (LoginFrm.typeUtilisateur == "admin")
            {
                toolStripMenuItem2.Visible = true;
                factureToolStripMenuItem.Visible = true;
                entrepriseToolStripMenuItem.Visible = true;
                administrerToolStripMenuItem.Visible = true;
                rapportToolStripMenuItem.Visible = true;
                fichierToolStripMenuItem.Visible = true;
                employeToolStripMenuItem1.Visible = true;
                verssementToolStripMenuItem.Enabled = true;
                employéToolStripMenuItem.Enabled = true;
                rapportDunMedecinToolStripMenuItem.Enabled = true ;
                hospitalisationToolStripMenuItem.Enabled = true ;
                consultationToolStripMenuItem.Enabled = true ;
                analyseToolStripMenuItem.Enabled = true ;
                logToolStripMenuItem.Enabled = true ;
                rapportDunMedecinToolStripMenuItem.Enabled = true ;
                recettesToolStripMenuItem.Enabled = true;
                ordonnanceToolStripMenuItem.Enabled = false;
            }
            else if (LoginFrm.typeUtilisateur == "admin assistance")
            {
                toolStripMenuItem2.Visible = true;
                factureToolStripMenuItem.Visible = true;
                entrepriseToolStripMenuItem.Visible = true;
                administrerToolStripMenuItem.Visible = true;
                rapportToolStripMenuItem.Visible = true;
                fichierToolStripMenuItem.Visible = true;
                employeToolStripMenuItem1.Visible = true;
                verssementToolStripMenuItem.Enabled = true;
                employéToolStripMenuItem.Enabled = true;
                rapportDunMedecinToolStripMenuItem.Enabled = true;
                hospitalisationToolStripMenuItem.Enabled = true;
                consultationToolStripMenuItem.Enabled = true;
                analyseToolStripMenuItem.Enabled = true;
                logToolStripMenuItem.Enabled = true;
                rapportDunMedecinToolStripMenuItem.Enabled = true;
                recettesToolStripMenuItem.Enabled = true;
            }
            else if (LoginFrm.typeUtilisateur == "caissier")
            {
                factureToolStripMenuItem.Visible = true;
                fichierToolStripMenuItem.Visible = true;
                administrerToolStripMenuItem.Visible = true;
                entrepriseToolStripMenuItem.Visible = true;
                hospitalisationToolStripMenuItem.Enabled = true;
                consultationToolStripMenuItem.Enabled = true;
                analyseToolStripMenuItem.Enabled = true;
                rapportDeLaCaisseToolStripMenuItem.Enabled = true;
                rapportToolStripMenuItem.Visible = true;
                rapportDunMedecinToolStripMenuItem.Enabled = false;
                verssementToolStripMenuItem.Enabled = false;
                recettesToolStripMenuItem.Enabled = true;
                acteDuPatientToolStripMenuItem.Enabled = true; employeToolStripMenuItem1.Visible = true;
            }
            else if (LoginFrm.typeUtilisateur == "recept")
            {
                fichierToolStripMenuItem.Visible = true;
                ordonnanceToolStripMenuItem.Visible = false;
                consultationToolStripMenuItem.Visible = false;
                analyseToolStripMenuItem.Visible = false;
                hospitalisationToolStripMenuItem.Visible = false;
                ordonnanceToolStripMenuItem.Visible = false;
            }
        }

       public static int  height, width, xLocation, yLocation;
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            var  mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(255, 128, 0), 0);
            var  area1 = new Rectangle(0, 0, groupBox3.Width - 1, groupBox3.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        //se connecter
        private void seConnecterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(LoginFrm.ShowBox()=="1")
            {
                lblUtilisateur.Text = LoginFrm.nomUtilisateur;
                lblHeure.Text = "Connecter à " + DateTime.Now.ToShortTimeString();
                SeConnecter();
                seDeconnecterToolStripMenuItem.Enabled = true;
                seConnecterToolStripMenuItem.Enabled = false;
            }
        }

        private void nouveauPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm  = new PatientFrm();
            frm.Location = new Point(xLocation, yLocation);
            
            frm.ShowDialog();
        }

        private void comptesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoginFrm.ShowBox() == "1")
            {
                if (LoginFrm.typeUtilisateur == "admin")
                {
                    var frm = new GestionDesPelerinsTchad.Formes.AdminFrm();
                    frm.ShowDialog();
                }
                else
                {
                    MonMessageBox.ShowBox("Vous n'etest pas autoriser a faire cette operation", "Erreur", "erreur.png");
                }
            }
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoginFrm.ShowBox() == "1")
            {
                if (LoginFrm.typeUtilisateur == "admin")
                {
                    var frm = new GestionDesPelerinsTchad.Formes.TrackFrm();
                    frm.ShowDialog();
                }
                else
                {
                    MonMessageBox.ShowBox("Vous n'etest pas autoriser a faire cette operation", "Erreur", "erreur.png");
                }
            }
        }

        private void modifierMonCompteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionDesPelerinsTchad.Formes.ModifierMotPasse();
            frm.ShowDialog();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MonMessageBox.ShowBox("Voulez vous quitter l'application ?", "Confirmation", "confirmation.pnng") == "1")
            {
                Close();
            }
        }

        private void ordonnanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           var frm = new GestionDuneClinique.AppCode. OrdonnanceFrm();
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void consultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ConsultationFrm();
            frm.Location = new Point(xLocation, yLocation);
            frm.Height = height;
            frm.flag = true;
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.ShowDialog();
        }

        private void analyseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionPharmacetique.Forme.ExamenFrm();
            frm.Size = new System.Drawing.Size(width, height);
            frm.flag = true;
            frm.ShowDialog();
        }

        private void hospitalisationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new HospitalisationFrm();
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void factureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FactureFrm();
            frm.Size = new Size(width+190, height);
            frm.Location = new Point(18, 117);
            frm.ShowDialog();
        }

        private void rapportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new RapportEntrepriseFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void entrepriseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new EntrepriseFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void rapportDunMedecinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new RapportMedecinFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void rapportDeLaCaisseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new RapportCaissierFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }


        private void pharmacieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path1 = @"C:\Program Files\Legacy Computer Engineering\PharmLogi\GestionPharmacetique.exe";
            var path2 = @"C:\Program Files (x86)\Legacy Computer Engineering\PharmLogi\GestionPharmacetique.exe";
            if(System.IO.File.Exists(path1))
            {
              System.Diagnostics.Process.Start(path1);
            }else if(System.IO.File.Exists(path2))
            {
              System.Diagnostics.Process.Start(path2);
            }

        }

        private void seDeconnecterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeDeconnecter();
            seDeconnecterToolStripMenuItem.Enabled = false;
            seConnecterToolStripMenuItem.Enabled = true;
        }


        private void employeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new GestionPharmacetique.Forme.EmployeFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void rapportDUnPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new RapportPatientFrm();
            frm.ShowDialog();
        }

        private void employéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionDuneClinique.FormesClinique.EntreEmplFrm();
            frm.Location = new Point(xLocation, yLocation);
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.ShowDialog();
        }

        private void certificatDeNaissanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionDuneClinique.FormesClinique.ActeDeNaissanceFrm();
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void acteDeGrossessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionDuneClinique.FormesClinique.CertificatDeGrossesseFrm();
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void certificatDeDecésToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var frm = new GestionDuneClinique.FormesClinique.CertificatDeDecesFrm();
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void acteDuPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
           var frm = new RapportPatientFrm();
          
           frm.Location = new Point(xLocation, yLocation);
           frm.ShowDialog();
        }

        private void rapportDunMedecinToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var frm = new RapportMedecinFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
           
        }

        private void administrerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verssementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionPharmacetique.Forme.EncaissementFrm();

            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }

        private void rapportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var pathFolder = "C:\\Backup Folder";
                if (!System.IO.Directory.Exists(pathFolder) )
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                var path = "C:\\Backup Folder\\Donnees_Clinique_du_" + DateTime.Now.ToShortDateString().Trim().Replace("/", "_")+"_"+DateTime.Now.ToShortTimeString().Replace(":","_");
                var path1 = "C:\\Backup Folder\\Donnees_Pharmacie_du_" + DateTime.Now.ToShortDateString().Trim().Replace("/", "_") + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_");
                GestionDuneClinique.AppCode.ConnectionClassClinique.Backup(path);
                GestionDuneClinique.AppCode.ConnectionClassPharmacie.Backup(path1);
            }
            catch (Exception Exception) { MonMessageBox.ShowBox("Back up", Exception); }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GestionDuneClinique.AppCode.ConnectionClassClinique.Restore(ofd.FileName);
                MonMessageBox.ShowBox("Good","c","affirmation.png");
            }
        }

        private void recettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionDuneClinique.FormesClinique.JournalDesRecettesFrm();
            frm.Location = new Point(xLocation, yLocation+5);
            frm.Height = height-15;
            frm.ShowDialog();
        }

        private void statistiquesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new GestionDuneClinique.FormesClinique.StatistiqueFrm();
                frm.Size = new Size(width + 190, height);
                frm.Location = new Point(18, 117);
                frm.ShowDialog();
            }
            catch { }
        }

        private void observationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new GestionDuneClinique.FormesClinique.ObservationFrm();
            frm.Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            frm.Location = new Point(xLocation, yLocation);
            frm.ShowDialog();
        }   

 
    }
}
