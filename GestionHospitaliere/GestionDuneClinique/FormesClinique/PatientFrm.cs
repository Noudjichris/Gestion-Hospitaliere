using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using GestionDuneClinique.AppCode;
using System.Windows.Forms;
using GestionDuneClinique.FormesClinique;
using SGDP.Formes;

namespace GestionDuneClinique.Formes
{
    public partial class PatientFrm : Form
    {
        public PatientFrm()
        {
            InitializeComponent();
        }

        public static int numeroPatient;
        static PatientFrm patientFrm;
        public static string btnClick;
        public static string ShowBox()
        {
            patientFrm = new PatientFrm();
            patientFrm.ShowDialog();
            return btnClick;
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox6.Width - 1, groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.ControlLight, SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox4.Width - 1, groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void PatientFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 5);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);

        }

        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox7.Width - 1, groupBox7.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.ControlLight, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox10_Paint(object sender, PaintEventArgs e)
        {
            //Graphics mGraphics = e.Graphics;
            //Pen pen1 = new Pen(SystemColors.ControlLight, 0);
            //Rectangle area1 = new Rectangle(0, 0, groupBox10.Width - 1, groupBox10.Height - 1);
            //LinearGradientBrush linearGradientBrush = new
            //    LinearGradientBrush(area1, SystemColors.ControlLight, SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            //mGraphics.FillRectangle(linearGradientBrush, area1);
            //mGraphics.DrawRectangle(pen1, area1);

        }
        private void PatientFrm_Load(object sender, EventArgs e)
        {
            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
            cmbEntreprise.Items.Add("");
            comboBox2.Items.Add("");
            foreach (Entreprise entreprise in listeEntrep)
            {
                cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
                comboBox2.Items.Add(entreprise.NomEntreprise.ToUpper());
            }
         
            Location = new Point(205, 120);
            Size = new Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            //groupBox7.Location = new Point((tabControl1.Width - groupBox7.Width) / 2, 60);
            //groupBox5.Location = new Point((tabPage3.Width - groupBox5.Width) / 2, 50);
            DesActiverLesChamps();
            panel1.Location = new Point(dgvPatient.Width + 10, panel1.Location.Y);
            //panel1.Focus();
            btnAjouter.Focus();
            etatAntecedant = "1";
            dataGridViewTextBoxColumn9.Width = 150;
            Column22.Width = 150;
            if(numeroPatient>0)
            textBox1.Text = numeroPatient.ToString();
        }

        //nouveau patient
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {

                ActiverLesChamps();
                ViderLesChamps();
                btnAjouter.Enabled = false;
                txtIdentifiant.Text = (ConnectionClassClinique.ObtenirNumeroPatient() + 1).ToString();
                etat = "1";
                txtNom.Focus();
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                comboBox1.Items.Clear();
                //cmbEntreprise.Items.Clear();
                comboBox1.Text = "";
                cmbEntreprise.Text = "";

                tabControl1.SelectTab(tabPage4);
                txtNom.Focus();
            }
            catch { }
        }

        void ActiverLesChamps()
        {
            groupBox10.Enabled = true;
            txtTemp.Enabled = true;
            txtMois.Enabled = true;
            txtNom.Enabled = true;
            txtPrenom.Enabled = true;
            txtTension.Enabled = true;
            rdbABNegative.Enabled = true;
            rdbABPositive.Enabled = true;
            rdbAPositive.Enabled = true;
            rdbANegative.Enabled = true;
            rdbBNegative.Enabled = true;
            rdbBPositive.Enabled = true;
            rdbInconnu.Enabled = true;
            rdbONegative.Enabled = true;
            rdbOPositive.Enabled = true;
            txtPoids.Enabled = true;
            txtAn.Enabled = true;
            txtTele.Enabled = true;
            txtNumeroSocial.Enabled = true;
            txtFonction.Enabled = true;
            txtAdresse.Enabled = true;
            txtPouls.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            btnEnregistrer.Enabled = true;
            groupBox3.Enabled = true;
            groupBox6.Enabled = true;
        }

        //desactiver les controles
        void DesActiverLesChamps()
        {
            txtMois.Enabled = false;
            txtTemp.Enabled = false;
            txtNom.Enabled = false;
            txtPrenom.Enabled = false;
            txtTension.Enabled = false;
            txtPoids.Enabled = false;
            txtAn.Enabled = false;
            txtTele.Enabled = false;
            txtNumeroSocial.Enabled = false;
            txtFonction.Enabled = false;
            txtAdresse.Enabled = false;
            txtPouls.Enabled = false;
            checkBox1.Enabled = false;
            rdbABNegative.Enabled = false;
            rdbABPositive.Enabled = false;
            rdbAPositive.Enabled = false;
            rdbANegative.Enabled = false;
            rdbBNegative.Enabled = false;
            rdbBPositive.Enabled = false;
            rdbONegative.Enabled = false;
            rdbOPositive.Enabled = false;
            rdbInconnu.Enabled = false;
            checkBox2.Enabled = false;
            btnEnregistrer.Enabled = false;
            groupBox3.Enabled = false;
            groupBox6.Enabled = false;
            groupBox10.Enabled = false;
            txtMois.Enabled = false;
        }

        void ViderLesChamps()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            txtTemp.Text = "";
            txtNom.Text = "";
            txtPrenom.Text = "";
            txtTension.Text = "";
            txtPoids.Text = "";
            txtAn.Text = "";
            txtTele.Text = "";
            txtMois.Text = "";
            rdbInconnu.Checked = true;
            txtAdresse.Text = "";
            txtFonction.Text = "";
            txtNumeroSocial.Text = "";
            chkAlcool.Checked = false;
            chkDrogue.Checked = false;
            chkTabac.Checked = false;
        }


        #region ProprietesPatient
        int idAntecedant, idPatient, idParam;
        double glycemie, poids, temperature, taille;
        string sexe, etat, stateParam, etatAntecedant;
        public int state;
        #endregion

        string ObtenirLeGroupeSanguin()
        {
            if (rdbABNegative.Checked)
            {
                return "AB-";
            }
            else if (rdbABPositive.Checked)
            {
                return "AB+";
            }
            else if (rdbANegative.Checked)
            {
                return "A-";
            }
            else if (rdbAPositive.Checked)
            {
                return "A+";
            }
            else if (rdbBNegative.Checked)
            {
                return "B-";
            }
            else if (rdbBPositive.Checked)
            {
                return "B+";
            }
            else if (rdbONegative.Checked)
            {
                return "O-";
            }
            else if (rdbOPositive.Checked)
            {
                return "O+";
            }
            else
            {
                return "";
            }
        }
        void PointerLeRhesus(string rhesus)
        {
            if (rhesus == "A-")
            {
                rdbANegative.Checked = true;
            }
            else if (rhesus == "A+")
            {
                rdbAPositive.Checked = true;
            }
            else if (rhesus == "AB-")
            {
                rdbABNegative.Checked = true;
            }
            else if (rhesus == "AB+")
            {
                rdbABPositive.Checked = true;
            }
            else if (rhesus == "B-")
            {
                rdbBNegative.Checked = true;
            }
            else if (rhesus == "B+")
            {
                rdbBPositive.Checked = true;
            }
            else if (rhesus == "O-")
            {
                rdbONegative.Checked = true;
            }
            else if (rhesus == "O+")
            {
                rdbOPositive.Checked = true;
            }
            else
            {
                rdbInconnu.Checked = true;
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                { return; }
                if (!string.IsNullOrEmpty(txtNom.Text) && !string.IsNullOrEmpty(txtPrenom.Text))
                {
                    if (checkBox1.Checked || checkBox2.Checked)
                    {
                        int ans, mois;
                        if (!string.IsNullOrEmpty(txtAn.Text) || !string.IsNullOrEmpty(txtMois.Text))
                        {
                            if ((!Int32.TryParse(txtAn.Text, out  ans) && !string.IsNullOrEmpty(txtAn.Text)) &&
                                (!txtAn.Text.ToUpper().Equals("ADULTE") && !txtAn.Text.ToUpper().Equals("ENFANT")))
                               
                            {
                                MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour l'age ou indiquer comme adulte ou enfant", "Erreur", "erreur.png");
                                return;
                            }
                            //else if ( 
                            //{
                            //    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour l'age ou indiquer comme adulte ou enfant", "Erreur", "erreur.png");
                            //    return;
                            //}
                            if (!Int32.TryParse(txtMois.Text, out mois) &&
                                !string.IsNullOrEmpty(txtMois.Text))
                            {
                                MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le mois ou laisser la le champ vide", "Erreur", "erreur.png");
                                return;
                            }
                            var patient = new Patient();
                            patient.Nom = txtNom.Text;
                            patient.Prenom = txtPrenom.Text;
                            patient.Telephone = txtTele.Text;
                            patient.NomEntreprise = cmbEntreprise.Text;
                            patient.SousCouvert = comboBox1.Text;
                            patient.Sexe = sexe;
                            patient.NumeroSocial = txtNumeroSocial.Text;
                            patient.Adresse = txtAdresse.Text;
                            patient.Fonction = txtFonction.Text;
                            patient.An = txtAn.Text;
                            patient.Couvert= chkSousCouvert.Checked ? true : false;
                            patient.Mois = txtMois.Text;
                            patient.Rhesus = ObtenirLeGroupeSanguin();
                            patient.Matricule = lblMatricule.Text;
                            if (chkTabac.Checked)
                            {
                                patient.Tabagiste = true;
                            }
                            else
                            {
                                patient.Tabagiste = false;
                            }
                            if (chkDrogue.Checked)
                            {
                                patient.Drogueur = true;
                            }
                            else
                            {
                                patient.Drogueur = false;
                            }
                            if (chkAlcool.Checked)
                            {
                                patient.Alcoolo = true;
                            }
                            else
                            {
                                patient.Alcoolo = false;
                            }
                            idPatient = Convert.ToInt32(txtIdentifiant.Text);
                            patient.NumeroPatient = idPatient;
                            if (etat == "1")
                            {
                                var listePatient = ConnectionClassClinique.ListeDesPatients();

                                foreach (var pat in listePatient)
                                {
                                    if (pat.Nom.ToUpper() + " " + pat.Prenom.ToUpper() == patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper())
                                    {
                                        if (MonMessageBox.ShowBox(
                                                "Ce patient existe deja dans la base de données, Voulez vous continuer à l'enregistrer comme un nouveau patient?",
                                                "Confirmation", "confirmation.png") == "1")
                                        {
                                            
                                        } //return;
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                            if (ConnectionClassClinique.EnregistrerUnPatient(patient, etat))
                            {
                                if (etat == "1")
                                {
                                   numeroPatient = ConnectionClassClinique.ObtenirNumeroPatientEnregistrer();
                                    //if (Int32.Parse(txtIdentifiant.Text) == ConnectionClassClinique.ObtenirNumeroPatient())
                                    //{
                                    //    numeroPatient = Int32.Parse(txtIdentifiant.Text);
                                    //}
                                    //else if (Int32.Parse(txtIdentifiant.Text) + 1 == ConnectionClassClinique.ObtenirNumeroPatient())
                                    //{
                                    //    numeroPatient = Int32.Parse(txtIdentifiant.Text) + 1;
                                    //}
                                    //else
                                    //{
                                    //    numeroPatient = Int32.Parse(txtIdentifiant.Text) + 2;
                                    //}
                                }
                                else
                                {
                                    numeroPatient = patient.NumeroPatient;
                                }

                                DesActiverLesChamps();
                                ViderLesChamps();
                                btnAjouter.Enabled = true;
                                btnEnregistrer.Enabled = false;
                                groupBox3.Enabled = true;
                                cmbEntreprise.Text = "";
                                dgvPatient.Rows.Clear();
                                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                                   where p.NumeroPatient == idPatient
                                                   select p;

                                foreach (var p in listePatient)
                                {

                                    dgvPatient.Rows.Add(
                                        p.NumeroPatient,
                                        p.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                                        p.Sexe, patient.An,
                                        p.Telephone,
                                        p.NumeroSocial,
                                        p.Fonction,
                                        p.Adresse,
                                        p.Rhesus,
                                        p.SousCouvert,
                                        p.NomEntreprise
                                        );
                                }
                                tabControl1.SelectTab(tabPage2);
                                btnAjouter.Focus();
                            }

                            if (btnClick == "1")
                            {
                                Dispose();
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez entrer l' age en année mois ou indiquer le statut en adulte ou enfant du patient en chiffre, puis réessayer", "Erreur", "erreur.png");
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez cocher pour le sexe du patient, puis réessayer", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer le nom et prenom du patient, puis réessayer", "Erreur", "erreur.png");
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            sexe = "M";
            checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            sexe = "F";
            checkBox1.Checked = false;
        }


        void ListeDespatients(List<Patient> listePatient)
        {
            try
            {
                dgvPatient.Rows.Clear();
                lblNombre.Text = listePatient.Count().ToString();
                foreach (Patient patient in listePatient)
                {
                    var age = AfficherAge(patient.An, patient.Mois);
                    dgvPatient.Rows.Add(
                        patient.NumeroPatient,
                        patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                        patient.Sexe, age,
                        patient.Telephone,
                        patient.NumeroSocial,
                        patient.Fonction,
                        patient.Adresse,
                        patient.Rhesus,
                        patient.SousCouvert,
                        patient.NomEntreprise,
                        patient.Couvert
                        );
                }
            }
            catch (Exception)
            {
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var listePatient =
                        ConnectionClassClinique.ListeDesPatientsParEntreprise(textBox1.Text, cmbEntreprise.Text);
                    ListeDespatients(listePatient);
                    dgvPatient.Focus();
                }
                catch
                {
                }
            }
        }

        Bitmap fichePatient;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(fichePatient, 10, 10, fichePatient.Width, fichePatient.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvPatient.SelectedRows.Count > 0)
            {
                var id = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == id
                                   select p;

                var patient = new Patient();
                foreach (var p in listePatient)
                    patient = p;
                fichePatient = Impression.FichePatient(patient);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
            }
        }
        string attache = "", matricule="";
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);
                    var idEntrep = listeEntrep[0].NumeroEntreprise;
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep, textBox2.Text);
                    comboBox1.Items.Clear();
                    foreach (EmployeEntreprise entreEmpl in listeEmpl)
                    {
                        comboBox1.Items.Add(entreEmpl.Nom);
                    }
                    if (attache != "")
                    {
                        listeEntrep = ConnectionClassClinique.ListeDesEntreprises(attache);
                        idEntrep = listeEntrep[0].NumeroEntreprise;
                        listeEmpl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep, textBox2.Text);

                        foreach (EmployeEntreprise entreEmpl in listeEmpl)
                        {
                            comboBox1.Items.Add(entreEmpl.Nom);
                        }
                    }

                    comboBox1.DroppedDown = true;
                    comboBox1.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            textBox2.Focus();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Visible = false;
        }

        private void dgvPatient_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox3.ReadOnly = false;

            try
            {
               
                if (!txtIdentifiant.Enabled)
                {
                    txtIdentifiant.Enabled = true;
                }
                else
                {
                    var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                       where p.NumeroPatient == Convert.ToInt32(txtIdentifiant.Text)
                                       select p;

                    if (listePatient.Count() > 0)
                    {
                        ViderLesChamps();
                        foreach (Patient patient in listePatient)
                        {


                            idPatient = patient.NumeroPatient;
                            txtNom.Text = patient.Nom.ToUpper();
                            txtPrenom.Text = patient.Prenom.ToUpper();
                            if (patient.Sexe == "M")
                            {
                                checkBox1.Checked = true;
                            }
                            else if (patient.Sexe == "F")
                            {
                                checkBox2.Checked = true;
                            }
                            txtAn.Text = patient.An;
                            txtTele.Text = patient.Telephone;
                            txtNumeroSocial.Text = patient.NumeroSocial;
                            txtFonction.Text = patient.Fonction;
                            txtAdresse.Text = patient.Adresse;
                            PointerLeRhesus(patient.Rhesus);
                            comboBox1.Text = patient.SousCouvert;
                            chkSousCouvert.Checked = patient.Couvert;
                            if (patient.Alcoolo)
                            {
                                chkAlcool.Checked = true;
                            }
                            else
                            {
                                chkAlcool.Checked = false;
                            }
                            if (patient.Drogueur)
                            {
                                chkDrogue.Checked = true;
                            }
                            else
                            {
                                chkDrogue.Checked = false;
                            }
                            if (patient.Tabagiste)
                            {
                                chkTabac.Checked = true;
                            }
                            else
                            {
                                chkTabac.Checked = false;
                            }
                            cmbEntreprise.Text = patient.NomEntreprise;

                            ActiverLesChamps();
                            etat = "2";
                            txtIdentifiant.Enabled = false;
                        }
                    }
                }
            }

            catch { }
        }

        private void checkBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkBox2.Checked = true;
                txtAn.Focus();
            }
        }

        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkBox1.Checked = true;
                txtAn.Focus();
            }
        }


        private void txtNom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(txtNom.Text))
                    {
                        txtPrenom.Focus();
                        if (txtNom.Text.Length >= 3)
                        {
                            listView2.Items.Clear();
                            var listePatient =
                                ConnectionClassClinique.ListeDesPatientsParNomOuPrenom(txtNom.Text, "");
                            foreach (Patient patient in listePatient)
                            {
                                var age = AfficherAge(patient.An, patient.Mois);

                                var items = new string[]
                    {
                        patient.NumeroPatient.ToString(),
                        patient.Nom + " "+
                        patient.Prenom,
                        age,patient.Telephone
                    };

                                var lstItems = new ListViewItem(items);
                                listView2.Items.Add(lstItems);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void txtPrenom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtPrenom.Text))
                {
                    checkBox1.Focus();
                    if (txtNom.Text.Length >= 3)
                    {
                        listView2.Items.Clear();
                        var listePatient =
                            ConnectionClassClinique.ListeDesPatientsParNomOuPrenom("",txtPrenom.Text);
                        foreach (Patient patient in listePatient)
                        {
                            var age = AfficherAge(patient.An, patient.Mois);

                            var items = new string[]
                            {
                                patient.NumeroPatient.ToString(),
                                patient.Nom + " "+
                                patient.Prenom,
                                age,patient.Telephone
                            };

                            var lstItems = new ListViewItem(items);
                            listView2.Items.Add(lstItems);
                        }
                    }
                }
            }
        }

        private void txtAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtAn.Text))
                    txtTele.Focus();
            }
        }

        private void txtTele_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnregistrer.Focus();
            }
        }

        public static string AfficherAge(string an, string mois)
        {
            int annee;
            if (!string.IsNullOrEmpty(an))
            {
                if (Int32.TryParse(an, out annee))
                {
                    if (!string.IsNullOrEmpty(mois))
                    {
                        if (annee == 1)
                        { return an + " an et " + mois + " mois "; }
                        else
                        {
                            return an + " ans et " + mois + " mois ";
                        }
                    }
                    else
                    {
                        if (annee == 1)
                        { return an + " an "; }
                        else
                        {
                            return an + " ans ";
                        }
                    }
                }
                else
                {
                    return an;
                }
            }
            else
            {
                return mois + " mois";
            }
        }
    
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (Int32.TryParse(textBox1.Text, out idPatient))
                {
                    dgvPatient.Rows.Clear();
                    try
                    {
                        var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                           where p.NumeroPatient == idPatient
                                           select p;

                        foreach (Patient patient in listePatient)
                        {
                            var age = AfficherAge(patient.An, patient.Mois);
                            dgvPatient.Rows.Add(
                                patient.NumeroPatient,
                                patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                                patient.Sexe, age,
                                patient.Telephone,
                                patient.NumeroSocial,
                                patient.Fonction,
                                patient.Adresse,
                                patient.Rhesus,
                                patient.SousCouvert,
                                patient.NomEntreprise,
                                patient.Couvert,
                                patient.DateEnregistrement
                                );
                        }
                    }
                    catch { }
                }
                else
                {
                    if (textBox1.Text.Length >= 3)
                    {
                        
                        var listePatient =
                            ConnectionClassClinique.ListeDesPatientsParEntreprise(textBox1.Text, comboBox2.Text);
                        ListeDespatients(listePatient);
                        dgvPatient.Focus();
                    }
                }
            }
            catch
            {
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button8_Click(null, null);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var listePatient =
                ConnectionClassClinique.ListeDesPatientsParEntreprise("", comboBox2.Text);
            ListeDespatients(listePatient);
            dgvPatient.Focus();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(comboBox2.Text);
                    var idEntrep = listeEntrep[0].NumeroEntreprise;
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep, textBox4.Text);
                    comboBox3.Items.Clear();
                    foreach (EmployeEntreprise entreEmpl in listeEmpl)
                    {
                        comboBox3.Items.Add(entreEmpl.Nom);
                    }
                    comboBox3.DroppedDown = true;
                    comboBox3.Focus();
                }
            }
            catch { }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox3.Text != "" && comboBox2.Text != "")
                {

                    dgvPatient.Rows.Clear();

                    var listePatient = ConnectionClassClinique.ListeDesPatientsDependants(comboBox3.Text, comboBox2.Text);

                    foreach (Patient patient in listePatient)
                    {
                        var age = AfficherAge(patient.An, patient.Mois);
                        dgvPatient.Rows.Add(
                            patient.NumeroPatient,
                            patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                            patient.Sexe, age,
                            patient.Telephone,
                            patient.NumeroSocial,
                            patient.Fonction,
                            patient.Adresse,
                            patient.Rhesus,
                            patient.SousCouvert,
                            patient.NomEntreprise,
                            patient.Couvert,
                            patient.DateEnregistrement
                            );
                    }
                }
            }
            catch
            {
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPatient.SelectedRows.Count > 0)
                {
                    idPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    if (MonMessageBox.ShowBox("Voulez vous supprimer les données de ce patient ?", "Confirmation", "confirmation.png") == "1")
                    {
                        if (ConnectionClassClinique.SupprimerUnPatient(idPatient))
                        {
                            dgvPatient.Rows.Remove(dgvPatient.SelectedRows[0]);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPatient.SelectedRows.Count > 0)
                {
                    etat = "2";
                    ActiverLesChamps();
                    var patiente = dgvPatient.SelectedRows[0].Cells[1].Value.ToString();
                    idPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    var listePatient = ConnectionClassClinique.ListeDesPatients(idPatient);
                    txtIdentifiant.Text = idPatient.ToString();
                    txtNom.Text = listePatient[0].Nom;
                    txtPrenom.Text = listePatient[0].Prenom;
                    txtMois.Text = listePatient[0].Mois;
                    txtAn.Text = listePatient[0].An;
                    txtTele.Text = listePatient[0].Telephone;
                    txtNumeroSocial.Text = listePatient[0].NumeroSocial;
                    txtFonction.Text = listePatient[0].Fonction;
                    txtAdresse.Text = listePatient[0].Adresse;
                    PointerLeRhesus(listePatient[0].Rhesus);
                    lblMatricule.Text = listePatient[0].Matricule;
                    if (listePatient[0].SousCouvert.ToUpper() == "TRUE")
                        chkSousCouvert.Checked = true;
                    else
                        chkSousCouvert.Checked = false;
                    
                        if (listePatient[0].Alcoolo)
                        {
                            chkAlcool.Checked = true;
                        }
                        else
                        {
                            chkAlcool.Checked = false;
                        }
                        if (listePatient[0].Drogueur)
                        {
                            chkDrogue.Checked = true;
                        }
                        else
                        {
                            chkDrogue.Checked = false;
                        }
                        if (listePatient[0].Tabagiste)
                        {
                            chkTabac.Checked = true;
                        }
                        else
                        {
                            chkTabac.Checked = false;
                        }
                    
                    var entreprise = listePatient[0].NomEntreprise;

                    var sc = listePatient[0].SousCouvert;
                    if (entreprise != "")
                    {
                        cmbEntreprise.DropDownStyle = ComboBoxStyle.DropDown;
                        cmbEntreprise.Text = entreprise;
                        comboBox1.Text = sc;
                        groupBox3.Enabled = true;
                    }
                    else
                    {
                        cmbEntreprise.Text="";
                        groupBox3.Enabled = true;
                    }

                    if (listePatient[0].Sexe.ToString() == "M")
                    {
                        checkBox1.Checked = true;
                    }
                    else if (listePatient[0].Sexe.ToString() == "F")
                    {
                        checkBox2.Checked = true;
                    }
                    stateParam = "1";
                    tabControl1.SelectTab(tabPage4);
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var frm = new GestionDuneClinique.FormesClinique.EntreEmplFrm();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTemp.Text) || Double.TryParse(txtTemp.Text, out temperature))
            {
                if (string.IsNullOrEmpty(txtPoids.Text) || Double.TryParse(txtPoids.Text, out poids))
                {
                    if (string.IsNullOrEmpty(txtTaille.Text) || Double.TryParse(txtTaille.Text, out taille))
                    {
                        if (string.IsNullOrEmpty(txtGlycemie.Text) || Double.TryParse(txtGlycemie.Text, out glycemie))
                        {
                            var param = new Parametres();
                            param.Temperature = temperature;
                            param.Taille = taille;
                            param.Poids = poids;
                            param.Identifiant = idPatient;
                            param.Glycemie = glycemie;
                            param.Pouls = txtPouls.Text;
                            param.Tension = txtTension.Text;
                            param.Date = DateTime.Now;
                            param.ID = idParam;
                            if (idPatient > 0)
                            {
                                if (ConnectionClassClinique.EnregistrerLesParametres(param, stateParam))
                                {
                                    txtNomPatientParam.Text = txtNom.Text + " " + txtPrenom.Text;
                                    txtTemp.Text = "";
                                    txtTension.Text = "";
                                    txtPoids.Text = "";
                                    txtPouls.Text = "";
                                    txtTaille.Text = "";
                                    txtGlycemie.Text = "";
                                    ListeDesParametrePatient();
                                    tabControl1.SelectTab(tabPage1);
                                }
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez saisir un chiffre valide pour la glycemie, puis réessayer", "Erreur", "erreur.png");
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez saisir un chiffre valide pour la taille, puis réessayer", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez saisir un chiffre valide pour le poids, puis réessayer", "Erreur", "erreur.png");
                }
            }
            else
            {
                MonMessageBox.ShowBox("Si la temperature est connu, veuillez entrer un chiffre valide, puis réessayer", "Erreur", "erreur.png");
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                ActiverLesChamps();
                ViderLesChamps();
                button6.Enabled = false;
                txtIdentifiant.Text = (ConnectionClassClinique.ObtenirNumeroPatient() + 1).ToString();
                etat = "1";
                txtNom.Focus();
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                comboBox1.Items.Clear();
                //cmbEntreprise.Items.Clear();
                comboBox1.Text = "";
                cmbEntreprise.Text = "";
            }
            catch { }
        }

        void ListeDesParametrePatient()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var taille = ""; var poids = ""; var temps = ""; var glycemie ="" ;
                foreach (var pr in ConnectionClassClinique.ListeDesParametres(idPatient))
                {
                    if (pr.Taille > 0)
                    {
                        taille = pr.Taille.ToString();
                    }

                    if (pr.Poids > 0)
                    {
                        poids = pr.Poids.ToString();
                    }
                    if (pr.Temperature > 0)
                    {
                        temps = pr.Temperature.ToString();
                    } 
                    if (pr.Glycemie > 0)
                    {
                        glycemie = pr.Glycemie.ToString();
                    }
                    dataGridView1.Rows.Add(
                        pr.ID, idPatient, pr.Date, taille, poids, temps, pr.Tension, pr.Pouls,glycemie);
                }
            }
            catch
            {
            }

        }
        //parametres du patient
        private void button7_Click_1(object sender, EventArgs e)
        {
            if (dgvPatient.SelectedRows.Count > 0)
            {
                idPatient = Convert.ToInt32(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                txtNomPatientParam.Text = dgvPatient.SelectedRows[0].Cells[1].Value.ToString();
                ListeDesParametrePatient();
                ActiverLesChamps();
                stateParam = "1";
                tabControl1.SelectTab(tabPage1);
            }
        }

        //remplir les champs des parametres
        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    idPatient = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    txtPoids.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                    txtTaille.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    txtTemp.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                    txtTension.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                    txtPouls.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                    txtGlycemie.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                    idParam = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    txtPoids.Enabled = true;
                    txtPouls.Enabled = true;
                    txtTaille.Enabled = true;
                    txtTemp.Enabled = true;
                    txtTension.Enabled = true;
                    groupBox6.Enabled = true;
                    stateParam = "2";

                }
            }
            catch { }
        }

        // supprimer les donnees de parametres
        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                if (ConnectionClassClinique.SupprimerUnParametre(id))
                {
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                }
            }
            catch { }
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            textBox5.SelectAll();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                idPatient = Convert.ToInt32(textBox5.Text);
                var liste = from p in ConnectionClassClinique.ListeDesPatients()
                            where p.NumeroPatient == idPatient
                            select p;
                foreach (var p in liste)
                {
                    txtNomPatientParam.Text = p.Nom + " " + p.Prenom;
                }
                ListeDesParametrePatient();
            }
            catch
            {
            }
        }

        //enregistrer un antecdant
        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                var antecedant = new Antecedant();
                antecedant.Traitement = txtTraitements.Text;
                antecedant.Description = txtAntecedant.Text;
                antecedant.FinAntecedant = dtpFinAntecedant.Value;
                antecedant.DebutAntecedant = dtpAntecedant.Value;
                antecedant.IdPatient = idPatient;
                antecedant.NumeroAntecedant = idAntecedant;
                if (ConnectionClassClinique.EnregistrerUnAntecedant(antecedant, etatAntecedant))
                {
                    etatAntecedant = "1";
                    txtAntecedant.Text = "";
                    txtTraitements.Text = "";
                    dtpAntecedant.Value = DateTime.Now;
                    dtpFinAntecedant.Value = DateTime.Now;
                    ListeDesAntecedants();
                }
            }
            catch { }
        }

        //liste des antecedants du patient
        void ListeDesAntecedants()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var liste = ConnectionClassClinique.ListeDesAntecedants(idPatient);
                foreach (var a in liste)
                {
                    dataGridView2.Rows.Add
                        (
                            a.NumeroAntecedant,
                            a.IdPatient,
                            a.DebutAntecedant.ToShortDateString(),
                            a.FinAntecedant.ToShortDateString(),
                            a.Description,
                            a.Traitement
                        );
                }
            }
            catch
            {
            }
        }

        //modifier les antecedants
        private void btnModifierAntecedant_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    idAntecedant = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    idPatient = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[1].Value.ToString());
                    dtpFinAntecedant.Value = DateTime.Parse(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
                    dtpAntecedant.Value = DateTime.Parse(dataGridView2.SelectedRows[0].Cells[3].Value.ToString());
                    txtTraitements.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                    txtAntecedant.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                    etatAntecedant = "2";
                }
            }
            catch { }
        }

        //supprimer les antecedant
        private void btnSupprimerAntecedant_Click(object sender, EventArgs e)
        {
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données? ", "Confirmation", "confirmation.png") == "1")
                {
                    idAntecedant = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    ConnectionClassClinique.SupprimerUnAntecedant(idAntecedant);
                    dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);

                }
            }
            catch { }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dgvPatient.SelectedRows.Count > 0)
            {
                idPatient = Convert.ToInt32(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                ListeDesAntecedants();
                tabControl1.SelectTab(tabPage3);
            }
        }

        //recherche entre 2 dates
        private void button16_Click(object sender, EventArgs e)
        {

            try
            {
                dataGridView1.Rows.Clear();
                var taille = ""; var poids = ""; var temps = "";
                var liste = from p in ConnectionClassClinique.ListeDesParametres(idPatient)
                            where p.Date >= dtp1.Value.Date
                            where p.Date < dtp2.Value.Date.AddHours(24)
                            select p;

                foreach (var pr in liste)
                {
                    if (pr.Taille > 0)
                    {
                        taille = pr.Taille.ToString();
                    }

                    if (pr.Poids > 0)
                    {
                        poids = pr.Poids.ToString();
                    }
                    if (pr.Temperature > 0)
                    {
                        temps = pr.Temperature.ToString();
                    }
                    dataGridView1.Rows.Add(
                        pr.ID, idPatient, pr.Date, taille, poids, temps, pr.Tension, pr.Pouls);
                }
            }
            catch
            {
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            stateParam = "1";
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
                ////btnAjouter_Click(null, null);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            btnAjouter_Click(null, null);
        }

        private void cmbEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbEntreprise.Text))
                {
                    chkSousCouvert.Checked = true;
                }
                else
                {
                    chkSousCouvert.Checked = false;
                }
                var liste = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);
                attache = liste[0].Attache;
            }
            catch (Exception)
            {
            }
        }

        private void chkSousCouvert_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbEntreprise.Text))
            {
                chkSousCouvert.Checked = true;
            }
            else
            {
                chkSousCouvert.Checked = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox3.Text != "" && comboBox2.Text != "")
                {

                    dgvPatient.Rows.Clear();

                    var listePatient = ConnectionClassClinique.ListeDesPatientsDependants(comboBox3.Text, comboBox2.Text);

                    foreach (Patient patient in listePatient)
                    {
                        var age = AfficherAge(patient.An, patient.Mois);
                        dgvPatient.Rows.Add(
                            patient.NumeroPatient,
                            patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                            patient.Sexe, age,
                            patient.Telephone,
                            patient.NumeroSocial,
                            patient.Fonction,
                            patient.Adresse,
                            patient.Rhesus,
                            patient.SousCouvert,
                            patient.NomEntreprise,
                            patient.Couvert,
                            patient.DateEnregistrement
                            );
                    }
                }
            }
            catch
            {
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                if (DateFrm.ShowBox())
                {
                    var dateDebut = DateFrm.dateDebut;
                    var dateFin = DateFrm.dateFin;

                    if (dgvPatient.SelectedRows.Count > 0)
                    {
                        var idPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                        var listePatient = from pp in AppCode.ConnectionClassClinique.ListeDesPatients()
                            where pp.NumeroPatient == idPatient
                            select pp;

                        var patient = new AppCode.Patient();
                        foreach (var pp in listePatient)
                            patient = pp;
                        var telephone = string.Format("{0,-22} {1,-80}", "Téléphone :  ", patient.Telephone);


                        var montantTotal = 0.0;
                        var days = (dateFin.Subtract(dateDebut)).Days;
                        var listeDateTimes = new List<DateTime>();
                        for (var i = 0; i <= days; i++)
                        {
                            listeDateTimes.Add(dateDebut.AddDays(i));
                        }
                        var listeFacture = new List<Facture>();
                        foreach (var date in listeDateTimes)
                        {
                            var facture = new Facture();

                            var dtFacture = ConnectionClassClinique.ListeDesFacturesParPatient(idPatient, date.Date, date.Date.AddHours(24));
                            DataTable dtPharmacie = ConnectionClassPharmacie.ListeDesVentes(idPatient, patient.Nom + " "+patient.Prenom, patient.NomEntreprise, date.Date, date.Date.AddHours(24));
                            if (dtFacture.Count() + dtPharmacie.Rows.Count > 0)
                            {
                                facture.DateFacture = date;
                                listeFacture.Add(facture);
                            }
                            foreach (var fac in dtFacture)
                            {
                                facture = new Facture();
                                facture.Designation = fac.Designation;
                                facture.PrixTotal = fac.PrixTotal;
                                facture.Quantite = fac.Quantite;
                                facture.Prix = fac.Prix;
                                listeFacture.Add(facture);
                                montantTotal += fac.PrixTotal;

                            }
                            foreach (DataRow facRow in dtPharmacie.Rows)
                            {
                                facture = new Facture();
                                facture.Designation = facRow.ItemArray[4].ToString();
                                facture.PrixTotal = Convert.ToDouble(facRow.ItemArray[7].ToString());
                                facture.Quantite = Convert.ToInt32(facRow.ItemArray[5].ToString());
                                facture.Prix = Convert.ToDouble(facRow.ItemArray[6].ToString());
                                montantTotal += Convert.ToDouble(facRow.ItemArray[7].ToString());
                                listeFacture.Add(facture);
                            }
                        }

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                        var jour = DateTime.Now.Day;
                        var mois = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var date1 = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" +
                                   min + "_" + sec;

                        var pathFolder = "C:\\Dossier Clinique";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        pathFolder = pathFolder + "\\Rapport des Patient";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        sfd.InitialDirectory = pathFolder;
                        sfd.FileName = "Rapport de la caisse  _imprimé_le_" + date1 + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var rapportCaissier = Impression.RapportPatient("RAPPORTS DE LA PHARMACIE du "+dateDebut.ToShortDateString() +
                                " au " + dateFin.ToShortDateString(), patient, listeFacture,montantTotal);
                            var inputImage = @"cdali";
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(rapportCaissier, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                            if (listeFacture.Count > 40)
                            {
                                var count = (listeFacture.Count - 40) / 52;

                                for (var i = 0; i <= count; i++)
                                {
                                    if (i * 52 < listeFacture.Count)
                                    {
                                         rapportCaissier = Impression.RapportPatient(listeFacture, montantTotal, i);
                                        inputImage = @"cdali" + i;
                                        // Create an empty page
                                        pageIndex = document.addPage();

                                        document.addImageReference(rapportCaissier, inputImage);
                                        img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                    }
                                }

                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception)
            {

            }



        }

        private void button27_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var liste = ConnectionClassClinique.ListeDesPatients(dateTimePicker1.Value, dateTimePicker1.Value.Date.AddHours(24));
            ListeDespatients(liste);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            cmbEntreprise.Items.Clear();
            var listeEntrep = from en in ConnectionClassClinique.ListeDesEntreprises()
                              where en.NomEntreprise.ToUpper().Contains(textBox3.Text.ToUpper())
                              select en;
            cmbEntreprise.Items.Add("");
            foreach (Entreprise entreprise in listeEntrep)
            {
                cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                    var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);
                    var idEntrep = listeEntrep[0].NumeroEntreprise;
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep, comboBox1.Text);

                    foreach (EmployeEntreprise entreEmpl in listeEmpl)
                    {
                        lblMatricule.Text = entreEmpl.Matricule;
                    }
                    if (attache != "")
                    {
                        listeEntrep = ConnectionClassClinique.ListeDesEntreprises(attache);

                        idEntrep = listeEntrep[0].NumeroEntreprise;
                        listeEmpl = ConnectionClassClinique.ListeDesEmployeesEntreprise(idEntrep, comboBox1.Text);
                        foreach (EmployeEntreprise entreEmpl in listeEmpl)
                        {
                            lblMatricule.Text = entreEmpl.Matricule;
                        }
                    }

                }
            catch (Exception)
            {
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var listePatient = ConnectionClassClinique.ListeDesPatients(Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text));
                         
                    if (listePatient.Count() > 0)
                    {
                        ViderLesChamps();
                        foreach (Patient patient in listePatient)
                        {


                            idPatient = patient.NumeroPatient;
                            txtNom.Text = patient.Nom.ToUpper();
                            txtPrenom.Text = patient.Prenom.ToUpper();
                            if (patient.Sexe == "M")
                            {
                                checkBox1.Checked = true;
                            }
                            else if (patient.Sexe == "F")
                            {
                                checkBox2.Checked = true;
                            }
                            txtAn.Text = patient.An;
                            txtTele.Text = patient.Telephone;
                            txtNumeroSocial.Text = patient.NumeroSocial;
                            txtFonction.Text = patient.Fonction;
                            txtAdresse.Text = patient.Adresse;
                            PointerLeRhesus(patient.Rhesus);
                            comboBox1.Text = patient.SousCouvert;
                            chkSousCouvert.Checked = patient.Couvert;
                            if (patient.Alcoolo)
                            {
                                chkAlcool.Checked = true;
                            }
                            else
                            {
                                chkAlcool.Checked = false;
                            }
                            if (patient.Drogueur)
                            {
                                chkDrogue.Checked = true;
                            }
                            else
                            {
                                chkDrogue.Checked = false;
                            }
                            if (patient.Tabagiste)
                            {
                                chkTabac.Checked = true;
                            }
                            else
                            {
                                chkTabac.Checked = false;
                            }
                            cmbEntreprise.Text = patient.NomEntreprise;

                            ActiverLesChamps();
                            etat = "2";
                            txtIdentifiant.Enabled = false;
                        }
                    
                }
            }

            catch { }
        }

    }
}
