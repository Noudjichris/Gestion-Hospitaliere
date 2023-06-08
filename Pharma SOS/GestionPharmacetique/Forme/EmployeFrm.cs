using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace GestionPharmacetique.Forme
{
    public partial class EmployeFrm : Form
    {
        public EmployeFrm()
        {
            InitializeComponent();
        }

        private void EmployeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ScrollBar, 3);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                , SystemColors.ScrollBar, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 1);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                , SystemColors.ScrollBar, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void btnRetirer_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de l'employé " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "?", "Confirmation", "confirmation.png")
                            == "1")
                    ConnectionClass.SupprimerEmployee(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ViderLesChamps();
                 ListeDesEmployes();
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez selectionner la ligne à supprimer", "erreur", "erreur.png");
            }
        }

        private void EmployeFrm_Load(object sender, EventArgs e)
        {
            ListeDesEmployes();
            button3.Location = new Point(Width - 45, 3);
        }
        //liste des employe
        private void ListeDesEmployes()
        {
            try
            {
                dataGridView1.Rows.Clear();
                List<Employe> listeEmployes = ConnectionClass.ListeDesEmployees();
                foreach (var employe in listeEmployes)
                {
                    string numUtilisateur = "", motPasse = "", typeUtilisateur = "", utilistateur ="";
                    
                    var listeUtilisateur = from l in ConnectionClass.ListesDesUtilisateurs()
                                           where l.NomEmploye.ToUpper() == employe.NomEmployee.ToUpper()
                                           select l;
                    if (listeUtilisateur.Count() > 0)
                    {
                        foreach (var l in listeUtilisateur)
                        {
                            numUtilisateur = l.NumeroUtilisateur.ToString();
                            motPasse = l.MotPasse;
                            typeUtilisateur = l.TypeUtilisateur;
                            utilistateur = l.NomUtilisateur;
                        }
                    }
                   
                        dataGridView1.Rows.Add
                        (
                          employe.NumMatricule,
                            employe.NomEmployee,
                            employe.Addresse,
                            employe.Telephone1,
                            employe.Telephone2,
                            employe.Email,
                            employe.Ville,
                            employe.Titre,
                            numUtilisateur,utilistateur,motPasse,typeUtilisateur
                        );
                    
                }
            }
            catch (Exception exce )
            {
                MonMessageBox.ShowBox( "Liste employe", exce )
                ;
            }
        }

        private void btnFermere_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //ajouter un employe
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumeroEmploe.Text) && !string.IsNullOrEmpty(txtNomEmploye.Text) && 
                    !string.IsNullOrEmpty(txtAdresse.Text) && !string.IsNullOrEmpty(txtTele1.Text) &&
                   ! string.IsNullOrEmpty(txtVille.Text))
                {

                    if (txtNomEmploye.Text.Contains(" "))
                    {
                        if (string.IsNullOrEmpty(txtEmail.Text) || (!string.IsNullOrEmpty(txtEmail.Text) &&
                                                                    txtEmail.Text.Contains("@") &&
                                                                    txtEmail.Text.Contains(".")))
                        {
                            string typeUtilisateur;
                            if (radioButton1.Checked )
                            {
                                typeUtilisateur = "caissier";
                            }
                            else if (radioButton2.Checked)
                            {
                                typeUtilisateur = "admin";
                            }
                            else
                            {
                                MonMessageBox.ShowBox("Veuillez selectionner le type d'utilisateur", "Confirmation", "confirmation.pmg");
                                return;
                            }
                                var numeroEmploye = txtNumeroEmploe.Text;
                                var nomEmploye = txtNomEmploye.Text;
                                var adresse = txtAdresse.Text;
                                var tele1 = txtTele1.Text;
                                var tele2 = txtTele2.Text;
                                var email = txtEmail.Text;
                                var ville = txtVille.Text;
                                var titre = txtTitre.Text;
                                var employe = new Employe(numeroEmploye, nomEmploye, adresse, tele1, tele2, email, titre, ville);

                                ConnectionClass.EnregistrerUnEmploye(employe, typeUtilisateur);
                                ViderLesChamps();
                                ListeDesEmployes();
                            
                        }
                        else
                        {
                            MonMessageBox.ShowBox(
                                "Si l'email de l'employe existe, veuillez pourvoir une adresse email valide", "erreur",
                                "erreur.png");
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer le nom et prenom de l'employe", "erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Les champs numero employe, nom, adresse, telephone1, ville sont à remplir","erreur","erreur.png");
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Ajouter employe", exception);
            }

        }
        private void ViderLesChamps()
        {
            txtAdresse.Text = "";
            txtEmail.Text = "";
            txtNomEmploye.Text = "";
            txtNumeroEmploe.Text = "";
            txtTele1.Text = "";
            txtTele2.Text = "";
            txtTitre.Text = "";
            txtVille.Text = "";
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if(dataGridView1.SelectedRows.Count>0)
                {
                OpenFileDialog open = new OpenFileDialog();
                //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                open.Filter = "Image Files (*.jpg)|*.jpg|all files(*.*)|*.*";
                open.FilterIndex = 1;
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (open.CheckFileExists)
                    {
                        var paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                        var nomFichier = System.IO.Path.GetFileName(open.FileName);
                        var image = paths + "\\Images\\Employe\\" + nomFichier;
                        if (!System.IO.File.Exists(image))
                        {
                            System.IO.File.Copy(open.FileName, image);
                            ConnectionClass.ModifierEmployee(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), nomFichier);
                            MonMessageBox.ShowBox("Photo transferée avec succés ", "Information Photo",
                                "affirmation.png");
                        }
                        else
                        {
                            MonMessageBox.ShowBox(
                                "Une image existe deja sous ce nom. Rénommer le fichier et réessayez.",
                                "Erreur insertion", "erreur.png");
                        }
                    }
                }
            }
            }

            catch (Exception ex) { MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }

        private void btnInitilialiser_Click(object sender, EventArgs e)
        {
            ViderLesChamps();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                txtNumeroEmploe.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtNomEmploye.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtAdresse.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtTele1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtTele2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtVille.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                txtTitre.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                txtEmail.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txtUtilisateur.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
                lblMotDePasse.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
                if (dataGridView1.SelectedRows[0].Cells[11].Value.ToString().ToUpper() == "CAISSIER")
                {
                    radioButton1.Checked = true ;
                }
                else if (dataGridView1.SelectedRows[0].Cells[11].Value.ToString().ToUpper() == "ADMIN")
                { 
                    radioButton2.Checked = true;
                }
                else 
                {
                    radioButton2.Checked = false;
                    radioButton1.Checked = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtNumeroEmploe_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<Employe> listeEmployes = ConnectionClass.ListeDesEmployees("num_empl","'"+txtNumeroEmploe.Text+"'");
                if (listeEmployes.Count() > 0)
                {
                    foreach (var employe in listeEmployes)
                    {
                        string numUtilisateur = "", motPasse = "", typeUtilisateur = "", utilistateur = "";

                        var listeUtilisateur = from l in ConnectionClass.ListesDesUtilisateurs()
                                               where l.NomEmploye.ToUpper() == employe.NomEmployee.ToUpper()
                                               select l;
                        if (listeUtilisateur.Count() > 0)
                        {
                            foreach (var l in listeUtilisateur)
                            {
                                numUtilisateur = l.NumeroUtilisateur.ToString();
                                motPasse = l.MotPasse;
                                typeUtilisateur = l.TypeUtilisateur;
                                utilistateur = l.NomUtilisateur;
                            }
                        }

                        txtNomEmploye.Text =employe.NomEmployee;
                        txtAdresse.Text = employe.Addresse;
                        txtTele1.Text = employe.Telephone1;
                        txtTele2.Text = employe.Telephone2;
                        txtVille.Text = employe.Ville;
                        txtTitre.Text = employe.Titre;
                        txtEmail.Text = employe.Email;
                        txtUtilisateur.Text = utilistateur ;
                        lblMotDePasse.Text = motPasse; 
                        if (typeUtilisateur  == "CAISSIER")
                        {
                            radioButton1.Checked = true;
                        }
                        else if (typeUtilisateur == "ADMIN")
                        {
                            radioButton2.Checked = true;
                        }
                        else
                        {
                            radioButton2.Checked = false;
                            radioButton1.Checked = false;
                        }

                    }
                }
                else
                {
                    txtNomEmploye.Text = "";
                    txtAdresse.Text = "";
                    txtTele1.Text = "";
                    txtTele2.Text = "";
                    txtVille.Text = "";
                    txtTitre.Text = "";
                    txtEmail.Text = "";
                    txtUtilisateur.Text = "";
                    lblMotDePasse.Text = "";
                    radioButton2.Checked = false;
                    
                }
            }
            catch (Exception exce )
            {
                MonMessageBox.ShowBox( "Liste employe", exce )
                ;
            }
        }

    }
}
