using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.FormesClinique
{
    public partial class TableDesAnalyseFrm : Form
    {
        public TableDesAnalyseFrm()
        {
            InitializeComponent();
        }

        private void TableDesAnalyseFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 128, 0), 0);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.FromArgb(255, 128, 0), Color.FromArgb(255, 128, 0), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1,this.groupBox3. Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.FromArgb(255, 128, 0), Color.FromArgb(255, 128, 0), LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void TableDesAnalyseFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dgvAnal.RowTemplate.Height = 30;
                ListesDesExamens();

                var listeEntrep = AppCode.ConnectionClassClinique.ListeDesEntreprises();
                cmbEntreprise.Items.Add("");
                foreach (var  entreprise in listeEntrep)
                {
                    cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
                }
            }
            catch { }
        }
        string etat; int id;
        //liste des examens
        private void ListesDesExamens()
        {
            try
            {
                dgvAnal.Rows.Clear();
                var listeAnalyse =from a in AppCode. ConnectionClassClinique.ListeDesAnalyses()
                                      where a.TypeAnalyse.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                                      select a;

                foreach (var analyse in listeAnalyse)
                {dgvAnal.Rows.Add
                    (
                        analyse.NumeroListeAnalyse.ToString(),
                        analyse.TypeAnalyse.ToUpper(),
                        analyse.Frais.ToString(),
                        analyse.FraisNRC,
                        analyse.FraisCICR,
                        analyse.FraisMSF 
                    );
                Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
            ;
                }

            }
            catch { }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                double prix, fraisCICR=0,fraisNRC=0,fraisMSF=0;
                string typeAnal;

                if (Double.TryParse(txtprix.Text, out prix) )
                {
                    if (!string.IsNullOrEmpty(txtTypeExma.Text))
                    {
                        typeAnal = txtTypeExma.Text;
                        var analyse = new AppCode.Analyse();
                        analyse.Frais = prix;
                        analyse.TypeAnalyse = txtTypeExma.Text;
                        analyse.NumeroListeAnalyse = id;
                        analyse.FraisCICR = fraisCICR;
                        analyse.FraisNRC = fraisNRC;
                        analyse.FraisMSF = fraisMSF;
                        if (AppCode.ConnectionClassClinique.EnregisterLesAnalyses(analyse, etat))
                        {
                            etat = "1";
                            txtTypeExma.Text = "";
                            txtprix.Text = "";
                            textBox1.Focus();
                            ListesDesExamens();
                            textBox1.Text = "";
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer le type d'examen, puis réessayer", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer des chiffres valides pour les frais d'examen", "Erreur", "erreur.png");
                }
                dgvAnal.Location = new Point(13, 105);
                dgvAnal.Height = dgvAnal.Height + 110;
                groupBox3.Visible = false;
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnajexamen_Click(object sender, EventArgs e)
        {
            if (groupBox3.Visible == false)
            {
                dgvAnal.Location = new Point(13, 190);
                dgvAnal.Height = dgvAnal.Height - 100;
                txtTypeExma.Focus();
                groupBox3.Visible = true;
                etat = "1";
            }
        }

        private void dgvAnal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (dgvAnal.Rows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer les donnees de cet analyse?", "demande confirmation", "confirmation.png") == "1")
                    {
                        AppCode.ConnectionClassClinique.SupprimerLesAnalyses(Convert.ToInt32(dgvAnal.SelectedRows[0].Cells[0].Value.ToString()));
                        ListesDesExamens();
                        textBox1.Focus();
                    }

                }
            }
            else
            {
                id = Convert.ToInt32(dgvAnal.SelectedRows[0].Cells[0].Value.ToString());
                txtprix.Text = dgvAnal.SelectedRows[0].Cells[2].Value.ToString();
                txtTypeExma.Text = dgvAnal.SelectedRows[0].Cells[1].Value.ToString();
               
                etat = "2";
                if (groupBox3.Visible == false)
                {
                    dgvAnal.Location = new Point(13, 190);
                    dgvAnal.Height = dgvAnal.Height - 110;
                    groupBox3.Visible = true;
                  
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListesDesExamens();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListesDesExamens();
            }
        }

        private void btnsupexamen_Click(object sender, EventArgs e)
        {
           try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";
                var dateInv = DateTime.Now;               

                sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "Liste_des_analyses_imprimé_le_" + date + ".pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                     var inputImage = @"cdali" ;
                    var index = dgvAnal.Rows.Count/ 40;
                    if (dgvAnal.Rows.Count > 40)
                    {
                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 40 < dgvAnal.Rows.Count)
                            {
                                var bitmap = GestionDuneClinique.AppCode.Impression.ListeDesExamen(dgvAnal, i);

                                inputImage = @"cdali" + i;
                                // Create an empty page
                                 sharpPDF.pdfPage  pageIndex = document.addPage(842,595);

                                document.addImageReference(bitmap, inputImage);
                                sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                            }
                        }
                    }
                }
                document.createPDF(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }

            catch (Exception ex) { MonMessageBox.ShowBox("", ex); } 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from l in AppCode.ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text)
                            where l.NomEntreprise.ToUpper().Equals(cmbEntreprise.Text.ToUpper())
                            select l.IndexPrix;
                foreach (var i in liste)
                    textBox2.Text = i.ToString();
            }
            catch { }
        }

    }
}
