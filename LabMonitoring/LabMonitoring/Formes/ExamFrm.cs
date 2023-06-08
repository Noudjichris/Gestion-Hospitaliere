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


        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
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
            tabControl1.Enabled = false;
            Column1.Width = 40;
           dgvResultatPatient.RowTemplate.Height = 30;
            chkAutres.Checked = true;
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
    
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                    if (textBox1.Text != "")
                    {
                        dataGridView1.Rows.Add(0, 1,  " ", textBox1.Text);
                        textBox1.Text = "";
                    }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ConnectionClass.EnregistrerAnalyse(dataGridView1))
            {
                ListeAnalyse();
                MonMessageBox.ShowBox("Données enregistrées avec succés ", "Affirmation", "affirmation.png");
            }
        }

        void ListeAnalyse()
        {
            try
            {
                var listeAnalyse = ConnectionClass.ListeExamen();
                var listeGrp = ConnectionClass.ListeGroupeExamen();
                var liste = from e in listeAnalyse
                            join g in listeGrp
                            on e.NumeroGroupe equals g.NumeroGroupe
                            where e.Analyse.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby  e.Analyse
                            select new
                            {
                                e.NumeroAnalyse,
                                e.Analyse,
                                g.NumeroGroupe,
                                g.GroupeAnalyse
                            };
                dataGridView1.Rows.Clear();
                liste.Count();
                foreach (var ex in liste)
                {
                    dataGridView1.Rows.Add(ex.NumeroAnalyse, ex.NumeroGroupe, ex.GroupeAnalyse, ex.Analyse);
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
                dataGridViewTextBoxColumn6.HeaderText = "APPRECIATIONS"; 
                lblExamen.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                numExam = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                 ListeDesAppreciations();
                
            }
        }

        private void txtApp_KeyDown(object sender, KeyEventArgs e)
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
                        txtResult.Text = "";
                    }
                    else
                    {
                        if (comboBox2.Text != null)
                        {
                            var liste = ConnectionClass.ListeDesAppreciations();
                            var l = from ls in liste
                                    where ls.Appreciation.ToUpper() == comboBox2.Text.ToUpper()
                                    where ls.NumeroAnalyse==numExam
                                    select ls.IDAppreciation;
                            foreach (var ex in l)
                                dgvResultat.Rows.Add(0, ex, txtResult.Text);
                            txtResult.Text = "";
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le type d'examen sur la liste", "Erreur", "erreur.png");
                }
            }
        }

     

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int numPatient;
                if (Int32.TryParse(txtNumeroPatient.Text, out numPatient))
                {
                    if (!string.IsNullOrEmpty(txtNomPatient.Text) && txtNomPatient.Text.Contains(" "))
                    {
                        var patient = new Patient();
                        if (checkBox1.Checked)
                        {
                            patient.Sexe = "F";
                        }
                        else if (checkBox2.Checked)
                        {
                            patient.Sexe = "M";
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez selectionner pour le sexe du patient", "Erreur", "erreur.png");
                            return;
                        }
                        if(!string.IsNullOrEmpty(txtAgePatient.Text))
                        {
                            patient.Age = txtAgePatient.Text;
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez entrer l'age du patient", "Erreur", "erreur.png");
                            return;
                        }
                        patient.NomPatient = txtNomPatient.Text;
                        patient.Age = txtAgePatient.Text;
                        patient.IDPatient = numPatient;
                        if (ConnectionClass.EnregistrerUnPatient(patient,state ))
                        {
                            ListePatient();
                            state = "1";
                            txtAgePatient.Text = "";
                            txtNomPatient.Text = "";
                            txtNumeroPatient.Text = "";
                            checkBox1.Checked = false;
                            checkBox2.Checked = false;
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez entrer le nom et prenom du patient", "Erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le numéro du patient", "Erreur", "erreur.png");
                }
            }
            catch { }
        }

        void ListePatient()
        {
            try
            {
                var listePatient = ConnectionClass.ListeDesPatients();
                var liste = from p in listePatient
                            where p.NomPatient.StartsWith(txtNomPatient.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby p.NomPatient
                            select p;
                dataGridView2.Rows.Clear();
                foreach (var p in liste)
                {
                    dataGridView2.Rows.Add(p.IDPatient, p.NomPatient, p.Sexe, p.Age);
                }
            }
            catch { }
        }

        private void txtNomPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListePatient();
                dataGridView2.Focus();
                ListePatient();
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAgePatient.Text = dataGridView2.Rows[0].Cells[3].Value.ToString();
                txtNomPatient.Text = dataGridView2.Rows[0].Cells[1].Value.ToString();
                txtNumeroPatient.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
                if (dataGridView2.Rows[0].Cells[2].Value.ToString() == "F")
                {
                    checkBox1.Checked = true;
                }
                else if (dataGridView2.Rows[0].Cells[2].Value.ToString() == "M")
                {
                    checkBox2.Checked = true;
                }
                state = "2";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedRows.Count>0)
            if(MonMessageBox.ShowBox("Voulez vous supprimer les données de ce patient?","Confirmation","confirmation.png")=="1")
            {
                if (ConnectionClass.SupprimerUnPatient(Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString())))
                {
                    dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
                }
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
                e.Graphics.DrawImage(bitmap, -10,  10,bitmap.Width, bitmap.Height);
                e.HasMorePages = false;
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
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
                            where le.Appreciation ==comboBox2.Text
                            select new 
                                {
                                    lr.IDAppreciation, lr.Resultat,lr.IDResultat
                                };
                dgvResultat.Rows.Clear();
                liste.Count();
                foreach (var  examen in liste)
                {
                    dgvResultat.Rows.Add(examen.IDResultat,examen.IDAppreciation,examen.Resultat);
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
                    MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");

                    ListeDesResultats();
                }
            }
            else if (!checkBox3.Checked)
            {
                if (ConnectionClass.EnregistrerLesAppreciations(dgvResultat))
                {
                    MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
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
                            orderby  ee.Analyse
                            select new
                            {
                                ee.NumeroAnalyse,
                                ee.Analyse,
                                g.NumeroGroupe,
                                g.GroupeAnalyse
                            };
                dgvListeExam.Rows.Clear();
                foreach (var ex in liste)
                {
                    dgvListeExam.Rows.Add(ex.NumeroAnalyse, ex.Analyse);
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
              
                dataGridViewTextBoxColumn6.HeaderText ="RESULTATS";
            }

            if (lblExamen.Text != null)
            {
                if ( !checkBox3.Checked)
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

        private void dgvListeExam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                numExam = Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString());
                examen = dgvListeExam.SelectedRows[0].Cells[1].Value.ToString();
                lblExamResultat.Text = examen;
                cmbResultat.Items.Clear();
                if (examen.ToUpper() == "ECBU")
                {
                    etatResultat = "2";
                    chkECBU.Checked=true;
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
                else if(examen.ToUpper()=="SELLES KAOP")
                {
                    etatResultat = "2";
                    chkKaop.Checked = true; 
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
                        && !dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("PCV"))
                    {
                        etatResultat = "1"; chkAutres.Checked = true;
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
                                        dgvResultatPatient.Rows.Add(app.NumeroAnalyse, dgvListeExam.SelectedRows[0].Cells[1].Value.ToString(), app.Appreciation, "");
                                        Column1.Image = global::LabMonitoring.Properties.Resources.close11;
                                    }
                                }
                            }
                            else
                            {
                                foreach (var app in liste)
                                {
                                    dgvResultatPatient.Rows.Add(app.NumeroAnalyse, dgvListeExam.SelectedRows[0].Cells[1].Value.ToString(), app.Appreciation, "");
                                    Column1.Image = global::LabMonitoring.Properties.Resources.close11;
                                }
                            }
                        }
                    }
                }
                //else
                //{
                //    //if(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString()
                //    etatResultat = "2";
                //    cmbResultat.Items.Clear();
                //        var appreciation = ConnectionClass.ListeDesAppreciations();
                //        var liste = from r in appreciation
                //                    where r.NumeroAnalyse == Convert.ToInt32(dgvListeExam.SelectedRows[0].Cells[0].Value.ToString())
                //                    orderby r.Appreciation
                //                    select r;
                //        if (liste.Count() > 0)
                //        {
                //            foreach (var app in liste)
                //            {
                //                cmbResultat.Items.Add(app.Appreciation);
                //            }
                //        }
                    
                //}
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
            if (Int32.TryParse(textBox3.Text, out numPatient) && textBox2.Text != null)
            {
                Patient patient =new Patient();
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

                bool flag;
                if (chkUrologie.Checked)
                {
                    flag = true;
                }
                else if ( chkKaop.Checked)
                {
                    flag = false;
                }
                else if (chkECBU.Checked)
                {
                    flag = false;
                }
                else
                {
                    flag = false;
                }
                if (dgvResultatPatient.Rows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous enregistrer les resultat du patient " + patient.NomPatient + "?", "Confirmation", "confirmation.png") == "1")
                    {
                        if (ConnectionClass.EnregistrerLesResultatsDuPatient(dgvResultatPatient, patient, flag))
                        {
                            var chercherATB = false;
                            foreach (DataGridViewRow dtRow in dgvResultat.Rows)
                            {
                                if (dtRow.Cells[2].Value.ToString().ToUpper().Contains("ANTIBIOGRAMME"))
                                {
                                    chercherATB = true;
                                }
                            }
                            if (chkUrologie.Checked)
                            {
                                var titre = "UROCULTURE";
                                if (chercherATB)
                                {
                                    titre = "UROCULTURE + ATB";
                                }
                                bitmap = Impression.ImprimerResultatComplique(patient, dgvResultatPatient, titre);
                            }
                            else if (chkKaop.Checked)
                            {
                                var titre = "SELLE KAOP";
                                if (chercherATB)
                                {
                                    titre = "COPROCULTURE + ATB";
                                }
                                bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                            }
                            else if (chkECBU.Checked)
                            {
                                var titre = "ECBU";
                                if (chercherATB)
                                {
                                    titre = "UROCULTURE + ATB";
                                }
                                bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                            }
                            else if (chkKaop.Checked)
                            {
                                var titre = "SELLE KAOP";
                                if (chercherATB)
                                {
                                    titre = "COPROCULTURE + ATB";
                                }
                                bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                            }
                            else if (chkPCV.Checked)
                            {
                                var titre = "PRELEVEMENT CERVICO-VAGINAL (PVC)";
                                if (chercherATB)
                                {
                                    titre = "PRELEVEMENT CERVICO-VAGINAL (PVC) + ATB";
                                }
                                bitmap = Impression.ImprimerResultatComplique(patient, dgvResultatPatient, titre);
                            }
                            else if (chkAutres.Checked)
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
                                bitmap = Impression.ImprimerResultat1(patient, dgvResultatPatient, flagNFS);
                            }

                            if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                                printPreviewDialog1.ShowDialog();
                            }


                            textBox2.Text = "";
                            textBox3.Text = "";
                            dgvResultatPatient.Rows.Clear();

                        }
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
            chkUrologie.Checked = false;
            chkECBU.Checked = false;
            chkAutres.Checked = true ;
            chkPCV.Checked = false;
            chkNFS.Checked = false;
            chkCoproculture.Checked = false;
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
                dgvResultatPatient.Rows.Clear();
                string flagAnalyse="";
                textBox3.Text = MainForm.IDPatient.ToString();
                textBox2.Text = MainForm.nomPatient;
                var liste = from lr in ConnectionClass.ListeResultatDetaillePatient()
                            join le in ConnectionClass.ListeExamen()
                            on lr.IDExam equals le.NumeroAnalyse
                            where lr.NumeroResultat == MainForm.numeroResultat
                            select new
                            {
                                lr.IDExam,
                                lr.Appreciation,
                                lr.NumeroResultat,
                                lr.ResultatExamen,
                                le.Analyse
                            };
                foreach (var l in liste)
                {
                    if(l.Analyse.ToUpper().Contains("KAOP"))
                    {
                        flagAnalyse = "KAOP";
                    }
                    else if (l.Analyse.ToUpper().Contains("ECBU"))
                    {
                        flagAnalyse = "ECBU";
                    }
                    else
                    {
                        flagAnalyse = "AUTRES";
                    }
                    var  exam = l.Appreciation;
                    if (string.IsNullOrEmpty(l.Appreciation))
                    {
                        exam = l.Appreciation;
                    }
                    dgvResultatPatient.Rows.Add(l.IDExam,l.Analyse, exam, l.ResultatExamen);
                }

                if (flagAnalyse == "KAOP")
                {
                    chkKaop.Checked = true;
                }
                else if (flagAnalyse == "ECBU")
                {
                    chkECBU.Checked = true;
                }
                else if (flagAnalyse == "AUTRES")
                {
                    chkAutres.Checked = true;
                }
            }
        }

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
                        var chercherATB = false;
                        foreach (DataGridViewRow dtRow in dgvResultatPatient.Rows)
                        {
                            if (dtRow.Cells[2].Value.ToString().ToUpper().Contains("ANTIBIOGRAMME"))
                            {
                                chercherATB = true;
                            }
                        }
                        if (chkUrologie.Checked)
                        {
                            var titre = "UROCULTURE";
                            if (chercherATB)
                            {
                                titre = "UROCULTURE + ATB";
                            }
                            bitmap = Impression.ImprimerResultatComplique(patient, dgvResultatPatient, titre );
                        }
                        else if (chkKaop.Checked)
                        {
                            var titre = "SELLE KAOP";
                            if (chercherATB)
                            {
                                titre = "UROCULTURE + ATB";
                            }
                            bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                        }
                        else if (chkECBU.Checked)
                        {
                            var titre = "ECBU";
                            if (chercherATB)
                            {
                                titre = "COPROCULTURE + ATB";
                            }
                            bitmap = Impression.ImprimerResultatKAOP(patient, dgvResultatPatient, titre);
                        }
                        else if (chkPCV.Checked)
                        {
                            var titre = "PRELEVEMENT CERVICO-VAGINAL (PVC)";
                            if (chercherATB)
                            {
                                titre = "PRELEVEMENT CERVICO-VAGINAL (PVC) + ATB";
                            }
                            bitmap = Impression.ImprimerResultatComplique(patient, dgvResultatPatient, titre);
                        }
                        else if (chkAutres.Checked)
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
                            bitmap = Impression.ImprimerResultat1(patient, dgvResultatPatient, flagNFS);
                        }

                        if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printPreviewDialog1.ShowDialog();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if(MonMessageBox.ShowBox("Voulez vous supprimer cet examen?","Confirmation","confirmation")=="1")
                {
                    ConnectionClass.SupprimerUnAnalyse(dataGridView1);
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                }
            }
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
                            where la.NumeroAnalyse==numExam
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
                if (e.ColumnIndex == 4)
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

        private void chkECBU_CheckedChanged(object sender, EventArgs e)
        {
            if (chkECBU.Checked)
            {
                dataGridViewTextBoxColumn11.Visible = false;
                chkAutres.Checked = false;
                chkPCV.Checked = false;
                chkNFS.Checked = false;
                chkCoproculture.Checked = false;
                chkKaop.Checked = false;
            }
        }   
        private void chkUrologie_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUrologie.Checked)
            {
                dataGridViewTextBoxColumn11.Visible = false;
                chkAutres.Checked = false;
                chkPCV.Checked = false;
                chkNFS.Checked = false;
                chkCoproculture.Checked = false;
                chkKaop.Checked = false;
            }
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
                return ;
            }
            etatResultat = "2";
            if (chkPCV.Checked)
            { 
                if(dgvListeExam.SelectedRows[0].Cells[1].Value.ToString().ToUpper().Contains("PCV"))
                {
                    dgvResultatPatient.Rows.Add(numExam, examen, cmbAspect.Text, "");
                    ListesDesApp();
                }
                 else
                {
                     MonMessageBox.ShowBox("Veuillez selectionner le type d'examen approprié", "Errreur", "erreur.png");
                }
            }        
            else if (chkUrologie.Checked)
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
            else if (chkKaop.Checked)
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

        private void chkCoproculture_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCoproculture.Checked)
            {
                dataGridViewTextBoxColumn11.Visible = false;
                chkPCV.Checked = false;
                chkNFS.Checked = false;
                chkUrologie.Checked = false;
                chkAutres.Checked = false;
                chkKaop.Checked = false;
                chkECBU.Checked = false;
            }
        }

        private void chkPCV_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPCV.Checked)
            {
                dataGridViewTextBoxColumn11.Visible = false;
                chkCoproculture.Checked = false;
                chkNFS.Checked = false;
                chkUrologie.Checked = false;
                chkAutres.Checked = false;
                chkKaop.Checked = false;
                chkECBU.Checked = false;
            }
        }

        private void chkNFS_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNFS.Checked)
            {
                chkCoproculture.Checked = false;
                chkPCV.Checked = false;
                chkUrologie.Checked = false;
                chkKaop.Checked = false;
                chkECBU.Checked = false;
                //chkNFS.Checked = false;
            }
        }

          private void chkKaop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKaop.Checked)
            {
                dataGridViewTextBoxColumn11.Visible = false;
                chkCoproculture.Checked = false;
                chkNFS.Checked = false;
                chkUrologie.Checked = false;
                chkAutres.Checked = false;
                chkPCV.Checked = false;
                chkECBU.Checked = false;
            }
          }
        private void chkAutres_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutres.Checked)
            {
                dataGridViewTextBoxColumn11.Visible = true ;
                chkCoproculture.Checked = false;
                chkPCV.Checked = false;
                chkUrologie.Checked = false;
                chkNFS.Checked = false;
                chkKaop.Checked = false;
                chkECBU.Checked = false;
            }
        }
      
        int numPatient;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    state = "2";
                    txtAgePatient.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                    txtNomPatient.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                    txtNumeroPatient.Text =dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    if (dataGridView2.SelectedRows[0].Cells[2].Value.ToString() == "F")
                    {
                        checkBox1.Checked = true;
                        checkBox2.Checked = false;
                    }
                    else if (dataGridView2.SelectedRows[0].Cells[2].Value.ToString() == "M")
                    {
                        checkBox2.Checked = true;
                        checkBox1.Checked = false;
                    }
                }
            }
            catch { }
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
                    if (dt.Rows[i].ItemArray[0].ToString().ToUpper().Contains("WIDAL") ||
                        dt.Rows[i].ItemArray[0].ToString().ToUpper().Contains("KAOP"))
                    {
                        count = Math.Round(count / 8);
                    }
                   dataGridView3.Rows.Add(dt.Rows[i].ItemArray[0].ToString(),count );
                    
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
                bitmap = Impression.ImprimerBilanDesExamens(dataGridView3, titre);
                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
            }
            catch { }
        }

        private void dgvListeExam_Click(object sender, EventArgs e)
        {
            dgvListeExam_CellContentClick(null, null);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (GestionDuneClinique.FormesClinique.ListePatientFrm.ShowBox()=="1")
            {
                textBox3.Text = GestionDuneClinique.FormesClinique.ListePatientFrm.idPatient.ToString();
                textBox2.Text = GestionDuneClinique.FormesClinique.ListePatientFrm.patient;
            }
        }
        public static string typeUtlisateur;
  
        private void button11_Click_1(object sender, EventArgs e)
        {
            if (button11.Text == "Se connecter".ToUpper())
            {
                if (GestionAcademique.LoginFrm.ShowBox() == "1")
                {
                    lblUtilisateur.Text = GestionAcademique.LoginFrm.nomUtilisateur;
                    typeUtlisateur = GestionAcademique.LoginFrm.typeUtilisateur;
                    lblHeure.Text = "Connecter à " + DateTime.Now.ToShortTimeString();
                    tabControl1.Enabled = true;
                    button11.Text = "Se deconnecter".ToUpper();
                }
            }
            else if (button11.Text == "Se deconnecter".ToUpper())
            {
                tabControl1.Enabled = false;
                button11.Text = "Se connecter".ToUpper();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var frm = new GestionDesPelerinsTchad.Formes.ModifierMotPasse();
            frm.ShowDialog();
        }
    }
}
