using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class CongeFrm : Form
    {
        public CongeFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox7.Width - 1, this.groupBox7.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void CongeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void CongeFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var natureConge = new string[]
                {
                   "Télétravail","Congé payés", "Récupérations",  "Maladie", "Arrêt de travail simplifié",
                    "Congé pour cas sociaux","Congé epargne temps","Congé maternité","Congé paternité","Formation","Déplacement","Congé sans solde"
                };
                dudNatureConge.Items.AddRange(natureConge);
                etat = "1";
                panel1.Location = new Point(dataGridView1.Width + 15, panel1.Location.Y);
                //groupBox8.Location = new Point(10 , panel1.Height - 2*groupBox8.Height -10);
                Column3.Width = dataGridView1.Width / 3;
            dUDAnnee1.Text = DateTime.Now.Year.ToString();
            dUDAnnee2.Text = DateTime.Now.Year.ToString();
            dUDJour1.Text = DateTime.Now.Day.ToString();
            dUDJour2.Text = DateTime.Now.Day.ToString();
            dUDMois1.Text = DateTime.Now.Month.ToString();
            dUDMois2.Text = DateTime.Now.Month.ToString();
            for (var jour = 1; jour <= 31; jour++)
            {
                dUDJour1.Items.Add(jour);
                dUDJour2.Items.Add(jour);
            }
            for (var mois = 1; mois <= 12; mois++)
            {
                dUDJour1.Items.Add(mois);
                dUDJour2.Items.Add(mois);
            }
            for (var annee = 2017; annee <= 2030; annee++)
            {
                dUDAnnee1.Items.Add(annee);
                dUDAnnee2.Items.Add(annee);
            }
            ListeConge("");
            }
            catch (Exception ex)
            {
               GestionPharmacetique. MonMessageBox.ShowBox("Liste  dep", ex);
            }

        }
        string etat; int  id;
        void ListeConge(string nom)
        {
            try 
            {
                dataGridView1.Rows.Clear();
                var liste = from l in AppCode. ConnectionClass.ListeConge()
                            where l.NomPersonnel.StartsWith(nom, StringComparison.CurrentCultureIgnoreCase)
                            orderby l.IDConge descending
                            select l;

                foreach (var a in liste)
                {
                    var nomPersonnel = "";
                    var fonction = "";
                    var p = AppCode.ConnectionClass.ListeDesPersonnelParNumeroMatricule(a.NumeroMatricule);
                    if (p.Rows.Count > 0)
                    {
                      nomPersonnel=  p.Rows[0].ItemArray[1].ToString() + " " + p.Rows[0].ItemArray[2].ToString();
                      fonction = p.Rows[0].ItemArray[12].ToString();
                    }
                    dataGridView1.Rows.Add
                        (
                        a.IDConge,
                        a.NumeroMatricule,
                        nomPersonnel,fonction,
                        a.DateDemande.ToShortDateString(),
                        a.DateDebutConge.ToShortDateString(),
                        a.DateRetour.ToShortDateString(),
                        a.NatureConge
                        );
                }
             }
            catch (Exception ex) {GestionPharmacetique. MonMessageBox.ShowBox("", ex); }
        }

        private void txtEmploye_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListePerso.indexRecherche = txtEmploye.Text;
                    if (ListePerso.ShowBox() == "1")
                    {
                        txtEmploye.Text = ListePerso.nomPersonnel;
                        txtFonction.Text = ListePerso.fonction;
                        txtMatricule.Text = ListePerso.numerMatricule;
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMatricule.Text))
                {
                    if (!string.IsNullOrEmpty(dUDJour1.Text) || !string.IsNullOrEmpty(dUDMois1.Text) || !string.IsNullOrEmpty(dUDAnnee1.Text))
                    {
                        if (!string.IsNullOrEmpty(dUDJour2.Text) || !string.IsNullOrEmpty(dUDMois2.Text) || !string.IsNullOrEmpty(dUDAnnee2.Text))
                        {
                            var conge = new AppCode.Conge();
                            conge.NatureConge = dudNatureConge.Text;
                            conge.IDConge = id;
                            conge.NumeroMatricule = txtMatricule.Text;
                            conge.DateDebutConge = Convert.ToDateTime(dUDJour1.Text + "/" + dUDMois1.Text + "/" + dUDAnnee1.Text);
                            conge.DateRetour = Convert.ToDateTime(dUDJour2.Text + "/" + dUDMois2.Text + "/" + dUDAnnee2.Text);
                            if (AppCode.ConnectionClass.EnregistrerUnConge(conge, etat))
                            {
                                etat = "1";
                                txtMatricule.Text = "";
                                dudNatureConge.Text = "";
                                txtEmploye.Text = "";
                                txtFonction.Text = "";
                                dUDAnnee1.Text = DateTime.Now.Year.ToString();
                                dUDAnnee2.Text = DateTime.Now.Year.ToString();
                                dUDJour1.Text = DateTime.Now.Day.ToString();
                                dUDJour2.Text = DateTime.Now.Day.ToString();
                                dUDMois1.Text = DateTime.Now.Month.ToString();
                                dUDMois2.Text = DateTime.Now.Month.ToString();
                                ListeConge("");

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              GestionPharmacetique.  MonMessageBox.ShowBox("", ex);
            }
        }

        private void btnModifierPersonnel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    txtEmploye.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    txtFonction.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    txtMatricule.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    dUDJour1.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString()).Day.ToString();
                    dUDMois1.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString()).Month.ToString();
                    dUDAnnee1.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString()).Year.ToString();
                    dUDJour2.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[6].Value.ToString()).Day.ToString();
                    dUDMois2.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[6].Value.ToString()).Month.ToString();
                    dUDAnnee2.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[6].Value.ToString()).Year.ToString();
                    dudNatureConge.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

                    etat = "2";
                }
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListeConge(textBox3.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        AppCode.ConnectionClass.SupprimerUnConge(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    }
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                dataGridView1.Rows.Clear();
                var liste = from l in AppCode.ConnectionClass.ListeConge()
                            where l.DateDebutConge >=dtp1.Value.Date
                            where l.DateDebutConge <dtp2.Value.Date.AddHours(24)
                            orderby l.IDConge descending
                            select l;

                foreach (var a in liste)
                {
                    var nomPersonnel = "";
                    var fonction = "";
                    var p = AppCode.ConnectionClass.ListeDesPersonnelParNumeroMatricule(a.NumeroMatricule);
                    if (p.Rows.Count > 0)
                    {
                        nomPersonnel = p.Rows[0].ItemArray[1].ToString() + " " + p.Rows[0].ItemArray[2].ToString();
                        fonction = p.Rows[0].ItemArray[12].ToString();
                    }
                    dataGridView1.Rows.Add
                        (
                        a.IDConge,
                        a.NumeroMatricule,
                        nomPersonnel, fonction,
                        a.DateDemande.ToShortDateString(),
                        a.DateDebutConge.ToShortDateString(),
                        a.DateRetour.ToShortDateString(),
                        a.NatureConge
                        );
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
    }
}
