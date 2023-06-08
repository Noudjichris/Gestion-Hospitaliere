using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.FormesClinique
{
    public partial class EntreEmplFrm : Form
    {
        public EntreEmplFrm()
        {
            InitializeComponent();
        }

        private void EntreEmplFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
               Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void EntreEmplFrm_Load(object sender, EventArgs e)
        {
            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
            foreach (Entreprise entrep in listeEntrep)
            {
                cmbEntrep.Items.Add(entrep.NomEntreprise);
            }
            Location = new Point(205, 120);
            Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);

        }

        void ListeEmploye(List<EmployeEntreprise> listeEmpl)
        {
            try
            {
                listView2.Columns.Clear();
                listView2.Columns.Add("Id", 0);
                listView2.Columns.Add("Nom", listView2.Width / 3);
                listView2.Columns.Add("Prenom", listView2.Width / 3);
                listView2.Columns.Add("Sexe", listView2.Width / 11);
                listView2.Columns.Add("Age", listView2.Width / 11);
                listView2.Columns.Add("Telephone", listView2.Width / 7);
                listView2.Columns.Add("Entreprise", 0);

                listView2.Items.Clear();
                var nom = "";
                var prenom = "";
                var entrep = "";
                foreach (EmployeEntreprise empl in listeEmpl)
                {
                    var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(empl.IdEntreprise);
                    entrep = listeEntrep[0].NomEntreprise;
                    if (empl.Nom.Contains(" "))
                    {
                        nom = empl.Nom.Substring(0, empl.Nom.LastIndexOf(" "));
                        prenom = empl.Nom.Substring(empl.Nom.LastIndexOf(" ") + 1);
                    }
                    else
                    {
                        nom = listeEntrep[0].NomEntreprise;
                    }
                    var items = new string[]
                    {
                        empl.Numero.ToString(),
                        nom,prenom,
                        empl.Sexe,
                        empl.Age.ToString(),
                        empl.Telephone,
                        entrep
                    };

                    var lstItems = new ListViewItem(items);
                    listView2.Items.Add(lstItems);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste employe", ex);
            }
        }
        string nom, sexe, tele;
        int id, idEntrep, age;
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNom.Text != "" && txtPrenom.Text!="")
            {
                if (Int32.TryParse(txtAge.Text, out age))
                {
                    if (cmbEntrep.Text != "")
                    {
                        if (rdb1.Checked)
                        {
                            sexe = "M";
                        }else if ( rdb2.Checked)
                        {
                            sexe ="F";
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez selectionner le sexe de l'employe", "Erreur", "erreur.png");
                            return;
                        }
                        var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntrep.Text);
                        idEntrep = listeEntrep[0].NumeroEntreprise;
                        nom = txtNom.Text + " " + txtPrenom.Text;
                        tele = txtTele.Text;
                          nomPat = txtNom.Text;
                         prenom =txtPrenom.Text;
                         entreprise = cmbEntrep.Text;
                         var patient = new Patient();
                         patient.NumeroPatient = idPatient;
                         patient.Nom = nomPat;
                         patient.Prenom = prenom;
                         patient.An = age.ToString();
                         patient.Sexe = sexe;
                         patient.Telephone = tele;
                         var listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep, textBox1.Text);
                         foreach (var pat in listeempl)
                         {
                             if (pat.Nom.ToUpper()  == nom.ToUpper())
                             {
                                 MonMessageBox.ShowBox("Cet employé existe deja dans la base de données, si c'est deux patients differents, faite un signe pour differencier", "Erreur", "erreur.png");
                                 return;

                             }
                         }
                        var emplo = new EmployeEntreprise(id, nom,sexe,age,tele,idEntrep);
                        if(ConnectionClassClinique.AjouterEmployeDuneEntreprise(emplo,patient))
                        {
                            txtNom.Text ="";
                            txtPrenom.Text ="";
                            txtAge.Text ="";
                            rdb1.Checked = false;
                            rdb2.Checked = false;
                            txtTele.Text ="";
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("veuillez selectionnre l'entreprise a laquelle l' emoloyé appartient", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("veuillez entrez un chiffre valide pour l' age", "Erreur", "erreur.png");
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez saisir lenom et prenom de l'employe", "erreur", "erreur.png");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
             if (cmbEntrep.Text != "")
                {
                    Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
                    Location = new Point(205, 120);
                    var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntrep.Text);
                    idEntrep = listeEntrep[0].NumeroEntreprise;
                    var listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep);
                    ListeEmploye(listeempl);
                    label4.Text = "Nbre : " + listeempl.Count.ToString();
                }
           
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                id = Int32.Parse(listView2.SelectedItems[0].SubItems[0].Text);
                txtNom.Text = listView2.SelectedItems[0].SubItems[1].Text;
                txtPrenom.Text = listView2.SelectedItems[0].SubItems[2].Text;
                txtAge.Text = listView2.SelectedItems[0].SubItems[4].Text;
                txtTele.Text = listView2.SelectedItems[0].SubItems[5].Text;
                cmbEntrep.DropDownStyle = ComboBoxStyle.DropDown;
                cmbEntrep.Text = listView2.SelectedItems[0].SubItems[6].Text;
                sousCouvert = listView2.SelectedItems[0].SubItems[1].Text + " " + listView2.SelectedItems[0].SubItems[2].Text;
                var sexe = listView2.SelectedItems[0].SubItems[3].Text;
                if (sexe == "F")
                { 
                    rdb2.Checked = true; }
                else if (sexe == "M")
                {
                    rdb1.Checked = true;
                }
            }
        }

        //modifier

        #region
        int idPatient;
       string nomPat, prenom,  entreprise, rhesus,sousCouvert;
        double poids, tension, temperature,  fraisCarnet;
         
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtNom.Text != "" && txtPrenom.Text != "")
            {
                if (Int32.TryParse(txtAge.Text, out age))
                {
                    if (cmbEntrep.Text != "")
                    {
                        if (rdb1.Checked)
                        {
                            sexe = "M";
                        }
                        else if (rdb2.Checked)
                        {
                            sexe = "F";
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez selectionner le sexe de l'employe", "Erreur", "erreur.png");
                            return;
                        }
                        var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntrep.Text);
                        idEntrep = listeEntrep[0].NumeroEntreprise;
                        nom = txtNom.Text + " " + txtPrenom.Text;
                        tele = txtTele.Text;
                        nomPat = txtNom.Text;
                         prenom =txtPrenom.Text;
                         entreprise = cmbEntrep.Text;
                        var emplo = new EmployeEntreprise(id, nom, sexe, age, tele, idEntrep);
                      
                       if(MonMessageBox.ShowBox("Voulez vous modifier les données de cet employé? " ,"Confirmation","confirmation.png")=="1")
                        {
                            ConnectionClassClinique.ModifierEmployeeEntreprise(emplo,sousCouvert);
                            {
                                txtNom.Text = "";
                                txtPrenom.Text = "";
                                txtAge.Text = "";
                                rdb1.Checked = false;
                                rdb2.Checked = false;
                                txtTele.Text = "";
                            }
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("veuillez selectionnre l'entreprise a laquelle l' emoloyé appartient", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("veuillez entrez un chiffre valide pour l' age", "Erreur", "erreur.png");
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez saisir lenom et prenom de l'employe", "erreur", "erreur.png");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                if(MonMessageBox.ShowBox("Voulez supprimer les données de cet employé?" ,"Confirmation","confirmation.png")=="1")
                {
                    ConnectionClassClinique.SupprimerEmployeeEntreprise(id);
                    {
                        
                                txtNom.Text = "";
                                txtPrenom.Text = "";
                                txtAge.Text = "";
                                rdb1.Checked = false;
                                rdb2.Checked = false;
                                txtTele.Text = "";
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (cmbEntrep.Text != null && listView2.SelectedItems.Count > 0)
            {
                var employe = listView2.SelectedItems[0].SubItems[1].Text + " " + listView2.SelectedItems[0].SubItems[2].Text;
                var frm = new ListeSCFrm();
                frm.listView2.Items.Clear();
                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                   where p.NomEntreprise.ToUpper()==cmbEntrep.Text
                                   where p.SousCouvert.ToUpper() == employe.ToUpper()
                                       select p;
                foreach (Patient patient in listePatient)
                {
                    var items = new string[]
                    {
                        patient.NumeroPatient.ToString(),
                        patient.Nom+" "+
                        patient.Prenom,
                        patient.Sexe,
                        patient.An.ToString()
                    };
                    var lstItems = new ListViewItem(items);
                    frm.listView2.Items.Add(lstItems);
                }
                frm.label1.Text = "Liste des patients S/C de " + employe;
                frm.ShowDialog();
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (cmbEntrep.Text != "")
            {
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntrep.Text);
                idEntrep = listeEntrep[0].NumeroEntreprise;
                var listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep, textBox1.Text);
                ListeEmploye(listeempl);
                label4.Text = "Nbre : " + listeempl.Count.ToString();
            }
        }

    }
}
