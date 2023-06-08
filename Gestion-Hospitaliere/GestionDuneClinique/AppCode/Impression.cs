using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Data;
using System.Windows.Forms;
namespace GestionDuneClinique.AppCode
{
    public class Impression
    {
        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        //fonction pour dessiner l'ordonnance
        public static Bitmap ImprimerOrdonnance(int numeroOrdonnance, DataGridView dtGrid,Patient patient,string medecin)
        {
            //#region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 12 * unite_hauteur;
            int hauteur_facture = 38 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //// creer un objet graphic
            //Graphics graphic = Graphics.FromImage(bitmap);

            ////la couleur de l'image
            //graphic.Clear(Color.White);
            //try
            //{
            //    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
            //    graphic.DrawImage(logo, unite_largeur, 10, 17 * unite_largeur, 6 * unite_hauteur);
            //}
            //catch { }
            ////definir les polices 
            //Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            //Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            //Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
            //Font fnt3 = new Font("Century Gothic", 20, FontStyle.Bold);
            //Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);

            // #endregion
            //graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur , 17 * unite_largeur, 5 * unite_hauteur);
            //graphic.FillRectangle(Brushes.Black, unite_largeur, 8 * unite_hauteur , 17 * unite_largeur + 2, unite_hauteur);
            //if (patient.Sexe == "M")
            //{
            //    graphic.DrawString("Informations du patient  ", fnt1, Brushes.White, 6 * unite_largeur, 8 * unite_hauteur );
            //}
            //else
            //{
            //    graphic.DrawString("Informations de la patiente  ", fnt1, Brushes.White, 6 * unite_largeur, 8 * unite_hauteur );
            //}
            //graphic.DrawString(string.Format("{0,-25} {1,-80}", "Patient :", patient.Nom + " " + patient.Prenom), fnt1, Brushes.Black, 3*unite_largeur, 9 * unite_hauteur);
            //graphic.DrawString(string.Format("{0,-25} {1,-80}", "Sexe :   ", patient.Sexe), fnt1, Brushes.Black, 3 * unite_largeur, 10 * unite_hauteur - 3);
            //graphic.DrawString(string.Format("{0,-25} {1,-80}", "Age :    ", patient.Age.ToString() ), fnt1, Brushes.Black, 3 * unite_largeur, 11 * unite_hauteur - 6);
            //if (patient.Poids == 0)
            //{
            //    graphic.DrawString(string.Format("{0,-25} {1,-80}", "Poids :  ", "-"), fnt1, Brushes.Black, 3 * unite_largeur, 12 * unite_hauteur - 9);
            //}
            //else if (patient.Poids > 0)
            //{
            //    graphic.DrawString(string.Format("{0,-25} {1,-80} ", "Poids :  ", patient.Poids.ToString() + " kg"), fnt1, Brushes.Black, 3 * unite_largeur, 12 * unite_hauteur - 9);
            //}
            //graphic.DrawString("N'DJAMENA LE  "+DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 13 * unite_hauteur +15);
            //graphic.DrawString("ORDONNANCE MEDICALE", fnt3, Brushes.Black, 3*unite_largeur, 16*unite_hauteur);
            //graphic.DrawLine(Pens.Black, unite_largeur*3+10, 17 * unite_hauteur + 10, 13 * unite_largeur + 20, 17 * unite_hauteur + 10);

            
            //for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
            //{
            //    var Yloc = 30 * i + 19 * unite_hauteur ;
            //    graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper(), fnt1, Brushes.Black,  unite_largeur, Yloc);
            //    graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt1, Brushes.Black, 11 * unite_largeur - 10, Yloc);
            //    graphic.DrawString(" X ", fnt1, Brushes.Black, 12 * unite_largeur, Yloc);
            //    graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper() + "/Jr", fnt1, Brushes.Black, 13 * unite_largeur, Yloc);
            //    graphic.DrawString(" X ", fnt1, Brushes.Black, 14 * unite_largeur + 15, Yloc);
            //    graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper() + "Jour(s)", fnt1, Brushes.Black, 15 * unite_largeur + 15, Yloc);
            //  }

            //graphic.DrawString("Le Medecin", fnt1, Brushes.Black, 7 * unite_largeur - 10, 33 * unite_hauteur + 20);
            //graphic.DrawString(medecin, fnt1, Brushes.Black, 10 * unite_largeur - 10, 33 * unite_hauteur + 16);
            //graphic.DrawLine(Pens.Black, 10 * unite_largeur - 12, 35 * unite_hauteur - 5, 14 * unite_largeur, 35 * unite_hauteur - 5);
            return bitmap;
        }

        //fonction pour dessiner le recu de la facture consultation
        public static Bitmap FactureConsultation(int numeroConsultation, DataGridView dtGrid, Patient patient, string caissier)
        {


            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 17 * unite_largeur;
            int detail_hauteur_facture = 12 * unite_hauteur;
            int hauteur_facture = 38 * unite_hauteur;

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logoFacture;
                graphic.DrawImage(logo,  unite_largeur,  unite_hauteur, 16* unite_largeur , 6*unite_hauteur-10);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Ubuntu", 10, FontStyle.Regular);
            Font fnt11 = new Font("Ubuntu", 11, FontStyle.Regular);
            Font fnt2 = new Font("Ubuntu", 15, FontStyle.Bold);
            Font fnt3 = new Font("Ubuntu", 15, FontStyle.Bold | FontStyle.Underline);
            Font fnt22 = new Font("Ubuntu", 11, FontStyle.Bold);
            #endregion

            
                graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur , 16 * unite_largeur, 6 * unite_hauteur+6);
                graphic.FillRectangle(Brushes.Black, unite_largeur, 8 * unite_hauteur , 16 * unite_largeur + 2, unite_hauteur);
                graphic.DrawString("Informations du patient  ".ToUpper(), fnt22, Brushes.White, 6 * unite_largeur, 8 * unite_hauteur );
                graphic.DrawString("IDENTIFIANT : ", fnt1, Brushes.Black,2 * unite_largeur-12, 9 * unite_hauteur +7);
                graphic.DrawString("PATIENT : ", fnt1, Brushes.Black, 2 * unite_largeur-12, 10 * unite_hauteur + 10);
                graphic.DrawString("SEXE : ", fnt1, Brushes.Black, 2 * unite_largeur-12, 11 * unite_hauteur + 7);
                graphic.DrawString("AGE :                   ",  fnt1, Brushes.Black, 2* unite_largeur-12, 12 * unite_hauteur + 4);
                graphic.DrawString("CONVENTIONNE : ", fnt1, Brushes.Black, 2 * unite_largeur-12, 13 * unite_hauteur + 4);

                graphic.DrawString(patient.NumeroPatient.ToString(), fnt2, Brushes.Black, 5* unite_largeur, 9 * unite_hauteur+4);
                if (!string.IsNullOrWhiteSpace(patient.SousCouvert))
                {
                    graphic.DrawString(patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper() + " S/C " + patient.SousCouvert.ToUpper() + " / " ,
                        fnt1, Brushes.Black, 5 * unite_largeur, 10 * unite_hauteur+10);
                }
                else
                {
                    graphic.DrawString(patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(), fnt1, Brushes.Black, 5* unite_largeur, 10 * unite_hauteur+10);
                }
            graphic.DrawString( patient.Sexe.ToUpper(), fnt1, Brushes.Black,5* unite_largeur, 11 * unite_hauteur + 7);
            graphic.DrawString(AfficherAge(patient.An, patient.Mois), fnt1, Brushes.Black, 5 * unite_largeur, 12 * unite_hauteur + 4);
            graphic.DrawString( patient.NomEntreprise.ToString(), fnt1, Brushes.Black,5 * unite_largeur, 13 * unite_hauteur + 4);
            
            graphic.DrawString("N'DJAMENA LE  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 14 * unite_hauteur + 15);
            graphic.DrawString("BON DE CONSULTATION No " + numeroConsultation, fnt3, Brushes.Black, 5 * unite_largeur, 17 * unite_hauteur);
            //graphic.DrawLine(Pens.Black, unite_largeur * 2 + 5, 17 * unite_hauteur + 10, 15 * unite_largeur + 15, 17 * unite_hauteur + 10);

            var id = Int32.Parse(dtGrid.SelectedRows[0].Cells[0].Value.ToString());
            var typeConsult = dtGrid.SelectedRows[0].Cells[1].Value.ToString();
            var date = DateTime.Parse(dtGrid.SelectedRows[0].Cells[2].Value.ToString());
            var frais = Double.Parse(dtGrid.SelectedRows[0].Cells[7].Value.ToString());
            var medecin = dtGrid.SelectedRows[0].Cells[6].Value.ToString();

            graphic.FillRectangle(Brushes.Black, unite_largeur, 19 * unite_hauteur , 16 * unite_largeur + 2, unite_hauteur+2);

            graphic.DrawString("Type de consultation", fnt2, Brushes.White, 5 * unite_largeur, 19 * unite_hauteur-2);
            graphic.DrawString("Date  ", fnt2, Brushes.White, unite_largeur, 19 * unite_hauteur-2);
            graphic.DrawString("Frais ", fnt2, Brushes.White, 15 * unite_largeur, 19 * unite_hauteur-2);

            graphic.DrawString(typeConsult, fnt1, Brushes.Black, 5 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString(date.ToShortDateString(), fnt1, Brushes.Black, unite_largeur, 22 * unite_hauteur);
            graphic.DrawString(frais.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, 22 * unite_hauteur);

            graphic.DrawLine(Pens.Black, unite_largeur, 30 * unite_hauteur-2, 16 * unite_largeur+20, 30 * unite_hauteur-2);
            graphic.DrawString("total", fnt2, Brushes.Black,  unite_largeur, 30 * unite_hauteur-1);
            graphic.DrawString(frais.ToString(), fnt2, Brushes.Black, 15 * unite_largeur-15, 30 * unite_hauteur-1);
            graphic.DrawLine(Pens.Black, unite_largeur, 31 * unite_hauteur+1, 16 * unite_largeur+20, 31 * unite_hauteur+1);

            graphic.DrawString("Arrêtée la présente facture de la consultation à la somme de : ", fnt1, Brushes.Black, unite_largeur, 32 * unite_hauteur);
            graphic.DrawString(Converti((long)frais) + " Francs CFA", fnt1, Brushes.Black, unite_largeur, 33 * unite_hauteur);
            graphic.DrawString("Caissier(e)  :  ".ToUpper(), fnt1, Brushes.Black, 6 * unite_largeur, 35 * unite_hauteur + 17);
            graphic.DrawString(caissier.ToUpper(), fnt2, Brushes.Black,8 * unite_largeur+16, 35 * unite_hauteur + 15);
            
            return bitmap;
        }

        public static Bitmap FactureHospitalisation(Occupation occupation, Patient patient, string caissier)
        {


            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 17 * unite_largeur;
            int detail_hauteur_facture = 12 * unite_hauteur;
            int hauteur_facture = 38 * unite_hauteur;

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur, 16 * unite_largeur, 6 * unite_hauteur - 10);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 9, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 15, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 15, FontStyle.Bold | FontStyle.Underline);
            Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);
            #endregion


            graphic.DrawRectangle(Pens.Black, unite_largeur, 8 * unite_hauteur, 16 * unite_largeur, 6 * unite_hauteur + 6);
            graphic.FillRectangle(Brushes.Black, unite_largeur, 8 * unite_hauteur, 16 * unite_largeur + 2, unite_hauteur);
            graphic.DrawString("Informations du patient  ".ToUpper(), fnt1, Brushes.White, 6 * unite_largeur, 8 * unite_hauteur);
            graphic.DrawString("IDENTIFIANT : ", fnt1, Brushes.Black, 5 * unite_largeur - 10, 9 * unite_hauteur + 7);
            graphic.DrawString("PATIENT : ", fnt1, Brushes.Black, 5 * unite_largeur - 10, 10 * unite_hauteur + 10);
            graphic.DrawString("SEXE : ", fnt1, Brushes.Black, 5 * unite_largeur - 10, 11 * unite_hauteur + 7);
            graphic.DrawString("AGE :                   ", fnt1, Brushes.Black, 5 * unite_largeur - 10, 12 * unite_hauteur + 4);
            graphic.DrawString("CONVENTIONNE : ", fnt1, Brushes.Black, 5 * unite_largeur - 10, 13 * unite_hauteur + 4);

            graphic.DrawString(patient.NumeroPatient.ToString(), fnt2, Brushes.Black, 8 * unite_largeur, 9 * unite_hauteur + 4);
            if (!string.IsNullOrWhiteSpace(patient.SousCouvert))
            {
                graphic.DrawString(patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper() + " S/C " + patient.SousCouvert.ToUpper() + " / ",
                    fnt1, Brushes.Black, 8 * unite_largeur, 10 * unite_hauteur + 10);
            }
            else
            {
                graphic.DrawString(patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(), fnt1, Brushes.Black, 8 * unite_largeur, 10 * unite_hauteur + 10);
            }
            graphic.DrawString(patient.Sexe.ToUpper(), fnt1, Brushes.Black, 8 * unite_largeur, 11 * unite_hauteur + 7);
            graphic.DrawString(AfficherAge(patient.An, patient.Mois), fnt1, Brushes.Black, 8 * unite_largeur, 12 * unite_hauteur + 4);
            graphic.DrawString(patient.NomEntreprise.ToString(), fnt1, Brushes.Black, 8 * unite_largeur, 13 * unite_hauteur + 4);

            graphic.DrawString("N'DJAMENA LE  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 14 * unite_hauteur + 15);
            graphic.DrawString("BON D'HOSPITALISATION No " + occupation.NumeroOccupation, fnt3, Brushes.Black, 5 * unite_largeur, 17 * unite_hauteur);
            //graphic.DrawLine(Pens.Black, unite_largeur * 2 + 5, 17 * unite_hauteur + 10, 15 * unite_largeur + 15, 17 * unite_hauteur + 10);

           

            graphic.FillRectangle(Brushes.Black, unite_largeur, 19 * unite_hauteur, 16 * unite_largeur + 2, unite_hauteur);

            graphic.DrawString("Désignation".ToUpper(), fnt1, Brushes.White, 5 * unite_largeur, 19 * unite_hauteur);
            graphic.DrawString("Date  ".ToUpper(), fnt1, Brushes.White, unite_largeur, 19 * unite_hauteur);
            graphic.DrawString("Frais ".ToUpper(), fnt1, Brushes.White, 15 * unite_largeur, 19 * unite_hauteur);

            //graphic.DrawString(typeConsult, fnt1, Brushes.Black, 5 * unite_largeur, 22 * unite_hauteur);
            //graphic.DrawString(date.ToShortDateString(), fnt1, Brushes.Black, unite_largeur, 22 * unite_hauteur);
            //graphic.DrawString(frais.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, 22 * unite_hauteur);

            //graphic.DrawLine(Pens.Black, unite_largeur, 30 * unite_hauteur, 16 * unite_largeur + 20, 30 * unite_hauteur);
            //graphic.DrawString("total".ToUpper(), fnt1, Brushes.Black, unite_largeur, 30 * unite_hauteur);
            //graphic.DrawString(frais.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, 30 * unite_hauteur);
            //graphic.DrawLine(Pens.Black, unite_largeur, 31 * unite_hauteur, 16 * unite_largeur + 20, 31 * unite_hauteur);

            //graphic.DrawString("Arrêtée la présente facture de la consultation à la somme de : ", fnt1, Brushes.Black, unite_largeur, 32 * unite_hauteur);
            //graphic.DrawString(Converti((long)frais) + " Francs CFA", fnt1, Brushes.Black, unite_largeur, 33 * unite_hauteur);
            //graphic.DrawString("Caissier(e)  :  ".ToUpper(), fnt1, Brushes.Black, 7 * unite_largeur, 35 * unite_hauteur + 17);
            //graphic.DrawString(caissier.ToUpper(), fnt1, Brushes.Black, 9 * unite_largeur + 16, 35 * unite_hauteur + 17);

            return bitmap;
        }

        public static string AfficherAge(string an, string mois)
        {
            int annee;
            if (!string.IsNullOrEmpty(an))
            {
                if (Int32.TryParse(an, out annee))
                {
                    if (!string.IsNullOrEmpty(mois))
                    {
                        return an + " ans et " + mois + " mois ";
                    }
                    else
                    {
                        return an + " ans ";
                    }
                }
                else
                {
                    return an;
                }
            }   
            else
            {
                return mois + " mois";
            }
        }
        //fonction pour dessiner le recu de la facuter
        public static Bitmap FichePatient(  Patient patient)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 12 * unite_hauteur;
            int hauteur_facture = 38 * unite_hauteur;

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, 12 * unite_largeur, 2 * unite_hauteur, 120, 100);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 20, FontStyle.Bold);
            Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);

            graphic.DrawString("CLINIQUE AL-SHIFA", fnt3, Brushes.Black, unite_largeur, unite_hauteur + 10);
            graphic.DrawString("CLINIQUE MEDICO-CHIRURGICALE", fnt22, Brushes.Black, unite_largeur, 3 * unite_hauteur);
            graphic.DrawString("B.P : 365,  DIGUEL-EST", fnt22, Brushes.Black, unite_largeur, 4 * unite_hauteur);
            graphic.DrawString("TEL : 22 51 04 57 - No_____/AL,CMC/" + DateTime.Now.Year, fnt22, Brushes.Black, unite_largeur, 5 * unite_hauteur);
            graphic.DrawString("Fax : 22 51 04 57 - No RCCM TC/NDJ/13 A 1398 ", fnt22, Brushes.Black, unite_largeur, 6 * unite_hauteur);
            graphic.DrawString("Email : Cliniquealshifa@yahoo.com ", fnt22, Brushes.Black, unite_largeur, 7 * unite_hauteur);
            graphic.DrawString("N'DJAMENA-TCHAD", fnt22, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            #endregion
            var unite = 30;
            graphic.FillRectangle(Brushes.Black, unite_largeur, 8 * unite + 13, 16 * unite_largeur, unite_hauteur*2);
            graphic.DrawString("N'djamena :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 7 * unite + 5);
            graphic.DrawString("Fiche du patient ", fnt3, Brushes.White, unite_largeur*4, 8 * unite+15);
            graphic.DrawString("Patient no :", fnt1, Brushes.Black, unite_largeur, 10 * unite);
            graphic.DrawString("Nom :", fnt1, Brushes.Black, unite_largeur, 11 * unite);
            graphic.DrawString("Prenom :", fnt1, Brushes.Black, unite_largeur, 12 * unite);
            graphic.DrawString("Sexe :   ", fnt1, Brushes.Black, unite_largeur, 13 * unite);
            graphic.DrawString("Age :    ", fnt1, Brushes.Black, unite_largeur, 14 * unite);
            graphic.DrawString("Téléphone :", fnt1, Brushes.Black, unite_largeur, 15 * unite);
            graphic.DrawString("Poids :   ", fnt1, Brushes.Black, unite_largeur, 16 * unite);
            graphic.DrawString("Température :    ", fnt1, Brushes.Black, unite_largeur, 17 * unite);
            graphic.DrawString("Tension :", fnt1, Brushes.Black, unite_largeur, 18 * unite);
            graphic.DrawString("Groupe sanguin/Rhésus :", fnt1, Brushes.Black, unite_largeur, 19 * unite);
            graphic.DrawString("Frais carnet :", fnt1, Brushes.Black, unite_largeur, 20 * unite);
            graphic.DrawString(patient.NumeroPatient.ToString(), fnt1, Brushes.Black, 8 * unite_largeur, 10 * unite);
            graphic.DrawString(patient.Nom, fnt1, Brushes.Black, 8 * unite_largeur, 11 * unite);
            graphic.DrawString(patient.Prenom, fnt1, Brushes.Black, 8 * unite_largeur, 12 * unite);
            graphic.DrawString(patient.Sexe, fnt1, Brushes.Black, 8 * unite_largeur, 13 * unite);
            graphic.DrawString(patient.An.ToString() , fnt1, Brushes.Black, 8 * unite_largeur, 14 * unite);
            graphic.DrawString(patient.Telephone, fnt1, Brushes.Black, 8 * unite_largeur, 15 * unite);
            //if (patient.Poids > 0)
            //{
            //    graphic.DrawString(patient.Poids.ToString() + " Kg", fnt1, Brushes.Black, 8 * unite_largeur, 16 * unite);
            //}
            //else
            //{
            //    graphic.DrawString("-", fnt1, Brushes.Black, 8 * unite_largeur, 16 * unite);
            //}
            //if (patient.Temperature > 0)
            //{
            //    graphic.DrawString(patient.Temperature.ToString() + "oC", fnt1, Brushes.Black, 8 * unite_largeur, 17* unite);
            //}
            //else
            //{
            //    graphic.DrawString("-", fnt1, Brushes.Black, 8 * unite_largeur, 17 * unite);
            //}
            //if (patient.Tension > 0)
            //{
            //    graphic.DrawString(patient.Tension.ToString(), fnt1, Brushes.Black, 8 * unite_largeur, 18 * unite);
            //}
            //else
            //{
            //    graphic.DrawString("-", fnt1, Brushes.Black, 8 * unite_largeur, 18 * unite);
            //}
            graphic.DrawString(patient.Rhesus, fnt1, Brushes.Black, 8 * unite_largeur, 19 * unite);
            //graphic.DrawString(patient.FraisCarnet.ToString(), fnt1, Brushes.Black, 8 * unite_largeur, 20 * unite);

return bitmap;
        }

        //fonction pour dessiner le recu de la facuter
        public static Bitmap FactureOfficielle(int numeroFacture , DataGridView dtGrid,
            Patient patient, string modePaiement, double remise, string caissier)
        {


            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 18;
                int unite_largeur = 32;
                int largeur_facture = 17 * unite_largeur + 10;
                int detail_hauteur_facture = 20 * unite_hauteur;
                int hauteur_facture = 45 * unite_hauteur + 5;

                //creer un bit map
                var bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                Image logo = global::GestionDuneClinique.Properties.Resources.logoFacture;
                graphic.DrawImage(logo, 10, 5, 17 * unite_largeur - 20, 6 * unite_hauteur + 5);

                logo = global::GestionDuneClinique.Properties.Resources.rectangleEntete;
                graphic.DrawImage(logo, 10, 8 * unite_hauteur + 5, 17 * unite_largeur - 28, unite_hauteur + 2);
                logo = global::GestionDuneClinique.Properties.Resources.rectangle;
                graphic.DrawImage(logo, 5, 9*unite_hauteur + 7, 17 * unite_largeur - 18, 4 * unite_hauteur + 10 );

                graphic.DrawImage(logo, 5, 14 * unite_hauteur + 5, 17 * unite_largeur - 18, unite_hauteur + 9);

                logo = global::GestionDuneClinique.Properties.Resources.rectangleEntete;
                graphic.DrawImage(logo, 10, 15 * unite_hauteur + 20, 11 * unite_largeur -18, unite_hauteur + 5);
                graphic.DrawImage(logo, 11*unite_largeur-10, 15 * unite_hauteur + 20,1 * unite_largeur+3 , unite_hauteur + 5);
                graphic.DrawImage(logo, 12 * unite_largeur-6, 15 * unite_hauteur + 20, 2 * unite_largeur, unite_hauteur + 5);
                graphic.DrawImage(logo, 14 * unite_largeur - 6, 15 * unite_hauteur + 20, 2 * unite_largeur + 17, unite_hauteur + 5);

                logo = global::GestionDuneClinique.Properties.Resources.rectangle1;
                graphic.DrawImage(logo, 10, 16 * unite_hauteur + 20, 11 * unite_largeur - 18, detail_hauteur_facture + 5);
                graphic.DrawImage(logo, 11 * unite_largeur - 10, 16 * unite_hauteur + 20, 1 * unite_largeur + 3, detail_hauteur_facture + 5);
                graphic.DrawImage(logo, 12 * unite_largeur - 6, 16 * unite_hauteur + 20, 2 * unite_largeur, detail_hauteur_facture + 5);
                graphic.DrawImage(logo, 14 * unite_largeur - 6, 16 * unite_hauteur + 20, 2 * unite_largeur + 17, detail_hauteur_facture + 5);

                logo = global::GestionDuneClinique.Properties.Resources.rectangleEntete;
                graphic.DrawImage(logo, 9, 36 * unite_hauteur + 14, 17 * unite_largeur - 26, unite_hauteur + 7);
                logo = global::GestionDuneClinique.Properties.Resources.rect;
                graphic.DrawImage(logo, 9*unite_largeur, 37* unite_hauteur + 21, 8 * unite_largeur - 22, unite_hauteur + 7);
                //definir les polices 
                Font fnt1 = new Font("Ubuntu", 9, FontStyle.Regular);
                Font fnt11 = new Font("Ubuntu", 11, FontStyle.Bold);
                Font fnt2 = new Font("Ubuntu", 15, FontStyle.Bold);
                Font fnt3 = new Font("Ubuntu", 14, FontStyle.Bold | FontStyle.Underline);
                Font fnt22 = new Font("Ubuntu", 10, FontStyle.Regular);

                #endregion
                
                var listeFacture = ConnectionClassClinique.ListeDesFactures(numeroFacture);
                graphic.DrawString("N'Djaména le, " + listeFacture[0].DateFacture, fnt1, Brushes.Black, 10, 7 * unite_hauteur);
                graphic.DrawString("FACTURE DE SOIN N° " + numeroFacture, fnt3, Brushes.Black, 5 * unite_largeur, 14 * unite_hauteur+4);
                graphic.DrawString("Date validation :  " + listeFacture[0].DateFacture.AddDays(15), fnt1, Brushes.Black, 9 * unite_largeur+16, 7 * unite_hauteur);
                //graphic.DrawRectangle(Pens.Black, 10, 8 * unite_hauteur, 17 * unite_largeur - 10, 5 * unite_hauteur + 10);
                //graphic.FillRectangle(Brushes.Black, 10, 8 * unite_hauteur, 17 * unite_largeur - 10, unite_hauteur);

                graphic.DrawString("Informations du patient  ", fnt11, Brushes.White, 6 * unite_largeur, 8 * unite_hauteur+5);
                graphic.DrawString("N° identifiant :", fnt1, Brushes.Black, 2 * unite_largeur, 9 * unite_hauteur + 10);
                graphic.DrawString("Patient :", fnt1, Brushes.Black, 2 * unite_largeur, 10 * unite_hauteur + 10);
                graphic.DrawString("Sexe :   ", fnt1, Brushes.Black, 2 * unite_largeur, 11 * unite_hauteur + 7);
                graphic.DrawString("Age :    ", fnt1, Brushes.Black, 2 * unite_largeur, 12 * unite_hauteur + 4);

                graphic.DrawString(patient.NumeroPatient.ToString(), fnt2, Brushes.Black, 5 * unite_largeur, 9 * unite_hauteur + 5);
                graphic.DrawString(patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(), fnt1, Brushes.Black, 5 * unite_largeur, 10 * unite_hauteur + 10);
                graphic.DrawString(patient.Sexe + "                         GS/RH :   " + patient.Rhesus + "     Adresse : " + patient.Adresse, fnt1, Brushes.Black, 5 * unite_largeur, 11 * unite_hauteur + 7);
                graphic.DrawString(AfficherAge(patient.An, patient.Mois) + "                Tél :   " + patient.Telephone + "            Fonction : " + patient.Fonction, fnt1, Brushes.Black, 5 * unite_largeur, 12 * unite_hauteur + 4);
                
                graphic.DrawString("Acte médical  ", fnt11, Brushes.White, 20, 15 * unite_hauteur+20);
                graphic.DrawString("Qté  ", fnt11, Brushes.White, 11 * unite_largeur - 8, 15 * unite_hauteur + 20);
                graphic.DrawString("P unit  ", fnt11, Brushes.White, 12 * unite_largeur+5, 15 * unite_hauteur + 20);
                graphic.DrawString("P Total  ", fnt11, Brushes.White, 14 * unite_largeur+0, 15 * unite_hauteur+20);

                var total = 0.0;
                for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
                {
                    var Yloc = unite_hauteur * i + 17 * unite_hauteur + 5;
                    graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper(), fnt1, Brushes.Black, 20, Yloc);
                    graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dtGrid.Rows[i].Cells[1].Value.ToString())), fnt1, Brushes.Black, 12 * unite_largeur+0, Yloc);
                    graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dtGrid.Rows[i].Cells[2].Value.ToString())), fnt1, Brushes.Black, 11 * unite_largeur - 2, Yloc);
                    graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dtGrid.Rows[i].Cells[3].Value.ToString())), fnt1, Brushes.Black, 14* unite_largeur+5, Yloc);
                    total += Double.Parse(dtGrid.Rows[i].Cells[3].Value.ToString());
                }
                var totalNet = total - remise;
                //graphic.FillRectangle(Brushes.White, 10, 37 * unite_hauteur + 5, 17 * unite_largeur, 15*unite_hauteur);
                //graphic.FillRectangle(Brushes.Black, 10, 37 * unite_hauteur + 10, 17 * unite_largeur - 10, unite_hauteur);
                graphic.DrawString("TOTAL ", fnt11, Brushes.White, 20, 36 * unite_hauteur + 15);
                graphic.DrawString(string.Format(elGR, "{0:0,0}", totalNet) + " F CFA", fnt11, Brushes.White, 13 * unite_largeur-10, 36 * unite_hauteur + 15);
                graphic.DrawString("Total remise :  ".ToUpper() + string.Format(elGR, "{0:0,0}", remise) + " FCFA", fnt11, Brushes.Black, 10 * unite_largeur - 25, 37 * unite_hauteur+23 );

                graphic.DrawString("Arrêtée la présente facture à la somme de : ........................................................................... ",
                    fnt22, Brushes.Black, 10, 39 * unite_hauteur + 15);
                graphic.DrawString(".......................................................................................................................................................... ",
                   fnt22, Brushes.Black, 10, 40 * unite_hauteur + 15);
                var lettre = Converti((long)totalNet) + " Francs CFA";
                if (lettre.Length > 40)
                {
                    graphic.DrawString(lettre.Substring(0, 40), fnt22, Brushes.Black, 8 * unite_largeur +28, 39 * unite_hauteur + 10);
                    graphic.DrawString(lettre.Substring(40), fnt22, Brushes.Black, unite_largeur, 40 * unite_hauteur + 10);
                }
                else
                {
                    graphic.DrawString(lettre, fnt22, Brushes.Black, 9 * unite_largeur + 5, 39 * unite_hauteur + 10);
                }
                graphic.DrawString("Caissier(e) :  ".ToUpper() + caissier.ToUpper(), fnt11, Brushes.Black, 6 * unite_largeur - 15, 42 * unite_hauteur + 5);
                return bitmap;
            }
            catch { return null; }
        }

        //fonction pour dessiner le recu de la facture analyse
        public static Bitmap ImprimerAnalyse(int numeroAnalyse, DataGridView dtGrid, Patient patient, DateTime dateAnalyse, string caissier)
        {

            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 17 * unite_largeur;
                int detail_hauteur_facture = 21 * unite_hauteur-10;
                int hauteur_facture = 40 * unite_hauteur;

                //creer un bit map
                var bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                
                Font fnt1 = new Font("Ubuntu", 9, FontStyle.Regular);
                Font fnt11 = new Font("Ubuntu", 11, FontStyle.Regular);
                Font fnt2 = new Font("Ubuntu", 11, FontStyle.Bold);
                Font fnt3 = new Font("Ubuntu", 15, FontStyle.Bold | FontStyle.Underline);
                Font fnt22 = new Font("Ubuntu", 10, FontStyle.Regular);
                #endregion


                Image logo = global::GestionDuneClinique.Properties.Resources.logoFacture;
                graphic.DrawImage(logo, 10, 5, 17 * unite_largeur - 20, 4 * unite_hauteur + 5);

                logo = global::GestionDuneClinique.Properties.Resources.rectangleEntete;
                graphic.DrawImage(logo, 10, 5 * unite_hauteur + 5, 17 * unite_largeur - 28, unite_hauteur + 2);
                logo = global::GestionDuneClinique.Properties.Resources.rectangle;
                graphic.DrawImage(logo, 5, 6* unite_hauteur + 7, 17 * unite_largeur - 18, 3 * unite_hauteur + 25);

                graphic.DrawImage(logo, 5, 10 * unite_hauteur + 20, 17 * unite_largeur - 18, unite_hauteur + 9);

                logo = global::GestionDuneClinique.Properties.Resources.rectangleEntete;
                graphic.DrawImage(logo, 10, 13* unite_hauteur + 10, 11 * unite_largeur - 18, unite_hauteur + 5);
                graphic.DrawImage(logo, 11 * unite_largeur - 10, 13* unite_hauteur + 10, 1 * unite_largeur + 3, unite_hauteur + 5);
                graphic.DrawImage(logo, 12 * unite_largeur - 6, 13 * unite_hauteur + 10, 2 * unite_largeur, unite_hauteur + 5);
                graphic.DrawImage(logo, 14 * unite_largeur - 6, 13* unite_hauteur + 10, 2 * unite_largeur + 17, unite_hauteur + 5);

                logo = global::GestionDuneClinique.Properties.Resources.rectangle1;
                graphic.DrawImage(logo, 10, 14* unite_hauteur + 10, 11 * unite_largeur - 18, detail_hauteur_facture + 5);
                graphic.DrawImage(logo, 11 * unite_largeur - 10, 14 * unite_hauteur + 10, 1 * unite_largeur + 3, detail_hauteur_facture + 5);
                graphic.DrawImage(logo, 12 * unite_largeur - 6, 14* unite_hauteur + 10, 2 * unite_largeur, detail_hauteur_facture + 5);
                graphic.DrawImage(logo, 14 * unite_largeur - 6, 14 * unite_hauteur +10, 2 * unite_largeur + 17, detail_hauteur_facture + 5);

                logo = global::GestionDuneClinique.Properties.Resources.rectangleEntete;
                graphic.DrawImage(logo, 9, 35 * unite_hauteur -10, 17 * unite_largeur - 26, unite_hauteur + 7);
                logo = global::GestionDuneClinique.Properties.Resources.rect;
                //graphic.DrawImage(logo, 9 * unite_largeur, 37 * unite_hauteur + 21, 8 * unite_largeur - 22, unite_hauteur + 7);
                graphic.DrawString("Informations du patient  ".ToUpper(), fnt2, Brushes.White, 5 * unite_largeur, 5* unite_hauteur +4);
                graphic.DrawString("IDENTIFIANT : ", fnt22, Brushes.Black, 2 * unite_largeur,6 * unite_hauteur +8);
                graphic.DrawString("PATIENT : ", fnt22, Brushes.Black, 2 * unite_largeur, 7 * unite_hauteur + 8);
                graphic.DrawString("SEXE : ", fnt22, Brushes.Black, 2 * unite_largeur, 8 * unite_hauteur + 8);
                graphic.DrawString("AGE :      ", fnt22, Brushes.Black, 9* unite_largeur, 8 * unite_hauteur + 8);
                graphic.DrawString("CONVENTIONNE : ", fnt22, Brushes.Black, 2 * unite_largeur, 9 * unite_hauteur + 8);

                if (!string.IsNullOrWhiteSpace(patient.SousCouvert))
                {
                    graphic.DrawString(patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper() + " S/C " + patient.SousCouvert.ToUpper(),
                        fnt22, Brushes.Black, 6 * unite_largeur, 7 * unite_hauteur + 8);
                }
                else
                {
                    graphic.DrawString(patient.Nom.ToUpper() + " " + patient.Prenom.ToUpper(), fnt22, Brushes.Black, 6 * unite_largeur,7 * unite_hauteur + 8);
                }
                graphic.DrawString(patient.NumeroPatient.ToString().ToUpper(), fnt2, Brushes.Black, 6 * unite_largeur, 6 * unite_hauteur + 8);
                graphic.DrawString(patient.Sexe.ToUpper(), fnt22, Brushes.Black, 6 * unite_largeur, 8 * unite_hauteur + 8);
                graphic.DrawString(AfficherAge(patient.An, patient.Mois), fnt22, Brushes.Black, 11 * unite_largeur, 8 * unite_hauteur + 8);
                graphic.DrawString(patient.NomEntreprise.ToString(), fnt22, Brushes.Black, 6 * unite_largeur, 9 * unite_hauteur + 8);


                graphic.DrawString("N'DJAMENA LE  " + DateTime.Now.ToString(), fnt1, Brushes.Black, 12, 12 * unite_hauteur + 10);
                graphic.DrawString("BON D'EXAMEN No " + numeroAnalyse, fnt3, Brushes.Black, 5 * unite_largeur, 10 * unite_hauteur +20);


                graphic.DrawString("Acte médical  ", fnt2, Brushes.White, 20, 13 * unite_hauteur + 10);
                graphic.DrawString("Qté  ", fnt2, Brushes.White, 11 * unite_largeur - 8, 13 * unite_hauteur + 10);
                graphic.DrawString("P unit  ", fnt2, Brushes.White, 12 * unite_largeur + 5, 13 * unite_hauteur + 10);
                graphic.DrawString("P Total  ", fnt2, Brushes.White, 14 * unite_largeur + 0, 13* unite_hauteur + 10);

                var total = 0.0;
                for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
                {
                    var Yloc = unite_hauteur * i + 14* unite_hauteur+15;
                    var typeExmen = dtGrid.Rows[i].Cells[1].Value.ToString();
                  
                    graphic.DrawString(typeExmen, fnt1, Brushes.Black, 20, Yloc);
                    graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dtGrid.Rows[i].Cells[2].Value.ToString())), fnt1, Brushes.Black, 12 * unite_largeur + 0, Yloc);
                    graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dtGrid.Rows[i].Cells[3].Value.ToString())), fnt1, Brushes.Black, 11 * unite_largeur - 2, Yloc);
                    graphic.DrawString(string.Format(elGR, "{0:0,0}", Double.Parse(dtGrid.Rows[i].Cells[4].Value.ToString())), fnt1, Brushes.Black, 14 * unite_largeur + 5, Yloc);

                    total += Double.Parse(dtGrid.Rows[i].Cells[4].Value.ToString());
                }
             
                graphic.DrawString("Total ", fnt2, Brushes.White, 15, 35 * unite_hauteur - 11);
                graphic.DrawString(string.Format(elGR, "{0:0,0}",total) + " F CFA", fnt2, Brushes.White, 13 * unite_largeur - 20, 35 * unite_hauteur - 11);

                graphic.DrawString("Arrêtée la présente facture des examens à la somme de : ", fnt1, Brushes.Black, unite_largeur, 36 * unite_hauteur -5);
                graphic.DrawString(Converti((long)total) + " Francs CFA", fnt1, Brushes.Black, unite_largeur, 37 * unite_hauteur -7);
                graphic.DrawString("Caissier(e)  :  ".ToUpper() + caissier.ToUpper(), fnt2, Brushes.Black, 4 * unite_largeur - 15, 38 * unite_hauteur -5);
                //graphic.DrawString( fnt2, Brushes.Black, 9 * unite_largeur, 37 * unite_hauteur + 20);


                return bitmap;
            }
            catch { return null; }
        }

        public static string Converti(long chiffre)
        {
            long centaine, dizaine, unite, reste, y;
            bool dix = false;
            string lettre = "";
            //strcpy(lettre, "");

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
        public static Bitmap RapportDuneEntreprise(DataGridView dtGrid, Entreprise entreprise, DateTime date, double montantTotal, double reduction)
        {
            Bitmap bitmap;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 25 * unite_largeur ;
                int hauteur_facture = 0;
                bool flag = false;
                if (dtGrid.Rows.Count <= 30)
                {
                    flag = true;
                    hauteur_facture = 55 * unite_hauteur ;
                }
                else
                {
                    flag = false;
                    hauteur_facture = 50 * unite_hauteur + 15;
                }
                
                //creer un bit map
                 bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
              
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo, unite_largeur,  unite_hauteur+5, 13 * unite_largeur, 8 * unite_hauteur);
                
                    if (entreprise.NomEntreprise.ToLower().Equals("nrc"))
                    {
                        logo = global::GestionDuneClinique.Properties.Resources.logoNRC1;
                    }
                    else
                    {
                        logo = global::GestionDuneClinique.Properties.Resources.logo1;

                    }
                    graphic.DrawImage(logo, 12 * unite_largeur, 1 * unite_hauteur, 140, 160);
              
                //definir les polices 
                Font fnt1 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt33 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt0 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt11 = new Font("Ubuntu", 10, FontStyle.Bold);
                Font fnt2 = new Font("Ubuntu", 14, FontStyle.Bold);
                Font fnt3 = new Font("Ubuntu", 18, FontStyle.Bold | FontStyle.Underline);
                Font fnt22 = new Font("Ubuntu", 11, FontStyle.Regular);

               #endregion

                 graphic.DrawString("Page 1", fnt0, Brushes.SteelBlue, 23*unite_largeur,  5);


               graphic.DrawString("FACTURE DU  " + date.ToShortDateString(), fnt3, Brushes.SteelBlue, 7* unite_largeur, 17 * unite_hauteur - 15);
               
                graphic.DrawString("Date d'émission :  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.SteelBlue, 19 * unite_largeur, 8 * unite_hauteur+20);

                graphic.DrawRectangle(Pens.SteelBlue, unite_largeur - 5, 13 * unite_hauteur + 10, 24 * unite_largeur, 3 * unite_hauteur - 5);
                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 12 * unite_hauteur + 13, 24 * unite_largeur + 2, unite_hauteur);
                graphic.DrawString("Information de la convention  ".ToUpper(), fnt1, Brushes.White, 2 * unite_largeur, 12 * unite_hauteur + 13);
                graphic.DrawString("Nom :", fnt1, Brushes.SteelBlue, 2 * unite_largeur, 13 * unite_hauteur + 12);
                graphic.DrawString("Tél :   ", fnt1, Brushes.SteelBlue, 2 * unite_largeur, 14 * unite_hauteur + 9);
                graphic.DrawString("Adresse :    ", fnt1, Brushes.SteelBlue, 2 * unite_largeur, 15 * unite_hauteur + 6);

                graphic.DrawString(entreprise.NomEntreprise, fnt1, Brushes.SteelBlue, 6 * unite_largeur, 13 * unite_hauteur + 12);
                graphic.DrawString(entreprise.Telephone1 + " / " + entreprise.Telephone2, fnt1, Brushes.SteelBlue, 6 * unite_largeur, 14 * unite_hauteur + 9);
                graphic.DrawString(entreprise.Adresse, fnt1, Brushes.SteelBlue, 6 * unite_largeur, 15 * unite_hauteur + 6);

                //graphic.DrawString("Page 1 ", fnt11, Brushes.SteelBlue, 12 * unite_largeur, 5);
                //graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 12 * unite_hauteur + 13, 24 * unite_largeur + 2, unite_hauteur);
                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 3, 19 * unite_hauteur - 15, 24 * unite_largeur + 6, unite_hauteur);
                graphic.DrawString("Nom & Prenom  ".ToUpper(), fnt1, Brushes.White, unite_largeur, 19 * unite_hauteur - 15);
                graphic.DrawString("Date  ".ToUpper(), fnt1, Brushes.White, 8 * unite_largeur, 19 * unite_hauteur - 15);
                graphic.DrawString("Désignation ".ToUpper(), fnt1, Brushes.White, 12 * unite_largeur, 19 * unite_hauteur - 15);
                graphic.DrawString("Qté ".ToUpper(), fnt1, Brushes.White, 20 * unite_largeur, 19 * unite_hauteur - 15);
                graphic.DrawString("P unit ".ToUpper(), fnt1, Brushes.White, 21 * unite_largeur, 19 * unite_hauteur - 15);
                graphic.DrawString("Montant  ".ToUpper(), fnt1, Brushes.White, 23 * unite_largeur - 6, 19 * unite_hauteur - 15);

                for (var i = 0; i < dtGrid.Rows.Count - 1; i++)
                {

                    var Yloc = unite_hauteur * i + 20 * unite_hauteur;
                    var patient = dtGrid.Rows[i].Cells[1].Value.ToString().ToUpper();
                   
                    if (patient.ToLower().Contains("s/c"))
                    {
                      var listeEmp = ConnectionClassClinique.ListeDesEmployeesEntreprise(patient.Substring(0, patient.IndexOf("/") - 1));
                        var td = "";
                        if (listeEmp.Count > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(listeEmp[0].Telephone))
                            {
                                td = " / " + listeEmp[0].Telephone;
                            }
                        }

                        patient = patient.Substring(0, patient.IndexOf("/") - 1) +
                            "\n " + patient.Substring(patient.IndexOf("/") - 1) +td ;
                    }
                    else
                    {
                        //var listeEmp = ConnectionClassClinique.ListeDesEmployeesEntreprise(patient);
                        //var td = "";
                        //if (listeEmp.Count > 0)
                        //{
                        //    if (!string.IsNullOrWhiteSpace(listeEmp[0].Telephone))
                        //    {
                        //        td = " / " + listeEmp[0].Telephone;
                        //    }
                        //}
                        //patient = patient ;
                    }
                    if (patient.Length > 45)
                        patient = patient.Substring(0, 45);
                    graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur - 5, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.SteelBlue, 8 * unite_largeur, Yloc);
                    if (dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper() == "SOUS TOTAL")
                    {
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.SteelBlue, 23 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper(), fnt11, Brushes.SteelBlue, 11 * unite_largeur, Yloc);
                    }
                    else
                    {
                        if (dtGrid.Rows[i].Cells[3].Value.ToString().Length > 33)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().Substring(0, 33).ToUpper() + "...",
                                fnt0, Brushes.SteelBlue, 11 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper(), fnt0,
                                Brushes.SteelBlue, 11 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.SteelBlue, 23 * unite_largeur, Yloc);
                    }
                    graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.SteelBlue, 20 * unite_largeur, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.SteelBlue, 21 * unite_largeur, Yloc);
                }
                graphic.FillRectangle(Brushes.White, 0, 49*unite_hauteur+15, 24 * unite_largeur + 10, unite_hauteur *8);
                var Kloc = 50 * unite_hauteur;
                if (flag == true)
                {
                    if (reduction <= 0)
                    {
                        graphic.FillRectangle(Brushes.SteelBlue, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 8);
                        graphic.DrawString("Total ", fnt2, Brushes.White, unite_largeur, Kloc);
                        graphic.DrawString(montantTotal.ToString() + " F CFA", fnt2, Brushes.White, 17 * unite_largeur, Kloc);
                        graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 20, Kloc + 2 * unite_hauteur);
                        graphic.DrawString(Converti((long)montantTotal) + " Francs CFA", fnt1, Brushes.SteelBlue, 10 * unite_largeur-10, Kloc + 2 * unite_hauteur);

                    }
                    else
                    {
                        var montant = montantTotal - reduction;
                        graphic.FillRectangle(Brushes.SteelBlue, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 8);
                        graphic.DrawString("Total  :  "+montantTotal+"F CFA", fnt2, Brushes.White, unite_largeur, Kloc);
                        graphic.DrawString("Total réduction :  " + reduction .ToString() + " F CFA", fnt2, Brushes.White, 10 * unite_largeur, Kloc);
                        graphic.DrawString("Net Total :  " + montant.ToString() + " F CFA", fnt2, Brushes.White, 19 * unite_largeur, Kloc);
                        graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 20, Kloc + 2 * unite_hauteur);
                        graphic.DrawString(Converti((long)montant) + " Francs CFA", fnt1, Brushes.SteelBlue, 10 * unite_largeur - 10, Kloc + 2 * unite_hauteur);
                  
                    }
                    graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                    graphic.DrawString(GestionAcademique.LoginFrm.nom.ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("I;pression ", exception);
                return null;
            }
            return bitmap;
        }

        public static Bitmap RapportDuneEntrepriseParPage(DataGridView dtGrid, Entreprise entreprise, DateTime date, double montantTotal,double reduction, int start)
        {
            Bitmap bitmap;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 25 * unite_largeur;
                //int hauteur_facture = 31 * unite_hauteur;//+ 15 + dtGrid.Rows.Count * unite_hauteur;

                int hauteur_facture = 0;
                bool flag = false;
                if (dtGrid.Rows.Count <= 30 + 45 * (1 + start))
                {
                    hauteur_facture = 55 * unite_hauteur + 15;
                    flag = true;
                }
                else
                {
                    hauteur_facture = 48 * unite_hauteur + 15;
                    flag = false;
                }

                //creer un bit map
                bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);

                //definir les polices 
                Font fnt1 = new Font("Century Gothic", 10, FontStyle.Regular);
                Font fnt33 = new Font("Century Gothic", 10, FontStyle.Regular);
                Font fnt0 = new Font("Century Gothic", 10, FontStyle.Regular);
                Font fnt11 = new Font("Century Gothic", 10, FontStyle.Bold);
                Font fnt2 = new Font("Bodoni MT", 14, FontStyle.Bold);
                Font fnt3 = new Font("Century Gothic", 18, FontStyle.Bold);
                Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);

                #endregion

                graphic.DrawString("Page " + (start + 2).ToString(), fnt0, Brushes.SteelBlue, 23 * unite_largeur, 10);

                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 3, 2 * unite_hauteur - 10, 24 * unite_largeur + 6, unite_hauteur);
                graphic.DrawString("Nom & Prenom  ".ToUpper(), fnt1, Brushes.White, unite_largeur, 2 * unite_hauteur - 10);
                graphic.DrawString("Date  ".ToUpper(), fnt1, Brushes.White, 8 * unite_largeur, 2 * unite_hauteur - 10);
                graphic.DrawString("Désignation ".ToUpper(), fnt1, Brushes.White, 11 * unite_largeur, 2 * unite_hauteur - 10);
                graphic.DrawString("Qté ".ToUpper(), fnt1, Brushes.White, 20 * unite_largeur, 2 * unite_hauteur - 10);
                graphic.DrawString("P unit ".ToUpper(), fnt1, Brushes.White, 21 * unite_largeur, 2 * unite_hauteur - 10);
                graphic.DrawString("Montant  ".ToUpper(), fnt1, Brushes.White, 23 * unite_largeur-5, 2 * unite_hauteur - 10);

                var j = 0;
                for (var i = 30 + start * 45; i < dtGrid.Rows.Count - 1; i++)
                {

                    var Yloc = unite_hauteur * j + 3 * unite_hauteur;
                    var patient = dtGrid.Rows[i].Cells[1].Value.ToString().ToUpper();
                    if (patient.ToLower().Contains("s/c"))
                    {
                        patient = patient.Substring(0, patient.IndexOf("/") - 1) +
                            "\n " + patient.Substring(patient.IndexOf("/") - 1);
                    }
                    else
                    {
                    }
                    if (patient.Length > 45)
                        patient = patient.Substring(0, 45);
                    graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur - 5, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.SteelBlue, 8 * unite_largeur, Yloc);

                    if (dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper() == "SOUS TOTAL")
                    {
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.SteelBlue, 23 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper(), fnt11, Brushes.SteelBlue, 11 * unite_largeur, Yloc);
                    }
                    else
                    {
                        if (dtGrid.Rows[i].Cells[3].Value.ToString().Length > 35)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().Substring(0, 30).ToUpper() + "...",
                                fnt0, Brushes.SteelBlue, 11 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper(), fnt0,
                                Brushes.SteelBlue, 11 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.SteelBlue, 23 * unite_largeur, Yloc);
                    }

                    graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.SteelBlue, 20 * unite_largeur, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.SteelBlue, 21 * unite_largeur, Yloc);
                    j++;
                }
                graphic.FillRectangle(Brushes.White, 10, 47*unite_hauteur+15, 24 * unite_largeur + 20, unite_hauteur + 8);
                var Kloc = 50 * unite_hauteur;
                if (flag == true)
                {
                    if (reduction <= 0)
                    {
                        graphic.FillRectangle(Brushes.SteelBlue, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 8);
                        graphic.DrawString("Total ", fnt2, Brushes.White, unite_largeur, Kloc);
                        graphic.DrawString(montantTotal.ToString() + " F CFA", fnt2, Brushes.White, 19 * unite_largeur, Kloc);
                        graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 20, Kloc + 2 * unite_hauteur);
                        graphic.DrawString(Converti((long)montantTotal) + " Francs CFA", fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 2 * unite_hauteur);

                    }
                    else
                    {
                        var montant = montantTotal - reduction;
                        graphic.FillRectangle(Brushes.SlateGray, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 8);
                        graphic.DrawString("Total  :  " + montantTotal + "F CFA", fnt2, Brushes.White, unite_largeur, Kloc);
                        graphic.DrawString("Total réduction :  " + reduction.ToString() + " F CFA", fnt2, Brushes.White, 9 * unite_largeur, Kloc);
                        graphic.DrawString("Net Total :  " + montant.ToString() + " F CFA", fnt2, Brushes.White, 18 * unite_largeur, Kloc);
                        graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 19, Kloc + 2 * unite_hauteur);
                        graphic.DrawString(Converti((long)montant) + " Francs CFA", fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 2 * unite_hauteur);

                    } 
                    graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                    //graphic.DrawString(GestionAcademique.LoginFrm.nom.ToUpper(), fnt1, Brushes.Black, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);
                }

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Impression ", exception);
                return null;
            }
            return bitmap;
        }

        /********************************************************************/
      
        /********************************************************************/
        public static Bitmap RapportDuneEntrepriseAvecCharge(DataGridView dtGrid, Entreprise entreprise, DateTime date, double montantTotal,double charge)
        {
            Bitmap bitmap;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 35 * unite_largeur+3;
                int hauteur_facture = 33 * unite_hauteur;//+ 15 + dtGrid.Rows.Count * unite_hauteur;

               if(dtGrid.Rows.Count<=11)
               {
                   hauteur_facture = 38 * unite_hauteur;
               }

                //creer un bit map
                bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                try
                {
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo, unite_largeur,  unite_hauteur, 14* unite_largeur, 7* unite_hauteur);
                    if (entreprise.NomEntreprise.ToLower().Equals("nrc"))
                    {
                        logo = global::GestionDuneClinique.Properties.Resources.logoNRC1;
                    }
                    else
                    {
                        logo = global::GestionDuneClinique.Properties.Resources.logo1;

                    }
                    graphic.DrawImage(logo, 15 * unite_largeur, 1 * unite_hauteur, 140, 150);
              
                }
                catch { }
                //definir 
                //definir les polices 
                Font fnt1 = new Font("Ubuntu", 11, FontStyle.Regular);
                Font fnt33 = new Font("Ubuntu", 14, FontStyle.Regular);
                Font fnt0 = new Font("Ubuntu", 12, FontStyle.Regular);
                Font fnt11 = new Font("Ubuntu", 12, FontStyle.Bold);
                Font fnt2 = new Font("Ubuntu", 14, FontStyle.Bold);
                Font fnt3 = new Font("Ubuntu", 18, FontStyle.Bold | FontStyle.Underline);
                Font fnt22 = new Font("Ubuntu", 12, FontStyle.Regular);
                 #endregion

                graphic.DrawString("Page 1", fnt0, Brushes.Black, 32 * unite_largeur,  2);

                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 10 * unite_hauteur, 34 * unite_largeur, 2 * unite_hauteur);

                graphic.DrawString("FACTURE DU  " + date.ToShortDateString(), fnt3, Brushes.SteelBlue, 15 * unite_largeur, 17 * unite_hauteur - 15);
                //graphic.DrawLine(Pens.SteelBlue, 8 * unite_largeur, 18 * unite_hauteur - 10, 17 * unite_largeur, 18 * unite_hauteur - 10);
                graphic.DrawString("Date d'émission :  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.SteelBlue, 28 * unite_largeur, 9 * unite_hauteur);

                graphic.DrawRectangle(Pens.SteelBlue, unite_largeur - 5, 13 * unite_hauteur + 10, 34 * unite_largeur, 3 * unite_hauteur - 5);
                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 12 * unite_hauteur + 13, 34 * unite_largeur + 2, unite_hauteur);
                graphic.DrawString("Information de la convention  ".ToUpper(), fnt1, Brushes.White, 4 * unite_largeur, 12 * unite_hauteur + 13);
                graphic.DrawString("Nom :", fnt1, Brushes.SteelBlue, 4 * unite_largeur, 13 * unite_hauteur + 12);
                graphic.DrawString("Tél :   ", fnt1, Brushes.SteelBlue, 4 * unite_largeur, 14 * unite_hauteur + 9);
                graphic.DrawString("Adresse :    ", fnt1, Brushes.SteelBlue, 4 * unite_largeur, 15 * unite_hauteur + 6);

                graphic.DrawString(entreprise.NomEntreprise, fnt1, Brushes.SteelBlue, 8 * unite_largeur, 13 * unite_hauteur + 12);
                graphic.DrawString(entreprise.Telephone1 + " / " + entreprise.Telephone2, fnt1, Brushes.SteelBlue, 8 * unite_largeur, 14 * unite_hauteur + 9);
                graphic.DrawString(entreprise.Adresse, fnt1, Brushes.SteelBlue, 8 * unite_largeur, 15 * unite_hauteur + 6);

                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 3, 19 * unite_hauteur - 15, 34 * unite_largeur +4, 2 * unite_hauteur);
                graphic.DrawString("Nom & Prenom  ".ToUpper(), fnt1, Brushes.White,  unite_largeur, 19 * unite_hauteur - 15);
                graphic.DrawString("Date".ToUpper(), fnt1, Brushes.White, 9 * unite_largeur, 19 * unite_hauteur - 15);
                graphic.DrawString("Désignation ".ToUpper(), fnt1, Brushes.White, 12 * unite_largeur, 19 * unite_hauteur - 15);
                graphic.DrawString("Qté  ".ToUpper(), fnt1, Brushes.White, 21 * unite_largeur + 15, 19 * unite_hauteur - 15);
                graphic.DrawString("P unit ".ToUpper(), fnt1, Brushes.White, 23 * unite_largeur-7, 19 * unite_hauteur - 15);
                graphic.DrawString("Montant  ".ToUpper(), fnt1, Brushes.White, 26 * unite_largeur-17, 19 * unite_hauteur - 15);
                graphic.DrawString("Charge \nAssuré - ".ToUpper() + charge + "%", fnt1, Brushes.White, 29 * unite_largeur-25, 19 * unite_hauteur - 15);
                var chargeAssureur = 100- charge;
                graphic.DrawString("Charge \nAssureur - ".ToUpper() + chargeAssureur.ToString() + "%", fnt1, Brushes.White, 32 * unite_largeur-19, 19 * unite_hauteur - 15);

                for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
                {

                    var Yloc = unite_hauteur * i + 21 * unite_hauteur;
                    var patient = dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper();
                    if (patient.ToLower().Contains("s/c"))
                    {
                        patient = patient.Substring(0, patient.IndexOf("/") - 1) +
                            "\n " + patient.Substring(patient.IndexOf("/") - 1);
                    }
                    else
                    {
                    }

                    if (dtGrid.Rows[i].Cells[2].Value.ToString().Contains( "TOTAL"))
                    {
                        graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.SteelBlue, 9 * unite_largeur, Yloc);

                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 37)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 37).ToUpper(),
                                fnt11, Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.SteelBlue, 21 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 23 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt11, Brushes.SteelBlue, 26 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.SteelBlue, 29 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt11, Brushes.SteelBlue, 32 * unite_largeur, Yloc);
                    }
                    else
                    {
                        graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.SteelBlue, 9 * unite_largeur, Yloc);

                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 37)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 37).ToUpper(),
                                fnt0, Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt0,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.SteelBlue, 21 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.SteelBlue, 23 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.SteelBlue, 26 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.SteelBlue, 29 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.SteelBlue, 32 * unite_largeur, Yloc);
                    }
                }

                var Kloc = 21 * unite_hauteur+ unite_hauteur* dtGrid.Rows.Count;
                if (dtGrid.Rows.Count<=11)
                {
                    graphic.FillRectangle(Brushes.SteelBlue, 20, Kloc, 34 * unite_largeur + 10, unite_hauteur + 5);
                    graphic.DrawString("Total assureur", fnt2, Brushes.White, 24 * unite_largeur, Kloc);
                    graphic.DrawString(montantTotal.ToString() + " F CFA", fnt2, Brushes.White, 29 * unite_largeur, Kloc);
                    graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 20, Kloc + 2 * unite_hauteur);
                    graphic.DrawString(Converti((long)montantTotal) + " Francs CFA", fnt1, Brushes.SteelBlue, 9 * unite_largeur + 15, Kloc + 2 * unite_hauteur);
                    graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                    graphic.DrawString(GestionAcademique.LoginFrm.nom.ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);

                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Impression ", exception);
                return null;
            }
            return bitmap;
        }

        /********************************************************************/
        public static Bitmap RapportDuneEntrepriseParPageAvecCharge(DataGridView dtGrid, double montantTotal, int start,double charge)
        {
            Bitmap bitmap;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 25;
                int unite_largeur = 32;
                int largeur_facture = 35 * unite_largeur+3;
                //int hauteur_facture = 31 * unite_hauteur;//+ 15 + dtGrid.Rows.Count * unite_hauteur;

                int hauteur_facture = 0;
                bool flag = false;
                if (dtGrid.Rows.Count <= 11 + 23 * (1 + start))
                {
                    hauteur_facture = 37 * unite_hauteur;
                    flag = true;
                }
                else
                {
                    hauteur_facture += 26 * unite_hauteur;
                    flag = false;
                }
                //creer un bit map
                bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);

                //definir les polices 
                Font fnt1 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt33 = new Font("Ubuntu Gothic", 11, FontStyle.Regular);
                Font fnt0 = new Font("Ubuntu Gothic", 10, FontStyle.Regular);
                Font fnt11 = new Font("Ubuntu Gothic", 11, FontStyle.Bold);
                Font fnt2 = new Font("Ubuntu MT", 11, FontStyle.Bold);
                Font fnt3 = new Font("Ubuntu Gothic", 15, FontStyle.Bold);
                Font fnt22 = new Font("Ubuntu Gothic", 10, FontStyle.Regular);

                #endregion

                var page = start +1;
                graphic.DrawString("Page "+page, fnt0, Brushes.SteelBlue, 32 * unite_largeur, 15);

                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 3, unite_hauteur + 10, 34 * unite_largeur + 8, 2 * unite_hauteur - 15);
                graphic.DrawString("Date  ".ToUpper(), fnt1, Brushes.White, 9*unite_largeur, unite_hauteur + 10);
                graphic.DrawString("Nom & Prenom  ".ToUpper(), fnt1, Brushes.White, unite_largeur, unite_hauteur + 10);
                graphic.DrawString("Désignation ".ToUpper(), fnt1, Brushes.White, 12 * unite_largeur, unite_hauteur + 10);
                graphic.DrawString("Qté  ".ToUpper(), fnt1, Brushes.White, 21 * unite_largeur+15, unite_hauteur + 10);
                graphic.DrawString("P unit ".ToUpper(), fnt1, Brushes.White, 23 * unite_largeur, unite_hauteur + 10);
                graphic.DrawString("Montant  ".ToUpper(), fnt1, Brushes.White, 26 * unite_largeur-22, unite_hauteur + 10);
                graphic.DrawString("Charge \nAssuré - ".ToUpper() + charge + "%", fnt1, Brushes.White, 29 * unite_largeur-24, unite_hauteur + 10);
                var chargeAssureur = 100 - charge;
                graphic.DrawString("Charge \nAssureur - ".ToUpper() + chargeAssureur.ToString() + "%", fnt1, Brushes.White, 32 * unite_largeur-19, unite_hauteur + 10);

                var j = 0;
                for (var i = 11 + start * 23; i <= dtGrid.Rows.Count - 1; i++)
                {

                    var Yloc = unite_hauteur * j + 3 * unite_hauteur+10;
                    var patient = dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper();
                    if (patient.ToLower().Contains("s/c"))
                    {
                        patient = patient.Substring(0, patient.IndexOf("/") - 1) +
                            "\n " + patient.Substring(patient.IndexOf("/") - 1);
                    }
                    else
                    {
                    }
                    graphic.DrawString(patient, fnt0, Brushes.SteelBlue,  unite_largeur - 5, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.SteelBlue, 9*unite_largeur, Yloc);

                    if (dtGrid.Rows[i].Cells[2].Value.ToString().Contains("TOTAL"))
                    {
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt11, Brushes.SteelBlue, 32 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11,
                                         Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.SteelBlue, 21 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 23 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt11, Brushes.SteelBlue, 26 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.SteelBlue, 29 * unite_largeur, Yloc);
                                       }
                    else
                    {
                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 40)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 40).ToUpper(),
                                fnt0, Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt0,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.SteelBlue, 32 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.SteelBlue, 21 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.SteelBlue, 23 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.SteelBlue, 26 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.SteelBlue, 29 * unite_largeur, Yloc);
                    }
                    
                    //graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.SteelBlue, 32 * unite_largeur, Yloc);
                    j++;
                }
                

                var Kloc = 26 * unite_hauteur;
                if (flag == true)
                {
                    graphic.FillRectangle(Brushes.White, 20, Kloc, 34 * unite_largeur + 10, 5*unite_hauteur + 5);
                    graphic.FillRectangle(Brushes.SteelBlue, 20, Kloc, 34 * unite_largeur + 10, unite_hauteur + 5);
                    graphic.DrawString("Total assureur", fnt2, Brushes.White, 24 * unite_largeur, Kloc);
                    graphic.DrawString(montantTotal.ToString() + " F CFA", fnt2, Brushes.White, 29 * unite_largeur, Kloc);
                    graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 20, Kloc + 2 * unite_hauteur);
                    graphic.DrawString(Converti((long)montantTotal) + " Francs CFA", fnt1, Brushes.SteelBlue, 10 * unite_largeur + 25, Kloc + 2 * unite_hauteur);
                    graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                    //graphic.DrawString(GestionAcademique.LoginFrm.nom.ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);

                }

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("I;pression ", exception);
                return null;
            }
            return bitmap;
        }

        public static Bitmap RapportDuneEntrepriseAvecCharge(DataGridView dtGrid, Entreprise entreprise, DateTime date)
        {
            Bitmap bitmap;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 35 * unite_largeur + 3;
                int hauteur_facture = 32 * unite_hauteur;//+ 15 + dtGrid.Rows.Count * unite_hauteur;

                //creer un bit map
                bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                try
                {
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo, unite_largeur, unite_hauteur, 14 * unite_largeur, 7 * unite_hauteur);
                    if (entreprise.NomEntreprise.ToLower().Equals("nrc"))
                    {
                        logo = global::GestionDuneClinique.Properties.Resources.logoNRC1;
                    }
                    else
                    {
                        logo = global::GestionDuneClinique.Properties.Resources.logo1;

                    }
                    graphic.DrawImage(logo, 15 * unite_largeur, 1 * unite_hauteur, 160, 100);

                }
                catch { }
                //definir 
                //definir les polices 
                Font fnt1 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt33 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt0 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt11 = new Font("Ubuntu", 10, FontStyle.Bold);
                Font fnt2 = new Font("Ubuntu", 11, FontStyle.Bold);
                Font fnt3 = new Font("Ubuntu", 14, FontStyle.Bold | FontStyle.Underline);
                Font fnt22 = new Font("Ubuntu", 11, FontStyle.Regular);
                #endregion

                graphic.DrawString("Page 1", fnt0, Brushes.SteelBlue, 32 * unite_largeur, 5);

         
                graphic.DrawString("FACTURE DU  " + date.ToShortDateString(), fnt3, Brushes.SteelBlue, 16 * unite_largeur, 13 * unite_hauteur - 15);
                //graphic.DrawLine(Pens.SteelBlue, 8 * unite_largeur, 18 * unite_hauteur - 10, 17 * unite_largeur, 18 * unite_hauteur - 10);
                graphic.DrawString("Date d'émission :  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.SteelBlue, 28 * unite_largeur, 7 * unite_hauteur);

                graphic.DrawRectangle(Pens.SteelBlue, unite_largeur - 5, 9 * unite_hauteur + 10, 34 * unite_largeur, 3 * unite_hauteur - 5);
                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 8 * unite_hauteur + 8, 34 * unite_largeur + 2, unite_hauteur);
                graphic.DrawString("Information de la convention  ".ToUpper(), fnt1, Brushes.White, 4 * unite_largeur, 8 * unite_hauteur + 13);
                graphic.DrawString("Nom :", fnt1, Brushes.SteelBlue, 4 * unite_largeur, 9 * unite_hauteur + 12);
                graphic.DrawString("Tél :   ", fnt1, Brushes.SteelBlue, 4 * unite_largeur, 10 * unite_hauteur + 9);
                graphic.DrawString("Adresse :    ", fnt1, Brushes.SteelBlue, 4 * unite_largeur, 11 * unite_hauteur + 6);

                graphic.DrawString(entreprise.NomEntreprise, fnt1, Brushes.SteelBlue, 8 * unite_largeur, 9 * unite_hauteur + 12);
                graphic.DrawString(entreprise.Telephone1 + " / " + entreprise.Telephone2, fnt1, Brushes.SteelBlue, 10 * unite_largeur, 14 * unite_hauteur + 9);
                graphic.DrawString(entreprise.Adresse, fnt1, Brushes.SteelBlue, 8 * unite_largeur, 11 * unite_hauteur + 6);

                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 3, 15 * unite_hauteur - 15, 34 * unite_largeur + 4, 2 * unite_hauteur);
                graphic.DrawString("Nom & Prenom  ".ToUpper(), fnt11, Brushes.White, unite_largeur, 15 * unite_hauteur - 15);
                graphic.DrawString("Date".ToUpper(), fnt11, Brushes.White, 9 * unite_largeur, 15 * unite_hauteur - 15);
                graphic.DrawString("Désignation ".ToUpper(), fnt11, Brushes.White, 12 * unite_largeur, 15 * unite_hauteur - 15);
                graphic.DrawString(" Ass-Asr\n    (%)".ToUpper(), fnt11, Brushes.White, 23 * unite_largeur + 0, 15 * unite_hauteur - 15);
                graphic.DrawString("Qté  ".ToUpper(), fnt11, Brushes.White, 25 * unite_largeur +5, 15 * unite_hauteur - 15);
                graphic.DrawString("P. unit ".ToUpper(), fnt11, Brushes.White, 26 * unite_largeur +15, 15 * unite_hauteur - 15);
                graphic.DrawString("Total  ".ToUpper(), fnt11, Brushes.White, 28 * unite_largeur+15 , 15 * unite_hauteur - 15);
                graphic.DrawString("Total \nAssuré ".ToUpper() , fnt11, Brushes.White, 30 * unite_largeur+15, 15 * unite_hauteur - 15);
                graphic.DrawString("Total \nAssureur ".ToUpper() , fnt11, Brushes.White, 32 * unite_largeur+12 , 15 * unite_hauteur - 15);

                for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
                {

                    var Yloc = unite_hauteur * i + 17 * unite_hauteur;
                    var patient = dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper();
                    if (patient.ToLower().Contains("s/c"))
                    {
                        patient = patient.Substring(0, patient.IndexOf("/") - 1) +
                            "\n " + patient.Substring(patient.IndexOf("/") - 1);
                    }
                    else
                    {
                    }

                    if (dtGrid.Rows[i].Cells[2].Value.ToString().Equals("TOTAL"))
                    {
                        graphic.DrawLine(Pens.SteelBlue, 20, Yloc, 34 * unite_largeur + 10, Yloc);

                        graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur, Yloc+2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.SteelBlue, 9 * unite_largeur, Yloc+2);

                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 37)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 37).ToUpper(),
                                fnt11, Brushes.SteelBlue, 12 * unite_largeur, Yloc+2);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc+2);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.SteelBlue, 23 * unite_largeur + 0, Yloc+2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 25 * unite_largeur+5, Yloc+2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 26 * unite_largeur+15, Yloc+2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt11, Brushes.SteelBlue, 28 * unite_largeur+15, Yloc+2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.SteelBlue, 30 * unite_largeur+15, Yloc+2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt11, Brushes.SteelBlue, 32 * unite_largeur+15, Yloc+2);
                    }else if (dtGrid.Rows[i].Cells[2].Value.ToString().Equals("SOUS TOTAL"))
                    {
                        graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.SteelBlue, 9 * unite_largeur, Yloc);

                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 37)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 37).ToUpper(),
                                fnt11, Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.SteelBlue, 23 * unite_largeur + 0, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 25 * unite_largeur + 5, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 26 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt11, Brushes.SteelBlue, 28 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.SteelBlue, 30 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt11, Brushes.SteelBlue, 32 * unite_largeur + 15, Yloc);
                    }
                    else
                    {
                        graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.SteelBlue, 9 * unite_largeur, Yloc);

                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 37)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 37).ToUpper(),
                                fnt0, Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt0,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        var txAssure = double.Parse(dtGrid.Rows[i].Cells[6].Value.ToString()) * 100 / double.Parse(dtGrid.Rows[i].Cells[5].Value.ToString());
                        var txAssureur = double.Parse(dtGrid.Rows[i].Cells[7].Value.ToString()) * 100 / double.Parse(dtGrid.Rows[i].Cells[5].Value.ToString());
  

                        graphic.DrawString(txAssure +"-"+txAssureur, fnt1, Brushes.SteelBlue, 23 * unite_largeur + 0, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.SteelBlue, 25 * unite_largeur + 5, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.SteelBlue, 26 * unite_largeur+15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.SteelBlue, 28 * unite_largeur+15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.SteelBlue, 30 * unite_largeur+15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.SteelBlue, 32 * unite_largeur+15, Yloc);
                    }
                }

                var Kloc = 30 * unite_hauteur;
                graphic.FillRectangle(Brushes.White, 0, Kloc, 35* unite_largeur , 5 * unite_hauteur + 5);
                if (dtGrid.Rows.Count <= 12)
                {
                 
                    graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 2 * unite_hauteur + 7);
                    //graphic.DrawString(GestionAcademique.LoginFrm.nom.ToUpper(), fnt1, Brushes.SteelBlue, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 5);

                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Impression ", exception);
                return null;
            }
            return bitmap;
        }

        public static Bitmap RapportDuneEntrepriseParPageAvecCharge(DataGridView dtGrid, int start)
        {
            Bitmap bitmap;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 35 * unite_largeur + 3;
                 int  hauteur_facture = 32* unite_hauteur;
                
                //creer un bit map
                bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);

                //definir les polices 
                Font fnt1 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt33 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt0 = new Font("Ubuntu", 10, FontStyle.Regular);
                Font fnt11 = new Font("Ubuntu", 10, FontStyle.Bold);
                Font fnt2 = new Font("Ubuntu", 11, FontStyle.Bold);
                Font fnt3 = new Font("Ubuntu", 14, FontStyle.Bold | FontStyle.Underline);
                Font fnt22 = new Font("Ubuntu", 11, FontStyle.Regular);

                #endregion

                var page = start + 2;
                graphic.DrawString("Page " + page, fnt0, Brushes.SteelBlue, 32 * unite_largeur, 15);

                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 3, unite_hauteur + 10, 34 * unite_largeur + 8, 2 * unite_hauteur );
                graphic.DrawString("Nom & Prenom  ".ToUpper(), fnt11, Brushes.White, unite_largeur,  unite_hauteur +15);
                graphic.DrawString("Date".ToUpper(), fnt11, Brushes.White, 9 * unite_largeur,  unite_hauteur +15);
                graphic.DrawString("Désignation ".ToUpper(), fnt11, Brushes.White, 12 * unite_largeur,  unite_hauteur +15);
                graphic.DrawString(" Ass-Asr\n    (%)".ToUpper(), fnt11, Brushes.White, 23 * unite_largeur + 0,  unite_hauteur +15);
                graphic.DrawString("Qté  ".ToUpper(), fnt11, Brushes.White, 25 * unite_largeur + 5,  unite_hauteur + 15);
                graphic.DrawString("P. unit ".ToUpper(), fnt11, Brushes.White, 26 * unite_largeur + 15,  unite_hauteur + 15);
                graphic.DrawString("Total  ".ToUpper(), fnt11, Brushes.White, 28 * unite_largeur + 15,  unite_hauteur + 15);
                graphic.DrawString("Total \nAssuré ".ToUpper(), fnt11, Brushes.White, 30 * unite_largeur + 15,unite_hauteur +15);
                graphic.DrawString("Total \nAssureur ".ToUpper(), fnt11, Brushes.White, 32 * unite_largeur + 12, unite_hauteur +15);

                var j = 0;
                for (var i = 13 + start * 23; i <= dtGrid.Rows.Count - 1; i++)
                {

                    var Yloc = unite_hauteur * j + 3 * unite_hauteur + 10;
                    var patient = dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper();
                    if (patient.ToLower().Contains("s/c"))
                    {
                        patient = patient.Substring(0, patient.IndexOf("/") - 1) +
                            "\n " + patient.Substring(patient.IndexOf("/") - 1);
                    }
                    else
                    {
                    }

                    if (dtGrid.Rows[i].Cells[2].Value.ToString().Equals("TOTAL"))
                    {
                        graphic.DrawLine(Pens.SteelBlue, 20, Yloc, 34 * unite_largeur + 10, Yloc);

                        graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur, Yloc + 2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.SteelBlue, 9 * unite_largeur, Yloc + 2);

                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 37)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 37).ToUpper(),
                                fnt11, Brushes.SteelBlue, 12 * unite_largeur, Yloc + 2);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc + 2);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.SteelBlue, 23 * unite_largeur + 0, Yloc + 2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 25 * unite_largeur + 5, Yloc + 2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 26 * unite_largeur + 15, Yloc + 2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt11, Brushes.SteelBlue, 28 * unite_largeur + 15, Yloc + 2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.SteelBlue, 30 * unite_largeur + 15, Yloc + 2);
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt11, Brushes.SteelBlue, 32 * unite_largeur + 15, Yloc + 2);
                    }
                    else if (dtGrid.Rows[i].Cells[2].Value.ToString().Equals("SOUS TOTAL"))
                    {
                        graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.SteelBlue, 9 * unite_largeur, Yloc);

                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 37)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 37).ToUpper(),
                                fnt11, Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt11,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.SteelBlue, 23 * unite_largeur + 0, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 25 * unite_largeur + 5, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.SteelBlue, 26 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt11, Brushes.SteelBlue, 28 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.SteelBlue, 30 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt11, Brushes.SteelBlue, 32 * unite_largeur + 15, Yloc);
                    }
                    else
                    {
                        graphic.DrawString(patient, fnt0, Brushes.SteelBlue, unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.SteelBlue, 9 * unite_largeur, Yloc);

                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 37)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 37).ToUpper(),
                                fnt0, Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt0,
                                Brushes.SteelBlue, 12 * unite_largeur, Yloc);
                        }
                        var txAssure = double.Parse(dtGrid.Rows[i].Cells[6].Value.ToString()) * 100 / double.Parse(dtGrid.Rows[i].Cells[5].Value.ToString());
                        var txAssureur = double.Parse(dtGrid.Rows[i].Cells[7].Value.ToString()) * 100 / double.Parse(dtGrid.Rows[i].Cells[5].Value.ToString());


                        graphic.DrawString(txAssure + "-" + txAssureur, fnt1, Brushes.SteelBlue, 23 * unite_largeur + 0, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.SteelBlue, 25 * unite_largeur + 5, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.SteelBlue, 26 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.SteelBlue, 28 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.SteelBlue, 30 * unite_largeur + 15, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.SteelBlue, 32 * unite_largeur + 15, Yloc);
                    }
                    j++;
                }

                var ind = dtGrid.Rows.Count - 11 - 23 * (0 + start);
                var Kloc = 3 * unite_hauteur + 10 + ind*unite_hauteur;
                graphic.FillRectangle(Brushes.White, 20, Kloc, 34 * unite_largeur + 10, 5 * unite_hauteur + 5);
                if (dtGrid.Rows.Count <= 11 + 23 * (1 + start))
                {
                    //graphic.FillRectangle(Brushes.White, 20, Kloc, 34 * unite_largeur + 10, 5 * unite_hauteur + 5);
                    //graphic.DrawLine(Pens.SteelBlue, 20, Kloc, 34 * unite_largeur + 10, Kloc);
                       //graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, 20, Kloc + 2 * unite_hauteur);
                    graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt11, Brushes.SteelBlue, 13 * unite_largeur, Kloc + 0 * unite_hauteur + 7);
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("I;pression ", exception);
                return null;
            }
            return bitmap;
        }

        public static Bitmap FactureOfficielleDuneEntreprise(Entreprise entrep, Entreprise entreprise)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 14 * unite_hauteur;
            int hauteur_facture = 42 * unite_hauteur + detail_hauteur_facture;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 2 * unite_hauteur, 24 * unite_largeur, 6 * unite_hauteur);
            }
            catch { }
            //definir 
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 18, FontStyle.Bold);
            Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);

            #endregion

            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 10 * unite_hauteur, 24 * unite_largeur, 2 * unite_hauteur);

            graphic.DrawRectangle(Pens.Black, unite_largeur - 5, 13 * unite_hauteur + 10, 24 * unite_largeur, 4 * unite_hauteur + 5);
            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 12 * unite_hauteur + 13, 24 * unite_largeur + 2, unite_hauteur);
            graphic.DrawString("Information de la convention  ", fnt1, Brushes.White, 4 * unite_largeur, 12 * unite_hauteur + 13);
            graphic.DrawString("Nom :", fnt1, Brushes.Black, 4 * unite_largeur, 13 * unite_hauteur + 12);
            graphic.DrawString("Tél :   ", fnt1, Brushes.Black, 4 * unite_largeur, 14 * unite_hauteur + 9);
            graphic.DrawString("Email :    ", fnt1, Brushes.Black, 4 * unite_largeur, 15 * unite_hauteur + 6);
            graphic.DrawString("Adresse :    ", fnt1, Brushes.Black, 4 * unite_largeur, 16 * unite_hauteur + 3);

            graphic.DrawString(entreprise.NomEntreprise, fnt1, Brushes.Black, 8 * unite_largeur, 13 * unite_hauteur + 12);
            graphic.DrawString(entreprise.Telephone1 + " / " + entreprise.Telephone2, fnt1, Brushes.Black, 8 * unite_largeur, 14 * unite_hauteur + 9);
            graphic.DrawString(entreprise.Email, fnt1, Brushes.Black, 8 * unite_largeur, 15 * unite_hauteur + 6);
            graphic.DrawString(entreprise.Adresse, fnt1, Brushes.Black, 8 * unite_largeur, 16 * unite_hauteur + 3);

            graphic.DrawString("REÇU DE PAIEMENT", fnt3, Brushes.Black, 9 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString("Date d'émission :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 19 * unite_hauteur + 15);
            graphic.DrawLine(Pens.Black, 9 * unite_largeur - 2, 24 * unite_hauteur - 7, 16 * unite_largeur, 24 * unite_hauteur - 7);
            graphic.DrawString("Mode de paiement : ", fnt1, Brushes.Black, 2 * unite_largeur, 28 * unite_hauteur);
            graphic.DrawString("Montant de paiement : ", fnt1, Brushes.Black, 2 * unite_largeur, 29 * unite_hauteur);
            graphic.DrawString(entrep.ModePaiement, fnt1, Brushes.Black, 12 * unite_largeur, 28 * unite_hauteur);
            graphic.DrawString(entrep.Montant.ToString() + " F CFA", fnt1, Brushes.Black, 12 * unite_largeur, 29 * unite_hauteur);
            //graphic.DrawString(total.ToString() + " F CFA", fnt1, Brushes.White, 3 * unite_largeur, 50 * unite_hauteur);
            //graphic.DrawString("payé ", fnt1, Brushes.White, 9 * unite_largeur, 50 * unite_hauteur);
            //graphic.DrawString(montantPaye.ToString() + " F CFA", fnt1, Brushes.White, 11 * unite_largeur, 50 * unite_hauteur);
            //graphic.DrawString("Reste ", fnt1, Brushes.White, 18 * unite_largeur, 50 * unite_hauteur);
            //graphic.DrawString(reste.ToString() + " F CFA", fnt1, Brushes.White, 20 * unite_largeur, 50 * unite_hauteur);
            //graphic.FillRectangle(Brushes.Black, 10, 50 * unite_hauteur, 24 * unite_largeur + 10, unite_hauteur + 3);

            graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.Black, 7 * unite_largeur, 50 * unite_hauteur + 12);
            graphic.DrawString(GestionAcademique.LoginFrm.nom.ToUpper(), fnt1, Brushes.Black, 9 * unite_largeur, 51 * unite_hauteur + 20);
            graphic.DrawLine(Pens.Black, 7 * unite_largeur - 2, 52 * unite_hauteur - 7, 16 * unite_largeur, 52 * unite_hauteur - 7);

            return bitmap;
        }

        //fonction pour dessiner le recu de la facture consultation
        public static Bitmap FactureEmployeDuneEntreprise(string titre, Patient patient, string entreprise, decimal montantTotal, string caissier, DataGridView dtGrid, int index)
        {
            Bitmap bitmap = null;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 25 * unite_largeur;
                int detail_hauteur_facture = 14 * unite_hauteur;

                int hauteur_facture;
                var flag = false;
                if (dtGrid.Rows.Count <= (1+index)*24)
                {
                    hauteur_facture = 56 * unite_hauteur;
                    flag = true ;
                }
                else
                {
                    hauteur_facture = 48 * unite_hauteur;
                    flag = false;
                }
                //creer un bit map
                bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                
                //definir les polices 
                Font fnt1 = new Font("Ubuntu", 12, FontStyle.Regular);
                Font fnt11 = new Font("Ubuntu", 8, FontStyle.Regular);
                Font fnt2 = new Font("Ubuntu", 28, FontStyle.Bold);
                Font fnt3 = new Font("Ubuntu", 20, FontStyle.Bold);
                Font fnt22 = new Font("Ubuntu", 12, FontStyle.Bold);

                try
                {
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo, unite_largeur, 2 * unite_hauteur, 13 * unite_largeur, 7 * unite_hauteur);

                    if (entreprise.ToLower().Equals("nrc"))
                    {
                        logo = global::GestionDuneClinique.Properties.Resources.logoNRC1;
                    }
                    else
                    {
                        logo = global::GestionDuneClinique.Properties.Resources.logo1;

                    }
                    graphic.DrawImage(logo, 12 * unite_largeur, 1 * unite_hauteur, 140, 180);
                }
                catch { }
                //definir 
                #endregion

                graphic.DrawString(titre, fnt3, Brushes.SteelBlue, unite_largeur, 11 * unite_hauteur);
                graphic.DrawString("Date d'émission :  " + DateTime.Now.ToString(), fnt1, Brushes.SteelBlue, unite_largeur, 13 * unite_hauteur - 6);


                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 15 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
                graphic.DrawRectangle(Pens.SteelBlue, unite_largeur - 5, 16 * unite_hauteur - 3, 24 * unite_largeur, 5 * unite_hauteur - 3);
                graphic.DrawString("Informations du patient  ", fnt1, Brushes.White, 6 * unite_largeur, 16 * unite_hauteur);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Identifiant :", patient.NumeroPatient.ToString()), fnt22, Brushes.SteelBlue, 8 * unite_largeur, 16 * unite_hauteur);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Patient :", patient.Nom + " " + patient.Prenom), fnt1, Brushes.SteelBlue, 8 * unite_largeur, 17 * unite_hauteur);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Sexe :   ", patient.Sexe), fnt1, Brushes.SteelBlue, 8 * unite_largeur, 18 * unite_hauteur - 3);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Age :    ", AfficherAge(patient.An, patient.Mois)), fnt1, Brushes.SteelBlue, 8 * unite_largeur, 19 * unite_hauteur - 6);
                graphic.DrawString(entreprise, fnt22, Brushes.SteelBlue, 8 * unite_largeur, 20 * unite_hauteur - 9);

                graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 22 * unite_hauteur, 24 * unite_largeur, unite_hauteur);
                graphic.DrawString("Date", fnt22, Brushes.White, unite_largeur, 22 * unite_hauteur);
                graphic.DrawString("Libellé  ", fnt22, Brushes.White, 4 * unite_largeur, 22 * unite_hauteur);
                graphic.DrawString("Qté  ", fnt22, Brushes.White, 18 * unite_largeur - 15, 22 * unite_hauteur);
                graphic.DrawString("P unit  ", fnt22, Brushes.White, 19 * unite_largeur, 22 * unite_hauteur);
                graphic.DrawString("P Total  ", fnt22, Brushes.White, 22 * unite_largeur, 22 * unite_hauteur);

                var j = 0;
                for (var i = 24 * index; i <= dtGrid.Rows.Count - 1; i++)
                {

                    var Yloc = unite_hauteur * j + 24 * unite_hauteur;
                  
                                           graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper(), fnt1,
                            Brushes.SteelBlue,  4*unite_largeur, Yloc);
                
                    graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.SteelBlue,  unite_largeur, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.SteelBlue, 18 * unite_largeur - 15, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.SteelBlue, 19 * unite_largeur, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.SteelBlue, 22 * unite_largeur, Yloc);

                    j++;
                }
               
                #region MyRegion
       
                #endregion

                if (flag == true)
                {
                    graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 49 * unite_hauteur - 10, 24 * unite_largeur, unite_hauteur);
                    graphic.DrawString("Total =    " + montantTotal.ToString(), fnt22, Brushes.White, 20 * unite_largeur - 15, 49 * unite_hauteur - 10);
                    graphic.DrawString("Arrêtée la présente facture à la somme de : "+Converti((long)montantTotal) + " Francs CFA", fnt1, Brushes.Black, unite_largeur, 50 * unite_hauteur);

                    graphic.DrawLine(Pens.SteelBlue, 11 * unite_largeur-5 , 51 * unite_hauteur, 24 * unite_largeur + 25, 51 * unite_hauteur);
                    //graphic.DrawString(, fnt1, Brushes.Black, unite_largeur, 51 * unite_hauteur);
                    //graphic.DrawLine(Pens.Black, unite_largeur, 52 * unite_hauteur, 24 * unite_largeur + 10, 52 * unite_hauteur);
                    graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.Black, 7 * unite_largeur, 52 * unite_hauteur -5);
                    graphic.DrawString(caissier.ToUpper(), fnt1, Brushes.SteelBlue, 9 * unite_largeur, 53 * unite_hauteur);
                    graphic.DrawLine(Pens.SteelBlue, 7 * unite_largeur, 53 * unite_hauteur - 5, 16 * unite_largeur, 53 * unite_hauteur - 5);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture", ex);
            }
            return bitmap;
        }

        public static Bitmap RapportPatient(string titre,Patient patient, DateTime date1, DateTime date2)
        {
            Bitmap bitmap = null;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 25 * unite_largeur;
                int detail_hauteur_facture = 14 * unite_hauteur;
                int hauteur_facture = 42 * unite_hauteur + detail_hauteur_facture;

                //creer un bit map
                bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                try
                {
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo,  unite_largeur,  0, 24*unite_largeur, 7*unite_hauteur);
                }
                catch { }
                //definir les polices 
                Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
                Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
                Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
                Font fnt3 = new Font("Century Gothic", 20, FontStyle.Bold);
                Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);
                #endregion

               //graphic.DrawString("Compte ORABANK N° 10649200100*92", fnt1, Brushes.White, 4 * unite_largeur, 11 * unite_hauteur);
                graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur, 9 * unite_hauteur);
                graphic.DrawString("Date d'émission :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 11 * unite_hauteur - 6);


                graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 12 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur - 5, 13 * unite_hauteur - 3, 24 * unite_largeur, 4 * unite_hauteur - 3);
                graphic.DrawString("Informations du patient  ", fnt1, Brushes.White, 6 * unite_largeur, 12 * unite_hauteur);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Patient :", patient.Nom + " " + patient.Prenom), fnt1, Brushes.Black, 6 * unite_largeur, 13 * unite_hauteur+5);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Sexe :   ", patient.Sexe), fnt1, Brushes.Black, 6 * unite_largeur, 14 * unite_hauteur +5);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Age :  ", patient.An.ToString() + "          Tél  : " +patient.Telephone), fnt1, Brushes.Black, 6 * unite_largeur, 15 * unite_hauteur + 5);
             
                graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 18 * unite_hauteur, 24 * unite_largeur, unite_hauteur);
                graphic.DrawString("Date", fnt1, Brushes.White, unite_largeur, 18 * unite_hauteur);
                graphic.DrawString("Libellé  ", fnt1, Brushes.White, 6 * unite_largeur, 18 * unite_hauteur);
                graphic.DrawString("Qté  ", fnt1, Brushes.White, 18 * unite_largeur - 15, 18 * unite_hauteur);
                graphic.DrawString("P unit  ", fnt1, Brushes.White, 19 * unite_largeur, 18* unite_hauteur);
                graphic.DrawString("P Total  ", fnt1, Brushes.White, 22 * unite_largeur, 18 * unite_hauteur);

                var idPatient = patient.NumeroPatient;
                var nomPatient = patient.Nom + " " + patient.Prenom;
                var dtAnalyse = ConnectionClassClinique.TableDesDetailsFacturesProforma(idPatient, date1, date2.AddHours(24));
                var dtConsultation = ConnectionClassClinique.ListeConsultationDesEntreprise(idPatient, date1, date2.AddHours(24));
                var dtHospitalisation = ConnectionClassClinique.ListeHospitalisationDesEntreprise(idPatient, date1, date2.AddHours(24));
                //var dtCredit = ConnectionClassPharmacie.ListeDesCredit(nomPatient, date1, date2.AddHours(24));
                //var dtVente = ConnectionClassPharmacie.ListeDesClientsVentesDetailles(nomPatient, date1, date2.AddHours(24));

                #region MyRegion
                var height1 = 0;
                var height2 = 0;
                var montantTotal = 0.0;
                for (int i = 0; i <= dtAnalyse.Rows.Count - 1; i++)
                {
                    var Yloc = unite_hauteur * i + 20 * unite_hauteur;

                    var montant = Double.Parse(dtAnalyse.Rows[i].ItemArray[7].ToString())*Int32.Parse(dtAnalyse.Rows[i].ItemArray[8].ToString());
                    graphic.DrawString(DateTime.Parse(dtAnalyse.Rows[i].ItemArray[1].ToString()).ToShortDateString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                    graphic.DrawString(dtAnalyse.Rows[i].ItemArray[6].ToString().ToUpper(), fnt1, Brushes.Black, 6 * unite_largeur, Yloc);
                    graphic.DrawString(dtAnalyse.Rows[i].ItemArray[8].ToString(), fnt1, Brushes.Black, 18 * unite_largeur - 15, Yloc);
                    graphic.DrawString(dtAnalyse.Rows[i].ItemArray[7].ToString(), fnt1, Brushes.Black, 19 * unite_largeur, Yloc);
                    graphic.DrawString(montant.ToString(), fnt1, Brushes.Black, 22 * unite_largeur, Yloc);
                    montantTotal += montant;
                }

                height1 = unite_hauteur * dtAnalyse.Rows.Count + 20 * unite_hauteur;
                #endregion

                for (int j = 0; j <= dtConsultation.Rows.Count - 1; j++)
                {
                    var wloc = unite_hauteur * j + height1;
                    var consulta = "";
                    if (dtConsultation.Rows[j].ItemArray[2].ToString() == "Consultation De  Specialité")
                    {
                        consulta = "Consultation en " + dtConsultation.Rows[j].ItemArray[7].ToString();
                    }
                    else
                    {
                        consulta = dtConsultation.Rows[j].ItemArray[2].ToString();
                    }
                    graphic.DrawString(DateTime.Parse(dtConsultation.Rows[j].ItemArray[3].ToString()).ToShortDateString(), fnt1, Brushes.Black, unite_largeur, wloc);
                    graphic.DrawString(consulta.ToUpper(), fnt1, Brushes.Black, 6 * unite_largeur, wloc);
                    graphic.DrawString("1", fnt1, Brushes.Black, 18 * unite_largeur - 15, wloc);
                    graphic.DrawString(dtConsultation.Rows[j].ItemArray[4].ToString(), fnt1, Brushes.Black, 19 * unite_largeur, wloc);
                    graphic.DrawString(dtConsultation.Rows[j].ItemArray[4].ToString(), fnt1, Brushes.Black, 22 * unite_largeur, wloc);
                    montantTotal += Double.Parse(dtConsultation.Rows[j].ItemArray[4].ToString());

                }
                height2 = height1 + unite_hauteur * dtConsultation.Rows.Count;
                var height3 = 0;
                for (int k = 0; k <= dtHospitalisation.Rows.Count - 1; k++)
                {
                    var wloc = unite_hauteur * k + height2;

                    graphic.DrawString(DateTime.Parse(dtHospitalisation.Rows[k].ItemArray[3].ToString()).ToShortDateString(), fnt1, Brushes.Black, unite_largeur, wloc);
                    graphic.DrawString("Hospitalisation".ToUpper(), fnt1, Brushes.Black, 6 * unite_largeur, wloc);
                    graphic.DrawString("1", fnt1, Brushes.Black, 18 * unite_largeur - 15, wloc);
                    graphic.DrawString(dtHospitalisation.Rows[k].ItemArray[4].ToString(), fnt1, Brushes.Black, 19 * unite_largeur, wloc);
                    graphic.DrawString(dtHospitalisation.Rows[k].ItemArray[4].ToString(), fnt1, Brushes.Black, 22 * unite_largeur, wloc);
                    montantTotal += Double.Parse(dtHospitalisation.Rows[k].ItemArray[4].ToString());

                }
                
                height3 = height2 + dtHospitalisation.Rows.Count * unite_hauteur;
                //for (var l = 0; l <= dtCredit.Rows.Count - 1; l++)
                //{
                //    var zLoc = unite_hauteur * l + height3;
                //    graphic.DrawString(DateTime.Parse(dtCredit.Rows[l].ItemArray[2].ToString()).ToShortDateString(), fnt1, Brushes.Black, unite_largeur, zLoc);
                //    graphic.DrawString(dtCredit.Rows[l].ItemArray[4].ToString().ToUpper(), fnt1, Brushes.Black, 6 * unite_largeur, zLoc);
                //    graphic.DrawString(dtCredit.Rows[l].ItemArray[6].ToString(), fnt1, Brushes.Black, 18 * unite_largeur - 15, zLoc);
                //    graphic.DrawString(dtCredit.Rows[l].ItemArray[5].ToString(), fnt1, Brushes.Black, 19 * unite_largeur, zLoc);
                //    graphic.DrawString(dtCredit.Rows[l].ItemArray[7].ToString(), fnt1, Brushes.Black, 22 * unite_largeur, zLoc);
                //    montantTotal += Double.Parse(dtCredit.Rows[l].ItemArray[7].ToString());

                //}

                //var height4 = height3 + dtCredit.Rows.Count * unite_hauteur;
                //for (var l = 0; l <= dtVente.Rows.Count - 1; l++)
                //{
                //    var zLoc = unite_hauteur * l + height4;
                //    graphic.DrawString(DateTime.Parse(dtVente.Rows[l].ItemArray[2].ToString()).ToShortDateString(), fnt1, Brushes.Black, unite_largeur, zLoc);
                //    graphic.DrawString(dtVente.Rows[l].ItemArray[4].ToString().ToUpper(), fnt1, Brushes.Black, 6 * unite_largeur, zLoc);
                //    graphic.DrawString(dtVente.Rows[l].ItemArray[6].ToString(), fnt1, Brushes.Black, 18 * unite_largeur - 15, zLoc);
                //    graphic.DrawString(dtVente.Rows[l].ItemArray[5].ToString(), fnt1, Brushes.Black, 19 * unite_largeur, zLoc);
                //    graphic.DrawString(dtVente.Rows[l].ItemArray[7].ToString(), fnt1, Brushes.Black, 22 * unite_largeur, zLoc);
                //    montantTotal += Double.Parse(dtVente.Rows[l].ItemArray[7].ToString());

                //}

                graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 49 * unite_hauteur - 10, 24 * unite_largeur, unite_hauteur);
                graphic.DrawString("Total =    " + montantTotal.ToString(), fnt1, Brushes.White, 20 * unite_largeur - 15, 49 * unite_hauteur - 10);
                graphic.DrawString("Arrêtée la présente facture de la consultation à la somme de : ", fnt1, Brushes.Black, unite_largeur, 50 * unite_hauteur);

                graphic.DrawLine(Pens.Black, 16 * unite_largeur - 10, 51 * unite_hauteur - 5, 24 * unite_largeur + 10, 51 * unite_hauteur - 5);
                graphic.DrawString(Converti((long)montantTotal) + " Francs CFA", fnt1, Brushes.Black, unite_largeur, 51 * unite_hauteur);
                graphic.DrawLine(Pens.Black, unite_largeur, 52 * unite_hauteur, 24 * unite_largeur + 15, 52 * unite_hauteur);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture", ex);
            }
            return bitmap;
        }


        public static Bitmap EtatFacturePatient(DataGridView dgView, Patient patient, double totalFacture, double totalPaye,int index)
        {
            Bitmap bitmap = null;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 22;
                int unite_largeur = 32;
                int largeur_facture = 25 * unite_largeur;
                int detail_hauteur_facture = 14 * unite_hauteur;
                int hauteur_facture = 53 * unite_hauteur;

                //creer un bit map
                bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                try
                {
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo, unite_largeur, 0, 24 * unite_largeur, 6 * unite_hauteur);
                }
                catch { }
                //definir les polices 
                Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
                Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
                Font fnt3 = new Font("Century Gothic", 15, FontStyle.Bold);
                Font fnt22 = new Font("Century Gothic", 12, FontStyle.Bold);
                #endregion

                //graphic.DrawString("Compte ORABANK N° 10649200100*92", fnt1, Brushes.White, 4 * unite_largeur, 11 * unite_hauteur);
                graphic.DrawString("Rapport des actes du patient", fnt3, Brushes.Black, unite_largeur, 7* unite_hauteur+5);
                graphic.DrawString("Emis le :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, 16*unite_largeur, 7 * unite_hauteur +5);


                graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 9 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur - 5, 10 * unite_hauteur - 3, 24 * unite_largeur, 4 * unite_hauteur - 3);
                graphic.DrawString("Informations du patient  ", fnt1, Brushes.White, 7 * unite_largeur, 9 * unite_hauteur+1);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Patient :", patient.Nom + " " + patient.Prenom), fnt1, Brushes.Black, 6 * unite_largeur, 10 * unite_hauteur + 5);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Sexe :   ", patient.Sexe), fnt1, Brushes.Black, 6 * unite_largeur, 11 * unite_hauteur + 5);
                graphic.DrawString(string.Format("{0,-25} {1,-80}", "Age :  ", patient.An.ToString() + "        Tél  : " + patient.Telephone), fnt1, Brushes.Black, 6 * unite_largeur, 12 * unite_hauteur + 5);

                graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 15 * unite_hauteur, 24 * unite_largeur, unite_hauteur);
                graphic.DrawString("Libellé", fnt22, Brushes.White, unite_largeur, 15 * unite_hauteur);
                graphic.DrawString("Prix  ", fnt22, Brushes.White, 17 * unite_largeur, 15 * unite_hauteur);
                graphic.DrawString("Qté  ", fnt22, Brushes.White, 20 * unite_largeur - 15, 15 * unite_hauteur);
                graphic.DrawString("P Total  ", fnt22, Brushes.White, 22 * unite_largeur, 15 * unite_hauteur);

                var j = 0;
                for (var i = 35 * index; i < dgView.Rows.Count; i++)
                {
                    var YLOC = 16 * unite_hauteur +5 + j * unite_hauteur;

                    if (dgView.Rows[i].Cells[0].Value.ToString().Contains("FACTURE N°") || dgView.Rows[i].Cells[0].Value.ToString().Contains("TOTAL"))
                    {
                        graphic.DrawString(dgView.Rows[i].Cells[0].Value.ToString(), fnt22, Brushes.Black, unite_largeur, YLOC-2);
                        graphic.DrawString(dgView.Rows[i].Cells[3].Value.ToString(), fnt22, Brushes.Black, 22 * unite_largeur, YLOC);
                    }
                    else
                    {
                        graphic.DrawString(dgView.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, 2*unite_largeur, YLOC);
                        graphic.DrawString(dgView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 22 * unite_largeur, YLOC);
               
                    }
                    graphic.DrawString(dgView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur, YLOC);
                    graphic.DrawString(dgView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur - 15, YLOC);
                         j++;
                }
                graphic.FillRectangle(Brushes.White, unite_largeur - 5, 51 * unite_hauteur +5, 24 * unite_largeur,5*unite_hauteur);
                   
                if (dgView.Rows.Count <= 38 * (1 + index))
                {
                    graphic.FillRectangle(Brushes.Black, unite_largeur , 51 * unite_hauteur +5, 24 * unite_largeur, unite_hauteur);
                    graphic.DrawString("Total     :       " + totalFacture +"           Payé     :        " +
                        totalPaye + "         Reste :        "+ (totalFacture- totalPaye ), fnt22, Brushes.White, 7* unite_largeur - 15, 51 * unite_hauteur +5);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer facture", ex);
            }
            return bitmap;
        }

        public static Bitmap RapportDunMedecin(DataGridView dtGrid,
           GestionPharmacetique.AppCode.Employe employe, string titreRapport,int start)
        {

            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 14 * unite_hauteur;
            int hauteur_facture = 42 * unite_hauteur + detail_hauteur_facture;
            bool flag;
            if (dtGrid.Rows.Count <= 31 * (1 + start))
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
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur, 24*unite_largeur, 7*unite_hauteur);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 20, FontStyle.Bold);
 #endregion

            graphic.DrawString("N'Djamena le :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 16 * unite_hauteur + 10);


            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 10 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur - 5, 10 * unite_hauteur - 3, 24 * unite_largeur, 5 * unite_hauteur - 3);

            graphic.DrawString("information employé", fnt1, Brushes.White, 5 * unite_largeur, 10 * unite_hauteur);
            try
            {
                graphic.DrawString(employe.NomEmployee, fnt1, Brushes.Black, 5 * unite_largeur, 11 * unite_hauteur);
                graphic.DrawString(employe.Telephone1 + " / " + employe.Telephone2, fnt1, Brushes.Black, 5 * unite_largeur, 12 * unite_hauteur - 3);
                graphic.DrawString(employe.Email, fnt1, Brushes.Black, 5 * unite_largeur, 13 * unite_hauteur - 6);
                graphic.DrawString(employe.Addresse, fnt1, Brushes.Black, 5 * unite_largeur, 14 * unite_hauteur - 9);
            }
            catch { }

            graphic.DrawString(titreRapport, fnt1, Brushes.Black, unite_largeur, 15 * unite_hauteur + 10);
            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 18 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);

            graphic.DrawString("Type de consultation ".ToUpper(), fnt1, Brushes.White, unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("NBRE  ", fnt1, Brushes.White, 13 * unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("FRAIS ", fnt1, Brushes.White, 15 * unite_largeur + 10, 18 * unite_hauteur);
            graphic.DrawString("PRIX TOTAL  ", fnt1, Brushes.White, 18 * unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("PART DOCTEUR ", fnt1, Brushes.White, 21 * unite_largeur, 18 * unite_hauteur);

            var total = 0.0;
            var partDocteur = 0.0;

            var j = 0;
            for (var i = start * 31; i <= dtGrid.Rows.Count - 1; i++)
            {
                var Yloc = unite_hauteur * j + 19 * unite_hauteur + 15;

                if (i % 2 == 0)
                {
                    graphic.FillRectangle(Brushes.White, unite_largeur, Yloc, 24 * unite_largeur + 10, unite_hauteur);
                }
                else
                {
                    graphic.FillRectangle(Brushes.WhiteSmoke, unite_largeur, Yloc, 24 * unite_largeur + 10, unite_hauteur);
                }
                graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString().ToUpper(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur + 10, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 22 * unite_largeur, Yloc);


                j++;
            }
            for (var l = 0; l < dtGrid.Rows.Count; l++)
            {
                total += Double.Parse(dtGrid.Rows[l].Cells[3].Value.ToString());
                partDocteur += Double.Parse(dtGrid.Rows[l].Cells[5].Value.ToString());
            }
            graphic.FillRectangle(Brushes.White, unite_largeur, 51 * unite_hauteur, 24 * unite_largeur + 10, 6 * unite_hauteur);
            graphic.FillRectangle(Brushes.Black, unite_largeur, 51 * unite_hauteur, 24 * unite_largeur + 10, unite_hauteur);

            if (flag)
            {
                graphic.DrawString("TOTAL", fnt1, Brushes.White,  unite_largeur + 10, 51 * unite_hauteur);
                graphic.DrawString(total.ToString() + " F CFA", fnt1, Brushes.White, 17 * unite_largeur-10, 51 * unite_hauteur);
                graphic.DrawString(partDocteur.ToString() + " F CFA", fnt1, Brushes.White, 22 * unite_largeur-10, 51 * unite_hauteur);
            }
            return bitmap;
        }

        public static Bitmap RapportDesAnalysesDunMedecin(DataGridView dtGrid,
               GestionPharmacetique.AppCode.Employe employe, string titreRapport, int start)
        {
            try
            {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 14 * unite_hauteur;
            int hauteur_facture = 42 * unite_hauteur + detail_hauteur_facture;
            bool flag;
            if (dtGrid.Rows.Count <= 31 * (1 + start))
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
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo,  unite_largeur,  unite_hauteur, 24*unite_largeur, 7*unite_hauteur);
            }
            catch { }
            //definir les polices   
            Font fnt1 = new Font("Century Gothic", 10, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt3 = new Font("Century Gothic", 14, FontStyle.Bold);
            #endregion

            graphic.DrawString("N'Djamena le :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 16 * unite_hauteur + 10);
            graphic.DrawString("Page " + (start + 1).ToString(), fnt11, Brushes.Black, 32 * unite_largeur, 15);

            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 10 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur - 5, 10 * unite_hauteur - 3, 24 * unite_largeur, 5 * unite_hauteur - 3);

            graphic.DrawString("information employé", fnt1, Brushes.White, 5 * unite_largeur, 10 * unite_hauteur);
            try
            {
                graphic.DrawString(employe.NomEmployee, fnt1, Brushes.Black, 5 * unite_largeur, 11 * unite_hauteur);
                graphic.DrawString(employe.Telephone1 + " / " + employe.Telephone2, fnt1, Brushes.Black, 5 * unite_largeur, 12 * unite_hauteur - 3);
                graphic.DrawString(employe.Email, fnt1, Brushes.Black, 5 * unite_largeur, 13 * unite_hauteur - 6);
                graphic.DrawString(employe.Addresse, fnt1, Brushes.Black, 5 * unite_largeur, 14 * unite_hauteur - 9);

            }
            catch (Exception)
            {
            }
            graphic.DrawString(titreRapport, fnt1, Brushes.Black, unite_largeur, 15 * unite_hauteur + 10);
            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 18 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);

            graphic.DrawString("Type d' examen ".ToUpper(), fnt1, Brushes.White, unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("NBRE  ", fnt1, Brushes.White, 13 * unite_largeur + 15, 18 * unite_hauteur);
            graphic.DrawString("FRAIS  ", fnt1, Brushes.White, 15 * unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("PRIX. TOTAL ", fnt1, Brushes.White, 18 * unite_largeur + 15, 18 * unite_hauteur);
            graphic.DrawString("PART DOCTEUR ", fnt1, Brushes.White, 21 * unite_largeur + 15, 18 * unite_hauteur);

            var total = 0.0;
            var partDocteur = 0.0;
            var j = 0;
           
            for (var i = start * 31; i <= dtGrid.Rows.Count - 1; i++)
            {
                var Yloc = unite_hauteur * j + 19 * unite_hauteur + 15;

                if (i % 2 == 0)
                {
                    graphic.FillRectangle(Brushes.White, unite_largeur, Yloc, 24 * unite_largeur + 10 ,unite_hauteur);
                }
                else
                {
                    graphic.FillRectangle(Brushes.WhiteSmoke, unite_largeur, Yloc, 24 * unite_largeur + 10, unite_hauteur);
                }

                graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur + 15, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur + 15, Yloc);
                //graphic.DrawString(dtGrid.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 15, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 22 * unite_largeur, Yloc);


                j++;
            }
            for (var l = 0; l < dtGrid.Rows.Count; l++)
            {
                total += Double.Parse(dtGrid.Rows[l].Cells[3].Value.ToString());
                partDocteur += Double.Parse(dtGrid.Rows[l].Cells[5].Value.ToString());
            }
            graphic.FillRectangle(Brushes.White, unite_largeur, 51 * unite_hauteur-2, 24 * unite_largeur + 10, 6 * unite_hauteur);
            graphic.FillRectangle(Brushes.Black, unite_largeur, 51 * unite_hauteur, 24 * unite_largeur + 10, unite_hauteur);

            if (flag)
            {
                graphic.DrawString("TOTAL  ", fnt1, Brushes.White, unite_largeur + 10, 51 * unite_hauteur);
                graphic.DrawString(total.ToString() + " F CFA", fnt1, Brushes.White, 17 * unite_largeur - 10, 51 * unite_hauteur);
                graphic.DrawString(partDocteur.ToString() + " F CFA", fnt1, Brushes.White, 22 * unite_largeur - 10, 51 * unite_hauteur);
            }
            return bitmap;
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap RapportDesOrdonnanceDunMedecin(DataGridView dtGrid,
            GestionPharmacetique.AppCode.Employe employe, string titreRapport)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 14 * unite_hauteur;
            int hauteur_facture = 42 * unite_hauteur + detail_hauteur_facture;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, 12 * unite_largeur, 2 * unite_hauteur, 120, 100);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 20, FontStyle.Bold);

            graphic.DrawString("CLINIQUE AL-SHIFA", fnt3, Brushes.Black, unite_largeur, unite_hauteur + 10);
            graphic.DrawString("CLINIQUE MEDICO-CHIRURGICALE", fnt1, Brushes.Black, unite_largeur, 3 * unite_hauteur);
            graphic.DrawString("B.P : 365,  DIGUEL-EST", fnt1, Brushes.Black, unite_largeur, 4 * unite_hauteur);
            graphic.DrawString("TEL : 22 51 04 57 - No   /AL,CMC/" + DateTime.Now.Year, fnt1, Brushes.Black, unite_largeur, 5 * unite_hauteur);
            graphic.DrawString("TEL : 22 51 04 57 - No RCCM TC/NDJ/13 A 1398 ", fnt1, Brushes.Black, unite_largeur, 6 * unite_hauteur);
            graphic.DrawString("Email : Cliniquealshifa@yahoo.com ", fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur);
            graphic.DrawString("N'DJAMENA-TCHAD", fnt1, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            #endregion

            graphic.DrawString("N'Djamena le :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 16 * unite_hauteur + 10);


            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 10 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur - 5, 10 * unite_hauteur - 3, 24 * unite_largeur, 5 * unite_hauteur - 3);

            graphic.DrawString("information employé", fnt1, Brushes.White, 5 * unite_largeur, 10 * unite_hauteur);
            graphic.DrawString(employe.NomEmployee, fnt1, Brushes.Black, 5 * unite_largeur, 11 * unite_hauteur);
            graphic.DrawString(employe.Telephone1 + " / " + employe.Telephone2, fnt1, Brushes.Black, 5 * unite_largeur, 12 * unite_hauteur - 3);
            graphic.DrawString(employe.Email, fnt1, Brushes.Black, 5 * unite_largeur, 13 * unite_hauteur - 6);
            graphic.DrawString(employe.Addresse, fnt1, Brushes.Black, 5 * unite_largeur, 14 * unite_hauteur - 9);

            graphic.DrawString(titreRapport, fnt1, Brushes.Black, unite_largeur, 15 * unite_hauteur + 10);
            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 18 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);

            graphic.DrawString("Date ", fnt1, Brushes.White, unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("Patient  ", fnt1, Brushes.White, 7 * unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("Produit ", fnt1, Brushes.White, 15 * unite_largeur + 10, 18 * unite_hauteur);

            for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
            {
                var Yloc = unite_hauteur * i + 19 * unite_hauteur + 15;
                graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 7 * unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur + 10, Yloc);
            }


            return bitmap;
        }

        public static Bitmap RapportDeLaCaisse(DataGridView dtGrid,
             GestionPharmacetique.AppCode.Employe employe, string titreRapport)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 14 * unite_hauteur;
            int hauteur_facture = 0;

            bool flag = false;
            if (dtGrid.Rows.Count <= 30)
            {
                flag = true;
                hauteur_facture = 54 * unite_hauteur + 13;
            }
            else
            {
                flag = false;
                hauteur_facture = 49 * unite_hauteur + 10;
            }

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo,  unite_largeur, 2 * unite_hauteur, 24 * unite_largeur, 6 * unite_hauteur);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 20, FontStyle.Bold);

            graphic.DrawString("Page 1 ", fnt11, Brushes.Black, 12 * unite_largeur, 5);
             #endregion

            graphic.DrawString("N'Djamena le :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, unite_largeur, 16 * unite_hauteur + 10);


            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 10 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur - 5, 10 * unite_hauteur - 3, 24 * unite_largeur, 5 * unite_hauteur - 3);

            graphic.DrawString("information employé", fnt1, Brushes.White, 5 * unite_largeur, 10 * unite_hauteur);
            graphic.DrawString(employe.NomEmployee, fnt1, Brushes.Black, 5 * unite_largeur, 11 * unite_hauteur);
            graphic.DrawString(employe.Telephone1 + " / " + employe.Telephone2, fnt1, Brushes.Black, 5 * unite_largeur, 12 * unite_hauteur - 3);
            graphic.DrawString(employe.Email, fnt1, Brushes.Black, 5 * unite_largeur, 13 * unite_hauteur - 6);
            graphic.DrawString(employe.Addresse, fnt1, Brushes.Black, 5 * unite_largeur, 14 * unite_hauteur - 9);

            graphic.DrawString(titreRapport, fnt1, Brushes.Black, unite_largeur, 15 * unite_hauteur + 10);
            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 18 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);


            graphic.DrawString("date ", fnt1, Brushes.White, unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("patient  ", fnt1, Brushes.White, 9 * unite_largeur, 18 * unite_hauteur);
            graphic.DrawString("Montant ", fnt1, Brushes.White, 20 * unite_largeur + 10, 18 * unite_hauteur);

            var total = 0.0;
            for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
            {
                var Yloc = unite_hauteur * i + 19 * unite_hauteur + 15;
                graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 9 * unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 10, Yloc);
                total += Double.Parse(dtGrid.Rows[i].Cells[2].Value.ToString());
            }

            if (flag)
            {
                graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 53 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
                graphic.DrawString("Total : ", fnt1, Brushes.White, 18 * unite_largeur + 10, 53 * unite_hauteur);
                graphic.DrawString(total.ToString() + " F CFA", fnt1, Brushes.White, 20 * unite_largeur, 53 * unite_hauteur);
            }

            return bitmap;
        }

        public static Bitmap RapportDeLaCaisse(DataGridView dtGrid,
        GestionPharmacetique.AppCode.Employe employe, string titreRapport, int numeroPage, int index, int start)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int detail_hauteur_facture = 14 * unite_hauteur;
            int hauteur_facture = 0;

            bool flag = false;
            if (dtGrid.Rows.Count <= 30 + index * (1 + start))
            {
                flag = true;
                hauteur_facture = 54 * unite_hauteur + 10;
            }
            else
            {
                flag = false;
                hauteur_facture = 49 * unite_hauteur + 10;
            }

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 20, FontStyle.Bold);

            #endregion
            graphic.DrawString("Page " + numeroPage, fnt11, Brushes.Black, 12 * unite_largeur, 5);


            graphic.DrawString(titreRapport, fnt1, Brushes.Black, unite_largeur, unite_hauteur + 10);
            graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 3 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);


            graphic.DrawString("date ", fnt1, Brushes.White, unite_largeur, 3 * unite_hauteur);
            graphic.DrawString("patient  ", fnt1, Brushes.White, 9 * unite_largeur, 3 * unite_hauteur);
            graphic.DrawString("Montant ", fnt1, Brushes.White, 20 * unite_largeur + 10, 3 * unite_hauteur);

            var total = 0.0;
            var j = 0;
            foreach (DataGridViewRow dtRows in dtGrid.Rows)
            {
                total += Double.Parse(dtRows.Cells[2].Value.ToString());
            }
            for (var i = 30 + start * index; i <= dtGrid.Rows.Count - 1; i++)
            {
                var Yloc = unite_hauteur * j + 4 * unite_hauteur + 15;
                graphic.DrawString(DateTime.Parse(dtGrid.Rows[i].Cells[0].Value.ToString()).ToShortDateString(), fnt1, Brushes.Black, unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 9 * unite_largeur, Yloc);
                graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 10, Yloc);
                j++;
            }

            if (flag == true)
            {
                graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 53 * unite_hauteur, 24 * unite_largeur + 2, unite_hauteur);
                graphic.DrawString("Total : ", fnt1, Brushes.White, 18 * unite_largeur + 10, 53 * unite_hauteur);
                graphic.DrawString(total.ToString() + " F CFA", fnt1, Brushes.White, 20 * unite_largeur, 53 * unite_hauteur);
            }
            return bitmap;
        }

        public static Bitmap FactureHonoraire(Entreprise entreprise, string date, string mois)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int hauteur_facture = 54 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture + 1, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 2 * unite_hauteur, 13 * unite_largeur, 7 * unite_hauteur);

                if (entreprise.NomEntreprise.ToLower().Equals("nrc"))
                {
                    logo = global::GestionDuneClinique.Properties.Resources.logoNRC1;
                }
                else
                {
                    logo = global::GestionDuneClinique.Properties.Resources.logo1;

                }
                graphic.DrawImage(logo, 12 * unite_largeur, 1 * unite_hauteur, 140, 180);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Ubuntu", 12, FontStyle.Regular);
            Font fntB = new Font("Ubuntu ", 12, FontStyle.Bold);
            Font fnt11 = new Font("Ubuntu ", 10, FontStyle.Regular);
            Font fnt2 = new Font("Ubuntu", 28, FontStyle.Bold);
            Font fnt3 = new Font("Ubuntu", 18, FontStyle.Bold);
            Font fnt22 = new Font("Ubuntu", 12, FontStyle.Regular);

           #endregion

            graphic.FillRectangle(Brushes.SteelBlue, unite_largeur - 5, 10 * unite_hauteur, 24 * unite_largeur, 2 * unite_hauteur);
            
            graphic.DrawString("FACTURE DU " + date, fnt3, Brushes.SteelBlue, 9 * unite_largeur, 13 * unite_hauteur);
            graphic.DrawLine(Pens.SteelBlue, 9 * unite_largeur + 5, 14 * unite_hauteur + 10, 17 * unite_largeur + 30, 14 * unite_hauteur + 10);
            graphic.DrawString("Adressée à :  " + DateTime.Now.ToString(), fnt1, Brushes.SteelBlue, unite_largeur, 15 * unite_hauteur + 15);
            graphic.DrawString("Organisation ou Société  :  " + entreprise.NomEntreprise, fnt1, Brushes.SteelBlue, unite_largeur, 16 * unite_hauteur + 15);



            graphic.DrawRectangle(Pens.SteelBlue, 20, 18 * unite_hauteur, 6 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 6 * unite_largeur + 20, 18 * unite_hauteur, 6 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 12 * unite_largeur + 20, 18 * unite_hauteur, 6 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 18 * unite_largeur + 20, 18 * unite_hauteur, 6 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 20, 19 * unite_hauteur, 6 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 6 * unite_largeur + 20, 19 * unite_hauteur, 6 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 12 * unite_largeur + 20, 19 * unite_hauteur, 6 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 18 * unite_largeur + 20, 19 * unite_hauteur, 6 * unite_largeur, unite_hauteur);

            graphic.DrawString("Référence  ", fnt1, Brushes.SteelBlue, 7 * unite_largeur, 18 * unite_hauteur + 2);
            graphic.DrawString("Mode de paiement  ", fnt1, Brushes.SteelBlue, 19 * unite_largeur + 10, 18 * unite_hauteur + 2);
            graphic.DrawString("Commande ", fnt1, Brushes.SteelBlue, 7 * unite_largeur, 19 * unite_hauteur + 2);

            graphic.FillRectangle(Brushes.SteelBlue, 20, 22 * unite_hauteur, 24 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 20, 23 * unite_hauteur, 4 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 4 * unite_largeur + 20, 23 * unite_hauteur, 12 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 16 * unite_largeur + 20, 23 * unite_hauteur, 4 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 20 * unite_largeur + 20, 23 * unite_hauteur, 4 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawRectangle(Pens.SteelBlue, 14 * unite_largeur + 20, 35 * unite_hauteur, 10 * unite_largeur, 40);
            graphic.DrawString("Date ", fnt1, Brushes.White, unite_largeur, 22 * unite_hauteur);
            graphic.DrawString("Désignation ", fnt1, Brushes.White, 5 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString("P.Unitaire ", fnt1, Brushes.White, 17 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString("P.Total  ", fnt1, Brushes.White, 21 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString("TOTAL    = ", fntB, Brushes.SteelBlue, 15 * unite_largeur, 35 * unite_hauteur + 10);

            graphic.DrawString(DateTime.Now.ToShortDateString(), fnt1, Brushes.SteelBlue, unite_largeur, 24 * unite_hauteur);
            graphic.DrawString("Honoraire du Médecin traitant", fnt1, Brushes.SteelBlue, 5 * unite_largeur, 24 * unite_hauteur);
            graphic.DrawString("Mois de " + mois, fnt1, Brushes.SteelBlue, 5 * unite_largeur, 25 * unite_hauteur);
            graphic.DrawString(entreprise.PrixHonoraire.ToString(), fnt1, Brushes.SteelBlue, 17 * unite_largeur, 24 * unite_hauteur);
            graphic.DrawString(entreprise.PrixHonoraire.ToString(), fnt1, Brushes.SteelBlue, 21 * unite_largeur, 24 * unite_hauteur);
            graphic.DrawString(entreprise.PrixHonoraire.ToString(), fntB, Brushes.SteelBlue, 21 * unite_largeur, 35 * unite_hauteur + 10);



            graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.SteelBlue, unite_largeur, 38 * unite_hauteur + 8);
            graphic.DrawString(Converti((long)entreprise.PrixHonoraire) + " Francs CFA", fntB, Brushes.SteelBlue, 10 * unite_largeur + 25, 38 * unite_hauteur + 8);
            graphic.DrawLine(Pens.SteelBlue, 10 * unite_largeur + 25, 39 * unite_hauteur + 7, 24 * unite_largeur + 15, 39 * unite_hauteur + 7);

            graphic.DrawString("Signature".ToUpper(), fntB, Brushes.SteelBlue, 14 * unite_largeur, 42 * unite_hauteur);
            graphic.DrawLine(Pens.SteelBlue, 14 * unite_largeur, 43 * unite_hauteur - 3, 17 * unite_largeur +6, 43 * unite_hauteur - 3);

            return bitmap;
        }

    // cretificat de grossesse
        public static Bitmap CertificatDeGrossesse(CertificatDeGrossesse certificat)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 30;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int hauteur_facture = 16 * unite_hauteur;

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur,  15, 23*unite_largeur, 4*unite_hauteur+15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Underline);
            Font fnt3 = new Font("Century Gothic", 20, FontStyle.Underline);
            Font fnt22 = new Font("Century Gothic", 12, FontStyle.Bold);

           #endregion
                 var listePatient = from p in AppCode. ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == certificat.IDPatiente
                                   select p;

                var patient = new AppCode. Patient();
                foreach (var p in listePatient)
                   patient = p;
            
            var nomPatient = patient.Nom + " " + patient.Prenom;
            var sageFemme = certificat.SageFemme;
            var mois = certificat.DateAccouchement.ToLongDateString().Substring(certificat.DateAccouchement.ToLongDateString().IndexOf(" ")+1);
            graphic.DrawString("CERTIFICAT DE GROSSESSE", fnt3, Brushes.Black,7* unite_largeur, 6 * unite_hauteur);
            graphic.DrawString("Je, soussignée, ", fnt1, Brushes.Black, unite_largeur, 8 * unite_hauteur);
            graphic.DrawString(sageFemme.ToUpper(), fnt22, Brushes.Black, 6 * unite_largeur - 10, 8 * unite_hauteur - 5);
            graphic.DrawString("sage-femme, certifie a voir examiné ce jour ", fnt1, Brushes.Black,14* unite_largeur+18, 8 * unite_hauteur);
            
            graphic.DrawString("Mme : ", fnt1, Brushes.Black, unite_largeur, 9 * unite_hauteur);
            graphic.DrawString(nomPatient.ToUpper(), fnt22, Brushes.Black, 7 * unite_largeur, 9 * unite_hauteur - 6);
            graphic.DrawString("Epouse de Mr :  ", fnt1, Brushes.Black, unite_largeur, 10 * unite_hauteur);
            graphic.DrawString(certificat.Epoux.ToUpper(), fnt22, Brushes.Black, 8 * unite_largeur, 10 * unite_hauteur - 6);
            graphic.DrawString("Et qu’elle présente les signes d’une grossesse en évolution, ", fnt1, Brushes.Black, unite_largeur, 11 * unite_hauteur);
            graphic.DrawString("de, ", fnt1, Brushes.Black, unite_largeur, 12 * unite_hauteur);
            graphic.DrawString(Converti(certificat.Periode) + "(" + certificat.Periode +") mois", fnt22, Brushes.Black, 3*unite_largeur, 12 * unite_hauteur-6);
            graphic.DrawString("Son accouchement est prévu pour", fnt1, Brushes.Black, unite_largeur, 13 * unite_hauteur);
            graphic.DrawString(mois.Substring(mois.IndexOf(" " )), fnt22, Brushes.Black, 12*unite_largeur, 13 * unite_hauteur-6);
            
            graphic.DrawString("Fait à N’Djamena, le " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 14*unite_largeur, 15 * unite_hauteur);

            graphic.DrawString(".....................................................................",
                           fnt22, Brushes.Black, unite_largeur * 5-10, 8 * unite_hauteur);
            graphic.DrawString("..........................................................................................................................................................................",
                fnt22, Brushes.Black, unite_largeur * 2 + 20, 9 * unite_hauteur);
            graphic.DrawString("............................................................................................................................................................. ",
                fnt22, Brushes.Black, 5 * unite_largeur, 10 * unite_hauteur);
            graphic.DrawString("............................................................................................................................................................. ",
     fnt22, Brushes.Black, 29+ unite_largeur, 12 * unite_hauteur);
            graphic.DrawString("............................................................................................................................................................. ",
    fnt22, Brushes.Black, 10 * unite_largeur-15, 13 * unite_hauteur);
            
            return bitmap;
        }

        // certificat de naissance
        public static Bitmap CertificatDeNaissance(CertificatNaissance certificat)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int hauteur_facture = 42 * unite_hauteur;

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 0, 23 * unite_largeur, 4 * unite_hauteur);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Underline);
            Font fnt3 = new Font("Century Gothic", 20, FontStyle.Underline);
            Font fnt22 = new Font("Century Gothic", 11, FontStyle.Bold);

            #endregion
            var listePatient = from p in AppCode. ConnectionClassClinique.ListeDesPatients()
                                   where p.NumeroPatient == certificat.IDPatient
                                   select p;

                var patient = new AppCode. Patient();
                foreach (var p in listePatient)
                    patient = p;
            var nomPatient = patient.Nom + " " + patient.Prenom;
           
            var sexe = "";
            if (certificat.Sexe == "M")
            {
                sexe ="MASCULIN";
            }
            else if (certificat.Sexe == "F")
            {
                sexe = "FEMININ";
            }
            graphic.DrawString("CERTIFICAT DE NAISSANCE", fnt3, Brushes.Black, 7 * unite_largeur, 5 * unite_hauteur-15);
            graphic.DrawString(certificat.SageFemme.ToUpper(), fnt22, Brushes.Black, 8 * unite_largeur , 7 * unite_hauteur - 20);
            graphic.DrawString("Je, soussignée, ......................................................................................................... sage-femme, certifie que",
                fnt1, Brushes.Black,  unite_largeur , 7 * unite_hauteur - 15);
            graphic.DrawString("Le ...................................................................................à.............................................................................heure",
                fnt1, Brushes.Black,  unite_largeur , 8 * unite_hauteur-15);
            graphic.DrawString(certificat.NaissanceEnfant.ToShortDateString(), fnt22, Brushes.Black, 6 * unite_largeur, 8 * unite_hauteur - 20);
            graphic.DrawString(certificat.NaissanceEnfant.ToShortTimeString(), fnt22, Brushes.Black, 15 * unite_largeur, 8 * unite_hauteur - 20);
           
            graphic.DrawString("A N’Djamena est né(e) ", fnt1, Brushes.Black, unite_largeur, 9 * unite_hauteur-10);
            graphic.DrawString("Un enfant de sexe ............................................................. de poids ...........................................................................", 
                fnt1, Brushes.Black,  unite_largeur, 10 * unite_hauteur -5);
            graphic.DrawString(sexe, fnt22, Brushes.Black, 9 * unite_largeur, 10 * unite_hauteur - 8);
            graphic.DrawString(certificat.Poids + " grammes", fnt22, Brushes.Black, 19 * unite_largeur, 10 * unite_hauteur - 8);
           
            graphic.DrawString("Monsieur : ", fnt22, Brushes.Black, unite_largeur, 11 * unite_hauteur);
            graphic.DrawString("Nom & Prénom ........................................................................................................................................................", 
                fnt1, Brushes.Black, unite_largeur, 12 * unite_hauteur);
            
            graphic.DrawString("Né le .......................................................................... Domicilié à ..................................................................................",
               fnt1, Brushes.Black, unite_largeur, 13 * unite_hauteur);
            graphic.DrawString("Profession .........................................................................................................................................................................",
               fnt1, Brushes.Black, unite_largeur, 14 * unite_hauteur);
            
            graphic.DrawString(certificat.Epoux.ToUpper(), fnt22, Brushes.Black, 10 * unite_largeur, 12 * unite_hauteur - 4);
            graphic.DrawString(certificat.NaissanceEpoux.ToShortDateString(), fnt22, Brushes.Black, 5 * unite_largeur, 13 * unite_hauteur - 4);
            graphic.DrawString(certificat.DomicileEpoux.ToUpper(), fnt22, Brushes.Black, 18 * unite_largeur, 13 * unite_hauteur - 4);
            graphic.DrawString(certificat.ProfessionEpoux.ToUpper(), fnt22, Brushes.Black, 12 * unite_largeur, 14 * unite_hauteur - 4);
           
            graphic.DrawString("Et de Madame : ", fnt22, Brushes.Black, unite_largeur, 15 * unite_hauteur+10);
            graphic.DrawString("Nom & Prénom .............................................................................................................................................................................",
                fnt1, Brushes.Black, unite_largeur, 16 * unite_hauteur+10);
            graphic.DrawString("Né le .......................................................................... Domicilié à ......................................................................................",
               fnt1, Brushes.Black, unite_largeur, 17 * unite_hauteur+10);
            graphic.DrawString("Profession .........................................................................................................................................................................",
               fnt1, Brushes.Black, unite_largeur, 18 * unite_hauteur+10);

            graphic.DrawString(nomPatient.ToUpper(), fnt22, Brushes.Black, 10 * unite_largeur, 16 * unite_hauteur +6);
            graphic.DrawString(certificat.NaissanceMere.ToShortDateString(), fnt22, Brushes.Black, 5 * unite_largeur, 17 * unite_hauteur +6);
            graphic.DrawString(certificat.DomicileMere.ToUpper(), fnt22, Brushes.Black, 18 * unite_largeur, 17 * unite_hauteur + 6);
            graphic.DrawString(certificat.ProfesssionMere.ToUpper(), fnt22, Brushes.Black, 12 * unite_largeur, 18 * unite_hauteur +6);
           
            graphic.DrawString("Et qu’ils déclarent prénommer l’enfant .........................................................................................................................................................",
                fnt1, Brushes.Black, unite_largeur , 19 * unite_hauteur+20);
            graphic.DrawString(certificat.BeBe.ToUpper(), fnt22, Brushes.Black, 13 * unite_largeur, 19 * unite_hauteur+15);
            graphic.DrawString("N’Djamena, le " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 22 * unite_hauteur-7);
 

            return bitmap;
        }

        // certificat de deces
        public static Bitmap CertificatDeDeces(CertificatDeDeces certificat)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 30;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int hauteur_facture = 30 * unite_hauteur;

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 0, 23 * unite_largeur, 4 * unite_hauteur+15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
            Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Underline);
            Font fnt3 = new Font("Century Gothic", 20, FontStyle.Underline);
            Font fnt22 = new Font("Century Gothic", 13, FontStyle.Bold);

            #endregion
            var listePatient = from p in AppCode.ConnectionClassClinique.ListeDesPatients()
                               where p.NumeroPatient == certificat.IDPatient
                               select p;

            var patient = new AppCode.Patient();
            foreach (var p in listePatient)
                patient = p;

            var nomPatient = patient.Nom + " " + patient.Prenom;
            var lisEmpl = ConnectionClassClinique.ListeDesEmployees("num_empl", certificat.IDEmploye);
           
            graphic.DrawString("CERTIFICAT MEDICAL DE DECES", fnt3, Brushes.Black, 5 * unite_largeur, 5 * unite_hauteur-7 );
            graphic.DrawString(lisEmpl[0].NomEmployee.ToUpper(), fnt22, Brushes.Black, 8 * unite_largeur, 7 * unite_hauteur-5 );
            graphic.DrawString("Je soussignée, ....................................................................................................................................... ,",
                fnt1, Brushes.Black, unite_largeur, 7 * unite_hauteur );
            graphic.DrawString("médecin  de l’Hopital Notre Dame des Apôtres (N.D.A) de N’djaména atteste que ....................",fnt1, Brushes.Black, unite_largeur, 8* unite_hauteur );
            graphic.DrawString(nomPatient.ToUpper(), fnt22, Brushes.Black, 2*unite_largeur, 9 * unite_hauteur-5);
            graphic.DrawString(patient.An.ToString(), fnt22, Brushes.Black, 17 * unite_largeur, 9 * unite_hauteur - 5);
            graphic.DrawString(patient.Sexe.ToString(), fnt22, Brushes.Black, 23 * unite_largeur, 9 * unite_hauteur - 5);
           
            graphic.DrawString("............................................................................................... Âgé(e) de ................. et de sexe ...............", fnt1, Brushes.Black, unite_largeur, 9 * unite_hauteur);

            graphic.DrawString("A été hospitalisé(e) au service de ................................................................................................................................",
                fnt1, Brushes.Black, unite_largeur, 10 * unite_hauteur );

            graphic.DrawString(certificat.DateHospitalisation.Date.ToShortDateString(), fnt22, Brushes.Black, 6 * unite_largeur, 11 * unite_hauteur - 5);
            graphic.DrawString(certificat.DateHospitalisation.ToShortTimeString(), fnt22, Brushes.Black, 18 * unite_largeur, 11 * unite_hauteur - 5);
            graphic.DrawString(certificat.DateDeces.ToShortDateString(), fnt22, Brushes.Black, 8 * unite_largeur, 12 * unite_hauteur - 5);
            graphic.DrawString(certificat.DateDeces.ToShortTimeString(), fnt22, Brushes.Black, 20 * unite_largeur, 12 * unite_hauteur - 5);
            graphic.DrawString(certificat.CauseDeces.ToUpper(), fnt22, Brushes.Black, 7 * unite_largeur, 13 * unite_hauteur - 5);
            graphic.DrawString(certificat.Service.ToUpper(), fnt22, Brushes.Black, 13 * unite_largeur, 10 * unite_hauteur - 5);
           
            graphic.DrawString("Le...............................................................................................à....................................................................",
                fnt1, Brushes.Black, unite_largeur, 11 * unite_hauteur);
            graphic.DrawString("Et décédé(e) le ............................................................................ à ..................................................................................",
               fnt1, Brushes.Black, unite_largeur, 12 * unite_hauteur);
            graphic.DrawString("Suite à ............................................................................................................................................................",
               fnt1, Brushes.Black, unite_largeur, 13 * unite_hauteur);

            graphic.DrawString("En foi de quoi ce certificat de décès lui est délivré pour servir et valoir ce que de droit. " , fnt1, Brushes.Black, unite_largeur, 14 * unite_hauteur);


            graphic.DrawString("N’Djamena, le " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 16 * unite_hauteur);


            return bitmap;
        }
                

        public static Bitmap ListeDesExamen(DataGridView dgvView , int start)
        {
            Bitmap bitmap = null;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur+2;
                int hauteur_facture = 51 * unite_hauteur ;

                //creer un bit map
                bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                try
                {
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 5 * unite_hauteur);
                }
                catch { }
                //definir les polices 
                Font fnt1 = new Font("Century Gothic", 10, FontStyle.Regular);
                Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
                Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
                Font fnt3 = new Font("Century Gothic", 20, FontStyle.Bold);
                Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);
                #endregion
                graphic.DrawString("Liste des examens du " + DateTime.Now, fnt1, Brushes.Black, unite_largeur + 5, 7 * unite_hauteur);

                graphic.FillRectangle(Brushes.DodgerBlue, unite_largeur, 9*unite_hauteur-2, unite_largeur * 2 - 3, unite_hauteur +2);
                graphic.FillRectangle(Brushes.DodgerBlue, unite_largeur * 3, 9 * unite_hauteur - 2, unite_largeur * 18 - 3, unite_hauteur + 2);
                graphic.FillRectangle(Brushes.DodgerBlue, unite_largeur * 21 - 1, 9 * unite_hauteur - 2, unite_largeur * 3 - 3, unite_hauteur + 2);
                
                graphic.DrawString("N°".ToUpper(), fnt1, Brushes.White, unite_largeur + 15, 9 * unite_hauteur);
                graphic.DrawString("TYPE D'EXAMEN ".ToUpper(), fnt1, Brushes.White, 3* unite_largeur +15, 9 * unite_hauteur);
                graphic.DrawString("FRAIS".ToUpper(), fnt1, Brushes.White, 21 * unite_largeur + 20, 9 * unite_hauteur);
                
                var j = 0;
                for (var i = start * 40; i <= dgvView.Rows.Count - 1; i++)
                {
                    var Yloc = unite_hauteur * j + 10 * unite_hauteur;

                    graphic.FillRectangle(Brushes.WhiteSmoke, unite_largeur, Yloc, unite_largeur * 2 - 3, unite_hauteur - 2);
                    graphic.FillRectangle(Brushes.WhiteSmoke, unite_largeur * 3, Yloc, unite_largeur * 18 - 3, unite_hauteur - 2);
                    graphic.FillRectangle(Brushes.WhiteSmoke, unite_largeur * 21 - 1, Yloc, unite_largeur * 3 - 3, unite_hauteur - 2);


                    if (dgvView.Rows[i].Cells[0].Value.ToString().Length > 70)

                        graphic.DrawString(dgvView.Rows[i].Cells[1].Value.ToString().Substring(0, 70) + "...", fnt1, Brushes.Black, 3 * unite_largeur, Yloc);
                    else
                        graphic.DrawString(dgvView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 3 * unite_largeur + 15, Yloc);

                    graphic.DrawString((i+1).ToString(), fnt1, Brushes.Black, 15+unite_largeur, Yloc);
                    graphic.DrawString(dgvView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur+20, Yloc);
                    j++;
                }
                graphic.FillRectangle(Brushes.White, unite_largeur , 50*unite_hauteur, unite_largeur * 24 - 3, unite_hauteur - 2);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Imprimer liste", ex);
            }
            return bitmap;
        }


        /********************************************************************/
        public static Bitmap DetailRapportDeLaCaisse(DataGridView dtGrid,string titre,  double montantTotal)
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
                if (dtGrid.Rows.Count <= 35)
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
                try
                {
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 24 * unite_largeur, 7 * unite_hauteur);
                }
                catch { }

                //definir les polices 
                Font fnt1 = new Font("Century Gothic", 8, FontStyle.Regular);
                Font fnt33 = new Font("Century Gothic", 10, FontStyle.Regular);
                Font fnt0 = new Font("Century Gothic", 8, FontStyle.Regular);
                Font fnt11 = new Font("Century Gothic", 10, FontStyle.Bold);
                Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
                Font fnt3 = new Font("Century Gothic", 14, FontStyle.Bold);
                Font fnt22 = new Font("Century Gothic", 9, FontStyle.Regular);

                #endregion

                graphic.DrawString("Page 1", fnt0, Brushes.Black, 23 * unite_largeur, 5);

                //graphic.FillRectangle(Brushes.Black, unite_largeur - 5, 10 * unite_hauteur, 24 * unite_largeur, 2 * unite_hauteur);

                graphic.DrawString(titre, fnt3, Brushes.Black,  unite_largeur, 10 * unite_hauteur - 15);
                //graphic.DrawLine(Pens.Black, 6 * unite_largeur, 18 * unite_hauteur - 10, 15 * unite_largeur, 18 * unite_hauteur - 10);
                graphic.DrawString("Date d'émission :  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 12 * unite_largeur, 7 * unite_hauteur+15);
             
                graphic.FillRectangle(Brushes.Black, unite_largeur - 3, 14 * unite_hauteur - 15, 24 * unite_largeur + 2, unite_hauteur);
                graphic.DrawString("Nom & Prenom  ", fnt1, Brushes.White, unite_largeur, 14 * unite_hauteur - 15);
                graphic.DrawString("Date  ", fnt1, Brushes.White, 9 * unite_largeur, 14 * unite_hauteur - 15);
                graphic.DrawString("Désignation ", fnt1, Brushes.White, 13 * unite_largeur, 14 * unite_hauteur - 15);
                graphic.DrawString("Montant  ", fnt1, Brushes.White, 23 * unite_largeur, 14 * unite_hauteur - 15);

                for (var i = 0; i <= dtGrid.Rows.Count - 1; i++)
                {

                    var Yloc = unite_hauteur * i + 15 * unite_hauteur;
                    var patient = dtGrid.Rows[i].Cells[1].Value.ToString().ToUpper();
                  
                    graphic.DrawString(patient, fnt0, Brushes.Black, unite_largeur - 5, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString(), fnt0, Brushes.Black, 9 * unite_largeur, Yloc);
                       
                    if (dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper() == "SOUS TOTAL")
                    {
                        graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.Black, 13 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper(), fnt11, Brushes.Black, 23 * unite_largeur, Yloc);
                    }
                    else
                    {
                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 40)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 40).ToUpper() + "...",
                                fnt0, Brushes.Black, 13 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt0,
                                Brushes.Black, 13 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 23 * unite_largeur, Yloc);
                    }
                }

                var Kloc = 50 * unite_hauteur;
                if (flag == true)
                {
                    graphic.FillRectangle(Brushes.Black, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 5);
                    graphic.DrawString("Total ", fnt11, Brushes.White, unite_largeur, Kloc);
                    graphic.DrawString(montantTotal.ToString() + " F CFA", fnt11, Brushes.White, 17 * unite_largeur, Kloc);
                    graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.Black, 20, Kloc + 2 * unite_hauteur);
                    graphic.DrawString(Converti((long)montantTotal) + " Francs CFA", fnt1, Brushes.Black, 10 * unite_largeur, Kloc + 2 * unite_hauteur);
                    //graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.Black, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                    //graphic.DrawString(GestionAcademique.LoginFrm.nom.ToUpper(), fnt1, Brushes.Black, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Impression ", exception);
                return null;
            }
            return bitmap;
        }

        public static Bitmap DetailRapportDeLaCaisseParPage(DataGridView dtGrid,  double montantTotal, int start)
        {
            Bitmap bitmap;
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 35 * unite_largeur;
                //int hauteur_facture = 31 * unite_hauteur;//+ 15 + dtGrid.Rows.Count * unite_hauteur;

                int hauteur_facture = 0;
                bool flag = false;
                if (dtGrid.Rows.Count <= 35 + 45 * (1 + start))
                {
                    hauteur_facture = 55 * unite_hauteur + 15;
                    flag = true;
                }
                else
                {
                    hauteur_facture = 47 * unite_hauteur + 15;
                    flag = false;
                }

                //creer un bit map
                bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);

                //definir les polices 
                Font fnt1 = new Font("Century Gothic", 8, FontStyle.Regular);
                Font fnt33 = new Font("Century Gothic", 10, FontStyle.Regular);
                Font fnt0 = new Font("Century Gothic", 8, FontStyle.Regular);
                Font fnt11 = new Font("Century Gothic", 10, FontStyle.Bold);
                Font fnt2 = new Font("Bodoni MT", 28, FontStyle.Bold);
                Font fnt3 = new Font("Century Gothic", 18, FontStyle.Bold);
                Font fnt22 = new Font("Century Gothic", 9, FontStyle.Regular);

                #endregion

                graphic.DrawString("Page " + (start + 2).ToString(), fnt0, Brushes.Black, 23 * unite_largeur, 10);

                graphic.FillRectangle(Brushes.Black, unite_largeur - 3, 2 * unite_hauteur - 15, 24 * unite_largeur + 2, unite_hauteur);
                graphic.DrawString("Nom & Prenom  ", fnt1, Brushes.White, unite_largeur, 2 * unite_hauteur - 15);
                graphic.DrawString("Date  ", fnt1, Brushes.White, 9 * unite_largeur, 2 * unite_hauteur - 15);
                graphic.DrawString("Désignation ", fnt1, Brushes.White, 13 * unite_largeur, 2 * unite_hauteur - 15);
                graphic.DrawString("Montant  ", fnt1, Brushes.White, 23 * unite_largeur, 2 * unite_hauteur - 15);

                var j = 0;
                for (var i = 35+ start * 45; i <= dtGrid.Rows.Count - 1; i++)
                {
                    var Yloc = unite_hauteur * j + 3 * unite_hauteur;
                    var patient = dtGrid.Rows[i].Cells[1].Value.ToString().ToUpper();

                    graphic.DrawString(patient, fnt0, Brushes.Black, unite_largeur - 5, Yloc);
                    graphic.DrawString(dtGrid.Rows[i].Cells[0].Value.ToString(), fnt0, Brushes.Black, 9 * unite_largeur, Yloc);

                    if (dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper() == "SOUS TOTAL")
                    {
                        graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.Black, 13 * unite_largeur, Yloc);
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString().ToUpper(), fnt11, Brushes.Black, 23 * unite_largeur, Yloc);
                    }
                    else
                    {
                        if (dtGrid.Rows[i].Cells[2].Value.ToString().Length > 40)
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().Substring(0, 40).ToUpper() + "...",
                                fnt0, Brushes.Black, 13 * unite_largeur, Yloc);
                        }
                        else
                        {
                            graphic.DrawString(dtGrid.Rows[i].Cells[2].Value.ToString().ToUpper(), fnt0,
                                Brushes.Black, 13 * unite_largeur, Yloc);
                        }
                        graphic.DrawString(dtGrid.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 23 * unite_largeur, Yloc); j++;
                    }
                }

                var Kloc = 50 * unite_hauteur;
                if (flag == true)
                {
                    graphic.FillRectangle(Brushes.Black, 20, Kloc, 24 * unite_largeur + 10, unite_hauteur + 5);
                    graphic.DrawString("Total ", fnt11, Brushes.White, unite_largeur, Kloc);
                    graphic.DrawString(montantTotal.ToString() + " F CFA", fnt11, Brushes.White, 17 * unite_largeur, Kloc);
                    graphic.DrawString("Arrêtée la présente facture à la somme de : ", fnt1, Brushes.Black, 20, Kloc + 2 * unite_hauteur);
                    graphic.DrawString(Converti((long)montantTotal) + " Francs CFA", fnt1, Brushes.Black, 10 * unite_largeur, Kloc + 2 * unite_hauteur);
                    //graphic.DrawString("Le Service Administratif et Financier".ToUpper(), fnt1, Brushes.Black, 10 * unite_largeur, Kloc + 3 * unite_hauteur + 7);
                    //graphic.DrawString(GestionAcademique.LoginFrm.nom.ToUpper(), fnt1, Brushes.Black, 10 * unite_largeur, Kloc + 4 * unite_hauteur + 5);
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Impression ", exception);
                return null;
            }
            return bitmap;
        }

        /********************************************************************/


        public static Bitmap ImprimerRapportDepenses(ListView dgvDepenses, string titre)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 48 * unite_hauteur + 5;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, 2 * unite_hauteur, 23 * unite_largeur, 6 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 13, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 14, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            // dessiner les ecritures 
            graphic.DrawString("Page 1 ", fnt33, Brushes.Black, 12 * unite_largeur, 10);

            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur, 6 * unite_hauteur);

            graphic.FillRectangle(Brushes.Black, unite_largeur, 10 * unite_hauteur, 23 * unite_largeur + 10,  unite_hauteur);

            graphic.DrawString("DATE", fnt1, Brushes.White,  2*unite_largeur, 10 * unite_hauteur);
            graphic.DrawString("DESIGNATION ", fnt1, Brushes.White, 10 * unite_largeur, 10 * unite_hauteur);
            graphic.DrawString("MONTANT", fnt1, Brushes.White, 20 * unite_largeur, 10 * unite_hauteur);

            int Loc = 20;
            decimal montant = 0;
            for (int i = 0; i <= dgvDepenses.Items.Count - 1; i++)
            {
                int Yloc = unite_hauteur * i + 11 * unite_hauteur;

                graphic.DrawRectangle(Pens.Black, unite_largeur  , Yloc, unite_largeur * 6 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 7, Yloc, unite_largeur * 11 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 18, Yloc, unite_largeur * 6 , unite_hauteur);

                graphic.DrawString(DateTime.Parse(dgvDepenses.Items[i].SubItems[1].Text).ToShortDateString(), fnt1, Brushes.Black, 3 * unite_largeur + 5, Yloc);
                graphic.DrawString(dgvDepenses.Items[i].SubItems[2].Text, fnt1, Brushes.Black, 8 * unite_largeur + 5, Yloc);
                graphic.DrawString(dgvDepenses.Items[i].SubItems[3].Text, fnt1, Brushes.Black, 21 * unite_largeur + 5, Yloc);
                montant += Decimal.Parse(dgvDepenses.Items[i].SubItems[3].Text);
            }
            Loc += dgvDepenses.Items.Count * unite_hauteur + 10 * unite_hauteur;

            graphic.FillRectangle(Brushes.Black, unite_largeur, 45 * unite_hauteur, 23 * unite_largeur + 10, unite_hauteur);

            graphic.DrawString("Total :  " + montant.ToString() + " F CFA", fnt11, Brushes.White, unite_largeur, 45 * unite_hauteur );
            return bitmap;
        }

        public static Bitmap RapportRecuPaiementDunMedecin(DataGridView dtGrid1, DataGridView dtGrid2,string employe, DateTime dt1, DateTime dt2)
        {
            try
            {
                #region facture1
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 25 * unite_largeur;
                int detail_hauteur_facture = 14 * unite_hauteur;
                int hauteur_facture = 42 * unite_hauteur + detail_hauteur_facture;
                
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                try
                {
                    Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                    graphic.DrawImage(logo, unite_largeur, unite_hauteur, 24 * unite_largeur, 7 * unite_hauteur);
                }
                catch { }
                //definir les polices   
                Font fnt1 = new Font("Century Gothic", 10, FontStyle.Regular);
                Font fnt2 = new Font("Century Gothic", 10, FontStyle.Bold);
                Font fnt11 = new Font("Century Gothic", 8, FontStyle.Regular);
                Font fnt3 = new Font("Century Gothic", 14, FontStyle.Bold | FontStyle.Underline);
                #endregion

                graphic.DrawString("N'Djamena le :  " + DateTime.Now.ToString(), fnt1, Brushes.Black, 2*unite_largeur, 12 * unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, unite_largeur , 10*unite_hauteur-5, 24 * unite_largeur , 4*unite_hauteur);

                  graphic.DrawString("Medecin : " + employe.ToUpper(), fnt1, Brushes.Black,  13*unite_largeur+10, 12 * unite_hauteur+10);

                  graphic.DrawString("RAPPORT DES ACTES DU " + dt1.ToShortDateString() +" AU " + dt2.ToShortDateString(), fnt3, Brushes.Black, unite_largeur*6, 11 * unite_hauteur -10);

                  graphic.DrawRectangle(Pens.Black, unite_largeur, 15 * unite_hauteur - 15, 11 * unite_largeur+15, 1 * unite_hauteur + 20);
                  graphic.DrawRectangle(Pens.Black, 12*unite_largeur+15, 15 * unite_hauteur - 15, 1 * unite_largeur+17, 1 * unite_hauteur + 20);
                  graphic.DrawRectangle(Pens.Black, 14*unite_largeur, 15 * unite_hauteur - 15, 3 * unite_largeur, 1 * unite_hauteur + 20);
                  graphic.DrawRectangle(Pens.Black, 17*unite_largeur, 15 * unite_hauteur - 15, 3 * unite_largeur+15, 1 * unite_hauteur + 20);
                  graphic.DrawRectangle(Pens.Black, 20*unite_largeur+15, 15 * unite_hauteur - 15,  unite_largeur, 1 * unite_hauteur + 20);
                 graphic.DrawRectangle(Pens.Black, 21*unite_largeur+15, 15 * unite_hauteur - 15, 3 * unite_largeur+17, 1 * unite_hauteur + 20);

                 graphic.DrawRectangle(Pens.Black, unite_largeur, 15 * unite_hauteur - 15, 11 * unite_largeur + 15, 36 * unite_hauteur + 20);
                 graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 15 * unite_hauteur - 15, 1 * unite_largeur + 17, 36 * unite_hauteur + 20);
                 graphic.DrawRectangle(Pens.Black, 14 * unite_largeur, 15 * unite_hauteur - 15, 3 * unite_largeur, 36 * unite_hauteur + 20);
                 graphic.DrawRectangle(Pens.Black, 17 * unite_largeur, 15 * unite_hauteur - 15, 3 * unite_largeur + 15, 36 * unite_hauteur + 20);
                 graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 15, 15 * unite_hauteur - 15, unite_largeur, 36 * unite_hauteur + 20);
                 graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 15, 15 * unite_hauteur - 15, 3 * unite_largeur + 17, 36 * unite_hauteur + 20);

                 graphic.DrawString("ACTES ".ToUpper(), fnt1, Brushes.Black, unite_largeur+10, 15 * unite_hauteur);
                 graphic.DrawString("NBRE  ", fnt1, Brushes.Black, 12 * unite_largeur + 25, 15 * unite_hauteur);
                 graphic.DrawString("FRAIS  ", fnt1, Brushes.Black, 14 * unite_largeur+10, 15 * unite_hauteur);
                 graphic.DrawString("TOTAL ", fnt1, Brushes.Black, 17 * unite_largeur + 10, 15 * unite_hauteur);
                graphic.DrawString("% ", fnt1, Brushes.Black, 20 * unite_largeur + 25, 15 * unite_hauteur);
                graphic.DrawString("PART MEDECIN ", fnt1, Brushes.Black, 21 * unite_largeur + 25, 15 * unite_hauteur);

                var total = 0.0;
                var partDocteur = 0.0;
                var Yloc = 16 * unite_hauteur + 15;
                for (var i = 0; i <= dtGrid1.Rows.Count - 1; i++)
                {
                     Yloc += unite_hauteur * i;


                    graphic.DrawString(dtGrid1.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur+15, Yloc);
                    graphic.DrawString(dtGrid1.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 12 * unite_largeur + 25, Yloc);
                    graphic.DrawString(dtGrid1.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur+10, Yloc);
                    graphic.DrawString(dtGrid1.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 10, Yloc);
                    graphic.DrawString(dtGrid1.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 25, Yloc);
                    graphic.DrawString(dtGrid1.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur+25, Yloc);

                }

                for (var j = 0; j <= dtGrid2.Rows.Count - 1; j++)
                {
                    Yloc +=unite_hauteur;

                    graphic.DrawString(dtGrid2.Rows[j].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur+15, Yloc);
                    graphic.DrawString(dtGrid2.Rows[j].Cells[1].Value.ToString(), fnt1, Brushes.Black, 12 * unite_largeur + 25, Yloc);
                    graphic.DrawString(dtGrid2.Rows[j].Cells[2].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur+10, Yloc);
                    graphic.DrawString(dtGrid2.Rows[j].Cells[3].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 10, Yloc);
                    graphic.DrawString(dtGrid2.Rows[j].Cells[4].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 25, Yloc);
                    graphic.DrawString(dtGrid2.Rows[j].Cells[5].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur+25, Yloc);
                }
                for (var l = 0; l < dtGrid1.Rows.Count; l++)
                {
                    total += Double.Parse(dtGrid1.Rows[l].Cells[3].Value.ToString());
                    partDocteur += Double.Parse(dtGrid1.Rows[l].Cells[5].Value.ToString());
                }
                for (var l = 0; l < dtGrid2.Rows.Count; l++)
                {
                    total += Double.Parse(dtGrid2.Rows[l].Cells[3].Value.ToString());
                    partDocteur += Double.Parse(dtGrid2.Rows[l].Cells[5].Value.ToString());
                }
                graphic.DrawRectangle(Pens.Black, unite_largeur, 51 * unite_hauteur+5, 24 * unite_largeur , unite_hauteur+15);

                 graphic.DrawString("TOTAL  ", fnt2, Brushes.Black, unite_largeur + 10, 51 * unite_hauteur+15);
                    graphic.DrawString(total.ToString() + " F CFA", fnt2, Brushes.Black, 17 * unite_largeur +15, 51 * unite_hauteur+15);
                    graphic.DrawString(partDocteur.ToString() + " F CFA", fnt2, Brushes.Black, 22 * unite_largeur, 51 * unite_hauteur+15);
                
                return bitmap;
            }
            catch
            {
                return null;
            }
        }

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
                Image logo = global::GestionDuneClinique.Properties.Resources.logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur, 23 * unite_largeur, 6 * unite_hauteur);
            }
            catch (Exception)
            { }
           
            graphic.DrawString("Page " + (start + 1).ToString(), fnt33, Brushes.Black, 15 * unite_largeur, 5);

            graphic.DrawString(titre, fnt1, Brushes.Black, unite_largeur, 8 * unite_hauteur );

            graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, 10 * unite_hauteur - 9, 23 * unite_largeur, unite_hauteur + 10);
            graphic.DrawString(dgvRapport.Columns[0].HeaderText, fnt1, Brushes.White, 10 + unite_largeur,10 * unite_hauteur - 10);
            graphic.DrawString(dgvRapport.Columns[1].HeaderText, fnt1, Brushes.White, 8 * unite_largeur, 10 * unite_hauteur - 5);
            graphic.DrawString(dgvRapport.Columns[2].HeaderText, fnt1, Brushes.White, 20 * unite_largeur, 10 * unite_hauteur - 5);
    
            var j = 0;
            for (var i = start * 45; i <= dgvRapport.Rows.Count - 1; i++)
            {
                var Yloc = unite_hauteur * j + 11* unite_hauteur;

                graphic.DrawLine(Pens.Salmon, unite_largeur, Yloc, unite_largeur * 24, Yloc);

                if (i % 2 == 1)
                    graphic.FillRectangle(Brushes.PeachPuff, unite_largeur, Yloc, unite_largeur * 23, unite_hauteur);
                
                graphic.DrawString(dgvRapport.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 10, Yloc);
                    graphic.DrawString(dgvRapport.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 8 * unite_largeur + 15, Yloc);
                    if (dgvRapport.Columns[2].Visible==true)
                    {
                        graphic.DrawString(dgvRapport.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur, Yloc);
                    }
                j++;
            }

            return bitmap;
        }

        public static Bitmap ImprimerStatistiqueDeConsultation(DataGridView dgvConsultation, DateTime dt1 , DateTime dt2)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 36 * unite_largeur;
            int hauteur_facture = 33 * unite_hauteur + 7;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            
            Font fnt1 = new Font("Arial Narrow", 12, FontStyle.Regular);
            Font fnt33 = new Font("Arial Narrow", 13, FontStyle.Bold);
            Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 13.5f, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            //Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);
            #endregion

            graphic.DrawString("STATISTIQUE ET RELEVE EPIDEMIOLOGIQUE DU CENTRE MEDICAL SOS VILLAGES ENFANTS DE N'DJAMENA DU "
                + dt1.ToShortDateString() + " AU " + dt2.ToShortDateString(), fnt3, Brushes.Black,  10,unite_hauteur);

            graphic.DrawRectangle(Pens.Black, 10, 2 * unite_hauteur + 10, 9 * unite_largeur-10 , 2 * unite_hauteur );
            graphic.DrawRectangle(Pens.Black, 9 * unite_largeur, 2 * unite_hauteur + 10, 4 * unite_largeur +12, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 13 * unite_largeur+12, 2 * unite_hauteur + 10, 4 * unite_largeur +12, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 17 * unite_largeur + 24, 2 * unite_hauteur + 10, 4 * unite_largeur + 12, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 4, 2 * unite_hauteur + 10, 13 * unite_largeur + 4, 2 * unite_hauteur);

            #region Columns
            for (var rows = 0; rows < 2; rows++)
            {
                for (var cols = 0; cols < dgvConsultation.Columns.Count; cols++)
                {
                    var columns = dgvConsultation.Columns[cols].HeaderText;
                    if (columns.ToLower().Contains(" "))
                    {
                        columns = columns.Substring(0, columns.IndexOf(" ")) +
                            "\n " + columns.Substring(columns.IndexOf(" ") + 1);
                    }
                    else if (columns.ToLower().Contains(" "))
                    {
                        columns = columns.Substring(0, columns.IndexOf(" ")) +
                            "\n " + columns.Substring(columns.IndexOf(" ") + 1);
                    }

                    var xLoc = 0;
                    if (cols == 0)
                    {
                        xLoc = 18 + unite_largeur * cols * 7;
                    }
                    else if ( cols > 0 && cols < 7)
                    {
                        xLoc = unite_largeur * 7 + 23 * cols * 3;
                    }
                    else if (cols > 7 && cols < dgvConsultation.Columns.Count - 1)
                    {
                        xLoc = unite_largeur * 7 + 23 * cols * 3;
                    }
                    else if (cols== 7 )
                    {
                        xLoc = unite_largeur *13+ 23 * cols * 3;
                    }

                    graphic.DrawString(columns
                        , fnt33, Brushes.Black, xLoc, 2 * unite_hauteur + 15);
                }
            }
            #endregion

            #region Rows
            for (var rows = 0; rows < dgvConsultation.Rows.Count; rows++)
            {
                var yLoc = 4*unite_hauteur +10+ 30 * rows;

                graphic.DrawRectangle(Pens.Black, 10,  yLoc , 9 * unite_largeur - 10, 30);
                graphic.DrawRectangle(Pens.Black, 9 * unite_largeur,  yLoc , 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 11 * unite_largeur+6, yLoc, 2 * unite_largeur + 6, 30);    
                graphic.DrawRectangle(Pens.Black, 13 * unite_largeur + 12, yLoc , 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 18, yLoc, 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 17 * unite_largeur + 24, yLoc, 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 30, yLoc, 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 4, yLoc, 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 24 * unite_largeur+10, yLoc, 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 26 * unite_largeur + 16, yLoc, 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 28 * unite_largeur + 22, yLoc, 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 28, yLoc, 2 * unite_largeur + 6, 30);
                graphic.DrawRectangle(Pens.Black, 33 * unite_largeur+2 , yLoc, 2 * unite_largeur + 6, 30);
                //graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 30, yLoc, 2 * unite_largeur + 6, 30);

                if (rows == 0)
                {
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[0].Value.ToString()
                         , fnt33, Brushes.Black, 18, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[1].Value.ToString()
                         , fnt33, Brushes.Black, unite_largeur * 10 - 5, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[2].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 12 - 1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[3].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 14 + 5, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[4].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 16 + 11, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[5].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 18 - 4, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[6].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 20 , yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[7].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 22+15 , yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[8].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 24 +24, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[9].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 26+20 , yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[10].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 28 +22, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[11].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 31-5 , yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[12].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 33 + 6, yLoc + 5);
                }
                else
                {
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[0].Value.ToString()
                          , fnt33, Brushes.Black, 18, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[1].Value.ToString()
                         , fnt33, Brushes.Black, unite_largeur * 10 - 5, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[2].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 12 - 1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[3].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 14 + 5, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[4].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 16 + 11, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[5].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 18 + 17, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[6].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 20 + 23, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[7].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 22 + 29, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[8].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 25 + 3, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[9].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 27 + 9, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[10].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 29 + 15, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[11].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 31 + 21, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[12].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 33 + 27, yLoc + 5);
                }
            }
            #endregion
       
            return bitmap;
        }

        public static Bitmap ImprimerStatistiqueDeLaboratoire(DataGridView dgvConsultation, DateTime dt1, DateTime dt2)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 36 * unite_largeur+2;
            int hauteur_facture = 33 * unite_hauteur + 7;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            Font fnt1 = new Font("Arial Narrow", 12, FontStyle.Regular);
            Font fnt33 = new Font("Arial Narrow", 13, FontStyle.Bold);
            Font fnt0 = new Font("Arial Narrow", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 14.0f, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            //Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);
            #endregion

            graphic.DrawString("STATISTIQUE ET RELEVE DU LABORATOIRE DU CENTRE MEDICAL SOS VILLAGES ENFANTS DE N'DJAMENA DU "
                + dt1.ToShortDateString() + " AU " + dt2.ToShortDateString(), fnt3, Brushes.Black, 10, unite_hauteur-5);

            graphic.DrawRectangle(Pens.Black, 10, 2 * unite_hauteur + 10, 9 * unite_largeur - 10, 2 * unite_hauteur+30);
            graphic.DrawRectangle(Pens.Black, 9 * unite_largeur, 2 * unite_hauteur + 10, 6 * unite_largeur , 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 2 * unite_hauteur + 10, 6 * unite_largeur , 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 21 * unite_largeur, 2 * unite_hauteur + 10, 15 * unite_largeur+1 , 2 * unite_hauteur+30);
            //graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 4, 2 * unite_hauteur + 10, 13 * unite_largeur + 4, 2 * unite_hauteur);

            #region Columns
            for (var rows = 0; rows < 2; rows++)
            {
                for (var cols = 0; cols < dgvConsultation.Columns.Count; cols++)
                {
                    var columns = dgvConsultation.Columns[cols].HeaderText;
                    if (columns.ToLower().Contains(" "))
                    {
                        columns = columns.Substring(0, columns.IndexOf(" ")) +
                            "\n " + columns.Substring(columns.IndexOf(" ") + 1);
                    }
                    else if (columns.ToLower().Contains(" "))
                    {
                        columns = columns.Substring(0, columns.IndexOf(" ")) +
                            "\n " + columns.Substring(columns.IndexOf(" ") + 1);
                    }

                    var xLoc = 0;
                    if (cols == 0)
                    {
                        xLoc = 3 * unite_largeur - 15 + unite_largeur * cols * 7;
                        graphic.DrawString(columns
                            , fnt33, Brushes.Black, xLoc, 3 * unite_hauteur );
                    }
                    else if (cols > 0 && cols <= 4)
                    {
                        xLoc = unite_largeur * 8 + unite_largeur * cols * 3;
                        graphic.DrawString(columns
                            , fnt33, Brushes.Black, xLoc, 2 * unite_hauteur + 15);
                    }
                    else if (cols > 4 && cols < 9)
                    {
                        xLoc = unite_largeur * 2 + unite_largeur * cols * 3;
                        graphic.DrawString(columns
                            , fnt33, Brushes.Black, xLoc, 2 * unite_hauteur + 15);
                    }
                    else if (cols > 9 && cols < dgvConsultation.Columns.Count - 1)
                    {
                        xLoc = unite_largeur * 8 + unite_largeur * cols * 3;
                        graphic.DrawString(columns
                            , fnt33, Brushes.Black, xLoc, 2 * unite_hauteur + 20);
                    }
                    else if (cols == 9)
                    {
                        xLoc = unite_largeur * 8 + 23 * cols * 3;
                        graphic.DrawString(columns
                            , fnt33, Brushes.Black, xLoc, 3 * unite_hauteur +15);
                    }

                }
            }
            #endregion

            #region Rows
            for (var rows = 0; rows < dgvConsultation.Rows.Count; rows++)
            {
                var yLoc = 4 * unite_hauteur + 10 + 30 * rows;

                #region
                if (rows == 0)
                {
                    graphic.DrawRectangle(Pens.Black, 9 * unite_largeur, yLoc, 3 * unite_largeur , 30);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur , yLoc, 3 * unite_largeur, 30);
                    graphic.DrawRectangle(Pens.Black, 15 * unite_largeur , yLoc, 3 * unite_largeur, 30);
                    graphic.DrawRectangle(Pens.Black, 18 * unite_largeur , yLoc, 3 * unite_largeur, 30);
                   
                    
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[1].Value.ToString()
                         , fnt33, Brushes.Black, unite_largeur * 9 + 15, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[3].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 12 +15, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[5].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 15 + 15, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[7].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 18 + 15, yLoc + 5);
                 
                }
                #endregion
               
                #region
                else if(rows==1)
                {
                    graphic.DrawRectangle(Pens.Black, 10, yLoc, 9 * unite_largeur - 10, 30);
                    graphic.DrawRectangle(Pens.Black, 9 * unite_largeur, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 10 * unite_largeur + 16, yLoc, 1* unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur +0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 13 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 24 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 28 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 31 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 33 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 34 * unite_largeur + 16, yLoc, 1 * unite_largeur + 17, 30);

                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[0].Value.ToString()
                          , fnt33, Brushes.Black, 18, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[1].Value.ToString()
                         , fnt33, Brushes.Black, unite_largeur * 9 +16, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[2].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 11, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[3].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 12 + 9, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[4].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 14 -7, yLoc + 5);

                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[5].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 15 + 14, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[6].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 16 + 30, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[7].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 18 + 10, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[8].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 19 + 25, yLoc + 5);

                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[9].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 21 + 1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[10].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 22 + 16, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[11].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 24 + 1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[12].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 25 + 17, yLoc + 5);

                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[13].Value.ToString()
                , fnt33, Brushes.Black, unite_largeur * 27 + 1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[14].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 28 + 16, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[15].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 30 + 1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[16].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 31 + 17, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[17].Value.ToString()
                       , fnt33, Brushes.Black, unite_largeur * 33 + 1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[18].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 34 + 17, yLoc + 5);
                }
            #endregion

                #region
                else 
                {
                    graphic.DrawRectangle(Pens.Black, 10, yLoc, 9 * unite_largeur - 10, 30);
                    graphic.DrawRectangle(Pens.Black, 9 * unite_largeur, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 10 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 13 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 24 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 28 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 31 * unite_largeur + 16, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 33 * unite_largeur + 0, yLoc, 1 * unite_largeur + 16, 30);
                    graphic.DrawRectangle(Pens.Black, 34 * unite_largeur + 16, yLoc, 1 * unite_largeur + 17, 30);

                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[0].Value.ToString()
                    , fnt33, Brushes.Black, 18, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[1].Value.ToString()
                         , fnt33, Brushes.Black, unite_largeur * 9 + 5, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[2].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 11-11, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[3].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 12 + 5, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[4].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 14 - 11, yLoc + 5);

                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[5].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 15 + 5, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[6].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 16 + 21, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[7].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 18 + 5, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[8].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 19 + 21, yLoc + 5);

                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[9].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 21 + 3, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[10].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 22 + 18, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[11].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 24 + 3, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[12].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 25 + 18, yLoc + 5);

                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[13].Value.ToString()
                , fnt33, Brushes.Black, unite_largeur * 27 + 3, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[14].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 28 + 18, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[15].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 30 + 3, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[16].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 31 + 18, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[17].Value.ToString()
                       , fnt33, Brushes.Black, unite_largeur * 33 + 1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[18].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 34 + 17, yLoc + 5);
                }
                #endregion
           
            }
            #endregion

            return bitmap;
        }


        public static Bitmap ImprimerStatistiqueDePharmacie(DataGridView dgvConsultation, DateTime dt1, DateTime dt2)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 36 * unite_largeur;
            int hauteur_facture = 33 * unite_hauteur + 7;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            Font fnt1 = new Font("Arial Narrow", 12, FontStyle.Regular);
            Font fnt33 = new Font("Arial Narrow", 13, FontStyle.Bold);
            Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 13.5f, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            //Font fnt22 = new Font("Century Gothic", 10, FontStyle.Regular);
            #endregion

            graphic.DrawString("STATISTIQUE ET RELEVE DE LA PHARMACIE DU CENTRE MEDICAL SOS VILLAGES ENFANTS DE N'DJAMENA DU "
                + dt1.ToShortDateString() + " AU " + dt2.ToShortDateString(), fnt3, Brushes.Black, 2*unite_largeur, unite_hauteur);

            graphic.DrawRectangle(Pens.Black, 20, 2 * unite_hauteur + 10, 13 * unite_largeur - 20, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 13 * unite_largeur, 2 * unite_hauteur + 10, 5 * unite_largeur + 0, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 0, 2 * unite_hauteur + 10, 5 * unite_largeur + 0, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 23 * unite_largeur , 2 * unite_hauteur + 10, 12 * unite_largeur + 16, 2 * unite_hauteur);

            #region Columns
            for (var rows = 0; rows < 2; rows++)
            {
                for (var cols = 0; cols < dgvConsultation.Columns.Count; cols++)
                {
                    var columns = dgvConsultation.Columns[cols].HeaderText;
                    if (columns.ToLower().Contains(" "))
                    {
                        columns = columns.Substring(0, columns.IndexOf(" ")) +
                            "\n " + columns.Substring(columns.IndexOf(" ") + 1);
                    }
                    else if (columns.ToLower().Contains(" "))
                    {
                        columns = columns.Substring(0, columns.IndexOf(" ")) +
                            "\n " + columns.Substring(columns.IndexOf(" ") + 1);
                    }

                    var xLoc = 0;
                    if (cols == 0)
                    {
                        xLoc = unite_largeur * 4;
                    }
                    else if (cols == 1)
                    {
                        xLoc = unite_largeur * 15-16;
                    }
                    else if (cols ==3)
                    {
                        xLoc = unite_largeur * 19+16;
                    }
                    else if (cols == 5)
                    {
                        xLoc = unite_largeur * 28 ;
                    }

                    graphic.DrawString(columns
                        , fnt33, Brushes.Black, xLoc, 2 * unite_hauteur + 15);
                }
            }
            #endregion

            #region Rows
            for (var rows = 0; rows < dgvConsultation.Rows.Count; rows++)
            {
                var yLoc = 4 * unite_hauteur + 10 + 30 * rows;

                graphic.DrawRectangle(Pens.Black, 20, yLoc, 13 * unite_largeur - 20, 30);
                graphic.DrawRectangle(Pens.Black, 13 * unite_largeur, yLoc, 2 * unite_largeur + 16, 30);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 16, yLoc, 2 * unite_largeur + 16, 30);
                graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 0, yLoc, 2 * unite_largeur + 16, 30);
                graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 16, yLoc, 2 * unite_largeur + 16, 30);
                graphic.DrawRectangle(Pens.Black, 23 * unite_largeur + 0, yLoc, 2 * unite_largeur + 16, 30);
                graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 16, yLoc, 2 * unite_largeur + 16, 30);
                graphic.DrawRectangle(Pens.Black, 28 * unite_largeur + 0, yLoc, 2 * unite_largeur + 16, 30);
                graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 16, yLoc, 2 * unite_largeur + 16, 30);
                graphic.DrawRectangle(Pens.Black, 33 * unite_largeur , yLoc, 2 * unite_largeur + 16, 30);
                
                if (rows == 0)
                {
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[0].Value.ToString()
                         , fnt33, Brushes.Black, unite_largeur*6, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[1].Value.ToString()
                         , fnt33, Brushes.Black, unite_largeur * 14 +1, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[2].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 16 + 17, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[3].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 19 + 0, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[4].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 21 + 16, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[5].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 23 + 6, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[6].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 25+23, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[7].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 28 + 0, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[8].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 30 +15, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[9].Value.ToString()
                        , fnt0, Brushes.Black, unite_largeur * 33 + 15, yLoc + 5);
                }
                else
                {
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[0].Value.ToString()
                          , fnt33, Brushes.Black, unite_largeur, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[1].Value.ToString()
                         , fnt33, Brushes.Black, unite_largeur * 13+25, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[2].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 16 + 9, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[3].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 18 + 25, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[4].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 21 + 11, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[5].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 23 + 26, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[6].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 26 + 8, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[7].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 28 + 26, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[8].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 31 + 10, yLoc + 5);
                    graphic.DrawString(dgvConsultation.Rows[rows].Cells[9].Value.ToString()
                        , fnt33, Brushes.Black, unite_largeur * 33 + 20, yLoc + 5);
                    //graphic.DrawString(dgvConsultation.Rows[rows].Cells[10].Value.ToString()
                    //    , fnt33, Brushes.Black, unite_largeur * 29 + 15, yLoc + 5);
                    //graphic.DrawString(dgvConsultation.Rows[rows].Cells[11].Value.ToString()
                    //    , fnt33, Brushes.Black, unite_largeur * 31 + 21, yLoc + 5);
                    //graphic.DrawString(dgvConsultation.Rows[rows].Cells[12].Value.ToString()
                    //    , fnt33, Brushes.Black, unite_largeur * 33 + 27, yLoc + 5);
                }
            }
            #endregion

            return bitmap;
        }

    }
}
