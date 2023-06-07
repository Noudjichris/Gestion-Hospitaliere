using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabMonitoring.Formes
{
    public partial class BilanExamFrm : Form
    {
        public BilanExamFrm()
        {
            InitializeComponent();
        }
        public int idExam;
           public string  examen;
           public DateTime date1, date2;
        private void BilanExamFrm_Load(object sender, EventArgs e)
        {
            try
            {
                label8.Text = "Bilan de resultat " + examen + " du " +date1.ToShortDateString() + " au "+ date2.ToShortDateString();
                var liste = from i in AppCode.ConnectionClass.ListeResultatDetaillePatient()
                            join ir in AppCode.ConnectionClass.ListeResultatPatient()
                            on i.NumeroResultat equals ir.NumeroResultat
                            where i.IDExam == idExam
                            where ir.DateResultat >=date1
                            where ir.DateResultat < date2.AddHours(24)
                            select i;
                var countPositive = 0;
                var countNegative = 0;
                foreach (var i in liste)
                {
                    if (i.ResultatExamen.ToLower().Contains("positif") ||  i.ResultatExamen.ToLower().Contains("positive") || i.ResultatExamen.ToLower().Contains("presence") || i.ResultatExamen.ToLower().Contains("présence"))
                    {
                        countPositive++;
                    }
                    else if (i.ResultatExamen.ToLower().Contains("négatif") || i.ResultatExamen.ToLower().Contains("négative") || i.ResultatExamen.ToLower().Contains("negative") || i.ResultatExamen.ToLower().Contains("absence"))
                    {
                        countNegative++;
                    }
                    else
                    {
                        if (examen.ToUpper().Contains("KAOP"))
                        {
                            countPositive = AppCode.ConnectionClass.BilanDesExamens("KAOP", date1, date2).Rows.Count - countNegative;
                        }
                    }
                }
                dataGridView3.Rows.Add(countNegative, countPositive);
            }
            catch { }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
    }
}
