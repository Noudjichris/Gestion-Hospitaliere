using System;

namespace SGSP.AppCode
{
    public class Absence
    {
        public string NumeroMatricule { get; set; }
        public DateTime DateDebutAbscense { get; set; }
        public DateTime DateRetour { get; set; }
        public DateTime DateDemande { get; set; }
        public string Motif { get; set; }
        public int IDAbsence { get; set; }
        public string NomPersonnel { get; set; }
    }
}
