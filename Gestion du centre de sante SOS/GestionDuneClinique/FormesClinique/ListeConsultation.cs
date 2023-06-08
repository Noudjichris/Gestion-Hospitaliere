using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.Formes
{
    public partial class ListeConsultation : Form
    {
        public ListeConsultation()
        {
            InitializeComponent();
        }

        private void ListeConsultation_Load(object sender, EventArgs e)
        {
            var listeConsult = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesConsultations();
            ListeConsultations(listeConsult);
            txtRechercher.Focus();
        }

        static ListeConsultation ListConsultFrm;
        public static  string btnClick,patient,typeConsultation;
        public static int id, idPatient;
        public static double montant;
   
        void ListeConsultations(List<GestionDuneClinique.AppCode.Consultation> listeConsult)
        {
            try
            {
                dgvConsultation.Rows.Clear();
                var consulta = "";
                foreach (GestionDuneClinique.AppCode.Consultation consultation in listeConsult)
                {
                    if (consultation.TypeConsultation.ToUpper() == "CONSULTATION DE SPECIALITE")
                    {
                        consulta = "Consultation en " + consultation.Specialite;
                    }
                    else
                    {
                        consulta = consultation.TypeConsultation;
                    }
                    dgvConsultation.Rows.Add(consultation.NumeroConsultation, consulta.ToUpper(),
                        consultation.NomPatient.ToUpper(), consultation.NomEmploye.ToUpper(),
                        consultation.Frais, consultation.DateConsultation,consultation.IdPatient);
                    //Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                }
            }
            catch { }
        }

        private void dgvAnal_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvConsultation.SelectedRows.Count > 0)
                {
                    btnClick = "1";
                    id = Int32.Parse(dgvConsultation.SelectedRows[0].Cells[0].Value.ToString());
                    montant = Double.Parse(dgvConsultation.SelectedRows[0].Cells[4].Value.ToString());
                    patient = dgvConsultation.SelectedRows[0].Cells[2].Value.ToString();
                    typeConsultation = dgvConsultation.SelectedRows[0].Cells[1].Value.ToString();
                    idPatient = Int32.Parse(dgvConsultation.SelectedRows[0].Cells[6].Value.ToString());
                    ListConsultFrm.Dispose();
                }
            }
            catch (Exception)
            {
               
            }
        }

        public static string ShowBox()
        {
            try
            {
                ListConsultFrm = new ListeConsultation();
                ListConsultFrm.ShowDialog();
                
            }
            catch { }
            return btnClick;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListeConsultation_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.DodgerBlue,Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);

        }

        private void txtRechercher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listeConsult = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesConsultations(txtRechercher.Text);
                ListeConsultations(listeConsult);
                dgvConsultation.Focus();
            }
        }

        private void dgvConsult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvAnal_DoubleClick(null, null);
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dgvConsultation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvConsultation_DoubleClick(object sender, EventArgs e)
        {
            dgvAnal_DoubleClick(null, null);
        }

        private void dgvConsultation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dgvAnal_DoubleClick(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try {
                var listeConsult = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesConsultationsParIdPatient(Int32.Parse(textBox1.Text));
                ListeConsultations(listeConsult);
                //dgvConsultation.Focus();
            }
            catch { }
        }

    }
}
