using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.AppCode
{
    public class Salaire
    {
        public double SalaireBase { get; set; }
        public decimal GrilleSalarialle { get; set; }
        public decimal Indice { get; set; }
        public string NumeroMatricule { get; set; }
        public int  IDSalaire { get; set; }
        public double PrimeLogement { get; set; }
        public double PrimeGarde { get; set; }
        public double PrimeResponsabilite { get; set; }
        public double PrimeTransport { get; set; }
        public double PrimeExceptionnel { get; set; }
        public double TauxCNPS { get; set; }
        public double TauxIRPP { get; set; }
        public double TauxCNPSPatronal { get; set; }
        public double TauxONASA { get; set; }
    }
}
