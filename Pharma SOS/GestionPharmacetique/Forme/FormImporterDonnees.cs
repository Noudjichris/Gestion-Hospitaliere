using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ImpoterLesDonneesExcels
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Excel|*.xls";
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                excelFileTextBox.Text = openDialog.FileName;
                //Get all worksheet names from the Excel file selected using GetSchema of an OleDbConnection
                string sourceConnectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES'", excelFileTextBox.Text);
                OleDbConnection connection = new OleDbConnection(sourceConnectionString);
                connection.Open();
                DataTable tables = connection.GetSchema("Tables", new String[] { null, null, null, "TABLE" });
                connection.Dispose();
                //Add each table name to the combo box
                if (tables != null && tables.Rows.Count > 0)
                {
                    worksheetsComboBox.Items.Clear();
                    foreach (DataRow row in tables.Rows)
                    {
                        worksheetsComboBox.Items.Add(row["TABLE_NAME"].ToString());
                    }
                }
            }

        }

        private void worksheetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Display the data from the selected Worksheet
            string sourceConnectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES'", excelFileTextBox.Text);
            OleDbDataAdapter adapter = new OleDbDataAdapter(String.Format("SELECT * FROM [{0}]", worksheetsComboBox.SelectedItem.ToString()), sourceConnectionString);
            DataTable currentSheet = new DataTable();
            adapter.Fill(currentSheet);
            adapter.Dispose();
            excelDataGridView.DataSource = currentSheet;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (DataGridViewRow dtRow in excelDataGridView.Rows)
                {
                    var codeFamille = Int32.Parse(dtRow.Cells[0].Value.ToString());
                    var designation = dtRow.Cells[1].Value.ToString();
                    if (GestionPharmacetique.AppCode.ConnectionClass.SupprimerFamilleMedicament(codeFamille))
                    {
                        GestionPharmacetique.AppCode.ConnectionClass.AjouterFamilleMedicament(codeFamille, designation,0);
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                GestionPharmacetique.AppCode.ConnectionClassClinique.AjouterEmployeDuneEntreprise(excelDataGridView);
                foreach (DataGridViewRow dtRow in excelDataGridView.Rows)
                {


                ////    var codeFamille = Convert.ToInt32(dtRow.Cells[0].Value.ToString());
                ////    var codeBarre = dtRow.Cells[1].Value.ToString();
                ////    var designation = dtRow.Cells[2].Value.ToString();
                ////    var prixAchat = Convert.ToDecimal(dtRow.Cells[3].Value.ToString());
                ////    var prixVente= Convert.ToDecimal(dtRow.Cells[4].Value.ToString());
                ////    var qte = Convert.ToInt32(dtRow.Cells[5].Value.ToString());
                ////    var qteAlerte = Convert.ToInt32(dtRow.Cells[6].Value.ToString());
                ////    var dateExpiration = DateTime.Now.AddMonths(3);
                ////    if(DateTime.TryParse (dtRow.Cells[7].Value.ToString(), out dateExpiration ))
                ////    {
                ////    }
                ////    var medicament = new GestionPharmacetique.AppCode.Medicament(codeBarre, designation
                ////        , prixAchat, prixVente, qteAlerte, "", codeFamille, dateExpiration, qte);
                ////    if (GestionPharmacetique.AppCode.ConnectionClass.SupprimerMedicament(codeBarre))
                ////    {
                ////        GestionPharmacetique.AppCode.ConnectionClass.AjouterMedicament(medicament);
                ////    }

                }
            }
            catch (Exception  ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
