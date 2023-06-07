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
    public partial class MontantFrm : Form
    {
        public MontantFrm()
        {
            InitializeComponent();
        }

        public int numeroPatient;
        private void MontantFrm_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtReduction_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
            if (e.KeyCode == Keys.Enter)
            {
                double remise,total;
                if (double.TryParse(txtReduction.Text, out remise) && double.TryParse(txtTotal.Text , out total))
                {
                    lblTotalRemise.Text = (remise * total / 100).ToString();
                    lblNetAPayer.Text = ((1 - remise/100) * total).ToString();
                }
                else
                {
                    lblTotalRemise.Text = "";
                }
            }
            }catch{}
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double montant;
                var total = 0.0;
                foreach (DataGridViewRow dtGrid in dataGridView1.Rows)
                {
                    var prix = Double.Parse(dtGrid.Cells[1].Value.ToString());
                    var qte = Double.Parse(dtGrid.Cells[2].Value.ToString());
                    var tot = prix * qte;
                    dtGrid.Cells[3].Value = tot;
                    if (Double.TryParse(dtGrid.Cells[3].Value.ToString(), out montant))
                    {

                        total += montant;
                    }
                    txtTotal.Text = total.ToString();
                    lblNetAPayer.Text = total.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtPaye_TextChanged(object sender, EventArgs e)
        {
            double paye,netPayer;
            if (double.TryParse(txtPaye.Text, out paye) && double.TryParse(lblNetAPayer.Text, out netPayer))
            {
                label5.Text = (netPayer - paye).ToString(); ;
                btnEnregistrer.Enabled = true;
            }
            else
            {
                btnEnregistrer.Enabled = false;
                label5.Text = "";
            }
           
        }
        public int numFacture;
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            double montantPaye,montantTotal;
            var numEmpl = GestionAcademique.LoginFrm.matricule;
            if (double.TryParse(txtPaye.Text, out montantPaye) && double.TryParse(txtTotal.Text ,out  montantTotal))
            {
                var facture = new AppCode.Facture();

                //if (AppCode.ConnectionClassClinique.EnregistrerUneFacture(facture, dataGridView1, numeroActe,"", montantPaye))
                //{
                //    Dispose();
                //}
            }
        }


        private void btnApercu_Click(object sender, EventArgs e)
        {
            try
            {
                var listePatient = from p in AppCode. ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == numeroPatient
                                   select p;

                var patient = new AppCode. Patient();
                foreach (var p in listePatient)
                    patient = p;
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
                sfd.FileName = "Rapport des actes du patient_imprimé_le_" + date + ".pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var count = dataGridView1.Rows.Count;
                    //if (count > 29)
                    {
                        var index = (dataGridView1.Rows.Count) / 35;

                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 35 < count)
                            {
                                var _listeImpression = AppCode.Impression.EtatFacturePatient(dataGridView1, patient, double.Parse(lblNetAPayer.Text), double.Parse(txtPaye.Text), i);

                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage pageIndex = document.addPage();

                                document.addImageReference(_listeImpression, inputImage);
                                sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                            }
                        }
                    }
                    document.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);

                }
            }
            catch { }
        }
    }
}
