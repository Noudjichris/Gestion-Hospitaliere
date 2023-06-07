using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.AppCode
{
    public class Salaire
    {
        public decimal SalaireBrut { get; set; }
        public decimal GrilleSalarialle { get; set; }
        public decimal Indice { get; set; }
        public string NumeroMatricule { get; set; }
        public int  IDSalaire { get; set; }
        public double PrimeLogement { get; set; }
        public double PrimeGarde { get; set; }
        public double PrimeResponsabilite { get; set; }
        public double PrimeTransport { get; set; }
        public double PrimeExceptionnel { get; set; }
    }
}
