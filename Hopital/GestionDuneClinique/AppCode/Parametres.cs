using System;

namespace GestionDuneClinique.AppCode
{
    public class Parametres
    {
       public  DateTime Date { get; set; }
        public string  Tension { get; set; }
        public double Poids { get; set; }
        public double Temperature { get; set; }
        public double Glycemie { get; set; }
        public double Taille { get; set; }
        public string   Pouls{get;set;}
        public int Identifiant { get; set; }
        public int ID { get; set; }
    }
}
