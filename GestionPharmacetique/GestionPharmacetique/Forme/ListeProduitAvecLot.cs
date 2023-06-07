using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class ListeProduitAvecLot : Form
    {
        public ListeProduitAvecLot()
        {
            InitializeComponent();
        }

        void ListeProduit(string index)
        {
            try
            {
                List<AppCode.Medicament> listeMedicament = AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom(index);

                dgvProduit.Rows.Clear();
                foreach (var produit in listeMedicament)
                {
                    var lst = AppCode.ConnectionClass.ListeDesLotsProduitsParCode(produit.NumeroMedicament);
                    if (lst.Count > 0 )
                    {
                        foreach (var p in lst)
                        {
                            if (produit.GrandStock > 0)
                            {
                                dgvProduit.Rows.Add(
                                   produit.NumeroMedicament,
                                   produit.NomMedicament.ToUpper(),
                               produit.PrixAchat,
                               produit.PrixVente,
                               p.DateExpiration.ToShortDateString(),
                               p.NoLot, produit.Quantite,
                               p.GrandStock
                               );
                                if (DateTime.Now.Date.AddMonths(3) >= p.DateExpiration && (p.GrandStock + p.Quantite) > 0)
                                {  dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;
                                dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.OrangeRed;}
                            }
                        }
                    }
                    else
                    {
                        dgvProduit.Rows.Add(
                            produit.NumeroMedicament,
                            produit.NomMedicament.ToUpper(),
                        produit.PrixAchat,
                        produit.PrixVente,
                        produit.DateExpiration.ToShortDateString(), "",
                        produit.Quantite, produit.GrandStock
                        );
                        if (DateTime.Now.Date.AddMonths(3) >= produit.DateExpiration && (produit.GrandStock + produit.Quantite) > 0)
                        {
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.BackColor = Color.OrangeRed;
                            dgvProduit.Rows[dgvProduit.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.OrangeRed;
                        }
                    }
                }
            }
            catch { }
        }

        private void ListeProduitAvecLot_Load(object sender, EventArgs e)
        {
            if (state == "1")
            {
                Column4.Visible = false;
            }
            ListeProduit(indexRecherche);
        }

        public static string numeroProduit, designation, btnClick, indexRecherche, NoLot, state = "0";
        public static ListeProduitAvecLot frm;
        public static decimal stock, prixPublic, prixCession;
        public static DateTime dateExpiration;
        public static String ShowBox()
        {
            try
            {
                frm = new ListeProduitAvecLot();
                frm.ShowDialog();
                return btnClick;
            }
            catch { return null; }
        }

        private void dgvProduit_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.SelectedRows.Count > 0)
                {
                    numeroProduit = dgvProduit.SelectedRows[0].Cells[0].Value.ToString();
                    designation = dgvProduit.SelectedRows[0].Cells[1].Value.ToString();
                    prixCession = Convert.ToDecimal(dgvProduit.SelectedRows[0].Cells[2].Value.ToString());
                    prixPublic = Convert.ToDecimal(dgvProduit.SelectedRows[0].Cells[3].Value.ToString());
                    stock = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[7].Value.ToString());
                    dateExpiration = Convert.ToDateTime(dgvProduit.SelectedRows[0].Cells[4].Value.ToString());
                    NoLot = dgvProduit.SelectedRows[0].Cells[5].Value.ToString();
                    btnClick = "1";
                    Dispose();
                }

            }
            catch { }
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

    }
}
