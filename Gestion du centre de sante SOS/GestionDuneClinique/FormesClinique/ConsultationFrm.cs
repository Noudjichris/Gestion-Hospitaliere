using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestionAcademique;
using GestionDuneClinique.AppCode;
using GestionDuneClinique.FormesClinique;

namespace GestionDuneClinique.Formes
{
    public partial class ConsultationFrm : Form
    {
        public ConsultationFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,   Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ConsultationFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1,    Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1,    Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int idPat;
                PatientFrm.btnClick = "1";
                if (Int32.TryParse(textBox1.Text, out idPat))
                    PatientFrm.numeroPatient=idPat;
                if (PatientFrm.ShowBox() == "1")
                {
                    textBox1.Text = PatientFrm.numeroPatient > 0 ? PatientFrm.numeroPatient.ToString() : "";
                    textBox1_TextChanged(null, null);
                    btnEnregistrer.Focus();
                }
            }
            catch { }
        }

        void DesactiverLesControles()
        {
            cmbType.Enabled = false;
            dtpConsult.Enabled = false;
            cmbMedecin.Enabled = false;
            textBox1.Enabled = false;
            txtFrais.Enabled = false;
        }

        void ActiverLesControles()
        {
            cmbType.Enabled = true;
            dtpConsult.Enabled = true;
            cmbMedecin.Enabled = true;
            textBox1.Enabled = true;
            //txtFrais.Enabled = true;
        }

        void ViderLesChamps()
        {
            //textBox1.Text = "";
            cmbType.Text = "";
            dataGridView2.Rows.Clear();
            comboBox2.Text = "";
            cmbType.Text = "";
            //cmbMedecin.Text = "";
            txtFrais.Text = "";
        }
        
        string etat;
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            etat = "1";
            ActiverLesControles();
            ViderLesChamps();
            btnAjouter.Enabled = false;
            btnEnregistrer.Enabled = true;
            cmbType.DroppedDown = true;
            cmbType.Focus();
            btnBonSoin.Enabled = true;
            btnEnregistrer.Enabled = true;
            idConsul = 0;
        }

        void ListeConsultation(List<Consultation> listeConsult)
        {
            try
            {
                dgvConsult.RowTemplate.Height = 25;
                dgvConsult.Rows.Clear();
                var consulta = "";
                foreach (Consultation consultation in listeConsult)
                {

                    if (consultation.TypeConsultation == "CONSULTATION DE SPECIALITE")
                    {
                        consulta = "Consultation en ".ToUpper() + consultation.Specialite.ToUpper();
                    }
                    else
                    {
                        consulta = consultation.TypeConsultation;
                    }
                    if (consultation.NomEmploye.ToUpper() == "PARTENAIRE EXTERNE")
                    {
                        consultation.NomEmploye = consultation.NomEmploye + " " + consultation.Partenaire;
                    }
                    dgvConsult.Rows.Add(consultation.NumeroConsultation, consulta.ToUpper(), 
                        consultation.DateConsultation, consultation.RV,
                        consultation.Description, consultation.NomEmploye.ToUpper(),
                        consultation.NomPatient.ToUpper(), consultation.Frais, consultation.IdPatient,consultation.Specialite.ToUpper());
                    editColumn.Image = global::GestionDuneClinique.Properties.Resources.edit;
                    deleteColumn.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    //foreach (DataGridViewRow dgrv in dgvConsult.Rows)
                    //{
                    //    flag = AppCode.ConnectionClassClinique.SiFactureEnBon("CONSULTATION", Int32.Parse(dgrv.Cells[0].Value.ToString()));
                    //    if (flag)
                    //    {
                    //        dgrv.DefaultCellStyle.BackColor = Color.White;
                    //    }
                    //}
                }
            }
            catch { }
        }

        #region StaticMemberOfConsultation
        static ConsultationFrm ConsultFrm;
        public static string btnClick, patient, typeConsultation, State="0";
        public static int idConsultation, idPatiente;
        public static double montant;
        public static string ShowBox()
        {
            try
            {
                ConsultFrm = new ConsultationFrm();
                //ConsultFrm.Location = new Point(205, 120);
                ConsultFrm.ShowDialog();
            }
            catch { }
            return btnClick;
        } 
        #endregion
        
        public bool flag = false;
        private void ConsultationFrm_Load(object sender, EventArgs e)
        {
            try
            {
                if (flag)
                {
                    State = "0";
                }
                txtFrais.Enabled = false;
                button7.Location = new Point(Width - 40, 5);
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
                cmbEntreprise.Items.Add("");
                foreach (Entreprise entreprise in listeEntrep)
                {
                    cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
                }
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
                var list = from p in listeEmploye
                           where !p.Titre.ToUpper().Contains("CAISS")
                           select p.NomEmployee;

                foreach (var empl in list)
                {
                    cmbMedecin.Items.Add(empl.ToUpper());
                }
                cmbMedecin.Text = "CONSULTANT";
       
                var items = from item in ConnectionClassClinique.ListeDesTypesConsultations()
                    group item.TypeConsultation by item.TypeConsultation
                    into groups
                    //orderby item.
                    select new {key=groups.Key };
                foreach (var item in items)
                {
                    cmbType.Items.Add(item.key);
                }
                if (State == "1")
                {
                }
                else
                {
                    var listeConsultation = ConnectionClassClinique.ListeDesConsultations(DateTime.Now.Date);
                    ListeConsultation(listeConsultation);
                }
                btnAjouter.Focus();
            }
            catch (Exception)
            {
            }
        }
        
           
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listePatient = new List<Patient>();
                if (Int32.TryParse(textBox1.Text, out idPatient))
                {
                     listePatient =  ConnectionClassClinique.ListeDesPatients(idPatient);                    
                }
                else
                {
                    if (textBox1.Text.Length > 2)
                    {
                        listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise(textBox1.Text, cmbEntreprise.Text);
                    }
                } if (listePatient.Count() > 0)
                {
                    dataGridView2.Rows.Clear();
                    foreach (var patient in listePatient)
                    {
                        var age =  AfficherAge( patient.An,patient.Mois);
                        dataGridView2.Rows.Add(
                            patient.NumeroPatient.ToString(),
                            patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                         age.ToLower(), patient.Sexe,
                            patient.NomEntreprise,
                            patient.Couvert,
                      patient.SousCouvert
                        );
                    }
                }
           
            }
            catch { }
        }

        #region 
        int idConsul, idPatient;
        DateTime dateConsult, rv;
        string typeConsult, desc, numEmpl,specialite;
        double frais;
        #endregion
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbType.Text))
                {
                    if (!string.IsNullOrEmpty(cmbMedecin.Text))
                    {
                        if (Double.TryParse(txtFrais.Text, out frais))
                        {
                            if (dataGridView2.SelectedRows.Count > 0)
                            {
                                idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                            }
                            else if (idPatient > 0)
                            {
                                //idPatient = idPatient;
                            }
                            else
                            {
                                MonMessageBox.ShowBox("Veuillez selectionner le nom du patient sur la liste.", "Erreur", "erreur.png");
                                return;
                            }
                            if (cmbType.Text == "CONSULTATION DE SPECIALITE" && string.IsNullOrEmpty(comboBox2.Text))
                            {
                                MonMessageBox.ShowBox("Veuillez selectionner la spécialité sur la liste deroulante.", "Erreur", "erreur.png");
                                return;
                            }
                            if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                            {
                                return;
                            }
                            rv = dtpConsult.Value;
                            dateConsult = dtpConsult.Value;
                            typeConsult = cmbType.Text;
                            specialite = comboBox2.Text;
                            var nomMedecin = cmbMedecin.Text;
                            var listeEmploye = ConnectionClassClinique.ListeDesEmployees("nom_empl", nomMedecin);
                            numEmpl = listeEmploye[0].NumMatricule;
                            //var consultation = new Consultation(idConsul, typeConsult, dateConsult, rv, desc, numEmpl, idPatient, frais, specialite, "");
                          var consultation= new Consultation();
                            consultation.NumeroConsultation = idConsul;
                            consultation.TypeConsultation = typeConsult;
                            consultation.DateConsultation = dateConsult;
                            consultation.RV = rv;
                            consultation.Description = desc;
                            consultation.NumeroEmploye = numEmpl;
                            consultation.IdPatient = idPatient;
                            consultation.Frais = frais;
                            consultation.Specialite = specialite;
                            consultation.Partenaire = "";
                            if (ConnectionClassClinique.Verification(idPatient, dateConsult, typeConsult))
                            {
                                MonMessageBox.ShowBox("La validité de la consultation est de deux(2) semaines", "Erreur", "erreur.png");
                                return;
                            }
                            if (etat == "1")
                            {

                                if (ConnectionClassClinique.EnregistrerUneConsultation(consultation))
                                {
                                    if (State == "1")
                                    {
                                        idPatiente = idPatient;
                                        if (cmbType.Text == "CONSULTATION DE SPECIALITE")
                                        {
                                            typeConsultation = "CONSULTATION EN " + comboBox2.Text.ToUpper();
                                        }
                                        else
                                        {
                                            typeConsultation = cmbType.Text.ToUpper();
                                        }
                                        montant = Double.Parse(txtFrais.Text);
                                        patient = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                                        idConsultation = ConnectionClassClinique.ObtenirDerniereConsultation(GestionAcademique.LoginFrm.matricule);
                                        btnClick = "1";
                                        Dispose();
                                    }
                                    else
                                    {
                                        idConsultation = ConnectionClassClinique.ObtenirDerniereConsultation(GestionAcademique.LoginFrm.matricule);
                                        btnBonSoin.Enabled = false;
                                        btnEnregistrer.Enabled = false;
                                        ViderLesChamps();
                                        idConsul = 0;
                                        DesactiverLesControles();
                                        btnEnregistrer.Enabled = false;
                                        btnAjouter.Enabled = true;
                                        var listeConsult = ConnectionClassClinique.ListeDesConsultations(dateConsult);
                                        ListeConsultation(listeConsult);
                                    }
                                }
                            }         
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le frais de la consultation.", "Erreur", "erreur.png");
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner le nom du medecin sur la liste deroulante", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer le type de consultation", "Erreur", "erreur.png");
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }
        //rechercher
        private void button5_Click(object sender, EventArgs e)
        {           
                var listeConsult = ConnectionClassClinique.ListeDesConsultations(textBox2.Text);
                ListeConsultation(listeConsult); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvConsult.SelectedRows.Count > 0)
                {
                    idConsul = Int32.Parse(dgvConsult.SelectedRows[0].Cells[0].Value.ToString());
                    idPatient = Int32.Parse(dgvConsult.SelectedRows[0].Cells[8].Value.ToString());
                    var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                       where p.NumeroPatient == idPatient
                                       select p;

                    var patient = new Patient();
                    foreach (var p in listePatient)
                        patient = p;
                    var listeFacture = from f in ConnectionClassClinique.TableDesDetailsFacturesProforma()
                                       where f.NumeroActe == idConsul
                                       where f.Sub == "CONSULTATION"
                                       select f.NumeroFacture;
                    var numeroFacture = 0;
                    foreach (var a in listeFacture)
                        numeroFacture = a;
                    bitmap = Impression.ImprimerAnalyse(numeroFacture, patient, dateTimePicker1.Value, GestionAcademique.LoginFrm.nom,1,"Examen");
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        Bitmap bitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            e.Graphics.DrawImage(bitmap, -5, 20, bitmap.Width ,bitmap.Height);
            e.HasMorePages = false;

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void cmbEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbEntreprise.Text))
                {;
                 
                    dataGridView2.Rows.Clear();
                    var listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise("", cmbEntreprise.Text);
                    foreach (var patient in listePatient)
                    {
                        var age = AfficherAge(patient.An, patient.Mois).ToLower();
                        dataGridView2.Rows.Add(
                          patient.NumeroPatient.ToString(),
                          patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                         age.ToLower(), patient.Sexe,
                          patient.NomEntreprise,
                          patient.Couvert,
                          patient.SousCouvert
                      );
                    }
                    if (cmbType.Text == "Carnet")
                    {
                    }
                    else
                    {
                        //txtFrais.Text = "5000";
                    }
                }
            }
            catch { }
        }

              private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5_Click(null, null);
            }
        }

        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
            }
        }

        private void txtType_KeyPress(object sender, KeyPressEventArgs e)
        { 
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (cmbType.Text == "CONSULTATION DE SPECIALITE")
                {
                    comboBox2.DroppedDown = true;
                    comboBox2.Focus();
                }
                else
                {
                textBox1.Focus();
                }
            
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dataGridView2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                button3_Click(null, null);
            }
        }
        private void txtFrais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }

        private void cmbMedecin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbMedecin.Text != "")
                {
                    textBox1.Focus();
                }
                else
                {
                    cmbMedecin.Focus();
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from cn in ConnectionClassClinique.ListeDesTypesConsultations()
                            where cn.TypeConsultation.StartsWith(cmbType.Text, StringComparison.CurrentCulture)
                    where cn.Specialite.StartsWith(comboBox2.Text,
                        StringComparison.InvariantCultureIgnoreCase)
                    orderby cn.Specialite
                    select cn;
                foreach (var frais in liste)
                {
                    if (rdb2.Checked)
                    {
                        txtFrais.Text = frais.FraisConventionnee.ToString();
                    }
                    else
                    {
                        txtFrais.Text = frais.Frais.ToString();
                    }
                }
               var  designation = typeConsultation = "CONSULTATION EN " + comboBox2.Text.ToUpper();
                var count = ConnectionClassClinique.CountDetailsActes(designation, DateTime.Now.Date, DateTime.Now.Date);
                count += ConnectionClassClinique.CountDetailsActesDansConventionnes(designation, DateTime.Now.Date, DateTime.Now.Date);
                button3.Text = count.ToString();
            }
            catch
            {
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtFrais.Enabled = true;
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Focus();
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvConsult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                try
                {
                    if (dgvConsult.SelectedRows.Count > 0)
                    {
                        //if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                        {
                            etat = "2";
                            textBox1.Text = "";
                            ActiverLesControles();
                            btnEnregistrer.Enabled = true;
                            idPatient = Int32.Parse(dgvConsult.SelectedRows[0].Cells[8].Value.ToString());
                            idConsul = Int32.Parse(dgvConsult.SelectedRows[0].Cells[0].Value.ToString());
                            cmbMedecin.DropDownStyle = ComboBoxStyle.DropDown;
                            textBox1.Text = idPatient.ToString();
                            cmbType.Text = dgvConsult.SelectedRows[0].Cells[1].Value.ToString();
                            if (dgvConsult.SelectedRows[0].Cells[5].Value.ToString().ToUpper().Contains("PARTENAIRE EXTERNE"))
                            {
                                cmbMedecin.Text = "PARTENAIRE EXTERNE";
                                var partenaire = dgvConsult.SelectedRows[0].Cells[5].Value.ToString().Substring(19);
                            }
                            else
                            {
                                cmbMedecin.Text = dgvConsult.SelectedRows[0].Cells[5].Value.ToString();
                            }
                            var liste = ConnectionClassClinique.TableDesDetailsFacturesProforma().Where(t =>
                                t.NumeroActe == Int32.Parse(dgvConsult.SelectedRows[0].Cells[0].Value.ToString())).Where(
                                p => p.Sub == "CONSULTATION");
                            var netPayer = .0;
                            foreach(var l in liste)    
                             netPayer = l.MontantFactural;
                            txtFrais.Text = dgvConsult.SelectedRows[0].Cells[7].Value.ToString();
                            dtpConsult.Value = DateTime.Parse(dgvConsult.SelectedRows[0].Cells[2].Value.ToString());
                            cmbType.Text= dgvConsult.SelectedRows[0].Cells[1].Value.ToString();
                            if (!string.IsNullOrEmpty(dgvConsult.SelectedRows[0].Cells[9].Value.ToString()))
                            {
                                cmbType.Text = "CONSULTATION DE SPECIALITE";
                                comboBox2.Text = dgvConsult.SelectedRows[0].Cells[9].Value.ToString();
                            }
                      
                            btnBonSoin.Enabled = true ;
                            btnEnregistrer.Enabled = true;
                        }
                    }
                }
                catch { }
            }
            else if (e.ColumnIndex == 11)
            {
                if (dgvConsult.SelectedRows.Count > 0)
                {
                    //if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                    {

                        if (MonMessageBox.ShowBox("Voulez vous supprimer les donnees de la consultation?", "Confirmation", "confirmation") == "1")
                        {
                            idConsul = Int32.Parse(dgvConsult.SelectedRows[0].Cells[0].Value.ToString());
                            ConnectionClassClinique.SupprimerUneConsultaion(idConsul);
                            ViderLesChamps();
                            DesactiverLesControles();
                            btnEnregistrer.Enabled = false;
                            btnAjouter.Enabled = true;
                            cmbMedecin.DropDownStyle = ComboBoxStyle.DropDownList;
                            var listeConsult = ConnectionClassClinique.ListeDesConsultations(DateTime.Now.Date);
                            ListeConsultation(listeConsult);
                        }
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            idConsultation = 0;
            btnClick = "2";
            Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnEnregistrer.Focus();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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
                        return an + " ans et " + mois + " mois ";
                    }
                    else
                    {
                        return an + " ans ";
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
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(cmbType.Text))
                {
                    if (!string.IsNullOrEmpty(cmbMedecin.Text))
                    {
                        if (Double.TryParse(txtFrais.Text, out frais))
                        {
                            if (dataGridView2.SelectedRows.Count > 0)
                            {
                                idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                            }
                            else if (idPatient > 0)
                            {
                                //idPatient = idPatient;
                            }
                            else
                            {
                                MonMessageBox.ShowBox("Veuillez selectionner le nom du patient sur la liste.", "Erreur", "erreur.png");
                                return;
                            }
                            if (cmbType.Text.ToUpper()== "CONSULTATION DE SPECIALITE" .ToUpper()&& string.IsNullOrEmpty(comboBox2.Text))
                            {
                                MonMessageBox.ShowBox("Veuillez selectionner la spécialité sur la liste deroulante.", "Erreur", "erreur.png");
                                return;
                            }

                            if (!string.IsNullOrEmpty(dataGridView2.SelectedRows[0].Cells[4].Value.ToString()) &&
                                bool.Parse(dataGridView2.SelectedRows[0].Cells[5].Value.ToString()))
                            {
                                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                                {
                                    return;
                                }
                                //idPatient = Int32.Parse(listView2.SelectedItems[0].SubItems[0].Text);
                                rv = dtpConsult.Value;
                                dateConsult = dtpConsult.Value;
                                typeConsult = cmbType.Text;
                                specialite = comboBox2.Text;
                                
                                numEmpl = GestionAcademique.LoginFrm.matricule; ;
                                var consultation = new Consultation();
                                consultation.NumeroConsultation = idConsul;
                                consultation.TypeConsultation = typeConsult;
                                consultation.DateConsultation = dateConsult;
                                consultation.RV = rv;
                                consultation.Description = desc;
                                consultation.NumeroEmploye = numEmpl;
                                consultation.IdPatient = idPatient;
                                consultation.FraisConventionnee = frais;
                                consultation.Frais = frais;
                                consultation.Specialite = specialite;
                                consultation.Partenaire = "";
                                {
                                    if (ConnectionClassClinique.BondeConsultation(consultation))
                                    {
                                        idConsul = 0;
                                        button1_Click(null, null);
                                        btnBonSoin.Enabled = false;
                                        btnEnregistrer.Enabled = false;
                                        ViderLesChamps();
                                        DesactiverLesControles();
                                        btnEnregistrer.Enabled = false;
                                        btnAjouter.Enabled = true;
                                        textBox1.Text = "";
                                        var listeConsult = ConnectionClassClinique.ListeDesConsultations(dateConsult);
                                        ListeConsultation(listeConsult);
                                    }
                                }
                            }
                            else
                            {
                                if (dataGridView2.SelectedRows[0].Cells[3].Value.ToString() == "M")
                                {
                                    MonMessageBox.ShowBox("Ce patient n'est pas couvert.", "Erreur", "erreur.png");
                                }
                                else
                                {
                                    MonMessageBox.ShowBox("Cette patiente n'est pas couverte.", "Erreur", "erreur.png");
                                }
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le frais de la consultation.", "Erreur", "erreur.png");
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner le nom du consultant sur la liste deroulante", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer le type de consultation", "Erreur", "erreur.png");
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            var listeConsult = ConnectionClassClinique.ListeDesConsultations(dateTimePicker1.Value.Date);
            ListeConsultation(listeConsult); 
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                cmbType.Items.Clear();
            var frm = new TypeConsultationFrm();
            frm.Location =new Point(Form1.xLocation+ groupBox2.Location.X, Form1.yLocation+groupBox2.Location.Y);
            frm.ShowDialog();
            var items = from item in ConnectionClassClinique.ListeDesTypesConsultations()
                group item.TypeConsultation by item.TypeConsultation
                into groups
                //orderby item.
                select new { key = groups.Key };
            foreach (var item in items)
            {
                cmbType.Items.Add(item.key);
            }

            }
            catch 
            {
            }
        }

        private void ChoisirLeMedecin()
        {
            //try
            //{
            //    var listeProgramme = from p in ConnectionClassClinique.ListeDesProgrammes()
            //        where p.Annee == DateTime.Now.Year
            //        where p.DateDebut >= DateTime.Now.Date
            //        where p.DateFin <= DateTime.Now.Date
            //        select p;
            //    foreach (var p in listeProgramme)
            //    {
            //        var service = "";
            //        var dt = ConnectionClassClinique.ListeService(p.IDService);
            //        if(dt.Rows.Count>0)
            //         service = dt.Rows[0].ItemArray[1].ToString();

            //        if (service.ToUpper().Contains("CONSULTATION"))
            //        {
            //            //var dtP=ConnectionClassClinique.
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //}
        }
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                button3.Text = "0";
                comboBox2.Items.Clear();
                if (cmbType.Text.StartsWith("CONSULTATION DE SPECIALITE"))
                {
                    comboBox2.Enabled = true;
                    var liste = from cn in ConnectionClassClinique.ListeDesTypesConsultations()
                        where cn.TypeConsultation.StartsWith("CONSULTATION DE SPECIALITE",
                            StringComparison.InvariantCultureIgnoreCase)
                        orderby cn.Specialite
                        select cn.Specialite;
                    foreach (var cn in liste)
                    {
                        comboBox2.Items.Add(cn.ToUpper());
                    }
                  
                }
                else if (cmbType.Text.ToUpper() == "Consultation Pre-Natale".ToUpper())
                {
                    txtFrais.Text = "";
                    comboBox2.Enabled = false;
                    comboBox2.Text = "";
                    var dayOfWeek = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday" };
                    bool found = false;
                    var lstConsultation = from c in ConnectionClassClinique.ListeDesTypesConsultations()
                                          where c.TypeConsultation.StartsWith(cmbType.Text, StringComparison.InvariantCultureIgnoreCase)
                                          select c;
                    foreach (var day in dayOfWeek)
                    {
                        if (DateTime.Now.DayOfWeek.ToString() == day)
                        {
                            found = true;
                        }
                    }
                    if (found)
                    {
                        if (DateTime.Now.Hour < 12)
                        {
                            foreach (var c in lstConsultation)
                             txtFrais.Text = c.FraisConventionnee.ToString();
                        }
                        else
                        {
                            txtFrais.Text = "3000";
                        }
                    }
                    else
                    {
                        txtFrais.Text = "3000";
                    }
                }
                else
                {
                    var lstConsultation = from c in ConnectionClassClinique.ListeDesTypesConsultations()
                        where c.TypeConsultation.StartsWith(cmbType.Text, StringComparison.InvariantCultureIgnoreCase)
                        select c;
                    foreach (var c in lstConsultation)
                    {
                        if (rdb2.Checked)
                        {
                            txtFrais.Text = c.FraisConventionnee.ToString();
                        }
                        else
                        {
                            txtFrais.Text = c.Frais.ToString();
                        }
                    }
                    //cmbMedecin.DroppedDown = true;
                    //cmbMedecin.Focus();
                }

              var   designation = cmbType.Text;
                var count = ConnectionClassClinique.CountDetailsActes(designation, DateTime.Now.Date, DateTime.Now.Date);
                count += ConnectionClassClinique.CountDetailsActesDansConventionnes(designation, DateTime.Now.Date, DateTime.Now.Date);
                button3.Text = count.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void cmbMedecin_Click(object sender, EventArgs e)
        {
            cmbMedecin.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        }
}
