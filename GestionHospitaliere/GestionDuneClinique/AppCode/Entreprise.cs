using System;

namespace GestionDuneClinique.AppCode
{
    public class Entreprise
    {
        public int NumeroEntreprise { get; set; }
        public string NomEntreprise { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public DateTime DateAbonnement{get;set;}
        public double Montant { get; set; }
        public int IdPaiement {get;set;}
        public string ModePaiement{get;set;}
        public  double PrixHonoraire {get;set;}
        public string Cheque { get; set; }
        public string Attache { get; set; }
        public bool SiLimite { get; set; }
        public double MontantLimite { get; set; }
        public Entreprise(int numEntreprise, string entreprise, string tele1, 
    string tele2, string email, string adresse,DateTime date, double prixHonor,string attache, bool siLimit, double montantLimit)
        {
            NumeroEntreprise = numEntreprise;
            NomEntreprise = entreprise;
            Telephone1 = tele1;
            Telephone2 = tele2;
            Email = email;
            Adresse = adresse;
            DateAbonnement = date;
            PrixHonoraire = prixHonor;
            Attache = attache;
            SiLimite = siLimit;
            MontantLimite = montantLimit;
        }
        public Entreprise( bool siLimit, double montantLimit)
        {
            SiLimite = siLimit;
            MontantLimite = montantLimit;
        }
    }
}
