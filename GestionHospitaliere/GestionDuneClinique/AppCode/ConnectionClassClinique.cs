using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;
using MySql.Data.MySqlClient;

namespace GestionDuneClinique.AppCode
{
    public class ConnectionClassClinique
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static MySqlTransaction transaction = null;
        private static string requete;     
       
        static ConnectionClassClinique()
        {
            string connectionString =
            @"server=192.168.1.3;user id=hnda;password=Hnda2021;database=clinique_db"; //AdminSOS2019
            //@"server=localhost;port=3306;user id=root;database=clinique_db";
            connection = new MySqlConnection(connectionString);
            command = new MySqlCommand("", connection);
        }

        public static void Backup(string sFilePath)
        {
            //string file = "C:tempbackup.sql";
            try
            {
                using (MySqlBackup mb = new MySqlBackup(command))
                {
                    connection.Open();
                    mb.ExportToFile(sFilePath);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("back up files", ex);
            }
        }

        public static void Restore(string sfilePath)
        {
            try
            {
                using (MySqlBackup mb = new MySqlBackup(command))
                {
                    connection.Open();
                    mb.ImportFromFile(sfilePath);
                    connection.Close();
                    MonMessageBox.ShowBox("Données importées avec succés", "Affirmation", "affirmaion.png");
                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Restore", ex);
            }
        }
        /*************************** CODE UTILISATEUR ***************************************/
        #region Utilisateur
        // ecrire la methode qui permet d' authentifier l'employee quand il veux se connecter
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

                            utilisateur = new Utilisateur(id, nomUtil, motPasse, type,numEmploy, nomEmploy, image);
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
                MonMessageBox.ShowBox("Erreur d'authentification", ex);
                return null;                
            }
            finally
            {
                connection.Close();
            }
            return utilisateur;
        }

        //modifier mot de passe 
        public static void ModifierMotDePasse(string utilisateur, string ancienMotPasse, string nouveauMotPasse)
        {
            try
            {
                //if (!string.IsNullOrEmpty(ancienMotPasse))
                //{
                connection.Open();
                //    string query = string.Format("SELECT * FROM nom_utilisateur WHERE mot_de_passe = '{0}' ", ancienMotPasse);
                //    command.CommandText = query;
                //    MySqlDataReader reader = command.ExecuteReader();
                //    DataTable dataTable = new DataTable();
                //    dataTable.Load(reader);
                //    int NbreMotPasse = dataTable.Rows.Count;
                //    if (NbreMotPasse >= 1)
                //    {

                var query = string.Format("UPDATE utilisateur_tbl SET mot_de_passe = '{0}'" +
                        " WHERE nom_utilisateur = '{1}'", nouveauMotPasse, utilisateur);
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        MonMessageBox.ShowBox("Mot de passe modifié avec succés", "Modification mode passe", "affirmation.png");
                //    }
                //    else
                //    {
                //        MonMessageBox.ShowBox("L'ancien mot de passe n'est pas correct.", "Erreur saisie", "erreur.png");
                //    }
                //}
                //else
                //{
                //    //MonMessageBox.ShowBox("Veuillez remplir les champs pui reéssayer.", "Erreur saisie", "erreur.png");
                //}
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La modification de l'utilisateur a échoué", "Erreur modification de l'utilisateur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }
        //obtenir la liste des utilisateurs
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
                       numeroEmpl, nomEmloye,"");
                    listeUtilisateur.Add(utilisateur);

                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Liste utilisateur", ex); }
            finally
            {
                connection.Close();
            }
            return listeUtilisateur;
        }

        //obtenir la liste des utilisateurs
        public static List<Utilisateur> ListesDesUtilisateurs(int numUtil,string password)
        {
           var  listeUtilisateur = new List<Utilisateur>();
            try
            {
                connection.Open();
                string query = "SELECT utilisateur_tbl.num_utilisateur, utilisateur_tbl.nom_utilisateur, " +
                               "utilisateur_tbl.mot_de_passe, utilisateur_tbl.type_utilisateur,  empl_tbl.nom_empl,empl_tbl.photos FROM ( " +
                               " utilisateur_tbl INNER JOIN empl_tbl ON utilisateur_tbl.num_empl = empl_tbl.num_empl) WHERE " +
                               "utilisateur_tbl.num_utilisateur = " + numUtil + " AND utilisateur_tbl.mot_de_passe = '"+password +"'";
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
            catch (Exception ex) { MonMessageBox.ShowBox("Liste utilisateur", ex); }
            finally
            {
                connection.Close();
            }
            return listeUtilisateur;
        }

        //ajouter un utilisateur
        public static void AjouterUnUtilisateur(Utilisateur utilisateur)
        {
            try
            {
                string requete = "SELECT * FROM utilisateur_tbl WHERE nom_utilisateur ='" +
                                 utilisateur.NomUtilisateur + "'";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                int count = dt.Rows.Count;
                if (count < 1)
                {
                    requete = "SELECT * FROM empl_tbl WHERE num_empl ='" + utilisateur.NumEmploye + "'";
                    command.CommandText = requete;
                    reader = command.ExecuteReader();
                    DataTable dt1 = new DataTable();
                    dt1.Load(reader);
                    count = dt1.Rows.Count;
                    if (count >= 1)
                    {
                        requete =
                            string.Format(
                                "INSERT INTO `utilisateur_tbl` (`nom_utilisateur`, `mot_de_passe`, `type_utilisateur`," +
                                "`num_empl`) VALUES ('{0}','{1}','{2}','{3}')", utilisateur.NomUtilisateur,
                                utilisateur.MotPasse,
                                utilisateur.TypeUtilisateur, utilisateur.NumEmploye);
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        MonMessageBox.ShowBox("Nouveau utilisateur ajouté avec succés", "Information ajout",
                            "affirmation.png");
                    }
                    else
                    {
                        MonMessageBox.ShowBox(
                            "Employe numero " + utilisateur.NumEmploye + " n'existe pas  dans la base de données",
                            "erreur", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox(
                        "Le nom d'utilisateur " + utilisateur.NomUtilisateur + " existe deja  dans la base de données",
                        "erreur",
                        "erreur.png");
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement de l'utilisateur a échoué", "erreur enregistrement utilisateur",
                    exception, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
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
                MonMessageBox.ShowBox("Compte d'utilisateur modifié avec succés", "Modification utilisateur","affirmation.png");
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'enregistrement de l'utilisateur a échoué", "Ajouter utilisateur", exception,"erreur.png");
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }

        //modifier un utilisateur
        public static bool ModifierUnUtilisateur(string nomUtilisateur, string motPasse)
        {
            var flag = false;
            try
            {
                var requete = string.Format("UPDATE utiliateur_tbl SET mot_de_passe = '{0}' WHERE (nom_utilisateur = '{1}')",
                   motPasse, nomUtilisateur);
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("le mot de passe a été modifié avec succés", "Modification utilisateur","affirmation.png");
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'enregistrement de l'utilisateur a échoué", "Ajouter utilisateur", exception,"erreur.png");
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }

        //ajouter un utilisateur
        public static void SupprimerUnUtilisateur(int numeroUtil)
        {
            try
            {
                string requete = string.Format("DELETE FROM utilisateur_tbl  WHERE num_utilisateur = {0}", numeroUtil);
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données d'utilisateur supprimées avec succés", "Suppression utilisateur","affirmation.png");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La Suppression de l'utilisateur a échoué", "Suppression utilisateur", exception,"erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
        /************************* FIN CODE UTILISATEUR ************************************/
       
        /**********************************  CODE PATIENT **********************************/
        #region PatientCode
        //ajouter un patient
        public static bool EnregistrerUnPatient(Patient patient, string etat)
        {
            var flag = false;
            try
            {
                connection.Open();
                if (etat == "1")
                {
                    requete = "INSERT INTO patient_tbl (`nom`,`prenom`,`sexe`,`age`,`telephone`,`entrep`,`rhesus`,`sc`,`date_enre`,`adresse`,`nosocial`,`fonction`, alcool, tabac, drogue,couvert,mois,matricule,idcai)" +
                              " VALUES(@nom,@prenom,@sexe,@age,@telephone,@entrep,@rhesus,@sc,@date_enre,@adresse,@nosocial,@fonction,@alcool, @tabac, @drogue,@couvert,@mois,@matricule,@idcai)";
                }
                else if(etat=="2")
                {
                    requete = "UPDATE patient_tbl SET `nom` = @nom,`prenom`=@prenom,`sexe`=@sexe,`age`=@age,matricule=@matricule," +
                         "`telephone`=@telephone,`adresse`=@adresse,`nosocial`=@nosocial,`fonction`=@fonction, `entrep` =" +
                         "@entrep,`rhesus` = @rhesus, sc = @sc,alcool=@alcool, tabac=@tabac, drogue=@drogue, couvert=@couvert, mois=@mois WHERE id = " + patient.NumeroPatient;
              
                }
                command.Parameters.Add(new MySqlParameter("nom", patient.Nom));
                command.Parameters.Add(new MySqlParameter("prenom", patient.Prenom));
                command.Parameters.Add(new MySqlParameter("sexe", patient.Sexe));
                command.Parameters.Add(new MySqlParameter("age", patient.An));
                command.Parameters.Add(new MySqlParameter("telephone", patient.Telephone));
                command.Parameters.Add(new MySqlParameter("entrep", patient.NomEntreprise));
                command.Parameters.Add(new MySqlParameter("rhesus", patient.Rhesus));
                command.Parameters.Add(new MySqlParameter("adresse", patient.Adresse));
                command.Parameters.Add(new MySqlParameter("nosocial", patient.NumeroSocial));
                command.Parameters.Add(new MySqlParameter("fonction", patient.Fonction));
                command.Parameters.Add(new MySqlParameter("date_enre", DateTime.Now));
                command.Parameters.Add(new MySqlParameter("sc", patient.SousCouvert));
                command.Parameters.Add(new MySqlParameter("alcool", patient.Alcoolo));
                command.Parameters.Add(new MySqlParameter("tabac", patient.Tabagiste));
                command.Parameters.Add(new MySqlParameter("drogue", patient.Drogueur));
                command.Parameters.Add(new MySqlParameter("couvert", patient.Couvert));
                command.Parameters.Add(new MySqlParameter("mois", patient.Mois));
                command.Parameters.Add(new MySqlParameter("matricule", patient.Matricule));
                command.Parameters.Add(new MySqlParameter("idcai", GestionAcademique.LoginFrm.matricule));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de patient enregistré avec succés", "affirmation", "affirmation.png");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L' enregistrement du patient a échoué", "Erreur",exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }
        //supprimer un patient
        public static bool SupprimerUnPatient(int idPatient)
        {
            var flag = false;
            try
            {
                connection.Open();
                requete = "DELETE FROM patient_tbl  WHERE id = " + idPatient;
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de patient supprimées avec succés", "affirmation", "affirmation.png");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression du patient a échoué", "Erreur", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
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
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = reader.GetString(4);
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement =!reader.IsDBNull(12) ? reader.GetDateTime(12) : DateTime.Now;
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
                //throw;
            }
            finally
            {
                connection.Close();
            }
            return listePatient;
        }
        public static List<Patient> ListeDesPatients(int id)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE id = "+id;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement = reader.GetDateTime(12);
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
                //throw;
            }
            finally
            {
                connection.Close();
            }
            return listePatient;
        }

        //liste des patients rechercher apr leur nom
        public static List<Patient> ListeDesPatients(string nomPatient)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE nom LIKE '" + nomPatient + "%' ORDER BY nom, prenom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement = reader.GetDateTime(12);
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
            }
            finally
            { connection.Close(); }
            return listePatient;
        }
       
        //LISTE DES PATIENTS TRIES PAR SEXE
        public static List<Patient> ListeDesPatientsTrieParSexe(string sexe, string entreprise)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE sexe = '" + sexe + "' AND entrep LIKE '%" + entreprise + "%'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement = reader.GetDateTime(12);
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
            }
            finally
            { connection.Close(); }
            return listePatient;
        }
        public static List<Patient> ListeDesPatientsTrieParSexe(string sexe)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE sexe = '" + sexe + "' AND entrep ='' ORDER BY id";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement = reader.GetDateTime(12);
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
            }
            finally
            { connection.Close(); }
            return listePatient;
        }

        //liste des patients rechercher apr leur nom et entreprise
        public static List<Patient> ListeDesPatientsParEntreprise(string nomPatient, string entrep)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE nom LIKE '" + nomPatient + 
                    "%' AND entrep LIKE '" + entrep + "%' ORDER BY nom, prenom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement =!reader.IsDBNull(12) ? reader.GetDateTime(12):DateTime.Now;
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14): false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";

                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
            }
            finally
            { connection.Close(); }
            return listePatient;
        }
        public static List<Patient> ListeDesPatientsDependants(string employe, string entrep)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE sc LIKE '" + employe +
                    "' AND entrep LIKE '" + entrep + "' ORDER BY nom, prenom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement = !reader.IsDBNull(12) ? reader.GetDateTime(12) : DateTime.Now;
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";

                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
            }
            finally
            { connection.Close(); }
            return listePatient;
        }
        
        //Liste des patients par nom et prenom
        public static List<Patient> ListeDesPatientsParNomOuPrenom(string nomPatient, string prenom)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE nom LIKE '" + nomPatient +
                    "%' AND prenom LIKE '" + prenom + "%' ORDER BY nom, prenom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement = !reader.IsDBNull(12) ? reader.GetDateTime(12) : DateTime.Now;
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";

                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
            }
            finally
            { connection.Close(); }
            return listePatient;
        }

        public static System.Data.DataTable ListeDesPatientsParEntreprise(string matricule)
        {
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE sc LIKE '" + matricule + "' AND entrep LIKE '%HNDA%' ORDER BY nom, prenom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                var dt = new System.Data.DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                //MonMessageBox.ShowBox("Liste des patients", ex);
                return null;
            }
            finally
            { connection.Close(); }
        }

        public static System.Data.DataTable TableDesDetailsFacturesProformaPCP(string matricule, DateTime date1, DateTime date2)
        {
            var dt = new System.Data.DataTable();
            try
            {
                requete = "SELECT pro_facture_tbl.num_patient, pro_facture_tbl.date_fact,pro_det_fact.id_fact," +
                    "pro_det_fact.design,pro_det_fact.prix,pro_det_fact.qte,pro_det_fact.prix_total ,pro_facture_tbl.acte FROM pro_facture_tbl" +
                    " INNER JOIN pro_det_fact ON pro_facture_tbl.id_fact=pro_det_fact.id_fact INNER JOIN patient_tbl ON   pro_facture_tbl.num_patient=patient_tbl.id WHERE  patient_tbl.matricule=@matricule" +
                    " AND pro_facture_tbl.date_fact>=@date1 AND pro_facture_tbl.date_fact<@date2";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("matricule", matricule));
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                //MonMessageBox.ShowBox("Liste analyse", ex);
                return null;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        //liste des patients rechercher apr leur nom
        public static List<Patient> ListeDesPatients(DateTime date1 , DateTime date2)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE date_enre >= @date1 AND date_enre <@date2";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1",date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement = reader.GetDateTime(12);
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
            }
            finally
            { connection.Close(); command.Parameters.Clear(); }
            return listePatient;
        }

        //liste des patients rechercher apr leur nom
        public static List<Patient> ListeDesPatients(string noms, string prenoms)
        {
            var listePatient = new List<Patient>();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE nom = '" + noms + "' AND prenom = '" + prenoms +"'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.NumeroPatient = reader.GetInt32(0);
                    patient.Nom = reader.GetString(1);
                    patient.Prenom = reader.GetString(2);
                    patient.Sexe = reader.GetString(3);
                    patient.An = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    patient.Telephone = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    patient.NomEntreprise = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    patient.SousCouvert = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    patient.Rhesus = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    patient.Adresse = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    patient.NumeroSocial = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    patient.Fonction = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    patient.DateEnregistrement = reader.GetDateTime(12);
                    patient.Alcoolo = !reader.IsDBNull(13) ? reader.GetBoolean(13) : false;
                    patient.Tabagiste = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    patient.Drogueur = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    patient.Couvert = !reader.IsDBNull(16) ? reader.GetBoolean(16) : false;
                    patient.Mois = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    patient.Matricule = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    listePatient.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
            }
            finally
            { connection.Close(); }
            return listePatient;
        }
        public static int ObtenirNumeroPatientEnregistrer()
        {
            try
            {
                requete = "SELECT MAX(id) FROM patient_tbl WHERE idcai = '" + GestionAcademique.LoginFrm.matricule+"'";
                command.CommandText = requete;
                connection.Open();

                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("numero patient", ex);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
       
        public static int ObtenirNumeroPatient()
        {
            try
            {
                requete = "SELECT MAX(id) FROM patient_tbl";
                command.CommandText = requete;
                connection.Open();

                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("numero patient", ex);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
        /*************************** FIN  CODE PATIENT **************************************/

        /**********************************  CODE PARAMETRES **********************************/
        #region ParametresCode
        //ajouter un parametres
        public static bool EnregistrerLesParametres(Parametres param, string etat)
        {
            var flag = false;
            try
            {
                connection.Open();
                if (etat == "1")
                {
                    requete = "INSERT INTO param_tbl (id_pat, `date`, poids, tension,pouls,taille, temp,gly)" +
                              " VALUES(@id_pat, @date, @poids, @tension,@pouls,@taille, @temp,@gly)";
                }
                else if (etat == "2")
                {
                    requete = "UPDATE param_tbl SET poids= @poids, tension=@tension,pouls=@pouls,taille=@taille,gly=@gly , temp=@temp WHERE id = " + param.ID;

                }
                command.Parameters.Add(new MySqlParameter("date", param.Date));
                command.Parameters.Add(new MySqlParameter("gly", param.Glycemie));
                command.Parameters.Add(new MySqlParameter("id_pat", param.Identifiant));
                command.Parameters.Add(new MySqlParameter("poids", param.Poids));
                command.Parameters.Add(new MySqlParameter("tension", param.Tension));
                command.Parameters.Add(new MySqlParameter("pouls", param.Pouls));
                command.Parameters.Add(new MySqlParameter("taille", param.Taille));
                command.Parameters.Add(new MySqlParameter("temp", param.Temperature));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Les paramètres du patient enregistrées avec succés", "affirmation", "affirmation.png");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L' enregistrement du patient a échoué", "Erreur", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }
        //supprimer un patient
        public static bool SupprimerUnParametre(int id)
        {
            var flag = false;
            try
            {
                connection.Open();
                requete = "DELETE FROM param_tbl  WHERE id = " + id;
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de parametres du patient supprimées avec succés", "affirmation", "affirmation.png");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression du patient a échoué", "Erreur", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        // liste des patients
        public static List<Parametres> ListeDesParametres(int idPatient)
        {
            var listeParam = new List<Parametres>();
            try
            {
                requete = "SELECT * FROM param_tbl WHERE id_pat =" + idPatient;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var param = new Parametres();
                    param.ID = reader.GetInt32(0);
                    param.Identifiant = reader.GetInt32(1);
                    param.Date = !reader.IsDBNull(2) ? reader.GetDateTime(2) : DateTime.Now;
                    param.Poids = !reader.IsDBNull(3) ? reader.GetDouble(3) : 0.0;
                    param.Tension = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    param.Pouls = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    param.Taille = !reader.IsDBNull(6) ? reader.GetDouble(6) : 0.0;
                    param.Temperature = !reader.IsDBNull(7) ? reader.GetDouble(7) : 0.0;
                    param.Glycemie = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listeParam.Add(param);
                    
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des patients", ex);
                //throw;
            }
            finally
            {
                connection.Close();
            }
            return listeParam;
        }


        public static int ObtenirNumeroParametres()
        {
            try
            {
                requete = "SELECT MAX(id) FROM param_tbl";
                command.CommandText = requete;
                connection.Open();

                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("numero parametres", ex);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
        /*************************** FIN  CODE PARAMETRES **************************************/

        /*****************************  CODE ANALYSE LISTE **********************************/
        #region AnalyseCode
        public static bool EnregisterLesAnalyses(Analyse analyse, string etat)
        {
            var flag = false;
            try
            {
                connection.Open();
                if (etat == "1")
                {
                    requete = "INSERT INTO liste_analyse_tbl (type_anal, montant,montantNRC,montantAs, descrip,idgrp) " +
                        "VALUES(@type_anal,@montant,@montantNRC,@montantAs,@descrip,@idgrp)";
                }
                else
                {
                    requete = "UPDATE liste_analyse_tbl SET type_anal=@type_anal, montant=@montant,descrip = " +
                     " @descrip , montantNRC = @montantNRC, montantAs =@montantAs,idgrp=@idgrp WHERE num_anal = " + analyse.NumeroListeAnalyse;

                }
                command.Parameters.Add(new MySqlParameter("descrip", analyse.Description));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse.TypeAnalyse));
                command.Parameters.Add(new MySqlParameter("montant", analyse.Frais));
                command.Parameters.Add(new MySqlParameter("montantNRC", analyse.FraisConventionnes));
                command.Parameters.Add(new MySqlParameter("montantAs", analyse.FraisInternes));
                command.Parameters.Add(new MySqlParameter("montantCICR", analyse.FraisCICR));
                command.Parameters.Add(new MySqlParameter("idgrp", analyse.NumeroGroupe));
                //command.Parameters.Add(new MySqlParameter("idcai", GestionAcademique.LoginFrm.matricule));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                flag = true;
                MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l'analyse a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static void SupprimerLesAnalyses(int numeroAnalyse)
        {
            try
            {
                connection.Open();
                requete = "DELETE FROM  liste_analyse_tbl WHERE num_anal = " + numeroAnalyse;
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de type d'analyse supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l'analyse a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Analyse> ListeDesAnalyses()
        {
            var listeAnalyse = new List<Analyse>();
            try
            {
                requete = "SELECT * FROM liste_analyse_tbl ORDER BY type_anal";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var analyse = new Analyse();
                    analyse.NumeroListeAnalyse = reader.GetInt32(0);
                    analyse.TypeAnalyse = reader.GetString(1);
                    analyse.Frais = reader.GetDouble(2);
                    analyse.Description = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    analyse.FraisConventionnes = reader.GetDouble(4);
                    analyse.FraisInternes = !reader.IsDBNull(5) ? reader.GetDouble(5) : 0.0;
                    //analyse.FraisCICR = !reader.IsDBNull(6) ? reader.GetDouble(6) : 0.0;
                    analyse.NumeroGroupe = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
                    listeAnalyse.Add(analyse);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeAnalyse;
        }

        public static List<Analyse> ListeDesGroupes()
        {
            var listeAnalyse = new List<Analyse>();
            try
            {
                requete = "SELECT * FROM grp_tbl ORDER BY description ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var analyse = new Analyse();
                    analyse.NumeroGroupe = reader.GetInt32(0);
                    analyse.Groupe = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    analyse.Libelle = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    listeAnalyse.Add(analyse);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeAnalyse;
        }

        public static List<Analyse> ListeDesGroupes(int idGroupe)
        {
            var listeAnalyse = new List<Analyse>();
            try
            {
                requete = "SELECT * FROM grp_tbl WHERE id =  "+idGroupe;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var analyse = new Analyse();
                    analyse.NumeroGroupe = reader.GetInt32(0);
                    analyse.Groupe = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    analyse.Libelle = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    listeAnalyse.Add(analyse);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeAnalyse;
        }
     
        public static List<Analyse> ListeDesDistinguesGroupes()
        {
            var listeAnalyse = new List<Analyse>();
            try
            {
                requete = "SELECT DISTINCT description FROM grp_tbl ORDER BY description ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var analyse = new Analyse();
                    //analyse.NumeroGroupe = reader.GetInt32(0);
                    analyse.Groupe = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    //analyse.Libelle = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    listeAnalyse.Add(analyse);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeAnalyse;
        }
        public static bool EnregistrerGroupe(Analyse analyse)
        {
            try
            {
                connection.Open();
                if (analyse.NumeroGroupe == 0)
                {
                    requete = "INSERT INTO grp_tbl  (description, sous_rubr) VALUES(@description, @sous_rubr)";
                    command.Parameters.Add(new MySqlParameter("description", analyse.Groupe));
                    command.Parameters.Add(new MySqlParameter("sous_rubr", analyse.Libelle));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    requete = "UPDATE grp_tbl  SET description=@description, sous_rubr=@sous_rubr WHERE id ="+analyse.NumeroGroupe;
                    command.Parameters.Add(new MySqlParameter("description", analyse.Groupe));
                    command.Parameters.Add(new MySqlParameter("sous_rubr", analyse.Libelle));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l'analyse a échoué", "Erreur", ex, "erreur.png"); return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static void SupprimerUnGroupe(int id)
        {
            try
            {
                connection.Open();
                if (MonMessageBox.ShowBox("Voulez vos supprimer ces données?", "Confirmation", "confirmation.pg") == "1")
                {
                    requete = "DELETE FROM grp_tbl WHERE id = " + id;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Données d' analyse suprimées avec succés", "Affirmation", "affirmation.png");
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression d'analyse a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Analyse> ListeDesGroupes(string groupe)
        {
            var listeAnalyse = new List<Analyse>();
            try
            {
                requete = "SELECT * FROM grp_tbl WHERE description=@groupe ";
                command.CommandText = requete;
                connection.Open();
                command.Parameters.Add(new MySqlParameter("groupe", groupe));
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var analyse = new Analyse();
                    analyse.NumeroGroupe = reader.GetInt32(0);
                    analyse.Groupe = reader.GetString(1);
                    analyse.Libelle = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    listeAnalyse.Add(analyse);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listeAnalyse;
        }

        public static List<Analyse> ListeDesAnalyses(int numAnal)
        {
            var listeAnalyse = new List<Analyse>();
            try
            {
                requete = "SELECT * FROM liste_analyse_tbl WHERE num_anal = " + numAnal;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var analyse = new Analyse();
                    analyse.NumeroListeAnalyse = reader.GetInt32(0);
                    analyse.TypeAnalyse = reader.GetString(1);
                    analyse.Frais = reader.GetDouble(2);
                    analyse.Description = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    analyse.FraisConventionnes = reader.GetDouble(4);
                    analyse.FraisInternes = !reader.IsDBNull(5) ? reader.GetDouble(5) : 0.0;
                    //analyse.FraisCICR = !reader.IsDBNull(6) ? reader.GetDouble(6) : 0.0;
                    analyse.NumeroGroupe = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
                    listeAnalyse.Add(analyse);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeAnalyse;
        }
        public static int ListeDesAnalyses(string typeAnalyse)
        {

            try
            {
                requete = "SELECT * FROM liste_analyse_tbl WHERE type_anal LIKE '" + typeAnalyse + "%'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                int numeroAnalyse=0;
                while (reader.Read())
                {
                    numeroAnalyse = reader.GetInt32(0);
                }
                return numeroAnalyse;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
        /***********************************FIN CODE ANALYSE ********************************/

        /*********************************ANALYSE EFFECTUE PAR UN PATIENT *******************/
        #region AnalyseEffectue
        public static bool AjouterLesAnalysesEffectues(Analyse analyse, DataGridView dgvAnal, string patient)
        {
            var flag = false;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "INSERT INTO analyse_tbl (date_anal, num_patient,num_empl,montantTotal,partern,idcai) " +
                    " VALUES(@date_anal,@num_patient,@num_empl,@montantTotal,@partern,@idcai)";
                command.Parameters.Add(new MySqlParameter("date_anal", analyse.DateAnalyse));
                command.Parameters.Add(new MySqlParameter("num_patient", analyse.IdPatient));
                command.Parameters.Add(new MySqlParameter("num_empl", analyse.NumeroEmploye));
                command.Parameters.Add(new MySqlParameter("montantTotal", analyse.MontantTotal));
                command.Parameters.Add(new MySqlParameter("partern", analyse.Libelle));
                command.Parameters.Add(new MySqlParameter("idcai", GestionAcademique.LoginFrm.matricule));

                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                requete = "select MAX(num)  FROM analyse_tbl";
                command.CommandText = requete;
                command.Connection = connection;
                command.Transaction = transaction;
                analyse.NumeroAnalyse = (int)command.ExecuteScalar();

                foreach (DataGridViewRow dgvRow in dgvAnal.Rows)
                {
                    var idAnal = Int32.Parse(dgvRow.Cells[0].Value.ToString());

                    var nbre = Double.Parse(dgvRow.Cells[3].Value.ToString());
                    var frais = Double.Parse(dgvRow.Cells[2].Value.ToString());
                    requete = "INSERT INTO det_anal (id_anal, id_liste_anal,frais,nbre,fact) VALUES(@id_anal1,@id_liste_anal,@frais,@nbre,@fact)";
                    command.Parameters.Add(new MySqlParameter("id_anal1", analyse.NumeroAnalyse));
                    command.Parameters.Add(new MySqlParameter("id_liste_anal", idAnal));
                    command.Parameters.Add(new MySqlParameter("frais", frais));
                    command.Parameters.Add(new MySqlParameter("nbre", nbre));
                    command.Parameters.Add(new MySqlParameter("fact", false));
                    command.Transaction = transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("Données du patient " + patient + " est enregistrée avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("L' enregistrement de l'analyse a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool BondesExamens(Analyse analyse, DataGridView dgvAnal)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();


                if (analyse.NumeroAnalyse == 0)
                {
                    requete = "INSERT INTO analyse_tbl (date_anal, num_patient,num_empl,montantTotal,partern) " +
                   " VALUES(@date_anal,@num_patient,@num_empl,@montantTotal,@partern)";
                    command.Parameters.Add(new MySqlParameter("date_anal", analyse.DateAnalyse));
                    command.Parameters.Add(new MySqlParameter("num_patient", analyse.IdPatient));
                    command.Parameters.Add(new MySqlParameter("num_empl", analyse.NumeroEmploye));
                    command.Parameters.Add(new MySqlParameter("montantTotal", analyse.MontantTotal));
                    command.Parameters.Add(new MySqlParameter("partern", analyse.Libelle));

                    command.Transaction = transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                    requete = "select MAX(num)  FROM analyse_tbl";
                    command.CommandText = requete;
                    command.Connection = connection;
                    command.Transaction = transaction;
                    analyse.NumeroAnalyse = (int)command.ExecuteScalar();

                    foreach (DataGridViewRow dgvRow in dgvAnal.Rows)
                    {
                        var idAnal = Int32.Parse(dgvRow.Cells[0].Value.ToString());

                        var nbre = Double.Parse(dgvRow.Cells[3].Value.ToString());
                        var frais = Double.Parse(dgvRow.Cells[2].Value.ToString());
                        requete = "INSERT INTO det_anal (id_anal, id_liste_anal,frais,nbre,fact) VALUES(@id_anal1,@id_liste_anal,@frais,@nbre,@fact)";
                        command.Parameters.Add(new MySqlParameter("id_anal1", analyse.NumeroAnalyse));
                        command.Parameters.Add(new MySqlParameter("id_liste_anal", idAnal));
                        command.Parameters.Add(new MySqlParameter("frais", frais));
                        command.Parameters.Add(new MySqlParameter("nbre", nbre));
                        command.Parameters.Add(new MySqlParameter("fact", false));
                        command.Transaction = transaction;
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                requete = "SELECT * FROM pro_facture_tbl WHERE num_acte = " + analyse.NumeroAnalyse + " AND acte ='EXAMEN'";
                command.CommandText = requete;
                command.Transaction = transaction;
                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = "INSERT INTO pro_facture_tbl (date_fact, montant_fact,num_patient,num_empl,num_acte,acte) " +
                       " VALUES(@date_fact," + analyse.MontantTotal + "," + analyse.IdPatient + ",'" +
                       analyse.NumeroEmploye + "'," + analyse.NumeroAnalyse + ", 'EXAMEN')";
                    command.Parameters.Add(new MySqlParameter("date_fact", analyse.DateAnalyse));
                    command.CommandText = requete;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    foreach (DataGridViewRow dgvRow in dgvAnal.Rows)
                    {
                        var idAnal = Int32.Parse(dgvRow.Cells[0].Value.ToString());
                        var typeAnalyse = dgvRow.Cells[1].Value.ToString();
                        var nbre = Double.Parse(dgvRow.Cells[3].Value.ToString());
                        var frais = Double.Parse(dgvRow.Cells[2].Value.ToString());
                        var groupage = dgvRow.Cells[5].Value.ToString();
                        requete = "select MAX(id_fact)  FROM pro_facture_tbl";
                        command.CommandText = requete;
                        command.Transaction = transaction;
                        var numeroFacture = (int)command.ExecuteScalar();

                        requete = "INSERT INTO pro_det_fact (id_fact, design,prix,qte,prix_total,groupage) VALUES(@id_fact1,@design,@prix,@qte,@prix_total,@groupage)";
                        command.Parameters.Add(new MySqlParameter("id_fact1", numeroFacture));
                        command.Parameters.Add(new MySqlParameter("design", typeAnalyse));
                        command.Parameters.Add(new MySqlParameter("qte", nbre));
                        command.Parameters.Add(new MySqlParameter("prix", frais));
                        command.Parameters.Add(new MySqlParameter("prix_total", frais * nbre));
                        command.Parameters.Add(new MySqlParameter("groupage", groupage));
                        command.CommandText = requete;
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaction.Commit();
                }
                reader.Close();
                return true;
            }
            catch(Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                MonMessageBox.ShowBox("", ex);
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
        public static bool SiFactureEnBon(string typeBon, int numero)
        {
            try
            {
                connection.Open();
                requete = "SELECT * FROM pro_facture_tbl WHERE num_acte = " + numero + " AND acte ='" + typeBon + "'";
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static bool ModifierLesAnalysesEffectues(Analyse analyse, DataGridView dgView)
        {
            var flag = false;
            try
            {
                //connection.Open();
                //transaction = connection.BeginTransaction();

                //requete = "UPDATE analyse_tbl SET date_anal = @date_anal , num_empl ="+
                //    analyse.NumeroEmploye +", montantTotal = "+ analyse.MontantTotal+", num_patient=@partern WHERE num = "
                //    + analyse.NumeroAnalyse;
                //command.Parameters.Add(new MySqlParameter("date_anal", analyse.DateAnalyse));
                //command.Parameters.Add(new MySqlParameter("partern", analyse.nu));
                //command.Transaction = transaction;
                //command.CommandText = requete;
                //command.ExecuteNonQuery();

                //foreach (DataGridViewRow dgrRow in dgView.Rows)
                //{
                //var idListeAnalyse = Int32.Parse(dgrRow.Cells[0].Value.ToString());
                //var resultat = dgrRow.Cells[3].Value.ToString();
                //var frais = Double.Parse(dgrRow.Cells[2].Value.ToString());
                //requete = "UPDATE det_anal SET resultat =@resultat, frais=@frais WHERE id_anal = @id_anal AND id_liste_anal = @id_liste_anal ";
                //command.Parameters.Add(new MySqlParameter("id_anal", analyse.NumeroAnalyse));
                //command.Parameters.Add(new MySqlParameter("id_liste_anal", idListeAnalyse));
                //command.Parameters.Add(new MySqlParameter("resultat", resultat));
                //command.Parameters.Add(new MySqlParameter("frais", frais));
                //command.CommandText = requete;
                //command.Transaction = transaction;
                //command.ExecuteNonQuery();                    
                //command.Parameters.Clear();
                //}

                //transaction.Commit();
                //MonMessageBox.ShowBox("L'analyse du patient  est enregistrée avec succés", "Affirmation", "affirmation.png");
                flag = true;
            }
            catch (Exception ex)
            {
                //if (transaction != null)
                //{
                //    transaction.Rollback();
                //}
                MonMessageBox.ShowBox("L' enregistrement de l'analyse a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static void SupprimerUneAnalyseFaite(int numeroAnal, int numeroListe, double montantTotal, string designation)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "DELETE FROM det_anal WHERE id_anal = " + numeroAnal + " AND id_liste_anal = " + numeroListe;
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                requete = "UPDATE analyse_tbl SET montantTotal = " + montantTotal + " WHERE num = " + numeroAnal;
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                ////requete = "SELECT id_fact FROM pro_facture_tbl WHERE num_acte = " + numeroAnal + " AND acte ='EXAMEN'";
                ////command.CommandText = requete;
                ////command.Transaction = transaction;
                ////var reader = command.ExecuteReader();
                //var dt = new DataTable();
                //dt.Load(reader);

                //if (dt.Rows.Count > 0)
                //{
                //    var id = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
                //    requete = "DELETE FROM pro_det_fact WHERE id_fact = " + id + " AND design =@design ";
                //    command.CommandText = requete;
                //    command.Parameters.Add(new MySqlParameter("design", designation));
                //    command.Transaction = transaction;
                //    command.ExecuteNonQuery();

                //    requete = "UPDATE pro_facture_tbl SET montant_fact = " + montantTotal + " WHERE id_fact = " + id;
                //    command.CommandText = requete;
                //    command.Transaction = transaction;
                //    command.ExecuteNonQuery();
                //}
                transaction.Commit();
                MonMessageBox.ShowBox("Données d' analyse retirées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("La suppression d'analyse a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static void SupprimerUneAnalyseFaite(int numeroAnal)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "DELETE FROM analyse_tbl WHERE num = " + numeroAnal;
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                //requete = "DELETE FROM pro_facture_tbl WHERE num_acte = " + numeroAnal + " AND acte ='EXAMEN'";
                //command.CommandText = requete;
                //command.Transaction = transaction;
                //command.ExecuteNonQuery();

                transaction.Commit();

                MonMessageBox.ShowBox("Données d' analyse suprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();
                MonMessageBox.ShowBox("La suppression d'analyse a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static int ObtenirDernierNumeroAnalyse(string numEmpl)
        {
            try
            {
                connection.Open();
                requete = "select MAX(num)  FROM analyse_tbl WHERE idcai = '" + numEmpl + "'";
                command.CommandText = requete;
                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur obtenir numero analyse", ex);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Analyse> ListeDesAnalysesEffectuees()
        {
            var listeAnalyse = new List<Analyse>();
            try
            {
                requete = "SELECT * FROM analyse_tbl";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var analyse = new Analyse();
                    analyse.NumeroAnalyse = reader.GetInt32(0);
                    analyse.DateAnalyse = reader.GetDateTime(1);
                    analyse.IdPatient = reader.GetInt32(2);
                    analyse.NumeroEmploye = reader.GetString(3);
                    analyse.MontantTotal = reader.GetDouble(4);
                    analyse.Libelle = reader.IsDBNull(5) ? reader.GetString(5) : "";
                    listeAnalyse.Add(analyse);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeAnalyse;
        }

        public static DataTable TableDesAnalysesEffectues()
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT analyse_tbl.num, analyse_tbl.date_anal, patient_tbl.nom, patient_tbl.prenom, " +
                    " empl_tbl.nom_empl, analyse_tbl.montantTotal,analyse_tbl.num_patient,analyse_tbl.partern  FROM  analyse_tbl INNER JOIN empl_tbl ON analyse_tbl.num_empl" +
                "= empl_tbl.num_empl INNER JOIN patient_tbl ON analyse_tbl.num_patient = patient_tbl.id ORDER BY analyse_tbl.num DESC";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static DataTable TableDesAnalysesEffectuesDuJour()
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT analyse_tbl.num, analyse_tbl.date_anal, patient_tbl.nom, patient_tbl.prenom, " +
                    " empl_tbl.nom_empl, analyse_tbl.montantTotal,analyse_tbl.num_patient,analyse_tbl.partern  FROM  analyse_tbl INNER JOIN empl_tbl ON analyse_tbl.num_empl" +
                "= empl_tbl.num_empl INNER JOIN patient_tbl ON analyse_tbl.num_patient = patient_tbl.id  WHERE analyse_tbl.date_anal>=@date2 ORDER BY analyse_tbl.num DESC";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date2", DateTime.Now.Date.AddDays(-10))); ;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable TableDesAnalysesVerif()
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT analyse_tbl.num, analyse_tbl.date_anal, patient_tbl.nom, patient_tbl.prenom, " +
                    " empl_tbl.nom_empl, analyse_tbl.montantTotal,analyse_tbl.num_patient,analyse_tbl.partern  FROM  analyse_tbl INNER JOIN empl_tbl ON analyse_tbl.num_empl" +
                "= empl_tbl.num_empl INNER JOIN patient_tbl ON analyse_tbl.num_patient = patient_tbl.id  WHERE analyse_tbl.date_anal>=@date2 AND analyse_tbl.date_anal<@date3 ORDER BY analyse_tbl.num DESC";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date2", DateTime.Parse("2018-02-27 17:20:00")));
                command.Parameters.Add(new MySqlParameter("date3", DateTime.Parse("2018-02-28 07:42:32")));
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable TableDesAnalysesEffectues(string nom)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT analyse_tbl.num, analyse_tbl.date_anal, patient_tbl.nom, patient_tbl.prenom, " +
                    " empl_tbl.nom_empl, analyse_tbl.montantTotal,analyse_tbl.num_patient,analyse_tbl.partern  FROM  analyse_tbl INNER JOIN empl_tbl ON analyse_tbl.num_empl" +
                "= empl_tbl.num_empl INNER JOIN patient_tbl ON analyse_tbl.num_patient = patient_tbl.id WHERE patient_tbl.nom LIKE '" +
                nom + "%' ORDER BY analyse_tbl.num DESC";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("nom", nom));
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable TableDesAnalysesEffectuesParIdPatient(int idPatient)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT analyse_tbl.num, analyse_tbl.date_anal, patient_tbl.nom, patient_tbl.prenom, " +
                    " empl_tbl.nom_empl, analyse_tbl.montantTotal,analyse_tbl.num_patient,analyse_tbl.partern  FROM  analyse_tbl INNER JOIN empl_tbl ON analyse_tbl.num_empl" +
                "= empl_tbl.num_empl INNER JOIN patient_tbl ON analyse_tbl.num_patient = patient_tbl.id WHERE analyse_tbl.num_patient = " +
                idPatient + " ORDER BY analyse_tbl.num DESC";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable TableDesDetailsFacturesProforma(int idPatient, DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT pro_facture_tbl.num_patient, pro_facture_tbl.date_fact,pro_det_fact.id_fact," +
                    "pro_det_fact.design,pro_det_fact.prix,pro_det_fact.qte,pro_det_fact.prix_total ,pro_facture_tbl.acte FROM pro_facture_tbl" +
                    " INNER JOIN pro_det_fact ON pro_facture_tbl.id_fact=pro_det_fact.id_fact " +
                    " WHERE pro_facture_tbl.date_fact>=@date1 AND pro_facture_tbl.date_fact <@date2 AND pro_facture_tbl.num_patient=" + idPatient;
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static double TotalProformaDuPatient(int idPatient, DateTime date1, DateTime date2)
        {
            try
            {
                requete = "SELECT SUM(montant_fact) FROM pro_facture_tbl  WHERE date_fact>=@date1 AND date_fact <@date2 AND  num_patient=" + idPatient;
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                connection.Open();
                return (double) command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
                return 0;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }
     
        public static List<Facture> TableDesDetailsFacturesProforma()
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT pro_facture_tbl.*, empl_tbl.*, patient_tbl.*  FROM pro_facture_tbl   INNER JOIN  patient_tbl ON pro_facture_tbl.num_patient=patient_tbl.id " +
                    " INNER JOIN empl_tbl ON pro_facture_tbl.num_empl = empl_tbl.num_empl ";
                    //" WHERE pro_facture_tbl.date_fact>=@date1 AND pro_facture_tbl.date_fact <@date2 AND patient_tbl.entrep LIKE '%" + entreprise + "%'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
while (reader.Read())
{
    var facture = new Facture();
    facture.NumeroFacture = reader.GetInt32(0);
    facture.DateFacture = reader.GetDateTime(1);
    facture.MontantFactural = reader.GetDouble(2);
    facture.IdPatient = reader.GetInt32(3);
    facture.Patient = reader.GetString(17) + " " + reader.GetString(18);
    facture.NumeroEmploye = reader.GetString(4);
    facture.NomEmploye = reader.GetString(8);
    facture.NumeroActe = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
    facture.Sub = !reader.IsDBNull(6) ? reader.GetString(6) : null;
    facture.Entreprise = !reader.IsDBNull(22) ? reader.GetString(22) : null;
    listefacture.Add(facture);
}
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listefacture;
        }

        public static List<Facture> TableDesDetailsFacturesProforma(int numFacture)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT * FROM pro_det_fact WHERE id_fact ="+numFacture;
                //" WHERE pro_facture_tbl.date_fact>=@date1 AND pro_facture_tbl.date_fact <@date2 AND patient_tbl.entrep LIKE '%" + entreprise + "%'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.Designation = reader.GetString(1);
                    facture.Quantite = reader.GetInt32(2);
                    facture.Prix = reader.GetDouble(3);
                    facture.PrixTotal = reader.GetDouble(4) ;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listefacture;
        }

        public static int CountActes(int idPatient, DateTime dt1, DateTime dt2,string groupage)
        {
            try
            {
                connection.Open();
                var
                requete = "SELECT COUNT( distinct det_fact.id_fact) FROM facture_tbl INNER JOIN " +
                " det_fact ON facture_tbl.id_fact=det_fact.id_fact WHERE facture_tbl.num_patient = " + idPatient +
                " AND facture_tbl.date_fact >=@dt1 AND facture_tbl.date_fact < @dt2 AND det_fact.groupage LIKE '%" + groupage + "%'";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("groupage", groupage));
                command.Parameters.Add(new MySqlParameter("dt1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("dt2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static int CountDetailsActes(int idPatient,string  groupage, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                requete = "select count(det_fact.design) FROM det_fact INNER JOIN facture_tbl ON det_fact.id_fact =facture_tbl.id_fact WHERE" +
                    "  groupage LIKE '%"+groupage+"%' AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 AND facture_tbl.num_patient=" + idPatient;
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("date2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static int CountDetailsActes(string designation, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                requete = "select count(det_fact.design) FROM det_fact INNER JOIN facture_tbl ON det_fact.id_fact =facture_tbl.id_fact WHERE" +
                    "  det_fact.design LIKE '%" + designation + "%' AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("date2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static int RangDetailsActes(string designation, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                requete = "select count(det_fact.design) FROM det_fact INNER JOIN facture_tbl ON det_fact.id_fact =facture_tbl.id_fact WHERE" +
                    "  det_fact.design LIKE '%" + designation + "%' AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact <= @date2 ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("date2", dt2));
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static int CountDetailsActesDansConventionnes(string designation, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                requete = "select count(pro_det_fact.design) FROM pro_det_fact INNER JOIN pro_facture_tbl ON pro_det_fact.id_fact =pro_facture_tbl.id_fact WHERE" +
                    "  pro_det_fact.design LIKE '%" + designation + "%' AND pro_facture_tbl.date_fact >= @date1 AND pro_facture_tbl.date_fact < @date2 ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("date2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static int RangDetailsActesDansConventionnes(string designation, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                requete = "select count(pro_det_fact.design) FROM pro_det_fact INNER JOIN pro_facture_tbl ON pro_det_fact.id_fact =pro_facture_tbl.id_fact WHERE" +
                    "  pro_det_fact.design LIKE '%" + designation + "%' AND pro_facture_tbl.date_fact >= @date1 AND pro_facture_tbl.date_fact <= @date2 ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("date2", dt2));
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static List<Facture> ListeConsultationsDansFacture(string sexe, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                //requete = "select det_fact.design,SUM( det_fact.qte) FROM det_fact INNER JOIN facture_tbl ON det_fact.id_fact =facture_tbl.id_fact WHERE" +
                //    "  det_fact.groupage LIKE '%" + groupage + "%' AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2 AND facture_tbl.num_patient=" + idPatient + " GROUP BY  det_fact.design";
                 requete = "select det_fact.design ,SUM(det_fact.qte) FROM det_fact INNER JOIN facture_tbl ON det_fact.id_fact =facture_tbl.id_fact" +
                        " INNER JOIN patient_tbl ON facture_tbl.num_patient = patient_tbl.id WHERE patient_tbl.sexe = '"+sexe+
                    "' AND det_fact.groupage LIKE '%CONSULTATION%' AND facture_tbl.date_fact >= @date1 "+
                    "AND facture_tbl.date_fact < @date2 GROUP BY det_fact.design";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("date2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                var liste = new List<Facture>();
                while (reader.Read())
                {
                    var f = new Facture();
                    f.Quantite = !reader.IsDBNull(1) ? reader.GetInt32(1) : 1;
                    f.Designation =!reader.IsDBNull(0) ? reader.GetString(0) : "";
                    liste.Add(f);
                }
                return liste;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
        public static int CountActesDesConventionnes(int idPatient, DateTime dt1, DateTime dt2, string groupage)
        {
            try
            {
                connection.Open();
                var
                requete = "SELECT COUNT( distinct pro_det_fact.id_fact) FROM pro_facture_tbl INNER JOIN " +
                " pro_det_fact ON pro_facture_tbl.id_fact=pro_det_fact.id_fact WHERE pro_facture_tbl.num_patient = " + idPatient +
                " AND pro_facture_tbl.date_fact >=@dt1 AND pro_facture_tbl.date_fact < @dt2 AND pro_det_fact.groupage LIKE '%" + groupage + "%'";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("groupage", groupage));
                command.Parameters.Add(new MySqlParameter("dt1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("dt2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static int CountDetailsActesDesConventionnes(int idPatient, string groupage, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                requete = "select count(pro_det_fact.design) FROM pro_det_fact INNER JOIN pro_facture_tbl ON pro_det_fact.id_fact =pro_facture_tbl.id_fact WHERE" +
                    "  groupage LIKE '%" + groupage + "%' AND pro_facture_tbl.date_fact >= @date1 AND pro_facture_tbl.date_fact < @date2 AND pro_facture_tbl.num_patient=" + idPatient;
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("date2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static List<Facture> ListeConsultationsDansFactureDesConventionnes(int idPatient, string groupage, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                requete = "select pro_det_fact.design, SUM(pro_det_fact.qte) FROM pro_det_fact INNER JOIN pro_facture_tbl ON pro_det_fact.id_fact =pro_facture_tbl.id_fact WHERE" +
                    "  pro_det_fact.groupage LIKE '%" + groupage + "%' AND pro_facture_tbl.date_fact >= @date1 AND pro_facture_tbl.date_fact < @date2 AND pro_facture_tbl.num_patient=" + idPatient + " GROUP BY pro_det_fact.design";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("date2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                var liste = new List<Facture>();
                while (reader.Read())
                {
                    var f = new Facture();
                    f.Quantite = !reader.IsDBNull(1) ? reader.GetInt32(1) : 1;
                    f.Designation = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    liste.Add(f);
                }
                return liste;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static DataTable TableDesAnalysesEffectues(int id)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT analyse_tbl.num, analyse_tbl.date_anal, patient_tbl.nom, patient_tbl.prenom, " +
                    " empl_tbl.nom_empl, analyse_tbl.montantTotal,analyse_tbl.partern FROM  analyse_tbl INNER JOIN empl_tbl ON analyse_tbl.num_empl" +
                "= empl_tbl.num_empl INNER JOIN patient_tbl ON analyse_tbl.num_patient = patient_tbl.id " +
                " WHERE analyse_tbl.num = " + id;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static List<Analyse> DetailsDesAnalyseEffectues(int numAnal)
        {
            var listeAnalyse = new List<Analyse>();
            try
            {
                requete = "SELECT * FROM det_anal WHERE id_anal = " + numAnal;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var analyse = new Analyse();
                    analyse.NumeroAnalyse = reader.GetInt32(0);
                    analyse.NumeroListeAnalyse = reader.GetInt32(1);
                    analyse.Frais = reader.GetDouble(3);
                    analyse.NombreAnalyse = !reader.IsDBNull(4) ? reader.GetInt32(4) : 1;
                    listeAnalyse.Add(analyse);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeAnalyse;
        }

        public static List<Consultation>  CountConsultation(string entreprise,int idPatient, DateTime dt1, DateTime dt2)
        {
            try
            {
                connection.Open();
                var
                requete = "SELECT patient_tbl.sexe ,COUNT(*) FROM consult_tbl INNER JOIN patient_tbl ON consult_tbl.num_pat=patient_tbl.id WHERE consult_tbl.special != '' AND patient_tbl.id=" + idPatient+
                    " AND consult_tbl.date_consult >= @dt1 AND consult_tbl.date_consult < @dt2 AND patient_tbl.entrep = @entrep  GROUP BY patient_tbl.entrep ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("entrep", entreprise));
                command.Parameters.Add(new MySqlParameter("dt1", dt1.Date));
                command.Parameters.Add(new MySqlParameter("dt2", dt2.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                var list = new List<Consultation>();
                while(reader.Read())
                {
                    var cns = new Consultation();
                    cns.Description = reader.GetString(0);
                    cns.NumeroConsultation = reader.GetInt32(1);
                    list.Add(cns);
                }
                return list;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur  ", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static DataTable DetailsDesAnalyseEffectuesChiffresParMedecin(string medecin, string analyse, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT det_anal.id_anal, analyse_tbl.date_anal, liste_analyse_tbl.type_anal, " +
                    " det_anal.frais, SUM(det_anal.nbre) FROM analyse_tbl INNER JOIN det_anal ON analyse_tbl.num =" +
                    "det_anal.id_anal INNER JOIN liste_analyse_tbl ON det_anal.id_liste_anal = liste_analyse_tbl.num_anal " +
                          " INNER JOIN empl_tbl ON analyse_tbl.num_empl=empl_tbl.num_empl  WHERE analyse_tbl.date_anal >= @date1" +
                          " AND analyse_tbl.date_anal < @date2 AND liste_analyse_tbl.type_anal=@type_anal AND empl_tbl.nom_empl LIKE '%" + medecin +
                    "%' GROUP BY liste_analyse_tbl.type_anal, det_anal.frais ORDER BY liste_analyse_tbl.type_anal";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }

        public static DataTable DetailsDeAnalyseEffectuesParMedecin(string medecin, string analyse, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT liste_analyse_tbl.type_anal, " +
                          " SUM(det_anal.nbre) FROM analyse_tbl INNER JOIN det_anal ON analyse_tbl.num =" +
                          "det_anal.id_anal INNER JOIN liste_analyse_tbl ON det_anal.id_liste_anal = liste_analyse_tbl.num_anal " +
                          " INNER JOIN empl_tbl ON analyse_tbl.num_empl=empl_tbl.num_empl WHERE analyse_tbl.date_anal >= @date1" +
                          " AND analyse_tbl.date_anal < @date2 AND liste_analyse_tbl.type_anal=@type_anal AND empl_tbl.nom_empl LIKE '%" + medecin +
                          "%' GROUP BY liste_analyse_tbl.type_anal ORDER BY liste_analyse_tbl.type_anal";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }


        public static DataTable DetailsDesAnalyseEffectuesChiffresTousLesConventionnes(string analyse, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT det_anal.id_anal, analyse_tbl.date_anal, liste_analyse_tbl.type_anal, " +
                    " det_anal.frais, SUM(det_anal.nbre) FROM analyse_tbl INNER JOIN det_anal ON analyse_tbl.num =" +
                    "det_anal.id_anal INNER JOIN liste_analyse_tbl ON det_anal.id_liste_anal = liste_analyse_tbl.num_anal " +
                          " INNER JOIN patient_tbl ON analyse_tbl.num_patient=patient_tbl.id  WHERE analyse_tbl.date_anal >= @date1" +
                          " AND analyse_tbl.date_anal < @date2 AND liste_analyse_tbl.type_anal=@type_anal AND patient_tbl.entrep NOT LIKE '" +
                    "' GROUP BY liste_analyse_tbl.type_anal, det_anal.frais ORDER BY liste_analyse_tbl.type_anal";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }

        public static DataTable DetailsDeAnalyseEffectuesTousLesConventionnes(string analyse, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT liste_analyse_tbl.type_anal, " +
                          " SUM(det_anal.nbre) FROM analyse_tbl INNER JOIN det_anal ON analyse_tbl.num =" +
                          "det_anal.id_anal INNER JOIN liste_analyse_tbl ON det_anal.id_liste_anal = liste_analyse_tbl.num_anal " +
                          " INNER JOIN patient_tbl ON analyse_tbl.num_patient=patient_tbl.id WHERE analyse_tbl.date_anal >= @date1" +
                          " AND analyse_tbl.date_anal < @date2 AND liste_analyse_tbl.type_anal=@type_anal AND patient_tbl.entrep NOT LIKE '" +
                          "' GROUP BY liste_analyse_tbl.type_anal ORDER BY liste_analyse_tbl.type_anal";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }

        public static DataTable DetailsDesAnalyseEffectuesChiffresParCOnvention(string convetion, string analyse, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT det_anal.id_anal, analyse_tbl.date_anal, liste_analyse_tbl.type_anal, " +
                    " det_anal.frais, SUM(det_anal.nbre) FROM analyse_tbl INNER JOIN det_anal ON analyse_tbl.num =" +
                    "det_anal.id_anal INNER JOIN liste_analyse_tbl ON det_anal.id_liste_anal = liste_analyse_tbl.num_anal " +
                          " INNER JOIN patient_tbl ON analyse_tbl.num_patient=patient_tbl.id  WHERE analyse_tbl.date_anal >= @date1" +
                          " AND analyse_tbl.date_anal < @date2 AND liste_analyse_tbl.type_anal=@type_anal AND patient_tbl.entrep LIKE '%" + convetion +
                    "%' GROUP BY liste_analyse_tbl.type_anal, det_anal.frais ORDER BY liste_analyse_tbl.type_anal";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }

        public static DataTable DetailsDeAnalyseEffectuesParConvention(string convention, string analyse, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT liste_analyse_tbl.type_anal, " +
                          " SUM(det_anal.nbre) FROM analyse_tbl INNER JOIN det_anal ON analyse_tbl.num =" +
                          "det_anal.id_anal INNER JOIN liste_analyse_tbl ON det_anal.id_liste_anal = liste_analyse_tbl.num_anal " +
                          " INNER JOIN patient_tbl ON analyse_tbl.num_patient=patient_tbl.id WHERE analyse_tbl.date_anal >= @date1" +
                          " AND analyse_tbl.date_anal < @date2 AND liste_analyse_tbl.type_anal=@type_anal AND patient_tbl.entrep LIKE '%" + convention +
                          "%' GROUP BY liste_analyse_tbl.type_anal ORDER BY liste_analyse_tbl.type_anal";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }

        public static DataTable DetailsDesAnalyseEffectuesChiffresDesParticuliers(string analyse, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT det_anal.id_anal, analyse_tbl.date_anal, liste_analyse_tbl.type_anal, " +
                    " det_anal.frais, SUM(det_anal.nbre) FROM analyse_tbl INNER JOIN det_anal ON analyse_tbl.num =" +
                    "det_anal.id_anal INNER JOIN liste_analyse_tbl ON det_anal.id_liste_anal = liste_analyse_tbl.num_anal " +
                          " INNER JOIN patient_tbl ON analyse_tbl.num_patient=patient_tbl.id  WHERE analyse_tbl.date_anal >= @date1" +
                          " AND analyse_tbl.date_anal < @date2 AND liste_analyse_tbl.type_anal=@type_anal AND patient_tbl.entrep = '" +
                    "' GROUP BY liste_analyse_tbl.type_anal, det_anal.frais ORDER BY liste_analyse_tbl.type_anal";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }

        public static DataTable DetailsDeAnalyseEffectuesDesParticuliers(string analyse, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT liste_analyse_tbl.type_anal, " +
                          " SUM(det_anal.nbre) FROM analyse_tbl INNER JOIN det_anal ON analyse_tbl.num =" +
                          "det_anal.id_anal INNER JOIN liste_analyse_tbl ON det_anal.id_liste_anal = liste_analyse_tbl.num_anal " +
                          " INNER JOIN patient_tbl ON analyse_tbl.num_patient=patient_tbl.id WHERE analyse_tbl.date_anal >= @date1" +
                          " AND analyse_tbl.date_anal < @date2 AND liste_analyse_tbl.type_anal=@type_anal AND patient_tbl.entrep  ='" +
                          "' GROUP BY liste_analyse_tbl.type_anal ORDER BY liste_analyse_tbl.type_anal";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("type_anal", analyse));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }

        #endregion

        /*********************************FIN ANALYSE EFFECTUE PAR UN PATIENT ***************/

        /****************************** CODE CONSULTATION ***********************************/
        #region Consultation

        public static int ObtenirDerniereConsultation(string numEmple)
        {
            try
            {
                connection.Open();
                requete = "select MAX(num_consult)  FROM consult_tbl WHERE idcai = '" + numEmple + "'";
                command.CommandText = requete;
                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur obtenir numero analyse", ex);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        
        public static bool Verification(int idPatient, DateTime dateCons, string type)
        {
            try
            {
                connection.Open();
                requete = "SELECT det_fact.* FROM  det_fact INNER JOIN facture_tbl ON det_fact.id_fact=facture_tbl.id_fact WHERE facture_tbl.date_fact >= @date   AND facture_tbl.num_patient = @idPat AND det_fact.design = @type  ";
                DateTime date = dateCons.AddDays(-15);

                command.Parameters.Add(new MySqlParameter("idPat", idPatient));
                command.Parameters.Add(new MySqlParameter("date", date.Date));
                command.Parameters.Add(new MySqlParameter("type", type));

                command.CommandText = requete;
                var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                if (dt.Rows.Count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch
            { return false; }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static bool EnregistrerUneConsultation(Consultation consultation)
        {
            try
            {
                connection.Open();

                requete = "select * FROM consult_tbl WHERE num_consult = " + consultation.NumeroConsultation;
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = "INSERT INTO consult_tbl ( type_consul,date_consult,rv,descrip,num_empl,num_pat,frais,special,partern,fact,idcai) " +
                        "VALUES(@type_consult,@date_consult,@rv,@descrip,@num_empl,@num_pat,@frais,@special,@partern,@fact,@idcai)";

                }
                else
                {
                    reader.Close();
                    requete = "UPDATE consult_tbl SET type_consul =@type_consult,date_consult=@date_consult,rv=@rv,descrip=@descrip" +
                        ",num_empl = @num_empl ,num_pat =@num_pat , frais = @frais , special =@special,partern=@partern,idcai=@idcai WHERE num_consult =  " + consultation.NumeroConsultation;
                }

                command.Parameters.Add(new MySqlParameter("type_consult", consultation.TypeConsultation));
                command.Parameters.Add(new MySqlParameter("date_consult", consultation.DateConsultation));
                command.Parameters.Add(new MySqlParameter("rv", consultation.RV));
                command.Parameters.Add(new MySqlParameter("descrip", consultation.Description));
                command.Parameters.Add(new MySqlParameter("num_empl", consultation.NumeroEmploye));
                command.Parameters.Add(new MySqlParameter("num_pat", consultation.IdPatient));
                command.Parameters.Add(new MySqlParameter("frais", consultation.Frais));
                command.Parameters.Add(new MySqlParameter("special", consultation.Specialite));
                command.Parameters.Add(new MySqlParameter("partern", consultation.Partenaire));
                command.Parameters.Add(new MySqlParameter("fact", false));
                command.Parameters.Add(new MySqlParameter("idcai", GestionAcademique.LoginFrm.matricule));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Les données de consultation sont enregistrées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de la consultation a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static bool BondeConsultation(Consultation consultation)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                requete = "SELECT * FROM pro_facture_tbl WHERE num_acte = " + consultation.NumeroConsultation + " AND acte ='CONSULTATION'";
                command.CommandText = requete;
                command.Transaction = transaction;
                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = "INSERT INTO pro_facture_tbl (date_fact, montant_fact,num_patient,num_empl,num_acte,acte) " +
                       " VALUES(@date_fact," + consultation.Frais + "," + consultation.IdPatient + ",'" +
                       consultation.NumeroEmploye + "'," + consultation.NumeroConsultation + ", 'CONSULTATION')";
                    command.Parameters.Add(new MySqlParameter("date_fact", consultation.DateConsultation));
                    command.CommandText = requete;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    requete = "select MAX(id_fact)  FROM pro_facture_tbl";
                    command.CommandText = requete;
                    command.Transaction = transaction;
                    var numeroFacture = (int)command.ExecuteScalar();
                    if (!string.IsNullOrEmpty(consultation.Specialite))
                    {
                        consultation.TypeConsultation = "CONSULTATION EN " + consultation.Specialite;
                    }
                    requete = "INSERT INTO pro_det_fact (id_fact, design,prix,qte,prix_total,groupage) VALUES(@id_fact1,@design,@prix,@qte,@prix_total,@groupage)";
                    command.Parameters.Add(new MySqlParameter("id_fact1", numeroFacture));
                    command.Parameters.Add(new MySqlParameter("design", consultation.TypeConsultation.ToUpper()));
                    command.Parameters.Add(new MySqlParameter("qte", 1));
                    command.Parameters.Add(new MySqlParameter("prix", consultation.Frais));
                    command.Parameters.Add(new MySqlParameter("prix_total", consultation.Frais));
                    command.Parameters.Add(new MySqlParameter("groupage", "Consultation"));
                    command.CommandText = requete;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                reader.Close();
                return true;
            }
            catch
            {
                if (transaction != null)
                    transaction.Rollback();

                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static void SupprimerUneConsultaion(int numeroConsult)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "DELETE FROM consult_tbl WHERE num_consult = " + numeroConsult;
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                //requete = "DELETE FROM pro_facture_tbl WHERE num_acte = " + numeroConsult + " AND acte ='CONSULTATION'";
                //command.CommandText = requete;
                //command.Transaction = transaction;
                //command.ExecuteNonQuery();
                transaction.Commit();
                MonMessageBox.ShowBox("Données de consultation supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("La suppression de la consultation a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Consultation> ListeDesConsultations()
        {
            var listeConsultation = new List<Consultation>();
            try
            {
                requete = "SELECT consult_tbl.num_consult, consult_tbl.type_consul, consult_tbl.date_consult, " +
                    "consult_tbl.rv, consult_tbl.descrip, consult_tbl.num_empl, consult_tbl.num_pat, empl_tbl.nom_empl, " +
                    "patient_tbl.nom, patient_tbl.prenom, consult_tbl.frais,consult_tbl.special, consult_tbl.partern FROM consult_tbl INNER JOIN empl_tbl ON consult_tbl.num_empl " +
                    "= empl_tbl.num_empl INNER JOIN patient_tbl ON consult_tbl.num_pat = patient_tbl.id   ORDER BY consult_tbl.num_consult DESC LIMIT  60";
                command.CommandText = requete;
                //command.Parameters.Add(new MySqlParameter("date", DateTime.Now.Date.AddDays(-15)));WHERE consult_tbl.date_consult>=@date
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var consultation = new Consultation();
                    consultation.NumeroConsultation = reader.GetInt32(0);
                    consultation.TypeConsultation = reader.GetString(1);
                    consultation.DateConsultation = reader.GetDateTime(2);
                    consultation.RV = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    consultation.Description = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    consultation.NumeroEmploye = reader.GetString(5);
                    consultation.IdPatient = reader.GetInt32(6);
                    consultation.NomEmploye = reader.GetString(7);
                    consultation.NomPatient = reader.GetString(8) + " " + reader.GetString(9);
                    consultation.Frais = reader.GetDouble(10);
                    consultation.Specialite = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    consultation.Partenaire = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    //var consultation = new Consultation(numConsult, typeConsult, dateConsult, 
                    //    rv,descript,numEmpl,numPat,nomEmpl,nomPatient,frais,specialite, partenaire);
                    listeConsultation.Add(consultation);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeConsultation;
        }

        public static List<Consultation> ListeDesConsultations(string nom)
        {
            var listeConsultation = new List<Consultation>();
            try
            {
                requete = "SELECT consult_tbl.num_consult, consult_tbl.type_consul, consult_tbl.date_consult, " +
                    "consult_tbl.rv, consult_tbl.descrip, consult_tbl.num_empl, consult_tbl.num_pat, empl_tbl.nom_empl, " +
                    "patient_tbl.nom, patient_tbl.prenom ,consult_tbl.frais,consult_tbl.special,consult_tbl.partern FROM consult_tbl INNER JOIN empl_tbl ON consult_tbl.num_empl = empl_tbl.num_empl" +
                    " INNER JOIN patient_tbl ON consult_tbl.num_pat = patient_tbl.id WHERE patient_tbl.nom LIKE '%" + nom + "%' ORDER BY consult_tbl.num_consult DESC ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var consultation = new Consultation();
                    consultation.NumeroConsultation = reader.GetInt32(0);
                    consultation.TypeConsultation = reader.GetString(1);
                    consultation.DateConsultation = reader.GetDateTime(2);
                    consultation.RV = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    consultation.Description = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    consultation.NumeroEmploye = reader.GetString(5);
                    consultation.IdPatient = reader.GetInt32(6);
                    consultation.NomEmploye = reader.GetString(7);
                    consultation.NomPatient = reader.GetString(8) + " " + reader.GetString(9);
                    consultation.Frais = reader.GetDouble(10);
                    consultation.Specialite = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    consultation.Partenaire = !reader.IsDBNull(12) ? reader.GetString(12) : "";

                    listeConsultation.Add(consultation);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeConsultation;
        }

        public static List<Consultation> ListeDesConsultationsParIdPatient(int idPatient)
        {
            var listeConsultation = new List<Consultation>();
            try
            {
                requete = "SELECT consult_tbl.num_consult, consult_tbl.type_consul, consult_tbl.date_consult, " +
                    "consult_tbl.rv, consult_tbl.descrip, consult_tbl.num_empl, consult_tbl.num_pat, empl_tbl.nom_empl, " +
                    "patient_tbl.nom, patient_tbl.prenom ,consult_tbl.frais,consult_tbl.special,consult_tbl.partern FROM consult_tbl INNER JOIN empl_tbl ON consult_tbl.num_empl = empl_tbl.num_empl" +
                    " INNER JOIN patient_tbl ON consult_tbl.num_pat = patient_tbl.id WHERE consult_tbl.num_pat =" + idPatient + " ORDER BY consult_tbl.num_consult DESC ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var consultation = new Consultation();
                    consultation.NumeroConsultation = reader.GetInt32(0);
                    consultation.TypeConsultation = reader.GetString(1);
                    consultation.DateConsultation = reader.GetDateTime(2);
                    consultation.RV = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    consultation.Description = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    consultation.NumeroEmploye = reader.GetString(5);
                    consultation.IdPatient = reader.GetInt32(6);
                    consultation.NomEmploye = reader.GetString(7);
                    consultation.NomPatient = reader.GetString(8) + " " + reader.GetString(9);
                    consultation.Frais = reader.GetDouble(10);
                    consultation.Specialite = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    consultation.Partenaire = !reader.IsDBNull(12) ? reader.GetString(12) : "";

                    listeConsultation.Add(consultation);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeConsultation;
        }

        public static List<Consultation> ListeDesConsultations(DateTime dateConsul)
        {
            var listeConsultation = new List<Consultation>();
            try
            {
                requete = "SELECT consult_tbl.num_consult, consult_tbl.type_consul, consult_tbl.date_consult, " +
                    "consult_tbl.rv, consult_tbl.descrip, consult_tbl.num_empl, consult_tbl.num_pat, empl_tbl.nom_empl, " +
                    "patient_tbl.nom, patient_tbl.prenom, consult_tbl.frais,consult_tbl.special,consult_tbl.partern FROM consult_tbl INNER JOIN empl_tbl ON consult_tbl.num_empl " +
                    "= empl_tbl.num_empl INNER JOIN patient_tbl ON consult_tbl.num_pat = patient_tbl.id WHERE" +
                    " consult_tbl.date_consult >=@date1 AND consult_tbl.date_consult <@date2 ORDER BY consult_tbl.num_consult DESC";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dateConsul.Date));
                command.Parameters.Add(new MySqlParameter("date2", dateConsul.Date.AddHours(24)));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var consultation = new Consultation();
                    consultation.NumeroConsultation = reader.GetInt32(0);
                    consultation.TypeConsultation = reader.GetString(1);
                    consultation.DateConsultation = reader.GetDateTime(2);
                    consultation.RV = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    consultation.Description = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    consultation.NumeroEmploye = reader.GetString(5);
                    consultation.IdPatient = reader.GetInt32(6);
                    consultation.NomEmploye = reader.GetString(7);
                    consultation.NomPatient = reader.GetString(8) + " " + reader.GetString(9);
                    consultation.Frais = reader.GetDouble(10);
                    consultation.Specialite = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    consultation.Partenaire = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    listeConsultation.Add(consultation);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeConsultation;
        }

        public static List<Consultation> ListeDesConsultations(int numConsultation)
        {
            var listeConsultation = new List<Consultation>();
            //try
            //{
            //    requete = "SELECT * FROM consult_tbl WHERE num_consult = "+numConsultation;
            //    command.CommandText = requete;
            //    connection.Open();
            //    var reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        var numConsult = reader.GetInt32(0);
            //        var typeConsult = reader.GetString(1);
            //        var dateConsult = reader.GetDateTime(2);
            //        var rv = reader.GetDateTime(3);
            //        var descript = reader.GetString(4);
            //        var numEmpl = reader.GetString(5);
            //        var numPat = reader.GetInt32(6);
            //        var frais = reader.GetDouble(7);
            //        var consultation = new Consultation(numConsult, typeConsult, dateConsult,
            //            rv, descript, numEmpl, numPat, nomEmpl, nomPatient, frais);
            //        //var consultation = new Consultation(numConsult, typeConsult, dateConsult, rv, descript, numEmpl, numPat);
            //        listeConsultation.Add(consultation);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MonMessageBox.ShowBox("Liste consultation", ex);
            //}
            //finally
            //{
            //    connection.Close();
            //}
            return listeConsultation;
        }

        public static DataTable ListeConsultationDesEntreprise(int num, DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT patient_tbl.nom, patient_tbl.prenom, consult_tbl.type_consul, consult_tbl.date_consult, consult_tbl.frais," +
                    " consult_tbl.num_pat,patient_tbl.sc,consult_tbl.special, consult_tbl.num_consult FROM patient_tbl INNER JOIN consult_tbl ON patient_tbl.id = consult_tbl.num_pat " +
              " WHERE (patient_tbl.id = " + num + " AND consult_tbl.fact= @fact) AND (consult_tbl.date_consult >= @date1 AND consult_tbl.date_consult < @date2) ORDER BY consult_tbl.date_consult";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("fact", false));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeConsultationParMedecin(DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT * FROM consult_tbl WHERE (date_consult >= @date1 AND date_consult < @date2)";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeConsultationChiffre(string nomEmpl, DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT consult_tbl.type_consul, COUNT(consult_tbl.type_consul), consult_tbl.frais, " +
                    " COUNT(consult_tbl.type_consul) * consult_tbl.frais, consult_tbl.special,consult_tbl.num_consult FROM consult_tbl INNER JOIN empl_tbl ON " +
                    " consult_tbl.num_empl = empl_tbl.num_empl WHERE  nom_empl  LIKE '" + nomEmpl + "%' AND (date_consult" +
                ">= @date1 AND date_consult < @date2)  GROUP BY consult_tbl.type_consul, consult_tbl.frais";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable RapportConsultation(string nomEmpl, DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT consult_tbl.type_consul, COUNT(consult_tbl.type_consul),  consult_tbl.special FROM consult_tbl INNER JOIN empl_tbl ON " +
                          " consult_tbl.num_empl = empl_tbl.num_empl WHERE  nom_empl  LIKE '" + nomEmpl + "%' AND (date_consult" +
                          ">= @date1 AND date_consult < @date2)  GROUP BY consult_tbl.type_consul,consult_tbl.special ";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable RapportConsultationDeTousLesConvention(DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT consult_tbl.type_consul, COUNT(consult_tbl.type_consul),  consult_tbl.special FROM consult_tbl INNER JOIN patient_tbl ON " +
                          " consult_tbl.num_pat = patient_tbl.id WHERE  patient_tbl.entrep not  LIKE '' AND (date_consult" +
                          ">= @date1 AND date_consult < @date2)  GROUP BY consult_tbl.type_consul,consult_tbl.special ";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeConsultationChiffreDeTousLesConvention(DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT consult_tbl.type_consul, COUNT(consult_tbl.type_consul), consult_tbl.frais, " +
                    " COUNT(consult_tbl.type_consul) * consult_tbl.frais, consult_tbl.special,consult_tbl.num_consult FROM consult_tbl INNER JOIN patient_tbl ON " +
                    " consult_tbl.num_pat = patient_tbl.id WHERE  patient_tbl.entrep not LIKE  '' AND (date_consult" +
                ">= @date1 AND date_consult < @date2)  GROUP BY consult_tbl.type_consul, consult_tbl.frais";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable RapportConsultationParConvention(string Conventionne, DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT consult_tbl.type_consul, COUNT(consult_tbl.type_consul),  consult_tbl.special FROM consult_tbl INNER JOIN patient_tbl ON " +
                          " consult_tbl.num_pat = patient_tbl.id WHERE  patient_tbl.entrep  LIKE '%" + Conventionne + "%' AND (date_consult" +
                          ">= @date1 AND date_consult < @date2)  GROUP BY consult_tbl.type_consul,consult_tbl.special ";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeConsultationChiffreParConvention(string conventionne, DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT consult_tbl.type_consul, COUNT(consult_tbl.type_consul), consult_tbl.frais, " +
                    " COUNT(consult_tbl.type_consul) * consult_tbl.frais, consult_tbl.special,consult_tbl.num_consult  FROM consult_tbl INNER JOIN patient_tbl ON " +
                    " consult_tbl.num_pat = patient_tbl.id WHERE  patient_tbl.entrep  LIKE '%" + conventionne + "%' AND (date_consult" +
                ">= @date1 AND date_consult < @date2)  GROUP BY consult_tbl.type_consul, consult_tbl.frais";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }


        public static DataTable RapportConsultationParticuliers(DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT consult_tbl.type_consul, COUNT(consult_tbl.type_consul),  consult_tbl.special FROM consult_tbl INNER JOIN patient_tbl ON " +
                          " consult_tbl.num_pat = patient_tbl.id WHERE  patient_tbl.entrep  ='' AND (date_consult" +
                          ">= @date1 AND date_consult < @date2)  GROUP BY consult_tbl.type_consul,consult_tbl.special ";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeConsultationChiffreParticuliers(DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT consult_tbl.type_consul, COUNT(consult_tbl.type_consul), consult_tbl.frais, " +
                    " COUNT(consult_tbl.type_consul) * consult_tbl.frais, consult_tbl.special FROM consult_tbl INNER JOIN patient_tbl ON " +
                    " consult_tbl.num_pat = patient_tbl.id WHERE  patient_tbl.entrep  ='' AND (date_consult" +
                ">= @date1 AND date_consult < @date2)  GROUP BY consult_tbl.type_consul, consult_tbl.frais";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static bool EnregistrerUnTypeConsultation(Consultation consultation)
        {
            try
            {
                connection.Open();

                requete = "select * FROM type_consultation WHERE id = " + consultation.NumeroConsultation;
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = "INSERT INTO type_consultation ( type_consult,specialite,prix,prix_conv,descrip) " +
                              "VALUES(@type_consult,@specialite,@prix,@prix_conv,@descrip)";

                }
                else
                {
                    reader.Close();
                    requete = "UPDATE type_consultation SET prix_conv=@prix_conv, type_consult =@type_consult, descrip=@descrip, prix=@prix, specialite =@specialite WHERE id =  " + consultation.NumeroConsultation;
                }

                command.Parameters.Add(new MySqlParameter("type_consult", consultation.TypeConsultation));
                command.Parameters.Add(new MySqlParameter("descrip", consultation.Description));
                command.Parameters.Add(new MySqlParameter("prix", consultation.Frais));
                command.Parameters.Add(new MySqlParameter("specialite", consultation.Specialite));
                command.Parameters.Add(new MySqlParameter("prix_conv", consultation.FraisConventionnee));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de la consultation a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static List<Consultation> ListeDesTypesConsultations()
        {
            var listeConsultation = new List<Consultation>();
            try
            {
                requete = "SELECT * FROM type_consultation ORDER BY type_consult, specialite";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var consultation = new Consultation();
                    consultation.NumeroConsultation = reader.GetInt32(0);
                    consultation.TypeConsultation = reader.GetString(1);
                    consultation.Specialite = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    consultation.Description = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    consultation.Frais = !reader.IsDBNull(3) ? reader.GetDouble(3) : 0;
                    consultation.FraisConventionnee = !reader.IsDBNull(4) ? reader.GetDouble(4) : 0;
                    listeConsultation.Add(consultation);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste consultation", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeConsultation;
        }

        public static void SupprimerUnTypeConsultation(int numeroConsult)
        {
            try
            {
                connection.Open();
                requete = "DELETE FROM type_consultation WHERE id = " + numeroConsult;
                command.CommandText = requete;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression de la consultation a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }
        public static void SupprimerFactureProforma(int numeroFacure, string design)
        {
            try
            {
                connection.Open();
                requete = "DELETE  FROM pro_det_fact WHERE id_fact = " + numeroFacure + " AND design =@design";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("design", design));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
        public static void SupprimerFactureProforma(int numeroFacure)
        {
            try
            {
                connection.Open();
                requete = "DELETE FROM pro_facture_tbl WHERE id_fact = " + numeroFacure;
                command.CommandText = requete;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
        /******************************** FIN CODE CONSULTATION ****************************/

        /******************************* CODE ANTECEDANT DUNPATIENT ***************************/
        #region AntecedantDuPatient
        public static bool EnregistrerUnAntecedant(Antecedant antecedant, string etat)
        {
            var flag = false;
            try
            {
                connection.Open();
                if (etat == "1")
                {
                    requete = "INSERT INTO antecedant_tbl ( date_debut,date_fin,descrip,num_pat,traitement) " +
                        "VALUES(@date_debut,@date_fin,@descrip,@num_pat,@traitement)";
                }
                else
                {
                    requete = "UPDATE antecedant_tbl SET  date_debut=@date_debut,date_fin=@date_fin,descrip=@descrip," +
                            "traitement=@traitement WHERE num_ante= "+antecedant.NumeroAntecedant;
                }
                command.Parameters.Add(new MySqlParameter("date_debut", antecedant.DebutAntecedant));
                command.Parameters.Add(new MySqlParameter("date_fin", antecedant.FinAntecedant));
                command.Parameters.Add(new MySqlParameter("descrip", antecedant.Description));
                command.Parameters.Add(new MySqlParameter("traitement", antecedant.Traitement));
                command.Parameters.Add(new MySqlParameter("num_pat", antecedant.IdPatient));
                command.CommandText = requete;
                flag = true;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Les données des antecedants du patient sont enregistrées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement des antecedants du patient a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return flag;
        }

        public static void SupprimerUnAntecedant(int numeroAntecedant)
        {
            try
            {
                requete = "DELETE FROM antecedant_tbl WHERE num_ante = " + numeroAntecedant;
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données des antecedants du patient supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données des antecedants du patient a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Antecedant> ListeDesAntecedants(int idPatient)
        {
            var listeAntecedant= new List<Antecedant>();
            try
            {
                requete = "SELECT * FROM antecedant_tbl WHERE num_pat ="+idPatient;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var antecedant = new Antecedant();
                    antecedant.NumeroAntecedant = reader.GetInt32(0);
                    antecedant.DebutAntecedant = reader.GetDateTime(1);
                    antecedant.FinAntecedant = reader.GetDateTime(2);
                    antecedant.Description = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    antecedant.Traitement = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    antecedant.IdPatient = reader.GetInt32(5);
                   listeAntecedant.Add(antecedant);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste antecedant", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeAntecedant;
        }
       #endregion
        /***************************** FIN CODE ANTECEDANT DU PATIENT ***********************/

        /******************************* CODE ORDONNANCE DUN PATIENT ***************************/
        #region OrdonnanceDuPatient
        public static bool AjouterUneOrdonnance(Ordonnance ordonnance, DataGridView dgvDetail)
        {

            var flag = false;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "INSERT INTO ordon_tbl (date_ordon,num_empl,num_pat) VALUES(@date_ordon,@num_empl,@num_patient)";
                command.Parameters.Add(new MySqlParameter("date_ordon", ordonnance.DateOrdonnance));
                command.Parameters.Add(new MySqlParameter("num_patient", ordonnance.IdPatient));
                command.Parameters.Add(new MySqlParameter("num_empl", ordonnance.NumeroEmploye));
                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                requete = "select MAX(code_ordon)  FROM ordon_tbl";
                command.CommandText = requete;
                command.Connection = connection;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                ordonnance.NumeroOrdonnance = (int)command.ExecuteScalar();

                foreach(DataGridViewRow dgvRow in dgvDetail.Rows)
                {
                    var medi = dgvRow.Cells[0].Value.ToString();
                    var qte = dgvRow.Cells[1].Value.ToString();
                     var nbr = Int32.Parse(dgvRow.Cells[2].Value.ToString());
                     var jr = Int32.Parse(dgvRow.Cells[3].Value.ToString());
                     requete = "INSERT INTO det_ord (num_ordn, medi,qte,nbr,jr) VALUES(@num_ordn,@medi,@qte,@nbr,@jr)";
                    command.Parameters.Add(new MySqlParameter("num_ordn", ordonnance.NumeroOrdonnance));
                    command.Parameters.Add(new MySqlParameter("medi", medi));
                    command.Parameters.Add(new MySqlParameter("qte", qte));
                    command.Parameters.Add(new MySqlParameter("nbr", nbr));
                    command.Parameters.Add(new MySqlParameter("jr", jr));
                    command.Transaction = transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                
                transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("L'ordonnance du patient  est enregistrée avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("L' enregistrement de l'ordonnance a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool ModifierUneOrdonnance(Ordonnance ordonnance, string patient)
        {
            var flag = false;
            try
            {
                requete = "UPDATE ordon_tbl SET date_ordon=@date_ordon, num_empl=@num_empl, num_pat=@num_pat" +
                    " WHERE code_ordon = "+ordonnance.NumeroOrdonnance;
                command.Parameters.Add(new MySqlParameter("date_ordon", ordonnance.DateOrdonnance));
                command.Parameters.Add(new MySqlParameter("num_empl", ordonnance.NumeroEmploye));
                command.Parameters.Add(new MySqlParameter("num_pat", ordonnance.IdPatient));
                command.CommandText = requete;
                flag = true;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Les données de l'ordonnance du patient " + patient + " sont modifiées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l'ordonnance du patient a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                 command.Parameters.Clear();
            }
            return flag;
        }

        public static bool ModifierUneOrdonnance(Ordonnance ordonnance, DataGridView dgvDetail)
        {
            var flag = false;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "UPDATE ordon_tbl SET date_ordon=@date_ordon, num_empl=@num_empl, num_pat=@num_pat" +
                   " WHERE code_ordon = " + ordonnance.NumeroOrdonnance;
                command.Parameters.Add(new MySqlParameter("date_ordon", ordonnance.DateOrdonnance));
                command.Parameters.Add(new MySqlParameter("num_empl", ordonnance.NumeroEmploye));
                command.Parameters.Add(new MySqlParameter("num_pat", ordonnance.IdPatient));
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                foreach (DataGridViewRow dgvRow in dgvDetail.Rows)
                {
                    var medi = dgvRow.Cells[0].Value.ToString();
                    var qte = dgvRow.Cells[1].Value.ToString();
                    var nbr = Int32.Parse(dgvRow.Cells[2].Value.ToString());
                    var jr = Int32.Parse(dgvRow.Cells[3].Value.ToString());
                    requete = "UPDATE det_ord SET qte = '" + qte + "',nbr =" + nbr + ",jr="
                        + jr + " WHERE medi = @medi AND num_ordn = " + ordonnance.NumeroOrdonnance;
                    command.Parameters.Add(new MySqlParameter("medi", medi));
                    command.Transaction = transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("Les details de l'ordonnance du patient sont modifiées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("L' enregistrement de l'ordonnance du patient a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                 command.Parameters.Clear();
            }
            return flag;
        }
       
        public static void SupprimerUneOrdonnance(int numeroOrdonnance)
        {
            try
            {
                requete = "DELETE FROM ordon_tbl WHERE code_ordon = " + numeroOrdonnance;
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de l'ordonnance du patient supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données de l'ordonnace du patient a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static void SupprimerUneOrdonnance(int numeroOrdonnance,string medicament)
        {
            try
            {
                requete = "DELETE FROM det_ord WHERE num_ordn = " + numeroOrdonnance +" AND medi =@medicament";
                command.Parameters.Add(new MySqlParameter("medicament", medicament));
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox(medicament + " a été retiré de la liste de l'ordonnance du patient ", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données de l'ordonnance du patient a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static List<Ordonnance> ListeDesOrdonnances()
        {
            var listeOrdonnance = new List<Ordonnance>();
            try
            {
                requete = "SELECT ordon_tbl.code_ordon, ordon_tbl.date_ordon, ordon_tbl.num_empl, ordon_tbl.num_pat, "+
                    " empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom FROM ordon_tbl INNER JOIN  empl_tbl ON "+
                    "ordon_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON ordon_tbl.num_pat = patient_tbl.id";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var numOrdon = reader.GetInt32(0);
                    var dateOrdon = reader.GetDateTime(1);
                    var idPatient = reader.GetInt32(3);
                    var numEmpl = reader.GetString(2);
                    var nomEmpl = reader.GetString(4);
                    var patient = reader.GetString(5) + " " + reader.GetString(6);
                    var ordonnance = new Ordonnance(numOrdon, dateOrdon, idPatient,patient, numEmpl,nomEmpl);
                    listeOrdonnance.Add(ordonnance);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste ordonnance", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeOrdonnance;
        }

        public static List<Ordonnance> ListeDetailOrdonnances(int id)
        {
            var listeOrdonnance = new List<Ordonnance>();
            try
            {
                requete = "SELECT * FROM det_ord WHERE num_ordn = " + id;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var medi = reader.GetString(1);
                    var qte = reader.GetString(2);
                    var nbr = reader.GetInt32(3);
                    var jr = reader.GetInt32(4);
                    var ordonnance = new Ordonnance(id, medi, qte, nbr, jr);
                    listeOrdonnance.Add(ordonnance);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste ordonnance", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeOrdonnance;
        }

        public static List<Ordonnance> ListeDesUneOrdonnances(int numOrdon)
        {
            var listeOrdonnance = new List<Ordonnance>();
            try
            {
                requete = "SELECT * FROM ordon_tbl_tbl WHERE code_ordon = " + numOrdon;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                //    var numOrdon = reader.GetInt32(0);
                    var dateOrdon = reader.GetDateTime(1);
                    var idPatient = reader.GetInt32(3);
                    var numEmpl = reader.GetString(2);
                    var ordonnance = new Ordonnance(numOrdon, dateOrdon, idPatient,numEmpl);
                    listeOrdonnance.Add(ordonnance);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste ordonnance", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeOrdonnance;
        }

        public static List<Ordonnance> ListeDesOrdonnancesParPatient(int idPatient)
        {
            var listeOrdonnance = new List<Ordonnance>();
            try
            {
                requete = "SELECT * FROM ordon_tbl_tbl WHERE num_pat =" + idPatient;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var numOrdon = reader.GetInt32(0);
                    var dateOrdon = reader.GetDateTime(1);
                    var numEmpl = reader.GetString(2);
                    var ordonnance = new Ordonnance(numOrdon, dateOrdon, idPatient, numEmpl);
                    listeOrdonnance.Add(ordonnance);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste ordonnance", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeOrdonnance;
        }

        public static DataTable DetailsDesOrdonnancesPrescitesParMedecin(string idMedecin, DateTime date1, DateTime date2)
        {
            var listeDT = new DataTable();
            try
            {
                requete = "SELECT ordon_tbl.code_ordon, ordon_tbl.date_ordon, ordon_tbl.num_empl, det_ord.medi, det_ord.qte," +
                   " det_ord.nbr, det_ord.jr ,ordon_tbl.num_pat FROM ordon_tbl INNER JOIN det_ord ON ordon_tbl.code_ordon = det_ord.num_ordn " +
                   " WHERE (ordon_tbl.num_empl = '"+ idMedecin + "') AND (ordon_tbl.date_ordon >= @date1 AND ordon_tbl.date_ordon < @date2)"+
                   " ORDER BY ordon_tbl.code_ordon DESC";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                connection.Open();
                var reader = command.ExecuteReader();
                listeDT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste analyse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeDT;
        }
     
        #endregion
        /***************************** FIN CODE ORDONNANCE *******************************/

        /******************************* CODE ENTREPRISE *****************************/
        #region Entreprises
        public static bool AjouterUneEntreprise(Entreprise entreprise)
        {

            var flag = false;
            try
            {
                connection.Open();
                requete = "SELECT * FROM entre_tbl WHERE entreprise = '" + entreprise + "'";
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = "INSERT INTO entre_tbl (entreprise,tele1,tele2,email,adresse,date_abon,tauxHon,attache, plafond, si_limite) " +
                        "VALUES(@entreprise,@tele1,@tele2,@email,@adresse,@date_abon,@tauxHon,@attache, @plafond,@si_limite)";
                    command.Parameters.Add(new MySqlParameter("entreprise", entreprise.NomEntreprise));
                    command.Parameters.Add(new MySqlParameter("tele1", entreprise.Telephone1));
                    command.Parameters.Add(new MySqlParameter("tele2", entreprise.Telephone2));
                    command.Parameters.Add(new MySqlParameter("email", entreprise.Email));
                    command.Parameters.Add(new MySqlParameter("adresse", entreprise.Adresse));
                    command.Parameters.Add(new MySqlParameter("date_abon", entreprise.DateAbonnement));
                    command.Parameters.Add(new MySqlParameter("tauxHon", entreprise.PrixHonoraire));
                    command.Parameters.Add(new MySqlParameter("attache", entreprise.Attache));
                    command.Parameters.Add(new MySqlParameter("plafond", entreprise.MontantLimite));
                    command.Parameters.Add(new MySqlParameter("si_limite", entreprise.SiLimite));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    flag = true;
                    MonMessageBox.ShowBox("Nouvelle entreprise  est enregistrée avec succés", "Affirmation", "affirmation.png");

                }
                else
                {
                    MonMessageBox.ShowBox(entreprise.NomEntreprise + "  existe deja dans la base de données", "Erreur", "erreur.png");
                }
                }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l'entreprise a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool ModifierUneEntreprise(Entreprise entreprise)
        {
            var flag = false;
            try
            {
                connection.Open();
                requete = "UPDATE entre_tbl SET entreprise = @entreprise,tele1 = @tele1,tele2=@tele2,attache=@attache,"+
                    " email=@email,adresse=@adresse , date_abon=@date_abon,tauxHon = @tauxHon,plafond=@plafond,si_limite=@si_limite  WHERE id = " + entreprise.NumeroEntreprise;
                command.Parameters.Add(new MySqlParameter("entreprise", entreprise.NomEntreprise));
                command.Parameters.Add(new MySqlParameter("tele1", entreprise.Telephone1));
                command.Parameters.Add(new MySqlParameter("tele2", entreprise.Telephone2));
                command.Parameters.Add(new MySqlParameter("email", entreprise.Email));
                command.Parameters.Add(new MySqlParameter("adresse", entreprise.Adresse));
                command.Parameters.Add(new MySqlParameter("date_abon", entreprise.DateAbonnement));
                command.Parameters.Add(new MySqlParameter("tauxHon", entreprise.PrixHonoraire));
                command.Parameters.Add(new MySqlParameter("attache", entreprise.Attache));
                command.Parameters.Add(new MySqlParameter("plafond", entreprise.MontantLimite));
                command.Parameters.Add(new MySqlParameter("si_limite", entreprise.SiLimite));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                flag = true;
                MonMessageBox.ShowBox("Nouvelle entreprise  est enregistrée avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l'entreprise a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static void SupprimerUneEntreprise(int numeroEntreprise)
        {
            try
            {
                requete = "DELETE FROM entre_tbl WHERE id = " + numeroEntreprise;
                connection.Open();
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de l'entreprise supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données de l'entreprise a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

     
        public static List<Entreprise> ListeDesEntreprises()
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                requete = "SELECT * FROM entre_tbl ORDER BY entreprise ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var entrepr = reader.GetString(1);
                    var tele1 = reader.GetString(2);
                    var tele2 = reader.GetString(3);
                    var email = reader.GetString(4);
                    var adresse = reader.GetString(5);
                    var date = reader.GetDateTime(6);
                    var frais = !reader.IsDBNull(7) ? reader.GetDouble(7) : 0.0;
                    var attache = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    var siLimit = !reader.IsDBNull(9) ? reader.GetBoolean(9) : false;
                    var montantLimit = !reader.IsDBNull(10) ? reader.GetDouble(10) : 0.0;
                    var entreprise = new Entreprise(id,entrepr,tele1,tele2,email,adresse,date,frais,attache,siLimit,montantLimit);
                    listeEntreprise.Add(entreprise);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeEntreprise;
        }

        public static List<Entreprise> ListeDesEntreprises(int id)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                requete = "SELECT * FROM entre_tbl WHERE id = "+id ;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var entrepr = reader.GetString(1);
                    var tele1 = reader.GetString(2);
                    var tele2 = reader.GetString(3);
                    var email = reader.GetString(4);
                    var adresse = reader.GetString(5);
                    var date = reader.GetDateTime(6);
                    var frais = !reader.IsDBNull(7) ? reader.GetDouble(7) : 0.0;
                    var attache = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    var siLimit = !reader.IsDBNull(9) ? reader.GetBoolean(9) : false;
                    var montantLimit = !reader.IsDBNull(10) ? reader.GetDouble(10) : 0.0;
                    var entreprise = new Entreprise(id, entrepr, tele1, tele2, email, adresse, date, frais, attache, siLimit, montantLimit);
                    listeEntreprise.Add(entreprise);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeEntreprise;
        }

        public static List<Entreprise> ListeDesEntreprises(string entrepri)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                requete = "SELECT * FROM entre_tbl WHERE entreprise LIKE '" + entrepri+"%'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var entreprises = reader.GetString(1);
                    var tele1 = reader.GetString(2);
                    var tele2 = reader.GetString(3);
                    var email = reader.GetString(4);
                    var adresse = reader.GetString(5);
                    var date = reader.GetDateTime(6);
                    var frais = !reader.IsDBNull(7) ? reader.GetDouble(7) : 0.0;
                    var attache = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    var siLimit = !reader.IsDBNull(9) ? reader.GetBoolean(9) : false;
                    var montantLimit = !reader.IsDBNull(10) ? reader.GetDouble(10) : 0.0;
                    var entreprise = new Entreprise(id, entreprises, tele1, tele2, email, adresse, date, frais, attache, siLimit, montantLimit);
                    listeEntreprise.Add(entreprise);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeEntreprise;
        }


        public static List<Entreprise> ListeDesEntreprises(DateTime date)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                requete = "SELECT * FROM entre_tbl WHERE date_abon>=@date_abon ORDER BY entreprise";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date_abon", date));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var entreprises = reader.GetString(1);
                    var tele1 = reader.GetString(2);
                    var tele2 = reader.GetString(3);
                    var email = reader.GetString(4);
                    var adresse = reader.GetString(5);
                    var dateAbon = reader.GetDateTime(6);
                    var frais = !reader.IsDBNull(7) ? reader.GetDouble(7) : 0.0;
                    var attache = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    var siLimit = !reader.IsDBNull(9) ? reader.GetBoolean(9) : false;
                    var montantLimit = !reader.IsDBNull(10) ? reader.GetDouble(10) : 0.0;
                    var entreprise = new Entreprise(id, entreprises, tele1, tele2, email, adresse, date, frais, attache, siLimit, montantLimit);
                    listeEntreprise.Add(entreprise);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();

            }
            return listeEntreprise;
        }

        #endregion
        /***************************** FIN CODE ENTREPRISE ***********************/

        /******************************* CODE ABONNEMENT *****************************/
        #region EntrepriseAbonne
        public static bool EnregistrerUnAbonnement(Hashtable hashAbonne)
        {

            var flag = false;
            try
            {
                connection.Open();
                foreach (var k in hashAbonne.Keys)
                {
                    requete = "SELECT * FROM entre_tbl WHERE entreprise = " + k;
                    command.CommandText = requete;
                    var reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        DateTime dateAbonnement = DateTime.Parse(hashAbonne[k].ToString());
                        requete = "INSERT INTO abonne_tbl (date_abonn,id_entre) VALUES(@date_abonn,@id_entre";
                        command.Parameters.Add(new MySqlParameter("id_entre", k));
                        command.Parameters.Add(new MySqlParameter("date_abonn", dateAbonnement));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        flag = true;
                        MonMessageBox.ShowBox("L'abonnement  entreprise  est enregistrée avec succés", "Affirmation", "affirmation.png");

                    }
                    else
                    {
                        MonMessageBox.ShowBox("L'abonnement de cette entreprise   existe deja dans la base de données", "Erreur", "erreur.png");
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l'entreprise a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool ModifierAbonnement(Hashtable hashAbonne)
        {
            var flag = false;
            try
            {
                connection.Open();
                foreach (var k in hashAbonne.Keys)
                {
                        DateTime dateAbonnement = DateTime.Parse(hashAbonne[k].ToString());
                        requete = "INSERT INTO abonne_tbl SET date_abonn = @date_abonn WHERE id_entre = @id_entre";
                        command.Parameters.Add(new MySqlParameter("id_entre", k));
                        command.Parameters.Add(new MySqlParameter("date_abonn", dateAbonnement));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        flag = true;
                        MonMessageBox.ShowBox("La date d'abonnement  est modifiée avec succés", "Affirmation", "affirmation.png");
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l'abonnement a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static void SupprimerUnAbonnement(int numeroEntreprise)
        {
            try
            {
                requete = "DELETE FROM entre_tbl WHERE id = " + numeroEntreprise;
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de l'entreprise supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données de l'entreprise a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataTable ListeDesAbonnements()
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT * FROM abonne_tbl";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste abonnement", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable ListeDesAbonnements(int id)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT * FROM abonne_tbl WHERE id = " + id;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste abonnement", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable ListeDesAbonnements(string entrepri)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT * FROM entre_tbl WHERE entreprise LIKE '%" + entrepri + "%'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste abonnement", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        #endregion
        /***************************** FIN CODE ABONNEMENT ***********************/

        /****************************** CODE EMPLOYE ******************************/
        #region Employe
        // ajouter un nouveau Employee
        public static bool EnregistrerEmployee(Employe employee)
        {
            try
            {
                connection.Open();
                string requete = "SELECT * FROM empl_tbl WHERE num_empl ='" + employee.NumMatricule + "'";
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable(); dt.Load(reader); reader.Close();
                int count = dt.Rows.Count;
                if (count < 1)
                {
                    requete = "INSERT INTO `empl_tbl` (`num_empl`, `nom_empl`, `Addresse`, `telephone1`, `telephone2`," +
                            "`email`, `titre`,`id_service`) VALUES (@num_empl, @nom_empl, @Addresse, @telephone1, @telephone2, @email, @titre,@id_service )";
                }
                else
                {
                    requete =
                       string.Format(
                           "UPDATE empl_tbl SET nom_empl = @nom_empl, Addresse = @Addresse, telephone1 = @telephone1, telephone2 = @telephone2" +
                           ", email = @email, titre = @titre ,id_service=@id_service WHERE (num_empl = @num_empl)");
                }

                command.Parameters.Add(new MySqlParameter("num_empl", employee.NumMatricule));
                command.Parameters.Add(new MySqlParameter("nom_empl", employee.NomEmployee));
                command.Parameters.Add(new MySqlParameter("Addresse", employee.Addresse));
                command.Parameters.Add(new MySqlParameter("telephone1", employee.Telephone1));
                command.Parameters.Add(new MySqlParameter("telephone2", employee.Telephone2));
                command.Parameters.Add(new MySqlParameter("email", employee.Email));
                command.Parameters.Add(new MySqlParameter("id_service", employee.NumeroService));
                command.Parameters.Add(new MySqlParameter("titre", employee.Titre));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        // modifier donnees Employee
        public static void ModifierEmployee(string numEmploye, string image)
        {
            try
            {
                connection.Open();
                string requete = string.Format("UPDATE empl_tbl SET photos ='{0}' WHERE (num_empl = '{1}')", image,
                    numEmploye);

                command.CommandText = requete;
                command.ExecuteNonQuery();

                //MonMessageBox.ShowBox("image de l'employé inserée avec succés.", "Information modification",
                //    "affirmation.png");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur modification employe", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        // supprimer les donnees du  Employee
        public static void SupprimerEmployee(string employeeId)
        {

            try
            {
                connection.Open();
                string requete = string.Format("DELETE FROM empl_tbl WHERE num_empl = '{0}' ", employeeId);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox(
                    "Données de l'employé no " + employeeId + " ont été supprimées de la base de données",
                    "Information suppression", "affirmation.png");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("la suppression des données a échoué", "Erreur suppression employe", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        //liste des employe
        public static List<Employe> ListeDesEmployees()
        {
            List<Employe> list = new List<Employe>();

            try
            {
                string requete = "SELECT * FROM empl_tbl ORDER BY nom_empl ";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employe employe = new Employe();
                    employe.NumMatricule = reader.GetString(0);
                    employe.NomEmployee = reader.GetString(1);
                    employe.Addresse = reader.GetString(2);
                    employe.Telephone1 = reader.GetString(3);
                    employe.Telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    employe.Email = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    employe.Titre = reader.GetString(6);
                    employe.Photo = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    employe.NumeroService = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    list.Add(employe);
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste employe", exception);
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        //liste des employe par parametre
        public static List<Employe> ListeDesEmployees(string colonne, string param)
        {
            List<Employe> list = new List<Employe>();

            try
            {
                string requete = string.Format("SELECT * FROM empl_tbl WHERE  {0} = ('{1}')", colonne, param);
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employe employe = new Employe();
                    employe.NumMatricule = reader.GetString(0);
                    employe.NomEmployee = reader.GetString(1);
                    employe.Addresse = reader.GetString(2);
                    employe.Telephone1 = reader.GetString(3);
                    employe.Telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    employe.Email = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    employe.Titre = reader.GetString(6);
                    employe.Photo = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    employe.NumeroService = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    list.Add(employe);
                }
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        //liste des employe par parametre
        public static List<Employe> ListeDesEmployees(string param)
        {
            List<Employe> list = new List<Employe>();

            try
            {
                string requete = "SELECT * FROM empl_tbl WHERE  nom_empl LIKE '%" + param + "%'";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employe employe = new Employe();
                    employe.NumMatricule = reader.GetString(0);
                    employe.NomEmployee = reader.GetString(1);
                    employe.Addresse = reader.GetString(2);
                    employe.Telephone1 = reader.GetString(3);
                    employe.Telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    employe.Email = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    employe.Titre = reader.GetString(6);
                    employe.Photo = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    employe.NumeroService = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    list.Add(employe);
                }
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        //liste des employe par parametre
        public static bool EnregistrerService(int id, string service)
        {
            try
            {
                if (id <= 0)
                {
                    requete = "INSERT INTO service_tbl (service) VALUES(@service)";
                }
                else
                {
                    requete = "UPDATE service_tbl  SET service=@service WHERE id =" + id;
                }
                connection.Open();
                command.Parameters.Add(new MySqlParameter("service", service));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
        public static DataTable ListeService()
        {
            DataTable dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM service_tbl";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste service", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static DataTable ListeService(string service)
        {
            DataTable dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM service_tbl WHERE service=@service";
                command.CommandText = requete;
                connection.Open();
                command.Parameters.Add(new MySqlParameter("service", service));
                var reader = command.ExecuteReader();
                dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste service", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return dt;
        }
        public static void SupprimerService(int idService)
        {
            DataTable dt = new DataTable();
            try
            {
                var requete = "DELETE  FROM service_tbl WHERE id = " + idService;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste service", ex);
            }
            finally
            {
                connection.Close();
            }
        }
        public static DataTable ListeService(int idService)
        {
            DataTable dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM service_tbl WHERE id=" + idService;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste service", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        #endregion
     
        /****************************** FIN CODE EMPLOYE ******************************/

        /******************************* CODE TRACKER *********************************/
        #region Tracker
        //liste de  log 
        public static DataTable Log()
        {
            DataTable dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM track_tbl";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste track", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        //liste de  journal 
        public static DataTable Log(DateTime date)
        {
            var dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM track_tbl WHERE time >=@time1 AND time <=@time2";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("time1", date));
                command.Parameters.Add(new MySqlParameter("time2", date.AddHours(24)));
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste track", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        //liste de  journal 
        public static DataTable Log(string nomPers)
        {
            var dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM track_tbl WHERE nom LIKE '%" + nomPers + "%'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader(); dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste track", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        //enregistrer les donnees de la log
        public static bool Tracker(string nomUtil, string nom, bool etat)
        {
            var flag = false;
            try
            {
                connection.Open();
                var requete =
                    "INSERT INTO `track_tbl` (nom_util, nom,time,etat) VALUES(@nom_util,@nom,@time,@etat)";
                command.Parameters.Add(new MySqlParameter("nom_util", nomUtil));
                command.Parameters.Add(new MySqlParameter("nom", nom));
                command.Parameters.Add(new MySqlParameter("time", DateTime.Now));
                command.Parameters.Add(new MySqlParameter("etat", etat));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception )
            {
                flag = false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        //liste de  journal 
        public static DataTable AccorderPrivilege()
        {
            var dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM privi_tbl";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader(); dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste track", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        //supprimer les donnees de la 
        public static void ViderLog()
        {
            try
            {
                var requete = "DELETE FROM track_tbl ";
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données supprimées avec succés",
                    "Information","affirmation");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression a échoué veuillez verifier les parametres et réessayer",
                    "Erreur suppresion", exception,"erreur");
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
        /*******************************END CODE TRACKER *****************************/
   
        /******************************* CODE FACTURAL *******************************/
        #region FactureDuPatient
        public static bool EnregistrerUneFacture(Facture facture, DataGridView dgvFacture, int numActe, string TypeFacture, double montantPaye)
        {
            var flag = false;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "INSERT INTO facture_tbl (date_fact, montant_fact,num_patient,num_empl, reste) " +
                   " VALUES(@date_fact," + facture.MontantFactural + "," + facture.IdPatient + ",'" + facture.NumeroEmploye + "', " + facture.Reste + ")";
                command.Parameters.Add(new MySqlParameter("date_fact", facture.DateFacture));
                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                //requete = "INSERT INTO facture_tbl (date_fact, montant_fact,num_patient,num_empl, reste) " +
                // " VALUES(@date_fact," + facture.MontantFactural + "," + facture.IdPatient + ",'" + facture.NumeroEmploye + "', " + facture.Reste + ")";
                //command.Parameters.Add(new MySqlParameter("date_fact", facture.DateFacture));
                //command.Transaction = transaction;
                //command.CommandText = requete;
                //command.ExecuteNonQuery();

                requete = "select MAX(id_fact)  FROM facture_tbl";
                command.CommandText = requete;
                command.Connection = connection;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                facture.NumeroFacture = (int)command.ExecuteScalar();

                requete = "SELECT * FROM pro_facture_tbl WHERE num_acte = " + numActe + " AND acte ='" + TypeFacture + "'";
                command.CommandText = requete;
                command.Transaction = transaction;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    requete = "DELETE FROM pro_facture_tbl WHERE num_acte = " + numActe + " AND acte ='" + TypeFacture + "'";
                    command.CommandText = requete;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                }
                reader.Close();
                requete = "INSERT INTO det_paie_tbl (id_paie, date_paie ,montant)  VALUES(" + facture.NumeroFacture + ",@date_paie," + montantPaye + ")";
                command.Parameters.Add(new MySqlParameter("date_paie", DateTime.Now));
                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                var total = 0.0;
                for (var i = 0; i < dgvFacture.Rows.Count; i++)
                {
                    total += Double.Parse(dgvFacture.Rows[i].Cells[1].Value.ToString()) *
                        Int32.Parse(dgvFacture.Rows[i].Cells[2].Value.ToString());
                }

                flag = true;
                if (total > montantPaye)
                {
                    flag = false;
                }
                foreach (DataGridViewRow dtRow in dgvFacture.Rows)
                {
                    var designation = dtRow.Cells[0].Value.ToString();
                    var prix = Double.Parse(dtRow.Cells[1].Value.ToString());
                    var qte = Int32.Parse(dtRow.Cells[2].Value.ToString());
                    var prix_total = Double.Parse(dtRow.Cells[3].Value.ToString());
                    var groupage = dtRow.Cells[4].Value.ToString();

                    requete = "INSERT INTO det_fact (id_fact, design,prix,qte,prix_total,groupage,pourcentage,id) VALUES(@id_fact1,@design,@prix,@qte,@prix_total,@groupage,@pourcentage,@id)";
                    command.Parameters.Add(new MySqlParameter("id_fact1", facture.NumeroFacture));
                    command.Parameters.Add(new MySqlParameter("design", designation));
                    command.Parameters.Add(new MySqlParameter("qte", qte));
                    command.Parameters.Add(new MySqlParameter("prix", prix));
                    
                    command.Parameters.Add(new MySqlParameter("prix_total", prix_total));
                    command.Parameters.Add(new MySqlParameter("groupage", groupage));
                    command.Parameters.Add(new MySqlParameter("pourcentage", facture.PartPatient));
                    command.Parameters.Add(new MySqlParameter("id", dtRow.Cells[5].Value.ToString()));
                    command.Transaction = transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
                transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("Facture no " + facture.NumeroFacture + " est enregistrée avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("L' enregistrement des données de la facture a échoué. Verifier si vous n'avez pas ajouter deux libellés sous le meme nom.", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool EnregistrerUneFactureConventionnee(Facture facture, DataGridView dgvFacture, int numActe, string TypeFacture, double montantPaye)
        {
            var flag = false;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                var total = 0.0;
                for (var i = 0; i < dgvFacture.Rows.Count; i++)
                {
                    total += Double.Parse(dgvFacture.Rows[i].Cells[1].Value.ToString()) *
                        Int32.Parse(dgvFacture.Rows[i].Cells[2].Value.ToString());
                }

                requete = "INSERT INTO pro_facture_tbl (date_fact, montant_fact,num_patient,num_empl) " +
                   " VALUES(@date_fact," + facture.MontantFactural + "," + facture.IdPatient + ",'" + facture.NumeroEmploye + "')";
                command.Parameters.Add(new MySqlParameter("date_fact", facture.DateFacture));
                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                requete = "select MAX(id_fact)  FROM pro_facture_tbl";
                command.CommandText = requete;
                command.Connection = connection;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                facture.NumeroFacture = (int)command.ExecuteScalar();
            
                flag = true;
                if (total > montantPaye)
                {
                    flag = false;
                }
                foreach (DataGridViewRow dtRow in dgvFacture.Rows)
                {
                    var designation = dtRow.Cells[0].Value.ToString();
                    var prix = Double.Parse(dtRow.Cells[1].Value.ToString());
                    var qte = Int32.Parse(dtRow.Cells[2].Value.ToString());
                    var prix_total = Double.Parse(dtRow.Cells[3].Value.ToString());
                    var groupage = dtRow.Cells[4].Value.ToString();

                    requete = "INSERT INTO pro_det_fact (id_fact, design,prix,qte,prix_total,groupage,pourcentage,id) VALUES(@id_fact1,@design,@prix,@qte,@prix_total,@groupage,@pourcentage,@id)";
                    command.Parameters.Add(new MySqlParameter("id_fact1", facture.NumeroFacture));
                    command.Parameters.Add(new MySqlParameter("design", designation));
                    command.Parameters.Add(new MySqlParameter("qte", qte));
                    command.Parameters.Add(new MySqlParameter("prix", prix));
                    command.Parameters.Add(new MySqlParameter("prix_total", prix_total));
                    command.Parameters.Add(new MySqlParameter("groupage", groupage));
                    command.Parameters.Add(new MySqlParameter("pourcentage", facture.PartPatient));
                    command.Parameters.Add(new MySqlParameter("id", dtRow.Cells[5].Value.ToString()));
                    command.Transaction = transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                }
                transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("Facture no " + facture.NumeroFacture + " est enregistrée avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("L' enregistrement des données de la facture a échoué. Verifier si vous n'avez pas ajouter deux libellés sous le meme nom.", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        //public static bool ModifierUneFacture(Facture facture, DataGridView dgViewFacture, string montantPaye)
        //{
        //    var flag = false;
        //    try
        //    {
        //        connection.Open();
        //        transaction = connection.BeginTransaction();
        //        requete = "UPDATE facture_tbl SET  montant_fact=" + facture.MontantFactural +
        //        ",num_patient =" + facture.IdPatient + ", reste =" + facture.Reste + "  WHERE id_fact = " + facture.NumeroFacture;
        //        command.Transaction = transaction;
        //        command.CommandText = requete;
        //        command.ExecuteNonQuery();

        //        requete = "INSERT INTO det_paie_tbl (id_paie, date_paie ,montant)  VALUES(" + facture.NumeroFacture + ",@date_paie," + montantPaye + ")";
        //        command.Parameters.Add(new MySqlParameter("date_paie", DateTime.Now));
        //        command.Transaction = transaction;
        //        command.CommandText = requete;
        //        command.ExecuteNonQuery();

        //        foreach (DataGridViewRow dgrRow in dgViewFacture.Rows)
        //        {
        //            //var prix = Double.Parse(dgrRow.Cells[1].Value.ToString());
        //            //var designation = dgrRow.Cells[0].Value.ToString();
        //            //var qte = dgrRow.Cells[2].Value.ToString();
        //            //var prix_total = Double.Parse(dgrRow.Cells[1].Value.ToString());
        //            //requete = "UPDATE det_fact SET  prix = " + prix + ", qte = " + qte + ", prix_total = "+ prix_total
        //            //+ " WHERE design =@design AND id_fact = " + facture.NumeroFacture;
        //            //command.Parameters.Add(new MySqlParameter("design", designation));
        //            //command.CommandText = requete;
        //            // command.Transaction = transaction;
        //            //command.ExecuteNonQuery();
        //            //command.Parameters.Clear();
        //        }
        //        transaction.Commit();
        //        flag = true;
        //        MonMessageBox.ShowBox("Données de facture no " + facture.NumeroFacture + " sont modifiées avec succés", "Affirmation", "affirmation.png");
        //    }
        //    catch (Exception ex)
        //    {
        //        if (transaction != null)
        //        {
        //            transaction.Rollback();
        //        }
        //        MonMessageBox.ShowBox("L' enregistrement de la facture a échoué", "Erreur", ex, "erreur.png");
        //    }
        //    finally
        //    {
        //        connection.Close();
        //        command.Parameters.Clear();
        //    }
        //    return flag;
        //}

        public static bool ModifierUneFacture(Facture facture, DataGridView dgViewFacture, string montantPaye)
        {
            var flag = false;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "UPDATE facture_tbl SET  montant_fact=" + facture.MontantFactural +
                ",num_patient =" + facture.IdPatient + ", reste =" + facture.Reste + ", sub =@sub  WHERE id_fact = " + facture.NumeroFacture;
                command.Parameters.Add(new MySqlParameter("date_fact", facture.DateFacture));
                command.Parameters.Add(new MySqlParameter("sub", montantPaye + "-" + DateTime.Now));
                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                requete = "INSERT INTO det_paie_tbl (id_paie, date_paie ,montant)  VALUES(" + facture.NumeroFacture + ",@date_paie," + montantPaye + ")";
                command.Parameters.Add(new MySqlParameter("date_paie", DateTime.Now));
                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                foreach (DataGridViewRow dgrRow in dgViewFacture.Rows)
                {
                    var prix = Double.Parse(dgrRow.Cells[1].Value.ToString());
                    var designation = dgrRow.Cells[0].Value.ToString();
                    var qte = dgrRow.Cells[2].Value.ToString();
                    var prix_total = Double.Parse(dgrRow.Cells[1].Value.ToString());
                    requete = "UPDATE det_fact SET  prix = " + prix + ", qte = " + qte + ", prix_total = " + prix_total
                    + " WHERE design =@design AND id_fact = " + facture.NumeroFacture;
                    command.Parameters.Add(new MySqlParameter("design", designation));
                    command.CommandText = requete;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("Données de facture no " + facture.NumeroFacture + " sont modifiées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("L' enregistrement de la facture a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool ModifierUneFacture(int facture)
        {
            var flag = false;
            try
            {
                connection.Open();
                requete = "SELECT * FROM facture_tbl WHERE id_fact = " + facture;
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    requete = "UPDATE facture_tbl SET  sub =@sub  WHERE id_fact = " + facture;

                    command.Parameters.Add(new MySqlParameter("sub", null));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                } reader.Close();
                flag = true;
                MonMessageBox.ShowBox("Données de facture no " + facture + " sont modifiées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de la facture a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool SupprimerUneFacture(int numeroFacture, string libelle, double montantFactural, double montant)
        {
            var flag = false;
            try
            {

                connection.Open();
                transaction = connection.BeginTransaction();

                requete = "DELETE FROM det_fact  WHERE design = @design AND id_fact = " + numeroFacture;
                command.Parameters.Add(new MySqlParameter("design", libelle));
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                requete = "UPDATE facture_tbl SET montant_fact=" + montantFactural +
                    " WHERE id_fact = " + numeroFacture;
                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                requete = "UPDATE det_paie_tbl SET montant= montant - " + montant +
                        " WHERE id_paie = " + numeroFacture;
                command.Transaction = transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("Données retiréés de la facture avec succés", "Affirmation", "affirmation.png");

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("L' enregistrement de la facture a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool SupprimerUneFacture(int numeroFacture)
        {
            var flag = false;
            try
            {

                connection.Open();

                requete = "DELETE FROM facture_tbl  WHERE  id_fact = " + numeroFacture;
                command.CommandText = requete;
                command.ExecuteNonQuery();
                flag = true;
                MonMessageBox.ShowBox("Données de facture no " + numeroFacture + " sont supprimées avec succés", "Affirmation", "affirmation.png");

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression de la facture a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static List<Facture> ListeDesFactures()
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                    "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom,facture_tbl.reste FROM facture_tbl INNER JOIN" +
                    " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON " +
                    " facture_tbl.num_patient = patient_tbl.id ORDER BY facture_tbl.id_fact DESC ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listefacture;
        }

        public static List<Facture> ListeDesFacturesParEmploye(string numeroEmpl)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                    "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom,facture_tbl.reste FROM facture_tbl INNER JOIN" +
                    " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON " +
                    " facture_tbl.num_patient = patient_tbl.id WHERE facture_tbl.num_empl = '" + numeroEmpl +
                    "' ORDER BY facture_tbl.id_fact DESC";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listefacture;
        }

        public static List<Facture> ListeDesFactures(string numeroEmpl, DateTime date1, DateTime date2)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                    "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom,facture_tbl.reste,facture_tbl.sub FROM facture_tbl INNER JOIN" +
                    " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON " +
                    " facture_tbl.num_patient = patient_tbl.id WHERE empl_tbl.num_empl LIKE '" + numeroEmpl + "%' AND facture_tbl.date_fact >=@date1 " +
                    " AND facture_tbl.date_fact <@date2 ORDER BY facture_tbl.id_fact DESC ";
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    facture.Sub = !reader.IsDBNull(9) ? reader.GetString(9) : null;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listefacture;
        }

        public static List<Facture> ListeDesFacturesImpayees(string numeroEmpl, DateTime date1, DateTime date2)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                    "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom,facture_tbl.reste FROM facture_tbl INNER JOIN" +
                    " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON " +
                    " facture_tbl.num_patient = patient_tbl.id WHERE empl_tbl.num_empl LIKE '" + numeroEmpl + "%' AND facture_tbl.date_fact >=@date1 " +
                    " AND facture_tbl.date_fact <@date2 AND facture_tbl.reste >0 ORDER BY facture_tbl.id_fact DESC ";
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listefacture;
        }

        public static List<Facture> ListeDesFactures(string nomPatient)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                    "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom,facture_tbl.reste FROM facture_tbl INNER JOIN" +
                    " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON " +
                    " facture_tbl.num_patient = patient_tbl.id WHERE  patient_tbl.nom LIKE '%" + nomPatient + "%' ORDER BY facture_tbl.id_fact DESC ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listefacture;
        }

        public static List<Facture> ListeDesFactures(string nomPatient, string emplo)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                    "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom,facture_tbl.reste FROM facture_tbl INNER JOIN" +
                    " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON " +
                    " facture_tbl.num_patient = patient_tbl.id WHERE  patient_tbl.nom LIKE '%" + nomPatient +
                    "%' AND empl_tbl.nom_empl = '" + emplo + "' ORDER BY facture_tbl.id_fact DESC ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listefacture;
        }

        public static List<Facture> ListeDesFacturesParID(int ID)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                    "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom,facture_tbl.reste FROM facture_tbl INNER JOIN" +
                    " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON " +
                    " facture_tbl.num_patient = patient_tbl.id WHERE  facture_tbl.id_fact = " + ID;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listefacture;
        }

        public static DataTable ListeDesFacturesDT(DateTime dtp, string numEmpl)
        {

            try
            {
                requete = "SELECT * FROM facture_tbl WHERE  num_empl LIKE '%" + numEmpl + "%' AND date_fact >= @date1 AND date_fact < @date2 ORDER BY date_fact DESC ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dtp.Date));
                command.Parameters.Add(new MySqlParameter("date2", dtp.Date.AddDays(1)));
                connection.Open();
                var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static List<Facture> ListeDesFactures(int ID, string emplo)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                    "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom,facture_tbl.reste FROM facture_tbl INNER JOIN" +
                    " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON " +
                    " facture_tbl.num_patient = patient_tbl.id WHERE  facture_tbl.id_fact = " + ID +
                    " AND empl_tbl.nom_empl = '" + emplo + "' ORDER BY facture_tbl.id_fact DESC ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listefacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listefacture;
        }

        public static List<Facture> ListeDesFacturesParPatient(int IdPatient, DateTime date1, DateTime date2)
        {
            var listefacture = new List<Facture>();
            try
            {
                requete = "SELECT det_fact.design, det_fact.prix, det_fact.qte, det_fact.prix_total, facture_tbl.num_patient, facture_tbl.date_fact " +
                        " FROM patient_tbl INNER JOIN facture_tbl ON patient_tbl.id = facture_tbl.num_patient INNER JOIN det_fact ON facture_tbl.id_fact " +
                       " = det_fact.id_fact WHERE  facture_tbl.num_patient = " + IdPatient + " AND facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact <@date2";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(0);
                    facture.Quantite = reader.GetInt32(2);
                    facture.Prix = reader.GetDouble(1);
                    facture.PrixTotal = reader.GetDouble(3);
                    listefacture.Add(facture);
                }
                return listefacture;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static List<Facture> DetailsDesFactures(int numFacture)
        {
            var listeFacture = new List<Facture>();
            try
            {
                requete = "SELECT * FROM det_fact WHERE id_fact = " + numFacture;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(1);
                    facture.Prix = reader.GetDouble(2);
                    facture.Quantite = reader.GetInt32(3);
                    facture.PrixTotal = reader.GetDouble(4);

                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);

            }
            finally
            {
                connection.Close();
            }
            return listeFacture;
        }

        public static List<Facture> DetailsDesFacturesGrouperParCarnet(DateTime date1, DateTime date2)
        {
            var listeFacture = new List<Facture>();
            try
            {
                requete = "SELECT det_fact.design, det_fact.prix, SUM(det_fact.qte), SUM(det_fact.qte) * det_fact.prix " +
                        " FROM facture_tbl INNER JOIN det_fact ON facture_tbl.id_fact = det_fact.id_fact WHERE (det_fact.design" +
                        " LIKE '%CARNET%') AND (facture_tbl.date_fact >= @date1) AND (facture_tbl.date_fact < @date2) " +
                        " GROUP BY det_fact.design, det_fact.prix, det_fact.qte";
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(1);
                    facture.Prix = reader.GetDouble(2);
                    facture.Quantite = reader.GetInt32(3);
                    facture.PrixTotal = reader.GetDouble(4);
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);

            }
            finally
            {
                connection.Close();
            }
            return listeFacture;
        }

        public static List<Facture> ListeDesFactures(int numFacture)
        {
            var listeFacture = new List<Facture>();
            try
            {
                requete = "SELECT facture_tbl.id_fact, facture_tbl.date_fact, facture_tbl.montant_fact, facture_tbl.num_patient," +
                 "facture_tbl.num_empl, empl_tbl.nom_empl, patient_tbl.nom, patient_tbl.prenom, facture_tbl.reste FROM facture_tbl INNER JOIN" +
                 " empl_tbl ON facture_tbl.num_empl = empl_tbl.num_empl INNER JOIN patient_tbl ON facture_tbl.num_patient" +
             "= patient_tbl.id WHERE facture_tbl.id_fact = " + numFacture;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.DateFacture = reader.GetDateTime(1);
                    facture.MontantFactural = reader.GetDouble(2);
                    facture.IdPatient = reader.GetInt32(3);
                    facture.Patient = reader.GetString(6) + " " + reader.GetString(7);
                    facture.NumeroEmploye = reader.GetString(4);
                    facture.NomEmploye = reader.GetString(5);
                    facture.Reste = !reader.IsDBNull(8) ? reader.GetDouble(8) : 0.0;
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeFacture;
        }

        public static DataTable TableFacture(string query, DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {
                command.CommandText = query;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("TableFacture", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ExecuterUneRequeteSelect(string query)
        {
            var dt = new DataTable();
            try
            {
                command.CommandText = query;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("TableFacture", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }
        public static void  ExecuterUneRequete(string query)
        {
            var dt = new DataTable();
            try
            {
                command.CommandText = query;
                connection.Open();
                 command.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                //MonMessageBox.ShowBox("TableFacture", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static DataTable TablePaiement(int numFacture)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT SUM(montant) FROM det_paie_tbl WHERE id_paie = " + numFacture;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Table paiement", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable TableDetailPaiement(int numFacture)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT * FROM det_paie_tbl WHERE id_paie = " + numFacture;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Table paiement", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static List<Facture> ListeDesLibelles()
        {
            var listeFacture = new List<Facture>();
            try
            {
                requete = "SELECT * FROM grp_tbl ORDER BY id";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(1);
                    facture.Sub = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeFacture;
        }

        public static List<Facture> ListeDesLibellesDistingues()
        {
            var listeFacture = new List<Facture>();
            try
            {
                requete = "SELECT DISTINCT description FROM grp_tbl ORDER BY id";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(0);
                    //facture.Sub = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeFacture;
        }

        public static List<Facture> ListeDesLibelles(int id)
        {
            var listeFacture = new List<Facture>();
            try
            {
                requete = "SELECT * FROM grp_tbl WHERE id =" + id;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(1);
                    facture.Sub = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeFacture;
        }

        public static List<Facture> RecettesLaboratoire(DateTime date1, DateTime date2)
        {
            var listeFacture = new List<Facture>();
            try
            {
                var requete = "SELECT  det_fact.design, SUM(det_fact.qte), det_fact.prix,SUM(det_fact.prix_total*det_fact.pourcentage/100), det_fact.groupage  FROM det_fact INNER JOIN" +
                                                 " facture_tbl ON det_fact.id_fact = facture_tbl.id_fact WHERE det_fact.groupage  LIKE '%Laboratoire%' AND " +
                                                 " facture_tbl.date_fact >= @date1 AND facture_tbl.date_fact < @date2   AND facture_tbl.sub IS NULL  GROUP BY   det_fact.design";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(0);
                    facture.Quantite = reader.GetInt32(1);
                    facture.Prix = reader.GetDouble(2);
                    facture.PrixTotal = reader.GetDouble(3);
                    facture.Sub = !reader.IsDBNull(3) ? reader.GetString(4) : "";
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listeFacture;
        }
        public static List<Facture> RecettesAutresActesPourConventionne(string groupage,string entreprise, DateTime date1, DateTime date2)
        {
            var listeFacture = new List<Facture>();
            try
            {
                var requete = "SELECT  pro_det_fact.design, SUM(pro_det_fact.qte), SUM(pro_det_fact.prix_total), pro_det_fact.groupage  FROM pro_det_fact INNER JOIN" +
                              " pro_facture_tbl ON pro_det_fact.id_fact = pro_facture_tbl.id_fact INNER JOIN patient_tbl ON " +
                              " pro_facture_tbl.num_patient = patient_tbl.id WHERE pro_det_fact.groupage  = '"+ groupage+ "' AND " +
                              " pro_facture_tbl.date_fact >= @date1 AND pro_facture_tbl.date_fact < @date2  AND "+
                              " patient_tbl.entrep = @entrep GROUP BY   pro_det_fact.design";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                command.Parameters.Add(new MySqlParameter("entrep", entreprise));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(0);
                    facture.Quantite = reader.GetInt32(1);
                    facture.PrixTotal = reader.GetDouble(2);
                    facture.Sub = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listeFacture;
        }
        public static List<Facture> RecettesLaboratoireConventionne(DateTime date1, DateTime date2,string entreprise)
        {
            var listeFacture = new List<Facture>();
            try
            {
                var requete = "SELECT  pro_det_fact.design, SUM(pro_det_fact.qte), pro_det_fact.prix,SUM(pro_det_fact.prix_total), pro_det_fact.groupage  FROM pro_det_fact INNER JOIN" +
                             " pro_facture_tbl ON pro_det_fact.id_fact = pro_facture_tbl.id_fact INNER JOIN patient_tbl ON pro_facture_tbl.num_patient=" +
                            "patient_tbl.id WHERE pro_det_fact.groupage  LIKE '%Laboratoire%' AND patient_tbl.entrep=@entrep AND " +
                           " pro_facture_tbl.date_fact >= @date1 AND pro_facture_tbl.date_fact < @date2  GROUP BY   pro_det_fact.design,pro_det_fact.prix";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("entrep", entreprise));
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(0);
                    facture.Quantite = reader.GetInt32(1);
                    facture.Prix = reader.GetDouble(2);
                    facture.PrixTotal = reader.GetDouble(3);
                    facture.Sub = !reader.IsDBNull(3) ? reader.GetString(4) : "";
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listeFacture;
        }
        public static List<Facture> RecettesAutresActesTousConventionnes(string groupage, DateTime date1, DateTime date2)
        {
            var listeFacture = new List<Facture>();
            try
            {
                var requete = "SELECT  pro_det_fact.design, SUM(pro_det_fact.qte), SUM(pro_det_fact.prix_total), pro_det_fact.groupage  FROM pro_det_fact INNER JOIN" +
                              " pro_facture_tbl ON pro_det_fact.id_fact = pro_facture_tbl.id_fact INNER JOIN patient_tbl ON " +
                              " pro_facture_tbl.num_patient = patient_tbl.id WHERE pro_det_fact.groupage  = '" + groupage + "' AND " +
                              " pro_facture_tbl.date_fact >= @date1 AND pro_facture_tbl.date_fact < @date2  " +
                              "  GROUP BY   pro_det_fact.design";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(0);
                    facture.Quantite = reader.GetInt32(1);
                    facture.PrixTotal = reader.GetDouble(2);
                    facture.Sub = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listeFacture;
        }
        public static List<Facture> RecettesLaboratoirePourTousConventionnes(DateTime date1, DateTime date2)
        {
            var listeFacture = new List<Facture>();
            try
            {
                var requete = "SELECT  pro_det_fact.design, SUM(pro_det_fact.qte), pro_det_fact.prix,SUM(pro_det_fact.prix_total), pro_det_fact.groupage  FROM pro_det_fact INNER JOIN" +
                             " pro_facture_tbl ON pro_det_fact.id_fact = pro_facture_tbl.id_fact INNER JOIN patient_tbl ON pro_facture_tbl.num_patient=" +
                            "patient_tbl.id WHERE pro_det_fact.groupage  LIKE '%Laboratoire%' AND  " +
                           " pro_facture_tbl.date_fact >= @date1 AND pro_facture_tbl.date_fact < @date2  GROUP BY   pro_det_fact.design,pro_det_fact.prix";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Facture();
                    facture.Designation = reader.GetString(0);
                    facture.Quantite = reader.GetInt32(1);
                    facture.Prix = reader.GetDouble(2);
                    facture.PrixTotal = reader.GetDouble(3);
                    facture.Sub = !reader.IsDBNull(3) ? reader.GetString(4) : "";
                    listeFacture.Add(facture);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste facture", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listeFacture;
        }

        #endregion     /******************************* END CODE FACTURAL ***************************/

        /******************************* CODE OCCUPATION *****************************/
        #region Occupation
        public static bool EnregistrerUneOccupation(Occupation occupation, string etat)
        {

            var flag = false;
            try
            {
                connection.Open();
                //requete = "SELECT * FROM occup_tbl WHERE no_salle = " + occupation.NoSalle + " AND no_lit = " + 
                //    occupation.SalleLit+ " AND debut =@debut1  ";
                //  command.Parameters.Add(new MySqlParameter("debut1", occupation.DateEntree));
                //command.CommandText = requete;
                //var reader = command.ExecuteReader();
                //if (!reader.HasRows)
                //{
                    //reader.Close();
                if(etat=="1")
                {
                    requete = "INSERT INTO occup_tbl (id_patient,no_salle_lit,debut,depart,prix, nbre,prix_total,fact) VALUES" +
                        "(@id_patient,@no_salle,@debut,@depart,@prix, @nbre, @prix_total, @fact)";
                }else if(etat=="2")
                {
                    requete = "UPDATE  occup_tbl SET id_patient =@id_patient,no_salle_lit = @no_salle,debut=@debut,depart=@depart" +
                        ",prix=@prix, nbre=@nbre, prix_total=@prix_total WHERE id = " + occupation.NumeroOccupation;
                }
                    command.Parameters.Add(new MySqlParameter("id_patient", occupation.IdPatient));
                    command.Parameters.Add(new MySqlParameter("no_salle", occupation.NoSalle));
                    command.Parameters.Add(new MySqlParameter("debut", occupation.DateEntree));
                    command.Parameters.Add(new MySqlParameter("depart", occupation.DateSortie));
                    command.Parameters.Add(new MySqlParameter("no_lit", occupation.SalleLit));
                    command.Parameters.Add(new MySqlParameter("prix", occupation.Prix));
                    command.Parameters.Add(new MySqlParameter("prix_total", occupation.PrixTotal));
                    command.Parameters.Add(new MySqlParameter("nbre", occupation.NombreJour));
                    command.Parameters.Add(new MySqlParameter("fact", false));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    flag = true;
                    MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");

                //}
                //else
                //{
                //    MonMessageBox.ShowBox("Lit no " +occupation.SalleLit + " de la salle "+ occupation.NoSalle+" est deja  occupé.", "Erreur", "erreur.png");
                //}
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement de l' ocuupation a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

       public static void SupprimerUneOccupation(int numeroOccupation)
        {
            try
            {
                requete = "DELETE FROM occup_tbl WHERE id = " + numeroOccupation;
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de l'occupation supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données de l'occupation a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Occupation> ListeDesOccupations()
        {
            var listeOccupation = new List<Occupation>();
            try
            {
                requete = "SELECT occup_tbl.id, occup_tbl.id_patient, occup_tbl.no_salle_lit, occup_tbl.debut, occup_tbl.depart,"+
                    "  occup_tbl.prix,occup_tbl.nbre, occup_tbl.prix_total, salle_tbl.salle_lit, patient_tbl.nom, patient_tbl.prenom FROM occup_tbl INNER JOIN" +
                    " patient_tbl ON occup_tbl.id_patient = patient_tbl.id INNER JOIN salle_tbl ON occup_tbl.no_salle_lit=salle_tbl.id ORDER BY occup_tbl.id DESC";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var occup = new Occupation();
                    occup.NumeroOccupation = reader.GetInt32(0);
                    occup.IdPatient = reader.GetInt32(1);
                    occup.NoSalle = reader.GetInt32(2);
                    occup.DateEntree = reader.GetDateTime(3);
                    occup.DateSortie = reader.GetDateTime(4);
                    occup.Prix = reader.GetDouble(5);
                    occup.NombreJour = reader.GetInt32(6);
                    occup.PrixTotal = reader.GetDouble(7);
                    occup.SalleLit = reader.GetString(8);
                    occup.Patient = reader.GetString(9) + " " + reader.GetString(10);
                    listeOccupation.Add(occup);
                    
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste occupation", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeOccupation;
        }

        public static List<Occupation> ListeDesOccupations(string patient)
        {
            var listeOccupation = new List<Occupation>();
            try
            {
                requete = "SELECT occup_tbl.id, occup_tbl.id_patient, occup_tbl.no_salle, occup_tbl.debut, occup_tbl.depart," +
                    " occup_tbl.no_lit, occup_tbl.prix, patient_tbl.nom, patient_tbl.prenom FROM occup_tbl INNER JOIN" +
                    " patient_tbl ON occup_tbl.id_patient = patient_tbl.id WHERE patient_tbl.nom LIKE '%" + patient
                    + "%' ORDER BY patient_tbl.nom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var idPatient = reader.GetInt32(1);
                    var numSalle = reader.GetInt32(2);
                    var debut = reader.GetDateTime(3);
                    var depart = reader.GetDateTime(4);
                    var noLit = reader.GetInt32(5);
                    var prix = reader.GetDouble(6);
                    var nomPatient = reader.GetString(7) + " " + reader.GetString(8);
                    //var occupation = new Occupation(id, idPatient, numSalle, debut, depart, noLit, prix, nomPatient);
                    //listeOccupation.Add(occupation);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste occupation", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeOccupation;
        }

        public static List<Occupation> ListeDesOccupations(DateTime dateOccup)
        {
            var listeOccupation = new List<Occupation>();
            try
            {
                requete = "SELECT occup_tbl.id, occup_tbl.id_patient, occup_tbl.no_salle, occup_tbl.debut, occup_tbl.depart," +
                   " occup_tbl.no_lit, occup_tbl.prix, patient_tbl.nom, patient_tbl.prenom FROM occup_tbl INNER JOIN" +
                   " patient_tbl ON occup_tbl.id_patient = patient_tbl.id WHERE occup_tbl.debut >=@debut ORDER BY occup_tbl.id DESC";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("debut", dateOccup));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var idPatient = reader.GetInt32(1);
                    var numSalle = reader.GetInt32(2);
                    var debut = reader.GetDateTime(3);
                    var depart = reader.GetDateTime(4);
                    var noLit = reader.GetInt32(5);
                    var prix = reader.GetDouble(6);
                    var nomPatient = reader.GetString(7) + " " + reader.GetString(8);
                    //var occupation = new Occupation(id, idPatient, numSalle, debut, depart, noLit, prix, nomPatient);
                    //listeOccupation.Add(occupation);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste occupation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeOccupation;
        }

        public static List<Occupation> ListeDesOccupationsParPatient(int idPatient)
        {
            var listeOccupation = new List<Occupation>();
            try
            {
                requete = "SELECT * FROM occup_tbl WHERE id_patient =" + idPatient ;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var numSalle = reader.GetInt32(2);
                    var debut = reader.GetDateTime(3);
                    var depart = reader.GetDateTime(4);
                    var noLit = reader.GetInt32(5);
                    var prix = reader.GetDouble(6);
                    //var occupation = new Occupation(id, idPatient, numSalle, debut, depart, noLit, prix);
                    //listeOccupation.Add(occupation);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste occupation", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeOccupation;
        }
        
        public static DataTable ListeHospitalisationDesEntreprise(int num, DateTime date1, DateTime date2)
        {
            var dt = new DataTable();
            try
            {

                requete = "SELECT patient_tbl.id, patient_tbl.nom, patient_tbl.prenom, occup_tbl.debut, occup_tbl.prix "+
                           ",patient_tbl.sc,occup_tbl.depart FROM patient_tbl INNER JOIN occup_tbl ON patient_tbl.id = occup_tbl.id_patient WHERE(patient_tbl.id" +
                           "= " + num + " AND occup_tbl.fact = @fact) AND (occup_tbl.debut >= @date1 AND occup_tbl.debut < @date2)";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("fact", true));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste Hospitalisation", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static bool EnregistrerUneSalle(Occupation occupation, string etat)
        {
            var flag = false;
            try
            {
                connection.Open();
                if (etat == "1")
                {
                   requete= "INSERT INTO salle_tbl (salle_lit, prix) VALUES(@salle_lit, @prix)";
                }
                else if (etat == "2")
                {
                    requete = "UPDATE  salle_tbl SET salle_lit = @salle_lit ,prix=@prix  WHERE id = " + occupation.NoSalle;
                }
                command.Parameters.Add(new MySqlParameter("salle_lit", occupation.SalleLit));
                command.Parameters.Add(new MySqlParameter("prix", occupation.Prix));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                flag = true;
               }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement des données a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static void SupprimerUneSalle(int id)
        {
            try
            {
                requete = "DELETE FROM salle_tbl WHERE id = " + id;
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de l'occupation supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données de l'occupation a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Occupation> ListeSalles()
        {
            var listeOccupation = new List<Occupation>();
            try
            {
                requete = "SELECT * FROM salle_tbl ORDER BY salle_lit";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var salle = new Occupation();
                    salle.NoSalle = reader.GetInt32(0);
                    salle.SalleLit= reader.GetString(1);
                    salle.Prix = reader.GetDouble(2);
listeOccupation.Add(salle);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste occupation", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeOccupation;
        }

        #endregion
        /***************************** FIN CODE OCCUPATION ***********************/

        /******************************* CODE PAIEMENT D UNE NTREPRISE *****************************/
        #region Paiement
        public static bool EnregistrerPaiement(Entreprise entreprise)
        {

            var flag = false;
            try
            {
                connection.Open();
                
                    requete = "INSERT INTO paie_entr_tbl (id_entrep,date_pai,montant,mode_pai,numCheque) VALUES" +
                        "(" + entreprise.NumeroEntreprise + ",@date_pai," + entreprise.Montant + ", '" + entreprise.ModePaiement + "', @numCheque)";
                    command.Parameters.Add(new MySqlParameter("date_pai", entreprise.DateAbonnement));
                    command.Parameters.Add(new MySqlParameter("numCheque", entreprise.Cheque));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    flag = true;
                    MonMessageBox.ShowBox("Données  enregistrées avec succés", "Affirmation", "affirmation.png");

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement des données a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool ModifierPaiement(Entreprise entreprise)
        {

            var flag = false;
            try
            {
                connection.Open();

                requete = "UPDATE paie_entr_tbl SET id_entrep = "+ entreprise.NumeroEntreprise +",date_pai = @date_pai"+
                ",montant="+ entreprise.Montant+",mode_pai = '" +entreprise.ModePaiement +"', numCheque=@numCheque WHERE id = "+ entreprise.IdPaiement;
                command.Parameters.Add(new MySqlParameter("date_pai", entreprise.DateAbonnement));
                command.Parameters.Add(new MySqlParameter("numCheque", entreprise.Cheque));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                flag = true;
                MonMessageBox.ShowBox("Données  enregistrées avec succés", "Affirmation", "affirmation.png");

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L' enregistrement des données a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static void SupprimerUnPaiement(int idPaie)
        {
            try
            {
                requete = "DELETE FROM paie_entr_tbl WHERE id = " + idPaie;
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de paiement numero "+ idPaie +" supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Entreprise> ListeDesPaiements()
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                requete = "SELECT paie_entr_tbl.id , paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai," +
                   " paie_entr_tbl.montant, entre_tbl.entreprise,paie_entr_tbl.mode_pai,paie_entr_tbl.numCheque FROM paie_entr_tbl INNER JOIN entre_tbl " +
                   " ON paie_entr_tbl.id_entrep = entre_tbl.id ORDER BY paie_entr_tbl.id";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var idEntrep = reader.GetInt32(1);
                    var datePai = reader.GetDateTime(2);
                    var montant = reader.GetDouble(3);
                    var entrep = reader.GetString(4);
                    var mode = reader.GetString(5);
                    var cheque = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    //var entreprise = new Entreprise(id, idEntrep, datePai, montant,entrep,mode,cheque);
                    //listeEntreprise.Add(entreprise);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeEntreprise;
        }

        public static List<Entreprise> ListeDesPaiements(int id )
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                requete = "SELECT paie_entr_tbl.id , paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai"+
                    " paie_entr_tbl.montant, entre_tbl.entreprise,paie_entr_tbl.mode_pai,paie_entr_tbl.numCheque FROM paie_entr_tbl INNER JOIN entre_tbl " +
                    " ON paie_entr_tbl.id = entre_tbl.id WHERE paie_entr_tbl.id =" + id;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //var id = reader.GetInt32(0);
                    var idEntrep = reader.GetInt32(1);
                    var datePai = reader.GetDateTime(2);
                    var montant = reader.GetDouble(3);
                    var entrep = reader.GetString(4);
                    var mode = reader.GetString(5);
                    var cheque = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    //var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
                    //listeEntreprise.Add(entreprise);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeEntreprise;
        }

        public static List<Entreprise> ListeDesPaiements(DateTime date)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                requete = "SELECT paie_entr_tbl.id , paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai" +
                    " paie_entr_tbl.montant, entre_tbl.entreprise,paie_entr_tbl.mode_pai,paie_entr_tbl.numCheque FROM paie_entr_tbl INNER JOIN entre_tbl " +
                    " ON paie_entr_tbl.id = entre_tbl.id WHERE paie_entr_tbl.date_pai =@date";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date", date));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var idEntrep = reader.GetInt32(1);
                    var datePai = reader.GetDateTime(2);
                    var montant = reader.GetDouble(3);
                    var entrep = reader.GetString(4);
                    var mode = reader.GetString(5);
                    var cheque = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    //var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
                    //listeEntreprise.Add(entreprise);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeEntreprise;
        }

        public static List<Entreprise> ListeDesPaiements(string entrep, DateTime date1 , DateTime date2)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                connection.Open();
                requete = "SELECT paie_entr_tbl.id, paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai, " +
                    "paie_entr_tbl.montant, paie_entr_tbl.mode_pai, entre_tbl.entreprise,paie_entr_tbl.numCheque FROM entre_tbl " +
                    "INNER JOIN paie_entr_tbl ON entre_tbl.id = paie_entr_tbl.id_entrep WHERE (entre_tbl.entreprise " +
                    "= 'CBT Consultant') AND (paie_entr_tbl.date_pai >= @date1 AND paie_entr_tbl.date_pai < @date2)";
               
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("entrep", entrep));
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
               
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var idEntrep = reader.GetInt32(1);
                    var datePai = reader.GetDateTime(2);
                    var montant = reader.GetDouble(3);
                    //var entrep = reader.GetString(4);
                    var mode = reader.GetString(5);
                    var cheque = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    //var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
                    //var entreprise = new Entreprise(id, idEntrep, datePai, montant,entrep,mode);
                    //listeEntreprise.Add(entreprise);
                }
                
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeEntreprise;
        }

        public static List<Entreprise> ListeDesPaiements(string entrep)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                requete = "SELECT paie_entr_tbl.id , paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai," +
                    " paie_entr_tbl.montant, entre_tbl.entreprise,paie_entr_tbl.mode_pai,paie_entr_tbl.numCheque FROM paie_entr_tbl INNER JOIN entre_tbl " +
                    " ON paie_entr_tbl.id = entre_tbl.id WHERE (entre_tbl.entreprise =@entrep) " +
                    " ORDER BY paie_entr_tbl.date_pai DESC";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("entrep", entrep));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var idEntrep = reader.GetInt32(1);
                    var datePai = reader.GetDateTime(2);
                    var montant = reader.GetDouble(3);
                    //var entrep = reader.GetString(4);
                    var mode = reader.GetString(5);
                    var cheque = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    //var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
                    //listeEntreprise.Add(entreprise);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeEntreprise;
        }

        #endregion
        /***************************** FIN CODE FACTURE ENTREPRISE ***********************/

        /****************************** CODE EMPLOYE ******************************/
        #region Employe_Entreprise
        // ajouter un nouveau Employee
        public static bool AjouterEmployeDuneEntreprise(EmployeEntreprise employee, Patient patient)
        {
            var flag = false;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                requete = "INSERT INTO patient_tbl (`nom`,`prenom`,`sexe`,`age`,`telephone`,`entrep`,`rhesus`,`sc`,`date_enre`,`adresse`,`nosocial`,`fonction`, alcool, tabac, drogue,couvert,mois,matricule)" +
                         " VALUES(@nom,@prenom,@sexe,@age,@telephone,@entrep,@rhesus,@sc,@date_enre,@adresse,@nosocial,@fonction,@alcool, @tabac, @drogue,@couvert,@mois,@matriculle)";

                command.Parameters.Add(new MySqlParameter("nom", patient.Nom));
                command.Parameters.Add(new MySqlParameter("prenom", patient.Prenom));
                command.Parameters.Add(new MySqlParameter("sexe", patient.Sexe));
                command.Parameters.Add(new MySqlParameter("age", patient.An));
                command.Parameters.Add(new MySqlParameter("telephone", patient.Telephone));
                command.Parameters.Add(new MySqlParameter("entrep", patient.NomEntreprise));
                command.Parameters.Add(new MySqlParameter("rhesus", patient.Rhesus));
                command.Parameters.Add(new MySqlParameter("adresse", patient.Adresse));
                command.Parameters.Add(new MySqlParameter("nosocial", patient.NumeroSocial));
                command.Parameters.Add(new MySqlParameter("fonction", patient.Fonction));
                command.Parameters.Add(new MySqlParameter("date_enre", patient.DateEnregistrement));
                command.Parameters.Add(new MySqlParameter("sc", patient.SousCouvert));
                command.Parameters.Add(new MySqlParameter("alcool", patient.Alcoolo));
                command.Parameters.Add(new MySqlParameter("tabac", patient.Tabagiste));
                command.Parameters.Add(new MySqlParameter("drogue", patient.Drogueur));
                command.Parameters.Add(new MySqlParameter("couvert", patient.Couvert));
                command.Parameters.Add(new MySqlParameter("mois", patient.Mois));
                command.Parameters.Add(new MySqlParameter("matriculle", patient.Matricule));
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                requete = "SELECT MAX(id) FROM patient_tbl";
                command.CommandText = requete;
                command.Transaction = transaction;
                var idPatient = (int)command.ExecuteScalar();

                requete = "INSERT INTO `emp_entr` (`nom`, `sexe`, `age`, `tele`, `id_entre`,`matricule`,`fonction`, `idPatient`)" +
                            " VALUES (@nomEmpl, @sexeEmpl, @ageEmpl, @teleEmpl, @id_entre,@matricule,@fonctionEmpl,@idPatient )";

                command.Parameters.Add(new MySqlParameter("nomEmpl", employee.Nom));
                command.Parameters.Add(new MySqlParameter("sexeEmpl", employee.Sexe));
                command.Parameters.Add(new MySqlParameter("ageEmpl", employee.Age));
                command.Parameters.Add(new MySqlParameter("teleEmpl", employee.Telephone));
                command.Parameters.Add(new MySqlParameter("id_entre", employee.IdEntreprise));
                command.Parameters.Add(new MySqlParameter("fonctionEmpl", employee.Fonction));
                command.Parameters.Add(new MySqlParameter("matricule", employee.Matricule));
                command.Parameters.Add(new MySqlParameter("idPatient", idPatient));
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                transaction.Commit();
                MonMessageBox.ShowBox("Nouveau employé a été inseré avec succés dans la base de données",
                    "Information Insertion", "affirmation.png");
                flag = true;

            }
            catch (Exception exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                flag = false;
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static void ModifierEmployeeEntreprise(EmployeEntreprise employee, Patient patient, string sousCouvert)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                string requete =
                    string.Format(
                        "UPDATE emp_entr SET nom = @nomEmpl, sexe = @sexeEmpl,matricule=@matriculeEmpl, fonction=@fonctionEmpl, age = @ageEmpl, tele = @teleEmpl , id_entre = @id_entre" +
                        " WHERE (id = '{0}')", employee.Numero);
                command.Parameters.Add(new MySqlParameter("nomEmpl", employee.Nom));
                command.Parameters.Add(new MySqlParameter("sexeEmpl", employee.Sexe));
                command.Parameters.Add(new MySqlParameter("ageEmpl", employee.Age));
                command.Parameters.Add(new MySqlParameter("teleEmpl", employee.Telephone));
                command.Parameters.Add(new MySqlParameter("id_entre", employee.IdEntreprise));
                command.Parameters.Add(new MySqlParameter("fonctionEmpl", employee.Fonction));
                command.Parameters.Add(new MySqlParameter("matriculeEmpl", employee.Matricule));
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                requete = "UPDATE patient_tbl SET  sc = @sc1  WHERE sc = @sc2 AND entrep=@entreprise1";
                command.Parameters.Add(new MySqlParameter("sc1", employee.Nom));
                command.Parameters.Add(new MySqlParameter("sc2", sousCouvert));
                command.Parameters.Add(new MySqlParameter("entreprise1", patient.NomEntreprise));
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                requete = "UPDATE patient_tbl SET `nom` = @nom,`prenom`=@prenom,`sexe`=@sexe,`age`=@age,matricule=@matriculle," +
                   "`telephone`=@telephone,`fonction`=@fonction, entrep = @entrep WHERE id = " + patient.NumeroPatient;
                command.Parameters.Add(new MySqlParameter("nom", patient.Nom));
                command.Parameters.Add(new MySqlParameter("prenom", patient.Prenom));
                command.Parameters.Add(new MySqlParameter("sexe", patient.Sexe));
                command.Parameters.Add(new MySqlParameter("age", patient.An));
                command.Parameters.Add(new MySqlParameter("telephone", patient.Telephone));
                command.Parameters.Add(new MySqlParameter("entrep", patient.NomEntreprise));
                command.Parameters.Add(new MySqlParameter("fonction", patient.Fonction));
                command.Parameters.Add(new MySqlParameter("matriculle", patient.Matricule));
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                transaction.Commit();
                MonMessageBox.ShowBox("données employé modifiées avec succés.", "Information modification",
                       "affirmation.png");

            }
            catch (Exception exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur modification employe", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        // supprimer les donnees du  Employee
        public static void SupprimerEmployeeEntreprise(int employeeId)
        {

            try
            {
                connection.Open();
                string requete = string.Format("DELETE FROM emp_entr WHERE id = {0} ", employeeId);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox(
                    "Données de l'employé no " + employeeId + " ont été supprimées de la base de données",
                    "Information suppression", "affirmation.png");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("la suppression des données a échoué", "Erreur suppression employe", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        //liste des employe
        public static List<EmployeEntreprise> ListeDesEmployeesEntreprise()
        {
            var list = new List<EmployeEntreprise>();

            try
            {
                string requete = "SELECT * FROM emp_entr ORDER BY nom ";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var employe = new EmployeEntreprise();
                    employe.Numero = reader.GetInt32(0);
                    employe.Nom = reader.GetString(1);
                    employe.Sexe = reader.GetString(2);
                    employe.Age = reader.GetString(3);
                    employe.Telephone = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    employe.IdEntreprise = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    employe.Matricule = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    employe.Fonction = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    employe.IdPatient = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    list.Add(employe);
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste employe", exception);
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        //liste des employe par parametre
        public static List<EmployeEntreprise> ListeDesEmployeesEntreprise(int idEntrep)
        {
            var list = new List<EmployeEntreprise>();

            try
            {
                var requete = "SELECT * FROM emp_entr WHERE id_entre = " + idEntrep + " ORDER BY nom";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var employe = new EmployeEntreprise();
                    employe.Numero = reader.GetInt32(0);
                    employe.Nom = reader.GetString(1);
                    employe.Sexe = reader.GetString(2);
                    employe.Age = reader.GetString(3);
                    employe.Telephone = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    employe.IdEntreprise = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    employe.Matricule = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    employe.Fonction = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    employe.IdPatient = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    list.Add(employe);
                }
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        //liste des employe par parametre
        public static List<EmployeEntreprise> ListeDesEmployeesEntreprise(int idEntrep, string nomEmpl)
        {
            var list = new List<EmployeEntreprise>();

            try
            {
                var requete = "SELECT * FROM emp_entr WHERE id_entre = " + idEntrep + " AND nom LIKE '%" + nomEmpl + "%' ORDER BY nom";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var employe = new EmployeEntreprise();
                    employe.Numero = reader.GetInt32(0);
                    employe.Nom = reader.GetString(1);
                    employe.Sexe = reader.GetString(2);
                    employe.Age = reader.GetString(3);
                    employe.Telephone = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    employe.IdEntreprise = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    employe.Matricule = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    employe.Fonction = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    employe.IdPatient = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    list.Add(employe);
                }
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        //liste des employe par parametre
        public static List<EmployeEntreprise> ListeDesEmployeesEntreprise(string param)
        {
            var list = new List<EmployeEntreprise>();

            try
            {
                var requete = "SELECT * FROM emp_entr WHERE  nom = @nom";
                connection.Open();
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("nom", param));
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var employe = new EmployeEntreprise();
                    employe.Numero = reader.GetInt32(0);
                    employe.Nom = reader.GetString(1);
                    employe.Sexe = reader.GetString(2);
                    employe.Age = reader.GetString(3);
                    employe.Telephone = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    employe.IdEntreprise = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    employe.Matricule = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    employe.Fonction = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    employe.IdPatient = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    list.Add(employe); ;
                }
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return list;
        }

        #endregion
        /****************************** FIN CODE EMPLOYE ******************************/

        /*********************************POUR IMPRESSION*******************************/
        public static DataTable InsererDansTemp(DataGridView dgvPatient)
        {
            var dt = new DataTable();
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                foreach (DataGridViewRow dgvRow in dgvPatient.Rows)
                {
                    var patient = dgvRow.Cells[2].Value.ToString();
                    requete = "INSERT INTO temp_tbl (nomPat) VALUES(@nomPat)";
                    command.Parameters.Add(new MySqlParameter("nomPat", patient));
                    command.Transaction = transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

                requete = "SELECT DISTINCT nomPat, COUNT(nomPat) FROM temp_tbl GROUP BY nomPat";
                command.Transaction = transaction;
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                dt.Load(reader);
                transaction.Commit();
                
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("Inserer Temp", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }
        // supprimer les donnees du temp
        public static void ViderTemps()
        {
            try
            {
                connection.Open();
                string requete = "DELETE FROM temp_tbl ";
                command.CommandText = requete;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }
        }

        /****************************** CODE POUR CERTIFICAT DE GROSSESSE ******************************/
        #region CerificatDeGrossesse
        // ajouter un nouveau Employee
        public static bool AjouterUnCertifiactDeGrossesse(CertificatDeGrossesse certificat)
        {
            var flag = false;
            try
            {
                connection.Open();
                requete = "INSERT INTO `certif_gross` ( `patiente`, `epoux`, `sage_femme`, `periode`, `date_ac`,`frais`)" +
                           " VALUES ( @patiente, @epoux, @sage_femme, @periode, @date_ac,@frais )";

               command.Parameters.Add(new MySqlParameter("patiente", certificat.IDPatiente));
                command.Parameters.Add(new MySqlParameter("epoux", certificat.Epoux));
                command.Parameters.Add(new MySqlParameter("sage_femme", certificat.SageFemme));
                command.Parameters.Add(new MySqlParameter("periode", certificat.Periode));
                command.Parameters.Add(new MySqlParameter("date_ac", certificat.DateAccouchement));
                command.Parameters.Add(new MySqlParameter("frais", certificat.FraisCertificat));
                command.CommandText = requete;
                command.ExecuteNonQuery();

                MonMessageBox.ShowBox("Nouveau certificat de grossesse a été inseré avec succés dans la base de données",
                    "Information Insertion", "affirmation.png");
                flag = true;

            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        // modifier donnees certificat
        public static bool ModifierUnCertificatDeGrossesse(CertificatDeGrossesse certificat)
        {
            try
            {
                connection.Open();
                var requete = "UPDATE  `certif_gross` SET `patiente` = @patiente, `epoux`=@epoux, `sage_femme`=@sage_femme, " +
                    "`periode` =@periode, `date_ac`=@date_ac, frais=@frais WHERE id = @id ";

                command.Parameters.Add(new MySqlParameter("id", certificat.ID));
                command.Parameters.Add(new MySqlParameter("patiente", certificat.IDPatiente));
                command.Parameters.Add(new MySqlParameter("epoux", certificat.Epoux));
                command.Parameters.Add(new MySqlParameter("sage_femme", certificat.SageFemme));
                command.Parameters.Add(new MySqlParameter("periode", certificat.Periode));
                command.Parameters.Add(new MySqlParameter("date_ac", certificat.DateAccouchement));
                command.Parameters.Add(new MySqlParameter("frais", certificat.FraisCertificat));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("données de cerificat de grossesse modifiées avec succés.", "Information modification",
                       "affirmation.png");
                return true;

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur modification employe", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        // supprimer les donnees du  Employee
        public static bool  SupprimerUnCertficatDeGrossesse(int ID)
        {

            try
            {
                connection.Open();
                string requete = string.Format("DELETE FROM certif_gross WHERE id = {0} ", ID);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox(
                    "Données de certificat de grossesse numéro  " + ID + " ont été supprimées de la base de données",
                    "Information suppression", "affirmation.png");
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("la suppression des données a échoué", "Erreur suppression employe", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        //liste des employe
        public static List<CertificatDeGrossesse> ListeDesCertificatDeGrossesse()
        {
            var list = new List<CertificatDeGrossesse>();

            try
            {
                string requete = "SELECT * FROM certif_gross ORDER BY id DESC ";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var certif = new CertificatDeGrossesse();
                    certif.ID  = reader.GetInt32(0);
                   certif.IDPatiente = reader.GetInt32(1);
                    certif.Epoux = reader.GetString(2);
                    certif.Periode = reader.GetInt32(4);
                    certif .SageFemme  = reader.GetString(3) ;
                    certif.DateAccouchement = reader.GetDateTime(5);
                    certif.FraisCertificat  =!reader.IsDBNull(6) ? reader.GetDouble(6) : 0.0;
                    list.Add(certif);
                }
                return list;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste cerificat", exception);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

       
        #endregion
        /****************************** FIN CODE CERTIFICAT DE GROSSESSE ******************************/

        /****************************** CODE POUR CERTIFICAT DE ACTE DE NAISSANCE ******************************/
        #region CerificatDActeNaissance
        // ajouter un nouveau Employee
        public static bool AjouterUnCertificatNaissance(CertificatNaissance certificat)
        {
            var flag = false;
            try
            {
                connection.Open();
                requete = "INSERT INTO `certif_nai` ( `idPatient`, `sage_femme`, `date_nai`, `bebe`, `sexe`," +
                           " `poids`, `prof_mere`, `domicile1`, `date_nai1`, `epoux`, `prof_pere`, `domicile2`, `date_nai2`, `frais`   )"+
                           " VALUES ( @idPatient, @sage_femme, @date_nai, @bebe, @sexe, @poids,@prof_mere,@domicile1,@date_nai1, @epoux,@prof_pere,@domicile2,@date_nai2,@frais )";

                command.Parameters.Add(new MySqlParameter("idPatient", certificat.IDPatient));
                command.Parameters.Add(new MySqlParameter("epoux", certificat.Epoux));
                command.Parameters.Add(new MySqlParameter("sage_femme", certificat.SageFemme));
                command.Parameters.Add(new MySqlParameter("date_nai", certificat.NaissanceEnfant));
                command.Parameters.Add(new MySqlParameter("bebe", certificat.BeBe));
                command.Parameters.Add(new MySqlParameter("sexe", certificat.Sexe));
                command.Parameters.Add(new MySqlParameter("poids", certificat.Poids));
                command.Parameters.Add(new MySqlParameter("prof_mere", certificat.ProfesssionMere));
                command.Parameters.Add(new MySqlParameter("domicile1", certificat.DomicileMere));
                command.Parameters.Add(new MySqlParameter("date_nai1", certificat.NaissanceMere));
                command.Parameters.Add(new MySqlParameter("prof_pere", certificat.ProfessionEpoux));
                command.Parameters.Add(new MySqlParameter("domicile2", certificat.DomicileEpoux));
                command.Parameters.Add(new MySqlParameter("date_nai2", certificat.NaissanceEpoux));
                command.Parameters.Add(new MySqlParameter("frais", certificat.Frais));
                command.CommandText = requete;
                command.ExecuteNonQuery();

                MonMessageBox.ShowBox("Nouveau certificat de naissance a été inseré avec succés dans la base de données",
                    "Information Insertion", "affirmation.png");
                flag = true;

            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        // modifier donnees certificat
        public static bool ModifierUnCertificatDeNaissance(CertificatNaissance certificat)
        {
            try
            {
                connection.Open();
                requete = "UPDATE `certif_nai`  SET `idPatient` = @idPatient, `sage_femme`=@sage_femme, `date_nai` = date_nai," +
                    " `bebe`=@bebe, `sexe`=@sexe, `poids` = @poids, `prof_mere` = @prof_mere, `domicile1` = @domicile1," +
                    "`date_nai1`=@date_nai1, `epoux`=@epoux, `prof_pere` = @prof_pere, `domicile2`=@domicile2, " +
                    "`date_nai2`=@date_nai2, frais=@frais   WHERE `id` = @id";
                command.Parameters.Add(new MySqlParameter("id", certificat.ID));
                command.Parameters.Add(new MySqlParameter("idPatient", certificat.IDPatient));
                command.Parameters.Add(new MySqlParameter("epoux", certificat.Epoux));
                command.Parameters.Add(new MySqlParameter("sage_femme", certificat.SageFemme));
                command.Parameters.Add(new MySqlParameter("date_nai", certificat.NaissanceEnfant));
                command.Parameters.Add(new MySqlParameter("bebe", certificat.BeBe));
                command.Parameters.Add(new MySqlParameter("sexe", certificat.Sexe));
                command.Parameters.Add(new MySqlParameter("poids", certificat.Poids));
                command.Parameters.Add(new MySqlParameter("prof_mere", certificat.ProfesssionMere));
                command.Parameters.Add(new MySqlParameter("domicile1", certificat.DomicileMere));
                command.Parameters.Add(new MySqlParameter("date_nai1", certificat.NaissanceMere));
                command.Parameters.Add(new MySqlParameter("prof_pere", certificat.ProfessionEpoux));
                command.Parameters.Add(new MySqlParameter("domicile2", certificat.DomicileEpoux));
                command.Parameters.Add(new MySqlParameter("date_nai2", certificat.NaissanceEpoux));
                command.Parameters.Add(new MySqlParameter("frais", certificat.Frais));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("données de cerificat de naissance modifiées avec succés.", "Information modification",
                       "affirmation.png");
                return true;

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur modification employe", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        // supprimer les donnees du  Employee
        public static bool SupprimerUnCertficatDeNaissance(int ID)
        {

            try
            {
                connection.Open();
                string requete = string.Format("DELETE FROM certif_nai WHERE id = {0} ", ID);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox(
                    "Données de certificat de naissance numéro  " + ID + " ont été supprimées de la base de données",
                    "Information suppression", "affirmation.png");
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("la suppression des données a échoué", "Erreur suppression employe", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        //liste des employe
        public static List<CertificatNaissance> ListeDesCertificatDeNaissance()
        {
            var list = new List<CertificatNaissance>();

            try
            {
                string requete = "SELECT * FROM certif_nai ORDER BY id DESC ";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var certif = new CertificatNaissance();
                    certif.ID = reader.GetInt32(0);
                    certif.IDPatient = reader.GetInt32(1);
                    certif.SageFemme = reader.GetString(2);
                    certif.NaissanceEnfant= reader.GetDateTime(3);
                    certif.BeBe = reader.GetString(4);
                    certif.Sexe = reader.GetString(5);
                    certif.Poids = reader.GetFloat(6);
                    certif.ProfesssionMere =!reader.IsDBNull(7) ?  reader.GetString(7) : "";
                    certif.DomicileMere = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    certif.NaissanceMere = reader.GetDateTime(9);
                    certif.Epoux = reader.GetString(10);
                    certif.ProfessionEpoux = !reader.IsDBNull(11) ?  reader.GetString(11) : "";
                    certif.DomicileEpoux = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    certif.NaissanceEpoux = reader.GetDateTime(13);
                    certif.Frais = !reader.IsDBNull(14) ? reader.GetDouble(14) : 0.0;
                    list.Add(certif);
                }
                return list;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste cerificat", exception);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }


        #endregion
        /****************************** FIN CODE CERTIFICAT DE GROSSESSE ******************************/

        /****************************** CODE POUR CERTIFICAT DE DECES ******************************/
        #region CerificatDeDeces
        // ajouter un nouveau Employee
        public static bool AjouterUnCertificatDeCes(CertificatDeDeces  certificat)
        {
            var flag = false;
            try
            {
                connection.Open();
                requete = "INSERT INTO `certif_deces` ( `idPatient`, `idEmpl`, `date_hosp`, `date_deces`, `cause_deces`,`service`,`frais`)" +
                            " VALUES ( @idPatient, @idEmpl, @date_hosp, @date_deces, @cause_deces,@service,@frais)";
               command.Parameters.Add(new MySqlParameter("idPatient", certificat.IDPatient));;
                command.Parameters.Add(new MySqlParameter("idEmpl", certificat.IDEmploye));
                command.Parameters.Add(new MySqlParameter("service", certificat.Service)); 
                command.Parameters.Add(new MySqlParameter("date_hosp", certificat.DateHospitalisation));
                command.Parameters.Add(new MySqlParameter("date_deces", certificat.DateDeces));
                command.Parameters.Add(new MySqlParameter("cause_deces", certificat.CauseDeces));
                command.Parameters.Add(new MySqlParameter("frais", certificat.FraisCertificat));
                command.CommandText = requete;
                command.ExecuteNonQuery();

                MonMessageBox.ShowBox("Nouveau certificat de decès a été inseré avec succés dans la base de données",
                    "Information Insertion", "affirmation.png");
                flag = true;

            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        // modifier donnees certificat
        public static bool ModifierUnCertificatDeces(CertificatDeDeces certificat)
        {
            try
            {
                connection.Open();
                requete = "UPDATE `certif_deces` SET `idPatient` = @idPatient, `idEmpl`=@idEmpl, `date_hosp`=@date_hosp," +
                    " `date_deces`=@date_deces, `cause_deces` =@cause_deces, service = @service, frais=@frais WHERE id  = @id";
                command.Parameters.Add(new MySqlParameter("id", certificat.ID));
                command.Parameters.Add(new MySqlParameter("service", certificat.Service)); 
                command.Parameters.Add(new MySqlParameter("idPatient", certificat.IDPatient)); 
                command.Parameters.Add(new MySqlParameter("idEmpl", certificat.IDEmploye));
                command.Parameters.Add(new MySqlParameter("date_hosp", certificat.DateHospitalisation));
                command.Parameters.Add(new MySqlParameter("date_deces", certificat.DateDeces));
                command.Parameters.Add(new MySqlParameter("cause_deces", certificat.CauseDeces));
                command.Parameters.Add(new MySqlParameter("frais", certificat.FraisCertificat)); 
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("données de cerificat de decès modifiées avec succés.", "Information modification",
                       "affirmation.png");
                return true;

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur modification employe", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        // supprimer les donnees du  Employee
        public static bool SupprimerUnCertficatDeDeces(int ID)
        {

            try
            {
                connection.Open();
                string requete = string.Format("DELETE FROM certif_deces WHERE id = {0} ", ID);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox(
                    "Données de certificat de decès numéro  " + ID + " ont été supprimées de la base de données",
                    "Information suppression", "affirmation.png");
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("la suppression des données a échoué", "Erreur suppression employe", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        //liste des employe
        public static List<CertificatDeDeces> ListeDesCertificatDeDeces()
        {
            var list = new List<CertificatDeDeces>();

            try
            {
                string requete = "SELECT * FROM certif_deces ORDER BY id DESC ";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var certif = new CertificatDeDeces();
                    certif.ID = reader.GetInt32(0);
                    certif.IDPatient = reader.GetInt32(1);
                    certif.IDEmploye = reader.GetString(2);
                    certif.DateHospitalisation = reader.GetDateTime(3);
                    certif.DateDeces = reader.GetDateTime(4);
                    certif.CauseDeces = reader.GetString(5);
                    certif.Service = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    certif.FraisCertificat = !reader.IsDBNull(7) ? reader.GetDouble(7) : 0.0;
                    list.Add(certif);
                }
                return list;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste cerificat", exception);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }


        #endregion
        /****************************** FIN CODE CERTIFICAT DE GROSSESSE ******************************/

        
        #region Encaissement

        public static bool EnregistrerUnEncaissement(Encaissement encaissement)
        {
            try
            {
                connection.Open();
                var requete = "";
                if (encaissement.ID == 0)
                {
                    requete = "INSERT INTO `caisse_tbl`(`date`,mois,annee, `montant`, `id_tiers`,`avoir`, libelle,code,datePaiement) VALUES ("
                         + "@date,@mois,@annee, @montant,  @tiers,  @avoir,@libelle,@code,@datePaiement)";
                }
                else
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier ces données ?", "Confirmation","confirmation.png") == "1")
                    {
                        requete = "UPDATE `caisse_tbl` SET `date`=@date,mois=@mois,annee=@annee, " +
                            "`montant`=@montant, `id_tiers`=@tiers,`avoir`=  @avoir, code=@code, libelle=@libelle WHERE id =" + encaissement.ID;
                    }
                }
                command.Parameters.Add(new MySqlParameter("date", encaissement.Date));
                command.Parameters.Add(new MySqlParameter("code", encaissement.Code));
                command.Parameters.Add(new MySqlParameter("montant", encaissement.Montant));
                command.Parameters.Add(new MySqlParameter("tiers", encaissement.NumeroCaissier));
                command.Parameters.Add(new MySqlParameter("avoir", encaissement.Avoir));
                command.Parameters.Add(new MySqlParameter("mois", encaissement.Mois));
                command.Parameters.Add(new MySqlParameter("annee", encaissement.Exercice));
                command.Parameters.Add(new MySqlParameter("libelle", encaissement.Objet));
                command.Parameters.Add(new MySqlParameter("datePaiement", DateTime.Now));
                command.CommandText = requete;
                command.ExecuteNonQuery();

                MonMessageBox.ShowBox("L'enregistrement a été éffectuée avec succés", "Enregistrement","affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement a echoue", ex);
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }
        public static bool SupprimerUnEncaissement(int id)
        {
            try
            {
                connection.Open();
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données ?", "Supprimer","supprssion.png") == "1")
                {
                    string query = "DELETE FROM caisse_tbl WHERE id =" + id;
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données a echoue", "Supprimer", ex, "suppression.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        //liste des depenses
        public static List<Encaissement> ListeEncaissement(int annee)
        {
            var liste = new List<Encaissement>();
            try
            {
                connection.Open();
                string query = "SELECT caisse_tbl.* , empl_tbl.* FROM  caisse_tbl INNER JOIN empl_tbl ON caisse_tbl.id_tiers = empl_tbl.num_empl WHERE annee=" + annee;
                command.CommandText = query;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var enc = new Encaissement();
                    enc.ID = reader.GetInt32(0);
                    enc.Objet = reader.GetString(2);
                    enc.Date = reader.GetDateTime(1);
                    enc.Montant = reader.GetDouble(3);
                    enc.Mois = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    enc.Exercice = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    enc.Avoir = !reader.IsDBNull(6) ? reader.GetDouble(6) : .0;
                    enc.NumeroCaissier= !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    enc.Code = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    enc.DateEncaissment = !reader.IsDBNull(9) ? reader.GetDateTime(9) : DateTime.Now;
                    enc.NomCaissier = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    liste.Add(enc);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste depense", exception);
                return null;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static List<Encaissement> ListeEncaissementGroupeParJour(DateTime dateTime)
        {
            var liste = new List<Encaissement>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM  caisse_tbl WHERE datePaiement>=@date1 AND datePaiement<@date2";
                command.CommandText = query;
                command.Parameters.Add(new MySqlParameter("date1", dateTime.Date));
                command.Parameters.Add(new MySqlParameter("date2", dateTime.Date.AddHours(24)));
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var enc = new Encaissement();
                    enc.ID = reader.GetInt32(0);
                    enc.Objet = reader.GetString(2);
                    enc.Date = reader.GetDateTime(1);
                    enc.Montant = reader.GetDouble(3);
                    enc.Mois = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    enc.Exercice = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    enc.Avoir = !reader.IsDBNull(6) ? reader.GetDouble(6) : .0;
                    enc.NumeroCaissier = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    enc.Code = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    enc.DateEncaissment = !reader.IsDBNull(9) ? reader.GetDateTime(9) : DateTime.Now;
                    liste.Add(enc);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste depense", exception);
                return null;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

#endregion
    }
}
