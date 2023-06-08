using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Collections;

namespace GestionPharmacetique.AppCode
{
     
     public class ImprimerRaportVente
     {
       static   System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

       public static Bitmap ImprimerRapportDesVentes(string titre, System.Windows.Forms.DataGridView dgvRapport, int start, double montantTotalVente)
         {
             //les dimension de la facture
             #region
             int unite_hauteur = 20;
             int unite_largeur = 32;
             int largeur_facture = 24 * unite_largeur ;
             int detail_hauteur_facture = 10 * unite_hauteur;
             int hauteur_facture = 56 * unite_hauteur;
             bool flag = false;
             if (dgvRapport.Rows.Count <= 45 * (1 + start))
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
             Font fnt1 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
             Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
             Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold | FontStyle.Underline);
             Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
             Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
             try
             {
                 Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                 graphic.DrawImage(logo, unite_largeur-3, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
             }
             catch { }

             graphic.DrawString("Page " + (start + 1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, unite_hauteur);

             graphic.DrawString(titre, fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur -10);

             graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 8 * unite_hauteur - 2, 25 * unite_largeur + 15, unite_hauteur);
             graphic.DrawString("Désignation".ToUpper(), fnt1, Brushes.White, unite_largeur+5, 8 * unite_hauteur);
             graphic.DrawString("Qté ".ToUpper(), fnt1, Brushes.White, 14 * unite_largeur + 5, 8 * unite_hauteur);
             graphic.DrawString("Prix vente".ToUpper(), fnt1, Brushes.White, 16 * unite_largeur + 5, 8 * unite_hauteur);
             graphic.DrawString("Prix total".ToUpper(), fnt1, Brushes.White, 20 * unite_largeur+5, 8 * unite_hauteur);


             var j = 0;
             for (var i = start * 45; i <= dgvRapport.Rows.Count - 1; i++)
             {
                 var Yloc = unite_hauteur * j + 9 * unite_hauteur;

                
                 if (dgvRapport.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                 {
                     graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 23 - 3, unite_hauteur - 2);
                    
                     if (dgvRapport.Rows[i].Cells[0].Value.ToString().Length > 45)

                         graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString().ToUpper(System.Globalization.CultureInfo.CurrentCulture).Substring(0, 45) + "...", fnt2, Brushes.Black, unite_largeur, Yloc);
                     else
                         graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString().ToUpper(System.Globalization.CultureInfo.CurrentCulture), fnt2, Brushes.Black, unite_largeur, Yloc);


                     graphic.DrawString(dgvRapport.Rows[i].Cells[2].Value.ToString(), fnt2, Brushes.Black, 14 * unite_largeur + 5, Yloc);
                     graphic.DrawString(dgvRapport.Rows[i].Cells[1].Value.ToString(), fnt2, Brushes.Black, 17 * unite_largeur + 5, Yloc);
                     graphic.DrawString(dgvRapport.Rows[i].Cells[3].Value.ToString(), fnt2, Brushes.Black, 21 * unite_largeur + 5, Yloc);
                 
                 }
                 else
                 {
                     graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 12 - 3, unite_hauteur - 2);
                     graphic.DrawRectangle(Pens.Black, unite_largeur * 13, Yloc, unite_largeur * 3 - 3, unite_hauteur - 2);
                     graphic.DrawRectangle(Pens.Black, unite_largeur * 16 - 1, Yloc, unite_largeur * 4 - 3, unite_hauteur - 2);
                     graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 1, Yloc, unite_largeur * 4, unite_hauteur - 2);

                     if (dgvRapport.Rows[i].Cells[0].Value.ToString().Length > 45)

                         graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString().ToUpper(System.Globalization.CultureInfo.CurrentCulture).Substring(0, 45) + "...", fnt1, Brushes.Black, unite_largeur, Yloc);
                     else
                         graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString().ToUpper(System.Globalization.CultureInfo.CurrentCulture), fnt1, Brushes.Black, unite_largeur, Yloc);


                     graphic.DrawString(dgvRapport.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur + 5, Yloc);
                     graphic.DrawString(dgvRapport.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 5, Yloc);
                     graphic.DrawString(dgvRapport.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur + 5, Yloc);
                 }
                 j++;
             }

             
             graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, 25 * unite_largeur + 15, 10*unite_hauteur);
             if (flag)
             {
                 graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 54 * unite_hauteur, 25 * unite_largeur + 15, unite_hauteur+5);
                 graphic.DrawString("Montant total  ", fnt11, Brushes.White, unite_largeur, 54 * unite_hauteur-2);
                 graphic.DrawString(string.Format(elGR, "{0:0,0}",montantTotalVente) +" F.CFA", fnt11, Brushes.White, 17 * unite_largeur, 54 * unite_hauteur-2);
             }
             return bitmap;
         }

         public static Bitmap ImprimerRapportDeLaCaisse(string titre, System.Windows.Forms.DataGridView dgvRapport, int start)
         {
             //les dimension de la facture
             #region
             int unite_hauteur = 20;
             int unite_largeur = 32;
             int largeur_facture = 24 * unite_largeur;
             int detail_hauteur_facture = 10 * unite_hauteur;
             int hauteur_facture = 56 * unite_hauteur;
             bool flag = false;
             if (dgvRapport.Rows.Count <= 45 * (1 + start))
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
             Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
             Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold | FontStyle.Underline);
             Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
             Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Bold);

             try
             {
                 Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                 graphic.DrawImage(logo, unite_largeur-2, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
             }
             catch { }

             graphic.DrawString("Page " + (start + 1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, unite_hauteur);

             graphic.DrawString(titre, fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur - 10);

             graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 8 * unite_hauteur - 2, 23 * unite_largeur , unite_hauteur);
             graphic.DrawString("date".ToUpper(), fnt1, Brushes.White, unite_largeur * 5, 8 * unite_hauteur);
             graphic.DrawString("MONTANT ".ToUpper(), fnt1, Brushes.White, 15 * unite_largeur + 5, 8 * unite_hauteur);
           
             var montantTotalVente = 0.0;
             var j = 0;
             for (var i = start * 45; i <= dgvRapport.Rows.Count - 1; i++)
             {
                 var Yloc = unite_hauteur * j + 9 * unite_hauteur;

                 graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 23, unite_hauteur - 2);
                 graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, 5 * unite_largeur , Yloc);
                 graphic.DrawString(dgvRapport.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur , Yloc);
                 j++;
             }

             for (var l = 0; l < dgvRapport.Rows.Count; l++)
             {
                 montantTotalVente += Double.Parse(dgvRapport.Rows[l].Cells[1].Value.ToString());
             }
             graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, 25 * unite_largeur + 15, 10 * unite_hauteur);
             if (flag)
             {
                 graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 54 * unite_hauteur, 25 * unite_largeur + 15, unite_hauteur + 5);
                 graphic.DrawString("Montant total  ", fnt11, Brushes.White, unite_largeur, 54 * unite_hauteur - 2);
                 graphic.DrawString(string.Format(elGR, "{0:0,0}", montantTotalVente) + " F.CFA", fnt11, Brushes.White, 18 * unite_largeur, 54 * unite_hauteur - 2);
             }
             return bitmap;
         }
         
         /********************************************************************/
         public static Bitmap RapportDuneEntreprise(System.Windows.Forms.DataGridView dtGrid, Entreprise entreprise, DateTime date, double montantTotal)
         {
             Bitmap bitmap;
             try
             {
                 #region facture1
                 //les dimension de la facture
                 int unite_hauteur = 20;
                 int unite_largeur = 32;
                 int largeur_facture = 25 * unite_largeur;
                 int hauteur_facture = 0;
                 bool flag = false;
                 if (dtGrid.Rows.Count <= 30)
                 {
                     flag = true;
                     hauteur_facture = 55 * unite_hauteur;
                 }
                 else
                 {
                     flag = false;
                     hauteur_facture = 49 * unite_hauteur + 15;
                 }

                 //creer un bit map
                 bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                 // creer un objet graphic
                 Graphics graphic = Graphics.FromImage(bitmap);

                 //la couleur de l'image
                 graphic.Clear(Color.White);

                 //definir les polices 
                 Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
                 Font fnt33 = new Font("Century Gothic", 11, FontStyle.Regular);
                 Font fnt0 = new Font("Century Gothic", 11, FontStyle.Regular);
                 Font fnt11 = new Font("Century Gothic", 11, FontStyle.Bold);
                 //Font fnt2 = new Font("Bodoni MT", 8, FontStyle.Bold);
                 Font fnt3 = new Font("Century Gothic", 18, FontStyle.Bold);
                 Font fnt22 = new Font("Century Gothic", 9, FontStyle.Regular);

                 try
                 {
                     Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                     graphic.DrawImage(logo, 19 * unite_largeur, 10, 3 * unite_largeur, 4 * unite_hauteur);


                 }
                 catch (Exception)
                 { }
                         var listeDepot = ConnectionClass.ListeDesDepots();
             graphic.DrawString(listeDepot[0].NomDepot, fnt11, Brushes.DeepSkyBlue, unite_largeur, unite_hauteur);
             graphic.DrawString(listeDepot[0].Adresse, fnt33, Brushes.Black, unite_largeur, 2 * unite_hauteur + 10);
             graphic.DrawString("Tél : " + listeDepot[0].Telephone1 + " / " + listeDepot[0].Telephone2, fnt33, Brushes.Black, unite_largeur, 3 * unite_hauteur + 6);
             graphic.DrawString(listeDepot[0].Ville + "-" + listeDepot[0].Pays, fnt33, Brushes.Black, unite_largeur, 4 * unite_hauteur + 2);
             if (!string.IsNullOrEmpty(listeDepot[0].Email))
             {
                 graphic.DrawString(listeDepot[0].Email, fnt33, Brushes.Black, unite_largeur, 5 * unite_hauteur - 2);
                 graphic.DrawString(listeDepot[0].Commentaire, fnt33, Brushes.Black, unite_largeur, 6 * unite_hauteur - 6);
             }
             else
             {
                 graphic.DrawString(listeDepot[0].Commentaire, fnt33, Brushes.Black, unite_largeur, 5 * unite_hauteur - 2);
             }
                 #endregion

                 graphic.DrawString("Page 1", fnt0, Brushes.SteelBlue, 22 * unite_largeur, unite_hauteur);


                 //graphic.DrawString("Compte ORABANK N° 10649200100*92", fnt1, Brushes.White, 4 * unite_largeur, 11 * unite_hauteur);
                 graphic.DrawString("FACTURE DU  " + date.ToShortDateString(), fnt3, Brushes.SteelBlue, 6 * unite_largeur, 15 * unite_hauteur - 15);
                 graphic.DrawLine(Pens.SteelBlue, 6 * unite_largeur, 16 * unite_hauteur - 10, 15 * unite_largeur, 16 * unite_hauteur - 10);
                 graphic.DrawString("Date d'émission :  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.SteelBlue, 17 * unite_largeur, 8 * unite_hauteur);

                 graphic.DrawRectangle(Pens.SteelBlue, unite_largeur - 5, 11 * unite_hauteur + 10, 23 * unite_largeur, 3 * unite_hauteur - 5);
                 graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 10 * unite_hauteur + 13, 23 * unite_largeur + 2, unite_hauteur);
                 graphic.DrawString("Information de l' entreprise  ".ToUpper(), fnt1, Brushes.White, 2 * unite_largeur, 10 * unite_hauteur + 13);
                 graphic.DrawString("Nom :", fnt1, Brushes.SteelBlue, 2 * unite_largeur, 11 * unite_hauteur + 12);
                 graphic.DrawString("Tél :   ", fnt1, Brushes.SteelBlue, 2 * unite_largeur, 12 * unite_hauteur + 9);
                 graphic.DrawString("Adresse :    ", fnt1, Brushes.SteelBlue, 2 * unite_largeur, 13 * unite_hauteur + 6);

                 graphic.DrawString(entreprise.NomEntreprise, fnt1, Brushes.SteelBlue, 6 * unite_largeur, 11 * unite_hauteur + 12);
                 graphic.DrawString(entreprise.Telephone1 + " / " + entreprise.Telephone2, fnt1, Brushes.SteelBlue, 6 * unite_largeur, 12 * unite_hauteur + 9);
                 graphic.DrawString(entreprise.Adresse, fnt1, Brushes.SteelBlue, 6 * unite_largeur, 13 * unite_hauteur + 6);

                 graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 3, 17 * unite_hauteur - 15, 23 * unite_largeur + 2, unite_hauteur);
                 graphic.DrawString("Nom & Prenom  ".ToUpper(), fnt1, Brushes.White, unite_largeur, 17 * unite_hauteur - 15);
                 graphic.DrawString("Date  ".ToUpper(), fnt1, Brushes.White, 15 * unite_largeur, 17 * unite_hauteur - 15);
                 graphic.DrawString("Montant  ".ToUpper(), fnt1, Brushes.White, 21 * unite_largeur, 17 * unite_hauteur - 15);

                 for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
                 {

                     var Yloc = unite_hauteur * i + 18 * unite_hauteur;
                     var client = dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper();


                     if (dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper() == "SOUS TOTAL")
                     {
                         graphic.DrawLine(Pens.SteelBlue, unite_largeur, Yloc + 2, 24 * unite_largeur, Yloc);
                         //graphic.DrawLine(Pens.SteelBlue, unite_largeur, Yloc + unite_hauteur + 2, 24 * unite_largeur, Yloc + unite_hauteur + 2);

                         graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString(), fnt11, Brushes.SteelBlue, unite_largeur, Yloc + 4);
                         graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11, Brushes.SteelBlue, 21 * unite_largeur, Yloc + 4);
                     }
                     else
                     {
                         graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.SteelBlue, 15 * unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper(), fnt0,
                             Brushes.SteelBlue, unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.SteelBlue, 21 * unite_largeur, Yloc);
                     }
                 }

                 var Kloc = 48 * unite_hauteur;
                 if (flag == true)
                 {
                     graphic.FillRectangle(Brushes.White, 20, Kloc, 24 * unite_largeur + 10, 14 * unite_hauteur + 5);

                     graphic.FillRectangle(Brushes.SteelBlue, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 5);
                     graphic.DrawString("Total ", fnt11, Brushes.White, unite_largeur, Kloc);
                     graphic.DrawString(montantTotal.ToString() + " F CFA", fnt11, Brushes.White, 20 * unite_largeur, Kloc);
                     graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 20, Kloc + 2 * unite_hauteur);
                     graphic.DrawString(Converti((int)montantTotal) + " Francs CFA", fnt1, Brushes.SteelBlue, 11 * unite_largeur, Kloc + 2 * unite_hauteur);
                     try
                     {
                         //graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                         graphic.DrawString(Form1.nomEmploye.ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);
                     }
                     catch { }
                 }
             }
             catch (Exception exception)
             {
                 MonMessageBox.ShowBox("I;pression ", exception);
                 return null;
             }
             return bitmap;
         }

         public static Bitmap RapportDuneEntreprise(System.Windows.Forms.DataGridView dtGrid, double montantTotal, int start)
         {
             Bitmap bitmap;
             try
             {
                 #region facture1
                 //les dimension de la facture
                 int unite_hauteur = 20;
                 int unite_largeur = 32;
                 int largeur_facture = 25 * unite_largeur;
                 int hauteur_facture = 0;
                 bool flag = false;
                 if (dtGrid.Rows.Count <= 30 + 45 * (1 + start))
                 {
                     flag = true;
                     hauteur_facture = 55 * unite_hauteur;
                 }
                 else
                 {
                     flag = false;
                     hauteur_facture = 49 * unite_hauteur + 15;
                 }

                 //creer un bit map
                 bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                 // creer un objet graphic
                 Graphics graphic = Graphics.FromImage(bitmap);
                 graphic.Clear(Color.White);
                 //definir les polices 
                 Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
                 Font fnt33 = new Font("Century Gothic", 11, FontStyle.Regular);
                 Font fnt0 = new Font("Century Gothic", 11, FontStyle.Regular);
                 Font fnt11 = new Font("Century Gothic", 11, FontStyle.Bold);
                 //Font fnt2 = new Font("Bodoni MT", 8, FontStyle.Bold);
                 Font fnt3 = new Font("Century Gothic", 18, FontStyle.Bold);
                 Font fnt22 = new Font("Century Gothic", 9, FontStyle.Regular);
                 #endregion

                 graphic.DrawString("Page " + (start + 1), fnt0, Brushes.SteelBlue, 22 * unite_largeur, 5);

                 graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 3, 2 * unite_hauteur - 10, 23 * unite_largeur + 2, unite_hauteur);
                 graphic.DrawString("Nom & Prenom  ".ToUpper(), fnt1, Brushes.White, unite_largeur, 2 * unite_hauteur - 10);
                 graphic.DrawString("Date  ".ToUpper(), fnt1, Brushes.White, 15 * unite_largeur, 2 * unite_hauteur - 10);
                 graphic.DrawString("Montant  ".ToUpper(), fnt1, Brushes.White, 21 * unite_largeur, 2 * unite_hauteur - 10);

                 var j = 0;
                 for (var i = 30 + start * 45; i <= dtGrid.Rows.Count - 1; i++)
                 {
                     var Yloc = unite_hauteur * j + 3 * unite_hauteur;
                     var client = dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper();


                     if (dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper() == "SOUS TOTAL")
                     {
                         graphic.DrawLine(Pens.SteelBlue, unite_largeur, Yloc + 2, 24 * unite_largeur, Yloc);
                         //graphic.DrawLine(Pens.SteelBlue, unite_largeur, Yloc + unite_hauteur + 2, 24 * unite_largeur, Yloc + unite_hauteur + 2);

                         graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString(), fnt11, Brushes.SteelBlue, unite_largeur, Yloc + 4);
                         graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11, Brushes.SteelBlue, 21 * unite_largeur, Yloc + 4);
                     }
                     else
                     {
                         graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.SteelBlue, 15 * unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper(), fnt0,
                             Brushes.SteelBlue, unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.SteelBlue, 21 * unite_largeur, Yloc);
                     }
                     j++;
                 }

                 var Kloc = 48 * unite_hauteur;
                 if (flag == true)
                 {
                     graphic.FillRectangle(Brushes.White, 20, Kloc, 24 * unite_largeur + 10, 14 * unite_hauteur + 5);
                     graphic.FillRectangle(Brushes.SteelBlue, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 5);
                     graphic.DrawString("Total ", fnt11, Brushes.White, unite_largeur, Kloc);
                     graphic.DrawString(montantTotal.ToString() + " F CFA", fnt11, Brushes.White, 20 * unite_largeur, Kloc);
                     graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 20, Kloc + 2 * unite_hauteur);
                     graphic.DrawString(Converti((int)montantTotal) + " Francs CFA", fnt1, Brushes.SteelBlue, 11 * unite_largeur, Kloc + 2 * unite_hauteur);
                     try
                     {
                         //graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                         graphic.DrawString(Form1.nomEmploye.ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);
                     }
                     catch { }
                 }
             }
             catch (Exception exception)
             {
                 MonMessageBox.ShowBox("I;pression ", exception);
                 return null;
             }
             return bitmap;
         }

         public static Bitmap ImprimerRapportDesVentesParJour(string titre, System.Windows.Forms.DataGridView dgvRapport, int start)
         {
             //les dimension de la facture
             #region
             int unite_hauteur = 20;
             int unite_largeur = 32;
             int largeur_facture = 24 * unite_largeur;
             int detail_hauteur_facture = 10 * unite_hauteur;
             int hauteur_facture = 56 * unite_hauteur;
             bool flag = false;
             if (dgvRapport.Rows.Count <= 45 * (1 + start))
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
             Font fnt11 = new Font("Arial Unicode MS", 14, FontStyle.Bold);
             Font fnt3 = new Font("Arial Unicode MS", 18, FontStyle.Bold);
             Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
             // dessiner les ecritures 
             
             graphic.DrawString(titre, fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur - 10);

             graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 8 * unite_hauteur - 2, 25 * unite_largeur + 15, unite_hauteur);
             graphic.DrawString("Date ".ToUpper(), fnt1, Brushes.White, 5 * unite_largeur, 8 * unite_hauteur);
             graphic.DrawString("Total ".ToUpper(), fnt1, Brushes.White, 17 * unite_largeur, 8 * unite_hauteur);
             var montantTotalVente = 0.0;
             var j = 0;
             for (var i = start * 45; i <= dgvRapport.Rows.Count - 1; i++)
             {
                 var Yloc = unite_hauteur * j + 9 * unite_hauteur;

                 graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 12 - 19, unite_hauteur - 2);
                 graphic.DrawRectangle(Pens.Black, unite_largeur * 13 - 16, Yloc, unite_largeur * 12 - 19, unite_hauteur - 2);
                 graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, 5 * unite_largeur + 5, Yloc);
                 graphic.DrawString(dgvRapport.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 5, Yloc);
                 j++;
             }

             for (var l = 0; l < dgvRapport.Rows.Count; l++)
             {
                 montantTotalVente += Double.Parse(dgvRapport.Rows[l].Cells[1].Value.ToString());
             }
             graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, 25 * unite_largeur + 15, 3 * unite_hauteur);
            if (flag)
             {

                 graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 54 * unite_hauteur, 25 * unite_largeur + 15, unite_hauteur + 5);
                 graphic.DrawString("Montant total  ", fnt11, Brushes.White, unite_largeur, 54 * unite_hauteur - 2);
                 graphic.DrawString(string.Format(elGR, "{0:0,0}", montantTotalVente) + " F.CFA", fnt11, Brushes.White, 18 * unite_largeur, 54 * unite_hauteur - 2);
             }
             return bitmap;
         }

         public static string Converti(int chiffre)
         {
             int centaine, dizaine, unite, reste, y;
             bool dix = false;
             string lettre = "";
             //strcpy(lettre, "Ndjaména-Tchad");

             reste = chiffre / 1;

             for (int i = 1000000000; i >= 1; i /= 1000)
             {
                 y = reste / i;
                 if (y != 0)
                 {
                     centaine = y / 100;
                     dizaine = (y - centaine * 100) / 10;
                     unite = y - (centaine * 100) - (dizaine * 10);
                     switch (centaine)
                     {
                         case 0:
                             break;
                         case 1:
                             lettre += "cent ";
                             break;
                         case 2:
                             if ((dizaine == 0) && (unite == 0))
                             {
                                 lettre += "deux cents ";
                             }
                             else
                             {
                                 lettre += "deux cent ";
                             }
                             break;
                         case 3:
                             if ((dizaine == 0) && (unite == 0))
                             {
                                 lettre += "trois cents ";
                             }
                             else
                             {
                                 lettre += "trois cent ";
                             }
                             break;
                         case 4:
                             if ((dizaine == 0) && (unite == 0))
                             {
                                 lettre += "quatre cents ";
                             }
                             else { lettre += "quatre cent "; }
                             break;
                         case 5:
                             if ((dizaine == 0) && (unite == 0)) { lettre += "cinq cents "; }
                             else { lettre += "cinq cent "; }
                             break;
                         case 6:
                             if ((dizaine == 0) && (unite == 0)) { lettre += "six cents "; }
                             else { lettre += "six cent "; }
                             break;
                         case 7:
                             if ((dizaine == 0) && (unite == 0)) { lettre += "sept cents "; }
                             else { lettre += "sept cent "; }
                             break;
                         case 8:
                             if ((dizaine == 0) && (unite == 0)) { lettre += "huit cents "; }
                             else { lettre += "huit cent "; }
                             break;
                         case 9:
                             if ((dizaine == 0) && (unite == 0)) lettre += "neuf cents ";
                             else lettre += "neuf cent ";
                             break;
                     }// endSwitch(centaine)

                     switch (dizaine)
                     {
                         case 0:
                             break;
                         case 1:
                             dix = true;
                             break;
                         case 2:
                             lettre += "vingt ";
                             break;
                         case 3:
                             lettre += "trente ";
                             break;
                         case 4:
                             lettre += "quarante ";
                             break;
                         case 5:
                             lettre += "cinquante ";
                             break;
                         case 6:
                             lettre += "soixante ";
                             break;
                         case 7:
                             dix = true;
                             lettre += "soixante ";
                             break;
                         case 8:
                             lettre += "quatre-vingt ";
                             break;
                         case 9:
                             dix = true;
                             lettre += "quatre-vingt ";
                             break;
                     } // endSwitch(dizaine)

                     switch (unite)
                     {
                         case 0:
                             if (dix) lettre += "dix ";
                             break;
                         case 1:
                             if (dix) lettre += "onze ";
                             else lettre += "un ";
                             break;
                         case 2:
                             if (dix) lettre += "douze ";
                             else lettre += "deux ";
                             break;
                         case 3:
                             if (dix) lettre += "treize ";
                             else lettre += "trois ";
                             break;
                         case 4:
                             if (dix) lettre += "quatorze ";
                             else lettre += "quatre ";
                             break;
                         case 5:
                             if (dix) lettre += "quinze ";
                             else lettre += "cinq ";
                             break;
                         case 6:
                             if (dix) lettre += "seize ";
                             else lettre += "six ";
                             break;
                         case 7:
                             if (dix) lettre += "dix-sept ";
                             else lettre += "sept ";
                             break;
                         case 8:
                             if (dix) lettre += "dix-huit ";
                             else lettre += "huit ";
                             break;
                         case 9:
                             if (dix) lettre += "dix-neuf ";
                             else lettre += "neuf ";
                             break;
                     } // endSwitch(unite)

                     switch (i)
                     {
                         case 1000000000:
                             if (y > 1) lettre += "milliards ";
                             else lettre += "milliard ";
                             break;
                         case 1000000:
                             if (y > 1) lettre += "millions ";
                             else lettre += "million ";
                             break;
                         case 1000:
                             lettre += "mille ";
                             break;
                     }
                 } // end if(y!=0)
                 reste -= y * i;
                 dix = false;
             } // end for
             if (lettre.Length == 0) lettre += "zero";

             return lettre;
         }

         /********************************************************************/
  
         public static Bitmap RapportDuneEntrepriseDtetailles(System.Windows.Forms.DataGridView dtGrid, double montantTotal, int start)
         {
             Bitmap bitmap;
             try
             {
                 #region facture1
                 //les dimension de la facture
                 int unite_hauteur = 20;
                 int unite_largeur = 32;
                 int largeur_facture = 25 * unite_largeur;
                 int hauteur_facture = 0;
                 bool flag = false;
                 if (dtGrid.Rows.Count <= 36 + 45 * (1 + start))
                 {
                     flag = true;
                     hauteur_facture = 55 * unite_hauteur;
                 }
                 else
                 {
                     flag = false;
                     hauteur_facture = 49 * unite_hauteur + 15;
                 }

                 //creer un bit map
                 bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                 // creer un objet graphic
                 Graphics graphic = Graphics.FromImage(bitmap);
                 graphic.Clear(Color.White);
                 //definir les polices 
                 Font fnt1 = new Font("Century Gothic", 10, FontStyle.Regular);
                 Font fnt33 = new Font("Century Gothic", 11, FontStyle.Regular);
                 Font fnt0 = new Font("Century Gothic", 10, FontStyle.Regular);
                 Font fnt11 = new Font("Century Gothic", 11, FontStyle.Bold);
                 Font fnt2 = new Font("Century Gothic", 11, FontStyle.Italic);
                 Font fnt3 = new Font("Century Gothic", 18, FontStyle.Bold);
                 Font fnt22 = new Font("Century Gothic", 11, FontStyle.Regular);
      #endregion

                 graphic.DrawString("Page " + (start + 1), fnt0, Brushes.Black, 22 * unite_largeur, 5);

                 graphic.FillRectangle(Brushes.SlateGray, unite_largeur - 3, 2 * unite_hauteur - 10, 24 * unite_largeur + 2, 35);
                 graphic.DrawString("Societes ".ToUpper(), fnt1, Brushes.White, unite_largeur, 2 * unite_hauteur - 10);
                 graphic.DrawString("Date  ".ToUpper(), fnt1, Brushes.White, 5 * unite_largeur + 16, 2 * unite_hauteur - 10);
                 graphic.DrawString("beneficiaires  ".ToUpper(), fnt1, Brushes.White, 9 * unite_largeur, 2 * unite_hauteur - 10);
                 graphic.DrawString("MONTANT  ".ToUpper(), fnt1, Brushes.White, 17 * unite_largeur, 2 * unite_hauteur - 10);
                 graphic.DrawString("PART\nassurés  ".ToUpper(), fnt1, Brushes.White, 20 * unite_largeur, 2 * unite_hauteur - 10);
                 graphic.DrawString("part\nassureurs  ".ToUpper(), fnt1, Brushes.White, 22 * unite_largeur + 16, 2 * unite_hauteur - 10);

                 var j = 0;
                 for (var i = 36 + start * 45; i <= dtGrid.Rows.Count - 1; i++)
                 {
                     var Yloc = unite_hauteur * j + 3 * unite_hauteur+15;
                     var client = dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper();

                     if (dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper() == "SOUS TOTAL" ||
    dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper() == "TOTAL GENERAL")
                     {
                         graphic.DrawLine(Pens.SteelBlue, unite_largeur, Yloc + 2, 25 * unite_largeur, Yloc);
                         //graphic.DrawLine(Pens.SteelBlue, unite_largeur, Yloc + unite_hauteur + 2, 24 * unite_largeur, Yloc + unite_hauteur + 2);

                         graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11, Brushes.Black, 9 * unite_largeur, Yloc + 4);
                         graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, 17 * unite_largeur, Yloc + 4);
                         graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString().ToUpper(), fnt11, Brushes.Black, 20 * unite_largeur, Yloc + 4);
                         graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString().ToUpper(), fnt11, Brushes.Black, 22 * unite_largeur + 16, Yloc + 4);
                     }
                     else
                     {
                         graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt0,
                                    Brushes.Black, 5 * unite_largeur + 16, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 9 * unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString().ToUpper(), fnt0,
                                    Brushes.Black, 20 * unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 22 * unite_largeur + 16, Yloc);
                     } j++;
                 }

                 var Kloc = 48 * unite_hauteur;
                 if (flag == true)
                 {
                     graphic.FillRectangle(Brushes.White, 20, Kloc, 24 * unite_largeur + 10, 14*unite_hauteur + 5);
                     graphic.FillRectangle(Brushes.SlateGray, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 5);
                     graphic.DrawString("Total ", fnt11, Brushes.White, unite_largeur, Kloc);
                     graphic.DrawString(montantTotal.ToString() + " F CFA", fnt11, Brushes.White, 20 * unite_largeur, Kloc);
                     graphic.DrawString("Arrêtée la présente facture à la somme de : " + Converti((int)montantTotal) + " Francs CFA", fnt2, Brushes.Black, 20, Kloc + 2 * unite_hauteur);
                     //graphic.DrawString(, fnt1, Brushes.Black, 11 * unite_largeur, Kloc + 2 * unite_hauteur);
                     try
                     {
                         //graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.Black, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                         graphic.DrawString(Form1.nomEmploye.ToUpper(), fnt11, Brushes.Black, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);
                     }
                     catch { }
                 }
             }
             catch (Exception exception)
             {
                 MonMessageBox.ShowBox("I;pression ", exception);
                 return null;
             }
             return bitmap;
         }

         public static Bitmap RapportDuneEntrepriseDtetailles(System.Windows.Forms.DataGridView dtGrid, string entrerise, DateTime date, double montantTotal)
         {
             Bitmap bitmap;
             try
             {
                 #region facture1
                 //les dimension de la facture
                 int unite_hauteur = 20;
                 int unite_largeur = 32;
                 int largeur_facture = 25 * unite_largeur;
                 int hauteur_facture = 0;
                 bool flag = false;
                 if (dtGrid.Rows.Count <= 36)
                 {
                     flag = true;
                     hauteur_facture = 55 * unite_hauteur;
                 }
                 else
                 {
                     flag = false;
                     hauteur_facture = 49 * unite_hauteur + 22;
                 }

                 //creer un bit map
                 bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                 // creer un objet graphic
                 Graphics graphic = Graphics.FromImage(bitmap);

                 //la couleur de l'image
                 graphic.Clear(Color.White);

                 //definir les polices 
                 Font fnt1 = new Font("Century Gothic", 10, FontStyle.Regular);
                 Font fnt33 = new Font("Century Gothic", 11, FontStyle.Regular);
                 Font fnt0 = new Font("Century Gothic", 10, FontStyle.Regular);
                 Font fnt11 = new Font("Century Gothic", 11, FontStyle.Bold);
                 Font fnt2 = new Font("Century Gothic", 11, FontStyle.Italic);
                 Font fnt3 = new Font("Times New Roman", 20, FontStyle.Bold);
                 Font fnt22 = new Font("Century Gothic", 9, FontStyle.Regular);

                 try
                 {
                     Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                     graphic.DrawImage(logo, 19 * unite_largeur, 10, 3 * unite_largeur, 4 * unite_hauteur);


                 }
                 catch (Exception)
                 { }
                 var listeDepot = ConnectionClass.ListeDesDepots();
             graphic.DrawString(listeDepot[0].NomDepot, fnt11, Brushes.DeepSkyBlue, unite_largeur, unite_hauteur);
             graphic.DrawString(listeDepot[0].Adresse, fnt33, Brushes.Black, unite_largeur, 2 * unite_hauteur + 10);
             graphic.DrawString("Tél : " + listeDepot[0].Telephone1 + " / " + listeDepot[0].Telephone2, fnt33, Brushes.Black, unite_largeur, 3 * unite_hauteur + 6);
             graphic.DrawString(listeDepot[0].Ville + "-" + listeDepot[0].Pays, fnt33, Brushes.Black, unite_largeur, 4 * unite_hauteur + 2);
             if (!string.IsNullOrEmpty(listeDepot[0].Email))
             {
                 graphic.DrawString(listeDepot[0].Email, fnt33, Brushes.Black, unite_largeur, 5 * unite_hauteur - 2);
                 graphic.DrawString(listeDepot[0].Commentaire, fnt33, Brushes.Black, unite_largeur, 6 * unite_hauteur - 6);
             }
             else
             {
                 graphic.DrawString(listeDepot[0].Commentaire, fnt33, Brushes.Black, unite_largeur, 5 * unite_hauteur - 2);
             }
                 #endregion

                 graphic.DrawString("Page 1", fnt0, Brushes.SteelBlue, 22 * unite_largeur, unite_hauteur);


                 //graphic.DrawString("Compte ORABANK N° 10649200100*92", fnt1, Brushes.White, 4 * unite_largeur, 11 * unite_hauteur);
                 graphic.DrawString("FACTURE N°  0" + date.Month + "/" + date.Year, fnt3, Brushes.Black, 6 * unite_largeur, 10 * unite_hauteur - 15);
                 graphic.DrawLine(Pens.SlateGray, 6 * unite_largeur, 11 * unite_hauteur - 5, 14 * unite_largeur + 20, 11 * unite_hauteur - 5);
                 graphic.DrawString("Date d'émission :  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 17 * unite_largeur, 8 * unite_hauteur);

                 graphic.DrawString("Details des credits de " + entrerise, fnt33, Brushes.Black, unite_largeur, 12 * unite_hauteur - 20);

                 graphic.FillRectangle(Brushes.SlateGray, unite_largeur - 3, 13 * unite_hauteur - 15, 24 * unite_largeur + 2, 35);
                 graphic.DrawString("Nom & Prenom ".ToUpper(), fnt1, Brushes.White, unite_largeur, 13 * unite_hauteur - 15);
                 graphic.DrawString("Date  ".ToUpper(), fnt1, Brushes.White, 7 * unite_largeur , 13 * unite_hauteur - 15);
                 graphic.DrawString("Designation  ".ToUpper(), fnt1, Brushes.White, 10 * unite_largeur, 13 * unite_hauteur - 15);
                 graphic.DrawString("qte  ".ToUpper(), fnt1, Brushes.White, 19 * unite_largeur, 13 * unite_hauteur - 15);
                 graphic.DrawString("prix\nunit  ".ToUpper(), fnt1, Brushes.White, 20 * unite_largeur+16, 13 * unite_hauteur - 15);
                 graphic.DrawString("prix\ntotal  ".ToUpper(), fnt1, Brushes.White, 22 * unite_largeur + 16, 13 * unite_hauteur - 15);

                 for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
                 {

                     var Yloc = unite_hauteur * i + 14 * unite_hauteur;
                     var client = dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper();


                     if (dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper() == "SOUS TOTAL" ||
                         dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper() == "TOTAL GENERAL")
                     {
                         graphic.DrawLine(Pens.SteelBlue, unite_largeur, Yloc + 2, 25 * unite_largeur, Yloc);
                         //graphic.DrawLine(Pens.SteelBlue, unite_largeur, Yloc + unite_hauteur + 2, 24 * unite_largeur, Yloc + unite_hauteur + 2);

                         graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11, Brushes.Black, 10 * unite_largeur, Yloc + 4);
                         graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, 19 * unite_largeur, Yloc + 4);
                         graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString().ToUpper(), fnt11, Brushes.Black, 20 * unite_largeur+16, Yloc + 4);
                         graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString().ToUpper(), fnt11, Brushes.Black, 22 * unite_largeur + 16, Yloc + 4);
                     }
                     else
                     {
                         graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt0,
                                    Brushes.Black, 7 * unite_largeur , Yloc);
                         var designation = dtGrid.Rows[i].Cells[2].Value.ToString();
                         if (designation.Length > 35)
                         {
                             designation = designation.Substring(0, 35);
                         }
                         graphic.DrawString(designation.ToUpper(), fnt1, Brushes.Black, 10 * unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 19 * unite_largeur, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString().ToUpper(), fnt0,
                                    Brushes.Black, 20 * unite_largeur+16, Yloc);
                         graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 22 * unite_largeur + 16, Yloc);
                     }
                 }

                 var Kloc = 48 * unite_hauteur;
                 if (flag == true)
                 {
                     graphic.FillRectangle(Brushes.White, 20, Kloc, 24 * unite_largeur + 10, 14 * unite_hauteur + 5);

                     graphic.FillRectangle(Brushes.SlateGray, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 5);
                     graphic.DrawString("Total ", fnt11, Brushes.White, unite_largeur, Kloc);
                     graphic.DrawString(montantTotal.ToString() + " F CFA", fnt11, Brushes.White, 20 * unite_largeur, Kloc);
                     graphic.DrawString("Arrêtée la présente facture à la somme de : " + Converti((int)montantTotal) + " Francs CFA", fnt2, Brushes.Black, 20, Kloc + 2 * unite_hauteur);

                     try
                     {
                         //graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                         graphic.DrawString(Form1.nomEmploye.ToUpper(), fnt11, Brushes.Black, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);
                     }
                     catch { }
                 }
             }
             catch (Exception exception)
             {
                 MonMessageBox.ShowBox("I;pression ", exception);
                 return null;
             }
             return bitmap;
         }


         #region ListeMedicamentStockBas
        //fonction pour dessiner le stock inventaire
        public static Bitmap ImprimerInventaireStockTresBas(int start)
        {
            //les dimension de la facture
            List<Medicament> stockArrayList = ConnectionClass.ListeDesMedicamentsAStockBas();
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
                graphic.DrawImage(logo,  unite_largeur, 10, 23 * unite_largeur, 5 * unite_hauteur);

            }
            catch (Exception)
            { }
      
            // dessiner les ecritures 
            graphic.DrawString("Page " +(start +1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, 10);
          

            graphic.DrawString("stocks médicaments a quantite tres bas  " + DateTime.Now, fnt3, Brushes.DeepSkyBlue, unite_largeur, 6 * unite_hauteur + 5);

            graphic.DrawLine(Pens.LightBlue, unite_largeur, 8 * unite_hauteur, 24 * unite_largeur + 10, 8 * unite_hauteur);
            graphic.DrawLine(Pens.LightBlue, unite_largeur, 9 * unite_hauteur + 5, 24 * unite_largeur + 10, 9 * unite_hauteur + 5);

            graphic.DrawString("Nom médicament", fnt11, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Prix achat", fnt11, Brushes.Black, 15 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Stock", fnt11, Brushes.Black, 18 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Qté alerte", fnt11, Brushes.Black, 21 * unite_largeur, 8 * unite_hauteur);

            var j = 0;
            for (var i = start * 45; i <= stockArrayList.Count - 1; i++)
            {
                var Yloc = unite_hauteur * j + 9 * unite_hauteur;

                if (i % 2 == 0)
                {
                    graphic.FillRectangle(Brushes.SlateGray, unite_largeur, Yloc, unite_largeur * 24 + 10, unite_hauteur + 10);
                }
                else
                    graphic.FillRectangle(Brushes.White, unite_largeur, Yloc, unite_largeur * 24 + 10, unite_hauteur + 10);

                 if (stockArrayList[i].NomMedicament.Length > 40)
                    graphic.DrawString(stockArrayList[i].NomMedicament.Substring(0, 40).ToUpper(), fnt1, Brushes.Black,  unite_largeur, Yloc);
                else
                    graphic.DrawString(stockArrayList[i].NomMedicament.ToUpper(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(stockArrayList[i].PrixAchat.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, Yloc);
                graphic.DrawString(stockArrayList[i].Quantite.ToString(), fnt1, Brushes.Black, 18 * unite_largeur, Yloc);
                graphic.DrawString(stockArrayList[i].QuantiteAlerte.ToString()+" "+i, fnt1, Brushes.Black, 21 * unite_largeur, Yloc);
                j++;
            } 
            graphic.FillRectangle(Brushes.White, unite_largeur, 54*unite_hauteur, unite_largeur * 24 + 10, 2*unite_hauteur + 10);

            return bitmap;
        }

         #endregion


        public static Bitmap ImprimerJournalDesLivraisons(System.Windows.Forms.DataGridView dgvInventaire,string titre, string montantTotal, int index)
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
            Font fnt1 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 4 * unite_hauteur);
            }
            catch { }
            // dessiner les ecritures 
           
            graphic.DrawString(titre, fnt11, Brushes.Black, unite_largeur, 7 * unite_hauteur - 10);
            graphic.DrawString("Page " + (index + 1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, 10);

            graphic.FillRectangle(Brushes.Black, unite_largeur, 8 * unite_hauteur - 2, unite_largeur * 24, unite_hauteur + 2);
            graphic.DrawString("Nom fournisseur".ToUpper(), fnt11, Brushes.White, unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Date ".ToUpper(), fnt11, Brushes.White, 13 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("N° facture".ToUpper(), fnt11, Brushes.White, 17 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("Montant".ToUpper(), fnt11, Brushes.White, 21 * unite_largeur, 8 * unite_hauteur);

            int j = 0;
            for (int i = index * 45; i < dgvInventaire.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 9 * unite_hauteur;

                if (dgvInventaire.Rows[i].Cells[0].Value.ToString().ToUpper() == "SOUS TOTAL")
                {
                    graphic.DrawLine(Pens.Black, unite_largeur, Yloc, unite_largeur * 24, Yloc);

                    graphic.DrawString(dgvInventaire.Rows[i].Cells[0].Value.ToString().ToUpper(), fnt11, Brushes.Black, unite_largeur, Yloc);
                    graphic.DrawString(dgvInventaire.Rows[i].Cells[3].Value.ToString() + ".F", fnt11, Brushes.Black, 21 * unite_largeur, Yloc);

                }
                else
                {
                    graphic.DrawString(dgvInventaire.Rows[i].Cells[0].Value.ToString().ToUpper(), fnt1, Brushes.Black, unite_largeur, Yloc);
                    graphic.DrawString(dgvInventaire.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 13 * unite_largeur, Yloc);
                    graphic.DrawString(dgvInventaire.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur, Yloc);
                    graphic.DrawString(dgvInventaire.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur, Yloc);

                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur , unite_largeur * 24, unite_hauteur * 8);

            if (dgvInventaire.Rows.Count <= 45 * (1 + index))
            {
                graphic.FillRectangle(Brushes.Black, unite_largeur, 54 * unite_hauteur + 4, unite_largeur * 24, unite_hauteur);
                graphic.DrawString("Montant TOTAL".ToUpper(), fnt11, Brushes.White, unite_largeur, 54 * unite_hauteur );
                graphic.DrawString(montantTotal + ".F", fnt11, Brushes.White, 20 * unite_largeur, 54 * unite_hauteur );

            }
            return bitmap;
        }

        #region statistiqueVente
        //fonction pour dessiner le stock inventaire
        public static Bitmap ImprimerStatistique(System.Windows.Forms.DataGridView dgvView, string titre, int index)
        {
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
            Font fnt1 = new Font("Arial Narrow", 12, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 16, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold | FontStyle.Underline);
            Font fnt33 = new Font("Arial Narrow", 10, FontStyle.Regular);
            Font fnt2 = new Font("Arial Narrow", 12, FontStyle.Bold);
            Font fnt0 = new Font("Arial Narrow", 11.5f, FontStyle.Bold);
            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
            }
            catch { }
            graphic.DrawString("Page " + (index + 1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, unite_hauteur);

            graphic.DrawString(titre, fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur - 10);

            graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur-6, unite_largeur * 23, unite_hauteur+3);
            
            graphic.DrawString("Désignation".ToUpper(), fnt0, Brushes.Black, unite_largeur + 5, 8 * unite_hauteur-2);
            graphic.DrawString("Stock".ToUpper(), fnt0, Brushes.Black, 13 * unite_largeur -10, 8 * unite_hauteur-2);
            graphic.DrawString("Qté".ToUpper(), fnt0, Brushes.Black, 15 * unite_largeur-10 , 8 * unite_hauteur-2);
            graphic.DrawString("Prix".ToUpper(), fnt0, Brushes.Black, 17 * unite_largeur + 5, 8 * unite_hauteur-2);
            graphic.DrawString("Px total".ToUpper(), fnt0, Brushes.Black, 19 * unite_largeur +9, 8 * unite_hauteur-2);
            graphic.DrawString("% vente".ToUpper(), fnt0, Brushes.Black, 22 * unite_largeur - 6, 8 * unite_hauteur-2);

            var j = 0;
            for (int i = 45*index; i < dgvView.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 9 * unite_hauteur;
                graphic.DrawRectangle(Pens.Black, unite_largeur, Yloc, unite_largeur * 11 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 12, Yloc, unite_largeur * 2 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 14, Yloc, unite_largeur * 3 - 18, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 16+16, Yloc, unite_largeur * 3 - 18, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 19, Yloc, unite_largeur * 3 - 18, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 22-16, Yloc, unite_largeur * 2+16 , unite_hauteur);

                graphic.DrawString(dgvView.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 10, Yloc);
                graphic.DrawString(dgvView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 12 * unite_largeur + 15, Yloc);
                graphic.DrawString(dgvView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur + 10, Yloc);
                graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur + 28, Yloc);
                graphic.DrawString(dgvView.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 19 * unite_largeur + 15, Yloc);
                graphic.DrawString(dgvView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 22 * unite_largeur, Yloc);
                j++;
            }
            graphic.FillRectangle(Brushes.White, unite_largeur  - 16, 54*unite_hauteur+2, unite_largeur * 24 + 16, unite_hauteur*3);
              
            return bitmap;
        }
       #endregion

        #region statistiqueVente
        //fonction pour dessiner le stock inventaire
        public static Bitmap ImprimerListingProduit(System.Windows.Forms.DataGridView dgvProduit,  int index)
        {
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
            Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold | FontStyle.Underline);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt0 = new Font("Arial Unicode MS", 10, FontStyle.Bold);


            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
            }
            catch { } 
            
            graphic.DrawString("Page " + (index + 1).ToString(), fnt33, Brushes.Black, 12 * unite_largeur, unite_hauteur);

            graphic.DrawString("Listing des produits", fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, 8 * unite_hauteur - 6, unite_largeur * 23, unite_hauteur + 3);

            graphic.DrawString("Désignation".ToUpper(), fnt0, Brushes.White, unite_largeur*2, 8 * unite_hauteur - 2);
            graphic.DrawString("Prix achat".ToUpper(), fnt0, Brushes.White, 16 * unite_largeur, 8 * unite_hauteur - 2);
            graphic.DrawString("Px vente".ToUpper(), fnt0, Brushes.White, 20 * unite_largeur, 8 * unite_hauteur - 2);
            
            var j = 0;
            for (int i = 45 * index; i < dgvProduit.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 9 * unite_hauteur;
                graphic.DrawLine(Pens.Salmon, unite_largeur, Yloc, unite_largeur * 24, Yloc);

                if (i % 2 == 1)
                    graphic.FillRectangle(Brushes.PeachPuff, unite_largeur, Yloc, unite_largeur * 23, unite_hauteur);
              
                graphic.DrawString(dgvProduit.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, unite_largeur *2, Yloc);
                graphic.DrawString(dgvProduit.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur , Yloc);
                graphic.DrawString(dgvProduit.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur , Yloc);
                j++;
            }
            graphic.FillRectangle(Brushes.White, unite_largeur - 16, 54 * unite_hauteur, unite_largeur * 24 + 16, unite_hauteur * 3);

            return bitmap;
        }
        #endregion


        public static Bitmap ImprimerRapportComptabilite(string titre, System.Windows.Forms.DataGridView dgvRapport, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 21;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 56 * unite_hauteur;
            bool flag = false;
            if (dgvRapport.Rows.Count <= 45 * (1 + start))
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
            Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 14, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            // dessiner les ecritures 


            try
            {
                Image logo = global::GestionPharmacetique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 5 * unite_hauteur);
            }
            catch { } 
            graphic.DrawString("Page " + (start + 1).ToString(), fnt33, Brushes.Black, 15 * unite_largeur, 5);

            graphic.DrawString(titre, fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur -15);

            graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, 8 * unite_hauteur - 9, 23 * unite_largeur , unite_hauteur+10);
            graphic.DrawString("Date ".ToUpper(), fnt1, Brushes.White, 10+ unite_largeur, 8 * unite_hauteur-10);
            graphic.DrawString("Recettes ".ToUpper(), fnt1, Brushes.White, 5* unite_largeur, 8 * unite_hauteur-5);
            graphic.DrawString("Dépenses éffectuées ".ToUpper(), fnt1, Brushes.White, 9* unite_largeur, 8 * unite_hauteur-5);
            graphic.DrawString("Solde ".ToUpper(), fnt1, Brushes.White, 21 * unite_largeur, 8 * unite_hauteur-5);
            var montantTotalVente = 0.0;
            var j = 0;
            for (var i = start * 45; i <= dgvRapport.Rows.Count - 1; i++)
            {
                var Yloc = unite_hauteur * j + 9 * unite_hauteur;

                graphic.DrawLine(Pens.Salmon, unite_largeur, Yloc, unite_largeur * 24,  Yloc);
               
                if(i%2==1)
                graphic.FillRectangle(Brushes.PeachPuff, unite_largeur, Yloc, unite_largeur * 23, unite_hauteur );
                
                if (dgvRapport.Rows[i].Cells[0].Value.ToString().ToUpper() == "TOTAUX")
                {
                    graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, Yloc, 23 * unite_largeur , unite_hauteur );
                    
                    graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString(), fnt11, Brushes.White, unite_largeur + 10, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.White, 5 * unite_largeur+15, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.White, 9 * unite_largeur, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.White, 18 * unite_largeur, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.White, 21 * unite_largeur, Yloc);

                }
                else if (dgvRapport.Rows[i].Cells[0].Value.ToString().ToUpper() == "SOUS TOTAUX")
                {
                    graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString(), fnt11, Brushes.Black, unite_largeur + 10, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.Black, 5 * unite_largeur+15, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.Black, 9 * unite_largeur, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, 18 * unite_largeur, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.Black, 21 * unite_largeur, Yloc);

                }
                else
                {
                    graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 10, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 5 * unite_largeur+15, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 9 * unite_largeur, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur, Yloc);

                }
                    j++;
            }

            //for (var l = 0; l < dgvRapport.Rows.Count; l++)
            //{
            //    //montantTotalVente += Double.Parse(dgvRapport.Rows[l].Cells[1].Value.ToString());
            //}
            //graphic.FillRectangle(Brushes.White, unite_largeur, 54 * unite_hauteur, 25 * unite_largeur + 15, 3 * unite_hauteur);
            //if (flag)
            //{

            //    graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 54 * unite_hauteur, 25 * unite_largeur + 15, unite_hauteur + 5);
            //    graphic.DrawString("Montant total  ", fnt11, Brushes.White, unite_largeur, 54 * unite_hauteur - 2);
            //    graphic.DrawString(string.Format(elGR, "{0:0,0}", montantTotalVente) + " F.CFA", fnt11, Brushes.White, 18 * unite_largeur, 54 * unite_hauteur - 2);
            //}
            return bitmap;
        }


        public static Bitmap ImprimerChronologieProduit(System.Windows.Forms.DataGridView listView, string titre, int index)
        {
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 46 * unite_hauteur + detail_hauteur_facture;

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
            graphic.DrawRectangle(Pens.Black, unite_largeur, 7 * unite_hauteur - 10, unite_largeur * 23, 2 * unite_hauteur - 5);

            graphic.DrawString(titre, fnt2, Brushes.Black, 2 * unite_largeur, 7* unite_hauteur - 5);

            graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, 9 * unite_hauteur, 23 * unite_largeur, unite_hauteur + 10);
            graphic.DrawString("Date", fnt2, Brushes.White, unite_largeur + 15, 10 * unite_hauteur - 15);
            graphic.DrawString("Qté livrée", fnt2, Brushes.White, 6 * unite_largeur , 10 * unite_hauteur - 15);
            graphic.DrawString("Qté rétournée", fnt2, Brushes.White, 10 * unite_largeur , 10 * unite_hauteur - 15);
            graphic.DrawString("Heure vente", fnt2, Brushes.White, 15 * unite_largeur , 10 * unite_hauteur - 15);
            graphic.DrawString("Qté Vendue", fnt2, Brushes.White, 20 * unite_largeur , 10 * unite_hauteur - 15);


            for (int i = index*45; i < listView.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * i + 11 * unite_hauteur;

                graphic.DrawLine(Pens.Salmon, unite_largeur, Yloc, unite_largeur * 24, Yloc);

                if (i % 2 == 1)
                    graphic.FillRectangle(Brushes.PeachPuff, unite_largeur, Yloc, unite_largeur * 23, unite_hauteur);
                if (listView.Rows[i].Cells[0].Value.ToString() == "Nbres totaux")
                {
                    graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, Yloc+3, 23 * unite_largeur, unite_hauteur);
                    graphic.DrawString(listView.Rows[i].Cells[0].Value.ToString(), fnt2, Brushes.White, unite_largeur + 15, Yloc);
                    graphic.DrawString(listView.Rows[i].Cells[1].Value.ToString(), fnt2, Brushes.White, 6 * unite_largeur, Yloc);
                    graphic.DrawString(listView.Rows[i].Cells[2].Value.ToString(), fnt2, Brushes.White, 10 * unite_largeur, Yloc);
                    graphic.DrawString(listView.Rows[i].Cells[3].Value.ToString(), fnt2, Brushes.White, 15 * unite_largeur, Yloc);
                    graphic.DrawString(listView.Rows[i].Cells[4].Value.ToString(), fnt2, Brushes.White, 20 * unite_largeur, Yloc);
              
                }
                else
                {
                    graphic.DrawString(listView.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 15, Yloc);
                    graphic.DrawString(listView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 6 * unite_largeur, Yloc);
                    graphic.DrawString(listView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 10 * unite_largeur, Yloc);
                    graphic.DrawString(listView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, Yloc);
                    graphic.DrawString(listView.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur, Yloc);
                }
            }
            graphic.FillRectangle(Brushes.White, unite_largeur - 16, 56 * unite_hauteur, unite_largeur * 24 + 16, unite_hauteur * 3);              
           
            return bitmap;
        }

     }
}
