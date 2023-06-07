using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class ListeModFrm : Form
    {
        public ListeModFrm()
        {
            InitializeComponent();
        }

        private void ListeModFrm_Load(object sender, EventArgs e)
        {
            ListeMouvement();
        }
        public static int etatDoc, typeDepot;
        void ListeMouvement()
        {
            try
            {
                dgvProduit.Rows.Clear();
                if (Form1.typeUtilisateur == "caissier")
                {
                    var liste = from m in AppCode.ConnectionClass.ListeMouvementStock(typeDepot)
                                join e in AppCode.ConnectionClass.ListeDesEmployees()
                                on m.NumeroMatricule equals e.NumMatricule
                                join r in AppCode.ConnectionClass.ListeDocumentStock(etat)
                                on m.IDReference equals r.IDReference
                                where e.NomEmployee.StartsWith(txtNom.Text, StringComparison.CurrentCultureIgnoreCase)
                                where r.Reference.ToLower().Contains("pharmacie")
                                where m.Etat == etat
                                orderby m.Date descending
                                select new
                                {
                                    m.NumeroMatricule,
                                    m.ID,
                                    m.IDReference,
                                    m.Date,
                                    m.Etat,
                                    e.NomEmployee,
                                    r.Reference,
                                    m.EtatValider
                                };
                    foreach (var m in liste)
                    {
                        string etatMouvement;
                        if (m.Etat == 1)
                        {
                            etatMouvement = "MOUVEMENT ENTREE";
                        }
                        else
                        {
                            etatMouvement = "MOUVEMENT SORTIE";
                        }
                        var etatValider = "";
                        if (m.EtatValider)
                        {
                            etatValider = "VALIDER";
                        }
                        else
                        {
                            etatValider = "NON VALIDER";
                        }
                        dgvProduit.Rows.Add(m.ID, m.NomEmployee, m.Date.ToShortDateString(), etatMouvement, m.Reference, etatValider);
                    }
                }
                else
                {
                    var liste = from m in AppCode.ConnectionClass.ListeMouvementStock(typeDepot)
                                join e in AppCode.ConnectionClass.ListeDesEmployees()
                                on m.NumeroMatricule equals e.NumMatricule
                                join r in AppCode.ConnectionClass.ListeDocumentStock(etat)
                                on m.IDReference equals r.IDReference
                                where e.NomEmployee.StartsWith(txtNom.Text, StringComparison.CurrentCultureIgnoreCase)
                                where m.Etat == etat
                                orderby m.Date descending
                                select new
                                {
                                    m.NumeroMatricule,
                                    m.ID,
                                    m.IDReference,
                                    m.Date,
                                    m.Etat,
                                    e.NomEmployee,
                                    r.Reference,
                                    m.EtatValider
                                };
                    foreach (var m in liste)
                    {
                        string etatMouvement;
                        if (m.Etat == 1)
                        {
                            etatMouvement = "MOUVEMENT ENTREE";
                        }
                        else
                        {
                            etatMouvement = "MOUVEMENT SORTIE";
                        }
                        var etatValider = "";
                        if (m.EtatValider)
                        {
                            etatValider = "VALIDER";
                        }
                        else
                        {
                            etatValider = "NON VALIDER";
                        }
                        dgvProduit.Rows.Add(m.ID, m.NomEmployee, m.Date.ToShortDateString(), etatMouvement, m.Reference, etatValider);
                    }
                }
            }
            catch { }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dgvProduit.Rows.Clear();
                var liste = from m in AppCode.ConnectionClass.ListeMouvementStock(typeDepot)
                            join em in AppCode.ConnectionClass.ListeDesEmployees()
                            on m.NumeroMatricule equals em.NumMatricule
                            join r in AppCode.ConnectionClass.ListeDocumentStock(etat)
                            on m.IDReference equals r.IDReference
                            where m.Date >=dateTimePicker1.Value.Date
                            where m.Date <dateTimePicker2.Value.Date.AddHours(24)
                            where m.Etat == etat
                            select new
                            {
                                m.NumeroMatricule,
                                m.ID,
                                m.IDReference,
                                m.Date,
                                m.Etat,
                                em.NomEmployee,
                                r.Reference,
                                m.EtatValider
                            };
                foreach (var m in liste)
                {
                    string etatMouvement;
                    if (m.Etat == 1)
                    {
                        etatMouvement = "MOUVEMENT ENTREE";
                    }
                    else
                    {
                        etatMouvement = "MOUVEMENT SORTIE";
                    }
                    var etatValider = "";
                    if (m.EtatValider)
                    {
                        etatValider = "VALIDER";
                    }
                    else
                    {
                        etatValider = "NON VALIDER";
                    }
                    dgvProduit.Rows.Add(m.ID, m.NomEmployee, m.Date.ToShortDateString(), etatMouvement, m.Reference, etatValider);
                }

            }
            catch { }
        }

        private void dgvProduit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                if (GestionPharmacetique.Form1.typeUtilisateur == "caissier")
                {
                    return;
                }
                if (GestionPharmacetique.Form1.typeUtilisateur == "admin" || GestionPharmacetique.Form1.typeUtilisateur == "intermediaire")
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation", "confirmation.png") == "1")
                    {
                        AppCode.ConnectionClass.RetirerMouvement(Convert.ToInt32(dgvProduit.CurrentRow.Cells[0].Value.ToString()));
                       AppCode. ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "suppression du mouvement du mouvement " +dgvProduit.CurrentRow.Cells[3].Value.ToString() +" de réference : " + dgvProduit.CurrentRow.Cells[4].Value.ToString(), this.Name);
                        dgvProduit.Rows.Remove(dgvProduit.CurrentRow);
                    }
                }
            }
        }

        public static ListeModFrm frm;
     
        public static AppCode.DocumentStock doc = new AppCode.DocumentStock();
        static bool flag;
        static public int etat, idMouvement;
        static public DateTime dateMouvement;
        public static bool ShowBox()
        {
            frm = new ListeModFrm();
            frm.ShowDialog();
            return flag;
        }

        private void dgvProduit_DoubleClick(object sender, EventArgs e)
        {
            if (dgvProduit.SelectedRows.Count > 0)
            {
                doc.ID = Convert.ToInt32(dgvProduit.SelectedRows[0].Cells[0].Value.ToString());
                doc.NumeroMatricule = dgvProduit.SelectedRows[0].Cells[1].Value.ToString();
                flag = true;
                Dispose();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            flag = false;
            Dispose();
        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {
            ListeMouvement();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvProduit.Rows.Clear();
                var liste = from m in AppCode.ConnectionClass.ListeMouvementStock(typeDepot)
                            join em in AppCode.ConnectionClass.ListeDesEmployees()
                            on m.NumeroMatricule equals em.NumMatricule
                            join r in AppCode.ConnectionClass.ListeDocumentStock(etat)
                            on m.IDReference equals r.IDReference
                            where r.Reference.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                            where m.Etat == etat
                            select new
                            {
                                m.NumeroMatricule,
                                m.ID,
                                m.IDReference,
                                m.Date,
                                m.Etat,
                                em.NomEmployee,
                                r.Reference,
                                m.EtatValider
                            };
                foreach (var m in liste)
                {
                    string etatMouvement;
                    if (m.Etat == 1)
                    {
                        etatMouvement = "MOUVEMENT ENTREE";
                    }
                    else
                    {
                        etatMouvement = "MOUVEMENT SORTIE";
                    }
                    var etatValider = "";
                    if (m.EtatValider)
                    {
                        etatValider = "VALIDER";
                    }
                    else
                    {
                        etatValider = "NON VALIDER";
                    }
                    dgvProduit.Rows.Add(m.ID, m.NomEmployee, m.Date.ToShortDateString(), etatMouvement, m.Reference, etatValider);
                }

            }
            catch { }
        }
    }
}
