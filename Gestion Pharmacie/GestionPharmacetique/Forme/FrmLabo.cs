using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class FrmLabo : Form
    {
        public FrmLabo()
        {
            InitializeComponent();
        }

        private void FrmLabo_Load(object sender, EventArgs e)
        {
            Liste();
        }

        void Liste()
        {
            dgvProduit.Rows.Clear();
              var dt =AppCode.ConnectionClassClinique.ListeDesAnalyses(textBox1.Text);
            for (var rows=0;rows<dt.Rows.Count;rows++)
            {
                dgvProduit.Rows.Add(dt.Rows[rows].ItemArray[0].ToString(), dt.Rows[rows].ItemArray[1].ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Liste();
        }
        public string numeroProduit;
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.SelectedRows.Count > 0)
                {
                    int dosage;

                    if (Int32.TryParse(txtDosage.Text, out dosage))
                    {
                        AppCode.ConnectionClass.MisAjourLeDosage(numeroProduit,dgvProduit.SelectedRows[0].Cells[0].Value.ToString(), dosage);
                        Dispose();
                    }
                }
            }
            catch { }

        }
    }
}
