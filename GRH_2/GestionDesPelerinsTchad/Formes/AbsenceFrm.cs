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
    public partial class AbsenceFrm : Form
    {
        public AbsenceFrm()
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
        private void AbsenceFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void AbsenceFrm_Load(object sender, EventArgs e)
        {
            try
            {
                etat = "1";
                panel1.Location = new Point(dataGridView1.Width + 15, panel1.Location.Y);
                //groupBox8.Location = new Point(10 , panel1.Height - 2*groupBox8.Height -10);
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
                ListeAbsence("");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste  dep", ex);
            }

        }

        void ListeAbsence(string nom)
        {
            try 
            {
                dataGridView1.Rows.Clear();
                var liste = from l in ConnectionClass.ListeUneAbsence()
                            where l.NomPersonnel.StartsWith(nom, StringComparison.CurrentCultureIgnoreCase)
                            orderby l.IDAbsence descending
                            select l;

                foreach (var a in liste)
                {
                    var nomPersonnel = "";
                    var fonction = "";
                    var p = ConnectionClass.ListeDesPersonnelParNumeroMatricule(a.NumeroMatricule);
                    if (p.Rows.Count > 0)
                    {
                      nomPersonnel=  p.Rows[0].ItemArray[1].ToString() + " " + p.Rows[0].ItemArray[2].ToString();
                      fonction = p.Rows[0].ItemArray[12].ToString();
                    }
                    dataGridView1.Rows.Add
                        (
                        a.IDAbsence,
                        a.NumeroMatricule,
                        nomPersonnel,fonction,
                        a.DateDemande.ToShortDateString(),
                        a.DateDebutAbscense.ToShortDateString(),
                        a.DateRetour.ToShortDateString(),
                        a.Motif
                        );
                }
             }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

       
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
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
        string etat; int id;
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
                            var absence = new Absence();
                            absence.Motif = txtMotif.Text;
                            absence.IDAbsence = id;
                            absence.NumeroMatricule = txtMatricule.Text;
                            absence.DateDemande = DateTime.Now;
                            absence.DateDebutAbscense = Convert.ToDateTime(dUDJour1.Text + "/" + dUDMois1.Text + "/" + dUDAnnee1.Text);
                            absence.DateRetour = Convert.ToDateTime(dUDJour2.Text + "/" + dUDMois2.Text + "/" + dUDAnnee2.Text);
                            if (ConnectionClass.EnregistrerUneAbsence(absence, etat))
                            {
                                etat = "1";
                                txtMatricule.Text = "";
                                txtMotif.Text = "";
                                txtEmploye.Text = "";
                                txtFonction.Text = "";
                                dUDAnnee1.Text = DateTime.Now.Year.ToString();
                                dUDAnnee2.Text = DateTime.Now.Year.ToString();
                                dUDJour1.Text = DateTime.Now.Day.ToString();
                                dUDJour2.Text = DateTime.Now.Day.ToString();
                                dUDMois1.Text = DateTime.Now.Month.ToString();
                                dUDMois2.Text = DateTime.Now.Month.ToString();
                                ListeAbsence("");

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
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
                    txtMotif.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

                    etat = "2";
                }
            }
            catch { }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from l in ConnectionClass.ListeUneAbsence()
                            where l.DateDebutAbscense >=dtp1.Value.Date
                            where l.DateDebutAbscense<dtp2.Value.Date.AddHours(24)
                            orderby l.IDAbsence descending
                            select l;

                foreach (var a in liste)
                {
                    var nomPersonnel = "";
                    var fonction = "";
                    var p = ConnectionClass.ListeDesPersonnelParNumeroMatricule(a.NumeroMatricule);
                    if (p.Rows.Count > 0)
                    {
                        nomPersonnel = p.Rows[0].ItemArray[1].ToString() + " " + p.Rows[0].ItemArray[2].ToString();
                        fonction = p.Rows[0].ItemArray[12].ToString();
                    }
                    dataGridView1.Rows.Add
                        (
                        a.IDAbsence,
                        a.NumeroMatricule,
                        nomPersonnel, fonction,
                        a.DateDemande.ToShortDateString(),
                        a.DateDebutAbscense.ToShortDateString(),
                        a.DateRetour.ToShortDateString(),
                        a.Motif
                        );
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListeAbsence(textBox3.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        AppCode.ConnectionClass.SupprimerUneAbsence(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    }
                }
            }
            catch { }
        }


    }
}
