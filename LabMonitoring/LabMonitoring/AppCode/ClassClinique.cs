using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace LabMonitoring.AppCode
{
   public  class ClassClinique
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static string requete;

        static ClassClinique()
        {
            string connectionString =
                 @"server=172.24.114.7;user id=sos;password=sos2019;database=clinique_db";
             //@"server=localhost;user id=root;password=chris@2022;database=clinique_db";
            connection = new MySqlConnection(connectionString);
            command = new MySqlCommand("", connection);
        }
        // liste des patients
        public static List<Patient> ListeDesPatients()
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl ORDER BY nom, prenom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                   var patient = new  Patient();
                    patient.IDPatient = reader.GetInt32(0);
                    patient.NomPatient = reader.GetString(1).ToUpper()+" " + reader.GetString(2).ToUpper();
                    patient.Sexe = reader.GetString(3);
                    patient.Age = reader.GetString(4);
                    
                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }
            return listePatient;
        }

        public static Utilisateur SeConnecter(string nomUtil, string motPasse)
        {
            Utilisateur utilisateur = null;

            try
            {
                if (!string.IsNullOrEmpty(nomUtil))
                {
                    connection.Open();
                    string query = string.Format(
                        "SELECT mot_de_passe FROM utilisateur_tbl WHERE nom_utilisateur= '{0}' ", nomUtil);
                    command.CommandText = query;

                    var dbMotPassword = command.ExecuteScalar().ToString();

                    if (dbMotPassword == motPasse)
                    {
                        query =
                            string.Format(
                                "SELECT utilisateur_tbl.num_utilisateur , utilisateur_tbl.type_utilisateur,  empl_tbl.nom_empl, empl_tbl.num_empl,empl_tbl.photos FROM (utilisateur_tbl INNER JOIN " +
                                " empl_tbl ON utilisateur_tbl.num_empl = empl_tbl.num_empl) WHERE utilisateur_tbl.nom_utilisateur = '{0}' ",
                                nomUtil);
                        command.CommandText = query;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var id = reader.GetInt32(0);
                            var type = reader.GetString(1).Trim();
                            var nomEmploy = reader.GetString(2).Trim();
                            var numEmploy = reader.GetString(3);
                            var image = !reader.IsDBNull(4) ? reader.GetString(4).Trim() : "";

                            utilisateur = new Utilisateur(id, nomUtil, motPasse, type, numEmploy, nomEmploy, image);
                        }
                    }
                    else
                    {

                        return null;
                    }
                }
                else
                {

                    return null;
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Erreur d'authentification", ex);
                return null;
            }
            finally
            {
                connection.Close();
            }
            return utilisateur;
        }

        //modifier un utilisateur
        public static bool ModifierUnUtilisateur(Utilisateur utilisateur)
        {
            bool flag = false;
            try
            {
                string requete = string.Format("UPDATE utilisateur_tbl SET nom_utilisateur = '{0}', mot_de_passe = '{1}', type_utilisateur = '{2}' WHERE (num_utilisateur = {3})",
                    utilisateur.NomUtilisateur, utilisateur.MotPasse, utilisateur.TypeUtilisateur, utilisateur.NumeroUtilisateur);
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
               GestionPharmacetique. MonMessageBox.ShowBox("Compte d'utilisateur modifié avec succés", "Modification utilisateur", "affirmation.png");
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
               GestionPharmacetique. MonMessageBox.ShowBox("L'enregistrement de l'utilisateur a échoué", "Ajouter utilisateur", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }
        public static List<Utilisateur> ListesDesUtilisateurs()
        {
            List<Utilisateur> listeUtilisateur = new List<Utilisateur>();
            try
            {
                connection.Open();
                string query = "SELECT utilisateur_tbl.num_utilisateur, utilisateur_tbl.nom_utilisateur, " +
                               "utilisateur_tbl.mot_de_passe, utilisateur_tbl.type_utilisateur,  empl_tbl.nom_empl,empl_tbl.photos,empl_tbl.num_empl  FROM ( " +
                               " utilisateur_tbl INNER JOIN empl_tbl ON utilisateur_tbl.num_empl = empl_tbl.num_empl)";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int numeroUtilsateur = reader.GetInt32(0);
                    string nomEmloye = reader.GetString(4);
                    string nomUtilisateur = reader.GetString(1);
                    string motPasse = reader.GetString(2);
                    string type = reader.GetString(3);
                    string numeroEmpl = reader.GetString(6);
                    Utilisateur utilisateur = new Utilisateur(numeroUtilsateur, nomUtilisateur, motPasse, type,
                       numeroEmpl, nomEmloye, "");
                    listeUtilisateur.Add(utilisateur);

                }
            }
            catch (Exception ex) {GestionPharmacetique. MonMessageBox.ShowBox("Liste utilisateur", ex); }
            finally
            {
                connection.Close();
            }
            return listeUtilisateur;
        }

        //obtenir la liste des utilisateurs
        public static List<Utilisateur> ListesDesUtilisateurs(int numUtil, string password)
        {
            var listeUtilisateur = new List<Utilisateur>();
            try
            {
                connection.Open();
                string query = "SELECT utilisateur_tbl.num_utilisateur, utilisateur_tbl.nom_utilisateur, " +
                               "utilisateur_tbl.mot_de_passe, utilisateur_tbl.type_utilisateur,  empl_tbl.nom_empl,empl_tbl.photos FROM ( " +
                               " utilisateur_tbl INNER JOIN empl_tbl ON utilisateur_tbl.num_empl = empl_tbl.num_empl) WHERE " +
                               "utilisateur_tbl.num_utilisateur = " + numUtil + " AND utilisateur_tbl.mot_de_passe = '" + password + "'";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int numeroUtilsateur = reader.GetInt32(0);
                    string nomEmloye = reader.GetString(4);
                    string nomUtilisateur = reader.GetString(1);
                    string motPasse = reader.GetString(2);
                    string type = reader.GetString(3);

                    Utilisateur utilisateur = new Utilisateur(numeroUtilsateur, nomUtilisateur, motPasse, type,
                        nomEmloye);
                    listeUtilisateur.Add(utilisateur);

                }
            }
            catch (Exception ex) {GestionPharmacetique. MonMessageBox.ShowBox("Liste utilisateur", ex); }
            finally
            {
                connection.Close();
            }
            return listeUtilisateur;
        }

    }
}
