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
    public partial class ObservationFrm : Form
    {
        public ObservationFrm()
        {
            InitializeComponent();
        }

        static ObservationFrm ListObservation;
        public static string btnClick, patiente, libelle, state = "0";
        public static int idHospt, numeroPatient, nbreJour;
        public static double montant, fraisObservation;
        int  noOccup;
        public static string ShowBox()
        {
            try
            {
                ListObservation = new ObservationFrm();
                ListObservation.ShowDialog();

            }
            catch { }
            return btnClick;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.DodgerBlue, 0);
            var area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 0);
            var area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void ObeservationFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.RowTemplate.Height = 25;
                //dataGridView1.Rows.Add("1","1","wwwwww",
                Column8.Width = dataGridView3.Width / 3;
                Column4.Width = 50;
                button7.Location = new Point(Width - 50, 3);
                ListeObservation();
                ListeOberservationFaite();
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
                cmbEntreprise.Items.Add("");
                foreach (Entreprise entreprise in listeEntrep)
                {
                    cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtRechercher_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listePatient = new List<Patient>();
                if (Int32.TryParse(txtRechercher.Text, out idPatient))
                {
                    listePatient = ConnectionClassClinique.ListeDesPatients(idPatient);
                }
                else
                {
                    if (txtRechercher.Text.Length > 2)
                    {
                        listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise(txtRechercher.Text, cmbEntreprise.Text);
                    }
                } if (listePatient.Count() > 0)
                {
                    dataGridView2.Rows.Clear();
                    foreach (var patient in listePatient)
                    {
                        var age =Impression. AfficherAge(patient.An, patient.Mois);
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
            }
            catch { }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var observation = new Observation();
                int id = 0; double frais;
                
                if (Int32.TryParse(dataGridView1.CurrentRow.Cells[0].Value.ToString(), out id))
                {
                }
                else
                {
                    id = 0;
                }

                if (Double.TryParse(dataGridView1.CurrentRow.Cells[2].Value.ToString(), out frais ))
                {
                    observation.Observations = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    observation.Frais = frais;
                    observation.IDObservation = id;
                    if(ConnectionClassClinique.EnregistrerUneObservation(observation))
                    {
                        ListeObservation();
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le frais de l'observation", "Erreur", "erreur.png");
                }
            }
            catch { }
        }
        void ListeObservation()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = ConnectionClassClinique.ListeDesObservations();
                foreach (var ob in liste)
                {
                    dataGridView1.Rows.Add(ob.IDObservation, ob.Observations, ob.Frais);
                }
            }
            catch { }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    var id = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    ConnectionClassClinique.SupprimerUneObservation(id);
                    ListeObservation();
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var observation = new Observation();
                
                    observation.Observations = "OBSERVATION";
                    observation.Frais = 0;
                    if (ConnectionClassClinique.EnregistrerUneObservation(observation))
                    {
                        ListeObservation();
                    }
                
            }
            catch { }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                lblObservation.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                lblFrais.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

                idObservation = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                lblNbr_TextChanged(null, null);
            }
        }

        int idObservation, id,idPatient;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double frais, total, nbre;
                if (!string.IsNullOrEmpty(lblObservation.Text))
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        var observation = new Observation();
                        if (Double.TryParse(lblJr.Text, out nbre) &&
                            Double.TryParse(lblFrais.Text, out frais) &&
                            Double.TryParse(lblTotal.Text, out total))
                        {
                            idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                            observation.Nombre = nbre;
                            observation.Total = total;
                            observation.Frais = frais;
                            observation.DateDebut = dateTimePicker1.Value;
                            observation.IDObservation = idObservation;
                            observation.IDPatient = idPatient;
                            observation.ID = id;
                            if (ConnectionClassClinique.EnregistrerUneObservationFaite(observation))
                            {
                                if (state == "1")
                                {
                                    libelle = lblObservation.Text;
                                    numeroPatient = idPatient;
                                    patiente = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                                    fraisObservation = double.Parse(lblFrais.Text);
                                    nbreJour = Int32.Parse(lblJr.Text);
                                    montant = double.Parse(lblTotal.Text);
                                    btnClick = "1";
                                    Dispose();
                                }
                                else
                                {
                                    lblFrais.Text = "";
                                    lblJr.Text = "";
                                    lblObservation.Text = "";
                                    lblTotal.Text = "";
                                    dataGridView2.Rows.Clear();
                                    ListeOberservationFaite();
                                    dateTimePicker1.Value = DateTime.Now;
                                    id = 0;
                                }
                            }
                        }
                        else
                        {
                            lblJr.BackColor = Color.Red;
                        }

                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner le nom du patient", "Erreur", "erreur.png");
                    }
                }

            }
            catch { }
        }

        void ListeOberservationFaite()
        {
            try
            {
                dataGridView3.Rows.Clear();
                var liste = from o in ConnectionClassClinique.ListeDesObservationsFaite()
                            join p in ConnectionClassClinique.ListeDesPatients()
                            on o.IDPatient equals p.NumeroPatient
                            where o.DateDebut >=DateTime.Parse("01/01/"+DateTime.Now.Year )
                            where o.DateDebut < DateTime.Parse("31/12/" + DateTime.Now.Year)
                            orderby o.ID descending
                            select new
                            {
                                o.IDPatient,o.IDObservation,o.ID,o.Nombre,o.Observations,
                                o.Total,o.Frais,o.DateDebut,p.Prenom,p.Nom
                            };
                foreach (var o in liste)
                {
                    dataGridView3.Rows.Add(
                        o.ID, o.IDPatient,
                        o.Nom + " " + o.Prenom, o.IDObservation, o.Observations,
                        o.DateDebut.ToShortDateString(), o.Nombre, o.Frais, o.Total
                        );
                    Column9.Image = global::GestionDuneClinique.Properties.Resources.edit;
                    Column10.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
                foreach (DataGridViewRow dgrv in dataGridView3.Rows)
                {
                   var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("OBSERVATION", Int32.Parse(dgrv.Cells[0].Value.ToString()));
                    if (flag)
                    {
                        dgrv.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void lblNbr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblJr.BackColor = Color.White;
                double frais, nbreJour;
                if (double.TryParse(lblFrais.Text, out frais) && double.TryParse(lblJr.Text, out nbreJour))
                {
             
                    //var nbreJour = dateTimePicker2.Value.DayOfYear - dateTimePicker1.Value.DayOfYear;
                    //var year = dateTimePicker2.Value.Year - dateTimePicker1.Value.Year;
                    //if (nbreJour < 0 && year > 0)
                    //{
                    //    nbreJour = nbreJour + 365 * year;
                    //}
                    //else if (nbreJour < 0 && year == 0)
                    //{
                    //    nbreJour = 0;
                    //}
                    //else if (nbreJour > 0 && year > 0)
                    //{
                    //    nbreJour = 365 * year + nbreJour;
                    //}
                    //else if (nbreJour == 0 && year == 0)
                    //{
                    //    nbreJour = 1;
                    //}
                    var total = nbreJour * frais;
                    lblTotal.Text = total.ToString();
                }
                else
                {
                    lblTotal.Text = "";
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblFrais.Text = "";
            lblJr.Text = "";
            lblObservation.Text = "";
            lblTotal.Text = "";
            dataGridView2.Rows.Clear();
            ListeOberservationFaite();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    dataGridView3.Rows.Clear();
                    var liste = from o in ConnectionClassClinique.ListeDesObservationsFaite()
                                join p in ConnectionClassClinique.ListeDesPatients()
                                on o.IDPatient equals p.NumeroPatient
                                where o.IDPatient == idPatient
                                select new
                                {
                                    o.IDPatient,
                                    o.IDObservation,
                                    o.ID,
                                    o.Nombre,
                                    o.Observations,
                                    o.Total,
                                    o.Frais,
                                    o.DateDebut,
                                    p.Prenom,
                                    p.Nom
                                };
                    foreach (var o in liste)
                    {
                        dataGridView3.Rows.Add(
                            o.ID, o.IDPatient,
                            o.Nom + " " + o.Prenom, o.IDObservation, o.Observations,
                            o.DateDebut.ToShortDateString(), o.Nombre, o.Frais, o.Total
                            );
                    }
                    foreach (DataGridViewRow dgrv in dataGridView3.Rows)
                    {
                       var  flag = AppCode.ConnectionClassClinique.SiFactureEnBon("OBSERVATION", Int32.Parse(dgrv.Cells[0].Value.ToString()));
                        if (flag)
                        {
                            dgrv.DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
            }
            catch { }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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
                      patient.NomEntreprise,
                      patient.Couvert,
                      patient.SousCouvert
                  );
                }
              
            }
            catch { }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {if (dataGridView3.SelectedRows.Count > 0)
            {
                lblTotal.Text = dataGridView3.SelectedRows[0].Cells[8].Value.ToString();
                lblFrais.Text = dataGridView3.SelectedRows[0].Cells[7].Value.ToString();
                lblJr.Text = dataGridView3.SelectedRows[0].Cells[6].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView3.SelectedRows[0].Cells[5].Value.ToString());
                lblObservation.Text = dataGridView3.SelectedRows[0].Cells[4].Value.ToString();
                txtRechercher.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString();
                id = Int32.Parse(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                idObservation = Int32.Parse(dataGridView3.SelectedRows[0].Cells[3].Value.ToString());
                idPatient = Int32.Parse(dataGridView3.SelectedRows[0].Cells[1].Value.ToString());
                txtRechercher.Text = idPatient.ToString();
                btnBonSoin.Enabled = true;
                
            }
            }
            else if (e.ColumnIndex == 10)
            {
                if (dataGridView3.SelectedRows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données ?", "Confirmation", "confirmation.png") == "1")
                    {
                        var id = Int32.Parse(dataGridView3.SelectedRows[0].Cells[0].Value.ToString());
                        AppCode.ConnectionClassClinique.SupprimerUneObservationFaite(id);
                        ListeOberservationFaite();
                    }
                }
            }
        }

        private void btnBonSoin_Click(object sender, EventArgs e)
        {
            try
            {
                double frais, total, nbre;
                if (!string.IsNullOrEmpty(lblObservation.Text))
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {  if (!string.IsNullOrEmpty(dataGridView2.SelectedRows[0].Cells[4].Value.ToString()) &&
                                bool.Parse(dataGridView2.SelectedRows[0].Cells[5].Value.ToString()))
                            {
                        var observation = new Observation();
                        if (Double.TryParse(lblJr.Text, out nbre) &&
                            Double.TryParse(lblFrais.Text, out frais) &&
                            Double.TryParse(lblTotal.Text, out total))
                        {
                            idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                            observation.Nombre = nbre;
                            observation.Total = total;
                            observation.Frais = frais;
                            observation.Observations = lblObservation.Text;
                            observation.DateDebut = dateTimePicker1.Value;
                            observation.IDObservation = id;
                            observation.IDPatient = idPatient;
                            observation.ID = id;
                            if(id>0)
                            if (ConnectionClassClinique.BondeObservation(observation))
                            {
                                MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "Affirmation.png");
                                btnBonSoin.Enabled = false;
                                lblFrais.Text = "";
                                lblJr.Text = "";
                                lblObservation.Text = "";
                                lblTotal.Text = "";
                                dataGridView2.Rows.Clear();
                                ListeOberservationFaite();
                                id = 0;
                            }
                        }
                        else
                        {
                            lblJr.BackColor = Color.Red;
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
                        MonMessageBox.ShowBox("Veuillez selectionner le nom du patient", "Erreur", "erreur.png");
                    }
                }

            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int idPat;
                GestionDuneClinique.Formes.PatientFrm.btnClick = "1";
                if (Int32.TryParse(txtRechercher.Text, out idPat))
                    GestionDuneClinique.Formes.PatientFrm.numeroPatient = idPat;

                GestionDuneClinique.Formes.PatientFrm.btnClick = "1";
                if (GestionDuneClinique.Formes.PatientFrm.ShowBox() == "1")
                {
                    txtRechercher.Text = GestionDuneClinique.Formes.PatientFrm.numeroPatient > 0 ? GestionDuneClinique.Formes.PatientFrm.numeroPatient.ToString() : "";
                    txtRechercher_TextChanged(null, null);
                    txtRechercher.Focus();
                }
            }
            catch { }
        }

    }
}
