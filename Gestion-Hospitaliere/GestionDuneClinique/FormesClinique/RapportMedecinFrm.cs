using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.Formes
{
    public partial class RapportMedecinFrm : Form
    {
        public RapportMedecinFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Blue, 3);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void RapportMedecin_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, groupBox4.Width - 1, groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
               SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, groupBox3.Width - 1, groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
               SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void RapportMedecinFrm_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    if (!string.IsNullOrEmpty(cmbNomEmpoye.Text))
                    {
                        var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                        var id = listeEmpl[0].NumMatricule;
                        var dt = ConnectionClassClinique.DetailsDesAnalyseEffectuesParMedecin(id, DateTime.Now.Date, DateTime.Now.Date.AddHours(24));

                        ListeDesExamens(dt);
                        var dtConsultation = ConnectionClassClinique.ListeConsultationParMedecin(cmbNomEmpoye.Text, DateTime.Now.Date, DateTime.Now.Date.AddHours(24));
                        ListeDesConsultation(dtConsultation, DateTime.Now.Date, DateTime.Now.Date.AddHours(24));
                    }
                    else
                    {
                        var dt = ConnectionClassClinique.DetailsDesAnalyseEffectuesParMedecin(DateTime.Now.Date, DateTime.Now.Date.AddHours(24));

                        ListeDesExamens(dt);
                        var dtConsultation = ConnectionClassClinique.ListeConsultationParMedecin(cmbNomEmpoye.Text, DateTime.Now.Date, DateTime.Now.Date.AddHours(24));
                        ListeDesConsultation(dtConsultation,DateTime.Now.Date, DateTime.Now.Date.AddHours(24));
                    }
                }
                else if (checkBox2.Checked)
                {
                   if (!string.IsNullOrEmpty(cmbNomEmpoye.Text))
                    {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var dtAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectuesParMedecin(id, dtp1.Value.Date, dtp1.Value.Date.AddHours(24));
                    ListeDesExamens(dtAnalyse);

                    var dtConsultation = ConnectionClassClinique.ListeConsultationParMedecin(cmbNomEmpoye.Text, dtp1.Value.Date, dtp1.Value.Date.AddHours(24));
                    ListeDesConsultation(dtConsultation, dtp1.Value.Date, dtp1.Value.Date.AddHours(24));
                    }
                   else
                   {
                       var dt = ConnectionClassClinique.DetailsDesAnalyseEffectuesParMedecin(dtp1.Value.Date, dtp1.Value.Date.AddHours(24));

                       ListeDesExamens(dt);
                       var dtConsultation = ConnectionClassClinique.ListeConsultationParMedecin(cmbNomEmpoye.Text, dtp1.Value.Date, dtp1.Value.Date.AddHours(24));
                       ListeDesConsultation(dtConsultation, dtp1.Value.Date, dtp1.Value.Date.AddHours(24));
                   }
                }
                else if (checkBox3.Checked)
                { if (!string.IsNullOrEmpty(cmbNomEmpoye.Text))
                    {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var dtAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectuesParMedecin(id, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    ListeDesExamens(dtAnalyse);

                    var dtConsultation = ConnectionClassClinique.ListeConsultationParMedecin(cmbNomEmpoye.Text, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    ListeDesConsultation(dtConsultation, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    }
                 else
                {
                    var dt = ConnectionClassClinique.DetailsDesAnalyseEffectuesParMedecin(dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                    ListeDesExamens(dt);
                    var dtConsultation = ConnectionClassClinique.ListeConsultationParMedecin(cmbNomEmpoye.Text, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                    ListeDesConsultation(dtConsultation, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                }               
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport medecin", ex);
            }
        }

        //rapport des examens
        void ListeDesExamens(DataTable dtExam)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var total = 0.0;
                foreach (DataRow dtRow in dtExam.Rows)
                {
                    int  nbre ;
                    if (Int32.TryParse(dtRow.ItemArray[4].ToString(), out nbre))
                    {
                    }
                    else
                    {
                        nbre = 1;
                    }

                    dataGridView2.Rows.Add(
                       dtRow.ItemArray[2].ToString(),
                         nbre ,
                          dtRow.ItemArray[3].ToString(),
                         Double.Parse(dtRow.ItemArray[3].ToString())*nbre 
                       , 0, 0
                        );
                    total += Double.Parse(dtRow.ItemArray[3].ToString()) * nbre;
                }
                label9.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("rapport examen", ex);
            }
        }

        //rapport des consultations
        void ListeDesConsultation(DataTable dtConsultation, DateTime date1, DateTime date2)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var total =0.0;
                foreach (DataRow dtRow in dtConsultation.Rows)
                {
                    var consultation = dtRow.ItemArray[0].ToString().ToUpper();
                    if (consultation.ToUpper() == "Consultation De  Specialité".ToUpper())
                    {
                        consultation = "CONSULTATION EN " + dtRow.ItemArray[4].ToString().ToUpper();
                    }
                                       dataGridView1.Rows.Add(
                         consultation.ToUpper() ,
                         dtRow.ItemArray[1].ToString(),
                         dtRow.ItemArray[2].ToString(),
                         dtRow.ItemArray[3].ToString(),0,0
                        );
                total += Double.Parse(dtRow.ItemArray[3].ToString());
                }
                if (string.IsNullOrEmpty(cmbNomEmpoye.Text))
                {
                   var liste = ConnectionClassClinique. DetailsDesFacturesGrouperParCarnet(date1,date2);
                   foreach (var facture in liste)
                   {
                       dataGridView1.Rows.Add(
                           facture.Designation,
                           facture.Quantite,
                           facture.Prix,                           
                           facture.PrixTotal,0,0
                       );
                       total += facture.PrixTotal;
                   }
                }
                label10.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("rapport consultation", ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        Bitmap rapportMedecin;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(rapportMedecin, -10, 0, rapportMedecin.Width, rapportMedecin.Height);
                e.HasMorePages = false;
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GestionPharmacetique.AppCode.Employe employe=null ;
            string titre = "";
            if (checkBox1.Checked)
            {
                if (!string.IsNullOrWhiteSpace(cmbNomEmpoye.Text))
                {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                    employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                       listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                    titre = "Rapport de " + cmbNomEmpoye.Text + " du " + DateTime.Now.Date.ToShortTimeString();
                }
                else
                {
                    titre = "Rapport du " + DateTime.Now.Date.ToShortDateString();
                }
            }
            else if (checkBox2.Checked)
            {
                if (!string.IsNullOrWhiteSpace(cmbNomEmpoye.Text))
                {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                    employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                       listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                    titre = "Rapport de " + cmbNomEmpoye.Text + " du " + dtp1.Value.Date.ToShortTimeString();
                }
                else
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString();
                }
            }
            else if (checkBox3.Checked)
            {
                if (!string.IsNullOrWhiteSpace(cmbNomEmpoye.Text))
                {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                    employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                       listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                    titre = "Rapport de " + cmbNomEmpoye.Text + " du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                }
                else
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString();
               
                }
            }
          
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    rapportMedecin = Impression.RapportDesAnalysesDunMedecin(dataGridView2, employe, titre, 0);
                    printPreviewDialog1.ShowDialog();
                }
            
        }

        private void cmbNomEmpoye_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbNomEmpoye.Items.Clear();
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                if (listeEmploye.Count > 0)
                {
                    var list = from l in listeEmploye
                               where !l.NomEmployee.ToUpper().Contains("EXTERNE")
                               select l.NomEmployee;
                    foreach (var empl in list )
                    {
                        cmbNomEmpoye.Items.Add(empl);
                    }
                    cmbNomEmpoye.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmbNomEmpoye.DroppedDown = true;

                }
            }
        }

        private void cmbNomEmpoye_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNomEmpoye.Text = cmbNomEmpoye.SelectedText;
            cmbNomEmpoye.DropDownStyle = ComboBoxStyle.Simple;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox5.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Visible = false ;
            groupBox5.Visible = true ;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                dataGridViewTextBoxColumn3.Width = dataGridView1.Width / 9;
                dataGridViewTextBoxColumn4.Width = dataGridView1.Width / 9;
                Column10.Width = dataGridView1.Width / 9;
                dataGridViewTextBoxColumn3.Width = dataGridView1.Width / 2 - 130;
                Column11.Width = dataGridView1.Width / 9;

                Column10.Visible = true;
                Column11.Visible = true;
                dataGridViewTextBoxColumn9.Visible = true;
                Column6.Visible = true ;
                textBox1.Visible = true;
                label4.Visible = true ;
                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
                dataGridViewTextBoxColumn9.Visible = false;
                Column6.Visible = false;
                textBox1.Visible = false ;
                label4.Visible = false;  
                Column10.Visible = false;
                Column11.Visible = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                Column3.Width = dataGridView1.Width / 8;
                Column4.Width = dataGridView1.Width / 8;
                Column2.Width = dataGridView1.Width / 8;
                Column1.Width = dataGridView1.Width / 3-15;
                Column5.Width = dataGridView1.Width / 8;
                id.Width = dataGridView1.Width / 8;
                Column3.Visible = true;
                Column5.Visible = true;
                Column4.Visible = true;
                id.Visible = true;
                textBox2.Visible = true;
                textBox2.Text = "";
                label5.Visible = true;
                textBox2.Focus();
            }
            else
            {
                Column4.Visible = false;
                id.Visible = false;
                Column3.Visible = false ;
                Column5.Visible = false;
                textBox2.Visible = false;
                label5.Visible = false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    double part;
                    var total = 0.0;
                    if (Double.TryParse(textBox1.Text, out part))
                    {
                        foreach (DataGridViewRow dgRow in dataGridView2.Rows)
                        {
                            var montant = Double.Parse(dgRow.Cells[3].Value.ToString());
                            var partMedecin = montant * part / 100;
                            dgRow.Cells[4].Value = part;
                            dgRow.Cells[5].Value = partMedecin;
                            total += partMedecin;
                        }
                        label4.Text = total.ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    double part, total = 0.0;
                    if (Double.TryParse(textBox2.Text, out part))
                    {
                        foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                        {
                            var montant = Double.Parse(dgRow.Cells[3].Value.ToString());
                            var partMedecin = montant * part / 100;
                            dgRow.Cells[4].Value = part;
                            dgRow.Cells[5].Value = partMedecin;
                            total += partMedecin;
                        }
                        label5.Text = total.ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }


        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double part, total = 0.0;
                bool state = true ;
                    foreach (DataGridViewRow dgRow in dataGridView2.Rows)
                    {
                        if (Double.TryParse(dgRow.Cells[4].Value.ToString(), out part))
                        {
                            var montant = Double.Parse(dgRow.Cells[3].Value.ToString());
                            var partMedecin = montant * part / 100;
                            dgRow.Cells[4].Value = part;
                            dgRow.Cells[5].Value = partMedecin;
                            total += partMedecin;
                        }
                        else
                        {
                            state = false;
                        }
                        label4.Text = total.ToString();
                    }
                    if (!state)
                    {
                        MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour la part du medecin", "Erreur", "erreur.png");
                    }
                
                
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double part, total = 0.0;
                bool state = true;
                foreach (DataGridViewRow dgRow in dataGridView1.Rows)
                {
                    if (Double.TryParse(dgRow.Cells[4].Value.ToString(), out part))
                    {
                        CalulerTotalConsultation();
                    }
                    else
                    {
                        state = false;
                    }
                    //label5.Text = total.ToString();
                }
                if (!state)
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour la part du medecin", "Erreur", "erreur.png");
                }


            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GestionPharmacetique.AppCode.Employe employe = null;
            string titre = "";
            if (checkBox1.Checked)
            {
                if (!string.IsNullOrWhiteSpace(cmbNomEmpoye.Text))
                {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                    employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                       listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                    titre = "Rapport de " + cmbNomEmpoye.Text + " du " + DateTime.Now.Date.ToShortTimeString();
                }
                else
                {
                    titre = "Rapport du " + DateTime.Now.Date.ToShortDateString();
                }
            }
            else if (checkBox2.Checked)
            {
                if (!string.IsNullOrWhiteSpace(cmbNomEmpoye.Text))
                {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                    employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                       listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                    titre = "Rapport de " + cmbNomEmpoye.Text + " du " + dtp1.Value.Date.ToShortTimeString();
                }
                else
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString();
                }
            }
            else if (checkBox3.Checked)
            {
                if (!string.IsNullOrWhiteSpace(cmbNomEmpoye.Text))
                {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    var id = listeEmpl[0].NumMatricule;
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                    employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                       listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                    titre = "Rapport de " + cmbNomEmpoye.Text + " du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();
                }
                else
                {
                    titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " et " + dtp2.Value.Date.ToShortDateString();

                }
            }

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    var Count = (dataGridView2.Rows.Count) / 31;
                    for (var i = 0; i <= Count; i++)
                    {
                        if (i * 31 < dataGridView2.Rows.Count)
                        {
                            rapportMedecin = Impression.RapportDesAnalysesDunMedecin(dataGridView2, employe, titre, i);
                            printDocument1.Print();
                        }
                    }

                }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GestionPharmacetique.AppCode.Employe employe;
            string titre="";
            if (checkBox1.Checked && cmbNomEmpoye.Text != null)
            {
                var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                var id = listeEmpl[0].NumMatricule;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                 employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                    listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                 titre = "Rapport du " + DateTime.Now.Date.ToShortTimeString();

               
               
            }
            else if (checkBox2.Checked && cmbNomEmpoye.Text != null)
            {
                var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                var id = listeEmpl[0].NumMatricule;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                 employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                    listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                 titre = "Rapport du " + dtp1.Value.Date.ToShortTimeString();
            }
            else if (checkBox3.Checked && cmbNomEmpoye.Text != null)
            {
                var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                var id = listeEmpl[0].NumMatricule;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                   listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();

            }
            else
            {
                employe = null;
            }
          
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                var Count = (dataGridView1.Rows.Count) / 31;
                for (var i = 0; i <= Count; i++)
                {
                    if (i * 31 < dataGridView1.Rows.Count)
                    {
                        rapportMedecin = Impression.RapportDunMedecin(dataGridView1, employe, titre, i);
                        printDocument1.Print();
                    }
                }

            }
        
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GestionPharmacetique.AppCode.Employe employe;
            string titre = "";
            if (checkBox1.Checked && cmbNomEmpoye.Text != "")
            {
                var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                var id = listeEmpl[0].NumMatricule;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                 employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                    listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                 titre = "Rapport du " + DateTime.Now.Date.ToShortTimeString();
            }
            else if (checkBox2.Checked && cmbNomEmpoye.Text != "")
            {
                var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                var id = listeEmpl[0].NumMatricule;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                 employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                    listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                 titre = "Rapport du " + dtp1.Value.Date.ToShortTimeString();
            }
            else if (checkBox3.Checked && cmbNomEmpoye.Text != "")
            {
                var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                var id = listeEmpl[0].NumMatricule;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("num_empl", id);
                employe = new GestionPharmacetique.AppCode.Employe(listeEmploye[0].NumMatricule, listeEmploye[0].NomEmployee, listeEmploye[0].Addresse,
                    listeEmploye[0].Telephone1, listeEmploye[0].Telephone2, listeEmploye[0].Email, listeEmploye[0].Titre, listeEmploye[0].Photo);
                titre = "Rapport du " + dtp1.Value.Date.ToShortDateString() + " au " + dtp2.Value.Date.ToShortDateString();

            }
            else
            {
                employe = null;
            }

            //if (employe != null)
            //{
                rapportMedecin = Impression.RapportDunMedecin(dataGridView1, employe, titre, 0);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
            //}
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR"); 
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Checked = false;
                checkBox1.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    for (var i = 0; i < dataGridView2.SelectedRows.Count; i++)
                    {
                        dataGridView2.Rows.Remove(dataGridView2.SelectedRows[i]);
                        CalulerTotalExamen();
                    }
                }
            }
            catch { }
        }
        void CalulerTotalExamen()
        {
            try
            {
                double prix, nombre, pourcentage, montantTotal=0.0, partMedecinTotal = 0.0;
                for (var i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if(double.TryParse(dataGridView2.Rows[i].Cells[1].Value.ToString(), out nombre ))
                    {
                        if (double.TryParse(dataGridView2.Rows[i].Cells[2].Value.ToString(), out prix))
                        {
                            if (double.TryParse(dataGridView2.Rows[i].Cells[4].Value.ToString(), out pourcentage ))
                            {
                                var total = nombre * prix;
                                var partMedecin = nombre * prix * pourcentage / 100;
                                dataGridView2.Rows[i].Cells[3].Value=total;
                                dataGridView2.Rows[i].Cells[5].Value = partMedecin;
                                montantTotal += total;
                                partMedecinTotal += partMedecin;
                            }
                        }
                    }

                }

                label9.Text = montantTotal.ToString();
                label4.Text = partMedecinTotal.ToString();
            }
            catch { }

        }
        void CalulerTotalConsultation()
        {
            try
            {
                double prix, nombre, pourcentage, montantTotal = 0.0, partMedecinTotal = 0.0;
                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (double.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out nombre))
                    {
                        if (double.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out prix))
                        {
                            if (double.TryParse(dataGridView1.Rows[i].Cells[4].Value.ToString(), out pourcentage))
                            {
                                var total = nombre * prix;
                                var partMedecin = nombre * prix * pourcentage / 100;
                                dataGridView1.Rows[i].Cells[3].Value = total;
                                dataGridView1.Rows[i].Cells[5].Value = partMedecin;
                                montantTotal += total;
                                partMedecinTotal += partMedecin;
                            }
                        }
                    }

                }

                label10.Text = montantTotal.ToString();
                label5.Text = partMedecinTotal.ToString();
            }
            catch { }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    for (var i = 0; i < dataGridView1.SelectedRows.Count; i++)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                        CalulerTotalConsultation();
                    }
                }
            }
            catch { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    rapportMedecin = Impression.RapportRecuPaiementDunMedecin
                        (dataGridView1,dataGridView2, cmbNomEmpoye.Text, dtp1.Value, dtp2.Value);
                    printPreviewDialog1.ShowDialog();
                }
            }
            catch { }
        }
    }
}
