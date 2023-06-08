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

      
         #region ImprimerLivraison
        //fonction pour dessiner la livraison
        public static Bitmap ImprimerLivraison( DataGridView lstInventaire, string numeroLivraison,
         string nomFournisseur, string dateLivraison, string montantTotal, string autresCharges,string montantTotalTTC,int start)
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
            Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold |  FontStyle.Underline);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo,  unite_largeur, 10, 23 * unite_largeur, 5 * unite_hauteur);

            }
            catch (Exception)
            { }

           
            
            graphic.DrawString("Page " + (start + 1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, unite_hauteur);

            graphic.DrawString("Nom fournisseur : " + nomFournisseur, fnt1, Brushes.Black, 13*unite_largeur,6 * unite_hauteur);
            graphic.DrawString("Facture du " + dateLivraison, fnt1, Brushes.Black,  unite_largeur, 6 * unite_hauteur);
         
            graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur-15, 23 * unite_largeur , 2 * unite_hauteur-10);

            graphic.DrawString("Facture N° " + numeroLivraison, fnt3, Brushes.Black, 10 * unite_largeur, 8 * unite_hauteur -15);

            graphic.DrawLine(Pens.LightBlue, unite_largeur, 12 * unite_hauteur, 24 * unite_largeur + 10, 12 * unite_hauteur);
            graphic.DrawLine(Pens.LightBlue, unite_largeur, 13 * unite_hauteur + 5, 24 * unite_largeur + 10, 13 * unite_hauteur + 5);

            graphic.DrawString("N°", fnt1, Brushes.Black, unite_largeur + 5, 10 * unite_hauteur - 5);
            graphic.DrawString("Article", fnt1, Brushes.Black, 2 * unite_largeur + 5, 10 * unite_hauteur - 5);
            graphic.DrawString("Qté", fnt1, Brushes.Black, 16 * unite_largeur + 15, 10 * unite_hauteur - 5);
            graphic.DrawString("Px achat", fnt1, Brushes.Black, 18 * unite_largeur, 10 * unite_hauteur - 5);
            graphic.DrawString("Px Total", fnt1, Brushes.Black, 21 * unite_largeur, 10 * unite_hauteur-5);

            var j = 0;
            for (int i = start * 44; i <= lstInventaire.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 11 * unite_hauteur;

                if (i % 2 == 0)
                {
                    graphic.FillRectangle(Brushes.AliceBlue, unite_largeur, Yloc, unite_largeur * 24 + 10, unite_hauteur + 10);
                }
                else
                    graphic.FillRectangle(Brushes.White, unite_largeur, Yloc, unite_largeur * 24 + 10, unite_hauteur + 10);

                graphic.DrawString((i+1).ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                //graphic.DrawString(lstInventaire.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 2 * unite_largeur + 5, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur + 15, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur + 5, Yloc);
                graphic.DrawString(lstInventaire.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur, Yloc);
                j++;
            } graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, unite_largeur * 24 + 10, unite_hauteur * 10);

            if (lstInventaire.Rows.Count <= 43 * (1 + start))
            {
                graphic.DrawRectangle(Pens.Black, unite_largeur, 54 * unite_hauteur , 23 * unite_largeur, 2 * unite_hauteur - 15);
                graphic.DrawString("Mx. HT : " + montantTotal + " .FCFA", fnt2, Brushes.Black, unite_largeur, 54 * unite_hauteur + 2);
                graphic.DrawString("A. Charges  : " + autresCharges + " .FCFA", fnt2, Brushes.Black, 8 * unite_largeur+20, 54 * unite_hauteur + 2);
                graphic.DrawString("Mx. TTC  : " + montantTotalTTC + " .FCFA", fnt2, Brushes.Black, 16 * unite_largeur, 54 * unite_hauteur + 2);
            }
            return bitmap;
        }       
        #endregion
         
       
        //fonction pour dessiner le stock expires
        public static Bitmap ImprimerInventaireStockExpires(int start)
        {
            List<Medicament> stockArrayList = ConnectionClass.ListeDesMedicamentsExpirees();
            #region

            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
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
            Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo,  unite_largeur, 10, 23 * unite_largeur,5 * unite_hauteur);

            }
            catch (Exception)
            { }
            

            graphic.DrawString("Page " +(start +1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, unite_hauteur);

            graphic.DrawString("stocks médicaments en cour d'expiration ou expirés ", fnt3, Brushes.DeepSkyBlue, unite_largeur, 6 * unite_hauteur + 15);

            graphic.DrawLine(Pens.LightBlue, unite_largeur, 8 * unite_hauteur, 24 * unite_largeur + 10, 8 * unite_hauteur);
            graphic.DrawLine(Pens.LightBlue, unite_largeur, 9 * unite_hauteur + 5, 24 * unite_largeur + 10, 9 * unite_hauteur + 5);

            //graphic.DrawString("Code médi", fnt1, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Nom médicament", fnt1, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Prix vente", fnt1, Brushes.Black, 12 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("stock", fnt1, Brushes.Black, 15 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("prix total", fnt1, Brushes.Black, 17 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("date expiration", fnt1, Brushes.Black, 20 * unite_largeur + 10, 8 * unite_hauteur);

            var j = 0;
            for (var i = start * 45; i <= stockArrayList.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 10 * unite_hauteur;

                graphic.DrawString(stockArrayList[i].NomMedicament.ToUpper() , fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(stockArrayList[i].PrixVente.ToString(), fnt1, Brushes.Black, 12 * unite_largeur, Yloc);
                graphic.DrawString(stockArrayList[i].Quantite.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, Yloc);
                graphic.DrawString((stockArrayList[i].Quantite * stockArrayList[i].PrixVente).ToString(), fnt1, Brushes.Black, 17 * unite_largeur, Yloc);
                graphic.DrawString(stockArrayList[i].DateExpiration.ToShortDateString(), fnt1, Brushes.Black, 20 * unite_largeur + 10, Yloc);
                j++;
            }

            graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, unite_largeur * 24 + 10, 2 * unite_hauteur + 10);

            return bitmap;
        }
   
        #endregion

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
            Font fnt11 = new Font("Ubuntu", 18, FontStyle.Bold );
            Font fnt3 = new Font("Ubuntu", 12, FontStyle.Bold);
            Font fnt2 = new Font("Ubuntu", 15, FontStyle.Bold | FontStyle.Underline);
            Font fnt33 = new Font("Ubuntu", 10, FontStyle.Regular);
            //try
            //{


            Image logo = global::GestionPharmacetique.Properties.Resources.logo;
            graphic.DrawImage(logo, 23, 10, 16 * unite_largeur +20, 6 * unite_hauteur);


            logo = global::GestionPharmacetique.Properties.Resources.rectangle;
            graphic.DrawImage(logo, 23, 8 * unite_hauteur+8, 16 * unite_largeur + 24, 32);

            graphic.DrawString(titre.ToUpper() + numVente, fnt2, Brushes.White, unite_largeur*6, 8 * unite_hauteur + 8);
            graphic.DrawString("Date : ".ToUpper() + dateVente.ToShortDateString(), fnt33, Brushes.Black, 14 * unite_largeur-5, 7 * unite_hauteur +5);
        
           
           graphic.DrawString("Nom client : ".ToUpper() + client.NomClient.ToUpper(System.Globalization.CultureInfo.CurrentCulture), fnt33, Brushes.Black, unite_largeur, 7 * unite_hauteur + 5);            
           graphic.DrawString("INSTITUTION : ".ToUpper() + client.Entreprise.ToUpper(), fnt33, Brushes.Black, unite_largeur, 6 * unite_hauteur + 10);
            
            logo = global::GestionPharmacetique.Properties.Resources.enteteFact;
            graphic.DrawImage(logo, 23 , 10 * unite_hauteur, 16 * unite_largeur+24, 50);

            logo = global::GestionPharmacetique.Properties.Resources.detailFacture;
            graphic.DrawImage(logo, 25 , 11*unite_hauteur+15, 16 * unite_largeur+20, detail_hauteur_facture);
           
            int Loc = 0;
            decimal montant = 0, montantTotal = 0;

            for (int i = 0; i <= dgvVente.Rows.Count - 1; i++)
            {
                int Yloc = 12 * unite_hauteur  + i * unite_hauteur;
                graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, 2*unite_largeur-25, Yloc);
                if (dgvVente.Rows[i].Cells[1].Value.ToString().Length > 30)
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().Substring(0, 30).ToUpper() + "...", fnt1, Brushes.Black, 3 * unite_largeur-15, Yloc);
                }
                else
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 3 * unite_largeur-15, Yloc);
                }
                graphic.DrawString(dgvVente.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 11 * unite_largeur +25, Yloc);
                graphic.DrawString(dgvVente.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur + 10, Yloc);
                var total = Decimal.Parse(dgvVente.Rows[i].Cells[5].Value.ToString()) * Decimal.Parse(dgvVente.Rows[i].Cells[3].Value.ToString());
                graphic.DrawString(total.ToString(), fnt1, Brushes.Black, 15 * unite_largeur +15, Yloc);
                montant += total;
                montantTotal += Decimal.Parse(dgvVente.Rows[i].Cells[6].Value.ToString());
            }
            Loc += 12 * unite_hauteur -5 + detail_hauteur_facture;
            graphic.FillRectangle(Brushes.White, 22, Loc, 16 * unite_largeur+4, 6*unite_hauteur);

            logo = global::GestionPharmacetique.Properties.Resources.rectangle;
            graphic.DrawImage(logo, 22, Loc, 16 * unite_largeur + 26, 35);

            graphic.DrawString("Total ", fnt3, Brushes.White, unite_largeur, Loc + 3);
            graphic.DrawString(montantTotal.ToString() + " FCFA", fnt3, Brushes.White, 13 * unite_largeur, Loc + 3);
            if (totalRemise > 0)
            {
                graphic.DrawString("Total remise :  " + totalRemise.ToString() + " FCFA", fnt3, Brushes.Black, 10 * unite_largeur + 10, Loc + 2*unite_hauteur );
            }
            graphic.DrawString(typeVente + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 25, Loc + 2 * unite_hauteur );
            graphic.DrawString("Merci pour votre visite ", fnt1, Brushes.Black, 25, Loc + 3 * unite_hauteur );
            graphic.DrawString("Bonne guerison ", fnt1, Brushes.Black, 13*unite_largeur, Loc + 3 * unite_hauteur );
            graphic.DrawString("CAISSIER(E)  " + Form1.nomEmploye, fnt3,Brushes.Black, 4 * unite_largeur, Loc + 5 * unite_hauteur-15);
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
                if (dgvVente.Rows[i].Cells[1].Value.ToString().Length > 40)
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().Substring(0, 40).ToUpper() + "...", fnt1, Brushes.Black, 2 * unite_largeur + 20, Yloc);
                }
                else
                {
                    graphic.DrawString(dgvVente.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 2 * unite_largeur + 20, Yloc);
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
            int unite_hauteur = 15;
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
            Font fnt1 = new Font("Arial Narrow", 10, FontStyle.Regular);
            Font fnt4 = new Font("Arial Narrow", 13, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 9, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 13F, FontStyle.Bold);


            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);

            graphic.DrawRectangle(Pens.DimGray, unite_largeur, 7 * unite_hauteur - 5, unite_largeur * 23 - 3, 2 * unite_hauteur + 16);

            if (titre.ToUpper().Contains("SORTIE"))
            {
                titre = "Mouvement de sortie";
            }
            else
            {
                titre = "Mouvement d'entrée";
            }

            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, 19 * unite_largeur, 10, 3 * unite_largeur, 4 * unite_hauteur);

            }
            catch (Exception)
            { }
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 7, 7 * unite_hauteur - 5);
            graphic.DrawString("Page " + (index + 1).ToString(), fnt1, Brushes.Black, 12 * unite_largeur, 10);

            graphic.DrawString("Imprimé le " + DateTime.Now, fnt1, Brushes.Black, unite_largeur * 18, 7 * unite_hauteur - 5);

            graphic.DrawString("N° pièce : " + doc.ID, fnt1, Brushes.Black, unite_largeur + 10, 8 * unite_hauteur + 7);
            graphic.DrawString("Référence : " + doc.Reference, fnt1, Brushes.Black, 5 * unite_largeur + 10, 8 * unite_hauteur + 7);
            graphic.DrawString("Depôt : " + doc.Destination, fnt1, Brushes.Black, 18 * unite_largeur + 10, 8 * unite_hauteur + 7);

            graphic.DrawRectangle(Pens.Black, unite_largeur, 10 * unite_hauteur, unite_largeur * 2, unite_hauteur + 15);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 3, 10 * unite_hauteur, unite_largeur * 4, unite_hauteur + 15);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 7, 10 * unite_hauteur, unite_largeur * 10, unite_hauteur + 15);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 17, 10 * unite_hauteur, unite_largeur * 3 - 16, unite_hauteur + 15);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 16, 10 * unite_hauteur, unite_largeur * 2 - 16, unite_hauteur + 15);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 21, 10 * unite_hauteur, unite_largeur * 3 - 1, unite_hauteur + 15);

            graphic.DrawString("DATE".ToUpper(), fnt11, Brushes.Black, unite_largeur + 10, 10 * unite_hauteur + 3);
            graphic.DrawString("Reference \ndu produit".ToUpper(), fnt11, Brushes.Black, 4 * unite_largeur + 3, 10 * unite_hauteur + 3);
            graphic.DrawString("designation".ToUpper(), fnt11, Brushes.Black, 11 * unite_largeur, 10 * unite_hauteur + 3);
            graphic.DrawString("Prix\n Achat".ToUpper(), fnt11, Brushes.Black, 17 * unite_largeur + 15, 10 * unite_hauteur + 3);
            graphic.DrawString("qte ".ToUpper(), fnt11, Brushes.Black, 20 * unite_largeur - 5, 10 * unite_hauteur + 3);
            graphic.DrawString("Montant HT".ToUpper(), fnt11, Brushes.Black, 21 * unite_largeur + 10, 10 * unite_hauteur + 3);


            int j = 0;

            graphic.DrawRectangle(Pens.Black, unite_largeur, 10 * unite_hauteur, unite_largeur * 2, unite_hauteur * 42);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 3, 10 * unite_hauteur, unite_largeur * 4, unite_hauteur * 42);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 7, 10 * unite_hauteur, unite_largeur * 10, unite_hauteur * 42);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 17, 10 * unite_hauteur, unite_largeur * 3 - 16, unite_hauteur * 42);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 16, 10 * unite_hauteur, unite_largeur * 2 - 16, unite_hauteur * 42);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 21, 10 * unite_hauteur, unite_largeur * 3 - 1, unite_hauteur * 42);

            graphic.DrawRectangle(Pens.Black, unite_largeur, 52 * unite_hauteur, unite_largeur * 23 - 1, unite_hauteur * 2 - 5);


            for (int i = index * 40; i < dgvInventaire.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 12 * unite_hauteur;

                graphic.DrawString(dgvInventaire.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 5, Yloc);
                //graphic.DrawString(dgvInventaire.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 3 * unite_largeur + 15, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 7 * unite_largeur + 10, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 15, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur, Yloc);
                graphic.DrawString(dgvInventaire.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur + 20, Yloc);

                j++;
            }
            graphic.DrawString("Total HT".ToUpper(), fnt11, Brushes.Black, unite_largeur + 10, 52 * unite_hauteur + 3);
            graphic.DrawString(totalHT, fnt11, Brushes.Black, 21 * unite_largeur + 15, 52 * unite_hauteur + 3);

            return bitmap;
        }

     }
}
