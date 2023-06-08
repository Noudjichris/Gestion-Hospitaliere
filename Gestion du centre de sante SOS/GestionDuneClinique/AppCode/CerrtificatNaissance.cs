using System;

namespace GestionDuneClinique.AppCode
{
    public class CertificatNaissance
    {
        public int ID { get; set; }
        public int IDPatient { get; set; }
        public string NumeroEmplye { get; set; }
        public DateTime DateNaissanceEnfant { get; set; }
        public string LieuNaissanceEnfant { get; set; }
        public string NomEnfant { get; set; }
        public string Sexe { get; set; }
        public float Poids { get; set; }
        public string ProfesssionMere { get; set; }
        public string LieuNaissanceMere { get; set; }
        public DateTime DateNaissanceMere { get; set; }
        public string NomPere { get; set; }
        public string ProfessionPere { get; set; }
        public string LieuNaissancePere { get; set; }
        public DateTime DateNaissancePere { get; set; }
       
        public string ProfessionMere { get; set; }

   
    }
}
