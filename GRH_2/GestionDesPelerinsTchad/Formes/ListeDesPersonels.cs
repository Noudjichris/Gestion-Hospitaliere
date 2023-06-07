using System;
using System.Security;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class ListeDesPersonelsFrm : Form
    {
        public ListeDesPersonelsFrm()
        {
            InitializeComponent();
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control,SystemColors.Control, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(255, 255, 128), 2);
            var area1 = new Rectangle(0, 0, this.groupBox5.Width - 4, this.groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 128), LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 2);
            var area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            var frm = new PersonnelFrm();
           
            frm.btnAjouter.Enabled = true;
            frm.ShowDialog();
            ListeDesPersonnels();
        }

        private void btnModifierPersonnel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if(string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    PersonnelFrm .numeroMatricule= dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    PersonnelFrm.etat = "2";
                    if (PersonnelFrm.ShowBox())
                    {
                        ListeDesPersonnels();
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste personnel", ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    if (MonMessageBox.ShowBox("Voulez vous supprimer les données de  " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() +
                        " " + dataGridView1.SelectedRows[0].Cells[2].Value.ToString() + "?", "Demande confirmation") == "1")
                    {
                        ConnectionClass.SupprimerUnPersonnel(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        ListeDesPersonnels();
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste personnel", ex);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var frmPersonnel = new InformationPersonnelFrm();
                    var numeroMatricule = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    var dtpersonnel = ConnectionClass.ListeDesPersonnelParNumeroMatricule(numeroMatricule);
                    var dtService = ConnectionClass.ListeDesServices(numeroMatricule);
                    var dtsalaire = ConnectionClass.ListeSalaire(dtpersonnel.Rows[0].ItemArray[1].ToString(),
                        dtpersonnel.Rows[0].ItemArray[2].ToString());
                    frmPersonnel.lblMatricule.Text = dtpersonnel.Rows[0].ItemArray[0].ToString();
                    frmPersonnel.lblNom.Text = dtpersonnel.Rows[0].ItemArray[1].ToString();
                    frmPersonnel.lblPrenom.Text = dtpersonnel.Rows[0].ItemArray[2].ToString();
                    var datenaissance = DateTime.Parse(dtpersonnel.Rows[0].ItemArray[3].ToString());
                    frmPersonnel.lblNaissance.Text = datenaissance.ToShortDateString();
                    frmPersonnel.lblLieuNaissance.Text = dtpersonnel.Rows[0].ItemArray[4].ToString();
                    frmPersonnel.lblAdresse.Text = dtpersonnel.Rows[0].ItemArray[5].ToString();
                    frmPersonnel.lblTelephone.Text = dtpersonnel.Rows[0].ItemArray[6].ToString() + "/" +
                                                     dtpersonnel.Rows[0].ItemArray[7].ToString();
                    frmPersonnel.lblEmail.Text = dtpersonnel.Rows[0].ItemArray[8].ToString();
                    frmPersonnel.lblDirection.Text = dtpersonnel.Rows[0].ItemArray[11].ToString();
                    frmPersonnel.lblEchelon.Text = dtService.Rows[0].ItemArray[4].ToString();
                    frmPersonnel.lblDateService.Text =
                        DateTime.Parse(dtService.Rows[0].ItemArray[1].ToString()).ToShortDateString();
                    frmPersonnel.lblPoste.Text = dtService.Rows[0].ItemArray[2].ToString();
                    var salaire = 0m;
                    var grille = 0m;
                    var indice = 0m;
                    if (dtsalaire.Rows.Count > 0)
                    {
                        salaire = decimal.Parse(dtsalaire.Rows[0].ItemArray[0].ToString());
                        grille = decimal.Parse(dtsalaire.Rows[0].ItemArray[1].ToString());
                        indice = decimal.Parse(dtsalaire.Rows[0].ItemArray[2].ToString());
                    }
                    frmPersonnel.lblSalaire.Text = salaire.ToString();
                    frmPersonnel.lblCategorie.Text = dtService.Rows[0].ItemArray[5].ToString();
                    frmPersonnel.lblTypeContrat.Text = dtService.Rows[0].ItemArray[9].ToString();
                    frmPersonnel.lblGrille.Text = grille.ToString();
                    frmPersonnel.lblAnciennete.Text = dtService.Rows[0].ItemArray[6].ToString();
                    frmPersonnel.lblNoCNPS.Text = dtService.Rows[0].ItemArray[7].ToString();
                    frmPersonnel.lblDiplome.Text = dtService.Rows[0].ItemArray[8].ToString();
                    //var dtDivision = ConnectionClass.ListeDepartement(frmPersonnel.lblDivision.Text);
                    //frmPersonnel.lblDirection.Text = dtDivision.Rows[0].ItemArray[1].ToString();
                    frmPersonnel.lblIndice.Text = indice.ToString();

                    var anneeActuel = DateTime.Now.Year;
                    var moisActuel = DateTime.Now.Month;
                    var mois = datenaissance.Month;
                    var age = new int();
                    if (moisActuel >= mois)
                    {
                        age = anneeActuel - datenaissance.Year;
                    }
                    else
                    {
                        age = anneeActuel - datenaissance.Year - 1;
                    }
                    var etat = "";
                    if (age >= 60)
                    {
                        etat = "Retraité";
                        frmPersonnel.lblEtat.ForeColor = Color.Red;
                    }
                    else
                    {
                        etat = "En service";
                        frmPersonnel.lblEtat.ForeColor = Color.GreenYellow;
                    }

                    frmPersonnel.lblAge.Text = age.ToString();
                    frmPersonnel.lblEtat.Text = etat;
                    var image = dtpersonnel.Rows[0].ItemArray[10].ToString();

                    if (System.IO.File.Exists(image))
                    {
                        frmPersonnel.pictureBox2.Image = Image.FromFile(image);
                    }
                    else
                    {
                        frmPersonnel.pictureBox2.Image = null;
                    }
                    frmPersonnel.lblSexe.Text = dtpersonnel.Rows[0].ItemArray[9].ToString().ToUpper();

                    //var dtFormation = ConnectionClass.ListeFormation(numeroMatricule);
                    //var stringBuilder = new StringBuilder();
                    ////foreach (DataRow dtRow in dtFormation.Rows)
                    //{
                    //    stringBuilder.AppendLine("");
                    //    stringBuilder.AppendFormat("{0,15} {1,-20} {2,100}", "", "Formé(e) en ",
                    //        dtRow.ItemArray[0].ToString());
                    //    stringBuilder.AppendLine("");
                    //    stringBuilder.AppendFormat("{0,15} {1,-20} {2,100}", "", "Le  ", dtRow.ItemArray[1].ToString());
                    //    stringBuilder.AppendLine("");
                    //    stringBuilder.AppendFormat("{0,15} {1,-20} {2,100}", "", "Pour une durée de  ", dtRow.ItemArray[2].ToString() + " jours");
                    //    stringBuilder.AppendLine("");
                    //}
                    //frmPersonnel.richTextBox1.Text = stringBuilder.ToString();
                    //frmPersonnel.Height = Height;
                    frmPersonnel.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Information formation", ex);
            }
        }

        private Bitmap document;
        string titreImpression;
        //liste des personnels
        private void ListeDesPersonnels()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var dt = ConnectionClass.ListeDepartement();
                var j = 0;
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtPersonnel = ConnectionClass.ListeDesPersonnelParNomPersonnelEtService(textBox1.Text, dt.Rows[i].ItemArray[1].ToString());
                    if (dtPersonnel.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Add(
                             "",  dt.Rows[i].ItemArray[1].ToString(), "", "", "", "", "","","",""
                             );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255,255,192);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);

                        foreach (DataRow dtRow in dtPersonnel.Rows)
                        {
                            var dtSalaire = ConnectionClass.ListeSalaire(dtRow.ItemArray[0].ToString());
                            var salaire = 0.0;
                            if (dtSalaire.Rows.Count > 0)
                                if (double.TryParse(dtSalaire.Rows[0].ItemArray[0].ToString(), out salaire))
                                {
                                }
                            dataGridView1.Rows.Add(
                                dtRow.ItemArray[0].ToString(),
                                dtRow.ItemArray[1].ToString(),
                                dtRow.ItemArray[2].ToString(),
                                dtRow.ItemArray[12].ToString(),
                                dtRow.ItemArray[15].ToString(),
                                dtRow.ItemArray[14].ToString(),
                                dtRow.ItemArray[17].ToString(),
                               dtRow.ItemArray[25].ToString(),
                                dtRow.ItemArray[18].ToString(),
                                dtRow.ItemArray[19].ToString(),
                                salaire,
                                dtRow.ItemArray[9].ToString()
                                );
                            j++;
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                                var retraite = Convert.ToInt32(row.Cells[8].Value.ToString());
                                var finContrat = Convert.ToInt32(row.Cells[9].Value.ToString());
                                if (retraite == 1 || finContrat == 1)
                                {
                                    row.DefaultCellStyle.BackColor = Color.Red;  //
                                    row.DefaultCellStyle.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
                lblNombre.Text =  j.ToString();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }


        private void btnIImprimante_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                        //    SaveFileDialog sfd = new SaveFileDialog();
                        //    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        //    sharpPDF.pdfDocument pdfDocument = new sharpPDF.pdfDocument("christian", "cdali");
                        //    var jour = DateTime.Now.Day;
                        //    var mois = DateTime.Now.Month;
                        //    var year = DateTime.Now.Year;
                        //    var hour = DateTime.Now.Hour;
                        //    var min = DateTime.Now.Minute;
                        //    var sec = DateTime.Now.Second;
                        //    var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                        //    var pathFolder = "C:\\Dossier Personnel";
                        //    if (!System.IO.Directory.Exists(pathFolder))
                        //    {
                        //        System.IO.Directory.CreateDirectory(pathFolder);
                        //    }
                        //    pathFolder = pathFolder + "\\Liste Personnel";
                        //    if (!System.IO.Directory.Exists(pathFolder))
                        //    {
                        //        System.IO.Directory.CreateDirectory(pathFolder);
                        //    }
                        //    sfd.InitialDirectory = pathFolder;
                        //    sfd.FileName = titreImpression + "_" + date + ".pdf";

                        //    var div = dataGridView1.Rows.Count / 44;
                        //    for (var i = 0; i <= div; i++)
                        //    {
                        //        if (checkBox1.Checked)
                        //        {
                        //            document = Impression.ImprimerLalisteDesPersonnelsAvecSalaire(dataGridView1, titreImpression, i, lblNombre.Text);
                        //        }
                        //        else
                        //        {
                        //            document = Impression.ImprimerLalisteDesPersonnels(dataGridView1, titreImpression, i, lblNombre.Text);
                        //        }
                        //        sharpPDF.pdfPage pageIndex = pdfDocument.addPage();

                        //        var inputImage = @"cdali" + i;
                        //        pdfDocument.addImageReference(document, inputImage);
                        //        sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                        //        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                        //    }
                        //    pdfDocument.createPDF(sfd.FileName);
                        //    System.Diagnostics.Process.Start(sfd.FileName);
                        //}
                    var div = dataGridView1.Rows.Count / 44;

                    if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDocument1.PrinterSettings;
                        for (var i = 0; i <= div; i++)
                        {
                            if (checkBox1.Checked)
                            {
                                document = Impression.ImprimerLalisteDesPersonnelsAvecSalaire(dataGridView1, titreImpression, i, lblNombre.Text);
                            }
                            else
                            {
                                document = Impression.ImprimerLalisteDesPersonnels(dataGridView1, titreImpression, i, lblNombre.Text);
                            }
                            printDocument1.Print();

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
   private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            e.Graphics.DrawImage(document, 0, 10, width, height);
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox2.Checked)
                {
                    titreImpression = "Liste general du personnel retraité";

                    dataGridView1.Rows.Clear();
                    checkBox3.Checked = false;
                    checkBox5.Checked = false;
                    checkBox4.Checked = false;

                    var dt = ConnectionClass.ListeDepartement();
                    var j = 0;
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dtPersonnel = ConnectionClass.ListeDesPersonnelParNomPersonnelEtServiceEtatRetratite(textBox1.Text, dt.Rows[i].ItemArray[1].ToString(),true);
                        if (dtPersonnel.Rows.Count > 0)
                        {
                            dataGridView1.Rows.Add(
                                 "", dt.Rows[i].ItemArray[1].ToString(), "", "", "", "", "", "", "", ""
                                 );
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192);
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
                          
                                foreach (DataRow dtRow in dtPersonnel.Rows)
                                {
                                    if (dtRow.ItemArray[19].ToString() == "1")
                                    dataGridView1.Rows.Add(
                                        dtRow.ItemArray[0].ToString(),
                                        dtRow.ItemArray[1].ToString(),
                                        dtRow.ItemArray[2].ToString(),
                                        dtRow.ItemArray[12].ToString(),
                                        dtRow.ItemArray[15].ToString(),
                                        dtRow.ItemArray[14].ToString(),
                                        dtRow.ItemArray[17].ToString(),
                                       dtRow.ItemArray[25].ToString(),
                                        dtRow.ItemArray[18].ToString(),
                                        dtRow.ItemArray[19].ToString()
                                        );
                                    j++;
                                }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (!string.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                                {
                                    var retraite = Convert.ToInt32(row.Cells[8].Value.ToString());
                                    var finContrat = Convert.ToInt32(row.Cells[9].Value.ToString());
                                    if (retraite == 1 || finContrat == 1)
                                    {
                                        row.DefaultCellStyle.BackColor = Color.Red;  //
                                        row.DefaultCellStyle.ForeColor = Color.White;
                                    }
                                }
                            }
                        }
                    }
                    lblNombre.Text =  j.ToString();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox3.Checked)
                {
                    titreImpression = "Liste general du personnel non retraité";
                    dataGridView1.Rows.Clear();
                    checkBox2.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                  
                var dt = ConnectionClass.ListeDepartement();
                var j = 0;
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtPersonnel = ConnectionClass.ListeDesPersonnelParNomPersonnelEtServiceEtatRetratite(textBox1.Text, dt.Rows[i].ItemArray[1].ToString(), false);
                    if (dtPersonnel.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Add(
                             "",  dt.Rows[i].ItemArray[1].ToString(), "", "", "", "", "","","",""
                             );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255,255,192);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
                      
                        foreach (DataRow dtRow in dtPersonnel.Rows)
                        {
                            if (dtRow.ItemArray[19].ToString() == "0")
                            dataGridView1.Rows.Add(
                                dtRow.ItemArray[0].ToString(),
                                dtRow.ItemArray[1].ToString(),
                                dtRow.ItemArray[2].ToString(),
                                dtRow.ItemArray[12].ToString(),
                                dtRow.ItemArray[15].ToString(),
                                dtRow.ItemArray[14].ToString(),
                                dtRow.ItemArray[17].ToString(),
                               dtRow.ItemArray[25].ToString(),
                                dtRow.ItemArray[18].ToString(),
                                dtRow.ItemArray[19].ToString()
                                );
                            j++;
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                                var retraite = Convert.ToInt32(row.Cells[8].Value.ToString());
                                var finContrat = Convert.ToInt32(row.Cells[9].Value.ToString());
                                if (retraite == 1 || finContrat == 1)
                                {
                                    row.DefaultCellStyle.BackColor = Color.Red;  //
                                    row.DefaultCellStyle.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
                lblNombre.Text =  j.ToString();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox4.Checked)
                {
                    titreImpression = "Liste general du personnel à contrat en cour";
                    dataGridView1.Rows.Clear();
                    checkBox5.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                   
                var dt = ConnectionClass.ListeDepartement();
                var j = 0;
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtPersonnel = ConnectionClass.ListeDesPersonnelParNomPersonnelEtServiceEtatContrat
                        (textBox1.Text, dt.Rows[i].ItemArray[1].ToString(),false);
                    if (dtPersonnel.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Add(
                             "",  dt.Rows[i].ItemArray[1].ToString(), "", "", "", "", "","","",""
                             );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255,255,192);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
                      
                        foreach (DataRow dtRow in dtPersonnel.Rows)
                        {

                            dataGridView1.Rows.Add(
                                dtRow.ItemArray[0].ToString(),
                                dtRow.ItemArray[1].ToString(),
                                dtRow.ItemArray[2].ToString(),
                                dtRow.ItemArray[12].ToString(),
                                dtRow.ItemArray[15].ToString(),
                                dtRow.ItemArray[14].ToString(),
                                dtRow.ItemArray[17].ToString(),
                               dtRow.ItemArray[25].ToString(),
                                dtRow.ItemArray[18].ToString(),
                                dtRow.ItemArray[19].ToString()
                                );
                            j++;
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                                var retraite = Convert.ToInt32(row.Cells[8].Value.ToString());
                                var finContrat = Convert.ToInt32(row.Cells[9].Value.ToString());
                                if (retraite == 1 || finContrat == 1)
                                {
                                    row.DefaultCellStyle.BackColor = Color.Red;  //
                                    row.DefaultCellStyle.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
                lblNombre.Text =  j.ToString();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox5.Checked)
                {
                    titreImpression = "Liste general du personnel à fin de contrat";
                    dataGridView1.Rows.Clear();
                    checkBox4.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    
                var dt = ConnectionClass.ListeDepartement();
                var j = 0;
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtPersonnel = ConnectionClass.ListeDesPersonnelParNomPersonnelEtServiceEtatContrat(textBox1.Text, dt.Rows[i].ItemArray[1].ToString(),true);
                    if (dtPersonnel.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Add(
                             "",  dt.Rows[i].ItemArray[1].ToString(), "", "", "", "", "","","",""
                             );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255,255,192);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);
                      
                        foreach (DataRow dtRow in dtPersonnel.Rows)
                        {
                            dataGridView1.Rows.Add(
                                dtRow.ItemArray[0].ToString(),
                                dtRow.ItemArray[1].ToString(),
                                dtRow.ItemArray[2].ToString(),
                                dtRow.ItemArray[12].ToString(),
                                dtRow.ItemArray[15].ToString(),
                                dtRow.ItemArray[14].ToString(),
                                dtRow.ItemArray[17].ToString(),
                               dtRow.ItemArray[25].ToString(),
                                dtRow.ItemArray[18].ToString(),
                                dtRow.ItemArray[19].ToString()
                                );
                            j++;
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                                var retraite = Convert.ToInt32(row.Cells[8].Value.ToString());
                                var finContrat = Convert.ToInt32(row.Cells[9].Value.ToString());
                                if (retraite == 1 || finContrat == 1)
                                {
                                    row.DefaultCellStyle.BackColor = Color.Red;  //
                                    row.DefaultCellStyle.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
                lblNombre.Text =  j.ToString();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                titreImpression  = "Liste general du personnel";
                ListeDesPersonnels();
            }
            catch { }
        }

        private void ListeDesPersonels_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.RowTemplate.Height = 23;
                 if(GestionAcademique.LoginFrm.typeUtilisateur=="util")
                {
                    button1.Enabled = false;
                    button4.Enabled = false;
                    button8.Enabled = false;
                }
                var typeContrat = new string[]
                    {
                        "<Recherche par type contrat>",
                        "CDD",
                        "CDI",
                        "Decisionaire",
                        "Decreté",
                        "Detaché",
                        "Journalier",
                           "Prestataire",
                        "Stage"
                    };
                cmbTypeContrat.Items.AddRange(typeContrat);
                titreImpression = "Liste general du personnel";
                panel1.Location = new Point(dataGridView1.Width + 15, panel1.Location.Y);
                if (GestionAcademique.LoginFrm.typeUtilisateur == "")
                {
                    button1.Enabled = false;
                    button11.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button6.Enabled = false;
                    button7.Enabled = false;
                    button9.Enabled = false;
                    btnIImprimante.Enabled = false;
                    
                }
                cmbTypeContrat.Text = "<Recherche par type contrat>";
                btnFermer.Location = new Point(Width - 45, btnFermer.Location.Y);
                ListeDesPersonnels();
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var rootPath = GlobalVariable.rootPathPersonnel;
                    {

                        var nomPersonnel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " "
                                            + dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                        rootPath = rootPath + nomPersonnel;
                        if (MonMessageBox.ShowBox("Voulez vous creer le dossier de " + nomPersonnel + "?", "Confirmation") == "1")
                        {

                            //= @"\\192.168.115.27\New\new2"; //I assume that: Your file server ip is 192.168.115.27
                            //                             //you have shared the "New" folder and set Full control, then you want to create "new2" folder and add domain account for "new2" ACL
                            //if (System.IO.Directory.Exists(rootPath))
                            //{
                            //    return;
                            //}
                            //System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(rootPath );
                            //System.IO.DirectoryInfo dInfo = new System.IO.DirectoryInfo(rootPath);
                            // Get a DirectorySecurity object that represents the
                            // current security settings.
                            //System.Security.AccessControl.DirectorySecurity dSecurity = dInfo.GetAccessControl();


                            //string domainName = "Administrateur";
                            //string identity = "Admin2020";
                            // Add the FileSystemAccessRule to the security settings.        
                            //System.Security.AccessControl. FileSystemAccessRule fsr = new System.Security.AccessControl.FileSystemAccessRule(domainName + @"\" + identity,
                            //                                                            System.Security.AccessControl.FileSystemRights.ReadData |
                            //                                                            System.Security.AccessControl.FileSystemRights.WriteData |
                            //                                                            System.Security.AccessControl.FileSystemRights.Modify,
                            //                                                            System.Security.AccessControl.AccessControlType.Allow);

                            //dSecurity.AddAccessRule(fsr);
                            //// Set the new access settings.
                            //dInfo.SetAccessControl(dSecurity);
                            if (!System.IO.Directory.Exists(rootPath))
                            {
                                System.IO.Directory.CreateDirectory(rootPath);
                                MonMessageBox.ShowBox("Dossier du personnel " + nomPersonnel + " crée avec succés", "Inforation");
                            }
                            else
                            {
                                MonMessageBox.ShowBox("Le dossier de " + nomPersonnel + " existe dans la machine", "Erreur");
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("creation du dossier", ex);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var frm = new DossierForm();
                    frm.Size = new Size(Width, Height);
                    frm.Location = new Point(Location.X, Location.Y);
                    frm.nomPersonnel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " "
                                                     + dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    frm.state = "1";
                    //frm.rootPath = AppCode.GlobalVariable.rootPathPersonnel + nomPersonnel;
                    frm.ShowDialog();
                    //var rootPath =  @"C://Program Files (x86)//CDALI//GesPersonnel//Dossiers du Personnel//" + nomPersonnel;
                    //if (System.IO.Directory.Exists(rootPath))
                    //{
                    //    System.Diagnostics.Process.Start(rootPath);
                    //}
                    //else
                    //{
                    //    System.IO.Directory.CreateDirectory(rootPath);
                    //    System.Diagnostics.Process.Start(rootPath);
                    //}

                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("creation du dossier", ex);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var nomPersonnel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " "
                                               + dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    var rootPath = GlobalVariable.rootPathPersonnel + nomPersonnel;
                    //if (System.IO.Directory.Exists(rootPath))
                    var open = new OpenFileDialog();
                    //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    open.Filter = "Tous les fichiers (all files(*.*)|*.*";
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (open.CheckFileExists)
                        {
                            var fileName = open.FileName;

                            if (!System.IO.Directory.Exists(rootPath))
                            {
                                System.IO.Directory.CreateDirectory(rootPath);
                            }
                            rootPath = rootPath + @"//" + System.IO.Path.GetFileName(fileName);
                            if (!System.IO.File.Exists(rootPath))
                            {
                                System.IO.File.Copy(fileName, rootPath);
                                MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                            }
                            else
                            {
                                if (MonMessageBox.ShowBox("Ce fichier existe deja Voulez vous faire une copie?", "confirmation") == "1")
                                {
                                    var rootPath1 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie1" + rootPath.Substring(rootPath.LastIndexOf("."));
                                    if (!System.IO.File.Exists(rootPath1))
                                    {
                                        System.IO.File.Copy(fileName, rootPath1);
                                        MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                    }
                                    else
                                    {

                                        var rootPath2 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie2" + rootPath.Substring(rootPath.LastIndexOf("."));
                                        if (!System.IO.File.Exists(rootPath2))
                                        {
                                            System.IO.File.Copy(fileName, rootPath2);
                                            MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                        }
                                        else
                                        {
                                            var rootPath3 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie3" + rootPath.Substring(rootPath.LastIndexOf("."));
                                            if (!System.IO.File.Exists(rootPath3))
                                            {
                                                System.IO.File.Copy(fileName, rootPath3);
                                                MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                            }
                                            else
                                            {
                                                var rootPath4 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie4" + rootPath.Substring(rootPath.LastIndexOf("."));
                                                if (!System.IO.File.Exists(rootPath4))
                                                {
                                                    System.IO.File.Copy(fileName, rootPath4);
                                                    MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                                }
                                            }
                                        }
                                    }

                                }
                            }


                        }

                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données du personnel. Puis réessayez ", "Information Photo");
                }
            }

            catch (Exception ex) { MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var open = new OpenFileDialog();
                    //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    open.Filter = "Image Files (*.jpg)|*.jpg|all files(*.*)|*.*";
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (open.CheckFileExists)
                        {
                            var _photo = System.IO.Path.GetFileName(open.FileName);
                            var imagePath = GlobalVariable.rootPathPersonnel +
                                 dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " " + dataGridView1.SelectedRows[0].Cells[2].Value.ToString() + "//";
                            if (!System.IO.Directory.Exists(imagePath))
                            {
                                System.IO.Directory.CreateDirectory(imagePath);
                            }
                            imagePath = imagePath + _photo;
                            if (!System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Copy(open.FileName, imagePath);
                                //pictureBox1.Image = Image.FromFile(open.FileName);
                                ConnectionClass.InsereImage(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), imagePath);
                                MonMessageBox.ShowBox("Photo transferée avec succés ", "Information Photo");
                            }
                            else
                            {
                                if (MonMessageBox.ShowBox("Ce fichier existe deja Voulez vous faire une copie?", "confirmation") == "1")
                                {
                                    var rootPath1 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie1" + imagePath.Substring(imagePath.LastIndexOf("."));
                                    if (!System.IO.File.Exists(rootPath1))
                                    {
                                        System.IO.File.Copy(open.FileName, rootPath1);
                                        MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                    }
                                    else
                                    {

                                        var rootPath2 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie2" + imagePath.Substring(imagePath.LastIndexOf("."));
                                        if (!System.IO.File.Exists(rootPath2))
                                        {
                                            System.IO.File.Copy(open.FileName, rootPath2);
                                            MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                        }
                                        else
                                        {
                                            var rootPath3 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie3" + imagePath.Substring(imagePath.LastIndexOf("."));
                                            if (!System.IO.File.Exists(rootPath3))
                                            {
                                                System.IO.File.Copy(open.FileName, rootPath3);
                                                MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                            }
                                            else
                                            {
                                                var rootPath4 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie4" + imagePath.Substring(imagePath.LastIndexOf("."));
                                                if (!System.IO.File.Exists(rootPath4))
                                                {
                                                    System.IO.File.Copy(open.FileName, rootPath4);
                                                    MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données du personnel. Puis réessayez ", "Information Photo");
                }
            }

            catch (Exception ex) { MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListeDesPersonnels();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (GestionAcademique.LoginFrm.typeUtilisateur != "util")
                btnModifierPersonnel_Click(null, null);
        }
        private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var dt = ConnectionClass.ListeDepartement();
                var j = 0;
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dtPersonnel = ConnectionClass.ListeDesPersonnelParNomPersonnelEtServiceEtContrat(textBox1.Text, dt.Rows[i].ItemArray[1].ToString(),cmbTypeContrat.Text);
                    if (dtPersonnel.Rows.Count > 0)
                    {
                        dataGridView1.Rows.Add(
                             "", dt.Rows[i].ItemArray[1].ToString(), "", "", "", "", "", "", "", ""
                             );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold);

                        foreach (DataRow dtRow in dtPersonnel.Rows)
                        {
                            if( dtRow.ItemArray[25].ToString()==cmbTypeContrat.Text)
                            dataGridView1.Rows.Add(
                                dtRow.ItemArray[0].ToString(),
                                dtRow.ItemArray[1].ToString(),
                                dtRow.ItemArray[2].ToString(),
                                dtRow.ItemArray[12].ToString(),
                                dtRow.ItemArray[15].ToString(),
                                dtRow.ItemArray[14].ToString(),
                                dtRow.ItemArray[17].ToString(),
                               dtRow.ItemArray[25].ToString(),
                                dtRow.ItemArray[18].ToString(),
                                dtRow.ItemArray[19].ToString()
                                );
                            j++;
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                                var retraite = Convert.ToInt32(row.Cells[8].Value.ToString());
                                var finContrat = Convert.ToInt32(row.Cells[9].Value.ToString());
                                if (retraite == 1 || finContrat == 1)
                                {
                                    row.DefaultCellStyle.BackColor = Color.Red;  //
                                    row.DefaultCellStyle.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
                lblNombre.Text =  j.ToString();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
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
                    sfd.FileName = titreImpression + "_" + date + ".pdf";

                    var div = dataGridView1.Rows.Count / 44;
                    for (var i = 0; i <= div; i++)
                    {
                        if (checkBox1.Checked)
                        {
                            document = Impression.ImprimerLalisteDesPersonnelsAvecSalaire(dataGridView1, titreImpression, i, lblNombre.Text);
                        }
                        else
                        {
                            document = Impression.ImprimerLalisteDesPersonnels(dataGridView1, titreImpression, i, lblNombre.Text);
                        }
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

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (MonMessageBox.ShowBox("Ce dossier pourrait comporter des fichiers. Voulez vous vraiment supprimer ?", "Confirmation") == "1")
                {
                    var nomPersonnel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " "
                                       + dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    var rootPath = GlobalVariable.rootPathPersonnel;
                    rootPath = rootPath + nomPersonnel;
                    if (System.IO.Directory.Exists(rootPath))
                    {
                        System.IO.Directory.Delete(rootPath);
                        MonMessageBox.ShowBox("Dossier du personnel " + nomPersonnel + " supprimée avec succés", "Inforation");
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dGV.Columns.Count ; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dGV.Rows[i].Cells.Count ; j++)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(1254);
                byte[] output = utf16.GetBytes(stOutput);
                System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {try
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
                
                    sfd.FileName = "Liste personnel " + date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
              GestionPharmacetique.  MonMessageBox.ShowBox("", ex);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AppCode.ConnectionClassPharmacie.AjouterEmployeDuneEntreprise(dataGridView1, 10, "");
        }
    }
}
