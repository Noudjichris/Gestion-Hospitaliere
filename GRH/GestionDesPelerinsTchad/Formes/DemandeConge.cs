using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SGSP.AppCode;
using GestionPharmacetique;

namespace SGSP.Formes
{
    public partial class DemandeConge : Form
    {
        public DemandeConge()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.CadetBlue, Color.CadetBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var nom = comboBox2.Text.Substring(0, comboBox2.Text.LastIndexOf(" ") );
                var prenom = comboBox2.Text.Substring(comboBox2.Text.LastIndexOf(" ") + 1);
                var dtPersonel = ConnectionClass.ListeDesPersonnelParNomPersonnel(nom, prenom);
                foreach (DataRow dtRow in dtPersonel.Rows)
                {
                    lblMatricule.Text = dtRow.ItemArray[0].ToString();
                    lblNom.Text = dtRow.ItemArray[1].ToString();
                    lblPrenom.Text = dtRow.ItemArray[2].ToString();
                    lblPoste.Text = dtRow.ItemArray[12].ToString();
                    lblDep.Text = dtRow.ItemArray[11].ToString();
                    DateService.Text = dtRow.ItemArray[13].ToString();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste  dep", ex);
            }
        }

        private void DemandeConge_Load(object sender, EventArgs e)
        {
            dUDAnnee1.Text = DateTime.Now.Year.ToString();
            dUDAnnee2.Text = DateTime.Now.Year.ToString();
            dUDJour1.Text = DateTime.Now.Day.ToString();
            dUDJour2.Text = DateTime.Now.Day.ToString();
            dUDMois1.Text = DateTime.Now.Month.ToString();
            dUDMois2.Text = DateTime.Now.Month.ToString();
            try
            {
                var dtPersonel = ConnectionClass.ListeDepartement();
                foreach (DataRow dtRow in dtPersonel.Rows)
                {
                    comboBox1.Items.Add(dtRow.ItemArray[1].ToString());
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste  dep", ex);
            }
        }

        private void DemandeConge_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.White, Color.White, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        //rempir le combobox des personnels appartenat a un departement
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();
                var dtPersonel = ConnectionClass.ListeDesPersonnelsParDepartement(comboBox1.Text);
                foreach (DataRow dtRow in dtPersonel.Rows)
                {
                    comboBox2.Items.Add(dtRow.ItemArray[1].ToString() + " " + dtRow.ItemArray[2].ToString());
                }
            }
            catch (Exception ex )
            {
                MonMessageBox.ShowBox("Liste personnel par dep", ex);
            }
        }
     
    }
}
