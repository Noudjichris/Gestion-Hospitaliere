using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using GestionDuneClinique.FormesClinique;

namespace GestionDuneClinique.Formes
{
    public partial class FactureFrm : Form
    {
        public FactureFrm()
        {
            InitializeComponent();
        }

        private void FactureFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 128, 0), 0);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.FromArgb(255, 128, 0), Color.FromArgb(255, 128, 0), LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
       
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox3.Width - 1, groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
     
        private void groupBox9_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox9.Width - 1, groupBox9.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),Color.FromArgb(255, 128, 0), LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox4.Width - 1, groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void FactureFrm_Load(object sender, EventArgs e)
        {
            cl1.Width = dataGridView1.Width / 2;
            cl2.Width = dataGridView1.Width / 6;
            cl3.Width = dataGridView1.Width / 8;
            cl4.Width =  dataGridView1.Width / 5+4;
            //checkBox1.Checked=true;
            //listView1.Columns.Add("ID", 0);
            //listView1.Columns.Add("DATE FACTURATION", listView1.Width / 4+1);
            //listView1.Columns.Add("MONTANT", listView1.Width/4 );
            //listView1.Columns.Add("ID PATIENT", 0);
            //listView1.Columns.Add("PATIENT", listView1.Width / 4);
            //listView1.Columns.Add("ID EMPLOYE", 0);
            //listView1.Columns.Add("EMPLOYE", listView1.Width / 4);
            //listView1.Columns.Add("DIFFERENCES",0);
            //liste des factures
            var listeFacture = new List<Facture>();
            if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
            {
                button7.Enabled = true;
                if (checkBox1.Checked)
                {
                    listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                       DateTime.Now.Date, DateTime.Now.AddHours(24));
                }
                else
                {
                    listeFacture = ConnectionClassClinique.ListeDesFactures("", DateTime.Now.Date.AddHours(-24), DateTime.Now.AddHours(24));
                }
            }
            else
            {
                checkBox1.Enabled = false;
                listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                        DateTime.Now.Date, DateTime.Now.AddHours(24));
            }
            ListeFacturation(listeFacture);
            btnAjouterAnalyse.Focus();
        }
        //liste des factures
        void ListeFacturation(List<Facture> listeFacture)
        {
            try
            {
                dataGridView2.Rows.Clear();
                foreach(Facture facture in listeFacture)
                {
                    var totalPaiemt = 0.0;
                    var detPaiement = ConnectionClassClinique.TablePaiement(facture.NumeroFacture);
                    for (int i = 0; i < detPaiement.Rows.Count; i++)
                    {
                        totalPaiemt += double.Parse(detPaiement.Rows[i].ItemArray[0].ToString());
                    }
                    var items = new string[] 
                    { 
                        facture.NumeroFacture.ToString(),
                        facture.DateFacture.ToString(),
                        facture.MontantFactural.ToString(),
                        facture.IdPatient.ToString(),
                        facture.Patient.ToUpper(),
                        facture.NumeroEmploye,
                        facture.NomEmploye,
                        facture.Reste.ToString()
                    };
                    dataGridView2.Rows.Add
                    ( 
                        facture.NumeroFacture.ToString(),
                        facture.DateFacture.ToString(),
                        facture.MontantFactural.ToString(),
                        facture.IdPatient.ToString(),
                        facture.Patient.ToUpper(),
                        facture.NumeroEmploye,
                        facture.NomEmploye,
                        facture.Reste.ToString()
                    );
                    //var listItems = new ListViewItem(items);
                    //listView1.Items.Add(listItems);

                    
            }
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (double.Parse(row.Cells[7].Value.ToString()) !=0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        //row.DefaultCellStyle.ForeColor = Color.DarkSlateBlue;
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste facture", ex);
            }
        }
  
       
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                { return; }
                numAnalyse = 0;
                if (MonMessageBox.ShowBox("Voulez vous ajouter de nouveau analyse?", "Confirmation", "confirmation.png") == "1")
                {
                    GestionPharmacetique.Forme.ExamenFrm.state = "1";
                   //GestionPharmacetique.Forme.ExamenFrm.
                    if (GestionPharmacetique.Forme.ExamenFrm.ShowBox() == "1")
                    {
                        numAnalyse = GestionPharmacetique.Forme.ExamenFrm.idExamen;
                        txtPatient.Text = GestionPharmacetique.Forme.ExamenFrm.patiente;
                        idPatient = GestionPharmacetique.Forme.ExamenFrm.idPatiente;
                    }
                }
                else
                {
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                    {
                        ListeExamFrm.State = true;
                    }
                    else
                    {
                        ListeExamFrm.State = false;
                    }
                    if (ListeExamFrm.ShowBox() == "1")
                    {
                        numAnalyse = ListeExamFrm.id;
                        txtPatient.Text = ListeExamFrm.patient;
                        idPatient = ListeExamFrm.idPatient;
                    }
                }

                var dtAnalyse = ConnectionClassClinique.TableDesAnalysesEffectues(numAnalyse);

                lblAnalyse.Text = dtAnalyse.Rows[0].ItemArray[5].ToString();
                txtAnalyse.Text = "Frais d'analyse".ToUpper();
                var detailAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectues(numAnalyse);
                var montant = 0.0;
                foreach (Analyse analyse in detailAnalyse)
                {
                    var typeAnalyse = ConnectionClassClinique.ListeDesAnalyses(analyse.NumeroListeAnalyse);

                    var type = typeAnalyse[0].TypeAnalyse;
                    var idLibelle = typeAnalyse[0].NumeroGroupe;
                    var lst = ConnectionClassClinique.ListeDesLibelles(idLibelle);
                    var libelle = "";
                    if (lst.Count >0)
                    {
                         libelle = ConnectionClassClinique.ListeDesLibelles(idLibelle)[0].Designation;
                    }
                    dataGridView1.Rows.Add(type.ToUpper(), analyse.Frais, analyse.NombreAnalyse,
                        analyse.Frais * analyse.NombreAnalyse,libelle);
                }
                for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    montant += Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                }
                txtTotal.Text = montant.ToString();
                txtReste.Text = montant.ToString();
                textBox3.Text = "0";
                if (dataGridView1.Rows.Count > 0)
                {
                    etat = "1";
                    btnEnregistrer.Enabled = true;
                    txtPaye.Focus(); txtPaye.Text = montant.ToString();
                    indexModification = "anal";
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception);
            }
        }
        int numConsultation;
        int numHosp ;
        int numAnalyse;
        int idPatient;
        string indexModification;
        private void btnAjouterAnalyse_Click(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                { return; }
                if (MonMessageBox.ShowBox("Voulez vous ajouter une nouvelle consultation?", "Consultation", "consultation.png") == "1")
                {
                    idPatient = 1;
                    ConsultationFrm.State = "1";
                    if (ConsultationFrm.ShowBox() == "1")
                    {

                        numConsultation = ConsultationFrm.idConsultation;
                        txtConsultation.Text = "FRAIS CONSULTATION";
                        var dtConsultation = ConnectionClassClinique.TableDesAnalysesEffectues(ConsultationFrm.idConsultation);
                        lblConsultation.Text = ConsultationFrm.montant.ToString();
                        txtPatient.Text = ConsultationFrm.patient;
                        idPatient = ConsultationFrm.idPatiente;
                        dataGridView1.Rows.Add(ConsultationFrm.typeConsultation.ToUpper(), lblConsultation.Text, "1", lblConsultation.Text,"CONSULTATION");
                        var montant = 0.0;
                        for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                        {
                            montant += Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        }
                        txtTotal.Text = montant.ToString();
                        txtReste.Text = montant.ToString();
                        textBox3.Text = "0";
                        if (dataGridView1.Rows.Count > 0)
                        {
                            etat = "1";
                            btnEnregistrer.Enabled = true;
                            txtPaye.Text = montant.ToString();
                            txtPaye.Focus();
                        }
                    }
                }
                else
                {

                    if (ListeConsultation.ShowBox() == "1")
                    {
                        numConsultation = ListeConsultation.id;
                        txtConsultation.Text = "FRAIS CONSULTATION";
                        var dtConsultation = ConnectionClassClinique.TableDesAnalysesEffectues(ListeConsultation.id);
                        lblConsultation.Text = ListeConsultation.montant.ToString();
                        txtPatient.Text = ListeConsultation.patient;
                        idPatient = ListeConsultation.idPatient;
                        dataGridView1.Rows.Add(ListeConsultation.typeConsultation, lblConsultation.Text, "1", lblConsultation.Text,"CONSULTATION");
                        var montant = 0.0;
                        for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                        {
                            montant += Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        }
                        txtTotal.Text = montant.ToString();
                        txtReste.Text = montant.ToString();
                        textBox3.Text = "0";
                        if (dataGridView1.Rows.Count > 0)
                        {
                            etat = "1";
                            btnEnregistrer.Enabled = true;
                            txtPaye.Text = montant.ToString();
                            txtPaye.Focus();
                            indexModification = "consult";
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                { return; }
                int nbreJour = 0; ;
                double montantHosp = 0.0;
                if (MonMessageBox.ShowBox("Voulez vous ajouter des nouvelles données d'hospitalisation?", "Confirmation", "confirmation.png") == "1")
                {
                    HospitalisationFrm.state = "1";
                    if (HospitalisationFrm.ShowBox() == "1")
                    {
                        txtHosp.Text = lblHospitalisation.Text = HospitalisationFrm.libelle.ToUpper();
                        lblHospitalisation.Text = HospitalisationFrm.montant.ToString();
                        nbreJour = HospitalisationFrm.nbre;
                        montantHosp = HospitalisationFrm.montant;
                        idPatient = HospitalisationFrm.idPatiente;
                        txtPatient.Text = HospitalisationFrm.patiente; numHosp = HospitalisationFrm.idHospt;
                    }
                }
                else
                {
                    if (ListeHospitalisationFrm.ShowBox() == "1")
                    {
                        txtHosp.Text = "Frais d'hospitalisation".ToUpper();
                        numHosp = ListeHospitalisationFrm.id;
                        montantHosp = ListeHospitalisationFrm.montant;
                        txtPatient.Text = ListeHospitalisationFrm.patient;
                        idPatient = ListeHospitalisationFrm.idPatient;
                        nbreJour = ListeHospitalisationFrm.nbre;
                    }
                }
                if (montantHosp > 0)
                {
                    lblHospitalisation.Text = montantHosp.ToString();
                    var fraisJournalier = montantHosp / nbreJour;
                    dataGridView1.Rows.Add(txtHosp.Text, fraisJournalier, nbreJour, montantHosp, numHosp);
                    var montant = 0.0;
                    for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    {
                        montant += Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    }
                    txtTotal.Text = montant.ToString();
                    txtReste.Text = "0";
                    txtPaye.Text = montant.ToString();
                    textBox3.Text = "0";
                    if (dataGridView1.Rows.Count > 0)
                    {
                        etat = "1";
                        btnEnregistrer.Enabled = true;
                    }
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
                }
            }
            catch (Exception)
            {
            }
        }

        //enregistrer
        int numFacture;
        string etat;
        double montantPaye;
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPatient.Text !="")
                {
                        if (Double.TryParse(txtPaye.Text, out montantPaye))
                        {
                            //if (montantPaye == 0)
                            //{
                            //    txtPaye.BackColor = Color.Red;
                            //    return;
                            //}
                            DateTime dateFacture = DateTime.Now;
                            double montantTotal;
                            if (Double.TryParse(label1.Text, out montantTotal))
                            {
                            }
                            else
                            {
                                montantTotal = Convert.ToDouble(txtTotal.Text);
                            }

                            var reste =double.Parse(txtReste.Text);
                            if (reste < 0 || reste >0)
                            {
                                return;
                            }
                            var numEmpl = GestionAcademique.LoginFrm.matricule;
                            var facture = new Facture();
                            facture.NumeroFacture = numFacture;
                            facture.DateFacture = dateFacture;
                            facture.MontantFactural = montantTotal;
                            facture.IdPatient = idPatient;
                            facture.NumeroEmploye = numEmpl;
                            facture.Reste = reste;

                            if (etat == "1")
                            {
                                if (ConnectionClassClinique.EnregistrerUneFacture(facture, dataGridView1, montantPaye))
                                {

                                    button3_Click(null, null);
                                    var listeFacture = new List<Facture>();
                                    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                                    {
                                        if (checkBox1.Checked)
                                        {
                                            listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                                               DateTime.Now.Date, DateTime.Now.AddHours(24));
                                        }
                                        else
                                        {
                                            listeFacture = ConnectionClassClinique.ListeDesFactures("", DateTime.Now.Date, DateTime.Now.AddHours(24));
                                        }
                                    }
                                    else
                                    {
                                        checkBox1.Enabled = false;
                                        listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                                                DateTime.Now.Date, DateTime.Now.AddHours(24));
                                    }
                                    ListeFacturation(listeFacture);
                                    btnImprimer.Enabled = true;
                                    btnApercc.Enabled = true;
                                        btnDetail.Focus();
                                        txtPaye.BackColor = Color.White;
                                        
                                }
                            }
                            else if (etat == "2")
                            {
                                if (MonMessageBox.ShowBox("Voulez vous modifier les données de cette facture ?", "Confirmation", "confirmation.png") == "1")
                                {
                                    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                                    {
                                        if (ConnectionClassClinique.ModifierUneFacture(facture, dataGridView1, txtPaye.Text))
                                        {

                                            button3_Click(null, null);
                                            var listeFacture = new List<Facture>();

                                            if (checkBox1.Checked)
                                            {
                                                listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                                                   DateTime.Now.Date, DateTime.Now.AddHours(24));
                                            }
                                            else
                                            {
                                                listeFacture = ConnectionClassClinique.ListeDesFactures("", DateTime.Now.Date, DateTime.Now.AddHours(24));
                                            }
                                        }
                                        
                                        else
                                        {
                                            //checkBox1.Enabled = false;
                                            //listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                                            //        DateTime.Now.Date, DateTime.Now.AddHours(24));
                                        }
                                        //ListeFacturation(listeFacture);
                                        }
                                    
                                    

                                }
                            }
                        }
                        else
                        {
                            txtPaye.BackColor = Color.Red;
                            txtPaye.Focus();
                            //MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le montant de paiement si le patient a payé ", "Erreur", "erreur.png");
                        }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le nom du patient sur la liste ", "Erreur", "erreur");
                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrer facture", ex);
            }
        }
        private void btnRetirer_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {

                    if (etat == "1")
                    {

                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                        double montant; int qte;
                        var total = 0.0;
                        foreach (DataGridViewRow dtGrid in dataGridView1.Rows)
                        {
                            if (Double.TryParse(dtGrid.Cells[1].Value.ToString(), out montant) &&
                                Int32.TryParse(dtGrid.Cells[2].Value.ToString(), out qte ))
                            {
                                total += montant*qte;
                            }
                            txtTotal.Text = total.ToString();
                            txtPaye.Text = total.ToString();
                        }
                    }
                    else if (etat == "2")
                    {
                        double montant;
                        var total = 0.0; int qte;
                        var libelle = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        var montantFrais = double.Parse( dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                     
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                        foreach (DataGridViewRow dtGrid in dataGridView1.Rows)
                        {
                            if (Double.TryParse(dtGrid.Cells[1].Value.ToString(), out montant) &&
                                Int32.TryParse(dtGrid.Cells[2].Value.ToString(), out qte))
                            {
                                total += montant * qte;
                            }
                            txtTotal.Text = total.ToString();
                        }
                        if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                        {
                            if (MonMessageBox.ShowBox("Voulez vous retirez ces données de la facture?", "Confirmation", "confirmation") == "1")
                            {
                                ConnectionClassClinique.SupprimerUneFacture(numFacture, libelle, total, montantFrais);
                                btnDetail_Click(null, null);
                            }
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    txtTotal.Text = "0";
                    btnEnregistrer.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control,
                LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        string nomEmploye;
        //detail facture
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    button3_Click(null,null);
                    numFacture = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[3].Value.ToString());
                     var listeFacture = ConnectionClassClinique.ListeDesFactures(numFacture);
                     var detailListe = ConnectionClassClinique.DetailsDesFactures(numFacture);
                     var dtPaie = ConnectionClassClinique.TablePaiement(numFacture);
                    txtPatient.Text = listeFacture[0].Patient;
                   
                    nomEmploye = listeFacture[0].NomEmploye;
                    dataGridView1.Rows.Clear();
                    etat = "2";
                    btnEnregistrer.Enabled = true;
                    var montant = 0.0;
                    foreach(Facture facture in detailListe)
                    {
                        dataGridView1.Rows.Add(facture.Designation, facture.Prix, facture.Quantite,facture.PrixTotal);
                        montant += facture.Prix * facture.Quantite;
                    }
                    txtTotal.Text = montant.ToString();
                    var totalPaye = 0.0;
                    if (dtPaie.Rows.Count > 0)
                    {
                        totalPaye = Double.Parse(dtPaie.Rows[0].ItemArray[0].ToString());
                    }
                    else
                    {
                        totalPaye = 0.0;
                    }
                    var reste = listeFacture[0].MontantFactural - totalPaye;
                    textBox3.Text = totalPaye.ToString();
                    txtReste.Text = listeFacture[0].Reste.ToString();
                    //lblRemise.Text = "0";
                    if ((totalPaye + reste) < montant)
                    {
                        var totalRemise = montant - totalPaye - reste;
                        var remise = totalRemise * 100 / montant;
                        txtReduction.Text = remise.ToString();
                        lblRemise.Text = totalRemise.ToString();
                        txtPaye.Text  = "";
                    }
                    else
                    {
                        txtReduction.Text = "";
                        lblRemise.Text = "";
                        label1.Text = "";
                    }

                    btnImprimer.Enabled = true;
                    btnApercc.Enabled = true;
                    btnImprimer.Focus();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Detail facture",ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtHosp.Text = "";
            txtConsultation.Text = "";
            txtAnalyse.Text = "";
            label1.Text = "";
            dataGridView1.Rows.Clear();
            txtTotal.Text = "";
            txtPatient.Text = "";
            txtPaye.Text = "";
            txtReste.Text = "";
            lblAnalyse.Text = "";
            txtReduction.Text = "";
            lblHospitalisation.Text = "" ;
            btnApercc.Enabled = false;
            btnImprimer.Enabled = false;
            textBox3.Text = "";
            lblConsultation.Text = "";
            lblRemise.Text = "";
            btnEnregistrer.Enabled = false;
            lblObservation.Text = "";
            lblMontantObserv.Text = "";
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        Bitmap factureBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width-5;
            var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            e.Graphics.DrawImage(factureBitmap, 0, 10, width, height);
            //e.Graphics.DrawImage(factureBitmap, 0, 10, factureBitmap.Width, factureBitmap.Height);
            e.HasMorePages = false;
        }
        string modePaiement;
        private void btnApercc_Click(object sender, EventArgs e)
        {
            try
            {

                string nom = txtPatient.Text.Substring(0, txtPatient.Text.LastIndexOf(" "));
                string prenom = txtPatient.Text.Substring(txtPatient.Text.LastIndexOf(" ") + 1);
                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == idPatient
                                   select p;

                var patient = new Patient();
                foreach (var p in listePatient)
                    patient = p;
                var paye =  Double.Parse(textBox3.Text);
                 //var reste =  Double.Parse(txtReste.Text);
                 var remise = 0.0;
                 if (double.TryParse(lblRemise.Text, out remise))
                 {
                 }
                 factureBitmap = Impression.FactureOfficielle(numFacture, dataGridView1, patient, modePaiement,remise, nomEmploye);
               
                    printPreviewDialog1.ShowDialog();
                    btnImprimer.Enabled = false;
                    btnApercc.Enabled = false;
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Imprimer facture ", ex); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {
            try
            {
                string nom = txtPatient.Text.Substring(0, txtPatient.Text.LastIndexOf(" "));
                string prenom = txtPatient.Text.Substring(txtPatient.Text.LastIndexOf(" ") + 1);
                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == idPatient
                                   select p;

                var patient = new Patient();
                foreach (var p in listePatient)
                    patient = p;
                
                var paye = Double.Parse(textBox3.Text);
                //var reste = Double.Parse(txtReste.Text);
                var remise = 0.0;
                if (double.TryParse(lblRemise.Text,out remise ))
                {
                }
                factureBitmap = Impression.FactureOfficielle(numFacture, dataGridView1, patient, modePaiement, remise , nomEmploye);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printDocument1.Print();
                    button3_Click(null, null);
                    btnAjouterAnalyse.Focus();
                }
            }
            catch
            { }
        }
     

        private void txtPaye_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtPaye.BackColor = Color.White;
                double montantPaye, total, dejaPaye, remise = 0.0;
                if (Double.TryParse(txtPaye.Text, out montantPaye))
                {
                    //if (Double.TryParse(txtReduction.Text, out remise))
                    //{
                    //}
                    if (Double.TryParse(label1.Text, out total))
                    {
                    }
                    else
                    {
                        total = Convert.ToDouble(txtTotal.Text);
                    }
                    if (Double.TryParse(textBox3.Text, out dejaPaye))
                    {
                        double reste = total - montantPaye - dejaPaye;
                        txtReste.Text = reste.ToString();
                    }
                    else
                    {
                        double reste = total - montantPaye;
                        txtReste.Text = reste.ToString();
                    }

                }
                else
                {
                    txtReste.Text = "";
                }
            }
            catch { }
        }

                     private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                    {
                        var listeFacture = ConnectionClassClinique.ListeDesFactures(textBox4.Text);
                        ListeFacturation(listeFacture);
                        dataGridView2.Focus();
                    }
                    else
                    {
                        var listeFacture = ConnectionClassClinique.ListeDesFactures(textBox4.Text, GestionAcademique.LoginFrm.nom);
                        ListeFacturation(listeFacture);
                        dataGridView2.Focus();
                    }

                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void txtReduction_TextChanged(object sender, EventArgs e)
        {
            double remise;
            if (Double.TryParse(txtReduction.Text, out remise))
            {
                var montantTotal = 0.0;
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    montantTotal += Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                }
                var total = montantTotal * (1 - remise / 100);
                var montantRemis = montantTotal * remise / 100;
                label1.Text = total.ToString();
                txtPaye.Text = total.ToString();
                lblRemise.Text = montantRemis.ToString();
            }
            else
            {
                lblRemise.Text = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (GestionDuneClinique.FormesClinique.ListePatientFrm.ShowBox() == "1")
            {
                txtPatient.Text = GestionDuneClinique.FormesClinique.ListePatientFrm.patient;
                idPatient = GestionDuneClinique.FormesClinique.ListePatientFrm.idPatient;
                if (GestionDuneClinique.FormesClinique.ListePatientFrm.fraisCarnet > 0)
                {
                    //textBox1.Text = "FRAIS CARNET";
                    //textBox2.Text = GestionDuneClinique.FormesClinique.ListePatientFrm.fraisCarnet.ToString();
                }
            }
        }

        private void dgvFact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDetail_Click(null, null);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    cmbCaissier.Items.Clear();
                    cmbCaissier.Items.Add("<Toutes>");
                    var listeCaissier = ConnectionClassClinique.ListeDesEmployees();
                    var listeUtilisateur = ConnectionClassClinique.ListesDesUtilisateurs();
                    var listeCais = from lc in listeCaissier
                                    join lu in listeUtilisateur
                                    on lc.NumMatricule equals lu.NumEmploye
                                    where lu.TypeUtilisateur.ToLower() == "caissier"
                                    select new { lc.NomEmployee };
                    var listeAdmin = from lc in listeCaissier
                                     join lu in listeUtilisateur
                                     on lc.NumMatricule equals lu.NumEmploye
                                     where lu.TypeUtilisateur.ToLower() == "admin"
                                     select new { lc.NomEmployee };
                    foreach (var p in listeCais)
                    {
                        cmbCaissier.Items.Add(p.NomEmployee);
                    }
                    foreach (var p in listeAdmin)
                    {
                        cmbCaissier.Items.Add(p.NomEmployee);
                    }
                    cmbCaissier.Visible = true;
                }
                else
                {
                    cmbCaissier.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Chck", ex);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de cette factures", "Confirmation", "confirmation.png") == "1")
                {
                    if (dataGridView2.SelectedRows[0].Cells[6].Value.ToString() == GestionAcademique.LoginFrm.nom || GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                    {
                        ConnectionClassClinique.SupprimerUneFacture(Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()));
                        var listeFacture = new List<Facture>();
                        if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                        {
                            if (checkBox1.Checked)
                            {
                                listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                                   DateTime.Now.Date, DateTime.Now.AddHours(24));
                            }
                            else
                            {
                                listeFacture = ConnectionClassClinique.ListeDesFactures("", DateTime.Now.Date, DateTime.Now.AddHours(24));
                            }
                        }
                        else
                        {
                            checkBox1.Enabled = false;
                            listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                                    DateTime.Now.Date, DateTime.Now.AddHours(24));
                        }
                        ListeFacturation(listeFacture);
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Vous n'etes pas autorisés à supprimer les données de " + dataGridView2.SelectedRows[0].Cells[6].Value.ToString(), "Erreur", "erreur.png");
                    }
                }
            }
        }

        private void cmbCaissier_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var listeFacture = new List<Facture>();

                if (cmbCaissier.Text == "<Toutes>")
                {
                    listeFacture = ConnectionClassClinique.ListeDesFactures();
                }
                else
                {
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees(cmbCaissier.Text);
                    var numEmploye = listeEmploye[0].NumMatricule;
                    listeFacture = ConnectionClassClinique.ListeDesFacturesParEmploye(numEmploye);
                }
                ListeFacturation(listeFacture);
            }
            catch { }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                    {
                        var listeFacture = ConnectionClassClinique.ListeDesFacturesParID(Convert.ToInt32(textBox5.Text));
                        ListeFacturation(listeFacture);
                        dataGridView2.Focus();
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                    else
                    {
                        var listeFacture = ConnectionClassClinique.ListeDesFactures(Convert.ToInt32(textBox5.Text), GestionAcademique.LoginFrm.nom);
                        ListeFacturation(listeFacture);
                        dataGridView2.Focus();
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }

                }
            }
            catch (Exception Exception) { MonMessageBox.ShowBox("Facture", Exception); }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                {
                    ConnectionClassClinique.ModifierUneFacture(Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()));

                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
          
        }

        private void txtPaye_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnEnregistrer_Click(null, null);
                    
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Home )
                {
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                    {
                        var liste = new GestionDuneClinique.FormesClinique.ListeFrm();
                        liste.state = "1";
                        liste.ShowDialog();
                    }
                }
                else if (e.KeyCode == Keys.Right && e.Modifiers == Keys.Control)
                {
                    var liste = new GestionDuneClinique.FormesClinique.ListeFrm();
                    liste.state = "2";
                    liste.ShowDialog();
                }
            }
            catch { }
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDetail_Click(null, null);            
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                bool found = false;
                decimal montantTotal = 0;
                var frais = 500;
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                { return; }
                if (dataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dtGrid in dataGridView1.Rows)
                    {
                        if (dtGrid.Cells[0].Value.ToString().Equals("CARNET"))
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        dataGridView1.Rows.Add("CARNET", frais, "1", frais, "CARNET MEDICAL");
                    }

                }
                else
                {
                    dataGridView1.Rows.Add("CARNET", frais, "1", frais, "CARNET MEDICAL");
                }
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    montantTotal += decimal.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                }
                txtTotal.Text = montantTotal.ToString();
                txtPaye.Text = montantTotal.ToString();
                button3.Enabled = true;
                btnEnregistrer.Enabled = true;
                etat = "1";
                txtPaye.Focus();

            }
            catch { }
        }

        private void txtPaye_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                button5.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                { return; }
                int nbreJour = 0; ;
                double montantObservation = 0.0, fraisObservation=0.0;
                if (MonMessageBox.ShowBox("Voulez vous ajouter des nouvelles données d'hospitalisation?", "Confirmation", "confirmation.png") == "1")
                {
                    ObservationFrm.state = "1";
                    if (ObservationFrm.ShowBox() == "1")
                    {
                    
                        nbreJour = ObservationFrm.nbreJour;
                        montantObservation = ObservationFrm.montant;
                        idPatient = ObservationFrm.numeroPatient;
                        txtPatient.Text = ObservationFrm.patiente; numHosp = ObservationFrm.idHospt;
                        fraisObservation = ObservationFrm.fraisObservation;
                    }
                }
                else
                {
                    //if (ListeHospitalisationFrm.ShowBox() == "1")
                    //{
                    //    txtHosp.Text = "Frais d'hospitalisation".ToUpper();
                    //    numHosp = ListeHospitalisationFrm.id;
                    //    montantHosp = ListeHospitalisationFrm.montant;
                    //    txtPatient.Text = ListeHospitalisationFrm.patient;
                    //    idPatient = ListeHospitalisationFrm.idPatient;
                    //    nbreJour = ListeHospitalisationFrm.nbre;
                    //}
                }
                if (montantObservation > 0)
                {
                    //lblHospitalisation.Text = montantObservation.ToString();
                    lblObservation.Text = ObservationFrm.libelle.ToUpper();
                    lblMontantObserv.Text = montantObservation.ToString();
                    dataGridView1.Rows.Add(txtHosp.Text, fraisObservation, nbreJour, montantObservation, numHosp);
                    var montant = 0.0;
                    for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                    {
                        montant += Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    }
                    txtTotal.Text = montant.ToString();
                    txtReste.Text = "0";
                    txtPaye.Text = montant.ToString();
                    textBox3.Text = "0";
                    if (dataGridView1.Rows.Count > 0)
                    {
                        etat = "1";
                        btnEnregistrer.Enabled = true;
                    }
                }

            }
            catch (Exception)
            {
            }
        }       
    }
}
