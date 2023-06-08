using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace LabMonitoring.AppCode
{
    public class Impression
    {
        public static Bitmap ImprimerResultat1(Patient patient, DataGridView dgView, bool flagNFS)
        {
            #region
            int unite_hauteur = 25;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int hauteur_facture = 46 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::LabMonitoring.Properties.Resources.logo;
                graphic.DrawImage(logo, 1 * unite_largeur, unite_hauteur, 23 * unite_largeur, 5 * unite_hauteur + 0);

            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt0 = new Font("Times New Roman", 13, FontStyle.Bold);
            Font fnt1 = new Font("Times New Roman", 13, FontStyle.Regular);
            Font fnt11 = new Font("Times New Roman", 13, FontStyle.Bold | FontStyle.Italic);
            Font fnt3 = new Font("Times New Roman", 13, FontStyle.Italic);
            Font fnt33 = new Font("Times New Roman", 13, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            //var logo1 = global::LabMonitoring.Properties.Resources.fleche;

            graphic.DrawString("Nom & Prénom : .................." + patient.NomPatient.ToUpper() + "..........................................................................................", fnt3, Brushes.Black, unite_largeur * 7, 8 * unite_hauteur);
            graphic.DrawString("Age : ........ " + patient.Age + " .........Sexe :........." + patient.Sexe + ".......... N° :...................." + patient.IDPatient.ToString() + ".....................................................", fnt3, Brushes.Black, 7 * unite_largeur, 9 * unite_hauteur);
            graphic.DrawString("Service :...... Consultation Medecin ............ Date :..... " + DateTime.Now.ToShortDateString() + "...............................................", fnt3, Brushes.Black, 7 * unite_largeur, 10 * unite_hauteur);
            graphic.FillRectangle(Brushes.White, unite_largeur *24, 8 * unite_hauteur, unite_largeur * 9, unite_hauteur * 4);
       
            graphic.DrawRectangle(Pens.Black, unite_largeur + 15, 15 * unite_hauteur, unite_largeur * 10, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 11 + 15, 15 * unite_hauteur, unite_largeur * 12 + 15, unite_hauteur * 2);
            graphic.DrawString("Examen", fnt0, Brushes.Black, unite_largeur * 2, 16 * unite_hauteur);
            graphic.DrawString("Résultat", fnt0, Brushes.Black, 17 * unite_largeur, 16 * unite_hauteur);
            //graphic.DrawRectangle(Pens.Black, unite_largeur + 15, 15 * unite_hauteur, unite_largeur * 9, 9 * unite_hauteur);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 10 + 15, 15 * unite_hauteur, unite_largeur * 14 + 15, 9*unite_hauteur);

            var found = false;
            var index = 0;
            var indexWidal = false;

            for (var I = 0; I < dgView.Rows.Count; I++)
            {
                var YLOC = 17 * unite_hauteur +5+ I * (unite_hauteur + 4);
                string analyse = "";
                var listeExam = from le in ConnectionClass.ListeExamen()
                                where le.NumeroAnalyse == Convert.ToInt32(dgView.Rows[I].Cells[0].Value.ToString())
                                select le.Analyse;
                foreach (var a in listeExam)
                    analyse = a;
                if (!analyse.ToUpper().Contains("WIDAL"))
                {
            //graphic.DrawRectangle(Pens.Black, unite_largeur + 15, 15*unite_hauteur, unite_largeur * 9, unite_hauteur * 8);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 10 + 15, 15*unite_hauteur, unite_largeur * 14 + 15, unite_hauteur * 8);
                    graphic.DrawString(dgView.Rows[I].Cells[1].Value.ToString(), fnt3, Brushes.Black, unite_largeur * 2, YLOC);
                    //graphic.DrawImage(logo1, 12 * unite_largeur, YLOC + 5, 3 * unite_largeur, 8);
                    graphic.DrawString(dgView.Rows[I].Cells[3].Value.ToString(), fnt11, Brushes.Black, 17 * unite_largeur, YLOC);
                    index++;
                    //graphic.FillRectangle(Brushes.White, unite_largeur + 15, 15 * unite_hauteur, unite_largeur * 9, unite_hauteur *8);
            
}
                else
                {
                    found = true;
                    if (dgView.Rows.Count == 8)
                    {
                        indexWidal = true;
                    }
                }
            }
            #region WIDAL

            if (found)
            {
                int wid = 0;

                if (indexWidal)
                {
                    graphic.FillRectangle(Brushes.White, unite_largeur, 13 * unite_hauteur, 23 * unite_largeur, 12 * unite_hauteur);
                    wid = -unite_hauteur * 10;
                }
                graphic.DrawString("SEROLOGIE DE WIDAL ET FELIX", fnt11, Brushes.Black, unite_largeur * 9, 28 * unite_hauteur - 10 + wid);
                for (var j = 0; j < 4; j++)
                {
                    var YLOC = 29 * unite_hauteur + j * 33;
                    graphic.DrawRectangle(Pens.Black, unite_largeur + 16, YLOC + wid, unite_largeur * 12 - 10, 33);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 13 + 6, YLOC + wid, unite_largeur * 11 - 10, 33);

                }

                graphic.DrawString(dgView.Rows[index].Cells[2].Value.ToString() + " : "
                    + dgView.Rows[index].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, 29 * unite_hauteur + wid);

                graphic.DrawString(dgView.Rows[index + 1].Cells[2].Value.ToString() + " : " +
                    dgView.Rows[index + 1].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 14, 29 * unite_hauteur + wid);


                graphic.DrawString(dgView.Rows[index + 2].Cells[2].Value.ToString() + " : " +
                dgView.Rows[index + 2].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, 29 * unite_hauteur + 34 + wid);

                graphic.DrawString(dgView.Rows[index + 3].Cells[2].Value.ToString() + " : " +
                dgView.Rows[index + 3].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 14, 29 * unite_hauteur + 34 + wid);

                graphic.DrawString(dgView.Rows[index + 4].Cells[2].Value.ToString() + " : " +
                    dgView.Rows[index + 4].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, 29 * unite_hauteur + 34 * 2 + wid);

                graphic.DrawString(dgView.Rows[index + 5].Cells[2].Value.ToString() + " : " +
                dgView.Rows[index + 5].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 14, 29 * unite_hauteur + 34 * 2 + wid);

                graphic.DrawString(dgView.Rows[index + 6].Cells[2].Value.ToString() + " : " +
                dgView.Rows[index + 6].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, 29 * unite_hauteur + 34 * 3 + wid);

                graphic.DrawString(dgView.Rows[index + 7].Cells[2].Value.ToString() + " : " +
                dgView.Rows[index + 7].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 14, 29 * unite_hauteur + 34 * 3 + wid);
            }
            #endregion
            if (flagNFS)
            {
                graphic.DrawString("-  NFS: Voir La Fiche Imprimée", fnt11, Brushes.Black, unite_largeur * 9, 37 * unite_hauteur);
            }
            if (index > 0)
            {
                index = index + 1;
                graphic.DrawRectangle(Pens.Black, unite_largeur + 15, 17 * unite_hauteur, unite_largeur * 10, index * (unite_hauteur + 4));
                graphic.DrawRectangle(Pens.Black, unite_largeur * 11 + 15, 17 * unite_hauteur, unite_largeur * 12 + 15, index * (unite_hauteur + 4));
                
            }

            graphic.DrawString("Sourire, Service et Santé", fnt11, Brushes.Black, unite_largeur * 2, 38 * unite_hauteur);
            graphic.DrawString("Le Spécialiste", fnt33, Brushes.Black, 20 * unite_largeur, 38 * unite_hauteur);
            graphic.DrawString("Fait à Ndjamena le " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 9 * unite_largeur, 40 * unite_hauteur);
            return bitmap;
        }

        //public static Bitmap ImprimerResultat1(Patient patient,DataGridView dgView, bool flagNFS)
        //{
        //    #region
        //    int unite_hauteur = 20;
        //    int unite_largeur = 32;
        //    int largeur_facture = 25 * unite_largeur;
        //    int hauteur_facture = 46 * unite_hauteur;

        //    //creer un bit map
        //    Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

        //    // creer un objet graphic
        //    Graphics graphic = Graphics.FromImage(bitmap);

        //    //la couleur de l'image
        //    graphic.Clear(Color.White);
        //    #endregion
        //     try
        //    {
        //        Image logo = global::LabMonitoring.Properties.Resources.logo;
        //        graphic.DrawImage(logo, 1 * unite_largeur, unite_hauteur, 22 * unite_largeur, 5 * unite_hauteur+0);

        //    }
        //    catch (Exception)
        //    { }
        //    //definir les polices 
        //     Font fnt0 = new Font("Times New Roman", 14, FontStyle.Bold);
        //    Font fnt1 = new Font("Times New Roman", 14, FontStyle.Regular);
        //    Font fnt11 = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Italic);
        //    Font fnt3 = new Font("Times New Roman", 14, FontStyle.Italic);
        //    Font fnt33 = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);


        //    graphic.DrawString("Nom & Prénom : .................." + patient.NomPatient.ToUpper() + "..........................................................................................", fnt3, Brushes.Black, unite_largeur*7, 7 * unite_hauteur);
        //    graphic.DrawString("Age : ........ " + patient.Age + " .........Sexe :........." + patient.Sexe + ".......... N° :...................." + patient.IDPatient.ToString() + ".....................................................", fnt3, Brushes.Black, 7 * unite_largeur, 8 * unite_hauteur);
        //    graphic.DrawString("Service :...... Consultation Medecin ............ Date :..... " + DateTime.Now.ToShortDateString() + "...............................................", fnt3, Brushes.Black, 7 * unite_largeur, 9 * unite_hauteur);

        //    graphic.FillRectangle(Brushes.White, unite_largeur * 23, 7 * unite_hauteur -2, unite_largeur * 5, unite_hauteur * 4);

        //    graphic.DrawRectangle(Pens.Black, unite_largeur*1, 13 * unite_hauteur, unite_largeur * 11, unite_hauteur *2-2);
        //    graphic.DrawRectangle(Pens.Black, unite_largeur*12 , 13 * unite_hauteur, unite_largeur * 11, unite_hauteur * 2-2);
        //    graphic.DrawString("Examen", fnt0, Brushes.Black, unite_largeur * 2, 14 * unite_hauteur-5);
        //    graphic.DrawString("Résultat", fnt0, Brushes.Black, 14 * unite_largeur, 14 * unite_hauteur-5);
        //    ////graphic.DrawRectangle(Pens.Black, unite_largeur + 15, 15 * unite_hauteur, unite_largeur * 14, 10 * unite_hauteur);
        //    ////graphic.DrawRectangle(Pens.Black, unite_largeur * 15 + 15, 15 * unite_hauteur, unite_largeur * 9 + 15, 10*unite_hauteur);

        //    var found = false;
        //    var index = 0;
        //    var indexWidal = false;

        //    for (var I = 0; I < dgView.Rows.Count; I++)
        //    {

        //        var YLOC = 15 * unite_hauteur + I * (unite_hauteur+10);
        //        string analyse ="";
        //        var listeExam = from le in ConnectionClass.ListeExamen()
        //                        where le.NumeroAnalyse == Convert.ToInt32(dgView.Rows[I].Cells[0].Value.ToString())
        //                        select le.Analyse;
        //        foreach (var a in listeExam)
        //            analyse = a;
        //        if (!analyse.ToUpper().Contains("WIDAL"))
        //        {
        //            //graphic.DrawRectangle(Pens.Black, unite_largeur * 2, YLOC, unite_largeur * 10, unite_hauteur + 10);
        //            graphic.FillRectangle(Brushes.MintCream, unite_largeur * 1, YLOC, unite_largeur * 22, unite_hauteur + 5);

        //            graphic.DrawString(dgView.Rows[I].Cells[1].Value.ToString(), fnt3, Brushes.Black, unite_largeur * 2, YLOC);
        //            graphic.DrawString(dgView.Rows[I].Cells[3].Value.ToString(), fnt11, Brushes.Black, 13 * unite_largeur, YLOC);
        //            index++;
        //        }
        //        else
        //        {
        //            found = true;
        //            if (dgView.Rows.Count == 8)
        //            {
        //                indexWidal = true;
        //            }
        //        }
        //     }
        //    #region WIDAL

        //    if (found)
        //    {
        //        int wid=0;

        //        if (indexWidal)
        //        {
        //            graphic.FillRectangle(Brushes.White, unite_largeur, 13 * unite_hauteur, 24 * unite_largeur, 12 * unite_hauteur);
        //            wid = -unite_hauteur * 10;
        //        }
        //        graphic.DrawString("SEROLOGIE DE WIDAL ET FELIX", fnt11, Brushes.Black, unite_largeur * 9, 28 * unite_hauteur - 10 + wid);
        //        for (var j = 0; j < 4; j++)
        //        {
        //            var YLOC = 29 * unite_hauteur + j * 30 ;
        //            graphic.DrawRectangle(Pens.Black, unite_largeur *1, YLOC+wid, unite_largeur * 11, 30);
        //            graphic.DrawRectangle(Pens.Black, unite_largeur * 12, YLOC+wid , unite_largeur * 11, 30);

        //        }

        //        graphic.DrawString(dgView.Rows[index].Cells[2].Value.ToString() + " : "
        //            + dgView.Rows[index].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, 29 * unite_hauteur+31 + wid);

        //        graphic.DrawString(dgView.Rows[index + 1].Cells[2].Value.ToString() + " : " +
        //            dgView.Rows[index + 1].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 13, 29 * unite_hauteur+31 + wid);


        //        graphic.DrawString(dgView.Rows[index + 2].Cells[2].Value.ToString() + " : " +
        //        dgView.Rows[index + 2].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, 29 * unite_hauteur + 31 + wid);

        //        graphic.DrawString(dgView.Rows[index + 3].Cells[2].Value.ToString() + " : " +
        //        dgView.Rows[index + 3].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 13, 29 * unite_hauteur + 31 + wid);

        //        graphic.DrawString(dgView.Rows[index + 4].Cells[2].Value.ToString() + " : " +
        //            dgView.Rows[index + 4].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, 29 * unite_hauteur + 31 * 2 + wid);

        //        graphic.DrawString(dgView.Rows[index + 5].Cells[2].Value.ToString() + " : " +
        //        dgView.Rows[index + 5].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 13, 29 * unite_hauteur + 31 * 2 + wid);

        //        graphic.DrawString(dgView.Rows[index + 6].Cells[2].Value.ToString() + " : " +
        //        dgView.Rows[index + 6].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, 29 * unite_hauteur + 31 * 3 + wid);

        //        graphic.DrawString(dgView.Rows[index + 7].Cells[2].Value.ToString() + " : " +
        //        dgView.Rows[index + 7].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 13, 29 * unite_hauteur + 31 * 3 + wid);
        //    }
        //    #endregion
        //    if (flagNFS)
        //    {
        //        graphic.DrawString("-  NFS: Voir La Fiche Imprimée", fnt11, Brushes.Black, unite_largeur * 9, 37 * unite_hauteur);           
        //    }

        //    graphic.DrawRectangle(Pens.Black, unite_largeur * 1, 15 * unite_hauteur - 2, unite_largeur * 11, index * (unite_hauteur + 10));
        //    graphic.DrawRectangle(Pens.Black, unite_largeur * 12, 15 * unite_hauteur - 2, unite_largeur * 11, index * (unite_hauteur + 10));

        //    graphic.DrawString("Sourire, Service et Santé", fnt11, Brushes.Black, unite_largeur * 1, 38 * unite_hauteur);
        //    graphic.DrawString("Le Spécialiste", fnt33, Brushes.Black, 20 * unite_largeur, 38 * unite_hauteur);
        //    graphic.DrawString("Fait à Ndjamena le " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 9 * unite_largeur, 40 * unite_hauteur);
        //    return bitmap;
        //}

        public static Bitmap ImprimerResultatComplique(Patient patient, DataGridView dgvView,string titre)
        {
            #region
            int unite_hauteur = 28;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int hauteur_facture = 46 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::LabMonitoring.Properties.Resources.logo;
                graphic.DrawImage(logo, 2 * unite_largeur, unite_hauteur, 23 * unite_largeur, 5 * unite_hauteur + 10);

            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Times New Roman", 13, FontStyle.Regular);
            Font fnt11 = new Font("Times New Roman", 13, FontStyle.Bold | FontStyle.Italic);
            Font fnt3 = new Font("Times New Roman", 13, FontStyle.Italic);
            Font fnt33 = new Font("Times New Roman", 13, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            
            var capture1 = global::LabMonitoring.Properties.Resources.Capture1;
            var capture2 = global::LabMonitoring.Properties.Resources.Capture2;
            var capture3 = global::LabMonitoring.Properties.Resources.Capture3;

            graphic.DrawString("Nom & Prénom : .................." + patient.NomPatient.ToUpper() + "..........................................................................................", fnt3, Brushes.Black, unite_largeur * 8, 7 * unite_hauteur);
            graphic.DrawString("Age : ........ " + patient.Age + " .........Sexe :........." + patient.Sexe + ".......... N° :.............." + patient.IDPatient.ToString() + ".................................................", fnt3, Brushes.Black, 8 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Service :........ Consultation Medecin ............... Date :......  " +DateTime.Now.ToShortDateString() +" ...............................................", fnt3, Brushes.Black, 8 * unite_largeur, 9 * unite_hauteur);

            graphic.DrawString(titre, fnt11, Brushes.Black, unite_largeur * 6, 11 * unite_hauteur);

            for (var i = 0; i < dgvView.Rows.Count; i++)
            {
                var YLOC=12*unite_hauteur+15+ i*unite_hauteur;
                if(  string.IsNullOrWhiteSpace( dgvView.Rows[i].Cells[3].Value.ToString()))
                {
                    graphic.DrawImage(capture1, 5 * unite_largeur, YLOC, 20, 20);
                    graphic.DrawString(dgvView.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 6, YLOC);           
                }
                else
                {
                    graphic.DrawImage(capture2, 7 * unite_largeur, YLOC, 20, 20);
                    var appreciation = dgvView.Rows[i].Cells[2].Value.ToString() + " : ";
                    if (dgvView.Rows[i].Cells[2].Value.ToString().ToUpper().Contains("CONCLUSION"))
                    {
                        graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 8, YLOC);
                    }
                    if (dgvView.Rows[i].Cells[2].Value.ToString().ToUpper().Contains("KAOP"))
                    {
                        graphic.FillRectangle(Brushes.White, unite_largeur, YLOC, unite_largeur * 24, unite_hauteur);
                        graphic.DrawString(appreciation, fnt11, Brushes.Black, unite_largeur * 6-10, YLOC + unite_hauteur);
                        graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 8, YLOC + unite_hauteur);
                    }
                    else
                    {
                        graphic.DrawString(appreciation, fnt1, Brushes.Black, unite_largeur * 8, YLOC);
                        graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 17, YLOC);
                    }
                }                 
            }
            graphic.DrawString("Sourire, Service et Santé", fnt11, Brushes.Black, unite_largeur * 2, 36 * unite_hauteur);
            graphic.DrawString("Le Spécialiste", fnt33, Brushes.Black, 20 * unite_largeur, 36 * unite_hauteur);
            graphic.DrawString("Fait à Ndjamena le " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 9 * unite_largeur, 38 * unite_hauteur+15);
            return bitmap;
        }

        public static Bitmap ImprimerResultatKAOP(Patient patient, DataGridView dgvView, string titre)
        {
            #region
            int unite_hauteur = 28;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int hauteur_facture = 46 * unite_hauteur;
            //int unite_hauteur = 28;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::LabMonitoring.Properties.Resources.logo;
                graphic.DrawImage(logo, 2 * unite_largeur, unite_hauteur, 23 * unite_largeur, 5 * unite_hauteur + 10);

            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Times New Roman", 13, FontStyle.Regular);
            Font fnt11 = new Font("Times New Roman", 13, FontStyle.Bold | FontStyle.Italic);
            Font fnt0 = new Font("Times New Roman", 16, FontStyle.Bold | FontStyle.Italic);
            Font fnt3 = new Font("Times New Roman", 13, FontStyle.Italic);
            Font fnt33 = new Font("Times New Roman", 13, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);

            var capture1 = global::LabMonitoring.Properties.Resources.Capture1;
            var capture2 = global::LabMonitoring.Properties.Resources.Capture2;
            var capture3 = global::LabMonitoring.Properties.Resources.Capture3;

            graphic.DrawString("Nom & Prénom : .................." + patient.NomPatient.ToUpper() + "..........................................................................................", fnt3, Brushes.Black, unite_largeur * 8, 7 * unite_hauteur);
            graphic.DrawString("Age : ........ " + patient.Age + " .........Sexe :........." + patient.Sexe + ".......... N° :.............." + patient.IDPatient.ToString() + ".................................................", fnt3, Brushes.Black, 8 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Service :........ Consultation Medecin ............... Date :......  " + DateTime.Now.ToShortDateString() + " ...............................................", fnt3, Brushes.Black, 8 * unite_largeur, 9 * unite_hauteur);

            graphic.DrawString(titre, fnt0, Brushes.Black, unite_largeur * 11, 14 * unite_hauteur);

            for (var i = 0; i < dgvView.Rows.Count; i++)
            {
                var YLOC = 16 * unite_hauteur  + i * 38;
                if (string.IsNullOrWhiteSpace(dgvView.Rows[i].Cells[3].Value.ToString()))
                {
                    graphic.DrawImage(capture1, 5 * unite_largeur, YLOC, 20, 20);
                    graphic.DrawString(dgvView.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 6, YLOC);
                }
                else
                {
                    graphic.DrawImage(capture2, 4 * unite_largeur, YLOC, 20, 20);
                    var appreciation = dgvView.Rows[i].Cells[2].Value.ToString() + " : ";
                    if (dgvView.Rows[i].Cells[2].Value.ToString().ToUpper().Contains("CONCLUSION"))
                    {
                        graphic.FillRectangle(Brushes.White, unite_largeur, YLOC, unite_largeur * 24, unite_hauteur);                      
                        graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur *4, YLOC);
                    }
                    else if (dgvView.Rows[i].Cells[2].Value.ToString().ToUpper().Contains("KAOP"))
                    {
                        graphic.FillRectangle(Brushes.White, unite_largeur, YLOC, unite_largeur * 24, unite_hauteur);
                        graphic.DrawString(appreciation, fnt11, Brushes.Black, unite_largeur * 5 - 10, YLOC + unite_hauteur);
                        graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 8, YLOC + unite_hauteur);
                    }
                    else
                    {
                        graphic.DrawString(appreciation, fnt1, Brushes.Black, unite_largeur * 5, YLOC);
                        graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 17, YLOC);
                    }
                }
            }
            graphic.DrawString("Sourire, Service et Santé", fnt11, Brushes.Black, unite_largeur * 2, 35 * unite_hauteur);
            graphic.DrawString("Le Spécialiste", fnt33, Brushes.Black, 20 * unite_largeur, 35 * unite_hauteur);
            graphic.DrawString("Fait à Ndjamena le " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 9 * unite_largeur, 37 * unite_hauteur + 15);
            return bitmap;
        }

        public static Bitmap ImprimerBilanDesExamens(DataGridView dgvView, string titre)
        {
            #region
            int unite_hauteur = 28;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int hauteur_facture = 46 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::LabMonitoring.Properties.Resources.logo;
                graphic.DrawImage(logo, 2 * unite_largeur, unite_hauteur, 23 * unite_largeur, 5 * unite_hauteur + 10);

            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Times New Roman", 13, FontStyle.Regular);
            Font fnt11 = new Font("Times New Roman", 13, FontStyle.Bold | FontStyle.Italic);
            Font fnt3 = new Font("Times New Roman", 13, FontStyle.Italic);
            Font fnt33 = new Font("Times New Roman", 13, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);

            var capture1 = global::LabMonitoring.Properties.Resources.Capture1;
            var capture2 = global::LabMonitoring.Properties.Resources.Capture2;
            var capture3 = global::LabMonitoring.Properties.Resources.Capture3;

           
            graphic.DrawString(titre, fnt11, Brushes.Black, unite_largeur * 3, 7 * unite_hauteur);
          
            graphic.FillRectangle(Brushes.SlateGray, unite_largeur * 3, 9 * unite_hauteur,  unite_largeur * 20, unite_hauteur);
           
            graphic.DrawString("EXAMEN", fnt11, Brushes.White, unite_largeur * 4, 9 * unite_hauteur);
            graphic.DrawString("NOMBRE", fnt11, Brushes.White, unite_largeur * 18, 9 * unite_hauteur);
             
            for (var i = 0; i < dgvView.Rows.Count; i++)
            {
                var YLOC =10 * unite_hauteur + 5 + i * unite_hauteur;
                if (i % 2 == 1)
                {
                    graphic.FillRectangle(Brushes.WhiteSmoke, unite_largeur * 3, YLOC, unite_largeur * 20, unite_hauteur-3);
                }
                graphic.DrawString(dgvView.Rows[i].Cells[0].Value.ToString(), fnt11, Brushes.Black, unite_largeur *4, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 18, YLOC);
             
            }
           return bitmap;
        }

    }
}
