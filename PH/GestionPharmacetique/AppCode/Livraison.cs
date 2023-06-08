using System;

namespace GestionPharmacetique.AppCode
{
    public class Livraison
    {
        public int ID { get; set; }
        public string NumLivraison { get; set; }
        public DateTime DateLivraison { get; set; }
        public decimal MontantFactural { get; set; }
        public int  NumFournisseur  { get; set; }
        public string  NumProduit { get; set; }
        public int Quantite { get; set; }
        public decimal PrixAchat { get; set; }
        public decimal PrixVente { get; set; }
   public string NomMedicament { get; set; }
        public string NomFournisseur { get; set; }
        public decimal AutresCharges { get; set; }

        public Livraison( string  numProduit ,decimal prixAchat,
           decimal prixVente, int quantite)
        {
            this.NumProduit = numProduit;
            this.PrixAchat = prixAchat;
            this.Quantite = quantite;
            this.PrixVente = prixVente;
        }


        public Livraison(string numLivraison, DateTime dateLivarison,
            int numFourn ,string  nomFournisseur, decimal ath, decimal ttc)
        {
            this.NumLivraison = numLivraison;
            this.DateLivraison = dateLivarison;
            this.NomFournisseur = nomFournisseur;
            this.NumFournisseur = numFourn;
            this.MontantFactural = ttc;
            this.AutresCharges = ath;
        }

        public Livraison(int id ,string numLivraison, string numProduit, string nomMedi, decimal prixAchat,
           decimal prixVente, int quantite)
        {
            this.ID = id;
            this.NumLivraison = numLivraison;
            this.NumProduit = numProduit;
            this.PrixAchat = prixAchat;
            this.Quantite = quantite;
            this.PrixVente = prixVente;
            this.NomMedicament = nomMedi;
        }
    }
}
