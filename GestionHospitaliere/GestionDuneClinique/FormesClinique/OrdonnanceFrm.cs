using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace GestionDuneClinique.AppCode
{
    public partial class OrdonnanceFrm : Form
    {
        public OrdonnanceFrm()
        {
            InitializeComponent();
        }

 

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void OrdonnanceFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 5);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox3.Width - 1, groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox4.Width - 1, groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        
        private void OrdonnanceFrm_Load(object sender, EventArgs e)
        {
            var listePatient = ConnectionClassClinique.ListeDesPatients();
            //ListeDesPatients(listePatient);

            var dt =ConnectionClassPharmacie.TableMedicament();
            ListeMedicament(dt);

            var listeEmploye = ConnectionClassClinique.ListeDesEmployees();
            foreach (var empl in listeEmploye)
            {
                cmbMedecin.Items.Add(empl.NomEmployee);
            }
        }

        //liste des patients
        void ListeDesPatients(List<Patient> listePatient)
        {
            listView2.Items.Clear();
            foreach (Patient patient in listePatient)
            {
                var items = new string[]
                {
                    patient.NumeroPatient.ToString(),
                    patient.Nom,
                    patient.Prenom
                };

                var lstItems = new ListViewItem(items);
                listView2.Items.Add(lstItems);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        //liste des medecimanets
        void ListeMedicament(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow dtRow in dt.Rows)
            {
                dataGridView1.Rows.Add(dtRow.ItemArray[0].ToString());
            }
        }

        //rechercher medic
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var dt = ConnectionClassPharmacie.TableMedicament(textBox2.Text);
            ListeMedicament(dt);
        }
        string etat;
        //rechercher patient
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var listePatient = ConnectionClassClinique.ListeDesPatients(textBox1.Text);
            ListeDesPatients(listePatient);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                bool found = false;
                if (dgvOrdon.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dtGrid in dgvOrdon.Rows)
                    {
                        if (dtGrid.Cells[0].Value.ToString().Equals(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        dgvOrdon.Rows.Add(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                            "0",                   "0",                          "0");
                    }

                }
                else
                {
                    dgvOrdon.Rows.Add(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                            "0", "0", "0");
                }
                etat = "1";
                btnEnregistrer.Enabled = true;
            }
            catch (Exception) { }  
        }

        //retirer produit
        private void btnAjouter_Click(object sender, EventArgs e)
        {  
            if (etat == "1")
            {
                if (dgvOrdon.SelectedRows.Count > 0)
                {
                    dgvOrdon.Rows.Remove(dgvOrdon.SelectedRows[0]);
                }
            }
            else if (etat == "2")
            {
                if (dgvOrdon.SelectedRows.Count > 0)
                {
                    var medi = dgvOrdon.SelectedRows[0].Cells[0].Value.ToString();
                    if (MonMessageBox.ShowBox("Voulez vous retirer le medicament  " + medi +" de l'ordonnance ?", "Confirmation", "confirmation.png") == "1")
                    {
                        
                        dgvOrdon.Rows.Remove(dgvOrdon.SelectedRows[0]);
                        ConnectionClassClinique.SupprimerUneOrdonnance(idOrdon, medi);
                    }
                }
            }
           
        }

        int idPatient, idOrdon;
        string numEmpl;
        DateTime dateOrdon;
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (cmbMedecin.Text != "")
            {
                if (listView2.SelectedItems.Count>0)
                { idPatient = Int32.Parse(listView2.SelectedItems[0].SubItems[0].Text);
                }
                else if (idPatient > 0)
                {
                    //idPatient = idPatient;
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le nom patient sur la liste", "Erreur", "erreur.png");
                    return ;
                } 
                var nomMedecin= cmbMedecin.Text;
                var listeEmploye = ConnectionClassClinique.ListeDesEmployees("nom_empl", nomMedecin);
                numEmpl = listeEmploye[0].NumMatricule;

                dateOrdon = dtpOrdon.Value;
                var ordonnance = new Ordonnance(idOrdon, dateOrdon, idPatient, numEmpl);
                if (etat == "1")
                {
                    if (ConnectionClassClinique.AjouterUneOrdonnance(ordonnance, dgvOrdon))
                    {
                        dgvOrdon.Rows.Clear();
                        cmbMedecin.Text = "";
                        textBox2.Text = "";
                        dtpOrdon.Value = DateTime.Now;
                    }
                }
                else if(etat=="2")
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier les données de l'ordonnance du patient?", "Confirmation", "confirmation.png") == "1")
                    {
                        if (ConnectionClassClinique.ModifierUneOrdonnance(ordonnance, dgvOrdon))
                        {
                            dgvOrdon.Rows.Clear();
                            cmbMedecin.Text = "";
                            textBox2.Text = "";
                            dtpOrdon.Value = DateTime.Now;
                        }
                    }
                }
            }
            else
            {
                MonMessageBox.ShowBox("Veuillez selectionner le nom du medecin sur la liste deroulante", "Erreur", "erreur.png");
            }
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            if (GestionDuneClinique.Formes.ListeOrdoFrm.ShowBox() == "1")
            {
                etat = "2";
                dgvOrdon.Rows.Clear();
                idOrdon = GestionDuneClinique.Formes.ListeOrdoFrm.id;
                var listeDetail = ConnectionClassClinique.ListeDetailOrdonnances(idOrdon);
                cmbMedecin.DropDownStyle = ComboBoxStyle.DropDown;
                cmbMedecin.Text = GestionDuneClinique.Formes.ListeOrdoFrm.employe;
                idPatient = GestionDuneClinique.Formes.ListeOrdoFrm.idPatient;
                btnEnregistrer.Enabled = true;
                foreach (Ordonnance ordon in listeDetail)
                {
                    dgvOrdon.Rows.Add(ordon.Medicament,
                        ordon.Quantite,
                        ordon.NombreDeFois,
                        ordon.Jour
                        );
                }
            }
            
        }

        Bitmap ordoBitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
             e.Graphics.DrawImage(ordoBitmap, -8, 0, ordoBitmap.Width, ordoBitmap.Height);
            e.HasMorePages = false;
        }

        //apercu
        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvOrdon.Rows.Count > 0 && idOrdon >= 0)
            {
                var listePatient = from p in ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == idPatient
                                   select p;

                var patient = new Patient();
                foreach (var p in listePatient)
                    patient = p;

                ordoBitmap = Impression.ImprimerOrdonnance(idOrdon, dgvOrdon, patient,cmbMedecin.Text);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
             printPreviewDialog1.Document = printDocument1;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    bool found = false;
                    if (dgvOrdon.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dtGrid in dgvOrdon.Rows)
                        {
                            if (dtGrid.Cells[0].Value.ToString().Equals(textBox2.Text))
                            {
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            dgvOrdon.Rows.Add(textBox2.Text,
                                "0", "0", "0");
                        }

                    }
                    else
                    {
                        dgvOrdon.Rows.Add(textBox2.Text,
                                "0", "0", "0");
                    }
                    etat = "1";
                    btnEnregistrer.Enabled = true;
                }
                catch { }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgvOrdon.Rows.Clear();
            textBox2.Text = "";
            dtpOrdon.Value = DateTime.Now;
            cmbMedecin.Text = "";
        }
    }
}
