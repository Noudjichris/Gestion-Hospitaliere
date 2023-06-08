using System;

namespace LabMonitoring.AppCode
{
   public class Utilisateur
    {
        public int NumeroUtilisateur { get; set; }
        public string NomUtilisateur { get; set; }
        public string MotPasse { get; set; }
        public string TypeUtilisateur { get; set; }
        public string NomEmploye { get; set; }
        public string Image { get; set; }
        public string NumEmploye { get; set; }
        public string Photo { get; set; }

        public Utilisateur(int numeroUtilisateur, string nomUtil,
            string motPasse, string typeUtil, string nomEmpl)
        {
            this.MotPasse = motPasse;
            this.NomUtilisateur = nomUtil;
            this.TypeUtilisateur = typeUtil;
            this.NumeroUtilisateur = numeroUtilisateur;
            this.NomEmploye = nomEmpl;
        }

        public Utilisateur(string nomUtil, string motPasse,
            string typeUtil, string nomEmp, string image)
        {
            this.MotPasse = motPasse;
            this.NomUtilisateur = nomUtil;
            this.TypeUtilisateur = typeUtil;
            this.NomEmploye = nomEmp;
            this.Image = image;

        }

        public Utilisateur(int id, string nomUtil, string motPasse,
            string typeUtil, string numEmpl, string nomEmp, string image)
        {
            NumeroUtilisateur = id;
            this.MotPasse = motPasse;
            this.NomUtilisateur = nomUtil;
            this.TypeUtilisateur = typeUtil;
            this.NomEmploye = nomEmp;
            this.Image = image;
            NumEmploye = numEmpl;
        }
        public Utilisateur(string nomUtil, string motPasse,
           string typeUtil, string numEmp)
        {
            this.MotPasse = motPasse;
            this.NomUtilisateur = nomUtil;
            this.TypeUtilisateur = typeUtil;
            this.NumEmploye = numEmp;

        }

        public Utilisateur(int numeroUtilisateur, string nomUtil,
              string motPasse, string typeUtil)
        {
            this.MotPasse = motPasse;
            this.NomUtilisateur = nomUtil;
            this.TypeUtilisateur = typeUtil;
            this.NumeroUtilisateur = numeroUtilisateur;
        }

    }
}
