using System;

namespace GestionDuneClinique.AppCode
{
   public  class Versement
    {
       public int  IDVersment { get; set; }
       public double  MontantVerse { get; set; }
       public string NumeroEmploye { get; set; }
       public DateTime DateVersement { get; set; }
    }
}
