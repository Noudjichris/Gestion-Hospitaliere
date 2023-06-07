using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;
using GestionPharmacetique;

namespace SGSP.Formes
{
    public partial class DirectionFrm : Form
    {
        public DirectionFrm()
        {
            InitializeComponent();
        }

        private void DirectionFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.WhiteSmoke, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            var area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var  linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue
                , LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                  SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        void ListeDivision()
        {
            try
            {
                var dt = ConnectionClass.ListeDepartement();
                dataGridView2.Rows.Clear();
                foreach (DataRow dataRow in dt.Rows)
                {
                    dataGridView2.Rows.Add(dataRow.ItemArray[0].ToString(), 
                        dataRow.ItemArray[1].ToString());
                    Column1.Image = global::SGSP.Properties.Resources.DeleteRed1;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void DirectionFrm_Load(object sender, EventArgs e)
        {
            dataGridView2.RowTemplate.Height = 30;
            if (GestionAcademique.LoginFrm.typeUtilisateur == "")
            {
                txtDivision.Enabled = false;
            }
            ListeDivision();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSupprimerDep_Click(object sender, EventArgs e)
        {
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données", "Confirmation") == "1")
                {
                    if (dataGridView2.Rows.Count > 0)
                    {
                        var num = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                        ConnectionClass.SupprimerUnDepartement(num);
                        ListeDivision();
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner les données à supprimer",
                            "Erreur");
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("modification division", ex);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "")
                    {
                        MonMessageBox.ShowBox("Vous n'etes pas autorisés à faire cette opération", "Erreur");
                   }
                    else
                    {
                        btnSupprimerDep_Click(null, null);
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    txtDivision.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                    etat = 2;
                }
            }
            catch { }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            dataGridView2_CellContentClick(null, null);
        }

        int etat=1;
        private void txtDivision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    if (!string.IsNullOrEmpty(txtDivision.Text))
                    {
                        var numDivision = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                        if (etat == 2)
                        {
                            if (MonMessageBox.ShowBox("Voulez vous modifier ces données", "Confirmation") == "1")
                            {
                                if (ConnectionClass.ModifierDepartement(numDivision, txtDivision.Text))
                                {
                                    ListeDivision();
                                    txtDivision.Text = "";
                                    etat = 1;
                                }
                            }
                        }
                        else
                        {
                            if (ConnectionClass.AjouterDepartement(txtDivision.Text))
                            {
                                ListeDivision();
                                txtDivision.Text = "";
                                etat = 1;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MonMessageBox.ShowBox("modification division", ex);
                }
            }
        }
        
    }
}
