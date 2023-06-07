using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using GestionDuneClinique.Formes;
using GestionDuneClinique;
namespace GestionPharmacetique.Forme
{
    public partial class ExamenFrm : Form
    {
        public ExamenFrm()
        {
            InitializeComponent();
        }
        
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        //modifier exame
        int num_examen;
        public bool  flag=false;
    
        private void button7_Click(object sender, EventArgs e)
        {
            state = "2";
            idExamen = 0;
            this.Dispose();
        }

        private void ExamenFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control, 
                SystemColors.ControlLight, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        public static string AfficherAge(string an, string mois)
        {
            int annee;
            if (!string.IsNullOrEmpty(an))
            {
                if (Int32.TryParse(an, out annee))
                {
                    if (!string.IsNullOrEmpty(mois))
                    {
                        return an + " ans et " + mois + " mois ";
                    }
                    else
                    {
                        return an + " ans ";
                    }
                }
                else
                {
                    return an;
                }
            }
            else
            {
                return mois + " mois";
            }
        }
    
        static ExamenFrm frmList;
        public static string btnClick, patiente , state ="0";
        public static int idExamen, idPatiente;
        public static string ShowBox()
        {
            try
            {
                frmList = new ExamenFrm();
                frmList.textBox1.Focus();
                frmList.ShowDialog();
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
            return btnClick;
        }
        private void ExamenFrm_Load(object sender, EventArgs e)
        {
            button7.Location = new Point(Width -35, 4);
            Location = new Point(GestionAcademique.Form1.xLocation, GestionAcademique.Form1.yLocation);
            Size = new System.Drawing.Size(GestionAcademique.Form1.width, GestionAcademique.Form1.height);
            ListesDesExamens();          
            Column5.Width = dgvAnal.Width*2 / 3;
            Column1.Width = Column2.Width = Column6.Width = dgvAnal.Width / 9;
            if (flag)
            {
                state = "0";
            }
            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
            cmbEntreprise.Items.Add("");
            foreach (Entreprise entreprise in listeEntrep)
            {
                cmbEntreprise.Items.Add(entreprise.NomEntreprise.ToUpper());
            }
            var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
            var list = from p in listeEmploye
                       where !p.Titre.ToUpper().Contains("CAISS")
                       select p.NomEmployee;

            foreach (var empl in list)
            {
                cmbMedecin.Items.Add(empl.ToUpper());
            }
            cmbMedecin.Text = "CONSULTANT";
            button8.Focus();
            Location = new Point(GestionAcademique.Form1.xLocation, GestionAcademique.Form1.yLocation);
        }

        //liste des examens
        private bool ListesDesExamens()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var listeAnalyse = from a in ConnectionClassClinique.ListeDesAnalyses()
                                   where a.TypeAnalyse.ToUpper().Contains(textBox1.Text.ToUpper())
                                   select a;
                if (listeAnalyse.Count()>0)
                {
                    foreach (var analyse in listeAnalyse)
                    {
                        
                        var idLibelle = analyse.NumeroGroupe;
                        var lst = ConnectionClassClinique.ListeDesLibelles(idLibelle);
                        var libelle = "";
                        if (lst.Count > 0)
                        {
                            libelle = lst[0].Designation + " " + lst[0].Sub;
                        }
                        if (rdb1.Checked)
                        {
                            dataGridView1.Rows.Add
                                (

                              analyse.NumeroListeAnalyse.ToString(),
                              analyse.TypeAnalyse.ToUpper(),
                              analyse.Frais.ToString(), libelle
                                );
                        }
                        else if (rdb2.Checked)
                        {
                            dataGridView1.Rows.Add
                                (

                              analyse.NumeroListeAnalyse.ToString(),
                              analyse.TypeAnalyse.ToUpper(),
                              analyse.FraisConventionnes.ToString(), libelle
                                );
                        }
                        else if (rdb3.Checked)
                        {
                            dataGridView1.Rows.Add
                                (

                              analyse.NumeroListeAnalyse.ToString(),
                              analyse.TypeAnalyse.ToUpper(),
                              analyse.FraisInternes.ToString(), libelle
                                );
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
        }

        //annuler
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvAnal.Rows.Count > 0)
                {
                    var listePatient =from p in  ConnectionClassClinique.ListeDesPatients()
                                          where p.NumeroPatient==idPatient
                                          select p;

                    var patient = new Patient();
                    foreach (var p in listePatient)
                        patient = p;

                    var listeFacture = from f in ConnectionClassClinique.TableDesDetailsFacturesProforma()
                                       where f.NumeroActe == idAnalyse
                                       where f.Sub == "EXAMEN"
                                       select f.NumeroFacture;
                    var numeroFacture =0;
                    foreach (var a in listeFacture)
                        numeroFacture = a;
                    analyseBitmap = Impression.ImprimerAnalyse(numeroFacture, patient, dateTimePicker1.Value, GestionAcademique.LoginFrm.nom,1);
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        //retirer items
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (dgvAnal.SelectedRows.Count > 0)
                {
                    if (etat == "1")
                    {
                        dgvAnal.Rows.Remove(dgvAnal.SelectedRows[0]);
                        var montantTotal = 0.0;
                        for (int i = 0; i <= dgvAnal.Rows.Count - 1; i++)
                        {
                            montantTotal += Double.Parse(dgvAnal.Rows[i].Cells[2].Value.ToString());
                        }
                        txtmontanttotal.Text = montantTotal.ToString();
                    }
                    else if (etat == "2")
                    {
                        if (dgvAnal.SelectedRows.Count > 0)
                            if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                            {
                                if (MonMessageBox.ShowBox("Voulez vous retirer les données des examen?", "Confirmation", "confirmation.png") == "1")
                                {
                                    var id = Int32.Parse(dgvAnal.SelectedRows[0].Cells[0].Value.ToString());
                                    var design = dgvAnal.SelectedRows[0].Cells[1].Value.ToString();
                                    var montantTotal = 0.0;

                                    montantTotal = double.Parse(txtmontanttotal.Text) - Double.Parse(dgvAnal.SelectedRows[0].Cells[2].Value.ToString());

                                    txtmontanttotal.Text = montantTotal.ToString();

                                    ConnectionClassClinique.SupprimerUneAnalyseFaite(idAnalyse, id, montantTotal, design);

                                    dgvAnal.Rows.Remove(dgvAnal.SelectedRows[0]);
                                }
                            }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        //enregistrer les donnees
        int idPatient;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string nomMedecin;
                double montantTotal;
                if (string.IsNullOrEmpty(cmbMedecin.Text))
                {
                    MonMessageBox.ShowBox("Veuillez slectionner le nom du medecin sur la liste deroulante", "erreur", "erreur.png");
                    return;
                }
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                {
                    return;
                }
                montantTotal = Double.Parse(txtmontanttotal.Text);
                            nomMedecin = cmbMedecin.Text;
                            var partenaire = "";
                            var listeEmploye = ConnectionClassClinique.ListeDesEmployees("nom_empl",nomMedecin);
                var numEmpl = listeEmploye[0].NumMatricule;

                             if (etat == "1")
                            {
                                if (dataGridView2.SelectedRows.Count > 0)
                                {
                                    idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                                }
                                else if (idPatient > 0)
                                {
                                    //idPatient = idPatient;
                                }
                                else
                                {
                                    MonMessageBox.ShowBox("Veuillez selectionner le nom du patient sur la liste.", "Erreur", "erreur.png");
                                    return;
                                }
                                var nomPatient = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                                var analyse = new Analyse();
                                analyse.NumeroAnalyse = idAnalyse;
                                analyse.DateAnalyse = dateTimePicker1.Value;
                                analyse.IdPatient =idPatient;
                                analyse.NumeroEmploye = numEmpl;
                                analyse.MontantTotal = montantTotal;
                                analyse.Libelle = partenaire;
                                if (ConnectionClassClinique.AjouterLesAnalysesEffectues(analyse, dgvAnal, nomPatient))
                                {
                                    idExamen = ConnectionClassClinique.ObtenirDernierNumeroAnalyse(GestionAcademique.LoginFrm.matricule); 
                                    
                                    if (state == "1")
                                    {
                                        patiente = nomPatient;
                                        idPatiente = idPatient;                                         
                                        btnClick = "1";
                                        Dispose(); 
                                    }
                        dgvAnal.Rows.Clear();
                        txtmontanttotal.Text = "";
                        txtRechercher.Text = "";
                        dataGridView2.Rows.Clear();
                        rdb1.Checked = true;
                        //button9.Enabled = true;
                    }
                            }
                            else if (etat == "2")
                             {
                                 var analyse = new Analyse();
                                 analyse.NumeroAnalyse = idAnalyse;
                                 analyse.DateAnalyse = dateTimePicker1.Value;
                                 analyse.IdPatient = idAnalyse;
                                 analyse.NumeroEmploye = numEmpl;
                                 analyse.MontantTotal = montantTotal;
                                 analyse.Libelle = partenaire;
                                if (ConnectionClassClinique.ModifierLesAnalysesEffectues(analyse, dgvAnal))
                                {
                                    btnClick = "1";
                                    dgvAnal.Rows.Clear();
                                    txtmontanttotal.Text = "";
                                }
                            }
                  
               
            }
            catch { }
        }

        Bitmap analyseBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            e.Graphics.DrawImage(analyseBitmap, -5, 10, width, height);
            e.HasMorePages = false;
        }

        
        string etat;
        //liste des examens
        int idAnalyse;
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListeExamFrm.ShowBox() == "1")
                {
                    etat = "2";
                    dgvAnal.Rows.Clear();
                    idAnalyse = ListeExamFrm.id;
                    var dtAnalyse = ConnectionClassClinique.TableDesAnalysesEffectues(idAnalyse);
                    dateTimePicker1.Value = DateTime.Parse(dtAnalyse.Rows[0].ItemArray[1].ToString());
                    var docteur = dtAnalyse.Rows[0].ItemArray[4].ToString();
                   
                    cmbMedecin.Text = docteur;
                    button9.Enabled = true;
                    txtmontanttotal.Text = dtAnalyse.Rows[0].ItemArray[5].ToString();
                    button3.Enabled = true;
                    idPatient = ListeExamFrm.idPatient;
                    txtRechercher.Text = idPatient.ToString();
                    var detailAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectues(idAnalyse);
                    foreach (Analyse analyse in detailAnalyse)
                    {
                        var typeAnalyse = ConnectionClassClinique.ListeDesAnalyses(analyse.NumeroListeAnalyse);
                        var type = typeAnalyse[0].TypeAnalyse;
                        var montant = typeAnalyse[0].Frais;
                          var idLibelle = typeAnalyse[0].NumeroGroupe;
                        var lst = ConnectionClassClinique.ListeDesLibelles(idLibelle);
                        var libelle = "";
                        if (lst.Count > 0)
                        {
                            libelle = lst[0].Designation + " " + lst[0].Sub;
                        }
                        dgvAnal.Rows.Add(analyse.NumeroListeAnalyse, type, analyse.Frais,
                            analyse.NombreAnalyse, analyse.NombreAnalyse * analyse.Frais,
                            libelle);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        //imorimer
        private void button1_Click(object sender, EventArgs e)
        {
            dgvAnal.Rows.Clear();
            txtmontanttotal.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void txtRechercherPatient_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var listePatient = new List<Patient>();
                if (Int32.TryParse(txtRechercher.Text, out idPatient))
                {
                    listePatient = ConnectionClassClinique.ListeDesPatients(idPatient);
                }
                else
                {
                    if (txtRechercher.Text.Length >= 2)
                    {
                        listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise(txtRechercher.Text, cmbEntreprise.Text);
                    }
                } if (listePatient.Count() > 0)
                {
                    dataGridView2.Rows.Clear();
                    foreach (var patient in listePatient)
                    {
                        var age = AfficherAge(patient.An, patient.Mois);
                        dataGridView2.Rows.Add(
                            patient.NumeroPatient.ToString(),
                            patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(),
                         age, patient.Sexe,
                            patient.NomEntreprise,
                            patient.Couvert
                        );
                    }
                } if (dataGridView2.SelectedRows.Count > 0)
                {
                    //if (!string.IsNullOrEmpty(dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper() ))
                    //{
                    //    rdb2.Checked = true;
                    //}
                    ////else if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper().Contains("FRAIS M INTERNE"))
                    ////{
                    ////    rdb3.Checked = true;
                    ////}
                    //else
                    //{
                    //    rdb1.Checked = true;
                    //}
                }
            }
            catch { }
        }
        
        private void dgvAnal_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var montantTotal = 0.0;
                double montant;
                foreach (DataGridViewRow dgvRow in dgvAnal.Rows)
                {
                    var frais = Double.Parse(dgvRow.Cells[2].Value.ToString());
                    var qte = Int32.Parse(dgvRow.Cells[3].Value.ToString());
                    var total = frais * qte;
                    dgvRow.Cells[2].Value = frais;
                    dgvRow.Cells[3].Value = qte ;
                    dgvRow.Cells[4].Value = total ;
                    if (Double.TryParse(dgvRow.Cells[4].Value.ToString(), out montant))
                    {
                        montantTotal += montant;
                    }
                }
                txtmontanttotal.Text = montantTotal.ToString();
            }
            catch { }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
printPreviewDialog1.Document = printDocument1;
        }

        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int idPat;
                    PatientFrm.btnClick = "1";
                    if (Int32.TryParse(txtRechercher.Text, out idPat))
                        PatientFrm.numeroPatient = idPat;

                PatientFrm.btnClick = "1";
                if (PatientFrm.ShowBox() == "1")
                {
                    txtRechercher.Text = PatientFrm.numeroPatient>0 ? PatientFrm.numeroPatient.ToString() : "";
                    textBox2_TextChanged(null, null);
                    txtRechercher.Focus();
                }
            }
            catch { }
        }

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                dataGridView1_DoubleClick(null, null);
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.S && e.Modifiers== Keys.Control)
            {
                dgvAnal.Rows.Clear();
                dgvAnal.Rows.Add(194,   "GE-RH",   1500, 1, 1500, "-");
                dgvAnal.Rows.Add(321, "NUMERATION FORMULE SANGUINE(NFS)", 3000, 1, 3000, "-");
                dgvAnal.Rows.Add(197, "SDW", 3000, 1, 3000, "-");

                var montantTotal = 0m;
                for (int i = 0; i <= dgvAnal.Rows.Count - 1; i++)
                {
                    montantTotal += decimal.Parse(dgvAnal.Rows[i].Cells[4].Value.ToString());
                }
                txtmontanttotal.Text = montantTotal.ToString();
                button3.Enabled = true;
                etat = "1";
            }
            else if (e.KeyCode == Keys.D && e.Modifiers == Keys.Control)
            {
                dgvAnal.Rows.Clear();
                dgvAnal.Rows.Add(194, "GE-RH", 1500, 1, 1500, "-");
                dgvAnal.Rows.Add(321, "NUMERATION FORMULE SANGUINE(NFS)", 3000, 1, 3000, "-");
                dgvAnal.Rows.Add(492, "CRP QUALITATVE", 3000, 1, 3000, "-");

                var montantTotal = 0m;
                for (int i = 0; i <= dgvAnal.Rows.Count - 1; i++)
                {
                    montantTotal += decimal.Parse(dgvAnal.Rows[i].Cells[4].Value.ToString());
                }
                txtmontanttotal.Text = montantTotal.ToString();
                button3.Enabled = true;
                etat = "1";
                txtRechercher.Focus();
            }
            else if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
            {
                dgvAnal.Rows.Clear();
                dgvAnal.Rows.Add(194, "GE-RH", 1500, 1, 1500, "-");
                dgvAnal.Rows.Add(321, "NUMERATION FORMULE SANGUINE(NFS)", 3000, 1, 3000, "-");
                dgvAnal.Rows.Add(197, "SDW", 3000, 1, 3000, "-");
                dgvAnal.Rows.Add(500, "SELLES KAOP", 1500, 1, 1500, "-");

                var montantTotal = 0m;
                for (int i = 0; i <= dgvAnal.Rows.Count - 1; i++)
                {
                    montantTotal += decimal.Parse(dgvAnal.Rows[i].Cells[4].Value.ToString());
                }
                txtmontanttotal.Text = montantTotal.ToString();
                button3.Enabled = true;
                etat = "1";
                txtRechercher.Focus();
            }
            else if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
            {
                dgvAnal.Rows.Clear();
                dgvAnal.Rows.Add(194, "GE-RH", 1500, 1, 1500, "-");
                dgvAnal.Rows.Add(492, "CRP QUALITATVE", 3000, 1, 3000, "-");
                dgvAnal.Rows.Add(321, "NUMERATION FORMULE SANGUINE(NFS)", 3000, 1, 3000, "-");
                dgvAnal.Rows.Add(500, "SELLES KAOP", 1500, 1, 1500, "-");

                var montantTotal = 0m;
                for (int i = 0; i <= dgvAnal.Rows.Count - 1; i++)
                {
                    montantTotal += decimal.Parse(dgvAnal.Rows[i].Cells[4].Value.ToString());
                }
                txtmontanttotal.Text = montantTotal.ToString();
                button3.Enabled = true;
                etat = "1";
                txtRechercher.Focus();
            }
            else if (e.KeyCode == Keys.I && e.Modifiers == Keys.Control)
            {
                dgvAnal.Rows.Clear();
                dgvAnal.Rows.Add(164, "CALCIUM", 2000, 1, 2000, "-");
                dgvAnal.Rows.Add(162, "MAGNESIUM", 2000, 1, 2000, "-");
                dgvAnal.Rows.Add(163, "POTASSIUM", 2000, 1, 2000, "-");
                dgvAnal.Rows.Add(197, "SODIUM", 2000, 1, 2000, "-");
                dgvAnal.Rows.Add(214, "PHOSPHORE", 2000, 1, 2000, "-");
                dgvAnal.Rows.Add(215, "CHLORE", 2000, 1, 2000, "-");

                var montantTotal = 0m;
                for (int i = 0; i <= dgvAnal.Rows.Count - 1; i++)
                {
                    montantTotal += decimal.Parse(dgvAnal.Rows[i].Cells[4].Value.ToString());
                }
                txtmontanttotal.Text = montantTotal.ToString();
                button3.Enabled = true;
                etat = "1";
                txtRechercher.Focus();
            }
        }

        private void cmbMedecin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbMedecin.Text.ToUpper() == "PARTENAIRE EXTERNE")
            //{
            //    //txtPartenaire.Visible = true;
            //    //txtPartenaire.Focus();
            //}
            //else
            //{
            //    txtPartenaire.Visible = false;
            //}
        }

        private void cmbEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            
                dataGridView2.Rows.Clear();
                var listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise("", cmbEntreprise.Text);
                foreach (var patient in listePatient)
                {
                    var age = AfficherAge(patient.An, patient.Mois).ToLower();
                    dataGridView2.Rows.Add
                (
                    patient.NumeroPatient.ToString(),
                    patient.Nom.ToUpper() +" "+ patient.Prenom.ToUpper(),
                   age,
                    patient.Sexe,
                    patient.NomEntreprise
                );
                }
            }
            catch { }
        }

        private void txtmontanttotal_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == Convert.ToInt32(txtRechercher.Text)
                                   select p;

                if (listePatient.Count() > 0)
                {
                    dataGridView2.Rows.Clear();
                    foreach (var patient in listePatient)
                    {
                       dataGridView2.Rows.Add(
                           patient.NumeroPatient.ToString(),
                    patient.Nom.ToUpper() +" " + patient.Prenom.ToUpper(),
                   AfficherAge(patient.An, patient.Mois),patient.Sexe,
                    patient.NomEntreprise,patient.Couvert);
                    }
                } 
            }
            catch { }
        }

        private void listView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.End)
            {
                txtRechercher.Focus();
            }
        }

        private void txtRechercher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dataGridView2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                linkLabel1_LinkClicked(null, null);
            }
            else if (e.KeyCode == Keys.Right)
            {
            textBox1.Focus();
            }
        }
        private void txtRechercher_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar ==(char) Keys.)
            //{
            //    //linkLabel1_LinkClicked(null,null);
            //}
        }
        private void cmbMedecin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbMedecin.Text != "")
                {
                    button3.Focus();
                }
                else
                {
                    cmbMedecin.Focus();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ListesDesExamens())
                {
                    dataGridView1.Focus();
                }
                else
                {
                    textBox1.Focus();
                    textBox1.SelectAll();
                }
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {              
                txtRechercher.Focus();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvAnal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { dataGridView1_DoubleClick(null, null);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
               
                    bool found = false;
                    decimal montantTotal = 0;
                    if (dgvAnal.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dtGrid in dgvAnal.Rows)
                        {
                            if (dtGrid.Cells[0].Value.ToString().Equals(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                            {
                                found = true;
                                textBox1.Text = "";
                                textBox1.Focus();
                            }
                        }
                        if (!found)
                        {
                            dgvAnal.Rows.Add(
                                dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                               dataGridView1.SelectedRows[0].Cells[1].Value.ToString(),
                               dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), 1,
                               dataGridView1.SelectedRows[0].Cells[2].Value.ToString(),
                               dataGridView1.SelectedRows[0].Cells[3].Value.ToString()
                               );
                            textBox1.Text = "";
                        }
                    }
                    else
                    {
                        dgvAnal.Rows.Add(
                             dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                            dataGridView1.SelectedRows[0].Cells[1].Value.ToString(),
                            dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), 1,
                            dataGridView1.SelectedRows[0].Cells[2].Value.ToString(),
                            dataGridView1.SelectedRows[0].Cells[3].Value.ToString()
                            );
                        textBox1.Text = "";
                    }
                    for (int i = 0; i <= dgvAnal.Rows.Count - 1; i++)
                    {
                        montantTotal += decimal.Parse(dgvAnal.Rows[i].Cells[4].Value.ToString());
                    }
                    txtmontanttotal.Text = montantTotal.ToString();
                    button3.Enabled = true;
                    etat = "1";
                    label1.Text = "0";
                    textBox1.Focus();
                    var designation = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    var count = ConnectionClassClinique.CountDetailsActes(designation, DateTime.Now.Date, DateTime.Now.Date);
                    count += ConnectionClassClinique.CountDetailsActesDansConventionnes(designation, DateTime.Now.Date, DateTime.Now.Date);
                    label1.Text = count.ToString();
            }
            catch (Exception) { }  
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    button3.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            linkLabel1_LinkClicked(null, null);
        }

        DateTime ObtenirDebutJour(int mois)
        {
            return DateTime.Parse("01/" + mois + "/" + DateTime.Now.Year);
        }

        DateTime ObtenirFinJour(int mois)
        {
            if (mois == 1)
            {
                return DateTime.Parse("31/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 2)
            {
                if (DateTime.IsLeapYear(DateTime.Now.Year))
                    return DateTime.Parse("29/" + mois + "/" + DateTime.Now.Year);
                else
                    return DateTime.Parse("28/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 3)
            {
                return DateTime.Parse("31/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 4)
            {
                return DateTime.Parse("30/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 5)
            {
                return DateTime.Parse("31/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 6)
            {
                return DateTime.Parse("30/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 7)
            {
                return DateTime.Parse("31/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 8)
            {
                return DateTime.Parse("31/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 9)
            {
                return DateTime.Parse("30/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 10)
            {
                return DateTime.Parse("31/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 11)
            {
                return DateTime.Parse("30/" + mois + "/" + DateTime.Now.Year);
            }
            else if (mois == 12)
            {
                return DateTime.Parse("31/" + mois + "/" + DateTime.Now.Year);
            }
            else
            {
                return DateTime.Now;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                double montantTotal;
              
                montantTotal = Double.Parse(txtmontanttotal.Text);
                var partenaire = "";

                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    }
                    else if (idPatient > 0)
                    {
                        //idPatient = idPatient;
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner le nom du patient sur la liste.", "Erreur", "erreur.png");
                        return;
                    }
                    var nomPatient = dataGridView2.SelectedRows[0].Cells[1].Value.ToString() + " " +
                   dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                    var analyse = new Analyse();
                    analyse.NumeroAnalyse = idAnalyse;
                    analyse.DateAnalyse = dateTimePicker1.Value;
                    analyse.IdPatient = idPatient;
                    analyse.NumeroEmploye = GestionAcademique.LoginFrm.matricule;
                    analyse.MontantTotal = montantTotal;
                    analyse.Libelle = partenaire;
                    if (!string.IsNullOrEmpty(dataGridView2.SelectedRows[0].Cells[4].Value.ToString()) &&
                                   bool.Parse(dataGridView2.SelectedRows[0].Cells[5].Value.ToString()))
                    {
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                    {
                        return;
                    }

                    var montantMensuelParPatient = ConnectionClassClinique.TotalProformaDuPatient(idPatient, ObtenirDebutJour(DateTime.Now.Month), ObtenirFinJour(DateTime.Now.Month));
                    montantMensuelParPatient += ConnectionClassPharmacie.MontantDesCredit(dataGridView2.Rows[0].Cells[1].Value.ToString(), dataGridView2.Rows[0].Cells[4].Value.ToString(), ObtenirDebutJour(DateTime.Now.Month), ObtenirFinJour(DateTime.Now.Month));
                    var entreprise = ConnectionClassClinique.ListeDesEntreprises(dataGridView2.Rows[0].Cells[4].Value.ToString());
                    var montantLimite = entreprise[0].MontantLimite;
                    var siLimit = entreprise[0].SiLimite;
                    //if ((montantMensuelParPatient + montantTotal >= montantLimite) && siLimit)
                    //{
                    //    MonMessageBox.ShowBox("La prise en charge de la convention " + dataGridView2.Rows[0].Cells[4].Value.ToString() + " ne doit pas excéder " + montantLimite + " \nTotal prise en charge du mois en cours: " + montantMensuelParPatient, "Erreur", "erreur.png");
                    //    return;
                    //}
                    if (ConnectionClassClinique.BondesExamens(analyse, dgvAnal))
                        {
                            idExamen = ConnectionClassClinique.ObtenirDernierNumeroAnalyse(GestionAcademique.LoginFrm.matricule);

                            var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                               where p.NumeroPatient == idPatient
                                               select p;
                            var patient = new Patient();
                            foreach (var p in listePatient)
                                patient = p;
                        var listeFacture = from f in ConnectionClassClinique.TableDesDetailsFacturesProforma()
                                           where f.NumeroActe == idAnalyse
                                           where f.Sub == "EXAMEN"
                                           select f.NumeroFacture;
                        var numeroFacture = 0;
                        foreach (var a in listeFacture)
                            numeroFacture = a;
                        analyseBitmap = Impression.ImprimerAnalyse(numeroFacture, patient, dateTimePicker1.Value, GestionAcademique.LoginFrm.nom,1);
                        if (printDialog1.ShowDialog() == DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printPreviewDialog1.ShowDialog();
                        }
                        idAnalyse = 0;
                            idExamen = 0;
                            num_examen = 0;
                        state = "0";
                        dgvAnal.Rows.Clear();
                        txtmontanttotal.Text = "";
                    }

                }
                    else
                    {
                        if (dataGridView2.SelectedRows[0].Cells[3].Value.ToString() == "M")
                        {
                            MonMessageBox.ShowBox("Ce patient n'est pas couvert.", "Erreur", "erreur.png");
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Cette patiente n'est pas couverte.", "Erreur", "erreur.png");
                        }
                    }
            }
            catch { }
        }

        private void rdb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb2.Checked == true)
            {
                dataGridView1.Rows.Clear();
                var listeAnalyse = ConnectionClassClinique.ListeDesAnalyses();
                clFRAIS.HeaderText = "FRAIS CONVENTIONNES";
                ListesDesExamens();
            }
        }

        private void rdb1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb1.Checked == true)
            {
                dataGridView1.Rows.Clear();
                var listeAnalyse = ConnectionClassClinique.ListeDesAnalyses();
                clFRAIS.HeaderText = "FRAIS";
                ListesDesExamens();
            }

        }

        private void rdb3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb3.Checked == true)
            {
                dataGridView1.Rows.Clear();
                var listeAnalyse = ConnectionClassClinique.ListeDesAnalyses();
                clFRAIS.HeaderText = "FRAIS M INTERNE";
                ListesDesExamens();
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                //if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper().Contains("FRAIS CONVENTIONNES"))
                //{
                //    rdb2.Checked = true;
                //}
                //else if (dataGridView2.SelectedRows[0].Cells[4].Value.ToString().ToUpper().Contains("FRAIS M INTERNES"))
                //{
                //    rdb3.Checked = true;
                //}
                //else
                //{
                //    rdb1.Checked = true;
                //}
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new GestionDuneClinique.FormesClinique.TableDesAnalyseFrm();
                frm.Location = new Point(GestionAcademique.Form1.xLocation, GestionAcademique.Form1.yLocation);
                frm.Height = GestionAcademique.Form1.height;
                frm.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

        private void cmbMedecin_Click(object sender, EventArgs e)
        {
            cmbMedecin.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                label1.Text = "0";
                var designation = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                var count = ConnectionClassClinique.CountDetailsActes(designation, DateTime.Now.Date, DateTime.Now.Date);
                count += ConnectionClassClinique.CountDetailsActesDansConventionnes(designation, DateTime.Now.Date, DateTime.Now.Date);
                label1.Text = count.ToString();
            }
            catch { }
        }

    }
}
