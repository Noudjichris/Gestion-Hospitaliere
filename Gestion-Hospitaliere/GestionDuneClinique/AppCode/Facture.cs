using System;

namespace GestionDuneClinique.AppCode
{
   public  class Facture
    {
        public int NumeroFacture { get; set; }
        public DateTime DateFacture { get; set; }
        public double MontantFactural { get; set; }
        public int IdPatient { get; set; }
        public string NumeroEmploye { get; set; }
        public string Designation { get; set; }
        public double Prix { get; set; }
        public string Patient { get; set; }
        public string NomEmploye { get; set; }
       public int  Quantite {get;set;}
       public double PrixTotal { get; set; }
       public double Reste { get; set; }

        //public Facture(int numFact, DateTime dateFact, double montantFact, int idPatient, string numEmpl,double reste)
        //{
        //    NumeroFacture = numFact;
        //    DateFacture = dateFact;
        //    NumeroEmploye = numEmpl;
        //    MontantFactural = montantFact;
        //    IdPatient = idPatient;
        //    Reste = reste;
        //}

        //public Facture(int numFact, DateTime dateFact, double montantFact, int idPatient, string patient,string numEmpl,string nomEmpl,double reste)
        //{
        //    NumeroFacture = numFact;
        //    DateFacture = dateFact;
        //    MontantFactural = montantFact;
        //    Patient = patient;
        //    NumeroEmploye = numEmpl;
        //    IdPatient = idPatient;
        //    Reste = reste;
        //    NomEmploye = nomEmpl;
        //}

        //public Facture(string designation ,double prix,int qte,double prixTotal)
        //{
        //    Designation = designation;
        //    Prix = prix;
        //    Quantite = qte;
        //    PrixTotal = prixTotal;
        //}
    }
}
