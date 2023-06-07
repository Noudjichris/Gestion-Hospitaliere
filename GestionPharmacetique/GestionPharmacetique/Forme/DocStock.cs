using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDuneClinique.Formes;
using GestionPharmacetique.AppCode;

namespace SGDP.Formes
{
    public partial class DocStock : UserControl
    {
        public DocStock()
        {
            InitializeComponent();
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen  (Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SkyBlue
                , Color.SkyBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen (SystemColors.ControlLight, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                , SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void LogStockUser_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                , SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        public int etat;
        private void LogStockUser_Load(object sender, EventArgs e)
        {
            try
            {
               Column5.Width =  200;
               Column1.Width = 120;
               Column4.Width = 300;
               Column3.Width = 1*dataGridView1.Width/3;
                button3.Location = new Point(Width - 40, button3.Location.Y);
                if (GestionPharmacetique.Form1.typeUtilisateur == "intermediaire" || GestionPharmacetique.Form1.typeUtilisateur == "admin")
                {
                    button4.Enabled = true;
                }
                else if (GestionPharmacetique.Form1.typeUtilisateur == "caissier")
                {
                    button6.Enabled = false;
                    button4.Enabled = false;
                }
                var liste = ConnectionClass.ListeDocumentStock(etat);
                foreach (var d in liste)
                {
                    var typePrix = "";
                    if(d.TypePrix==1)
                    {
                        typePrix="prix achat";
                    }else if(d.TypePrix==2)
                    {
                        typePrix ="prix vente";
                    }
                    dataGridView1.Rows.Add(d.IDReference, d.Reference, d.Date.ToShortDateString(), d.Source, d.Destination,typePrix);
                }
            }
            catch(Exception ex)
            { GestionPharmacetique.MonMessageBox.ShowBox("Forme load", ex); }
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        private void button3_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            { 
                dataGridView1.Rows.Clear();
                var document = new DocumentStock();
                document.Date = DateTime.Now;
                document.Reference = "";
                document.Source = "";
                document.Destination = "";
                document.Etat = etat;
                
                    if (GestionPharmacetique.Form1.typeUtilisateur == "caissier")
                    {
                        button6.Enabled = false;
                        return;
                    }
                if (ConnectionClass.CreerUnNouveauDocumentStock(document))
                {
                    ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Creation d'un nouveau document pour le mouvement de  stock",this.Name);
                    var liste = ConnectionClass.ListeDocumentStock(etat);
                    foreach (var d in liste)
                    {
                        dataGridView1.Rows.Add(d.IDReference, d.Reference, d.Date.ToShortDateString(), d.Source, d.Destination);
                    }
                    dataGridView1.Focus();
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
                    dataGridView1.BeginEdit(true);
                }

            }catch(Exception )
                {
                }
        }

    
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                    var document = new DocumentStock();
                    document.IDReference = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    document.Date = DateTime.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    document.Reference = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    document.Source = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    document.Destination = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                    if (ConnectionClass.MettreAjourDocumentStock(document))
                    {
                        dataGridView1.Rows.Clear();
                        var liste = ConnectionClass.ListeDocumentStock(etat);
                        foreach (var d in liste)
                        {
                            dataGridView1.Rows.Add(d.IDReference, d.Reference, d.Date.ToShortDateString(), d.Source, d.Destination);
                        }
                    }
                //}
            }
            catch (Exception )
            {
                //MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");

            } 
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    //var frm = new GestionPharmacetique.Forme.FrmType();
                    GestionPharmacetique.Forme.FrmType.numeroPiece = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    GestionPharmacetique.Forme.FrmType.typePrix =dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

                    GestionPharmacetique.Forme.FrmType.etat = etat;
                    if (GestionPharmacetique.Forme.FrmType.ShowBox())
                    {
                    }
                }
            }
            catch { }
        }

        private void txtReference_TextChanged(object sender, EventArgs e)
        {
         
            {
                dataGridView1.Rows.Clear();
                var liste = from f in ConnectionClass.ListeDocumentStock(etat)
                                where f.Reference.ToUpper().Contains(txtReference.Text.ToUpper())
                                select f;
                foreach (var d in liste)
                {
                    dataGridView1.Rows.Add(d.IDReference, d.Reference, d.Date.ToShortDateString(), d.Source, d.Destination);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (GestionPharmacetique.Form1.typeUtilisateur != "caissier")
                {
                    button4.Enabled = false;
                    return;
                }
                if (GestionPharmacetique.MonMessageBox.ShowBox("Ce rubrique contient peut être des données. Etes vous surs de vouloir supprimer", "Confirmation", "confirmation")=="1")
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox(" Etes vous sur de vouloir continuer", "Confirmation", "confirmation") == "1")      
                    if (GestionPharmacetique.AppCode.ConnectionClass.SupprimerUnNouveauDocumentStock(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())))
                    {
                        ConnectionClass.InsererDansLog(GestionPharmacetique.Form1.nomEmploye, "Suppression des documents pour le mouvement de  stock reference : "+dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), this.Name);
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                GestionPharmacetique.Forme.PrixParamFrm.idDoc =Int32.Parse( dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                if(GestionPharmacetique.Forme.PrixParamFrm.ShowBox())
                {
                    dataGridView1.Rows[e.RowIndex].Cells[5].Value = GestionPharmacetique.Forme.PrixParamFrm.typePrix;
                }
            }
        }
 }
}
