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
            Pen pen1 = new Pen(Color.FromArgb(255, 128, 0), 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 128, 0), 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
               Color.FromArgb(255, 128, 0), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 1);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
               Color.FromArgb(255, 128, 0), LinearGradientMode.BackwardDiagonal);
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
            etat = "1";
        }

        void ListeEmploye(List<EmployeEntreprise> listeEmpl)
        {
            try
            {
                var nom = "";
                var prenom = "";
                var entrep = "";
                dgvPatient.Rows.Clear();
                foreach (EmployeEntreprise empl in listeEmpl)
                {
                    var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(empl.IdEntreprise);
                    entrep = listeEntrep[0].NomEntreprise;

                    dgvPatient.Rows.Add(
                         empl.Numero.ToString(),
                         empl.Nom,
                         empl.Matricule,
                         empl.Sexe,
                         empl.Age.ToString(),
                         empl.Fonction,
                         empl.Telephone,
                         empl.IdPatient
                    );
                    editColumn.Image = global::GestionDuneClinique.Properties.Resources.edit;
                    deleteColumn.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
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

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        //modifier

        #region
        int idPatient;
        string nomPat, prenom, entreprise, rhesus, sousCouvert;
        #endregion

        string etat;
        private void button6_Click(object sender, EventArgs e)
        {
            if (cmbEntrep.Text != null && dgvPatient.SelectedRows.Count > 0)
            {
                var employe = dgvPatient.SelectedRows[0].Cells[1].Value.ToString();
                var frm = new ListeSCFrm();
                frm.listView2.Items.Clear();
                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                   where p.NomEntreprise.ToUpper() == cmbEntrep.Text
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

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (txtNom.Text != "" && txtPrenom.Text != "")
            {
                var patient = new Patient();
                if (Int32.TryParse(txtAge.Text, out age))
                {
                    patient.An = age.ToString();
                }
                else
                {
                    if (txtAge.Text.ToUpper() == "Adulte".ToUpper())
                    {
                        patient.An = txtAge.Text;
                    }
                    else
                    {
                        MonMessageBox.ShowBox("veuillez entrez un chiffre valide pour l' age", "Erreur", "erreur.png");
                        return;
                    }
                }
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
                    prenom = txtPrenom.Text;
                    entreprise = cmbEntrep.Text;
                    patient.NumeroPatient = idPatient;
                    patient.Nom = nomPat;
                    patient.Prenom = prenom;
                    patient.Sexe = sexe;
                    patient.Telephone = tele;
                    patient.NomEntreprise = cmbEntrep.Text;
                    patient.Rhesus = "";
                    patient.SousCouvert = "";
                    patient.Tabagiste = false;
                    patient.NumeroSocial = "";
                    patient.Drogueur = false;
                    patient.Couvert = true;
                    patient.Alcoolo = false;
                    patient.Adresse = "";
                    patient.NumeroPatient = idPatient;
                    patient.Fonction = txtFonction.Text;
                    var listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep, textBox1.Text);
                    ListeEmploye(listeempl);
                 
                    var emplo = new EmployeEntreprise();
                    emplo.Numero = id;
                    emplo.Nom = nom;
                    emplo.Sexe = sexe;
                    emplo.Telephone = txtTele.Text;
                    emplo.IdEntreprise = idEntrep;
                    emplo.Fonction = txtFonction.Text;
                    emplo.Age = age;
                    emplo.Matricule = txtMatricule.Text;
                    if (etat == "1")
                    {
                        if (ConnectionClassClinique.AjouterEmployeDuneEntreprise(emplo, patient))
                        {
                            txtNom.Text = "";
                            txtPrenom.Text = "";
                            txtAge.Text = "";
                            rdb1.Checked = false;
                            rdb2.Checked = false;
                            txtTele.Text = "";
                            txtFonction.Text = "";
                            txtMatricule.Text = "";
                            etat = "1";
                            listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntrep.Text);
                            //idEntrep = listeEntrep[0].NumeroEntreprise;
                            listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep);
                            ListeEmploye(listeempl);
                            label4.Text = "Nbre : " + listeempl.Count.ToString();
                        }
                    }
                    else if (etat == "2")
                    {
                        if (MonMessageBox.ShowBox("Voulez vous modifier les données de cet employé? ", "Confirmation", "confirmation.png") == "1")
                        {
                            ConnectionClassClinique.ModifierEmployeeEntreprise(emplo,patient, sousCouvert);
                            {
                                txtNom.Text = "";
                                txtPrenom.Text = "";
                                txtAge.Text = "";
                                rdb1.Checked = false;
                                rdb2.Checked = false;
                                txtTele.Text = "";
                                txtFonction.Text = "";
                                txtMatricule.Text = "";
                                etat = "1";
                                //var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntrep.Text);
                                idEntrep = listeEntrep[0].NumeroEntreprise;
                                //var listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep);
                                ListeEmploye(listeempl);
                                label4.Text = "Nbre : " + listeempl.Count.ToString();
                            }
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
                MonMessageBox.ShowBox("Veuillez saisir lenom et prenom de l'employe", "erreur", "erreur.png");
            }
        }

        private void cmbEntrep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEntrep.Text != "")
            {
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntrep.Text);
                idEntrep = listeEntrep[0].NumeroEntreprise;
                var listeempl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep);
                ListeEmploye(listeempl);
                label4.Text = "Nbre : " + listeempl.Count.ToString();
            }
        }

        private void dgvPatient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                if (dgvPatient.SelectedRows.Count > 0)
                {
                    if (dgvPatient.SelectedRows[0].Cells[1].Value.ToString().Contains(" "))
                    {
                        nom = dgvPatient.SelectedRows[0].Cells[1].Value.ToString().Substring(0, dgvPatient.SelectedRows[0].Cells[1].Value.ToString().LastIndexOf(" "));
                        prenom = dgvPatient.SelectedRows[0].Cells[1].Value.ToString().Substring(dgvPatient.SelectedRows[0].Cells[1].Value.ToString().LastIndexOf(" ") + 1);
                    }
                    else
                    {
                        nom = dgvPatient.SelectedRows[0].Cells[1].Value.ToString();
                    }
                    id = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    txtNom.Text = nom;
                    txtPrenom.Text = prenom;
                    txtMatricule.Text = dgvPatient.SelectedRows[0].Cells[2].Value.ToString();
                    txtAge.Text = dgvPatient.SelectedRows[0].Cells[4].Value.ToString();
                    txtFonction.Text = dgvPatient.SelectedRows[0].Cells[5].Value.ToString();
                    txtTele.Text = dgvPatient.SelectedRows[0].Cells[6].Value.ToString();
                    var sexe = dgvPatient.SelectedRows[0].Cells[3].Value.ToString();
                    idPatient = Convert.ToInt32(dgvPatient.SelectedRows[0].Cells[7].Value.ToString());
                    sousCouvert = dgvPatient.SelectedRows[0].Cells[1].Value.ToString();
                    if (sexe == "F")
                    {
                        rdb2.Checked = true;
                    }
                    else if (sexe == "M")
                    {
                        rdb1.Checked = true;
                    }
                    etat = "2";
                }
            }
            else if (e.ColumnIndex == 9)
            {
                if (MonMessageBox.ShowBox("Voulez supprimer les données de cet employé?", "Confirmation", "confirmation.png") == "1")
                {
                    id = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    ConnectionClassClinique.SupprimerEmployeeEntreprise(id);
                    {
                        dgvPatient.Rows.Remove(dgvPatient.SelectedRows[0]);
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


    }
}
