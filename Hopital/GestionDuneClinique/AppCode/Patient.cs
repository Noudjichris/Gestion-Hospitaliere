using System;
namespace GestionDuneClinique.AppCode
{
   public  class Patient
    {
       public int NumeroPatient { get; set; }
       public string Nom { get; set; }
       public string Rhesus { get; set; }
       public string Prenom { get; set; }
       public string Sexe { get; set; }
       public string Matricule { get; set; }
       public string  An { get; set; }
       public string Telephone { get; set; }
       public string NomEntreprise{get;set;}
       public DateTime DateEnregistrement { get; set; }
       public string SousCouvert {get;set;}
       public string NumeroSocial { get; set; }
       public string Adresse { get; set; }
       public string Fonction { get; set; }
       public bool Alcoolo { get; set; }
       public bool Tabagiste { get; set; }
       public bool Drogueur { get; set; }
       public bool Couvert { get; set; }
       public string  Mois { get; set; }
    }
}
