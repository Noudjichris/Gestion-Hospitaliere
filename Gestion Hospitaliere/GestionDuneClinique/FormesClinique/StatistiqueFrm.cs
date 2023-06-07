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
    public partial class StatistiqueFrm : Form
    {
        public StatistiqueFrm()
        {
            InitializeComponent();
        }

        private void StatistiqueFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 0);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox8_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox8.Width - 1, groupBox8.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.ControlLight, SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);

        }
        private void StatistiqueFrm_Load(object sender, EventArgs e)
        {
            try
            {
                button7.Location = new Point(Width - 36, 5);
                dgvStatistique.RowTemplate.Height = 30;
                //var width = 150;//listView2.Width/(liste.Count+3);
                //dgvStatistique.Columns.Add("SERVICES","SERVISES");
                //listView2.Columns.Add("SERVICES", width);
                //var i=1;
                //foreach (var rw in liste)
                //{
                //    var column= "Columns"+i;
                //    dgvStatistique.Columns.Add(column, rw.NomEntreprise);
                //    listView2.Columns.Add(rw.NomEntreprise, width);
                //        i++;
                //}
                //dgvStatistique.Columns.Add("COMMUNAUTE", "COMMUNAUTE");
                //dgvStatistique.Columns.Add("TOTAL", "TOTAL");
                //listView2.Columns.Add("COMMUNAUTE",width);
                //listView2.Columns.Add("TOTAL", width);
                var liste = AppCode.ConnectionClassClinique.ListeDesDistinguesGroupes();
                foreach (var p in liste)
                {
                    if (p.Groupe.ToUpper().Contains("VENTE MÉDICAMENTS"))
                        p.Groupe = "PHARMACIE";
                    comboBox2.Items.Add(p.Groupe.ToUpper());
                }
                //comboBox2.Items.Add("CONSULTATION SPECIALISEE");
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            //StatistiqueDuLaboratoire();
            if(comboBox2.Text=="CONSULTATION")
            {
                StatistiqueDesConsultations();
            }
            else if (comboBox2.Text == "PHARMACIE")
            {
                StatistiqueDeLaPharmacie();
            }
            else if (comboBox2.Text == "CONSULTATION SPECIALISEE")
            {
                StatistiqueDesConsultationsSpecialisees();
            }
            else 
            {
                StatistiqueDuLaboratoire();
            }
        }

        void StatistiqueDeLaPharmacie()
        {
            try
            {
                dgvStatistique.CellBorderStyle = DataGridViewCellBorderStyle.RaisedVertical;
                dgvStatistique.Columns.Clear();
                dgvStatistique.Columns.Add("SERVICES", "SERVICES ET PHARMACIE");
                dgvStatistique.Columns[0].Width = dgvStatistique.Width / 4 ;
                dgvStatistique.Columns.Add("enfants", "ENFANTS ≤ 18 ans");
                dgvStatistique.Columns[1].Width = dgvStatistique.Width / 12-5;
                dgvStatistique.Columns.Add("enfants1", "");
                dgvStatistique.Columns[2].Width = dgvStatistique.Width / 12 - 5;
                dgvStatistique.Columns.Add("Adultes", "ADULTES ≥ 18 ans");
                dgvStatistique.Columns[3].Width = dgvStatistique.Width / 12 - 5;
                dgvStatistique.Columns.Add("adultes", "");
                dgvStatistique.Columns[4].Width = dgvStatistique.Width / 12 - 5;
                dgvStatistique.Columns.Add("total", "TOTAL");
                dgvStatistique.Columns[5].Width = dgvStatistique.Width / 12 - 0;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[6].Width = dgvStatistique.Width / 12 - 0;
                dgvStatistique.Columns.Add("total2", "");
                dgvStatistique.Columns[7].Width = dgvStatistique.Width / 12 - 0;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[8].Width = dgvStatistique.Width / 12 - 0;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[9].Width = dgvStatistique.Width / 12 - 0;

                dgvStatistique.Rows.Clear();
                dgvStatistique.Rows.Add("SEXE ", "M", "F", "M", "F", "TOTAL M", "TOTAL F", "TOTAL ENF", "TOTAL ADL", "TOTAL");
                dgvStatistique.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                var liste = AppCode.ConnectionClassClinique.ListeDesEntreprises();
                var entreprise = new AppCode.Entreprise(0, "", "", "", "", "", DateTime.Now, 0.0,"", false,0);
                liste.Add(entreprise);
                var totalCountFemininEnfant = 0;
                var totalCountFemininAdulte = 0;
                var totalCountMasculinEnfant = 0;
                var totalCountMasculinAdulte = 0;
                var totalGlobalFeminin = 0;
                var totalGlobalMasculin = 0;
                var totalGlobalEnfant = 0;
                var totalGlobalAdulte = 0;

                foreach (var rw in liste)
                {
                    var nomEntreprise = "";
                    var countFemininEnfant = 0;
                    var counttFemininAdulte = 0;
                    var countMasculinEnfant = 0;
                    var counttMasculinAdulte = 0;
                  
                    if (string.IsNullOrEmpty(rw.NomEntreprise))
                    {
                        nomEntreprise = "COMMUNAUTE";
                        #region SujetMasculin
                        var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M");
                        var patientMasculinEnfant = new List<AppCode.Patient>();
                        var patientMasculinAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientMasculin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientMasculinEnfant.Add(p);
                                }
                                else
                                {
                                    patientMasculinAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientMasculinEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientMasculinAdulte.Add(p);
                            }
                            else 
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }

                        var pp = new AppCode.Patient();
                        pp.Nom = "CLIENT COMPTANT";
                        patientMasculinAdulte.Add(pp);

                        foreach (var pma in patientMasculinAdulte)
                        {
                            totalCountMasculinAdulte += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatient
                               (dateTimePicker1.Value, dateTimePicker2.Value.Date, pma.Nom + " " + pma.Prenom);

                            counttMasculinAdulte += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatient
                                (dateTimePicker1.Value, dateTimePicker2.Value.Date, pma.Nom + " " + pma.Prenom);
                         }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            totalCountMasculinEnfant += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatient
                               (dateTimePicker1.Value, dateTimePicker2.Value.Date, pme.Nom + " " + pme.Prenom);

                            countMasculinEnfant += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatient
                                (dateTimePicker1.Value, dateTimePicker2.Value.Date, pme.Nom + " " + pme.Prenom);

                         }
                    
                        #endregion

                        #region SujetFeminin
                        var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F");
                        var patientFemininEnfant = new List<AppCode.Patient>();
                        var patientFemininAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientFeminin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientFemininEnfant.Add(p);
                                }
                                else
                                {
                                    patientFemininAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientFemininEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientFemininAdulte.Add(p);
                            }
                            else 
                            {
                                patientFemininEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientFemininAdulte)
                        {
                            counttFemininAdulte += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatient
                                (dateTimePicker1.Value, dateTimePicker2.Value.Date, pma.Nom + " " + pma.Prenom);

                            totalCountFemininAdulte += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatient
                             (dateTimePicker1.Value, dateTimePicker2.Value.Date, pma.Nom + " " + pma.Prenom);
}
                        foreach (var pme in patientFemininEnfant)
                        {
                            countFemininEnfant += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatient
                               (dateTimePicker1.Value, dateTimePicker2.Value.Date, pme.Nom + " " + pme.Prenom);

                            totalCountFemininEnfant += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatient
                                (dateTimePicker1.Value, dateTimePicker2.Value.Date, pme.Nom + " " + pme.Prenom);

                        }

                        #endregion

                    }
                    else
                    {
                        nomEntreprise = rw.NomEntreprise;
                        #region SujetMasculin
                        var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M", rw.NomEntreprise);
                        var patientMasculinEnfant = new List<AppCode.Patient>();
                        var patientMasculinAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientMasculin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientMasculinEnfant.Add(p);
                                }
                                else
                                {
                                    patientMasculinAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientMasculinEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientMasculinAdulte.Add(p);
                            }
                            else
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }
                        
                        foreach (var pma in patientMasculinAdulte)
                        {
                            totalCountMasculinAdulte += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatientConventionne
                               (dateTimePicker1.Value, dateTimePicker2.Value.Date, pma.Nom + " " + pma.Prenom);
                            counttMasculinAdulte += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatientConventionne
                                (dateTimePicker1.Value, dateTimePicker2.Value.Date, pma.Nom + " " + pma.Prenom);
 }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            totalCountMasculinEnfant += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatientConventionne
                               (dateTimePicker1.Value, dateTimePicker2.Value.Date, pme.Nom + " " + pme.Prenom);

                            countMasculinEnfant += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatientConventionne
                                (dateTimePicker1.Value, dateTimePicker2.Value.Date, pme.Nom + " " + pme.Prenom);
                        }

                        #endregion

                        #region SujetFeminin
                        var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F", rw.NomEntreprise);
                        var patientFemininEnfant = new List<AppCode.Patient>();
                        var patientFemininAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientFeminin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientFemininEnfant.Add(p);
                                }
                                else
                                {
                                    patientFemininAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientFemininEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientFemininAdulte.Add(p);
                            }
                            else
                            {
                                patientFemininEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientFemininAdulte)
                        {
                            counttFemininAdulte += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatientConventionne
                                (dateTimePicker1.Value, dateTimePicker2.Value.Date, pma.Nom + " " + pma.Prenom);

                            totalCountFemininAdulte += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatientConventionne
                             (dateTimePicker1.Value, dateTimePicker2.Value.Date, pma.Nom + " " + pma.Prenom);
                         }
                        foreach (var pme in patientFemininEnfant)
                        {
                            countFemininEnfant += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatientConventionne
                               (dateTimePicker1.Value, dateTimePicker2.Value.Date, pme.Nom + " " + pme.Prenom);

                            totalCountFemininEnfant += AppCode.ConnectionClassPharmacie.CompterLeNombreDePatientConventionne
                                (dateTimePicker1.Value, dateTimePicker2.Value.Date, pme.Nom + " " + pme.Prenom);
                         }

                        #endregion
                    }


                    var totalCountMasculin = countMasculinEnfant + counttMasculinAdulte;
                    var totalCountFeminin = countFemininEnfant + counttFemininAdulte;
                    var totalCountEnfantParConvention = countFemininEnfant + countMasculinEnfant;
                    var totalCountAdulteParConvention = counttFemininAdulte + counttMasculinAdulte;
                    var totalParSecteur = totalCountFeminin + totalCountMasculin;

                    totalGlobalMasculin += totalCountMasculin;
                    totalGlobalFeminin += totalCountFeminin;
                    totalGlobalEnfant += totalCountEnfantParConvention;
                    totalGlobalAdulte += totalCountAdulteParConvention;

                    dgvStatistique.Rows.Add(nomEntreprise, countMasculinEnfant, countFemininEnfant,
                        counttMasculinAdulte, counttFemininAdulte, totalCountMasculin, 
                        totalCountFeminin,totalCountEnfantParConvention,totalCountAdulteParConvention, totalParSecteur);
                }

                var totalGlobal = totalGlobalFeminin + totalGlobalMasculin;

                dgvStatistique.Rows.Add("TOTAUX", totalCountMasculinEnfant, totalCountFemininEnfant,
                    totalCountMasculinAdulte, totalCountFemininAdulte, totalGlobalMasculin, totalGlobalFeminin,
                    totalGlobalEnfant,totalGlobalAdulte, totalGlobal);

            }
            catch
            { }
        }

        #region 
        //void StatistiqueDesConsultations()
        //{
        //    try
        //    {
        //        dgvStatistique.CellBorderStyle = DataGridViewCellBorderStyle.RaisedVertical ;
        //        dgvStatistique.Columns.Clear();
        //        dgvStatistique.Columns.Add("SERVICES", "SERVICES ET CONSULTATIONS CURATIVES");
        //        dgvStatistique.Columns[0].Width = dgvStatistique.Width / 4-50;
        //        dgvStatistique.Columns.Add("enfants", "CONSULTATION ENFANTS ≤ 18 ans");
        //        dgvStatistique.Columns[1].Width = dgvStatistique.Width / 15;
        //        dgvStatistique.Columns.Add("enfants1", "");
        //        dgvStatistique.Columns[2].Width = dgvStatistique.Width / 16;
        //        dgvStatistique.Columns.Add("Adultes", "CONSULTATION ADULTES ≥ 18 ans");
        //        dgvStatistique.Columns[3].Width = dgvStatistique.Width / 15;
        //        dgvStatistique.Columns.Add("adultes1", "");
        //        dgvStatistique.Columns[4].Width = dgvStatistique.Width / 16;
        //        dgvStatistique.Columns.Add("AdultesPre", "CONSULTATION PRENATALE");
        //        dgvStatistique.Columns[4].Width = dgvStatistique.Width / 15;
        //        dgvStatistique.Columns.Add("AdultesPre", "");
        //        dgvStatistique.Columns[3].Width = dgvStatistique.Width / 16;
        //        dgvStatistique.Columns.Add("total", "TOTAL");
        //        dgvStatistique.Columns[5].Width = dgvStatistique.Width / 16;
        //        dgvStatistique.Columns.Add("total1", "");
        //        dgvStatistique.Columns[6].Width = dgvStatistique.Width / 16;
        //        dgvStatistique.Columns.Add("total2", "");
        //        dgvStatistique.Columns[7].Width = dgvStatistique.Width / 16;
        //        dgvStatistique.Columns.Add("total1", "");
        //        dgvStatistique.Columns[8].Width = dgvStatistique.Width / 16;
        //        dgvStatistique.Columns.Add("total1", "");
        //        dgvStatistique.Columns[9].Width = dgvStatistique.Width / 16;
        //        dgvStatistique.Columns.Add("total1", "");
        //        dgvStatistique.Columns[10].Width = dgvStatistique.Width / 16;

        //        dgvStatistique.Rows.Clear();
        //        dgvStatistique.Rows.Add("SEXE ", "M", "F", "M", "F", "F≤ 18 ans", "F ≥ 18 ans", "TOT M", "TOT F", "TOT ENF", "TOT ADLT","TOT PREN", "TOTAL");
        //        dgvStatistique.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
        //        var liste = AppCode.ConnectionClassClinique.ListeDesEntreprises();
        //        var entreprise = new AppCode.Entreprise(0, "", "", "", "", "", DateTime.Now, 0.0);
        //        liste.Add(entreprise);
                
        //        var totalCountFemininEnfant = 0;
        //        var totalCountFemininAdulte = 0;
        //        var totalCountMasculinEnfant = 0;
        //        var totalCountMasculinAdulte = 0;
        //        var totalCountPrenatalEnfant = 0;
        //        var totalCountPrenatalAdulte = 0;

        //        var totalGlobalFeminin = 0;
        //        var totalGlobalMasculin = 0;
        //        var totalGlobalEnfant = 0;
        //        var totalGlobalAdulte = 0;

        //        foreach (var rw in liste)
        //        {
        //            var nomEntreprise = "";
        //            var countConsultationFemininEnfant = 0;
        //            var countConsultationFemininAdulte = 0;
        //            var countConsultationMasculinEnfant = 0;
        //            var countConsultationMasculinAdulte = 0;
        //            var countConsultationPrenataleEnfant = 0;
        //            var countConsultationPrenataleAdulte = 0;

        //            if (string.IsNullOrEmpty(rw.NomEntreprise))
        //            {
        //                nomEntreprise = "COMMUNAUTE";
        //                #region SujetMasculin
        //                var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M");
        //                var patientMasculinEnfant = new List<AppCode.Patient>();
        //                var patientMasculinAdulte = new List<AppCode.Patient>();
        //                foreach (var p in listePatientMasculin)
        //                {
        //                    int an;
        //                    if (Int32.TryParse(p.An, out an))
        //                    {
        //                        if (an < 18)
        //                        {
        //                            patientMasculinEnfant.Add(p);
        //                        }
        //                        else
        //                        {
        //                            patientMasculinAdulte.Add(p);
        //                        }
        //                    }
        //                    else if (p.An.ToLower().Contains("enfant"))
        //                    {
        //                        patientMasculinEnfant.Add(p);
        //                    }
        //                    else if (p.An.ToLower().Contains("adulte"))
        //                    {
        //                        patientMasculinAdulte.Add(p);
        //                    }
        //                    else 
        //                    {
        //                        patientMasculinEnfant.Add(p);
        //                    }
        //                }
        //                foreach (var pma in patientMasculinAdulte)
        //                {
        //                    var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFacture(pma.NumeroPatient,
        //                       comboBox2.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
        //                    foreach (var cn in listeC)
        //                    {
        //                        if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
        //                        {
        //                            countConsultationPrenataleAdulte +=cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
        //                        {
        //                            countConsultationMasculinEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
        //                        {
        //                            countConsultationMasculinAdulte += cn.Quantite;
        //                        }
        //                    }
        //                }
        //                foreach (var pme in patientMasculinEnfant)
        //                {
        //                    var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFacture(pme.NumeroPatient,
        //                      comboBox2.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
        //                    foreach (var cn in listeC)
        //                    {
        //                        if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
        //                        {
        //                            countConsultationPrenataleEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
        //                        {
        //                            countConsultationMasculinEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
        //                        {
        //                            countConsultationMasculinAdulte += cn.Quantite;
        //                        }
        //                    }
        //                }

        //                #endregion

        //                #region SujetFeminin
        //                var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F");
        //                var patientFemininEnfant = new List<AppCode.Patient>();
        //                var patientFemininAdulte = new List<AppCode.Patient>();
        //                foreach (var p in listePatientFeminin)
        //                {
        //                    int an;
        //                    if (Int32.TryParse(p.An, out an))
        //                    {
        //                        if (an < 18)
        //                        {
        //                            patientFemininEnfant.Add(p);
        //                        }
        //                        else
        //                        {
        //                            patientFemininAdulte.Add(p);
        //                        }
        //                    }
        //                    else if (p.An.ToLower().Contains("enfant"))
        //                    {
        //                        patientFemininEnfant.Add(p);
        //                    }
        //                    else if (p.An.ToLower().Contains("adulte"))
        //                    {
        //                        patientFemininAdulte.Add(p);
        //                    }
        //                    else 
        //                    {
        //                        patientFemininEnfant.Add(p);
        //                    }
        //                }
        //                foreach (var pma in patientFemininAdulte)
        //                {
        //                    var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pma.NumeroPatient,comboBox2.Text,
        //                       dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
        //                    foreach (var cn in listeC)
        //                    {
        //                        if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
        //                        {
        //                            countConsultationPrenataleAdulte += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
        //                        {
        //                            countConsultationFemininEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
        //                        {
        //                            countConsultationFemininAdulte += cn.Quantite;
        //                        }
        //                    }
        //                }
        //                foreach (var pme in patientFemininEnfant)
        //                {
        //                    var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pme.NumeroPatient,comboBox2.Text,
        //                      dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
        //                    foreach (var cn in listeC)
        //                    {
        //                        if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
        //                        {
        //                            countConsultationPrenataleEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
        //                        {
        //                            countConsultationFemininEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
        //                        {
        //                            countConsultationFemininAdulte += cn.Quantite;
        //                        }
        //                    }
        //                }

        //                #endregion

        //            }
        //            else
        //            {
        //                nomEntreprise = rw.NomEntreprise;
        //                #region SujetMasculin
        //                var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M", rw.NomEntreprise);
        //                var patientMasculinEnfant = new List<AppCode.Patient>();
        //                var patientMasculinAdulte = new List<AppCode.Patient>();
        //                foreach (var p in listePatientMasculin)
        //                {
        //                    int an;
        //                    if (Int32.TryParse(p.An, out an))
        //                    {
        //                        if (an < 18)
        //                        {
        //                            patientMasculinEnfant.Add(p);
        //                        }
        //                        else
        //                        {
        //                            patientMasculinAdulte.Add(p);
        //                        }
        //                    }
        //                    else if (p.An.ToLower().Contains("enfant"))
        //                    {
        //                        patientMasculinEnfant.Add(p);
        //                    }
        //                    else if (p.An.ToLower().Contains("adulte"))
        //                    {
        //                        patientMasculinAdulte.Add(p);
        //                    }
        //                    else  
        //                    {
        //                        patientMasculinEnfant.Add(p);
        //                    }
        //                }
        //                foreach (var pma in patientMasculinAdulte)
        //                {
        //                    var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pma.NumeroPatient,
        //                     comboBox2.Text,  dateTimePicker1.Value, dateTimePicker2.Value.Date);
        //                    foreach (var cn in listeC)
        //                    {
        //                        if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
        //                        {
        //                            countConsultationPrenataleAdulte += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
        //                        {
        //                            countConsultationMasculinEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
        //                        {
        //                            countConsultationMasculinAdulte += cn.Quantite;
        //                        }
        //                    }
        //                }
        //                foreach (var pme in patientMasculinEnfant)
        //                {
        //                    var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pme.NumeroPatient,
        //                      comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value.Date);
        //                    foreach (var cn in listeC)
        //                    {
        //                        if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
        //                        {
        //                            countConsultationPrenataleEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
        //                        {
        //                            countConsultationMasculinEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
        //                        {
        //                            countConsultationMasculinAdulte += cn.Quantite;
        //                        }
        //                    }
        //                }

        //                #endregion

        //                #region SujetFeminin
        //                var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F",rw.NomEntreprise );
        //                var patientFemininEnfant = new List<AppCode.Patient>();
        //                var patientFemininAdulte = new List<AppCode.Patient>();
        //                foreach (var p in listePatientFeminin)
        //                {
        //                    int an;
        //                    if (Int32.TryParse(p.An, out an))
        //                    {
        //                        if (an < 18)
        //                        {
        //                            patientFemininEnfant.Add(p);
        //                        }
        //                        else
        //                        {
        //                            patientFemininAdulte.Add(p);
        //                        }
        //                    }
        //                    else if (p.An.ToLower().Contains("enfant"))
        //                    {
        //                        patientFemininEnfant.Add(p);
        //                    }
        //                    else if (p.An.ToLower().Contains("adulte"))
        //                    {
        //                        patientFemininAdulte.Add(p);
        //                    }
        //                    else 
        //                    {
        //                        patientMasculinEnfant.Add(p);
        //                    }
        //                }
        //                foreach (var pma in patientFemininAdulte)
        //                {
        //                    var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pma.NumeroPatient,
        //                      comboBox2.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
        //                    foreach (var cn in listeC)
        //                    {
        //                        if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
        //                        {
        //                            countConsultationPrenataleAdulte += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
        //                        {
        //                            countConsultationFemininEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
        //                        {
        //                            countConsultationFemininAdulte += cn.Quantite;
        //                        }
        //                    }
        //                }
        //                foreach (var pme in patientFemininEnfant)
        //                {
        //                    var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pme.NumeroPatient,
        //                     comboBox2.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
        //                    foreach (var cn in listeC)
        //                    {
        //                        if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
        //                        {
        //                            countConsultationPrenataleEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
        //                        {
        //                            countConsultationFemininEnfant += cn.Quantite;
        //                        }
        //                        else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
        //                        {
        //                            countConsultationFemininAdulte += cn.Quantite;
        //                        }
        //                    }
        //                }
        //                #endregion
        //            }


        //            var totalCountMasculin = countConsultationMasculinEnfant + countConsultationMasculinAdulte;
        //            var totalCountFeminin = countConsultationFemininEnfant + countConsultationFemininAdulte;
        //            var totalCountEnfantParConvention = countConsultationFemininEnfant + countConsultationMasculinEnfant;
        //            var totalCountAdulteParConvention = countConsultationFemininAdulte + countConsultationMasculinAdulte;
        //            var totalCountPrenatal = countConsultationPrenataleAdulte + countConsultationPrenataleEnfant;
        //            var totalParSecteur = totalCountFeminin + totalCountMasculin+totalCountPrenatal;


        //            totalCountMasculinEnfant += countConsultationMasculinEnfant;
        //            totalCountFemininEnfant += countConsultationFemininEnfant;
        //            totalCountMasculinAdulte += countConsultationMasculinAdulte;
        //            totalCountFemininAdulte += countConsultationFemininAdulte;
        //            totalCountPrenatalEnfant += countConsultationPrenataleEnfant;
        //            totalCountPrenatalAdulte += countConsultationPrenataleAdulte;

        //            totalGlobalEnfant += totalCountEnfantParConvention;
        //            totalGlobalAdulte += totalCountAdulteParConvention;
        //            totalGlobalMasculin += totalCountMasculin;
        //            totalGlobalFeminin += totalCountFeminin;
                    
        //            dgvStatistique.Rows.Add(nomEntreprise, countConsultationMasculinEnfant, 
        //                countConsultationFemininEnfant,countConsultationMasculinAdulte, 
        //                countConsultationFemininAdulte,countConsultationPrenataleEnfant,
        //               countConsultationPrenataleAdulte, totalCountMasculin,
        //                totalCountFeminin, totalCountEnfantParConvention, 
        //                totalCountAdulteParConvention,totalCountPrenatal, totalParSecteur);
        //        }

        //        var totalPrenatal = totalCountPrenatalEnfant + totalCountPrenatalAdulte;
        //        var totalGlobal = totalGlobalFeminin + totalGlobalMasculin+totalPrenatal;
        //        dgvStatistique.Rows.Add("TOTAUX", totalCountMasculinEnfant, totalCountFemininEnfant,
        //            totalCountMasculinAdulte, totalCountFemininAdulte,totalCountPrenatalEnfant,totalCountPrenatalAdulte,
        //            totalGlobalMasculin, totalGlobalFeminin,totalGlobalEnfant, totalGlobalAdulte,totalPrenatal, totalGlobal);
        //        dgvStatistique.Rows[dgvStatistique.Rows.Count-1].DefaultCellStyle.BackColor = Color.GreenYellow;
        //    }
        //    catch(Exception ex)
        //    {
        //        MonMessageBox.ShowBox("", ex);
        //    }
        //}
#endregion
        void StatistiqueDuLaboratoire()
        {
            try
            {
                dgvStatistique.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dgvStatistique.Columns.Clear();
                dgvStatistique.Columns.Add("SERVICES", "SERVICES ET LABORATOIRE");
                dgvStatistique.Columns[0].Width = dgvStatistique.Width / 4 - 50;
                dgvStatistique.Columns.Add("enfants1", "ENFANTS ≤ 18 ans");
                dgvStatistique.Columns[1].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("enfants2", "");
                dgvStatistique.Columns[2].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("enfants3", "");
                dgvStatistique.Columns[3].Width = dgvStatistique.Width / 20;
                dgvStatistique.Columns.Add("enfants4", "");
                dgvStatistique.Columns[4].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("adultes1", "ADULTES ≥ 18 ans");
                dgvStatistique.Columns[5].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("adultes2", "");
                dgvStatistique.Columns[6].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("adultes3", "");
                dgvStatistique.Columns[7].Width = dgvStatistique.Width / 20;
                dgvStatistique.Columns.Add("adultes4", "");
                dgvStatistique.Columns[8].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total", "TOTAL");
                dgvStatistique.Columns[9].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[10].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total2", "");
                dgvStatistique.Columns[11].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[12].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[13].Width = dgvStatistique.Width / 20;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[14].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[15].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[16].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[17].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[17].Width = dgvStatistique.Width / 22;

                dgvStatistique.Rows.Clear();
                dgvStatistique.Rows.Add(" ", "Patient", "", "Analyse", "", "Patient", "", "Analyse", "", "", "", "", "", "");
                dgvStatistique.Rows.Add("SEXE ", "M", "F", "Nbr", "Nbr", "M", "F", "Nbr", "Nbr", "To enf", "Nb anl", "To adl","nb anl","Tot M","nb anl","Tot F", "Nb anl", "Total", "to anl");
                dgvStatistique.Rows[0].DefaultCellStyle.BackColor = Color.Red;
                dgvStatistique.Rows[1].DefaultCellStyle.BackColor = Color.Yellow;
                var liste = AppCode.ConnectionClassClinique.ListeDesEntreprises();
                var entreprise = new AppCode.Entreprise(0, "", "", "", "", "", DateTime.Now, 0.0,"", false,0);
                liste.Add(entreprise);

                var GlobalCountFemininEnfant = 0;
                var GlobalCountMasculinEnfant = 0;
                var GlobalCountFemininAdulte = 0;
                var GlobalCountMasculinAdulte = 0;
                var GlobalDetailsCountFemininEnfant = 0;
                var GlobalDetailsCountMasculinEnfant = 0;
                var GlobalDetailsCountFemininAdulte = 0;
                var GlobalDetailsCountMasculinAdulte = 0;
                var totalP = 0;
                foreach (var rw in liste)
                {
                    #region
                    var nomEntreprise = "";
                    var CountActesFemininEnfant = 0;
                    var CountActesFemininAdulte = 0;
                    var CountActesMasculinEnfant = 0;
                    var CountActesMasculinAdulte = 0;
                    

                    var CountDetailsActessFemininEnfant = 0;
                    var CountDetailsActessFemininAdulte = 0;
                    var CountDetailsActessMasculinEnfant = 0;
                    var CountDetailsActessMasculinAdulte = 0;
                    #endregion

                    if (string.IsNullOrWhiteSpace(rw.NomEntreprise))
                    {
                        nomEntreprise = "COMMUNAUTE";
                        #region SujetMasculin
                        var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M");
                        var patientMasculinEnfant = new List<AppCode.Patient>();
                        var patientMasculinAdulte = new List<AppCode.Patient>();
                        totalP += listePatientMasculin.Count();
                        foreach (var p in listePatientMasculin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientMasculinEnfant.Add(p);
                                }
                                else
                                {
                                    patientMasculinAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientMasculinEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientMasculinAdulte.Add(p);
                            }
                            else
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientMasculinAdulte)
                        {
                            CountDetailsActessMasculinAdulte += AppCode.ConnectionClassClinique.CountDetailsActes(pma.NumeroPatient,
                               comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            if (CountDetailsActessMasculinAdulte > 0)
                                CountActesMasculinAdulte += AppCode.ConnectionClassClinique.CountActes(pma.NumeroPatient,
                                   dateTimePicker1.Value, dateTimePicker2.Value.Date, comboBox2.Text);
                        }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            CountDetailsActessMasculinEnfant += AppCode.ConnectionClassClinique.CountDetailsActes(pme.NumeroPatient, comboBox2.Text,
                            dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            if (CountDetailsActessMasculinEnfant > 0)
                                CountActesMasculinEnfant += AppCode.ConnectionClassClinique.CountActes(pme.NumeroPatient,
                                    dateTimePicker1.Value, dateTimePicker2.Value.Date, comboBox2.Text);
                        }

                        #endregion

                        #region SujetFeminin
                        var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F");
                        var patientFemininEnfant = new List<AppCode.Patient>();
                        var patientFemininAdulte = new List<AppCode.Patient>();
                        totalP += listePatientFeminin.Count();
                        foreach (var p in listePatientFeminin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientFemininEnfant.Add(p);
                                }
                                else
                                {
                                    patientFemininAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientFemininEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientFemininAdulte.Add(p);
                            }
                            else { patientFemininEnfant.Add(p); }
                        }
                        foreach (var pma in patientFemininAdulte)
                        {

                            CountDetailsActessFemininAdulte += AppCode.ConnectionClassClinique.CountDetailsActes(pma.NumeroPatient, comboBox2.Text,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);

                            CountActesFemininAdulte += AppCode.ConnectionClassClinique.CountActes(pma.NumeroPatient,
                               dateTimePicker1.Value, dateTimePicker2.Value.Date, comboBox2.Text);
                        }
                        foreach (var pme in patientFemininEnfant)
                        {
                            CountDetailsActessFemininEnfant += AppCode.ConnectionClassClinique.CountDetailsActes(pme.NumeroPatient, comboBox2.Text,
                             dateTimePicker1.Value, dateTimePicker2.Value.Date);

                            CountActesFemininEnfant += AppCode.ConnectionClassClinique.CountActes(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date, comboBox2.Text);
                        }
                        #endregion

                    }
                    else
                    {
                        nomEntreprise = rw.NomEntreprise;
                        #region SujetMasculin
                        var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M", rw.NomEntreprise);
                        var patientMasculinEnfant = new List<AppCode.Patient>();
                        var patientMasculinAdulte = new List<AppCode.Patient>();
                        totalP += listePatientMasculin.Count();
                        foreach (var p in listePatientMasculin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientMasculinEnfant.Add(p);
                                }
                                else
                                {
                                    patientMasculinAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientMasculinEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientMasculinAdulte.Add(p);
                            }
                            else { patientMasculinEnfant.Add(p); }
                        }
                        foreach (var pma in patientMasculinAdulte)
                        {
                            CountDetailsActessMasculinAdulte += AppCode.ConnectionClassClinique.CountDetailsActesDesConventionnes(pma.NumeroPatient, comboBox2.Text,
                              dateTimePicker1.Value, dateTimePicker2.Value.Date);

                            CountActesMasculinAdulte += AppCode.ConnectionClassClinique.CountActesDesConventionnes(pma.NumeroPatient,
                               dateTimePicker1.Value, dateTimePicker2.Value.Date, comboBox2.Text);


                        }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            CountDetailsActessMasculinEnfant += AppCode.ConnectionClassClinique.CountDetailsActesDesConventionnes(pme.NumeroPatient, comboBox2.Text,
    dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            if (CountDetailsActessMasculinEnfant > 0)
                                CountActesMasculinEnfant += AppCode.ConnectionClassClinique.CountActesDesConventionnes(pme.NumeroPatient,
                                    dateTimePicker1.Value, dateTimePicker2.Value.Date, comboBox2.Text);

                        }
                        #endregion

                        #region SujetFeminin
                        var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F", rw.NomEntreprise);
                        var patientFemininEnfant = new List<AppCode.Patient>();
                        var patientFemininAdulte = new List<AppCode.Patient>();
                        totalP += listePatientFeminin.Count();
                        foreach (var p in listePatientFeminin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientFemininEnfant.Add(p);
                                }
                                else
                                {
                                    patientFemininAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientFemininEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientFemininAdulte.Add(p);
                            }
                            else
                            {
                                patientFemininEnfant.Add(p);
                            }
                        }

                        foreach (var pma in patientFemininAdulte)
                        {
                            CountDetailsActessFemininAdulte += AppCode.ConnectionClassClinique.CountDetailsActesDesConventionnes(pma.NumeroPatient,
                               comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value.Date);

                            CountActesFemininAdulte += AppCode.ConnectionClassClinique.CountActesDesConventionnes(pma.NumeroPatient,
                               dateTimePicker1.Value, dateTimePicker2.Value.Date, comboBox2.Text);
                        }
                        foreach (var pme in patientFemininEnfant)
                        {
                            var numeroAnalyse = AppCode.ConnectionClassClinique.ListeDesAnalyses("echographie");
                            CountDetailsActessFemininEnfant += AppCode.ConnectionClassClinique.CountDetailsActesDesConventionnes(pme.NumeroPatient,
                              comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            CountActesFemininEnfant += AppCode.ConnectionClassClinique.CountActesDesConventionnes(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date, comboBox2.Text);

                        }
                        #endregion
                    }

                    #region
                    var totalAnalyseMasculinParConvention = CountActesMasculinEnfant + CountActesMasculinAdulte;
                    var totalAnalyseFemininParConvention = CountActesFemininEnfant + CountActesFemininAdulte;
                    var totalDetailsAnalyseMasculinParConvention = CountDetailsActessMasculinAdulte+CountDetailsActessMasculinEnfant;
                    var totalDetailsAnalyseFemininParConvention = CountDetailsActessFemininAdulte+CountDetailsActessFemininEnfant;
                    var totalAnalyseMasculinFemininParConvention = totalAnalyseFemininParConvention + totalAnalyseMasculinParConvention ;
                    var totalDetailsAnalyseMasculinFemininParConvention = totalDetailsAnalyseFemininParConvention + totalDetailsAnalyseMasculinParConvention;
                    var totalCountEnfantParConvention = CountActesFemininEnfant + CountActesMasculinEnfant;
                    var totalCountAdulteParConvention = CountActesFemininAdulte + CountActesMasculinAdulte;
                    var totalCountEnfantDetailleParConvention = CountDetailsActessFemininEnfant + CountDetailsActessMasculinEnfant;
                    var totalCountAdulteDetailleParConvention = CountDetailsActessFemininAdulte  + CountDetailsActessMasculinAdulte;

                    GlobalCountMasculinEnfant += CountActesMasculinEnfant;
                    GlobalCountFemininEnfant += CountActesFemininEnfant;
                    GlobalCountMasculinAdulte += CountActesMasculinAdulte;
                    GlobalCountFemininAdulte += CountActesFemininAdulte;

                    GlobalDetailsCountFemininAdulte += CountDetailsActessFemininAdulte;
                    GlobalDetailsCountFemininEnfant += CountDetailsActessFemininEnfant;
                    GlobalDetailsCountMasculinAdulte += CountDetailsActessMasculinAdulte;
                    GlobalDetailsCountMasculinEnfant += CountDetailsActessMasculinEnfant;
              
                    dgvStatistique.Rows.Add(nomEntreprise, CountActesMasculinEnfant, CountActesFemininEnfant,
                      CountDetailsActessMasculinEnfant, CountDetailsActessFemininEnfant, CountActesMasculinAdulte,
                      CountActesFemininAdulte, CountDetailsActessMasculinAdulte, CountDetailsActessFemininAdulte,
                      totalCountEnfantParConvention, totalCountEnfantDetailleParConvention,
                      totalCountAdulteParConvention, totalCountAdulteDetailleParConvention,
                      totalAnalyseMasculinParConvention, totalDetailsAnalyseMasculinParConvention,
                      totalAnalyseFemininParConvention,totalDetailsAnalyseFemininParConvention,
                      totalAnalyseMasculinFemininParConvention,totalDetailsAnalyseMasculinFemininParConvention);
                    #endregion
                }

                var totalGlobalEnfant = GlobalCountFemininEnfant + GlobalCountMasculinEnfant;
                var totalGlobalAdulte = GlobalCountFemininAdulte + GlobalCountMasculinAdulte;
                var totalGlobalDetailsEnfant = GlobalDetailsCountFemininEnfant + GlobalDetailsCountMasculinEnfant;
                var totalGlobalDetailsAdulte = GlobalDetailsCountMasculinAdulte + GlobalDetailsCountFemininAdulte;
                var totalGlobalMasculin = GlobalCountMasculinAdulte + GlobalCountMasculinEnfant;
                var totalGlobalFeminin = GlobalCountFemininAdulte + GlobalCountFemininEnfant;
                var totalGlobalDetailsMasculin = GlobalDetailsCountMasculinAdulte + GlobalDetailsCountMasculinEnfant;
                var totalGlobalDetailsFeminin = GlobalDetailsCountFemininEnfant + GlobalDetailsCountFemininAdulte;

                var totalGlobal = totalGlobalFeminin + totalGlobalMasculin;
                var totalGlobalDetail = totalGlobalDetailsFeminin + totalGlobalDetailsMasculin;

                dgvStatistique.Rows.Add("TOTAUX "+totalP, GlobalCountMasculinEnfant, GlobalCountFemininEnfant,
                    GlobalDetailsCountMasculinEnfant,GlobalDetailsCountFemininEnfant,
                    GlobalCountMasculinAdulte, GlobalCountFemininAdulte,
                    GlobalDetailsCountMasculinAdulte, GlobalDetailsCountFemininAdulte,
                    totalGlobalEnfant, totalGlobalDetailsEnfant,
                    totalGlobalAdulte,totalGlobalDetailsAdulte ,
                    totalGlobalMasculin, totalGlobalDetailsMasculin,
                    totalGlobalFeminin, totalGlobalDetailsFeminin ,totalGlobal,totalGlobalDetail);
                dgvStatistique.Rows[dgvStatistique.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
            }
            catch
            { }
        }
        void StatistiqueDesConsultations()
        {
            try
            {
                dgvStatistique.CellBorderStyle = DataGridViewCellBorderStyle.RaisedVertical;
                dgvStatistique.Columns.Clear();
                dgvStatistique.Columns.Add("SERVICES", "SERVICES ET CONSULTATIONS CURATIVES");
                dgvStatistique.Columns[0].Width = dgvStatistique.Width / 4 - 50;
                dgvStatistique.Columns.Add("enfants", "CONSULTATION ENFANTS ≤18ans");
                dgvStatistique.Columns[1].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("enfants1", "");
                dgvStatistique.Columns[2].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("Adultes", "CONSULTATION ADULTES ≥18ans");
                dgvStatistique.Columns[3].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("adultes1", "");
                dgvStatistique.Columns[4].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("AdultesPre", "CONSULTATION PRENATALE");
                dgvStatistique.Columns[5].Width = dgvStatistique.Width / 17;
               dgvStatistique.Columns.Add("AdultesPre", "");
                dgvStatistique.Columns[6].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("ConSpeci", "CONSULTATION SPECIALISEE");
                dgvStatistique.Columns[7].Width = dgvStatistique.Width / 18;
                dgvStatistique.Columns.Add("ConSpeciAF", "");
                dgvStatistique.Columns[8].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("ConSpeciEM", "");
                dgvStatistique.Columns[9].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("ConSpeciEF", "");
                dgvStatistique.Columns[10].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total", "TOTAL");
                dgvStatistique.Columns[11].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[12].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total2", "");
                dgvStatistique.Columns[13].Width = dgvStatistique.Width / 20;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[14].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[15].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[16].Width = dgvStatistique.Width / 22;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[17].Width = dgvStatistique.Width / 22;

                dgvStatistique.Rows.Clear();
                dgvStatistique.Rows.Add("SEXE ", "M", "F", "M", "F", "F≤18an", "F≥18an", "M≤18an", "F≤18an", "M≥18a", "F≥18an", "TOT M", "TOT F", "TOT ENF", "TOT ADLT", "TOT PREN","TOT SPEC", "TOTAL");
                dgvStatistique.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                var liste = AppCode.ConnectionClassClinique.ListeDesEntreprises();
                var entreprise = new AppCode.Entreprise(0, "", "", "", "", "", DateTime.Now, 0.0,"",false,0);
                liste.Add(entreprise);

                var totalCountFemininEnfant = 0;
                var totalCountFemininAdulte = 0;
                var totalCountMasculinEnfant = 0;
                var totalCountMasculinAdulte = 0;
                var totalCountPrenatalEnfant = 0;
                var totalCountPrenatalAdulte = 0;
                var totalCountSpecialiseeMasculinAdulte = 0;
                var totalCountSpecialiseeFemininAdulte = 0;
                var totalCountSpecialiseeMasculinEnfant = 0;
                var totalCountSpecialiseeFemininEnfant = 0;

                var totalGlobalFeminin = 0;
                var totalGlobalMasculin = 0;
                var totalGlobalEnfant = 0;
                var totalGlobalAdulte = 0;

                foreach (var rw in liste)
                {
                    var nomEntreprise = "";
                    var countConsultationFemininEnfant = 0;
                    var countConsultationFemininAdulte = 0;
                    var countConsultationMasculinEnfant = 0;
                    var countConsultationMasculinAdulte = 0;
                    var countConsultationPrenataleEnfant = 0;
                    var countConsultationPrenataleAdulte = 0;
                    var countConsultationSpecialMasculinEnfant = 0;
                    var countConsultationSpecialFemininEnfant = 0;
                    var countConsultationSpecialMasculinAdulte = 0;
                    var countConsultationSpecialFemininAdulte = 0;

                    if (string.IsNullOrEmpty(rw.NomEntreprise))
                    {
                        nomEntreprise = "COMMUNAUTE";
                        var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFacture("M", dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                        foreach (var cn in listeC)
                        {
                            if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                            {
                                countConsultationPrenataleAdulte += cn.Quantite;
                            }
                            else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                            {
                                countConsultationMasculinEnfant += cn.Quantite;
                            }
                            else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                            {
                                countConsultationMasculinAdulte += cn.Quantite;
                            }
                        }
                        var listeF = AppCode.ConnectionClassClinique.ListeConsultationsDansFacture("F", dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                        foreach (var cn in listeF)
                        {
                            if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                            {
                                countConsultationPrenataleAdulte += cn.Quantite;
                            }
                            else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                            {
                                countConsultationFemininEnfant += cn.Quantite;
                            }
                            else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                            {
                                countConsultationFemininAdulte += cn.Quantite;
                            }
                        }
                        #region SujetMasculin
                        var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M");
                        var patientMasculinEnfant = new List<AppCode.Patient>();
                        var patientMasculinAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientMasculin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientMasculinEnfant.Add(p);
                                }
                                else
                                {
                                    patientMasculinAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientMasculinEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientMasculinAdulte.Add(p);
                            }
                            else
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientMasculinAdulte)
                        {
                            var listeCpma = AppCode.ConnectionClassClinique.CountConsultation("", pma.NumeroPatient,
                                dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listeCpma)
                            {
                                countConsultationSpecialMasculinAdulte += cn.NumeroConsultation;
                            }
                        }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            var listeCpme = AppCode.ConnectionClassClinique.CountConsultation("", pme.NumeroPatient,
                               dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listeCpme)
                            {
                                countConsultationSpecialMasculinEnfant += cn.NumeroConsultation;
                            }
                        }
                    

                    #endregion

                    #region SujetFeminin
                    var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F");
                    var patientFemininEnfant = new List<AppCode.Patient>();
                    var patientFemininAdulte = new List<AppCode.Patient>();
                    foreach (var p in listePatientFeminin)
                    {
                        int an;
                        if (Int32.TryParse(p.An, out an))
                        {
                            if (an < 18)
                            {
                                patientFemininEnfant.Add(p);
                            }
                            else
                            {
                                patientFemininAdulte.Add(p);
                            }
                        }
                        else if (p.An.ToLower().Contains("enfant"))
                        {
                            patientFemininEnfant.Add(p);
                        }
                        else if (p.An.ToLower().Contains("adulte"))
                        {
                            patientFemininAdulte.Add(p);
                        }
                        else
                        {
                            patientFemininEnfant.Add(p);
                        }
                    }
                    foreach (var pma in patientFemininAdulte)
                    {
                        var listeCpma = AppCode.ConnectionClassClinique.CountConsultation("", pma.NumeroPatient,
                              dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                        foreach (var cn in listeCpma)
                        {
                            countConsultationSpecialFemininAdulte += cn.NumeroConsultation;
                        }
                    }
                    foreach (var pme in patientFemininEnfant)
                    {
                        var listeCpme = AppCode.ConnectionClassClinique.CountConsultation("", pme.NumeroPatient,
                            dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                        foreach (var cn in listeCpme)
                        {
                            countConsultationSpecialFemininEnfant += cn.NumeroConsultation;
                        }
                    }
                
                    #endregion

                }
                    else
                    {
                        nomEntreprise = rw.NomEntreprise;
                        #region SujetMasculin
                        var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M", rw.NomEntreprise);
                        var patientMasculinEnfant = new List<AppCode.Patient>();
                        var patientMasculinAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientMasculin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientMasculinEnfant.Add(p);
                                }
                                else
                                {
                                    patientMasculinAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientMasculinEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientMasculinAdulte.Add(p);
                            }
                            else
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientMasculinAdulte)
                        {
                            var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pma.NumeroPatient,
                             comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            foreach (var cn in listeC)
                            {
                                if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                                {
                                    countConsultationPrenataleAdulte += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                                {
                                    countConsultationMasculinEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                                {
                                    countConsultationMasculinAdulte += cn.Quantite;
                                }
                                else
                                {
                                    countConsultationSpecialMasculinAdulte += cn.Quantite;
                                }
                            }
                        }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pme.NumeroPatient,
                              comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            foreach (var cn in listeC)
                            {
                                if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                                {
                                    countConsultationPrenataleEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                                {
                                    countConsultationMasculinEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                                {
                                    countConsultationMasculinAdulte += cn.Quantite;
                                }
                                else
                                {
                                    countConsultationSpecialMasculinEnfant += cn.Quantite;
                                }
                            }
                        }

                        #endregion

                        #region SujetFeminin
                        var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F", rw.NomEntreprise);
                        var patientFemininEnfant = new List<AppCode.Patient>();
                        var patientFemininAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientFeminin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientFemininEnfant.Add(p);
                                }
                                else
                                {
                                    patientFemininAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientFemininEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientFemininAdulte.Add(p);
                            }
                            else
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientFemininAdulte)
                        {
                            var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pma.NumeroPatient,
                              comboBox2.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listeC)
                            {
                                if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                                {
                                    countConsultationPrenataleAdulte += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                                {
                                    countConsultationFemininEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                                {
                                    countConsultationFemininAdulte += cn.Quantite;
                                }
                                else
                                {
                                    countConsultationSpecialFemininAdulte += cn.Quantite;
                                }
                            }
                        }
                        foreach (var pme in patientFemininEnfant)
                        {
                            var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pme.NumeroPatient,
                             comboBox2.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listeC)
                            {
                                if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                                {
                                    countConsultationPrenataleEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                                {
                                    countConsultationFemininEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                                {
                                    countConsultationFemininAdulte += cn.Quantite;
                                }
                                else
                                {
                                    countConsultationSpecialFemininEnfant += cn.Quantite;
                                }
                            }
                        }
                        #endregion
                    }
                    
                    var totalCountMasculin = countConsultationMasculinEnfant + countConsultationMasculinAdulte+countConsultationSpecialMasculinAdulte+countConsultationSpecialMasculinEnfant;
                    var totalCountFeminin = countConsultationFemininEnfant + countConsultationFemininAdulte+countConsultationSpecialFemininEnfant+countConsultationSpecialFemininAdulte;
                    var totalCountEnfantParConvention = countConsultationFemininEnfant + countConsultationMasculinEnfant+countConsultationSpecialFemininEnfant+countConsultationSpecialMasculinEnfant;
                    var totalCountAdulteParConvention = countConsultationFemininAdulte + countConsultationMasculinAdulte+countConsultationSpecialMasculinAdulte+countConsultationSpecialFemininAdulte;
                    var totalCountPrenatal = countConsultationPrenataleAdulte + countConsultationPrenataleEnfant;
                    var totalCountSpecialise = countConsultationSpecialFemininAdulte + countConsultationSpecialFemininEnfant + countConsultationSpecialMasculinAdulte + countConsultationSpecialMasculinEnfant;
                    var totalParSecteur = totalCountFeminin + totalCountMasculin + totalCountPrenatal+ totalCountSpecialise;


                    totalCountMasculinEnfant += countConsultationMasculinEnfant;
                    totalCountFemininEnfant += countConsultationFemininEnfant;
                    totalCountMasculinAdulte += countConsultationMasculinAdulte;
                    totalCountFemininAdulte += countConsultationFemininAdulte;
                    totalCountPrenatalEnfant += countConsultationPrenataleEnfant;
                    totalCountPrenatalAdulte += countConsultationPrenataleAdulte;
                     totalCountSpecialiseeMasculinAdulte += countConsultationSpecialMasculinAdulte;
                     totalCountSpecialiseeFemininAdulte += countConsultationSpecialFemininAdulte;
                     totalCountSpecialiseeMasculinEnfant += countConsultationSpecialMasculinEnfant;
                     totalCountSpecialiseeFemininEnfant += countConsultationSpecialFemininEnfant;

                    totalGlobalEnfant += totalCountEnfantParConvention;
                    totalGlobalAdulte += totalCountAdulteParConvention;
                    totalGlobalMasculin += totalCountMasculin;
                    totalGlobalFeminin += totalCountFeminin;

                    dgvStatistique.Rows.Add(nomEntreprise, countConsultationMasculinEnfant,
                        countConsultationFemininEnfant, countConsultationMasculinAdulte,
                        countConsultationFemininAdulte, countConsultationPrenataleEnfant,
                       countConsultationPrenataleAdulte,countConsultationSpecialMasculinEnfant,
                      countConsultationSpecialFemininEnfant,countConsultationSpecialMasculinAdulte,
                     countConsultationSpecialFemininAdulte, totalCountMasculin,
                        totalCountFeminin, totalCountEnfantParConvention,
                        totalCountAdulteParConvention, totalCountPrenatal, totalCountSpecialise, totalParSecteur);
                }

                var totalPrenatal = totalCountPrenatalEnfant + totalCountPrenatalAdulte;
                var totalSpecialisee = totalCountSpecialiseeFemininAdulte + totalCountSpecialiseeFemininEnfant +
                    totalCountSpecialiseeMasculinAdulte + totalCountSpecialiseeMasculinEnfant;
                var totalGlobal = totalGlobalFeminin + totalGlobalMasculin + totalPrenatal;
                dgvStatistique.Rows.Add("TOTAUX", totalCountMasculinEnfant, totalCountFemininEnfant,
                    totalCountMasculinAdulte, totalCountFemininAdulte, totalCountPrenatalEnfant, totalCountPrenatalAdulte,
                   totalCountSpecialiseeMasculinEnfant,totalCountSpecialiseeFemininEnfant,totalCountSpecialiseeMasculinAdulte,
                  totalCountSpecialiseeFemininAdulte, totalGlobalMasculin, totalGlobalFeminin, totalGlobalEnfant, totalGlobalAdulte, totalPrenatal,totalSpecialisee, totalGlobal);
                dgvStatistique.Rows[dgvStatistique.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }
        void StatistiqueDesConsultationsSpecialisees()
        {
            try
            {
                dgvStatistique.CellBorderStyle = DataGridViewCellBorderStyle.RaisedVertical;
                dgvStatistique.Columns.Clear();
                dgvStatistique.Columns.Add("SERVICES", "SERVICES ET CONSULTATIONS SPECIALISEES");
                dgvStatistique.Columns[0].Width = dgvStatistique.Width / 4 - 50;
                dgvStatistique.Columns.Add("enfants", "CONSULTATION ENFANTS ≤ 18 ans");
                dgvStatistique.Columns[1].Width = dgvStatistique.Width / 15;
                dgvStatistique.Columns.Add("enfants1", "");
                dgvStatistique.Columns[2].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("Adultes", "CONSULTATION ADULTES ≥ 18 ans");
                dgvStatistique.Columns[3].Width = dgvStatistique.Width / 15;
                dgvStatistique.Columns.Add("adultes1", "");
                dgvStatistique.Columns[4].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("AdultesPre", "CONSULTATION PRENATALE");
                dgvStatistique.Columns[4].Width = dgvStatistique.Width / 15;
                dgvStatistique.Columns.Add("AdultesPre", "");
                dgvStatistique.Columns[3].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("total", "TOTAL");
                dgvStatistique.Columns[5].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[6].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("total2", "");
                dgvStatistique.Columns[7].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[8].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[9].Width = dgvStatistique.Width / 16;
                dgvStatistique.Columns.Add("total1", "");
                dgvStatistique.Columns[10].Width = dgvStatistique.Width / 16;

                dgvStatistique.Rows.Clear();
                dgvStatistique.Rows.Add("SEXE ", "M", "F", "M", "F", "F≤ 18 ans", "F ≥ 18 ans", "TOT M", "TOT F", "TOT ENF", "TOT ADLT", "TOT PREN", "TOTAL");
                dgvStatistique.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                var liste = AppCode.ConnectionClassClinique.ListeDesEntreprises();
                var entreprise = new AppCode.Entreprise(0, "", "", "", "", "", DateTime.Now, 0.0,"",false,0);
                liste.Add(entreprise);

                var totalCountFemininEnfant = 0;
                var totalCountFemininAdulte = 0;
                var totalCountMasculinEnfant = 0;
                var totalCountMasculinAdulte = 0;
                var totalCountPrenatalEnfant = 0;
                var totalCountPrenatalAdulte = 0;
                var totalCountSpecFemininEnfant = 0;
                var totalCountSpecFemininAdulte = 0;
                var totalCountSpecMasculinEnfant = 0;
                var totalCountSpecMasculinAdulte = 0;

                var totalGlobalFeminin = 0;
                var totalGlobalMasculin = 0;
                var totalGlobalEnfant = 0;
                var totalGlobalAdulte = 0;

                foreach (var rw in liste)
                {
                    var nomEntreprise = "";
                    var countConsultationFemininEnfant = 0;
                    var countConsultationFemininAdulte = 0;
                    var countConsultationMasculinEnfant = 0;
                    var countConsultationMasculinAdulte = 0;
                    var countConsultationPrenataleEnfant = 0;
                    var countConsultationPrenataleAdulte = 0;

                    var countConsultationSpecialiseeMasculinEnfant = 0;
                    var countConsultationSpecialiseeMasculinAdulte = 0;
                    var countConsultationSpecialiseeFemininEnfant = 0;
                    var countConsultationSpecialiseeFemininAdulte = 0;

                    if (string.IsNullOrEmpty(rw.NomEntreprise))
                    {
                        nomEntreprise = "COMMUNAUTE";
                        var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFacture("M", dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                        foreach (var cn in listeC)
                        {
                            if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                            {
                                countConsultationPrenataleAdulte += cn.Quantite;
                            }
                            else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                            {
                                countConsultationMasculinEnfant += cn.Quantite;
                            }
                            else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                            {
                                countConsultationMasculinAdulte += cn.Quantite;
                            }
                        }
                        var listeF = AppCode.ConnectionClassClinique.ListeConsultationsDansFacture("F", dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                        foreach (var cn in listeF)
                        {
                            if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                            {
                                countConsultationPrenataleAdulte += cn.Quantite;
                            }
                            else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                            {
                                countConsultationFemininEnfant += cn.Quantite;
                            }
                            else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                            {
                                countConsultationFemininAdulte += cn.Quantite;
                            }
                        }
                        #region SujetMasculin
                        var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M");
                        var patientMasculinEnfant = new List<AppCode.Patient>();
                        var patientMasculinAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientMasculin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientMasculinEnfant.Add(p);
                                }
                                else
                                {
                                    patientMasculinAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientMasculinEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientMasculinAdulte.Add(p);
                            }
                            else
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientMasculinAdulte)
                        {
                            var listeMSpe = AppCode.ConnectionClassClinique.CountConsultation("", pma.NumeroPatient,
                                dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listeMSpe)
                            {
                                    countConsultationSpecialiseeMasculinAdulte += cn.NumeroConsultation;
                            }
                        }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            var listePme = AppCode.ConnectionClassClinique.CountConsultation("", pme.NumeroPatient,
                               dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listePme)
                            {
                                countConsultationSpecialiseeMasculinEnfant += cn.NumeroConsultation;                              
                            }
                        }

                        #endregion

                        #region SujetFeminin
                        var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F");
                        var patientFemininEnfant = new List<AppCode.Patient>();
                        var patientFemininAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientFeminin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientFemininEnfant.Add(p);
                                }
                                else
                                {
                                    patientFemininAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientFemininEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientFemininAdulte.Add(p);
                            }
                            else
                            {
                                patientFemininEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientFemininAdulte)
                        {
                            var listePma = AppCode.ConnectionClassClinique.CountConsultation("",pma.NumeroPatient, 
                               dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listePma)
                            {
                                countConsultationSpecialiseeFemininAdulte += cn.NumeroConsultation;                            }
                        }
                        foreach (var pme in patientFemininEnfant)
                        {
                            var listePme = AppCode.ConnectionClassClinique.CountConsultation("", pme.NumeroPatient, 
                              dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listePme)
                            {
                                countConsultationSpecialiseeFemininEnfant += cn.NumeroConsultation;
                            }
                        }

                        #endregion

                    }
                    else
                    {
                        nomEntreprise = rw.NomEntreprise;
                        #region SujetMasculin
                        var listePatientMasculin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("M", rw.NomEntreprise);
                        var patientMasculinEnfant = new List<AppCode.Patient>();
                        var patientMasculinAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientMasculin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientMasculinEnfant.Add(p);
                                }
                                else
                                {
                                    patientMasculinAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientMasculinEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientMasculinAdulte.Add(p);
                            }
                            else
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientMasculinAdulte)
                        {
                            var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pma.NumeroPatient,
                             comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            foreach (var cn in listeC)
                            {
                                if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                                {
                                    countConsultationPrenataleAdulte += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                                {
                                    countConsultationMasculinEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                                {
                                    countConsultationMasculinAdulte += cn.Quantite;
                                }
                                else
                                {
                                    countConsultationSpecialiseeMasculinAdulte += cn.Quantite;
                                }
                            }
                        }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pme.NumeroPatient,
                              comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            foreach (var cn in listeC)
                            {
                                if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                                {
                                    countConsultationPrenataleEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                                {
                                    countConsultationMasculinEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                                {
                                    countConsultationMasculinAdulte += cn.Quantite;
                                }
                                else
                                {
                                    countConsultationSpecialiseeMasculinEnfant += cn.Quantite;
                                }
                            }
                        }

                        #endregion

                        #region SujetFeminin
                        var listePatientFeminin = AppCode.ConnectionClassClinique.ListeDesPatientsTrieParSexe("F", rw.NomEntreprise);
                        var patientFemininEnfant = new List<AppCode.Patient>();
                        var patientFemininAdulte = new List<AppCode.Patient>();
                        foreach (var p in listePatientFeminin)
                        {
                            int an;
                            if (Int32.TryParse(p.An, out an))
                            {
                                if (an < 18)
                                {
                                    patientFemininEnfant.Add(p);
                                }
                                else
                                {
                                    patientFemininAdulte.Add(p);
                                }
                            }
                            else if (p.An.ToLower().Contains("enfant"))
                            {
                                patientFemininEnfant.Add(p);
                            }
                            else if (p.An.ToLower().Contains("adulte"))
                            {
                                patientFemininAdulte.Add(p);
                            }
                            else
                            {
                                patientMasculinEnfant.Add(p);
                            }
                        }
                        foreach (var pma in patientFemininAdulte)
                        {
                            var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pma.NumeroPatient,
                              comboBox2.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listeC)
                            {
                                if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                                {
                                    countConsultationPrenataleAdulte += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                                {
                                    countConsultationFemininEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                                {
                                    countConsultationFemininAdulte += cn.Quantite;
                                }
                                else
                                {
                                    countConsultationSpecialiseeFemininAdulte += cn.Quantite;
                                }
                            }
                        }
                        foreach (var pme in patientFemininEnfant)
                        {
                            var listeC = AppCode.ConnectionClassClinique.ListeConsultationsDansFactureDesConventionnes(pme.NumeroPatient,
                             comboBox2.Text, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                            foreach (var cn in listeC)
                            {
                                if (cn.Designation.ToUpper() == "Consultation Pre-Natale".ToUpper())
                                {
                                    countConsultationPrenataleEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation enfant".ToUpper())
                                {
                                    countConsultationFemininEnfant += cn.Quantite;
                                }
                                else if (cn.Designation.ToUpper() == "Consultation adulte".ToUpper())
                                {
                                    countConsultationFemininAdulte += cn.Quantite;
                                }
                                else
                                {
                                    countConsultationSpecialiseeFemininEnfant += cn.Quantite;
                                }
                            }
                        }
                        #endregion
                    }

                    var totalCountMasculin = countConsultationMasculinEnfant + countConsultationMasculinAdulte+countConsultationSpecialiseeMasculinAdulte+countConsultationSpecialiseeMasculinEnfant;
                    var totalCountFeminin = countConsultationFemininEnfant + countConsultationFemininAdulte + countConsultationSpecialiseeFemininEnfant + countConsultationSpecialiseeFemininAdulte ;
                    var totalCountEnfantParConvention = countConsultationFemininEnfant + countConsultationMasculinEnfant+countConsultationSpecialiseeFemininEnfant+countConsultationSpecialiseeMasculinEnfant;
                    var totalCountAdulteParConvention = countConsultationFemininAdulte + countConsultationMasculinAdulte+countConsultationSpecialiseeFemininAdulte+countConsultationSpecialiseeMasculinAdulte;
                    var totalCountPrenatal = countConsultationPrenataleAdulte + countConsultationPrenataleEnfant;
                    var totalCountSpecialise = countConsultationSpecialiseeFemininAdulte + countConsultationSpecialiseeFemininEnfant +
                        countConsultationSpecialiseeMasculinAdulte + countConsultationSpecialiseeMasculinEnfant;
                    var totalParSecteur = totalCountFeminin + totalCountMasculin + totalCountPrenatal+totalCountSpecialise;


                    totalCountMasculinEnfant += countConsultationMasculinEnfant;
                    totalCountFemininEnfant += countConsultationFemininEnfant;
                    totalCountMasculinAdulte += countConsultationMasculinAdulte;
                    totalCountFemininAdulte += countConsultationFemininAdulte;
                    totalCountPrenatalEnfant += countConsultationPrenataleEnfant;
                    totalCountPrenatalAdulte += countConsultationPrenataleAdulte;
                    totalCountSpecMasculinAdulte +=countConsultationSpecialiseeMasculinAdulte;
                    totalCountSpecMasculinEnfant +=countConsultationSpecialiseeMasculinEnfant;
                    totalCountSpecFemininEnfant += countConsultationSpecialiseeFemininEnfant;
                    totalCountSpecFemininAdulte +=countConsultationSpecialiseeFemininAdulte;

                    totalGlobalEnfant += totalCountEnfantParConvention;
                    totalGlobalAdulte += totalCountAdulteParConvention;
                    totalGlobalMasculin += totalCountMasculin;
                    totalGlobalFeminin += totalCountFeminin;

                    dgvStatistique.Rows.Add(nomEntreprise, countConsultationMasculinEnfant,
                        countConsultationFemininEnfant, countConsultationMasculinAdulte,
                        countConsultationFemininAdulte, countConsultationPrenataleEnfant,
                       countConsultationPrenataleAdulte,countConsultationSpecialiseeMasculinAdulte,
                      countConsultationSpecialiseeFemininAdulte, totalCountMasculin,
                        totalCountFeminin, totalCountEnfantParConvention,
                        totalCountAdulteParConvention, totalCountPrenatal, totalParSecteur);
                }

                var totalPrenatal = totalCountPrenatalEnfant + totalCountPrenatalAdulte;
                var totalGlobal = totalGlobalFeminin + totalGlobalMasculin + totalPrenatal;
                dgvStatistique.Rows.Add("TOTAUX", totalCountMasculinEnfant, totalCountFemininEnfant,
                    totalCountMasculinAdulte, totalCountFemininAdulte, totalCountPrenatalEnfant, totalCountPrenatalAdulte,
                    totalGlobalMasculin, totalGlobalFeminin, totalGlobalEnfant, totalGlobalAdulte, totalPrenatal, totalGlobal);
                dgvStatistique.Rows[dgvStatistique.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvStatistique.Rows.Clear();
            dgvStatistique.Columns.Clear();
        }

        Bitmap statistiqueBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(statistiqueBitmap, 7, 10, statistiqueBitmap.Width, statistiqueBitmap.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStatistique.Rows.Count > 0)
                {
                    if (comboBox2.Text == "CONSULTATION")
                    {
                        statistiqueBitmap = AppCode.Impression.ImprimerStatistiqueDeConsultation(dgvStatistique, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);

                    }
                    else if (comboBox2.Text == "PHARMACIE")
                    {
                        statistiqueBitmap = AppCode.Impression.ImprimerStatistiqueDePharmacie(dgvStatistique, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                    }

                    else //if (comboBox2.Text == "LABORATOIRE")
                    {
                        statistiqueBitmap = AppCode.Impression.ImprimerStatistiqueDeLaboratoire(dgvStatistique, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                    }
                    if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch { }
        }
    }
}
