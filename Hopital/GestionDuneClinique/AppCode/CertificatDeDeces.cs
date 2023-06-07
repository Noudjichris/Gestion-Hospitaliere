using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionDuneClinique.AppCode
{
   public  class CertificatDeDeces
    {
       public int ID { get; set; }
       public int IDPatient { get; set; }
       public string  IDEmploye { get; set; }
       public DateTime DateHospitalisation { get; set; }
       public DateTime DateDeces{ get; set; }
       public string CauseDeces { get; set; }
       public string Service { get; set; }
       public double FraisCertificat{get;set;}
    }
}
