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
    public partial class ListeOrdoFrm : Form
    {
        public ListeOrdoFrm()
        {
            InitializeComponent();
        }

        private void ListeOrdofrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue,
                Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        static ListeOrdoFrm frmOrdon;
        static string btnClick;
        public static string employe;
        public static int id, idPatient;
        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvAnal_DoubleClick(object sender, EventArgs e)
        {
         if (dgvOrdo.SelectedRows.Count > 0)
            {
                btnClick = "1";
                idPatient = Int32.Parse(dgvOrdo.SelectedRows[0].Cells[2].Value.ToString());
             employe= dgvOrdo.SelectedRows[0].Cells[5].Value.ToString();
                id = Int32.Parse(dgvOrdo.SelectedRows[0].Cells[0].Value.ToString());
                frmOrdon.Dispose();
            }
        }

         public static string ShowBox()
        {
            try
            {
                frmOrdon = new ListeOrdoFrm();
                frmOrdon.ShowDialog();                
            }
            catch { }
            return btnClick;
        }

         private void ListeOrdoFrm_Load(object sender, EventArgs e)
         {
             var listeOrdon = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesOrdonnances();
             ListeOrdonnances(listeOrdon);
         }

         void ListeOrdonnances(List<GestionDuneClinique.AppCode.Ordonnance> listeOrdon)
         {
             dgvOrdo.Rows.Clear();
             foreach (GestionDuneClinique.AppCode.Ordonnance ordonnance in listeOrdon)
             {
                 dgvOrdo.Rows.Add(ordonnance.NumeroOrdonnance,
                     ordonnance.DateOrdonnance,
                     ordonnance.IdPatient,
                     ordonnance.Patient,
                     ordonnance.NumeroEmploye,
                     ordonnance.Employe
                         );
             }
         }

         private void btnsupexamen_Click(object sender, EventArgs e)
         {
             if (dgvOrdo.SelectedRows.Count > 0)
             {
                 id = Int32.Parse(dgvOrdo.SelectedRows[0].Cells[0].Value.ToString());
                 if (MonMessageBox.ShowBox("Voulez vous supprimer les donnees de l'ordonnance numero " + id + "?", "Confirmation", "confirmation.png") == "1")
                 {
                     GestionDuneClinique.AppCode.ConnectionClassClinique.SupprimerUneOrdonnance(id);
                     var listeOrdon = GestionDuneClinique.AppCode.ConnectionClassClinique.ListeDesOrdonnances();
                     ListeOrdonnances(listeOrdon);
                 }
             }
         }
    }
}
