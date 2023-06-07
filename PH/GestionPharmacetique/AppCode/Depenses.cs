using System;

namespace GestionPharmacetique.AppCode
{
    public class Depenses
    {
        public int ID { get; set; }
        public string Categorie { get; set; }
        public string Libelle { get; set; }
        public int IDDepense { get; set; }
        public DateTime Date { get; set; }
        public double Montant { get; set; }
        public string Beneficiaire { get; set; }
        public string NumeroFacture { get; set; }
    }
}
