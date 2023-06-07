using SGSP.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class ListeRetraiteFinContratFrm : Form
    {
        public ListeRetraiteFinContratFrm()
        {
            InitializeComponent();
        }
       public  int indexState;
        private void ListeRetraiteFinContratFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var List = new List<Service>();
                if(indexState==1)
                {
                    List = ConnectionClass.ListeAlerteFinContrat();
                    foreach (var s in List)
                    {
                        var dtPersonnel = ConnectionClass.ListeDesPersonnelParNumeroMatricule(s.NumeroMatricule);
                         dataGridView1.Rows.Add(
                                dtPersonnel.Rows[0].ItemArray[0].ToString(),
                                dtPersonnel.Rows[0].ItemArray[1].ToString(),
                                dtPersonnel.Rows[0].ItemArray[2].ToString(),
                                dtPersonnel.Rows[0].ItemArray[12].ToString(),
                                DateTime.Parse(dtPersonnel.Rows[0].ItemArray[17].ToString()).ToShortDateString(),
                                DateTime.Parse( dtPersonnel.Rows[0].ItemArray[22].ToString()).ToShortDateString(),
                                dtPersonnel.Rows[0].ItemArray[14].ToString(),
                                dtPersonnel.Rows[0].ItemArray[17].ToString(),
                               dtPersonnel.Rows[0].ItemArray[25].ToString(),
                                dtPersonnel.Rows[0].ItemArray[18].ToString(),
                                dtPersonnel.Rows[0].ItemArray[19].ToString()
                                );
                    }
                }
                else if(indexState==2)
                {
                    List = ConnectionClass.ListeAlerteRetraite();
                    foreach (var s in List)
                    {
                        var dtPersonnel = ConnectionClass.ListeDesPersonnelParNumeroMatricule(s.NumeroMatricule);
                         dataGridView1.Rows.Add(
                                dtPersonnel.Rows[0].ItemArray[0].ToString(),
                                dtPersonnel.Rows[0].ItemArray[1].ToString(),
                                dtPersonnel.Rows[0].ItemArray[2].ToString(),
                                dtPersonnel.Rows[0].ItemArray[12].ToString(),
                                DateTime.Parse(dtPersonnel.Rows[0].ItemArray[17].ToString()).ToShortDateString(),
                               DateTime.Parse(dtPersonnel.Rows[0].ItemArray[21].ToString()).ToShortDateString(),
                                dtPersonnel.Rows[0].ItemArray[14].ToString(),
                                dtPersonnel.Rows[0].ItemArray[17].ToString(),
                               dtPersonnel.Rows[0].ItemArray[25].ToString(),
                                dtPersonnel.Rows[0].ItemArray[18].ToString(),
                                dtPersonnel.Rows[0].ItemArray[19].ToString()
                                );
                    }
                }
               
            }
            catch { }
        }

        private void btnAjouterUneAgence_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument pdfDocument = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var mois = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                    var pathFolder = "C:\\Dossier Personnel";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    pathFolder = pathFolder + "\\Liste Personnel";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    sfd.InitialDirectory = pathFolder;
                    string titreImpression="";
                    if (indexState == 1)
                        titreImpression = "Liste de personnel approchant vers la fin du contrat";
                    else if (indexState == 2)
                        titreImpression = "Liste de personnel approchant vers la retraite";
                    sfd.FileName = titreImpression + "_" + date + ".pdf";
                    Bitmap document;

                    var div = dataGridView1.Rows.Count / 44;
                    for (var i = 0; i <= div; i++)
                    {
                       
                            document = Impression.ImprimerLalisteDesPersonnelsContratFinOuRetraite(dataGridView1, titreImpression, i, dataGridView1.Rows.Count.ToString());
                        
                        sharpPDF.pdfPage pageIndex = pdfDocument.addPage();

                        var inputImage = @"cdali" + i;
                        pdfDocument.addImageReference(document, inputImage);
                        sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                    }
                    pdfDocument.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }

            }
            catch (Exception)
            {
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
