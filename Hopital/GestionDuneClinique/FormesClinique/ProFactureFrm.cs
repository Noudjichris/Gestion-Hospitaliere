using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.FormesClinique
{
    public partial class ProFactureFrm : Form
    {
        public ProFactureFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ProFactureFrm_Load(object sender, EventArgs e)
        {
                        button1.Location = new Point(Width - 35, 4);
                        foreach (var l in AppCode.ConnectionClassClinique.ListeDesEntreprises())
                        {
                            cmbCaissier.Items.Add(l.NomEntreprise);
                        }
                        Column1.Width = 45;
            Column4.Width = 45;
                        if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                        {
                            Column2.Visible = true;
                            Column2.Width = 45;
                        }

                        ListeFacturation();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        void ListeFacturation()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listeFacture = from f in ConnectionClassClinique.TableDesDetailsFacturesProforma()
                                   where f.DateFacture >= dtp1.Value.Date
                                   where f.DateFacture < dtp2.Value.Date.AddHours(24)
                                   where f.Entreprise.ToUpper().StartsWith(cmbCaissier.Text.ToUpper(), StringComparison.CurrentCultureIgnoreCase)
                                   select f;
                foreach (Facture facture in listeFacture)
                {
                    dataGridView2.Rows.Add
                    (
                        facture.NumeroFacture.ToString(),
                        facture.DateFacture.ToString(),
                        facture.MontantFactural.ToString(),
                        facture.IdPatient.ToString(),
                        facture.Patient.ToUpper(),
                        facture.NumeroEmploye,
                        facture.NomEmploye.ToUpper(), facture.Entreprise,
                        facture.Sub);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste facture", ex);
            }
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void cmbCaissier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeFacturation();
        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listeFacture = from f in  ConnectionClassClinique.TableDesDetailsFacturesProforma()
                                                where f.DateFacture>=dtp1.Value.Date 
                                               where f.DateFacture <dtp2 .Value.Date.AddHours(24)
                                              select f;
                foreach (Facture facture in listeFacture)
                {
                    dataGridView2.Rows.Add
                    (
                        facture.NumeroFacture.ToString(),
                        facture.DateFacture.ToString(),
                        facture.MontantFactural.ToString(),
                        facture.IdPatient.ToString(),
                        facture.Patient.ToUpper(),
                        facture.NumeroEmploye,
                        facture.NomEmploye.ToUpper(), facture.Entreprise,
                        facture.Sub);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste facture", ex);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listeFacture = from f in ConnectionClassClinique.TableDesDetailsFacturesProforma()
                                   where f.NumeroFacture==Convert.ToInt32(textBox5.Text)
                                   select f;
                foreach (Facture facture in listeFacture)
                {
                    dataGridView2.Rows.Add
                    (
                        facture.NumeroFacture.ToString(),
                        facture.DateFacture.ToString(),
                        facture.MontantFactural.ToString(),
                        facture.IdPatient.ToString(),
                        facture.Patient.ToUpper(),
                        facture.NumeroEmploye,
                        facture.NomEmploye.ToUpper(), facture.Entreprise,
                        facture.Sub);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste facture", ex);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listeFacture = from f in ConnectionClassClinique.TableDesDetailsFacturesProforma()
                                   where f.Patient.StartsWith(textBox4.Text, StringComparison.CurrentCultureIgnoreCase)
                                   select f;
                foreach (Facture facture in listeFacture)
                {
                    dataGridView2.Rows.Add
                    (
                        facture.NumeroFacture.ToString(),
                        facture.DateFacture.ToString(),
                        facture.MontantFactural.ToString(),
                        facture.IdPatient.ToString(),
                        facture.Patient.ToUpper(),
                        facture.NumeroEmploye,
                        facture.NomEmploye.ToUpper(),facture.Entreprise,
                        facture.Sub);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste facture", ex);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9)
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        var frm = new ProDetFactureFrm();
                        frm.numeroFacture = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                        frm.ShowDialog();
                    }
                }else if(e.ColumnIndex==10)
                {
                    var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                       where p.NumeroPatient == Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString())
                                       select p;

                    var patient = new Patient();
                    foreach (var p in listePatient)
                        patient = p;
                    var numeroFacture = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                    var dateFacture = DateTime.Parse(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                    bitmap = Impression.FactureOfficiellePourBonDesActes(numeroFacture,dataGridView2, patient, dateFacture, dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString());
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
                else if (e.ColumnIndex == 11)
                {
                    if(MonMessageBox.ShowBox("Voulez vous supprimer ces données ?","Confirmation","confirmation.png")=="1")
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        AppCode.ConnectionClassClinique.SupprimerFactureProforma(Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()));
                        dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        Bitmap bitmap;
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            //var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            //e.Graphics.DrawImage(bitmap, -5, 20, bitmap.Width, bitmap.Height);
            //e.HasMorePages = false;
        }
    }
}
