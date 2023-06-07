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
            else if(comboBox2.Text=="LABORATOIRE")
            {
                StatistiqueDuLaboratoire();
            }
            else if (comboBox2.Text == "PHARMACIE")
            {
                StatistiqueDeLaPharmacie();
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
                var entreprise = new AppCode.Entreprise(0, "", "", "", "", "", DateTime.Now, 0.0,"");
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
                        }
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
                        }
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
        int indexColumns;
        void StatistiqueDesConsultations()
        {
            try
            {
                if (ChoixAgeFrm.ShowBox())
                {
                  var columns="";
                  if (ChoixAgeFrm.ageDebut == "0")
                  {
                      columns =  " < " + ChoixAgeFrm.ageFin;
                  }
                  else if (ChoixAgeFrm.ageFin == "130")
                  {
                      columns = ">=" + ChoixAgeFrm.ageDebut;
                  }
                  else
                  {
                      columns =  ChoixAgeFrm.ageDebut + " - " + ChoixAgeFrm.ageFin;
                  }

                dgvStatistique.CellBorderStyle = DataGridViewCellBorderStyle.RaisedVertical ;
                //dgvStatistique.Columns.Clear();
                //dgvStatistique.Columns.Add("SERVICES", "SERVICES ET CONSULTATIONS CURATIVES");
                dgvStatistique.Columns[0].Width = dgvStatistique.Width / 4-50;
                var columnName = "cl" + indexColumns;
                dgvStatistique.Columns.Add(columnName,columns );
                dgvStatistique.Columns[indexColumns].Width = dgvStatistique.Width / 8;
                //dgvStatistique.Columns.Add("enfants1", "");
                //dgvStatistique.Columns[2].Width = dgvStatistique.Width / 16;
                //dgvStatistique.Columns.Add("Adultes", "CONSULTATION ADULTES ≥ 18 ans");
                //dgvStatistique.Columns[3].Width = dgvStatistique.Width / 15;
                //dgvStatistique.Columns.Add("adultes1", "");
                //dgvStatistique.Columns[4].Width = dgvStatistique.Width / 16;
                //dgvStatistique.Columns.Add("AdultesPre", "CONSULTATION PRENATALE");
                //dgvStatistique.Columns[4].Width = dgvStatistique.Width / 15;
                //dgvStatistique.Columns.Add("AdultesPre", "");
                //dgvStatistique.Columns[3].Width = dgvStatistique.Width / 16;
                //dgvStatistique.Columns.Add("total", "TOTAL");
                //dgvStatistique.Columns[5].Width = dgvStatistique.Width / 16;
                //dgvStatistique.Columns.Add("total1", "");
                //dgvStatistique.Columns[6].Width = dgvStatistique.Width / 16;
                //dgvStatistique.Columns.Add("total2", "");
                //dgvStatistique.Columns[7].Width = dgvStatistique.Width / 16;
                //dgvStatistique.Columns.Add("total1", "");
                //dgvStatistique.Columns[8].Width = dgvStatistique.Width / 16;
                //dgvStatistique.Columns.Add("total1", "");
                //dgvStatistique.Columns[9].Width = dgvStatistique.Width / 16;
                //dgvStatistique.Columns.Add("total1", "");
                //dgvStatistique.Columns[10].Width = dgvStatistique.Width / 16;

                //dgvStatistique.Rows.Clear();
                //dgvStatistique.Rows.Add("SEXE ", "M", "F", "M", "F", "F≤ 18 ans", "F ≥ 18 ans", "TOT M", "TOT F", "TOT ENF", "TOT ADLT","TOT PREN", "TOTAL");
                //dgvStatistique.Rows[0].DefaultCellStyle.BackColor = Color.Yellow;
                //var liste = AppCode.ConnectionClassClinique.ListeDesEntreprises();
                //var entreprise = new AppCode.Entreprise(0, "", "", "", "", "", DateTime.Now, 0.0);
                //liste.Add(entreprise);
                
                var totalCountFemininEnfant = 0;
                var totalCountFemininAdulte = 0;
                var totalCountMasculinEnfant = 0;
                var totalCountMasculinAdulte = 0;
                var totalCountPrenatalEnfant = 0;
                var totalCountPrenatalAdulte = 0;

                var totalGlobalFeminin = 0;
                var totalGlobalMasculin = 0;
                var totalGlobalEnfant = 0;
                var totalGlobalAdulte = 0;


                    indexColumns++;
                }
            }
            catch
            { }
        }

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
                var entreprise = new AppCode.Entreprise(0, "", "", "", "", "", DateTime.Now, 0.0,"");
                liste.Add(entreprise);

                var GlobalCountFemininEnfant = 0;
                var GlobalCountMasculinEnfant = 0;
                var GlobalCountFemininAdulte = 0;
                var GlobalCountMasculinAdulte = 0;
                var GlobalDetailsCountFemininEnfant = 0;
                var GlobalDetailsCountMasculinEnfant = 0;
                var GlobalDetailsCountFemininAdulte = 0;
                var GlobalDetailsCountMasculinAdulte = 0;
                
                foreach (var rw in liste)
                {
                    #region
                    var nomEntreprise = "";
                    var countAnalyseFemininEnfant = 0;
                    var countAnalyseFemininAdulte = 0;
                    var countAnalyseMasculinEnfant = 0;
                    var countAnalyseMasculinAdulte = 0;
                    

                    var countDetailsAnalysesFemininEnfant = 0;
                    var countDetailsAnalysesFemininAdulte = 0;
                    var countDetailsAnalysesMasculinEnfant = 0;
                    var countDetailsAnalysesMasculinAdulte = 0;
                    #endregion

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
                        }
                        foreach (var pma in patientMasculinAdulte)
                        {
                            countAnalyseMasculinAdulte += AppCode.ConnectionClassClinique.CountAnalyse(pma.NumeroPatient,
                               dateTimePicker1.Value, dateTimePicker2.Value.Date);
                             countDetailsAnalysesMasculinAdulte += AppCode.ConnectionClassClinique.CountDetailsAnalyse(pma.NumeroPatient,
                                 dateTimePicker1.Value, dateTimePicker2.Value.Date);
                        }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            countAnalyseMasculinEnfant += AppCode.ConnectionClassClinique.CountAnalyse(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            countDetailsAnalysesMasculinEnfant += AppCode.ConnectionClassClinique.CountDetailsAnalyse(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
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
                        }
                        foreach (var pma in patientFemininAdulte)
                        {
                            countAnalyseFemininAdulte += AppCode.ConnectionClassClinique.CountAnalyse(pma.NumeroPatient,
                               dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            countDetailsAnalysesFemininAdulte += AppCode.ConnectionClassClinique.CountDetailsAnalyse(pma.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
                        }
                        foreach (var pme in patientFemininEnfant)
                        {
                            countAnalyseFemininEnfant += AppCode.ConnectionClassClinique.CountAnalyse(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            countDetailsAnalysesFemininEnfant += AppCode.ConnectionClassClinique.CountDetailsAnalyse(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
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
                        }
                        foreach (var pma in patientMasculinAdulte)
                        {
                            countAnalyseMasculinAdulte += AppCode.ConnectionClassClinique.CountAnalyse(pma.NumeroPatient,
                               dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            countDetailsAnalysesMasculinAdulte += AppCode.ConnectionClassClinique.CountDetailsAnalyse(pma.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
                        }
                        foreach (var pme in patientMasculinEnfant)
                        {
                            countAnalyseMasculinEnfant += AppCode.ConnectionClassClinique.CountAnalyse(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            countDetailsAnalysesMasculinEnfant += AppCode.ConnectionClassClinique.CountDetailsAnalyse(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
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
                        }

                        foreach (var pma in patientFemininAdulte)
                        {
                            countAnalyseFemininAdulte += AppCode.ConnectionClassClinique.CountAnalyse(pma.NumeroPatient,
                               dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            countDetailsAnalysesFemininAdulte += AppCode.ConnectionClassClinique.CountDetailsAnalyse(pma.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
                        }
                        foreach (var pme in patientFemininEnfant)
                        {
                            countAnalyseFemininEnfant += AppCode.ConnectionClassClinique.CountAnalyse(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
                            countDetailsAnalysesFemininEnfant += AppCode.ConnectionClassClinique.CountDetailsAnalyse(pme.NumeroPatient,
                                dateTimePicker1.Value, dateTimePicker2.Value.Date);
                        }
                        #endregion
                    }

                    #region
                    var totalAnalyseMasculinParConvention = countAnalyseMasculinEnfant + countAnalyseMasculinAdulte;
                    var totalAnalyseFemininParConvention = countAnalyseFemininEnfant + countAnalyseFemininAdulte;
                    var totalDetailsAnalyseMasculinParConvention = countDetailsAnalysesMasculinAdulte+countDetailsAnalysesMasculinEnfant;
                    var totalDetailsAnalyseFemininParConvention = countDetailsAnalysesFemininAdulte+countDetailsAnalysesFemininEnfant;
                    var totalAnalyseMasculinFemininParConvention = totalAnalyseFemininParConvention + totalAnalyseMasculinParConvention ;
                    var totalDetailsAnalyseMasculinFemininParConvention = totalDetailsAnalyseFemininParConvention + totalDetailsAnalyseMasculinParConvention;
                    var totalCountEnfantParConvention = countAnalyseFemininEnfant + countAnalyseMasculinEnfant;
                    var totalCountAdulteParConvention = countAnalyseFemininAdulte + countAnalyseMasculinAdulte;
                    var totalCountEnfantDetailleParConvention = countDetailsAnalysesFemininEnfant + countDetailsAnalysesMasculinEnfant;
                    var totalCountAdulteDetailleParConvention = countDetailsAnalysesFemininAdulte  + countDetailsAnalysesMasculinAdulte;

                    GlobalCountMasculinEnfant += countAnalyseMasculinEnfant;
                    GlobalCountFemininEnfant += countAnalyseFemininEnfant;
                    GlobalCountMasculinAdulte += countAnalyseMasculinAdulte;
                    GlobalCountFemininAdulte += countAnalyseFemininAdulte;

                    GlobalDetailsCountFemininAdulte += countDetailsAnalysesFemininAdulte;
                    GlobalDetailsCountFemininEnfant += countDetailsAnalysesFemininEnfant;
                    GlobalDetailsCountMasculinAdulte += countDetailsAnalysesMasculinAdulte;
                    GlobalDetailsCountMasculinEnfant += countDetailsAnalysesMasculinEnfant;
              
                    dgvStatistique.Rows.Add(nomEntreprise, countAnalyseMasculinEnfant, countAnalyseFemininEnfant,
                      countDetailsAnalysesMasculinEnfant, countDetailsAnalysesFemininEnfant, countAnalyseMasculinAdulte,
                      countAnalyseFemininAdulte, countDetailsAnalysesMasculinAdulte, countDetailsAnalysesFemininAdulte,
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

                dgvStatistique.Rows.Add("TOTAUX", GlobalCountMasculinEnfant, GlobalCountFemininEnfant,
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
       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexColumns = 1;
            dgvStatistique.Rows.Clear();
            dgvStatistique.Columns.Clear();
            dgvStatistique.Columns.Add("SERVICES", "SERVICES CPN");
            dgvStatistique.Columns[0].Width = dgvStatistique.Width / 4 - 50;
        }

        Bitmap statistiqueBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(statistiqueBitmap, 2, 10, statistiqueBitmap.Width, statistiqueBitmap.Height);
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
                pathFolder = pathFolder + "\\Statistique laboratoire";
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                if (dgvStatistique.Rows.Count > 0)
                {
                    if (comboBox2.Text == "CONSULTATION")
                    {
                        statistiqueBitmap = AppCode.Impression.ImprimerStatistiqueDeConsultation(dgvStatistique, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);

                    }
                    else if (comboBox2.Text == "LABORATOIRE")
                    {                      
                      sfd.FileName = "Statistique laboratoire_imprimé_le_" + date + ".pdf";

                      if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                      {

                          var count = dgvStatistique.Rows.Count-2;

                          var index = (dgvStatistique.Rows.Count-2) / 21;

                          for (var i = 0; i <= index; i++)
                          {
                              if (i * 21 < count)
                              {
                                  statistiqueBitmap = AppCode.Impression.  ImprimerStatistiqueDeLaboratoire(dgvStatistique, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, i);

                                  var inputImage = @"cdali" + i;
                                  // Create an empty page
                                  sharpPDF.pdfPage pageIndex = document.addPage(500, 700);

                                  document.addImageReference(statistiqueBitmap, inputImage);
                                  sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                  pageIndex.addImage(img1, -2, 0, pageIndex.height, pageIndex.width);
                              }
                          }
                          document.createPDF(sfd.FileName);
                          System.Diagnostics.Process.Start(sfd.FileName);
                      }
                    }
                    else if (comboBox2.Text == "PHARMACIE")
                    {
                        sfd.FileName = "Statistique laboratoire_imprimé_le_" + date + ".pdf";

                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                            var count = dgvStatistique.Rows.Count - 1;

                            var index = (dgvStatistique.Rows.Count - 1) / 21;

                            for (var i = 0; i <= index; i++)
                            {
                                if (i * 21 < count)
                                {
                                  statistiqueBitmap=  AppCode.Impression.ImprimerStatistiqueDePharmacie(dgvStatistique, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, i);

                                    var inputImage = @"cdali" + i;
                                    // Create an empty page
                                    sharpPDF.pdfPage pageIndex = document.addPage(500, 700);

                                    document.addImageReference(statistiqueBitmap, inputImage);
                                    sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }

                    //if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                    //    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    //    printPreviewDialog1.ShowDialog();
                    //}
                }
            }
            catch(Exception ex)
            { }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
