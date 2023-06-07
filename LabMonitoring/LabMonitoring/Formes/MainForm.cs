using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using LabMonitoring.AppCode;

namespace LabMonitoring.Formes
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Blue, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.White,
                Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void txtGroupeExam_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvGroupe.Rows.Clear();
                var liste = from lr in ConnectionClass.ListeResultatPatient()
                            join lp in ConnectionClass.ListeDesPatients()
                            on lr.IDPatient equals lp.IDPatient
                            where lp.NomPatient.StartsWith(txtGroupeExam.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby lr.DateResultat descending
                            select new
                            {
                                lp.NomPatient,
                                lp.IDPatient,
                                lr.NumeroResultat,
                                lr.DateResultat,
                                lr.Service,
                                lr.Lit,
                                lr.Prescripteur
                            };
                foreach (var l in liste)
                {
                    dgvGroupe.Rows.Add(l.NumeroResultat, l.IDPatient, l.NomPatient, l.DateResultat, l.Service, l.Lit, l.Prescripteur);
                }
            }
            catch (Exception )
            {
            }
        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                dgvGroupe.Rows.Clear();
                var liste = from lr in ConnectionClass.ListeResultatPatient()
                            join lp in ConnectionClass.ListeDesPatients()
                            on lr.IDPatient equals lp.IDPatient
                            where lr.DateResultat >= DateTime.Now.Date.AddDays(-30)

                            orderby lr.DateResultat descending
                            select new
                            {
                                lp.NomPatient,
                                lp.IDPatient,
                                lr.NumeroResultat,
                                lr.DateResultat,
                                lr.Service,
                                lr.Lit,
                                lr.Prescripteur
                            };
                foreach (var l in liste)
                {
                    dgvGroupe.Rows.Add(l.NumeroResultat, l.IDPatient, l.NomPatient, l.DateResultat,l.Service,l.Lit,l.Prescripteur);
                }
            }
            catch (Exception)
            {
            }
           Height= height;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            btnClick = "2";
            frm.Dispose();
        }

        static string btnClick;
        public static string prescripteur, service, nomPatient, lit;
        public static DateTime dateResultat;
        public static int numeroResultat, IDPatient, height;
        public static MainForm frm;
        public static string ShowBox()
        {
            try
            {
                frm = new MainForm();
                frm.ShowDialog();
            }
            catch { }
            return btnClick;
        }

        private void dgvGroupe_DoubleClick(object sender, EventArgs e)
        {
            try 
	{	        
		if (dgvGroupe.SelectedRows.Count > 0)
            {
                nomPatient = dgvGroupe.SelectedRows[0].Cells[2].Value.ToString();
            IDPatient = Convert.ToInt32(dgvGroupe.SelectedRows[0].Cells[1].Value.ToString());
            numeroResultat = Convert.ToInt32(dgvGroupe.SelectedRows[0].Cells[0].Value.ToString());
            dateResultat=Convert.ToDateTime(dgvGroupe.SelectedRows[0].Cells[3].Value.ToString());
                    service  = dgvGroupe.SelectedRows[0].Cells[4].Value.ToString();
                    prescripteur = dgvGroupe.SelectedRows[0].Cells[6].Value.ToString();
                    lit  = dgvGroupe.SelectedRows[0].Cells[5].Value.ToString();
                    btnClick = "1";
                Dispose();
            }
            else
            {
                btnClick = "2";
            }
	}
	catch (Exception)
	{
        btnClick ="2";
	}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvGroupe.SelectedRows.Count > 0)
            {
                var ID = Convert.ToInt32(dgvGroupe.SelectedRows[0].Cells[0].Value.ToString());
                ConnectionClass.SupprimerUnResultatPatient(ID);
                txtGroupeExam_TextChanged(null, null);
            }
        }

 
    }
}
