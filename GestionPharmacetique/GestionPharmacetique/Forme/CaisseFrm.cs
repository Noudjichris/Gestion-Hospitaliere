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
    public partial class CaisseFrm : Form
    {
        public DateTime dateDebut, dateFin;
        public bool siVenteTotal=false;
        public CaisseFrm()
        {
            InitializeComponent();
        }

        private void RapportCaisseFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var listeEMploye = from f in AppCode.ConnectionClass.ListeDesEmployees()
                                   orderby f.NomEmployee
                                   select f;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("<Tous les caissiers>");
                foreach (AppCode.Employe empl in listeEMploye)
                {
                    comboBox2.Items.Add(empl.NomEmployee.ToUpper());
                }
                if (siVenteTotal)
                {
                    ListePaiementGrouperParDate();
                }
                else
                {
                    ListePaiement();
                }
            }
            catch { }
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
       
        void ListePaiement()
        {
            try
            {
                var caissier = "";
                if (comboBox2.Text == "<Tous les caissiers>")
                {
                    caissier = "";
                }
                else
                {
                    caissier = comboBox2.Text;
                }

                dgvVente.Rows.Clear();
                var montant=0.0;
                var listePaiement = AppCode.ConnectionClass.ListeDesPaiements(dateDebut, dateFin, caissier);
                for (var i = 0; i < listePaiement.Rows.Count; i++)
                {
                    montant +=double.Parse(listePaiement.Rows[i].ItemArray[2].ToString());
                    dgvVente.Rows.Add(
                       DateTime.Parse( listePaiement.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                        String.Format(elGR, "{0:0,0}", (double.Parse(listePaiement.Rows[i].ItemArray[2].ToString()))
                        ));
                }
                var liste =AppCode. ConnectionClass.ListeDesEmployees("nom_empl", "'" + comboBox2.Text + "'");
                var numEmploye = liste.Count > 0 ? liste[0].NumMatricule : "";
                 var dt = new DataTable();
                 if (string.IsNullOrEmpty(comboBox2.Text))
                 {
                     dt = AppCode.ConnectionClass.ListeDesFactures("", dateDebut, dateFin.AddHours(24));
                 }
                 else
                 {
                     dt = AppCode.ConnectionClass.ListeDesFactures(numEmploye, dateDebut, dateFin.AddHours(24));
                 }
                double solde = 0;
                for (var l = 0; l < dt.Rows.Count; l++)
                {
                    solde += double.Parse(dt.Rows[l].ItemArray[3].ToString());
                }
                montant = montant - solde;
                if (montant < 0)
                    montant = 0;
                label1.Text =  String.Format(elGR, "{0:0,0}", (montant));
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!siVenteTotal)
            ListePaiement();
        }

        void ListePaiementGrouperParDate()
        {
            try
            {
                var caissier = "";
                

                dgvVente.Rows.Clear();
                var montant = 0.0;
                 var listeJour = new List<DateTime>();
                    var day = dateFin.DayOfYear - dateDebut.DayOfYear;
                    if (day < 0)
                    {
                        day = day + 365;
                    }
                    for (var i = 0; i <= day; i++)
                    {
                        listeJour.Add(dateDebut.AddDays(i));
                    }

                    foreach (var date in listeJour)
                    {
                        var total = 0.0;
                        var listePaiement = AppCode.ConnectionClass.ListeDesPaiements(date, date, caissier);
                        for (var i = 0; i < listePaiement.Rows.Count; i++)
                        {
                            montant += double.Parse(listePaiement.Rows[i].ItemArray[2].ToString());
                            total  += double.Parse(listePaiement.Rows[i].ItemArray[2].ToString());                            
                        }
                        dgvVente.Rows.Add(
                             date.ToShortDateString(),
                                String.Format(elGR, "{0:0,0}", (total )
                                ));
                    }
                label1.Text = String.Format(elGR, "{0:0,0}", (montant));
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                var titre="";
                if (siVenteTotal)
                {
                    titre = "Caisse totale journalier du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                }
                else
                {
                    titre = "Caisse totale journalier du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
                }
                    if (dgvVente.Rows.Count > 0)
                    {
                    
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                        var jour = DateTime.Now.Day;
                        var mois = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                        var pathFolder = "C:\\Dossier Pharmacie";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        pathFolder = pathFolder + "\\Rapport vente";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        sfd.InitialDirectory = pathFolder;
                        var s = titre;
                        if (titre.Contains("/"))
                        { s = s.Replace("/", "_"); }
                        sfd.FileName = s + "_imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                            var count = dgvVente.Rows.Count;

                            var index = (dgvVente.Rows.Count) / 45;

                            for (var i = 0; i <= index; i++)
                            {
                                if (i * 45 < count)
                                {
                                    var _listeImpression = AppCode.ImprimerRaportVente.ImprimerRapportDeLaCaisse(titre, dgvVente, i);

                                    var inputImage = @"cdali" + i;
                                    // Create an empty page
                                    sharpPDF.pdfPage pageIndex = document.addPage();

                                    document.addImageReference(_listeImpression, inputImage);
                                    sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                }
                            }

                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);

                        }

                    }
               
                
            }
            catch (Exception)
            {
            }
        }
    }
}
