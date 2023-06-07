using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.AppCode
{
   public  class Service
    {
        public string Anciennete { get; set; }
        public string Categorie { get; set; }
        public string Contrat { get; set; }
        public DateTime DateService { get; set; }
        public string Diplome { get; set; }
        public string Echelon { get; set; }
        public bool Etat { get; set; }
        public string NoCNPS { get; set; }
        public string  NumeroMatricule { get; set; }
        public int NumeroService { get; set; }
        public string  Poste { get; set; }
        public bool  EtatContrat { get; set; }
        public DateTime DateFinContrat { get; set; }
        public DateTime DateRetraite { get; set; }
    }
}
