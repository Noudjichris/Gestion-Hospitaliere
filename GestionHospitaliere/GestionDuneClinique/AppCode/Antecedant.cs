using System;

namespace GestionDuneClinique.AppCode
{
   public  class Antecedant
    {
       public int NumeroAntecedant { get; set; }
       public DateTime DebutAntecedant { get; set; }
       public DateTime FinAntecedant { get; set; }
       public string Description { get; set; }
       public string Traitement { get; set; }
       public int IdPatient { get; set; }

    }
}
