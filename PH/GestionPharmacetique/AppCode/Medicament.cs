
using System;

namespace GestionPharmacetique.AppCode
{
    public class Medicament
    {
        public string NumeroMedicament { get; set; }
        public string NomMedicament { get; set; }
        public decimal PrixAchat { get; set; }
        public decimal PrixVente { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public DateTime DateExpiration { get; set; }
        public int CodeFamille { get; set; }
        public string Designation { get; set; }
        public int Quantite { get; set; }
        public int GrandStock { get; set; }
        public decimal PrixTotal { get; set; }
        public int QuantiteAlerte { get; set; }
        public int NombreDetail { get; set; }
        public decimal PrixVenteDetail { get; set; }
        public int NombreBoite { get; set; }

    }
}
