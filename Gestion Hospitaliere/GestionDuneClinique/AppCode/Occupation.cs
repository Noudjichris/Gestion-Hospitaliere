using System;

namespace GestionDuneClinique.AppCode
{
    public class Occupation
    {
        public int NumeroOccupation { get; set; }
        public int IdPatient { get; set; }
        public int NoSalle { get; set; }
        public DateTime DateEntree { get; set; }
        public DateTime DateSortie { get; set; }
        public double PrixTotal { get; set; }
        public string SalleLit { get; set; }
        public string Patient { get; set; }
        public double Prix { get; set; }
        public double NombreJour { get; set; }
    }
}
