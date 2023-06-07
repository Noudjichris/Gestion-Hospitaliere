using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class FrmType : Form
    {
        public FrmType()
        {
            InitializeComponent();
        }
       public static  FrmType frm;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            checkBox2.Checked = false;
            
        }

        public static bool  ShowBox()
        {
            frm = new FrmType();
            frm.ShowDialog();
            return true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
                checkBox1.Checked = false;
        }
        public static  int numeroPiece,etat;
        public static string typePrix;

        private void button6_Click(object sender, EventArgs e)
        {
                     SGDP.Formes.ModifTsockFrm.etat = etat;
                     SGDP.Formes.ModifTsockFrm.numeroPiece = numeroPiece;
                     SGDP.Formes.ModifTsockFrm.typePrix = typePrix;
            if (checkBox2.Checked)
            {
                SGDP.Formes.ModifTsockFrm.typeDepot = 2;
                SGDP.Formes.ModifTsockFrm.depot = checkBox2.Text;
                SGDP.Formes.ModifTsockFrm.depot = checkBox2.Text;
                SGDP.Formes.ModifTsockFrm.ShowBox();
                Dispose();
            }
            else if (checkBox1.Checked)
            {
                SGDP.Formes.ModifTsockFrm.typeDepot = 1;
                SGDP.Formes.ModifTsockFrm.depot = checkBox2.Text;
                SGDP.Formes.ModifTsockFrm.ShowBox();
                Dispose();
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez cocher le type de depôt", "Erreur", "erreur.png");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
