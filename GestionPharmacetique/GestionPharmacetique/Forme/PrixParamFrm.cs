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
    public partial class PrixParamFrm : Form
    {
        public PrixParamFrm()
        {
            InitializeComponent();
        }
     static   bool flag;
        public static int idDoc;
        public static PrixParamFrm frm;
        public static string typePrix;
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int prix;
                if (checkBox1.Checked)
                {
                    prix = 1;
                    typePrix = "prix achat";
                }
                else if (checkBox2.Checked)
                {
                    prix = 2;
                    typePrix = "prix vente";
                }
                else {
                    prix = 0; return;
                }
                if(AppCode.ConnectionClass.MettreAjourDocumentStock(idDoc,prix))
                {
                    flag = true;
                    frm.Dispose();
                }
            }
            catch (Exception)
            {
            }
        }

        public static bool ShowBox()
        {
            frm = new PrixParamFrm();
            frm.ShowDialog();
            return flag;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
