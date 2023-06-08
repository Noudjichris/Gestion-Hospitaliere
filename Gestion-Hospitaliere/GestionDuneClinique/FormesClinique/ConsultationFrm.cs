using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using GestionDuneClinique.Formes;
using GestionDuneClinique;

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
            Pen pen1 = new Pen(Color.FromArgb(255, 128, 0), 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ConsultationFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkOrange, 3);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.FromArgb(255, 128, 0), Color.FromArgb(255, 128, 0), LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.FromArgb(255, 128, 0), Color.FromArgb(255, 128, 0), LinearGradientMode.ForwardDiagonal);
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
            textBox1.Text = "";
            cmbType.Text = "";
            dataGridView2.Rows.Clear();
            comboBox2.Text = "";
            cmbType.Text = "";
            cmbMedecin.Text = "";
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

                    if (consultation.TypeConsultation == "Consultation De  Specialité")
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
                        consultation.NomPatient.ToUpper(), consultation.Frais, consultation.IdPatient);
                    editColumn.Image = global::GestionDuneClinique.Properties.Resources.edit;
                    deleteColumn.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    foreach (DataGridViewRow dgrv in dgvConsult.Rows)
                    {
                        flag = AppCode.ConnectionClassClinique.SiFactureEnBon("CONSULTATION", Int32.Parse(dgrv.Cells[0].Value.ToString()));
                        if (flag)
                        {
                            dgrv.DefaultCellStyle.BackColor = Color.White;
                        }
                    }
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

                var specialite = new string[]
            {
                "Psychiatrie",
                "Cardiologie",
                "Chirurgie",
                "Chirurgie-orthopedie",
                "Gynécologie",
                "Pédiatrie",
                "Urologie",
                "Neurologie",
                "Anapathie",
                "Odonto-Stomatologie",
                "Ophtalmologie",
                "ORL", 
                "Traumatologie",
                "Pre-anesthesie",
                "Gastrologie",
                "Hematologie",
                "Endocrinologie",
                "Nephrologie",
                "Dermatologie",
                "Pneumologie"
            };
                rdb1.Checked = true;
                var items = from item in specialite
                            orderby item
                            select item;
                foreach (var item in items)

                    comboBox2.Items.Add(item);
                //var listeConsultation = ConnectionClassClinique.ListeDesConsultations();
                //ListeConsultation(listeConsultation);
             
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
                         age, patient.Sexe,
                            patient.NomEntreprise,
                            patient.Couvert,
                      patient.SousCouvert
                        );
                    }
                }
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "NRC" ||
                        dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "ASECNA" ||
                        dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "SHT" ||
                          dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "SATOM" ||
                                          dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "PAM" ||
                       dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper().Contains("OIM"))
                    {
                        rdb2.Checked = true;
                    }
                    else if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "CICR")
                    {
                        rdb4.Checked = true;
                    }
                    else if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper().Contains("MSF"))
                    {
                        rdb3.Checked = true;
                    }
                    else
                    {
                        rdb1.Checked = true;
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
                            if (cmbType.Text == "Consultation De  Specialité" && string.IsNullOrEmpty(comboBox2.Text))
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
                            var consultation = new Consultation(idConsul, typeConsult, dateConsult, rv, desc, numEmpl, idPatient, frais, specialite, "");
                            if(ConnectionClassClinique.Verification(idPatient,dateConsult,typeConsult,specialite))
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
                                        if (cmbType.Text == "Consultation De  Specialité")
                                        {
                                            typeConsultation = "CONSULTATION EN " + comboBox2.Text.ToUpper();
                                        }
                                        else
                                        {
                                            typeConsultation = cmbType.Text.ToUpper();
                                        }
                                        montant = Double.Parse(txtFrais.Text);
                                        patient = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                                        idConsultation = ConnectionClassClinique.ObtenirDerniereConsultation();
                                        btnClick = "1";
                                        Dispose();
                                    }
                                    else
                                    {
                                        btnBonSoin.Enabled = false;
                                        btnEnregistrer.Enabled = false;
                                        ViderLesChamps();
                                        idConsul = 0;
                                        DesactiverLesControles();
                                        btnEnregistrer.Enabled = false;
                                        btnAjouter.Enabled = true;
                                        var listeConsult = ConnectionClassClinique.ListeDesConsultations();
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
        private void button6_Click(object sender, EventArgs e)
        {
            var listeConsult = ConnectionClassClinique.ListeDesConsultations(dateTimePicker1.Value);
            ListeConsultation(listeConsult);
        }
        //modifier les donnees
        private void button4_Click(object sender, EventArgs e)
        {
            
        }

  
        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdb1.Checked == true)
            {
                if (cmbType.Text == "Consultation Generale")
                {
                    txtFrais.Text = "5000";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation Pre-Natale")
                {
                    txtFrais.Text = "5000";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation De  Specialité")
                {
                    txtFrais.Text = "10000";
                    comboBox2.Enabled = true;
                    comboBox2.Text = "";
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            else if (rdb2.Checked == true)
            {
                if (cmbType.Text == "Consultation Generale")
                {
                    txtFrais.Text = "15000";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation Pre-Natale")
                {
                    txtFrais.Text = "10000";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation De  Specialité")
                {
                    txtFrais.Text = "20000";
                    comboBox2.Enabled = true;
                    comboBox2.Text = "";
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            else if (rdb3.Checked == true)
            {
                if (cmbType.Text == "Consultation Generale")
                {
                    txtFrais.Text = "5000";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation Pre-Natale")
                {
                    txtFrais.Text = "5000";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation De  Specialité")
                {
                    txtFrais.Text = "10000";
                    comboBox2.Enabled = true;
                    comboBox2.Text = "";
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            else if (rdb4.Checked == true)
            {
                if (cmbType.Text == "Consultation Generale")
                {
                    txtFrais.Text = "7500";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation Pre-Natale")
                {
                    txtFrais.Text = "5000";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation De  Specialité")
                {
                    txtFrais.Text = "10000";
                    comboBox2.Enabled = true;
                    comboBox2.Text = "";
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }else  if (rd5.Checked == true)
            {
                if (cmbType.Text == "Consultation Generale")
                {
                    txtFrais.Text = "7500";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation Pre-Natale")
                {
                    txtFrais.Text = "5000";
                    comboBox2.Enabled = false;
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox2.Text = "";
                }
                else if (cmbType.Text == "Consultation De  Specialité")
                {
                    txtFrais.Text = "10000";
                    comboBox2.Enabled = true;
                    comboBox2.Text = "";
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void cmbMedecin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtFrais.Focus();
            //if (cmbMedecin.Text.ToUpper() == "PARTENAIRE EXTERNE")
            //{
            //    txtPartenaire.Visible = true;
            //}
            //else
            //{
            //    txtPartenaire.Visible = false;
            //}
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
                    bitmap = Impression.FactureConsultation(idConsul, dgvConsult, patient, GestionAcademique.LoginFrm.nom);
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
            e.Graphics.DrawImage(bitmap, -5, 20, bitmap.Width, bitmap.Height);
            e.HasMorePages = false;

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }
        int indexPrix=0;
        private void cmbEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from l in ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text)
                            where l.NomEntreprise.ToUpper().Equals(cmbEntreprise.Text.ToUpper())
                            select l.IndexPrix;
                foreach (var i in liste)
                    indexPrix = i;
                if (cmbEntreprise.Text.ToUpper() == "NRC" || cmbEntreprise.Text.Contains("OIM") ||
                   cmbEntreprise.Text.ToUpper() == "SHT" || cmbEntreprise.Text.ToUpper() == "PAM"
                   || cmbEntreprise.Text.ToUpper() == "ASECNA")
                {
                    rdb2.Checked = true;
                }
                else if (cmbEntreprise.Text.ToUpper().Contains("MSF"))
                {
                    rdb3.Checked = true;
                }
                else if (cmbEntreprise.Text.ToUpper().Contains("CICR"))
                {
                    rdb4.Checked = true;
                }
                else if (indexPrix == 4)
                {
                    rd5.Checked = true;
                }
                else
                {
                    rdb1.Checked = true;
                }
                txtType_SelectedIndexChanged(null, null);
                dataGridView2.Rows.Clear();
                var listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise("", cmbEntreprise.Text);
                foreach (var patient in listePatient)
                {
                    dataGridView2.Rows.Add(
                      patient.NumeroPatient.ToString(),
                      patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                      patient.An, patient.Sexe,
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
                if (cmbType.Text.ToUpper() == "Consultation De  Specialité".ToUpper())
                {
                    //txtFrais.Text = "20000";
                    comboBox2.Enabled = true;
                    //comboBox2.Text = "";
                    comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboBox2.DroppedDown = true;
                    comboBox2.Focus();

                }
                else
                {
                    cmbMedecin.Focus();
                    cmbMedecin.DroppedDown = true;
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

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEnregistrer_Click(null, null);
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
            //"Cardiologie",
            //     "Chirurgie",
            //     "Chirurgie-orthopedie",
            //     "Gynécologie",
            //     "Pédiatrie",
            //     "Odonto-Stomatologie",
            //     "Ophtalmologie",
            //     "ORL",
            //     "Urologie",
            //     "Neurologie",
            //     "Psychatrie",
            //     "Anapathie",
            //     "Traumatologie",
            //     "Pre-anesthesie",
            //     "Gastro-enterologie",
            //     "Hematologie","Endocrinologie",
            //     "Nephrologie"
            if (comboBox2.Text == "Cardiologie" || comboBox2.Text == "Gynécologie"
                || comboBox2.Text == "Pédiatrie" || comboBox2.Text =="Gastro-enterologie")
            {
                //txtFrais.Text = "5000";
            }
            else if (comboBox2.Text == "Nephrologie" || comboBox2.Text == "Endocrinologie")
            {
                //txtFrais.Text = "6000";
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
                cmbMedecin.Focus();
                cmbMedecin.DroppedDown = true;
            }
        }

        private void rdb2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvConsult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                try
                {
                    if (dgvConsult.SelectedRows.Count > 0)
                    {
                        //if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                        {
                            etat = "2";
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
                            txtFrais.Text = dgvConsult.SelectedRows[0].Cells[7].Value.ToString();
                            dtpConsult.Value = DateTime.Parse(dgvConsult.SelectedRows[0].Cells[2].Value.ToString());
                            cmbType.Text= dgvConsult.SelectedRows[0].Cells[1].Value.ToString();
                            //if (dgvConsult.SelectedRows[0].Cells[1].Value.ToString().Equals("Consultation De  Specialité"))
                            //{
                            //    cmbType.Text = "Consultation De  Specialité";
                            //    comboBox2.Text = dgvConsult.SelectedRows[0].Cells[1].Value.ToString();
                            //}
                            btnBonSoin.Enabled = true ;
                            btnEnregistrer.Enabled = true;
                        }
                    }
                }
                catch { }
            }
            else if (e.ColumnIndex == 10)
            {
                if (dgvConsult.SelectedRows.Count > 0)
                {
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
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
                            var listeConsult = ConnectionClassClinique.ListeDesConsultations();
                            ListeConsultation(listeConsult);
                        }
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            idConsultation = 0;
            btnClick = "0";
            Close();
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
                            if (cmbType.Text.ToUpper()== "Consultation De  Specialité" .ToUpper()&& string.IsNullOrEmpty(comboBox2.Text))
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
                                var consultation = new Consultation(idConsul, typeConsult, dateConsult, rv, desc, numEmpl, idPatient, frais, specialite, "");

                                //if (etat == "1")
                                {
                                    if (ConnectionClassClinique.BondeConsultation(consultation))
                                    {
                                        button1_Click(null, null);
                                        btnBonSoin.Enabled = false;
                                        btnEnregistrer.Enabled = false;
                                        ViderLesChamps();
                                        DesactiverLesControles();
                                        btnEnregistrer.Enabled = false;
                                        btnAjouter.Enabled = true;
                                        var listeConsult = ConnectionClassClinique.ListeDesConsultations();
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "NRC" ||
                    dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "ASECNA" ||
                    dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "SHT" ||
                      dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "SATOM" ||
                                      dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "PAM" ||
                   dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper().Contains("OIM"))
                {
                    rdb2.Checked = true;
                }
                else if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() == "CICR")
                {
                    rdb4.Checked = true;
                }
                else if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper().Contains("MSF"))
                {
                    rdb3.Checked = true;
                }
                else if (indexPrix == 4)
                {
                    rd5.Checked = true;
                }
                else
                {
                    rdb1.Checked = true;
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            var listeConsult = ConnectionClassClinique.ListeDesConsultations(dateTimePicker1.Value.Date);
            ListeConsultation(listeConsult); 
        }

        private void rd5_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
