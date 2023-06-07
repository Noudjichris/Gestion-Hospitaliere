using System;

namespace GestionPharmacetique.AppCode
{
public class Vente
    {

    public int IdVente { get; set; }
       public string  NumeroMedicament { get; set; }
       public int Quantite { get; set; }
       public decimal PrixTotal { get; set; }
       public DateTime DateVente { get; set; }
       public int NumClient { get; set; }
       public decimal PrixAchat { get; set; }
       public decimal PrixVente { get; set; }
        public string Designation { get; set; }
        public decimal  NombreVente { get; set; }
       public string NomMedicament { get; set; }
       public string NumeroEmploye { get; set; }
       public DateTime Heure { get; set; }
       public double PartAssure { get; set; }
       public bool SiVente { get; set; }
    
}
}
