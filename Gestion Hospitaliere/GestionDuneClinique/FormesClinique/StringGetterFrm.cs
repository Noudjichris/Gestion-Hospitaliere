using System;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.FormesClinique
{
    public partial class StringGetterFrm : Form
    {
        public StringGetterFrm()
        {
            InitializeComponent();
        }

        public static StringGetterFrm  frm;
        private static bool flag;
        public static string libelle;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                libelle = textBox1.Text;
                flag = true;
                frm.Close();
            }
        }

        public static  bool ShowBox()
        {
            frm=new StringGetterFrm();
            frm.ShowDialog();
            
            return flag;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            flag = false;
            Dispose();
        }

        private void StringGetterFrm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(libelle))
            {
                textBox1.Text = libelle;
            }
        }
    }
}
