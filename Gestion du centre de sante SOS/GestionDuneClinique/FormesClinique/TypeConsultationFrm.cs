using System;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.FormesClinique
{
    public partial class TypeConsultationFrm : Form
    {
        public TypeConsultationFrm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtTypeConsultation.Enabled = false;
                txtSpecialite.Enabled = true;
                txtTypeConsultation.Text = "CONSULTATION DE SPECIALITE";
            }
            else
            {
                txtTypeConsultation.Enabled = true ;
                txtTypeConsultation.Text = "";
                txtSpecialite.Enabled = false;
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            double frais, fraisCon;
            if (!string.IsNullOrEmpty(txtTypeConsultation.Text))
            {
                if (double.TryParse(txtFrais.Text, out frais) && double.TryParse(txtFraisConventionnee.Text, out fraisCon))
                {
                    if (checkBox1.Checked && string.IsNullOrEmpty(txtSpecialite.Text))
                    {
                        MonMessageBox.ShowBox("Veuillez entrer la specialité de la consultation", "Erreur",
                            "erreur.png");
                        return;
                    }
                    var consultation = new Consultation();
                    consultation.Frais = frais;
                    consultation.TypeConsultation = txtTypeConsultation.Text;
                    consultation.Specialite = txtSpecialite.Text;
                    consultation.Description = textBox1.Text;
                    consultation.NumeroConsultation = idConsult;
                    consultation.FraisConventionnee = fraisCon;
                    if (ConnectionClassClinique.EnregistrerUnTypeConsultation(consultation))
                    {
                        if(!checkBox1.Checked)
                        txtTypeConsultation.Text = "";
                        txtSpecialite.Text = "";
                        txtFrais.Text = "";
                        textBox1.Text = "";
                        ListeTypeConsultation();
                    }

                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le frais de la consultation",
                        "Erreur", "erreur.png");
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez entrer le type de consultation", "Erreur", "erreur.png");
            }
        }

        void ListeTypeConsultation()
        {
            dgvConsult.Rows.Clear();
            foreach (var cons in ConnectionClassClinique.ListeDesTypesConsultations())
            {
                dgvConsult.Rows.Add(
                    cons.NumeroConsultation,
                    cons.TypeConsultation.ToUpper(),
                    cons.Specialite.ToUpper(),
                    cons.Description,
                    cons.Frais,cons.FraisConventionnee);
                editButton.Image =Properties.Resources.edit;
                deleteButton.Image = Properties.Resources.deleteButton;
            }
        }

        private void TypeConsultationFrm_Load(object sender, EventArgs e)
        {
            ListeTypeConsultation();
        }

        private int idConsult;
        private void dgvConsult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (dgvConsult.SelectedRows.Count > 0)
                {
                    if (dgvConsult.SelectedRows[0].Cells[1].Value.ToString() == "CONSULTATION DE SPECIALITE")
                    {
                        checkBox1.Checked = true;
                        checkBox1_CheckedChanged(null, null);
                    }
                    else
                    {   checkBox1.Checked = false;
                        checkBox1_CheckedChanged(null, null);
                    }
                    txtTypeConsultation.Text = dgvConsult.SelectedRows[0].Cells[1].Value.ToString();
                    txtFrais.Text = dgvConsult.SelectedRows[0].Cells[4].Value.ToString();
                    txtFraisConventionnee.Text = dgvConsult.SelectedRows[0].Cells[5].Value.ToString();
                    textBox1.Text = dgvConsult.SelectedRows[0].Cells[3].Value.ToString();
                    txtSpecialite.Text = dgvConsult.SelectedRows[0].Cells[2].Value.ToString();
                    idConsult = Convert.ToInt32(dgvConsult.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            else if (e.ColumnIndex == 7)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation", "confirmation.png") ==
                    "1")
                {
                   ConnectionClassClinique. SupprimerUnTypeConsultation(Int32.Parse(dgvConsult.SelectedRows[0].Cells[0].Value.ToString()));
                    dgvConsult.Rows.Remove(dgvConsult.SelectedRows[0]);
                }
            }
        }

        public bool SupprimerUnTypeConsultation { get; set; }
    }
}
