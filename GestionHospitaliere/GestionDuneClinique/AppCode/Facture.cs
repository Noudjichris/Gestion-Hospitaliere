using System;

namespace GestionDuneClinique.AppCode
{
   public  class Facture
    {
        public int NumeroFacture { get; set; }
        public DateTime DateFacture { get; set; }
        public double MontantFactural { get; set; }
        public int IdPatient { get; set; }
        public int NumeroActe { get; set; }
        public string NumeroEmploye { get; set; }
        public string Designation { get; set; }
        public double Prix { get; set; }
        public string Patient { get; set; }
        public string NomEmploye { get; set; }
       public int  Quantite {get;set;}
       public double PrixTotal { get; set; }
       public double Reste { get; set; }
        public double PartPatient { get; set; }
        public string Sub { get; set; }
        public string Entreprise { get; set; }
    }
}
