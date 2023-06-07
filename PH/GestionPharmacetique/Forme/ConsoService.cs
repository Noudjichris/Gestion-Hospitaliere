using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace GestionPharmacetique.Forme
{
    public partial class ConsoService : Form
    {
        public ConsoService()
        {
            InitializeComponent();
        }
         DateTime ObtenirDebutJour(string mois, int exercice)
        {
            return DateTime.Parse("01/" + ObtenirMois(mois) + "/" + exercice);
        }

         DateTime ObtenirFinJour(string mois, int exercice)
        {
            if (mois == "Janvier")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Février")
            {
                if (DateTime.IsLeapYear(exercice))
                    return DateTime.Parse("29/" + ObtenirMois(mois) + "/" + exercice);
                else
                    return DateTime.Parse("28/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Mars")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Avril")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Mai")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Juin")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Juillet")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Août")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Septembre")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Octobre")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Novembre")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Décembre")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else
            {
                return DateTime.Now.Date; ;
            }
        }
         static int ObtenirMois(string mois)
         {
             switch (mois)
             {
                 case "Janvier":
                     return 1;
                 case "Février":
                     return 2;
                 case "Mars":
                     return 3;
                 case "Avril":
                     return 4;
                 case "Mai":
                     return 5;
                 case "Juin":
                     return 6;
                 case "Juillet":
                     return 7;
                 case "Août":
                     return 8;
                 case "Septembre":
                     return 9;
                 case "Octobre":
                     return 10;
                 case "Novembre":
                     return 11;
                 case "Décembre":
                     return 12;
                 default:
                     return 0;
             };
         }
         public int etat, typeDepot;
        private void ConsoService_Load(object sender, EventArgs e)
        {
            for (var i = 2017; i < DateTime.Now.Year + 10; i++)
            {
                lblExercice.Items.Add(i.ToString());
            }
            var listeReference1 = from r in ConnectionClass.ListeDocumentStock(etat)
                                  select r;
           
            foreach (var r in listeReference1)
            {
                if(!string.IsNullOrEmpty(r.Reference))
                dgvProduit.Rows.Add(r.Reference,0);
            }
        }

        private void lblExercice_SelectedIndexChanged(object sender, EventArgs e)
        {

            Rapport();
        }

        void Rapport()
        {
            try
            {
                int exercice;
                if (Int32.TryParse(lblExercice.Text, out exercice))
                {
                    DateTime dateDebut, dateFin;
                    if (string.IsNullOrEmpty(cmbMois.Text))
                    {
                        dateDebut = DateTime.Parse("01/01/" + exercice);
                        dateFin = DateTime.Parse("31/12/" + exercice);
                    }
                    else
                    {
                        dateDebut = ObtenirDebutJour(cmbMois.Text, exercice);
                        dateFin = ObtenirFinJour(cmbMois.Text, exercice);
                    }
                   
                    var totalGeneral = .0m;
                    for (var i = 0; i < dgvProduit.Rows.Count; i++)
                    {
                        var liste = ConnectionClass.ListeMouvementStockGroupeParQuantite(typeDepot, etat, dateDebut, dateFin, dgvProduit.Rows[i].Cells[0].Value.ToString());
                        var total = 0m;
                        foreach (var p in liste)
                        {
                            total += p.PrixAchat * p.DifferenceStock;
                            totalGeneral += p.PrixAchat * p.DifferenceStock;
                        }
                        dgvProduit.Rows[i].Cells[1].Value = String.Format(elGR, "{0:0,0}", total);
                    }
                    lblOperateur.Text = String.Format(elGR, "{0:0,0}", totalGeneral);
                }

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception);
            }

        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            Rapport();
        }

        private void dgvProduit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dgvProduit.Rows.Remove(dgvProduit.Rows[e.RowIndex]);
                var totalGeneral = .0;
                for (var i = 0; i < dgvProduit.Rows.Count; i++)
                {
                    
                   totalGeneral +=double.Parse( dgvProduit.Rows[i].Cells[1].Value.ToString());
                }
                lblOperateur.Text = String.Format(elGR, "{0:0,0}", totalGeneral);
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }
        Bitmap _listeImpression;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(_listeImpression, 0, 0, _listeImpression.Width, _listeImpression.Height);
            e.HasMorePages = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var titre = cmbMois.Text + " " + lblExercice.Text;
            _listeImpression = ImprimerRaportVente.ImprimerRapportDesConsommations(titre, dgvProduit);
            if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                printPreviewDialog1.ShowDialog();
            }
        }
    }
}
