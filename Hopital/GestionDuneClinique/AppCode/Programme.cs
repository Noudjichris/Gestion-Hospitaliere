using System;

namespace GestionDuneClinique.AppCode
{
    public class Programme
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int NumeroDetailProgramme { get; set; }
        public int NumeroProgramme { get; set; }
        public int Annee { get; set; }
        public string  Libelle { get; set; }
        public string NumeroEmploye { get; set; }
        public string NomEmploye { get; set; }
        public string Legende { get; set; }
        public string Abreviation { get; set; }
        public int IDLegende { get; set; }
        public int IDService { get; set; }
    }
}
