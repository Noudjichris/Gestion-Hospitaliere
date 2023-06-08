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
    public partial class RapportPatientFrm : Form
    {
        public RapportPatientFrm()
        {
            InitializeComponent();
        }

        private void RapportPatientFrm_Load(object sender, EventArgs e)
        {
            var listePatient = ConnectionClassClinique.ListeDesPatients();
            //ListeDespatients(listePatient);
        }

        void ListeDespatients(List<Patient> listePatient)
        {
            dgvPatient.Rows.Clear();

            foreach (Patient patient in listePatient)
            {

                //dgvPatient.Rows.Add(
                //    patient.NumeroPatient,
                //    patient.Nom.ToUpper(), patient.Prenom.ToUpper(),
                //    patient.Sexe, patient.Age,
                //    patient.Telephone,
                //    patient.Poids,
                //    patient.Tension,
                //    patient.Temperature,
                //    patient.NomEntreprise,
                //    patient.Rhesus
                //    );
            }
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkOrange, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox3.Width - 1, groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkOrange, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void RapportPatientFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DarkOrange, 0);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var listePatient = ConnectionClassClinique.ListeDesPatients(textBox1.Text);
            ListeDespatients(listePatient);
        }

        private void btnApercu_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPatient.SelectedRows.Count > 0)
                {
                    var idPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    var listePatient = from p in AppCode.ConnectionClassClinique.ListeDesPatients()
                                       where p.NumeroPatient == idPatient
                                       select p;

                    var patient = new AppCode.Patient();
                    foreach (var p in listePatient)
                        patient = p;
                    var telephone = string.Format("{0,-22} {1,-80}", "Téléphone :  ", patient.Telephone);

                    if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        printDocument1.PrinterSettings =printDialog1.PrinterSettings;
                        rapportPatient = Impression.RapportPatient("RAPPORTS DES ACTES DU PATIENT ", patient, dtp1.Value, dtp2.Value.AddHours(24));
                        printPreviewDialog1.ShowDialog();
                    }
                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture employe ", ex);
            }
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        Bitmap rapportPatient;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportPatient, -10, 10, rapportPatient.Width, rapportPatient.Height);
            e.HasMorePages = false;
        }


       
    }
}
