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
    public partial class DetailProFrm : Form
    {
        public DetailProFrm()
        {
            InitializeComponent();
        }

        public static DetailProFrm frm;
        public static int quantiteInitiale, nbreDetaille, nbreEnBoite, qteRestante;
        public static bool btnClick = false;
        public static string medicament, numeroProduit;
        public static decimal prix;
        private void DetailProFrm_Load(object sender, EventArgs e)
        {
            try
            {
                lblDesignation.Text = medicament;
                txtQteInit.Text = quantiteInitiale.ToString();
                var liste = AppCode.ConnectionClass.ListeDesMedicamentsRechercherParNom(medicament);
               
                if (liste[0].NombreBoite>0)
                {
                    txtNbreDetail.Text = liste[0].NombreDetail.ToString();
                    txtQteEnBoite.Text = liste[0].NombreBoite.ToString();
                    txtPrix.Text = liste[0].PrixVenteDetail.ToString();
                }
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nbreEnBoite, qteInitial, qteRestante;
            if (Int32.TryParse(txtQteInit.Text, out qteInitial) && 
                Int32.TryParse(txtQteEnBoite.Text, out nbreEnBoite))
            {
                txtNbreDetail.Text  = nbreEnBoite.ToString();
                qteRestante =qteInitial- 1;
                if (qteRestante < 0)
                    txtQteInit.Text = "0";
                else
                    txtQteInit.Text = qteRestante.ToString();
            }
        }

        public static bool ShowBox()
        {
            frm = new DetailProFrm();
            frm.ShowDialog();
            return btnClick;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtQteInit.Text, out qteRestante) && 
                Int32.TryParse(txtQteEnBoite.Text, out nbreEnBoite) &&
                Int32.TryParse(txtNbreDetail.Text, out nbreDetaille)&&
                decimal.TryParse(txtPrix.Text, out prix))
            {
                if (AppCode.ConnectionClass.ParametrerLesDetails(numeroProduit, qteRestante, nbreEnBoite, nbreDetaille,prix))
                {
                    btnClick = true;
                    Dispose();
                }
            }
        }
    }
}
