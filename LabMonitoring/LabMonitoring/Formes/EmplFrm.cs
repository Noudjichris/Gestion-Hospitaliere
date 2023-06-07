using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabMonitoring.Formes
{
    public partial class EmplFrm : Form
    {
        public EmplFrm()
        {
            InitializeComponent();
        }
        int id;
        void ListePrescripteur()
        {
            dgvGroupe.Rows.Clear();
            var dt = AppCode.ConnectionClass.ListePrescripteurs();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                dgvGroupe.Rows.Add(dt.Rows[i].ItemArray[0].ToString(), dt.Rows[i].ItemArray[1].ToString());
            }
        }
        private void txtGroupeExam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtGroupeExam.Text))
                {
                    if (AppCode.ConnectionClass.EnregistrerUnPrescriteur(id ,txtGroupeExam.Text))
                    {
                        txtGroupeExam.Text = "";
                        id = 0;
                        ListePrescripteur();
                    }
                }
            }
        }

        private void dgvGroupe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                id = Convert.ToInt32(dgvGroupe.CurrentRow.Cells[0].Value.ToString());
                txtGroupeExam.Text = dgvGroupe.CurrentRow.Cells[1].Value.ToString();
            }
            else if (e.ColumnIndex == 3)
            {
                if(AppCode.ConnectionClass.SupprimerUnPrescriteur(Convert.ToInt32(dgvGroupe.CurrentRow.Cells[0].Value.ToString())))
                {
                    dgvGroupe.Rows.Remove(dgvGroupe.CurrentRow);
                }
            }
        }

        private void EmplFrm_Load(object sender, EventArgs e)
        {
            ListePrescripteur();
        }
    }
}
