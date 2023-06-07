using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace GestionPharmacetique.AppCode
{
   public  class Imprimer
    {

       
        //fonction pour dessiner rapport depenses
       public static Bitmap ImprimerRapportDepenses(ListView lstDepenses, string titre, int start)
        {

            //les dimension de la facture
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur ;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 56 * unite_hauteur;
            //bool flag = false;
            //if (lstDepenses.Items.Count <= 45 * (1 + start))
            //{

            //    flag = true;
            //}
            //else
            //{
            //    flag = false;
            //}
            ////creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            #endregion


            Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold | FontStyle.Underline);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
            }
            catch { }
           graphic.DrawString("Page  " + (start + 1), fnt33, Brushes.Black, 12 * unite_largeur, 10);
            graphic.DrawString(titre, fnt1, Brushes.Black, 11*unite_largeur, 6*unite_hauteur);
            
           graphic.FillRectangle(Brushes.Black, unite_largeur, 8 * unite_hauteur - 5, 24 * unite_largeur, unite_hauteur);

            graphic.DrawString("Libellé", fnt2, Brushes.White, unite_largeur, 8 * unite_hauteur-5 );
            graphic.DrawString("Date", fnt2, Brushes.White, 15 * unite_largeur - 15, 8 * unite_hauteur-5 );
            graphic.DrawString("N° Facture ", fnt2, Brushes.White, 18 * unite_largeur, 8 * unite_hauteur -5);
            graphic.DrawString("Montant", fnt2, Brushes.White, 21 * unite_largeur, 8 * unite_hauteur-5 );

            int j = 0;
            for (int i = start * 45; i < lstDepenses.Items.Count; i++)
            {
                int Yloc = unite_hauteur * j + 9 * unite_hauteur;


                graphic.DrawString(lstDepenses.Items[i].SubItems[2].Text, fnt2, Brushes.Black, unite_largeur, Yloc);
                if (lstDepenses.Items[i].SubItems[2].Text.ToUpper() == "Sous total".ToUpper())
                {
                    graphic.FillRectangle(Brushes.Lavender, unite_largeur, Yloc, 24 * unite_largeur, unite_hauteur);
                  
                    graphic.DrawString(lstDepenses.Items[i].SubItems[2].Text, fnt2, Brushes.Black, unite_largeur, Yloc);
                    graphic.DrawString(lstDepenses.Items[i].SubItems[6].Text, fnt2, Brushes.Black, 21 * unite_largeur, Yloc);
                }
                else if (lstDepenses.Items[i].SubItems[2].Text.ToUpper() == "TOTAL")
                {
                    graphic.FillRectangle(Brushes.White, unite_largeur, Yloc, 24 * unite_largeur, 3 * unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, 24 * unite_largeur, unite_hauteur + 3);
                    graphic.DrawString(lstDepenses.Items[i].SubItems[2].Text, fnt2, Brushes.Black, unite_largeur, Yloc);
                    graphic.DrawString(lstDepenses.Items[i].SubItems[6].Text, fnt2, Brushes.Black, 21 * unite_largeur + 5, Yloc);
                }
                else
                {
                    graphic.DrawString(lstDepenses.Items[i].SubItems[5].Text, fnt1, Brushes.Black, unite_largeur, Yloc);
                    graphic.DrawString(lstDepenses.Items[i].SubItems[3].Text, fnt1, Brushes.Black, 15 * unite_largeur - 15, Yloc);
                    graphic.DrawString(lstDepenses.Items[i].SubItems[4].Text, fnt1, Brushes.Black, 18 * unite_largeur, Yloc);
                    graphic.DrawString(lstDepenses.Items[i].SubItems[6].Text, fnt1, Brushes.Black, 21 * unite_largeur, Yloc);
                }
                j++;
            }

                graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, 24 * unite_largeur, 10 * unite_hauteur);
            
            return bitmap;
        }

        //fonction pour dessiner rapport depenses
        public static Bitmap ImprimerRapportDepensesGroupeParLibelle(ListView lstDepenses,string titre, int start)
        {
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 56 * unite_hauteur;
            //bool flag = false;
            //if (lstDepenses.Items.Count <= 45 * (1 + start))
            //{

            //    flag = true;
            //}
            //else
            //{
            //    flag = false;
            //}
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            #endregion


            Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold | FontStyle.Underline);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
            }
            catch { }
            graphic.DrawString("Page  " + (start + 1), fnt33, Brushes.Black, 12 * unite_largeur, 10);
            graphic.DrawString(titre, fnt1, Brushes.Black, 8* unite_largeur, 6 * unite_hauteur+5);
            graphic.FillRectangle(Brushes.Black, unite_largeur, 8 * unite_hauteur - 7, 23 * unite_largeur, unite_hauteur);

            graphic.DrawString("Libellé", fnt2, Brushes.White, 9 * unite_largeur - 5, 8 * unite_hauteur - 7);
            graphic.DrawString("Montant", fnt2, Brushes.White, 21 * unite_largeur - 5, 8 * unite_hauteur - 7);

            var j = 0;
            for (int i = start * 45; i <= lstDepenses.Items.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 9 * unite_hauteur - 2;

                if (lstDepenses.Items[i].SubItems[0].Text.ToUpper() == "TOTAL")
                {
                    graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, 23 * unite_largeur, unite_hauteur + 4);
                    graphic.DrawString(lstDepenses.Items[i].SubItems[0].Text, fnt2, Brushes.Black, 2 * unite_largeur, Yloc );
                    graphic.DrawString(lstDepenses.Items[i].SubItems[1].Text, fnt2, Brushes.Black, 21 * unite_largeur - 20, Yloc );

                }
                else
                {
                    graphic.DrawString(lstDepenses.Items[i].SubItems[0].Text, fnt1, Brushes.Black, 2 * unite_largeur, Yloc - 2);
                    graphic.DrawString(lstDepenses.Items[i].SubItems[1].Text, fnt1, Brushes.Black, 21 * unite_largeur - 10, Yloc - 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, 19 * unite_largeur - 3, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 20 * unite_largeur, Yloc, 4 * unite_largeur, unite_hauteur - 2);
                }
                j++;
            }
              graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur - 2, 24 * unite_largeur, 10 * unite_hauteur);
            
            return bitmap;
        }


        #region InventaireStock
        public static Bitmap ImprimerInventaireStockPage(DataGridView dgvInventaire, string typeInventaire,
            int index, DateTime dt, double total, bool flag)
        {

            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 56 * unite_hauteur;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);
            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion

            //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 10, FontStyle.Regular);
            Font fnt4 = new Font("Arial Narrow", 13, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 9, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 13F, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, 15, 5, 23 * unite_largeur, 4 * unite_hauteur + 5);
            }
            catch { }
            graphic.FillRectangle(Brushes.Lavender, 15, 4 * unite_hauteur + 20, unite_largeur * 23, unite_hauteur + 8);

            graphic.DrawString("Inventaire de stock " + typeInventaire + " du " + dt.ToString(), fnt3, Brushes.Black, unite_largeur * 5, 4 * unite_hauteur + 27);
            graphic.DrawString("Page " + (index + 1).ToString(), fnt1, Brushes.Lavender, 11 * unite_largeur, 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 7 * unite_hauteur - 5, unite_largeur - 3, unite_hauteur + 8);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 1 + 15, 7 * unite_hauteur - 5, unite_largeur * 9 + 15, unite_hauteur + 8);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 11, 7 * unite_hauteur - 5, unite_largeur * 3 - 3, unite_hauteur + 8);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 14, 7 * unite_hauteur - 5, unite_largeur * 2 - 3, unite_hauteur + 8);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 16, 7 * unite_hauteur - 5, unite_largeur * 2 - 3, unite_hauteur + 8);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 18, 7 * unite_hauteur - 5, unite_largeur * 2 - 3, unite_hauteur + 8);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 20, 7 * unite_hauteur - 5, unite_largeur * 3 + 17, unite_hauteur + 8);

            graphic.DrawString("N°".ToUpper(), fnt11, Brushes.Black, 20, 7 * unite_hauteur - 2);
            graphic.DrawString("designation".ToUpper(), fnt11, Brushes.Black, unite_largeur * 2, 7 * unite_hauteur - 2);
            graphic.DrawString("Expire le".ToUpper(), fnt11, Brushes.Black, 11 * unite_largeur + 15, 7 * unite_hauteur - 2);
            graphic.DrawString("STOCK \n PREC".ToUpper(), fnt11, Brushes.Black, 14 * unite_largeur + 10, 7 * unite_hauteur - 2);
            graphic.DrawString("STOCK \n REEL".ToUpper(), fnt11, Brushes.Black, 16 * unite_largeur + 10, 7 * unite_hauteur - 2);
            graphic.DrawString("Ecart".ToUpper(), fnt11, Brushes.Black, 18 * unite_largeur + 20, 7 * unite_hauteur - 2);
            graphic.DrawString("Observations".ToUpper(), fnt11, Brushes.Black, 20 * unite_largeur + 10, 7 * unite_hauteur - 2);

            int j = 0;
            var numero = 1 + (index * 45);
            for (int i = index * 45; i < dgvInventaire.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 8 * unite_hauteur + 5;
                if (string.IsNullOrEmpty(dgvInventaire.Rows[i].Cells[0].Value.ToString()) &&
                    string.IsNullOrEmpty(dgvInventaire.Rows[i].Cells[2].Value.ToString()))
                {
                    //numero--;
                    graphic.FillRectangle(Brushes.Lavender, 15, Yloc + 1, unite_largeur * 23 + 2, unite_hauteur - 2);
                    graphic.DrawString(dgvInventaire.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2 + 10, Yloc);
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur - 3, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, unite_largeur + 15, Yloc, unite_largeur * 9 + 15, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 11, Yloc, unite_largeur * 3 - 3, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 14, Yloc, unite_largeur * 2 - 3, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 16, Yloc, unite_largeur * 2 - 3, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 18, Yloc, unite_largeur * 2 - 3, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 20, Yloc, unite_largeur * 3 + 17, unite_hauteur + 0);

                    //graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, 20, Yloc);
                    graphic.DrawString(dgvInventaire.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 2 * unite_largeur + 10, Yloc);
                    graphic.DrawString(dgvInventaire.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur + 10, Yloc);
                    if (flag)
                    {
                        graphic.DrawString(dgvInventaire.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, 11 * unite_largeur + 10, Yloc);
                        graphic.DrawString(dgvInventaire.Rows[i].Cells[4].Value.ToString() + ",00", fnt1, Brushes.Black, 16 * unite_largeur + 10, Yloc);
                        graphic.DrawString(dgvInventaire.Rows[i].Cells[6].Value.ToString() + ",00", fnt1, Brushes.Black, 18 * unite_largeur + 10, Yloc);
                    }
                    numero++;
                }
                //total += double.Parse(dgvInventaire.Rows[i].Cells[4].Value.ToString())*
                j++;
            }
            graphic.FillRectangle(Brushes.White, 10, 53 * unite_hauteur + 6, unite_largeur * 26, unite_hauteur * 4);
            if (flag)
            {
                if (dgvInventaire.Rows.Count <= (index + 1) * +45)
                {
                    graphic.FillRectangle(Brushes.Lavender, unite_largeur, 54 * unite_hauteur + 1, unite_largeur * 26, unite_hauteur);
                    graphic.DrawString("Stock total", fnt1, Brushes.Black, 2 * unite_largeur, 54 * unite_hauteur + 2);
                    graphic.DrawString(total.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 15, 54 * unite_hauteur + 2);
                }
            }
            return bitmap;
        }

        //fonction pour dessiner le stock inventaire
        public static Bitmap ImprimerInventaireStockAnnuel(string titre, DataGridView dgvView , int start, string depot)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 15;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 55 * unite_hauteur;
            bool flag = false;
      
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            #endregion


            Font fnt1 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 9.5f, FontStyle.Regular);
            Font fnt3 = new Font("Arial Narrow", 11.5F, FontStyle.Bold );
            Font fnt33 = new Font("Arial Narrow", 10, FontStyle.Regular);
            Font fnt2 = new Font("Arial Narrow", 9, FontStyle.Bold);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 22 * unite_largeur, 3 * unite_hauteur + 15);
            }
            catch { }

            graphic.DrawString("Page " + (start + 1).ToString(), fnt33, Brushes.Black, 22 * unite_largeur, 0);

            graphic.DrawString(titre, fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur - 5);

            graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 8 * unite_hauteur - 2, 24 * unite_largeur+0, unite_hauteur);
            if (depot == "Pharmacie de cession" || depot == "Grand depot")
            {
                graphic.DrawString("Désignation", fnt2, Brushes.White, unite_largeur, 8 * unite_hauteur);
                graphic.DrawString("Quantité ", fnt2, Brushes.White, 13 * unite_largeur -5, 8 * unite_hauteur);
                graphic.DrawString("Px. achat", fnt2, Brushes.White, 14 * unite_largeur + 25, 8 * unite_hauteur);
                graphic.DrawString("Px vente", fnt2, Brushes.White, 17 * unite_largeur-5, 8 * unite_hauteur);
                graphic.DrawString("Achat total", fnt2, Brushes.White, 19 * unite_largeur + 8, 8 * unite_hauteur);
                graphic.DrawString("Vente Total", fnt2, Brushes.White, 22 * unite_largeur + 8, 8 * unite_hauteur);
            }
            else
            {
                graphic.DrawString("Désignation", fnt2, Brushes.White, unite_largeur+10, 8 * unite_hauteur);
                graphic.DrawString("Qté Ph ", fnt2, Brushes.White, 9 * unite_largeur + 12, 8 * unite_hauteur);
                graphic.DrawString("Gr. depôt", fnt2, Brushes.White, 11 * unite_largeur+8, 8 * unite_hauteur);
                graphic.DrawString("Stock Tot", fnt2, Brushes.White, 13 * unite_largeur+1, 8 * unite_hauteur);
                graphic.DrawString("Px. achat", fnt2, Brushes.White, 15 * unite_largeur + 3, 8 * unite_hauteur);
                graphic.DrawString("Px vente", fnt2, Brushes.White, 17 * unite_largeur+1, 8 * unite_hauteur);
                graphic.DrawString("Achat total", fnt2, Brushes.White, 19 * unite_largeur + 15, 8 * unite_hauteur);
                graphic.DrawString("Vente Total", fnt2, Brushes.White, 22 * unite_largeur + 8, 8 * unite_hauteur);
            }

            var j = 0;
            for (var i = start * 45; i <= dgvView.Rows.Count - 1; i++)
            {
                var Yloc = unite_hauteur * j + 9 * unite_hauteur;
                if (depot == "Pharmacie de cession" || depot == "Grand depot")
                {

                    if (dgvView.Rows[i].Cells[1].Value.ToString() == "")
                    {
                        graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 24 , unite_hauteur - 2);
                        graphic.DrawString(dgvView.Rows[i].Cells[0].Value.ToString(), fnt3, Brushes.Black, unite_largeur + 10, Yloc);
                     
                        graphic.DrawString(dgvView.Rows[i].Cells[6].Value.ToString(), fnt3, Brushes.Black, 19 * unite_largeur + 10, Yloc);
                        graphic.DrawString(dgvView.Rows[i].Cells[7].Value.ToString(), fnt3, Brushes.Black, 22 * unite_largeur + 20, Yloc);
                    }
                    else
                    {
                        graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 11 + 19, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 12 + 22, Yloc, 2 * unite_largeur - 2, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 14 + 22, Yloc, unite_largeur * 2 - 2, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 16 + 22, Yloc, unite_largeur * 2 - 2, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 18 + 22, Yloc, unite_largeur * 3 + 8, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 22, Yloc, unite_largeur * 3, unite_hauteur - 2);
                        graphic.DrawString(dgvView.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black,  unite_largeur +10, Yloc);
                        if (depot == "Pharmacie de cession")
                                                graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[1].Value.ToString())), fnt1, Brushes.Black, 13 * unite_largeur , Yloc);
                        else
                                         graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[2].Value.ToString())), fnt1, Brushes.Black, 13 * unite_largeur, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[4].Value.ToString())), fnt1, Brushes.Black, 15 * unite_largeur, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[5].Value.ToString())), fnt1, Brushes.Black, 17 * unite_largeur, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[6].Value.ToString())), fnt1, Brushes.Black, 19 * unite_largeur+10, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[7].Value.ToString())), fnt1, Brushes.Black, 22 * unite_largeur + 20, Yloc);
                    }
                }
                else
                {
                           if (dgvView.Rows[i].Cells[1].Value.ToString() == "")
                    {
                        graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 24, unite_hauteur - 2);
                        graphic.DrawString(dgvView.Rows[i].Cells[0].Value.ToString(), fnt3, Brushes.Black, unite_largeur + 10, Yloc);

                        graphic.DrawString(dgvView.Rows[i].Cells[6].Value.ToString(), fnt3, Brushes.Black, 19 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dgvView.Rows[i].Cells[7].Value.ToString(), fnt3, Brushes.Black, 22 * unite_largeur + 15, Yloc);
                    }
                    else
                    {
                        graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 8 +7, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 9+10, Yloc, 2 * unite_largeur - 7, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 11+5, Yloc, 2 * unite_largeur - 7, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 13, Yloc, 2 * unite_largeur - 2, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 15, Yloc, unite_largeur * 2 - 2, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 17, Yloc, unite_largeur * 2 - 2, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 19, Yloc, unite_largeur * 3 - 2, unite_hauteur - 2);
                        graphic.DrawRectangle(Pens.Black, unite_largeur * 22, Yloc, unite_largeur * 3, unite_hauteur - 2);
                        var designation = dgvView.Rows[i].Cells[0].Value.ToString();
                        if (designation.Length > 37)
                            designation = designation.Substring(0, 37);
                        graphic.DrawString(designation , fnt11, Brushes.Black, unite_largeur + 10, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[1].Value.ToString())), fnt1, Brushes.Black, 9 * unite_largeur + 13, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[2].Value.ToString())), fnt1, Brushes.Black, 11 * unite_largeur + 12, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[3].Value.ToString())), fnt1, Brushes.Black, 13 * unite_largeur+10, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[4].Value.ToString())), fnt1, Brushes.Black, 15 * unite_largeur+10, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[5].Value.ToString())), fnt1, Brushes.Black, 17 * unite_largeur+10, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[6].Value.ToString())), fnt1, Brushes.Black, 19 * unite_largeur + 15, Yloc);
                        graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dgvView.Rows[i].Cells[7].Value.ToString())), fnt1, Brushes.Black, 22 * unite_largeur + 15, Yloc);
                    }
                }


                
                j++;
            }
            graphic.FillRectangle(Brushes.White, unite_largeur, 53 * unite_hauteur, 25 * unite_largeur + 15, 3 * unite_hauteur);
     
            return bitmap;
        }
        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        #endregion

        public static Bitmap ImprimerRetourCommande(DataGridView listView, string fournisseur)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 45 * unite_hauteur + detail_hauteur_facture;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion

           Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
           Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
           Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold | FontStyle.Underline);
           Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
           Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
           try
           {
               Image logo = global::GestionPharmacetique.Properties.Resources.logo;
               graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
           }
           catch { } 
           graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur - 10, unite_largeur * 23, 2 * unite_hauteur - 5);

           graphic.DrawString("BON RETOUR".ToUpper(), fnt3, Brushes.Black, 2*unite_largeur, 8 * unite_hauteur-5);
           graphic.DrawString(fournisseur.ToUpper(), fnt2, Brushes.Black, 18 * unite_largeur, 8 * unite_hauteur-5);
          
           graphic.DrawRectangle(Pens.Black, unite_largeur, 10 * unite_hauteur , unite_largeur * 23, 2 * unite_hauteur - 5);

           graphic.DrawString("Designation".ToUpper(), fnt2, Brushes.Black, 2 * unite_largeur - 15, 11 * unite_hauteur - 15);
           graphic.DrawString("Qté".ToUpper(), fnt2, Brushes.Black, 10 * unite_largeur - 15, 11 * unite_hauteur - 15);
           graphic.DrawString("Prix".ToUpper(), fnt2, Brushes.Black, 11 * unite_largeur + 15, 11 * unite_hauteur - 15);
           graphic.DrawString("Total".ToUpper(), fnt2, Brushes.Black, 13 * unite_largeur + 15, 11 * unite_hauteur - 15);
           graphic.DrawString("Raison".ToUpper(), fnt2, Brushes.Black, 15 * unite_largeur + 15, 11 * unite_hauteur-15);

           graphic.DrawRectangle(Pens.Black, unite_largeur, 12*unite_hauteur-5, unite_largeur * 23 , 40*unite_hauteur);

           var total = 0.0;
           for (int i = 0; i < listView.Rows.Count; i++)
           {
               int Yloc = unite_hauteur * i + 12* unite_hauteur ;

               //graphic.DrawRectangle(Pens.Black, unite_largeur * 4, Yloc, unite_largeur * 8 - 3, unite_hauteur);
               //graphic.DrawRectangle(Pens.Black, unite_largeur * 12, Yloc, unite_largeur * 2 - 3, unite_hauteur);
               //graphic.DrawRectangle(Pens.Black, unite_largeur * 14, Yloc, unite_largeur * 10, unite_hauteur);

               graphic.DrawString(listView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 2 * unite_largeur-15, Yloc);
               graphic.DrawString(listView.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 10 * unite_largeur-15, Yloc);
               graphic.DrawString(listView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 11 * unite_largeur+15, Yloc);
               graphic.DrawString(listView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur+15, Yloc);
               graphic.DrawString(listView.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur+15, Yloc);
               total += Convert.ToDouble(listView.Rows[i].Cells[5].Value.ToString());
           }
           graphic.DrawRectangle(Pens.Black, unite_largeur, 52 * unite_hauteur - 5, unite_largeur * 23, 2 * unite_hauteur - 5);
           graphic.DrawString("MONTANT TOTAL".ToUpper(), fnt2, Brushes.Black, unite_largeur + 15, 52 * unite_hauteur );
           graphic.DrawString(total.ToString(), fnt2, Brushes.Black, 20 * unite_largeur + 15, 52 * unite_hauteur );

           return bitmap;
       }

       public static Bitmap ImprimerRapportRetourCommande(DataGridView listView, string titre)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 45 * unite_hauteur + detail_hauteur_facture;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion

           Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
           Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
           Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold | FontStyle.Underline);
           Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
           Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
           try
           {
               Image logo = global::GestionPharmacetique.Properties.Resources.logo;
               graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
           }
           catch { } 
           graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur - 10, unite_largeur * 23, 2 * unite_hauteur - 5);

           graphic.DrawString(titre, fnt2, Brushes.Black, 2 * unite_largeur, 8 * unite_hauteur - 5);
           
           graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, 10 * unite_hauteur, 23 * unite_largeur, unite_hauteur + 10);
           graphic.DrawString("Date".ToUpper(), fnt2, Brushes.White, unite_largeur + 15, 11 * unite_hauteur - 15);
           graphic.DrawString("Designation".ToUpper(), fnt2, Brushes.White, 5 * unite_largeur - 15, 11 * unite_hauteur - 15);
           graphic.DrawString("Qté".ToUpper(), fnt2, Brushes.White, 16 * unite_largeur - 15, 11 * unite_hauteur - 15);
           graphic.DrawString("Prix".ToUpper(), fnt2, Brushes.White, 18 * unite_largeur + 15, 11 * unite_hauteur - 15);
           graphic.DrawString("Total".ToUpper(), fnt2, Brushes.White, 21 * unite_largeur + 15, 11 * unite_hauteur - 15);
          
           
           for (int i = 0; i < listView.Rows.Count; i++)
           {
               int Yloc = unite_hauteur * i + 12 * unite_hauteur;

               graphic.DrawLine(Pens.Salmon, unite_largeur, Yloc, unite_largeur * 24, Yloc);

               if (i % 2 == 1)
                   graphic.FillRectangle(Brushes.PeachPuff, unite_largeur, Yloc, unite_largeur * 23, unite_hauteur);
                
               graphic.DrawString(listView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black,  unite_largeur + 15, Yloc);
               graphic.DrawString(listView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 5 * unite_largeur - 15, Yloc);
               graphic.DrawString(listView.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur - 15, Yloc);
               graphic.DrawString(listView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur + 15, Yloc);
               graphic.DrawString(listView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur + 15, Yloc);
               
           }
           graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, unite_hauteur * listView.Rows.Count + 12 * unite_hauteur+2, 23 * unite_largeur, 4);
         
           return bitmap;
       }

       public static Bitmap ImprimerTableJournaliere(DateTime date)
       {
           //les dimension de la facture
           #region
           int unite_hauteur = 23;
           int unite_largeur = 32;
           int largeur_facture = 49 * unite_hauteur;
           int hauteur_facture = 25 * unite_largeur + 2;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           
           #endregion


           //definir les polices 
           Font fnt1 = new Font("Eras ITC", 13, FontStyle.Regular);
           Font fnt11 = new Font("Eras ITC", 13, FontStyle.Bold);
           Font fnt3 = new Font("Eras ITC", 18, FontStyle.Bold);
           Font fnt33 = new Font("Eras ITC", 11, FontStyle.Regular);
           // dessiner les ecritures 
           try
           {
               Image logo = global::GestionPharmacetique.Properties.Resources.logo;
               graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
           }
           catch { } 
           graphic.DrawString("Recapitulatif de vente journaliere du " + date.ToShortDateString(), fnt11, Brushes.Black, unite_largeur, 6 * unite_hauteur + 15);
           graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur, unite_largeur * 13 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 14, 8 * unite_hauteur, unite_largeur * 4 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 18, 8 * unite_hauteur, unite_largeur * 5 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 23, 8 * unite_hauteur, unite_largeur * 4 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 27, 8 * unite_hauteur, unite_largeur * 4 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 31, 8 * unite_hauteur, unite_largeur * 4, unite_hauteur);
           graphic.DrawString("Nom employe".ToUpper(), fnt1, Brushes.Black, unite_largeur + 10, 8 * unite_hauteur);
           graphic.DrawString("Ventes ".ToUpper(), fnt1, Brushes.Black, 14 * unite_largeur + 10, 8 * unite_hauteur);
           graphic.DrawString("Depenses".ToUpper(), fnt1, Brushes.Black, 18 * unite_largeur + 10, 8 * unite_hauteur);
           graphic.DrawString("Soldes".ToUpper(), fnt1, Brushes.Black, 23 * unite_largeur + 10, 8 * unite_hauteur);
           graphic.DrawString("Versements".ToUpper(), fnt1, Brushes.Black, 27 * unite_largeur + 10, 8 * unite_hauteur);
           graphic.DrawString("Differences".ToUpper(), fnt1, Brushes.Black, 31 * unite_largeur + 10, 8 * unite_hauteur);

           DataTable dtCaisse = ConnectionClass.ListeEmployeALaCaisse(date,date);
           decimal totalCaisse = 0;
           decimal totalCredit = 0;
           decimal totalEncaissement = 0;
           decimal totalDifference = 0;
           decimal grdTotal = 0;
           int Loc = 0;
           for (int i = 0; i < dtCaisse.Rows.Count; i++)
           {
               int Yloc = unite_hauteur * i + 9 * unite_hauteur;
               string nomEmploye = dtCaisse.Rows[i].ItemArray[0].ToString();
               var dtEncaissement = ConnectionClass.ListeDesEncaissements(nomEmploye, date,date);
               var listeEmploye = ConnectionClass.ListeDesEmployees("nom_empl", "'" + nomEmploye + "'");
               string numEmploye = listeEmploye[0].NumMatricule;
               var dtDepenses = ConnectionClass.ListeDesDepenses(numEmploye, date);
               var montantEncaisse = 0m;
               var caisse = decimal.Parse(dtCaisse.Rows[i].ItemArray[1].ToString());// le montant total recu a la caisse
               var difference = 0m;
               decimal montantDepenses = 0m;
               if (dtEncaissement.Rows.Count > 0)
               {
                   montantEncaisse = decimal.Parse(dtEncaissement.Rows[0].ItemArray[0].ToString());
               }
               else
               {
                   montantEncaisse = 0;
               }
               if (decimal.TryParse(dtDepenses.Rows[0].ItemArray[0].ToString(), out montantDepenses))
               {

               }
               else
               {
                   montantDepenses = 0;
               }

               var solde = caisse - montantDepenses; // total = montant recu a la caisse + paiement de credit
               difference = montantEncaisse - solde; // la difference qui lq soustraction du montant total total recu a la caisse par le montant encaisse

               graphic.DrawString(nomEmploye.ToUpper(), fnt1, Brushes.Black, unite_largeur + 10, Yloc);
               graphic.DrawString(caisse.ToString(), fnt1, Brushes.Black, 14 * unite_largeur + 10, Yloc);

               graphic.DrawString(montantDepenses.ToString(), fnt1, Brushes.Black, 18 * unite_largeur + 10, Yloc);
               graphic.DrawString(solde.ToString(), fnt1, Brushes.Black, 23 * unite_largeur + 10, Yloc);
               graphic.DrawString(montantEncaisse.ToString(), fnt1, Brushes.Black, 27 * unite_largeur + 10, Yloc);
               graphic.DrawString(difference.ToString(), fnt1, Brushes.Black, 31 * unite_largeur + 10, Yloc);

               graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 13 - 3, unite_hauteur);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 14, Yloc, unite_largeur * 4 - 3, unite_hauteur);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 18, Yloc, unite_largeur * 5 - 3, unite_hauteur);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 23, Yloc, unite_largeur * 4 - 3, unite_hauteur);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 27, Yloc, unite_largeur * 4 - 3, unite_hauteur);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 31, Yloc, unite_largeur * 4, unite_hauteur);

               grdTotal += solde;
               totalCaisse += caisse;
               totalCredit += montantDepenses;
               totalDifference += difference;
               totalEncaissement += montantEncaisse;
           }
           Loc = dtCaisse.Rows.Count * unite_hauteur + 9 * unite_hauteur + 5;
           graphic.DrawRectangle(Pens.Black, unite_largeur, Loc, unite_largeur * 13 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 14, Loc, unite_largeur * 4 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 18, Loc, unite_largeur * 5 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 23, Loc, unite_largeur * 4 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 27, Loc, unite_largeur * 4 - 3, unite_hauteur);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 31, Loc, unite_largeur * 4, unite_hauteur);

           graphic.DrawString("Total ", fnt1, Brushes.Black, unite_largeur + 10, Loc);
           graphic.DrawString(totalCaisse.ToString(), fnt1, Brushes.Black, 14 * unite_largeur + 10, Loc);
           graphic.DrawString(totalCredit.ToString(), fnt1, Brushes.Black, 18 * unite_largeur + 10, Loc);
           graphic.DrawString(grdTotal.ToString(), fnt1, Brushes.Black, 23 * unite_largeur + 10, Loc);
           graphic.DrawString(totalEncaissement.ToString(), fnt1, Brushes.Black, 27 * unite_largeur + 10, Loc);
           graphic.DrawString(totalDifference.ToString(), fnt1, Brushes.Black, 31 * unite_largeur + 10, Loc);
           return bitmap;
       }

   
   }
}
