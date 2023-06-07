using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GestionAcademique;
using GestionDuneClinique.AppCode;
using GestionDuneClinique;
using GestionPharmacetique.AppCode;

namespace GestionDesPelerinsTchad.Formes
{
    public partial class AdminFrm : Form
    {
        public AdminFrm()
        {
            InitializeComponent();
        }

        private void AdminFrm_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.Silver, 0);
            var area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.SteelBlue, 0);
            var area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void AdminFrm_Load(object sender, EventArgs e)
        {
           Size = new Size(915, 430);
            Controls.Remove(groupBox2);
            ListeDesUtilisateurs();

            var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
            foreach (var empl in listeEmploye)
            {
                comboBox1.Items.Add(empl.NomEmployee);
            }
        }

        void ListeDesUtilisateurs()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var listeUtil = ConnectionClassClinique.ListesDesUtilisateurs();
                foreach (Utilisateur util in listeUtil)
                {
                    dataGridView1.Rows.Add(
                        util.NumeroUtilisateur,
                        util.NomEmploye,
                        util.NomUtilisateur,
                        util.MotPasse,
                        util.TypeUtilisateur,
                        util.NumEmploye
                        );
                }
            }
            catch (Exception)
            {

            }
        }

        //supprimer un utilisateur
        private void button1_Click(object sender, EventArgs e)
        {
            if (LoginFrm.ShowBox() == "1")
            {if (LoginFrm.typeUtilisateur == "admin")
                {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (
                        MonMessageBox.ShowBox(
                            "Voulez vous vous supprimer les données de l'utilisateur " +
                            dataGridView1.SelectedRows[0].Cells[2].Value.ToString() + "?", "Confirmation", "confirmation.png") == "1")
                    {
                        ConnectionClassClinique.SupprimerUnUtilisateur(
                            Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        ListeDesUtilisateurs();

                    }
                }
                 }
                else
                {
                    MonMessageBox.ShowBox("Vous etes pas autorisés à faire cette opération", "Avertissement","erreur.png");
                }
            }
        }

        private void btnAjouterUneAgence_Click(object sender, EventArgs e)
        {
           
                if (btnAjouterUneAgence.Text == "AJOUTER UTILISATEUR")
                {
                    label2.Visible = true;
                    comboBox1.Visible = true;
                    btnAjouterUneAgence.Text = "ENREGISTRER";
                }
                else if (btnAjouterUneAgence.Text == "ENREGISTRER")
                {
                    if (LoginFrm.ShowBox() == "1")
                    {
                        if (LoginFrm.typeUtilisateur == "admin")
                        {
                            var nom = comboBox1.Text;
                            var listEmpl = ConnectionClassClinique.ListeDesEmployees("nom_empl", nom);
                            var numMatricule = listEmpl[0].NumMatricule;
                            var utilisateur = new Utilisateur(nom, numMatricule.ToString().GetHashCode().ToString(),
                                "util",
                                numMatricule);
                            ConnectionClassClinique.AjouterUnUtilisateur(utilisateur);
                            {
                                ListeDesUtilisateurs();
                                label2.Visible = false;
                                comboBox1.Visible = false;
                                btnAjouterUneAgence.Text = "AJOUTER UTILISATEUR";
                            }
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Vous etes pas autorisés à faire cette opération", "Avertissement","erreur.png");
                    }
                }
                    
                  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close()
            ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LoginFrm.ShowBox() == "1")
            {
                if (LoginFrm.typeUtilisateur == "admin")
                {
                var frm = new TrackFrm();
                frm.ShowDialog();
                 }
                else
                {
                    MonMessageBox.ShowBox("Vous etes pas autorisés à faire cette opération", "Avertissement","erreur.png");
                }
            }
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.SteelBlue, 0);
            var area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (LoginFrm.ShowBox() == "1")
            {
                if (LoginFrm.typeUtilisateur == "admin")
                {
                    Size = new Size(915, 450);
                    Controls.Add( groupBox2);
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            var id=            Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            var nomUtil = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            var motPasse = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            var type = comboBox3.Text;
            var utilisateur = new Utilisateur(id,nomUtil,motPasse,type,"");
            if (ConnectionClassClinique.ModifierUnUtilisateur(utilisateur))
            {
                Size = new Size(915, 430);
                Controls.Remove(groupBox2);
                ListeDesUtilisateurs();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            if (MonMessageBox.ShowBox("Voulez vous reinitialiser le mot de passe de " + dataGridView1.SelectedRows[0].Cells[2].Value.ToString() + "?", "Confirmation", "confirmation.png") == "1")
            {
                ConnectionClassClinique.ModifierMotDePasse(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(),
                    dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), dataGridView1.SelectedRows[0].Cells[5].Value.ToString().GetHashCode().ToString());
            }
        }

    }
}
