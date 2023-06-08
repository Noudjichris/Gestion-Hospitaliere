using System;
namespace GestionDuneClinique.AppCode
{
   public  class Observation
    {
       public int ID { get; set; }
       public int IDObservation { get; set; }
       public string Observations { get; set; }
       public double Frais { get; set; }
       public double Nombre { get; set; }
       public DateTime DateDebut { get; set; }
       public int IDPatient { get; set; }
       public double Total { get; set; } 
    }
}
