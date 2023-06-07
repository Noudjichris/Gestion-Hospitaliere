using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GestionPharmacetique.AppCode;
using GestionPharmacetique;

namespace GestionDesVetements.Formes
{
    public partial class ListePaiementFrm : Form
    {
        public ListePaiementFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
      
        private void ListePaiementFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
               SystemColors.Desktop, SystemColors.Desktop, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, SystemColors.Desktop, SystemColors.Desktop, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListePaiementFrm_Load(object sender, EventArgs e)
        {
            var listePaiement = ConnectionClass.ListeDesPaiements(DateTime.Now.Date, DateTime.Now.Date.AddHours(24), textBox1.Text);
            ListePaiement(listePaiement);
        }

        // liste des paiements
        void ListePaiement(DataTable listePaiement)
        {
            try
            {
                dgvVente.Rows.Clear();
                var totalPaye = 0.0;
                //var listePaiement = ConnectionClass.ListeDesPaies(textBox1.Text);
                foreach (DataRow  paiement in listePaiement.Rows)
                {
                    totalPaye += double.Parse(paiement.ItemArray[2].ToString());
                    dgvVente.Rows.Add(paiement.ItemArray[0].ToString(),
                       DateTime.Parse( paiement.ItemArray[1].ToString()).ToShortDateString(),
                        paiement.ItemArray[2].ToString(),
                        paiement.ItemArray[3].ToString().ToUpper(),
                        paiement.ItemArray[4].ToString()                        
                        );                    
                }
                label3.Text = "Total = " + totalPaye;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste paiement ", ex);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var liste = new DataTable();
            if (textBox1.Text != "")
            {
               liste= ConnectionClass.ListeDesPaies(textBox1.Text);
            }
            else
            {
                liste = ConnectionClass.ListeDesPaiements(DateTime.Now.Date, DateTime.Now.Date.AddHours(24), textBox1.Text);
                           
            }
            ListePaiement(liste);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 2);
            var area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush
                (area1, SystemColors.Desktop, SystemColors.Desktop, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Form1.typeUtilisateur == "admin")
                {
                    if (dgvVente.SelectedRows.Count > 0)
                    {
                        var idPaiement = Convert.ToInt32(dgvVente.SelectedRows[0].Cells[0].Value.ToString());
                        if (MonMessageBox.ShowBox("Voulez vous supprimer les données de ce paiement?", "Confirmation", "confirmation.png") == "1")
                        {
                            ConnectionClass.AnnulerUnPaiement(idPaiement);
                            var listePaiement = ConnectionClass.ListeDesPaiements(DateTime.Now.Date, DateTime.Now.Date.AddHours(24), textBox1.Text);
                            ListePaiement(listePaiement);
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }


        private void dgvVente_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Form1.typeUtilisateur == "admin")
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier les données de ce paiement?", "Confirmation", "confirmation.png") == "1")
                    {
                        var rowIndex = dgvVente.SelectedRows[0].Index;

                        var idPaiement = Convert.ToInt32(dgvVente.Rows[rowIndex].Cells[0].Value.ToString());
                        var montant = Convert.ToDouble(dgvVente.Rows[rowIndex].Cells[2].Value.ToString());
                        ConnectionClass.ModifierUnPaiement(idPaiement, montant);

                        //MonMessageBox.ShowBox("Données de ce paiement modifiées avec succés", "Affirmation", "affirmation.png");
                        var listePaiement = ConnectionClass.ListeDesPaiements(DateTime.Now.Date, DateTime.Now.Date.AddHours(24), textBox1.Text);
                        ListePaiement(listePaiement);
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Données de ce paiement entrées ne sont pas correctes", "Erreur", ex, "erreur.png");
            }
        }
    }
}
