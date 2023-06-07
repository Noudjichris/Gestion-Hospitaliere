using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.Formes
{
    public partial class HospitalisationFrm : Form
    {
        public HospitalisationFrm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void HospitalisationFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 1);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        void ViderLesChamps()
        {
            dataGridView2.Rows.Clear();
            textBox1.Text = "";
            txtFrais.Text = "";
            txtTotal.Text = "";
            cmbSalleLit.Text = "";
            textBox2.Text = "";
            cmbSalleLit.Text = "";
            dtpEntree.Value = DateTime.Now;
            dtpSortie.Value = DateTime.Now;
        }
                string etat;

        static HospitalisationFrm ListHosFrm;
        public static string btnClick, patiente, state = "0";
        public static int idHospt, idPatiente, nbre;
        public static double montant;
        int idPatient, noOccup;
        public static string ShowBox()
        {
            try
            {
                ListHosFrm = new HospitalisationFrm();
                ListHosFrm.ShowDialog();

            }
            catch { }
            return btnClick;
        }

        //enregistrer
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            double prix, prixTotal;
            if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
            { return; }
            if (!string.IsNullOrEmpty
                (cmbSalleLit.Text))
            {
                  if (Double.TryParse(txtFrais.Text, out prix) && double.TryParse(txtTotal.Text, out prixTotal))
                    {
                        if (dataGridView2.SelectedRows.Count > 0)
                        {
                            idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());

                            var occupation = new Occupation();
                            var liste = from p in ConnectionClassClinique.ListeSalles()
                                        where p.SalleLit.ToUpper() == cmbSalleLit.Text.ToUpper()
                                        select p;
                            foreach (var p in liste)
                                occupation.NoSalle = p.NoSalle;
                            occupation.DateEntree = dtpEntree.Value;
                            occupation.DateSortie = dtpSortie.Value;
                            occupation.IdPatient = idPatient;
                            occupation.Prix = prix;
                            occupation.PrixTotal = prixTotal;
                            occupation.NombreJour = Convert.ToInt32(textBox2.Text);
                            if (etat == "1")
                            {
                                if (ConnectionClassClinique.EnregistrerUneOccupation(occupation, etat))
                                {
                                    if (state == "1")
                                    {
                                        //montant = total;
                                        idPatiente = idPatient;
                                        patiente = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                                        var nbreJour = occupation.DateSortie.Date.Subtract(occupation.DateEntree.Date);
                                        nbre = nbreJour.Days;
                                        montant = prixTotal;
                                        ViderLesChamps();
                                        btnClick = "1";
                                        Dispose();
                                    }
                                    else
                                    {
                                        ViderLesChamps();
                                        var listeOccup = ConnectionClassClinique.ListeDesOccupations();
                                        ListeHospitalisation(listeOccup);
                                    }
                                }
                            }
                            else if (etat == "3")
                            {
                                if (MonMessageBox.ShowBox("Voulez vous supprimer les donnees d' hospitalisation?", "Confirmation", "confirmation.png") == "1")
                                {
                                    ConnectionClassClinique.SupprimerUneOccupation(noOccup);
                                    ViderLesChamps();
                                    btnEnregistrer.Enabled = false;
                                    var listeOccup = ConnectionClassClinique.ListeDesOccupations();
                                    ListeHospitalisation(listeOccup);
                                }
                            }
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer un valide valide pour le frais de la salle et le nombre de jour .", "Erreur", "erreur.png");
                    }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le numero de la salle.", "Erreur", "erreur.png");
            }
        }
        void ListeSalle()
        {
            try
            {
                cmbSalleLit.Items.Clear();
                var liste = ConnectionClassClinique.ListeSalles();
                foreach (var s in liste)
                {
                    cmbSalleLit.Items.Add(s.SalleLit.ToUpper());
                }
            }
            catch { }
        }
        private void HospitalisationFrm_Load(object sender, EventArgs e)
        {
            try
            {
                Size = new Size(Width, GestionAcademique.Form1.height);
                etat = "1";
                Height = GestionAcademique.Form1.height;
                var listeOccup = ConnectionClassClinique.ListeDesOccupations();
                ListeHospitalisation(listeOccup);
                ListeSalle();
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
                cmbEntreprise.Items.Add("");
                foreach (Entreprise entreprise in listeEntrep)
                {
                    cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
                }
                ListeSalle();
            
            }
            catch { }
        }

        void calculerMontantTotal()
        {
            try
            {
                double prixTotal;
                int nbreJour = 1;
                double prixSalle;
                nbreJour =  (dtpSortie.Value.DayOfYear - dtpEntree.Value.DayOfYear) + 
                    (dtpSortie.Value.Year - dtpEntree.Value.Year)*365;
                if (dtpEntree.Value.Date == dtpSortie.Value.Date)
                {
                    nbreJour = 1;
                }
                if (Double.TryParse(txtFrais.Text, out prixSalle))
                {
                    prixTotal = prixSalle * nbreJour;
                    txtTotal.Text = prixTotal.ToString();
                    textBox2.Text = nbreJour.ToString();
                }
                else
                {
                    txtTotal.Text = "";
                }

            }
            catch (Exception) { }
        }

        void ListeHospitalisation(List<Occupation> listeOccup)
        {
            try
            {
                dgvHosp.Rows.Clear();
                foreach (Occupation occupation in listeOccup)
                {
                    dgvHosp.Rows.Add(
                        occupation.NumeroOccupation,
                        occupation.IdPatient,
                        occupation.Patient.ToUpper(),
                        occupation.DateEntree.ToShortDateString(),
                        occupation.DateSortie.ToShortDateString(),
                        occupation.NoSalle,
                        occupation.SalleLit,
                        occupation.Prix,
                        occupation.NombreJour,
                        occupation.PrixTotal
                        );
                }
            }
            catch (Exception ex )
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void txtFrais_TextChanged(object sender, EventArgs e)
        {
            calculerMontantTotal();
        }

        private void dtpEntree_ValueChanged(object sender, EventArgs e)
        {
            calculerMontantTotal();
        }

        private void dtpSortie_ValueChanged(object sender, EventArgs e)
        {
            calculerMontantTotal();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var listeOccup = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesOccupations(dateTimePicker1.Value.Date);
            ListeHospitalisation(listeOccup);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void dgvHosp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (dgvHosp.SelectedRows.Count > 0)
                {
                    cmbSalleLit.Text = dgvHosp.SelectedRows[0].Cells[6].Value.ToString();
                    txtTotal.Text = dgvHosp.SelectedRows[0].Cells[7].Value.ToString();
                    dtpEntree.Value = DateTime.Parse(dgvHosp.SelectedRows[0].Cells[4].Value.ToString());
                    dtpSortie.Value = DateTime.Parse(dgvHosp.SelectedRows[0].Cells[5].Value.ToString());
                    idPatient = Int32.Parse(dgvHosp.SelectedRows[0].Cells[1].Value.ToString());
                    noOccup = Int32.Parse(dgvHosp.SelectedRows[0].Cells[0].Value.ToString());
                    etat = "2";
                    btnEnregistrer.Enabled = true;
                }
                else if (e.ColumnIndex == 5)
                {
                    if (dgvHosp.SelectedRows.Count > 0)
                    {
                        cmbSalleLit.Text = dgvHosp.SelectedRows[0].Cells[6].Value.ToString();
                        txtTotal.Text = dgvHosp.SelectedRows[0].Cells[7].Value.ToString();
                        txtFrais.Text = dgvHosp.SelectedRows[0].Cells[7].Value.ToString();
                        dtpEntree.Value = DateTime.Parse(dgvHosp.SelectedRows[0].Cells[4].Value.ToString());
                        dtpSortie.Value = DateTime.Parse(dgvHosp.SelectedRows[0].Cells[5].Value.ToString());
                        idPatient = Int32.Parse(dgvHosp.SelectedRows[0].Cells[1].Value.ToString());
                        noOccup = Int32.Parse(dgvHosp.SelectedRows[0].Cells[0].Value.ToString());
                        etat = "3";
                        btnEnregistrer.Enabled = true;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PatientFrm.btnClick = "1";
                if (PatientFrm.ShowBox() == "1")
                {

                    textBox1.Text = PatientFrm.numeroPatient > 0 ? PatientFrm.numeroPatient.ToString() : "";
                    btnEnregistrer.Focus();
                }
            }
            catch { }
        }

        private void cmbEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise("", cmbEntreprise.Text);
                foreach (var patient in listePatient)
                {
                    dataGridView2.Rows.Add(
                      patient.NumeroPatient.ToString(),
                      patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                      patient.An, patient.Sexe,
                      patient.NomEntreprise
                  );
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                PatientFrm.btnClick = "1";
                if (PatientFrm.ShowBox() == "1")
                {

                    textBox1.Text = PatientFrm.numeroPatient > 0 ? PatientFrm.numeroPatient.ToString() : "";
                    btnEnregistrer.Focus();
                }
            }
            catch { }

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
            try
            {
                int idPatient;
                dataGridView2.Rows.Clear();
                var listePatient = new List<Patient>();
                if (Int32.TryParse(textBox1.Text, out idPatient))
                {
                    listePatient = ConnectionClassClinique.ListeDesPatients(idPatient);
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
                        dataGridView2.Rows.Add(
                            patient.NumeroPatient.ToString(),
                            patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                            patient.An, patient.Sexe,
                            patient.NomEntreprise
                        );
                    }
                }
            }
            catch { }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (GestionDesPelerinsTchad.Formes.SalleFrm.ShowBox())
            {
                ListeSalle();
            }
        }

        private void cmbSalleLit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var  frais = from f in ConnectionClassClinique.ListeSalles()
                           where f.SalleLit.ToUpper() == cmbSalleLit.Text.ToUpper()
                           select f.Prix;
            foreach (var f in frais)
                txtFrais.Text = f.ToString();
            calculerMontantTotal();
        }

        private void dtpEntree_ValueChanged_1(object sender, EventArgs e)
        {
            calculerMontantTotal();
        }


    }
}
