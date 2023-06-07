using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.LightSkyBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CadetBlue, Color.CadetBlue,  LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPassword.Enabled = true;
            btnLogin.Enabled = true;
            txtPassword.Focus();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<AppCode.Utilisateur> listeUtilisateur = AppCode.ConnectionClass.ListesDesUtilisateurs();
                comboBox1.Items.Clear();
                foreach(AppCode.Utilisateur utilisateur in listeUtilisateur)
                {
                    comboBox1.Items.Add(utilisateur.NomUtilisateur.ToUpper());
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Forme login load", ex); }
        }

        


  
    }
}
