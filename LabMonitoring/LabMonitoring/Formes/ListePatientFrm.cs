using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LabMonitoring.AppCode;

namespace GestionDuneClinique.FormesClinique
{
    public partial class ListePatientFrm : Form
    {
        public ListePatientFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        static ListePatientFrm listePatientFrm;
        public static string btnClick, patient;
        public static int id,idPatient;
        public static double fraisCarnet;
        void ListeDespatients(List<Patient> listePatient)
        {
            dgvPatient.Rows.Clear();

            foreach (Patient patient in listePatient)
            {

                dgvPatient.Rows.Add(
                    patient.IDPatient,
                    patient.NomPatient,
                    patient.Age
                    );
            }
        }

        private void ListePatient_Load(object sender, EventArgs e)
        {
            txtRechercher.Focus();
            //var listePatient = ConnectionClassClinique.ListeDesPatients();
            //ListeDespatients(listePatient);
        }

        private void txtRechercher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listePatient =from l in  ClassClinique.ListeDesPatients()
                                      where l.NomPatient.StartsWith(txtRechercher.Text, StringComparison.CurrentCultureIgnoreCase)
                                      select l;
                foreach (var l in listePatient)
                {
                    dgvPatient.Rows.Add(l.IDPatient, l.NomPatient, l.Age);
                }
                dgvPatient.Focus();
            }
        }

        private void dgvHosp_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvPatient.SelectedRows.Count > 0)
                {
                    btnClick = "1";
                    idPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    patient = dgvPatient.SelectedRows[0].Cells[1].Value.ToString();
                    listePatientFrm.Dispose();
                }
            }
            catch { }
        }


        public static string ShowBox()
        {
            try
            {
                listePatientFrm = new ListePatientFrm();
                listePatientFrm.ShowDialog();

            }
            catch { }
            return btnClick;
        }

        private void dgvPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvHosp_DoubleClick(null, null);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtRechercher_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvPatient.Rows.Clear();
                var listePatient = from p in ClassClinique.ListeDesPatients()
                                   where p.IDPatient == Convert.ToInt32(txtRechercher.Text)
                                   select p;

                if (listePatient.Count() > 0)
                {
                    foreach (Patient patient in listePatient)
                    {
                        dgvPatient.Rows.Add(
                         patient.IDPatient,
                         patient.NomPatient,patient.Age,patient
                         );
                    }
                }
                else { dgvPatient.Rows.Clear(); }

            }
            catch { }
        }

    }
}
