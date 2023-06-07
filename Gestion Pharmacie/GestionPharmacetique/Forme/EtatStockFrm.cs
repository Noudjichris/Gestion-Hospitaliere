using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class EtatStockFrm : Form
    {
        public EtatStockFrm()
        {
            InitializeComponent();
        }

        private void EtatStockFrm_Load(object sender, EventArgs e)
        {
            try
            {
                button3.Location = new Point(Width - 45, button3.Location.Y);
                cl2.Width = dgvProduit.Width / 2;
                if (Form1.typeUtilisateur == "caissier")
                {
                    cl3.Visible = false;
                }
            }
            catch { }
        }

        private void txtTaux_TextChanged(object sender, EventArgs e)
        {
            dgvProduit.Rows.Clear();
            var liste = from l in AppCode.ConnectionClass.ListeEtatStock(dateTimePicker1.Value.Date)
                        join m in AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom( txtTaux.Text)
                        on l.NumeroMedicament equals m.NumeroMedicament
                        select new { l.DateExpiration, l.GrandStock, l.Quantite, m.NomMedicament };
            foreach (var l in liste)
                dgvProduit.Rows.Add(l.DateExpiration, l.NomMedicament, l.GrandStock, l.Quantite);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dgvProduit.Rows.Clear();
                var liste =from l in AppCode.ConnectionClass.ListeEtatStock(dateTimePicker1.Value.Date)
                                join m in AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom("")
                                on l.NumeroMedicament equals m.NumeroMedicament                              
                           select new { l.DateExpiration,l.GrandStock,l.Quantite,m.NomMedicament};
                foreach (var l in liste)
                    dgvProduit.Rows.Add(l.DateExpiration,l.NomMedicament, l.GrandStock, l.Quantite);
            }
            catch { }
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
