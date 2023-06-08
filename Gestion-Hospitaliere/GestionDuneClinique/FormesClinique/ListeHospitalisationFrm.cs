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
    public partial class ListeHospitalisationFrm : Form
    {
        public ListeHospitalisationFrm()
        {
            InitializeComponent();
        }

        private void ListeHospitalisationFrm_Load(object sender, EventArgs e)
        {
            var listeOccup = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesOccupations();
            ListeHospitalisation(listeOccup);
        }

        void ListeHospitalisation(List<GestionDuneClinique.AppCode.Occupation> listeOccup)
        {
            dgvHosp.Rows.Clear();
            foreach (GestionDuneClinique.AppCode.Occupation occupation in listeOccup)
            {
                dgvHosp.Rows.Add(
                    occupation.NumeroOccupation,
                    occupation.Patient.ToUpper(),
                    occupation.NoSalle,
                    occupation.SalleLit,
                    occupation.DateEntree,
                    occupation.DateSortie,                    
                    occupation.PrixTotal,
                    occupation.IdPatient);
            }
        }
      
        static ListeHospitalisationFrm ListHosFrm;
       public  static string btnClick, patient;
        public static int id,idPatient, nbre;
        public static double montant;

        public static string ShowBox()
        {
            try
            {
                ListHosFrm = new ListeHospitalisationFrm();
                ListHosFrm.ShowDialog();

            }
            catch { }
            return btnClick;
        }

        private void dgvHosp_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvHosp.SelectedRows.Count > 0)
                {
                    btnClick = "1";
                    id = Int32.Parse(dgvHosp.SelectedRows[0].Cells[0].Value.ToString());
                    montant = Double.Parse(dgvHosp.SelectedRows[0].Cells[6].Value.ToString());
                    patient = dgvHosp.SelectedRows[0].Cells[1].Value.ToString();
                    idPatient = Int32.Parse(dgvHosp.SelectedRows[0].Cells[7].Value.ToString());
                    var dateSortie = DateTime.Parse(dgvHosp.SelectedRows[0].Cells[5].Value.ToString());
                    var dateDentre = DateTime.Parse(dgvHosp.SelectedRows[0].Cells[4].Value.ToString());
                    var nbreDate = dateSortie.Subtract(dateDentre);
                    nbre = nbreDate.Days;
                    ListHosFrm.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }

       private void button7_Click(object sender, EventArgs e)
        {
            Close();
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

        private void ListeHospitalisationFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var listeOccup = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesOccupations(textBox2.Text);
                ListeHospitalisation(listeOccup);
                dgvHosp.Focus();
            }
        }

        private void dgvHosp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvHosp_DoubleClick(null, null);
            }
        }

        
    }
}
