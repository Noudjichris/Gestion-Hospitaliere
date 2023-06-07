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
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
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
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
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
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
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
            button1.Location = new Point(Width - 35, 4);
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

        int numeroActe;
        string typeActe;
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
                { return; }
                if (MonMessageBox.ShowBox("Voulez vous ajouter de nouveau analyse?", "Confirmation", "confirmation.png") == "1")
                {
                    GestionPharmacetique.Forme.ExamenFrm.state = "1";
                    if (GestionPharmacetique.Forme.ExamenFrm.ShowBox() == "1")
                    {
                        numeroActe = GestionPharmacetique.Forme.ExamenFrm.idExamen;
                        txtPatient.Text = GestionPharmacetique.Forme.ExamenFrm.patiente;
                        idPatient = GestionPharmacetique.Forme.ExamenFrm.idPatiente;
                    }
                    else                     
                    {
                        numeroActe = 0;
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
                        numeroActe = ListeExamFrm.id;
                        txtPatient.Text = ListeExamFrm.patient;
                        idPatient = ListeExamFrm.idPatient;
                    }
                    else
                    {
                        numeroActe = 0;
                    }
                }

                var dtAnalyse = ConnectionClassClinique.TableDesAnalysesEffectues(numeroActe);
                
                lblAnalyse.Text = dtAnalyse.Rows[0].ItemArray[5].ToString();
                txtAnalyse.Text = "Frais d'analyse".ToUpper();
                var detailAnalyse = ConnectionClassClinique.DetailsDesAnalyseEffectues(numeroActe);
                var montant = 0.0;
                typeActe = "EXAMEN";
                foreach (Analyse analyse in detailAnalyse)
                {
                    var typeAnalyse = ConnectionClassClinique.ListeDesAnalyses(analyse.NumeroListeAnalyse);

                    var type = typeAnalyse[0].TypeAnalyse;
                    var idLibelle = typeAnalyse[0].NumeroGroupe;
                    var lst = ConnectionClassClinique.ListeDesLibelles(idLibelle);
                    var libelle = "";
                    if (lst.Count >0)
                    {
                         libelle = lst[0].Designation  + " "+ lst[0].Sub;
                    }
                    dataGridView1.Rows.Add(type.ToUpper(), analyse.Frais, analyse.NombreAnalyse,
                        analyse.Frais * analyse.NombreAnalyse,libelle,analyse.NumeroAnalyse);
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
                numFacture = 0;
                //btnImprimer.Enabled = false;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception);
            }
        }
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

                        numeroActe = ConsultationFrm.idConsultation;
                        txtConsultation.Text = "FRAIS CONSULTATION";
                        var dtConsultation = ConnectionClassClinique.TableDesAnalysesEffectues(ConsultationFrm.idConsultation);
                        lblConsultation.Text = ConsultationFrm.montant.ToString();
                        txtPatient.Text = ConsultationFrm.patient;
                        idPatient = ConsultationFrm.idPatiente;
                        var groupe = "Consultation";
                        if (ConsultationFrm.typeConsultation.ToUpper() == "Carnet".ToUpper())
                        {
                            groupe = "Carnet medical";
                        }
                        dataGridView1.Rows.Add(ConsultationFrm.typeConsultation.ToUpper(), lblConsultation.Text, "1", lblConsultation.Text,groupe,ConsultationFrm.idConsultation);
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
                        numeroActe = ListeConsultation.id;
                        txtConsultation.Text = "FRAIS CONSULTATION";
                        var dtConsultation = ConnectionClassClinique.TableDesAnalysesEffectues(ListeConsultation.id);
                        lblConsultation.Text = ListeConsultation.montant.ToString();
                        txtPatient.Text = ListeConsultation.patient;
                        idPatient = ListeConsultation.idPatient;
                        var groupe = "Consultation";
                        if(ListeConsultation.typeConsultation.ToUpper()=="Carnet".ToUpper())
                        {
                            groupe = "Carnet medical";
                        }
                        dataGridView1.Rows.Add(ListeConsultation.typeConsultation, lblConsultation.Text, "1", lblConsultation.Text,groupe,ListeConsultation.id);
                        var montant = 0.0;
                        for (var i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                        {
                            montant += Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        }
                        txtTotal.Text = montant.ToString();
                        txtReste.Text = montant.ToString();
                        typeActe = "CONSULTATION";
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
                numFacture = 0;
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
                    btnDetail.Enabled = true;
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
                           
                            DateTime dateFacture = DateTime.Now;
                            double montantTotal;
                            if (Double.TryParse(label1.Text, out montantTotal))
                            {
                            }
                            else
                            {
                                montantTotal = Convert.ToDouble(txtTotal.Text);
                            }

                        double deduction = 0;
                        if (double.TryParse(txtReduction.Text, out deduction))
                        {
                        }
                        var partPatient = 100 - deduction;
                       
                        var numEmpl = GestionAcademique.LoginFrm.matricule;
                            var facture = new Facture();
                        facture.PartPatient = partPatient;
                        facture.NumeroFacture = numFacture;
                            facture.DateFacture = dateFacture;
                            facture.MontantFactural = montantTotal;
                            facture.IdPatient = idPatient;
                            facture.NumeroEmploye = numEmpl;
                           
                           
                            if (etat == "1")
                            {
                                if (numEmpl == "104")
                                    return;
                                var reste = double.Parse(txtReste.Text);
                                if (reste != 0)
                                {
                                    txtPaye.Focus();
                                    txtPaye.BackColor = Color.Red;
                                    return;
                                }
                                if (montantPaye == 0)
                                {
                                    txtPaye.BackColor = Color.Red;
                                    return;
                                } facture.Reste = reste;
                                if (ConnectionClassClinique.EnregistrerUneFacture(facture, dataGridView1,numeroActe,typeActe, montantPaye))
                                {
                                    if (checkBox2.Checked)
                                    {
                                        ConnectionClassClinique.EnregistrerUneFactureConventionnee(facture, dataGridView1,numeroActe,typeActe, montantPaye);
                                    }
                                    checkBox2.Checked = false;
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
                                        btnDetail.Focus();
                                        txtPaye.BackColor = Color.White;
                                        
                                }
                            }
                            else 
                                if (etat == "2")
                            {
                                //if (MonMessageBox.ShowBox("Voulez vous modifier les données de cette facture ?", "Confirmation", "confirmation.png") == "1")
                                //{
                                    //if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                                    //{
                                    //    if (ConnectionClassClinique.ModifierUneFacture(facture, dataGridView1, txtPaye.Text))
                                    //    {

                                    //        button3_Click(null, null);
                                    //        var listeFacture = new List<Facture>();

                                    //        if (checkBox1.Checked)
                                    //        {
                                    //            listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                                    //               DateTime.Now.Date, DateTime.Now.AddHours(24));
                                    //        }
                                    //        else
                                    //        {
                                    //            listeFacture = ConnectionClassClinique.ListeDesFactures("", DateTime.Now.Date, DateTime.Now.AddHours(24));
                                    //        }
                                    //    }

                                    //    else
                                    //    {
                                    //        //checkBox1.Enabled = false;
                                    //        //listeFacture = ConnectionClassClinique.ListeDesFactures(GestionAcademique.LoginFrm.matricule,
                                    //        //        DateTime.Now.Date, DateTime.Now.AddHours(24));
                                    //    }
                                    //    //ListeFacturation(listeFacture);
                                    //}



                                //}
                                //if (MonMessageBox.ShowBox("Voulez vous modifier les données de cette facture ?", "Confirmation", "confirmation.png") == "1")
                                {

               //                     var listeFacture = ConnectionClassClinique.ListeDesFactures(numEmploye,
               //                                dateFacture.Date, dateFacture.Date.AddHours(24));
               //                     var requete = "SELECT det_paie_tbl.montant,facture_tbl.sub FROM facture_tbl INNER JOIN det_paie_tbl ON facture_tbl.id_fact = det_paie_tbl.id_paie " +
               //" WHERE (facture_tbl.num_empl = '" + numEmploye + "' ) AND  (det_paie_tbl.date_paie >= @date1 AND det_paie_tbl.date_paie < @date2)";
               //                     var dt = ConnectionClassClinique.TableFacture(requete, dateFacture.Date, dateFacture.Date.AddHours(24));
               //                     var mt = .0;
               //                     foreach (DataRow dtRow in dt.Rows)
               //                     {
               //                         if (!string.IsNullOrEmpty(dtRow.ItemArray[1].ToString()))
               //                         {
               //                             mt += double.Parse(dtRow.ItemArray[0].ToString());
               //                         }
               //                     }
               //                     mt = mt + Double.Parse(textBox3.Text);
               //                     var montant = .0;
                                    if (txtPaye.Text != "0")
                                    {
                                        return;
                                    }
                                    if (ConnectionClassClinique.ModifierUneFacture(facture, dataGridView1, txtPaye.Text))
                                    {
                                        txtPaye.Text = "";
                                        txtPatient.Text = "";
                                        txtReduction.Text = "";
                                        lblRemise.Text = "";
                                        txtReste.Text = "";
                                        txtTotal.Text = "";
                                        textBox3.Text = "";
                                        dataGridView1.Rows.Clear();
                                        //button3_Click(null, null);
                                        //var listeFacture = new List<Facture>();
                                    }
                                }
                            }
                        }
                        else
                        {
                            txtPaye.BackColor = Color.Red;
                            txtPaye.Focus();
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
                                ConnectionClassClinique.Tracker(GestionAcademique.LoginFrm.nomUtilisateur, GestionAcademique.LoginFrm.nom, true);
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
        string nomEmploye, numEmploye;
        //detail facture
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
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
                    lblHospitalisation.Text = "";
                    btnImprimer.Enabled = false;
                    textBox3.Text = "";
                    lblConsultation.Text = "";
                    lblRemise.Text = "";
                    btnEnregistrer.Enabled = false;
                    lblObservation.Text = "";
                    numFacture = Int32.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    idPatient = Int32.Parse(dataGridView2.SelectedRows[0].Cells[3].Value.ToString());
                    numEmploye = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
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
            btnImprimer.Enabled = false;
            textBox3.Text = "";
            lblConsultation.Text = "";
            lblRemise.Text = "";
            btnEnregistrer.Enabled = false;
            lblObservation.Text = "";
            lblMontantObserv.Text = ""; var listeFacture = new List<Facture>();
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

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        Bitmap factureBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            //var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            //e.Graphics.DrawImage(factureBitmap, 0, 10, width, height);
            e.Graphics.DrawImage(factureBitmap, 0, 10, factureBitmap.Width, factureBitmap.Height);
            e.HasMorePages = false;

            //var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            //var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            ////e.Graphics.DrawImage(factureBitmap, 0, 10, width, height);
            //e.Graphics.DrawImage(factureBitmap, 0, 10, factureBitmap.Width,factureBitmap.Height );
            //e.HasMorePages = false;
        }
        string modePaiement;
        private void btnApercc_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
               var  listeFacture = new List<Facture>();
                //if (GestionAcademique.LoginFrm.typeUtilisateur == "admin")
                {
                    button7.Enabled = true;
                    if (checkBox1.Checked)
                    {
                        listeFacture = ConnectionClassClinique.ListeDesFacturesImpayees(GestionAcademique.LoginFrm.matricule,
                             DateTime.Parse("01/01/2020"), DateTime.Now.AddHours(24));
                    }
                    else
                    {
                        listeFacture = ConnectionClassClinique.ListeDesFacturesImpayees("", DateTime.Parse("01/01/2020"), DateTime.Now.AddHours(24));
                    }
                }
                //else
                //{
                //    checkBox1.Enabled = false;
                //    listeFacture = ConnectionClassClinique.ListeDesFacturesImpayees(GestionAcademique.LoginFrm.matricule,
                //            DateTime.Parse("01/01/2020"), DateTime.Now.AddDays(1));
                //}
                foreach (Facture facture in listeFacture)
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
                    if (facture.Reste > 0)
                    {
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
                    }
                }
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (double.Parse(row.Cells[7].Value.ToString()) != 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste facture", ex);
            }
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
                //var reste =  Double.Parse(txtReste.Text);
                var remise = 0.0;
                if (double.TryParse(lblRemise.Text, out remise))
                {
                }
               
                factureBitmap = Impression.FactureOfficielle(numFacture, dataGridView1, patient, modePaiement, remise,  nomEmploye);

                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                    //if (dataGridView1.Rows.Count > 8)
                    //{
                    //    printPreviewDialog1.ShowDialog();
                    //}
                }
                //#region MyRegion

                //SaveFileDialog sfd = new SaveFileDialog();
                //sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                //sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                //var jour = DateTime.Now.Day;
                //var mois = DateTime.Now.Month;
                //var year = DateTime.Now.Year;
                //var hour = DateTime.Now.Hour;
                //var min = DateTime.Now.Minute;
                //var sec = DateTime.Now.Second;
                //var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                //var pathFolder = "C:\\Dossier Clinique";
                //if (!System.IO.Directory.Exists(pathFolder))
                //{
                //    System.IO.Directory.CreateDirectory(pathFolder);
                //}
                //pathFolder = pathFolder + "\\Factures Client";
                //if (!System.IO.Directory.Exists(pathFolder))
                //{
                //    System.IO.Directory.CreateDirectory(pathFolder);
                //}
                //sfd.InitialDirectory = pathFolder;
                //sfd.FileName = "Rapport de " + patient.Nom + " " + patient.Prenom + " _imprimé_le_" + date + ".pdf";

                //if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    factureBitmap = Impression.FactureOfficielle(numFacture, dataGridView1, patient, modePaiement, remise, nomEmploye);
                //    var inputImage = @"cdali";
                //    // Create an empty page
                //    sharpPDF.pdfPage pageIndex = document.addPage(390, 590);
                //    var rowCount = dataGridView1.Rows.Count;
                //    document.addImageReference(factureBitmap, inputImage);

                //    sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                //    pageIndex.addImage(img1, -5, -10, pageIndex.height, pageIndex.width);
                //    if (rowCount > 8)
                //    {
                //        var Count = (dataGridView1.Rows.Count - 8) / 20;

                //        for (var i = 0; i <= Count; i++)
                //        {
                //            if (i * 20 < dataGridView1.Rows.Count)
                //            {

                //                //factureBitmap = Impression.FactureOfficielle(dataGridView1, remise, nomEmploye, i);
                //                //inputImage = @"cdali" + i;
                //                //// Create an empty page
                //                //pageIndex = document.addPage(330, 500);

                //                //document.addImageReference(factureBitmap, inputImage);
                //                //img1 = document.getImageReference(inputImage);
                //                //pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                //            }
                //        }

                //    }
                //    document.createPDF(sfd.FileName);
                //    System.Diagnostics.Process.Start(sfd.FileName);
                //}
                //#endregion
                txtHosp.Text = "";
                txtConsultation.Text = "";
                txtAnalyse.Text = "";
                label1.Text = "";
                txtTotal.Text = "";
                txtPatient.Text = "";
                txtPaye.Text = "";
                txtReste.Text = "";
                lblAnalyse.Text = "";
                txtReduction.Text = "";
                lblHospitalisation.Text = "";
                btnImprimer.Enabled = false;
                textBox3.Text = "";
                lblConsultation.Text = "";
                lblRemise.Text = "";
                btnEnregistrer.Enabled = false;
                lblObservation.Text = "";
                lblMontantObserv.Text = ""; 
                btnImprimer.Enabled = false;
                dataGridView1.Rows.Clear();
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Imprimer facture ", ex); }
            //try
            //{
            //    string nom = txtPatient.Text.Substring(0, txtPatient.Text.LastIndexOf(" "));
            //    string prenom = txtPatient.Text.Substring(txtPatient.Text.LastIndexOf(" ") + 1);
            //    var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
            //                       where p.NumeroPatient == idPatient
            //                       select p;

            //    var patient = new Patient();
            //    foreach (var p in listePatient)
            //        patient = p;
                
            //    var paye = Double.Parse(textBox3.Text);
            //    //var reste = Double.Parse(txtReste.Text);
            //    var remise = 0.0;
            //    if (double.TryParse(lblRemise.Text,out remise ))
            //    {
            //    }
            //    factureBitmap = Impression.FactureOfficielle(numFacture, dataGridView1, patient, modePaiement, remise , Convert.ToDouble(txtReste.Text), nomEmploye);
            //    if (printDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
            //        printDocument1.Print();
            //        if (dataGridView1.Rows.Count > 8)
            //        {
            //            var Count = (dataGridView1.Rows.Count - 8) / 20;

            //            for (var i = 0; i <= Count; i++)
            //            {
            //                if (i * 20 < dataGridView1.Rows.Count)
            //                {
            //                    factureBitmap = Impression.FactureOfficielle( dataGridView1,  remise, nomEmploye,i);
            //                    printPreviewDialog1.ShowDialog();
            //                    //printDocument1.Print();
            //                }
            //            }
            //        }
            //        button3_Click(null, null);
            //        btnAjouterAnalyse.Focus();
            //    }
            //}
            //catch
            //{ }
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
                btnImprimer.Enabled = false;
                if (GestionDuneClinique.FormesClinique.ListePatientFrm.fraisCarnet > 0)
                {
                    btnImprimer.Enabled = false;
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
                        ConnectionClassClinique.Tracker(GestionAcademique.LoginFrm.nomUtilisateur, GestionAcademique.LoginFrm.nom, true);
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
                        //var liste = new GestionDuneClinique.FormesClinique.ListeFrm();
                        //liste.state = "1";
                        //liste.ShowDialog();
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
                        dataGridView1.Rows.Add("CARNET", frais, "1", frais, "Carnet medical",0);
                    }
                }
                else
                {
                    dataGridView1.Rows.Add("CARNET", frais, "1", frais, "Carnet medical",0);
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
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    bool found = false;
            //    decimal montantTotal = 0;
            //    var frais = 200;
            //    if (GestionAcademique.LoginFrm.typeUtilisateur == "admin assistant")
            //    { return; }

            //    if (LunetteFrm.ShowBox())
            //    {

            //        dataGridView1.Rows.Add(LunetteFrm.lunettes.Designation, LunetteFrm.lunettes.Frais, "1", LunetteFrm.lunettes.Frais, "Lunettes");
            //        lblMontantObserv.Text = LunetteFrm.lunettes.Frais.ToString();
            //        lblObservation.Text = "Frais lunettes";
            //    }
            //    for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            //    {
            //        montantTotal += decimal.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
            //    }
            //    txtTotal.Text = montantTotal.ToString();
            //    txtPaye.Text = montantTotal.ToString();
            //    button3.Enabled = true;
            //    btnEnregistrer.Enabled = true;
            //    etat = "1";
            //    txtPaye.Focus();

            //}
            //catch { }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                bool found = false;
                decimal montantTotal = 0;
                var frais = 100;
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
                        dataGridView1.Rows.Add("PETIT CARNET", frais, "1", frais, "Carnet medical", 0);
                    }
                }
                else
                {
                    dataGridView1.Rows.Add("CARNET", frais, "1", frais, "Carnet medical", 0);
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
    }
}
