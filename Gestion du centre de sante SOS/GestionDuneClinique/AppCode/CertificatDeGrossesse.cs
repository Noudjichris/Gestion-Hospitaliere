using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionDuneClinique.AppCode
{
   public  class CertificatDeGrossesse
    {
       public int ID { get; set; }
       public int IDPatient { get; set; }
       public string Matricule { get; set; }
        public string NomDocteur { get; set; }
        public string NomPatient { get; set; }
        public int PeriodeGrossesse { get; set; }
       public DateTime DateAccouchement { get; set; }
       public DateTime  DateDDR { get; set; }
        public DateTime DateConge { get; set; }
    }
}
