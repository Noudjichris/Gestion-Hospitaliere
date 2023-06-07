using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.FormesClinique
{
    public partial class ListeProFrm : Form
    {
        public ListeProFrm()
        {
            InitializeComponent();
        }

        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListeProFrm_Load(object sender, EventArgs e)
        {
            Location = new Point(GestionAcademique.Form1.xLocation, GestionAcademique.Form1.yLocation);
            var year = DateTime.Now.Year + 10;
            for (var i = 2016; i <= year; i++)
            {
                cmbAnnne.Items.Add(i.ToString());
            }
            cmbAnnne.Text = DateTime.Now.Year.ToString();
            ListeDesProgrammes();
        }

        void ListeDesProgrammes()
        {
            try
            {
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.Rows.Clear();
                var liste = from p in ConnectionClassClinique.ListeDesProgrammes()
                    where p.Annee == Convert.ToInt32(cmbAnnne.Text)
                    select p;
                foreach (var p in liste)
                {
                    var service = "";
                    if (p.IDService > 0)
                    {
                        service = ConnectionClassClinique.ListeService(p.IDService).Rows[0].ItemArray[1].ToString();
                    }
                    dataGridView1.Rows.Add(p.NumeroProgramme, p.Libelle, p.DateDebut.Date.ToShortDateString(),
                        p.DateFin.Date.ToShortDateString(), p.Annee, p.NumeroEmploye,service);
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void cmbAnnne_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeDesProgrammes();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex ==7)
                {
                    if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString()
                            .Equals(GestionAcademique.LoginFrm.matricule) ||
                        GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                    {
                        programme = new Programme();
                        programme.NumeroEmploye = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                        programme.Annee = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                        programme.DateDebut =
                            Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                        programme.Libelle = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                        programme.DateFin = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                        programme.NumeroProgramme =
                            Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        programme.Legende = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                        flag = true;
                    }
                    Dispose();
                }
                else if (e.ColumnIndex == 8)
                {
                    if (dataGridView1.SelectedRows[0].Cells[5].Value.ToString().Equals(GestionAcademique.LoginFrm.matricule) || GestionAcademique.LoginFrm.typeUtilisateur=="admin")
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données", "Confirmation", "confirmation.png") ==
                        "1")
                    {
                        ConnectionClassClinique.SupprimerUnProgramme(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                }
            }
            catch
                (Exception Exception)
            { }
        }

        public static ListeProFrm frm;
        private static bool flag;
        public static Programme programme;
        public static bool ShowBox()
        {
            frm=new ListeProFrm();
            frm.ShowDialog();
            return flag;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            flag = false;
            Dispose();
        }
    }
}
