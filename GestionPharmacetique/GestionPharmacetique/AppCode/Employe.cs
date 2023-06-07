namespace GestionPharmacetique.AppCode
{
   public  class Employe
    {
        public string  NumMatricule { get; set; }
        public string NomEmployee { get; set; }
        public string Addresse { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Email { get; set; }
        public string Titre { get; set; }
        public string Ville { get; set; }
        public string Photo { get; set; }
    

        public Employe(string  numMatricule, string nomEmployee, string addresse , 
            string telephone1, string telephone2, string email,
            string titre,string ville,string photo)
        {
            this.NumMatricule = numMatricule;
            this.NomEmployee = nomEmployee;
            this.Addresse = addresse;
            this.Telephone1 = telephone1;
            this.Photo = photo;
            this.Telephone2 = telephone2;
            this.Email = email;
            this.Titre = titre;
            this.Ville = ville;
            this.Photo = photo;
        }

        public Employe(string numMatricule, string nomEmployee, string addresse,
            string telephone1, string telephone2, string email,
            string titre, string ville)
        {
            this.NumMatricule = numMatricule;
            this.NomEmployee = nomEmployee;
            this.Addresse = addresse;
            this.Telephone1 = telephone1;
            this.Telephone2 = telephone2;
            this.Email = email;
            this.Titre = titre;
            this.Ville = ville;
        }
    }
}
