using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.FormesClinique
{
    public partial class LunetteFrm : Form
    {
        public LunetteFrm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0,this.groupBox1. Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LunetteFrmP.id = 0;
            if(LunetteFrmP.ShowBox())
            {
                listeLnuette();
            }
        }

        void listeLnuette()
        {
            try
            {
                dgvConsultation.Rows.Clear();
                var liste = from l in  AppCode.ConnectionClassClinique.ListeDesLunettes()
                            where l.Designation.ToUpper().StartsWith(txtRechercher.Text.ToUpper(), StringComparison.CurrentCultureIgnoreCase)
                            orderby l.Designation
                            select l;
                foreach(var l in liste)
                {
                    dgvConsultation.Rows.Add(l.IDLunette, l.Designation, l.Frais, l.Stock);
                }
            }
            catch { }
        }

        private void LunetteFrm_Load(object sender, EventArgs e)
        {
            listeLnuette();
        }

        public static LunetteFrm frm;
        public static AppCode.Lunettes lunettes;
        static bool flag=false;

        public static bool ShowBox()
        {
            frm = new LunetteFrm();
            frm.ShowDialog();
            return flag;
        }
        private void dgvConsultation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==4)
            {
                LunetteFrmP.id =Convert.ToInt32(dgvConsultation.CurrentRow.Cells[0].Value.ToString());
                if (LunetteFrmP.ShowBox())
                {
                    listeLnuette();
                }
            }
            else if(e.ColumnIndex==5)
            {
             var id = Convert.ToInt32(dgvConsultation.CurrentRow.Cells[0].Value.ToString());
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données ?","Confirmation","confirmtion.png") == "1")
                {
                    AppCode.ConnectionClassClinique.SupprimerUneLunette(id);
                    dgvConsultation.Rows.Remove(dgvConsultation.CurrentRow);
                }
            }
        }

        private void dgvConsultation_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lunettes = new AppCode.Lunettes();
                if(dgvConsultation.SelectedRows.Count>0)
                {
                    lunettes.Designation = dgvConsultation.SelectedRows[0].Cells[1].Value.ToString();
                    lunettes.Frais = double.Parse(dgvConsultation.SelectedRows[0].Cells[2].Value.ToString());
                    lunettes.Stock = double.Parse(dgvConsultation.SelectedRows[0].Cells[3].Value.ToString());
                    flag = true;
                    frm.Dispose();
                }
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
