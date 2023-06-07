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
    public partial class LunetteFrmP : Form
    {
        public LunetteFrmP()
        {
            InitializeComponent();
        }
        public static LunetteFrmP frm;
       static  bool flag = false;
     public static   int id;
        public static  bool ShowBox()
        {
            frm = new LunetteFrmP();
            frm.ShowDialog();
            return flag;
        }
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            int stock=0; double frais;
            if ( double.TryParse(txtFrais.Text , out frais))//Int32.TryParse(txtStock.Text, out stock  ) &&
            {
                var lunettes = new AppCode.Lunettes();
                lunettes.IDLunette = id;
                lunettes.Designation = txtDesignation.Text;
                lunettes.Frais = frais;
                lunettes.Stock = stock;
                if(AppCode.ConnectionClassClinique.EnregistrerUneLunette(lunettes))
                {
                    flag = true;
                    Dispose();
                }
            }
        }

        private void LunetteFrmP_Load(object sender, EventArgs e)
        {
            var liste = from l in AppCode.ConnectionClassClinique.ListeDesLunettes()
                        where l.IDLunette == id
                        select l;
            foreach (var l in liste)
            {
                txtDesignation.Text = l.Designation;
                txtFrais.Text = l.Frais.ToString();
                txtStock.Text = l.Stock.ToString();
            }
        }
    }
}
