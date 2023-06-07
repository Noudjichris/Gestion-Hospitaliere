using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGDP.Formes
{
    public partial class BanqueFrm : Form
    {
        public BanqueFrm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnClick = "2";
            Dispose();
        }


        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue
                , Color.SlateGray, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void BanqueFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.White
                , SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        string state;
        int numeorBanque;
        private void btnValider_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBanque.Text != "")
                {
                    if (SGSP.AppCode.ConnectionClass.EnregistrerUneBank(numeorBanque, txtBanque.Text, state))
                    {
                        ListeBanque();
                        txtBanque.Text = "";
                        numeorBanque = 0;
                        state = "1";
                    }
                }
            }
            catch { }
        }

        private void BanqueFrm_Load(object sender, EventArgs e)
        {
            ListeBanque();
            state = "1";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                numeorBanque = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                txtBanque.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                state = "2";
            }
        }

        void ListeBanque()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste =SGSP. AppCode.ConnectionClass.ListeBanques();
                for (var i = 0; i < liste.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(liste.Rows[i].ItemArray[0].ToString(), liste.Rows[i].ItemArray[1].ToString());
                }
            }
            catch { }
        }

        public static BanqueFrm frm;
       public  static string btnClick, nomBanque;
        public static string ShowBox()
        {
            frm = new BanqueFrm();
            frm.ShowDialog();
            return btnClick;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    nomBanque = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    btnClick = "1";
                    Dispose();
                }
            }
            catch { }
        }

        private void txtBanque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnValider_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count>0)
                {
                    numeorBanque = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    if (SGSP.AppCode.ConnectionClass.EnregistrerUneBank(numeorBanque, txtBanque.Text, "3"))
                    {
                        ListeBanque();
                        txtBanque.Text = "";
                        numeorBanque = 0;
                        state = "1";
                    }
                }
            }
            catch { }
        }

    }
}
