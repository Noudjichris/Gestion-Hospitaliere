using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

namespace GestionPharmacetique.AppCode
{
     public class Impression
    {

        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        #region ImprimerLivraison
        //fonction pour dessiner la livraison
        public static Bitmap ImprimerBonDeCommande(DataGridView lstInventaire, string numeroLivraison,
         string nomFournisseur, string dateLivraison, int start)
        {
            //les dimension de la facture
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
            Font fnt1 = new Font("Arial Unicode MS", 10.5f, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 10.5f, FontStyle.Bold);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 16, FontStyle.Bold | FontStyle.Underline);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 5, 23 * unite_largeur, 5 * unite_hauteur + 0);

            }
            catch (Exception)
            { }



            graphic.DrawString("Page " + (start + 1).ToString(), fnt1, Brushes.Black, 12 * unite_largeur - 13, unite_hauteur);

            graphic.DrawString("Nom fournisseur : " + nomFournisseur, fnt1, Brushes.Black, unite_largeur, 6 * unite_hauteur);
            graphic.DrawString("Date commande : " + dateLivraison, fnt1, Brushes.Black, 18*unite_largeur, 6 * unite_hauteur);

            graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur - 15, 23 * unite_largeur, 2 * unite_hauteur - 10);

            graphic.DrawString("BON DE COMMANDE N° " + numeroLivraison, fnt3, Brushes.Black, 8 * unite_largeur, 8 * unite_hauteur - 15);

            graphic.FillRectangle(Brushes.Lavender, unite_largeur, 9 * unite_hauteur + 10, 23 * unite_largeur, unite_hauteur + 5);

            graphic.DrawString("N°", fnt2, Brushes.Black, unite_largeur + 5, 9 * unite_hauteur + 3);
            graphic.DrawString("Désignation", fnt2, Brushes.Black, 2 * unite_largeur + 5, 9 * unite_hauteur + 13);
            graphic.DrawString("Qté ", fnt2, Brushes.Black, 18 * unite_largeur - 10, 9 * unite_hauteur + 13);
            graphic.DrawString("Prix Unit ", fnt2, Brushes.Black, 20 * unite_largeur - 10, 9 * unite_hauteur + 13);
            graphic.DrawString("Prix Total ", fnt2, Brushes.Black, 22 * unite_largeur - 10, 9 * unite_hauteur + 13);
            var j = 0;
            var total = .0;
            for (int i = start * 44; i <= lstInventaire.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 11 * unite_hauteur;

                graphic.DrawString((i + (start * 44) + 1).ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 2 * unite_largeur + 5, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur - 10, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur - 10, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 22 * unite_largeur - 10, Yloc);
                total += double.Parse(lstInventaire.Rows[i].Cells[5].Value.ToString()) * double.Parse(lstInventaire.Rows[i].Cells[3].Value.ToString());
                j++;
            }
            var LOC = 12 * unite_hauteur + lstInventaire.Rows.Count * unite_hauteur;
            graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, unite_largeur * 24 + 10, unite_hauteur * 10);
            graphic.DrawLine(Pens.Black, unite_largeur, LOC, unite_largeur * 24, LOC);
            graphic.DrawString("Total", fnt2, Brushes.Black, unite_largeur, LOC + unite_hauteur);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", total) + "F. CFA", fnt2, Brushes.Black, 20 * unite_largeur - 10, LOC + unite_hauteur);
            return bitmap;
        }

        public static Bitmap ImprimerBonDeLivraison(DataGridView lstInventaire, string numeroLivraison,
    string nomFournisseur, string dateLivraison, int start)
        {
            //les dimension de la facture
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
            Font fnt1 = new Font("Arial Unicode MS", 10.5f, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 16, FontStyle.Bold | FontStyle.Underline);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 5, 23 * unite_largeur, 4 * unite_hauteur + 10);

            }
            catch (Exception)
            { }

            graphic.DrawString("Page " + (start + 1).ToString(), fnt1, Brushes.Black, 12 * unite_largeur - 15, unite_hauteur);

            graphic.DrawString("Nom fournisseur : " + nomFournisseur, fnt1, Brushes.Black, unite_largeur, 6 * unite_hauteur);
            graphic.DrawString("Date livraison : " + dateLivraison, fnt1, Brushes.Black,18* unite_largeur, 6 * unite_hauteur);

            graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur - 15, 23 * unite_largeur, 2 * unite_hauteur - 10);

            graphic.DrawString("BON DE LIVRAISON N° " + numeroLivraison, fnt3, Brushes.Black, 8 * unite_largeur, 8 * unite_hauteur - 15);

            graphic.FillRectangle(Brushes.Lavender, unite_largeur, 9 * unite_hauteur, 23 * unite_largeur, unite_hauteur + 5);

            graphic.DrawString("N°", fnt1, Brushes.Black, unite_largeur + 5, 9 * unite_hauteur + 3);
            graphic.DrawString("Article", fnt1, Brushes.Black, 2 * unite_largeur + 5, 9 * unite_hauteur + 3);
            graphic.DrawString("Qté cmdée ", fnt1, Brushes.Black, 13 * unite_largeur + 6, 9 * unite_hauteur + 3);
            graphic.DrawString("Qté livrée ", fnt1, Brushes.Black, 16 * unite_largeur - 16, 9 * unite_hauteur + 3);
            graphic.DrawString("No Lot", fnt1, Brushes.Black, 18 * unite_largeur - 10, 9 * unite_hauteur + 3);
            graphic.DrawString("Date exp ", fnt1, Brushes.Black, 21 * unite_largeur - 10, 9 * unite_hauteur + 3);
            var j = 0;
            for (int i = start * 44; i <= lstInventaire.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 11 * unite_hauteur;

                graphic.DrawString((i + (start * 44) + 1).ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 2 * unite_largeur + 5, Yloc);
                graphic.DrawString(ConnectionClass.ListeParIDLivraisons(Convert.ToInt32(numeroLivraison), Convert.ToInt32(lstInventaire.Rows[i].Cells[0].Value.ToString()))[0].QuantiteCommandee.ToString()
                    , fnt1, Brushes.Black, 13 * unite_largeur + 6, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur - 16, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur - 10, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[8].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur - 10, Yloc);
                j++;
            }
            var LOC = 12 * unite_hauteur + lstInventaire.Rows.Count * unite_hauteur;
            graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, unite_largeur * 24 + 10, unite_hauteur * 10);
            graphic.DrawLine(Pens.Black, unite_largeur, LOC, unite_largeur * 24, LOC);

            return bitmap;
        }
        public static Bitmap ImprimerLivraison(DataGridView lstInventaire, Livraison livraison, int start)
        {
            //les dimension de la facture
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
            Font fnt1 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold | FontStyle.Underline);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 5, 23 * unite_largeur, 4* unite_hauteur + 15);

            }
            catch (Exception)
            { }



            graphic.DrawString("Page " + (start + 1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, unite_hauteur);

            graphic.DrawString("Nom fournisseur : " + livraison.NomFournisseur, fnt1, Brushes.Black, 1 * unite_largeur,6 * unite_hauteur);
            graphic.DrawString("Date facture " + livraison.DateLivraison.ToShortDateString(), fnt1, Brushes.Black, 18*unite_largeur, 6 * unite_hauteur);

            graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur - 15, 23 * unite_largeur, 2 * unite_hauteur - 10);

            graphic.DrawString("Facture N° " + livraison.NumeroFacture, fnt3, Brushes.Black, 10 * unite_largeur, 8 * unite_hauteur - 15);

            graphic.FillRectangle(Brushes.Lavender, unite_largeur, 10 * unite_hauteur - 5, 23 * unite_largeur, unite_hauteur + 5);
            graphic.DrawString("N°", fnt2, Brushes.Black, unite_largeur + 5, 10 * unite_hauteur - 5);
            graphic.DrawString("Article", fnt2, Brushes.Black, 2 * unite_largeur + 5, 10 * unite_hauteur - 5);
            graphic.DrawString("Qté", fnt2, Brushes.Black, 16 * unite_largeur + 15, 10 * unite_hauteur - 5);
            graphic.DrawString("Px achat", fnt2, Brushes.Black, 18 * unite_largeur, 10 * unite_hauteur - 5);
            graphic.DrawString("Px Total", fnt2, Brushes.Black, 21 * unite_largeur, 10 * unite_hauteur - 5);

            var j = 0;
            for (int i = start * 44; i <= lstInventaire.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 11 * unite_hauteur;

                graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 2 * unite_largeur + 5, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur + 15, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur + 5, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur, Yloc);
                j++;
            }
            graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, unite_largeur * 24 + 10, unite_hauteur * 10);

            if (lstInventaire.Rows.Count <= 43 * (1 + start))
            {
                graphic.DrawRectangle(Pens.Black, unite_largeur, 54 * unite_hauteur, 23 * unite_largeur, 2 * unite_hauteur - 15);
                graphic.DrawString("Mx. HT : " + String.Format(elGR, "{0:0,0}", livraison.MontantFactural) + " .FCFA", fnt2, Brushes.Black, unite_largeur, 54 * unite_hauteur + 2);
                graphic.DrawString("A. Charges  : " + String.Format(elGR, "{0:0,0}", livraison.AutresCharges) + " .FCFA", fnt2, Brushes.Black, 8 * unite_largeur + 20, 54 * unite_hauteur + 2);
                graphic.DrawString("Mx. TTC  : " + String.Format(elGR, "{0:0,0}", (livraison.AutresCharges + livraison.MontantFactural)) + "  FCFA", fnt2, Brushes.Black, 16 * unite_largeur, 54 * unite_hauteur + 2);
            }
            return bitmap;
        }

        #endregion



        //fonction pour dessiner le stock expires
        public static Bitmap ImprimerInventaireStockExpires(DataGridView dgv, int start)
        {

            #region
            int unite_hauteur = 17;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int hauteur_facture = 58 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion


            //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);

            }
            catch (Exception)
            { }


            graphic.DrawString("Page " + (start + 1).ToString(), fnt33, Brushes.Black, 22 * unite_largeur, unite_hauteur);

            graphic.DrawString("stocks médicaments en cour d'expiration ou expirés ", fnt3, Brushes.Black, unite_largeur, 5 * unite_hauteur + 10);

            graphic.DrawLine(Pens.Black, unite_largeur, 8 * unite_hauteur, 24 * unite_largeur + 10, 8 * unite_hauteur);
            graphic.DrawLine(Pens.Black, unite_largeur, 9 * unite_hauteur + 5, 24 * unite_largeur + 10, 9 * unite_hauteur + 5);

            //graphic.DrawString("Code médi", fnt1, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Nom médicament", fnt11, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Expire le", fnt11, Brushes.Black, 13 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Stock", fnt11, Brushes.Black, 16 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Prix achat", fnt11, Brushes.Black, 18 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Prix total", fnt11, Brushes.Black, 21 * unite_largeur + 0, 8 * unite_hauteur);

            var j = 0;
            for (var i = start * 45; i <= dgv.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 10 * unite_hauteur;
                if (string.IsNullOrEmpty(dgv.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.DrawString(dgv.Rows[i].Cells[0].Value.ToString(), fnt11, Brushes.Black, unite_largeur, Yloc);
                    graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.Black, 15 * unite_largeur, Yloc);
                    graphic.DrawString(dgv.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, 16 * unite_largeur, Yloc);
                    graphic.DrawString(dgv.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.Black, 18 * unite_largeur, Yloc);
                    graphic.DrawString(dgv.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.Black, 21 * unite_largeur + 0, Yloc);

                }
                else
                {
                    graphic.DrawString(dgv.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                    graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur, Yloc);
                    graphic.DrawString(dgv.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur, Yloc);
                    graphic.DrawString(dgv.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur, Yloc);
                    graphic.DrawString(dgv.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur + 0, Yloc);
                }
                j++;
            }

            graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, unite_largeur * 24 + 10, 2 * unite_hauteur + 10);

            return bitmap;
        }
    

        //fonction pour dessiner le stock inventaire
        public static Bitmap ImprimerFacture(int numVente, DataGridView dgvVente,
            Client client, DateTime dateVente, string titre, string typeVente, Double totalRemise)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 18 * unite_largeur + 10;
            int detail_hauteur_facture = 23 * unite_hauteur;
            int hauteur_facture = 43 * unite_hauteur;


            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion


            //definir les polices 
            Font fnt1 = new Font("Ubuntu", 11, FontStyle.Regular);
            Font fnt11 = new Font("Ubuntu", 18, FontStyle.Bold);
            Font fnt3 = new Font("Ubuntu", 11, FontStyle.Bold);
            Font fnt2 = new Font("Ubuntu", 15, FontStyle.Bold | FontStyle.Underline);
            Font fnt33 = new Font("Ubuntu", 10, FontStyle.Regular);
            Font fnt4 = new Font("Arial Narrow", 12.0f, FontStyle.Bold);


            Image logo = global::GestionPharmacetique.Properties.Resources.logo;
            graphic.DrawImage(logo, 23, 18, 17 * unite_largeur + 0, 5 * unite_hauteur+8);


            logo = global::GestionPharmacetique.Properties.Resources.rectangle;
            graphic.DrawImage(logo, 23, 8 * unite_hauteur + 8, 16 * unite_largeur + 24, 32);

            graphic.DrawString(titre.ToUpper() + numVente, fnt2, Brushes.Black, unite_largeur * 6, 8 * unite_hauteur + 8);
            graphic.DrawString("Date : ".ToUpper() + dateVente.ToShortDateString(), fnt33, Brushes.Black, 14 * unite_largeur - 5, 7 * unite_hauteur + 5);

            graphic.DrawString("NIF : 9017308A ", fnt4, Brushes.Black, 11 * unite_largeur + 18, 5 * unite_hauteur + 0);
            
            graphic.DrawString("Nom client : ".ToUpper() + client.NomClient.ToUpper(System.Globalization.CultureInfo.CurrentCulture), fnt33, Brushes.Black, unite_largeur, 7 * unite_hauteur + 5);
            if (client.Entreprise != null)
                graphic.DrawString("INSTITUTION : ".ToUpper() + client.Entreprise.ToUpper(), fnt33, Brushes.Black, unite_largeur, 6 * unite_hauteur + 10);

            logo = global::GestionPharmacetique.Properties.Resources.enteteFact;
            graphic.DrawImage(logo, 23, 10 * unite_hauteur, 16 * unite_largeur + 24, 50);


            graphic.DrawRectangle(Pens.Black, 26, 10 * unite_hauteur + 40, 1 * unite_largeur + 6, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 2 * unite_largeur, 10 * unite_hauteur + 40, 9 * unite_largeur + 7, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 7, 10 * unite_hauteur + 40, 1 * unite_largeur + 14, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 21, 10 * unite_hauteur + 40, 2 * unite_largeur + 7, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 28, 10 * unite_hauteur + 40, 2 * unite_largeur + 15, detail_hauteur_facture);

            int Loc = 0;
            decimal montant = 0, montantTotal = 0;

            for (int i = 0; i <= dgvVente.Rows.Count - 1; i++)
            {
                int Yloc = 12 * unite_hauteur + 5 + i * unite_hauteur;
                graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, 2 * unite_largeur - 25, Yloc);
                if (dgvVente.Rows[i].Cells[1].Value.ToString().Length > 30)
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().Substring(0, 30).ToUpper() + "...", fnt1, Brushes.Black, 3 * unite_largeur - 15, Yloc);
                }
                else
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 3 * unite_largeur - 15, Yloc);
                }
                graphic.DrawString(dgvVente.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 11 * unite_largeur + 25, Yloc);
                graphic.DrawString(dgvVente.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur + 10, Yloc);
                var total = Decimal.Parse(dgvVente.Rows[i].Cells[5].Value.ToString()) * Decimal.Parse(dgvVente.Rows[i].Cells[3].Value.ToString());
                graphic.DrawString(total.ToString(), fnt1, Brushes.Black, 15 * unite_largeur + 15, Yloc);
                montant += total;
                montantTotal += Decimal.Parse(dgvVente.Rows[i].Cells[6].Value.ToString());
            }
            Loc += 12 * unite_hauteur + detail_hauteur_facture;
            graphic.FillRectangle(Brushes.White, 22, Loc, 16 * unite_largeur + 4, 6 * unite_hauteur);

            logo = global::GestionPharmacetique.Properties.Resources.rectangle;
            //graphic.DrawImage(logo, 22, Loc, 16 * unite_largeur + 26, 35);
            graphic.DrawRectangle(Pens.Black, 26, Loc, 16 * unite_largeur + 18, unite_hauteur + 5);

            graphic.DrawString("Total ", fnt3, Brushes.Black, unite_largeur, Loc + 3);
            graphic.DrawString(montantTotal.ToString() + " FCFA", fnt3, Brushes.Black, 14 * unite_largeur, Loc + 3);
            if (totalRemise > 0)
            {
                graphic.DrawString("Total remise :  " + totalRemise.ToString() + " FCFA", fnt3, Brushes.Black, 11 * unite_largeur + 0, Loc + 2 * unite_hauteur-10);
                graphic.DrawString("Net à payer :  " +(montantTotal - (decimal)totalRemise).ToString()+ " FCFA", fnt3, Brushes.Black, 11 * unite_largeur + 0, Loc + 3 * unite_hauteur-10);
            }
            graphic.DrawString(typeVente + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 25, Loc + 2 * unite_hauteur);
            graphic.DrawString("Merci pour votre visite ", fnt1, Brushes.Black, 25, Loc + 3 * unite_hauteur);
            graphic.DrawString("Bonne guerison ", fnt1, Brushes.Black, 13 * unite_largeur, Loc + 4 * unite_hauteur);
            graphic.DrawString("CAISSIER(E)  " + Form1.nomEmploye, fnt3, Brushes.Black, 5 * unite_largeur, Loc + 5 * unite_hauteur - 15);
            return bitmap;
        }

        public static Bitmap ImprimerFacturePetitFormat(int numVente, DataGridView dgvVente, Client client,
             DateTime dateVente, string montantPaye, string reste)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 8 * unite_largeur;
            int hauteur_facture = 10 * unite_hauteur + 35 * dgvVente.Rows.Count + 120;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.Wheat);
            #endregion

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                //graphic.DrawImage(logo, 3 * unite_largeur, 0, 2 * unite_largeur, 2 * unite_hauteur);
            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 9, FontStyle.Regular);
            Font fnt3 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt33 = new Font("Calibri", 9, FontStyle.Regular);
            Font fnt2 = new Font("Calibri", 9, FontStyle.Bold);
            // dessiner les ecritures 
            graphic.DrawString("Centre médical SOS", fnt3, Brushes.Black, 10, unite_hauteur+5);
            graphic.DrawString("BP 135 Quartier Ndjari", fnt33, Brushes.Black, 10, 2 * unite_hauteur + 5);
            graphic.DrawString("Ndjaména-Tchad", fnt1, Brushes.Black, 10, 3 * unite_hauteur + 0);
            //graphic.DrawString("Email :", fnt33, Brushes.Black, 10, 7 * unite_hauteur - 10);

            if (numVente < 10)
            {
                graphic.DrawString("Reçu de vente N° : 00" + numVente, fnt3, Brushes.Black, 10, 5 * unite_hauteur);
            }
            if (numVente < 100)
            {
                graphic.DrawString("Reçu de vente N° : 0" + numVente, fnt3, Brushes.Black, 10, 5 * unite_hauteur);
            }
            else
            { graphic.DrawString("Reçu de vente N° : " + numVente, fnt3, Brushes.Black, 10, 5 * unite_hauteur); }

            graphic.DrawString("Date vente " + dateVente, fnt33, Brushes.Black, 5, 6 * unite_hauteur);
            if (string.IsNullOrEmpty(client.Entreprise))
            {
                graphic.DrawString("ID Patient :  " + client.Matricule, fnt33, Brushes.Black, 5, 7 * unite_hauteur - 0);
                graphic.DrawString("Nom client : " + client.NomClient, fnt33, Brushes.Black, 5, 8 * unite_hauteur - 5);

            }
            else
            {

                if (!string.IsNullOrEmpty(client.SousCouvert))
                {
                    graphic.DrawString("ID Patient :  " + client.Matricule, fnt33, Brushes.Black, 5, 7 * unite_hauteur - 0);
                    graphic.DrawString("Nom client : " + client.NomClient, fnt33, Brushes.Black, 5, 8 * unite_hauteur - 5);
                    graphic.DrawString("S/C :  " + client.SousCouvert, fnt33, Brushes.Black, 5, 9 * unite_hauteur - 10);
                    graphic.DrawString("Institution :  " + client.Entreprise, fnt33, Brushes.Black, 5, 10 * unite_hauteur - 15);
                }
                else
                {
                    if (client.NomClient.Contains("S/C"))
                    {
                        graphic.DrawString("ID Patient :  " + client.Matricule, fnt33, Brushes.Black, 5, 7 * unite_hauteur - 0);
                        graphic.DrawString("Nom client : " + client.NomClient.Substring(0, client.NomClient.IndexOf("S/C")), fnt33, Brushes.Black, 5, 8 * unite_hauteur - 5);
                        graphic.DrawString("" + client.NomClient.Substring(client.NomClient.IndexOf("S/C")), fnt33, Brushes.Black, 5, 9 * unite_hauteur - 10);
                        graphic.DrawString("" + client.Entreprise, fnt33, Brushes.Black, 5, 10 * unite_hauteur - 15);
                    }
                    else
                    {
                        graphic.DrawString("ID Patient :  " + client.Matricule, fnt33, Brushes.Black, 5, 7 * unite_hauteur - 0);
                        graphic.DrawString("Nom client : " + client.NomClient, fnt33, Brushes.Black, 5, 8 * unite_hauteur - 5);
                        graphic.DrawString("Institution :  " + client.Entreprise, fnt33, Brushes.Black, 5, 9 * unite_hauteur - 10);
                    }
                }

            }
            //graphic.DrawString("========================================================", fnt33, Brushes.Black, 10, 10 * unite_hauteur + 5);
            graphic.DrawString("===============================", fnt33, Brushes.Black, 5, 5 * unite_hauteur - 10);

            int Loc = 20;
            decimal montant = 0;
            for (int i = 0; i <= dgvVente.Rows.Count - 1; i++)
            {
                int Yloc = 37 * i + 10 * unite_hauteur - 10;

                graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString(), fnt2, Brushes.Black, 10, Yloc + 15);
                graphic.DrawString(string.Format(elGR, "{0:0,0}", double.Parse(dgvVente.Rows[i].Cells[3].Value.ToString())) + "F.CFA", fnt1, Brushes.Black, 10, Yloc + 30);
                graphic.DrawString(string.Format(elGR, "{0:0,0}", double.Parse(dgvVente.Rows[i].Cells[5].Value.ToString())), fnt1, Brushes.Black, 3 * unite_largeur - 5, Yloc + 30);
                graphic.DrawString(string.Format(elGR, "{0:0,0}", double.Parse(dgvVente.Rows[i].Cells[6].Value.ToString())) + "F.CFA", fnt1, Brushes.Black, 4 * unite_largeur - 0, Yloc + 30);
                montant += Decimal.Parse(dgvVente.Rows[i].Cells[6].Value.ToString());
            }
            Loc += dgvVente.Rows.Count * 37 + 10 * unite_hauteur - 10;
            graphic.DrawString("Montant TH ".ToString(), fnt1, Brushes.Black, 10, Loc + 0);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", montant) + " F.CFA", fnt1, Brushes.Black, 4 * unite_largeur - 0, Loc + 0);

            graphic.DrawString("-------------------------------------------------------------", fnt33, Brushes.Black, 10, Loc + 10);
            graphic.DrawString("Espèces (XAF) : ", fnt1, Brushes.Black, 10, Loc + 30);
            graphic.DrawString("Reste : ", fnt1, Brushes.Black, 10, Loc + 45);
            if (string.IsNullOrEmpty(montantPaye))
            {
                graphic.DrawString(string.Format(elGR, "{0:0,0}", montant) + " F.CFA", fnt1, Brushes.Black, 4 * unite_largeur - 0, Loc + 30);
                graphic.DrawString("0 F.CFA", fnt1, Brushes.Black, 4 * unite_largeur - 0, Loc + 45);
            }
            else
            {
                graphic.DrawString(montantPaye + " F.CFA", fnt1, Brushes.Black, 4 * unite_largeur - 0, Loc + 30);
                graphic.DrawString(reste + " F.CFA", fnt1, Brushes.Black, 4 * unite_largeur - 0, Loc + 45);
            }
            graphic.DrawString("CAISSIER(E) ", fnt1, Brushes.Black, 15, Loc + 65);
            graphic.DrawString(Form1.nomEmploye.ToUpper(), fnt1, Brushes.Black, 15, Loc + 80);
            return bitmap;
        }

        //fonction pour dessiner le proforma
        public static Bitmap ImprimerFactureProFormat(DataGridView dgvVente, string nomClient, string somme)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 25 * unite_hauteur;
            int hauteur_facture = 45 * unite_hauteur;


            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 0, 23 * unite_largeur, 6 * unite_hauteur);
            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 24, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
            
            graphic.DrawString("FACTURE PROFORMA  No  001/" + DateTime.Now.Year, fnt11, Brushes.Black, 2 * unite_largeur, 7 * unite_hauteur);
            graphic.DrawLine(Pens.Black, 2 * unite_largeur + 3, 8 * unite_hauteur + 12, 21 * unite_largeur - 12, 8 * unite_hauteur + 12);
            graphic.DrawString(" DOIT  CCA  :  " + nomClient.ToUpper(), fnt1, Brushes.Black, 25, 9 * unite_hauteur);
            graphic.DrawString("Date : " + DateTime.Now, fnt33, Brushes.Black, unite_largeur, 10 * unite_hauteur);

            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 21 * unite_largeur, 25);
            graphic.FillRectangle(Brushes.WhiteSmoke, 25, 11 * unite_hauteur + 20, 16 * unite_largeur, 25);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, unite_largeur, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 12 * unite_largeur, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 15 * unite_largeur, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 18 * unite_largeur, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 21 * unite_largeur, detail_hauteur_facture);

            graphic.DrawString("No", fnt1, Brushes.Black, unite_largeur, 12 * unite_hauteur);
            graphic.DrawString("Désignation", fnt1, Brushes.Black, unite_largeur * 2, 12 * unite_hauteur);
            graphic.DrawString("prix unit", fnt1, Brushes.Black, 13 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawString("Quantité", fnt1, Brushes.Black, 16 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawString("Prix total", fnt1, Brushes.Black, 19 * unite_largeur, 12 * unite_hauteur);

            int Loc = 0;
            decimal montant = 0;
            for (int i = 0; i <= dgvVente.Rows.Count - 1; i++)
            {
                int Yloc = 13 * unite_hauteur + 10 + i * unite_hauteur;

                graphic.DrawString(i.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                if (dgvVente.Rows[i].Cells[1].Value.ToString().Length > 35)
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().Substring(0, 35).ToUpper() + "...", fnt1, Brushes.Black, 2 * unite_largeur, Yloc);
                }
                else
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 2 * unite_largeur, Yloc);
                }
                graphic.DrawString(dgvVente.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur + 18, Yloc);
                graphic.DrawString(dgvVente.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur - 3, Yloc);
                graphic.DrawString(dgvVente.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 19 * unite_largeur + 10, Yloc);
                montant += Decimal.Parse(dgvVente.Rows[i].Cells[6].Value.ToString());
            }
            Loc += 11 * unite_hauteur + 20 + detail_hauteur_facture;

            graphic.DrawRectangle(Pens.Black, 25, Loc, 21 * unite_largeur, 25);
            graphic.DrawString("Total FCFA : " + montant.ToString(), fnt3, Brushes.Black, 16 * unite_largeur, Loc + 5);
            graphic.DrawString("Arrêtée la présente facture proforma à la somme de : " + somme + " Francs CFA", fnt3, Brushes.Black, 25, Loc + 45);
            // graphic.DrawString("Le fournisseur ", fnt3, Brushes.Black, 17 * unite_largeur, 43 * unite_hauteur);      
            graphic.DrawString("Le Gerant", fnt3, Brushes.Black, 17 * unite_largeur, 43 * unite_hauteur + 10);
            return bitmap;
        }


        public static Bitmap ImprimerFactureCredit
           (DataGridView dgvVente, Client client, double montantTotal, double montantPaye, double resteAPayer, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int detail_hauteur_facture = 27 * unite_hauteur;
            int hauteur_facture = 45 * unite_hauteur;
            bool flag = false;
            if (dgvVente.Rows.Count <= 25 * (1 + start))
            {

                flag = true;
            }
            else
            {
                flag = false;
            }
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion


            //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 15, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font fnt2 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 12, FontStyle.Regular);

            Image logo = global::GestionPharmacetique.Properties.Resources.logo;
            graphic.DrawImage(logo, 10, 10, 16 * unite_largeur + 20, 6 * unite_hauteur);

            //graphic.DrawString("Date : " + dateVente.ToShortDateString(), fnt3, Brushes.DeepSkyBlue, 10, 13 * unite_hauteur);
            graphic.DrawString("REÇU  DE CREDIT  ", fnt3, Brushes.Black, unite_largeur, 10 * unite_hauteur);
            graphic.DrawString("Date   ".ToUpper() + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, unite_largeur, 9 * unite_hauteur - 5);
            //graphic.DrawString("Client comptant  ", fnt33, Brushes.Black, 9 * unite_largeur, 5 * unite_hauteur - 5);
            if (client.NomClient == "CLIENT COMPTANT")
            {
                graphic.DrawString("Nom client : - ".ToUpper(), fnt33, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            }
            else
            {
                graphic.DrawString("Nom client : ".ToUpper() + client.NomClient.ToUpper(System.Globalization.CultureInfo.CurrentCulture), fnt33, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            }

            graphic.FillRectangle(Brushes.WhiteSmoke, 25, 11 * unite_hauteur + 20, 24 * unite_largeur, 25);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 24 * unite_largeur, 25);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, unite_largeur + 20, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 13 * unite_largeur, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 16 * unite_largeur, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 19 * unite_largeur, detail_hauteur_facture);
            graphic.DrawRectangle(Pens.Black, 25, 11 * unite_hauteur + 20, 23 * unite_largeur + 7, detail_hauteur_facture);

            graphic.DrawString("No", fnt1, Brushes.Black, unite_largeur, 12 * unite_hauteur);
            graphic.DrawString("Désignation".ToUpper(), fnt1, Brushes.Black, 2 * unite_largeur + 20, 12 * unite_hauteur);
            graphic.DrawString("prix u".ToUpper(), fnt1, Brushes.Black, 14 * unite_largeur + 18, 12 * unite_hauteur);
            graphic.DrawString("Quantité".ToUpper(), fnt1, Brushes.Black, 17 * unite_largeur - 3, 12 * unite_hauteur);
            graphic.DrawString("Prix total".ToUpper(), fnt1, Brushes.Black, 20 * unite_largeur + 10, 12 * unite_hauteur);

            int Loc = 0;

            var j = 0;
            for (int i = start * 25; i <= dgvVente.Rows.Count - 1; i++)
            {
                //int Yloc = 13 * unite_hauteur + 20 + i * unite_hauteur;

                var Yloc = 13 * unite_hauteur + 20 + j * unite_hauteur;
                graphic.DrawString(i.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                if (dgvVente.Rows[i].Cells[1].Value.ToString().Length > 50)
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().Substring(0, 50).ToUpper().ToUpper() + "...", fnt1, Brushes.Black, 2 * unite_largeur + 20, Yloc);
                }
                else
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().ToUpper().ToUpper(), fnt1, Brushes.Black, 2 * unite_largeur + 20, Yloc);
                }
                graphic.DrawString(dgvVente.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur + 3, Yloc);
                graphic.DrawString(dgvVente.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 3, Yloc);
                var total = Decimal.Parse(dgvVente.Rows[i].Cells[5].Value.ToString()) * Decimal.Parse(dgvVente.Rows[i].Cells[3].Value.ToString());
                graphic.DrawString(total.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 3, Yloc);
                j++;
            }
            Loc += 11 * unite_hauteur + 20 + detail_hauteur_facture;
            graphic.FillRectangle(Brushes.White, 24, 38 * unite_hauteur + 20, 24 * unite_largeur, 11 * unite_hauteur);
            graphic.FillRectangle(Brushes.WhiteSmoke, 24, Loc, 24 * unite_largeur, 25);
            if (flag)
            {

                graphic.FillRectangle(Brushes.WhiteSmoke, 24, Loc, 24 * unite_largeur, 25);
                graphic.DrawString("Montant total : ", fnt1, Brushes.Black, 25, Loc + 2 * unite_hauteur);
                graphic.DrawString("Montant payé : ", fnt1, Brushes.Black, 25, Loc + 3 * unite_hauteur);
                graphic.DrawString("Reste à payer : ", fnt1, Brushes.Black, 25, Loc + 4 * unite_hauteur);

                graphic.DrawString(montantTotal.ToString() + " F.CFA", fnt1, Brushes.Black, 5 * unite_largeur, Loc + 2 * unite_hauteur);
                graphic.DrawString(montantPaye.ToString() + " F.CFA", fnt1, Brushes.Black, 5 * unite_largeur, Loc + 3 * unite_hauteur);
                graphic.DrawString(resteAPayer.ToString() + " F.CFA", fnt1, Brushes.Black, 5 * unite_largeur, Loc + 4 * unite_hauteur);
            }
            return bitmap;
        }
        public static Bitmap ImprimerMouvementStock(DataGridView dgvInventaire, string titre, DocumentStock doc, int index, DateTime dt, string totalHT)
        {

            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 54 * unite_hauteur;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);
            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion


            //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt4 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 10, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 14F, FontStyle.Bold | FontStyle.Underline);


            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);

            graphic.DrawRectangle(Pens.Black, 15, 5 * unite_hauteur - 6, unite_largeur * 23 + 1, 3 * unite_hauteur + 6);

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, 15, 5, 23 * unite_largeur, 4 * unite_hauteur + 5);
            }
            catch (Exception)
            { }
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 8, 5 * unite_hauteur - 4);
            //graphic.DrawString("Page " + (index + 1).ToString(), fnt1, Brushes.Black, 22 * unite_largeur, 10);

            graphic.DrawString("Opérateur : " + doc.NumeroMatricule, fnt11, Brushes.Black, unite_largeur + 15, 7 * unite_hauteur - 5);
            graphic.DrawString("Imprimé le " + DateTime.Now, fnt1, Brushes.Black, unite_largeur * 16 - 0, 7 * unite_hauteur - 5);

            //graphic.DrawString("N° pièce : " + doc.IDReference, fnt1, Brushes.Black, unite_largeur + 10, 6 * unite_hauteur -10);
            graphic.DrawString("Référence : " + doc.Reference, fnt1, Brushes.Black, unite_largeur + 15, 6 * unite_hauteur - 3);
            if (titre.ToUpper().Contains("FACTURE"))
            {
                graphic.DrawString("Client  :  " + doc.Destination, fnt1, Brushes.Black, 16 * unite_largeur - 0, 6 * unite_hauteur - 3);
            }
            else
            {
                graphic.DrawString("Destination :  " + doc.Destination, fnt1, Brushes.Black, 16 * unite_largeur - 0, 6 * unite_hauteur - 3);
            }
            graphic.DrawRectangle(Pens.Black, 15, 8 * unite_hauteur, unite_largeur * 1, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 2 - 17, 8 * unite_hauteur, unite_largeur * 13, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15 - 17, 8 * unite_hauteur, unite_largeur * 2, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 17 - 17, 8 * unite_hauteur, unite_largeur * 3, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 17, 8 * unite_hauteur, unite_largeur * 4 + 1, unite_hauteur + 14);

            graphic.DrawString("N°".ToUpper(), fnt11, Brushes.Black, 1 * unite_largeur - 7, 8 * unite_hauteur + 3);
            graphic.DrawString("designation".ToUpper(), fnt11, Brushes.Black, 3 * unite_largeur - 17, 8 * unite_hauteur + 3);
            graphic.DrawString("Prix unit".ToUpper(), fnt11, Brushes.Black, 17 * unite_largeur - 5, 8 * unite_hauteur + 3);
            graphic.DrawString("Quantité".ToUpper(), fnt11, Brushes.Black, 15 * unite_largeur - 15, 8 * unite_hauteur + 3);
            graphic.DrawString("Montant HT".ToUpper(), fnt11, Brushes.Black, 20 * unite_largeur + 5, 8 * unite_hauteur + 3);

            graphic.DrawRectangle(Pens.Black, 15, 9 * unite_hauteur + 14, unite_largeur * 1, unite_hauteur * 40);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 2 - 17, 9 * unite_hauteur + 14, unite_largeur * 13, unite_hauteur * 40);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15 - 17, 9 * unite_hauteur + 14, unite_largeur * 2, unite_hauteur * 40);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 17 - 17, 9 * unite_hauteur + 14, unite_largeur * 3, unite_hauteur * 40);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 17, 9 * unite_hauteur + 14, unite_largeur * 4 + 1, unite_hauteur * 40);

            int j = 0;
            for (int i = index * 40; i < dgvInventaire.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 9 * unite_hauteur + 14;

                graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, unite_largeur - 5, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 3 * unite_largeur - 17, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur - 2, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur - 2, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur - 2, Yloc);
                j++;
            }
            graphic.FillRectangle(Brushes.White, 15, 49 * unite_hauteur + 14, unite_largeur * 23 + 1, unite_hauteur * 5);
            graphic.DrawRectangle(Pens.Black, 15, 49 * unite_hauteur + 14, unite_largeur * 23 + 1, unite_hauteur * 2 - 5);

            graphic.DrawString("Total HT".ToUpper(), fnt11, Brushes.Black, unite_largeur + 0, 50 * unite_hauteur);
            graphic.DrawString(totalHT + "F.CFA", fnt11, Brushes.Black, 20 * unite_largeur + 0, 50 * unite_hauteur + 0);

            return bitmap;
        }

        public static Bitmap ImprimerBonLivraison(DataGridView dgvInventaire, string titre, DocumentStock doc, int index, DateTime dt, string totalHT)
        {

            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 54 * unite_hauteur;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);
            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion

            //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt4 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 10, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 14F, FontStyle.Bold | FontStyle.Underline);


            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);

            graphic.DrawRectangle(Pens.Black, 15, 5 * unite_hauteur - 6, unite_largeur * 23 + 1, 3 * unite_hauteur + 6);

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, 15, 5, 23 * unite_largeur, 4 * unite_hauteur+5);

            }
            catch (Exception)
            { }
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 8, 5 * unite_hauteur - 4);
            //graphic.DrawString("Page " + (index + 1).ToString(), fnt1, Brushes.Black, 12 * unite_largeur, 10);

            graphic.DrawString("Opérateur : " + doc.NumeroMatricule, fnt11, Brushes.Black, unite_largeur + 15, 7 * unite_hauteur - 5);
            graphic.DrawString("Imprimé le " + DateTime.Now, fnt1, Brushes.Black, unite_largeur * 16 - 0, 7 * unite_hauteur - 5);

            //graphic.DrawString("N° pièce : " + doc.IDReference, fnt1, Brushes.Black, unite_largeur + 10, 6 * unite_hauteur -10);
            graphic.DrawString("Référence : " + doc.Reference, fnt1, Brushes.Black, unite_largeur + 15, 6 * unite_hauteur - 3);
            if (titre.ToUpper().Contains("FACTURE"))
            {
                graphic.DrawString("Client  :  " + doc.Destination, fnt1, Brushes.Black, 16 * unite_largeur - 0, 6 * unite_hauteur - 3);
            }
            else
            {
                graphic.DrawString("Destination :  " + doc.Destination, fnt1, Brushes.Black, 16 * unite_largeur - 0, 6 * unite_hauteur - 3);
            }
            graphic.DrawRectangle(Pens.Black, 15, 8 * unite_hauteur, unite_largeur * 1, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 2 - 17, 8 * unite_hauteur, unite_largeur * 13, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15 - 17, 8 * unite_hauteur, unite_largeur * 2, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 17 - 17, 8 * unite_hauteur, unite_largeur * 3, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 17, 8 * unite_hauteur, unite_largeur * 4 + 1, unite_hauteur + 14);

            graphic.DrawString("N°".ToUpper(), fnt11, Brushes.Black, 1 * unite_largeur - 7, 8 * unite_hauteur + 3);
            graphic.DrawString("designation".ToUpper(), fnt11, Brushes.Black, 3 * unite_largeur - 17, 8 * unite_hauteur + 3);
            graphic.DrawString("Lot N°".ToUpper(), fnt11, Brushes.Black, 17 * unite_largeur - 8, 8 * unite_hauteur + 3);
            graphic.DrawString("Quantité".ToUpper(), fnt11, Brushes.Black, 15 * unite_largeur - 15, 8 * unite_hauteur + 3);
            graphic.DrawString("Date expiration".ToUpper(), fnt11, Brushes.Black, 20 * unite_largeur - 5, 8 * unite_hauteur + 3);

            graphic.DrawRectangle(Pens.Black, 15, 9 * unite_hauteur + 14, unite_largeur * 1, unite_hauteur * 40);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 2 - 17, 9 * unite_hauteur + 14, unite_largeur * 13, unite_hauteur * 40);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15 - 17, 9 * unite_hauteur + 14, unite_largeur * 2, unite_hauteur * 40);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 17 - 17, 9 * unite_hauteur + 14, unite_largeur * 3, unite_hauteur * 40);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 17, 9 * unite_hauteur + 14, unite_largeur * 4 + 1, unite_hauteur * 40);

            int j = 0;
            for (int i = index * 40; i < dgvInventaire.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 15 + 9 * unite_hauteur;

                graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, unite_largeur - 5, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 3 * unite_largeur - 17, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur - 8, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur - 8, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur - 2, Yloc);

                j++;
            }
            graphic.FillRectangle(Brushes.White, 15, 49 * unite_hauteur + 15, unite_largeur * 23 + 1, unite_hauteur * 5);
            //graphic.DrawString("Total HT".ToUpper(), fnt11, Brushes.Black, unite_largeur + 0, 47 * unite_hauteur);
            //graphic.DrawString(totalHT + "F.CFA", fnt11, Brushes.Black, 20 * unite_largeur + 0, 47 * unite_hauteur + 0);

            return bitmap;
        }

        public static Bitmap ImprimerRapportMouvementStock(DataGridView dgvInventaire, string titre, int index, string totalHT)
        {

            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 54 * unite_hauteur;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);
            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion


            //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt4 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 9.5f, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 12F, FontStyle.Bold | FontStyle.Underline);
            Font fnt21 = new Font("Arial Narrow", 9, FontStyle.Regular);

            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);

            graphic.DrawRectangle(Pens.Black, 15, 5 * unite_hauteur - 6, unite_largeur * 23 + 1, 2 * unite_hauteur + 6);

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, 15, 5, 23 * unite_largeur, 4 * unite_hauteur+5);

            }
            catch (Exception)
            { }
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 1, 5 * unite_hauteur - 4);
            graphic.DrawString("Page " + (index + 1).ToString(), fnt1, Brushes.WhiteSmoke, 11 * unite_largeur, 10);
            graphic.DrawString("Imprimé le " + DateTime.Now, fnt1, Brushes.Black, unite_largeur * 16 - 0, 6 * unite_hauteur - 2);

            graphic.DrawRectangle(Pens.Black, 15, 7 * unite_hauteur, unite_largeur * 1, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 2 - 17, 7 * unite_hauteur, unite_largeur * 13, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15 - 17, 7 * unite_hauteur, unite_largeur * 2, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 17 - 17, 7 * unite_hauteur, unite_largeur * 3, unite_hauteur + 14);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 17, 7 * unite_hauteur, unite_largeur * 4 + 1, unite_hauteur + 14);

            graphic.DrawString("N°".ToUpper(), fnt11, Brushes.Black, 1 * unite_largeur - 7, 7 * unite_hauteur + 3);
            graphic.DrawString("designation".ToUpper(), fnt11, Brushes.Black, 3 * unite_largeur - 17, 7 * unite_hauteur + 3);
            graphic.DrawString("Prix unit".ToUpper(), fnt11, Brushes.Black, 17 * unite_largeur - 5, 7 * unite_hauteur + 3);
            graphic.DrawString("Quantité".ToUpper(), fnt11, Brushes.Black, 15 * unite_largeur - 15, 7 * unite_hauteur + 3);
            graphic.DrawString("Montant HT".ToUpper(), fnt11, Brushes.Black, 20 * unite_largeur + 5, 7 * unite_hauteur + 3);

            graphic.DrawRectangle(Pens.Black, 15, 8 * unite_hauteur + 14, unite_largeur * 1, unite_hauteur * 41);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 2 - 17, 8 * unite_hauteur + 14, unite_largeur * 13, unite_hauteur * 41);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15 - 17, 8 * unite_hauteur + 14, unite_largeur * 2, unite_hauteur * 41);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 17 - 17, 8 * unite_hauteur + 14, unite_largeur * 3, unite_hauteur * 41);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 17, 8 * unite_hauteur + 14, unite_largeur * 4 + 1, unite_hauteur * 41);

            int j = 0;
            for (int i = index * 41; i < dgvInventaire.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 8 * unite_hauteur + 15;

                graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, unite_largeur - 5, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 3 * unite_largeur - 17, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur - 2, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur - 2, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur - 2, Yloc);
                j++;
            }
            graphic.FillRectangle(Brushes.White, 15, 49 * unite_hauteur + 14, unite_largeur * 23 + 1, unite_hauteur * 5);
            if (dgvInventaire.Rows.Count < (1 + index) * 41)
            {
                graphic.DrawRectangle(Pens.Black, 15, 49 * unite_hauteur + 14, unite_largeur * 23 + 1, unite_hauteur * 2 - 5);

                graphic.DrawString("Total HT".ToUpper(), fnt11, Brushes.Black, unite_largeur + 0, 50 * unite_hauteur);
                graphic.DrawString(totalHT + "F.CFA", fnt11, Brushes.Black, 20 * unite_largeur + 0, 50 * unite_hauteur + 0);
            }
            return bitmap;
        }

    }
}
