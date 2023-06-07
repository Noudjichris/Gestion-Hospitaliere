using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using GestionPharmacetique.AppCode;

namespace GestionPharmacetique.Forme
{
    public partial class DepenseFrm : Form
    {
        public DepenseFrm()
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
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.MidnightBlue, Color.CadetBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
        string etat = null; int id;
        private void i_Click(object sender, EventArgs e)
        {
            try
            {
                double montant;
                if (double.TryParse(txtPrix.Text, out montant))
                {

                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le montant", "Erreur","erreur.png");
                    return;
                }
                if (idLibelle > 0)
                {
                    var depense = new Depenses();
                    depense.ID = idLibelle;
                    depense.Date = dtpDebut.Value;
                    depense.IDDepense = idDepense;
                    depense.Beneficiaire = txtBeneficiaire.Text;
                    depense.Montant = montant;
                    depense.NumeroFacture = txtFacture.Text;
                    if (etat != null)
                    {
                        if (ConnectionClass.ModifierUneDepense(depense))
                        {
                            etat = null;
                            txtBeneficiaire.Text = "";
                            txtFacture.Text = "";
                            txtPrix.Text = "";
                            ListeDesDepensesEffectues();
                        }
                    }
                    else
                    {
                        if (ConnectionClass.EnregistrerUneDepense(depense))
                        {
                            etat = null;
                            txtBeneficiaire.Text = "";
                            txtFacture.Text = "";
                            txtPrix.Text = "";
                            ListeDesDepensesEffectues();
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le libellé sur la liste ci haut ", "Erreur","erreur.png");
                }
                
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Enregistrer depense", ex); }
        }

        private void DepenseFrm_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("NUM", 0);
            listView1.Columns.Add("NUM LIB", 0);
            listView1.Columns.Add("CATEGORIE", listView1.Width / 7);
            listView1.Columns.Add("DATE", listView1.Width / 8);
            listView1.Columns.Add("N° FACTURE", listView1.Width / 8);
            listView1.Columns.Add("LIBELLE", listView1.Width / 3 );
            listView1.Columns.Add("MONTANT", listView1.Width / 9);
            listView1.Columns.Add("BENEFICIAIRE", listView1.Width /4);
            Column2.Width = dgvDepenses.Width / 4;
            ListeLibelle();
            ListeDesDepensesEffectues();
        }

       
        private void ListeLibelle()
        {
            try
            {
                var liste = ConnectionClass.ListeLibelle();
                var q = from l in liste
                        where l.Categorie.StartsWith(comboBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                        orderby l.Categorie, l.Libelle                       
                        select l;
                dgvDepenses.Rows.Clear();
                foreach(var l  in q)
                {
                    dgvDepenses.Rows.Add(l.ID, l.Categorie, l.Libelle);
                }
            }
            catch { }
        }
        private void btnSupprimerHospi_Click(object sender, EventArgs e)
        {
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous suprimer les donnees de cette depense?", "Confirmation", "confirmation.png") == "1")
                {
                    int numero ;
                    if (Int32.TryParse(listView1.SelectedItems[0].SubItems[0].Text, out numero))
                    {
                        ConnectionClass.SupprimerUneDepense(numero);
                        txtFacture.Text = "";
                        txtPrix.Text = "";
                        ListeDesDepensesEffectues();
                    }
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                listView1.Items.Clear();
                listView2.Items.Clear();
                listView2.Visible = false;
                listView1.Visible = true;

                var libelle = ConnectionClass.ListeLibelle();
                var q = from l in libelle
                        orderby l.Categorie
                        group l by l.Categorie;

                var montantTotal = 0.0;
                foreach (var l in q)
                {
                    var count = 0;

                    var p = from c in libelle
                            where c.Categorie == l.Key
                            orderby c.Libelle
                            select c;
                    var montant = 0.0;
                    var items11 = new string[]
                            {
                                "","",l.Key,"", "","","",""
                            };
                    var listItems11 = new ListViewItem(items11);
                    listView1.Items.Add(listItems11);
                    foreach (var lib in p)
                    {
                        var listeDepense = ConnectionClass.ListeDesDepenses();
                        var liste = from ld in listeDepense
                                    join bl in libelle on
                                    ld.ID equals bl.ID
                                    orderby bl.Categorie, bl.Libelle, ld.Date
                                    where bl.ID == lib.ID
                                    where ld.Date >= dateTimePicker1.Value.Date
                                    where ld.Date <dateTimePicker2.Value.Date.AddHours(24)
                                    select new
                                    {
                                        bl.Categorie,
                                        ld.IDDepense,
                                        bl.ID,
                                        ld.Date,
                                        ld.Beneficiaire,
                                        ld.Montant,
                                        ld.NumeroFacture,
                                        bl.Libelle
                                    };
                        foreach (var d in liste)
                        {
                            var items = new string[]
                            {
                                d.IDDepense.ToString(),d.ID.ToString(),"",d.Date.ToShortDateString(), d.NumeroFacture,d.Libelle ,String.Format(elGR, "{0:0,0}",d.Montant),d.Beneficiaire
                            };
                            var listItems = new ListViewItem(items);
                            listView1.Items.Add(listItems);
                            montant += d.Montant;
                            montantTotal += d.Montant;
                        }
                        count = liste.Count();
                    }
                    //if (count > 0)
                    //{
                    var items1 = new string[]
                            {
                                "","","Sous total","", "","",String.Format(elGR, "{0:0,0}",String.Format(elGR, "{0:0,0}",montant)),""
                            };
                    var listItems1 = new ListViewItem(items1);
                    listView1.Items.Add(listItems1);
                    var items2 = new string[]
                            {
                                "","","","", "","","",""
                            };
                    var listItems2 = new ListViewItem(items2);
                    listView1.Items.Add(listItems2);
                    ////}
                }

                var items3 = new string[]
                            {
                                "","","TOTAL","", "","",String.Format(elGR, "{0:0,0}",montantTotal ),""
                            };
                var listItems3 = new ListViewItem(items3);
                listView1.Items.Add(listItems3);

                foreach (ListViewItem lstItems in listView1.Items)
                {
                    if (string.IsNullOrEmpty(lstItems.SubItems[0].Text) && lstItems.SubItems[2].Text == "SOUS TOTAL")
                    {
                        //lstItems.BackColor = Color.LightSkyBlue; 
                        lstItems.Font = new System.Drawing.Font("Arial unicode", 12F, System.Drawing.FontStyle.Regular |
                           FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }
                    else if (string.IsNullOrEmpty(lstItems.SubItems[0].Text) &&
                        lstItems.SubItems[2].Text != "SOUS TOTAL" && lstItems.SubItems[2].Text != "")
                    {
                        lstItems.ForeColor = SystemColors.ControlLightLight;
                        lstItems.BackColor = SystemColors.ActiveCaption; 
                        lstItems.Font = new System.Drawing.Font("Arial unicode", 12F, System.Drawing.FontStyle.Regular |
                            FontStyle.Bold | FontStyle.Underline | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Afficher raaport", ex);
            }
        }

        Bitmap rapportDepense;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportDepense, 0, 10, rapportDepense.Width, rapportDepense.Height);
            e.HasMorePages = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                     id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                    var dt = ConnectionClass.ListeDesDepenses(id);
                    if (dt.Rows.Count > 0)
                    {
                        dgvDepenses.Rows.Clear();
                        //label8.Text = listView1.SelectedItems[0].SubItems[2].Text;
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            dgvDepenses.Rows.Add(dt.Rows[i].ItemArray[1].ToString(),
                                dt.Rows[i].ItemArray[2].ToString(),
                                dt.Rows[i].ItemArray[3].ToString()
                                );
                        }
                        etat = "1";
                    }
                }
            }
            catch { }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text != "")
                {
                    dgvDepenses.Rows.Add(0, comboBox1.Text, textBox1.Text);
                    textBox1.Text = "";
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (ConnectionClass.EnregistrerUnLibelle(dgvDepenses))
            {
                MonMessageBox.ShowBox("Données enregistrées avec succés ", "Affirmation", "affirmation.png");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeLibelle();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation", "confirmation.png") == "1")
            if (ConnectionClass.SuprimerUnLibelle(dgvDepenses))
            {
                ListeLibelle();
            }
        }

        int idLibelle ,idDepense;
        private void dgvDepenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDepenses.SelectedRows.Count > 0)
                    textBox3.Text = dgvDepenses.SelectedRows[0].Cells[2].Value.ToString();
                    idLibelle = Convert.ToInt32(dgvDepenses.SelectedRows[0].Cells[0].Value.ToString());
            }
            catch { }
        }

        private void ListeDesDepensesEffectues()
        {
            try
            {
                var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                listView1.Items.Clear();
                var totalDepenses = 0.0;
                var libelle = ConnectionClass.ListeLibelle();
                var q = from l in libelle
                        orderby l.Categorie
                        group l by l.Categorie;
                
                foreach (var l in q)
                {
                    var count = 0;

                    var p = from c in libelle
                            where c.Categorie == l.Key
                            orderby c.Libelle
                            select c;
                    var montant = 0.0;
                    var items11 = new string[]
                            {
                                "","",l.Key,"", "","","",""
                            };
                    var listItems11 = new ListViewItem(items11);
                    listView1.Items.Add(listItems11);
                    foreach(var lib in p)
                    {
                        var listeDepense = ConnectionClass.ListeDesDepenses();
                        var liste = from ld in listeDepense
                                    join bl in libelle on
                                    ld.ID equals bl.ID
                                    orderby bl.Categorie, bl.Libelle,ld.Date
                                    where bl.ID == lib.ID
                                    select new
                                    {
                                       bl.Categorie, ld.IDDepense, bl.ID, ld.Date,ld.Beneficiaire,ld.Montant,ld.NumeroFacture,bl.Libelle
                                    };
                        foreach (var d in liste)
                        {

                           
                                var items = new string[]
                            {
                                d.IDDepense.ToString(),d.ID.ToString(),"",d.Date.ToShortDateString(), d.NumeroFacture,d.Libelle ,String.Format(elGR, "{0:0,0}",d.Montant),d.Beneficiaire
                            };
                                var listItems = new ListViewItem(items);
                                listView1.Items.Add(listItems);

                                montant += d.Montant;
                                totalDepenses += d.Montant;
                            //}
                            //else
                            //{

                            //}
                        }
                        count = liste.Count();
                    }
                    //if (count > 0)
                    if (montant > 0)
                    {
                        var items1 = new string[]
                            {
                                "","","Sous total","", "","",String.Format(elGR, "{0:0,0}",montant),""
                            };
                        var listItems1 = new ListViewItem(items1);
                        listView1.Items.Add(listItems1);
                    }
                    else
                    {           
                        listView1.Items.RemoveAt(listView1.Items.Count - 1);
                    }
                        //var items2 = new string[]
                        //    {
                        //        "","","","", "","","",""
                        //    };
                        //var listItems2 = new ListViewItem(items2);
                        //listView1.Items.Add(listItems2);
                    ////}
                }
                var items111 = new string[]
                            {
                                "","","Total","", "","",String.Format(elGR, "{0:0,0}",totalDepenses),""
                            };
                var listItems111 = new ListViewItem(items111);
                listView1.Items.Add(listItems111);

                foreach (ListViewItem lstItems in listView1.Items)
                {
                    if (string.IsNullOrEmpty(lstItems.SubItems[0].Text) &&
                        lstItems.SubItems[2].Text != "Sous total" && lstItems.SubItems[2].Text !="")
                    {
                        lstItems.ForeColor = Color.Black;
                        lstItems.BackColor = SystemColors.ActiveCaption; ;
                        lstItems.Font = new System.Drawing.Font("Arial unicode", 12F, System.Drawing.FontStyle.Regular |
                            FontStyle.Bold | FontStyle.Underline | FontStyle.Italic , System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }
                    else if (string.IsNullOrEmpty(lstItems.SubItems[0].Text) &&
                       lstItems.SubItems[2].Text == "Sous total" )
                    {
                        lstItems.Font = new System.Drawing.Font("Arial unicode", 12F, System.Drawing.FontStyle.Regular |
                              FontStyle.Bold | FontStyle.Underline | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste Depenses", ex);
            }

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    var listeDepense = ConnectionClass.ListeDesDepenses();
                    
                    if (Int32.TryParse(listView1.SelectedItems[0].SubItems[0].Text, out idDepense))
                    {
                        var q = from l in listeDepense
                                where l.IDDepense == idDepense
                                select l;
                        foreach (var l in q)
                        {
                            txtBeneficiaire.Text = l.Beneficiaire;
                            txtFacture.Text = l.NumeroFacture;
                            txtPrix.Text = l.Montant.ToString();
                            dtpDebut.Value = l.Date;
                            idLibelle = l.ID;
                            idDepense = l.IDDepense;
                        }
                        etat = "modif";
                    }
                    else
                    {
                        txtBeneficiaire.Text = "";
                        txtFacture.Text = "";
                        txtPrix.Text = "";
                        dtpDebut.Value = DateTime.Now ;
                        idLibelle = 0;
                        etat = null;
                    }
                }
            }

            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var mois = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                    sfd.FileName = "Jounal_des_depenses_du_" + date + ".pdf";
                
                if (listView1.Visible == true)
                {
                    if (listView1.Items.Count > 0)
                    {
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var Count = listView1.Items.Count / 45;
                            var titre = "Journal des dépenses du " + dateTimePicker1.Value.ToShortDateString() +
                                " au " + dateTimePicker2.Value.ToShortDateString();
                            if (listView1.Items.Count > 45)
                            {
                                for (var i = 0; i <= Count; i++)
                                {
                                    if (i * 45 < listView1.Items.Count)
                                    {
                                        var _listeImpression = Imprimer.ImprimerRapportDepenses(listView1, titre, i);

                                        var inputImage = @"cdali" + i;
                                        // Create an empty page
                                        sharpPDF.pdfPage pageIndex = document.addPage();

                                        document.addImageReference(_listeImpression, inputImage);
                                        sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                                document.createPDF(sfd.FileName);
                                System.Diagnostics.Process.Start(sfd.FileName);
                            }
                        }
                    }
                }
                else if (listView2.Visible == true)
                {
                    if (listView2.Items.Count > 0)
                    {
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var Count = listView2.Items.Count / 45;
                            var titre = "Journal des dépenses groupé par libellé du " + dateTimePicker1.Value.ToShortDateString() +
                                " au " + dateTimePicker2.Value.ToShortDateString();
                            if (listView2.Items.Count > 45)
                            {
                                for (var i = 0; i <= Count; i++)
                                {
                                    if (i * 45 < listView2.Items.Count)
                                    {
                                        var _listeImpression = Imprimer.ImprimerRapportDepensesGroupeParLibelle(listView2,titre, i);

                                        var inputImage = @"cdali" + i;
                                        // Create an empty page
                                        sharpPDF.pdfPage pageIndex = document.addPage();

                                        document.addImageReference(_listeImpression, inputImage);
                                        sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                                document.createPDF(sfd.FileName);
                                System.Diagnostics.Process.Start(sfd.FileName);
                            }
                        }
                    }
                }

            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = ConnectionClass.ListeLibelle();
                var q = from l in liste
                        where l.Libelle.StartsWith(textBox2.Text, StringComparison.CurrentCultureIgnoreCase)
                        orderby l.Categorie, l.Libelle
                        select l;
                dgvDepenses.Rows.Clear();
                foreach (var l in q)
                {
                    dgvDepenses.Rows.Add(l.ID, l.Categorie, l.Libelle);
                }
            }
            catch { }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                listView1.Visible = false;
                listView2.Visible = true;
                listView2.Items.Clear();

                var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                listView2.Columns.Add("Libellé", 2*listView2.Width / 3);
                listView2.Columns.Add("Montant", listView2.Width / 3);
                var listeDepense = ConnectionClass.ListeDesDepensesGrouperParLibelle(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                var total = 0.0;
                for (var i = 0; i < listeDepense.Rows.Count; i++)
                {
                    var items = new string[]
                            {
                                listeDepense.Rows[i].ItemArray[0].ToString(),
                                String.Format(elGR, "{0:0,0}",double.Parse(listeDepense.Rows[i].ItemArray[1].ToString()))
                            };
                    total += double.Parse(listeDepense.Rows[i].ItemArray[1].ToString());
                    var listItems = new ListViewItem(items);
                    listView2.Items.Add(listItems);
                }
                var items1 = new string[]
                            {
                                "Total",
                                String.Format(elGR, "{0:0,0}",total)
                            };
                var listItems2 = new ListViewItem(items1);
                listView2.Items.Add(listItems2);
                foreach (ListViewItem lstItems in listView2.Items)
                {
                    lstItems.BackColor = lstItems.Index % 2 == 0 ? Color.AliceBlue : Color.White;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Afficher raaport", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                var dgView = new DataGridView();
                if (listView1.Visible == true)
                {
                    dgView.Columns.Clear();
                    dgView.Columns.Add("c1", "Catégorie");
                    dgView.Columns.Add("c2", "Numéro");
                    dgView.Columns.Add("c3", "Date");
                    dgView.Columns.Add("c4", "Libellé");
                    dgView.Columns.Add("c5", "Montant");
                    //dgView.Columns.Add("c6", "");

                    var j =0;
                    for (var i = 0; i < listView1.Items.Count; i++)
                    {
                        int c;
                        if (listView1.Items[i].SubItems[3].Text.ToUpper() != "")
                        {
                            c=1;
                        }else
                        {
                            c=0;
                        }
                        string num;
                        j = j+c;
                        if (c == 0)
                        { 
                            num = ""; 
                        }
                        else
                        {
                            num = j.ToString();
                        }
                        dgView.Rows.Add(listView1.Items[i].SubItems[2].Text, num ,
                            listView1.Items[i].SubItems[3].Text,
                            listView1.Items[i].SubItems[5].Text,
                            listView1.Items[i].SubItems[6].Text
                            );
                       
                    }
                    sfd.FileName = "Journal_Des_Depenses_detaillées_Impriméé_le_" + date + ".xls";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        
                        ToCsV1(dgView, sfd.FileName); // Here dataGridview1 is your grid view name
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                else if (listView2.Visible == true)
                {
                    dgView.Columns.Add("c2", "Numéro");
                    dgView.Columns.Add("c4", "Libellé");
                    dgView.Columns.Add("c5", "Montant");
                    
                    for (var i = 0; i < listView2.Items.Count; i++)
                    {
                        dgView.Rows.Add(i+1,
                            listView2.Items[i].SubItems[0].Text,
                            listView2.Items[i].SubItems[1].Text);
                    }
                    sfd.FileName = "Journal_Des_Depenses_detaillées_Impriméé_le_" + date + ".xls";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                       ToCsV1(dgView, sfd.FileName); // Here dataGridview1 is your grid view name
                       System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dGV.Columns.Count; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dGV.Rows[i].Cells.Count; j++)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(1254);
                byte[] output = utf16.GetBytes(stOutput);
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void ToCsV1(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}