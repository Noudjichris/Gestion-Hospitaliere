using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GestionDuneClinique.AppCode;
using GestionDuneClinique;
using System.Linq;

namespace GestionPharmacetique.Forme
{
    public partial class EncaissementFrm : Form
    {
        public EncaissementFrm()
        {
            InitializeComponent();
        }

        private void DepenseFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.BlueViolet, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = 
                new LinearGradientBrush(area1, SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Black, Color.CadetBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //enregistrer une depense
        private void btnEnrgistrerHospital_Click_1(object sender, EventArgs e)
        {
            try
            {
                var versement = CreerUnVersement();
                if (versement != null)
                {
                    if (ConnectionClassClinique.InsereréUnVersement(versement))
                    {
                        txtPrix.Text = "";
                        cmbNomEmpoye.Text = "";
                        dtpDebut.Value = DateTime.Now;
                        ListeDesVersements();
                    }
                }
            }
            catch (Exception exception) { MonMessageBox.ShowBox("", exception); }
        }


        private void i_Click(object sender, EventArgs e)
        {
            try
            {
                var versement = CreerUnVersement();
                if (versement != null)
                {
                    if(MonMessageBox.ShowBox("Voulez vous modifier ces données?","Confirmation","confirmation.png")=="1")
                    {
                    if (ConnectionClassClinique.ModifierUnVersement(versement))
                    {
                        txtPrix.Text = "";
                        cmbNomEmpoye.Text = "";
                        dtpDebut.Value = DateTime.Now;
                        ListeDesVersements();
                    }
                    }
                }
            }
            catch (Exception exception) { MonMessageBox.ShowBox("", exception); }
        }

        private void DepenseFrm_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("NUM", 0);
            listView1.Columns.Add("DATE", listView1.Width / 4);
            listView1.Columns.Add("NOM CAISSIER(E)", listView1.Width / 2);
            listView1.Columns.Add("MONTANT", listView1.Width / 4-5);
            ListeDesVersements();
        }

        //liste 
        private void ListeDesVersements()
        {
            try
            {
                var listeVersement = ConnectionClassClinique.ListeDesVersements();
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
                var liste = from v in listeVersement
                            join em in listeEmploye
                            on v.NumeroEmploye equals
                            em.NumMatricule
                            select new
                            {
                                em.NumMatricule,
                                em.NomEmployee,
                                v.DateVersement,
                                v.MontantVerse,
                                v.IDVersment
                            };
                listView1.Items.Clear();
                foreach (var ev in liste)
                {
                    var items = new string[]
                    {
                        ev.IDVersment.ToString(),
                        ev.DateVersement.ToShortDateString(),
                        ev.NomEmployee,
                        ev.MontantVerse.ToString()
                    };

                    var listItems = new ListViewItem(items);
                    listView1.Items.Add(listItems);   
                }
                foreach (ListViewItem list in listView1.Items)
                {
                    list.BackColor = list.Index% 2==0 ? Color.Bisque : Color.White;
                }
            }
            catch 
            { 
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbNomEmpoye.Text = listView1.SelectedItems[0].SubItems[2].Text;
                txtPrix.Text = listView1.SelectedItems[0].SubItems[3].Text;
                dtpDebut.Value = DateTime.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                idVersement = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
            }
            catch
            {
            }
        }

        private void btnSupprimerHospi_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    var versement = new Versement();
                    versement.IDVersment = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation", "confirmation.png") == "1")
                    {
                        if (ConnectionClassClinique.SupprimerUnVerssement(versement.IDVersment))
                        {
                            txtPrix.Text = "";
                            cmbNomEmpoye.Text = "";
                            dtpDebut.Value = DateTime.Now;
                            ListeDesVersements();
                        }
                    }
                }
            }
            catch (Exception exception) { MonMessageBox.ShowBox("", exception); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
                try
                {
                   
                    if (cmbTypeRapport.Text == "VERSEMENT TOTAL")
                    {
                        var listeVersement = ConnectionClassClinique.ListeDesVersements();
                        var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
                        var liste = from v in listeVersement
                                    join em in listeEmploye
                                    on v.NumeroEmploye equals
                                    em.NumMatricule
                                    where v.DateVersement >= dateTimePicker1.Value.Date
                                    where v.DateVersement < dateTimePicker2.Value.Date.AddHours(24)
                                    select new
                                    {
                                        em.NumMatricule,
                                        em.NomEmployee,
                                        v.DateVersement,
                                        v.MontantVerse,
                                        v.IDVersment
                                    };
                        listView1.Items.Clear();
                        foreach (var ev in liste)
                        {
                            var items = new string[]
                            {
                                ev.IDVersment.ToString(),
                                ev.DateVersement.ToShortDateString(),
                                ev.NomEmployee,
                                ev.MontantVerse.ToString()
                            };

                            var listItems = new ListViewItem(items);
                            listView1.Items.Add(listItems);
                        }
                    }
                    else if (cmbTypeRapport.Text == "VERSEMENT PAR CAISSIER(E)")
                    {
                        var listeVersement = ConnectionClassClinique.ListeDesVersements();
                        var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
                        var liste = from v in listeVersement
                                    join em in listeEmploye
                                    on v.NumeroEmploye equals
                                    em.NumMatricule
                                    where v.DateVersement >= dateTimePicker1.Value.Date
                                    where v.DateVersement < dateTimePicker2.Value.Date.AddHours(24)
                                    where em.NomEmployee.ToUpper()==cmbNomEmpoye.Text.ToUpper()
                                    select new
                                    {
                                        em.NumMatricule,
                                        em.NomEmployee,
                                        v.DateVersement,
                                        v.MontantVerse,
                                        v.IDVersment
                                    };
                        listView1.Items.Clear();
                        foreach (var ev in liste)
                        {
                            var items = new string[]
                            {
                                ev.IDVersment.ToString(),
                                ev.DateVersement.ToShortDateString(),
                                ev.NomEmployee,
                                ev.MontantVerse.ToString()
                            };

                            var listItems = new ListViewItem(items);
                            listView1.Items.Add(listItems);
                        }
                    }
                    var montant = 0.0;
                    foreach (ListViewItem list in listView1.Items)
                    {
                        list.BackColor = list.Index % 2 == 0 ? Color.Bisque : Color.White;
                        montant += double.Parse(list.SubItems[3].Text);
                    }
                    label1.Text = "TOTAL : " + montant.ToString();
                }
                catch(Exception ex)
                {
                    MonMessageBox.ShowBox("Afficher raaport", ex);
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                rapportDepense = Impression.ImprimerRapportDepenses(listView1, "");
                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    //printDocument1.Print();
                    printPreviewDialog1.ShowDialog();
                }
            
            }
            catch { }
        }
        Bitmap rapportDepense;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportDepense, 20, 10, rapportDepense.Width, rapportDepense.Height);
            e.HasMorePages = false;
        }
        int idVersement;
        Versement CreerUnVersement()
        {
            try
            {
                var versement = new Versement();
                double montant;
                if (double.TryParse(txtPrix.Text, out montant))
                {
                    versement.MontantVerse = montant;
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le montant versé", "Erreur", "erreur.png");
                    return null;
                }
                if (!string.IsNullOrEmpty(cmbNomEmpoye.Text))
                {
                    var listeEmpl = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);
                    versement.NumeroEmploye = listeEmpl[0].NumMatricule;
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner un nom existant de la caissiere sur la liste", "Erreur", "erreur.png");
                    return null;
                }
                versement.IDVersment = idVersement;
                versement.DateVersement = dtpDebut.Value;
                return versement;
            }
            catch { return null; }
        }

        private void cmbNomEmpoye_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbNomEmpoye.Items.Clear();
                    var listeEmploye = ConnectionClassClinique.ListeDesEmployees(cmbNomEmpoye.Text);

                    if (listeEmploye.Count() > 0)
                        {
                            foreach (var empl in listeEmploye)
                            {
                                cmbNomEmpoye.Items.Add(empl.NomEmployee);
                            }
                            cmbNomEmpoye.DropDownStyle = ComboBoxStyle.DropDownList;
                            cmbNomEmpoye.DroppedDown = true;
                        }
                    
                }
            }
        }

        private void cmbNomEmpoye_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbNomEmpoye.DropDownStyle = ComboBoxStyle.Simple;
                            
        }

        
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        
    }
}
