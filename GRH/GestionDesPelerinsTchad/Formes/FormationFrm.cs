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
    public partial class FormationFrm : Form
    {
        public FormationFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 1);
            Rectangle area1 = new Rectangle(0, 0, this.panel2.Width - 1, this.panel2.Height - 1);
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
       
       private void DemandeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
              SystemColors.Control, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DemandeFrm_Load(object sender, EventArgs e)
        {
            try
            {
                etat = "1";
                panel2.Visible = false;
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
                etat = "1";
                DureeDeFormation();
                _typeFormation = "";
                ListeDesFormations();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste  dep", ex);
            }

        }

        //liste des personnels
        private void ListeDesFormations()
        {
            try
            {
                var liste = from f in ConnectionClass.ListeFormation()
                            where f.TypeFormation.StartsWith(_typeFormation)
                            select f;
                dataGridView1.Rows.Clear();
                foreach (var  f in liste)
                {
                    dataGridView1.Rows.Add(
                        f.NumeroFormation,
                        f.TypeFormation,
                        f.DateDebutFormation.ToShortDateString(),
                        f.DateFinFormation.ToShortDateString(),
                        f.DureeFormation,
                        f.Description
                    );

                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("t", ex); }
        }

        
        #region

        private int _duree;
        private DateTime _dateFomation;
        private string _typeFormation, _nom, _prenom;
        private int _matricule;
        #endregion

        private int numeroFormation; string etat;

        private void button1_Click(object sender, EventArgs e)
        {
            ListeDesFormations();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        void  DureeDeFormation()
        {
            try
            {
                var dateDebut = Convert.ToDateTime(dUDJour1.Text + "/" + dUDMois1.Text + "/" + dUDAnnee1.Text);
                var dateFin = Convert.ToDateTime(dUDJour2.Text + "/" + dUDMois2.Text + "/" + dUDAnnee2.Text);
                         
                var day = dateFin.DayOfYear - dateDebut.DayOfYear;
                var i = dateFin.Year - dateDebut.Year;
                 day = day + 365*i;
                
                 if (day >= 0)
                 {
                         day++;
                     txtDureeFormation.Text = day.ToString();
                 }
                 else
                 {
                     txtDureeFormation.Text = "0";
                 }
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTypeFormation.Text))
                {
                    if (!string.IsNullOrEmpty(dUDJour1.Text) || !string.IsNullOrEmpty(dUDMois1.Text) || !string.IsNullOrEmpty(dUDAnnee1.Text))
                    {
                        if (!string.IsNullOrEmpty(dUDJour2.Text) || !string.IsNullOrEmpty(dUDMois2.Text) || !string.IsNullOrEmpty(dUDAnnee2.Text))
                        {
                            var formation = new Formation();
                          
                            formation.TypeFormation = txtTypeFormation.Text;
                            formation.Description = txtDescription.Text;
                            formation.DureeFormation = Convert.ToInt32(txtDureeFormation.Text);
                            formation.NumeroFormation = numeroFormation;
                            formation.DateDebutFormation = Convert.ToDateTime(dUDJour1.Text + "/" + dUDMois1.Text + "/" + dUDAnnee1.Text);
                            formation.DateFinFormation = Convert.ToDateTime(dUDJour2.Text + "/" + dUDMois2.Text + "/" + dUDAnnee2.Text);
                            if (ConnectionClass.EnregistrerUneFormation(formation, etat))
                            {
                                etat = "1";
                                txtDescription.Text = "";
                                txtDureeFormation.Text = "";
                                txtTypeFormation.Text = "";
                                dUDAnnee1.Text = DateTime.Now.Year.ToString();
                                dUDAnnee2.Text = DateTime.Now.Year.ToString();
                                dUDJour1.Text = DateTime.Now.Day.ToString();
                                dUDJour2.Text = DateTime.Now.Day.ToString();
                                dUDMois1.Text = DateTime.Now.Month.ToString();
                                dUDMois2.Text = DateTime.Now.Month.ToString();
                                _typeFormation = "";
                                ListeDesFormations();
                                DureeDeFormation();
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

        private void dUDJour2_SelectedItemChanged(object sender, EventArgs e)
        {
            DureeDeFormation();
        }

        private void btnModifierPersonnel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    numeroFormation = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    txtTypeFormation.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    dUDJour1.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString()).Day.ToString();
                    dUDMois1.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString()).Month.ToString();
                    dUDAnnee1.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString()).Year.ToString();
                    dUDJour2.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString()).Day.ToString();
                    dUDMois2.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString()).Month.ToString();
                    dUDAnnee2.Text = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString()).Year.ToString();
                    txtDescription.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                    etat = "2";
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        AppCode.ConnectionClass.SuppressionDeLaFormation(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    }
                }
            }
            catch { }
        }

        private void txtPersonnel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListePerso.indexRecherche = txtPersonnel.Text;
                    if (ListePerso.ShowBox() == "1")
                    {
                        var f = new AppCode.Formation();
                        f.NumeroMatricule = ListePerso.numerMatricule;
                        f.NumeroFormation = numeroFormation;
                        var found = false;
                        if (dataGridView2.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow dg in dataGridView2.Rows)
                            {
                                if (dg.Cells[1].Value.ToString().Equals(f.NumeroMatricule))
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                if (AppCode.ConnectionClass.InsererUneFormation(f))
                                {
                                    txtPersonnel.Text = "";
                                    ListeParticipants();
                                }
                            }

                        }
                        else
                        {   
                            if (AppCode.ConnectionClass.InsererUneFormation(f))
                            {
                                txtPersonnel.Text = "";
                                ListeParticipants();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        void ListeParticipants()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var liste = from l in AppCode.ConnectionClass.ListeFormation(numeroFormation)
                            orderby l.NomPersonnel
                            select l;
                if (liste.Count() > 0)
                {
                    foreach (var l in liste)
                    {
                        var fonction = "";
                        var p = ConnectionClass.ListeDesPersonnelParNumeroMatricule(l.NumeroMatricule);
                        if (p.Rows.Count > 0)
                        {
                            fonction = p.Rows[0].ItemArray[12].ToString();
                        }
                        dataGridView2.Rows.Add(l.NumeroFormation, l.NumeroMatricule,l.NomPersonnel, fonction);
                    }
                }
            }
            catch { }
        }

        private void btnParticipants_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                numeroFormation = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ListeParticipants();
                panel2.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    var f = new Formation();
                    f.NumeroFormation = numeroFormation;
                    f.NumeroMatricule = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                    ConnectionClass.SuppressionDeLaFormation(f);
                    ListeParticipants();
                }
            }
            catch { }
        }



    }
}
