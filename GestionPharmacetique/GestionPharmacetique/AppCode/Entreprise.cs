using System;

namespace GestionPharmacetique.AppCode
{
    public class Entreprise
    {
        public int NumeroEntreprise { get; set; }
        public string NomEntreprise { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public double Montant { get; set; }
        public int IdPaiement { get; set; }
        public string ModePaiement { get; set; }
        public string Cheque { get; set; }
        public DateTime DatePaiement { get; set; }
        public bool SiLimite { get; set; }
        public double MontantLimite { get; set; }

        public Entreprise(int numEntreprise, string entreprise, string tele1,
    string tele2, string email, string adresse)
        {
            NumeroEntreprise = numEntreprise;
            NomEntreprise = entreprise;
            Telephone1 = tele1;
            Telephone2 = tele2;
            Email = email;
            Adresse = adresse;
        }

        public Entreprise(int id, int idEntrep, DateTime datePaie, double montant, string entrep, string mode, string cheque)
        {
            IdPaiement = id;
            NomEntreprise = entrep;
            NumeroEntreprise = idEntrep;
            Montant = montant;
            ModePaiement = mode;
            Cheque = cheque;
            DatePaiement = datePaie;
        }

        public Entreprise(bool siLimit, double montantLimit)
        {
            SiLimite = siLimit;
            MontantLimite = montantLimit;
        }
    }
}
