using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1,this.groupBox3. Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void TableDesAnalyseFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dgvAnal.RowTemplate.Height = 30;
                ListesDesExamens();
                foreach (var a in AppCode.ConnectionClassClinique.ListeDesLibellesDistingues())
                    cmbGroupe.Items.Add(a.Designation);

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
                var listeAnalyse = from a in AppCode.ConnectionClassClinique.ListeDesAnalyses()
                                   join g in AppCode.ConnectionClassClinique.ListeDesGroupes()
                                   on a.NumeroGroupe equals g.NumeroGroupe
                                   where a.TypeAnalyse.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                                   select new
                                   {
                                       a.NumeroListeAnalyse,
                                       a.TypeAnalyse,
                                       a.FraisInternes,
                                       a.FraisConventionnes,
                                       a.Frais,g.Groupe,g.Libelle
                                   };

                foreach (var analyse in listeAnalyse)
                
                {
                    dgvAnal.Rows.Add
                    (
                        analyse.NumeroListeAnalyse.ToString(),
                        analyse.Groupe+" "+analyse.Libelle,
                        analyse.TypeAnalyse.ToUpper(),
                        analyse.Frais.ToString(),
                        analyse.FraisInternes,
                        analyse.FraisConventionnes
                    );
                Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                   
                }

            }
            catch
            { }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                double prix, fraisCICR,fraisNRC,fraisAssure;
                string typeAnal;

                if (Double.TryParse(txtprix.Text, out prix) && Double.TryParse(txtCICR.Text, out fraisCICR) 
                   && Double.TryParse(txtNRC.Text , out fraisNRC))
                {
                    if (!string.IsNullOrEmpty(txtTypeExma.Text))
                    {
                        
                        typeAnal = txtTypeExma.Text;
                        var analyse = new AppCode.Analyse();
                        analyse.Frais = prix;
                        analyse.TypeAnalyse = txtTypeExma.Text;
                        analyse.NumeroListeAnalyse = id;
                        analyse.FraisInternes = fraisCICR;
                        analyse.FraisConventionnes = fraisNRC;
                        if (string.IsNullOrEmpty(comboBox1.Text))
                        {
                            analyse.NumeroGroupe = AppCode.ConnectionClassClinique.ListeDesGroupes(cmbGroupe.Text)[0].NumeroGroupe;
                        }
                        else
                        {
                            var liste = from l in AppCode.ConnectionClassClinique.ListeDesGroupes(cmbGroupe.Text)
                                        where l.Libelle.StartsWith(comboBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                                        select l;
                            foreach (var li in liste)
                            {
                                analyse.NumeroGroupe = li.NumeroGroupe;
                            }
                        }

                        if (AppCode.ConnectionClassClinique.EnregisterLesAnalyses(analyse, etat))
                        {
                            etat = "1";
                            txtTypeExma.Text = "";
                            txtprix.Text = "";
                            txtCICR.Text = "";
                            txtNRC.Text = "";
                            ListesDesExamens();
                            btnajexamen.Focus();
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
                dgvAnal.Location = new Point(13, 210);
                dgvAnal.Height = dgvAnal.Height - 120;
                groupBox3.Visible = true;
                txtTypeExma.Focus();
                etat = "1";
            }
        }

        private void dgvAnal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    if (dgvAnal.Rows.Count > 0)
                    {
                        if (MonMessageBox.ShowBox("Voulez vous supprimer les donnees de cet analyse?", "demande confirmation", "confirmation.png") == "1")
                        {
                            AppCode.ConnectionClassClinique.SupprimerLesAnalyses(Convert.ToInt32(dgvAnal.SelectedRows[0].Cells[0].Value.ToString()));
                            ListesDesExamens();
                            txtTypeExma.Focus();
                        }

                    }
                }
                else if (e.ColumnIndex == 6)
                {
                    id = Convert.ToInt32(dgvAnal.SelectedRows[0].Cells[0].Value.ToString());
                    txtprix.Text = dgvAnal.SelectedRows[0].Cells[3].Value.ToString();
                    txtTypeExma.Text = dgvAnal.SelectedRows[0].Cells[2].Value.ToString();
                    txtNRC.Text = dgvAnal.SelectedRows[0].Cells[5].Value.ToString();
                    txtCICR.Text = dgvAnal.SelectedRows[0].Cells[3].Value.ToString();
                    cmbGroupe.Text = dgvAnal.SelectedRows[0].Cells[1].Value.ToString().Substring(0, dgvAnal.SelectedRows[0].Cells[1].Value.ToString().IndexOf(" ")); ;
                    etat = "2";
                    if (groupBox3.Visible == false)
                    {
                        dgvAnal.Location = new Point(13, 210);
                        dgvAnal.Height = dgvAnal.Height - 120;
                        groupBox3.Visible = true;

                    }
                    comboBox1.Text = dgvAnal.SelectedRows[0].Cells[1].Value.ToString().Substring(dgvAnal.SelectedRows[0].Cells[1].Value.ToString().IndexOf(" ") + 1); ;
                }
            }
            catch { }
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
                    //if (dgvAnal.Rows.Count > 40)
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
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                dgvAnal.Rows.Clear();
                var listeAnalyse = from a in AppCode.ConnectionClassClinique.ListeDesAnalyses()
                                   join g in AppCode.ConnectionClassClinique.ListeDesGroupes()
                                   on a.NumeroGroupe equals g.NumeroGroupe
                                   where g.Groupe.StartsWith(cmbGroupe.Text, StringComparison.CurrentCultureIgnoreCase)
                                   select new
                                   {
                                       a.NumeroListeAnalyse,
                                       a.TypeAnalyse,
                                       a.FraisInternes,
                                       a.FraisConventionnes,
                                       a.Frais,
                                       g.Groupe,
                                   };

                foreach (var analyse in listeAnalyse)
                {
                    dgvAnal.Rows.Add
                       (
                           analyse.NumeroListeAnalyse.ToString(),
                           analyse.Groupe,
                           analyse.TypeAnalyse.ToUpper(),
                           analyse.Frais.ToString(),
                           analyse.FraisInternes,
                           analyse.FraisConventionnes
                       );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;

                }

            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "Liste des actes de _Impriméé_le_" + date + ".xls";
                //var pathFolder = "C:\\Dossier Clinique";
                //if (!System.IO.Directory.Exists(pathFolder))
                //{
                //    System.IO.Directory.CreateDirectory(pathFolder);
                //}
                //pathFolder = pathFolder + "\\Rapport des Conventionnes";
                //if (!System.IO.Directory.Exists(pathFolder))
                //{
                //    System.IO.Directory.CreateDirectory(pathFolder);
                //}
                //sfd.InitialDirectory = pathFolder;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToCsV1(dgvAnal, sfd.FileName); // Here dataGridview1 is your grid view name
                }
                System.Diagnostics.Process.Start(sfd.FileName);
            }
            catch (Exception)
            {
            }
        }

        private void ToCsV1(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 1; j < dGV.Columns.Count-2; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 1; j < dGV.Rows[i].Cells.Count-2; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();



        }

        private void cmbGroupe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var a in AppCode.ConnectionClassClinique.ListeDesGroupes(cmbGroupe.Text))
                 comboBox1.Items.Add(a.Libelle);
                dgvAnal.Rows.Clear();
                var listeAnalyse = from a in AppCode.ConnectionClassClinique.ListeDesAnalyses()
                                   join g in AppCode.ConnectionClassClinique.ListeDesGroupes()
                                   on a.NumeroGroupe equals g.NumeroGroupe
                                   where g.Groupe.StartsWith(cmbGroupe.Text, StringComparison.CurrentCultureIgnoreCase)
                                   where a.TypeAnalyse.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                                   select new
                                   {
                                       a.NumeroListeAnalyse,
                                       a.TypeAnalyse,
                                       a.FraisInternes,
                                       a.FraisConventionnes,
                                       a.Frais,
                                       g.Groupe,g.Libelle
                                   };

                foreach (var analyse in listeAnalyse)
                {
                    dgvAnal.Rows.Add
                    (
                        analyse.NumeroListeAnalyse.ToString(),
                        analyse.Groupe + " " + analyse.Libelle,
                        analyse.TypeAnalyse.ToUpper(),
                        analyse.Frais.ToString(),
                        analyse.FraisInternes,
                        analyse.FraisConventionnes
                    );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;

                }
            }
            catch { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvAnal.Rows.Clear();
                var listeAnalyse = from a in AppCode.ConnectionClassClinique.ListeDesAnalyses()
                                   join g in AppCode.ConnectionClassClinique.ListeDesGroupes()
                                   on a.NumeroGroupe equals g.NumeroGroupe
                                   where g.Libelle.StartsWith(comboBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                                   where a.TypeAnalyse.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                                   select new
                                   {
                                       a.NumeroListeAnalyse,
                                       a.TypeAnalyse,
                                       a.FraisInternes,
                                       a.FraisConventionnes,
                                       a.Frais,
                                       g.Groupe,g.Libelle
                                   };

                foreach (var analyse in listeAnalyse)
                {
                    dgvAnal.Rows.Add
                    (
                        analyse.NumeroListeAnalyse.ToString(),
                        analyse.Groupe + " " + analyse.Libelle,
                        analyse.TypeAnalyse.ToUpper(),
                        analyse.Frais.ToString(),
                        analyse.FraisInternes,
                        analyse.FraisConventionnes
                    );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;

                }
            }
            catch { }
        }
    }
}
