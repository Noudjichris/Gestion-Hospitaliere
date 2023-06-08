using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using GestionDuneClinique;
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
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, 
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
               Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void btnRetirer_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de l'employé " + listView1.SelectedItems[0].SubItems[1].Text  + "?", "Confirmation", "confirmation.png")
                            == "1")
                ConnectionClassClinique.SupprimerEmployee(listView1.SelectedItems[0].SubItems[0].Text );
                ConnectionClassPharmacie.SupprimerEmployee(listView1.SelectedItems[0].SubItems[0].Text);
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
            listView1.Columns.Clear();
            listView1.Columns.Add("NUM", listView1.Width / 18);
            listView1.Columns.Add("NOM EMPLOYE", listView1.Width / 4);
            listView1.Columns.Add("ADRESSE", listView1.Width / 4);
            listView1.Columns.Add("TELEPHONE 1", listView1.Width / 11);
            listView1.Columns.Add("TELEPHONE 2", listView1.Width / 11);
            listView1.Columns.Add("EMAIL", listView1.Width / 9);
            listView1.Columns.Add("TITRE", listView1.Width / 6 - 15);
            ListeDesEmployes();
        }
        //liste des employe
        private void ListeDesEmployes()
        {
            try
            {
                listView1.Items.Clear();
                List<Employe> listeEmployes = ConnectionClassClinique.ListeDesEmployees();
                var list = from l in listeEmployes
                           where !l.NomEmployee.Contains("EXTERNE")
                           select l;
                foreach (var employe in list)
                {
                    var items = new string[]
                    {
                      employe.NumMatricule,
                        employe.NomEmployee,
                        employe.Addresse,
                        employe.Telephone1,
                        employe.Telephone2,
                        employe.Email,
                        employe.Titre
                    };

                    ListViewItem lstListViewItem = new ListViewItem(items);
                    listView1.Items.Add(lstListViewItem);
                }
               // foreach (ListViewItem item in listView1.Items)
                 //   item.BackColor = item.Index %2 == 0 ? Color.White : Color.DarkOrange;
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
                    !string.IsNullOrEmpty(txtAdresse.Text) && !string.IsNullOrEmpty(txtTele1.Text))
                {

                    if (txtNomEmploye.Text.Contains(" "))
                    {
                        if (string.IsNullOrEmpty(txtEmail.Text) || (!string.IsNullOrEmpty(txtEmail.Text) &&
                                                                    txtEmail.Text.Contains("@") &&
                                                                    txtEmail.Text.Contains(".")))
                        {
                            var numeroEmploye = txtNumeroEmploe.Text;
                            var nomEmploye = txtNomEmploye.Text;
                            var adresse = txtAdresse.Text;
                            var tele1 = txtTele1.Text;
                            var tele2 = txtTele2.Text;
                            var email = txtEmail.Text;
                            var titre = txtTitre.Text;
                            var photo = "";
                            var employe = new Employe(numeroEmploye,nomEmploye,adresse,tele1,tele2,email,titre,photo);
                            
                            ConnectionClassClinique.AjouterEmployee(employe);
                            ConnectionClassPharmacie.AjouterEmployee(employe);
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
                    MonMessageBox.ShowBox("Les champs numero employe, nom, adresse, telephone1 sont à remplir","erreur","erreur.png");
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
            
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumeroEmploe.Text) && !string.IsNullOrEmpty(txtNomEmploye.Text) && !
                    string.IsNullOrEmpty(txtAdresse.Text) && !string.IsNullOrEmpty(txtTele1.Text) )
                {

                    if (txtNomEmploye.Text.Contains(" "))
                    {
                        if (string.IsNullOrEmpty(txtEmail.Text) || (!string.IsNullOrEmpty(txtEmail.Text) &&
                                                                    txtEmail.Text.Contains("@") &&
                                                                    txtEmail.Text.Contains(".")))
                        {
                            var numeroEmploye = txtNumeroEmploe.Text;
                            var nomEmploye = txtNomEmploye.Text;
                            var adresse = txtAdresse.Text;
                            var tele1 = txtTele1.Text;
                            var tele2 = txtTele2.Text;
                            var email = txtEmail.Text;
                            var titre = txtTitre.Text;
                            var photo = "";
                            var employe = new Employe(numeroEmploye, nomEmploye, adresse, tele1, tele2, email, titre,photo);
                            if (MonMessageBox.ShowBox("Voulez vous modifier les données de l'employé " + nomEmploye + "?", "Confirmation", "confirmation.png")
                             == "1")
                            ConnectionClassClinique.ModifierEmployee(employe);
                            ConnectionClassPharmacie.ModifierEmployee(employe);
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
                    MonMessageBox.ShowBox("Les champs numero employe, nom, adresse, telephone1, ville sont à remplir", "erreur", "erreur.png");
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Ajouter employe", exception);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtNumeroEmploe.Text = listView1.SelectedItems[0].SubItems[0].Text;
                txtNomEmploye.Text = listView1.SelectedItems[0].SubItems[1].Text;
                txtAdresse.Text = listView1.SelectedItems[0].SubItems[2].Text;
                txtTele1.Text = listView1.SelectedItems[0].SubItems[3].Text;
                txtTele2.Text = listView1.SelectedItems[0].SubItems[4].Text;
                txtTitre.Text = listView1.SelectedItems[0].SubItems[6].Text;
                txtEmail.Text = listView1.SelectedItems[0].SubItems[5].Text;
            }
            catch (Exception)
            {
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if(listView1.CheckedItems.Count>0)
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
                            ConnectionClassClinique.ModifierEmployee(listView1.CheckedItems[0].SubItems[0].Text, nomFichier);
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

        


    }
}
