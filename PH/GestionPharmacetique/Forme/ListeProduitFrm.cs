using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace GestionPharmacetique.Forme
{
    public partial class ListeProduitFrm : Form
    {
        public ListeProduitFrm()
        {
            InitializeComponent();
        }

        private void ListeProduitFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ScrollBar, 3);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                , SystemColors.ScrollBar, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        void ListeProduit(string index)
        {
            try
            {
                List<Medicament> listeMedicament = ConnectionClass.ListeDesMedicamentsRechercherParNom(index);
                
                dgvProduit.Rows.Clear();
                foreach (var produit in listeMedicament)
                {
                    dgvProduit.Rows.Add(
                        produit.NumeroMedicament,
                        produit.NomMedicament.ToUpper(),
                    produit.PrixAchat,
                    produit.PrixVente,
                    produit.Quantite,
                    produit.DateExpiration.ToShortDateString(),"0",produit.GrandStock
                    );


                    if (produit.NombreBoite > 0 && produit.PrixVenteDetail > 0)
                    {
                        dgvProduit.Rows.Add(
                        produit.NumeroMedicament.ToUpper(),
                        produit.NomMedicament.ToUpper() + " DETAIL", (int)produit.PrixAchat / produit.NombreBoite,
                        produit.PrixVenteDetail.ToString(), produit.NombreDetail,
                        produit.DateExpiration.ToShortDateString(),
                        "1", produit.GrandStock
                    );
                    }
                }        
            }
            catch { }
        }

        private void ListeProduitFrm_Load(object sender, EventArgs e)
        {
            if (state == "1")
            {
                Column4.Visible = false;
            }
            ListeProduit(indexRecherche);
        }

        public static string numeroProduit, designation, btnClick, indexRecherche,indexDetail, state="0";
        public static ListeProduitFrm frm;
        public static double  stock, prixPublic, prixCession;
        public static DateTime dateExpiration;
        public static String ShowBox()
        {
            try
            {
                frm = new ListeProduitFrm();
                frm.ShowDialog();
                return btnClick;
            }
            catch { return null; }
        }

        private void dgvProduit_DoubleClick(object sender, EventArgs e)
        {
            try{
                if (dgvProduit.SelectedRows.Count > 0)
                {
                    numeroProduit = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                    designation = dgvProduit.SelectedRows[0].Cells[1].Value.ToString();
                    prixCession = Convert.ToDouble(dgvProduit.SelectedRows[0].Cells[2].Value.ToString());
                    prixPublic = Convert.ToDouble(dgvProduit.SelectedRows[0].Cells[3].Value.ToString());
                    stock = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[4].Value.ToString());
                    dateExpiration  = Convert.ToDateTime(dgvProduit.SelectedRows[0].Cells[5].Value.ToString());
                    indexDetail = dgvProduit.SelectedRows[0].Cells[6].Value.ToString();
                    btnClick = "1";
                    Dispose();
                }

            }
            catch{}
        }

        private void dgvProduit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvProduit_DoubleClick(null, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                btnClick = "2";
                Dispose();
            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
