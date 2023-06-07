﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.AppCode
{
   public  class Paiement
    {
       public int ID { get; set; }
       public int Exercice { get; set; }
       public int IDPaie { get; set; }
       public int IDAcompte { get; set; }
       public double  SalaireBase{ get; set; }
       public double GainSalarial { get; set; }
       public double GainAnciennete{ get; set; }
       public double PrimeResponsabilite { get; set; }
       public double JourPrestations { get; set; }
       public double PrimeLogement { get; set; }
       public double HeureSupplementaire { get; set; }
       public double PrimeGarde { get; set; }
       public double SalaireBrut { get; set; }
       public double SalaireNet { get; set; }
       public double AcomptePaye { get; set; }
       //public double ChargeSoinFamille { get; set; }
       public string  ModePaiement { get; set; }
       public string MoisPaiement { get; set; }
       public string Employe { get; set; }
       public double MontantTotal { get; set; }
       public double AvanceSurSalaire { get; set; }
       public string  NumeroMatricule { get; set; }
       public string LibelleRecap { get; set; }
        public string Service { get; set; }
        public string Banque { get; set; }
        public int IDRecap  { get; set; }
       public DateTime DatePaiement { get; set; }
       public double CNPS { get; set; }
       public double IRPP { get; set; }
       public double CongeAnnuel { get; set; }
       public double ONASA { get; set; }
       public double Transport { get; set; }
       public double ChargePatronale { get; set; }
       public double CoutAbsence { get; set; }
       public double CoutDuSalarie { get; set; }
    }
}
