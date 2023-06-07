using System;

namespace GestionDuneClinique.AppCode
{
    public class EmployeEntreprise
    {
        public int Numero { get; set; }
        public string Nom { get; set; }
        public string Sexe { get; set; }
        public string Age { get; set; }
        public string Telephone { get; set; }
        public string Matricule { get; set; }
        public string Fonction { get; set; }
        public int IdEntreprise { get; set; }
        public int IdPatient { get; set; }
    }
}
