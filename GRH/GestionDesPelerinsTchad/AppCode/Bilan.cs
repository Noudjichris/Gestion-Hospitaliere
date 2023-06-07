using System;


namespace SGSP.AppCode
{
   public  class Bilan
    {
       public double GRGR{get; set}
        public int IDBilan { get; set; }
        public int IDDetailBilan { get; set; }
        public int Annee { get; set; }
        public string TypeBilan { get; set; }
        public string Code { get; set; }
        public string Designation { get; set; }
        public string Etat { get; set; }
        public string TypeDetail { get; set; }
        public double Montant { get; set; }
    }
}
