using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LabMonitoring.AppCode;
using GestionPharmacetique;
using System.Drawing.Drawing2D;

namespace LabMonitoring.Formes
{
    public partial class ExamFrm : Form
    {
        public ExamFrm()
        {
            InitializeComponent();
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void ExamFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 1);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                 SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        string state;
        private void ExamFrm_Load(object sender, EventArgs e)
        {
            try
            {
                cmbPrescripteur.Items.Clear();
                cmbPrescripteur.Items.Add(" ");
                var dt = AppCode.ConnectionClass.ListePrescripteurs();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    cmbPrescripteur.Items.Add(dt.Rows[i].ItemArray[1].ToString());
                }
                etat = "1";
                Column5.Width = 40;
                Column15.Width = 45;

                Column14.Width = 80;
                Column1.Width = 40;
                dgvListeExam.RowTemplate.Height = 30;
                dgvResultatPatient.RowTemplate.Height = 30;
                etatDesExamens = "autres";
                txtExamListe_TextChanged(null, null);
                ListeAnalyse();
                state = "1";
                var aspect = new string[]
            {
                "ASPECT MACROCOSCOPIQUE",
                "ASPECT MICROSCOPIQUE",
                "CYTOLOGIE QUALITATIVE",
                "CYTOLOGIE QUANTITATIVE",
                "ETAT COLORE",
                "ISOLEMENT",
                "IDENTIFICATION",
                "ANTIBIOGRAMME",
                "ETAT FRAIS"

            };

                var l = from a in aspect
                        orderby a
                        select a;
                cmbAspect.Items.Clear();
                foreach (var a in l)
                {
                    cmbAspect.Items.Add(a.ToUpper());
                }

            }
            catch (Exception)
            {
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtValeurNormal.Focus();
            }
        }
        int id; string etat;

        void ListeAnalyse()
        {
            try
            {
                var listeAnalyse = ConnectionClass.ListeExamen();
                var listeGrp = ConnectionClass.ListeGroupeExamen();
                var liste = from e in listeAnalyse
                            join g in listeGrp
                            on e.NumeroGroupe equals g.NumeroGroupe
                            where e.Analyse.StartsWith(txtTypeExamen.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby e.Analyse
                            select new
                            {
                                e.NumeroAnalyse,
                                e.Analyse,
                                g.NumeroGroupe,
                                g.GroupeAnalyse, e.ValeurNormal, e.Unite
                            };
                dataGridView1.Rows.Clear();
                liste.Count();
                foreach (var ex in liste)
                {
                    dataGridView1.Rows.Add(ex.NumeroAnalyse, ex.NumeroGroupe, ex.GroupeAnalyse, ex.Analyse, ex.ValeurNormal, ex.Unite);
                    Column7.Image = global::LabMonitoring.Properties.Resources.close11;
                    Column8.Image = global::LabMonitoring.Properties.Resources.edit;
                    Column9.Image = global::LabMonitoring.Properties.Resources.settings1;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ListeAnalyse();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                if (e.ColumnIndex == 8)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        if (MonMessageBox.ShowBox("Voulez vous supprimer cet examen?", "Confirmation", "confirmation") == "1")
                        {
                            ConnectionClass.SupprimerUnAnalyse(dataGridView1);
                            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                            etat = "0";
                        }
                    }
                }
                else if (e.ColumnIndex == 6)
                {
                    etat = "1";
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    txtTypeExamen.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    txtValeurNormal.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                    txtUnite.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                }

                else if (e.ColumnIndex == 7)
                {
                    dataGridViewTextBoxColumn6.HeaderText = "APPRECIATIONS";
                    lblExamen.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    numExam = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ListeDesAppreciations();
                }
            }
        }

        private void txtApp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (lblExamen.Text != "")
                    {
                        if (!checkBox3.Checked)
                        {
                            var liste = ConnectionClass.ListeExamen();
                            var l = from ls in liste
                                    where ls.Analyse.ToUpper() == lblExamen.Text.ToUpper()
                                    select ls.NumeroAnalyse;
                            foreach (var ex in l)
                                dgvResultat.Rows.Add(0, ex, txtResult.Text);

                            button10_Click(null, null);
                            txtResult.Text = "";
                        }
                        else
                        {
                            if (comboBox2.Text != null)
                            {
                                var liste = ConnectionClass.ListeDesAppreciations();
                                var l = from ls in liste
                                        where ls.Appreciation.ToUpper() == comboBox2.Text.ToUpper()
                                        where ls.NumeroAnalyse == numExam
                                        select ls.IDAppreciation;
                                foreach (var ex in l)
                                    dgvResultat.Rows.Add(0, ex, txtResult.Text);
                                txtResult.Text = "";
                                button10_Click(null, null);
                            }
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner le type d'examen sur la liste", "Erreur", "erreur.png");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        Bitmap bitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //if(printDocument1.==System.Drawing.Printing.PaperKind.A4)
                var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width;
                var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
                e.Graphics.DrawImage(bitmap, -5, 10, width, height);
                e.HasMorePages = false;
            }
            catch { }
        }

        void ListeDesResultats()
        {
            try
            {
                var listeResultat = ConnectionClass.ListeDesResultats();
                var listeApp = ConnectionClass.ListeDesAppreciations();
                var liste = from lr in listeResultat
                            join le in listeApp
                            on lr.IDAppreciation equals
                            le.IDAppreciation
                            where le.Appreciation == comboBox2.Text
                            select new
                            {
                                lr.IDAppreciation, lr.Resultat, lr.IDResultat
                            };
                dgvResultat.Rows.Clear();
                liste.Count();
                foreach (var examen in liste)
                {
                    dgvResultat.Rows.Add(examen.IDResultat, examen.IDAppreciation, examen.Resultat);
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Liste examen", ex); }
        }

        void ListeDesAppreciations()
        {
            try
            {
                var listeApp = ConnectionClass.ListeDesAppreciations();
                var listeExamen = ConnectionClass.ListeExamen();
                var liste = from lr in listeApp
                            join le in listeExamen
                            on lr.NumeroAnalyse equals
                            le.NumeroAnalyse
                            where le.Analyse == lblExamen.Text
                            select new
                            {
                                lr.NumeroAnalyse,
                                lr.Appreciation,
                                lr.IDAppreciation
                            };
                dgvResultat.Rows.Clear();
                comboBox2.Items.Clear();
                liste.Count();
                foreach (var examen in liste)
                {
                    comboBox2.Items.Add(examen.Appreciation);
                    dgvResultat.Rows.Add(examen.IDAppreciation, examen.NumeroAnalyse, examen.Appreciation);
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Liste examen", ex); }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                if (ConnectionClass.EnregistrerLesResultats(dgvResultat))
                {
                    //MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");

                    ListeDesResultats();
                }
            }
            else if (!checkBox3.Checked)
            {
                if (ConnectionClass.EnregistrerLesAppreciations(dgvResultat))
                {
                    //MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
                    ListeDesAppreciations();
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dgvResultat.SelectedRows.Count > 0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation", "confirmation.png") == "1")
                {
                    if (checkBox3.Checked)
                    {
                        if (ConnectionClass.SupprimerLesResultats(dgvResultat))
                        {
                            ListeDesResultats();
                        }
                    }
                    else if (!checkBox3.Checked)
                    {
                        if (ConnectionClass.SupprimerLesAppreciations(dgvResultat))
                        {
                            ListeDesAppreciations();
                        }
                    }
                }
            }
        }

        private void txtExamListe_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var listeAnalyse = ConnectionClass.ListeExamen();
                var listeGrp = ConnectionClass.ListeGroupeExamen();
                var liste = from ee in listeAnalyse
                            join g in listeGrp
                            on ee.NumeroGroupe equals g.NumeroGroupe
                            where ee.Analyse.StartsWith(txtExamListe.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby ee.Analyse
                            select new
                            {
                                ee.NumeroAnalyse,
                                ee.Analyse, ee.ValeurNormal,
                                g.NumeroGroupe,
                                g.GroupeAnalyse, ee.Unite
                            };
                dgvListeExam.Rows.Clear();
                foreach (var ex in liste)
                {
                    dgvListeExam.Rows.Add(ex.NumeroAnalyse, ex.Analyse, ex.ValeurNormal, ex.Unite);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            dgvResultat.Rows.Clear();
            if (!checkBox3.Checked)
            {
                dataGridViewTextBoxColumn6.HeaderText = "APPRECIATIONS"; ;
            }
            else
            {

                dataGridViewTextBoxColumn6.HeaderText = "RESULTATS";
            }

            if (lblExamen.Text != null)
            {
                if (!checkBox3.Checked)
                {
                    ListeDesAppreciations();
                }
                else
                {
                    ListeDesResultats();
                }
            }
            else
            {
                dgvResultat.Rows.Clear();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (etatResultat == "1")
            {
                if (dgvResultatPatient.SelectedRows.Count > 0)
                {
                    dgvResultatPatient.SelectedRows[0].Cells[3].Value = cmbResultat.Text;
                }
            }
            else if (etatResultat == "2")
            {
                dgvResultatPatient.Rows.Add(numExam, examen, cmbResultat.Text, "");
            }

        }

        string examen;
        int numExam;

        private void button12_Click(object sender, EventArgs e)
        {
            if (dgvListeExam.SelectedRows.Count > 0)
            {
                cmbResultat.Items.Clear();
                var appreciation = ConnectionClass.ListeDesAppreciations();
                var liste = from r in appreciation
                            where r.NumeroAnalyse == Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString())
                            select r;
                if (liste.Count() > 0)
                {
                    foreach (var r in liste)
                    {
                        cmbResultat.Items.Add(r.Resultat);

                    }
                    examen = dgvListeExam.SelectedRows[0].Cells[1].Value.ToString();
                    numExam = Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
        }
        string etatDesExamens;
        private void dgvListeExam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    numExam = Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString());
                    examen = dgvListeExam.SelectedRows[0].Cells[1].Value.ToString();
                    var valeurNormal = dgvListeExam.SelectedRows[0].Cells[2].Value.ToString();
                    var unite = dgvListeExam.SelectedRows[0].Cells[3].Value.ToString();
                    lblExamResultat.Text = examen;
                    cmbResultat.Items.Clear();
                    if (examen.ToUpper() == "ECBU")
                    {
                        etatResultat = "2";
                        etatDesExamens = "ecbu";
                        var appreciation = ConnectionClass.ListeDesAppreciations();
                        var liste = from r in appreciation
                                    where r.NumeroAnalyse == Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString())
                                    orderby r.Appreciation
                                    select r;
                        if (liste.Count() > 0)
                        {
                            foreach (var app in liste)
                            {
                                cmbResultat.Items.Add(app.Appreciation);
                            }
                        }
                    }
                    else if (examen.ToUpper() == "SPERMOGRAMME")
                    {
                        etatResultat = "2";
                        etatDesExamens = "sperme";
                        var appreciation = ConnectionClass.ListeDesAppreciations();
                        var liste = from r in appreciation
                                    where r.NumeroAnalyse == Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString())
                                    orderby r.Appreciation
                                    select r;
                        if (liste.Count() > 0)
                        {
                            foreach (var app in liste)
                            {
                                cmbResultat.Items.Add(app.Appreciation);
                            }
                        }
                    }
                    else if (examen.ToUpper() == "PCV")
                    {
                        etatResultat = "2";
                        etatDesExamens = "pcv";
                        var appreciation = ConnectionClass.ListeDesAppreciations();
                        var liste = from r in appreciation
                                    where r.NumeroAnalyse == Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString())
                                    orderby r.Appreciation
                                    select r;
                        if (liste.Count() > 0)
                        {
                            foreach (var app in liste)
                            {
                                cmbResultat.Items.Add(app.Appreciation);
                            }
                        }
                    }
                    else if (examen.ToUpper() == "SELLES KAOP")
                    {
                        etatResultat = "2";
                        etatDesExamens = "kaop";
                        var appreciation = ConnectionClass.ListeDesAppreciations();
                        var liste = from r in appreciation
                                    where r.NumeroAnalyse == Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString())
                                    orderby r.Appreciation
                                    select r;
                        if (liste.Count() > 0)
                        {
                            foreach (var app in liste)
                            {
                                cmbResultat.Items.Add(app.Appreciation);
                            }
                        }
                    }
                    else
                    {
                        if (!dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("KAOP")
                            && !dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("ECBU")
                            && !dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("PCV")
                            && !dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("SPERMOGRAMME"))
                        {
                            etatResultat = "1";
                            etatDesExamens = "autres";
                            numExam = Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString());
                            var appreciation = ConnectionClass.ListeDesAppreciations();
                            var liste = from r in appreciation
                                        where r.NumeroAnalyse == Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString())
                                        select r;
                            if (liste.Count() > 0)
                            {
                                var found = false;
                                if (dgvResultatPatient.Rows.Count > 0)
                                {
                                    for (var i = 0; i < dgvResultatPatient.Rows.Count; i++)
                                    {
                                        if (Int32.Parse(dgvResultatPatient.Rows[i].Cells[0].Value.ToString()).Equals(numExam))
                                        {
                                            found = true;
                                        }
                                    }
                                    if (!found)
                                    {

                                        foreach (var app in liste)
                                        {
                                            dgvResultatPatient.Rows.Add(app.NumeroAnalyse, dgvListeExam.SelectedRows[0].Cells[1].Value.ToString(), app.Appreciation, "", valeurNormal, unite);
                                            Column1.Image = global::LabMonitoring.Properties.Resources.close11;
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var app in liste)
                                    {
                                        dgvResultatPatient.Rows.Add(app.NumeroAnalyse, dgvListeExam.SelectedRows[0].Cells[1].Value.ToString(), app.Appreciation, "", valeurNormal, unite);
                                        Column1.Image = global::LabMonitoring.Properties.Resources.close11;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            if (dgvResultatPatient.SelectedRows.Count > 0)
            {
                dgvResultatPatient.Rows.Remove(dgvResultatPatient.SelectedRows[0]);
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from p in ClassClinique.ListeDesPatients()
                            where p.IDPatient == Convert.ToInt32(textBox3.Text)
                            select p.NomPatient;
                if (liste.Count() > 0)
                {
                    foreach (var p in liste)
                    {
                        textBox2.Text = p.ToUpper();
                    }
                }
                else
                {
                    textBox2.Text = "";
                }
            }
            catch { }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                int numPatient;
                bool siFlagNFS;
                if (Int32.TryParse(textBox3.Text, out numPatient) && textBox2.Text != null)
                {
                    Patient patient = new Patient();
                    var listePatient = from l in ClassClinique.ListeDesPatients()
                                       where l.IDPatient == numPatient
                                       select l;

                    foreach (var p in listePatient)
                    {
                        patient.IDPatient = p.IDPatient;
                        patient.NomPatient = p.NomPatient;
                        patient.Sexe = p.Sexe;
                        patient.Age = p.Age;
                    }
                    patient.Lit = txtService.Text;
                    patient.Service = cmbService.Text;
                    patient.Prescripteur = cmbPrescripteur.Text;
                    if (chkNFS.Checked)
                    {
                        siFlagNFS = true;
                    } else
                    {
                        siFlagNFS = false;
                    }
                    bool flag;
                    if (etatDesExamens == "urologie")
                    {
                        flag = true;
                    }
                    else if (etatDesExamens == "kaop")
                    {
                        flag = false;
                    }
                    else if (etatDesExamens == "sperme")
                    {
                        flag = false;
                    }
                    else if (etatDesExamens == "pcv")
                    {
                        flag = false;
                    }
                    else if (etatDesExamens == "ecbu")
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = false;
                    }
                    //if (dgvResultatPatient.Rows.Count > 0)
                    {
                        if (MonMessageBox.ShowBox("Voulez vous enregistrer les resultat du patient " + patient.NomPatient + "?", "Confirmation", "confirmation.png") == "1")
                        {
                            if (ConnectionClass.EnregistrerLesResultatsDuPatient(dgvResultatPatient, patient, flag, siFlagNFS))
                            {
                                dateExam = DateTime.Now;
                                var chercherATB = false;
                                foreach (DataGridViewRow dtRow in dgvResultat.Rows)
                                {
                                    if (dtRow.Cells[2].Value.ToString().ToUpper().Contains("ANTIBIOGRAMME"))
                                    {
                                        chercherATB = true;
                                    }
                                }
                                if (etatDesExamens == "urologie")
                                {
                                    var titre = "RESULTAT DE L'EXAMEN UROCULTURE";
                                    if (chercherATB)
                                    {
                                        titre = "RESULTAT DE L'EXAMEN UROCULTURE + ATB";
                                    }
                                    bitmap = Impression.ImprimerResultatComplique(patient, dgvResultatPatient, titre);
                                }
                                else if (etatDesExamens == "kaop")
                                {
                                    var titre = "RESULTAT DE L'EXAMEN SELLE KAOP";
                                    if (chercherATB)
                                    {
                                        titre = "RESULTAT DE L'EXAMEN COPROCULTURE + ATB";
                                    }
                                    bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                                }
                                else if (etatDesExamens == "ecbu")
                                {
                                    var titre = "RESULTAT DE L'EXAMEN ECBU";
                                    if (chercherATB)
                                    {
                                        titre = "RESULTAT DE L'EXAMEN UROCULTURE + ATB";
                                    }
                                    bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                                }
                                else if (etatDesExamens == "kaop")
                                {
                                    var titre = "RESULTAT DE L'EXAMEN SELLE KAOP";
                                    if (chercherATB)
                                    {
                                        titre = "RESULTAT DE L'EXAMEN COPROCULTURE + ATB";
                                    }
                                    bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                                }
                                else if (etatDesExamens == "pcv")
                                {
                                    var titre = "RESULTAT DE L'EXAMEN PVC";
                                    if (chercherATB)
                                    {
                                        titre = "RESULTAT DE L'EXAMEN PVC + ATB";
                                    }
                                    bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                                }
                                else if (etatDesExamens == "sperme")
                                {
                                    var titre = "RESULTAT DE L'EXAMEN SPERMOGRAMME";

                                    bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                                }
                                else if (etatDesExamens == "autres")
                                {
                                    bool flagNFS;
                                    if (chkNFS.Checked)
                                    {
                                        flagNFS = true;
                                    }
                                    else
                                    {
                                        flagNFS = false;
                                    }
                                    bool flagRetrouveValeurNormal = false; ;
                                    foreach (DataGridViewRow dgvRow in dgvResultatPatient.Rows)
                                    {
                                        if (!string.IsNullOrEmpty(dgvRow.Cells[4].Value.ToString()))
                                        {
                                            flagRetrouveValeurNormal = true;
                                        }
                                    }
                                    if (flagRetrouveValeurNormal)
                                    {
                                        bitmap = Impression.ImprimerResultatBiochimie(patient, dateExam, dgvResultatPatient, flagNFS);
                                    }
                                    else
                                    {
                                        bitmap = Impression.ImprimerResultat1(patient, dateExam, dgvResultatPatient, flagNFS);
                                    }
                                }
                            }

                            if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                                printPreviewDialog1.ShowDialog();
                            }

                            txtService.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            cmbService.Text = " ";
                            cmbPrescripteur.Text = " ";
                            chkNFS.Checked = false;
                            dgvResultatPatient.Rows.Clear();

                        }

                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer le numéro du patient pour le selectionner", "Erreur", "erreur.png");
                }
            }
            catch (Exception)
            {
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            lblExamResultat.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dgvResultatPatient.Rows.Clear();
            cmbResultat.Items.Clear();
            etatDesExamens = "autres";
            chkNFS.Checked = false;
        }

        private void button12_Click_2(object sender, EventArgs e)
        {
            if (dgvResultatPatient.SelectedRows.Count > 0)
            {
                dgvResultatPatient.Rows.Remove(dgvResultatPatient.SelectedRows[0]);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MainForm.height = Height;
            if (MainForm.ShowBox() == "1")
            {
                chkNFS.Checked = false;
                dateExam = MainForm.dateResultat;
                dgvResultatPatient.Rows.Clear();
                string flagAnalyse = "";
                textBox3.Text = MainForm.IDPatient.ToString();
                textBox2.Text = MainForm.nomPatient;
                cmbService.Text = MainForm.service;
                cmbPrescripteur.Text = MainForm.prescripteur;
                txtService.Text = MainForm.lit;
                var liste = from lr in ConnectionClass.ListeResultatDetaillePatient()
                            join le in ConnectionClass.ListeExamenAll()
                            on lr.IDExam equals le.NumeroAnalyse
                            where lr.NumeroResultat == MainForm.numeroResultat
                            select new
                            {
                                lr.IDExam,
                                lr.Appreciation,
                                lr.NumeroResultat,
                                lr.ResultatExamen,
                                le.Analyse, le.ValeurNormal, le.Unite
                            };
                var bc = liste.Count();
                foreach (var l in liste)
                {
                    if (l.Analyse.ToUpper().Contains("KAOP"))
                    {
                        flagAnalyse = "KAOP";
                    }
                    else if (l.Analyse.ToUpper().Contains("ECBU"))
                    {
                        flagAnalyse = "ECBU";
                    }
                    else if (l.Analyse.ToUpper().Contains("PCV"))
                    {
                        flagAnalyse = "PCV";
                    }
                    else if (l.Analyse.ToUpper().Contains("SPERMOGRAMME"))
                    {
                        flagAnalyse = "SPERMOGRAMME";
                    }
                    else
                    {
                        flagAnalyse = "AUTRES";
                    }
                    var exam = l.Appreciation;
                    if (string.IsNullOrEmpty(l.Appreciation))
                    {
                        exam = l.Appreciation;
                    }
                    if (l.IDExam > 0)
                    {
                        dgvResultatPatient.Rows.Add(l.IDExam, l.Analyse, exam, l.ResultatExamen, l.ValeurNormal, l.Unite);
                        Column1.Image = global::LabMonitoring.Properties.Resources.close11;
                    } else if (l.IDExam == 0)
                    {
                        chkNFS.Checked = true;
                    }
                }

                if (flagAnalyse == "KAOP")
                {
                    etatDesExamens = "kaop";
                }
                else if (flagAnalyse == "PCV")
                {
                    etatDesExamens = "pcv";
                }
                else if (flagAnalyse == "ECBU")
                {
                    etatDesExamens = "ecbu";
                }
                else if (flagAnalyse == "SPERMOGRAMME")
                {
                    etatDesExamens = "sperme";
                }
                else if (flagAnalyse == "AUTRES")
                {
                    etatDesExamens = "autres";
                }
            }
        }
        DateTime dateExam;
        private void button16_Click(object sender, EventArgs e)
        {
            if (dgvResultatPatient.Rows.Count > 0)
            {

                if (textBox2.Text != "")
                {
                    int numPatient;
                    if (Int32.TryParse(textBox3.Text, out numPatient) && textBox2.Text != null)
                    {
                        Patient patient = new Patient();
                        var listePatient = from l in ConnectionClass.ListeDesPatients()
                                           where l.IDPatient == numPatient
                                           select l;

                        foreach (var p in listePatient)
                        {
                            patient.IDPatient = p.IDPatient;
                            patient.NomPatient = p.NomPatient;
                            patient.Sexe = p.Sexe;
                            patient.Age = p.Age;

                        }
                        patient.Service = cmbService.Text;
                        patient.Lit = txtService.Text;
                        patient.Prescripteur = cmbPrescripteur.Text;
                        var chercherATB = false;
                        foreach (DataGridViewRow dtRow in dgvResultatPatient.Rows)
                        {
                            if (dtRow.Cells[2].Value.ToString().ToUpper().Contains("ANTIBIOGRAMME"))
                            {
                                chercherATB = true;
                            }
                        }
                        if (etatDesExamens == "urologie")
                        {
                            var titre = "RESULTAT DE L'EXAMEN UROCULTURE";
                            if (chercherATB)
                            {
                                titre = "RESULTAT DE L'EXAMEN UROCULTURE + ATB";
                            }
                            bitmap = Impression.ImprimerResultatComplique(patient, dgvResultatPatient, titre);
                        }
                        else if (etatDesExamens == "kaop")
                        {
                            var titre = "RESULTAT DE L'EXAMEN SELLE KAOP";
                            if (chercherATB)
                            {
                                titre = "RESULTAT DE L'EXAMEN UROCULTURE + ATB";
                            }
                            bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                        }
                        else if (etatDesExamens == "ecbu")
                        {
                            var titre = "RESULTAT DE L'EXAMEN ECBU";
                            if (chercherATB)
                            {
                                titre = "RESULTAT DE L'EXAMEN COPROCULTURE + ATB";
                            }
                            bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                        }
                        else if (etatDesExamens == "pcv")
                        {
                            var titre = "RESULTAT DE L'EXAMEN PVC";
                            if (chercherATB)
                            {
                                titre = "RESULTAT DE L'EXAMEN PVC + ATB";
                            }
                            bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                        }
                        else if (etatDesExamens == "sperme")
                        {
                            var titre = "RESULTAT DE L'EXAMEN SPERMOGRAMME";
                            bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                        }
                        else if (etatDesExamens == "autres")
                        {
                            bool flagNFS;
                            if (chkNFS.Checked)
                            {
                                flagNFS = true;
                            }
                            else
                            {
                                flagNFS = false;
                            }
                            bool flagRetrouveValeurNormal = false; ;
                            foreach (DataGridViewRow dgvRow in dgvResultatPatient.Rows)
                            {
                                if (!string.IsNullOrEmpty(dgvRow.Cells[4].Value.ToString()))
                                {
                                    flagRetrouveValeurNormal = true;
                                }
                            }
                            if (flagRetrouveValeurNormal)
                            {
                                bitmap = Impression.ImprimerResultatBiochimie(patient, dateExam, dgvResultatPatient, flagNFS);
                            }
                            else
                            {
                                bitmap = Impression.ImprimerResultat1(patient, dateExam, dgvResultatPatient, flagNFS);
                            }
                        }

                        if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printPreviewDialog1.ShowDialog();
                        }
                        txtService.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        cmbPrescripteur.Text = " ";
                        cmbService.Text = " ";
                        chkNFS.Checked = false;
                        dgvResultatPatient.Rows.Clear();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text != null)
                {
                    dataGridViewTextBoxColumn6.HeaderText = "RESULTATS";
                    dgvResultat.Rows.Clear();
                    var liste = ConnectionClass.ListeDesResultats();
                    var l = from ls in liste
                            join la in ConnectionClass.ListeDesAppreciations()
                            on ls.IDAppreciation equals la.IDAppreciation
                            where la.Appreciation.ToUpper() == comboBox2.Text.ToUpper()
                            where la.NumeroAnalyse == numExam
                            select new
                            {
                                ls.IDAppreciation,
                                ls.Resultat,
                                ls.IDResultat
                            };
                    foreach (var ex in l)
                    {
                        dgvResultat.Rows.Add(ex.IDResultat, ex.IDAppreciation, ex.Resultat);
                    }
                }
            }
            catch { }
        }
        string etatResultat;
        private void dgvResultatPatient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    button12_Click_2(null, null);
                }
                else
                {
                    if (dgvResultatPatient.SelectedRows.Count > 0)
                    {
                        etatResultat = "1";
                        var liste = ConnectionClass.ListeDesResultats();
                        var l = from ls in liste
                                join la in ConnectionClass.ListeDesAppreciations()
                                on ls.IDAppreciation equals la.IDAppreciation
                                where la.Appreciation.ToUpper() == dgvResultatPatient.SelectedRows[0].Cells[2].Value.ToString().ToUpper()
                                where la.NumeroAnalyse == Convert.ToInt32(dgvResultatPatient.SelectedRows[0].Cells[0].Value.ToString().ToUpper())
                                select new
                                {
                                    ls.IDAppreciation,
                                    ls.Resultat,
                                    ls.IDResultat
                                };
                        cmbResultat.Items.Clear();
                        foreach (var ex in l)
                        {
                            cmbResultat.Items.Add(ex.Resultat);
                        }
                    }
                }
            }
            catch { }
        }

        void ListesDesApp()
        {
            try
            {
                cmbResultat.Items.Clear();
                var appreciation = ConnectionClass.ListeDesAppreciations();
                var liste = from r in appreciation
                            where r.NumeroAnalyse == Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString())
                            orderby r.Appreciation
                            select r;
                if (liste.Count() > 0)
                {
                    foreach (var app in liste)
                    {
                        cmbResultat.Items.Add(app.Appreciation);
                    }
                }
            }
            catch
            {
            }
        }
        private void cmbAspect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblExamResultat.Text == "")
            {
                MonMessageBox.ShowBox("Veuillez selectionner le type d'examen sur la liste à gauche", "Erreur", "erreur.png");
                return;
            }
            etatResultat = "2";
            if (etatDesExamens == "pcv")
            {
                if (dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("PCV"))
                {
                    dgvResultatPatient.Rows.Add(numExam, examen, cmbAspect.Text, "");
                    ListesDesApp();
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le type d'examen approprié", "Errreur", "erreur.png");
                }
            }
            else if (etatDesExamens == "pcv")
            {
                if (dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("ECBU"))
                {
                    dgvResultatPatient.Rows.Add(numExam, examen, cmbAspect.Text, "");
                    ListesDesApp();
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le type d'examen approprié", "Errreur", "erreur.png");
                }
            }
            else if (etatDesExamens == "kaop")
            {
                if (dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("KAOP"))
                {
                    dgvResultatPatient.Rows.Add(numExam, examen, cmbAspect.Text, "");
                    ListesDesApp();
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le type d'examen approprié", "Errreur", "erreur.png");
                }
            }

        }

        private void chkNFS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNFS.Checked)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Rows.Clear();
                var dt = ConnectionClass.BilanDesExamens(dtp1.Value.Date, dtp2.Value.Date);
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var count = Convert.ToDecimal(dt.Rows[i].ItemArray[1].ToString());
                    if (dt.Rows[i].ItemArray[0].ToString().ToUpper().Contains("WIDAL"))
                    {
                        count = Math.Round(count / 8);
                    } else if (dt.Rows[i].ItemArray[0].ToString().ToUpper().Contains("KAOP"))
                    {
                        count = ConnectionClass.BilanDesExamens("KAOP", dtp1.Value.Date, dtp2.Value.Date).Rows.Count;
                    }
                    else if (dt.Rows[i].ItemArray[0].ToString().ToUpper().Contains("ECBU"))
                    {
                        count = ConnectionClass.BilanDesExamens("ECBU", dtp1.Value.Date, dtp2.Value.Date).Rows.Count;
                    }
                    else if (dt.Rows[i].ItemArray[0].ToString().ToUpper().Contains("PCV"))
                    {
                        count = ConnectionClass.BilanDesExamens("PCV", dtp1.Value.Date, dtp2.Value.Date).Rows.Count;
                    }
                    else if (dt.Rows[i].ItemArray[0].ToString().ToUpper().Contains("BU"))
                    {
                        count = ConnectionClass.BilanDesExamens("BU", dtp1.Value.Date, dtp2.Value.Date).Rows.Count;
                    }
                    dataGridView3.Rows.Add(dt.Rows[i].ItemArray[2].ToString(), dt.Rows[i].ItemArray[0].ToString(), count);

                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("erreur", ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var titre = "Bilan des examens du " + dtp1.Value.ToShortDateString() + " au " + dtp2.Value.ToShortDateString();

                //bitmap = Impression.ImprimerBilanDesExamens(dataGridView3, titre, 0);
                //if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                //    printPreviewDialog1.ShowDialog();
                //}
                var count = dataGridView1.Rows.Count;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                var jour = DateTime.Now.Day;
                var moiSs = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "_imprimé_le_" + datTe + ".pdf";
                //string pathFile = "";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var Count = dataGridView3.Rows.Count / 40;
                    for (var i = 0; i <= Count; i++)
                    {
                        bitmap = Impression.ImprimerBilanDesExamens(dataGridView3, titre, i);
                        var inputImage = @"cdali" + i;
                        // Create an empty page
                        sharpPDF.pdfPage page = document.addPage();

                        document.addImageReference(bitmap, inputImage);
                        sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage);
                        page.addImage(img, -10, 0, page.height, page.width);
                    }
                    document.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch { }
        }
        
        private void button6_Click_1(object sender, EventArgs e)
        {
            if (GestionDuneClinique.FormesClinique.ListePatientFrm.ShowBox()=="1")
            {
                textBox3.Text = GestionDuneClinique.FormesClinique.ListePatientFrm.idPatient.ToString();
                textBox2.Text = GestionDuneClinique.FormesClinique.ListePatientFrm.patient;
            }
        }

        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    dataGridView1_CellContentClick(null, null);
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            try 
            {
                if (txtTypeExamen.Text != "")
                {
                    var exam = new Examen();
                    exam.Analyse = txtTypeExamen.Text;
                    exam.ValeurNormal = txtValeurNormal.Text;
                    exam.Unite = txtUnite.Text;
                    exam.NumeroAnalyse = id;
                    if (ConnectionClass.EnregistrerAnalyse(exam,etat))
                    {
                        id = 0;
                        txtTypeExamen.Text = "";
                        txtValeurNormal.Text = "";
                        txtUnite.Text = "";
                        etat = "0";
                        ListeAnalyse();
                        MonMessageBox.ShowBox("Données enregistrées avec succés ", "Affirmation", "affirmation.png");
                    }
                }
            }
            catch { }
        }

        private void txtValeurNormal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button4.Focus();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var frm = new BilanExamFrm();
                frm.idExam = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value.ToString());
                frm.examen = dataGridView3.CurrentRow.Cells[1].Value.ToString();
                frm.date1 = dtp1.Value.Date;
                frm.date2 = dtp2.Value.Date;
                frm.ShowDialog();
            }
        }

        private void dgvResultatPatient_Click(object sender, EventArgs e)
        {
            dgvResultatPatient_CellContentClick(null, null);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var frm = new EmplFrm();
            frm.ShowDialog();
            cmbPrescripteur.Items.Clear();
            cmbPrescripteur.Items.Add(" ");
            var dt = AppCode.ConnectionClass.ListePrescripteurs();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                  cmbPrescripteur.Items.Add(dt.Rows[i].ItemArray[1].ToString());
            }
        }

    }
}
