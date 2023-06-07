using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class AlerteReraiteCtrl : UserControl
    {
        public AlerteReraiteCtrl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = Color.Orange;
            label1.ForeColor = SystemColors.Control;
            label2.ForeColor = SystemColors.Control;
            timer2.Start();
            timer1.Stop();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control; ;
            label1.ForeColor = SystemColors.Control;
            button1.ForeColor = SystemColors.Control;
            label2.ForeColor = SystemColors.Control;
            timer1.Start();
            timer2.Stop();

        }

        private void AlerteExpirationFrm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        static string state;
        public static AlerteReraiteCtrl ctrl;
        public static string ShowBox(Form box)
        {
            ctrl = new AlerteReraiteCtrl();
            ctrl.Location = new Point(450, 270);
            box.Controls.Add(ctrl);
            return state;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                state = "1";
                Dispose();
                var frm = new SGSP.Formes.ListeRetraiteFinContratFrm();
                frm.indexState = 2;
                frm.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

        private void AlerteStockCtrl_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
