using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionDuneClinique.AppCode
{
   public  class CertificatDeGrossesse
    {
       public int ID { get; set; }
       public int IDPatiente { get; set; }
       public string Epoux { get; set; }
       public string SageFemme { get; set; }
       public int Periode { get; set; }
       public DateTime DateAccouchement { get; set; }
       public Double FraisCertificat { get; set; }
    }
}
