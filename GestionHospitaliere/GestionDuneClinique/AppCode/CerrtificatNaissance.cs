using System;

namespace GestionDuneClinique.AppCode
{
    public class CertificatNaissance
    {
        public int ID { get; set; }
        public int IDPatient { get; set; }
        public string SageFemme { get; set; }
        public DateTime NaissanceEnfant { get; set; }
        public string BeBe { get; set; }
        public string Sexe { get; set; }
        public float Poids { get; set; }
        public string ProfesssionMere { get; set; }
        public string DomicileMere { get; set; }
        public DateTime NaissanceMere { get; set; }
        public string Epoux { get; set; }
        public string ProfessionEpoux { get; set; }
        public string DomicileEpoux { get; set; }
        public DateTime NaissanceEpoux { get; set; }
        public Double Frais { get; set; }
    }
}
