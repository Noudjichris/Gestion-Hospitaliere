using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionPharmacetique.AppCode
{
    public class Inventaire
    {

        public int ID { get; set; }
        public int IDInventaire { get; set; }
        public int Quantite { get; set; }
        public double PrixCession { get; set; }
        public double PrixPublic { get; set; }
        public string IDProduit { get; set; }
        public int QuantiteAvant { get; set; }
        public string Emplacement { get; set; }
        public int Ordre { get; set; }
        public string IndexDetail { get; set; }
        public DateTime DateInventaire { get; set; }
        public string Employe { get; set; }
        public int NumeroFamille { get; set; }
        public string Nomproduit { get; set; }
        public DateTime DateExpiration { get; set; }
        public bool Etat { get; set; }
    }
}
