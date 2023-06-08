using System;

namespace GestionDuneClinique.AppCode
{
     public   class Analyse
     {
         public int NumeroGroupe { get; set; }
         public int NumeroAnalyse { get; set; }
         public string TypeAnalyse { get; set; }
         public string Description { get; set; }
         public DateTime DateAnalyse { get; set; }
         public int IdPatient { get; set; }
         public int IndexPrix { get; set; }
         public string Matricule { get; set; }
         public double MontantTotal { get; set; }
         public int NumeroListeAnalyse{get;set;}
         public string NumeroEmploye { get; set; }
         public string Partenaire { get; set; }
         public double FraisNRC { get; set; }
         public double Frais { get; set; }
         public double FraisAssure { get; set; }
         public double FraisCICR { get; set; }
         public double FraisMSF { get; set; }
         public int NombreAnalyse { get; set; }

     }
}
