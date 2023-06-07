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
    public partial class ListeSCFrm : Form
    {
        public ListeSCFrm()
        {
            InitializeComponent();
        }

        private void ListeSCFrm_Load(object sender, EventArgs e)
        {
            listView2.Columns.Clear();
            listView2.Columns.Add("Id", 120);
            listView2.Columns.Add("Nom", listView2.Width -140);
            listView2.Columns.Add("Sexe", 0);
            listView2.Columns.Add("Age", 0);

            listView2.Items.Clear();
            var listePatient = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesPatientsParEntreprise(employe, entreprise);
            foreach (GestionDuneClinique.AppCode.Patient patient in listePatient)
            {
                var items = new string[]
                        {
                            patient.NumeroPatient.ToString(),
                            patient.Nom+" "+
                            patient.Prenom,
                            patient.Sexe,
                            patient.An.ToString()
                        };

                var lstItems = new ListViewItem(items);
                listView2.Items.Add(lstItems);
            }
            label1.Text = "Liste des patients S/C de " + employe;
            
        }

        public static int numeroPatient;
        static ListeSCFrm frm;
        public static string btnClick, employe, entreprise;

        public static string ShowBox()
        {
            frm = new ListeSCFrm();
            frm.ShowDialog();
            return btnClick;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                numeroPatient = Convert.ToInt32(listView2.SelectedItems[0].SubItems[0].Text);
                btnClick = "1";
                frm.Dispose();

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(listView2.SelectedItems.Count>0)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de ce patient ?", "Confirmation", "confirmation.png") == "1")
                                        {
                                           if( AppCode.ConnectionClassClinique.SupprimerUnPatient(Int32.Parse(listView2.SelectedItems[0].SubItems[0].Text)))
                                           {
                                               listView2.Items.Remove(listView2.SelectedItems[0]);
                                           }
                }
            }
        }
    }
}
