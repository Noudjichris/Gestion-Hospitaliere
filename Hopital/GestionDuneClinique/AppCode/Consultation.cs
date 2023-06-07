using System;
namespace GestionDuneClinique.AppCode
{
    public class Consultation
    {
        public int NumeroConsultation { get; set; }
        public string TypeConsultation { get; set; }
        public DateTime DateConsultation { get; set; }
        public DateTime RV { get; set; }
        public string Description { get; set; }
        public string NumeroEmploye { get; set; }
        public int IdPatient { get; set; }
        public string NomPatient {get;set;}
        public string NomEmploye { get; set; }
        public double Frais { get; set; }
        public double FraisConventionnee { get; set; }
        public string Specialite { get; set; }
        public string Partenaire { get; set; }  }
}
