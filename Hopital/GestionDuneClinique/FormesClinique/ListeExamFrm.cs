using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionDuneClinique.Formes
{
    public partial class ListeExamFrm : Form
    {
        public ListeExamFrm()
        {
            InitializeComponent();
        }

        private void ListeExamFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control,
                SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListeExamFrm_Load(object sender, EventArgs e)
        {
            textBox2.Focus();
            dgvAnal.RowTemplate.Height = 25;
            var dt = AppCode.ConnectionClassClinique.TableDesAnalysesEffectuesDuJour();
            ListeAnalyse(dt);
            
        }

        void ListeAnalyse(DataTable dt)
        {
            try
            {
                dgvAnal.Rows.Clear();
                foreach (DataRow dtRow in dt.Rows)
                {
                    var docteur = dtRow.ItemArray[4].ToString();
                    //if (dtRow.ItemArray[4].ToString().ToUpper() == "PARTENAIRE EXTERNE")
                    //{
                    //    docteur = docteur.ToUpper() + " " + dtRow.ItemArray[7].ToString().ToUpper();
                    //}
                    dgvAnal.Rows.Add(
                        dtRow.ItemArray[0].ToString(),
                        dtRow.ItemArray[1].ToString().ToUpper(),
                        dtRow.ItemArray[2].ToString().ToUpper() + " " +
                        dtRow.ItemArray[3].ToString().ToUpper(),
                          docteur,
                        dtRow.ItemArray[5].ToString().ToUpper(),
                        dtRow.ItemArray[6].ToString()
                        );
                    Column1.Image = global::GestionDuneClinique.Properties.Resources.deleteButton;
                    var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("EXAMEN", Int32.Parse(dtRow.ItemArray[0].ToString()));
                    if (flag)
                    {
                    dgvAnal.Rows[dgvAnal.Rows.Count-1].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void btnsupexamen_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAnal.SelectedRows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer les données d' analyse numero " + dgvAnal.SelectedRows[0].Cells[0].Value.ToString() + "?", "confirmation", "confirmation.png") == "1")
                    {
                        var id = Int32.Parse(dgvAnal.SelectedRows[0].Cells[0].Value.ToString());
                        AppCode.ConnectionClassClinique.SupprimerUneAnalyseFaite(id);
                        var dt = AppCode.ConnectionClassClinique.TableDesAnalysesEffectuesDuJour();
                        ListeAnalyse(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        static ListeExamFrm frmList;
        public static string btnClick, patient;
        public static int id, idPatient;
        public static bool State;
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                State = false;
                btnClick = "2";
                Close();
            }
            catch(Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void dgvAnal_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvAnal.SelectedRows.Count > 0)
                {
                    btnClick = "1";
                    patient = dgvAnal.SelectedRows[0].Cells[2].Value.ToString();
                    id = Int32.Parse(dgvAnal.SelectedRows[0].Cells[0].Value.ToString());
                    idPatient = Int32.Parse(dgvAnal.SelectedRows[0].Cells[5].Value.ToString());
                    
                    frmList.Dispose();
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        public static string ShowBox()
        {
            try
            {
                frmList = new ListeExamFrm();
                frmList.ShowDialog();
                
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
            return btnClick;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text == "")
                {
                    dgvAnal.Rows.Clear();
                }
                else
                {
                    var dt = AppCode.ConnectionClassClinique.TableDesAnalysesEffectues(textBox2.Text);
                    ListeAnalyse(dt);
                }
                foreach (DataGridViewRow dgrv in dgvAnal.Rows)
                {
                    var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("EXAMEN", Int32.Parse(dgrv.Cells[0].Value.ToString()));
                    if (flag)
                    {
                        dgrv.DefaultCellStyle.BackColor = Color.White;
                    }
                }
                dgvAnal.Focus();
            }
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.DodgerBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void dgvAnal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgvAnal_DoubleClick(null, null);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void button7_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dgvAnal_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                
                    btnsupexamen_Click(null, null);
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int idPatient;
                if (Int32.TryParse(textBox1.Text , out idPatient))
                {
                    var dt = AppCode.ConnectionClassClinique.TableDesAnalysesEffectuesParIdPatient(idPatient);
                    ListeAnalyse(dt);
                }
                else
                {
                    dgvAnal.Rows.Clear();
                }
                foreach (DataGridViewRow dgrv in dgvAnal.Rows)
                {
                    var flag = AppCode.ConnectionClassClinique.SiFactureEnBon("EXAMEN", Int32.Parse(dgrv.Cells[0].Value.ToString()));
                    if (flag)
                    {
                        dgrv.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch { }
        }
        
     
    }
}
