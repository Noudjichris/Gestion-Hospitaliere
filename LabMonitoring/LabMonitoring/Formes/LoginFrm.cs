using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionAcademique
{
    public partial class LoginFrm : Form
    {
        public LoginFrm()
        {
            InitializeComponent();
        }

        private void LoginFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.White,
                Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        static LoginFrm loginFrm;
        static string btnClick;

        public static string ShowBox()
        {

            try
            {
                loginFrm = new LoginFrm();
                loginFrm.Location = new Point();//(((Form1.width+228) - loginFrm.Width) / 2, ((Form1.height+125) - loginFrm.Height) / 2);
                loginFrm.ShowDialog();
            }
            catch (Exception)
            {
            }
            return btnClick;
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            btnClick = "0";
            loginFrm.Dispose();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            loginFrm.progressBar1.Visible = true;
            loginFrm.lblEtat.Text = "Connection au serveur de la base de données...  ";
            loginFrm.lblEtat.Visible = true;
            loginFrm.timerProgressBar1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Visible = true;
            label3.ForeColor = Color.Red;
            timer2.Start();
            timer1.Stop();
          
        }
        public static string nomUtilisateur,matricule, typeUtilisateur, nom, photo;
        public static int  numUtilisateur;
        private void timer2_Tick(object sender, EventArgs e)
        {
            label3.Visible = true;
            label3.ForeColor = Color.White;
            timer1.Start();
            timer2.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            var listeUtilisateur =LabMonitoring.AppCode. ClassClinique.ListesDesUtilisateurs().Where(l=> !l.TypeUtilisateur.ToLower().Contains("cais"));
            foreach (var utilisteur in listeUtilisateur)
            {
                comboBox1.Items.Add(utilisteur.NomUtilisateur.ToUpper());
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
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
                loginFrm.timerProgressBar1.Enabled = false;
                loginFrm.progressBar1.Value = 0;
                loginFrm.progressBar2.Visible = true;
                lblEtat.Text = "Vérification du mot de passe...";
                loginFrm.timerProgressBar2.Start();
            }
        }
        //public SpeechSynthesizer _synthesizer;
        private void timer4_Tick(object sender, EventArgs e)
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
                    loginFrm.timerProgressBar2.Dispose();
                    loginFrm.progressBar2.Visible = false;
                    loginFrm.progressBar2.Value = 0;
                    loginFrm.lblEtat.Visible = false;

                    var nomUtil = loginFrm.comboBox1.Text.Trim();
                    var motPasse = loginFrm.txtMotPasse.Text.Trim().GetHashCode().ToString();
                    var utilisateur = LabMonitoring.AppCode.ClassClinique.SeConnecter(nomUtil, motPasse);
                    if (utilisateur != null)
                    {
                        typeUtilisateur = utilisateur.TypeUtilisateur;
                        nom = utilisateur.NomEmploye;
                        matricule = utilisateur.NumEmploye;
                        nomUtilisateur = nomUtil;
                        numUtilisateur = utilisateur.NumeroUtilisateur;
                        photo = utilisateur.Photo;
                        btnClick = "1";
                        Close();
                    }
                    else
                    {
                        //ConnectionClassClinique.Tracker(nomUtil, nom, false);
                        btnClick = "2";
                        timer1.Start();
                        timer2.Stop();
                        label3.ForeColor = Color.Red;
                        timer2.Start();
                        timer1.Stop();
                        btnClick = "0";
                    }
                }
            }catch{}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = true;
            txtMotPasse.Enabled = true;
            txtMotPasse.Focus();
        }

        private void txtMotPasse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(btnLogin.Enabled ==true)
                {
                    btnLogin_Click(null, null);
                }
            }
        }

    }
}
