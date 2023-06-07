using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.FormesClinique
{
    public partial class ProDetFactureFrm : Form
    {
        public ProDetFactureFrm()
        {
            InitializeComponent();
        }

        public int numeroFacture;
        private void ProDetFactureFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var liste = AppCode.ConnectionClassClinique.TableDesDetailsFacturesProforma(numeroFacture);
                var total = 0.0;
                foreach (var l in liste)
                {
                    dataGridView1.Rows.Add(l.Designation, l.Quantite, l.Prix, l.PrixTotal);
                    total += l.PrixTotal;
                }
                txtTotal.Text = total.ToString();
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==4)
                try
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        AppCode.ConnectionClassClinique.SupprimerUneFactureProforma(numeroFacture, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                            double.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString()));
                        var liste = AppCode.ConnectionClassClinique.TableDesDetailsFacturesProforma(numeroFacture);
                        var total = 0.0;
                        dataGridView1.Rows.Clear();
                        foreach (var l in liste)
                        {
                            dataGridView1.Rows.Add(l.Designation, l.Quantite, l.Prix, l.PrixTotal);
                            total += l.PrixTotal;
                        }
                        txtTotal.Text = total.ToString();
                    }
                }
                catch { }
        }
    }
}
