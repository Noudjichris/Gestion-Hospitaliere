using System;

namespace GestionDuneClinique.AppCode
{
    public class Ordonnance
    {
        public int NumeroOrdonnance { get; set; }
        public DateTime DateOrdonnance { get; set; }
        public int IdPatient { get; set; }
        public string NumeroEmploye { get; set; }
        public string Medicament { get; set; }
        public string Quantite { get; set; }
        public int Jour { get; set; }
        public int NombreDeFois { get; set; }
        public string Patient { get; set; }
        public string Employe
        { get; set; }
        public Ordonnance(int idOrdonnance, DateTime dateOrdonnance, int idPatient, string numempl)
        {
            NumeroOrdonnance = idOrdonnance;
            DateOrdonnance = dateOrdonnance;
            IdPatient = idPatient;
            NumeroEmploye = numempl;
        }

            public Ordonnance(int idOrdonnance, DateTime dateOrdonnance, int idPatient, string patient,string numempl,string nomEmpl)
        {
            NumeroOrdonnance = idOrdonnance;
            DateOrdonnance = dateOrdonnance;
            IdPatient = idPatient;
            NumeroEmploye = numempl;
                Employe = nomEmpl;
                Patient = patient;
        }

        public Ordonnance(int idOrdonnance, string medicament, string qte, int nbreFois, int jour)
        {
            NumeroOrdonnance = idOrdonnance;
            Medicament = medicament;
            Quantite = qte;
            Jour = jour;
            NombreDeFois = nbreFois;
        }

    }
}
