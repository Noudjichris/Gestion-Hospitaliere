using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Data;
using System.Linq;

namespace SGSP.AppCode
{
    public class Impression
    {

        //imprimer dossier d'un personnel
        public static Bitmap ImprimerInformatonDunPersonnel(string numeroMatricule)
        {
            var dtPersonnel = ConnectionClass.ListeDesPersonnelParNumeroMatricule(numeroMatricule);
            var dtService = ConnectionClass.ListeDesServices(numeroMatricule);
            var listeSalaire = ConnectionClass.ListeSalaire(numeroMatricule);
            var dtDivision = ConnectionClass.ListeDepartement(dtPersonnel.Rows[0].ItemArray[11].ToString());

            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur ;
            int hauteur_facture = 57 * unite_hauteur ;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            try
            {
                var image = dtPersonnel.Rows[0].ItemArray[10].ToString();
                graphic.DrawImage(Image.FromFile(image), 18 * unite_largeur,4 * unite_hauteur+20, 6* unite_largeur, 10 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt11 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 25, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 12, FontStyle.Regular);

            #endregion

            graphic.FillRectangle(Brushes.SaddleBrown,2* unite_largeur, unite_hauteur, unite_largeur * 22, unite_hauteur+15);
            graphic.DrawString("Details personnel ", fnt3, Brushes.White,9* unite_largeur,  unite_hauteur-5);

                    #region
            graphic.DrawString("Etat civil ", fnt11, Brushes.Black, 2 * unite_largeur, 4 * unite_hauteur - 5);
            graphic.FillRectangle(Brushes.SaddleBrown, 2*unite_largeur, 5* unite_hauteur-5, unite_largeur * 22, 3);

            graphic.DrawString("Matricule :", fnt1, Brushes.Black, 2 * unite_largeur, 6 * unite_hauteur-10);
            graphic.DrawString("Nom :", fnt1, Brushes.Black, 2 * unite_largeur, 7 * unite_hauteur - 10);
            graphic.DrawString("Prenom  :", fnt1, Brushes.Black, 2 * unite_largeur, 8 * unite_hauteur - 10);
            graphic.DrawString("Né(e) le  :", fnt1, Brushes.Black, 2 * unite_largeur, 9 * unite_hauteur - 10);
            graphic.DrawString("à :", fnt1, Brushes.Black, 2 * unite_largeur, 10 * unite_hauteur - 10);
            graphic.DrawString("Sexe :", fnt1, Brushes.Black, 2 * unite_largeur, 11 * unite_hauteur - 10);
            graphic.DrawString("Age :", fnt1, Brushes.Black, 2 * unite_largeur, 12 * unite_hauteur - 10);
            graphic.DrawString("Diplome :", fnt1, Brushes.Black, 2 * unite_largeur, 13 * unite_hauteur-10);

            graphic.DrawString("Adresses et contacts ", fnt11, Brushes.Black, 2 * unite_largeur, 14 * unite_hauteur +5);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 15 * unite_hauteur +5, unite_largeur * 22, 3);
            graphic.DrawString("Adresse :", fnt1, Brushes.Black, 2 * unite_largeur, 16 * unite_hauteur);
            graphic.DrawString("Télephone :", fnt1, Brushes.Black, 2 * unite_largeur, 17 * unite_hauteur);
            graphic.DrawString("Email :", fnt1, Brushes.Black, 2 * unite_largeur, 18 * unite_hauteur);

            graphic.DrawString("Services ", fnt11, Brushes.Black, 2 * unite_largeur, 19 * unite_hauteur + 6);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 20 * unite_hauteur + 5, unite_largeur * 22, 3);
            graphic.DrawString("Récruté(e) le :", fnt1, Brushes.Black, 2 * unite_largeur, 21 * unite_hauteur);
            graphic.DrawString("Au poste de :", fnt1, Brushes.Black, 2 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString("Type de contrat ", fnt1, Brushes.Black, 2 * unite_largeur, 23 * unite_hauteur);
            graphic.DrawString("Service :", fnt1, Brushes.Black, 2 * unite_largeur, 24 * unite_hauteur);
            graphic.DrawString("Catégorie :", fnt1, Brushes.Black, 2 * unite_largeur, 25 * unite_hauteur);
            graphic.DrawString("Echelon :", fnt1, Brushes.Black, 2 * unite_largeur, 26 * unite_hauteur);
            graphic.DrawString("Ancienneté :", fnt1, Brushes.Black, 2 * unite_largeur, 27 * unite_hauteur);
            graphic.DrawString("Etat contrat:", fnt1, Brushes.Black, 2 * unite_largeur, 28 * unite_hauteur);
            graphic.DrawString("Date fin contrat :", fnt1, Brushes.Black, 2 * unite_largeur, 29 * unite_hauteur);
            graphic.DrawString("Etat retraite:", fnt1, Brushes.Black, 2 * unite_largeur, 30 * unite_hauteur);
            graphic.DrawString("Date fin retraite :", fnt1, Brushes.Black, 2 * unite_largeur, 31 * unite_hauteur);


            graphic.DrawString("Services sociaux", fnt11, Brushes.Black, 2 * unite_largeur, 32* unite_hauteur + 6);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 33 * unite_hauteur + 5, unite_largeur * 22, 3);
            graphic.DrawString("No CNPS :", fnt1, Brushes.Black, 2 * unite_largeur, 34 * unite_hauteur);
            //graphic.DrawString("Numéro social : ", fnt1, Brushes.Black, 2 * unite_largeur, 35 * unite_hauteur);

            graphic.DrawString("Renumérations", fnt11, Brushes.Black, 2 * unite_largeur, 36* unite_hauteur + 6);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 37 * unite_hauteur + 5, unite_largeur * 22, 3);
             graphic.DrawString("Salaire brut :", fnt1, Brushes.Black, 2 * unite_largeur, 38 * unite_hauteur);
            graphic.DrawString("Grille salarialle :", fnt1, Brushes.Black, 2 * unite_largeur, 39 * unite_hauteur);
            graphic.DrawString("Indice :", fnt1, Brushes.Black, 2 * unite_largeur, 40 * unite_hauteur);
            graphic.DrawString("Prime de garde :", fnt1, Brushes.Black, 2 * unite_largeur, 41 * unite_hauteur);

            graphic.DrawString("Prime de  transport :", fnt1, Brushes.Black, 12 * unite_largeur, 38 * unite_hauteur);
            graphic.DrawString("Prime de logement  :", fnt1, Brushes.Black, 12 * unite_largeur, 39 * unite_hauteur);
            graphic.DrawString("Prime exceptionnelle :", fnt1, Brushes.Black, 12* unite_largeur, 40 * unite_hauteur);
            graphic.DrawString("Prime de responsabilité : ", fnt1, Brushes.Black, 12 * unite_largeur, 41 * unite_hauteur);

            graphic.DrawString("Service bancaire", fnt11, Brushes.Black, 2 * unite_largeur, 42 * unite_hauteur + 6);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 43 * unite_hauteur + 5, unite_largeur * 22, 3);
            graphic.DrawString("Nom banque :", fnt1, Brushes.Black, 2 * unite_largeur, 44 * unite_hauteur);
            graphic.DrawString("Compte bancaire :", fnt1, Brushes.Black, 2 * unite_largeur, 45 * unite_hauteur);
            graphic.DrawString("Code guichet :", fnt1, Brushes.Black, 2 * unite_largeur, 46 * unite_hauteur);
            graphic.DrawString("Code banque :", fnt1, Brushes.Black, 2 * unite_largeur, 47 * unite_hauteur);
            graphic.DrawString("Clé :", fnt1, Brushes.Black, 2 * unite_largeur, 48* unite_hauteur);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 49 * unite_hauteur + 5, unite_largeur * 22, 3);

            var datenaissance = DateTime.Parse(dtPersonnel.Rows[0].ItemArray[3].ToString());
            var anneeActuel = DateTime.Now.Year;
            var moisActuel = DateTime.Now.Month;
            var mois = datenaissance.Month;
            var age = new int();
            if (moisActuel >= mois)
            {
                age = anneeActuel - datenaissance.Year;
            }
            else
            {
                age = anneeActuel - datenaissance.Year - 1;
            }
          
            #endregion
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[0].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 6 * unite_hauteur-10);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[1].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 7 * unite_hauteur-10);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 8 * unite_hauteur-10);
            graphic.DrawString(datenaissance.ToShortDateString(), fnt33, Brushes.Black, 8 * unite_largeur, 9 * unite_hauteur-10);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[4].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 10 * unite_hauteur-10);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[9].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 11 * unite_hauteur-10);
            graphic.DrawString(age.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 12 * unite_hauteur-10);
            graphic.DrawString(dtService.Rows[0].ItemArray[8].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 13 * unite_hauteur - 10);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[5].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 16 * unite_hauteur);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[6].ToString() + " / " + dtPersonnel.Rows[0].ItemArray[7].ToString()
                , fnt33, Brushes.Black, 8 * unite_largeur, 17 * unite_hauteur);

            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[8].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 18 * unite_hauteur);
            graphic.DrawString(DateTime.Parse(dtService.Rows[0].ItemArray[1].ToString()).ToShortDateString(), fnt33, Brushes.Black, 8 * unite_largeur, 21 * unite_hauteur);
            graphic.DrawString(dtService.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString(dtService.Rows[0].ItemArray[9].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 23 * unite_hauteur);
            graphic.DrawString(dtDivision.Rows[0].ItemArray[1].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 24 * unite_hauteur);
            //graphic.DrawString(dtDivision.Rows[0].ItemArray[1].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 25 * unite_hauteur);

            graphic.DrawString(dtService.Rows[0].ItemArray[5].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 25 * unite_hauteur);
            graphic.DrawString(dtService.Rows[0].ItemArray[4].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 26 * unite_hauteur);
            graphic.DrawString(dtService.Rows[0].ItemArray[6].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 27 * unite_hauteur);
            //graphic.DrawString(dtService.Rows[0].ItemArray[9].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 29 * unite_hauteur);
            var etat = "";
            if (dtService.Rows[0].ItemArray[10].ToString()=="1")
            {
                etat = "Retraité";
                graphic.DrawString(etat, fnt33, Brushes.Red, 8 * unite_largeur, 30 * unite_hauteur);
            }
            else
            {
                etat = "En service";
                graphic.DrawString(etat, fnt33, Brushes.Green, 8 * unite_largeur, 30 * unite_hauteur);
            }
            var etatContrat = "";
            if (dtService.Rows[0].ItemArray[11].ToString() == "1")
            {
                etatContrat = "Fin ";
                graphic.DrawString(etatContrat, fnt33, Brushes.Red, 8 * unite_largeur, 28 * unite_hauteur);
            }
            else
            {
                etatContrat = "En cours";
                graphic.DrawString(etatContrat, fnt33, Brushes.Green, 8 * unite_largeur, 28 * unite_hauteur);
            }

            graphic.DrawString(DateTime.Parse(dtService.Rows[0].ItemArray[12].ToString()).ToShortDateString(), fnt33, Brushes.Black, 8 * unite_largeur, 29 * unite_hauteur);
            graphic.DrawString(DateTime.Parse(dtService.Rows[0].ItemArray[13].ToString()).ToShortDateString(), fnt33, Brushes.Black, 8 * unite_largeur, 31 * unite_hauteur);

            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[15].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 34 * unite_hauteur);
            graphic.DrawString("", fnt33, Brushes.Black, 8 * unite_largeur, 36 * unite_hauteur);
            
            var salaire = .0;
            var grille = 0m;
            var indice = 0m;
            
            if (listeSalaire.Count > 0)
            {
                salaire = listeSalaire[0].SalaireBase;
                grille = listeSalaire[0].GrilleSalarialle;
                indice = listeSalaire[0].Indice;
            }

            graphic.DrawString(salaire.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 38 * unite_hauteur);
            graphic.DrawString(grille.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 39 * unite_hauteur);
            graphic.DrawString(indice.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 40 * unite_hauteur);
            graphic.DrawString(listeSalaire[0].PrimeGarde.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 41 * unite_hauteur);
            graphic.DrawString(listeSalaire[0].PrimeTransport.ToString(), fnt33, Brushes.Black, 20 * unite_largeur, 38 * unite_hauteur);
            graphic.DrawString(listeSalaire[0].PrimeLogement.ToString(), fnt33, Brushes.Black, 20 * unite_largeur, 39 * unite_hauteur);
            graphic.DrawString(listeSalaire[0].PrimeExceptionnel.ToString(), fnt33, Brushes.Black, 20 * unite_largeur, 40 * unite_hauteur);
            graphic.DrawString(listeSalaire[0].PrimeResponsabilite.ToString(), fnt33, Brushes.Black, 20 * unite_largeur, 41 * unite_hauteur);

            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[26].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 44 * unite_hauteur);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[18].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 45 * unite_hauteur);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[23].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 46 * unite_hauteur);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[25].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 47 * unite_hauteur);
            graphic.DrawString(dtPersonnel.Rows[0].ItemArray[24].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 48 * unite_hauteur);

            graphic.FillRectangle(Brushes.White, 2 * unite_largeur, 49* unite_hauteur +9, unite_largeur * 24, 1*unite_hauteur);

            return bitmap;
        }

        //imprimer dossier d'un personnel
        public static Bitmap ImprimerFormation(string numeroMatricule)
        {
            #region
            int unite_hauteur = 21;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int hauteur_facture = 54 * unite_hauteur ;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 11 * unite_largeur, unite_hauteur, 4 * unite_largeur, 3 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 9, FontStyle.Italic);
            Font fnt0 = new Font("Arial Unicode MS", 18, FontStyle.Bold);
            Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 30, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 14, FontStyle.Regular);

            #endregion


            graphic.DrawString("Formation éffectuées ", fnt3, Brushes.SlateGray, unite_largeur, 7 * unite_hauteur);



            //var dtFormation = ConnectionClass.ListeFormation(numeroMatricule);
            //for (var i = 0; i < dtFormation.Rows.Count; i++)
            //{
            //    var Yloc = 4 * unite_hauteur * i + 10 * unite_hauteur;

            //    graphic.FillRectangle(Brushes.Snow, unite_largeur, Yloc, unite_largeur * 24, 3 * unite_hauteur);
            //    graphic.DrawString("Type de formation", fnt11, Brushes.SlateGray, unite_largeur, Yloc);
            //    graphic.DrawString("Date ", fnt11, Brushes.SlateGray, unite_largeur, Yloc + unite_hauteur);
            //    graphic.DrawString("Durée", fnt11, Brushes.SlateGray, unite_largeur, Yloc + 2 * unite_hauteur);
            //    graphic.DrawString(dtFormation.Rows[i].ItemArray[0].ToString(), fnt11, Brushes.SlateGray, 8 * unite_largeur, Yloc);
            //    graphic.DrawString(DateTime.Parse(dtFormation.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
            //        fnt11, Brushes.SlateGray, 8 * unite_largeur, Yloc + unite_hauteur);
            //    graphic.DrawString(dtFormation.Rows[i].ItemArray[2].ToString(), fnt11, Brushes.SlateGray, 8 * unite_largeur, Yloc + 2 * unite_hauteur);
            //    //graphic.DrawString("", fnt11, Brushes.SlateGray, unite_largeur, Yloc + 6 * unite_hauteur);
            //}

            return bitmap;
        }

        public static Bitmap ImprimerLalisteDesPersonnels(DataGridView dgvPersonnel, string titreImpression, int start, string  nombre)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int hauteur_facture = 56 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 10, 5, 16 * unite_largeur+10, 4* unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 10, FontStyle.Regular);
            Font fnt0 = new Font("Arial Narrow", 9, FontStyle.Italic);
            Font fnt11 = new Font("Arial Narrow", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 18, FontStyle.Bold);
            Font fnt33 = new Font("Arial Narrow", 9, FontStyle.Regular);

            graphic.DrawRectangle(Pens.Black, 14, 6 * unite_hauteur + 12, unite_largeur *23-13,45*  (unite_hauteur )+ unite_hauteur+3);

            graphic.DrawString(titreImpression.ToUpper(), fnt11, Brushes.Black, 15, 5 * unite_hauteur + 3);
            graphic.DrawString("Nombre total : " + nombre, fnt33, Brushes.Black, 20*unite_largeur, 5*unite_hauteur +3);
            graphic.DrawRectangle(Pens.Black, 15, 6 * unite_hauteur + 12, unite_largeur + 10, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 2 + 7, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 8, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 5, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 17 * unite_largeur + 0, 6 * unite_hauteur + 12, unite_largeur + 0, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 0, 6 * unite_hauteur + 12, unite_largeur + 0, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 0, 6 * unite_hauteur + 12, unite_largeur + 0, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 0, 6*unite_hauteur+12, unite_largeur * 3 , unite_hauteur+3);
            //graphic.DrawString("N°", fnt11, Brushes.Black, unite_largeur * 1, 6 * unite_hauteur+13);
            graphic.DrawString("Matricule", fnt11, Brushes.Black, unite_largeur * 2-7, 6 * unite_hauteur+13);
            graphic.DrawString("Nom & Prenom", fnt11, Brushes.Black, 4 * unite_largeur + 10, 6 * unite_hauteur + 13);
            graphic.DrawString("Qualification", fnt11, Brushes.Black, 12 * unite_largeur + 10, 6 * unite_hauteur + 13);
            graphic.DrawString("Cat", fnt11, Brushes.Black, 17 * unite_largeur + 0, 6 * unite_hauteur + 13);
            graphic.DrawString("Ech", fnt11, Brushes.Black, 18 * unite_largeur + 0 ,6 * unite_hauteur + 13);
            graphic.DrawString("Anc", fnt11, Brushes.Black, 19 * unite_largeur + 0, 6 * unite_hauteur + 13);
            graphic.DrawString("Type contrat", fnt11, Brushes.Black, 20 * unite_largeur + 3, 6 * unite_hauteur + 13);
            var j = 0;
    
            //var numero = 0;
            //for (var p = 1 + st; p = nombre; p++)
            //{
            //    numero = p;
            //}
            for (var i = start *45; i < dgvPersonnel.Rows.Count; i++)
            {
                int Yloc = unite_hauteur *j  +7* unite_hauteur+15;
                graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur + 0);
                //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 25, Yloc, unite_largeur * 8, unite_hauteur + 0);
                if (string.IsNullOrWhiteSpace(dgvPersonnel.Rows[i].Cells[0].Value.ToString()))
                {
                 graphic.FillRectangle(Brushes.SaddleBrown, 15, Yloc, unite_largeur * 23-14 , unite_hauteur );
                    graphic.DrawString(" Personnels service " + dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.White, unite_largeur * 5 ,Yloc);
                     Yloc += unite_hauteur;
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 25+unite_largeur, Yloc, unite_largeur *2+7, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 4* unite_largeur, Yloc, unite_largeur * 8, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, Yloc, unite_largeur * 5, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 17 * unite_largeur+0, Yloc, unite_largeur + 0, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 18 * unite_largeur+0, Yloc, unite_largeur + 0, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur+0, Yloc, unite_largeur + 0, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 0, Yloc, unite_largeur * 3, unite_hauteur);

                    //graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, unite_largeur , Yloc);
                      graphic.DrawString(dgvPersonnel.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur*2, Yloc);
                      var employe = dgvPersonnel.Rows[i].Cells[1].Value.ToString() + " " + dgvPersonnel.Rows[i].Cells[2].Value.ToString();
                      if (employe.Length > 28)
                      {
                       employe=   employe.Substring(0, 28) + ".";

                      }
                      graphic.DrawString(employe, fnt1, Brushes.Black, 4 * unite_largeur + 10, Yloc + 1);
                      if (dgvPersonnel.Rows[i].Cells[3].Value.ToString().Length > 20)
                      {
                          graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString().Substring(0,20)+"...", fnt33, Brushes.Black, 12 * unite_largeur + 4, Yloc + 1);
                      }
                      else
                      {
                          graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 12 * unite_largeur + 6, Yloc + 1);
                      }
                      graphic.DrawString(dgvPersonnel.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 6, Yloc + 1);
                      graphic.DrawString(dgvPersonnel.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur + 6, Yloc + 1);
                      graphic.DrawString(dgvPersonnel.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 19 * unite_largeur + 6, Yloc + 1);
                      graphic.DrawString(dgvPersonnel.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 10, Yloc + 1);
                  
                }
                graphic.FillRectangle(Brushes.White, 15, 52 * unite_hauteur + 17, unite_largeur * 24 - 14, 8 * unite_hauteur);
                graphic.DrawString("Page " + (start + 1), fnt1, Brushes.Black, 21*unite_largeur+20, 20);

                j++;
            }
            return bitmap;
        }
        public static Bitmap ImprimerLalisteDesPersonnelsContratFinOuRetraite(DataGridView dgvPersonnel, string titreImpression, int start, string nombre)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int hauteur_facture = 56 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 10, 5, 12 * unite_largeur + 10, 3 * unite_hauteur+16);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 10, FontStyle.Regular);
            Font fnt0 = new Font("Arial Narrow", 9, FontStyle.Italic);
            Font fnt11 = new Font("Arial Narrow", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 18, FontStyle.Bold);
            Font fnt33 = new Font("Arial Narrow", 9, FontStyle.Regular);

            graphic.DrawRectangle(Pens.Black, 14, 6 * unite_hauteur + 12, unite_largeur * 23 - 13, 45 * (unite_hauteur) + unite_hauteur + 3);

            graphic.DrawString(titreImpression.ToUpper(), fnt11, Brushes.Black, 15, 5 * unite_hauteur + 3);
            graphic.DrawString("Nombre total : " + nombre, fnt33, Brushes.Black, 20 * unite_largeur, 5 * unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 15, 6 * unite_hauteur + 12, unite_largeur + 10, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 2 + 7, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 8, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 5, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 17 * unite_largeur + 0, 6 * unite_hauteur + 12, unite_largeur *3, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 0, 6 * unite_hauteur + 12, unite_largeur * 3, unite_hauteur + 0);

            graphic.DrawString("Matricule", fnt11, Brushes.Black, unite_largeur * 2 - 7, 6 * unite_hauteur + 13);
            graphic.DrawString("Nom & Prenom", fnt11, Brushes.Black, 4 * unite_largeur + 10, 6 * unite_hauteur + 13);
            graphic.DrawString("Qualification", fnt11, Brushes.Black, 12 * unite_largeur + 10, 6 * unite_hauteur + 13);
            graphic.DrawString("Date service", fnt11, Brushes.Black, 17 * unite_largeur + 3, 6 * unite_hauteur + 13);
            graphic.DrawString("Date", fnt11, Brushes.Black, 21 * unite_largeur + 3, 6 * unite_hauteur + 13);
            var j = 0;

            //var numero = 0;
            //for (var p = 1 + st; p = nombre; p++)
            //{
            //    numero = p;
            //}
            for (var i = start * 45; i < dgvPersonnel.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur + 15;
                graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur + 0);
                //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 25, Yloc, unite_largeur * 8, unite_hauteur + 0);
                if (string.IsNullOrWhiteSpace(dgvPersonnel.Rows[i].Cells[0].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.SaddleBrown, 15, Yloc, unite_largeur * 23 - 14, unite_hauteur);
                    graphic.DrawString(" Personnels service " + dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.White, unite_largeur * 5, Yloc);
                    Yloc += unite_hauteur;
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, Yloc, unite_largeur * 2 + 7, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, Yloc, unite_largeur * 8, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, Yloc, unite_largeur * 5, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 17 * unite_largeur , Yloc, unite_largeur *3, unite_hauteur);
                     graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 0, Yloc, unite_largeur * 3, unite_hauteur);

                    //graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, unite_largeur , Yloc);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 2, Yloc);
                    var employe = dgvPersonnel.Rows[i].Cells[1].Value.ToString() + " " + dgvPersonnel.Rows[i].Cells[2].Value.ToString();
                    if (employe.Length > 28)
                    {
                        employe = employe.Substring(0, 28) + ".";

                    }
                    graphic.DrawString(employe, fnt1, Brushes.Black, 4 * unite_largeur + 10, Yloc + 1);
                    if (dgvPersonnel.Rows[i].Cells[3].Value.ToString().Length > 20)
                    {
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString().Substring(0, 20) + "...", fnt33, Brushes.Black, 12 * unite_largeur + 4, Yloc + 1);
                    }
                    else
                    {
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 12 * unite_largeur + 6, Yloc + 1);
                    }
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 6, Yloc + 1);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 10, Yloc + 1);

                }
                graphic.FillRectangle(Brushes.White, 15, 52 * unite_hauteur + 17, unite_largeur * 24 - 14, 8 * unite_hauteur);
                graphic.DrawString("Page " + (start + 1), fnt1, Brushes.Black, 21 * unite_largeur + 20, 20);

                j++;
            }
            return bitmap;
        }

        public static Bitmap ImprimerLalisteDesPersonnelsAvecSalaire(DataGridView dgvPersonnel, string titreImpression, int start, string nombre)
        {
            try
            {
              
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int hauteur_facture = 56 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
              Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 10, 5, 24 * unite_largeur + 10, 4 * unite_hauteur);
           
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Italic);
            Font fnt11 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 18, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 9, FontStyle.Regular);

            graphic.DrawRectangle(Pens.Black, 14, 6 * unite_hauteur + 12, unite_largeur * 23 - 13, 45 * (unite_hauteur) + unite_hauteur + 3);

            graphic.DrawString(titreImpression.ToUpper(), fnt11, Brushes.Black, 15, 5 * unite_hauteur + 3);
            graphic.DrawString("Nombre total : " + nombre, fnt33, Brushes.Black, 20 * unite_largeur, 5 * unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 15, 6 * unite_hauteur + 12, unite_largeur + 10, unite_hauteur + 3);
            //graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 2 + 7, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 1 * unite_largeur+25, 6 * unite_hauteur + 12, unite_largeur * 8, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 9 * unite_largeur+25, 6 * unite_hauteur + 12, unite_largeur * 5, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 13 * unite_largeur + 25, 6 * unite_hauteur + 12, unite_largeur + 0, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 25, 6 * unite_hauteur + 12, unite_largeur + 0, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 25, 6 * unite_hauteur + 12, unite_largeur + 0, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 25, 6 * unite_hauteur + 12, unite_largeur * 3, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 25, 6 * unite_hauteur + 12, unite_largeur * 3+7, unite_hauteur + 3);
            //graphic.DrawString("N°", fnt11, Brushes.Black, unite_largeur * 1, 6 * unite_hauteur+13);
      
            graphic.DrawString("Nom & Prenom", fnt11, Brushes.Black, 3 * unite_largeur + 10, 6 * unite_hauteur + 13);
            graphic.DrawString("Qualification", fnt11, Brushes.Black, 10 * unite_largeur + 10, 6 * unite_hauteur + 13);
            graphic.DrawString("Cat", fnt11, Brushes.Black, 13 * unite_largeur + 26, 6 * unite_hauteur + 13);
            graphic.DrawString("Ech", fnt11, Brushes.Black, 14 * unite_largeur + 26, 6 * unite_hauteur + 13);
            graphic.DrawString("Anc", fnt11, Brushes.Black, 15 * unite_largeur + 26, 6 * unite_hauteur + 13);
            graphic.DrawString("Type contrat", fnt11, Brushes.Black, 17 * unite_largeur - 5, 6 * unite_hauteur + 13);
            graphic.DrawString("Salaire base", fnt11, Brushes.Black, unite_largeur * 20+-5, 6 * unite_hauteur + 13);
            var j = 0;

            //var numero = 0;
            //for (var p = 1 + st; p = nombre; p++)
            //{
            //    numero = p;
            //}
            for (var i = start * 45; i < dgvPersonnel.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur + 15;
                graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur +10, unite_hauteur + 0);
           
                if (string.IsNullOrWhiteSpace(dgvPersonnel.Rows[i].Cells[0].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.SaddleBrown, 15, Yloc, unite_largeur * 23 - 14, unite_hauteur);
                    graphic.DrawString(" Personnels service " + dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.White, unite_largeur * 5, Yloc);
                    Yloc += unite_hauteur;
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 25, Yloc, unite_largeur * 8, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 25, Yloc , unite_largeur * 5, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, 13 * unite_largeur + 25, Yloc , unite_largeur + 0, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 25, Yloc , unite_largeur + 0, unite_hauteur + 0);
                    graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 25, Yloc , unite_largeur + 0, unite_hauteur );
                    graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 25, Yloc , unite_largeur * 3, unite_hauteur );
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 25, Yloc , unite_largeur * 3 + 7, unite_hauteur );

                    //graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, unite_largeur , Yloc);
                    //graphic.DrawString(dgvPersonnel.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 2, Yloc);
                 
                    var employe = dgvPersonnel.Rows[i].Cells[1].Value.ToString() + " " + dgvPersonnel.Rows[i].Cells[2].Value.ToString();
                    if (employe.Length > 28)
                    {
                        employe = employe.Substring(0, 28) + ".";
                    }
                    graphic.DrawString(employe, fnt1, Brushes.Black, 2 * unite_largeur + 2, Yloc + 1); 
                    
                    if (dgvPersonnel.Rows[i].Cells[3].Value.ToString().Length > 17)
                    {
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString().Substring(0, 17) + "...", fnt33, Brushes.Black, 10 * unite_largeur + 0, Yloc + 1);
                    }
                    else
                    {
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 10 * unite_largeur + 0, Yloc + 1);
                    }
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur + 6, Yloc + 1);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur + 6, Yloc + 1);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur + 6, Yloc + 1);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 0, Yloc + 1);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", double.Parse(dgvPersonnel.Rows[i].Cells[10].Value.ToString())), fnt1, Brushes.Black, 20 * unite_largeur + 15, Yloc + 1);
                }
                graphic.FillRectangle(Brushes.White, 15, 52 * unite_hauteur + 17, unite_largeur * 24 - 14, 8 * unite_hauteur);
                graphic.DrawString("Page " + (start + 1), fnt1, Brushes.Black, 21 * unite_largeur + 20, 20);

                j++;
            }
            return bitmap;
            }
            catch (Exception exception){
                GestionPharmacetique.MonMessageBox.ShowBox("imp", exception);
                return null;
            } //definir les polices 
        }

        //imprimer le badge
        public static Bitmap ImprimerBadgePersonnel(string numero, string nom, string prenom,
             string fonction, Image photo)
        {
            int unite_hauteur = 25;
            int unite = 20;
            int unite_largeur = 25;
            int largeur_batch = 9 * unite_largeur;
            int hauteur_batch = 14 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_batch + 2, hauteur_batch + 7, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);
            Font fnt1 = new Font("Arial", 7, FontStyle.Regular);
            Font fnt2 = new Font("Arial", 8, FontStyle.Bold);
            Font fnt3 = new Font("Arial", 11, FontStyle.Bold);
            //la couleur de l'image
            graphic.Clear(Color.White);
            Image image1 = global::SGSP.Properties.Resources.Logo;
            //Image capture = global::GestionDesPelerinsTchad.Properties.Resources.Capture2;
            //graphic.DrawImage(capture, 0, 0, bitmap.Width, bitmap.Height);
            graphic.DrawImage(image1, 7 * unite_largeur + 7, 5, 2 * unite, unite_hauteur);

            graphic.DrawString("MON ENTREPRISE ", fnt3, Brushes.White, 10, 5);
            graphic.DrawString("CARTE PROFESSIONNELLE ", fnt3, Brushes.White, 10, 50);
            graphic.FillRectangle(Brushes.SlateGray, 0, 70, bitmap.Width, 5);

            graphic.DrawString(numero, fnt2, Brushes.White, 2 * unite_largeur, 3 * unite_hauteur + 19);
            graphic.DrawString(nom, fnt2, Brushes.White, 2 * unite_largeur, 3 * unite_hauteur + 39);
            graphic.DrawString(prenom, fnt2, Brushes.White, 2 * unite_largeur, 3 * unite_hauteur + 59);
            graphic.DrawString(fonction, fnt2, Brushes.White, 2 * unite_largeur, 3 * unite_hauteur + 79);
            graphic.DrawString("expiration : 31/12/" + DateTime.Now.Year, fnt2, Brushes.Red, 2 * unite_largeur, 13 * unite_hauteur + 15);
            graphic.DrawString("ANNEE " + DateTime.Now.Year, fnt3, Brushes.Yellow, 2 * unite_largeur, 3 * unite_hauteur + 100);

            graphic.DrawImage(photo, 2 * unite_largeur + 10, 10 * unite + 5, 4 * unite_largeur + 10, 6 * unite_hauteur - 15);
            //graphic.DrawImage(barcode, 15, 12 * unite_hauteur, 3 * unite_largeur, 2 * unite);

            return bitmap;
        }
        //imprimer le badge
        public static Bitmap ImprimerBadgePersonnel(Image barcode)
        {
            int unite_hauteur = 25;
            int unite = 20;
            int unite_largeur = 25;
            int largeur_batch = 9 * unite_largeur;
            int hauteur_batch = 14 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_batch + 2, hauteur_batch + 7, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);
            Font fnt1 = new Font("Arial", 7, FontStyle.Regular);
            Font fnt2 = new Font("Arial", 8, FontStyle.Bold);
            Font fnt3 = new Font("Arial", 11, FontStyle.Bold);
            //la couleur de l'image
            graphic.Clear(Color.White);
            Image image1 = global::SGSP.Properties.Resources.Logo;
            //Image capture = global::GestionDesPelerinsTchad.Properties.Resources.Capture2;
            //graphic.DrawImage(capture, 0, 0, bitmap.Width, bitmap.Height);
            graphic.DrawImage(image1, 3 * unite_largeur + 7, unite_hauteur + 10, 3 * unite, unite_hauteur + 10);

            graphic.DrawString("REPUBLIQUE DU TCHAD ", fnt3, Brushes.White, 15, 10);

            graphic.FillRectangle(Brushes.SlateGray, 0, 87, bitmap.Width, 5);

            graphic.DrawImage(barcode, 2 * unite_largeur + 10, 8 * unite_hauteur, 4 * unite_largeur, 2 * unite_hauteur);
            graphic.DrawString("Soft development company", fnt1, Brushes.White, 10, 11 * unite_hauteur);
            graphic.DrawString("Devevloppement des applicaton informatique", fnt1, Brushes.White, 10, 11 * unite_hauteur + 15);
            graphic.DrawString("Adresse : Motorway Hill Fox 40", fnt1, Brushes.White, 10, 11 * unite_hauteur + 30);
            graphic.DrawString("Tel : (+235) 66304238 / 66661286", fnt1, Brushes.White, 10, 11 * unite_hauteur + 45);
            graphic.DrawString("Email : noudjichrist87@yahoo.com", fnt1, Brushes.White, 10, 11 * unite_hauteur + 60);
            return bitmap;
        }

        //imprimer l'ordre de paiement
        public static Bitmap ImprimerOrdreDePaiement(int numeroPaiement
            ,DataGridView dgvPaiement, int exercice, string mois)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 34 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 4* unite_hauteur-15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 8, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt00 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Regular);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            graphic.DrawString("Page " +1, fnt1, Brushes.Black,33*unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
         var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            graphic.DrawString("ETAT DE PAIEMENT SALAIRE DU MOIS DE " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, unite_largeur * 17+10, 6 * unite_hauteur - 10,drawFormatCenter);
            graphic.FillRectangle(Brushes.SaddleBrown, 12, 7 * unite_hauteur + 4, unite_largeur * 35 - 15, 2 * unite_hauteur - 4);
            graphic.DrawString("N°", fnt0, Brushes.White, 15, 7 * unite_hauteur + 6);
            graphic.DrawString("Nom & prenom", fnt0, Brushes.White, 35, 7 * unite_hauteur + 6);
            graphic.DrawString("Qualification", fnt0, Brushes.White, 7 * unite_largeur - 10, 7 * unite_hauteur + 6);
            graphic.DrawString("Cat\nEch", fnt0, Brushes.White, 9 * unite_largeur -0, 7 * unite_hauteur + 6);
            graphic.DrawString("Salaire \nbase", fnt0, Brushes.White, 11 * unite_largeur + 15, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Primes\nAnc", fnt0, Brushes.White, 13 * unite_largeur +3, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Salaire\ngain", fnt0, Brushes.White, 15 * unite_largeur + 10, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Cout\nAbsce", fnt0, Brushes.White, 16 * unite_largeur + 18, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Salaire\nbrut", fnt0, Brushes.White, 18 * unite_largeur +15, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("CNPS", fnt0, Brushes.White, 20 * unite_largeur + 0, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("IRPP", fnt0, Brushes.White, 21 * unite_largeur + 20, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("FIR", fnt0, Brushes.White, 22 * unite_largeur + 20, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Acompte", fnt0, Brushes.White, 24 * unite_largeur + 7, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Avance\nSalaire", fnt0, Brushes.White, 25 * unite_largeur + 25, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Total\nDéduction", fnt0, Brushes.White, 27 * unite_largeur + 15, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Total\nprimes", fnt0, Brushes.White, 29 * unite_largeur + 0, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Salaire\nNet", fnt0, Brushes.White, 31 * unite_largeur + 0, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Charge\npatron", fnt0, Brushes.White, 32 * unite_largeur + 20, 7 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Cout\nsalarial", fnt0, Brushes.White, 34 * unite_largeur + 20, 7 * unite_hauteur + 6, drawFormatRight);
     
            var j = 0;
            var count = 1;
            for (var i = 0; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 9 + 3 + unite_hauteur * j;
                graphic.DrawRectangle(Pens.SlateGray, 12, YLOC, unite_largeur * 35 - 18, unite_hauteur);
                if (i % 2 == 0)
                {
                    graphic.FillRectangle(Brushes.White, 13, YLOC + 1, unite_largeur * 35 - 19, unite_hauteur - 1);
                }
                else
                {
                    graphic.FillRectangle(Brushes.AliceBlue, 13, YLOC + 1, unite_largeur * 35 - 19, unite_hauteur - 1);
                }
                var employe = dgvPaiement.Rows[i].Cells[1].Value.ToString();
               //employe = employe.Substring(0).tou + employe 
                var qualification = dgvPaiement.Rows[i].Cells[2].Value.ToString();
                if (qualification.Length > 14)
                {
                    qualification = qualification.Substring(0, 14) + "..."; ;
                }
                if (!string.IsNullOrWhiteSpace(qualification))
                {
                    qualification = qualification.Substring(0, 1).ToUpper() + qualification.Substring(1).ToLower();
                }
                if (qualification.Length <= 5)
                    qualification = qualification.ToUpper();
                if (dgvPaiement.Rows[i].Cells[1].Value.ToString().Contains("TOTAL"))
                {
                    if (dgvPaiement.Rows[i].Cells[1].Value.ToString().Contains("TOTAL GENERAL"))
                    {
                        graphic.FillRectangle(Brushes.Bisque, 13, YLOC + 1, unite_largeur * 35 - 19, unite_hauteur - 1);
                    }
                 
                    graphic.DrawString(employe, fnt0, Brushes.Black, 35, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString())), fnt0, Brushes.Black, 11 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[5].Value.ToString())), fnt0, Brushes.Black, 13 * unite_largeur + 3, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[6].Value.ToString())), fnt0, Brushes.Black, 15 * unite_largeur + 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[8].Value.ToString())), fnt0, Brushes.Black, 16 * unite_largeur + 18, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[9].Value.ToString())), fnt0, Brushes.Black, 18 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt0, Brushes.Black, 20 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[11].Value.ToString())), fnt0, Brushes.Black, 21 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[12].Value.ToString())), fnt0, Brushes.Black, 22 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[13].Value.ToString())), fnt0, Brushes.Black, 24 * unite_largeur + 7, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[14].Value.ToString())), fnt0, Brushes.Black, 25 * unite_largeur + 25, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[15].Value.ToString())), fnt0, Brushes.Black, 27 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt0, Brushes.Black, 29 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt0, Brushes.Black, 31 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString())), fnt0, Brushes.Black, 32 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[19].Value.ToString())), fnt0, Brushes.Black, 34 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[4].Value.ToString()) && string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[5].Value.ToString()))
                    {
                        graphic.FillRectangle(Brushes.Moccasin, 13, YLOC + 1, unite_largeur * 35 - 19, unite_hauteur - 1); 
                  graphic.DrawString(employe.ToUpper(), fnt33, Brushes.Black, 20, YLOC + 3);
                
                  
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[20].Value.ToString()))
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[20].Value.ToString(), fnt1, Brushes.Black, 15, YLOC + 3);
                        }
                        graphic.DrawString(employe, fnt1, Brushes.Black, 35, YLOC + 3);
                        graphic.DrawString(qualification, fnt1, Brushes.Black, 7 * unite_largeur - 13, YLOC + 3);
                        graphic.DrawString(dgvPaiement.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 9 * unite_largeur - 0, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString())), fnt11, Brushes.Black, 11 * unite_largeur+15, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[5].Value.ToString())), fnt11, Brushes.Black, 13 * unite_largeur + 3, YLOC + 3,drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[6].Value.ToString())), fnt11, Brushes.Black, 15 * unite_largeur + 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[8].Value.ToString())), fnt11, Brushes.Black, 16 * unite_largeur + 18, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[9].Value.ToString())), fnt11, Brushes.Black, 18 * unite_largeur+ 15, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt11, Brushes.Black, 20* unite_largeur + 0, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[11].Value.ToString())), fnt11, Brushes.Black, 21 * unite_largeur +20, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[12].Value.ToString())), fnt11, Brushes.Black, 22 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[13].Value.ToString())), fnt11, Brushes.Black, 24 * unite_largeur + 7, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[14].Value.ToString())), fnt11, Brushes.Black, 25 * unite_largeur + 25, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[15].Value.ToString())), fnt11, Brushes.Black, 27 * unite_largeur +15, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt11, Brushes.Black, 29 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt11, Brushes.Black, 31 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString())), fnt11, Brushes.Black, 32 * unite_largeur +20, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[19].Value.ToString())), fnt1, Brushes.Black, 34 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                        count++;
                    }
                }
                j++;
                #endregion
            }

            graphic.FillRectangle(Brushes.White, 0, 34*unite_hauteur +4, unite_largeur * 35, unite_hauteur*6);
            ////if (dgvPaiement.Rows.Count <=  20)
            ////{
            ////    //height = (13 + unite_hauteur);
            ////    //var YLOC = unite_hauteur * 8 + height * j;

            ////    var index = unite_hauteur * 9 + j * unite_hauteur + 15;
            ////    graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index + 0);
            ////    graphic.DrawString("Le Directeur ", fnt33, Brushes.Black, 15 * unite_largeur + 10, index + unite_hauteur + 5);
            ////    var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
            ////    if (dtP.Rows.Count > 0)
            ////    {
            ////        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index + 2 * unite_hauteur + 10);
            ////    }
            ////}
            return bitmap;
        }

        public static Bitmap ImprimerOrdreDePaiement(int numeroPaiement
       , DataGridView dgvPaiement, int exercice, string mois, int start)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 34 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
         
    Font fnt1 = new Font("Calibri", 8, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt00 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Regular);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);
            #endregion

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            graphic.DrawString("Page " + (start + 2).ToString(), fnt1, Brushes.Black, 33 * unite_largeur, 2);

            graphic.FillRectangle(Brushes.SaddleBrown, 12, 1 * unite_hauteur + 4, unite_largeur * 35 - 18, 2 * unite_hauteur - 4);
            graphic.DrawString("N°", fnt0, Brushes.White, 18, 1 * unite_hauteur + 6);
            graphic.DrawString("Nom & prenom", fnt0, Brushes.White, 35, 1 * unite_hauteur + 6);
            graphic.DrawString("Qualification", fnt0, Brushes.White, 7 * unite_largeur - 10, 1 * unite_hauteur + 6);
            graphic.DrawString("Cat\nEch", fnt0, Brushes.White, 9 * unite_largeur -0,1 * unite_hauteur + 6);
            graphic.DrawString("Salaire \nbase", fnt0, Brushes.White, 11 * unite_largeur + 15, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Primes\nAnc", fnt0, Brushes.White, 13 * unite_largeur +3, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Salaire\ngain", fnt0, Brushes.White, 15 * unite_largeur + 10, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Cout\nAbsce", fnt0, Brushes.White, 16 * unite_largeur + 18, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Salaire\nbrut", fnt0, Brushes.White, 18 * unite_largeur +15, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("CNPS", fnt0, Brushes.White, 20 * unite_largeur + 0, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("IRPP", fnt0, Brushes.White, 21 * unite_largeur + 20, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("FIR", fnt0, Brushes.White, 22 * unite_largeur + 20, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Acompte", fnt0, Brushes.White, 24 * unite_largeur + 7,1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Avance \nSalaire", fnt0, Brushes.White, 25 * unite_largeur + 25, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Total\nDéduction", fnt0, Brushes.White, 27 * unite_largeur + 15, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Total\nprimes", fnt0, Brushes.White, 29 * unite_largeur + 0, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Salaire\nNet", fnt0, Brushes.White, 31 * unite_largeur + 0, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Charge\npatron", fnt0, Brushes.White, 32 * unite_largeur + 20, 1 * unite_hauteur + 6, drawFormatRight);
            graphic.DrawString("Cout\nsalarial", fnt0, Brushes.White, 34 * unite_largeur + 20, 1 * unite_hauteur + 6, drawFormatRight);
     

            var j = 0;
            var count = 25 + start * 31;
            for (var i = 25+start * 31; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur *3 + 3 + unite_hauteur * j;
                graphic.DrawRectangle(Pens.SlateGray, 12, YLOC, unite_largeur * 35 - 18, unite_hauteur);
                if (i % 2 == 0)
                {
                    graphic.FillRectangle(Brushes.White, 13, YLOC + 1, unite_largeur * 35 - 19, unite_hauteur - 1);
                }
                else
                {
                    graphic.FillRectangle(Brushes.AliceBlue, 13, YLOC + 1, unite_largeur * 35 - 19, unite_hauteur - 1);
                }
                var employe = dgvPaiement.Rows[i].Cells[1].Value.ToString();
                //employe = employe.Substring(0).tou + employe 
                var qualification = dgvPaiement.Rows[i].Cells[2].Value.ToString();
                if (qualification.Length > 14)
                {
                    qualification = qualification.Substring(0, 14) + "..."; ;
                }
                if (!string.IsNullOrWhiteSpace(qualification))
                {
                    qualification = qualification.Substring(0, 1).ToUpper() + qualification.Substring(1).ToLower();
                }
                if (qualification.Length <= 5)
                    qualification = qualification.ToUpper();
                if (dgvPaiement.Rows[i].Cells[1].Value.ToString().Contains("TOTAL"))
                {
                    if (dgvPaiement.Rows[i].Cells[1].Value.ToString().Contains("TOTAL GENERAL"))
                    {
                        graphic.FillRectangle(Brushes.Bisque, 13, YLOC + 1, unite_largeur * 35 - 19, unite_hauteur - 1);
                    }

                    graphic.DrawString(employe, fnt0, Brushes.Black, 35, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString())), fnt0, Brushes.Black, 11 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[5].Value.ToString())), fnt0, Brushes.Black, 13 * unite_largeur + 3, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[6].Value.ToString())), fnt0, Brushes.Black, 15 * unite_largeur + 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[8].Value.ToString())), fnt0, Brushes.Black, 16 * unite_largeur + 18, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[9].Value.ToString())), fnt0, Brushes.Black, 18 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt0, Brushes.Black, 20 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[11].Value.ToString())), fnt0, Brushes.Black, 21 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[12].Value.ToString())), fnt0, Brushes.Black, 22 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[13].Value.ToString())), fnt0, Brushes.Black, 24 * unite_largeur + 7, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[14].Value.ToString())), fnt0, Brushes.Black, 25 * unite_largeur + 25, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[15].Value.ToString())), fnt0, Brushes.Black, 27 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt0, Brushes.Black, 29 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt0, Brushes.Black, 31 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString())), fnt0, Brushes.Black, 32 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[19].Value.ToString())), fnt0, Brushes.Black, 34 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[4].Value.ToString()) && string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[5].Value.ToString()))
                    {
                        graphic.FillRectangle(Brushes.Moccasin, 13, YLOC + 1, unite_largeur * 35 - 19, unite_hauteur - 1);
                        graphic.DrawString(employe.ToUpper(), fnt33, Brushes.Black, 20, YLOC + 3);


                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[20].Value.ToString()))
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[20].Value.ToString(), fnt1, Brushes.Black, 15, YLOC + 3);
                        }
                        graphic.DrawString(employe, fnt1, Brushes.Black, 35, YLOC + 3);
                        graphic.DrawString(qualification, fnt1, Brushes.Black, 7 * unite_largeur - 13, YLOC + 3);
                        graphic.DrawString(dgvPaiement.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 9 * unite_largeur - 0, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString())), fnt11, Brushes.Black, 11 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[5].Value.ToString())), fnt11, Brushes.Black, 13 * unite_largeur + 3, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[6].Value.ToString())), fnt11, Brushes.Black, 15 * unite_largeur + 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[8].Value.ToString())), fnt11, Brushes.Black, 16 * unite_largeur + 18, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[9].Value.ToString())), fnt11, Brushes.Black, 18 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt11, Brushes.Black, 20 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[11].Value.ToString())), fnt11, Brushes.Black, 21 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[12].Value.ToString())), fnt11, Brushes.Black, 22 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[13].Value.ToString())), fnt11, Brushes.Black, 24 * unite_largeur + 7, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[14].Value.ToString())), fnt11, Brushes.Black, 25 * unite_largeur + 25, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[15].Value.ToString())), fnt11, Brushes.Black, 27 * unite_largeur + 15, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt11, Brushes.Black, 29 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt11, Brushes.Black, 31 * unite_largeur + 0, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString())), fnt11, Brushes.Black, 32 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[19].Value.ToString())), fnt1, Brushes.Black, 34 * unite_largeur + 20, YLOC + 3, drawFormatRight);
                        count++;
                    }
                }
                j++;
                #endregion
       
            }

            graphic.FillRectangle(Brushes.White, 0, 34 * unite_hauteur + 4, unite_largeur * 35, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= (1 + start) * 31+25)
            {
                //height = (13 + unite_hauteur);
                //var YLOC = unite_hauteur * 8 + height * j;
                var index = unite_hauteur * 4 + j * unite_hauteur + 10;
                graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 14 * unite_largeur + 0, index + 0);
                graphic.DrawString("Le Directeur ", fnt33, Brushes.Black, 15 * unite_largeur + 10, index + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    //graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 13 * unite_largeur + 0, index + 2 * unite_hauteur + 10);
                    var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                    if (ecart <= 2 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 14 * unite_largeur + 0, index + 3 * unite_hauteur + 10);
                    }
                    else if (ecart > 2 * unite_hauteur && ecart <= 3 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 14 * unite_largeur + 0, index + 4 * unite_hauteur - 5);
                    }
                    else if (ecart > 3 * unite_hauteur && ecart <= 4 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 14 * unite_largeur + 0, index + 5 * unite_hauteur - 5);
                    }
                    else if (ecart > 4 * unite_hauteur && ecart <= 5 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 14 * unite_largeur + 0, index + 6 * unite_hauteur - 5);
                    }
                    else if (ecart > 5 * unite_hauteur && ecart <= 6 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 14 * unite_largeur + 0, index + 7 * unite_hauteur - 5);
                    }
                    else if (ecart > 6 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 14 * unite_largeur + 0, index + 8 * unite_hauteur - 5);
                    } 
                }
            }
            return bitmap;
        }

        //imprimer l'ordre de paiement
        public static Bitmap ImprimerOrdreDePaiementPourJournalierEtStagiaire(int numeroPaiement ,DataGridView dgvPaiement, string typeContrat, int exercice, string mois, int start)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 35 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 5, 12 * unite_largeur, 4 * unite_hauteur - 5);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 10, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 10, FontStyle.Regular);
            Font fnt0 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt00 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 10, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 18, FontStyle.Bold | FontStyle.Underline);
            Font fnt22 = new Font("Calibri", 12, FontStyle.Regular);

            #endregion

            graphic.DrawString("Page " + (start + 1).ToString(), fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            if (typeContrat == "Stage")
            {
                graphic.DrawString( "  FORFAITS TRANSPORT STAGIAIRE DU MOIS DE " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, unite_largeur * 6, 4 * unite_hauteur - 10);
            }
            else
            {
                graphic.DrawString(typeContrat.ToUpper() + "  ET FORFAITS DU MOIS DE " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, unite_largeur * 8, 4 * unite_hauteur - 10);
            }
            //graphic.FillRectangle(Brushes.SaddleBrown, 15, 6 * unite_hauteur + 4, unite_largeur * 34 - 18, 2 * unite_hauteur - 4);

         
            var height = (13 + unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 17, 6 * unite_hauteur + 0, unite_largeur * 1 - 2, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur + 17, 6 * unite_hauteur + 0, unite_largeur * 8 - 20, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 9 * unite_largeur + 0, 6 * unite_hauteur + 0, unite_largeur * 3 + 21, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 12 * unite_largeur + 24, 6 * unite_hauteur + 0, unite_largeur * 4 + 16, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 16 * unite_largeur + 11, 6 * unite_hauteur + 0, unite_largeur * 2 - 6, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 18 * unite_largeur + 8, 6 * unite_hauteur + 0, unite_largeur * 1 - 4, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 19 * unite_largeur + 6, 6 * unite_hauteur + 0, unite_largeur * 2 + 16, 2 * unite_hauteur);
            //graphic.FillRectangle(Brushes.Lavender, 18 * unite_largeur + 6, 6 * unite_hauteur + 0 + 1, unite_largeur * 2 + 16, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 21 * unite_largeur + 25, 6 * unite_hauteur + 0, unite_largeur * 2 - 2, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 23 * unite_largeur + 25, 6 * unite_hauteur + 0, unite_largeur * 2 - 2, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 25 * unite_largeur + 25, 6 * unite_hauteur + 0, unite_largeur * 2 - 2, 2 * unite_hauteur);
            //graphic.FillRectangle(Brushes.Lavender, 26 * unite_largeur + 26, 6 * unite_hauteur + 0 + 1, unite_largeur * 2 + 14, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 27 * unite_largeur + 26, 6 * unite_hauteur + 0, unite_largeur * 2 + 14, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 30 * unite_largeur + 11, 6 * unite_hauteur + 0, unite_largeur * 4 - 15, 2 * unite_hauteur);
            graphic.DrawString("N°", fnt00, Brushes.Black, unite_largeur - 10, 6 * unite_hauteur + 2);
            graphic.DrawString("Nom & prenom", fnt00, Brushes.Black, unite_largeur + 20, 6 * unite_hauteur + 2);
            graphic.DrawString("Qualification", fnt00, Brushes.Black, 9* unite_largeur + 10, 6 * unite_hauteur + 2);
            graphic.DrawString("Service", fnt00, Brushes.Black, 12 * unite_largeur + 30, 6 * unite_hauteur + 2);
            graphic.DrawString("Prime\n/Jour", fnt00, Brushes.Black,16* unite_largeur +20, 6 * unite_hauteur + 2);
            graphic.DrawString("Nbr\nJr", fnt00, Brushes.Black, 18*unite_largeur + 11, 6 * unite_hauteur + 2);
            graphic.DrawString("Montant", fnt00, Brushes.Black, 19 * unite_largeur + 15, 6 * unite_hauteur + 2);
            graphic.DrawString("Acompte", fnt00, Brushes.Black, 21 * unite_largeur + 30, 6 * unite_hauteur + 2);
            graphic.DrawString("Avance\nSalaire.", fnt00, Brushes.Black, 23 * unite_largeur + 32, 6 * unite_hauteur + 2);
            graphic.DrawString("Total\nDeduct.", fnt00, Brushes.Black, 25 * unite_largeur +30, 6 * unite_hauteur + 2);
            graphic.DrawString("Solde à \npayer", fnt00, Brushes.Black, 28 * unite_largeur + 5, 6 * unite_hauteur + 2);
            graphic.DrawString("Signature", fnt00, Brushes.Black, 30 * unite_largeur + 16, 6 * unite_hauteur + 2);
            //graphic.DrawString("Nbr\nJr", fnt00, Brushes.Black, 16 * unite_largeur + 3, 6 * unite_hauteur + 2);
            var j = 0;
            var totalAcompte = 0.0;
            var totalChargeSoin = .0;
            for (var i = start * 14; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion
              
                var YLOC = unite_hauteur * 8 + height* j;
                                
              
                var employe = dgvPaiement.Rows[i].Cells[1].Value.ToString();
                //if (employe.Length > 21)
                //{
                //    employe = employe.Substring(0, 21);
                //}
                var qualification = dgvPaiement.Rows[i].Cells[2].Value.ToString();
                if (qualification.Length > 13)
                {
                    qualification = qualification.Substring(0, 13) + "\n" + qualification.Substring(13);
                }
                if (!string.IsNullOrWhiteSpace(qualification))
                {
                    qualification = qualification.Substring(0, 1).ToUpper() + qualification.Substring(1).ToLower();
                }
                if (qualification.Length <= 5)
                {
                    qualification = qualification.ToUpper();
                }
                var dtService = ConnectionClass.ListeDesPersonnelParNumeroMatricule(dgvPaiement.Rows[i].Cells[0].Value.ToString());
                var p = ConnectionClass.PaiementParMatricule(numeroPaiement,dgvPaiement.Rows[i].Cells[0].Value.ToString());
                var service = "";
                if (dtService.Rows.Count > 0)
                {
                    service = dtService.Rows[0].ItemArray[11].ToString();
                }
                if (service.Length > 17)
                {
                    service = service.Substring(0, 17)+"...";
                }
                if (dgvPaiement.Rows[i].Cells[1].Value.ToString().ToUpper().Contains("TOTAL"))
                {
                    //graphic.DrawRectangle(Pens.Black, 17, YLOC, unite_largeur * 1 - 2, height - 3);
                    graphic.DrawRectangle(Pens.Black,  17, YLOC, unite_largeur * 34 - 20, height - 3);
                    var montant = Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString());
                    graphic.DrawString(employe, fnt00, Brushes.Black, unite_largeur + 20, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt00, Brushes.Black, 19 * unite_largeur + 10, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[13].Value.ToString())), fnt00, Brushes.Black, 21 * unite_largeur + 30, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[14].Value.ToString())), fnt00, Brushes.Black, 23 * unite_largeur + 30, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[15].Value.ToString())), fnt00, Brushes.Black, 25 * unite_largeur + 30, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt00, Brushes.Black, 28 * unite_largeur + 10, YLOC + 3);
                 
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 17, YLOC, unite_largeur * 1 - 2, height - 3);
                    graphic.DrawRectangle(Pens.Black, unite_largeur + 17, YLOC, unite_largeur * 8 - 20, height - 3);
                    graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 0, YLOC, unite_largeur * 3 + 21, height - 3);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 24, YLOC, unite_largeur * 3 + 16, height - 3);
                    graphic.DrawRectangle(Pens.Black, 16 * unite_largeur +11, YLOC, unite_largeur * 2 - 6, height - 3);
                    graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 8, YLOC, unite_largeur * 1 - 4, height - 3);
                    graphic.FillRectangle(Brushes.Lavender, 19 * unite_largeur + 6, YLOC + 1, unite_largeur * 2 + 16, height - 3);
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur +6, YLOC, unite_largeur *2 +16, height - 3);                  
                    graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 25, YLOC, unite_largeur * 2 - 2, height - 3);
                    graphic.DrawRectangle(Pens.Black, 23 * unite_largeur + 25, YLOC, unite_largeur * 2 - 2, height - 3);
                    graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 25, YLOC, unite_largeur * 2 - 2, height - 3);
                    graphic.FillRectangle(Brushes.Lavender, 27 * unite_largeur + 26, YLOC + 1, unite_largeur * 2 +14, height - 3);
                    graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 26, YLOC, unite_largeur * 2+14, height - 3);
                    graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 11, YLOC, unite_largeur *4 - 15, height - 3);

                    graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, 20, YLOC + 3);
                    graphic.DrawString(employe, fnt1, Brushes.Black, unite_largeur + 22, YLOC + 3);
                    graphic.DrawString(service.ToLower(), fnt1, Brushes.Black, 12 * unite_largeur + 30, YLOC + 3);
                    if (string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[4].Value.ToString()) && string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[5].Value.ToString()))
                    {

                    }
                    else
                    {
                        var montant = Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString());

                        graphic.DrawString(qualification, fnt1, Brushes.Black, 9*unite_largeur + 5, YLOC + 1);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", montant / 30), fnt1, Brushes.Black, 16 * unite_largeur + 17, YLOC + 3);
                        graphic.DrawString("30", fnt1, Brushes.Black, 18 * unite_largeur + 15, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt00, Brushes.Black, 19 * unite_largeur + 15, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", p.AvanceSurSalaire ), fnt1, Brushes.Black, 21 * unite_largeur + 30, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", p.AcomptePaye), fnt1, Brushes.Black, 23 * unite_largeur + 30, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", p.AcomptePaye+ p.AvanceSurSalaire), fnt1, Brushes.Black, 25 * unite_largeur + 30, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt00, Brushes.Black, 28 * unite_largeur + 10, YLOC + 3);
                        totalAcompte += p.AvanceSurSalaire+ p.AcomptePaye;
                        totalChargeSoin += 0;
                    }
                }
                j++;
            }
                #endregion
            graphic.FillRectangle(Brushes.White,  2,30 * unite_hauteur + 4, unite_largeur * 38 - 5, 8 * unite_hauteur);
            if ( dgvPaiement.Rows.Count<=(1+start) * 14 )
            {
                //height = (13 + unite_hauteur);
                //var YLOC = unite_hauteur * 8 + height * j;
             
                var index = unite_hauteur * 8 + j * 35+10;
                graphic.DrawString("Fait à Bébédjia le  "+DateTime.Now.ToShortDateString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index +0);
                graphic.DrawString("Le Directeur ", fnt00, Brushes.Black, 15 * unite_largeur + 10, index + unite_hauteur+5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    var ecart = hauteur_facture -( index + 2 * unite_hauteur + 10);
                    if (ecart<=2*unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index +3 * unite_hauteur + 10);
                    }
                    else if (ecart >2*unite_hauteur && ecart <=3*unite_hauteur )
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index + 4 * unite_hauteur -5);
                    }
                    else if (ecart > 3 * unite_hauteur && ecart <= 4* unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index + 5 * unite_hauteur - 5);
                    }
                    else if (ecart >4 * unite_hauteur && ecart <= 5 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index + 6 * unite_hauteur - 5);
                    }
                    else if (ecart > 5 * unite_hauteur && ecart <= 6 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index +7 * unite_hauteur - 5);
                    }
                    else if (ecart > 6 * unite_hauteur )
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                    } 
                }
            }

            return bitmap;
        }

        static int ObtenirMois(string mois)
        {
            switch (mois)
            {
                case "Janvier":
                    return 1;
                case "Fevrier":
                    return 2;
                case "Mars":
                    return 3;
                case "Avril":
                    return 4;
                case "Mai":
                    return 5;
                case "Juin":
                    return 6;
                case "Juillet":
                    return 7;
                case "Aout":
                    return 8;
                case "Septembre":
                    return 9;
                case "Octobre":
                    return 10;
                case "Novembre":
                    return 11;
                case "Decembre":
                    return 12;
                default:
                    return 0;
            };
        }

        static DateTime ObtenirDebutJour(string mois, int exercice)
        {
            return DateTime.Parse("01/" + ObtenirMois(mois) + "/" + exercice);
        }

        static DateTime ObtenirFinJour(string mois, int exercice)
        {
            if (mois == "Janvier")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Fevrier")
            {
                if (DateTime.IsLeapYear(exercice))
                    return DateTime.Parse("29/" + ObtenirMois(mois) + "/" + exercice);
                else
                    return DateTime.Parse("28/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Mars")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Avril")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Mai")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Juin")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Juillet")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Aout")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Septembre")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Octobre")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Novembre")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Decembre")
            {
                return DateTime.Parse("31/" +ObtenirMois( mois) + "/" + exercice);
            }
            else
            {
                return DateTime.Now.Date; ;
            }
        }
      public static Bitmap ImprimerUnBulletinDePaie(Paiement paiement, Personnel personnel, Service service)
        {
            try
            {
                #region
                int unite_hauteur = 18;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 10;
                int detail_hauteur_facture = 10 * unite_hauteur;
                int hauteur_facture = 62 * unite_hauteur;

                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #endregion
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 15, 10, 15 * unite_largeur - 5, 5 * unite_hauteur);

                var drawFormatCenter = new StringFormat();
                drawFormatCenter.Alignment = StringAlignment.Center;
                var drawFormatLeft = new StringFormat();
                drawFormatLeft.Alignment = StringAlignment.Near;
                var drawFormatRight = new StringFormat();
                drawFormatRight.Alignment = StringAlignment.Far;

                Font fnt1 = new Font("Arial Narrow", 9.5f, FontStyle.Regular);
                Font fnt0 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 9F, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Underline);
                Font fnt01 = new Font("Arial Unicode MS", 11, FontStyle.Italic);
                Font fnt110 = new Font("Arial Unicode MS", 12, FontStyle.Bold | FontStyle.Italic);


                #region DONNEES_PRSONNEL
                graphic.FillRectangle(Brushes.WhiteSmoke, 15, 6 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
                graphic.FillRectangle(Brushes.WhiteSmoke, 15, 12 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
                graphic.FillRectangle(Brushes.WhiteSmoke, 15, 14 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
                graphic.FillRectangle(Brushes.WhiteSmoke, 15, 16 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
                graphic.FillRectangle(Brushes.WhiteSmoke, 15, 19 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
                graphic.DrawString("Nom : ", fnt11, Brushes.Black, 15, 6 * unite_hauteur);
                graphic.DrawString("Prénom : ", fnt11, Brushes.Black, 12 * unite_largeur, 6 * unite_hauteur);
                graphic.DrawString(personnel.Nom, fnt1, Brushes.Black, 4 * unite_largeur - 15, 6 * unite_hauteur);
                graphic.DrawString(personnel.Prenom, fnt1, Brushes.Black, 16 * unite_largeur - 5, 6 * unite_hauteur);
                graphic.DrawString("Numéro matricule : ", fnt11, Brushes.Black, 15, 7 * unite_hauteur);
                graphic.DrawString("N° Sécurité sociale : ", fnt11, Brushes.Black, 12 * unite_largeur, 7 * unite_hauteur);
                graphic.DrawString(personnel.NumeroMatricule, fnt1, Brushes.Black, 4 * unite_largeur - 15, 7 * unite_hauteur);
                graphic.DrawString(service.NoCNPS, fnt1, Brushes.Black, 16 * unite_largeur - 5, 7 * unite_hauteur);

                graphic.FillRectangle(Brushes.Gainsboro, 15, 10 * unite_hauteur - 5, 24 * unite_largeur, 1 * unite_hauteur + 3);
                graphic.DrawString("BULLETIN DE PAIE  " + paiement.MoisPaiement.ToUpper() + " " + paiement.Exercice, fnt3, Brushes.Black, 9 * unite_largeur - 5, 10 * unite_hauteur - 4);

                graphic.DrawString("Nom : ", fnt11, Brushes.Black, 15, 11 * unite_hauteur);
                graphic.DrawString("Type contrat : ", fnt11, Brushes.Black, 15, 12 * unite_hauteur);
                graphic.DrawString("Catégorie : ", fnt11, Brushes.Black, 15, 13 * unite_hauteur);
                graphic.DrawString("Echelon : ", fnt11, Brushes.Black, 15, 14 * unite_hauteur);
                graphic.DrawString("Date de naissance : ", fnt11, Brushes.Black, 15, 15 * unite_hauteur);
                graphic.DrawString("Période du : ", fnt11, Brushes.Black, 15, 16 * unite_hauteur);
                graphic.DrawString("Situation matrimoniale ", fnt11, Brushes.Black, 15, 17 * unite_hauteur);
                //graphic.DrawString("Echelon : ", fnt11, Brushes.Black, 15, 18 * unite_hauteur);
                graphic.DrawString("Adresse de l'employé : ", fnt11, Brushes.Black, 15, 19 * unite_hauteur);

                graphic.DrawString(personnel.Nom, fnt1, Brushes.Black, 4 * unite_largeur + 15, 11 * unite_hauteur);
                graphic.DrawString(service.Contrat, fnt1, Brushes.Black, 4 * unite_largeur + 15, 12 * unite_hauteur);
                graphic.DrawString(service.Categorie, fnt1, Brushes.Black, 4 * unite_largeur + 15, 13 * unite_hauteur);
                graphic.DrawString(service.Echelon, fnt1, Brushes.Black, 4 * unite_largeur + 15, 14 * unite_hauteur);
                //graphic.DrawString(ObtenirDebutJour(paiement.MoisPaiement, paiement.Exercice).ToShortDateString(), fnt1, Brushes.Black, 4 * unite_largeur + 15, 14 * unite_hauteur);
                graphic.DrawString(personnel.DateNaissance.ToShortDateString(), fnt1, Brushes.Black, 4 * unite_largeur + 15, 15 * unite_hauteur);
                graphic.DrawString(ObtenirDebutJour(paiement.MoisPaiement, paiement.Exercice).ToShortDateString(), fnt1, Brushes.Black, 4 * unite_largeur + 15, 16 * unite_hauteur);
                //graphic.DrawString(service.Contrat, fnt1, Brushes.Black, 4 * unite_largeur + 15, 17 * unite_hauteur);
                //graphic.DrawString(service.Echelon, fnt1, Brushes.Black, 4 * unite_largeur + 15, 18 * unite_hauteur);
                graphic.DrawString(personnel.Adresse, fnt1, Brushes.Black, 4 * unite_largeur + 15, 19 * unite_hauteur);


                graphic.DrawString("Prénom : ", fnt11, Brushes.Black, 9 * unite_largeur, 11 * unite_hauteur);
                graphic.DrawString("Emploi : ", fnt11, Brushes.Black, 9 * unite_largeur, 12 * unite_hauteur);
                graphic.DrawString("Banque : ", fnt11, Brushes.Black, 9 * unite_largeur, 13 * unite_hauteur);
                graphic.DrawString("Guichet : ", fnt11, Brushes.Black, 9 * unite_largeur, 14 * unite_hauteur);
                graphic.DrawString("Date d'embauche : ", fnt11, Brushes.Black, 9 * unite_largeur, 15 * unite_hauteur);
                graphic.DrawString("Au : ", fnt11, Brushes.Black, 9 * unite_largeur, 16 * unite_hauteur);
                graphic.DrawString("Nombre d'enfant\nen charge : ", fnt11, Brushes.Black, 9 * unite_largeur, 17 * unite_hauteur);

                graphic.DrawString(personnel.Prenom, fnt1, Brushes.Black, 13 * unite_largeur, 11 * unite_hauteur);
                graphic.DrawString(service.Poste, fnt1, Brushes.Black, 13 * unite_largeur, 12 * unite_hauteur);
                graphic.DrawString(personnel.Banque, fnt1, Brushes.Black, 13 * unite_largeur, 13 * unite_hauteur);
                graphic.DrawString(personnel.CodeGuichet, fnt1, Brushes.Black, 13 * unite_largeur, 14 * unite_hauteur);
                graphic.DrawString(service.DateService.ToShortDateString(), fnt1, Brushes.Black, 13 * unite_largeur, 15 * unite_hauteur);
                graphic.DrawString(ObtenirFinJour(paiement.MoisPaiement, paiement.Exercice).ToShortDateString(), fnt1, Brushes.Black, 13 * unite_largeur, 16 * unite_hauteur);
                graphic.DrawString("".ToString(), fnt1, Brushes.Black, 13 * unite_largeur, 17 * unite_hauteur);


                graphic.DrawString("Numéro matricule : ", fnt11, Brushes.Black, 17 * unite_largeur, 12 * unite_hauteur);
                graphic.DrawString("N° compte bancaire : ", fnt11, Brushes.Black, 17 * unite_largeur, 13 * unite_hauteur);
                graphic.DrawString("Code banque : ", fnt11, Brushes.Black, 17 * unite_largeur, 14 * unite_hauteur);
                graphic.DrawString("Date depart : ", fnt11, Brushes.Black, 17 * unite_largeur, 15 * unite_hauteur);
                graphic.DrawString("Date paiement : ", fnt11, Brushes.Black, 17 * unite_largeur, 16 * unite_hauteur);
                graphic.DrawString("Clé", fnt11, Brushes.Black, 17 * unite_largeur, 17 * unite_hauteur);

                graphic.DrawString(personnel.NumeroMatricule, fnt1, Brushes.Black, 21 * unite_largeur, 12 * unite_hauteur);
                graphic.DrawString(personnel.NumeroCompte, fnt1, Brushes.Black, 21 * unite_largeur, 13 * unite_hauteur);
                graphic.DrawString(personnel.CodeBanque, fnt1, Brushes.Black, 21 * unite_largeur, 14 * unite_hauteur);
                graphic.DrawString(service.DateFinContrat.ToShortDateString(), fnt1, Brushes.Black, 21 * unite_largeur, 15 * unite_hauteur);
                graphic.DrawString(paiement.DatePaiement.ToShortDateString(), fnt1, Brushes.Black, 21 * unite_largeur, 16 * unite_hauteur);
                graphic.DrawString(personnel.Cle, fnt1, Brushes.Black, 21 * unite_largeur, 17 * unite_hauteur);
                #endregion

                #region GAINS
                graphic.FillRectangle(Brushes.Gainsboro, 16, 20 * unite_hauteur + 10, 24 * unite_largeur - 2, 1 * unite_hauteur + 1);
                graphic.FillRectangle(Brushes.Gainsboro, 12 * unite_largeur + 15, 21 * unite_hauteur + 10, 12 * unite_largeur - 6, 7 * unite_hauteur + 1);
                graphic.FillRectangle(Brushes.WhiteSmoke, 15, 27 * unite_hauteur + 10, 12 * unite_largeur - 1, 1 * unite_hauteur + 1);


                graphic.DrawRectangle(Pens.Black, 15, 20 * unite_hauteur + 10, 6 * unite_largeur, unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 20 * unite_hauteur + 10, 3 * unite_largeur, unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 20 * unite_hauteur + 10, 3 * unite_largeur, unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 20 * unite_hauteur + 10, 3 * unite_largeur, unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 20 * unite_hauteur + 10, 6 * unite_largeur - 8, unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 7, 20 * unite_hauteur + 10, 3 * unite_largeur + 2, unite_hauteur + 0);

                graphic.DrawString("GAINS", fnt11, Brushes.Black, 17, 20 * unite_hauteur + 10);
                graphic.DrawString("Base ", fnt11, Brushes.Black, 6 * unite_largeur + 20, 20 * unite_hauteur + 10);
                graphic.DrawString("Taux", fnt11, Brushes.Black, 9 * unite_largeur + 20, 20 * unite_hauteur + 10);
                graphic.DrawString("Montant", fnt11, Brushes.Black, 12 * unite_largeur + 20, 20 * unite_hauteur + 10);
                graphic.DrawString(" ", fnt11, Brushes.Black, 17 * unite_largeur, 20 * unite_hauteur + 10);
                graphic.DrawString("Cumul de l'année", fnt11, Brushes.Black, 21 * unite_largeur + 12, 20 * unite_hauteur + 10);

                graphic.DrawRectangle(Pens.Black, 15, 21 * unite_hauteur + 10, 6 * unite_largeur, 7 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 21 * unite_hauteur + 10, 3 * unite_largeur, 7 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 21 * unite_hauteur + 10, 3 * unite_largeur, 7 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 21 * unite_hauteur + 10, 3 * unite_largeur, 7 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 21 * unite_hauteur + 10, 6 * unite_largeur - 8, 7 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 7, 21 * unite_hauteur + 10, 3 * unite_largeur + 2, 7 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15, 27 * unite_hauteur + 10, 24 * unite_largeur - 5, 1 * unite_hauteur + 0);

                graphic.DrawString("Salaire De Base ", fnt1, Brushes.Black, 17, 21 * unite_hauteur + 10);
                graphic.DrawString("Ancienneté : ", fnt1, Brushes.Black, 17, 22 * unite_hauteur + 10);
                graphic.DrawString("Heures Supplementaire ", fnt1, Brushes.Black, 17, 23 * unite_hauteur + 10);
                graphic.DrawString("Indemnités de transport", fnt1, Brushes.Black, 17, 24 * unite_hauteur + 10);
                graphic.DrawString("Congés payé", fnt1, Brushes.Black, 17, 25 * unite_hauteur + 10);
                //graphic.DrawString("Coûts d'absence", fnt1, Brushes.Black, 17, 26 * unite_hauteur + 10);
                graphic.DrawString("Salaire Brut ".ToUpper(), fnt11, Brushes.Black, 17, 27 * unite_hauteur + 10);

                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireBase), fnt1, Brushes.Black, 15 * unite_largeur + 10, 21 * unite_hauteur + 13, drawFormatRight);
                double anciennete;
                if (service.Anciennete.Contains("%"))
                {
                    service.Anciennete.Remove(service.Anciennete.IndexOf("%"));
                }
                if (double.TryParse(service.Anciennete, out anciennete))
                {
                }
                else
                {
                    anciennete = 0;
                }
                paiement.GainAnciennete = Math.Round(paiement.SalaireBase * anciennete / 100);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.GainAnciennete), fnt1, Brushes.Black, 15 * unite_largeur + 10, 22 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.HeureSupplementaire), fnt1, Brushes.Black, 15 * unite_largeur + 10, 23 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.Transport), fnt1, Brushes.Black, 15 * unite_largeur + 10, 24 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel), fnt1, Brushes.Black, 15 * unite_largeur + 10, 25 * unite_hauteur + 13, drawFormatRight);
                //graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutAbsence), fnt1, Brushes.Black, 12 * unite_largeur + 20, 26 * unite_hauteur + 13);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireBrut), fnt11, Brushes.Black, 15 * unite_largeur + 10, 27 * unite_hauteur + 11, drawFormatRight);

                var coutAbsc = ConnectionClass.CumulDesOperations("cout_absc", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var conge = ConnectionClass.CumulDesOperations("conge", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var count = ConnectionClass.ListeoOrdrePaiementParAnnee(paiement.Exercice).Rows.Count;
                var gainAnciennete = paiement.GainAnciennete * count;
                if (conge > 0)
                { gainAnciennete = gainAnciennete + paiement.GainAnciennete; }

                graphic.DrawString(String.Format(elGR, "{0:0,0}", ConnectionClass.CumulDesOperations("salaireBase", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement)), fnt1, Brushes.Black, 24 * unite_largeur + 5, 21 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", gainAnciennete), fnt1, Brushes.Black, 24 * unite_largeur + 5, 22 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", ConnectionClass.CumulDesOperations("heure_supp", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement)), fnt1, Brushes.Black, 24 * unite_largeur + 5, 23 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", ConnectionClass.CumulDesOperations("ind_transp", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement)), fnt1, Brushes.Black, 24 * unite_largeur + 5, 24 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", conge), fnt1, Brushes.Black, 24 * unite_largeur + 5, 25 * unite_hauteur + 13, drawFormatRight);
                //graphic.DrawString(String.Format(elGR, "{0:0,0}", coutAbsc), fnt1, Brushes.Black, 21 * unite_largeur + 15, 26 * unite_hauteur + 13);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", ConnectionClass.CumulDesOperations("salaire_brut", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement)), fnt11, Brushes.Black, 24 * unite_largeur + 5, 27 * unite_hauteur + 11, drawFormatRight);

                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireBase), fnt1, Brushes.Black, 9 * unite_largeur + 13, 22 * unite_hauteur + 12, drawFormatRight);
                graphic.DrawString(Math.Round(paiement.GainAnciennete * 100 / paiement.SalaireBase) + "%", fnt1, Brushes.Black, 12 * unite_largeur + 10, 22 * unite_hauteur + 12, drawFormatRight);
                #endregion


                #region RETENUES
                graphic.FillRectangle(Brushes.Gainsboro, 15, 29 * unite_hauteur + 13, 24 * unite_largeur - 7, 1 * unite_hauteur + 1);
                graphic.FillRectangle(Brushes.Gainsboro, 9 * unite_largeur + 15, 28 * unite_hauteur + 13, 12 * unite_largeur - 6, 1 * unite_hauteur + 1);
                graphic.FillRectangle(Brushes.Gainsboro, 12 * unite_largeur + 15, 29 * unite_hauteur + 13, 12 * unite_largeur - 6, 10 * unite_hauteur + 1);
                graphic.FillRectangle(Brushes.WhiteSmoke, 15, 38 * unite_hauteur + 13, 12 * unite_largeur - 1, 1 * unite_hauteur + 1);

                graphic.DrawRectangle(Pens.Black, 15, 29 * unite_hauteur + 13, 24 * unite_largeur - 5, 1 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15, 28 * unite_hauteur + 13, 21 * unite_largeur - 8, 1 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15, 29 * unite_hauteur + 13, 6 * unite_largeur, 10 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 29 * unite_hauteur + 13, 3 * unite_largeur, 10 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 28 * unite_hauteur + 13, 6 * unite_largeur, 11 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 29 * unite_hauteur + 13, 3 * unite_largeur, 10 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 29 * unite_hauteur + 13, 3 * unite_largeur + 10, 10 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 7, 29 * unite_hauteur + 13, 3 * unite_largeur + 2, 10 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15, 38 * unite_hauteur + 13, 24 * unite_largeur - 5, 1 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15, 39 * unite_hauteur + 13, 24 * unite_largeur - 6, 1 * unite_hauteur + 3);

                graphic.DrawString("Part de l' employé", fnt11, Brushes.Black, 11 * unite_largeur, 28 * unite_hauteur + 13);
                graphic.DrawString("Part de l' employeur ", fnt11, Brushes.Black, 16 * unite_largeur + 20, 28 * unite_hauteur + 13);
                graphic.DrawString("RETENUES", fnt11, Brushes.Black, 17, 29 * unite_hauteur + 13);
                graphic.DrawString("Base ", fnt11, Brushes.Black, 6 * unite_largeur + 20, 29 * unite_hauteur + 13);
                graphic.DrawString("Taux", fnt11, Brushes.Black, 9 * unite_largeur + 20, 29 * unite_hauteur + 13);
                graphic.DrawString("Montant", fnt11, Brushes.Black, 12 * unite_largeur + 20, 29 * unite_hauteur + 13);
                graphic.DrawString("Taux ", fnt11, Brushes.Black, 15 * unite_largeur + 20, 29 * unite_hauteur + 13);
                graphic.DrawString("Montant ", fnt11, Brushes.Black, 18 * unite_largeur + 28, 29 * unite_hauteur + 13);
                graphic.DrawString("Cumul de l'année", fnt11, Brushes.Black, 21 * unite_largeur + 12, 29 * unite_hauteur + 13);

                graphic.DrawString("Avance salaire", fnt1, Brushes.Black, 17, 30 * unite_hauteur + 13);
                graphic.DrawString("Acompte ", fnt1, Brushes.Black, 17, 31 * unite_hauteur + 13);
                //graphic.DrawString("Charge soins ", fnt1, Brushes.Black, 17, 32 * unite_hauteur + 13);
                graphic.DrawString("CNPS", fnt1, Brushes.Black, 17, 32 * unite_hauteur + 13);
                graphic.DrawString("IRPP".ToUpper(), fnt1, Brushes.Black, 17, 33 * unite_hauteur + 13);
                graphic.DrawString("FIR", fnt1, Brushes.Black, 17, 34 * unite_hauteur + 13);
                graphic.DrawString("Coûts d'absence", fnt1, Brushes.Black, 17, 35 * unite_hauteur + 13);
                graphic.DrawString("Total", fnt11, Brushes.Black, 17, 38 * unite_hauteur + 13);
                graphic.DrawString("TOTAL RETENUES", fnt11, Brushes.Black, 17, 39 * unite_hauteur + 16);

                var gainSalariale = paiement.SalaireBase + paiement.GainAnciennete;
                var tauxIRPP = .0;
                if ((gainSalariale - paiement.CNPS) * 12 <= 800000)
                    tauxIRPP = 0;
                else if ((gainSalariale - paiement.CNPS) * 12 > 800000)//&& (gainSalariale - paiement.CNPS) * 12 <= 2500000)
                    tauxIRPP = 10.5;
                //else if ((gainSalariale - paiement.CNPS) * 12 > 2500000 && (gainSalariale - paiement.CNPS) * 12 <= 7500000)
                //    tauxIRPP = 20;
                //else if ((gainSalariale - paiement.CNPS) * 12 > 7500000)
                //    tauxIRPP = 30;
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire), fnt1, Brushes.Black, 15 * unite_largeur + 10, 31 * unite_hauteur + 13, drawFormatRight);
                if (paiement.AcomptePaye > 0)
                {
                    var listeAcompte = AppCode.ConnectionClass.ListeDesAccompte(paiement.IDAcompte, personnel.NumeroMatricule, paiement.AcomptePaye);
                    if (listeAcompte.Count() > 0)
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", listeAcompte[0].MontantAcompte), fnt1, Brushes.Black, 9 * unite_largeur + 13, 30 * unite_hauteur + 13, drawFormatRight);
                        graphic.DrawString("Solde : ", fnt1, Brushes.Black, 18 * unite_largeur + 15, 30 * unite_hauteur + 13, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", listeAcompte[0].MontantAcompte - listeAcompte[0].Rembourser), fnt1, Brushes.Black, 21 * unite_largeur + 3, 30 * unite_hauteur + 13, drawFormatRight);

                    }
                }
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.AcomptePaye), fnt1, Brushes.Black, 15 * unite_largeur + 10, 30 * unite_hauteur + 13, drawFormatRight);
   
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CNPS), fnt1, Brushes.Black, 15 * unite_largeur + 10, 32 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.IRPP), fnt1, Brushes.Black, 15 * unite_largeur + 10, 33 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.ONASA), fnt1, Brushes.Black, 15 * unite_largeur + 10, 34* unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutAbsence), fnt1, Brushes.Black, 15 * unite_largeur + 10, 35 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutAbsence + paiement.AcomptePaye + paiement.AvanceSurSalaire +
                     paiement.IRPP + paiement.ONASA + paiement.CNPS), fnt11, Brushes.Black, 15 * unite_largeur + 10, 38 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutAbsence + paiement.AcomptePaye + paiement.AvanceSurSalaire +
         paiement.IRPP + paiement.ONASA + paiement.CNPS), fnt11, Brushes.Black, 15 * unite_largeur + 10, 39 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", "3,5%"), fnt1, Brushes.Black, 12 * unite_largeur + 10, 32 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", tauxIRPP + "%"), fnt1, Brushes.Black, 12 * unite_largeur + 10, 33* unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.ChargePatronale), fnt1, Brushes.Black, 21 * unite_largeur + 3, 32 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", "16,5%"), fnt1, Brushes.Black, 18 * unite_largeur + 15, 32 * unite_hauteur + 13, drawFormatRight);

                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireBrut), fnt1, Brushes.Black, 9 * unite_largeur + 13, 32* unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireBrut - paiement.CNPS), fnt1, Brushes.Black, 9 * unite_largeur + 13, 33* unite_hauteur + 13, drawFormatRight);

                var avanceSurSalaire = ConnectionClass.CumulDesOperations("avanceSa", paiement.Exercice, paiement.NumeroMatricule,paiement.DatePaiement);
                var acompte = ConnectionClass.CumulDesOperations("acompte_paye", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                //var soinFami = ConnectionClass.CumulDesOperations("soinFami", paiement.Exercice, paiement.NumeroMatricule);
                var cnps = ConnectionClass.CumulDesOperations("taux_cnps", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var onasa = ConnectionClass.CumulDesOperations("onasa", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var cnpsCnds = ConnectionClass.CumulDesOperations("charge_pat", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);

                var irpp = ConnectionClass.CumulDesOperations("irpp", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var cumulTotalRetenue = avanceSurSalaire + acompte  + cnps + onasa + coutAbsc + irpp;
                graphic.DrawString(String.Format(elGR, "{0:0,0}", avanceSurSalaire), fnt1, Brushes.Black, 24 * unite_largeur + 5, 31 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", acompte), fnt1, Brushes.Black, 24 * unite_largeur + 5, 30 * unite_hauteur + 13, drawFormatRight);
                //graphic.DrawString(String.Format(elGR, "{0:0,0}", soinFami), fnt1, Brushes.Black, 24 * unite_largeur + 5, 32 * unite_hauteur + 13, drawFormatRight);
                //graphic.DrawString(String.Format(elGR, "{0:0,0}", cnps), fnt1, Brushes.Black, 21 * unite_largeur + 15, 32 * unite_hauteur + 13);
                graphic.DrawString(cnps + " - " + String.Format(elGR, "{0:0,0}", cnpsCnds), fnt1, Brushes.Black, 24 * unite_largeur + 5, 32 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", irpp), fnt1, Brushes.Black, 24 * unite_largeur + 5, 33 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", onasa), fnt1, Brushes.Black, 24 * unite_largeur + 5, 34 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", coutAbsc), fnt1, Brushes.Black, 24 * unite_largeur + 5, 35 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", cumulTotalRetenue), fnt11, Brushes.Black, 24 * unite_largeur + 5, 38 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", cumulTotalRetenue), fnt11, Brushes.Black, 24 * unite_largeur + 5, 39 * unite_hauteur + 13, drawFormatRight);
                #endregion

                #region PRIMES
                graphic.FillRectangle(Brushes.Gainsboro, 16, 40 * unite_hauteur + 19, 24 * unite_largeur - 2, 1 * unite_hauteur + 1);
                //graphic.FillRectangle(Brushes.Gainsboro, 9 * unite_largeur + 15, 41 * unite_hauteur + 19, 12 * unite_largeur - 6, 1 * unite_hauteur + 1);
                graphic.FillRectangle(Brushes.Gainsboro, 12 * unite_largeur + 15, 41 * unite_hauteur + 19, 12 * unite_largeur - 6, 7 * unite_hauteur + 1);
                graphic.FillRectangle(Brushes.WhiteSmoke, 15, 47 * unite_hauteur + 19, 12 * unite_largeur - 1, 1 * unite_hauteur + 1);

                graphic.DrawRectangle(Pens.Black, 15, 40 * unite_hauteur + 19, 24 * unite_largeur - 6, 1 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15, 40 * unite_hauteur + 19, 6 * unite_largeur, 8 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 40 * unite_hauteur + 19, 3 * unite_largeur, 8 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 40 * unite_hauteur + 19, 3 * unite_largeur, 8 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 40 * unite_hauteur + 19, 3 * unite_largeur, 8 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 40 * unite_hauteur + 19, 6 * unite_largeur - 10, 8 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 5, 40 * unite_hauteur + 19, 3 * unite_largeur + 4, 8 * unite_hauteur + 0);
                graphic.DrawRectangle(Pens.Black, 15, 47 * unite_hauteur + 19, 24 * unite_largeur - 5, 1 * unite_hauteur + 0);
                //graphic.DrawRectangle(Pens.Black, 15, 3 * unite_hauteur + 16, 24 * unite_largeur - 6, 1 * unite_hauteur + 0);

                graphic.DrawString("PRIMES ET INDEMNITES", fnt11, Brushes.Black, 17, 40 * unite_hauteur + 19);
                graphic.DrawString("Base ", fnt11, Brushes.Black, 6 * unite_largeur + 20, 40 * unite_hauteur + 19);
                graphic.DrawString("Taux", fnt11, Brushes.Black, 9 * unite_largeur + 20, 40 * unite_hauteur + 19);
                graphic.DrawString("Montant", fnt11, Brushes.Black, 12 * unite_largeur + 20, 40 * unite_hauteur + 19);
                graphic.DrawString(" ", fnt11, Brushes.Black, 17 * unite_largeur, 40 * unite_hauteur + 19);
                graphic.DrawString("Cumul de l'année", fnt11, Brushes.Black, 21 * unite_largeur + 12, 40 * unite_hauteur + 19);

                graphic.DrawString("13eme provision", fnt1, Brushes.Black, 17, 41 * unite_hauteur + 20);
                graphic.DrawString("Indemnité de Transport ", fnt1, Brushes.Black, 17, 42 * unite_hauteur + 20);
                graphic.DrawString("Prime de Logement ", fnt1, Brushes.Black, 17, 43 * unite_hauteur + 20);
                graphic.DrawString("Prime de Responsabilté", fnt1, Brushes.Black, 17, 44 * unite_hauteur + 20);
                //graphic.DrawString("Heures Supplementaires", fnt1, Brushes.Black, 17, 45 * unite_hauteur + 20);
                graphic.DrawString("Autres Primes", fnt1, Brushes.Black, 17, 45 * unite_hauteur + 20);
                graphic.DrawString("TOTAL PRIMES", fnt11, Brushes.Black, 17, 47 * unite_hauteur + 20);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.PrimeGarde), fnt1, Brushes.Black, 15 * unite_largeur + 10, 41 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.Transport), fnt1, Brushes.Black, 15 * unite_largeur + 10, 42 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.PrimeLogement), fnt1, Brushes.Black, 15 * unite_largeur + 10, 43 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.PrimeResponsabilite), fnt1, Brushes.Black, 15 * unite_largeur + 10, 44 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.HeureSupplementaire), fnt1, Brushes.Black, 15 * unite_largeur + 10, 45 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString("", fnt1, Brushes.Black, 15 * unite_largeur + 10, 46 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.HeureSupplementaire + paiement.PrimeLogement + paiement.Transport +
                    paiement.PrimeResponsabilite + paiement.PrimeGarde), fnt11, Brushes.Black, 15 * unite_largeur + 10, 47 * unite_hauteur + 20, drawFormatRight);
                var primeGarde = ConnectionClass.CumulDesOperations("prime_grd", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var prmeLogement = ConnectionClass.CumulDesOperations("prime_loge", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var primeResponsabilite = ConnectionClass.CumulDesOperations("prime_respo", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var HeureSupp = ConnectionClass.CumulDesOperations("heure_supp", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var indemniteTransport = ConnectionClass.CumulDesOperations("ind_transp", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var totalPrimes = primeGarde + prmeLogement + primeResponsabilite + HeureSupp + indemniteTransport;

                graphic.DrawString(String.Format(elGR, "{0:0,0}", primeGarde), fnt1, Brushes.Black, 24 * unite_largeur + 5, 41 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", indemniteTransport), fnt1, Brushes.Black, 24 * unite_largeur + 5, 42 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", prmeLogement), fnt1, Brushes.Black, 24 * unite_largeur + 5, 43 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", primeResponsabilite), fnt1, Brushes.Black, 24 * unite_largeur + 5, 44 * unite_hauteur + 20, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", HeureSupp), fnt1, Brushes.Black, 24 * unite_largeur + 5, 45 * unite_hauteur + 20,drawFormatRight);
                //graphic.DrawString("Autres Primes", fnt1, Brushes.Black, 17, 46 * unite_hauteur + 20);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", totalPrimes), fnt11, Brushes.Black, 24 * unite_largeur + 5, 47 * unite_hauteur + 20, drawFormatRight);
                #endregion
                graphic.FillRectangle(Brushes.Gainsboro, 15, 48 * unite_hauteur + 23, 24 * unite_largeur - 6, 1 * unite_hauteur + 1);
                //graphic.FillRectangle(Brushes.Gainsboro, 15, 49 * unite_hauteur + 28, 24 * unite_largeur - 6, 1 * unite_hauteur + 1);

                graphic.DrawRectangle(Pens.Black, 15, 48 * unite_hauteur + 22, 24 * unite_largeur - 6, 1 * unite_hauteur + 2);
                graphic.DrawRectangle(Pens.Black, 15, 48 * unite_hauteur + 22, 6 * unite_largeur, unite_hauteur + 2);
                graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 48 * unite_hauteur + 22, 3 * unite_largeur, unite_hauteur + 2);
                graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 48 * unite_hauteur + 22, 3 * unite_largeur, unite_hauteur + 2);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 48 * unite_hauteur + 22, 3 * unite_largeur, unite_hauteur + 2);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 48 * unite_hauteur + 22, 6 * unite_largeur - 10, unite_hauteur + 2);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 5, 48 * unite_hauteur + 22, 3 * unite_largeur + 4, unite_hauteur + 2);

                graphic.DrawString("NET A PAYER ", fnt11, Brushes.Black, 17, 48 * unite_hauteur + 24);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireNet), fnt11, Brushes.Black, 15 * unite_largeur + 10, 48 * unite_hauteur + 24, drawFormatRight);
                graphic.DrawRectangle(Pens.Black, 15, 49 * unite_hauteur + 28, 24 * unite_largeur - 6, 1 * unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 15, 49 * unite_hauteur + 28, 6 * unite_largeur, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 49 * unite_hauteur + 28, 3 * unite_largeur, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 49 * unite_hauteur + 28, 3 * unite_largeur, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 49 * unite_hauteur + 28, 3 * unite_largeur, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 49 * unite_hauteur + 28, 6 * unite_largeur - 10, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 5, 49 * unite_hauteur + 28, 3 * unite_largeur + 4, unite_hauteur + 10);
                graphic.DrawString("COÛT SALARIAL ", fnt11, Brushes.Black, 17, 49 * unite_hauteur + 30);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutDuSalarie), fnt11, Brushes.Black, 15 * unite_largeur + 10, 49 * unite_hauteur + 30, drawFormatRight);
                var cout_salariale = ConnectionClass.CumulDesOperations("cout_salariale", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                var salaire_net = ConnectionClass.CumulDesOperations("salaire_net", paiement.Exercice, paiement.NumeroMatricule, paiement.DatePaiement);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", cout_salariale), fnt11, Brushes.Black, 24 * unite_largeur + 5, 49 * unite_hauteur + 30, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", salaire_net), fnt11, Brushes.Black, 24 * unite_largeur + 5, 48 * unite_hauteur + 24, drawFormatRight);
                graphic.DrawRectangle(Pens.Black, 15, 51 * unite_hauteur + 20, 12 * unite_largeur, unite_hauteur * 9);
                graphic.DrawRectangle(Pens.Black, 15, 51 * unite_hauteur + 20, 24 * unite_largeur - 6, unite_hauteur * 9);
                graphic.DrawString("L' employé(e)".ToUpper(), fnt11, Brushes.Black, 17, 51 * unite_hauteur + 22);
                graphic.DrawString("Le Directeur".ToUpper(), fnt11, Brushes.Black, 12 * unite_largeur + 18, 51 * unite_hauteur + 22);

                return bitmap;
            }
            catch (Exception exception)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", exception);
                return null;
            }
        }
       
        //imprimer acompte
        public static Bitmap ImprimerListeDesAcomptes(DataGridView dgvAvance, string mois, int exercice)
        {
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 64 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3* unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt0 = new Font("Arial Narrow", 9, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 12, FontStyle.Bold);
            Font fnt33 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Underline);
            // dessiner les ecritures 
            if(!string.IsNullOrEmpty(mois))
            {
            graphic.DrawString("Liste des acomptes du mois "+ mois + " " + exercice, fnt3, Brushes.Black, unite_largeur, 3 * unite_hauteur+5);
            }else
            {
                graphic.DrawString("Liste des acomptes", fnt3, Brushes.Black, unite_largeur, 3 * unite_hauteur+5);
            }
            graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black,18* unite_largeur, 3* unite_hauteur + 5);

            graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, 5 * unite_hauteur-2, 23 * unite_largeur + 2, unite_hauteur+5);
            //graphic.DrawRectangle(Pens.White, unite_largeur - 1, 15 * unite_hauteur - 1, 9 * unite_largeur, unite_hauteur + 2);
            graphic.DrawString("N° ", fnt11, Brushes.White, unite_largeur , 5 * unite_hauteur);
            graphic.DrawString("Date ", fnt11, Brushes.White, 2*unite_largeur + 10, 5 * unite_hauteur); 
            graphic.DrawString("Nom du salarié ", fnt11, Brushes.White, unite_largeur * 6, 5 * unite_hauteur);
            graphic.DrawString("Montant ", fnt11, Brushes.White, unite_largeur * 17, 5 * unite_hauteur);
            graphic.DrawString("Signature ", fnt11, Brushes.White, unite_largeur * 20, 5* unite_hauteur);
            var total = 0.0;
            for (int i = 0; i < dgvAvance.Rows.Count; i++)
            {
                var YLOC = 6 * unite_hauteur + 6 + i * 20;
                
                graphic.DrawLine(Pens.Salmon, unite_largeur, YLOC, unite_largeur * 24,  YLOC);
               
                if(i%2==1)
                graphic.FillRectangle(Brushes.PeachPuff, unite_largeur, YLOC, unite_largeur * 23, 20 );
                
                graphic.DrawRectangle(Pens.SaddleBrown, unite_largeur, YLOC, 23 * unite_largeur + 2, 20);
                graphic.DrawString((i+1).ToString(), fnt1, Brushes.Black, unite_largeur + 10, YLOC+3);
                graphic.DrawString(dgvAvance.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 2*unite_largeur + 10, YLOC+3);
                graphic.DrawString(dgvAvance.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 6, YLOC+3);
                graphic.DrawString(dgvAvance.Rows[i].Cells[4].Value.ToString() , fnt1, Brushes.Black, unite_largeur * 17, YLOC+3);
                total += Double.Parse(dgvAvance.Rows[i].Cells[4].Value.ToString());
            }
            var LOC = 6 * unite_hauteur + 8 + dgvAvance.Rows.Count* 20;
            graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, LOC, 23 * unite_largeur + 2, unite_hauteur+5);
            graphic.DrawString("Total", fnt11, Brushes.White, unite_largeur + 10, LOC+2);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", total) + " F.CFA", fnt11, Brushes.White, 17 * unite_largeur-0, LOC+2);
            //graphic.DrawString("Fait à N’Djamena, le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 2 * unite_largeur, 51 * unite_hauteur);
            //graphic.DrawString("Le Responsable", fnt2, Brushes.Black, 16 * unite_largeur, 51 * unite_hauteur);
            //graphic.DrawString("Signature du l'employeur ", fnt2, Brushes.SlateGray, 16 * unite_largeur, 38 * unite_hauteur);
            return bitmap;
        }
    
       public static Bitmap ImprimerListeDesFraisConges(DataGridView dgvConge, string mois, int exercice)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur + 10;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 56 * unite_hauteur;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion
           try
           {
               Image logo = global::SGSP.Properties.Resources.Logo;
               graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
           }
           catch { } //definir les polices 
           Font fnt1 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
           Font fnt11 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
           Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold);
           Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt2 = new Font("Arial Unicode MS", 11, FontStyle.Underline);
           // dessiner les ecritures 
           if (!string.IsNullOrEmpty(mois))
           {
               graphic.DrawString("Frais de congé du mois " + mois + " " + exercice, fnt3, Brushes.Black, unite_largeur, 5 * unite_hauteur + 3);
           }
           else
           {
               graphic.DrawString("Frais de congé ", fnt3, Brushes.Black, unite_largeur, 5 * unite_hauteur + 3);
           }
           graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 18 * unite_largeur, 5 * unite_hauteur + 5);

           graphic.FillRectangle(Brushes.Lavender, 17, 7 * unite_hauteur - 2,1  * unite_largeur , unite_hauteur *2);
           graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 7 * unite_hauteur - 2, 8 * unite_largeur+11, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur*10, 7 * unite_hauteur - 2, 4 * unite_largeur-2, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 14, 7 * unite_hauteur - 2, 4 * unite_largeur-4, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 18, 7 * unite_hauteur - 2, 3 * unite_largeur - 18, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 21 - 16, 7 * unite_hauteur - 2, 3 * unite_largeur+16, unite_hauteur * 2);


           graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 7 * unite_hauteur + 10);
           graphic.DrawString("Nom du salarié ", fnt11, Brushes.Black, unite_largeur + 25, 7 * unite_hauteur+10);
           graphic.DrawString("Service ", fnt11, Brushes.Black, unite_largeur * 10 + 15, 7 * unite_hauteur + 10);
           graphic.DrawString("Fonction ", fnt11, Brushes.Black, unite_largeur * 14 + 15, 7 * unite_hauteur + 10);
           graphic.DrawString("Montant ", fnt11, Brushes.Black, unite_largeur * 18 + 5, 7 * unite_hauteur + 10);
           graphic.DrawString("Observation ", fnt11, Brushes.Black, 21 * unite_largeur -5, 7 * unite_hauteur + 10);
           var total = 0.0;
           for (int i = 0; i < dgvConge.Rows.Count; i++)
           {
               var YLOC = 9 * unite_hauteur + i * 35;

               graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, 33);
               graphic.DrawRectangle(Pens.Black, 20 + unite_largeur,  YLOC, 8 * unite_largeur + 10, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 10, YLOC, 4 * unite_largeur - 3, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 14,  YLOC, 4 * unite_largeur - 3, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 18,  YLOC, 3 * unite_largeur - 19, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 21 - 16, YLOC, 3 * unite_largeur + 14, 33);

               var dt = ConnectionClass.ListeDesPersonnelParNumeroMatricule(dgvConge.Rows[i].Cells[1].Value.ToString());
               var service = "";
               var qualification = "";
               if (dt.Rows.Count > 0)
               {
                   service=dt.Rows[0].ItemArray[11].ToString();
                   qualification = dt.Rows[0].ItemArray[12].ToString();
               }
               if (qualification.Length > 17)
               {
                   qualification = qualification.Substring(0, 17) + "\n" + qualification.Substring(17);
               }
               if (!string.IsNullOrWhiteSpace(qualification))
               {
                   qualification = qualification.Substring(0, 1).ToUpper() + qualification.Substring(1).ToLower();
               }
               if (qualification.Length <= 5)
               {
                   qualification = qualification.ToUpper();
               }
               if (!string.IsNullOrWhiteSpace(service ))
               {
                   service = service.Substring(0, 1).ToUpper() + service.Substring(1).ToLower();
               }
               graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, 20, YLOC + 3);
               graphic.DrawString(dgvConge.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black,  unite_largeur + 22, YLOC + 3);
               graphic.DrawString(service, fnt1, Brushes.Black, unite_largeur * 10+5, YLOC + 3);
               graphic.DrawString(qualification, fnt1, Brushes.Black, unite_largeur * 14 + 10, YLOC + 3);
               graphic.DrawString(dgvConge.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, unite_largeur*18+15, YLOC + 3);
               total += Double.Parse(dgvConge.Rows[i].Cells[3].Value.ToString());
           }
           var LOC = 9 * unite_hauteur + 3 + dgvConge.Rows.Count * 35;
           graphic.DrawRectangle(Pens.Black, 17, LOC, 23 * unite_largeur + 15, 2*unite_hauteur );
           graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", total) , fnt11, Brushes.Black, 18 * unite_largeur +12, LOC +12);

           graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + unite_hauteur * 6 - 10);
           graphic.DrawString("Le Directeur ", fnt11, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);
           var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
           if (dtP.Rows.Count > 0)
           {
               graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
           }
           return bitmap;
       }

       public static Bitmap ImprimerListeDesCNPS(List<Paiement> liste, double montant, double montantCNPS, string mois, int exercice, int start)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur + 10;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 56 * unite_hauteur;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion
           try
           {
               Image logo = global::SGSP.Properties.Resources.Logo;
               graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3* unite_hauteur);
           }
           catch { } //definir les polices 
           Font fnt1 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
           Font fnt11 = new Font("Arial Narrow", 10.0f, FontStyle.Bold);
           Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold);
           Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt2 = new Font("Arial Unicode MS", 11, FontStyle.Bold );
           // dessiner les ecritures 
           graphic.DrawString("BORDEREAU DE LA CNPS DU MOIS DE " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, unite_largeur, 4 * unite_hauteur + 3);
          
           graphic.DrawString("Page" + (start+1).ToString(),fnt0, Brushes.Black, 22 * unite_largeur-10,  unite_hauteur );
           graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 18 * unite_largeur, 5 * unite_hauteur + 7);

           graphic.FillRectangle(Brushes.Lavender, 17, 7 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 7 * unite_hauteur - 2, 10 * unite_largeur + 11, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 12, 7 * unite_hauteur - 2, 3 * unite_largeur - 2, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 15, 7 * unite_hauteur - 2, 2* unite_largeur - 4, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 17, 7 * unite_hauteur - 2, 3 * unite_largeur - 18, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 20- 16, 7 * unite_hauteur - 2, 3 * unite_largeur + 16, unite_hauteur * 2);
           
           graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 7 * unite_hauteur + 10);
           graphic.DrawString("NOMS & PRENOMS ", fnt11, Brushes.Black, unite_largeur + 25, 7 * unite_hauteur + 10);
           graphic.DrawString("MONTANT ", fnt11, Brushes.Black, unite_largeur * 12 + 10, 7 * unite_hauteur + 10);
           graphic.DrawString("TAUX ", fnt11, Brushes.Black, unite_largeur * 15 + 10, 7 * unite_hauteur + 10);
           graphic.DrawString("CNPS A\n PAYER", fnt11, Brushes.Black, unite_largeur * 17 + 5, 7 * unite_hauteur + 2);
           graphic.DrawString("ASSURANCE ", fnt11, Brushes.Black, 20 * unite_largeur - 12, 7 * unite_hauteur + 10);
          
           var j = 0; var numero = 1 + start * 40;
           for (int i = start * 40; i < liste.Count; i++)
           {
               var YLOC = 9 * unite_hauteur + j * 22 ;
               
                   graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, 19 + unite_largeur, YLOC, 10 * unite_largeur + 11, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, unite_largeur * 12, YLOC, 3 * unite_largeur - 2, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, unite_largeur * 15, YLOC, 2 * unite_largeur - 2, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, unite_largeur * 17, YLOC, 3 * unite_largeur - 18, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 16, YLOC, 3 * unite_largeur + 16, unite_hauteur);

                   var dt = ConnectionClass.ListeDesPersonnelParNumeroMatricule(liste[i].NumeroMatricule);
                   var cnps = "";
                   if (dt.Rows.Count > 0)
                   {
                       cnps = dt.Rows[0].ItemArray[15].ToString();
                   }
                   graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, 20, YLOC + 2);
                   graphic.DrawString(liste[i].Employe, fnt1, Brushes.Black, unite_largeur + 22, YLOC + 2);
                   graphic.DrawString(string.Format(elGR, "{0:0,0}",liste[i].GainSalarial), fnt1, Brushes.Black, unite_largeur * 12 + 15, YLOC + 2);
                   graphic.DrawString("20%", fnt1, Brushes.Black, unite_largeur * 15 + 15, YLOC + 2);
                   graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].GainSalarial*20 / 100), fnt1, Brushes.Black, unite_largeur * 17 + 15, YLOC + 2);
                   graphic.DrawString(cnps.ToLower(), fnt1, Brushes.Black, unite_largeur * 20 - 8 ,YLOC + 2);
                   numero++;
                   j++;
               
           }
           graphic.FillRectangle(Brushes.White, 16, 9* unite_hauteur  + 40*22, 24 * unite_largeur + 16, unite_hauteur * 8);

           var LOC = 9 * unite_hauteur  + j * 22;
           graphic.DrawRectangle(Pens.Black, 17, LOC, 12 * unite_largeur - 19, unite_hauteur*2);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 12 + 0, LOC, 5 * unite_largeur + -2, unite_hauteur*2);
           graphic.DrawRectangle(Pens.Black, unite_largeur * 17, LOC, 6 * unite_largeur +0, unite_hauteur*2);

           graphic.DrawString("Total", fnt2, Brushes.Black, unite_largeur + 10, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", montant), fnt2, Brushes.Black, 12 * unite_largeur + 15, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}",Math.Round( montant*20/100)), fnt2, Brushes.Black, 17 * unite_largeur + 15, LOC + 12);
           graphic.DrawString("Arrêté le présent bordereau à la somme de : "+Converti((int)Math.Round(montant * 20 / 100)) + "Frs CFA", fnt11, Brushes.Black,  unite_largeur + 20, LOC + unite_hauteur * 4 - 15);
          
           graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, LOC + unite_hauteur * 6-10);
           graphic.DrawString("Le Directeur ", fnt2, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    var index = unite_hauteur * 8 + j * 35 + 10;
                    //graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
                    var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                    if (ecart <= 2 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 2 * unite_hauteur + 10);
                    }
                    else if (ecart > 2 * unite_hauteur && ecart <= 3 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 3 * unite_hauteur - 5);
                    }
                    else if (ecart > 3 * unite_hauteur && ecart <= 4 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 4 * unite_hauteur - 5);
                    }
                    else if (ecart > 4 * unite_hauteur && ecart <= 5 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 5* unite_hauteur - 5);
                    }
                    else if (ecart > 5 * unite_hauteur && ecart <= 6 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 6 * unite_hauteur - 5);
                    }
                    else if (ecart > 6 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 7 * unite_hauteur - 5);
                    } 
                }
           return bitmap;
       }

       public static Bitmap ImprimerListeDesIRPPFIR(List<Paiement> liste, double montant,double totalFIR, string mois, int exercice, int start)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur + 10;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 56 * unite_hauteur;
            if(liste.Count()>40)
            {
                hauteur_facture = 54 * unite_hauteur + 1;
            }
           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion
           try
           {
               Image logo = global::SGSP.Properties.Resources.Logo;
               graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3 * unite_hauteur);
           }
           catch { } //definir les polices 
           Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
           Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
           Font fnt11 = new Font("Arial Narrow", 11.0f, FontStyle.Bold);
           Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold);
           Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt2 = new Font("Arial Unicode MS", 10, FontStyle.Bold | FontStyle.Italic);
           // dessiner les ecritures 
           graphic.DrawString("BORDEREAU IRPP / FIR DU MOIS " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, unite_largeur, 4 * unite_hauteur + 3);

           graphic.DrawString("Page" + (start + 1).ToString(), fnt0, Brushes.Black, 22 * unite_largeur - 10, unite_hauteur);
           graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 18 * unite_largeur, 5 * unite_hauteur + 7);

           graphic.FillRectangle(Brushes.Lavender, 17, 7 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 7 * unite_hauteur - 2, 8 * unite_largeur + 0, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 9+21, 7 * unite_hauteur - 2, 3 * unite_largeur - 9, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 12+14, 7 * unite_hauteur - 2, 3 * unite_largeur - 16, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 15, 7 * unite_hauteur - 2, 2 * unite_largeur - 2, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 17, 7 * unite_hauteur - 2, 3 * unite_largeur - 18, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 20 - 16, 7 * unite_hauteur - 2, 1* unite_largeur + 10, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 21- 4, 7 * unite_hauteur - 2, 3 * unite_largeur-12 , unite_hauteur * 2);

           graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 7 * unite_hauteur + 10);
           graphic.DrawString("NOMS & PRENOMS ", fnt11, Brushes.Black, unite_largeur + 25, 7 * unite_hauteur + 10);
           graphic.DrawString("MONTANT\n"+mois.ToLower(), fnt11, Brushes.Black, unite_largeur * 9 + 23, 7 * unite_hauteur + 5);
           graphic.DrawString("TOTAL  ", fnt11, Brushes.Black, unite_largeur * 12 + 22, 7 * unite_hauteur + 10);
           graphic.DrawString("TAUX ", fnt11, Brushes.Black, unite_largeur * 15 + 10, 7 * unite_hauteur + 10);
           graphic.DrawString("IRPP", fnt11, Brushes.Black, unite_largeur * 17 + 10, 7 * unite_hauteur + 10);
           graphic.DrawString("FIR ", fnt11, Brushes.Black, 20 * unite_largeur - 12, 7 * unite_hauteur + 10);
           graphic.DrawString("TOTAL ", fnt11, Brushes.Black, 21 * unite_largeur - 0, 7 * unite_hauteur + 10);

           var j = 0; var numero = 1 + start * 40;
           for (int i = start * 40; i < liste.Count; i++)
           {
               var YLOC = 9 * unite_hauteur + j * 22;
            
               graphic.DrawRectangle (Pens.Black, 17, YLOC, 1 * unite_largeur, unite_hauteur * 1);
               graphic.DrawRectangle(Pens.Black, 19 + unite_largeur, YLOC, 8 * unite_largeur + 0, unite_hauteur * 1);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 9+21, YLOC, 3 * unite_largeur - 9, unite_hauteur * 1);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 12+14, YLOC, 3 * unite_largeur - 16, unite_hauteur * 1);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 15, YLOC, 2 * unite_largeur - 2, unite_hauteur * 1);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 17, YLOC, 3 * unite_largeur - 18, unite_hauteur * 1);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 16, YLOC, 1 * unite_largeur + 10, unite_hauteur * 1);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 21 - 4, YLOC, 3 * unite_largeur - 12, unite_hauteur * 1);

               var baseIRPP = liste[i].GainSalarial * (1 - 3.5 / 100);
               graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, 20, YLOC + 2);
               graphic.DrawString(liste[i].Employe, fnt1, Brushes.Black, unite_largeur + 22, YLOC + 2);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", baseIRPP), fnt1, Brushes.Black, unite_largeur * 9 + 30, YLOC + 3);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", baseIRPP ), fnt1, Brushes.Black, unite_largeur * 12 + 25, YLOC + 3);
               graphic.DrawString("10,50%", fnt1, Brushes.Black, unite_largeur * 15 + 5, YLOC + 2);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].IRPP), fnt1, Brushes.Black, unite_largeur * 17 + 15, YLOC + 2);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].ONASA), fnt1, Brushes.Black, unite_largeur * 20 -0, YLOC + 2);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].IRPP + liste[i].ONASA), fnt1, Brushes.Black, unite_largeur * 21 +8, YLOC + 2);
               numero++;
               j++;

           }
            graphic.FillRectangle(Brushes.White, 16, 9 * unite_hauteur + 40 * 22, 24 * unite_largeur + 16, unite_hauteur * 10);

            var LOC = 9 * unite_hauteur + j * 22;
            //if(j>=liste.Count())

            graphic.DrawRectangle(Pens.Black, 17, LOC, 9 * unite_largeur + 2, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 9 + 21, LOC, 6 * unite_largeur - 23, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15, LOC, 5 * unite_largeur - 18, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 16, LOC, 1 * unite_largeur + 10, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 21 - 4, LOC, 3 * unite_largeur - 12, unite_hauteur * 2);

            graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", Math.Round(montant - montant * 3.5 / 100)), fnt11, Brushes.Black, 12 * unite_largeur + 15, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", Math.Round((montant - montant * 3.5 / 100) * 10.5 / 100)), fnt11, Brushes.Black, 17 * unite_largeur + 15, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", totalFIR), fnt11, Brushes.Black, unite_largeur * 20 - 15, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", totalFIR + Math.Round((montant - montant * 3.5 / 100) * 10.5 / 100)), fnt11, Brushes.Black, unite_largeur * 21 + 8, LOC + 12);
           graphic.DrawString("Arrêté le présent bordereau à la somme de : "+Converti((int)(totalFIR + Math.Round(montant * 10.5 / 100))) + "Frs CFA", fnt11, Brushes.Black,  unite_largeur + 20, LOC + unite_hauteur * 4 - 15);

           graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + unite_hauteur * 6 - 10);
           graphic.DrawString("Le Directeur ", fnt11, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);
           var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
           if (dtP.Rows.Count > 0)
           {
                //graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
                //var index = unite_hauteur * 9 + j * 22 + 10;
               //graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
               var ecart = hauteur_facture - (LOC + 2 * unite_hauteur + 10);
               if (ecart <= 2 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 7 * 22 + 10);
               }
               else if (ecart > 2 * unite_hauteur && ecart <= 3 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 9 * 22 - 5);
               }
               else if (ecart > 3 * unite_hauteur && ecart <= 4 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 11 * 22 - 5);
               }
               else if (ecart > 4 * unite_hauteur && ecart <= 5 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 12 * 22 - 5);
               }
               else if (ecart > 5 * unite_hauteur && ecart <= 6 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * 22 - 5);
               }
               else if (ecart > 6 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 14 * 22 - 5);
               } 
           }
           return bitmap;
       }

        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

       public static Bitmap ImprimerListeRecapitulatifs(DataGridView dgvRecap, string mois, int exercice)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur + 10;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 56 * unite_hauteur;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion
           try
           {
               Image logo = global::SGSP.Properties.Resources.Logo;
               graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
           }
           catch { } //definir les polices 
           Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
           Font fnt0 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
           Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
           Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold | FontStyle.Underline);
           Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt2 = new Font("Arial Unicode MS", 11, FontStyle.Underline);
           // dessiner les ecritures 
           graphic.DrawString("Recapitulatif de l' etat de paiement du   " + mois + " " + exercice, fnt3, Brushes.Black, unite_largeur, 7 * unite_hauteur + 3);
         
           graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 15 * unite_largeur, 5 * unite_hauteur + 5);

           graphic.FillRectangle(Brushes.Lavender, 17, 9 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 9 * unite_hauteur - 2, 14 * unite_largeur + 11, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 16, 9 * unite_hauteur - 2, 4 * unite_largeur - 2, unite_hauteur * 2);


           graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 9 * unite_hauteur + 10);
           graphic.DrawString("Libellé ", fnt11, Brushes.Black, unite_largeur *2, 9 * unite_hauteur + 10);
           graphic.DrawString("Montant ", fnt11, Brushes.Black, unite_largeur * 17 - 10, 9 * unite_hauteur + 10);
           var total = 0.0;
           for (int i = 0; i < dgvRecap.Rows.Count; i++)
           {
               var YLOC = 11 * unite_hauteur + i * 35;

               graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, 33);
               graphic.DrawRectangle(Pens.Black, 20 + unite_largeur, YLOC, 14 * unite_largeur + 10, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 16, YLOC, 4 * unite_largeur - 3, 33);

               graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, 20, YLOC + 3);
               graphic.DrawString(dgvRecap.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 22, YLOC + 3);
               graphic.DrawString(dgvRecap.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 17 -10, YLOC + 3);
               total += Double.Parse(dgvRecap.Rows[i].Cells[3].Value.ToString());
           }
           var LOC = 11 * unite_hauteur +2 + dgvRecap.Rows.Count * 35;
           graphic.DrawRectangle(Pens.Black, 16, LOC, 16 * unite_largeur - 18, 2 * unite_hauteur);
                     graphic.DrawRectangle(Pens.Black, 16*unite_largeur, LOC, 4 * unite_largeur -3, 2 * unite_hauteur);
           graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 17 * unite_largeur - 15, LOC + 12);

           graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 7 * unite_largeur + 0,25*unite_hauteur + 0);
           graphic.DrawString("Le Directeur ", fnt33, Brushes.Black, 8 * unite_largeur + 20, 26* unite_hauteur+10);
           var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
           if (dtP.Rows.Count > 0)
           {
               graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 6 * unite_largeur + 10, 34* unite_hauteur + 15);
           }
            return bitmap;
       }
     
        public static Bitmap ImprimerRecuDePaiement
            (System.Windows.Forms.DataGridView dgvVente)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 25;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 42 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 6 * unite_hauteur);
            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Ubuntu", 12, FontStyle.Regular);
            Font fnt11 = new Font("Ubuntu", 16, FontStyle.Bold);
            Font fnt12 = new Font("Ubuntu", 11, FontStyle.Italic);
            Font fnt3 = new Font("Ubuntu", 11, FontStyle.Bold);
            Font fnt2 = new Font("Ubuntu", 10, FontStyle.Regular);
            Font fnt22 = new Font("Ubuntu", 9, FontStyle.Regular);
            #endregion

            #region Section1
            graphic.FillRectangle(Brushes.Black, 15 * unite_largeur, 8 * unite_hauteur, 8 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 8 * unite_hauteur, 8 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 9 * unite_hauteur, 8 * unite_largeur, 2 * unite_hauteur);
            graphic.DrawString("Acompte  ", fnt11, Brushes.White, 17 * unite_largeur, 8 * unite_hauteur - 1);
            graphic.DrawString("Reçu No : " + dgvVente.SelectedRows[0].Cells[0].Value.ToString(), fnt2, Brushes.Black, 16 * unite_largeur, 9 * unite_hauteur + 4);
            graphic.DrawString("Date :        " + dgvVente.SelectedRows[0].Cells[3].Value.ToString(), fnt2, Brushes.Black, 16 * unite_largeur, 10 * unite_hauteur);

            graphic.DrawString("REÇU DE PAIEMENT DE L' ACOMPTE", fnt11, Brushes.Black, unite_largeur, 16 * unite_hauteur - 4);
            graphic.DrawString("Emis le " + DateTime.Now.ToString(), fnt2, Brushes.Black, unite_largeur, 17 * unite_hauteur);


            graphic.DrawRectangle(Pens.Black, unite_largeur, 19 * unite_hauteur, 23 * unite_largeur, 14 * unite_hauteur);

            graphic.DrawString("N° MATRICULE : " + dgvVente.SelectedRows[0].Cells[1].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur, 13 * unite_hauteur - 10);
            graphic.DrawString("EMPLOYE : " + dgvVente.SelectedRows[0].Cells[2].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur, 14 * unite_hauteur - 10);

            graphic.DrawString("Montant perçu ".ToUpper(), fnt1, Brushes.Black, unite_largeur * 4, 25 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[4].Value.ToString() + " Franc CFA", fnt1, Brushes.Black, 15 * unite_largeur, 25 * unite_hauteur);

            var total = Double.Parse(dgvVente.SelectedRows[0].Cells[4].Value.ToString());
            graphic.DrawString("Arreté la présente somme de : " + Converti((int)total) + " Franc CFA", fnt1, Brushes.Black, 4 * unite_largeur, 27 * unite_hauteur);

            graphic.DrawString("Montant perçu ".ToUpper(), fnt1, Brushes.Black, unite_largeur * 4, 25 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[4].Value.ToString() + " Franc CFA", fnt1, Brushes.Black, 15 * unite_largeur, 25 * unite_hauteur);


            graphic.DrawString("Fait à N’Djamena, le  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 37 * unite_hauteur - 10);
            graphic.DrawString("Signature du l'employé ", fnt1, Brushes.Black, unite_largeur, 38 * unite_hauteur);
            graphic.DrawString("Signature du l'employeur ", fnt1, Brushes.Black, 16 * unite_largeur, 38 * unite_hauteur);

            #endregion


            return bitmap;
        }

        public static Bitmap ImprimerRecuDeAvancesSureSalaire
              (System.Windows.Forms.DataGridView dgvVente)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 25;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 42 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 6 * unite_hauteur);
            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 13, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt12 = new Font("Arial Unicode MS", 11, FontStyle.Italic);
            Font fnt3 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font fnt2 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt22 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
            #endregion

            #region Section1
            graphic.FillRectangle(Brushes.Black, 15 * unite_largeur, 8 * unite_hauteur, 8 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 8 * unite_hauteur, 8 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 9 * unite_hauteur, 8 * unite_largeur, 2 * unite_hauteur);
            graphic.DrawString("Avance sur salaire  ", fnt11, Brushes.White, 17 * unite_largeur - 22, 8 * unite_hauteur - 1);
            graphic.DrawString("N° : " + dgvVente.SelectedRows[0].Cells[0].Value.ToString(), fnt2, Brushes.Black, 16 * unite_largeur, 9 * unite_hauteur + 4);
            graphic.DrawString("Date    :          " + dgvVente.SelectedRows[0].Cells[3].Value.ToString(), fnt2, Brushes.Black, 16 * unite_largeur, 10 * unite_hauteur);

            graphic.DrawString("REÇU DE PAIEMENT DE L'AVANCE SUR SALAIRE", fnt11, Brushes.Black, unite_largeur, 17 * unite_hauteur - 4);
            graphic.DrawString("Emis le " + DateTime.Now.ToString(), fnt2, Brushes.Black, unite_largeur, 18 * unite_hauteur);


            graphic.FillRectangle(Brushes.Black, unite_largeur - 1, 20 * unite_hauteur, 23 * unite_largeur + 2, unite_hauteur);

            graphic.DrawRectangle(Pens.Black, unite_largeur, 21 * unite_hauteur, 6 * unite_largeur, 5 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, 21 * unite_hauteur, 6 * unite_largeur, 5 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 13 * unite_largeur, 21 * unite_hauteur, 6 * unite_largeur, 5 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur, 21 * unite_hauteur, 5 * unite_largeur, 5 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur, 26 * unite_hauteur, 23 * unite_largeur, unite_hauteur);

            graphic.DrawString(dgvVente.SelectedRows[0].Cells[4].Value.ToString() + " F. CFA", fnt1, Brushes.Black, 3 * unite_largeur - 15, 23 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[5].Value.ToString() + " F. CFA", fnt1, Brushes.Black, 8 * unite_largeur + 15, 23 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[6].Value.ToString() + " F. CFA", fnt1, Brushes.Black, 14 * unite_largeur + 15, 23 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[7].Value.ToString() + " F. CFA", fnt1, Brushes.Black, 20 * unite_largeur, 23 * unite_hauteur);

            graphic.DrawString("Montant  ", fnt1, Brushes.White, 3 * unite_largeur, 20 * unite_hauteur);
            graphic.DrawString("Payé  ", fnt1, Brushes.White, 8 * unite_largeur, 20 * unite_hauteur);
            graphic.DrawString("Restant ", fnt1, Brushes.White, 14 * unite_largeur, 20 * unite_hauteur);
            graphic.DrawString("Déduction ", fnt1, Brushes.White, 21 * unite_largeur, 20 * unite_hauteur);

            graphic.DrawString("N° MATRICULE : " + dgvVente.SelectedRows[0].Cells[1].Value.ToString(), fnt1, Brushes.Black, 12 * unite_largeur, 13 * unite_hauteur - 10);
            graphic.DrawString("EMPLOYE : " + dgvVente.SelectedRows[0].Cells[2].Value.ToString(), fnt1, Brushes.Black, 12 * unite_largeur, 14 * unite_hauteur - 10);
            var total = Double.Parse(dgvVente.SelectedRows[0].Cells[4].Value.ToString());
            graphic.DrawString("Somme de : " + Converti((int)total) + " Franc CFA", fnt1, Brushes.Black, unite_largeur + 10, 26 * unite_hauteur);

            graphic.FillRectangle(Brushes.Black, unite_largeur - 1, 28 * unite_hauteur, 23 * unite_largeur + 2, unite_hauteur);
            graphic.DrawString("Détails de remboursement des avances sur salaire ", fnt1, Brushes.White, 8 * unite_largeur, 28 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur, 28 * unite_hauteur, 23 * unite_largeur, 9 * unite_hauteur);

            var IdAcompte = Convert.ToInt32(dgvVente.SelectedRows[0].Cells[0].Value.ToString());
            var liste = ConnectionClass.ListeDetailsPaiementParNumeroAcompte(IdAcompte);
            if (liste.Count > 0)
            {
                var j = 0;
                var q = from p in liste
                        orderby p.DatePaiement
                        select p;
                foreach (var p in q)
                {
                    var YLOC = 29 * unite_hauteur + 15 + j * unite_hauteur;
                    graphic.DrawString(p.DatePaiement.ToShortDateString(), fnt1, Brushes.Black, 9 * unite_largeur, YLOC);
                    graphic.DrawString(p.AcomptePaye.ToString() + " F. CFA", fnt1, Brushes.Black, 14 * unite_largeur, YLOC);
                    j++;
                }
            }
            graphic.DrawString("Fait à N’Djamena, le  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 38 * unite_hauteur - 10);
            graphic.DrawString("Signature du l'employé ", fnt1, Brushes.Black, unite_largeur, 39 * unite_hauteur);
            graphic.DrawString("Signature du l'employeur ", fnt1, Brushes.Black, 16 * unite_largeur, 39 * unite_hauteur);

            #endregion


            return bitmap;
        }


        public static Bitmap ImprimerListeDeAvancesSurSalaire(DataGridView dgv,  int exercice, int start)
        {
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 56 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 10, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 10, FontStyle.Bold | FontStyle.Italic);
            // dessiner les ecritures 
            graphic.DrawString("Liste des avances sur salaire " , fnt3, Brushes.Black, unite_largeur, 4 * unite_hauteur + 15);

            graphic.DrawString("Page" + (start + 1).ToString(), fnt0, Brushes.Black, 22 * unite_largeur - 10, unite_hauteur);
            graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 18 * unite_largeur, 5 * unite_hauteur + 7);

            graphic.FillRectangle(Brushes.Lavender, 17, 7 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 7 * unite_hauteur - 2, 9 * unite_largeur + 19, unite_hauteur * 2);
            //graphic.FillRectangle(Brushes.Lavender, unite_largeur * 9 + 21, 7 * unite_hauteur - 2, 3 * unite_largeur - 9, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 11 + 9, 7 * unite_hauteur - 2, 2 * unite_largeur+13, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 13+25, 7 * unite_hauteur - 2, 2 * unite_largeur +13, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 16+9, 7 * unite_hauteur - 2, 2 * unite_largeur + 13, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 18+25, 7 * unite_hauteur - 2, 2 * unite_largeur + 13, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 21 +9, 7 * unite_hauteur - 2, 2 * unite_largeur +16, unite_hauteur * 2);

            graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 7 * unite_hauteur + 10);
            graphic.DrawString("NOMS & PRENOMS ", fnt11, Brushes.Black, unite_largeur + 25, 7 * unite_hauteur + 10);
            graphic.DrawString("DATE\n", fnt11, Brushes.Black, unite_largeur * 11 + 23, 7 * unite_hauteur + 10);
            graphic.DrawString("MONTANT  ", fnt11, Brushes.Black, unite_largeur * 14-6, 7 * unite_hauteur + 10);
            graphic.DrawString("DEDUCT ", fnt11, Brushes.Black, unite_largeur * 16 + 15, 7 * unite_hauteur + 10);
            graphic.DrawString("PAYE", fnt11, Brushes.Black, unite_largeur * 19 + 5, 7 * unite_hauteur + 10);
            graphic.DrawString("SOLDE ", fnt11, Brushes.Black, 21 * unite_largeur +20, 7 * unite_hauteur + 10);

            var j = 0; var numero = 1 + start * 40;
            for (int i = start * 40; i < dgv.Rows.Count; i++)
            {
                var YLOC = 9 * unite_hauteur + j * 22;
                if (dgv.Rows[i].Cells[2].Value.ToString() == "Total")
                {
                    graphic.FillRectangle(Brushes.Lavender, 16 , YLOC-3, 23 * unite_largeur +12, unite_hauteur +3);
              
                    graphic.DrawString(dgv.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.Black, unite_largeur + 25, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 11 + 15, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 14 - 6, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 16 + 15, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 19 + 5, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[7].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 21 + 20, YLOC + 3);
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, 19 + unite_largeur, YLOC, 9 * unite_largeur + 19, unite_hauteur * 2);
                    //graphic.FillRectangle(Brushes.Lavender, unite_largeur * 9 + 21, 7 * unite_hauteur - 2, 3 * unite_largeur - 9, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 11 + 9, YLOC, 2 * unite_largeur + 13, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 13 + 25, YLOC, 2 * unite_largeur + 13, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 16 + 9, YLOC, 2 * unite_largeur + 13, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 18 + 25, YLOC, 2 * unite_largeur + 13, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 21 + 9, YLOC, 2 * unite_largeur + 16, unite_hauteur * 2);

                    graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, 20, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 25, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 11 + 15, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 14 - 6, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 16 + 15, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 19 + 5, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 21 + 20, YLOC + 3);
                }
                numero++;
                j++;

            }
            //graphic.FillRectangle(Brushes.White, 16, 9 * unite_hauteur + 40 * 22, 24 * unite_largeur + 16, unite_hauteur * 8);

            var LOC = 9 * unite_hauteur + j * 22;
            //graphic.DrawRectangle(Pens.Black, 17, LOC, 9 * unite_largeur + 2, unite_hauteur * 2);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 9 + 21, LOC, 6 * unite_largeur - 23, unite_hauteur * 2);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 15, LOC, 5 * unite_largeur - 18, unite_hauteur * 2);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 16, LOC, 1 * unite_largeur + 10, unite_hauteur * 2);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 21 - 4, LOC, 3 * unite_largeur - 12, unite_hauteur * 2);

            //graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", Math.Round(montant - montant * 3.5 / 100)), fnt11, Brushes.Black, 12 * unite_largeur + 15, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", Math.Round((montant - montant * 3.5 / 100) * 10.5 / 100)), fnt11, Brushes.Black, 17 * unite_largeur + 15, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", totalFIR), fnt11, Brushes.Black, unite_largeur * 20 - 15, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", totalFIR + Math.Round((montant - montant * 3.5 / 100) * 10.5 / 100)), fnt11, Brushes.Black, unite_largeur * 21 + 8, LOC + 12);
            //graphic.DrawString("Arrêté le présent bordereau à la somme de : ", fnt11, Brushes.Black, 20, LOC + unite_hauteur * 4 - 15);
            //graphic.DrawString(Converti((int)(totalFIR + Math.Round(montant * 10.5 / 100))) + "Frs CFA", fnt11, Brushes.Black, 9 * unite_largeur + 20, LOC + unite_hauteur * 4 - 15);

            graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + unite_hauteur * 6 - 10);
            graphic.DrawString("Le Directeur ", fnt11, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);
            var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
            if (dtP.Rows.Count > 0)
            {
                graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
            }
            return bitmap;
        }



        public static string Converti(int chiffre)
        {
            int centaine, dizaine, unite, reste, y;
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

        public static Bitmap ImprimerListeDesBancaries(List<Personnel> liste, string bank)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int hauteur_facture = 52 * unite_hauteur;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 16 * unite_largeur, 4 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 11.5f, FontStyle.Regular);
            Font fnt0 = new Font("Arial Narrow", 12.5f, FontStyle.Underline);
            Font fnt11 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
            Font fnt4 = new Font("Arial Narrow", 16, FontStyle.Bold);

            graphic.DrawString("Page 1", fnt1, Brushes.Black, 22 * unite_largeur, 5);
            graphic.DrawString("Liste de personnel", fnt0, Brushes.Black, unite_largeur, 6 * unite_hauteur - 15);
            graphic.DrawString("Nom banque : "+bank, fnt11, Brushes.Black, 16*unite_largeur, 6 * unite_hauteur - 15);

            System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

            graphic.DrawRectangle(Pens.Black, 25, 7 * unite_hauteur, unite_largeur + 25, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 2 * unite_largeur + 21, 7 * unite_hauteur, unite_largeur * 10 - 31, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur - 7, 7 * unite_hauteur, unite_largeur + 28, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 14 * unite_largeur - 8, 7 * unite_hauteur, unite_largeur + 27, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 16 * unite_largeur - 10, 7 * unite_hauteur, unite_largeur * 3 + 27, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 20, 7 * unite_hauteur, unite_largeur + 16, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 7, 7 * unite_hauteur, unite_largeur * 3 - 6, 2 * +unite_hauteur + 9);

            graphic.DrawString("N°\nMat", fnt11, Brushes.Black, unite_largeur + 5, 7 * unite_hauteur + 3);
            graphic.DrawString("Nom & Penom du salarié", fnt11, Brushes.Black, 2 * unite_largeur + 25, 7 * unite_hauteur + 3);
            graphic.DrawString("Code\nBanque", fnt11, Brushes.Black, 12 * unite_largeur - 5, 7 * unite_hauteur + 3);
            graphic.DrawString("Code\nguichet", fnt11, Brushes.Black, 14 * unite_largeur - 6, 7 * unite_hauteur + 3);
            graphic.DrawString("N° compte", fnt11, Brushes.Black, 16 * unite_largeur + 10, 7 * unite_hauteur + 3);
            graphic.DrawString("Clé", fnt11, Brushes.Black, 19 * unite_largeur + 28, 7 * unite_hauteur + 3);
            graphic.DrawString("Montant", fnt11, Brushes.Black, 21 * unite_largeur + 20, 7 * unite_hauteur + 3);

            for (int i = 0; i <= liste.Count() - 1; i++)
            {
                int Yloc = unite_hauteur * i + 9 * unite_hauteur + 12;
                if(i <liste.Count -1)
                { 
                graphic.DrawRectangle(Pens.Black, 25, Yloc, unite_largeur + 25, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 2 * unite_largeur + 21, Yloc, unite_largeur * 10 - 31, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur - 7, Yloc, unite_largeur + 28, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 14 * unite_largeur - 8, Yloc, unite_largeur + 27, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur - 10, Yloc, unite_largeur * 3 + 27, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 20, Yloc, unite_largeur + 16, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 7, Yloc, unite_largeur * 3 - 6, unite_hauteur);

                graphic.DrawString(liste[i].NumeroCompte, fnt1, Brushes.Black, unite_largeur * 16 + 5, Yloc);
                graphic.DrawString(liste[i].CodeBanque, fnt1, Brushes.Black, unite_largeur * 12 - 4, Yloc);
                graphic.DrawString(liste[i].CodeGuichet, fnt1, Brushes.Black, unite_largeur * 14 - 4, Yloc);
                graphic.DrawString(liste[i].Cle, fnt1, Brushes.Black, unite_largeur * 19 + 25, Yloc);

                var numero = (i + 1);
                var num = "";
                if (numero < 10)
                {
                    num = "00" + numero;
                }
                else
                {
                    num = "0" + numero;
                }
                graphic.DrawString(num.ToString(), fnt1, Brushes.Black, unite_largeur + 0, Yloc);
                graphic.DrawString(liste[i].Nom, fnt1, Brushes.Black, 3 * unite_largeur + 0, Yloc);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", double.Parse(liste[i].Telephone1)),
                    fnt1, Brushes.Black, unite_largeur * 21 + 15, Yloc);
            }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 25, Yloc+2, unite_largeur * 23 + 8, unite_hauteur * 2-10);
                    graphic.DrawString(liste[i].Nom , fnt11, Brushes.Black, unite_largeur + 20, Yloc+5 );
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", double.Parse(liste[i].Telephone1)), fnt11, Brushes.Black, unite_largeur * 21,Yloc+5);
                    graphic.DrawString("Le Directeur Général", fnt11, Brushes.Black, unite_largeur *10, Yloc + 2*unite_hauteur+10);
                }

            }

            graphic.FillRectangle(Brushes.White, 10, 40 * unite_hauteur - 9, unite_largeur * 24, unite_hauteur * 15);
           
            return bitmap;
        }

        public static Bitmap ImprimerListeDesBancaries(List<Personnel> liste, int start)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int hauteur_facture = 54 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion

            Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
            Font fnt0 = new Font("Arial Narrow", 12.5f, FontStyle.Underline);
            Font fnt11 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
            Font fnt4 = new Font("Arial Narrow", 16, FontStyle.Bold);

            graphic.DrawString("Page " + (start + 2), fnt1, Brushes.Black, 12 * unite_largeur, 5);
            System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

            graphic.DrawRectangle(Pens.Black, 25, 2 * unite_hauteur, unite_largeur + 25, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 2 * unite_largeur + 21, 2 * unite_hauteur, unite_largeur * 10 - 31, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur - 7, 2 * unite_hauteur, unite_largeur + 28, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 14 * unite_largeur - 8, 2 * unite_hauteur, unite_largeur + 27, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 16 * unite_largeur - 10, 2 * unite_hauteur, unite_largeur * 3 + 27, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 20, 2 * unite_hauteur, unite_largeur + 16, 2 * +unite_hauteur + 9);
            graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 7, 2 * unite_hauteur, unite_largeur * 3 - 6, 2 * +unite_hauteur + 9);

            graphic.DrawString("N° \nMat", fnt11, Brushes.Black, unite_largeur + 5, 2 * unite_hauteur + 3);
            graphic.DrawString("Nom & Penom du salarié", fnt11, Brushes.Black, 2 * unite_largeur + 25, 2 * unite_hauteur + 3);
            graphic.DrawString("Code\nBanque", fnt11, Brushes.Black, 12 * unite_largeur - 6, 2 * unite_hauteur + 3);
            graphic.DrawString("Code\nguichet", fnt11, Brushes.Black, 14 * unite_largeur - 6, 2 * unite_hauteur + 3);
            graphic.DrawString("N° compte", fnt11, Brushes.Black, 16 * unite_largeur + 10, 2 * unite_hauteur + 3);
            graphic.DrawString("Clé", fnt11, Brushes.Black, 19 * unite_largeur + 27, 2 * unite_hauteur + 3);
            graphic.DrawString("Montant", fnt11, Brushes.Black, 21 * unite_largeur + 17, 2 * unite_hauteur + 3);

            var j = 0;
            for (int i = 30 + start * 45; i < liste.Count; i++)
            {
                int Yloc = unite_hauteur * j + 4 * unite_hauteur + 12;
                if (i < liste.Count - 1)
                {
                    graphic.DrawRectangle(Pens.Black, 25, Yloc, unite_largeur + 25, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 2 * unite_largeur + 21, Yloc, unite_largeur * 10 - 31, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur - 7, Yloc, unite_largeur + 28, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 14 * unite_largeur - 8, Yloc, unite_largeur + 27, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 16 * unite_largeur - 10, Yloc, unite_largeur * 3 + 27, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 20, Yloc, unite_largeur + 16, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 7, Yloc, unite_largeur * 3 - 6, unite_hauteur);


                    graphic.DrawString(liste[i].NumeroCompte, fnt1, Brushes.Black, unite_largeur * 16 + 5, Yloc);
                    graphic.DrawString(liste[i].CodeBanque, fnt1, Brushes.Black, unite_largeur * 12 - 4, Yloc);
                    graphic.DrawString(liste[i].CodeGuichet, fnt1, Brushes.Black, unite_largeur * 14 - 4, Yloc);
                    graphic.DrawString(liste[i].Cle, fnt1, Brushes.Black, unite_largeur * 19 + 25, Yloc);

                    var numero = (i + 1);
                    var num = "";
                    if (numero < 10)
                    {
                        num = "0000" + numero;
                    }
                    else
                    {
                        num = "000" + numero;
                    }
                    graphic.DrawString(num.ToString(), fnt1, Brushes.Black, unite_largeur + 0, Yloc);
                    graphic.DrawString(liste[i].Nom, fnt1, Brushes.Black, 3 * unite_largeur + 0, Yloc);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", double.Parse(liste[i].Telephone1)),
                        fnt1, Brushes.Black, unite_largeur * 21 + 15, Yloc);
                }else 
                {
                    graphic.DrawRectangle(Pens.Black, 25, Yloc + 2, unite_largeur * 23 + 8, unite_hauteur * 2-10);
                    graphic.DrawString(liste[i].Nom, fnt11, Brushes.Black, unite_largeur + 20, Yloc+5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", double.Parse(liste[i].Telephone1)), fnt11, Brushes.Black, unite_largeur * 21, Yloc+5);
                    graphic.DrawString("Le Directeur Général", fnt11, Brushes.Black, unite_largeur * 10, Yloc + 2 * unite_hauteur+10);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 10, 50 * unite_hauteur + 1, unite_largeur * 25, unite_hauteur * 10);
               return bitmap;
        }

        public static Bitmap ImprimerLalisteDesRetraitesPersonnels(DataGridView dtListePersonnel, string titreImpression, int start)
        {
            #region
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 38 * unite_hauteur + detail_hauteur_facture;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 24 * unite_largeur, 6 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 9, FontStyle.Italic);
            Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 18, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 12, FontStyle.Regular);

            graphic.DrawString("Page " + (start + 1), fnt33, Brushes.Black, 16, 10);
            graphic.DrawString(titreImpression, fnt11, Brushes.Black, 10, 10 * unite_hauteur);
            graphic.DrawString("Matricule", fnt33, Brushes.Black, 16, 12 * unite_hauteur);
            graphic.DrawString("Nom", fnt33, Brushes.Black, 6 * unite_largeur - 15, 12 * unite_hauteur + 3);
            graphic.DrawString("Prenom", fnt33, Brushes.Black, 11 * unite_largeur, 12 * unite_hauteur + 3);
            graphic.DrawString("Fonction", fnt33, Brushes.Black, 16 * unite_largeur + 15, 12 * unite_hauteur + 3);
            graphic.DrawString("Date", fnt33, Brushes.Black, 22 * unite_largeur, 12 * unite_hauteur + 3);

            graphic.DrawRectangle(Pens.Black, 15, 12 * unite_hauteur, unite_largeur * 2 + 14, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 3, 12 * unite_hauteur, unite_largeur * 6 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 9, 12 * unite_hauteur, unite_largeur * 6 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15, 12 * unite_hauteur, unite_largeur * 6 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 21, 12 * unite_hauteur, unite_largeur * 4 - 3, unite_hauteur);


            for (int i = start * 30; i <= dtListePersonnel.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * i + 13 * unite_hauteur;

                graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur * 2 + 14, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 3, Yloc, unite_largeur * 6 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 9, Yloc, unite_largeur * 6 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 15, Yloc, unite_largeur * 6 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 21, Yloc, unite_largeur * 4 - 3, unite_hauteur);


                graphic.DrawString(dtListePersonnel.Rows[i].Cells[0].Value.ToString(), fnt33, Brushes.Black, 16, Yloc);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[1].Value.ToString(), fnt33, Brushes.Black, 3 * unite_largeur + 3, Yloc);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[2].Value.ToString(), fnt33, Brushes.Black, 9 * unite_largeur + 3, Yloc + 3);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 3, Yloc + 3);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[7].Value.ToString(), fnt33, Brushes.Black, 21 * unite_largeur + 3, Yloc + 3);

            }
            graphic.FillRectangle(Brushes.White, unite_largeur * 22, 44 * unite_hauteur, unite_largeur * 3 - 3, unite_hauteur);

            return bitmap;
        }


        #region RapportDepenses
        //fonction pour dessiner rapport depenses
        public static Bitmap ImprimerRapportDepenses(DataGridView dataGridView,string titre , int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 56 * unite_hauteur ;
            
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur,4 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 9, FontStyle.Bold);
            graphic.DrawString("Page "+(1+start ).ToString(), fnt1, Brushes.Black, 22*unite_largeur,5);

            graphic.DrawString(titre , fnt3, Brushes.Black, unite_largeur, 4 * unite_hauteur + 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur - 4, 23 * unite_largeur, unite_hauteur);
            graphic.DrawString("Date", fnt33, Brushes.Black, unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Code", fnt33, Brushes.Black, 3*unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Libellé", fnt33, Brushes.Black, 4 * unite_largeur +10, 6 * unite_hauteur - 4);
            graphic.DrawString("Bénéficiaire", fnt33, Brushes.Black, 16 * unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Montant", fnt33, Brushes.Black, 21 * unite_largeur, 6 * unite_hauteur-4);
            var j = 0;
            for (int i = 45* start; i <= dataGridView.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur;

                
                 if (!string.IsNullOrEmpty(dataGridView.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.Lavender, 15, Yloc, 23 * unite_largeur, unite_hauteur-2);
                    graphic.DrawString(dataGridView.Rows[i].Cells[1].Value.ToString(), fnt33, Brushes.Black, 3*unite_largeur+5, Yloc);
                     graphic.DrawString(dataGridView.Rows[i].Cells[9].Value.ToString(), fnt33, Brushes.Black, 21 * unite_largeur, Yloc);

                }
                else
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[6].Value.ToString() , fnt1, Brushes.Black, 15, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 3*unite_largeur+5, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 4 * unite_largeur +20, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[8].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[9].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur, Yloc);
                }
                j++;
            }
            //if (dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[2].Value.ToString() == "Total")
            //{
            //    int Yloc = unite_hauteur * dataGridView.Rows.Count + 10 * unite_hauteur;
            //    graphic.FillRectangle(Brushes.White, unite_largeur, Yloc, 24 * unite_largeur, 3 * unite_hauteur);
            //    graphic.FillRectangle(Brushes.Black, unite_largeur, Yloc, 23 * unite_largeur, unite_hauteur);
            //    graphic.DrawString(dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[2].Value.ToString(), fnt33, Brushes.White, unite_largeur, Yloc);
            //    graphic.DrawString(dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[6].Value.ToString(), fnt33, Brushes.White, 21 * unite_largeur + 15, Yloc);
                
            //}
           
                graphic.FillRectangle(Brushes.White, 12, 52 * unite_hauteur, 24 * unite_largeur, 10 * unite_hauteur);
            
            return bitmap;
        }

        public static Bitmap ImprimerRapporRecettes(DataGridView dataGridView, string titre, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 56 * unite_hauteur;

            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 9, FontStyle.Bold);
            graphic.DrawString("Page " + (1 + start).ToString(), fnt1, Brushes.Black, 22 * unite_largeur, 5);

            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur, 4 * unite_hauteur + 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur - 4, 23 * unite_largeur, unite_hauteur);
            graphic.DrawString("Date", fnt33, Brushes.Black, unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Code", fnt33, Brushes.Black, 3 * unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Libellé", fnt33, Brushes.Black, 4 * unite_largeur + 10, 6 * unite_hauteur - 4);
            //graphic.DrawString("Bénéficiaire", fnt33, Brushes.Black, 16 * unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Montant", fnt33, Brushes.Black, 20 * unite_largeur, 6 * unite_hauteur - 4);
            var j = 0;
            for (int i = 45 * start; i <= dataGridView.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur;


                if (!string.IsNullOrEmpty(dataGridView.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.Lavender, 15, Yloc, 23 * unite_largeur, unite_hauteur - 2);
                    graphic.DrawString(dataGridView.Rows[i].Cells[1].Value.ToString(), fnt33, Brushes.Black, 3 * unite_largeur + 5, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[7].Value.ToString(), fnt33, Brushes.Black, 20 * unite_largeur, Yloc);

                }
                else
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 15, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 3 * unite_largeur + 5, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 4 * unite_largeur + 20, Yloc);
                    //graphic.DrawString(dataGridView.Rows[i].Cells[8].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur, Yloc);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 12, 52 * unite_hauteur, 24 * unite_largeur, 10 * unite_hauteur);

            return bitmap;
        }

        public static Bitmap ImprimerRapportJournalCaisse(DataGridView dataGridView, string titre, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 56 * unite_hauteur;

            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Bold);
            graphic.DrawString("Page " + (1 + start).ToString(), fnt1, Brushes.Black, 22 * unite_largeur, 5);

            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur, 3* unite_hauteur + 5);

            graphic.FillRectangle(Brushes.Lavender, 5, 5* unite_hauteur - 4, 23 * unite_largeur+17, unite_hauteur);
            graphic.DrawString("Date", fnt33, Brushes.Black, 10, 5 * unite_hauteur - 4);
            graphic.DrawString("Code", fnt33, Brushes.Black, 3 * unite_largeur-10, 5 * unite_hauteur - 4);
            graphic.DrawString("Libellé", fnt33, Brushes.Black, 4 * unite_largeur + 0, 5 * unite_hauteur - 4);
            graphic.DrawString("Caissier", fnt33, Brushes.Black, 15 * unite_largeur, 5 * unite_hauteur - 4);
            graphic.DrawString("Montant", fnt33, Brushes.Black, 21 * unite_largeur+10, 5* unite_hauteur - 4);
            var j = 0;
            for (int i = 48 * start; i <= dataGridView.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 6 * unite_hauteur;

                var libelle = dataGridView.Rows[i].Cells[7].Value.ToString();
                if(libelle.Length>53)
                {
                    libelle = libelle.Substring(0, 53)+"...";
                }
                if (dataGridView.Rows[i].Cells[3].Value.ToString().Contains("Total + avoir"))
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 15 , Yloc);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", double.Parse(dataGridView.Rows[i].Cells[8].Value.ToString())), fnt33, Brushes.Black, 21 * unite_largeur + 15, Yloc);

                }
                else
                {
                    var total = double.Parse(dataGridView.Rows[i].Cells[8].Value.ToString()) + double.Parse(dataGridView.Rows[i].Cells[9].Value.ToString());
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 15, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 3 * unite_largeur - 10, Yloc);
                    graphic.DrawString(libelle, fnt1, Brushes.Black, 4 * unite_largeur + 8, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, Yloc);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}",total), fnt1, Brushes.Black, 21 * unite_largeur + 15, Yloc);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 12, 54 * unite_hauteur, 24 * unite_largeur, 10 * unite_hauteur);

            return bitmap;
        }


        public static Bitmap ImprimerJournalCaisse(DataGridView dataGridView, string titre, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 56 * unite_hauteur;

            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Bold);
            graphic.DrawString("Page " + (1 + start).ToString(), fnt1, Brushes.Black, 22 * unite_largeur, 5);

            graphic.DrawString(titre, fnt3, Brushes.Black, 5*unite_largeur, 3 * unite_hauteur + 5);

            graphic.FillRectangle(Brushes.Lavender, 2*unite_largeur, 5 * unite_hauteur - 4, 20 * unite_largeur , unite_hauteur);
            graphic.DrawString("Date", fnt33, Brushes.Black, 3*unite_largeur, 5 * unite_hauteur - 4);
            graphic.DrawString("Montant ", fnt33, Brushes.Black, 8 * unite_largeur, 5 * unite_hauteur - 4);
            graphic.DrawString("Depense ", fnt33, Brushes.Black, 13 * unite_largeur + 10, 5 * unite_hauteur - 4);
            graphic.DrawString("Solde ", fnt33, Brushes.Black, 18 * unite_largeur + 10, 5 * unite_hauteur - 4);
            var j = 0;
            for (int i = 48 * start; i <= dataGridView.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 6 * unite_hauteur;
                graphic.DrawRectangle(Pens.Black, 2 * unite_largeur, Yloc , 5 * unite_largeur -3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, Yloc , 5 * unite_largeur-3 , unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 12* unite_largeur, Yloc , 5 * unite_largeur-3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 17 * unite_largeur, Yloc, 5 * unite_largeur, unite_hauteur);
                if (i== dataGridView.Rows.Count-1)
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[0].Value.ToString(), fnt33, Brushes.Black, 3 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[1].Value.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[2].Value.ToString(), fnt33, Brushes.Black, 13 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 18 * unite_largeur, Yloc);
                }
                else
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, 3*unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 8 * unite_largeur , Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur, Yloc);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 5, 54 * unite_hauteur+1, 24 * unite_largeur, 10 * unite_hauteur);

            return bitmap;
        }

        #endregion

        //imprimer l'ordre de paiement
        public static Bitmap ImprimerListedocument(DataGridView dgvView, string titre)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 32 * unite_hauteur + 7;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 34 * unite_largeur, 7 * unite_hauteur);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt33 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt0 = new Font("Century Gothic", 11, FontStyle.Bold);
            Font fnt11 = new Font("Century Gothic", 13, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 18, FontStyle.Underline);
            Font fnt22 = new Font("Century Gothic", 12, FontStyle.Regular);

            #endregion


            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt11, Brushes.Black, unite_largeur * 8, 9 * unite_hauteur + 18);

            graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 12 * unite_hauteur-3, unite_largeur * 35,  unite_hauteur+3);

            graphic.DrawString("N°", fnt0, Brushes.White, unite_largeur + 2, 12 * unite_hauteur + 1);
            graphic.DrawString("EXERCICE", fnt0, Brushes.White, 3 * unite_largeur, 12 * unite_hauteur + 1);
            graphic.DrawString("REFERENCE", fnt0, Brushes.White, 6 * unite_largeur , 12 * unite_hauteur + 1);
            graphic.DrawString("EMIS LE", fnt0, Brushes.White, 11 * unite_largeur -5, 12 * unite_hauteur + 1);
            graphic.DrawString("ENREGISTRER LE", fnt0, Brushes.White, 14 * unite_largeur -13, 12 * unite_hauteur + 1);
            graphic.DrawString("TYPE DOCUMENT", fnt0, Brushes.White, 17 * unite_largeur +10, 12 * unite_hauteur + 1);
            graphic.DrawString("TOTAL HT", fnt0, Brushes.White, 22 * unite_largeur , 12 * unite_hauteur + 1);
            graphic.DrawString("TVA", fnt0, Brushes.White, 25 * unite_largeur , 12 * unite_hauteur + 1);
            graphic.DrawString("TOTAL TTC", fnt0, Brushes.White, 27 * unite_largeur , 12 * unite_hauteur + 1);
            graphic.DrawString("DOCUMENT DE", fnt0, Brushes.White, 30 * unite_largeur , 12 * unite_hauteur + 1);
            //graphic.DrawString("DESTINE A", fnt0, Brushes.White, 24 * unite_largeur - 40, 12 * unite_hauteur + 1);
          
            
            bool flag;
            if (dgvView.Rows.Count <= 18)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            var j = 0;
            graphic.FillRectangle(Brushes.LightYellow, unite_largeur, 13 * unite_hauteur +1, unite_largeur * 2, 18 * unite_hauteur);
            graphic.FillRectangle(Brushes.SteelBlue, unite_largeur*6, 13 * unite_hauteur + 1, unite_largeur * 5-5, 18 * unite_hauteur);
            graphic.FillRectangle(Brushes.Green, unite_largeur * 17-3, 13 * unite_hauteur + 1, unite_largeur * 5 +3, 18 * unite_hauteur);
            graphic.FillRectangle(Brushes.Yellow, unite_largeur * 27 - 3, 13 * unite_hauteur + 1, unite_largeur * 3+3, 18 * unite_hauteur);
            graphic.FillRectangle(Brushes.OrangeRed, unite_largeur * 30, 13 * unite_hauteur + 1, unite_largeur * 5, 18 * unite_hauteur);
          
            for (var i = 0; i < dgvView.Rows.Count; i++)
            {
                var YLOC = unite_hauteur * 13 + 3 + unite_hauteur * j;
                graphic.DrawString(dgvView.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 5, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 3, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.White, unite_largeur * 6, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 11-5, YLOC);
                graphic.DrawString(DateTime.Parse(dgvView.Rows[i].Cells[4].Value.ToString()).ToShortDateString(), fnt1, Brushes.Black, unite_largeur * 14, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.White, unite_largeur * 17, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 22, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 25, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[8].Value.ToString(), fnt1, Brushes.Black, unite_largeur *27, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[11].Value.ToString(), fnt1, Brushes.White, unite_largeur * 30, YLOC);
                j++;
            }

            if (flag)
            {
                //var LOC = unite_hauteur * 14 + +unite_hauteur * p.Count;
                //graphic.FillRectangle(Brushes.White, unite_largeur, 31 * unite_hauteur - 3, unite_largeur * 35, 3 * unite_hauteur);
                //graphic.FillRectangle(Brushes.SlateGray, unite_largeur, LOC, unite_largeur * 35, unite_hauteur);
                //graphic.DrawString("TOTAL", fnt11, Brushes.White, unite_largeur + 2, LOC);
           }
            return bitmap;
        }

    
        public static Bitmap RecuDePaiement( Encaissement  reglement)
        {
            try
            {
                
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 10;
                int detail_hauteur_facture = 14 * unite_hauteur;
                int hauteur_facture = 43 * unite_hauteur + detail_hauteur_facture;
                var hauteurFacture2 = 29 * unite_hauteur;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1

              
                //definir les polices 
                Font fnt1 = new Font("Arial Narrow", 12F, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt0 = new Font("Arial Narrow", 13, FontStyle.Bold);
                Font fnt2 = new Font("Bodoni MT", 22, FontStyle.Bold);
                Font fnt3 = new Font("Californian FB", 16, FontStyle.Bold);
                Font fnt4 = new Font("Bradley Hand ITC", 17, FontStyle.Bold);
                              
                //graphic.DrawString("MED PHARMA", fnt2, Brushes.White, 15 * unite_largeur + 10, 2 * unite_hauteur);
                #region FACTURE1
                graphic.DrawRectangle(Pens.Black, 10, 0, 24 * unite_largeur, 27 * unite_hauteur - 10);
                graphic.DrawString("REÇU OFFICIEL", fnt3, Brushes.Black, 10, unite_hauteur);
                graphic.DrawLine(Pens.Black, 10, 2 * unite_hauteur + 10, 16 * unite_largeur - 20, 2 * unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur-15, 10, 8 * unite_largeur+15, 8* unite_hauteur -10);

                graphic.DrawString("DIOCESE DE DOBA", fnt1, Brushes.Black, 16 * unite_largeur , 1 * unite_hauteur + 10);
                graphic.DrawString("BELACD CARITAS", fnt1, Brushes.Black, 16 * unite_largeur, 2 * unite_hauteur+10);
                graphic.DrawString("COORDINATION DE SANTE", fnt1, Brushes.Black, 16 * unite_largeur, 3 * unite_hauteur +10);
                graphic.DrawString("HOPITAL ST JOSEPH DE BEBEDJIA", fnt1, Brushes.Black, 16 * unite_largeur,4 * unite_hauteur+10);
                graphic.DrawString("B.P : 22 DOBA TCHAD", fnt1, Brushes.Black, 16 * unite_largeur, 5 * unite_hauteur +10);

                var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                
                    graphic.DrawString(reglement.Tiers.ToUpper(), fnt0, Brushes.Black, 2 * unite_largeur, 5 * unite_hauteur -4);

                graphic.DrawString("Objet: " + reglement.Objet, fnt11, Brushes.Black, unite_largeur, 9 * unite_hauteur - 5);

                graphic.DrawLine(Pens.Black, 10, 7 * unite_hauteur - 3, 16 * unite_largeur - 20, 7 * unite_hauteur - 3);
                graphic.DrawLine(Pens.Black, 10, 8 * unite_hauteur, 16 * unite_largeur - 20, 8 * unite_hauteur);

                graphic.DrawString(reglement.Code, fnt0, Brushes.Black, 7*unite_largeur, 7 * unite_hauteur -2);
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, 11 * unite_hauteur+5, 5 * unite_largeur, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, 11 * unite_hauteur+5, 5 * unite_largeur, unite_hauteur);

                graphic.DrawString("Désignation", fnt11, Brushes.Black, 8* unite_largeur, 11 * unite_hauteur +6);
                graphic.DrawString("Frais perçu ", fnt11, Brushes.Black, 13* unite_largeur+10, 11 * unite_hauteur+6);

                var liste = from lc in ConnectionClass.ListeEncaissement(reglement.Exercice)
                            where lc.DateEncaissment == reglement.DateEncaissment
                            where lc.Tiers == reglement.Tiers
                            select lc;
                var j = 0;
                var total = .0;
                foreach(var l in liste)
                {
                    var YLOC = 12 * unite_hauteur+5 + j * unite_hauteur;

                    graphic.DrawRectangle(Pens.Black, 7*unite_largeur, YLOC, 5 * unite_largeur,  unite_hauteur );
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC, 5 * unite_largeur,  unite_hauteur );
                    total += l.Montant;
                    graphic.DrawString(l.Date.ToShortDateString(), fnt1, Brushes.Black, 8 * unite_largeur, YLOC);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Montant), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC);
                    if (l.Avoir > 0)
                    {
                        total += l.Avoir;
                        if (liste.Count() == 1)
                        {
                            graphic.DrawRectangle(Pens.Black,7 * unite_largeur, YLOC + unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC + unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawString("Avoir", fnt1, Brushes.Black, 8 * unite_largeur, YLOC + unite_hauteur);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Avoir), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC + unite_hauteur);
                        }
                        else if (liste.Count() > 1)
                        {
                            graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, YLOC + 2*unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC + 2*unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawString("Avoir", fnt1, Brushes.Black, 8 * unite_largeur, YLOC + 2 * unite_hauteur);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Avoir), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC + 2 * unite_hauteur);
                        }
                    }
                    j++;
                }

                graphic.DrawString("La somme de : " + Converti((int)total ), fnt1, Brushes.Black, 2 * unite_largeur, 4 * unite_hauteur - 5);

                var LOC = 14 * unite_hauteur + 5 + j * unite_hauteur;
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, LOC - 1* unite_hauteur, 10 * unite_largeur,2* unite_hauteur);
                graphic.DrawString("Total", fnt11, Brushes.Black, 8 * unite_largeur, LOC);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 13 * unite_largeur + 10, LOC);

                //graphic.DrawLine(Pens.Black, unite_largeur, 25 * unite_hauteur + 20, 23 * unite_largeur + 10, 25 * unite_hauteur + 20);
                //graphic.DrawString(paiement + " à la somme de " + Converti((int)reglement.MontantPaiement), fnt1, Brushes.Black, unite_largeur, 21 * unite_hauteur-5);
                graphic.DrawString("Bébédjia le : " + reglement.DateEncaissment.ToShortDateString(), fnt1, Brushes.Black, 11 * unite_largeur - 10, 19 * unite_hauteur);
                graphic.DrawString("Reçu pour paiement conforme", fnt1, Brushes.Black, 10* unite_largeur, 25 * unite_hauteur - 5);
                graphic.DrawString("Signature ", fnt1, Brushes.Black, 2 * unite_largeur, 21 * unite_hauteur -5);
                graphic.DrawString("Caissier  " +reglement.Caissier, fnt1, Brushes.Black, 19 * unite_largeur, 21* unite_hauteur -5);
                
                #endregion
                graphic.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - "
                    ,fnt1,Brushes.Black, 10, 28 * unite_hauteur-15);
                #region FACTURE1
                graphic.DrawRectangle(Pens.Black, 10, hauteurFacture2, 24 * unite_largeur, 27 * unite_hauteur -10);
                graphic.DrawString("REÇU OFFICIEL", fnt3, Brushes.Black, 10, unite_hauteur + hauteurFacture2);
                graphic.DrawLine(Pens.Black, 10, 2 * unite_hauteur + 10 + hauteurFacture2, 16 * unite_largeur - 20, 2 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur - 15, 10 + hauteurFacture2, 8 * unite_largeur + 15 , 8 * unite_hauteur - 10);

                graphic.DrawString("DIOCESE DE DOBA", fnt1, Brushes.Black, 16 * unite_largeur, 1 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawString("BELACD CARITAS", fnt1, Brushes.Black, 16 * unite_largeur, 2 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawString("COORDINATION DE SANTE", fnt1, Brushes.Black, 16 * unite_largeur, 3 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawString("HOPITAL ST JOSEPH DE BEBEDJIA", fnt1, Brushes.Black, 16 * unite_largeur, 4 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawString("B.P : 22 DOBA TCHAD", fnt1, Brushes.Black, 16 * unite_largeur, 5 * unite_hauteur + 10 + hauteurFacture2);
                
                graphic.DrawString("La somme de : " + Converti((int)total), fnt1, Brushes.Black, 2 * unite_largeur, 4 * unite_hauteur - 5 + hauteurFacture2);
                graphic.DrawString(reglement.Tiers.ToUpper(), fnt0, Brushes.Black, 2 * unite_largeur, 5 * unite_hauteur - 4 + hauteurFacture2);

                graphic.DrawString("Objet: " + reglement.Objet, fnt11, Brushes.Black, unite_largeur, 9 * unite_hauteur - 5 + hauteurFacture2);

                graphic.DrawLine(Pens.Black, 10, 7 * unite_hauteur - 3 + hauteurFacture2, 16 * unite_largeur - 20, 7 * unite_hauteur - 3 + hauteurFacture2);
                graphic.DrawLine(Pens.Black, 10, 8 * unite_hauteur + hauteurFacture2, 16 * unite_largeur - 20, 8 * unite_hauteur + hauteurFacture2);

                graphic.DrawString(reglement.Code, fnt0, Brushes.Black, 7 * unite_largeur, 7 * unite_hauteur - 2 + hauteurFacture2);
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, 11 * unite_hauteur + 5 + hauteurFacture2, 5 * unite_largeur, unite_hauteur );
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, 11 * unite_hauteur + 5 + hauteurFacture2, 5 * unite_largeur, unite_hauteur );

                graphic.DrawString("Désignation", fnt11, Brushes.Black, 8 * unite_largeur, 11 * unite_hauteur + 6 + hauteurFacture2);
                graphic.DrawString("Frais perçu ", fnt11, Brushes.Black, 13 * unite_largeur + 10, 11 * unite_hauteur + 6 + hauteurFacture2);
                j = 0;
                foreach (var l in liste)
                {
                    var YLOC = 12 * unite_hauteur + 5 + j * unite_hauteur + hauteurFacture2;

                    graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, YLOC, 5 * unite_largeur, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC, 5 * unite_largeur, unite_hauteur);

                    graphic.DrawString(l.Date.ToShortDateString(), fnt1, Brushes.Black, 8 * unite_largeur, YLOC);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Montant), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC);
                    if (l.Avoir > 0)
                    {
                        if (liste.Count() == 1)
                        {
                            graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, YLOC + unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC + unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawString("Avoir", fnt1, Brushes.Black, 8 * unite_largeur, YLOC + unite_hauteur);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Avoir), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC + unite_hauteur);
                        }
                        else if (liste.Count() > 1)
                        {
                            graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, YLOC + 2 * unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC + 2 * unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawString("Avoir", fnt1, Brushes.Black, 8 * unite_largeur, YLOC + 2 * unite_hauteur);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Avoir), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC + 2 * unite_hauteur);
                        }
                    }
                    j++;
                }

                 LOC = 14 * unite_hauteur + 5 + j * unite_hauteur + hauteurFacture2;
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, LOC - 1 * unite_hauteur, 10 * unite_largeur, 2 * unite_hauteur);
                graphic.DrawString("Total", fnt11, Brushes.Black, 8 * unite_largeur, LOC);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 13 * unite_largeur + 10, LOC);

                //graphic.DrawLine(Pens.Black, unite_largeur, 25 * unite_hauteur + 20, 23 * unite_largeur + 10, 25 * unite_hauteur + 20);
                //graphic.DrawString(paiement + " à la somme de " + Converti((int)reglement.MontantPaiement), fnt1, Brushes.Black, unite_largeur, 21 * unite_hauteur-5);
                graphic.DrawString("Bébédjia le : " + reglement.DateEncaissment.ToShortDateString(), fnt1, Brushes.Black, 11 * unite_largeur - 10, 19 * unite_hauteur + hauteurFacture2);
                graphic.DrawString("Reçu pour paiement conforme", fnt1, Brushes.Black, 10 * unite_largeur, 25 * unite_hauteur - 5 + hauteurFacture2);
                graphic.DrawString("Signature ", fnt1, Brushes.Black, 2 * unite_largeur, 21 * unite_hauteur - 5 + hauteurFacture2);
                graphic.DrawString("Caissier  " + reglement.Caissier, fnt1, Brushes.Black, 19 * unite_largeur, 21 * unite_hauteur - 5 + hauteurFacture2);

                #endregion

                return bitmap;
                #endregion

            }
            catch(Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
                return null;
            }
        }

        //imprimer une facture
        public static Bitmap ImprimerUneFature(Document facture, DataGridView dgv )
        {
            try
            {

                //les dimension de la facture
                int unite_hauteur = 23;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 5;
                int detail_hauteur_facture = 20 * unite_hauteur;
                int hauteur_facture = 54 * unite_hauteur+10;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1

                Image imageLogo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(imageLogo,  1*unite_largeur, 32, 7 * unite_largeur, 4 * unite_hauteur -17);

                //definir les polices 
                Font fnt12 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
                Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 10.5f, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Narrow", 16, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold);
                Font fnt4 = new Font("Bradley Hand ITC", 17, FontStyle.Bold);

               graphic.DrawString("Esprit metier", fnt4, Brushes.Black, unite_largeur * 18, unite_hauteur+10);
                graphic.DrawString("N'Djaména-TCHAD", fnt11, Brushes.Black,  1*unite_largeur , 6 * unite_hauteur);
                graphic.DrawString("Tél. (+235) 66 36 17 04 / 99 82 77 55 ", fnt11, Brushes.Black,  1*unite_largeur , 7 * unite_hauteur-2);
                graphic.DrawString("Email : honographic@gmail.com ", fnt11, Brushes.Black,  1*unite_largeur , 8 * unite_hauteur-5);
                graphic.DrawString("Avenue Mobutu/Radio Arc-en ciel : ", fnt11, Brushes.Black,  1*unite_largeur , 9 * unite_hauteur-8);

                if (facture.TypeDocument.ToUpper().Contains("PROFORMA"))
                {
                    graphic.DrawString("Date de facture proforma : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 9 * unite_hauteur);
                }
                else if (facture.TypeDocument.ToUpper().Contains("TRAVAIL"))
                {
                    graphic.DrawString("Date de bon de travail : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 9 * unite_hauteur);
                }
                else if (facture.TypeDocument.ToUpper().Contains("COMMANDE"))
                {
                    graphic.DrawString("Date de commande : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 9 * unite_hauteur);
                }
                else if (facture.TypeDocument.ToUpper().Contains("ACHAT"))
                {
                    graphic.DrawString("Date de bon d'achat : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 9 * unite_hauteur);
                }
                else 
                {
                    graphic.DrawString("Date de facture : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 9 * unite_hauteur);
                } 
                imageLogo = global::SGSP.Properties.Resources.roundedRectangle;
                graphic.DrawImage(imageLogo,  1*unite_largeur ,11*unite_hauteur, 8 * unite_largeur,  3 * unite_hauteur+15);
                graphic.DrawImage(imageLogo,10* unite_largeur, 11 * unite_hauteur, 13 * unite_largeur, 3 * unite_hauteur+15);
                var idF="";
                if (facture.IDTypeDocument < 10)
                {
                    idF = "0000" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 10 && facture.IDTypeDocument < 100)
                {
                    idF = "000" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 100 && facture.IDTypeDocument < 1000)
                {
                    idF = "00" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 1000 && facture.IDTypeDocument < 10000)
                {
                    idF = "0" + facture.IDTypeDocument;
                }

                graphic.DrawString(facture.TypeDocument, fnt1, Brushes.Black, 2 * unite_largeur, 11 * unite_hauteur + 10);
                graphic.DrawString("N° : " + idF, fnt1, Brushes.Black, 2*unite_largeur, 12 * unite_hauteur+10);
                graphic.DrawString("Référence : " + facture.ReferenceDocument, fnt1, Brushes.Black, 2*unite_largeur, 13 * unite_hauteur+10);

                graphic.DrawString("Destinataire ", fnt1, Brushes.Black, 15* unite_largeur, 11 * unite_hauteur + 10);
                graphic.DrawString(facture.RootPathDocument.ToUpper(), fnt2, Brushes.Black, 12 * unite_largeur-15, 13 * unite_hauteur );
                graphic.FillRectangle(Brushes.DarkGray, 1* unite_largeur , 15 * unite_hauteur + 5, 22 * unite_largeur ,  unite_hauteur + 5);
                graphic.DrawString(facture.TypeDocument, fnt3, Brushes.Black, 10 * unite_largeur, 15 * unite_hauteur + 7);

                imageLogo = global::SGSP.Properties.Resources.detailFactures;
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 22 * unite_largeur, unite_hauteur+10);
                //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 16 * unite_hauteur, 22 * unite_largeur+20, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 1 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 14 * unite_largeur-4, 17 * unite_hauteur, 2 * unite_largeur+16, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur+12, 17 * unite_hauteur, 2 * unite_largeur + 16, detail_hauteur_facture);
                //graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 4, 17 * unite_hauteur, 3 * unite_largeur + 16, detail_hauteur_facture);
               
                graphic.DrawString("N° ", fnt0, Brushes.Black, 1 * unite_largeur+8, 17 * unite_hauteur + 5);
                graphic.DrawString("Désignation ", fnt0, Brushes.Black, 6 * unite_largeur + 8, 17 * unite_hauteur + 5);
                graphic.DrawString("Quantité", fnt0, Brushes.Black, 14 * unite_largeur+10, 17 * unite_hauteur + 5);
                graphic.DrawString("P.U ", fnt0, Brushes.Black, 17 * unite_largeur , 17 * unite_hauteur + 5);
                graphic.DrawString("P.T ", fnt0, Brushes.Black, 20 * unite_largeur , 17 * unite_hauteur + 5);
                
                int j = 0;
                for (var i = 0; i < dgv.Rows.Count; i++)
                {
                    var YLOC = 19 * unite_hauteur + unite_hauteur * j;

                    double prixAchat, qte, prixTotal;
                    if (double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out qte)
                        && double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out prixAchat)
                            &&
                            double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out prixTotal))
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", qte), fnt12, Brushes.Black, 14 * unite_largeur+10, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt12, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", prixAchat), fnt12, Brushes.Black, 17 * unite_largeur - 8, YLOC);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", prixTotal), fnt12, Brushes.Black, 19 * unite_largeur + 10, YLOC); graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 1 * unite_largeur + 8, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt12, Brushes.Black, 1 * unite_largeur + 8, YLOC);
    
                    }
                   else
                    {   
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString() , fnt1, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                    }
                    j++;
                }

                graphic.FillRectangle(Brushes.White, 1* unite_largeur, 37 * unite_hauteur+5, 24 * unite_largeur, 18 * unite_hauteur + 5);
                if (dgv.Rows.Count <= 18 )
                {
                   
                    graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, detail_hauteur_facture  +17* unite_hauteur, 22 * unite_largeur, 3 * unite_hauteur + 5);

                   graphic.DrawString("Modalité paiement : "+facture.ModalitePaiement, fnt1, Brushes.Black, 2 * unite_largeur, 37 * unite_hauteur + 15);
                    graphic.DrawString("Délai de livraison : "+facture.EcheanceLivraison.ToShortDateString(), fnt1, Brushes.Black,2 * unite_largeur, 38 * unite_hauteur + 15);
                    var tva = .0;
                    if (facture.MontantTTC != facture.MontantHT)
                    {
                         tva = 100 * (facture.MontantTTC - facture.MontantHT) / facture.MontantHT;
                    }
                    graphic.DrawString("Total HT", fnt0, Brushes.Black, 16 * unite_largeur + 10, 37 * unite_hauteur + 5);
                    graphic.DrawString("TVA (" + (int)tva + "%)", fnt0, Brushes.Black, 16 * unite_largeur + 10, 38 * unite_hauteur + 5);
                    graphic.DrawString("Total TTC", fnt0, Brushes.Black, 16 * unite_largeur + 10, 39 * unite_hauteur + 5);

                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.MontantHT), fnt0, Brushes.Black, 19 * unite_largeur + 10, 37 * unite_hauteur + 5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.TVA), fnt0, Brushes.Black, 19 * unite_largeur + 10, 38 * unite_hauteur + 5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.MontantTTC), fnt0, Brushes.Black, 19* unite_largeur + 10, 39 * unite_hauteur + 5);
                    string arrete = "";
                    if (facture.TypeDocument.ToUpper().Contains("FACTURE"))
                    {
                        arrete = "Arrêtée la présente " + facture.TypeDocument.ToLower() + " à la somme";
                    }
                    else
                    {

                    }
                    graphic.DrawString(arrete + " : " + Converti((int)facture.MontantTTC).ToLower() + " Franc CFA", fnt1, Brushes.Black, unite_largeur, 41 * unite_hauteur + 10);
                    graphic.DrawString("La Direction", fnt1, Brushes.Black, 18 * unite_largeur, 43 * unite_hauteur );
                }

                if (dgv.Rows.Count > 18)
                {
                    var page = 1;
                    graphic.DrawString("Page " + page, fnt11, Brushes.Black, 22 * unite_largeur-5, 53 * unite_hauteur-5);
                }
                imageLogo = global::SGSP.Properties.Resources.bas_2;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 51*unite_hauteur+5, 22 * unite_largeur,  unite_hauteur + 5);

                return bitmap;
               
                #endregion

            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); return null; }
        }

        public static Bitmap ImprimerUneFaturePage(Document facture, DataGridView dgv, int index)
        {
            try
            {

                //les dimension de la facture
                int unite_hauteur = 23;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 5;
                int detail_hauteur_facture = 39 * unite_hauteur;
                int hauteur_facture = 54 * unite_hauteur+10;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1
                
                //definir les polices 
                Font fnt12 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
                Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 12, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Narrow", 16, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold);
                Font fnt110 = new Font("Arial Narrow", 10.5f, FontStyle.Regular);
                var page = 2 + index;

                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, unite_hauteur + 10);
                
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 1 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 14 * unite_largeur - 4, 2 * unite_hauteur, 2 * unite_largeur + 16, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 12, 2 * unite_hauteur, 2 * unite_largeur + 16, detail_hauteur_facture);

                graphic.DrawString("N° ", fnt0, Brushes.Black, 1 * unite_largeur + 8, 2 * unite_hauteur + 5);
                graphic.DrawString("Désignation ", fnt0, Brushes.Black, 6 * unite_largeur + 8, 2 * unite_hauteur + 5);
                graphic.DrawString("Quantité", fnt0, Brushes.Black, 14 * unite_largeur + 10, 2 * unite_hauteur + 5);
                graphic.DrawString("P.U ", fnt0, Brushes.Black, 17 * unite_largeur, 2 * unite_hauteur + 5);
                graphic.DrawString("P.T ", fnt0, Brushes.Black, 20 * unite_largeur, 2 * unite_hauteur + 5);

                int j = 0;
                for (var i = 18 + index * 37; i < dgv.Rows.Count; i++)
                {
                    var YLOC = 4 * unite_hauteur + unite_hauteur * j;

                    double prixAchat, qte, prixTotal;
                    if (double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out qte)
                        && double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out prixAchat)
                            &&
                            double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out prixTotal))
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", qte), fnt12, Brushes.Black, 14 * unite_largeur + 10, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt12, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", prixAchat), fnt12, Brushes.Black, 17 * unite_largeur - 8, YLOC);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", prixTotal), fnt12, Brushes.Black, 19 * unite_largeur + 10, YLOC); graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 1 * unite_largeur + 8, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt12, Brushes.Black, 1 * unite_largeur + 8, YLOC);

                    }
                    else
                    {
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString() , fnt1, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                    }
                    graphic.DrawString("Page " + page, fnt110, Brushes.Black, 22 * unite_largeur, 53 * unite_hauteur);
                    j++;
                }
                graphic.FillRectangle(Brushes.White, 1 * unite_largeur, 41 * unite_hauteur + 5, 24 * unite_largeur, 20 * unite_hauteur + 22);
                if (dgv.Rows.Count <= 18 + 37 * (1 + index))
                {

                    graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 37 * unite_hauteur + 5, 22 * unite_largeur, 4 * unite_hauteur-5 );

                    graphic.DrawString("Modalité paiement : " + facture.ModalitePaiement, fnt1, Brushes.Black, 2 * unite_largeur, 37 * unite_hauteur + 15);
                    graphic.DrawString("Délai de livraison : " + facture.EcheanceLivraison.ToShortDateString(), fnt1, Brushes.Black, 2 * unite_largeur, 38 * unite_hauteur + 15);

                    var tva = 100 * (facture.MontantTTC - facture.MontantHT) / facture.MontantHT;
                    graphic.DrawString("Total HT", fnt0, Brushes.Black, 16 * unite_largeur + 10, 37 * unite_hauteur + 5);
                    graphic.DrawString("TVA (" + (int)tva + "%)", fnt0, Brushes.Black, 16 * unite_largeur + 10, 38 * unite_hauteur + 5);
                    graphic.DrawString("Total TTC", fnt0, Brushes.Black, 16 * unite_largeur + 10, 39 * unite_hauteur + 5);

                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.MontantHT), fnt0, Brushes.Black, 19 * unite_largeur + 10, 37 * unite_hauteur + 5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.TVA), fnt0, Brushes.Black, 19 * unite_largeur + 10, 38 * unite_hauteur + 5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.MontantTTC), fnt0, Brushes.Black, 19 * unite_largeur + 10, 39 * unite_hauteur + 5);
                    string arrete = "";
                    if (facture.TypeDocument.ToUpper().Contains("FACTURE"))
                    {
                        arrete = "Arrêtée la présente " + facture.TypeDocument.ToLower() + " à la somme";
                    }
                    else
                    {

                    }
                    graphic.DrawString(arrete + " : " + Converti((int)facture.MontantTTC).ToLower() + " Franc CFA", fnt1, Brushes.Black, unite_largeur, 41 * unite_hauteur + 10);
                    graphic.DrawString("La Direction", fnt1, Brushes.Black, 18 * unite_largeur, 43 * unite_hauteur);
                }

                graphic.DrawString("Page " + page, fnt110, Brushes.Black, 22 * unite_largeur-5, 53 * unite_hauteur-5);
                var imageLogo = global::SGSP.Properties.Resources.bas_2;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 51 * unite_hauteur + 5, 22 * unite_largeur, unite_hauteur + 5);

                return bitmap;

                #endregion

            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); return null; }
        }

        public static Bitmap ImprimerUnBonLivraison(Document facture, DataGridView dgv)
        {
            try
            {

                //les dimension de la facture
                int unite_hauteur = 23;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 5;
                int detail_hauteur_facture = 27 * unite_hauteur;
                int hauteur_facture = 54 * unite_hauteur + 10;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1

                Image imageLogo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 32, 7 * unite_largeur, 4 * unite_hauteur - 17);

                //definir les polices 
                Font fnt12 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
                Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 10.5f, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Narrow", 16, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold);
                Font fnt4 = new Font("Bradley Hand ITC", 17, FontStyle.Bold);

                graphic.DrawString("Esprit metier", fnt4, Brushes.Black, unite_largeur * 18, unite_hauteur + 10);
                graphic.DrawString("N'Djaména-TCHAD", fnt11, Brushes.Black, 1 * unite_largeur, 6 * unite_hauteur);
                graphic.DrawString("Tél. (+235) 66 36 17 04 / 99 82 77 55 ", fnt11, Brushes.Black, 1 * unite_largeur, 7 * unite_hauteur - 2);
                graphic.DrawString("Email : honographic@gmail.com ", fnt11, Brushes.Black, 1 * unite_largeur, 8 * unite_hauteur - 5);
                graphic.DrawString("Avenue Mobutu/Radio Arc-en ciel : ", fnt11, Brushes.Black, 1 * unite_largeur, 9 * unite_hauteur - 8);

                graphic.DrawString("Date de livraison : " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 9 * unite_hauteur);

                imageLogo = global::SGSP.Properties.Resources.roundedRectangle;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 11 * unite_hauteur, 8 * unite_largeur, 3 * unite_hauteur + 15);
                graphic.DrawImage(imageLogo, 10 * unite_largeur, 11 * unite_hauteur, 13 * unite_largeur, 3 * unite_hauteur + 15);
                var idF = "";
                if (facture.IDTypeDocument < 10)
                {
                    idF = "0000" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 10 && facture.IDTypeDocument < 100)
                {
                    idF = "000" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 100 && facture.IDTypeDocument < 1000)
                {
                    idF = "00" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 1000 && facture.IDTypeDocument < 10000)
                {
                    idF = "0" + facture.IDTypeDocument;
                }

                graphic.DrawString("BON DE LIVRAISON", fnt1, Brushes.Black, 2 * unite_largeur, 11 * unite_hauteur + 10);
                graphic.DrawString("N° : " + idF, fnt1, Brushes.Black, 2 * unite_largeur, 12 * unite_hauteur + 10);
                graphic.DrawString("Référence : " + facture.ReferenceDocument, fnt1, Brushes.Black, 2 * unite_largeur, 13 * unite_hauteur + 10);

                graphic.DrawString("Destinataire ", fnt1, Brushes.Black, 15 * unite_largeur, 11 * unite_hauteur + 10);
                graphic.DrawString(facture.RootPathDocument.ToUpper(), fnt2, Brushes.Black, 12 * unite_largeur - 15, 13 * unite_hauteur);
                graphic.FillRectangle(Brushes.DarkGray, 1 * unite_largeur, 15 * unite_hauteur + 5, 22 * unite_largeur, unite_hauteur + 5);
                graphic.DrawString("BON DE LIVRAISON", fnt3, Brushes.Black, 10 * unite_largeur, 15 * unite_hauteur + 7);

                imageLogo = global::SGSP.Properties.Resources.detailFactures;
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 22 * unite_largeur, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 18 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 1 * unite_largeur, detail_hauteur_facture);
   
                graphic.DrawString("N° ", fnt0, Brushes.Black, 1 * unite_largeur + 8, 17 * unite_hauteur + 5);
                graphic.DrawString("Désignation ", fnt0, Brushes.Black, 9 * unite_largeur + 8, 17 * unite_hauteur + 5);
                graphic.DrawString("Quantité", fnt0, Brushes.Black, 20 * unite_largeur, 17 * unite_hauteur + 5);

                int j = 0;
                for (var i = 0; i < dgv.Rows.Count; i++)
                {
                    var YLOC = 19 * unite_hauteur + unite_hauteur * j;
                    //graphic.DrawString(String.Format(elGR, "{0:0,0}", i), fnt12, Brushes.Black, 19 * unite_largeur + 10, YLOC);
                       
                    double prixAchat, qte, prixTotal;
                    if (double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out qte)
                        && double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out prixAchat)
                            &&
                            double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out prixTotal))
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", qte), fnt12, Brushes.Black, 20 * unite_largeur + 10, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString() + " " , fnt12, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                       graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt12, Brushes.Black, 1 * unite_largeur + 8, YLOC);

                    }
                    else
                    {
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                    }
                    j++;
                }

                graphic.FillRectangle(Brushes.White, 1 * unite_largeur, 44 * unite_hauteur + 5, 24 * unite_largeur, 18 * unite_hauteur + 5);
                if (dgv.Rows.Count <= 25)
                {
                    graphic.DrawString("Le Fournisseur", fnt1, Brushes.Black, 18 * unite_largeur, 46 * unite_hauteur);
                   graphic.DrawString("Le Receptionniste", fnt1, Brushes.Black, 2 * unite_largeur, 46 * unite_hauteur);
                        var page = 1;
                    graphic.DrawString("Page " + page, fnt11, Brushes.Black, 22 * unite_largeur - 5, 53 * unite_hauteur - 5);
                }
                imageLogo = global::SGSP.Properties.Resources.bas_2;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 51 * unite_hauteur + 5, 22 * unite_largeur, unite_hauteur + 5);

                return bitmap;

                #endregion

            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); return null; }
        }

        public static Bitmap ImprimerUnBonLivraison(Document facture, DataGridView dgv, int index)
        {
            try
            {

                //les dimension de la facture
                int unite_hauteur = 23;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 5;
                int detail_hauteur_facture = 42 * unite_hauteur;
                int hauteur_facture = 54 * unite_hauteur + 10;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1

                //definir les polices 
                Font fnt12 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
                Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 12, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Narrow", 16, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold);
                Font fnt110 = new Font("Arial Narrow", 10.5f, FontStyle.Regular);
                var page = 2 + index;

                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, unite_hauteur + 10);

                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);

                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 18 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 1 * unite_largeur, detail_hauteur_facture);
   
                graphic.DrawString("N° ", fnt0, Brushes.Black, 1 * unite_largeur + 8, 2 * unite_hauteur + 5);
                graphic.DrawString("Désignation ", fnt0, Brushes.Black, 9 * unite_largeur + 8, 2 * unite_hauteur + 5);
                graphic.DrawString("Quantité", fnt0, Brushes.Black, 20 * unite_largeur + 10, 2 * unite_hauteur + 5);

                int j = 0;
                for (var i = 25 + index * 40; i < dgv.Rows.Count; i++)
                {
                    var YLOC = 4 * unite_hauteur + unite_hauteur * j;

                    double prixAchat, qte, prixTotal;
                    if (double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out qte)
                        && double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out prixAchat)
                            &&
                            double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out prixTotal))
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", qte), fnt12, Brushes.Black, 20 * unite_largeur + 10, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt12, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt12, Brushes.Black, 1 * unite_largeur + 8, YLOC);

                    }
                    else
                    {
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString() , fnt1, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                    }
                    graphic.DrawString("Page " + page, fnt110, Brushes.Black, 22 * unite_largeur, 53 * unite_hauteur);
                    j++;
                }
                graphic.FillRectangle(Brushes.White, 1 * unite_largeur, 44 * unite_hauteur+5 , 24 * unite_largeur, 20 * unite_hauteur + 5);
                if (dgv.Rows.Count <= 25 + 40 * (1 + index))
                {
                    graphic.DrawString("Le Fournisseur", fnt1, Brushes.Black, 18 * unite_largeur, 45 * unite_hauteur);
                    graphic.DrawString("Le Receptionniste", fnt1, Brushes.Black, 2 * unite_largeur, 45 * unite_hauteur);
                }

                graphic.DrawString("Page " + page, fnt110, Brushes.Black, 22 * unite_largeur - 5, 53 * unite_hauteur - 5);
                var imageLogo = global::SGSP.Properties.Resources.bas_2;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 51 * unite_hauteur + 5, 22 * unite_largeur, unite_hauteur + 5);

                return bitmap;

                #endregion

            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); return null; }
        }

        public static Bitmap ImprimerEtatFinanceSemestriel(DataGridView dgvPaiement, string titre, int index)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 45 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 4 * unite_hauteur - 15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri",9, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt00 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 2, 5 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur + 4, unite_largeur * 35 - 18, 2 * unite_hauteur - 4);

            graphic.DrawString(dgvPaiement.Columns[1].HeaderText, fnt0, Brushes.Black, 20, 6 * unite_hauteur + 6);
            for (var i = 2; i < dgvPaiement.Columns.Count - 1; i++)
            {
                var XLOC = 12 * unite_largeur + i * (2 * unite_largeur);
                var header = dgvPaiement.Columns[i].HeaderText;
                if (header.Length > 10)
                {
                    header = header.Substring(0, 10) + "\n" + header.Substring(10);
                }
                graphic.DrawString(header, fnt0, Brushes.Black, XLOC, 6 * unite_hauteur + 6);
            }


            var j = 0;

            for (var i = index *35; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 8 + 3 + unite_hauteur * j;
                graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur * 35 - 22, unite_hauteur);
                if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                    !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur * 35 - 24, unite_hauteur - 1);
                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 3);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 12 * unite_largeur + y * (2 * unite_largeur);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 3);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 3);
                        }
                    }
                }
                else
                {

                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC + 3);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 12 * unite_largeur + y * (2 * unite_largeur);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 3);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC + 3);
                        }
                    }
                    #endregion
                }
                j++;
            }
                graphic.FillRectangle(Brushes.White, 0, 43 * unite_hauteur + 4, unite_largeur * 35, unite_hauteur * 6);
                if (dgvPaiement.Rows.Count <= 35*(1+index))
                {
                 var height = (13 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le Directeur ", fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 2 * unite_hauteur + 10);
                }
            }
                return bitmap;
            }

        public static Bitmap ImprimerEtatFinanceTrimestre(DataGridView dgvPaiement, string titre, int index)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 45 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 4 * unite_hauteur - 15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 10, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt00 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            var page = index + 1;
            graphic.DrawString("Page " + page, fnt1, Brushes.Black, 22 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 2, 5 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur + 4, unite_largeur * 35 - 18, 2 * unite_hauteur - 4);

            graphic.DrawString(dgvPaiement.Columns[1].HeaderText, fnt0, Brushes.Black, 20, 6 * unite_hauteur + 6);
            for (var i = 2; i < dgvPaiement.Columns.Count - 1; i++)
            {
                var XLOC = 12 * unite_largeur + i * (2 * unite_largeur+25);
                var header = dgvPaiement.Columns[i].HeaderText;
                if (header.Length > 10)
                {
                    ////header = header.Substring(0, 10) + "\n" + header.Substring(10);
                }
                graphic.DrawString(header, fnt0, Brushes.Black, XLOC, 6 * unite_hauteur + 6);
            }


            var j = 0;

            for (var i = index * 35; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 8 + 3 + unite_hauteur * j;
                //double totaux;
                //if (double.TryParse(dgvPaiement.Rows[i].Cells[5].Value.ToString(), out totaux))
                //{
                //    if (totaux < 0 || totaux > 0)
                //    {
                //if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[2].Value.ToString()) &&
                //   string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[3].Value.ToString()) &&
                //!string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                //{

                //}
                //else
                //{
                    graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur * 35 - 22, unite_hauteur);
                    if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                        !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                    {
                        graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur * 35 - 24, unite_hauteur - 1);
                        graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 3);

                        for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                        {
                            var XLOC = 12 * unite_largeur + y * (2 * unite_largeur + 25);
                            double montant;
                            if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                            {
                                graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 3);
                            }
                            else
                            {
                                graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 3);
                            }
                        }
                    }
                    else
                    {

                        graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC - 0);

                        for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                        {
                            var XLOC = 12 * unite_largeur + y * (2 * unite_largeur + 25);
                            double montant;
                            if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                            {
                                graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 0);
                            }
                            else
                            {
                                graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC + 0);
                            }
                        }
                        #endregion
                    }
                    j++;
                //}
                //}
                //                            else
                //                            {
                //                                graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur* 35 - 22, unite_hauteur);
                //                                if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                //                                    !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                //                                {
                //                                    graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur* 35 - 24, unite_hauteur - 1);
                //                                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 3);

                //                                    for (var y = 2; y<dgvPaiement.Columns.Count - 1; y++)
                //                                    {
                //                                        var XLOC = 12 * unite_largeur + y * (2 * unite_largeur + 25);
                //        double montant;
                //                                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                //                                        {
                //                                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 3);
                //                                        }
                //                                        else
                //                                        {
                //                                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 3);
                //                                        }
                //                                    }
                //                                }
                //                                else
                //                                {

                //                                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC + 0);

                //                                    for (var y = 2; y<dgvPaiement.Columns.Count - 1; y++)
                //                                    {
                //                                        var XLOC = 12 * unite_largeur + y * (2 * unite_largeur + 25);
                //double montant;
                //                                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                //                                        {
                //                                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 0);
                //                                        }
                //                                        else
                //                                        {
                //                                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC + 0);
                //                                        }
                //                                    }
                //            //#endregion
                //                                }
                //                                j++;

                //}
            }
            graphic.FillRectangle(Brushes.White, 0, 43 * unite_hauteur + 4, unite_largeur * 35, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= 35 * (1 + index))
            {
                var height = (13 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le Directeur ", fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 2 * unite_hauteur + 10);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerEtatFinanceAnnuel(DataGridView dgvPaiement, string titre, int index)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 45 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 4 * unite_hauteur - 15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 7.8f, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 7.8f, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion
            var page = 1 + index;
            graphic.DrawString("Page " + page, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 2, 5 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur + 4, unite_largeur * 35 - 18, 2 * unite_hauteur - 4);

            graphic.DrawString(dgvPaiement.Columns[1].HeaderText, fnt0, Brushes.Black, 20, 6 * unite_hauteur + 6);
            for (var i = 2; i < dgvPaiement.Columns.Count - 1; i++)
            {
                var XLOC = 6* unite_largeur + i * (1 * unite_largeur + 23);
                var header = dgvPaiement.Columns[i].HeaderText;
                if (header.Length > 8)
                {
                    header = header.Substring(0, 8) + "\n" + header.Substring(8);
                }
                graphic.DrawString(header, fnt0, Brushes.Black, XLOC, 6 * unite_hauteur + 6);
            }


            var j = 0;

            for (var i = index * 35; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 8 + 3 + unite_hauteur * j;
                graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur * 35 - 22, unite_hauteur);
                if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                    !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur * 35 - 24, unite_hauteur - 1);
                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 3);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 6 * unite_largeur + y * (1 *unite_largeur + 23);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 3);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 3);
                        }
                    }
                }
                else
                {

                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC + 3);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 6 * unite_largeur + y * (1 * unite_largeur + 23);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 3);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC + 3);
                        }
                    }
                    #endregion
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 0, 43 * unite_hauteur + 4, unite_largeur * 35, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= 35 * (1 + index))
            {
                var height = (13 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le Directeur ", fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 2 * unite_hauteur + 10);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerEtatFinanceMensuel(DataGridView dgvPaiement, string titre, int index)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int hauteur_facture = 62 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 3* unite_hauteur - 15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 10f, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 10f, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion
            var page = index + 1;
            graphic.DrawString("Page " + page, fnt1, Brushes.Black, 22 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 2, 4 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 5 * unite_hauteur + 4, unite_largeur * 24 - 18, 2 * unite_hauteur - 1);

            graphic.DrawString(dgvPaiement.Columns[1].HeaderText, fnt0, Brushes.Black, 20, 5 * unite_hauteur + 6);
            for (var i = 2; i < dgvPaiement.Columns.Count - 1; i++)
            {
                var XLOC = 11 * unite_largeur -15+ i * (2 * unite_largeur + 10);
                var header = dgvPaiement.Columns[i].HeaderText;
                if (header.Length > 9)
                {
                    header = header.Substring(0, 9) + "\n" + header.Substring(8);
                }
                graphic.DrawString(header, fnt0, Brushes.Black, XLOC, 5 * unite_hauteur + 3);
            }


            var j = 0;

            for (var i = index * 55; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 7 + 3 + unite_hauteur * j;
                graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur * 24 - 22, unite_hauteur);
                if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                    !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur * 23+7 , unite_hauteur - 1);
                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 1);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 11 * unite_largeur -15+ y * (2 * unite_largeur + 10);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 1);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 1);
                        }
                    }
                }
                else
                {

                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC + 1);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 11 * unite_largeur -15+ y * (2 * unite_largeur + 10);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 1);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC +1);
                        }
                    }
                    #endregion
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 0, 62 * unite_hauteur + 4, unite_largeur * 25, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= 55 * (1 + index))
            {
                var height = (13 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 10 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le Directeur ", fnt33, Brushes.Black, 10 * unite_largeur + 10, index2 + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 10 * unite_largeur + 10, index2 + 2 * unite_hauteur + 10);
                }
            }
            return bitmap;
        }


        public static Bitmap ImprimerBilanFinancier(DataGridView dataGridView, string titre, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 60 * unite_hauteur;

            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 4*unite_largeur, 10, 12 * unite_largeur, 3 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Bold);
            graphic.DrawString("Page " + (1 + start).ToString(), fnt1, Brushes.Black, 20 * unite_largeur, 5);

            graphic.DrawString(titre, fnt3, Brushes.Black, 5 * unite_largeur, 4 * unite_hauteur + 5);

            graphic.FillRectangle(Brushes.Lavender, 5 * unite_largeur, 6 * unite_hauteur - 4, 10 * unite_largeur-3, unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 15 * unite_largeur, 6 * unite_hauteur - 4, 5 * unite_largeur, unite_hauteur);
            graphic.DrawString("Désignation ", fnt33, Brushes.Black, 6 * unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Montant ", fnt33, Brushes.Black, 16 * unite_largeur + 10, 6 * unite_hauteur - 4);
            var j = 0;
            for (int i = 50 * start; i <= dataGridView.Rows.Count - 2; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur;
           
                if (string.IsNullOrWhiteSpace(dataGridView.Rows[i].Cells[0].Value.ToString()))
                {
                    if (dataGridView.Rows[i].Cells[3].Value.ToString() == "B-PASSIF" || dataGridView.Rows[i].Cells[3].Value.ToString() == "A-ACTIF")
                    {
                        graphic.FillRectangle(Brushes.Lavender, 5 * unite_largeur, Yloc, 15 * unite_largeur - 3, unite_hauteur);
                        graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 6 * unite_largeur, Yloc);
                        graphic.DrawString(dataGridView.Rows[i].Cells[4].Value.ToString(), fnt33, Brushes.Black, 16 * unite_largeur, Yloc);
                    }else if (dataGridView.Rows[i].Cells[3].Value.ToString() == "" &&  dataGridView.Rows[i].Cells[4].Value.ToString() == "")
                    {
                    }
                    else
                    {
                        graphic.DrawRectangle(Pens.Black, 5 * unite_largeur, Yloc, 10 * unite_largeur - 3, unite_hauteur);
                        graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, Yloc, 5 * unite_largeur - 3, unite_hauteur);
                        graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 6 * unite_largeur, Yloc);
                        graphic.DrawString(dataGridView.Rows[i].Cells[4].Value.ToString(), fnt33, Brushes.Black, 16 * unite_largeur, Yloc);
                    }
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 5 * unite_largeur, Yloc, 10 * unite_largeur - 3, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, Yloc, 5 * unite_largeur - 3, unite_hauteur);
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 6 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur, Yloc);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 5, 57 * unite_hauteur + 1, 24 * unite_largeur, 10 * unite_hauteur);
            if (dataGridView.Rows.Count <= 45 * (1 + start))
            {
                var height = (10 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 9 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le Directeur ", fnt33, Brushes.Black, 9 * unite_largeur + 10, index2 + unite_hauteur * 4);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 9 * unite_largeur + 10, index2 + 5 * unite_hauteur + 10);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerSoldePCP(List<Paiement> listeSolde,int start)
        {
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 56 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 11, FontStyle.Underline);
            // dessiner les ecritures 
                  var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
                graphic.DrawString("Solde Prise en charge de membres famille du personnel ", fnt11, Brushes.Black, unite_largeur, 5 * unite_hauteur + 3);
            
            graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 18 * unite_largeur, 4 * unite_hauteur + 5);

            graphic.FillRectangle(Brushes.Lavender, 17, 7 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 7 * unite_hauteur - 2, 8 * unite_largeur + 11, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 10, 7 * unite_hauteur - 2, 6 * unite_largeur - 4, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 16, 7 * unite_hauteur - 2, 3 * unite_largeur - 18, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 19 - 16, 7 * unite_hauteur - 2, 5 * unite_largeur + 10, unite_hauteur * 2);


            graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 7 * unite_hauteur + 10);
            graphic.DrawString("Nom du salarié ", fnt11, Brushes.Black, 4*unite_largeur + 0, 7 * unite_hauteur + 10);
            graphic.DrawString("Fonction ", fnt11, Brushes.Black, unite_largeur * 12 + 15, 7 * unite_hauteur + 10);
            graphic.DrawString("Montant ", fnt11, Brushes.Black, unite_largeur * 16 + 5, 7 * unite_hauteur + 10);
            graphic.DrawString("Observation ", fnt11, Brushes.Black, 19 * unite_largeur +5, 7 * unite_hauteur + 10);
            var total = 0.0;
            var j = 0;
            for (int i = start * 45; i < listeSolde.Count; i++)
            {
                var YLOC = 9 * unite_hauteur + j * unite_hauteur;

                graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, 19 + unite_largeur, YLOC, 8 * unite_largeur + 11, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 10, YLOC, 6 * unite_largeur - 4, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 16, YLOC, 3 * unite_largeur - 18, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 19 - 16, YLOC, 5 * unite_largeur + 10, unite_hauteur * 2);

                graphic.DrawString(i.ToString(), fnt1, Brushes.Black, 1 * unite_largeur + 9, YLOC,drawFormatRight);
                graphic.DrawString(listeSolde[i].Employe, fnt1, Brushes.Black, 2 * unite_largeur + 0, YLOC);
                graphic.DrawString(listeSolde[i].Service , fnt1, Brushes.Black, 10 * unite_largeur + 15, YLOC);
                //graphic.DrawString(String.Format(elGR, "{0:0,0}", listeSolde[i].ChargeSoinFamille), fnt1, Brushes.Black, 18 * unite_largeur - 0,YLOC,drawFormatRight);
              
                //total += listeSolde[i].ChargeSoinFamille;
                j++;
            }
            //var LOC = 9 * unite_hauteur + 3 + listeSolde.Count * 45;
            //graphic.DrawRectangle(Pens.Black, 17, LOC, 23 * unite_largeur + 15, 2 * unite_hauteur);
            //graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 18 * unite_largeur + 12, LOC + 12);

            //graphic.DrawString("Fait à Bébédjia le  " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + unite_hauteur * 6 - 10);
            //graphic.DrawString("Le Directeur ", fnt11, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);
            //var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
            //if (dtP.Rows.Count > 0)
            //{
            //    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
            //}
            return bitmap;
        }

    }
}
