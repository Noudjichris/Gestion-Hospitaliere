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
            Pen pen1 = new Pen  (Color.SteelBlue, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue
                , Color.SlateGray, LinearGradientMode.ForwardDiagonal);
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
            Pen pen1 = new Pen(Color.SteelBlue, 3);
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
               Column1.Width = 200;
               Column3.Width = 2*dataGridView1.Width/5;
                button3.Location = new Point(Width - 43, button3.Location.Y);
                
                var liste = ConnectionClass.ListeDocumentStock();
                foreach (var d in liste)
                {
                    dataGridView1.Rows.Add(d.ID, d.Reference, d.Date.ToShortDateString(), d.Source, d.Destination);
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
                if (ConnectionClass.CreerUnNouveauDocumentStock(document))
                {
                    var liste = ConnectionClass.ListeDocumentStock();
                    foreach (var d in liste)
                    {
                        dataGridView1.Rows.Add(d.ID, d.Reference, d.Date.ToShortDateString(), d.Source, d.Destination);
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
                    document.ID = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    document.Date = DateTime.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                    document.Reference = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    document.Source = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    document.Destination = dataGridView1.CurrentRow.Cells[4].Value.ToString();

                    if (ConnectionClass.MettreAjourDocumentStock(document))
                    {
                        dataGridView1.Rows.Clear();
                        var liste = ConnectionClass.ListeDocumentStock();
                        foreach (var d in liste)
                        {
                            dataGridView1.Rows.Add(d.ID, d.Reference, d.Date.ToShortDateString(), d.Source, d.Destination);
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
                    var numeroPiece = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ModifTsockFrm.etat = etat;
                    ModifTsockFrm.numeroPiece = numeroPiece;
                    
                    ModifTsockFrm.ShowBox();
                }
            }
            catch { }
        }

        private void txtReference_TextChanged(object sender, EventArgs e)
        {
            if (txtReference.Text.Length >= 3)
            {
                dataGridView1.Rows.Clear();
                var liste = from f in ConnectionClass.ListeDocumentStock()
                                where f.Reference.ToUpper().Contains(txtReference.Text.ToUpper())
                                select f;
                foreach (var d in liste)
                {
                    dataGridView1.Rows.Add(d.ID, d.Reference, d.Date.ToShortDateString(), d.Source, d.Destination);
                }
            }
        }
     
        
 }
}
