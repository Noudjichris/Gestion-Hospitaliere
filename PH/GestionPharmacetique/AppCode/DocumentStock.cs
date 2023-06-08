using System;

namespace GestionPharmacetique.AppCode
{
   public  class DocumentStock
    {
        public int ID { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }            
    }
}
