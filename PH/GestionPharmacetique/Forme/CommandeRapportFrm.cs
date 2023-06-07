using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class CommandeRapportFrm : Form
    {
        public CommandeRapportFrm()
        {
            InitializeComponent();
        }

        public DateTime dateDebut, dateFin;
        private void CommandeRapportFrm_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Add("<Tous les fournisseurs>");
            var ListeFournisseur = from f in AppCode.ConnectionClass.ListeFournisseur()
                                   orderby f.NomFournisseur
                                   select f;
            foreach (var f in ListeFournisseur)
            {
                comboBox2.Items.Add(f.NomFournisseur);
            }
            ListeDesRapportsVentes();
        }

        private void ListeDesRapportsVentes()
        {
            try
            {
                var Montant = 0.0;
                dataGridView1.Rows.Clear();
                string caissier;
                if (comboBox2.Text == "<Tous les fournisseurs>")
                {
                    caissier = "";
                }
                else
                {
                    caissier = comboBox2.Text;
                }
                DataTable dataTable = AppCode.ConnectionClass.ListeDesDetailCommandesParFournisseur(dateDebut, dateFin.Date.AddHours(24), caissier);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    var total = double.Parse(dataTable.Rows[i].ItemArray[2].ToString());
                    dataGridView1.Rows.Add(
                        dataTable.Rows[i].ItemArray[3].ToString().ToUpper(),
                        string.Format(elGR, "{0:0,0}", double.Parse(dataTable.Rows[i].ItemArray[0].ToString())),
                                    string.Format(elGR, "{0:0,0}", double.Parse(dataTable.Rows[i].ItemArray[1].ToString())),
                        string.Format(elGR, "{0:0,0}", total));
                    Montant += double.Parse(dataTable.Rows[i].ItemArray[2].ToString());
                }
                borderLabel2.Text = "Montant total de " + string.Format(elGR, "{0:0,0}", Montant) + " F CFA";

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
            }
        }

        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SGDP.Formes.DateFrm.state = true;
                if (SGDP.Formes.DateFrm.ShowBox())
                {
                    dateDebut = SGDP.Formes.DateFrm.dateDebut;
                    dateFin = SGDP.Formes.DateFrm.dateFin;
                    ListeDesRapportsVentes();
                }
            }
            catch { }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeDesRapportsVentes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    var titre = "Vente totale journalier du " + dateDebut.ToShortDateString() + " au " + dateFin.ToShortDateString();
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

                        var count = dataGridView1.Rows.Count;

                        var index = (dataGridView1.Rows.Count) / 45;

                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 45 < count)
                            {
                                var _listeImpression = AppCode.Impression.ImprimerRapportDeLivraison(dataGridView1, comboBox2.Text, dateDebut, dateFin, i);

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
            catch { }
        }
    }
}
