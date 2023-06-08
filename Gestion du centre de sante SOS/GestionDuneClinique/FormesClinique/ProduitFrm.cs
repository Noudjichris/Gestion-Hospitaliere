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
    public partial class ProduitFrm : Form
    {
        public ProduitFrm()
        {
            InitializeComponent();
        }

        public int nombreExamen;
        public string nomProduit;
        public DateTime dateDebut, dateFin;
        private void ProduitFrm_Load(object sender, EventArgs e)
        {
            var numeroExamen = 0;
            var liste = from l in AppCode.ConnectionClassClinique.ListeDesAnalyses()
                        where l.TypeAnalyse.ToUpper().Equals(nomProduit.ToUpper())
                        select l;
            foreach (var l in liste)
                numeroExamen = l.NumeroListeAnalyse;
            label1.Text = nomProduit;
            label2.Text = nombreExamen.ToString();

            var dt = GestionDuneClinique.AppCode.ConnectionClassPharmacie.ListeMouvementStockGroupeParQuantite(dateDebut, dateFin, numeroExamen);
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                int numAnal=0;
                if (Int32.TryParse(dt.Rows[i].ItemArray[3].ToString(), out numAnal))
                {
                }
                var quantite = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());
                if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[2].ToString()))
                {
                    quantite = quantite * Convert.ToInt32(dt.Rows[i].ItemArray[2].ToString());
                    //nombreExamen = nombreExamen * Convert.ToInt32(dt.Rows[i].ItemArray[2].ToString());
                }
                var nbreRestant = quantite - nombreExamen;

                if (numeroExamen == numAnal)
                    dataGridView3.Rows.Add(dt.Rows[i].ItemArray[1].ToString(), quantite, nombreExamen,nbreRestant);
            }
        }
    }
}
