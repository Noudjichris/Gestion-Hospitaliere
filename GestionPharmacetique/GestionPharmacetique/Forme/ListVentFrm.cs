using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class ListVentFrm : Form
    {
        public ListVentFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.CornflowerBlue, Color.CadetBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void ListVentFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ActiveCaption, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
         SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void ListVentFrm_Load(object sender, EventArgs e)
        {
            btnFermer.Location = new Point(Width - 40, 2);
            cl1.Width = dgvVente.Width / 6; 
            //cl2.Width = dgvVente.Width / 6; 
            cl3.Width = dgvVente.Width / 3-40; 
            //cl4.Width = dgvVente.Width / 3; 
            cl5.Width = dgvVente.Width / 11; 
            cl6.Width = dgvVente.Width / 11; 
            cl7.Width= dgvVente.Width / 11;
            cl0.Width = dgvVente.Width / 12;
            Column1.Width = 40;
            ListeVente();
        }

        private void borderLabel1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //liste des vente
        private void ListeVente()
        {
            try
            {
                 dgvVente.Rows.Clear();
                DataTable dt = AppCode.ConnectionClass.ListeDesVentesParDateVente(DateTime.Now.Date);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    var date = dt.Rows[i].ItemArray[1].ToString();
                    var heure = "00:00::00";

                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[8].ToString()))
                    {
                        heure = dt.Rows[i].ItemArray[8].ToString();
                    }
                    var dateVente = date.Substring(0, 10) + " " + heure;
                    dgvVente.Rows.Add(
                        dt.Rows[i].ItemArray[0].ToString(),
                        dateVente,
                        dt.Rows[i].ItemArray[2].ToString().ToUpper(),
                        dt.Rows[i].ItemArray[3].ToString(),
                        dt.Rows[i].ItemArray[4].ToString(),
                        dt.Rows[i].ItemArray[5].ToString(),
                        dt.Rows[i].ItemArray[6].ToString(),
                          dt.Rows[i].ItemArray[7].ToString()
                    );
                }
            }
            catch { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (Form1.typeUtilisateur == "admin")
                {
                    if (dgvVente.SelectedRows.Count > 0)
                    {

                        int numVente = Int32.Parse(dgvVente.SelectedRows[0].Cells[0].Value.ToString());
                        var listeVente = AppCode.ConnectionClass.ListeDesVentes(numVente);
                        if (listeVente.Rows[0].ItemArray[4].ToString() == GestionPharmacetique.Form1.numEmploye || GestionPharmacetique.Form1.typeUtilisateur == "admin")
                        {
                            if (MonMessageBox.ShowBox("Voulez vous supprimer cette vente?", "Confirmation", "confirmation.png") == "1")
                            {
                                string num_medi = dgvVente.SelectedRows[0].Cells[2].Value.ToString();
                                string nom_medi = dgvVente.SelectedRows[0].Cells[2].Value.ToString();
                                AppCode.ConnectionClass.SupprimerVentes(numVente, num_medi);
                                AppCode.ConnectionClass.SupprimerVentesComplet(numVente, num_medi);
                               AppCode. ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "annulation de la vente  numero " + numVente + " du produit " + nom_medi, this.Name);
                                ListeVente();
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Vous n'etes pas autorisés à supprimer cette vente car elle ne vous appartient pas.", "Erreur", "erreur.png");
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Vous n'etes autorisés à faire ces opérations", "Erreur", "erreur,png");
                }
            }
            catch { }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }
        private void dgvVente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                linkLabel1_LinkClicked(null, null);
                 
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtRechercherProduit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvVente.Rows.Clear();
                string requete = "SELECT vente.num_vente,detail_vente.date_vente, vente.num_medi,medicament.nom_medi, vente.prix_achat, vente.prix_vente, " +
                 " vente.quantite, vente.prixTotal,detail_vente.heure FROM ((vente INNER JOIN detail_vente " +
                 " ON vente.num_vente = detail_vente.num_vente) INNER JOIN medicament ON vente.num_medi = medicament.num_medi)" +
                 " WHERE detail_vente.num_vente = "+ Convert.ToInt32(txtRechercherProduit.Text) +" ORDER BY medicament.nom_medi" ;
                DataTable dt = AppCode.ConnectionClass.TableRows(requete);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    var date = dt.Rows[i].ItemArray[1].ToString();
                    var heure = "00:00::00";

                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[8].ToString()))
                    {
                        heure = dt.Rows[i].ItemArray[8].ToString();
                    }
                    var dateVente = date.Substring(0, 10) + " " + heure;
                    dgvVente.Rows.Add(
                        dt.Rows[i].ItemArray[0].ToString(),
                        dateVente,
                        dt.Rows[i].ItemArray[2].ToString().ToUpper(),
                        dt.Rows[i].ItemArray[3].ToString(),
                        dt.Rows[i].ItemArray[4].ToString(),
                        dt.Rows[i].ItemArray[5].ToString(),
                        dt.Rows[i].ItemArray[6].ToString(),
                          dt.Rows[i].ItemArray[7].ToString()
                    );
                }
            }
            catch 
            { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvVente.Rows.Clear();
                string requete = "SELECT vente.num_vente,detail_vente.date_vente, vente.num_medi,medicament.nom_medi, vente.prix_achat, vente.prix_vente, " +
                 " vente.quantite, vente.prixTotal,detail_vente.heure FROM ((vente INNER JOIN detail_vente " +
                 " ON vente.num_vente = detail_vente.num_vente) INNER JOIN medicament ON vente.num_medi = medicament.num_medi)" +
                 " WHERE medicament.nom_medi LIKE  '%" + textBox1.Text + "%' ORDER BY medicament.nom_medi, detail_vente.date_vente DESC, detail_vente.heure DESC";
                DataTable dt = AppCode.ConnectionClass.TableRows(requete);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    var date = dt.Rows[i].ItemArray[1].ToString();
                    var heure = "00:00::00";

                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[8].ToString()))
                    {
                        heure = dt.Rows[i].ItemArray[8].ToString();
                    }
                    var dateVente = date.Substring(0, 10) + " " + heure;
                    dgvVente.Rows.Add(
                        dt.Rows[i].ItemArray[0].ToString(),
                        dateVente,
                        dt.Rows[i].ItemArray[2].ToString().ToUpper(),
                        dt.Rows[i].ItemArray[3].ToString(),
                        dt.Rows[i].ItemArray[4].ToString(),
                        dt.Rows[i].ItemArray[5].ToString(),
                        dt.Rows[i].ItemArray[6].ToString(),
                          dt.Rows[i].ItemArray[7].ToString()
                    );
                }
            }
            catch
            { }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dgvVente.Rows.Clear();

                DataTable dt = AppCode.ConnectionClass.ListeDesVentesDetaillees(dateTimePicker1.Value.Date);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    var date = dt.Rows[i].ItemArray[1].ToString();
                    var heure = "00:00::00";

                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[8].ToString()))
                    {
                        heure = dt.Rows[i].ItemArray[8].ToString();
                    }
                    var dateVente = date.Substring(0, 10) + " " + heure;
                    dgvVente.Rows.Add(
                        dt.Rows[i].ItemArray[0].ToString(),
                        dateVente,
                        dt.Rows[i].ItemArray[2].ToString().ToUpper(),
                        dt.Rows[i].ItemArray[3].ToString(),
                        dt.Rows[i].ItemArray[4].ToString(),
                        dt.Rows[i].ItemArray[5].ToString(),
                        dt.Rows[i].ItemArray[6].ToString(),
                          dt.Rows[i].ItemArray[7].ToString()
                    );
                }
            }
            catch
            { }
        }

    }
}
