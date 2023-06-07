﻿using System;
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
            Pen pen1 = new Pen(Color.LightSkyBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.Blue, Color.FromArgb(0, 0, 192), LinearGradientMode.BackwardDiagonal);
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
                    if (ConnectionClass.AjouterFamilleMedicament(codeFamille, designation))
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
                    ConnectionClass.ModifierFamilleMedicament(codeFamille,designation);
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
            var  listeFamille = ConnectionClass.ListeDesFamille();
            foreach (Medicament familleMedicament in listeFamille)
            {
                string[] items = {familleMedicament.CodeFamille.ToString(), familleMedicament.Designation};
                ListViewItem lstListViewItem = new ListViewItem(items);
                
                listView1.Items.Add(lstListViewItem);
            }
            foreach(ListViewItem item in listView1.Items)
                item .BackColor = item.Index % 2 == 0 ? Color.White : Color.FromArgb(255, 192, 128);

        }
        private void FamilleMedicamentFrm_Load(object sender, EventArgs e)
        {
            ListeFamilleMedicament();
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
            }
            catch (Exception )
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
      
    }
}
