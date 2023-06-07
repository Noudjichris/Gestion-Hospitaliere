using System;

namespace GestionPharmacetique.AppCode
{
    public class Produit
    {
        public int GroupeID { get; set; }
        public string Reference { get; set; }
        public string Designation { get; set; }
        public int GrandStock { get; set; }
        public Decimal PrixAchat { get; set; }
        public Decimal PrixCession { get; set; }
        public double TotalAchat { get; set; }
        public double TotalCession { get; set; }
        public double PrixPublic { get; set; }
        public string Image { get; set; }
        public int StockMinimal { get; set; }
        public string Emplacement { get; set; }
        public string CodeBarre { get; set; }
        public int ReferenceFournisseur { get; set; }
        public double TauxTVA { get; set; }
        public string NumeroProduit { get; set; }
        public string Groupe { get; set; }
        public int NombreSortie { get; set; }
        public int NombreEntree { get; set; }
        public int NombreTotalVendue { get; set; }
        public int NombreTotalAchete { get; set; }
        public string Description { get; set; }
        public DateTime DateExpiration { get; set; }
        public DateTime DateModification { get; set; }
        public int NombreJour { get; set; }
        public int Stock { get; set; }
        public string IDUser { get; set; }
        public string Motif { get; set; }
        public int DifferenceStock { get; set; }
    }
}
