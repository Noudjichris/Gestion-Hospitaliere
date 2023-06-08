using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace GestionPharmacetique.Forme
{
    public partial class EnCaissementFrm : Form
    {
        public EnCaissementFrm()
        {
            InitializeComponent();
        }

        private void EnCaissementFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.White, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue, Color.SlateBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue, Color.SlateBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CalculerEncaissement()
        {
            try
            {
                decimal encaissement =0;
                decimal montant25, montant50, montant100, montant500, montant1000, montant2000, montant5000, montant10000;
             
               if (decimal.TryParse(textBox25.Text, out montant25))
               {
                   encaissement += montant25 * 25;
               }
               else
               {
                   montant25 = 0;
               }
               if (decimal.TryParse(textBox50.Text, out montant50))
               {
                   encaissement += montant50 * 50;
               }
               else
               {
                   montant50 = 0;
               } 
                if (decimal.TryParse(textBox100.Text, out montant100))
               {
                   encaissement += montant100 * 100;
               }
               else
               {
                   montant100 = 0;
               }
                if (decimal.TryParse(textBox500.Text, out montant500))
                {
                    encaissement += montant500 * 500;
                }
                else
                {
                    montant500 = 0;
                }
                if (decimal.TryParse(textBox1000.Text, out montant1000))
                {
                    encaissement += montant1000 * 1000;
                }
                else
                {
                    montant1000 = 0;
                }
                if (decimal.TryParse(textBox2000.Text, out montant2000))
                {
                    encaissement += montant2000 * 2000;
                }
                else
                {
                    montant2000 = 0;
                }
                if (decimal.TryParse(textBox5000.Text, out montant5000))
                {
                    encaissement += montant5000 * 5000;
                }
                else
                {
                    montant5000 = 0;
                }
                if (decimal.TryParse(textBox10000.Text, out montant10000))
                {
                    encaissement += montant10000 * 10000;
                }
                else
                {
                    montant10000 = 0;
                }
                txtMontant.Text = encaissement.ToString();
            }
            catch { }
        }
        private void textBox100_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox2000_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox1000_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox50_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox500_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox5000_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void textBox10000_TextChanged(object sender, EventArgs e)
        {
            CalculerEncaissement();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                decimal encaissement;
                if (!string.IsNullOrEmpty(cmbNomEmploye.Text))
                {
                    if (decimal.TryParse(txtMontant.Text, out  encaissement))
                    {
                        decimal  montant25, montant50, montant100, montant500, montant1000, montant2000, montant5000, montant10000;
                        
                        if (decimal.TryParse(textBox25.Text, out montant25))
                        {
                        }
                        else
                        {
                            montant25 = 0;
                        }
                        if (decimal.TryParse(textBox50.Text, out montant50))
                        {
                        }
                        else
                        {
                            montant50 = 0;
                        }
                        if (decimal.TryParse(textBox100.Text, out montant100))
                        {
                        }
                        else
                        {
                            montant100 = 0;
                        }
                        if (decimal.TryParse(textBox500.Text, out montant500))
                        {
                        }
                        else
                        {
                            montant500 = 0;
                        }
                        if (decimal.TryParse(textBox1000.Text, out montant1000))
                        {
                        }
                        else
                        {
                            montant1000 = 0;
                        }
                        if (decimal.TryParse(textBox2000.Text, out montant2000))
                        {
                        }
                        else
                        {
                            montant2000 = 0;
                        }
                        if (decimal.TryParse(textBox5000.Text, out montant5000))
                        {
                        }
                        else
                        {
                            montant5000 = 0;
                        }
                        if (decimal.TryParse(textBox10000.Text, out montant10000))
                        {
                        }
                        else
                        {
                            montant10000 = 0;
                        }
                        List<AppCode.Employe> listeEmploye = AppCode.ConnectionClass.ListeDesEmployees("nom_empl", "'" + cmbNomEmploye.Text + "'");
                        string numEmploye = listeEmploye[0].NumMatricule;
                        if (AppCode.ConnectionClass.EnregistrerEncaissement(encaissement, numEmploye, montant10000, montant5000, montant2000, montant1000, montant500, montant100, montant50, montant25,0,0,0,0))
                        {
                            button1_Click();
                            txtMontant.BackColor = Color.White;                            
                            textBox100.Text = "";
                            textBox1000.Text = "";
                            textBox10000.Text = "";
                            textBox2000.Text = "";
                            textBox500.Text = "";
                            textBox5000.Text = "";
                            textBox25.Text = "";
                            textBox50.Text = "";
                            txtMontant.Text = "";
                        }
                    }
                    else
                    {
                        txtMontant.BackColor = Color.Red;
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le nom du caissier(e) sur la liste deroulante", "Erreur", "erreur.png");
                }
            }
            catch { }
        }

        private void EnCaissementFrm_Load(object sender, EventArgs e)
        {
            try
            {
                cmbNomEmploye.Items.Clear();
                string requete = "SELECT nom_empl FROM  employe";
                DataTable dt = AppCode.ConnectionClass.TableRows(requete);
                foreach (DataRow dtRow in dt.Rows)
                {
                    cmbNomEmploye.Items.Add(dtRow.ItemArray[0].ToString().ToUpper());
                }
                button1_Click();
            }
            catch (Exception)
            {
            } 
        }

        private void button1_Click()
        {try
        {
            var requete = "SELECT encai_tbl.encai_id, encai_tbl.`date`, employe.nom_empl, encai_tbl.montant FROM employe INNER JOIN" +
                        " encai_tbl ON employe.num_empl = encai_tbl.num_empl ";//WHERE (employe.nom_empl = @Param1)
                var dt = AppCode.ConnectionClass.TableRows(requete);
                
                  dataGridView1.Rows.Clear();
                foreach (DataRow dtRow in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        dtRow.ItemArray[0].ToString(),
                        dtRow.ItemArray[1].ToString().ToUpper(),
                        dtRow.ItemArray[2].ToString(),
                        dtRow.ItemArray[3].ToString()
                    );
                }
        }
        catch { }
        }

        private void cmbNomEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var requete = "SELECT encai_tbl.encai_id, encai_tbl.`date`, employe.nom_empl, encai_tbl.montant FROM employe INNER JOIN" +
                            " encai_tbl ON employe.num_empl = encai_tbl.num_empl WHERE (employe.nom_empl = '" + cmbNomEmploye.Text + "') ";
                var dt = AppCode.ConnectionClass.TableRows(requete);
                dataGridView1.Rows.Clear();
                foreach (DataRow dtRow in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        dtRow.ItemArray[0].ToString(),
                        dtRow.ItemArray[1].ToString().ToUpper(),
                        dtRow.ItemArray[2].ToString(),
                        dtRow.ItemArray[3].ToString()
                    );

                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                AppCode.ConnectionClass.SupprimerEncaisement
                    (Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                button1_Click();
            }
        }
    }
}
