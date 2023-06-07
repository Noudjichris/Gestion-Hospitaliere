using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class ListePerso : Form
    {
        public ListePerso()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListePerso_Load(object sender, EventArgs e)
        {
            dataGridView2.Focus();
            ListeEmploye();
        }
        public static string btnClick, mois, state, indiceAnciennete, fonction, indexRecherche, telePhone, numerMatricule, nomPersonnel, etatContrat, etatRetraite, sexe;
        public static ListePerso frm;
        public static DateTime dateNaisance;
        public static int exercice, numeroPaiement;
     
        public static string ShowBox()
        {
            frm = new ListePerso();
            frm.ShowDialog();
            return btnClick;
        }
        private void ListeEmploye()
        {
            try
            {

                dataGridView2.Rows.Clear();
                var dtPersonnel =GestionDuneClinique.AppCode.ConnectionClassSGSP.ListeDesPersonnelParNomPersonnel(indexRecherche);
                foreach (DataRow dtRow in dtPersonnel.Rows)
                {
                    var IndiceAnciennete = dtRow.ItemArray[17].ToString().ToUpper();
                    if (IndiceAnciennete.Contains("%"))
                    {
                        IndiceAnciennete = IndiceAnciennete.Remove(IndiceAnciennete.IndexOf("%"));
                    }
                    dataGridView2.Rows.Add(
                    dtRow.ItemArray[0].ToString(),
                     dtRow.ItemArray[1].ToString().ToUpper() + " " +
                     dtRow.ItemArray[2].ToString().ToUpper(),
                      dtRow.ItemArray[12].ToString().ToUpper(),
                     IndiceAnciennete,
                     dtRow.ItemArray[16].ToString().ToUpper(),
                     dtRow.ItemArray[22].ToString().ToUpper(),
                     dtRow.ItemArray[23].ToString().ToUpper(),
                     dtRow.ItemArray[24].ToString().ToUpper(),
                     dtRow.ItemArray[18].ToString().ToUpper(),
                     dtRow.ItemArray[19].ToString().ToUpper(),
                     dtRow.ItemArray[13].ToString().ToUpper(),
                     dtRow.ItemArray[25].ToString().ToUpper(),
                      dtRow.ItemArray[3].ToString().ToUpper(),
                                dtRow.ItemArray[9].ToString().ToUpper(),
                                   dtRow.ItemArray[6].ToString().ToUpper()
                     );
                }
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    var retraite = Convert.ToInt32(row.Cells[8].Value.ToString());
                    var finContrat = Convert.ToInt32(row.Cells[9].Value.ToString());
                    if (retraite == 1 || finContrat == 1)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;  //
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }

            }
            catch { }
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    indiceAnciennete = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                    numerMatricule = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    nomPersonnel = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                    telePhone = dataGridView2.SelectedRows[0].Cells[14].Value.ToString();
                    etatRetraite = dataGridView2.SelectedRows[0].Cells[9].Value.ToString();
                    etatContrat = dataGridView2.SelectedRows[0].Cells[8].Value.ToString();
                    fonction = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                    sexe = dataGridView2.SelectedRows[0].Cells[13].Value.ToString();
                    dateNaisance = DateTime.Parse(dataGridView2.SelectedRows[0].Cells[12].Value.ToString());
                    if (state == "1")
                    {
                        if (etatRetraite == "1")
                        {
                             GestionDuneClinique.MonMessageBox.ShowBox("L'employé ci est deja rétraité", "Erreur","erreur.png");
                            btnClick = "2"; Dispose();
                        }
                        else if (etatContrat == "1")
                        {
                        GestionDuneClinique.    MonMessageBox.ShowBox("L' employé ci a deja fini son contrat", "Erreur","erreur.png");
                            btnClick = "2"; Dispose();
                        }
                    }
                    else
                    {

                        btnClick = "1";
                        Dispose();
                    }
                   
                }
            }
            catch { }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView2_DoubleClick(null, null);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)

        {
            btnClick = "2";
            Dispose();
        }

        private void txtRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                indexRecherche = txtRef.Text;
                ListeEmploye();
                dataGridView2.Focus();
            }
        }

    }
}
