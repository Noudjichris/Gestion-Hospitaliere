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
        public string Specialite { get; set; }
        public string Partenaire { get; set; }

        public Consultation(int numConsult, string typeConsult, DateTime dateConsult, 
            DateTime rv, string descript, string numEmpl, int idPatient, double frais,string specialite,string parten)
        {
            NumeroConsultation = numConsult;
                TypeConsultation =typeConsult;
            DateConsultation= dateConsult;
            Description = descript;
            RV = rv;
            Frais = frais;
            Specialite = specialite;
            NumeroEmploye = numEmpl;
            IdPatient = idPatient;
            Partenaire = parten;
        }

        public Consultation(int numConsult, string typeConsult, DateTime dateConsult,
          DateTime rv, string descript, string numEmpl, int idPatient, string nomEmpl, string nomPatient,double frais,string specialite, string parten)
        {
            NumeroConsultation = numConsult;
            TypeConsultation = typeConsult;
            DateConsultation = dateConsult;
            Description = descript;
            RV = rv;
            NumeroEmploye = numEmpl;
            Specialite = specialite;
            NomPatient = nomPatient;
            IdPatient = idPatient;
            NomEmploye = nomEmpl;
            Frais = frais;
            Partenaire = parten;
        }
    }
}
