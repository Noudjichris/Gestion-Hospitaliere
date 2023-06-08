using System;

namespace GestionDuneClinique.AppCode
{
    public class EmployeEntreprise
    {
        public int Numero { get; set; }
        public string Nom { get; set; }
        public string Sexe { get; set; }
        public int Age { get; set; }
        public string Telephone { get; set; }
        public int IdEntreprise { get; set; }

        public EmployeEntreprise(int num, string nom, string sexe, int age, string tele, int idEntrep)
        {
            Numero = num;
            Nom = nom;
            Sexe = sexe;
            Age = age;
            Telephone = tele;
            IdEntreprise = idEntrep;
        }

    }
}
