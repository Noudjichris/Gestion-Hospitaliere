using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using GestionPharmacetique.AppCode;

namespace GestionDuneClinique.FormesClinique
{
    public partial class ListePersFrm : Form
    {
        public ListePersFrm()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            flag = false;
            Dispose();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListePersFrm_Load(object sender, EventArgs e)
        {
            ListePersonnel();
        }

        void ListePersonnel()
        {
            try
            {
                dgvPatient.Rows.Clear();
                List<Employe> listeEmployes = ConnectionClassClinique.ListeDesEmployees(txtLibelle.Text);
                var list = from l in listeEmployes
                           where l.NumeroService==Convert.ToInt32(ConnectionClassClinique.ListeService(service).Rows[0].ItemArray[0].ToString())
                           where !l.NomEmployee.Contains("EXTERNE")
                           select l;
                foreach (var employe in list)
                {
                    dgvPatient.Rows.Add(
                        employe.NumMatricule,
                        employe.NomEmployee.ToUpper(),
                        employe.Addresse.ToUpper(),
                        employe.Telephone1,
                        employe.Telephone2,
                        employe.Email,
                        employe.Titre.ToUpper()
                    );
                }
            }
            catch (Exception exce)
            {
                MonMessageBox.ShowBox("Liste employe", exce)
                    ;
            }
        }

        private void txtLibelle_TextChanged(object sender, EventArgs e)
        {
            ListePersonnel();
        }

        public static  ListePersFrm frm;
        public static Employe employe;
        private static bool flag;
        public static string service;
        public static bool ShowBox()
        {
            try
            {

                frm = new ListePersFrm();
                frm.ShowDialog();
            }
            catch
            {
            }
            return flag;
        }

        private void dgvPatient_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvPatient.SelectedRows.Count > 0)
                {

                    var listeEmpl =
                        ConnectionClassClinique.ListeDesEmployees(dgvPatient.SelectedRows[0].Cells[1].Value.ToString());
                    foreach (var empl in listeEmpl)
                        employe = empl;
                    flag = true;
                    Dispose();
                }
            }
            catch { }
        }
    }
}
