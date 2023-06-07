using System;

namespace GestionPharmacetique.AppCode
{
    public class Livraison
    {
        public int ID { get; set; }
        public int NumeroCommande { get; set; }
        public DateTime DateCommande { get; set; }
        public decimal MontantFactural { get; set; }
        public int NumFournisseur { get; set; }
        public string NumProduit { get; set; }
        public int QuantiteCommandee { get; set; }
        public int QuantiteLivree { get; set; }
        public decimal PrixAchat { get; set; }
        public decimal PrixVente { get; set; }
        public string Designation { get; set; }
        public string NomFournisseur { get; set; }
        public string NumeroFacture { get; set; }
        public decimal AutresCharges { get; set; }
        public int Etat { get; set; }
        public string NoLot { get; set; }
        public DateTime DateExpiration { get; set; }
        public DateTime DateLivraison { get; set; }
    }
}