using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class listeDistributionFrm : Form
    {
        public listeDistributionFrm()
        {
            InitializeComponent();
        }

        private void listeCreditFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 192, 128), 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(64, 64, 64),
                Color.FromArgb(64, 64, 64), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.FromArgb(255, 192, 128), 5);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Green,
                Color.Green, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        static listeDistributionFrm frm;
        public  static string  btnClik, client, typeVente;
        public static int idClient, numVente, idPatient;
        private void listeCreditFrm_Load(object sender, EventArgs e)
        {
            ListeDesVentes();          
        }

        private void ListeDesVentes()
        {
            try
            {
                dgvVente.Rows.Clear();
                DataTable dt = AppCode.ConnectionClass.ListeDesVentes(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    var date = dt.Rows[i].ItemArray[1].ToString();
                    var heure = "00:00::00";

                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[5].ToString()))
                    {
                        heure = dt.Rows[i].ItemArray[5].ToString();
                    }
                    var dateVente = date.Substring(0, 10) + " " + heure;
                    string sivente = "";
                    if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[6].ToString()))
                    {
                        if (dt.Rows[i].ItemArray[6].ToString()=="1")
                        {
                            sivente = "COMPTANT";
                        }
                        else if (dt.Rows[i].ItemArray[6].ToString() == "0")
                        {
                            sivente = "CREDIT";
                        }
                    }
                  
                    dgvVente.Rows.Add(                   
                       dt.Rows[i].ItemArray[2].ToString().ToUpper(),
                       dateVente,
                       dt.Rows[i].ItemArray[0].ToString().ToUpper(),
                       dt.Rows[i].ItemArray[3].ToString(),
                       sivente,
                       dt.Rows[i].ItemArray[4].ToString(),
                        dt.Rows[i].ItemArray[7].ToString(),
                              dt.Rows[i].ItemArray[8].ToString(),
                                      dt.Rows[i].ItemArray[9].ToString()
                   ) ;
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Liste distribution", ex); }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvVente.SelectedRows.Count > 0)
                {
                    int numVente = Int32.Parse(dgvVente.SelectedRows[0].Cells[2].Value.ToString());
                    AppCode.ConnectionClass.SupprimerVentes(numVente );
                    ListeDesVentes();
                }
            }
            catch { }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ListeDesVentes();
        }

      
        public static string ShowBox()
        {
            frm = new listeDistributionFrm();
            frm.ShowDialog();
            return btnClik;
        }

        private void txtClient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    dgvVente.Rows.Clear();
                    var requete = "SELECT detail_vente.num_vente,detail_vente.date_vente,clientTbl.nomClient,detail_vente.prix_total, employe.nom_empl " +
                     ",detail_vente.heure,detail_vente.etat,clientTbl.id,clientTbl.entrep,clientTbl.matricule FROM detail_vente INNER JOIN clientTbl ON clientTbl.id = " +
                     " detail_vente.num_client INNER JOIN employe ON detail_vente.num_empl = employe.num_empl WHERE clientTbl.nomClient LIKE '%"+txtClient.Text +
                     "%'  ORDER BY clientTbl.nomClient , detail_vente.date_vente DESC";
                    DataTable dt = AppCode.ConnectionClass.TableRows(requete);
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        var date = dt.Rows[i].ItemArray[1].ToString();
                        var heure = "00:00::00";

                        if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[5].ToString()))
                        {
                            heure = dt.Rows[i].ItemArray[5].ToString();
                        }
                        string sivente = "";
                        if (!string.IsNullOrEmpty(dt.Rows[i].ItemArray[6].ToString()))
                        {
                            if (dt.Rows[i].ItemArray[6].ToString() == "1")
                            {
                                sivente = "COMPTANT";
                            }
                            else if (dt.Rows[i].ItemArray[6].ToString() == "0")
                            {
                                sivente = "CREDIT";
                            }
                        }
                        var dateVente = date.Substring(0, 10) + " " + heure;
                        dgvVente.Rows.Add(
                            dt.Rows[i].ItemArray[2].ToString().ToUpper(),
                            dateVente,
                            dt.Rows[i].ItemArray[0].ToString().ToUpper(),
                            dt.Rows[i].ItemArray[3].ToString(),
                            sivente,
                            dt.Rows[i].ItemArray[4].ToString(),
                              dt.Rows[i].ItemArray[7].ToString(),
                              dt.Rows[i].ItemArray[8].ToString(),
                                dt.Rows[i].ItemArray[9].ToString()
                         );
                    }
                }
                catch (Exception ex) { MonMessageBox.ShowBox("Liste distribution", ex); }
            }
        }

        private void dgvVente_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (dgvVente.SelectedRows.Count > 0)
                    {
                        idClient = Convert.ToInt32(dgvVente.SelectedRows[0].Cells[6].Value.ToString());
                        //if (Int32.TryParse(dgvVente.SelectedRows[0].Cells[8].Value.ToString(), out idPatient))
                        //{
                        //}
                        numVente = Convert.ToInt32(dgvVente.SelectedRows[0].Cells[2].Value.ToString());
                        typeVente = dgvVente.SelectedRows[0].Cells[4].Value.ToString();
                        client = dgvVente.SelectedRows[0].Cells[0].Value.ToString();
                        btnClik = "1";
                        Dispose();
                    }
                }
                catch (Exception ex) { MonMessageBox.ShowBox("listView2_ KeyDown", ex); }
            }
            catch { }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            ListeDesVentes();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVente.SelectedRows.Count > 0)
                {
                    if(Form1.typeUtilisateur =="admin")
                    if (MonMessageBox.ShowBox("Voulez vous supprimer cette vente ?", "Confirmation", "confirmation.png") == "1")
                    {
                        AppCode.ConnectionClass.SupprimerVentes(Int32.Parse(dgvVente.SelectedRows[0].Cells[2].Value.ToString()));
                      AppCode.  ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "annulation de la vente  numero " + dgvVente.SelectedRows[0].Cells[2].Value.ToString() + " de  " + dgvVente.SelectedRows[0].Cells[0].Value.ToString(), this.Name);
                        dgvVente.Rows.Remove(dgvVente.SelectedRows[0]);
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            btnClik = "2";
        }

        private void txtClient_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
