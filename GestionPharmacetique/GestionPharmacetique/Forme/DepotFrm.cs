using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGDP.Formes
{
    public partial class DepotFrm : Form
    {
        public DepotFrm()
        {
            InitializeComponent();
        }

        private void btnPaiement_Click(object sender, EventArgs e)
        {
            try
            {
                var depot = new GestionPharmacetique.AppCode.Depot();
                depot.ID = 1;
                depot.Adresse = txtAdresse.Text;
                depot.Commentaire = txtCommentaire.Text;
                depot.Email = txtEmail.Text;
                depot.NomDepot = txtDepot.Text;
                depot.Pays = txtPays.Text;
                depot.Reference = txtRef.Text;
                depot.Telephone1= txtTele1.Text;
                depot.Telephone2 = txtTele2.Text;
                depot.Ville = txtVille.Text;
                GestionPharmacetique.AppCode.ConnectionClass.ModifierUnDepot(depot);
            }
            catch { }
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,Color.SteelBlue
                ,Color.SlateGray, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void DepotFrm_Paint(object sender, PaintEventArgs e)
        {
        Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,SystemColors.ControlLight
                , SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void DepotFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var listeDepot =GestionPharmacetique. AppCode.ConnectionClass.ListeDesDepots();
                foreach (var depot in listeDepot)
                {
                    txtAdresse.Text = depot.Adresse;
                    txtCommentaire.Text = depot.Commentaire;
                    txtDepot.Text = depot.NomDepot;
                    txtEmail.Text = depot.Email;
                    txtPays.Text = depot.Pays;
                    txtRef.Text = depot.Reference;
                    txtTele1.Text = depot.Telephone1;
                    txtTele2.Text = depot.Telephone2;
                    txtVille.Text = depot.Ville;
                    
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
