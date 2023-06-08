using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;
using System.Linq;

namespace GestionDuneClinique.Formes
{
    public partial class RapportEntrepriseFrm : Form
    {
        public RapportEntrepriseFrm()
        {
            InitializeComponent();
        }

        private void RapportEntreprise_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Blue, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 128, 0), 3);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

       
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);

        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void RapportEntreprise_Load(object sender, EventArgs e)
        {
            
            //Column2.Width = 3 * dgvPatient.Width / 6;
            //Column4.Width =  dgvPatient.Width / 7;
            //cl1.Width = dataGridView1.Width / 6;
            //cl2.Width = dataGridView1.Width / 3;
            //cl3.Width = dataGridView1.Width / 3;
            //cl4.Width = dataGridView1.Width / 8;
            //cl5.Width = dataGridView1.Width / 8;
            button3.Location = new Point(Width - 50, 3);
            cll1.Width = dataGridView1.Width / 12;
            cll2.Width = dataGridView1.Width / 4;
            cll3.Width = dataGridView1.Width / 5;
            cll4.Width = dataGridView1.Width / 20;
            cll5.Width = dataGridView1.Width / 15;
            cll6.Width = dataGridView1.Width / 15;
            cll7.Width = dataGridView1.Width / 13;
            cll8.Width = dataGridView1.Width / 10;

            var listeEntrep = ConnectionClassClinique.ListeDesEntreprises();
            cmbEntreprise.Items.Clear();
            foreach (Entreprise entreprise in listeEntrep)
            {
                cmbEntreprise.Items.Add(entreprise.NomEntreprise);
            }
        }

        private void cmbEntreprise_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise("", cmbEntreprise.Text);

                ListeDespatients(listePatient);
                var listeEntrep = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);
                indexEntreprise = listeEntrep[0].IndexPrix;
            }
            catch { }
        }

        void ListeDespatients(List<Patient> listePatient)
        {
            dgvPatient.Rows.Clear();

            foreach (Patient patient in listePatient)
            {
               // var listeEmp = ConnectionClassClinique.ListeDesEmployeesEntreprise(patient.Nom + " " + patient.Prenom);
               //var td ="";
               //if (listeEmp.Count > 0)
               //{
               //    if (!string.IsNullOrWhiteSpace(listeEmp[0].Telephone))
               //    {
               //        td = " / " + listeEmp[0].Telephone;
               //    }
               //}

                dgvPatient.Rows.Add(
                    patient.NumeroPatient,
                    patient.Nom+" "+ patient.Prenom ,
                    patient.Sexe, 
                    patient.An,
                    patient.SousCouvert
                    );

            }
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 128, 0), 3);
            Rectangle area1 = new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(255, 128, 0),
                Color.FromArgb(255, 128, 0), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        void Rapport(DataTable dtAnalyse, DataTable dtConsult, DataTable dtHosp, DataTable dtPharm)
        {
            //try
            //{
            //    var items = new List<string>();
            //    var patientConsultation="";
            //    var patientAnalyse = "";
            //    var consulta = "";
            //    if (dtConsult.Rows.Count > 1)
            //    {
                    
            //        if (dtConsult.Rows[0].ItemArray[6].ToString() != "")
            //        {
                        

            //            patientConsultation = dtConsult.Rows[0].ItemArray[0].ToString() + " " +
            //                dtConsult.Rows[0].ItemArray[1].ToString() + " s/c " +
            //                dtConsult.Rows[0].ItemArray[6].ToString();
            //        }
            //        else
            //        {
            //            patientConsultation = dtConsult.Rows[0].ItemArray[0].ToString() + " " + 
            //                dtConsult.Rows[0].ItemArray[1].ToString() ;
            //        }

            //        if (dtConsult.Rows[0].ItemArray[2].ToString() == "Consultation De  Specialité")
            //        {
            //            consulta = "Consultation en " + dtConsult.Rows[0].ItemArray[7].ToString();
            //        }
            //        else
            //        {
            //            consulta = dtConsult.Rows[0].ItemArray[2].ToString();
            //        }
            //        dataGridView1.Rows.Add(
            //           dtConsult.Rows[0].ItemArray[8].ToString(),
            //           patientConsultation.ToUpper(),
            //           DateTime.Parse(dtConsult.Rows[0].ItemArray[3].ToString()).ToShortDateString(),
            //            consulta.ToUpper(),
            //           dtConsult.Rows[0].ItemArray[4].ToString(), 
            //           dtConsult.Rows[0].ItemArray[4].ToString(),
            //               0
            //           );

            //        for (int i = 1; i < dtConsult.Rows.Count; i++)
            //        {

            //            if (dtConsult.Rows[i].ItemArray[2].ToString() == "Consultation De  Specialité")
            //            {
            //                consulta = "Consultation en " + dtConsult.Rows[i].ItemArray[7].ToString();
            //            }
            //            else
            //            {
            //                consulta = dtConsult.Rows[i].ItemArray[2].ToString();
            //            }
            //            dataGridView1.Rows.Add(
            //               dtConsult.Rows[i].ItemArray[8].ToString(),
            //                " ",DateTime.Parse(dtConsult.Rows[i].ItemArray[3].ToString()).ToShortDateString(),
            //                consulta.ToUpper(),
            //               dtConsult.Rows[i].ItemArray[4].ToString(),
            //               dtConsult.Rows[i].ItemArray[4].ToString(),
            //               0

            //               );
            //        }
            //    }
            //    else if (dtConsult.Rows.Count == 1)
            //    {
            //        if (dtConsult.Rows[0].ItemArray[6].ToString() != "")
            //        {
            //            patientConsultation = dtConsult.Rows[0].ItemArray[0].ToString() + " " + dtConsult.Rows[0].ItemArray[1].ToString() + " s/c " +
            //                dtConsult.Rows[0].ItemArray[6].ToString();
            //        }
            //        else
            //        {
            //            patientConsultation = dtConsult.Rows[0].ItemArray[0].ToString() + " " + dtConsult.Rows[0].ItemArray[1].ToString();
            //        }
            //        if (dtConsult.Rows[0].ItemArray[2].ToString() == "Consultation De  Specialité")
            //        {
            //            consulta = "Consultation en " + dtConsult.Rows[0].ItemArray[7].ToString();
            //        }
            //        else
            //        {
            //            consulta = dtConsult.Rows[0].ItemArray[2].ToString();
            //        }
            //        dataGridView1.Rows.Add(
            //           dtConsult.Rows[0].ItemArray[8].ToString(),
            //           patientConsultation.ToUpper(),
            //           DateTime.Parse(dtConsult.Rows[0].ItemArray[3].ToString()).ToShortDateString(),
            //            consulta.ToUpper(),
            //           dtConsult.Rows[0].ItemArray[4].ToString(),
            //           dtConsult.Rows[0].ItemArray[4].ToString(),
            //               0

            //           );   
            //    }

            //    if(dtAnalyse.Rows.Count >1)
            //    {


            //           var  listePatient = from p in ConnectionClassClinique.ListeDesPatients()
            //                       where p.NumeroPatient == Int32.Parse(dtAnalyse.Rows[0].ItemArray[2].ToString())
            //                                   select p;
            //           var sousCouvert = "";
            //           foreach (var p in listePatient)
            //           {
            //               sousCouvert = p.SousCouvert;
            //               patientAnalyse = p.Nom + " " + p.Prenom;
            //           }

            //        if (sousCouvert != "")
            //        {
            //            patientAnalyse = patientAnalyse + " s/c " + sousCouvert;
            //        }
            //        if (patientConsultation == patientAnalyse)
            //        {
            //            for( j = 0; j < dtAnalyse.Rows.Count; j++)
            //            {
            //                #region MyRegion1
            //                var analyse = dtAnalyse.Rows[j].ItemArray[6].ToString().ToUpper();
            //                var montant = Double.Parse(dtAnalyse.Rows[j].ItemArray[7].ToString().ToUpper());
            //                int nbre ; 
            //                if( Int32.TryParse(dtAnalyse.Rows[j].ItemArray[8].ToString().ToUpper(), out nbre))
            //                {

            //                }
            //                else
            //                {
            //                    nbre = 1;
            //                }
            //                var montantTotal =montant * nbre;
            //                #endregion
                        
            //                dataGridView1.Rows.Add(
            //                dtAnalyse.Rows[j].ItemArray[0].ToString(),
            //                " ", DateTime.Parse(dtAnalyse.Rows[j].ItemArray[1].ToString()).ToShortDateString(), 
            //                analyse,
            //                montant, 
            //                montantTotal,
            //                dtAnalyse.Rows[j].ItemArray[5].ToString()
            //                );
            //            }
            //        }
            //        else
            //        {
            //           #region MyRegion2
            //                var analyse = dtAnalyse.Rows[0].ItemArray[6].ToString().ToUpper();
            //                var montant = Double.Parse(dtAnalyse.Rows[0].ItemArray[7].ToString().ToUpper());
            //                int nbre ; 
            //                if( Int32.TryParse(dtAnalyse.Rows[0].ItemArray[8].ToString().ToUpper(), out nbre))
            //                {

            //                }
            //                else
            //                {
            //                    nbre = 1;
            //                }
            //                var montantTotal =montant * nbre;
            //                #endregion
            //            dataGridView1.Rows.Add(
            //               dtAnalyse.Rows[0].ItemArray[0].ToString(),
            //               patientAnalyse.ToUpper(), 
            //               DateTime.Parse(dtAnalyse.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
            //               analyse,
            //               montant, 
            //               montantTotal,
            //                 dtAnalyse.Rows[0].ItemArray[5].ToString()
            //           );
            //            for (j = 1; j < dtAnalyse.Rows.Count; j++)
            //            {
            //                 listePatient = ConnectionClassClinique.ListeDesPatients(Int32.Parse(dtAnalyse.Rows[j].ItemArray[2].ToString()));

            //                 #region MyRegion1
            //                  analyse = dtAnalyse.Rows[j].ItemArray[6].ToString().ToUpper();
            //                  montant = Double.Parse(dtAnalyse.Rows[j].ItemArray[7].ToString().ToUpper());
                           
            //                 if (Int32.TryParse(dtAnalyse.Rows[j].ItemArray[8].ToString().ToUpper(), out nbre))
            //                 {

            //                 }
            //                 else
            //                 {
            //                     nbre = 1;
            //                 }
            //                  montantTotal = montant * nbre;
            //                 #endregion
            //                dataGridView1.Rows.Add(
            //                    dtAnalyse.Rows[j].ItemArray[0].ToString(),
            //                    "" ,
            //                   DateTime.Parse(dtAnalyse.Rows[j].ItemArray[1].ToString()).ToShortDateString(),
            //                    analyse,
            //                    montant, 
            //                    montantTotal,
            //                      dtAnalyse.Rows[j].ItemArray[5].ToString()
            //                    );
            //            }

            //        }
                    
            //    }
            //    else if (dtAnalyse.Rows.Count ==1)
            //    {
            //        #region MyRegion4
            //        var analyse = dtAnalyse.Rows[0].ItemArray[6].ToString().ToUpper();
            //        var montant = Double.Parse(dtAnalyse.Rows[0].ItemArray[7].ToString().ToUpper());
            //        int nbre;
            //        if (Int32.TryParse(dtAnalyse.Rows[0].ItemArray[8].ToString().ToUpper(), out nbre))
            //        {

            //        }
            //        else
            //        {
            //            nbre = 1;
            //        }
            //        var montantTotal = montant * nbre;
            //        #endregion
            //        var listePatient = ConnectionClassClinique.ListeDesPatients(Int32.Parse(dtAnalyse.Rows[0].ItemArray[2].ToString()));
                   
            //        //var nomPatient = "";

            //        if (listePatient[0].SousCouvert != "")
            //        {
            //            patientAnalyse = listePatient[0].Nom + " " + listePatient[0].Prenom + " s/c " + listePatient[0].SousCouvert;
            //        }
            //        else
            //        {
            //            patientAnalyse = listePatient[0].Nom + " " + listePatient[0].Prenom;
            //        }
            //        if (patientConsultation == patientAnalyse)
            //        {
            //            dataGridView1.Rows.Add(
            //                dtAnalyse.Rows[0].ItemArray[0].ToString(),
            //                "",
            //               DateTime.Parse(dtAnalyse.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
            //                analyse,
            //                montant, 
            //               montantTotal,
            //                 dtAnalyse.Rows[0].ItemArray[5].ToString()
            //                );
            //        }
            //        else
            //        {
            //            dataGridView1.Rows.Add(
            //                                       dtAnalyse.Rows[0].ItemArray[0].ToString(),
            //                                       patientAnalyse,
            //                                       DateTime.Parse(dtAnalyse.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
            //                                       analyse,
            //                                       montant, 
            //                                       montantTotal,
            //                                         dtAnalyse.Rows[0].ItemArray[5].ToString()
            //                                       );
            //        }
            //    }
            //    else
            //    {
                    
            //    }

            //    if (dtPharm.Rows.Count == 1)
            //    {
            //        var nomPatient = "";
            //        if (dtConsult.Rows.Count > 0)
            //        {
            //        }
            //        else if (dtAnalyse.Rows.Count > 0)
            //        {
            //            nomPatient = "";
            //        }
            //        else
            //        {
            //            nomPatient = dtPharm.Rows[0].ItemArray[1].ToString();
                        
            //        }

            //        dataGridView1.Rows.Add(
            //            dtPharm.Rows[0].ItemArray[3].ToString(),
            //            nomPatient.ToUpper(), 
            //            DateTime.Parse(dtPharm.Rows[0].ItemArray[2].ToString()).ToShortDateString(),
            //            dtPharm.Rows[0].ItemArray[4].ToString(),
            //            dtPharm.Rows[0].ItemArray[5].ToString(),
            //            dtPharm.Rows[0].ItemArray[7].ToString()

            //            );
            //    }
            //    else if (dtPharm.Rows.Count > 1)
            //    {
            //        var nomPatient = "";
            //        if (dtConsult.Rows.Count > 0)
            //        {
            //        }
            //        else if (dtAnalyse.Rows.Count > 0)
            //        {
            //            nomPatient = "";
            //        }
            //        else
            //        {
            //            nomPatient = dtPharm.Rows[0].ItemArray[1].ToString();
            //        }
            //        dataGridView1.Rows.Add(
            //              dtPharm.Rows[0].ItemArray[3].ToString(),
            //              nomPatient.ToUpper(),
            //              DateTime.Parse(dtPharm.Rows[0].ItemArray[2].ToString()).ToShortDateString(),
            //              dtPharm.Rows[0].ItemArray[4].ToString(),
            //              dtPharm.Rows[0].ItemArray[5].ToString(),
            //              dtPharm.Rows[0].ItemArray[7].ToString()

            //              );

            //        for (var m = 1; m < dtPharm.Rows.Count; m++)
            //        {
                     

            //            dataGridView1.Rows.Add(
            //                dtPharm.Rows[m].ItemArray[3].ToString(),
            //                "",
            //                DateTime.Parse(dtPharm.Rows[m].ItemArray[2].ToString()).ToShortDateString(),
            //                dtPharm.Rows[m].ItemArray[4].ToString(),
            //                dtPharm.Rows[m].ItemArray[5].ToString(),
            //                dtPharm.Rows[m].ItemArray[7].ToString()

            //                );
            //        }
            //    }
                
            //    for (int k = 0; k < dtHosp.Rows.Count; k++ )
            //    {
            //        var nomPatient = "";
            //        var patientHospitalisation = "";
            //        if (dtHosp.Rows[k].ItemArray[5].ToString() != "")
            //        {
            //            patientHospitalisation = dtHosp.Rows[k].ItemArray[1].ToString() + " " + dtHosp.Rows[k].ItemArray[2].ToString() +
            //                 " s/c " + dtHosp.Rows[k].ItemArray[5].ToString();
            //        }
            //        else
            //        {
            //            patientHospitalisation = dtHosp.Rows[k].ItemArray[1].ToString() + " " + dtHosp.Rows[k].ItemArray[2].ToString();
            //        }
            //        if (patientHospitalisation == patientConsultation || patientHospitalisation == patientAnalyse)
            //        {
            //            nomPatient = "";
            //        }
            //        else
            //        {
            //            nomPatient = patientHospitalisation;

            //        }

            //        DateTime debut = DateTime.Parse(dtHosp.Rows[k].ItemArray[3].ToString());
            //        DateTime sortie = DateTime.Parse(dtHosp.Rows[k].ItemArray[6].ToString());
            //        var  nbr = sortie.DayOfYear - debut.DayOfYear;

            //        var montanTotal = Double.Parse(dtHosp.Rows[k].ItemArray[4].ToString());
 
            //        var prix = montanTotal / nbr;
            //        string hospita;
            //        if (nbr <= 1)
            //        {
            //            hospita = "Hospitalisation";
            //        }else 
            //        {
            //           hospita= nbr + "jrs  d'hospitalisation";
            //        }
            //        dataGridView1.Rows.Add(
            //            dtHosp.Rows[k].ItemArray[0].ToString(),
            //            nomPatient, 
            //            DateTime.Parse(dtHosp.Rows[k].ItemArray[3].ToString()).ToShortDateString(),                        
            //            hospita.ToUpper(), prix,
            //            dtHosp.Rows[k].ItemArray[4].ToString()
            //            );
            //    }


            //    var count = dtAnalyse.Rows.Count + dtConsult.Rows.Count + dtHosp.Rows.Count + dtPharm.Rows.Count ;
               
            //    if (count > 0)
            //    {
            //        dataGridView1.Rows.Add(
            //                 "",
            //                "",
            //                 "",
            //                "",
            //                 "",
            //                ""
            //                 );
            //    }

                
            //}
            //catch (Exception ex)
            //{
            //    MonMessageBox.ShowBox("Rapport entreprise", ex);
            //}
        }
        string etatImpression;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (indexEntreprise == 1)
                //{
                //    label12.Text = "";
                //    label13.Visible = true;
                //    dataGridView1.Visible = false;
                //    dataGridView2.Visible = true;
                //    dataGridView2.Rows.Clear();
                //    dataGridView1.Rows.Clear();

                //    foreach (DataGridViewRow dtGrid in dgvPatient.Rows)
                //    {
                //        var nomPatient = "";
                //        int idPatient;
                //        if (dtGrid.Cells[4].Value.ToString() != "")
                //        {
                //            nomPatient = dtGrid.Cells[1].Value.ToString() + " s/c " + dtGrid.Cells[4].Value.ToString();
                //        }
                //        else
                //        {
                //            nomPatient = dtGrid.Cells[1].Value.ToString();
                //        }

                //        if (Int32.TryParse(dtGrid.Cells[0].Value.ToString(), out idPatient))
                //        {
                //            var dtFacture = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                //            var total = 0.0; var totalAssure = .0;var  totalAssureur = .0;
                //            foreach (DataRow dtRow in dtFacture.Rows)
                //            {
                //                total += Double.Parse(dtRow.ItemArray[6].ToString());
                //                var prixTotal = double.Parse(dtRow.ItemArray[6].ToString());

                //                var pourcentageAssure = 100.0;
                //                var pourcentageAssureur = 100.0;
                //                if (dtRow.ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                //                {
                //                    pourcentageAssure = 80;
                //                    pourcentageAssureur = 20;
                //                }
                //                else if (dtRow.ItemArray[3].ToString().ToUpper().Contains("CONSULTATION") &&
                //                    !dtRow.ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                //                {
                //                    pourcentageAssure = 60;
                //                    pourcentageAssureur = 40;
                //                }
                //                else if (dtRow.ItemArray[3].ToString().ToUpper().Contains("OPERATOIRE") ||
                //                  dtRow.ItemArray[3].ToString().ToUpper().Contains("RX") ||
                //                   dtRow.ItemArray[3].ToString().ToUpper().Contains("ACCOUCHEMENT") ||
                //                  dtRow.ItemArray[3].ToString().ToUpper().Contains("ECHOGRAPHIE") ||
                //                  dtRow.ItemArray[3].ToString().ToUpper().Contains("ELECTROCQRDIOGRAMME"))
                //                {
                //                    pourcentageAssure = 80;
                //                    pourcentageAssureur = 20;
                //                }
                //                else
                //                {
                //                    pourcentageAssure = 60;
                //                    pourcentageAssureur = 40;
                //                }
                //                totalAssure += prixTotal * pourcentageAssure / 100;
                //               totalAssureur += prixTotal * pourcentageAssureur / 100;
                //            }
                //            RapportTotalGrouperParPatientAvecIndexDifferente(dtFacture, total,totalAssure,totalAssureur, nomPatient);
                //            var count = dataGridView3.Rows.Count;
                //        }

                //    }
                //    var montantTotalAssure = .0;
                //    var montantTotal= .0;
                //    var montantTotalAssureur = .0;
                //    foreach (DataGridViewRow dtRow in dataGridView2.Rows)
                //    {
                //        double total,totalA,totalAs;
                //        if (dtRow.Cells[2].Value.ToString().ToUpper() != "SOUS TOTAL")
                //        {
                //            if (Double.TryParse(dtRow.Cells[5].Value.ToString(), out total))
                //            {
                //                montantTotal += total;
                //            }
                //            if (Double.TryParse(dtRow.Cells[6].Value.ToString(), out totalA))
                //            {
                //                montantTotalAssure += totalA;
                //            }
                //            if (Double.TryParse(dtRow.Cells[7].Value.ToString(), out totalAs))
                //            {
                //                montantTotalAssureur += totalAs;
                //            }
                //        }
                //    }
                //    lblTotal.Text = montantTotal.ToString();
                //    dataGridView3.Rows.Add("TOTAL", montantTotal);
                //    dataGridView2.Rows.Add(
                //              "",
                //             "",
                //              "TOTAL",
                //              "",
                //              "",
                //            montantTotal,
                //            montantTotalAssure,montantTotalAssureur
                //              );

                //    foreach (DataGridViewRow row in dataGridView2.Rows)
                //    {
                //        if (row.Cells[2].Value.ToString() == "SOUS TOTAL")
                //        {
                //            row.DefaultCellStyle.BackColor = Color.Yellow;
                //        }
                //        else if (row.Cells[2].Value.ToString() == "TOTAL")
                //        {
                //            row.DefaultCellStyle.BackColor = Color.Orange;
                //        }
                //    }
                //    etatImpression = "3";
                //}
                //else
                {
                    label12.Text = "";
                    label13.Visible = false;
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    dataGridView2.Rows.Clear();
                    dataGridView1.Rows.Clear();

                    dataGridView1.Rows.Clear();
                    foreach (DataGridViewRow dtGrid in dgvPatient.Rows)
                    {
                        var nomPatient = "";
                        int idPatient;
                        if (dtGrid.Cells[4].Value.ToString() != "")
                        {
                            nomPatient = dtGrid.Cells[1].Value.ToString() + " s/c " + dtGrid.Cells[4].Value.ToString();
                        }
                        else
                        {
                            nomPatient = dtGrid.Cells[1].Value.ToString();
                        }

                        if (Int32.TryParse(dtGrid.Cells[0].Value.ToString(), out idPatient))
                        {
                            var dtFacture = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                            var dtCredit = ConnectionClassPharmacie.ListeDesCredit(idPatient, dtGrid.Cells[1].Value.ToString(), cmbEntreprise.Text, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                            var total = 0.0;
                            foreach (DataRow dtRow in dtFacture.Rows)
                            {
                                total += Double.Parse(dtRow.ItemArray[6].ToString());
                            }
                            foreach (DataRow dtRow in dtCredit.Rows)
                            {
                                total += Double.Parse(dtRow.ItemArray[7].ToString());
                            }
                            RapportTotalGrouperParPatient(dtFacture, dtCredit, total, nomPatient);
                            var count = dataGridView3.Rows.Count;
                        }

                    }
                    if (cmbEntreprise.Text == "NRC" || cmbEntreprise.Text == "SHT")
                    {
                        dataGridView1.Rows.Add(
                         "", "",
                        dtp2.Value.Date.ToShortDateString(),
                        "HONORAIRE DU MEDECIN TRAITANT",
                         "",
                         "",
                        "500000"
                         );
                    }
                    var montantTotal = 0.0;

                    foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                    {
                        double total;
                        if (dtRow.Cells[3].Value.ToString().ToUpper() != "SOUS TOTAL")
                        {
                            if (Double.TryParse(dtRow.Cells[6].Value.ToString(), out total))
                            {
                                montantTotal += total;
                            }
                        }
                    }
                    lblTotal.Text = montantTotal.ToString();
                    dataGridView3.Rows.Add("TOTAL", montantTotal);
                    dataGridView1.Rows.Add(
                              "",
                             "",
                             "",
                              "TOTAL",
                              "",
                              "",
                            montantTotal,
                            ""
                              );

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[3].Value.ToString() == "SOUS TOTAL")
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (row.Cells[3].Value.ToString() == "TOTAL")
                        {
                            row.DefaultCellStyle.BackColor = Color.OrangeRed;
                        }
                    }
                    etatImpression = "1";
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        Bitmap rapportBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportBitmap, -10, 20, rapportBitmap.Width, rapportBitmap.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        int j= 1;
        private void btnApercu_Click(object sender, EventArgs e)
        {
            try
            {
                if (etatImpression == "1")
                {
                    var reduction = 0.0;
                    if (double.TryParse(label12.Text, out reduction))
                    {
                    }

                    if (dataGridView1.Rows.Count > 0 && cmbEntreprise.Text != "")
                    {
                        var listeEntreprise = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);

                        var entreprise = new Entreprise(listeEntreprise[0].NumeroEntreprise, listeEntreprise[0].NomEntreprise, listeEntreprise[0].Telephone1,
                            listeEntreprise[0].Telephone2, listeEntreprise[0].Email, listeEntreprise[0].Adresse, listeEntreprise[0].DateAbonnement, listeEntreprise[0].PrixHonoraire,indexEntreprise);
                        var rowCount = dataGridView1.Rows.Count;
                      
                        #region MyRegion

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                        var jour = DateTime.Now.Day;
                        var mois = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                        var pathFolder = "C:\\Dossier Clinique";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        pathFolder = pathFolder + "\\Rapport des conventionnees";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        sfd.InitialDirectory = pathFolder;
                        sfd.FileName =  "Rapport de "+cmbEntreprise.Text +" _imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            rapportBitmap = Impression.RapportDuneEntreprise(dataGridView1, entreprise, dtp2.Value, Double.Parse(lblTotal.Text), reduction);

                            var inputImage = @"cdali" ;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(rapportBitmap, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                            if (rowCount > 30)
                            {
                                var Count = (dataGridView1.Rows.Count - 30) / 45;

                                for (var i = 0; i <= Count; i++)
                                {
                                    if (i * 45 < dataGridView1.Rows.Count)
                                    {
                                        rapportBitmap = Impression.RapportDuneEntrepriseParPage(dataGridView1, entreprise, dtp2.Value, Double.Parse(lblTotal.Text), reduction, i);

                                         inputImage = @"cdali" + i;
                                        // Create an empty page
                                         pageIndex = document.addPage();

                                        document.addImageReference(rapportBitmap, inputImage);
                                         img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                    }
                                }

                            }
                        }
                        #endregion
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    
                }
                else if (etatImpression == "2")
                {
                    button2_Click(null, null);
                }
                else if (etatImpression == "3")
                {
                    ImprimerRapportAvecCharge();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Impression", ex);
            }
            
        }

        private void printPreviewDialog1_Load_1(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

     
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbEntreprise.Text != "")
                {
                    if (dgvPatient.SelectedRows.Count > 0)
                    {
                        var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                           where p.NumeroPatient == (Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString()))
                                           select p;
                        var patient = new Patient();
                        foreach (var p in listePatient)
                            patient = p;
                        if (dataGridView1.Rows.Count > 0)
                        {
                        #region MyRegion

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                        var jour = DateTime.Now.Day;
                        var mois = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                        var pathFolder = "C:\\Dossier Clinique";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        pathFolder = pathFolder + "\\Rapport des conventionnees";
                        if (!System.IO.Directory.Exists(pathFolder))
                        {
                            System.IO.Directory.CreateDirectory(pathFolder);
                        }
                        sfd.InitialDirectory = pathFolder;
                        sfd.FileName =  "Rapport de "+cmbEntreprise.Text +" _imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                             rapportBitmap = Impression.FactureEmployeDuneEntreprise("FACTURE", patient, cmbEntreprise.Text, Convert.ToDecimal(lblTotal.Text), GestionAcademique.LoginFrm.nom, dataGridView1, 0);
                              var inputImage = @"cdali" ;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage();

                            document.addImageReference(rapportBitmap, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                            if (dataGridView1.Rows.Count > 24)
                            {
                                var Count = (dataGridView1.Rows.Count - 24) / 24;

                                for (var i = 0; i <= Count; i++)
                                {
                                    if (i * 24 < dataGridView1.Rows.Count-24)
                                    {
                                        rapportBitmap = Impression.FactureEmployeDuneEntreprise("FACTURE", patient, cmbEntreprise.Text, Convert.ToDecimal(lblTotal.Text), GestionAcademique.LoginFrm.nom, dataGridView1, i);
                                   
                                         inputImage = @"cdali" + i;
                                        // Create an empty page
                                         pageIndex = document.addPage();

                                        document.addImageReference(rapportBitmap, inputImage);
                                         img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                    }
                                }

                            }
                        }
                        #endregion
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                            //if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            //{
                            //    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            //    printPreviewDialog1.ShowDialog();
                            //    if (dataGridView1.Rows.Count > 24)
                            //    {
                            //        printDocument1.Print();
                            //    }
                            //}
                        //}
                    }

                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture employe ", ex);
            }
        }
        private void dgvPatient_DoubleClick(object sender, EventArgs e)
        {
            
                   try
                   {
                       if (cmbEntreprise.Text != "")
                       {
                           if (dgvPatient.SelectedRows.Count > 0)
                           {
                               dataGridView1.Rows.Clear();
                               foreach (DataGridViewRow dtGrid in dgvPatient.SelectedRows)
                               {

                                   int idPatient;
                                   if (Int32.TryParse(dtGrid.Cells[0].Value.ToString(), out idPatient))
                                   {
                                       var nomPatient = dtGrid.Cells[1].Value.ToString() + " " + dtGrid.Cells[2].Value.ToString();
                                       var dtAnalyse = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                       var dtConsultation = ConnectionClassClinique.ListeConsultationDesEntreprise(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                       var dtHospitalisation = ConnectionClassClinique.ListeHospitalisationDesEntreprise(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                                       var dtCredit = ConnectionClassPharmacie.ListeDesCredit(idPatient,nomPatient, cmbEntreprise.Text, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));

                                       Rapport(dtAnalyse, dtConsultation, dtHospitalisation, dtCredit);
                                   }

                                   var montantTotal = 0.0;
                                   foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                                   {
                                       double total;
                                       if (Double.TryParse(dtRow.Cells[5].Value.ToString(), out total))
                                       {
                                           montantTotal += total;
                                       }
                                   }
                                   lblTotal.Text = montantTotal.ToString();
                               }

                           }
                       }
                   
                   
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture employe ", ex);
            }
        }
        //facture honraire
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //if (indexEntreprise == 1)
                //{
                //    label12.Text = "";
                //    label13.Visible = true;
                //    dataGridView1.Visible = false;
                //    dataGridView2.Visible = true;
                //    dataGridView2.Rows.Clear();
                //    dataGridView1.Rows.Clear();

                //    foreach (DataGridViewRow dtGrid in dgvPatient.SelectedRows)
                //    {
                //        var nomPatient = "";
                //        int idPatient;
                //        if (dtGrid.Cells[4].Value.ToString() != "")
                //        {
                //            nomPatient = dtGrid.Cells[1].Value.ToString() + " s/c " + dtGrid.Cells[4].Value.ToString();
                //        }
                //        else
                //        {
                //            nomPatient = dtGrid.Cells[1].Value.ToString();
                //        }

                //        if (Int32.TryParse(dtGrid.Cells[0].Value.ToString(), out idPatient))
                //        {
                //            var dtFacture = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                //            var total = 0.0; var totalAssure = .0; var totalAssureur = .0;
                //            foreach (DataRow dtRow in dtFacture.Rows)
                //            {
                //                total += Double.Parse(dtRow.ItemArray[6].ToString());
                //                var prixTotal = double.Parse(dtRow.ItemArray[6].ToString());

                //                var pourcentageAssure = 100.0;
                //                var pourcentageAssureur = 100.0;
                //                if (dtRow.ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                //                {
                //                    pourcentageAssure = 80;
                //                    pourcentageAssureur = 20;
                //                }
                //                else if (dtRow.ItemArray[3].ToString().ToUpper().Contains("CONSULTATION") &&
                //                    !dtRow.ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                //                {
                //                    pourcentageAssure = 60;
                //                    pourcentageAssureur = 40;
                //                }
                //                else if (dtRow.ItemArray[3].ToString().ToUpper().Contains("OPERATOIRE") ||
                //                  dtRow.ItemArray[3].ToString().ToUpper().Contains("RX") ||
                //                   dtRow.ItemArray[3].ToString().ToUpper().Contains("ACCOUCHEMENT") ||
                //                  dtRow.ItemArray[3].ToString().ToUpper().Contains("ECHOGRAPHIE") ||
                //                  dtRow.ItemArray[3].ToString().ToUpper().Contains("ELECTROCQRDIOGRAMME"))
                //                {
                //                    pourcentageAssure = 80;
                //                    pourcentageAssureur = 20;
                //                }
                //                else
                //                {
                //                    pourcentageAssure = 60;
                //                    pourcentageAssureur = 40;
                //                }
                //                totalAssure += prixTotal * pourcentageAssure / 100;
                //                totalAssureur += prixTotal * pourcentageAssureur / 100;
                //            }
                //            RapportTotalGrouperParPatientAvecIndexDifferente(dtFacture, total, totalAssure, totalAssureur, nomPatient);
                //            var count = dataGridView3.Rows.Count;
                //        }

                //    }
                //    var montantTotalAssure = .0;
                //    var montantTotal = .0;
                //    var montantTotalAssureur = .0;
                //    foreach (DataGridViewRow dtRow in dataGridView2.Rows)
                //    {
                //        double total, totalA, totalAs;
                //        if (dtRow.Cells[2].Value.ToString().ToUpper() != "SOUS TOTAL")
                //        {
                //            if (Double.TryParse(dtRow.Cells[5].Value.ToString(), out total))
                //            {
                //                montantTotal += total;
                //            }
                //            if (Double.TryParse(dtRow.Cells[6].Value.ToString(), out totalA))
                //            {
                //                montantTotalAssure += totalA;
                //            }
                //            if (Double.TryParse(dtRow.Cells[7].Value.ToString(), out totalAs))
                //            {
                //                montantTotalAssureur += totalAs;
                //            }
                //        }
                //    }
                //    lblTotal.Text = montantTotal.ToString();
                //    dataGridView3.Rows.Add("TOTAL", montantTotal);
                //    dataGridView2.Rows.Add(
                //              "",
                //             "",
                //              "TOTAL",
                //              "",
                //              "",
                //            montantTotal,
                //            montantTotalAssure, montantTotalAssureur
                //              );

                //    foreach (DataGridViewRow row in dataGridView2.Rows)
                //    {
                //        if (row.Cells[2].Value.ToString() == "SOUS TOTAL")
                //        {
                //            row.DefaultCellStyle.BackColor = Color.Yellow;
                //        }
                //        else if (row.Cells[2].Value.ToString() == "TOTAL")
                //        {
                //            row.DefaultCellStyle.BackColor = Color.Orange;
                //        }
                //    }
                //    etatImpression = "3";
                //}
                //else
                //{
                    label12.Text = "";
                    label13.Visible = false;
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    dataGridView2.Rows.Clear();
                    dataGridView1.Rows.Clear();

                    dataGridView1.Rows.Clear();
                    foreach (DataGridViewRow dtGrid in dgvPatient.SelectedRows)
                    {
                        var nomPatient = "";
                        int idPatient;
                        if (dtGrid.Cells[4].Value.ToString() != "")
                        {
                            nomPatient = dtGrid.Cells[1].Value.ToString() + " s/c " + dtGrid.Cells[4].Value.ToString();
                        }
                        else
                        {
                            nomPatient = dtGrid.Cells[1].Value.ToString();
                        }

                        if (Int32.TryParse(dtGrid.Cells[0].Value.ToString(), out idPatient))
                        {
                            var dtFacture = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                            var dtCredit = ConnectionClassPharmacie.ListeDesCredit(idPatient, dtGrid.Cells[1].Value.ToString(), cmbEntreprise.Text, dtp1.Value.Date, dtp2.Value.Date.AddHours(24));
                            var total = 0.0;
                            foreach (DataRow dtRow in dtFacture.Rows)
                            {
                                total += Double.Parse(dtRow.ItemArray[6].ToString());
                            }
                            foreach (DataRow dtRow in dtCredit.Rows)
                            {
                                total += Double.Parse(dtRow.ItemArray[7].ToString());
                            }
                            RapportTotalGrouperParPatient(dtFacture, dtCredit, total, nomPatient);
                            var count = dataGridView3.Rows.Count;
                        }
                    }

                    dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count - 1]);
                    var montantTotal = 0.0;

                    foreach (DataGridViewRow dtRow in dataGridView1.Rows)
                    {
                        double total;
                        if (Double.TryParse(dtRow.Cells[6].Value.ToString(), out total))
                        {
                            montantTotal += total;
                        }
                    }
                    lblTotal.Text = montantTotal.ToString();
                    etatImpression = "2";
                //}
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var listePatient = ConnectionClassClinique.ListeDesPatientsParEntreprise(cmbEntreprise.Text, textBox1.Text);
            ListeDespatients(listePatient);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void ImprimerRapportAvecCharge()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                var pathFolder = "C:\\Dossier Clinique";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                pathFolder = pathFolder + "\\Rapport des conventionnees";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                sfd.FileName = "Rapport de " + cmbEntreprise.Text + " _imprimé_le_" + date + ".pdf";

                if (indexEntreprise == 1)
                {   if (dataGridView2.Rows.Count > 0 && cmbEntreprise.Text != "")
                        {
                            var listeEntreprise = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);

                            var entreprise = new Entreprise(listeEntreprise[0].NumeroEntreprise, listeEntreprise[0].NomEntreprise, listeEntreprise[0].Telephone1,
                                listeEntreprise[0].Telephone2, listeEntreprise[0].Email, listeEntreprise[0].Adresse, listeEntreprise[0].DateAbonnement, listeEntreprise[0].PrixHonoraire, indexEntreprise);
                            var rowCount = dataGridView2.Rows.Count;

                            #region MyRegion
                            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                rapportBitmap = Impression.RapportDuneEntrepriseAvecCharge(dataGridView2, entreprise, dtp2.Value);
                                var inputImage = @"cdali";
                                // Create an empty page
                                sharpPDF.pdfPage page = document.addPage(550, 820);

                                document.addImageReference(rapportBitmap, inputImage);
                                sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage);
                                page.addImage(img, -0, 0, page.height, page.width);
                                if (rowCount >= 13)
                                {
                                    var Count = (dataGridView2.Rows.Count - 13) / 23;

                                    for (var i = 0; i <= Count; i++)
                                    {
                                        if (i * 23 < dataGridView2.Rows.Count)
                                        {
                                            rapportBitmap = Impression.RapportDuneEntrepriseParPageAvecCharge(dataGridView2, i);

                                            inputImage = @"cdali" + i;
                                            // Create an empty page
                                            sharpPDF.pdfPage page1 = document.addPage(550, 820);
                                            document.addImageReference(rapportBitmap, inputImage);
                                            img = document.getImageReference(inputImage);
                                            page1.addImage(img, -0, 0, page1.height, page1.width);
                                        }
                                    }

                                }
                            }
                            #endregion
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    
                }
                else
                {
                    double charge;
                    if (double.TryParse(textBox2.Text, out charge))
                    {
                        if (dataGridView2.Rows.Count > 0 && cmbEntreprise.Text != "")
                        {
                            var listeEntreprise = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);

                            var entreprise = new Entreprise(listeEntreprise[0].NumeroEntreprise, listeEntreprise[0].NomEntreprise, listeEntreprise[0].Telephone1,
                                listeEntreprise[0].Telephone2, listeEntreprise[0].Email, listeEntreprise[0].Adresse, listeEntreprise[0].DateAbonnement, listeEntreprise[0].PrixHonoraire, indexEntreprise);
                            var rowCount = dataGridView1.Rows.Count;

                            #region MyRegion
                            

                            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                rapportBitmap = Impression.RapportDuneEntrepriseAvecCharge(dataGridView2, entreprise, dtp2.Value, Double.Parse(lblTotal.Text), charge);
                                var inputImage = @"cdali";
                                // Create an empty page
                                sharpPDF.pdfPage page = document.addPage(500, 700);

                                document.addImageReference(rapportBitmap, inputImage);
                                sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage);
                                page.addImage(img, -10, 0, page.height, page.width);
                                if (rowCount > 11)
                                {
                                    var Count = (dataGridView2.Rows.Count - 11) / 23;

                                    for (var i = 0; i <= Count; i++)
                                    {
                                        if (i * 23 < dataGridView2.Rows.Count)
                                        {
                                            rapportBitmap = Impression.RapportDuneEntrepriseParPageAvecCharge(dataGridView2, Double.Parse(lblTotal.Text), i, charge);

                                            inputImage = @"cdali" + i;
                                            // Create an empty page
                                            sharpPDF.pdfPage page1 = document.addPage(500, 700);
                                            document.addImageReference(rapportBitmap, inputImage);
                                            img = document.getImageReference(inputImage);
                                            page1.addImage(img, -10, 0, page1.height, page1.width);
                                        }
                                    }

                                }
                            }
                            #endregion
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Impression", ex);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(null, null);
            }
        }

        void RapportTotalGrouperParPatient(DataTable dtFacture, DataTable dtPharm, double total,string nomPatient)
        {
            try
            {
                {
                    if (dtFacture.Rows.Count == 1)
                    {
                        dataGridView1.Rows.Add(
                                    dtFacture.Rows[0].ItemArray[0].ToString(),
                                    nomPatient.ToUpper(),
                                    DateTime.Parse(dtFacture.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                    dtFacture.Rows[0].ItemArray[3].ToString().ToUpper(),
                                    dtFacture.Rows[0].ItemArray[5].ToString(),
                                    dtFacture.Rows[0].ItemArray[4].ToString(),
                                          dtFacture.Rows[0].ItemArray[6].ToString(),
                                               dtFacture.Rows[0].ItemArray[2].ToString()
                                    );
                        Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    }
                    else if (dtFacture.Rows.Count > 1)
                    {
                        dataGridView1.Rows.Add(
                                  dtFacture.Rows[0].ItemArray[0].ToString(),
                                  nomPatient.ToUpper(),
                                  DateTime.Parse(dtFacture.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                  dtFacture.Rows[0].ItemArray[3].ToString().ToUpper(),
                                  dtFacture.Rows[0].ItemArray[5].ToString(),
                                  dtFacture.Rows[0].ItemArray[4].ToString(),
                                  dtFacture.Rows[0].ItemArray[6].ToString(),
                                  dtFacture.Rows[0].ItemArray[2].ToString()
                                  );
                        Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                        for (var i = 1; i < dtFacture.Rows.Count; i++)
                        {

                            dataGridView1.Rows.Add(
                                   dtFacture.Rows[i].ItemArray[0].ToString(),
                                   "",
                                   DateTime.Parse(dtFacture.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                                   dtFacture.Rows[i].ItemArray[3].ToString().ToUpper(),
                                   dtFacture.Rows[i].ItemArray[5].ToString(),
                                   dtFacture.Rows[i].ItemArray[4].ToString(),
                                   dtFacture.Rows[i].ItemArray[6].ToString(),
                                        dtFacture.Rows[i].ItemArray[2].ToString()
                                   );
                            Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                        }
                    }
               
                    #region Pharmacie
                    if (dtPharm.Rows.Count == 1)
                    {

                        if (dtFacture.Rows.Count > 0)
                        {
                            nomPatient = "";
                        }
                        else
                        {
                            nomPatient = dtPharm.Rows[0].ItemArray[8].ToString();
                        }

                        dataGridView1.Rows.Add(
                            dtPharm.Rows[0].ItemArray[0].ToString(),
                            nomPatient.ToUpper(),
                             DateTime.Parse(dtPharm.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                            dtPharm.Rows[0].ItemArray[4].ToString(),
                            dtPharm.Rows[0].ItemArray[5].ToString(),
                            dtPharm.Rows[0].ItemArray[6].ToString(),
                            dtPharm.Rows[0].ItemArray[7].ToString(),
                            dtPharm.Rows[0].ItemArray[3].ToString()

                            );
                        Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    }
                    else if (dtPharm.Rows.Count > 1)
                    {
                        if (dtFacture.Rows.Count > 0)
                        {
                            nomPatient = "";
                        }
                        else
                        {
                            nomPatient = dtPharm.Rows[0].ItemArray[8].ToString();
                        }
                        dataGridView1.Rows.Add(
                            dtPharm.Rows[0].ItemArray[0].ToString(),
                            nomPatient.ToUpper(),
                             DateTime.Parse(dtPharm.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                            dtPharm.Rows[0].ItemArray[4].ToString(),
                            dtPharm.Rows[0].ItemArray[5].ToString(),
                            dtPharm.Rows[0].ItemArray[6].ToString(),
                            dtPharm.Rows[0].ItemArray[7].ToString(),
                            dtPharm.Rows[0].ItemArray[3].ToString()

                            );
                        Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;

                        for (var m = 1; m < dtPharm.Rows.Count; m++)
                        {
                            dataGridView1.Rows.Add(
                            dtPharm.Rows[m].ItemArray[0].ToString(),
                            "",
                             DateTime.Parse(dtPharm.Rows[m].ItemArray[1].ToString()).ToShortDateString(),
                            dtPharm.Rows[m].ItemArray[4].ToString(),
                            dtPharm.Rows[m].ItemArray[5].ToString(),
                            dtPharm.Rows[m].ItemArray[6].ToString(),
                            dtPharm.Rows[m].ItemArray[7].ToString(),
                            dtPharm.Rows[m].ItemArray[3].ToString()

                            );
                            Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                        }
                    #endregion
                    }
                    var count = dtFacture.Rows.Count + dtPharm.Rows.Count;

                    if (count > 0)
                    {
                        dataGridView3.Rows.Add(nomPatient.ToUpper(), total);

                        dataGridView1.Rows.Add(
                                 "",
                                "",
                                 "",
                                "SOUS TOTAL",
                                 "",
                                "",
                                total,
                        ""
                                 );
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }
        }


        void RapportTotalGrouperParPatientAvecIndexDifferente(DataTable dtFacture, double total, double totalAssure,double totalAsureur,string nomPatient)
        {
            try
            {
                #region Facture
                if (indexEntreprise == 1)
                {

                    if (dtFacture.Rows.Count == 1)
                    {
                        var prixTotal = double.Parse(dtFacture.Rows[0].ItemArray[6].ToString());

                        var pourcentageAssure = 100.0;
                        var pourcentageAssureur = 100.0;
                        if (dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                        {
                            pourcentageAssure = 80;
                            pourcentageAssureur = 20;
                        }
                        else if (dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION") &&
                            !dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                        {
                            pourcentageAssure = 60;
                            pourcentageAssureur = 40;
                        }
                        else if (dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("OPERATOIRE") ||
                            dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("RX") ||
                            dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("ACCOUCHEMENT") ||
                            dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("ECHOGRAPHIE") ||
                            dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("ELECTROCQRDIOGRAMME"))
                        {
                            pourcentageAssure = 80;
                            pourcentageAssureur = 20;
                        }
                        else
                        {
                            pourcentageAssure = 60;
                            pourcentageAssureur = 40;
                        }
                        var prixTotalAssure = prixTotal * pourcentageAssure / 100;
                        var prixTotalAssureur = prixTotal * pourcentageAssureur / 100;
                        dataGridView2.Rows.Add(
                                    nomPatient.ToUpper(),
                                    DateTime.Parse(
                                    dtFacture.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                    dtFacture.Rows[0].ItemArray[3].ToString().ToUpper(),
                                    dtFacture.Rows[0].ItemArray[5].ToString(),
                                    dtFacture.Rows[0].ItemArray[4].ToString(),
                                      prixTotal,
                                      prixTotalAssure, prixTotalAssureur
                                    );
                    }
                    else if (dtFacture.Rows.Count > 1)
                    {
                        var prixTotal = double.Parse(dtFacture.Rows[0].ItemArray[6].ToString());

                        var pourcentageAssure = 100.0;
                        var pourcentageAssureur = 100.0;
                        if (dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                        {
                            pourcentageAssure = 80;
                            pourcentageAssureur = 20;
                        }
                        else if (dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION") &&
                            !dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                        {
                            pourcentageAssure = 60;
                            pourcentageAssureur = 40;
                        }
                        else if (dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("OPERATOIRE") ||
                            dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("RX") ||
                            dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("ACCOUCHEMENT") ||
                            dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("ECHOGRAPHIE") ||
                            dtFacture.Rows[0].ItemArray[3].ToString().ToUpper().Contains("ELECTROCQRDIOGRAMME"))
                        {
                            pourcentageAssure = 80;
                            pourcentageAssureur = 20;
                        }
                        else
                        {
                            pourcentageAssure = 60;
                            pourcentageAssureur = 40;
                        }
                        var prixTotalAssure = prixTotal * pourcentageAssure / 100;
                        var prixTotalAssureur = prixTotal * pourcentageAssureur / 100;
                        dataGridView2.Rows.Add(
                                    nomPatient.ToUpper(),
                                    DateTime.Parse(
                                    dtFacture.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
                                    dtFacture.Rows[0].ItemArray[3].ToString().ToUpper(),
                                    dtFacture.Rows[0].ItemArray[5].ToString(),
                                    dtFacture.Rows[0].ItemArray[4].ToString(),
                                      prixTotal,
                                      prixTotalAssure, prixTotalAssureur
                                    );
                        for (var i = 1; i < dtFacture.Rows.Count; i++)
                        {
                            prixTotal = double.Parse(dtFacture.Rows[i].ItemArray[6].ToString());

                            pourcentageAssure = 100.0;
                            pourcentageAssureur = 100.0;
                            if (dtFacture.Rows[i].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                            {
                                pourcentageAssure = 80;
                                pourcentageAssureur = 20;
                            }
                            else if (dtFacture.Rows[i].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION") &&
                                !dtFacture.Rows[i].ItemArray[3].ToString().ToUpper().Contains("CONSULTATION EN"))
                            {
                                pourcentageAssure = 60;
                                pourcentageAssureur = 40;
                            }
                            else if (dtFacture.Rows[i].ItemArray[3].ToString().ToUpper().Contains("OPERATOIRE") ||
                                dtFacture.Rows[i].ItemArray[3].ToString().ToUpper().Contains("RX") ||
                                dtFacture.Rows[i].ItemArray[3].ToString().ToUpper().Contains("ACCOUCHEMENT") ||
                                dtFacture.Rows[i].ItemArray[3].ToString().ToUpper().Contains("ECHOGRAPHIE") ||
                                dtFacture.Rows[i].ItemArray[3].ToString().ToUpper().Contains("ELECTROCQRDIOGRAMME"))
                            {
                                pourcentageAssure = 80;
                                pourcentageAssureur = 20;
                            }
                            else
                            {
                                pourcentageAssure = 60;
                                pourcentageAssureur = 40;
                            }
                            prixTotalAssure = prixTotal * pourcentageAssure / 100;
                            prixTotalAssureur = prixTotal * pourcentageAssureur / 100;
                            dataGridView2.Rows.Add(
                                        "",
                                        DateTime.Parse(
                                        dtFacture.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
                                        dtFacture.Rows[i].ItemArray[3].ToString().ToUpper(),
                                        dtFacture.Rows[i].ItemArray[5].ToString(),
                                        dtFacture.Rows[i].ItemArray[4].ToString(),
                                          prixTotal,
                                          prixTotalAssure, prixTotalAssureur
                                        );
                        }
                    }
                #endregion
                    var count = dtFacture.Rows.Count;

                    if (count > 0)
                    {
                        dataGridView3.Rows.Add(nomPatient.ToUpper(), total);
                        dataGridView2.Rows.Add(
                                 "",   "",   "SOUS TOTAL",  "",   "",  total, totalAssure, totalAsureur
                                 );
                    }
                }       
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Rapport entreprise", ex);
            }
        }

        void RapportGrouperAvecCharge(DataTable dtAnalyse, DataTable dtConsult, DataTable dtHosp, DataTable dtPharm, double charge, double total)
        {
//            try
//            {
//                var items = new List<string>();
//                var patientConsultation = "";
//                var patientAnalyse = "";
//                var consulta = "";
//                //var total = 0.0;
//                if (dtConsult.Rows.Count > 1)
//                {
//                    if (dtConsult.Rows[0].ItemArray[6].ToString() != "")
//                    {
//                        patientConsultation = dtConsult.Rows[0].ItemArray[0].ToString() + " " + dtConsult.Rows[0].ItemArray[1].ToString() + " s/c " +
//                            dtConsult.Rows[0].ItemArray[6].ToString();
//                    }
//                    else
//                    {
//                        patientConsultation = dtConsult.Rows[0].ItemArray[0].ToString() + " " + dtConsult.Rows[0].ItemArray[1].ToString();
//                    }
//                    if (dtConsult.Rows[0].ItemArray[2].ToString() == "Consultation De  Specialité")
//                    {
//                        consulta = "Consultation en " + dtConsult.Rows[0].ItemArray[7].ToString();
//                    }
//                    else
//                    {
//                        consulta = dtConsult.Rows[0].ItemArray[2].ToString();
//                    }
//                    var montant = double.Parse(dtConsult.Rows[0].ItemArray[4].ToString());
//                    var assure = montant * charge / 100;
//                    var assureur = montant - assure;
//                    dataGridView2.Rows.Add(
//                        patientConsultation.ToUpper(), 
//                        DateTime.Parse(dtConsult.Rows[0].ItemArray[3].ToString()).ToShortDateString(),
//                        consulta.ToUpper(),
//                        1, montant, montant, assure, assureur

//                       );

//                    for (int i = 1; i < dtConsult.Rows.Count; i++)
//                    {

//                        if (dtConsult.Rows[i].ItemArray[2].ToString() == "Consultation De  Specialité")
//                        {
//                            consulta = "Consultation en " + dtConsult.Rows[i].ItemArray[7].ToString();
//                        }
//                        else
//                        {
//                            consulta = dtConsult.Rows[i].ItemArray[2].ToString();
//                        }
//                        montant = double.Parse(dtConsult.Rows[1].ItemArray[4].ToString());
//                        assure = montant * charge / 100;
//                        assureur = montant - assure;
//                        dataGridView2.Rows.Add(
//                           "", DateTime.Parse(dtConsult.Rows[0].ItemArray[3].ToString()).ToShortDateString(),
//                            consulta.ToUpper(), 1,
//                            montant, montant, assure, assureur

//                           );
//                    }
//                }
//                else if (dtConsult.Rows.Count == 1)
//                {
//                    if (dtConsult.Rows[0].ItemArray[6].ToString() != "")
//                    {
//                        patientConsultation = dtConsult.Rows[0].ItemArray[0].ToString() + " " + dtConsult.Rows[0].ItemArray[1].ToString() + " s/c " +
//                            dtConsult.Rows[0].ItemArray[6].ToString();
//                    }
//                    else
//                    {
//                        patientConsultation = dtConsult.Rows[0].ItemArray[0].ToString() + " " + dtConsult.Rows[0].ItemArray[1].ToString();
//                    }
//                    if (dtConsult.Rows[0].ItemArray[2].ToString() == "Consultation De  Specialité")
//                    {
//                        consulta = "Consultation en " + dtConsult.Rows[0].ItemArray[7].ToString();
//                    }
//                    else
//                    {
//                        consulta = dtConsult.Rows[0].ItemArray[2].ToString();
//                    }
//                    var montant = double.Parse(dtConsult.Rows[0].ItemArray[4].ToString());
//                    var assure = montant * charge / 100;
//                    var assureur = montant - assure;
//                    dataGridView2.Rows.Add(
//                       patientConsultation.ToUpper(),
//                       DateTime.Parse(dtConsult.Rows[0].ItemArray[3].ToString()).ToShortDateString(),
//                        consulta.ToUpper(),
//                      1, montant, montant, assure, assureur

//                       );
//                }




//                if (dtAnalyse.Rows.Count > 1)
//                {
//                    var listePatient = ConnectionClassClinique.ListeDesPatients(Int32.Parse(dtAnalyse.Rows[0].ItemArray[2].ToString()));

//                    if (listePatient[0].SousCouvert != "")
//                    {
//                        patientAnalyse = listePatient[0].Nom + " " + listePatient[0].Prenom + " s/c " + listePatient[0].SousCouvert;
//                    }
//                    else
//                    {
//                        patientAnalyse = listePatient[0].Nom + " " + listePatient[0].Prenom;
//                    }
//                    if (patientConsultation == patientAnalyse)
//                    {
//                        for (j = 0; j < dtAnalyse.Rows.Count; j++)
//                        {
//                            #region MyRegion1
//                            var analyse = dtAnalyse.Rows[j].ItemArray[6].ToString().ToUpper();
//                            var montant = Double.Parse(dtAnalyse.Rows[j].ItemArray[7].ToString());
//                            int nbre;
//                            if (Int32.TryParse(dtAnalyse.Rows[j].ItemArray[8].ToString(), out nbre))
//                            {
//                            }
//                            else
//                            {
//                                nbre = 1;
//                            }
//                            var montantTotal = nbre * montant;
//                            double chargeAssure, chargeAssureur;
//                            if (analyse.Contains("HOSPITALISATION") || analyse.Contains("ACCOUCHEMENT") ||
//                              analyse.Contains("OBSERVATION") || analyse.Contains("ACTE OPERATOIRE"))
//                            {
//                                chargeAssure = 0;
//                                chargeAssureur = montantTotal;
//                            }
//                            else
//                            {
//                                chargeAssure = montantTotal * charge / 100;
//                                chargeAssureur = montantTotal - chargeAssure;
//                            }
//#endregion
//                            dataGridView2.Rows.Add(
//                             "",DateTime.Parse(dtAnalyse.Rows[j].ItemArray[1].ToString()).ToShortDateString(),
                          
//                            analyse, 1,
//                            montant,
//                            montantTotal, chargeAssure, chargeAssureur
//                            );
//                        }
//                    }
//                    else
//                    {
//                        #region MyRegion2
//                        var analyse = dtAnalyse.Rows[0].ItemArray[6].ToString().ToUpper();
//                        var montant = Double.Parse(dtAnalyse.Rows[0].ItemArray[7].ToString());
//                        int nbre;
//                        if (Int32.TryParse(dtAnalyse.Rows[0].ItemArray[8].ToString(), out nbre))
//                        {
//                        }
//                        else
//                        {
//                            nbre = 1;
//                        }
//                        var montantTotal = nbre * montant;
//                        #endregion
//                        double chargeAssure, chargeAssureur;
//                        if (analyse.Contains("HOSPITALISATION") || analyse.Contains("ACCOUCHEMENT") ||
//                          analyse.Contains("OBSERVATION") || analyse.Contains("ACTE OPERATOIRE"))
//                        {
//                             chargeAssure = montantTotal * charge / 100;
//                            chargeAssureur = montantTotal - chargeAssure;
//                        }
//                        else
//                        {
//                            chargeAssure = montantTotal * charge / 100;
//                            chargeAssureur = montantTotal - chargeAssure;
//                        }
//                        dataGridView2.Rows.Add(
//                           patientAnalyse.ToUpper(),
//                           DateTime.Parse(dtAnalyse.Rows[j].ItemArray[1].ToString()).ToShortDateString(),
//                           analyse, 1,
//                         montant,
//                          montantTotal, chargeAssure, chargeAssureur
//                       );
//                        for (j = 1; j < dtAnalyse.Rows.Count; j++)
//                        {
//                            listePatient = ConnectionClassClinique.ListeDesPatients(Int32.Parse(dtAnalyse.Rows[j].ItemArray[2].ToString()));

//                            #region MyRegion3
//                            analyse = dtAnalyse.Rows[j].ItemArray[6].ToString().ToUpper();
//                             montant = Double.Parse(dtAnalyse.Rows[j].ItemArray[7].ToString());
//                            if (Int32.TryParse(dtAnalyse.Rows[j].ItemArray[8].ToString(), out nbre))
//                            {
//                            }
//                            else
//                            {
//                                nbre = 1;
//                            }
//                            #endregion
//                            ////double chargeAssure, chargeAssureur;
//                            if (analyse.Contains("HOSPITALISATION") || analyse.Contains("ACCOUCHEMENT") ||
//                              analyse.Contains("OBSERVATION") || analyse.Contains("ACTE OPERATOIRE"))
//                            {
//                                chargeAssure = montantTotal * charge / 100;
//                                chargeAssureur = montantTotal - chargeAssure;
//                            }
//                            else
//                            {
//                                chargeAssure = montantTotal * charge / 100;
//                                chargeAssureur = montantTotal - chargeAssure;
//                            }
//                            dataGridView2.Rows.Add(
//                                "",
//                                DateTime.Parse(dtAnalyse.Rows[j].ItemArray[1].ToString()).ToShortDateString(),
//                                analyse, 1,
//                                montant,
//                              montantTotal, chargeAssure, chargeAssureur
//                                );
//                        }

//                    }

//                }
//                else if (dtAnalyse.Rows.Count == 1)
//                {

//                    #region MyRegion4
//                    var analyse = dtAnalyse.Rows[0].ItemArray[6].ToString().ToUpper();
//                    var montant = Double.Parse(dtAnalyse.Rows[0].ItemArray[7].ToString());
//                    int nbre;
//                    if (Int32.TryParse(dtAnalyse.Rows[0].ItemArray[8].ToString(), out nbre))
//                    {
//                    }
//                    else
//                    {
//                        nbre = 1;
//                    }
//                    var montantTotal = nbre * montant;
//                    double chargeAssure, chargeAssureur;
//                    if (analyse.Contains("HOSPITALISATION") || analyse.Contains("ACCOUCHEMENT") ||
//                      analyse.Contains("OBSERVATION") || analyse.Contains("ACTE OPERATOIRE"))
//                    {
//                        chargeAssure = 0;
//                        chargeAssureur = montantTotal;
//                    }
//                    else
//                    {
//                        chargeAssure = montantTotal * charge / 100;
//                        chargeAssureur = montantTotal - chargeAssure;
//                    }
//                    #endregion
//                    var listePatient = ConnectionClassClinique.ListeDesPatients(Int32.Parse(dtAnalyse.Rows[0].ItemArray[2].ToString()));

//                    //var nomPatient = "";

//                    if (listePatient[0].SousCouvert != "")
//                    {
//                        patientAnalyse = listePatient[0].Nom + " " + listePatient[0].Prenom + " s/c " + listePatient[0].SousCouvert;
//                    }
//                    else
//                    {
//                        patientAnalyse = listePatient[0].Nom + " " + listePatient[0].Prenom;
//                    }
//                    if (patientConsultation == patientAnalyse)
//                    {
//                        dataGridView2.Rows.Add(
//                             "",
//                            DateTime.Parse(dtAnalyse.Rows[0].ItemArray[1].ToString()).ToShortDateString(),
//                            analyse, 1,
//                            montant,
//                           montantTotal, chargeAssure, chargeAssureur
//                            );
//                    }
//                    else
//                    {
//                        dataGridView2.Rows.Add(
//                                                  patientAnalyse.ToUpper(),
//                                DateTime.Parse(dtAnalyse.Rows[j].ItemArray[1].ToString()).ToShortDateString(),
//                                                   analyse, 1,
//                                                   montant,
//                                               montantTotal, chargeAssure, chargeAssureur
//                                                   );
//                    }
//                }
//                else
//                {

//                }

//                if (dtPharm.Rows.Count == 1)
//                {
//                    var nomPatient = "";
//                    if (dtConsult.Rows.Count > 0)
//                    {
//                    }
//                    else if (dtAnalyse.Rows.Count > 0)
//                    {
//                        nomPatient = "";
//                    }
//                    else
//                    {
//                        nomPatient = dtPharm.Rows[0].ItemArray[1].ToString();
//                    }
//                    var montant = double.Parse(dtPharm.Rows[0].ItemArray[7].ToString());
//                    double chargeAssure, chargeAssureur;
//                    if (dtPharm.Rows[0].ItemArray[4].ToString().ToUpper().Contains("OBSERVATION"))
//                    {
//                        chargeAssure = 0;
//                        chargeAssureur = montant;
//                    }
//                    else
//                    {
//                        chargeAssure = charge * montant / 100;
//                        chargeAssureur = montant - chargeAssure;
//                    }
//                    dataGridView2.Rows.Add(
//                        nomPatient.ToUpper(),
//                         DateTime.Parse(dtPharm.Rows[0].ItemArray[2].ToString()).ToShortDateString(),
//                        dtPharm.Rows[0].ItemArray[4].ToString(),
//                        dtPharm.Rows[0].ItemArray[6].ToString(),
//                        dtPharm.Rows[0].ItemArray[5].ToString(),
//                        dtPharm.Rows[0].ItemArray[7].ToString(),
//                        charge, chargeAssureur
//                        );
//                }
//                else if (dtPharm.Rows.Count > 1)
//                {
//                    var nomPatient = "";
//                    if (dtConsult.Rows.Count > 0)
//                    {
//                    }
//                    else if (dtAnalyse.Rows.Count > 0)
//                    {
//                        nomPatient = "";
//                    }
//                    else
//                    {
//                        nomPatient = dtPharm.Rows[0].ItemArray[1].ToString();
//                    }
//                    var montant = double.Parse(dtPharm.Rows[0].ItemArray[7].ToString());
//                    double chargeAssure, chargeAssureur;
//                    if (dtPharm.Rows[0].ItemArray[4].ToString().ToUpper().Contains("OBSERVATION"))
//                    {
//                        chargeAssure = 0;
//                        chargeAssureur = montant;
//                    }
//                    else
//                    {
//                        chargeAssure = charge * montant / 100;
//                        chargeAssureur = montant - chargeAssure;
//                    }
//                    dataGridView2.Rows.Add(
//                          nomPatient.ToUpper(),
//                           DateTime.Parse(dtPharm.Rows[0].ItemArray[2].ToString()).ToShortDateString(),
//                          dtPharm.Rows[0].ItemArray[4].ToString(),
//                          dtPharm.Rows[0].ItemArray[6].ToString(),
//                          dtPharm.Rows[0].ItemArray[5].ToString(),
//                          dtPharm.Rows[0].ItemArray[7].ToString(),
//                          chargeAssure, chargeAssureur
//                          );

//                    for (var m = 1; m < dtPharm.Rows.Count; m++)
//                    {

//                        montant = double.Parse(dtPharm.Rows[m].ItemArray[7].ToString());
//                        //double chargeAssure, chargeAssureur;
//                        if (dtPharm.Rows[m].ItemArray[4].ToString().ToUpper().Contains("OBSERVATION"))
//                        {
//                            chargeAssure = 0;
//                            chargeAssureur = montant;
//                        }
//                        else
//                        {
//                            chargeAssure = charge * montant / 100;
//                            chargeAssureur = montant - chargeAssure;
//                        }
//                        dataGridView2.Rows.Add(
//                            "",
//                             DateTime.Parse(dtPharm.Rows[m].ItemArray[2].ToString()).ToShortDateString(),
//                            dtPharm.Rows[m].ItemArray[4].ToString(),
//                            dtPharm.Rows[m].ItemArray[6].ToString(),
//                            dtPharm.Rows[m].ItemArray[5].ToString(),
//                            dtPharm.Rows[m].ItemArray[7].ToString(),
//chargeAssure, chargeAssureur
//                            );
//                    }
//                }

//                for (int k = 0; k < dtHosp.Rows.Count; k++)
//                {
//                    var nomPatient = "";
//                    var patientHospitalisation = "";
//                    if (dtHosp.Rows[k].ItemArray[5].ToString() != "")
//                    {
//                        patientHospitalisation = dtHosp.Rows[k].ItemArray[1].ToString() + " " + dtHosp.Rows[k].ItemArray[2].ToString() +
//                             " s/c " + dtHosp.Rows[k].ItemArray[5].ToString();
//                    }
//                    else
//                    {
//                        patientHospitalisation = dtHosp.Rows[k].ItemArray[1].ToString() + " " + dtHosp.Rows[k].ItemArray[2].ToString();
//                    }
//                    if (patientHospitalisation == patientConsultation || patientHospitalisation == patientAnalyse)
//                    {
//                        nomPatient = "";
//                    }
//                    else
//                    {
//                        nomPatient = patientHospitalisation;

//                    }

//                    DateTime debut = DateTime.Parse(dtHosp.Rows[k].ItemArray[3].ToString());
//                    DateTime sortie = DateTime.Parse(dtHosp.Rows[k].ItemArray[6].ToString());
//                    var nbr = sortie.DayOfYear - debut.DayOfYear;

//                    var montanTotal = Double.Parse(dtHosp.Rows[k].ItemArray[4].ToString());

//                    var prix = montanTotal / nbr;
//                    string hospita;
//                    if (nbr <= 1)
//                    {
//                        hospita = "Hospitalisation";
//                    }
//                    else
//                    {
//                        hospita = nbr + "jrs  d'hospitalisation";
//                    }

//                    var chargeAssure = 0;
//                    var chargeAssureur = montanTotal - chargeAssure;
//                    dataGridView2.Rows.Add(

//                        nomPatient.ToUpper(), DateTime.Parse(dtHosp.Rows[k].ItemArray[3].ToString()).ToShortDateString(),
                        
//                        hospita.ToUpper(), nbr, prix,
//                        dtHosp.Rows[k].ItemArray[4].ToString(), chargeAssure, chargeAssureur
//                        );
//                }


//                var count = dtAnalyse.Rows.Count + dtConsult.Rows.Count + dtHosp.Rows.Count + dtPharm.Rows.Count;

//                if (count > 0)
//                {
//                    dataGridView2.Rows.Add(
//                            "",
//                           "",
//                            "SOUS TOTAL",
//                            "",
//                            "",
//                           "",
//                            "",
//                           total
//                            );
//                    dataGridView2.Rows.Add(
//                             "",
//                            "",
//                             "",
//                            "",
//                             "",
//                              "",
//                             "",
//                            ""
//                             );
//                }


//            }
//            catch (Exception ex)
//            {
//                MonMessageBox.ShowBox("Rapport entreprise", ex);
//            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode==Keys.Enter)
                {
                    double remise;
                    if (double.TryParse(textBox3.Text, out remise))
                    {
                        if (lblTotal.Text != "")
                        {
                            var totalRemise = remise * double.Parse(lblTotal.Text) / 100;
                            label12.Text = totalRemise.ToString();
                            label12.Visible = true;
                            label13.Visible = true;
                        }
                    }
                    else
                    {
                        label12.Visible = false;
                        label13.Visible = false;
                        label13.Text = "";
                    }
                }

            }
            catch { }
        }

        

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
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
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void ToCsV1(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount ; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
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

        
        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            var jour = DateTime.Now .Day;
            var mois = DateTime.Now .Month;
            var year = DateTime.Now .Year;
            var hour = DateTime.Now.Hour;
            var min = DateTime.Now.Minute;
            var sec = DateTime.Now.Second;
            var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() +"_"+hour +"_"+min +"_"+sec ;
            sfd.FileName = "Facture de " + cmbEntreprise.Text + "_Impriméé_le_" + date + ".xls";

            if (checkBox5.Checked)
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToCsV1(dataGridView3, sfd.FileName); // Here dataGridview1 is your grid view name
                }
            }else
            {
                

                if (dataGridView1.Visible == true || dataGridView2.Visible == false)
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                    }
                }
                else if (dataGridView2.Visible == true || dataGridView1.Visible == false)
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ToCsV1(dataGridView2, sfd.FileName); // Here dataGridview1 is your grid view name
                    }
                }
            }
            System.Diagnostics.Process.Start(sfd.FileName);
        }

        private void dgvPatient_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dgvPatient.ContextMenuStrip = contextMenuStripLivraison;
                dgvPatient.ContextMenuStrip.Show(dgvPatient, e.Location);
            }
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbEntreprise.Text) && dgvPatient.SelectedRows.Count > 0)
                {

                    //var numEmpl = GestionAcademique.LoginFrm.matricule;
                    //var facture = new Facture(numFacture, dateFacture, montantTotal, idPatient, numEmpl, reste);
                    var frm = new GestionDuneClinique.FormesClinique.MontantFrm();
                    var total = 0.0;
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        frm.dataGridView1.Rows.Add(
                            dataGridView1.Rows[i].Cells[3].Value.ToString(),
                            dataGridView1.Rows[i].Cells[4].Value.ToString(),
                            double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())
                            / double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()),
                            dataGridView1.Rows[i].Cells[5].Value.ToString(),
                             dataGridView1.Rows[i].Cells[6].Value.ToString(),
                              dataGridView1.Rows[i].Cells[0].Value.ToString()
                              );
                        total += double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                    }

                    frm.numeroPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    frm.txtPatient.Text = dgvPatient.SelectedRows[0].Cells[1].Value.ToString() + " "
                        + dgvPatient.SelectedRows[0].Cells[2].Value.ToString();
                    frm.txtTotal.Text = total.ToString();
                    frm.lblNetAPayer.Text = total.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void bonDeRetourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbEntreprise.Text) && dgvPatient.SelectedRows.Count > 0)
                {
                    var frm = new GestionDuneClinique.FormesClinique.MontantFrm();
                   
                    var listeFacture = from f in ConnectionClassClinique.ListeDesFactures()
                                       where f.DateFacture >= dtp1.Value.Date
                                       where f.DateFacture < dtp2.Value.Date.AddHours(24)
                                       where f.IdPatient == Convert.ToInt32(dgvPatient.SelectedRows[0].Cells[0].Value.ToString())
                                       orderby f.NumeroFacture
                                       select f;
                    var montant = 0.0;var totalPaye = 0.0;
                    frm.numeroPatient = Int32.Parse(dgvPatient.SelectedRows[0].Cells[0].Value.ToString());
                    foreach (var facture in listeFacture)
                    {
                        var sousTotalFacture = 0.0 ;
                        var detailListe = ConnectionClassClinique.DetailsDesFactures(facture.NumeroFacture);
                        var dtPaie = ConnectionClassClinique.TablePaiement(facture.NumeroFacture);
                        frm.dataGridView1.Rows.Add(" FACTURE N° " + facture.NumeroFacture + " du " +facture.DateFacture.ToShortDateString(), "", "", "");
                     
                        foreach (var f in detailListe)
                        {
                            frm. dataGridView1.Rows.Add(f.Designation, f.Prix, f.Quantite, f.PrixTotal);
                            montant += f.Prix * f.Quantite;
                            sousTotalFacture += f.Prix * f.Quantite;
                        }

                        frm.dataGridView1.Rows.Add("Total    ".ToUpper(), "", "", sousTotalFacture);
                     
                        for(var i=0; i<dtPaie.Rows.Count ;i++)
                        {
                             totalPaye += Double.Parse(dtPaie.Rows[i].ItemArray[0].ToString());
                        }
                    }
                    foreach (DataGridViewRow row in frm.dataGridView1.Rows)
                    {
                        
                        if (row.Cells[0].Value.ToString().Contains ("TOTAL    "))
                        {
                            row.DefaultCellStyle.BackColor = Color.Blue;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                    frm.txtPatient.Text = dgvPatient.SelectedRows[0].Cells[1].Value.ToString() + " "
                        + dgvPatient.SelectedRows[0].Cells[2].Value.ToString();
                    frm.txtTotal.Text = montant.ToString();
                    frm.lblNetAPayer.Text = montant.ToString();
                    frm.txtPaye.Text =totalPaye.ToString();
                    frm.txtReduction.Enabled = false;
                    frm.txtPaye.Enabled = false;
                    frm.btnApercu.Enabled = true;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                var charge = 0.0;
                dataGridView2.Rows.Clear();

              
                    if (double.TryParse(textBox2.Text, out charge))
                    {

                        dataGridView1.Visible = false;
                        dataGridView2.Visible = true;
                        foreach (DataGridViewRow dtGrid in dataGridView1.Rows)
                        {
                            string nomPatient = "", libelle = "", date = "", prixUnit = "", quantite = "", prixTotal = "", chargeAssure = "", chargeAssureur = "";
                            double montantTotal, montantUnit;
                            int qte;

                            nomPatient = dtGrid.Cells[1].Value.ToString();
                            date = dtGrid.Cells[2].Value.ToString();
                            libelle = dtGrid.Cells[3].Value.ToString();
                            if (Double.TryParse(dtGrid.Cells[5].Value.ToString(), out montantUnit))
                            {
                                prixUnit = montantUnit.ToString();
                            }
                            if (Double.TryParse(dtGrid.Cells[6].Value.ToString(), out montantTotal))
                            {
                                prixTotal = montantTotal.ToString();

                                chargeAssure = (montantTotal * charge / 100).ToString();
                                chargeAssureur = (montantTotal - (montantTotal * charge / 100)).ToString();
                            }
                            if (Int32.TryParse(dtGrid.Cells[4].Value.ToString(), out qte))
                            {
                                quantite = qte.ToString();
                            }
                            dataGridView2.Rows.Add(nomPatient, date, libelle,
                                    quantite, prixUnit, montantTotal, chargeAssure, chargeAssureur);
                            //}
                            //else
                            //{
                            //    dataGridView2.Rows.Add("", "", "",
                            //  "", "", "", "", "");
                            //}
                        }
                        var TotalCharge = 0.0;
                        double montantTotaux;
                        foreach (DataGridViewRow dtGridView in dataGridView2.Rows)
                        {
                            if (!dtGridView.Cells[2].Value.ToString().Contains("TOTAL"))
                            {
                                if (double.TryParse(dtGridView.Cells[7].Value.ToString(), out montantTotaux))
                                {
                                    TotalCharge += montantTotaux;
                                }
                            }
                        }
                        lblTotal.Text = TotalCharge.ToString();

                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (row.Cells[2].Value.ToString() == "SOUS TOTAL")
                            {
                                row.DefaultCellStyle.BackColor = Color.Yellow;
                            }
                            else if (row.Cells[2].Value.ToString() == "TOTAL")
                            {
                                row.DefaultCellStyle.BackColor = Color.OrangeRed;
                            }
                        }
                        etatImpression = "3";
                    }
                }
            
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Calculer la charge", ex);
            }
        }
        int indexEntreprise;
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cmbEntreprise.Text != "")
                {
                    var listeEntreprise = ConnectionClassClinique.ListeDesEntreprises(cmbEntreprise.Text);

                    var entreprise = new Entreprise(listeEntreprise[0].NumeroEntreprise, listeEntreprise[0].NomEntreprise, listeEntreprise[0].Telephone1,
                        listeEntreprise[0].Telephone2, listeEntreprise[0].Email, listeEntreprise[0].Adresse, listeEntreprise[0].DateAbonnement, listeEntreprise[0].PrixHonoraire,indexEntreprise);
                    dtp2.Format = DateTimePickerFormat.Long;
                    var mois = dtp2.Text.Substring(dtp2.Text.IndexOf(" ") + 1);
                    var mois1 = mois.Substring(mois.IndexOf(" "));
                    var date = dtp2.Value.ToShortDateString();
                    if (listeEntreprise[0].PrixHonoraire > 0)
                    {
                        rapportBitmap = Impression.FactureHonoraire(entreprise, date, mois1);
                        if (printDialog1.ShowDialog() == DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printPreviewDialog1.ShowDialog();
                        }
                    }
                    dtp2.Format = DateTimePickerFormat.Short;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture honoraire ", ex);
            }
        }
    }
}
