using System;

namespace LabMonitoring.AppCode
{
    public class Resultat
    {
        public int NumeroResultat { get; set; }
        public DateTime DateResultat { get; set; }
        public int IDPatient { get; set; }
        public string  Appreciation { get; set; }
        public string  ResultatExamen { get; set; }
        public int IDExam { get; set; }
        public string Service { get; set; }
        public string Lit { get; set; }
    public   string Prescripteur { get; set; }
    }
}
