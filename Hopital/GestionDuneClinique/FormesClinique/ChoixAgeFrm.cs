using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.FormesClinique
{
    public partial class ChoixAgeFrm : Form
    {
        public ChoixAgeFrm()
        {
            InitializeComponent();
        }

        private void ChoixAgeFrm_Load(object sender, EventArgs e)
        {
            for (var i = 0; i < 120; i++)
            {
                //numericUpDown1.
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (numericUpDown1.Visible)
                {
                    ageDebut = numericUpDown1.Value.ToString();
                    //ageDebut = "0";
                }
                else
                {
                    ageDebut = "0";
                }
                if (numericUpDown2.Visible)
                {
                    ageFin = numericUpDown2.Value.ToString();
                }
                else
                {
                    ageFin = "130";
                }
                flag = false;
                Dispose();
                flag = true;
                Dispose();
            }
            catch { }
        }


        public static bool flag;
        public static string ageDebut, ageFin;
        static ChoixAgeFrm frm;

        public static bool ShowBox()
        {
            try
            {
                frm = new ChoixAgeFrm();
                frm.ShowDialog();
                return flag;
            }
            catch
            { return false; }
        }

        private void button7_Click(object sender, EventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false; 
                numericUpDown1.Visible = false;
            }
            else
            {
                numericUpDown1.Visible = true ;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                numericUpDown2.Visible = false ;
            }
            else
            {
                numericUpDown2.Visible = true ;
            }
        }
    }
}
