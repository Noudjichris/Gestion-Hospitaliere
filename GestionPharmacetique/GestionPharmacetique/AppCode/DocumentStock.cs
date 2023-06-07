using System;

namespace GestionPharmacetique.AppCode
{
   public  class DocumentStock
    {
        public int IDReference { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Etat { get; set; }
        public string NumeroMatricule { get; set; }
        public int ID { get; set; }
        public bool EtatValider { get; set; }
        public int EtatDepot { get; set; }
        public int TypePrix { get; set; }
    }
}
