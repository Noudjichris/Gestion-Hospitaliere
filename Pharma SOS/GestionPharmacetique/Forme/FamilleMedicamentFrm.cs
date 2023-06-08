using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace GestionPharmacetique
{
    public partial class FamilleMedicamentFrm : Form
    {
        public FamilleMedicamentFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        //ajouter une nouvelle famille de medicament
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            int  codeFamille;
            string designation;
            if (Int32.TryParse(txtNumeroFamille.Text, out codeFamille))
            {
                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    designation = txtDescription.Text;
                    var idGroupe = -1;
                    var liste= ConnectionClass.ListeDesGroupe().Where(g=>g.Groupe==comboBox1.Text);
                    foreach (var g in liste)
                        idGroupe = g.CodeFamille;

                    if (ConnectionClass.AjouterFamilleMedicament(codeFamille, designation,idGroupe))
                    {
                        MonMessageBox.ShowBox(
                           "nouveau type de médicament a été inseré avec succés dans la base de données",
                           "Information Insertion", "affirmation.png");
                        txtDescription.Text = "";
                        txtNumeroFamille.Text = "";
                        ListeFamilleMedicament();
                    }
                }
                else MonMessageBox.ShowBox("Veuillez entrer des chiffres valides pour le code famille.", "Erreur saisie",
                        "erreur.png");
            }
            else MonMessageBox.ShowBox("Veuillez entrer la désignation pour la famille de médicament.", "Erreur saisie", "erreur.png");
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
              int   codeFamille;
            string designation;
            if (Int32.TryParse(txtNumeroFamille.Text, out codeFamille))
            {
                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    designation = txtDescription.Text;
                    var idGroupe = -1;
                    var liste = ConnectionClass.ListeDesGroupe().Where(g => g.Groupe == comboBox1.Text);
                    foreach (var g in liste)
                        idGroupe = g.CodeFamille;
                    ConnectionClass.ModifierFamilleMedicament(codeFamille,designation,idGroupe);
                    txtDescription.Text = "";
                    txtNumeroFamille.Text = "";
                    ListeFamilleMedicament();
                }
                else MonMessageBox.ShowBox("Veuillez entrer la désignation pour la famille de médicament.", "Erreur saisie", "erreur.png");
            }
            else MonMessageBox.ShowBox("Veuillez entrer des chiffres valides pour le code famille.", "Erreur saisie",
                        "erreur.png");
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //liste de famille medicament
        private void ListeFamilleMedicament()
        {
            listView1.Items.Clear();
            var listeFamille = from f in ConnectionClass.ListeDesFamille()
                               join g in ConnectionClass.ListeGroupe()
                               on f.NombreDetail equals g.CodeFamille
                               select new
                               {
                                   f.CodeFamille,
                                   f.Designation,
                                   g.Groupe
                               };                              ;
            foreach (var familleMedicament in listeFamille)
            {
               string[] items = {familleMedicament.CodeFamille.ToString(), familleMedicament.Designation,familleMedicament.Groupe};
                ListViewItem lstListViewItem = new ListViewItem(items);                
                listView1.Items.Add(lstListViewItem);
            }
            foreach(ListViewItem item in listView1.Items)
                item .BackColor = item.Index % 2 == 0 ? Color.White : Color.AliceBlue;

        }
        private void FamilleMedicamentFrm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("");
            ListeFamilleMedicament();
            foreach (var g in AppCode.ConnectionClass.ListeDesGroupe())
                comboBox1.Items.Add(g.Groupe);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var  listeFamille = from f in ConnectionClass.ListeDesFamille()
                                            where f.CodeFamille == Int32.Parse(listView1.SelectedItems[0].SubItems[0].Text )
                                             select f;
                foreach (Medicament famille in listeFamille)
                {
                    txtDescription.Text = famille.Designation;
                    txtNumeroFamille.Text = famille.CodeFamille.ToString().ToUpper();

                }
                comboBox1.Text = "";
                comboBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;
            }
            catch (Exception )
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new GestionPharmacetique.Forme.GroupeFrm();
            frm.ShowDialog();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("");
            foreach (var g in AppCode.ConnectionClass.ListeDesGroupe())
                comboBox1.Items.Add(g.Groupe);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
