using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using GestionDuneClinique.AppCode;

namespace GestionDuneClinique.FormesClinique
{
    public partial class ProgrammeFrm : Form
    {
        public ProgrammeFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.LightBlue, Color.LightBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void ProgrammeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new
                LinearGradientBrush(area1, Color.AliceBlue, Color.AliceBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ProgrammeFrm_Load(object sender, EventArgs e)
        {
            var year = DateTime.Now.Year + 10;
            button7.Location = new Point(Width-45, 5);
            for (var i = 2016; i <= year; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }
            comboBox1.Text=  DateTime.Now.Year.ToString();

            var dt = ConnectionClassClinique.ListeService();
            for (var i = 0; i < dt.Rows.Count; i++)
                comboBox3.Items.Add(dt.Rows[i].ItemArray[1].ToString());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLibelle.Text) && !string.IsNullOrEmpty(comboBox3.Text))
                {
                    idService = Convert.ToInt32(ConnectionClassClinique.ListeService(comboBox3.Text).Rows[0].ItemArray[0].ToString());
                    var programme = new Programme();
                    programme.Annee = Convert.ToInt32(comboBox1.Text);
                    programme.DateDebut = dtp1.Value.Date;
                    programme.DateFin = dtp2.Value.Date;
                    programme.Libelle = txtLibelle.Text;
                    programme.NumeroProgramme = id;
                    programme.IDService = idService;
                    if (dtp2.Value.Date.Subtract(dtp1.Value.Date).Days >= 28 &&
                        dtp2.Value.Date.Subtract(dtp1.Value.Date).Days <= 31)
                    {
                        if (ConnectionClassClinique.EnregistrerUnProgramme((programme)))
                        {
                            numero = ConnectionClassClinique.DernierNumeroProgramme();
                            lblTitre.Text = txtLibelle.Text;
                            txtLibelle.Text = "";
                            dtp1.Value = dtp2.Value = DateTime.Now;
                            comboBox1.Text = DateTime.Now.Year.ToString();
                            RemplirDonnee();
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Le planning ne doit être que pour un mois", "Erreur", "erreur.png");

                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        private int index;
   private void RemplirDonnee()
        {
            try
            {
                dgvProgramme.Columns.Clear();
                var p = (dtp2.Value.Date.Subtract(dtp1.Value.Date)).Days;
                var listeDateTimes = new List<DateTime>();
                for (var i = 0; i <= p; i++)
                {
                    listeDateTimes.Add(dtp1.Value.Date.AddDays(i));
                }
                index = 2;
                var rowCount = 0;
                var columnName = "Column";
                dgvProgramme.ColumnCount = listeDateTimes.Count + 2;
                dgvProgramme.Columns[0].ReadOnly = false;
                dgvProgramme.Columns[0].Name = columnName;
                dgvProgramme.Columns[0].HeaderText = "N°";
                dgvProgramme.Columns[0].Width = 0;
                dgvProgramme.Columns[0].Visible = false;
                columnName = "Column"+0;
                dgvProgramme.Columns[1].ReadOnly = false;
                dgvProgramme.Columns[1].Name = columnName;
                dgvProgramme.Columns[1].HeaderText = "Personnels/Jours";
                dgvProgramme.Columns[1].Width = 250;
           
                dgvProgramme.Rows.Add();
                dgvProgramme.Rows[rowCount].Cells[0].Value = "0";
                dgvProgramme.Rows[rowCount].Cells[1].Value = "";
                foreach (var date in listeDateTimes)
                {
                    columnName = "Column" + index;
                    dgvProgramme.Columns[index].ReadOnly = true; 
                    dgvProgramme.Columns[index].HeaderText= date.ToLongDateString().Substring(0, 1).ToUpper();
                    dgvProgramme.Columns[index].Width =30;
                    dgvProgramme.Columns[index].Name = columnName;
                    dgvProgramme.Rows[rowCount].Cells[index].Value = date.Day;
                    dgvProgramme.Rows[rowCount].Cells[index].ReadOnly = true;
                    dgvProgramme.Rows[rowCount].DefaultCellStyle.BackColor = Color.Yellow;
                    index++;
                }

                var dt = ConnectionClassClinique.ListeDetailProgramme(numero);
                rowCount = rowCount + 1;
              
                for(var i=0;i<dt.Rows.Count;i++)
                {
                    dgvProgramme.Rows.Add();
                    for (var j = 0; j < dgvProgramme.Rows[i].Cells.Count; j++)
                    {
                        dgvProgramme.Rows[rowCount].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                        dgvProgramme.Rows[rowCount].Cells[j].ReadOnly = false;
                    }
                    rowCount++;
                }
            }
            catch(Exception ex) 
            { }
        }

        private int id,numero;
        private void button3_Click(object sender, EventArgs e)
        {
            if (ListeProFrm.ShowBox())
            {
                    txtLibelle.Text = ListeProFrm.programme.Libelle;
                    dtp1.Value = ListeProFrm.programme.DateDebut;
                    dtp2.Value = ListeProFrm.programme.DateFin;
                    comboBox1.Text = ListeProFrm.programme.Annee.ToString();
                    id = ListeProFrm.programme.NumeroProgramme;
                numero = ListeProFrm.programme.NumeroProgramme;
                comboBox3.Text = ListeProFrm.programme.Legende;
                    lblTitre.Text = txtLibelle.Text;
                    RemplirDonnee();
                comboBox2.Items.Clear();
                var dt = ConnectionClassClinique.ListeLegendeProgramme(numero);
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox2.Items.Add(dt.Rows[i].ItemArray[2].ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(comboBox3.Text))
                {
                    ListePersFrm.service = comboBox3.Text;
                    if (ListePersFrm.ShowBox())
                    {
                        if (id > 0)
                        {
                            var prog = new Programme();
                            prog.NumeroDetailProgramme = numero;
                            prog.NomEmploye = ListePersFrm.employe.NomEmployee;
                            bool flag = false;
                            if (dgvProgramme.Rows.Count > 1)
                            {
                                foreach (DataGridViewRow dgvRow in dgvProgramme.Rows)
                                {
                                    if (dgvRow.Cells[1].Value.ToString()
                                        .Equals(prog.NomEmploye))
                                    {
                                        flag = true;
                                    }
                                }
                                if (!flag)
                                {
                                    if (ConnectionClassClinique.InsererDetailProgramme(prog))
                                    {
                                        var rowCount = dgvProgramme.Rows.Count;
                                        dgvProgramme.Rows.Add();
                                        dgvProgramme.Rows[rowCount].Cells[0].Value =
                                            ConnectionClassClinique.DernierNumeroDetailProgramme();
                                        dgvProgramme.Rows[rowCount].Cells[1].Value = prog.NomEmploye;
                                        for (var i = 2; i < dgvProgramme.ColumnCount; i++)
                                        {
                                            dgvProgramme.Rows[rowCount].Cells[i].Value = "";
                                        }
                                        rowCount++;
                                    }
                                }
                            }
                            else
                            {

                                if (ConnectionClassClinique.InsererDetailProgramme(prog))
                                {
                                    var rowCount = dgvProgramme.Rows.Count;
                                    dgvProgramme.Rows.Add();
                                    dgvProgramme.Rows[rowCount].Cells[0].Value =
                                        ConnectionClassClinique.DernierNumeroDetailProgramme();
                                    dgvProgramme.Rows[rowCount].Cells[1].Value = prog.NomEmploye;
                                    for (var i = 2; i < dgvProgramme.ColumnCount; i++)
                                    {
                                        dgvProgramme.Rows[rowCount].Cells[i].Value = "";
                                    }
                                    rowCount++;
                                }
                            }
                        }
                    }
                }
            }
                catch 
                {
                }
            
        }

        private void dgvProgramme_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ConnectionClassClinique.ModifierDetailProgramme(dgvProgramme);
            }
            catch
            {
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    button2_Click_1(null,null);
                }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox2.Text))
            {
                if(numero>0)
                {
                var programme=new Programme();
                programme.Legende = textBox2.Text;
                programme.NumeroProgramme = numero;
                    programme.IDLegende = idLegende;
                    if (ConnectionClassClinique.EnregistrerUneLegende(programme))
                    {
                        textBox2.Text = "";
                        comboBox2.Items.Clear();
                        var dt = ConnectionClassClinique.ListeLegendeProgramme(numero);
                        for (var i = 0; i < dt.Rows.Count; i++)
                        {
                            comboBox2.Items.Add(dt.Rows[i].ItemArray[2].ToString());
                        }
                    }
                }
            }
        }

        private int idLegende, idService;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = comboBox2.Text;
            var dt = ConnectionClassClinique.ListeLegendeProgramme(numero);
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[2].ToString().Equals(comboBox2.Text))
                    idLegende = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                idService = Convert.ToInt32(ConnectionClassClinique.ListeService(comboBox3.Text).Rows[0].ItemArray[0].ToString());
            }
            catch (Exception)
            {
            }
        }

        //imprimer une programme
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var programme =new Programme();
                var pro = from p in ConnectionClassClinique.ListeDesProgrammes()
                    where p.NumeroProgramme == numero
                    select p;
                foreach (var p in pro)
                    programme = p;

                   ficheProgramme = Impression.ImprimerUnProgramme( programme,dgvProgramme);
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    //printDocument1.Print();
                    printPreviewDialog1.ShowDialog();
                }
            }
            catch
            { }
        }

        Bitmap ficheProgramme;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            e.Graphics.DrawImage(ficheProgramme, 10, 10, ficheProgramme.Width,ficheProgramme.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }



    }
}
