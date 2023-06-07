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
    public partial class ListeFrm : Form
    {
        public ListeFrm()
        {
            InitializeComponent();
        }
        public string state;
        private void ListeFrm_Load(object sender, EventArgs e)
        {
            try
            {
               DataTable dt = AppCode.ConnectionClassClinique.ListeDesFacturesDT(dateTimePicker1.Value);
                for(int i=0; i<dt.Rows.Count;i++)
                {
                    if(!string.IsNullOrEmpty(dt.Rows[i].ItemArray[6].ToString()))
                    {
                        if (state == "1")
                        {
                            if (dt.Rows[i].ItemArray[6].ToString().StartsWith("0-"))
                            {
                                dataGridView1.Rows.Add(
                                   dt.Rows[i].ItemArray[0].ToString(),
                                   dt.Rows[i].ItemArray[1].ToString(),
                                   dt.Rows[i].ItemArray[4].ToString(),
                                   dt.Rows[i].ItemArray[2].ToString()

                                );
                            }
                        }
                        else if (state == "2")
                        {
                            if (dt.Rows[i].ItemArray[6].ToString().StartsWith("000-"))
                            {
                                dataGridView1.Rows.Add(
                                   dt.Rows[i].ItemArray[0].ToString(),
                                   dt.Rows[i].ItemArray[1].ToString(),
                                   dt.Rows[i].ItemArray[4].ToString(),
                                   dt.Rows[i].ItemArray[2].ToString()

                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                DataTable dt = AppCode.ConnectionClassClinique.ListeDesFacturesDT(dateTimePicker1.Value);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[6].ToString()))
                    {

                        if (state == "1")
                        {
                            if (dt.Rows[i].ItemArray[6].ToString().StartsWith("0-"))
                            {
                                dataGridView1.Rows.Add(
                                   dt.Rows[i].ItemArray[0].ToString(),
                                   dt.Rows[i].ItemArray[1].ToString(),
                                   dt.Rows[i].ItemArray[4].ToString(),
                                   dt.Rows[i].ItemArray[2].ToString()

                                );
                            }
                        }
                        else if (state == "2")
                        {
                            if (dt.Rows[i].ItemArray[6].ToString().StartsWith("000-"))
                            {
                                dataGridView1.Rows.Add(
                                   dt.Rows[i].ItemArray[0].ToString(),
                                   dt.Rows[i].ItemArray[1].ToString(),
                                   dt.Rows[i].ItemArray[4].ToString(),
                                   dt.Rows[i].ItemArray[2].ToString()

                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }
        
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
                   for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                   {
                       if(MonMessageBox.ShowBox("Voulez vous modifiez les donnees?","Confirmation","confirmation.png")=="1")
                AppCode.ConnectionClassClinique.ModifierUneFacture(Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[0].Value.ToString()));
            }      
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.End)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        var total = 0.0;
                        for (var i = 0; i < dataGridView1.SelectedRows.Count; i++)
                        {
                            total += double.Parse(dataGridView1.SelectedRows[i].Cells[3].Value.ToString());
                            Text = total.ToString();
                        }
                    }
                }
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
