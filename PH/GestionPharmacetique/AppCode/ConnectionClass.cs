using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GestionPharmacetique.AppCode
{
    public class ConnectionClass
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static MySqlTransaction _transaction = null;
        static string connectionString;
        static ConnectionClass()
        {
             connectionString =
                 @"server=192.168.1.211;user id=Hbebidja;database=;port=3306;password=Hbebidja2020;database=pharmdb";  
             //@"server=localhost;port=3306;user id=root;password=chris@2022;database=pharmdb";
            connection = new MySqlConnection(connectionString);
            command = new MySqlCommand("", connection);
        }

        public static  void Backup( string sFilePath)
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
                            MonMessageBox.ShowBox("Données importées avec succés","Affirmation","affirmaion.png");
                        }                    
                
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Restore", ex);
            }
        }

        public static void BackupDatabase(string location)
        {
            try
            {
                var database = connection.Database.ToString();
                var requete = "BACKUP DATABASE  " + database + "  TO DISK = '" + location + "\\" + "Database-" + DateTime.Now.Second + ".bak'";
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données sauvegardées avec succés", "Affirmation", "affirmation.png");
                connection.Close();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("back up files", ex);
            }
        }
   
         #region EMPLOYE_UTILISATEUR
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
                        "SELECT mot_de_passe FROM utilisateur WHERE nom_utilisateur = '{0}' ", nomUtil);
                    command.CommandText = query;
                    string dbMotPassword = command.ExecuteScalar().ToString().Trim();

                    if (dbMotPassword == motPasse)
                    {
                        query =
                            string.Format(
                                "SELECT utilisateur.type_utilisateur,  employe.nom_empl,employe.photos,employe.num_empl FROM (utilisateur INNER JOIN " +
                                " employe ON utilisateur.num_empl = employe.num_empl) WHERE utilisateur.nom_utilisateur = '{0}' ",
                                nomUtil);
                        command.CommandText = query;
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string type = reader.GetString(0).Trim();
                            string nomEmploy = reader.GetString(1).Trim();
                            string image = !reader.IsDBNull(2) ? reader.GetString(2).Trim() : "";
                            var numEmpl = reader.GetString(3);

                            utilisateur = new Utilisateur(nomUtil, motPasse, type, nomEmploy, numEmpl, image);
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
                MonMessageBox.ShowBox("l'authentification a échouée. Veuiller réessayer.", "Erreur d'authentification",
                    ex, "erreur.png");
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
                if (!string.IsNullOrEmpty(ancienMotPasse))
                {
                    connection.Open();
                    string query = string.Format("SELECT * FROM utilisateur WHERE mot_de_passe = '{0}' ", ancienMotPasse);
                    command.CommandText = query;
                    MySqlDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    int NbreMotPasse = dataTable.Rows.Count;
                    if (NbreMotPasse >= 1)
                    {

                        query = string.Format("UPDATE utilisateur SET mot_de_passe = '{0}'" +
                        " WHERE nom_utilisateur = '{1}'", nouveauMotPasse, utilisateur);
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        MonMessageBox.ShowBox("Mot de passe modifié avec succés", "Modification mode passe", "affirmation.png");
                    }
                    else
                    {
                        MonMessageBox.ShowBox("L'ancien mot de passe n'est pas correct.", "Erreur saisie", "erreur.png");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez remplir les champs pui reéssayer.", "Erreur saisie", "erreur.png");
                }
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
                string query = "SELECT utilisateur.num_utilisateur, utilisateur.nom_utilisateur, " +
                               "utilisateur.mot_de_passe, utilisateur.type_utilisateur,  employe.nom_empl,employe.photos FROM ( " +
                               " utilisateur INNER JOIN employe ON utilisateur.num_empl = employe.num_empl)";
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
            catch (Exception) { }
            finally
            {
                connection.Close();
            }
            return listeUtilisateur;
        }

        //mot de passe
        public static void ModifierMotDepasse(string nomUtil, string motDePasse)
        {
            try
            {
                string requete = "UPDATE utilisateur SET mot_de_passe = '" + motDePasse + "' WHERE nom_utilisateur = '" +
                                 nomUtil + "'";
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Modification mot de passe", exception);
            }
        }

        // supprimer les donnees de la livraison produit produit
        private static int _modifier;
        //liste des employe
        public static List<Employe> ListeDesEmployees()
        {
            List<Employe> list = new List<Employe>();

            try
            {
                string requete = "SELECT * FROM employe ORDER BY nom_empl ";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string num_emplo = reader.GetString(0);
                    string nom_empl = reader.GetString(1);
                    string Adddresse = reader.GetString(2);
                    string telephone1 = reader.GetString(3);
                    string telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    string email = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    string titre = reader.GetString(6);
                    string ville = reader.GetString(7);
                    string photo = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    Employe employe = new Employe(num_emplo, nom_empl, Adddresse, telephone1, telephone2, email, titre,
                        ville, photo);
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
                string requete = string.Format("SELECT * FROM employe WHERE  {0} = {1}", colonne, param);
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string num_emplo = reader.GetString(0);
                    string nom_empl = reader.GetString(1);
                    string Adddresse = reader.GetString(2);
                    string telephone1 = reader.GetString(3);
                    string telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    string email = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    string titre = reader.GetString(6);
                    string ville = reader.GetString(7);
                    string photo = !reader.IsDBNull(8) ? reader.GetString(8) : "";



                    Employe employe = new Employe(num_emplo, nom_empl, Adddresse, telephone1, telephone2, email, titre,
                        ville, photo);
                    list.Add(employe);
                }
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        // ajouter un nouveau Employee
        public static void EnregistrerUnEmploye(Employe employe, string typeUtilsateur)
        {
            try
            {
                connection.Open();
                string requete = "SELECT * FROM employe WHERE num_empl ='" + employe.NumMatricule + "'";
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable(); dt.Load(reader); reader.Close();
                int count = dt.Rows.Count;
                if (count < 1)
                {
                    requete = "INSERT INTO `employe` (`num_empl`, `nom_empl`, `Addresse`, `telephone1`, `telephone2`," +
                            "`email`, `titre`,`ville`) VALUES (@num_empl, @nom_empl, @Addresse, @telephone1, @telephone2, @email, @titre,@ville )";

                    command.Parameters.Add(new MySqlParameter("num_empl", employe.NumMatricule));
                    command.Parameters.Add(new MySqlParameter("nom_empl", employe.NomEmployee));
                    command.Parameters.Add(new MySqlParameter("Addresse", employe.Addresse));
                    command.Parameters.Add(new MySqlParameter("telephone1", employe.Telephone1));
                    command.Parameters.Add(new MySqlParameter("telephone2", employe.Telephone2));
                    command.Parameters.Add(new MySqlParameter("email", employe.Email));
                    command.Parameters.Add(new MySqlParameter("titre", employe.Titre));
                    command.Parameters.Add(new MySqlParameter("ville", employe.Ville));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                    requete =
                            string.Format(
                                "INSERT INTO `utilisateur` (`nom_utilisateur`, `mot_de_passe`, `type_utilisateur`," +
                                "`num_empl`) VALUES ('{0}','{1}','{2}','{3}')", employe.NomEmployee,
                                employe.NumMatricule.GetHashCode().ToString(), typeUtilsateur, employe.NumMatricule);
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                    MonMessageBox.ShowBox("Nouveau compte d'employé a été inseré avec succés dans la base de données",
                        "Information Insertion", "affirmation.png");
                }
                else
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier les données de cet employé?", "Confirmation", "confirmation.png") == "1")
                    {
                        requete =
                         string.Format(
                             "UPDATE employe SET nom_empl = @nom_empl, Addresse = @Addresse, telephone1 = @telephone1, telephone2 = @telephone2" +
                             ", email = @email, titre = @titre,  ville = @ville WHERE (num_empl = '{0}')", employe.NumMatricule);
                        command.Parameters.Add(new MySqlParameter("nom_empl", employe.NomEmployee));
                        command.Parameters.Add(new MySqlParameter("Addresse", employe.Addresse));
                        command.Parameters.Add(new MySqlParameter("telephone1", employe.Telephone1));
                        command.Parameters.Add(new MySqlParameter("telephone2", employe.Telephone2));
                        command.Parameters.Add(new MySqlParameter("email", employe.Email));
                        command.Parameters.Add(new MySqlParameter("titre", employe.Titre));
                        command.Parameters.Add(new MySqlParameter("ville", employe.Ville));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();

                        requete =
                             "UPDATE `utilisateur` SET `type_utilisateur`= '"+typeUtilsateur+"' WHERE num_empl ='"+employe.NumMatricule+"'";
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        MonMessageBox.ShowBox("Données  modifiées avec succés.", "Information modification",
                           "affirmation.png");
                    }
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
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
                string requete = string.Format("UPDATE employe SET photos ='{0}' WHERE (num_empl = '{1}')", image,
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
                string requete = string.Format("DELETE FROM employe WHERE num_empl = '{0}' ", employeeId);
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

        #endregion
      
        #region LIVRAISON_RETOUR_PRODUIT
        // enregistrer une vente
        public static bool CreerUneLivraison(Livraison livraison)
        {
            try
            {
                connection.Open();

                var requete = string.Format("INSERT INTO `detail_bl` (`date_cmd`, `num_fourn`,autre, etat,montant_fact) " +
                                            " VALUES (  @date_cmd, @num_fourn, @autre,@etat,@montant_fact)");
                    command.Parameters.Add(new MySqlParameter("date_cmd", livraison.DateCommande));
                    command.Parameters.Add(new MySqlParameter("num_fourn", livraison.NumFournisseur));
                    command.Parameters.Add(new MySqlParameter("montant_fact", livraison.MontantFactural));
                    command.Parameters.Add(new MySqlParameter("autre", livraison.AutresCharges));
                    command.Parameters.Add(new MySqlParameter("etat", 1));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                MonMessageBox.ShowBox("Nouvelle commande  creéé avec succés",
                    "Information Insertion", "affirmation.png");
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox( "Erreur d'insertion", exception);
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            };
        }
        public static int  DernierNumeroDUneLivraison()
        {
            try
            {
                connection.Open();
                var requete = string.Format("SELECT MAX(num_bl) FROM `detail_bl` ");
                command.CommandText = requete;
               return (int) command.ExecuteScalar();            
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Erreur d'insertion", exception);
                return 0;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            };
        }

        public static bool EnregistrerLivraison(Livraison livraison, DataGridView dgvLivraison,
            string etat, int rowIndex)
        {
            bool flag = false;
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();

                //inserer les details de livraison
                     var requete = string.Format("SELECT * FROM `detail_bl` WHERE  num_bl =@num_bll");
                command.Parameters.Add(new MySqlParameter("num_bll", livraison.NumeroCommande));
                command.CommandText = requete;
                command.Transaction = _transaction;
                var reader = command.ExecuteReader();
                var dt1 = new DataTable();
                dt1.Load(reader); reader.Close();
                if (dt1.Rows.Count > 0)
                {
                    livraison.Etat=Int32.Parse(dt1.Rows[0].ItemArray[5].ToString());
                }
                if (livraison.Etat <3)
                {
                    if (etat == "1")
                    {
                        requete = "SELECT COUNT(*) FROM bn_liv WHERE num_bl = '" + livraison.NumeroCommande + "'";
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                         reader = command.ExecuteReader();
                        var dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        livraison.ID = dgvLivraison.Rows.Count + 1;
                        if (livraison.Etat == 2)
                        {
                            requete = string.Format("INSERT INTO `cmd_tbl` (`num_bl`, `num_medi`, `prix_achat`,`quantite`,`prix_total`,id, lot, dateExpiration, qte_livre) VALUES ({0},'{1}', {2}, {3},{4},{5},@lot, @dateExpiration, {3})",
                                livraison.NumeroCommande, livraison.NumProduit, livraison.PrixAchat, livraison.QuantiteCommandee, livraison.PrixAchat * livraison.QuantiteCommandee, livraison.ID);
                        }
                        else if (livraison.Etat == 1)
                        {
                            requete = string.Format("INSERT INTO `cmd_tbl` (`num_bl`, `num_medi`, `prix_achat`,`quantite`,`prix_total`,id, lot, dateExpiration) VALUES ('{0}','{1}', {2}, {3},{4},{5},@lot, @dateExpiration)",
                                 livraison.NumeroCommande, livraison.NumProduit, livraison.PrixAchat, livraison.QuantiteCommandee, livraison.PrixAchat * livraison.QuantiteCommandee, livraison.ID);                     
                        }
                        command.CommandText = requete;
                        command.Parameters.Add(new MySqlParameter("lot", livraison.NoLot));
                        command.Parameters.Add(new MySqlParameter("dateExpiration", livraison.DateExpiration));
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();
                        
                        dgvLivraison.Rows.Add(
                            livraison.ID,
                            livraison.NumProduit,
                            livraison.Designation,
                                String.Format(elGR, "{0:0,0}", livraison.PrixAchat),
                                String.Format(elGR, "{0:0,0}", livraison.PrixVente),
                                String.Format(elGR, "{0:0,0}", livraison.QuantiteCommandee),
                                String.Format(elGR, "{0:0,0}", (livraison.PrixAchat * livraison.QuantiteCommandee)),
                                livraison.NoLot,
                                livraison.DateExpiration.ToShortDateString()
                                );

                    }
                    else if (etat == "2")
                    {
                        livraison.ID = Convert.ToInt32(dgvLivraison.Rows[rowIndex].Cells[0].Value.ToString());
                        if (livraison.Etat == 2)
                        {
                            requete = string.Format("UPDATE `cmd_tbl` SET  `prix_achat`={0},`qte_livre`={1},`prix_total`={3}, prix_achat ={2}, lot =@lot,  " +
                                " dateExpiration=@dateExpiration WHERE id = {4} AND num_medi=@num_medic AND num_bl=@num_livr",
                            livraison.PrixAchat, livraison.QuantiteCommandee, livraison.PrixAchat, livraison.PrixAchat * livraison.QuantiteCommandee, livraison.ID);
                        }
                        else if (livraison.Etat == 1)
                        {
                            requete = string.Format("UPDATE `cmd_tbl` SET  `prix_achat`={0},`quantite`={1},`prix_total`={3}, prix_achat ={2}, lot =@lot,  " +
                               " dateExpiration=@dateExpiration WHERE id = {4} AND num_medi=@num_medic AND num_bl=@num_livr",
                                livraison.PrixAchat, livraison.QuantiteCommandee, livraison.PrixAchat, livraison.PrixAchat * livraison.QuantiteCommandee, livraison.ID);
                        }
                        command.Parameters.Add(new MySqlParameter("num_medic", livraison.NumProduit));
                        command.Parameters.Add(new MySqlParameter("num_livr", livraison.NumeroCommande));
                        command.Parameters.Add(new MySqlParameter("lot", livraison.NoLot));
                        command.Parameters.Add(new MySqlParameter("dateExpiration", livraison.DateExpiration));
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();

                        dgvLivraison.Rows[rowIndex].Cells[3].Value = String.Format(elGR, "{0:0,0}", livraison.PrixAchat);
                        dgvLivraison.Rows[rowIndex].Cells[4].Value = String.Format(elGR, "{0:0,0}", livraison.PrixVente);
                        dgvLivraison.Rows[rowIndex].Cells[5].Value = String.Format(elGR, "{0:0,0}", livraison.QuantiteCommandee);
                        dgvLivraison.Rows[rowIndex].Cells[7].Value = String.Format(elGR, "{0:0,0}", livraison.NoLot);
                        dgvLivraison.Rows[rowIndex].Cells[6].Value = String.Format(elGR, "{0:0,0}", livraison.QuantiteCommandee * livraison.PrixAchat);
                        dgvLivraison.Rows[rowIndex].Cells[8].Value = String.Format(elGR, "{0:0,0}", livraison.DateExpiration.ToShortDateString());
                  
                    }
                    //requete = string.Format("UPDATE medicament SET prix_vente = @prix_vente,prix_achat = @prix_achat  WHERE (num_medi =@num_medi)");
                    //command.Parameters.Add(new MySqlParameter("num_medi", livraison.NumProduit));
                    //command.Parameters.Add(new MySqlParameter("prix_vente", livraison.PrixVente));
                    //command.Parameters.Add(new MySqlParameter("prix_achat", livraison.PrixAchat));
                    //command.CommandText = requete;
                    //command.Transaction = _transaction;
                    //command.ExecuteNonQuery();

                    _transaction.Commit();
                    flag = true;
                }
                else
                {
                    MonMessageBox.ShowBox("Commande deja validée", "Erreur", "erreur.png");
                    return false;
                }
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    flag = false;
                }
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static int  VerifierEtatLivraison(int numeroCommande)
        {
            try
            {
                connection.Open();
                var requete = string.Format("SELECT * FROM `detail_bl` WHERE  num_bl =@num_bll");
                command.Parameters.Add(new MySqlParameter("num_bll", numeroCommande));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                var dt1 = new DataTable();
                dt1.Load(reader); reader.Close();
                return Convert.ToInt32(dt1.Rows[0].ItemArray[5].ToString());
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox( "Erreur d'insertion", exception);
                return 0;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static bool ValiderLivraisonFournisseur(Livraison livraison, DataGridView dgvLivraison)
        {
            bool flag = false;
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();

                var requete = string.Format("SELECT * FROM `detail_bl` WHERE  num_bl =@num_bll");
                command.Parameters.Add(new MySqlParameter("num_bll", livraison.NumeroCommande));
                command.CommandText = requete;
                command.Transaction = _transaction;
                var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader); reader.Close();
                if (dt.Rows.Count > 0)
                {
                    livraison.Etat = Convert.ToInt32(dt.Rows[0].ItemArray[5].ToString());
                }
                if(livraison.Etat ==3)
                {
                    MonMessageBox.ShowBox("Cette commande a été deja validée", "Erreur", "erreur.png");
                    return false;
                }
                else if(livraison.Etat==1)
                { 
                    MonMessageBox.ShowBox("Veuillez transformer la commande en livraison avant de valider", "Erreur", "erreur.png");
                    return false;
                }
                 else
                {
                    requete = string.Format("UPDATE `detail_bl` SET etat = @etat , numFacture=@facture,date_bl=@date_bl WHERE num_bl =@num_bl");
                    command.Parameters.Add(new MySqlParameter("num_bl", livraison.NumeroCommande));
                    command.Parameters.Add(new MySqlParameter("etat", 3));
                    command.Parameters.Add(new MySqlParameter("facture", livraison.NumeroFacture));
                    command.Parameters.Add(new MySqlParameter("date_bl", livraison.DateLivraison));
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                    foreach (DataGridViewRow dgvRow in dgvLivraison.Rows)
                    {
                        livraison.NumProduit = dgvRow.Cells[1].Value.ToString();
                        livraison.QuantiteCommandee = (int)double.Parse(dgvRow.Cells[5].Value.ToString());
                        livraison.NoLot = dgvRow.Cells[7].Value.ToString();
                        livraison.PrixAchat = decimal.Parse(dgvRow.Cells[3].Value.ToString());
                        livraison.PrixVente =decimal.Parse(dgvRow.Cells[4].Value.ToString());
                        livraison.DateExpiration = DateTime.Parse(dgvRow.Cells[8].Value.ToString());
                        requete = string.Format(@"UPDATE stock SET grd_stock =grd_stock + {0} WHERE num_medi = '{1}'", livraison.QuantiteCommandee, livraison.NumProduit);
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();

                        requete = "UPDATE medicament SET  prix_achat = @prix_achat, prix_vente = @prix_vente, date_expiration =@date_expiration WHERE (num_medi ='" + livraison.NumProduit + "')";

                        command.Parameters.Add(new MySqlParameter("prix_achat", livraison.PrixAchat));
                        command.Parameters.Add(new MySqlParameter("prix_vente", livraison.PrixVente));
                        command.Parameters.Add(new MySqlParameter("date_expiration", livraison.DateExpiration));
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();


                        requete = "INSERT INTO lot_tbl (num_prod, lot, qte, `date`,idLiv) VALUES (@num_prodr, @lot, @qter, @dater,@idLiv)";
                        command.Parameters.Add(new MySqlParameter("num_prodr", livraison.NumProduit));
                        command.Parameters.Add(new MySqlParameter("lot", livraison.NoLot));
                        command.Parameters.Add(new MySqlParameter("qter", livraison.QuantiteCommandee));
                        command.Parameters.Add(new MySqlParameter("dater", livraison.DateExpiration));
                        command.Parameters.Add(new MySqlParameter("idLiv", livraison.NumeroCommande));
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    _transaction.Commit();
                    MonMessageBox.ShowBox("Commande validée avec succés et le stock est mis à jour  ", "Affirmation", "affirmation.png");
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    flag = false;
                }
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool TransformerLivraisonFournisseur(Livraison livraison, DataGridView dgvLivraison)
        {
            bool flag = false;
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();

                var requete = string.Format("SELECT * FROM `detail_bl` WHERE  etat = @etat1 AND num_bl =@num_bll");
                command.Parameters.Add(new MySqlParameter("etat1", 3));
                command.Parameters.Add(new MySqlParameter("num_bll", livraison.NumeroCommande));
                command.CommandText = requete;
                command.Transaction = _transaction;
                var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader); reader.Close();
                if (dt.Rows.Count == 0)
                {
                    requete = string.Format("UPDATE `detail_bl` SET etat = @etat  WHERE num_bl =@num_bl");
                    command.Parameters.Add(new MySqlParameter("num_bl", livraison.NumeroCommande));
                    command.Parameters.Add(new MySqlParameter("etat", 2));
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                    foreach (DataGridViewRow dgvRow in dgvLivraison.Rows)
                    {
                        livraison.NumProduit = dgvRow.Cells[1].Value.ToString();
                        livraison.QuantiteCommandee = (int)double.Parse(dgvRow.Cells[5].Value.ToString());
                        livraison.ID = Int32.Parse(dgvRow.Cells[0].Value.ToString());
                        requete = string.Format(@"UPDATE cmd_tbl SET qte_livre = {0} WHERE num_medi = '{1}' AND num_bl ={2} AND id ={3}",
                            livraison.QuantiteCommandee, livraison.NumProduit,livraison.NumeroCommande, livraison.ID);
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    _transaction.Commit();
                    flag = true;
                }
                else
                {
                    MonMessageBox.ShowBox("Cette commande a été deja validée", "Erreur", "erreur.png");
                    return false;
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }
     
        public static void SupprimerLivraison(string numeroLivraison)
        {
            try
            {
                connection.Open();
                var requete = string.Format("SELECT * FROM `detail_bl` WHERE  etat = @etat1 AND num_bl =@num_bll");
                command.Parameters.Add(new MySqlParameter("etat1", 3));
                command.Parameters.Add(new MySqlParameter("num_bll", numeroLivraison));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader); reader.Close();
                //if (dt.Rows.Count == 0)
                //{
                    requete = string.Format("DELETE FROM detail_bl WHERE(num_bl = '{0}')", numeroLivraison);
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                //}
                //else
                //{
                //    MonMessageBox.ShowBox("Cette commande a été deja validée",
                //        "Information suppression", "erreur.png");
                //}
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppresion des données a échoué", "Erreur suppression", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        //retirer un medicament de la livraison
        public static void RetirerLivraison(int id, string numProduit, int idCommande)
        {
            try
            {
                connection.Open();
                var requete = string.Format("SELECT * FROM `detail_bl` WHERE  etat = @etat1 AND num_bl =@num_bll");
                command.Parameters.Add(new MySqlParameter("etat1", 3));
                command.Parameters.Add(new MySqlParameter("num_bll", idCommande));
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader); reader.Close();
                if (dt.Rows.Count == 0)
                {
                     requete = string.Format("DELETE FROM cmd_tbl WHERE id = {0} AND num_bl = {1} AND num_medi=@num_medic", id, idCommande);
                    command.Parameters.Add(new MySqlParameter("num_medic", numProduit));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                }
                else
                {
                    MonMessageBox.ShowBox("Cette commande a été deja validée", "Erreur", "erreur.png");
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppresion des données a échoué", "Erreur suppression", exception,
                    "erreur.png");
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        //liste de la livraison
        public static ArrayList ListeDesLivraisons(int numeroLivraison)
        {
            ArrayList listeLivraison = new ArrayList();
            try
            {
                string requete = "SELECT detail_bl.num_bl, detail_bl.date_cmd,detail_bl.num_fourn, fournisseur.nom_fourn " +
                                 ",  detail_bl.montant_fact,detail_bl.autre,detail_bl.etat,detail_bl.numFacture FROM (detail_bl INNER JOIN  fournisseur ON " +
                                 " detail_bl.num_fourn = fournisseur.num_fourn) WHERE detail_bl.num_bl =" +
                                 numeroLivraison;
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Livraison livraison = new Livraison();
                    livraison.DateCommande = !reader.IsDBNull(1) ? reader.GetDateTime(1) : DateTime.Now;
                    livraison.MontantFactural = !reader.IsDBNull(4) ? reader.GetDecimal(4) : 0m;
                    livraison.AutresCharges = !reader.IsDBNull(5) ? reader.GetDecimal(5) : 0m;
                    livraison.NumFournisseur = reader.GetInt32(2);
                    livraison.NomFournisseur = reader.GetString(3);
                    livraison.Etat = !reader.IsDBNull(6) ? reader.GetInt32(6) : 1;
                    livraison.NumeroFacture = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    listeLivraison.Add(livraison);
                }
            }
            finally
            {
                connection.Close();
            }
            return listeLivraison;
        }
        public static ArrayList ListeDesLivraisonsParFacture(string numeroFacture)
        {
            ArrayList listeLivraison = new ArrayList();
            try
            {
                string requete = "SELECT detail_bl.num_bl, detail_bl.date_cmd,detail_bl.num_fourn, fournisseur.nom_fourn " +
                                 ",  detail_bl.montant_fact,detail_bl.autre,detail_bl.etat,detail_bl.numFacture FROM (detail_bl INNER JOIN  fournisseur ON " +
                                 " detail_bl.num_fourn = fournisseur.num_fourn) WHERE detail_bl.numFacture ='" +               numeroFacture+"'";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Livraison livraison = new Livraison();
                    livraison.DateCommande = !reader.IsDBNull(1) ? reader.GetDateTime(1) : DateTime.Now;
                    livraison.MontantFactural = !reader.IsDBNull(4) ? reader.GetDecimal(4) : 0m;
                    livraison.AutresCharges = !reader.IsDBNull(5) ? reader.GetDecimal(5) : 0m;
                    livraison.NumFournisseur = reader.GetInt32(2);
                    livraison.NomFournisseur = reader.GetString(3);
                    livraison.Etat = !reader.IsDBNull(6) ? reader.GetInt32(6) : 1;
                    livraison.NumeroFacture = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    livraison.NumeroCommande  = !reader.IsDBNull(0) ? reader.GetInt32(0) :0;
                    listeLivraison.Add(livraison);
                }
            }
            finally
            {
                connection.Close();
            }
            return listeLivraison;
        }
     
        public static List<Livraison> ListeParIDLivraisons(int numeroLivraison,int id)
        {
            List<Livraison> listeLivraison = new List<Livraison>();
            try
            {
                var requete = "SELECT cmd_tbl.num_bl, cmd_tbl.num_medi, cmd_tbl.quantite, cmd_tbl.prix_achat,medicament.prix_vente, medicament.nom_medi" +
                                " ,cmd_tbl.id, cmd_tbl.lot, cmd_tbl.dateExpiration,cmd_tbl.qte_livre FROM (cmd_tbl INNER JOIN medicament ON cmd_tbl.num_medi" +
                                " = medicament.num_medi) WHERE " + " cmd_tbl.num_bl = " + numeroLivraison + " AND cmd_tbl.id ='" + id +
                                "' ORDER BY medicament.nom_medi";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var livraison = new Livraison();
                    livraison.ID = reader.GetInt32(6);
                    livraison.NumProduit = reader.GetString(1);
                    livraison.QuantiteCommandee = reader.GetInt32(2);
                    livraison.PrixAchat = reader.GetDecimal(3);
                    livraison.PrixVente = reader.GetDecimal(4);
                    livraison.Designation = reader.GetString(5);
                    livraison.NoLot = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    livraison.DateExpiration = !reader.IsDBNull(8) ? reader.GetDateTime(8) : DateTime.Now;
                    livraison.QuantiteLivree = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    listeLivraison.Add(livraison);
                }
            }
            finally
            {
                connection.Close();
            }
            return listeLivraison;
        }
      
        //liste de la livraison
        public static ArrayList ListeDesDetailCommandes(string numeroLivraison)
        {
            var listeLivraison = new ArrayList();
            try
            {
                var requete = "SELECT cmd_tbl.num_bl, cmd_tbl.num_medi, cmd_tbl.quantite, cmd_tbl.prix_achat,medicament.prix_vente, medicament.nom_medi" +
                                 " ,cmd_tbl.id, cmd_tbl.lot, cmd_tbl.dateExpiration,cmd_tbl.qte_livre FROM (cmd_tbl INNER JOIN medicament ON cmd_tbl.num_medi" +
                                 " = medicament.num_medi) WHERE cmd_tbl.num_bl ='" + numeroLivraison +
                                 "' ORDER BY medicament.nom_medi";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var livraison = new Livraison();
                    livraison .ID= reader.GetInt32(6);
                    livraison .NumProduit= reader.GetString(1);
                    livraison .QuantiteCommandee= reader.GetInt32(2);
                    livraison.PrixAchat = reader.GetDecimal(3);
                    livraison.PrixVente = reader.GetDecimal(4);
                    livraison.Designation = reader.GetString(5);
                    livraison.NoLot = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    livraison.DateExpiration = !reader.IsDBNull(8) ? reader.GetDateTime(8) : DateTime.Now;
                    livraison.QuantiteLivree = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    listeLivraison.Add(livraison);
                }
            }
            finally
            {
                connection.Close();
            }
            return listeLivraison;
        }

        public static DataTable ListeDesDetailCommandesParFournisseur(DateTime date1, DateTime date2, string fournisseurs)
        {
            var listeLivraison = new DataTable();
            try
            {
                var requete = "SELECT  cmd_tbl.quantite,cmd_tbl.qte_livre, cmd_tbl.prix_total, medicament.nom_medi" +
                                 ",cmd_tbl.qte_livre FROM (cmd_tbl INNER JOIN medicament ON cmd_tbl.num_medi" +
                                 " = medicament.num_medi INNER JOIN detail_bl ON detail_bl.num_bl =cmd_tbl.num_bl INNER JOIN  fournisseur ON fournisseur.num_fourn=detail_bl.num_fourn) WHERE " +
                                 "detail_bl.date_bl >=@date1 AND detail_bl.date_bl<@date2 AND fournisseur.nom_fourn LIKE '%"+ fournisseurs +"%'";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                connection.Open();
                var reader = command.ExecuteReader();
                listeLivraison.Load(reader);
                //while (reader.Read())
                //{
                //    var livraison = new Livraison();
                //    livraison.ID = reader.GetInt32(6);
                //    livraison.NumProduit = reader.GetString(1);
                //    livraison.QuantiteCommandee = reader.GetInt32(2);
                //    livraison.PrixAchat = reader.GetDecimal(3);
                //    livraison.PrixVente = reader.GetDecimal(4);
                //    livraison.Designation = reader.GetString(5);
                //    livraison.NoLot = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                //    livraison.DateExpiration = !reader.IsDBNull(8) ? reader.GetDateTime(8) : DateTime.Now;
                //    livraison.QuantiteLivree = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                //    listeLivraison.Add(livraison);
                //}
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeLivraison;
        }
        
        public static DataTable  ListeDesDetailsLivraisons(string designation, DateTime date1,  DateTime date2)
        {
            var dt = new DataTable ();
            try
            {
                var requete = "SELECT cmd_tbl.num_bl, cmd_tbl.num_medi, cmd_tbl.quantite, cmd_tbl.prix_achat," +
                                 " medicament.prix_vente, medicament.nom_medi,cmd_tbl.id FROM cmd_tbl INNER JOIN medicament ON cmd_tbl.num_medi" +
                                 " = medicament.num_medi INNER JOIN detail_bl ON cmd_tbl.num_bl=detail_bl.num_bl WHERE medicament.nom_medi =@nom AND detail_bl.date_bl >=@date1 AND detail_bl.date_bl<@date2";
                command.CommandText = requete;
                connection.Open();
                command.Parameters.Add(new MySqlParameter("nom", designation));
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

            return dt;
        }
   
        public static ArrayList ListeDesDetailsLivraisonsParOrdreDeSaisie(string numeroLivraison)
        {
            var listeLivraison = new ArrayList();
            try
            {
                var requete = "SELECT bn_liv.num_bl, bn_liv.num_medi, bn_liv.quantite, bn_liv.prix_achat," +
                                 " medicament.prix_vente, medicament.nom_medi,bn_liv.id FROM (bn_liv INNER JOIN medicament ON bn_liv.num_medi" +
                                 " = medicament.num_medi) WHERE bn_liv.num_bl ='" + numeroLivraison +
                                 "' ORDER BY bn_liv.id";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var livraison = new Livraison();
                    livraison.ID= reader.GetInt32(6);
                    livraison.NumeroCommande = reader.GetInt32(0);
                    livraison.NumProduit= reader.GetString(1);
                   livraison.QuantiteCommandee= reader.GetInt32(2);
                   livraison.PrixAchat = reader.GetDecimal(3);
                   livraison.PrixVente = reader.GetDecimal(4);
                    livraison.Designation = reader.GetString(5);
                    listeLivraison.Add(livraison);
                }
            }
            finally
            {
                connection.Close();
            }
            return listeLivraison;
        }
        //liste de la livraison
        public static ArrayList ListeDesLivraisonsParFournisseur(string fournisseur)
        {
            var listeLivraison = new ArrayList();
            try
            {
                var requete = "SELECT detail_bl.num_bl, detail_bl.date_cmd,  detail_bl.num_fourn, fournisseur.nom_fourn , detail_bl.montant_fact," +
                                 " detail_bl.autre, detail_bl.etat FROM (detail_bl INNER JOIN  fournisseur ON detail_bl.num_fourn = fournisseur.num_fourn)" +
                                 " WHERE fournisseur.nom_fourn = @fourn ORDER BY detail_bl.num_bl DESC";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("@fourn", fournisseur));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Livraison livraison = new Livraison();
                    livraison.NumeroCommande = reader.GetInt32(0);
                    livraison.DateCommande = reader.GetDateTime(1);
                    livraison.NumFournisseur = reader.GetInt32(2);
                    livraison.NomFournisseur = reader.GetString(3);
                    livraison.MontantFactural = !reader.IsDBNull(4) ? reader.GetDecimal(4) : 0m;
                    livraison.AutresCharges = !reader.IsDBNull(5) ? reader.GetDecimal(5) : 0m;
                    listeLivraison.Add(livraison);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listeLivraison;
        }

        public static bool MettreAJourUneLivraison(Livraison livraison)
        {
            try
            {
                connection.Open();
                var requete = string.Format(@"UPDATE detail_bl SET autre = {0} WHERE(num_bl = {1})",
                    livraison.AutresCharges, livraison.NumeroCommande);
                command.Transaction = _transaction;
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

        //retirer la livraison
        public static bool InsererLeRetour(string id_produit, string medic, double prix, int qte, string motif, string numLivr,string lot)
        {
            bool flag = false;
            try
            {
                connection.Open();
             _transaction =    connection.BeginTransaction();
                string requete = "INSERT INTO `retour_tbl`(`medic`, `qte`,  `motif`, `date`,`id_liv`,prix) VALUES (@medic," + qte
                     + ", @motif , @date, '" + numLivr + "' ,@prix)";
                command.Parameters.Add(new MySqlParameter("medic", medic));
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.Parameters.Add(new MySqlParameter("motif", motif));
                command.Parameters.Add(new MySqlParameter("prix", prix));
                command.Parameters.Add(new MySqlParameter("date", DateTime.Now));
                command.ExecuteNonQuery();

                requete =
                  string.Format(
                      "UPDATE `stock` SET `grd_stock` = (`grd_stock` - {0} ) WHERE `num_medi` = ('{1}')",
                      qte, id_produit);
                command.Transaction = _transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                requete = "UPDATE   lot_tbl  SET qte =qte - @qter WHERE num_prod=@num_prodr AND  lot=@lot AND idLiv =" + numLivr;
                command.Parameters.Add(new MySqlParameter("num_prodr", id_produit));
                command.Parameters.Add(new MySqlParameter("lot", lot));
                command.Parameters.Add(new MySqlParameter("qter", qte ));
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                _transaction.Commit();
                flag = true;

            }
            catch (Exception ex)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox( "Ajouter retour livraison", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static bool RetirerLeRetour(int id, string id_produit, int qte)
        {
            bool flag = false;
            try
            {
                connection.Open();

                string requete = "DELETE FROM `retour_tbl` WHERE id = " + id;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                requete =
                  string.Format(
                      "UPDATE `stock` SET `grd_stock` = (`grd_stock` +{0} ) WHERE `num_medi` = ('{1}')",
                      qte, id_produit);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                flag = true;

            }
            catch (Exception)
            {
                //MonMessageBox.ShowBox("L'enregistrement a echoue", "Ajouter livraison", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static DataTable ListeRetourLivraison(string numLivraison)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                string requete = "SELECT  retour_tbl.id, retour_tbl.medic, retour_tbl.qte, retour_tbl.motif, retour_tbl.`date`," +
                    "retour_tbl.id_liv,retour_tbl.prix FROM retour_tbl INNER JOIN detail_bl ON retour_tbl.id_liv = detail_bl.num_bl" +
                    " WHERE (retour_tbl.id_liv = '" + numLivraison + "')";
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste credit", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeRetourLivraison(int numFournisseur, DateTime dt1, DateTime dt2)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                string requete = "SELECT  retour_tbl.id, retour_tbl.medic, retour_tbl.qte, retour_tbl.motif, retour_tbl.`date`," +
                    "retour_tbl.id_liv,retour_tbl.prix FROM retour_tbl INNER JOIN detail_bl ON retour_tbl.id_liv = detail_bl.num_bl" +
                    " WHERE (detail_bl.num_fourn = '" + numFournisseur + "' AND retour_tbl.`date`>=@date1 AND retour_tbl.`date`<@date2)";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1));
                command.Parameters.Add(new MySqlParameter("date2", dt2.AddHours(24)));
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste credit", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeRetourLivraison(string nomProduit, DateTime dt1, DateTime dt2)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                string requete = "SELECT  retour_tbl.id, retour_tbl.medic, retour_tbl.qte, retour_tbl.motif, retour_tbl.`date`," +
                    "retour_tbl.id_liv,retour_tbl.prix FROM retour_tbl INNER JOIN detail_bl ON retour_tbl.id_liv = detail_bl.num_bl" +
                    " WHERE ( retour_tbl.medic=@medi AND retour_tbl.`date`>=@date1 AND retour_tbl.`date`<@date2)";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("medi", nomProduit));
                command.Parameters.Add(new MySqlParameter("date1", dt1));
                command.Parameters.Add(new MySqlParameter("date2", dt2.AddHours(24)));
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste credit", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }
       
        //retirer un medicament de la livraison
        public static void RetirerLivraison(string numMedi, string numLiv, int qte)
        {
            try
            {
                connection.Open();
                string requete = "SELECT * FROM bn_liv WHERE num_medi = '" + numMedi + "' AND num_bl = '" + numLiv + "'";
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                string numMedicament = dataTable.Rows[0].ItemArray[1].ToString();
                int quantite = Int32.Parse(dataTable.Rows[0].ItemArray[2].ToString());
                if (quantite == qte)
                {
                    requete = string.Format("DELETE FROM bn_liv WHERE(num_medi = '{0}')", numMedi);
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                    // mettre a jour les quantites des dans le stock
                    requete =
                        string.Format(
                            "UPDATE `stock` SET `grd_stock` = (`grd_stock` - {0} ) WHERE `num_medi` = ('{1}')",
                            qte, numMedicament);
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                }
                else if (qte < quantite)
                {
                    requete = string.Format("UPDATE  bn_liv SET grd_stock = grd_stock - " + qte + " WHERE(num_medi = '{0}')", numMedi);
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                    // mettre a jour les quantites des dans le stock
                    requete =
                        string.Format(
                            "UPDATE `stock` SET `grd_stock` = (`grd_stock` - {0} ) WHERE `num_medi` = ('{1}')",
                            qte, numMedicament);
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                }

                MonMessageBox.ShowBox("La modification des données a été effectuée avec succés",
               "Information suppression", "affirmation.png");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur suppression", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion
         /****************************REGION FOURNISSEUR****************************/
        #region FOURNISSUERS

        public static bool CreerUnNouveauFournisseur(Fournisseur  fournisseur)
        {
            try
            {
                connection.Open();
               var  requete = "INSERT INTO fournisseur ( nom_fourn,addresse,telephone1,telephone2," +
            "fax,email, ville, pays, commentaire, postal,reference,telecopie,no_compte,nif) VALUES  (@nom,@adresse,@tel1,@tel2," +
            "@fax,@email, @ville, @pays, @commentaire, @postal,@reference,@telecopie,@no_compte,@nif)";
                command.Parameters.Add(new MySqlParameter("id", fournisseur.ID));
                command.Parameters.Add(new MySqlParameter("nom", fournisseur.NomFournisseur));
                command.Parameters.Add(new MySqlParameter("adresse", fournisseur.Adresse));
                command.Parameters.Add(new MySqlParameter("tel1", fournisseur.Telephone1));
                command.Parameters.Add(new MySqlParameter("tel2", fournisseur.Telephone2));
                command.Parameters.Add(new MySqlParameter("fax", fournisseur.FAX));
                command.Parameters.Add(new MySqlParameter("email", fournisseur.Email));
                command.Parameters.Add(new MySqlParameter("ville", fournisseur.Ville));
                command.Parameters.Add(new MySqlParameter("pays", fournisseur.Pays));
                command.Parameters.Add(new MySqlParameter("commentaire", fournisseur.Commentaire));
                command.Parameters.Add(new MySqlParameter("postal", fournisseur.NumeroPostal));
                command.Parameters.Add(new MySqlParameter("reference", fournisseur.Reference));
                command.Parameters.Add(new MySqlParameter("telecopie", fournisseur.Telecopie));
                command.Parameters.Add(new MySqlParameter("no_compte", fournisseur.NoCompte));
                command.Parameters.Add(new MySqlParameter("nif", fournisseur.NIF));

                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static bool  ModifierUnFournisseur(Fournisseur fournisseur)
        {
            try
            {
                connection.Open();
                var requete = string.Format("UPDATE fournisseur SET nom_fourn= @nom, addresse=@adresse,telephone1=@tel1,telephone2= @tel2," +
            "fax=@fax,email=@email, ville=@ville,pays= @pays,commentaire= @commentaire, "+
            " postal=@postal,reference=@reference,telecopie=@telecopie, no_compte=@no_compte, nif=@nif WHERE num_fourn ={0}", fournisseur.ID);

                command.Parameters.Add(new MySqlParameter("nom", fournisseur.NomFournisseur));
                command.Parameters.Add(new MySqlParameter("adresse", fournisseur.Adresse));
                command.Parameters.Add(new MySqlParameter("tel1", fournisseur.Telephone1));
                command.Parameters.Add(new MySqlParameter("tel2", fournisseur.Telephone2));
                command.Parameters.Add(new MySqlParameter("fax", fournisseur.FAX));
                command.Parameters.Add(new MySqlParameter("email", fournisseur.Email));
                command.Parameters.Add(new MySqlParameter("ville", fournisseur.Ville));
                command.Parameters.Add(new MySqlParameter("pays", fournisseur.Pays));
                command.Parameters.Add(new MySqlParameter("commentaire", fournisseur.Commentaire));
                command.Parameters.Add(new MySqlParameter("postal", fournisseur.NumeroPostal));
                command.Parameters.Add(new MySqlParameter("reference", fournisseur.Reference));
                command.Parameters.Add(new MySqlParameter("telecopie", fournisseur.Telecopie));
                command.Parameters.Add(new MySqlParameter("no_compte", fournisseur.NoCompte));
                command.Parameters.Add(new MySqlParameter("nif", fournisseur.NIF));

                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static bool SupprimerFournisseur(Fournisseur fournisseur)
        {
            try
            {
                connection.Open();
                var requete = string.Format("DELETE FROM fournisseur WHERE num_fourn={0} ", fournisseur.ID);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Fournisseur> ListeFournisseur()
        {
            try
            {
               var listeLabo =new  List<Fournisseur>();
                var requete = "SELECT * FROM fournisseur ORDER BY nom_fourn";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var fournisseur = new Fournisseur();
                    fournisseur.ID = reader.GetInt32(0);
                    fournisseur.NomFournisseur = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    fournisseur.Adresse = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    fournisseur.Telephone1 = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    fournisseur.Telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    fournisseur.FAX = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    fournisseur.Email = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    fournisseur.Ville = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    fournisseur.Pays = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    fournisseur.Commentaire = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    fournisseur.NumeroPostal = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    fournisseur.Reference = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    fournisseur.Telecopie = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    fournisseur.NoCompte = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    fournisseur.NIF = !reader.IsDBNull(14) ? reader.GetString(14) : "";

                    listeLabo.Add(fournisseur);
               }
                return listeLabo;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Fournisseur> ListeFournisseur(int id)
        {
            try
            {
                var listeLabo = new List<Fournisseur>();
                var requete = "SELECT * FROM fournisseur where num_four ="+id ;
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var fournisseur = new Fournisseur();
                    fournisseur.ID = reader.GetInt32(0);
                    fournisseur.NomFournisseur = !reader.IsDBNull(1) ? reader.GetString(1) :"";
                    fournisseur.Adresse = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    fournisseur.Telephone1 = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    fournisseur.Telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    fournisseur.FAX = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    fournisseur.Email = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    fournisseur.Ville = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    fournisseur.Pays = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    fournisseur.Commentaire = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    fournisseur.NumeroPostal = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    fournisseur.Reference = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    fournisseur.Telecopie = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    fournisseur.NoCompte = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    fournisseur.NIF = !reader.IsDBNull(14) ? reader.GetString(14) : "";

                    listeLabo.Add(fournisseur);
                }
                return listeLabo;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static int ObtenirLeDernierNumeroFournisseur()
        {
            try
            {
                var requete = "SELECT MAX(num_fourn) FROM fournisseur";
                command.CommandText = requete;
                connection.Open();
                return (int)command.ExecuteScalar();
            }
            catch { return 0; }
            finally
            {
                connection.Close();
            }
        }
        
        
        #endregion
         
        #region MEDICAMENTS
        // methode pour ajouter un nouveau medicament
        public static bool EnregistrerMedicament(Medicament medicament)
        {
            try
            {
                connection.Open();
            _transaction = connection.BeginTransaction();
            string requete = "SELECT * FROM medicament WHERE num_medi = '" + medicament.NumeroMedicament + "'";
            command.CommandText = requete;
            command.Transaction = _transaction;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable(); 
                dataTable.Load(reader); reader.Close();
                int nombreMedicament = dataTable.Rows.Count;

                if (nombreMedicament < 1)
                {

                    requete = string.Format("INSERT INTO `medicament` (`num_medi`, `numeroFamille`, `nom_medi`, " +
                                      "`prix_achat`, `prix_vente`, `qteAlerte`, `description`, `date_expiration` ) VALUES " +
                                      "(@num_medi,@numeroFamille, @nom_medi, @prix_achat, @prix_vente,@qteAlerte, @description, @date_expiration)");
                    command.Parameters.Add(new MySqlParameter("num_medi", medicament.NumeroMedicament));
                    command.Parameters.Add(new MySqlParameter("numeroFamille", medicament.CodeFamille));
                    command.Parameters.Add(new MySqlParameter("nom_medi", medicament.NomMedicament));
                    command.Parameters.Add(new MySqlParameter("prix_achat", medicament.PrixAchat));
                    command.Parameters.Add(new MySqlParameter("prix_vente", medicament.PrixVente));
                    command.Parameters.Add(new MySqlParameter("qteAlerte", medicament.QuantiteAlerte));
                    command.Parameters.Add(new MySqlParameter("description", medicament.Description));
                    command.Parameters.Add(new MySqlParameter("date_expiration", medicament.DateExpiration));

                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();

                    requete = string.Format("INSERT INTO `stock` (`num_medi`, `quantite`, `grd_stock`) VaLUES ('{0}',{1},{2})",
                        medicament.NumeroMedicament, medicament.Quantite,medicament.GrandStock);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                }
                else
                {
                    //if (MonMessageBox.ShowBox("Voulez vous modifier les données du produit " + medicament.NomMedicament + "?", "Confirmation", "confirmation.png") == "1")
                    //{
                        requete = "UPDATE medicament SET nom_medi = @nom_medi, prix_achat = @prix_achat, qteAlerte = " + medicament.QuantiteAlerte + ", " +
                                        " prix_vente = @prix_vente, description = @description, date_expiration =@date_expiration, " +
                                        " numeroFamille= " + medicament.CodeFamille + " WHERE (num_medi ='" + medicament.NumeroMedicament + "')";
                        command.Parameters.Add(new MySqlParameter("nom_medi", medicament.NomMedicament));
                        command.Parameters.Add(new MySqlParameter("prix_achat", medicament.PrixAchat));
                        command.Parameters.Add(new MySqlParameter("prix_vente", medicament.PrixVente));
                        command.Parameters.Add(new MySqlParameter("description", medicament.Description));
                        command.Parameters.Add(new MySqlParameter("date_expiration", medicament.DateExpiration));
                        command.Parameters.Add(new MySqlParameter("qteAlerte", medicament.QuantiteAlerte));
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();

                        requete = string.Format(@"UPDATE stock SET quantite = {0}, grd_stock={1} WHERE(num_medi = '{2}')", medicament.Quantite,medicament.GrandStock,
                            medicament.NumeroMedicament);
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();
                    //}
                }

                _transaction.Commit();
                return true;
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");

                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }
        public static bool SupprimeDateExpiration(int  id)
        {
            try
            {
                connection.Open();
                string requete = string.Format("DELETE FROM lot_tbl WHERE (id = {0})", id );
                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppresion des données a échoué", "Erreur d'insertion", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool ValiderQuantitePharmacie(string numeroProduit, int quantite)
        {
            try
            {
                connection.Open();

                var requete = string.Format("SELECT * FROM stock WHERE `num_medi` = '{0}'", numeroProduit);
                command.CommandText = requete;
                var r = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(r);
                if (dt.Rows.Count > 0)
                {
                    if (Int32.Parse(dt.Rows[0].ItemArray[2].ToString()) >= quantite)
                        return true;
                    else return false;
                }
                else return false;


            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Erreur vente", exception);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool ValiderQuantiteDepot(string numeroProduit, int quantite)
        {
            try
            {
                connection.Open();

                var requete = string.Format("SELECT * FROM stock WHERE `num_medi` = '{0}'", numeroProduit);
                command.CommandText = requete;
                var r = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(r);
                if (dt.Rows.Count > 0)
                {
                    if (Int32.Parse(dt.Rows[0].ItemArray[3].ToString()) >= quantite)
                        return true;
                    else return false;
                }
                else return false;


            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Erreur vente", exception);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
      
        public static bool EnregistrerLotDeProduit(Medicament medicament)
        {
            try
            {
                connection.Open();
                string requete = "SELECT * FROM lot_tbl WHERE id = '" + medicament.CodeFamille + "'";
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader); reader.Close();
                int nombreMedicament = dataTable.Rows.Count;

                if (nombreMedicament < 1)
                {

                    requete = string.Format("INSERT INTO `lot_tbl` (num_prod, lot , qte, `date`) VALUES (@num_medi, @lot,@qte ,@date )");
                    command.Parameters.Add(new MySqlParameter("num_medi", medicament.NumeroMedicament));
                    command.Parameters.Add(new MySqlParameter("date", medicament.DateExpiration));
                    command.Parameters.Add(new MySqlParameter("lot", medicament.NoLot));
                    command.Parameters.Add(new MySqlParameter("qte", medicament.Quantite));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                }
                else
                {
                    requete = "UPDATE `lot_tbl`  SET  `date`=@date , lot = @lot ,qte=@qte WHERE id = " + medicament.CodeFamille;
                    command.Parameters.Add(new MySqlParameter("date", medicament.DateExpiration));
                    command.Parameters.Add(new MySqlParameter("lot", medicament.NoLot));
                    command.Parameters.Add(new MySqlParameter("qte", medicament.Quantite));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    //}
                }
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

        public static bool MettreAjourLotDeProduit(string numeroMedicament)
        {
            try
            {
                connection.Open();
                string requete = "SELECT * FROM lot_tbl WHERE num_prod = '" + numeroMedicament + "' AND qte >0";
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader); reader.Close();
                int nombreMedicament = dataTable.Rows.Count;

                if (nombreMedicament == 1)
                {
                    requete = "SELECT * FROM stock WHERE num_medi = '" + numeroMedicament + "'";
                    command.CommandText = requete;
                    reader = command.ExecuteReader();
                    DataTable dataTable1 = new DataTable();
                    dataTable1.Load(reader); reader.Close();
                    var grandStock = Int32.Parse(dataTable1.Rows[0].ItemArray[3].ToString());
                    if (Int32.Parse(dataTable.Rows[0].ItemArray[3].ToString()) != Int32.Parse(dataTable1.Rows[0].ItemArray[3].ToString()))
                    {
                        requete = "UPDATE `lot_tbl`  SET   qte=@qte WHERE num_prod = '" + numeroMedicament + "'";
                        command.Parameters.Add(new MySqlParameter("qte", grandStock));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                    }
                }
                else if(nombreMedicament >1)
                {
                    //requete = "SELECT * FROM stock WHERE num_medi = '" + numeroMedicament + "'";
                    //command.CommandText = requete;
                    //reader = command.ExecuteReader();
                    //DataTable dataTable1 = new DataTable();
                    //dataTable1.Load(reader); reader.Close();
                    //var grandStock = 0;
                    //var idLot = 0;
                    //int quantiteMax = Int32.Parse(dataTable.Rows[0].ItemArray[3].ToString());
                    //for (var i = 0; i < dataTable.Rows.Count; i++)
                    //{
                    //    grandStock += Int32.Parse(dataTable.Rows[i].ItemArray[3].ToString());
                    //    if(Int32.Parse(dataTable.Rows[i].ItemArray[3].ToString())>quantiteMax)
                    //    {
                    //        quantiteMax= Int32.Parse(dataTable.Rows[i].ItemArray[3].ToString());
                    //        idLot = Int32.Parse(dataTable.Rows[i].ItemArray[0].ToString());
                    //    }
                    //}
                    //if (grandStock!= Int32.Parse(dataTable1.Rows[0].ItemArray[3].ToString()))
                    //{
                    //    //var difference = Int32.Parse(dataTable1.Rows[0].ItemArray[3].ToString()) - grandStock;
                    //    //requete = "UPDATE `lot_tbl`  SET   qte=qte + "+difference+" WHERE num_prod = '" + numeroMedicament + "' AND id = " + idLot;
                    //    //command.CommandText = requete;
                    //    //command.ExecuteNonQuery();
                    //}
                }
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

        public static bool EnregistrerEtatStock(List<Medicament>medicament)
        {
            try
            {
                connection.Open();
                foreach(var p in medicament)
                {
                    var requete = string.Format("INSERT INTO `etat_stock_tbl` (num_medi, grd_stock , pharm,`date`) VALUES (@num_medi, @grd_stock,@pharm ,@date )");
                    command.Parameters.Add(new MySqlParameter("num_medi", p.NumeroMedicament));
                    command.Parameters.Add(new MySqlParameter("date", DateTime.Now));
                    command.Parameters.Add(new MySqlParameter("grd_stock", p.GrandStock));
                    command.Parameters.Add(new MySqlParameter("pharm", p.Quantite));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                   command.Parameters.Clear();
                }
                return true;
            }
            catch (Exception exception)
            {
                //MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    
        public static List<Medicament> ListeEtatStock(DateTime date)
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            try
            {
                string requete = @"SELECT * FROM etat_stock_tbl WHERE date>=@date1 AND date<@date2";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date));
                command.Parameters.Add(new MySqlParameter("date2",date.Date.AddHours(24)));
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();
                    produit.NumeroMedicament = reader.GetString(0);
                    produit.DateExpiration = reader.GetDateTime(1);
                    produit.NumeroMedicament = reader.GetString(2);
                    produit.Quantite = reader.GetInt32(4);
                    produit.GrandStock = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeMedicament;
        }
      
        //liste des  medicament
        public static List<Medicament> ListeDesMedicamentsRechercherParNom(string nomMedicament)
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            try
            {
                string requete = @"SELECT medicament.num_medi, familleTbl.designation, medicament.nom_medi, " +
                                 " medicament.prix_achat, medicament.prix_vente,medicament.qteAlerte, medicament.description, " +
                                 "medicament.date_expiration, stock.quantite,medicament.nbreBoite,medicament.nbreDetail,medicament.prixDetail, stock.grd_stock FROM ((medicament INNER JOIN " +
                                 " stock ON medicament.num_medi = stock.num_medi) INNER JOIN familleTbl ON medicament.numeroFamille" +
                "= familleTbl.numeroFamille) WHERE medicament.nom_medi LIKE @medi  ORDER BY medicament.nom_medi";
                command.CommandText = requete;
                connection.Open();
                command.Parameters.AddWithValue("medi", nomMedicament+"%");
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();

                    produit .NumeroMedicament = reader.GetString(0);
                    produit .Designation = reader.GetString(1);
                    produit .NomMedicament = reader.GetString(2);
                    produit .PrixAchat = reader.GetDecimal(3);
                    produit .Quantite = reader.GetInt32(8);
                    produit .PrixVente = reader.GetDecimal(4);
                    produit.QuantiteAlerte = reader.GetInt32(5);
                    produit .Description = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    produit.DateExpiration = reader.GetDateTime(7);
                    produit.PrixVenteDetail =!reader.IsDBNull(11) ? reader.GetDecimal(11):0m;
                    produit.NombreDetail =!reader.IsDBNull(10) ? reader.GetInt32(10):0;
                    produit.NombreBoite = !reader.IsDBNull(9) ? reader.GetInt32(9): 0;
                    produit.GrandStock = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeMedicament;
        }
        public static List<Medicament> ListeDesMedicamentsRechercherParFamille(string famille)
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            try
            {
                string requete = @"SELECT medicament.num_medi, familleTbl.designation, medicament.nom_medi, " +
                                 " medicament.prix_achat, medicament.prix_vente,medicament.qteAlerte, medicament.description, " +
                                 "medicament.date_expiration, stock.quantite,medicament.nbreBoite,medicament.nbreDetail,medicament.prixDetail, stock.grd_stock FROM ((medicament INNER JOIN " +
                                 " stock ON medicament.num_medi = stock.num_medi) INNER JOIN familleTbl ON medicament.numeroFamille" +
                "= familleTbl.numeroFamille) WHERE familleTbl.designation LIKE @designation  ORDER BY medicament.nom_medi";
                command.CommandText = requete;
                connection.Open();
                command.Parameters.AddWithValue("designation", famille + "%");
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();

                    produit.NumeroMedicament = reader.GetString(0);
                    produit.Designation = reader.GetString(1);
                    produit.NomMedicament = reader.GetString(2);
                    produit.PrixAchat = reader.GetDecimal(3);
                    produit.Quantite = reader.GetInt32(8);
                    produit.PrixVente = reader.GetDecimal(4);
                    produit.QuantiteAlerte = reader.GetInt32(5);
                    produit.Description =!reader.IsDBNull(6) ? reader.GetString(6) : " ";
                    produit.DateExpiration = reader.GetDateTime(7);
                    produit.PrixVenteDetail = !reader.IsDBNull(11) ? reader.GetDecimal(11) : 0m;
                    produit.NombreDetail = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
                    produit.NombreBoite = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    produit.GrandStock = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeMedicament;
        }

        public static List<Medicament> ListeDesInventaireRechercherParNom(string nomMedicament)
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            try
            {
                string requete = @"SELECT medicament.num_medi, familleTbl.designation, medicament.nom_medi, " +
                                 " medicament.prix_achat, medicament.prix_vente,medicament.qteAlerte, medicament.description, " +
                                 "medicament.date_expiration, stock.quantite,medicament.nbreBoite,medicament.nbreDetail,medicament.prixDetail, stock.grd_stock FROM ((medicament INNER JOIN " +
                                 " stock ON medicament.num_medi = stock.num_medi) INNER JOIN familleTbl ON medicament.numeroFamille" +
                "= familleTbl.numeroFamille) WHERE medicament.nom_medi LIKE @medi  ORDER BY medicament.nom_medi";
                command.CommandText = requete;
                connection.Open();
                command.Parameters.AddWithValue("medi", nomMedicament + "%");
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();

                    produit.NumeroMedicament = reader.GetString(0);
                    produit.Designation = reader.GetString(1);
                    produit.NomMedicament = reader.GetString(2);
                    produit.PrixAchat = reader.GetDecimal(3);
                    produit.Quantite = reader.GetInt32(8);
                    produit.PrixVente = reader.GetDecimal(4);
                    produit.QuantiteAlerte = reader.GetInt32(5);
                    produit.Description = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    produit.DateExpiration = reader.GetDateTime(7);
                    produit.PrixVenteDetail = !reader.IsDBNull(11) ? reader.GetDecimal(11) : 0m;
                    produit.NombreDetail = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
                    produit.NombreBoite = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    produit.GrandStock = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeMedicament;
        }
        public static List<Medicament> ListeDesInventaireRechercherParFamille(string famille)
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            try
            {
                string requete = @"SELECT medicament.num_medi, familleTbl.designation, medicament.nom_medi, " +
                                 " medicament.prix_achat, medicament.prix_vente,medicament.qteAlerte, medicament.description, " +
                                 "medicament.date_expiration, stock.quantite,medicament.nbreBoite,medicament.nbreDetail,medicament.prixDetail, stock.grd_stock FROM ((medicament INNER JOIN " +
                                 " stock ON medicament.num_medi = stock.num_medi) INNER JOIN familleTbl ON medicament.numeroFamille" +
                "= familleTbl.numeroFamille) WHERE familleTbl.designation LIKE @designation  ORDER BY medicament.nom_medi";
                command.CommandText = requete;
                connection.Open();
                command.Parameters.AddWithValue("designation", famille + "%");
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();

                    produit.NumeroMedicament = reader.GetString(0);
                    produit.Designation = reader.GetString(1);
                    produit.NomMedicament = reader.GetString(2);
                    produit.PrixAchat = reader.GetDecimal(3);
                    produit.Quantite = reader.GetInt32(8);
                    produit.PrixVente = reader.GetDecimal(4);
                    produit.QuantiteAlerte = reader.GetInt32(5);
                    produit.Description = !reader.IsDBNull(6) ? reader.GetString(6) : " ";
                    produit.DateExpiration = reader.GetDateTime(7);
                    produit.PrixVenteDetail = !reader.IsDBNull(11) ? reader.GetDecimal(11) : 0m;
                    produit.NombreDetail = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
                    produit.NombreBoite = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    produit.GrandStock = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeMedicament;
        }
        
        // methode pour modifier un nouveau medicament
        public static bool ModifierLadateMedicament(string id, DateTime dateExpiration)
        {
            try
            {
                connection.Open();
                var requete = "UPDATE medicament SET date_expiration =@date_expiration WHERE (num_medi ='" + id + "')";
                command.Parameters.Add(new MySqlParameter("date_expiration", dateExpiration));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des donnees a echoue", exception);
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        //liste des  medicament filtre par code famille
        public static List<Medicament> ListeDesMedicamentsExpirees()
        {
            var listeMedicament = new List<Medicament>();
            try
            {

                var dateExpiration = DateTime.Now.Date.AddMonths(3);

                var requete = "SELECT medicament.num_medi, medicament.numeroFamille, medicament.nom_medi, " +
                                  " medicament.prix_achat, medicament.prix_vente,medicament.qteAlerte, medicament.description, " +
                                  "medicament.date_expiration, stock.quantite, stock.grd_stock FROM (medicament INNER JOIN " +
                                  " stock ON medicament.num_medi = stock.num_medi) WHERE medicament.date_expiration <= " +
                                  " @date_expiration AND stock.quantite >0 ORDER BY medicament.date_expiration";
                command.Parameters.Add(new MySqlParameter("date_expiration", dateExpiration.Date));
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var  produit = new Medicament();
                    produit .NumeroMedicament= reader.GetString(0);
                   produit.CodeFamille = reader.GetInt32(1);
                   produit.NomMedicament = reader.GetString(2);
                   produit.PrixAchat = reader.GetDecimal(3);
                   produit.PrixVente = reader.GetDecimal(4);
                   produit.QuantiteAlerte = reader.GetInt32(5);
                   produit.Quantite = reader.GetInt32(8);
                   produit.DateExpiration = reader.GetDateTime(7);
                   //produit.GrandStock = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return listeMedicament;
        }

        //liste des  medicament filtre par code famille
        public static List<Medicament> ListeDesMedicamentParCode(string Code)
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            //MySqlDataReader reader=new MySqlDataReader ;
           try
            {
                string requete = "SELECT medicament.num_medi, familleTbl.designation, medicament.nom_medi, " +
                                 " medicament.prix_achat, medicament.prix_vente,medicament.qteAlerte, medicament.description, " +
                                 "medicament.date_expiration, stock.quantite,medicament.nbreBoite,medicament.nbreDetail,"+
               "medicament.prixDetail,medicament.numeroFamille,stock.grd_stock FROM ((medicament INNER JOIN " +
                                 " stock ON medicament.num_medi = stock.num_medi) INNER JOIN familleTbl ON medicament.numeroFamille" +
                "= familleTbl.numeroFamille) WHERE medicament.num_medi =@code";
                command.CommandText = requete;
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                else
                {
                    connection.Close();
                    connection.Open();
                }
                command.Parameters.Add(new MySqlParameter("code", Code));
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();

                    produit .NumeroMedicament = reader.GetString(0);
                    produit .Designation = reader.GetString(1);
                    produit .NomMedicament = reader.GetString(2);
                    produit .PrixAchat = reader.GetDecimal(3);
                    produit .Quantite = reader.GetInt32(8);
                    produit .PrixVente = reader.GetDecimal(4);
                    produit.QuantiteAlerte = reader.GetInt32(5);
                    produit.Description = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    produit.DateExpiration = reader.GetDateTime(7);
                    produit.PrixVenteDetail =!reader.IsDBNull(11) ? reader.GetDecimal(11):0m;
                    produit.NombreDetail =!reader.IsDBNull(10) ? reader.GetInt32(10):0;
                    produit.NombreBoite = !reader.IsDBNull(9) ? reader.GetInt32(9): 0;
                    produit.CodeFamille = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
                    produit.GrandStock = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
           {
               command.Parameters.Clear();
                connection.Close();
            }
            return listeMedicament;
        }

        public static List<Medicament> ListeDesLotsProduitsParCode(string Code)
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            //MySqlDataReader reader=new MySqlDataReader ;
            try
            {
                string requete = "SELECT * FROM  lot_tbl WHERE num_prod ='"+ Code + "'  AND qte >0 ORDER BY `date`";
                command.CommandText = requete;
                    connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();
                    produit.CodeFamille = reader.GetInt32(0);
                    produit.NumeroMedicament = reader.GetString(1);
                    produit.NoLot =  !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    produit.GrandStock =!reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    produit.DateExpiration =!reader.IsDBNull(4) ? reader.GetDateTime(4) : DateTime.Now.Date;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeMedicament;
        }

        public static List<Medicament> ListeDesLotsProduitsParCodeExpires()
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            //MySqlDataReader reader=new MySqlDataReader ;
            try
            {
                string requete = "SELECT medicament.prix_achat, medicament.nom_medi, medicament.date_expiration, lot_tbl.qte, lot_tbl.`date`, lot_tbl.lot"+
                        " FROM medicament INNER JOIN lot_tbl ON medicament.num_medi = lot_tbl.num_prod WHERE  lot_tbl.`date` <=@date AND  lot_tbl.qte>0 ORDER BY medicament.nom_medi";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date",DateTime.Now.AddMonths(3)));
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();
                    produit.PrixAchat = reader.GetInt32(0);
                    produit.NomMedicament = reader.GetString(1);
                    produit.NoLot = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    produit.GrandStock = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    produit.DateExpiration = !reader.IsDBNull(4) ? reader.GetDateTime(4) : DateTime.Now.Date;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return listeMedicament;
        }

        //liste des  medicament
        public static ArrayList ListeParNomMedicaments(string nomMedicament)
        {
            ArrayList listeMedicament = new ArrayList();
            try
            {
                string requete = "SELECT medicament.num_medi, medicament.numeroFamille, medicament.nom_medi, " +
                                 " medicament.prix_achat, medicament.prix_vente, medicament.qteAlerte, medicament.description, " +
                                 "medicament.date_expiration, stock.quantite, stock.grd_stock FROM (medicament INNER JOIN " +
                                 " stock ON medicament.num_medi = stock.num_medi) WHERE medicament.nom_medi LIKE '%" +
                                 nomMedicament + "%' ORDER BY medicament.num_medi";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Medicament produit = new Medicament();

                    produit.NumeroMedicament = reader.GetString(0);
                    produit.CodeFamille = reader.GetInt32(1);
                    produit.NomMedicament = reader.GetString(2);
                    produit.PrixAchat = reader.GetDecimal(3);
                    produit.PrixVente = reader.GetDecimal(4);
                    produit.QuantiteAlerte = reader.GetInt32(5);
                    produit.Description = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    produit.Quantite = reader.GetInt32(8);
                    produit.DateExpiration = reader.GetDateTime(7);
                    produit.GrandStock = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeMedicament;
        }
      
        public static DataTable ListeParNomMedicaments()
        {
            DataTable listeMedicament = new DataTable();
            try
            {
                string requete = "SELECT * FROM medicament ORDER BY nom_medi";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                listeMedicament.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeMedicament;
        }

        //liste des  medicament dont le stock est tres bas
        public static List<Medicament> ListeDesMedicamentsAStockBas()
        {
            List<Medicament> listeMedicament = new List<Medicament>();
            try
            {
                string requete = "SELECT medicament.num_medi, medicament.numeroFamille, medicament.nom_medi, " +
                                 " medicament.prix_achat, medicament.prix_vente,medicament.qteAlerte, medicament.description, " +
                                 "medicament.date_expiration, stock.quantite FROM (medicament INNER JOIN " +
                         " stock ON medicament.num_medi = stock.num_medi AND stock.quantite + stock.grd_stock  <= medicament.qteAlerte ) ORDER BY" +
                " medicament.nom_medi";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Medicament();

                    produit.NumeroMedicament = reader.GetString(0);
                    produit .CodeFamille= reader.GetInt32(1);
                    produit.NomMedicament = reader.GetString(2);
                    produit.PrixAchat = reader.GetDecimal(3);
                    produit.PrixVente = reader.GetDecimal(4);
                    produit.QuantiteAlerte = reader.GetInt32(5);
                    produit.Description = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    produit.Quantite = reader.GetInt32(8);
                    produit.DateExpiration = reader.GetDateTime(7);
                    listeMedicament.Add(produit);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeMedicament;
        }

        //liste des  medicament dont le stock est tres bas
        public static DataTable ListeStock(string code)
        {
            var dt = new DataTable();
            try
            {
                string requete = "SELECT * FROM stock WHERE num_medi = '" + code + "'";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste medicament", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        // supprimer les donnees du produit
        public static bool SupprimerMedicament(string medicamentId)
        {
            try
            {
                connection.Open();
                string requete = string.Format("DELETE FROM medicament WHERE (num_medi = '{0}')", medicamentId);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppresion des données a échoué", "Erreur d'insertion", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        // // methode pour ajouter un nouveau medicament
        public static bool AjouterFamilleMedicament(int typeId, string typeMedicament)
        {
            string requete = string.Format(@"SELECT * FROM familleTbl WHERE numeroFamille = {0}", typeId);
            command.CommandText = requete;
            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt1 = new DataTable(); dt1.Load(reader);
                int nombreTypeMedicament = dt1.Rows.Count;
                if (nombreTypeMedicament < 1)
                {
                    requete = string.Format("INSERT INTO `familleTbl` (`numeroFamille`, `designation`) " +
                                            " VaLUES ({0},@designation)", typeId);
                    command.Parameters.Add(new MySqlParameter("designation", typeMedicament));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    MonMessageBox.ShowBox(
                        "Le numéro du type produit " + typeId + " existe deja dans la base de données.",
                        "Erreur d'Insertion", "erreur.png");
                    return false;
                }
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

        // // methode pour supprimer une familled de medicament
        public static bool SupprimerFamilleMedicament(int codeFamille)
        {
            try
            {
                connection.Open();
                var requete = string.Format("DELETE FROM `familleTbl` WHERE `numeroFamille` = {0}", codeFamille);
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

        //liste des familles medicament
        public static List<Medicament> ListeDesFamille()
        {
            var  listeFamille = new List<Medicament>();
            try
            {
                string requete = "SELECT * FROM familleTbl ORDER BY designation";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var familleMedicament = new Medicament();
                    familleMedicament.CodeFamille = reader.GetInt32(0);
                    familleMedicament.Designation = reader.GetString(1);
                    listeFamille.Add(familleMedicament);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste famille", ex);
            }
            finally
            {
                connection.Close();
            }
            return listeFamille;
        }

        // // methode pour modifier un nouveau medicament
        public static void ModifierFamilleMedicament(int typeId, string type_medicament)
        {

            try
            {
                connection.Open();
                string requete = string.Format("UPDATE familleTbl SET designation = @designation WHERE numeroFamille = {0}",
                     typeId);
                command.Parameters.Add(new MySqlParameter("designation", type_medicament));
                command.CommandText = requete;
                command.ExecuteNonQuery();

                MonMessageBox.ShowBox("La modification des données a été effectuée avec succés",
                    "Information modification", "affirmation.png");
            }
            catch (Exception exception)
            {

                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur modification", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        //inserer une image du produit
        public static void ModifierImageMedicament(string num_medi, string image)
        {

            try
            {
                connection.Open();
                string requete = string.Format("UPDATE medicament SET image = @image WHERE (num_medi = '{0}')", num_medi);
                command.Parameters.Add(new MySqlParameter("image", image));
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Nouvelle image de médicament a été inserée avec succés", "Information Insertion",
                    "affirmation.png");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Insertion de l'image a échoué .", "Erreur insertion", exception, "erreur.png");
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

        }

        public static DataTable ImageMedicament(string param)
        {
            DataTable dt = new DataTable();
            try
            {
                string requete = "SELECT image FROM medicament WHERE num_medi = '" + param + "'";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        //image employe
        public static DataTable ImageEmploye(string param)
        {
            DataTable dt = new DataTable();
            try
            {
                string requete = "SELECT photos FROM employe WHERE num_empl = '" + param + "'";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

       
        public static bool ModifierMedicament(DataGridView dgv)
        {
            bool siEnregistrer = false;
            try
            {
                connection.Open();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string codeBarre = dgv.Rows[i].Cells[0].Value.ToString();
                    decimal prixAchat = decimal.Parse(dgv.Rows[i].Cells[3].Value.ToString());

                    string requete = "UPDATE medicament SET prix_achat = @prix_achat WHERE (num_medi ='" + codeBarre + "')";
                    command.Parameters.Add(new MySqlParameter("prix_achat", prixAchat));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                };
                siEnregistrer = true;
                MonMessageBox.ShowBox("La modification des données a été éffectuée avec succés",
    "Information modification", "affirmation.png");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des donnees a echoue", "Erreur modification", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
            }
            return siEnregistrer;
        }


        public static void ModifierMedicamentCodeBarre(string codeBarre, string codeBarre1)
        {
            try
            {
                connection.Open();

                string requete = "SELECT * FROM medicament WHERE (num_medi ='" + codeBarre1 + "')";
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                if (dt.Rows.Count >= 1)
                {
                    MonMessageBox.ShowBox("Le code barre " + codeBarre1 + " est utilisé deux fois pour de different produit", "Erreur", "erreur.png");
                    return;
                }
                requete = "UPDATE medicament SET num_medi = '" + codeBarre1 + "' WHERE (num_medi ='" + codeBarre + "')";
                command.CommandText = requete;
                if (command.ExecuteNonQuery() >= 1)
                {

                    MonMessageBox.ShowBox("La modification des données a été éffectuée avec succés",
                                    "Information modification", "affirmation.png");
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des donnees a echoue", "Erreur modification", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool ParametrerLesDetails(string codeBarre, int qteRestante, int nbreEnBoite, int nbreDetaille, decimal prixDetail)
        {
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();

                string requete = "SELECT * FROM stock WHERE (num_medi ='" + codeBarre + "')";
                command.CommandText = requete;
                command.Transaction = _transaction;
                var reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                if (dt.Rows.Count >= 1)
                {
                    requete=string.Format("UPDATE stock SET quantite = {0} WHERE num_medi = '{1}'"
                        ,qteRestante, codeBarre);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();

                    requete = string.Format("UPDATE medicament SET prixDetail = {0}, nbreBoite = {1}, nbreDetail={2} WHERE num_medi = '{3}'"
                     , prixDetail, nbreEnBoite, nbreDetaille, codeBarre);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                }
                _transaction.Commit();
                return true;
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur modification", exception,
                    "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static void  MisAjourAutomatique(string num)
        {
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
               var  requete =
                              string.Format("SELECT * FROM `medicament` WHERE `num_medi` = ('{0}') AND nbreBoite >0 AND prixDetail>0", num);
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        var reader1 = command.ExecuteReader();
                        var dt1 = new DataTable();
                        dt1.Load(reader1);
                       
                        reader1.Close();

                if(dt1.Rows.Count>0)
                { 
                    var nbreEnBoite = Int32.Parse(dt1.Rows[0].ItemArray[10].ToString());
                        var nbreDetaille = Int32.Parse(dt1.Rows[0].ItemArray[11].ToString());
                    // mettre a jour les quantites des dans le stock
                    if(nbreDetaille<=0)
                    {
                    requete =
                              string.Format("UPDATE `medicament` SET `NbreDetail` =  {0} WHERE `num_medi` = ('{1}')",
                                  nbreEnBoite, num);
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();

                        requete =
                       string.Format("UPDATE `stock` SET `quantite` = (`quantite` - 1 ) WHERE `num_medi` = ('{0}')", num);
                                 
                                command.CommandText = requete;
                                command.Transaction = _transaction;
                                command.ExecuteNonQuery();
                        }
                }
                _transaction.Commit();
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

    
        #endregion

        #region ENTREE_SORTIE
        public static List<Produit> LogStock(int id)
        {
            try
            {
                var listeProduit = new List<Produit>();
               var  _requete = "SELECT * FROM stock_mod WHERE id= " + id;
                command.CommandText = _requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Produit();
                    produit.GroupeID = reader.GetInt32(0);
                    produit.NumeroProduit = reader.GetString(1);
                    produit.PrixAchat = reader.GetDecimal(2);
                    produit.DifferenceStock = reader.GetInt32(3);
                    produit.Description = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    produit.DateExpiration = !reader.IsDBNull(5) ? reader.GetDateTime(5) : DateTime.Now;
                    listeProduit.Add(produit);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        public static List<Produit> LogStock()
        {
            try
            {
                var listeProduit = new List<Produit>();
                var _requete = "SELECT * FROM stock_mod  " ;
                command.CommandText = _requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Produit();
                    produit.GroupeID = reader.GetInt32(0);
                    produit.NumeroProduit = reader.GetString(1);
                    produit.PrixAchat = reader.GetDecimal(2);
                    produit.DifferenceStock = reader.GetInt32(3);
                    produit.Description = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    produit.DateExpiration = !reader.IsDBNull(5) ? reader.GetDateTime(5) : DateTime.Now;
                    listeProduit.Add(produit);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        //creer un produit
        public static bool ModifierStock(Produit produit, int idMouv, int etat,DocumentStock doc, int typeDepot, string indexInser)
        {
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();

                var _requete = "SELECT * FROM stock WHERE num_medi = @nummedi";
                command.Parameters.Add(new MySqlParameter("nummedi", produit.NumeroProduit));
                command.CommandText = _requete;
                command.Transaction = _transaction;
               var reader = command.ExecuteReader();
               var dt = new DataTable();
               dt.Load(reader);
               var grd_stock = Convert.ToInt32(dt.Rows[0].ItemArray[3].ToString());
               var stock = Convert.ToInt32(dt.Rows[0].ItemArray[2].ToString());
               if (indexInser == "1")
               {
                   _requete = "UPDATE stock_mod  SET qte=@qte,prix=@prix WHERE id =@id AND id_prod=@id_prod";
                   command.Parameters.Add(new MySqlParameter("id", idMouv));
                   command.Parameters.Add(new MySqlParameter("id_prod", produit.NumeroProduit));
                   command.Parameters.Add(new MySqlParameter("qte", produit.DifferenceStock));
                   command.Parameters.Add(new MySqlParameter("prix", produit.PrixAchat));
               }
               else
               {
                   _requete = "INSERT stock_mod (id, id_prod, qte,prix,lot,date) VALUES(@id,@id_prod,@qte,@prix,@lot,@date)";
                   command.Parameters.Add(new MySqlParameter("id", idMouv));
                   command.Parameters.Add(new MySqlParameter("id_prod", produit.NumeroProduit));
                   command.Parameters.Add(new MySqlParameter("qte", produit.DifferenceStock));
                   command.Parameters.Add(new MySqlParameter("prix", produit.PrixAchat));
                   command.Parameters.Add(new MySqlParameter("lot", produit.Description));
                   command.Parameters.Add(new MySqlParameter("date", produit.DateExpiration));
               }

                   if (etat == 2)
                   {
                       if (typeDepot == 1)
                       {
                           if (produit.DifferenceStock <= grd_stock)
                           {
                               command.Transaction = _transaction;
                               command.CommandText = _requete;
                               command.ExecuteNonQuery(); command.Parameters.Clear();
                               _transaction.Commit();
                               return true;
                           }
                           else
                           {
                               MonMessageBox.ShowBox("La quantité n'est pas assez grande (" + grd_stock + ") pour transferer", "Erreur", "erreur.png");
                               return false;
                           }
                       }
                       else if (typeDepot == 2)
                       {
                           if (produit.DifferenceStock <= stock)
                           {
                               command.Transaction = _transaction;
                               command.CommandText = _requete;
                               command.ExecuteNonQuery(); command.Parameters.Clear();
                               _transaction.Commit();
                               return true;
                           }
                           else
                           {
                               MonMessageBox.ShowBox("La quantité n'est pas assez grande (" + grd_stock + ") pour transferer", "Erreur", "erreur.png");
                               return false;
                           }
                       }
                       else
                       {
                           return false;
                       }
                   }
                   else
                   {
                       command.Transaction = _transaction;
                       command.CommandText = _requete;
                       command.ExecuteNonQuery(); command.Parameters.Clear();
                       _transaction.Commit();
                       return true;
                   }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
        public static bool ValidererMouvementStock(DataGridView dgvMouvement,  int idMouv, int etat, DocumentStock doc,int etatDepot)
        {
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();

                var _requete = "UPDATE stock_mouv_tbl SET etatValider=@etatValider WHERE id = "+idMouv;
                command.Parameters.Add(new MySqlParameter("etatValider", true));
                command.CommandText = _requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                foreach (DataGridViewRow dgrRow in dgvMouvement.Rows)
                {
                    var produit = new Produit();
                    produit.NumeroProduit = dgrRow.Cells[0].Value.ToString();
                    produit.DifferenceStock = (int)Convert.ToDouble(dgrRow.Cells[4].Value.ToString());
                    produit.PrixAchat = decimal.Parse(dgrRow.Cells[3].Value.ToString());
                    produit.Description = dgrRow.Cells[2].Value.ToString();

                     _requete = "SELECT * FROM stock WHERE num_medi = @nummedi";
                    command.Parameters.Add(new MySqlParameter("nummedi", produit.NumeroProduit));
                    command.CommandText = _requete;
                    command.Transaction = _transaction;
                    var reader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(reader);
                    var grd_stock = Convert.ToInt32(dt.Rows[0].ItemArray[3].ToString());
                    var stock = Convert.ToInt32(dt.Rows[0].ItemArray[2].ToString());
                    if (etatDepot == 1)
                    {
                        if (etat == 1)
                        {
                            if (grd_stock < 0)
                            {
                                _requete = string.Format("UPDATE stock SET grd_stock = {0} WHERE num_medi ='{1}'",
                                       produit.DifferenceStock, produit.NumeroProduit);
                            }
                            else
                            {
                                _requete = string.Format("UPDATE stock SET grd_stock = grd_stock + {0} WHERE num_medi ='{1}'",
                                       produit.DifferenceStock, produit.NumeroProduit);
                            }
                            command.Transaction = _transaction;
                            command.CommandText = _requete;
                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
                            _requete = string.Format("UPDATE `lot_tbl`  SET qte=qte+{0} WHERE lot = @lot AND num_prod = @numProd AND qte >0 ", produit.DifferenceStock);
                            command.Parameters.Add(new MySqlParameter("numProd", produit.NumeroProduit));
                            command.Parameters.Add(new MySqlParameter("lot", produit.Description));
                            //command.Parameters.Add(new MySqlParameter("qte", produit.Description));
                            command.Transaction = _transaction;
                            command.CommandText = _requete;
                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
                        }
                        else if (etat == 2)
                        {
                            if (produit.DifferenceStock <= grd_stock)
                            {
                                if (doc.Reference.Contains("pharmacie".ToUpper()) || doc.Destination.ToUpper().Contains("PHARMACIE"))
                                {
                                    if (stock >= 0)
                                    {
                                        _requete = string.Format("UPDATE stock SET quantite =quantite + {0}, grd_stock =grd_stock - {0} WHERE num_medi ='{1}'",
                                            produit.DifferenceStock, produit.NumeroProduit);
                                        command.Transaction = _transaction;
                                        command.CommandText = _requete;
                                        command.ExecuteNonQuery();
                                        command.Parameters.Clear();

                                    }
                                    else
                                    {
                                        _requete = string.Format("UPDATE stock SET quantite = {0}, grd_stock =grd_stock - {0} WHERE num_medi ='{1}'",
                                     produit.DifferenceStock, produit.NumeroProduit);
                                        command.Transaction = _transaction;
                                        command.CommandText = _requete;
                                        command.ExecuteNonQuery();
                                        command.Parameters.Clear();
                                    }

                                }
                                else
                                {
                                    _requete = string.Format("UPDATE stock SET grd_stock = grd_stock - {0} WHERE num_medi ='{1}'",
                                        produit.DifferenceStock, produit.NumeroProduit);
                                    command.Transaction = _transaction;
                                    command.CommandText = _requete;
                                    command.ExecuteNonQuery();
                                    command.Parameters.Clear();
                                }

                                _requete = string.Format("UPDATE `lot_tbl`  SET qte=qte-{0} WHERE lot = @lot AND num_prod = @numProd AND qte >0 ", produit.DifferenceStock);
                                command.Parameters.Add(new MySqlParameter("numProd", produit.NumeroProduit));
                                command.Parameters.Add(new MySqlParameter("lot", produit.Description));
                                command.Transaction = _transaction;
                                command.CommandText = _requete;
                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("La quantité n'est pas assez grande (" + grd_stock + ") pour transferer", "Erreur", "erreur.png");
                            return false;
                        }
                    } 
                    else if (etatDepot == 2)
                    {
                        if (etat == 1)
                        {
                            //if (grd_stock < 0)
                            //{
                            //    _requete = string.Format("UPDATE stock SET grd_stock = {0} WHERE num_medi ='{1}'",
                            //           produit.DifferenceStock, produit.NumeroProduit);
                            //}
                            //else
                            //{
                            //    _requete = string.Format("UPDATE stock SET grd_stock = grd_stock + {0} WHERE num_medi ='{1}'",
                            //           produit.DifferenceStock, produit.NumeroProduit);
                            //}
                            //command.Transaction = _transaction;
                            //command.CommandText = _requete;
                            //command.ExecuteNonQuery();
                            //command.Parameters.Clear();
                            //_requete = string.Format("UPDATE `lot_tbl`  SET qte=qte+{0} WHERE lot = @lot AND num_prod = @numProd AND qte >0 ", produit.DifferenceStock);
                            //command.Parameters.Add(new MySqlParameter("numProd", produit.NumeroProduit));
                            //command.Parameters.Add(new MySqlParameter("lot", produit.Description));
                            ////command.Parameters.Add(new MySqlParameter("qte", produit.Description));
                            //command.Transaction = _transaction;
                            //command.CommandText = _requete;
                            //command.ExecuteNonQuery();
                            //command.Parameters.Clear();
                        }
                        else if (etat == 2)
                        {
                            if (produit.DifferenceStock <= stock)
                            {
                                      _requete = string.Format("UPDATE stock SET quantite = quantite - {0} WHERE num_medi ='{1}'",
                                        produit.DifferenceStock, produit.NumeroProduit);
                                    command.Transaction = _transaction;
                                    command.CommandText = _requete;
                                    command.ExecuteNonQuery();
                                    command.Parameters.Clear();
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("La quantité n'est pas assez grande (" + grd_stock + ") pour transferer", "Erreur", "erreur.png");
                            return false;
                        }
                    }
                }
                MonMessageBox.ShowBox("Données validées avec succés", "Affirmation", "affirmation.png");
                _transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static bool RetirerStock(Produit produit, int etat, DocumentStock docu)
        {
            try
            {
                connection.Open();
               var  _requete = "DELETE FROM stock_mod WHERE id_prod = '" + produit.NumeroProduit+"' AND id = " +docu.ID;
                command.CommandText = _requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

          public static bool CreerUnNouveauDocumentStock(DocumentStock doc)
        {
            try
            {
                connection.Open();
                //_transaction = connection.BeginTransaction();
                var _requete = "INSERT INTO doc_stock (reference,date,source,destin,etat) value (@reference,@date,@source,@destin,@etat)";

                command.Parameters.Add(new MySqlParameter("reference", doc.Reference));
                command.Parameters.Add(new MySqlParameter("date", doc.Date));
                command.Parameters.Add(new MySqlParameter("source", doc.Source));
                command.Parameters.Add(new MySqlParameter("destin", doc.Destination));
                command.Parameters.Add(new MySqlParameter("etat", doc.Etat));

                command.CommandText = _requete;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

          public static bool SupprimerUnNouveauDocumentStock(int idDoc)
          {
              try
              {
                  connection.Open();
                  //_transaction = connection.BeginTransaction();
                  var _requete = "DELETE FROM doc_stock WHERE id = "+idDoc;
                  command.CommandText = _requete;
                  command.ExecuteNonQuery();
                  return true;
              }
              catch (Exception ex)
              {
                  MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                  return false;
              }
              finally
              {
                  connection.Close();
              }
          }

          public static bool CreerUnNouveauMouvement(DocumentStock doc)
          {
              try
              {
                  connection.Open();
                  //_transaction = connection.BeginTransaction();
                  var _requete = "INSERT INTO stock_mouv_tbl (date,id_user,idref,etat,etatValider, etatDepot) value (@date,@id_user,@idref,@etat, false,@etatDepot)";

                  command.Parameters.Add(new MySqlParameter("id_user", Form1.numEmploye));
                  command.Parameters.Add(new MySqlParameter("date", doc.Date));
                  command.Parameters.Add(new MySqlParameter("idref", doc.IDReference));
                  command.Parameters.Add(new MySqlParameter("etat", doc.Etat));
                  command.Parameters.Add(new MySqlParameter("etatDepot", doc.EtatDepot));
                  command.CommandText = _requete;
                  command.ExecuteNonQuery();
                  command.Parameters.Clear();
                  return true;
              }
              catch (Exception ex)
              {
                  MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                  return false;
              }
              finally
              {
                  connection.Close();
              }
          }
          public static int  DernierNouveauMouvement()
          {
              try
              {
                  connection.Open();
                  //_transaction = connection.BeginTransaction();
                  var _requete = "SELECT MAX(id) FROM stock_mouv_tbl ";
                  command.CommandText = _requete;
                return (int)  command.ExecuteScalar();
              }
              catch (Exception ex)
              {
                  return 0;
              }
              finally
              {
                  connection.Close();
              }
          }

        public static bool MettreAjourDocumentStock(DocumentStock doc)
        {
            try
            {
                connection.Open();
                var _requete = "UPDATE doc_stock SET reference=@reference,date=@date,source=@source,destin= @destin WHERE id =" + doc.IDReference;

                command.Parameters.Add(new MySqlParameter("reference", doc.Reference));
                command.Parameters.Add(new MySqlParameter("date", doc.Date));
                command.Parameters.Add(new MySqlParameter("source", doc.Source));
                command.Parameters.Add(new MySqlParameter("destin", doc.Destination));
                command.CommandText = _requete;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool MettreAjourDocumentStock(int idReference, int idEtatPrix)
        {
            try
            {
                connection.Open();
                var _requete = "UPDATE doc_stock SET etatPrix=@etatPrix WHERE id =" + idReference;

                command.Parameters.Add(new MySqlParameter("etatPrix", idEtatPrix));
                command.CommandText = _requete;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<DocumentStock> ListeDocumentStock(int etat)
        {
            try
            {
                var listeProduit = new List<DocumentStock>();
                var _requete = "SELECT * FROM doc_stock WHERE etat = "+etat;
                command.CommandText = _requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var doc = new DocumentStock();
                    doc.IDReference = reader.GetInt32(0);
                    doc.Reference = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    doc.Date = !reader.IsDBNull(2) ? reader.GetDateTime(2) : DateTime.Now;
                    doc.Destination = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    doc.Source = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    doc.EtatValider = !reader.IsDBNull(5) ? reader.GetBoolean(5) : true;
                    doc.TypePrix = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
                    listeProduit.Add(doc);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<DocumentStock> ListeDocumentStockExclu(int etat)
        {
            try
            {
                var listeProduit = new List<DocumentStock>();
                var _requete = "SELECT * FROM doc_stock  WHERE (`reference`  NOT LIKE '%périmés%' OR  `reference`  NOT LIKE '%avariés%') AND  etat = " + etat;
                command.CommandText = _requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var doc = new DocumentStock();
                    doc.IDReference = reader.GetInt32(0);
                    doc.Reference = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    doc.Date = !reader.IsDBNull(2) ? reader.GetDateTime(2) : DateTime.Now;
                    doc.Destination = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    doc.Source = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    doc.EtatValider = !reader.IsDBNull(5) ? reader.GetBoolean(5) : true;
                    listeProduit.Add(doc);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<DocumentStock> ListeMouvementStock(int etatDepot)
        {
            try
            {
                var listeProduit = new List<DocumentStock>();
                var _requete = "SELECT * FROM stock_mouv_tbl WHERE etatDepot="+etatDepot;
                command.CommandText = _requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var doc = new DocumentStock();
                    doc.ID= reader.GetInt32(0);
                    doc.NumeroMatricule = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    doc.Date = !reader.IsDBNull(1) ? reader.GetDateTime(1) : DateTime.Now;
                    doc.Etat = reader.GetInt32(4);
                    doc.IDReference = reader.GetInt32(3);
                    doc.EtatValider=!reader.IsDBNull(5) ? reader.GetBoolean(5) : true;
                    doc.EtatDepot = !reader.IsDBNull(6) ? reader.GetInt32(6) : 1;
                    listeProduit.Add(doc);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Produit> ListeMouvementStockGroupeParQuantite(int etatDepot, int etat, DateTime dateDebut, DateTime dateFin, string Reference )
        {
            try
            {
                var listeProduit = new List<Produit>();
                var _requete = "SELECT stock_mod.id_prod, medicament.nom_medi, SUM(stock_mod.qte), stock_mod.prix FROM medicament INNER JOIN stock_mod ON medicament.num_medi "+
                "= stock_mod.id_prod INNER JOIN  stock_mouv_tbl ON stock_mod.id = stock_mouv_tbl.id INNER JOIN doc_stock ON stock_mouv_tbl.idRef = doc_stock.id "+
                " WHERE  (stock_mouv_tbl.etat = "+ etat +") AND "+
                " (stock_mouv_tbl.etatDepot = " + etatDepot + ") AND (stock_mouv_tbl.`date`>=@dateDebut) AND (stock_mouv_tbl.`date` <@dateFin) AND (doc_stock.reference =@reference) GROUP BY medicament.nom_medi, stock_mod.prix ";
                command.CommandText = _requete;
                command.Parameters.Add(new MySqlParameter("dateDebut", dateDebut));
                command.Parameters.Add(new MySqlParameter("dateFin", dateFin.AddHours(24)));
                command.Parameters.Add(new MySqlParameter("reference", Reference));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Produit();
                    produit.NumeroProduit = reader.GetString(0);
                    produit.Designation = reader.GetString(1);
                    produit.PrixAchat = reader.GetDecimal(3);
                    produit.DifferenceStock = reader.GetInt32(2);
                    listeProduit.Add(produit);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
        public static List<Produit> ListeMouvementStockGroupeParQuantiteExclu(int etatDepot, int etat, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                var listeProduit = new List<Produit>();
                var _requete = "SELECT stock_mod.id_prod, medicament.nom_medi, SUM(stock_mod.qte), stock_mod.prix FROM medicament INNER JOIN stock_mod ON medicament.num_medi " +
                "= stock_mod.id_prod INNER JOIN  stock_mouv_tbl ON stock_mod.id = stock_mouv_tbl.id INNER JOIN doc_stock ON stock_mouv_tbl.idRef = doc_stock.id " +
                " WHERE  (stock_mouv_tbl.etat = " + etat + ") AND " +
                " (stock_mouv_tbl.etatDepot = " + etatDepot + ") AND (stock_mouv_tbl.`date`>=@dateDebut) AND (stock_mouv_tbl.`date` <@dateFin) AND "+
                "(doc_stock.`reference`  NOT LIKE '%périmés%' OR  doc_stock.`reference`  NOT LIKE '%avariés%') GROUP BY medicament.nom_medi, stock_mod.prix ";
                command.CommandText = _requete;
                command.Parameters.Add(new MySqlParameter("dateDebut", dateDebut));
                command.Parameters.Add(new MySqlParameter("dateFin", dateFin.AddHours(24)));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Produit();
                    produit.NumeroProduit = reader.GetString(0);
                    produit.Designation = reader.GetString(1);
                    produit.PrixAchat = reader.GetDecimal(3);
                    produit.DifferenceStock = reader.GetInt32(2);
                    listeProduit.Add(produit);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
        public static List<Produit> ListeMouvementStockGroupeParQuantite(int etatDepot, int etat, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                var listeProduit = new List<Produit>();
                var _requete = "SELECT medicament.nom_medi, medicament.nom_medi, SUM(stock_mod.qte), stock_mod.prix FROM medicament INNER JOIN stock_mod ON medicament.num_medi " +
                "= stock_mod.id_prod INNER JOIN  stock_mouv_tbl ON stock_mod.id = stock_mouv_tbl.id INNER JOIN doc_stock ON stock_mouv_tbl.idRef = doc_stock.id " +
                " WHERE  (stock_mouv_tbl.etat = " + etat + ") AND " +
                " (stock_mouv_tbl.etatDepot = " + etatDepot + ") AND (stock_mouv_tbl.`date`>=@dateDebut) AND (stock_mouv_tbl.`date` <@dateFin) GROUP BY medicament.nom_medi, stock_mod.prix ";
                command.CommandText = _requete;
                command.Parameters.Add(new MySqlParameter("dateDebut", dateDebut));
                command.Parameters.Add(new MySqlParameter("dateFin", dateFin.AddHours(24)));
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var produit = new Produit();
                    produit.NumeroProduit = reader.GetString(0);
                    produit.Designation = reader.GetString(1);
                    produit.PrixAchat = reader.GetDecimal(3);
                    produit.DifferenceStock = reader.GetInt32(2);
                    listeProduit.Add(produit);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
     
        public static int  SiMouvementValider(int numero)
        {
            try
            {
                var _requete = "SELECT * FROM stock_mouv_tbl WHERE etatValider =true AND id="+numero;
                command.CommandText = _requete;
                connection.Open();
              var reader = command.ExecuteReader();
              var dt = new DataTable();
              dt.Load(reader);
              if (dt.Rows.Count>0)
              {
                  return 1;
              }
              else
                  return 0;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste produit", ex);
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool RetirerMouvement( int id)
        {
            try
            {
                connection.Open();
                var _requete = "DELETE FROM stock_mouv_tbl WHERE id = " + id;
                command.CommandText = _requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        #endregion
        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        #region VENTE_PAIEMENT

        public static bool EnregistrerVente(Vente vente, DataGridView dgvVente, Client client, bool part)
        {
            bool flag = false;
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                //inserer les donnees du client
                string requete = "SELECT * FROM clienttbl WHERE nomClient = @nomClient AND matricule = '" + client.Matricule + "'";
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.Parameters.Add(new MySqlParameter("nomClient", client.NomClient));
                var reader2 = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader2);
                reader2.Close();
                if (dt.Rows.Count > 0)
                {
                    client.Id = Int32.Parse(dt.Rows[0].ItemArray[0].ToString()); requete = string.Format("UPDATE  `clientTbl`  SET  `matricule` =@matricule,`entrep`=@entrep where id= " + client.Id);
                    command.CommandText = requete;
                    command.Parameters.Add(new MySqlParameter("entrep", client.Entreprise));
                    command.Parameters.Add(new MySqlParameter("matricule", client.Matricule));
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                }
                else
                {
                    requete = string.Format("INSERT INTO `clientTbl` ( `nomClient`, `tel`, `matricule`,`entrep`) " +
                                                 " VALUES ('{0}', '{1}', '{2}',@entrep)", client.NomClient, client.Telephone, client.Matricule);
                    command.CommandText = requete;
                    command.Parameters.Add(new MySqlParameter("entrep", client.Entreprise));
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();

                    //selectionner le dernier numero de client
                    requete = "select MAX(ID)  from clientTbl";
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                    client.Id = (int)command.ExecuteScalar();
                }

                //inserer les donnees de vente
                requete = string.Format("INSERT INTO `detail_vente` (  `date_vente`, `prix_total`, `num_client`, `num_empl`, `heure`,etat,etatPart) " +
                                        " VALUES (@date_vente1, @prix_total1, @num_client1,@num_empl1,@heure1,@etat1, @etatPart)");
                command.Parameters.Add(new MySqlParameter("date_vente1", vente.DateVente));
                command.Parameters.Add(new MySqlParameter("prix_total1", vente.PrixTotal));
                command.Parameters.Add(new MySqlParameter("num_empl1", vente.NumeroEmploye));
                command.Parameters.Add(new MySqlParameter("num_client1", client.Id));
                command.Parameters.Add(new MySqlParameter("heure1", vente.Heure));
                command.Parameters.Add(new MySqlParameter("etat1", vente.SiVente));
                command.Parameters.Add(new MySqlParameter("etatPart", vente.PartAssure));
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();


                //selectionner le dernier numero de vente
                requete = "select MAX(num_vente)  from detail_vente";
                command.CommandText = requete;
                command.Transaction = _transaction;
                var venteId = (int)command.ExecuteScalar();


                if (vente.SiVente || part)
                {
                    requete = string.Format("INSERT INTO `paie_tbl` (  `date_paye`, `montant`, `id_client`, `num_empl`,etat,num_vente) " +
                                          " VALUES (@date_paye, @montant, @id_client,@num_empll,@etatp,@idVente)");
                    command.Parameters.Add(new MySqlParameter("date_paye", vente.DateVente));
                    command.Parameters.Add(new MySqlParameter("montant", vente.TotalPaye));
                    command.Parameters.Add(new MySqlParameter("num_empll", vente.NumeroEmploye));
                    command.Parameters.Add(new MySqlParameter("id_client", client.Id));
                    command.Parameters.Add(new MySqlParameter("etatp", true));
                    command.Parameters.Add(new MySqlParameter("idVente", venteId));
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                }

                //inserer les details de vente
                var prixTotalCredit = 0m;
                for (int I = 0; I <= dgvVente.Rows.Count - 1; I++)
                {
                    string numMedicament = dgvVente.Rows[I].Cells[0].Value.ToString();
                    //  string nom_medicament = lstVente.Items[I].SubItems[1].Text;
                    int quantite = Int32.Parse(dgvVente.Rows[I].Cells[5].Value.ToString());
                    decimal prixAchat = decimal.Parse(dgvVente.Rows[I].Cells[2].Value.ToString());
                    decimal prixVente = decimal.Parse(dgvVente.Rows[I].Cells[3].Value.ToString());
                    decimal prixTotal;
                    if (dgvVente.Rows[I].Cells[6].Value.ToString().Contains(","))
                    {
                        prixTotal = decimal.Parse(dgvVente.Rows[I].Cells[6].Value.ToString().Substring(0,
                            dgvVente.Rows[I].Cells[6].Value.ToString().IndexOf(",")));
                    }
                    else
                    {
                        prixTotal = decimal.Parse(dgvVente.Rows[I].Cells[6].Value.ToString());
                    }

                    requete = string.Format("INSERT INTO `vente` (`num_vente`, `num_medi`, `prix_achat`," +
                                            "`prix_vente`, `quantite`,`prixTotal`, `id`) VALUES ({0},'{1}', {2}, {3}, {4},{5},{6})",
                        venteId, numMedicament, prixAchat, prixVente, quantite, prixTotal, I);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();

                    requete =
                        string.Format("UPDATE `stock` SET `quantite` = (`quantite` - {0} ) WHERE `num_medi` = ('{1}')",
                            quantite, numMedicament);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                    prixTotalCredit += prixAchat * quantite;
                }
                #region Part
                if (part)
                {
                    requete = string.Format("INSERT INTO `detail_vente` (  `date_vente`, `prix_total`, `num_client`, `num_empl`, `heure`,etat) " +
                                      " VALUES (@date_vente, @prix_total, @num_client,@num_empl,@heure,@etat)");
                    command.Parameters.Add(new MySqlParameter("date_vente", vente.DateVente));
                    command.Parameters.Add(new MySqlParameter("prix_total", prixTotalCredit * (decimal)vente.PartAssure/100));
                    command.Parameters.Add(new MySqlParameter("num_empl", vente.NumeroEmploye));
                    command.Parameters.Add(new MySqlParameter("num_client", client.Id));
                    command.Parameters.Add(new MySqlParameter("heure", vente.Heure));
                    command.Parameters.Add(new MySqlParameter("etat", false));
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();

                    requete = "select MAX(num_vente)  from detail_vente";
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    var venteId1 = (int)command.ExecuteScalar();


                    //inserer les details de vente
                    for (int I = 0; I <= dgvVente.Rows.Count - 1; I++)
                    {
                        string numMedicament = dgvVente.Rows[I].Cells[0].Value.ToString();
                        //  string nom_medicament = lstVente.Items[I].SubItems[1].Text;
                        int quantite = Int32.Parse(dgvVente.Rows[I].Cells[5].Value.ToString());
                        decimal prixAchat = decimal.Parse(dgvVente.Rows[I].Cells[2].Value.ToString());
                        decimal prixVente = decimal.Parse(dgvVente.Rows[I].Cells[2].Value.ToString()) *(decimal)vente.PartAssure/100;
                        decimal prixTotal;
                        if (dgvVente.Rows[I].Cells[6].Value.ToString().Contains(","))
                        {
                            prixTotal = decimal.Parse(dgvVente.Rows[I].Cells[6].Value.ToString().Substring(0,
                                dgvVente.Rows[I].Cells[6].Value.ToString().IndexOf(",")));
                        }
                        else
                        {
                            prixTotal = decimal.Parse(dgvVente.Rows[I].Cells[6].Value.ToString());
                        }

                        requete = string.Format("INSERT INTO `vente` (`num_vente`, `num_medi`, `prix_achat`," +
                                                "`prix_vente`, `quantite`,`prixTotal`, `id`) VALUES ({0},'{1}', {2}, {3}, {4},{5},{6})",
                            venteId1, numMedicament, prixAchat, prixVente, quantite, prixVente * quantite, I);
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();

                    }
                }

                #endregion
                _transaction.Commit();
                flag = true;
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    flag = false;
                }
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        // enregistrer une vente
        //public static bool EnregistrerVente(Vente vente, DataGridView dgvVente, Client client, bool part)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        connection.Open();
        //        _transaction = connection.BeginTransaction();
        //        //inserer les donnees du client
        //        string requete = "SELECT * FROM clienttbl WHERE nomClient = @nomClient AND matricule = '"+client.Matricule+"'";
        //        command.CommandText = requete;
        //        command.Transaction = _transaction;
        //        command.Parameters.Add(new MySqlParameter("nomClient", client.NomClient));
        //        var reader2 = command.ExecuteReader();
        //        var dt = new DataTable();
        //        dt.Load(reader2);
        //        reader2.Close();
        //        if (dt.Rows.Count > 0)
        //        {
        //            client.Id = Int32.Parse(dt.Rows[0].ItemArray[0].ToString()); requete = string.Format("UPDATE  `clientTbl`  SET  `matricule` =@matricule,`entrep`=@entrep where id= " +client.Id);
        //            command.CommandText = requete;
        //            command.Parameters.Add(new MySqlParameter("entrep", client.Entreprise));
        //            command.Parameters.Add(new MySqlParameter("matricule", client.Matricule));
        //            command.Transaction = _transaction;
        //            command.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            requete = string.Format("INSERT INTO `clientTbl` ( `nomClient`, `tel`, `matricule`,`entrep`) " +
        //                                         " VALUES ('{0}', '{1}', '{2}',@entrep)", client.NomClient, client.Telephone,client.Matricule);
        //            command.CommandText = requete;
        //            command.Parameters.Add(new MySqlParameter("entrep", client.Entreprise));
        //            command.Transaction = _transaction;
        //            command.ExecuteNonQuery();

        //            //selectionner le dernier numero de client
        //            requete = "select MAX(ID)  from clientTbl";
        //            command.CommandText = requete;
        //            command.Transaction = _transaction;
        //            command.ExecuteNonQuery();
        //            client.Id = (int)command.ExecuteScalar();
        //        }

        //        //inserer les donnees de vente
        //        requete = string.Format("INSERT INTO `detail_vente` (  `date_vente`, `prix_total`, `num_client`, `num_empl`, `heure`,etat,etatPart) " +
        //                                " VALUES (@date_vente1, @prix_total1, @num_client1,@num_empl1,@heure1,@etat1, @etatPart)");
        //        command.Parameters.Add(new MySqlParameter("date_vente1", vente.DateVente));
        //        command.Parameters.Add(new MySqlParameter("prix_total1", vente.PrixTotal));
        //        command.Parameters.Add(new MySqlParameter("num_empl1", vente.NumeroEmploye));
        //        command.Parameters.Add(new MySqlParameter("num_client1", client.Id));
        //        command.Parameters.Add(new MySqlParameter("heure1", vente.Heure));
        //        command.Parameters.Add(new MySqlParameter("etat1", vente.SiVente));
        //        command.Parameters.Add(new MySqlParameter("etatPart", vente.PartAssure));
        //        command.CommandText = requete;
        //        command.Transaction = _transaction;
        //        command.ExecuteNonQuery();


        //        //selectionner le dernier numero de vente
        //        requete = "select MAX(num_vente)  from detail_vente";
        //        command.CommandText = requete;
        //        command.Transaction = _transaction;
        //        var venteId = (int)command.ExecuteScalar();
               

        //        if (vente.SiVente || part)
        //        {
        //            requete = string.Format("INSERT INTO `paie_tbl` (  `date_paye`, `montant`, `id_client`, `num_empl`,etat,num_vente) " +
        //                                  " VALUES (@date_paye, @montant, @id_client,@num_empll,@etatp,@idVente)");
        //            command.Parameters.Add(new MySqlParameter("date_paye", vente.DateVente));
        //            command.Parameters.Add(new MySqlParameter("montant", vente.TotalPaye));
        //            command.Parameters.Add(new MySqlParameter("num_empll", vente.NumeroEmploye));
        //            command.Parameters.Add(new MySqlParameter("id_client", client.Id));
        //            command.Parameters.Add(new MySqlParameter("etatp", true));
        //            command.Parameters.Add(new MySqlParameter("idVente", venteId));
        //            command.CommandText = requete;
        //            command.Transaction = _transaction;
        //            command.ExecuteNonQuery();
        //        }

        //        //inserer les details de vente
        //        var prixTotalCredit = 0m;
        //        for (int I = 0; I <= dgvVente.Rows.Count - 1; I++)
        //        {
        //            string numMedicament = dgvVente.Rows[I].Cells[0].Value.ToString();
        //            //  string nom_medicament = lstVente.Items[I].SubItems[1].Text;
        //            int quantite = Int32.Parse(dgvVente.Rows[I].Cells[5].Value.ToString());
        //            decimal prixAchat = decimal.Parse(dgvVente.Rows[I].Cells[2].Value.ToString());
        //            decimal prixVente = decimal.Parse(dgvVente.Rows[I].Cells[3].Value.ToString());
        //            decimal prixTotal;
        //            if (dgvVente.Rows[I].Cells[6].Value.ToString().Contains(","))
        //            {
        //                prixTotal = decimal.Parse(dgvVente.Rows[I].Cells[6].Value.ToString().Substring(0,
        //                    dgvVente.Rows[I].Cells[6].Value.ToString().IndexOf(",")));
        //            }
        //            else
        //            {
        //                prixTotal = decimal.Parse(dgvVente.Rows[I].Cells[6].Value.ToString());
        //            }

        //            requete = string.Format("INSERT INTO `vente` (`num_vente`, `num_medi`, `prix_achat`," +
        //                                    "`prix_vente`, `quantite`,`prixTotal`, `id`) VALUES ({0},'{1}', {2}, {3}, {4},{5},{6})",
        //                venteId, numMedicament, prixAchat, prixVente, quantite, prixTotal, I);
        //            command.CommandText = requete;
        //            command.Transaction = _transaction;
        //            command.ExecuteNonQuery();

        //                requete =
        //                    string.Format("UPDATE `stock` SET `quantite` = (`quantite` - {0} ) WHERE `num_medi` = ('{1}')",
        //                        quantite, numMedicament);
        //                command.CommandText = requete;
        //                command.Transaction = _transaction;
        //                command.ExecuteNonQuery();
        //                prixTotalCredit += prixAchat * quantite;
        //        }
        //        #region Part
        //        //if (part)
        //        {
        //            //requete = string.Format("INSERT INTO `detail_vente` (  `date_vente`, `prix_total`, `num_client`, `num_empl`, `heure`,etat) " +
        //            //                  " VALUES (@date_vente, @prix_total, @num_client,@num_empl,@heure,@etat)");
        //            //command.Parameters.Add(new MySqlParameter("date_vente", vente.DateVente));
        //            //command.Parameters.Add(new MySqlParameter("prix_total", prixTotalCredit));
        //            //command.Parameters.Add(new MySqlParameter("num_empl", vente.NumeroEmploye));
        //            //command.Parameters.Add(new MySqlParameter("num_client", client.Id));
        //            //command.Parameters.Add(new MySqlParameter("heure", vente.Heure));
        //            //command.Parameters.Add(new MySqlParameter("etat", false));
        //            //command.CommandText = requete;
        //            //command.Transaction = _transaction;
        //            //command.ExecuteNonQuery();

        //            //requete = "select MAX(num_vente)  from detail_vente";
        //            //command.CommandText = requete;
        //            //command.Transaction = _transaction;
        //            //var venteId1 = (int)command.ExecuteScalar();


        //            ////inserer les details de vente
        //            //for (int I = 0; I <= dgvVente.Rows.Count - 1; I++)
        //            //{
        //            //    string numMedicament = dgvVente.Rows[I].Cells[0].Value.ToString();
        //            //    //  string nom_medicament = lstVente.Items[I].SubItems[1].Text;
        //            //    int quantite = Int32.Parse(dgvVente.Rows[I].Cells[5].Value.ToString());
        //            //    decimal prixAchat = decimal.Parse(dgvVente.Rows[I].Cells[2].Value.ToString());
        //            //    decimal prixVente = decimal.Parse(dgvVente.Rows[I].Cells[2].Value.ToString());
        //            //    decimal prixTotal;
        //            //    if (dgvVente.Rows[I].Cells[6].Value.ToString().Contains(","))
        //            //    {
        //            //        prixTotal = decimal.Parse(dgvVente.Rows[I].Cells[6].Value.ToString().Substring(0,
        //            //            dgvVente.Rows[I].Cells[6].Value.ToString().IndexOf(",")));
        //            //    }
        //            //    else
        //            //    {
        //            //        prixTotal = decimal.Parse(dgvVente.Rows[I].Cells[6].Value.ToString());
        //            //    }

        //            //    requete = string.Format("INSERT INTO `vente` (`num_vente`, `num_medi`, `prix_achat`," +
        //            //                            "`prix_vente`, `quantite`,`prixTotal`, `id`,partIn) VALUES ({0},'{1}', {2}, {3}, {4},{5},{6} ,{7})",
        //            //        venteId1, numMedicament, prixAchat, prixVente, quantite, prixVente*quantite, I, 1);
        //            //    command.CommandText = requete;
        //            //    command.Transaction = _transaction;
        //            //    command.ExecuteNonQuery();

        //            //}
        //        }

        //        #endregion
        //            _transaction.Commit();
        //        flag = true;
        //    }
        //    catch (Exception exception)
        //    {
        //        if (_transaction != null)
        //        {
        //            _transaction.Rollback();
        //            flag = false;
        //        }
        //        MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
        //    }
        //    finally
        //    {
        //        connection.Close();
        //        command.Parameters.Clear();
        //    }
        //    return flag;
        //}

        // supprimer les donnees de la vente produit produit
        public static void SupprimerVentes(int num_vente, string num_medi)
        {
            try
            {
                connection.Open();
                var requete = "SELECT * FROM vente WHERE num_vente = " + num_vente + " AND num_medi = '" + num_medi + "'";
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                decimal prixTotal = 0;
                if (dataTable.Rows.Count > 0)
                {
                    var quantite = Int32.Parse(dataTable.Rows[0].ItemArray[4].ToString());
                    _transaction = connection.BeginTransaction();
                    // mettre a jour les quantites des dans le stock
                    requete = string.Format("UPDATE `stock` SET `quantite` = (`quantite` + {0} ) WHERE `num_medi` = ('{1}')",
                    quantite, num_medi);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();

                    requete = string.Format("DELETE FROM vente WHERE(num_vente = {0} AND num_medi = '{1}')", num_vente, num_medi);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                    prixTotal += decimal.Parse(dataTable.Rows[0].ItemArray[5].ToString());
                    requete = string.Format("UPDATE `detail_vente` SET `prix_total` = (`prix_total` - {0} ) WHERE `num_vente` = ({1})", prixTotal, num_vente);
                    command.CommandText = requete;
                    command.Transaction = _transaction;;
                    command.ExecuteNonQuery();

                    requete = string.Format("UPDATE `paie_tbl` SET `montant` = (`montant` - {0} ) WHERE `num_vente` = ({1})", prixTotal, num_vente);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();

                    _transaction.Commit();
                    //MonMessageBox.ShowBox("Données supprimés a été effectuée avec succés.\nLe stock  a été mis  à jour", "Information suppression",
                    //"affirmation.png");
                }
            }
            catch (Exception exception)
            {
                if (_transaction == null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("La suppresion des données a échoué", "Erreur suppression", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
            }
        }
        // supprimer les donnees de la vente produit produit
        public static void SupprimerVentesComplet(int num_vente, string num_medi)
        {
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                string requete = "SELECT * FROM vente WHERE num_vente = " + num_vente;
                command.CommandText = requete;
                command.Transaction = _transaction;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                if (dataTable.Rows.Count == 0)
                {
                    requete = string.Format("DELETE FROM detail_vente WHERE(num_vente = {0} )", num_vente);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();

                    requete = string.Format("DELETE FROM `paie_tbl` WHERE `num_vente` = ({0})", num_vente);
                    command.CommandText = requete;
                    command.Transaction = _transaction;
                    command.ExecuteNonQuery();
                }
                _transaction.Commit();
            }
            catch (Exception exception)
            {
                if (_transaction == null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("Erreur suppression vente", exception);
            }
            finally
            {
                connection.Close();
            }
        }

        // supprimer les donnees de la vente produit produit
        public static void SupprimerVentes(int id)
        {
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                string requete = "SELECT * FROM vente WHERE num_vente = " + id;
                command.CommandText = requete;
                command.Transaction = _transaction;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                var montant = .0;
                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    var numMedicament = dataTable.Rows[i].ItemArray[1].ToString();
                    var quantite = Convert.ToInt32(dataTable.Rows[i].ItemArray[4].ToString());
                    var partIn = 0;
                    if(int.TryParse(dataTable.Rows[i].ItemArray[7].ToString(), out partIn ))
                    {
                    }
                    if (partIn == 0)
                    {
                        requete = string.Format("UPDATE `stock` SET `quantite` = (`quantite` + {0} ) WHERE `num_medi` = ('{1}')",
                                 quantite, numMedicament);
                        command.CommandText = requete;
                        command.Transaction = _transaction;
                        command.ExecuteNonQuery();
                    }
                }
                requete = string.Format("DELETE FROM `paie_tbl` WHERE `num_vente` = ({0})", id);
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                requete = string.Format("DELETE FROM detail_vente WHERE(num_vente = {0} )", id);
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();
                _transaction.Commit();
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("Erreur suppression vente", exception);
            }
            finally
            {
                connection.Close();
            }
        }
        //liste des ventes
        public static DataTable TableRows(string requete)
        {
            DataTable dtVente = new DataTable();
            try
            {
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
            }
            return dtVente;
        }

        public static DataTable ListeDesVentesDetaillees(DateTime date)
        {
            DataTable dtVente = new DataTable();
            try
            {

                string requete = "SELECT vente.num_vente,detail_vente.date_vente, vente.num_medi,medicament.nom_medi, vente.prix_achat, vente.prix_vente, " +
      " vente.quantite, vente.prixTotal,detail_vente.heure FROM ((vente INNER JOIN detail_vente " +
      " ON vente.num_vente = detail_vente.num_vente) INNER JOIN medicament ON vente.num_medi = medicament.num_medi)" +
      " WHERE detail_vente.date_vente>=@date_vente1 AND detail_vente.date_vente <@date_vente2 ORDER BY medicament.nom_medi, detail_vente.date_vente DESC, detail_vente.heure DESC";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date));
                command.Parameters.Add(new MySqlParameter("date_vente2", date.AddHours(24)));
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("vente liste", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        public static DataTable ListeDesVentes(DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {

                var requete = "SELECT detail_vente.num_vente,detail_vente.date_vente,clientTbl.nomClient,detail_vente.prix_total, employe.nom_empl " +
                 ",detail_vente.heure,detail_vente.etat,clientTbl.id,clientTbl.entrep,clientTbl.matricule FROM detail_vente INNER JOIN clientTbl ON clientTbl.id = " +
                 " detail_vente.num_client INNER JOIN employe ON detail_vente.num_empl = employe.num_empl WHERE " +
                 " detail_vente.date_vente >=@date_vente1 AND detail_vente.date_vente <@date_vente2 ORDER BY detail_vente.date_vente DESC,detail_vente.num_vente DESC";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2.AddHours(24)));
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("vente liste", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        public static DataTable ListeDesVentes(DateTime date1, DateTime date2, string nomClient)
        {
            DataTable dtVente = new DataTable();
            try
            {

                var requete = "SELECT detail_vente.num_vente,detail_vente.date_vente,clientTbl.nomClient,detail_vente.prix_total, employe.nom_empl " +
                 ",detail_vente.heure,detail_vente.etat,clientTbl.id FROM detail_vente INNER JOIN clientTbl ON clientTbl.id = " +
                 " detail_vente.num_client INNER JOIN employe ON detail_vente.num_empl = employe.num_empl WHERE clientTbl.nomClient LIKE '%" + nomClient +"%'"+
                 " AND detail_vente.date_vente >=@date_vente1 AND detail_vente.date_vente <@date_vente2 ORDER BY detail_vente.date_vente ,detail_vente.num_vente";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2.AddHours(24)));
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("vente liste", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        public static DataTable ListeDesVentesParClient(int id, DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {

                var requete = "SELECT detail_vente.date_vente,detail_vente.heure, vente.*, medicament.nom_medi, detail_vente.etat" +
                    " FROM detail_vente INNER JOIN vente ON detail_vente.num_vente= vente.num_vente INNER join " +
                    " medicament ON vente.num_medi = medicament.num_medi WHERE detail_vente.num_client=" + id +
                 " AND detail_vente.date_vente >=@date_vente1 AND detail_vente.date_vente <@date_vente2 ORDER BY detail_vente.date_vente";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2.AddHours(24)));
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("vente liste", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        public static DataTable ListeDesVentesParDateVente(int numeroVente)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT  vente.num_medi,medicament.nom_medi, vente.prix_achat, vente.prix_vente, " +
                           " vente.quantite, vente.prixTotal FROM vente INNER JOIN medicament ON vente.num_medi = medicament.num_medi" +
                           " WHERE vente.num_vente =  " + numeroVente +" ";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
            }
            return dtVente;
        }
        public static DataTable ListeDesVentes(int numeroVente)
        {
            DataTable dtVente = new DataTable();
            try
            {
                connection.Open();
                var requete = "SELECT * FROM detail_vente WHERE num_vente = " + numeroVente;
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
            }
            return dtVente;
        }

        //Rechercher vente par date
        public static DataTable ListeDesVentesParDateVente(DateTime date)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT vente.num_vente,detail_vente.date_vente, vente.num_medi,medicament.nom_medi, vente.prix_achat, vente.prix_vente, " +
                           " vente.quantite, vente.prixTotal,detail_vente.heure FROM ((vente INNER JOIN detail_vente " +
                           " ON vente.num_vente = detail_vente.num_vente) INNER JOIN medicament ON vente.num_medi = medicament.num_medi)" +
                           " WHERE (detail_vente.date_vente >= @date_vente ) ORDER BY detail_vente.date_vente DESC , vente.num_vente DESC";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente", date));
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        //Rechercher vente par date et medicament
        public static DataTable ListeDesVentesParNom(DateTime dtp1, DateTime dtp2, string nomMedicament)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT vente.num_vente, detail_vente.date_vente,vente.num_medi, medicament.nom_medi, vente.prix_achat, vente.prix_vente, vente.quantite, " +
                    " vente.prixTotal, detail_vente.heure FROM ((vente INNER JOIN medicament ON vente.num_medi = medicament.num_medi) INNER JOIN detail_vente " +
                     " ON vente.num_vente =detail_vente.num_vente ) WHERE medicament.nom_medi =@nom_medi AND detail_vente.date_vente>= " +
                " @date_vente1 AND  detail_vente.date_vente< @date_vente2 ORDER BY vente.num_vente";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", dtp1.Date));
                command.Parameters.Add(new MySqlParameter("date_vente2", dtp2.Date.AddHours(24)));
                command.Parameters.Add(new MySqlParameter("nom_medi", nomMedicament));
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        public static DataTable ListeDesVentesParNumeroVente(int numVente)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT vente.num_vente,detail_vente.date_vente, vente.num_medi,medicament.nom_medi, vente.prix_achat, vente.prix_vente, " +
                           " vente.quantite, vente.prixTotal,detail_vente.heure,detail_vente.etatPart FROM ((vente INNER JOIN detail_vente " +
                           " ON vente.num_vente = detail_vente.num_vente) INNER JOIN medicament ON vente.num_medi = medicament.num_medi)" +
                           " WHERE (detail_vente.num_vente = " + numVente + " ) ORDER BY detail_vente.date_vente DESC , vente.num_vente DESC";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }
        //Rechercher vente par date
        public static DataTable ListeDesVentes(string nomEmploye, DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT medicament.nom_medi, vente.prix_vente, SUM(vente.quantite) , SUM(vente.quantite * vente.prixTotal )," +
                    "detail_vente.date_vente FROM vente INNER JOIN detail_vente ON vente.num_vente = detail_vente.num_vente INNER JOIN medicament " +
                    " ON vente.num_medi = medicament.num_medi INNER JOIN employe ON employe.num_empl = detail_vente.num_empl WHERE employe.nom_empl LIKE '%" + nomEmploye +
                    "%' AND (detail_vente.date_vente >= @date_vente1 AND detail_vente.date_vente < @date_vente2) GROUP BY medicament.nom_medi, vente.prix_vente ";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2));
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }
        public static DataTable ListeDesVentesParNumeroProduit(string numeroProduit, DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT medicament.nom_medi, vente.prix_vente, SUM(vente.quantite) , SUM(vente.quantite * vente.prixTotal )," +
                    "detail_vente.date_vente FROM vente INNER JOIN detail_vente ON vente.num_vente = detail_vente.num_vente INNER JOIN medicament " +
                    " ON vente.num_medi = medicament.num_medi  WHERE  vente.num_medi  =@num " +
                    "%' AND (detail_vente.date_vente >= @date_vente1 AND detail_vente.date_vente < @date_vente2) GROUP BY medicament.nom_medi, vente.prix_vente ";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2));
                command.Parameters.Add(new MySqlParameter("num", numeroProduit));
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        public static int ObtenirNumeroVente()
        {
            int numeroVente = new int();
            try
            {
                string requete = "select MAX(num_vente)  from detail_vente";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    numeroVente = reader.GetInt32(0);
                }
            }
            finally
            {
                connection.Close();
            }
            return numeroVente;
        }

        public static void SolderCredit(int idClient, decimal montantPaye, string numEmploye, int idVente)
        {
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                var requete = string.Format("INSERT INTO `paie_tbl` (  `date_paye`, `montant`,`num_empl`,id_client, etat,num_vente) " +
                                     " VALUES (@date_paye, @montant,@num_employe,@id_client, @etatp, @idVente)");
                command.Parameters.Add(new MySqlParameter("date_paye", DateTime.Now));
                command.Parameters.Add(new MySqlParameter("montant", montantPaye));
                command.Parameters.Add(new MySqlParameter("id_client", idClient));
                command.Parameters.Add(new MySqlParameter("num_employe", numEmploye));
                command.Parameters.Add(new MySqlParameter("etatp", true));
                command.Parameters.Add(new MySqlParameter("idVente", idVente));
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                requete = string.Format("UPDATE `detail_vente` SET `etat` = true WHERE `num_vente` = ({0})",  idVente   );
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                _transaction.Commit();
                MonMessageBox.ShowBox("Credit  soldé au montant de " + montantPaye + "F.CFA", "Information Insertion",
              "affirmation.png");

            }
            catch (Exception exception)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("L'Operation a échoué .", "Erreur insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static DataTable ListeDesPaiesDesCreditss(int id, DateTime dt1, DateTime dt2)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT montant FROM paie_tbl WHERE (id_client =" + id +
                    ") AND date_paye >=@dt1 AND date_paye<@dt2";
                connection.Open();
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("@dt1", dt1));
                command.Parameters.Add(new MySqlParameter("@dt2", dt2.AddHours(24)));
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        public static DataTable ListeDesPaiesDesCredits(string nomCLient)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT clienttbl.nomClient, paie_tbl.montant FROM clienttbl INNER" +
                    " JOIN paie_tbl ON clienttbl.id = paie_tbl.id_client WHERE (clienttbl.nomClient = @Param1)";
                connection.Open();
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("@Param1", nomCLient));
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        public static DataTable ListeDesPaies(string client)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT paie_tbl.id,paie_tbl.date_paye, paie_tbl.montant,clienttbl.nomClient,employe.nom_empl FROM clienttbl INNER" +
                    " JOIN paie_tbl ON clienttbl.id = paie_tbl.id_client INNER JOIN employe ON employe.num_empl =paie_tbl.num_empl " +
                    " WHERE clienttbl.nomClient LIKE '" + client + "%' ORDER BY paie_tbl.date_paye DESC";
                connection.Open();
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                connection.Close();
            }
            return dtVente;
        }


        public static DataTable ListeDesPaiements(DateTime dt1, DateTime dt2, string nomCaissier)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT paie_tbl.id,paie_tbl.date_paye, paie_tbl.montant,clienttbl.nomClient,employe.nom_empl FROM clienttbl INNER" +
                    " JOIN paie_tbl ON clienttbl.id = paie_tbl.id_client INNER JOIN employe ON employe.num_empl =paie_tbl.num_empl " +
                    " WHERE paie_tbl.date_paye >=@dt1 AND paie_tbl.date_paye<@dt2  AND employe.nom_empl LIKE '%" + nomCaissier + "%' ORDER BY paie_tbl.date_paye";
                connection.Open();
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("@dt1", dt1));
                command.Parameters.Add(new MySqlParameter("@dt2", dt2.AddHours(24)));
                
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return dtVente;
        }


        public static DataTable ListeDistinctVenteDesVentes(DateTime dt1, DateTime dt2)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT DISTINCT employe.nom_empl FROM  paie_tbl  INNER JOIN employe ON employe.num_empl =paie_tbl.num_empl " +
                    " WHERE paie_tbl.date_paye >=@dt1 AND paie_tbl.date_paye<@dt2  ORDER BY paie_tbl.date_paye";
                connection.Open();
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("@dt1", dt1));
                command.Parameters.Add(new MySqlParameter("@dt2", dt2.AddHours(24)));

                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return dtVente;
        }

       // annuler un paiement
        public static bool AnnulerUnPaiement(int numPaiement)
        {
            try
            {
                connection.Open();
                var requete = string.Format("DELETE FROM paie_tbl WHERE(id = {0} )", numPaiement);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données supprimées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Erreur suppression credit", exception);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        // annuler un paiement
        public static bool ModifierUnPaiement(int numPaiement, double montant)
        {
            try
            {
                connection.Open();
                var requete = string.Format("UPDATE paie_tbl SET montant = {0} WHERE(id = {1} )", montant, numPaiement);
                command.CommandText = requete;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Erreur suppression paiement", exception);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region CLIENT_CONVENTION
        // modifier donnees Employee
        public static void ModifierClient(int id, string nomClient, string telephone)
        {
            try
            {
                connection.Open();
                string requete =
                    string.Format(
                        "UPDATE clientTbl SET nomClient = @nom, tel= @telephone WHERE (id = '{0}')", id);
                command.Parameters.Add(new MySqlParameter("nom", nomClient));
                command.Parameters.Add(new MySqlParameter("telephone", telephone));
                command.CommandText = requete;
                int count = command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des données a échoué", "Erreur modification employe", exception,
                    "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        //inventaire de stocks
        public static ArrayList ListeClient(string nomClien)
        {
            ArrayList listeClient = new ArrayList();
            try
            {
                string requete = "SELECT * FROM clientTbl  WHERE nomClient LIKE '" + nomClien + "%' ORDER BY nomClient";
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var client = new Client();
                    client.Id = reader.GetInt32(0);
                    client.NomClient = reader.GetString(1);
                   client.Telephone = reader.GetString(2);

                    listeClient.Add(client);
                }
            }
            finally
            {
                connection.Close();
            }
            return listeClient;
        }

    
        
        #endregion
       
       #region Encaissement
        // enregistrer une Encaissement
        public static bool EnregistrerEncaissement(decimal montantTotal, string numEmpl, decimal b10000, decimal b5000,
            decimal b2000, decimal b1000, decimal b500, decimal b100, decimal b50, decimal b25, decimal b10, decimal b5, decimal b2, decimal b1)
        {
            bool flag = false;
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                //inserer les donnees de encaissement

                string requete = string.Format("INSERT INTO `encai_tbl` (  `date`, `montant`, `num_empl`) " +
                                        " VALUES (@date, @montant,@num_empl)");
                command.Parameters.Add(new MySqlParameter("date", DateTime.Now));
                command.Parameters.Add(new MySqlParameter("montant", montantTotal));
                command.Parameters.Add(new MySqlParameter("num_empl", numEmpl));
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                //selectionner le dernier numero de encaissement
                requete = "select MAX(encai_id)  from encai_tbl";
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();
                var encai_id = (int)command.ExecuteScalar();

                requete = string.Format("INSERT INTO `encai_det_tbl` (`encai_id`, `b10000`, `b5000`,`b2000`, `b1000`, `b500`, `b100`, `b50`,`b25`,`b10`,`b5`,`b2`,`b1`)" +
                                          " VALUES ({0},{1}, {2}, {3}, {4},{5},{6},{7}, {8}, {9}, {10},{11},{12})",
                      encai_id, b10000, b5000, b2000, b1000, b500, b100, b50, b25, b10, b5, b2, b1);

                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                _transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("L'encaissement est faite avec succés", "Information", "information.png");
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    flag = false;
                }
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static DataTable ListeEmployeALaCaisse(DateTime date1, DateTime date2)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                string requete = "SELECT employe.nom_empl, SUM(detail_vente.prix_total) , detail_vente.`date_vente` FROM detail_vente INNER JOIN" +
                    " employe ON detail_vente.num_empl = employe.num_empl WHERE (detail_vente.`date_vente` >=@date1 AND detail_vente.`date_vente` <= @date2) GROUP BY employe.nom_empl ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste vente a ala caisse", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }
        //liste des Encaissement par date
        public static DataTable ListeDesEncaissements(string nomEmploye, DateTime date1, DateTime date2)
        {
            DataTable dtOperation = new DataTable();
            try
            {
                connection.Open();
                string query = "SELECT SUM(encai_tbl.montant), employe.nom_empl, encai_tbl.`date` FROM encai_tbl INNER JOIN  employe ON " +
                    " encai_tbl.num_empl = employe.num_empl GROUP BY employe.nom_empl, encai_tbl.`date` " +
                    " WHERE encai_tbl.`date` >= @Param1 AND encai_tbl.`date` < @Param2 AND employe.nom_empl LIKE '%" + nomEmploye + "%'";

                command.CommandText = query;
                command.Parameters.Add(new MySqlParameter("Param1", date1));
                command.Parameters.Add(new MySqlParameter("Param2", date2.AddHours(24)));
                MySqlDataReader reader = command.ExecuteReader();
                dtOperation.Load(reader);
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Liste encaissement", exception); }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtOperation;
        }
        // supprimer les donnees de de encaissement
        public static void SupprimerEncaisement(int id)
        {
            try
            {
                connection.Open();
                var requete = string.Format("DELETE FROM encaiss_tbl WHERE(encai_id = {0} )", id);
                command.CommandText = requete;
                command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Erreur suppression vente", exception);
            }
            finally
            {
                connection.Close();
            }
        }
        
        #endregion
       
        #region AUTRES

        public static DataSet Inventaire()
        {
            var ds = new DataSet();
            try
            {
                connection.Open();
                var requete = "SELECT medicament.nom_medi,medicament.prix_achat,medicament.prix_vente, " +
                             " stock.quantite,stock.grd_stock , (stock.grd_stock+stock.quantite) * medicament.prix_achat FROM medicament INNER JOIN " +
                                 " stock ON medicament.num_medi = stock.num_medi ORDER BY  medicament.nom_medi";
                var da = new MySqlDataAdapter(requete, connection);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Inventaire", ex);
                return null;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static DataSet InventaireGlobal()
        {
            var ds = new DataSet();
            try
            {
                connection.Open();
                var requete = "SELECT familletbl.designation , medicament.num_medi, medicament.nom_medi,medicament.prix_achat, " +
                         " medicament.prix_vente, stock.quantite, medicament.qteAlerte, medicament.date_expiration FROM medicament INNER JOIN " +
                         " stock ON medicament.num_medi = stock.num_medi INNER JOIN familletbl ON familletbl.numeroFamille= medicament.numeroFamille ORDER BY  medicament.nom_medi";
                var da = new MySqlDataAdapter(requete, connection);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Inventaire", ex);
                return null;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static DataSet ExporterFamille()
        {
            var ds = new DataSet();
            try
            {
                connection.Open();
                string requete = "SELECT * FROM familleTbl ORDER BY designation";
                var da = new MySqlDataAdapter(requete, connection);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Inventaire", ex);
                return null;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static List<Medicament> RapportStatistiqueParMontant(DateTime dateDebut, DateTime dateFin)
        {
            List<Medicament> dt = new List<Medicament>();
            try
            {
                //connection.Open();
                //string query = "SELECT medicament.nom_medi, vente.prix_vente, SUM(vente.quantite), SUM(vente.prixTotal), " +
                //    " detail_vente.date_vente FROM vente INNER JOIN medicament ON vente.num_medi = medicament.num_medi INNER " +
                //    " JOIN detail_vente ON vente.num_vente = detail_vente.num_vente GROUP BY medicament.nom_medi HAVING " +
                //    " (detail_vente.date_vente >= @Param1) AND (detail_vente.date_vente <= @Param2) ORDER BY SUM(vente.prixTotal) DESC";
                string query = "SELECT medicament.nom_medi, medicament.prix_vente, SUM(vente.quantite) , SUM( vente.prixTotal )," +
                 "detail_vente.date_vente FROM vente INNER JOIN detail_vente ON vente.num_vente = detail_vente.num_vente INNER JOIN medicament " +
                 " ON vente.num_medi = medicament.num_medi  WHERE (detail_vente.date_vente >= @Param1 AND detail_vente.date_vente < @Param2) GROUP BY medicament.nom_medi  ORDER BY  medicament.nom_medi ";
                connection.Open();
                command.CommandText = query;
                command.Parameters.Add(new MySqlParameter("Param1", dateDebut));
                command.Parameters.Add(new MySqlParameter("Param2", dateFin.AddHours(24)));

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var m = new Medicament();
                    m.NomMedicament = reader.GetString(0);
                    m.PrixVente = reader.GetDecimal(1);
                    m.Quantite = reader.GetInt32(2);
                    m.PrixTotal = reader.GetDecimal(3);
                    dt.Add(m);

                }
            }
            catch (Exception ex)
            { MonMessageBox.ShowBox("", ex); }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }
        
        #endregion
        
        /******************************* CODE ENTREPRISE *****************************/
        #region Entreprises
        public static bool AjouterUneEntreprise(Entreprise entreprise)
        {

            var flag = false;
            try
            {
                connection.Open();
                var requete = "SELECT * FROM entre_tbl WHERE entreprise = '" + entreprise + "'";
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = "INSERT INTO entre_tbl (entreprise,tele1,tele2,email,adresse) " +
                        "VALUES(@entreprise,@tele1,@tele2,@email,@adresse)";
                    command.Parameters.Add(new MySqlParameter("entreprise", entreprise.NomEntreprise));
                    command.Parameters.Add(new MySqlParameter("tele1", entreprise.Telephone1));
                    command.Parameters.Add(new MySqlParameter("tele2", entreprise.Telephone2));
                    command.Parameters.Add(new MySqlParameter("email", entreprise.Email));
                    command.Parameters.Add(new MySqlParameter("adresse", entreprise.Adresse));
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

        public static bool ModifierUneEntreprise(Entreprise entreprise,string entrp)
        {
            var flag = false;
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                var requete = "UPDATE entre_tbl SET entreprise = @entreprise,tele1 = @tele1,tele2=@tele2," +
                    " email=@email,adresse=@adresse  WHERE id = " + entreprise.NumeroEntreprise;
                command.Parameters.Add(new MySqlParameter("entreprise", entreprise.NomEntreprise));
                command.Parameters.Add(new MySqlParameter("tele1", entreprise.Telephone1));
                command.Parameters.Add(new MySqlParameter("tele2", entreprise.Telephone2));
                command.Parameters.Add(new MySqlParameter("email", entreprise.Email));
                command.Parameters.Add(new MySqlParameter("adresse", entreprise.Adresse));
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                requete = "UPDATE clienttbl SET  entrep=@entrep  WHERE entrep =@entr " ;
                command.Parameters.Add(new MySqlParameter("entr", entrp));
                command.Parameters.Add(new MySqlParameter("entrep", entreprise.NomEntreprise));
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();
                _transaction.Commit();
                flag = true;
                MonMessageBox.ShowBox("Nouvelle entreprise  est enregistrée avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if(_transaction!=null)
                {_transaction.Rollback();
                }
                MonMessageBox.ShowBox("L' enregistrement de l'entreprise a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static void SupprimerUneEntreprise(int numeroEntreprise, string entrep)
        {
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                var requete = "DELETE FROM entre_tbl WHERE id = " + numeroEntreprise;
  
                command.CommandText = requete;
                command.Transaction = _transaction;
                command.ExecuteNonQuery();

                requete = "DELETE FROM clienttbl WHERE entrep = @entrep" ;
               
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("entrep", entrep));
                command.Transaction = _transaction;
                command.ExecuteNonQuery();
                _transaction.Commit();
                MonMessageBox.ShowBox("Données de l'entreprise supprimées avec succés", "Affirmation", "affirmation.png");
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("La suppression des données de l'entreprise a échoué", "Erreur", ex, "erreur.png");
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }


        public static List<Entreprise> ListeDesEntreprises()
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                var requete = "SELECT * FROM entre_tbl ORDER BY entreprise ";
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
                   
                    var entreprise = new Entreprise(id, entrepr, tele1, tele2, email, adresse);
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
               var  requete = "SELECT * FROM entre_tbl WHERE id = " + id;
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
                    var entreprise = new Entreprise(id, entrepr, tele1, tele2, email, adresse);
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
               var  requete = "SELECT * FROM entre_tbl WHERE entreprise LIKE '%" + entrepri + "%'";
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
                    var entreprise = new Entreprise(id, entreprises, tele1, tele2, email, adresse);
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

        public static bool AjouterClient(Client client)
        {
            try
            {
                connection.Open();
                string requete = "INSERT INTO `clientTbl` ( `nomClient`, `tel`,`entrep`,matricule,sc) VALUES (@p1,@p2,@p3,@p4,@p5)";
                command.CommandText = requete;

                command.Parameters.Add(new MySqlParameter("p1", client.NomClient));
                command.Parameters.Add(new MySqlParameter("p2", client.Telephone));
                command.Parameters.Add(new MySqlParameter("p3", client.Entreprise));
                command.Parameters.Add(new MySqlParameter("p4", client.Matricule));
                command.Parameters.Add(new MySqlParameter("p5", client.SousCouvert));
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception exception) 
            { 
                MonMessageBox.ShowBox("Ajouter client", exception); 
                return false; 
            }
            finally
            {
                connection.Close();
               command.Parameters.Clear();
            }
        }

        public static bool ModifierClient(Client client)
        {
            try
            {
                connection.Open();
                string requete = "UPDATE `clientTbl` SET `nomClient` = @p1, `tel`=@p2,`entrep`=@p3, "+
                    "matricule=@p4, sc=@p5 WHERE id =" + client.Id;
                command.CommandText = requete;

                command.Parameters.Add(new MySqlParameter("p1", client.NomClient));
                command.Parameters.Add(new MySqlParameter("p2", client.Telephone));
                command.Parameters.Add(new MySqlParameter("p3", client.Entreprise));
                command.Parameters.Add(new MySqlParameter("p4", client.Matricule));
                command.Parameters.Add(new MySqlParameter("p5", client.SousCouvert));
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("modifier client", exception);
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static bool SupprimerClient(int id)
        {
            try
            {
                connection.Open();
                string requete = "DELETE FROM `clientTbl` WHERE id = " +id ;
                command.CommandText = requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("supprimer client", exception);
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static List<Client> ListeDesClient()
        {
            var listeClient = new List<Client>();
            try
            {
                var requete = "SELECT * FROM clienttbl ORDER BY nomClient ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var client = new Client();
                    client.Id= reader.GetInt32(0);
                    client.NomClient = reader.GetString(1);
                    client.Telephone = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    client.Entreprise = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    client.Matricule = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    client.SousCouvert = !reader.IsDBNull(5) ? reader.GetString(5) : ""; 
            
                    listeClient.Add(client);
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
            return listeClient;
        }
      
        #endregion
        /***************************** FIN CODE ENTREPRISE ***********************/

        /******************************* CODE PAIEMENT D UNE NTREPRISE *****************************/
        #region Paiement
        public static bool EnregistrerPaiement(Entreprise entreprise)
        {

            var flag = false;
            try
            {
                connection.Open();

               var  requete = "INSERT INTO paie_entr_tbl (id_entrep,date_pai,montant,mode_pai,numCheque) VALUES" +
                    "(" + entreprise.NumeroEntreprise + ",@date_pai," + entreprise.Montant + ", '" + entreprise.ModePaiement + "', @numCheque)";
                command.Parameters.Add(new MySqlParameter("date_pai", entreprise.DatePaiement));
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

                var requete = "UPDATE paie_entr_tbl SET id_entrep = " + entreprise.NumeroEntreprise + ",date_pai = @date_pai" +
                ",montant=" + entreprise.Montant + ",mode_pai = '" + entreprise.ModePaiement + "', numCheque=@numCheque WHERE id = " + entreprise.IdPaiement;
                command.Parameters.Add(new MySqlParameter("date_pai", entreprise.DatePaiement));
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
                var requete = "DELETE FROM paie_entr_tbl WHERE id = " + idPaie;
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de paiement numero " + idPaie + " supprimées avec succés", "Affirmation", "affirmation.png");
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
                var requete = "SELECT paie_entr_tbl.id , paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai," +
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
                    var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
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

        public static List<Entreprise> ListeDesPaiements(int id)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                var requete = "SELECT paie_entr_tbl.id , paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai" +
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
                    var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
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

        public static List<Entreprise> ListeDesPaiements(DateTime date)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
               var requete = "SELECT paie_entr_tbl.id , paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai" +
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
                    var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
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

        public static List<Entreprise> ListeDesPaiements(string entrep, DateTime date1, DateTime date2)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                connection.Open();
                var requete = "SELECT paie_entr_tbl.id, paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai, " +
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
                    var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
                    //var entreprise = new Entreprise(id, idEntrep, datePai, montant,entrep,mode);
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

        public static List<Entreprise> ListeDesPaiements(string entrep)
        {
            var listeEntreprise = new List<Entreprise>();
            try
            {
                var requete = "SELECT paie_entr_tbl.id , paie_entr_tbl.id_entrep, paie_entr_tbl.date_pai," +
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
                    var entreprise = new Entreprise(id, idEntrep, datePai, montant, entrep, mode, cheque);
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
        /***************************** FIN CODE FACTURE ENTREPRISE ***********************/

        /******************************* CODE ENTREPRISE *****************************/

        /****************************REGION INVENTAIRE****************************/
        #region INVENTAIRES
        //creer un produit
        public static int DerniereInventaire()
        {
            try
            {

                connection.Open();

                var requete = "SELECT MAX(id) FROM invent_tbl";

                command.CommandText = requete;
                return (int)command.ExecuteScalar();

            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }


        public static int EnregistrerInventaire( List<Medicament> listeProduit, string typeInventaire)
        {
            var numProduit="";
            try
            {
                connection.Open();
                _transaction = connection.BeginTransaction();
                var requete = "INSERT INTO invent_tbl (`date`,type_inven,etat) VALUES(@date,@type_inven,@etat)";
                command.Parameters.Add(new MySqlParameter("date", DateTime.Now));
                command.Parameters.Add(new MySqlParameter("type_inven", typeInventaire));
                command.Parameters.Add(new MySqlParameter("etat", false));
                command.Transaction = _transaction;
                command.CommandText = requete;
                command.ExecuteNonQuery();

                requete = "SELECT MAX(id) FROM invent_tbl";
                command.CommandText = requete;
                command.Transaction = _transaction;
                var id = (int)command.ExecuteScalar();

                int j = 0;

                foreach (var produit in listeProduit )
                {
                    int quantite;
                    if (typeInventaire == "Pharmacie")
                    {
                        quantite = produit.Quantite;
                    }
                    else
                    {
                        quantite = produit.GrandStock;
                    }
                    requete = "INSERT INTO det_invent (id, id_prod,qte, pc, pp,empla,ordre,qte_av)" +
                            "VALUES(@idd, @id_prod,@qte,@pc,@pp, @empl,@ordre,@qte_av )";
                    var qte = 0;
                    numProduit = produit.NumeroMedicament;
                    command.Parameters.Add(new MySqlParameter("idd", id));
                    command.Parameters.Add(new MySqlParameter("id_prod", produit.NumeroMedicament));
                    command.Parameters.Add(new MySqlParameter("qte", qte));
                    command.Parameters.Add(new MySqlParameter("pc", produit.PrixAchat));
                    command.Parameters.Add(new MySqlParameter("pp", produit.PrixVente));
                    command.Parameters.Add(new MySqlParameter("empl", " "));
                    command.Parameters.Add(new MySqlParameter("ordre", 0));
                    command.Parameters.Add(new MySqlParameter("qte_av", quantite));
                    command.Transaction = _transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    if (produit.NombreBoite > 0 && produit.PrixVenteDetail > 0)
                    {
                        requete = "INSERT INTO det_invent (id, id_prod,qte, pc, pp,empla,ordre,qte_av)" +
                               "VALUES(@idd1, @id_prod1,@qte1,@pc1,@pp1, @empl1,@ordre1,@qte_av1,)";
                        j++;
                        numProduit = produit.NumeroMedicament;
                        command.Parameters.Add(new MySqlParameter("idd1", id));
                        command.Parameters.Add(new MySqlParameter("id_prod1", produit.NumeroMedicament));
                        command.Parameters.Add(new MySqlParameter("qte1", qte));
                        command.Parameters.Add(new MySqlParameter("pc1", produit.PrixAchat/produit.NombreBoite));
                        command.Parameters.Add(new MySqlParameter("pp1", produit.PrixVenteDetail));
                        command.Parameters.Add(new MySqlParameter("empl1", " "));
                        command.Parameters.Add(new MySqlParameter("ordre1", 0));
                        command.Parameters.Add(new MySqlParameter("qte_av1", produit.NombreDetail));
                        command.Transaction = _transaction;
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                    }
                    j++;

                    command.Parameters.Clear();
                }

                //requete = string.Format("UPDATE stock SET quantite=0");
                //command.Transaction = _transaction;
                //command.CommandText = requete;
                //command.ExecuteNonQuery();
                
                _transaction.Commit();
                MonMessageBox.ShowBox("Nouvelle inventaire creée avec succés", "Affirmation", "affirmation.png");

                return id;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué " + numProduit, "Erreur ", ex, "erreur.png");
                return 0;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static bool EnregistrerInventaire(Inventaire inventaire) //DataGridView dgvProduit, int rowIndex)
        {
            try
            {

                connection.Open();
                _transaction = connection.BeginTransaction();
                var j = 0;
                
                var requete = "SELECT * FROM det_invent WHERE id = " + inventaire.IDInventaire + " AND id_prod = @prod  ";
                    command.Transaction = _transaction;
                    command.Parameters.Add(new MySqlParameter("prod", inventaire.IDProduit));
                    command.CommandText = requete;
                    var reader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    if (dt.Rows.Count <= 0)
                    {
                        requete = "INSERT INTO det_invent (id, id_prod,qte, pc, pp,empla,ordre,qte_av )" +
                            "VALUES(@idd, @id_prod,@qte,@pc,@pp, @empl,@ordre, " + inventaire.QuantiteAvant  + ")";
                    }
                    else
                    {
                        requete = "UPDATE det_invent SET qte=@qte,pc=@pc, pp=@pp, empla=@empl,ordre=@ordre  WHERE  id=@idd AND id_prod=@id_prod";
                    }
                    command.Parameters.Add(new MySqlParameter("idd", inventaire.IDInventaire));
                    command.Parameters.Add(new MySqlParameter("id_prod", inventaire.IDProduit));
                    command.Parameters.Add(new MySqlParameter("qte", inventaire.Quantite));
                    command.Parameters.Add(new MySqlParameter("pc", inventaire.PrixCession));
                    command.Parameters.Add(new MySqlParameter("pp", inventaire.PrixPublic));
                    command.Parameters.Add(new MySqlParameter("empl", inventaire.Emplacement));
                    command.Parameters.Add(new MySqlParameter("ordre", inventaire.Ordre));
                    command.Transaction = _transaction;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    j++;
                    //if (rowIndex == 0)
                    //{
                                                   
                    //}
                    //else
                    //{

                    //}
                _transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static bool ValiderInventaire(DataGridView dgvInventaire, string typeInventaire, int idInventaire )
        {
            try
            {

                connection.Open();
                _transaction = connection.BeginTransaction();

                    var _requete = "SELECT *FROM invent_tbl  WHERE etat=@etat AND id=" + idInventaire;
                command.Parameters.Add(new MySqlParameter("etat", true ));
                command.CommandText = _requete;
                command.Transaction = _transaction;
               var reader = command.ExecuteReader();
               if (!reader.HasRows)
               {
                   reader.Close();
                   _requete = "UPDATE invent_tbl  SET empl=@empl, etat=@etatt WHERE id=" + idInventaire;
                   command.Parameters.Add(new MySqlParameter("empl", Form1.nomEmploye));
                   command.Parameters.Add(new MySqlParameter("etatt", true));
                   command.CommandText = _requete;
                   command.Transaction = _transaction;
                   command.ExecuteNonQuery();

                   foreach (DataGridViewRow dgv in dgvInventaire.Rows)
                   {
                       var produit = new Inventaire();
                       produit.IDProduit = dgv.Cells[0].Value.ToString();
                       if (!string.IsNullOrEmpty(produit.IDProduit))
                       {
                           produit.Quantite = Convert.ToInt32(dgv.Cells[4].Value.ToString());
                           produit.PrixCession = Convert.ToDouble(dgv.Cells[2].Value.ToString());
                           produit.PrixPublic = Convert.ToDouble(dgv.Cells[3].Value.ToString());
                           produit.Emplacement = dgv.Cells[8].Value.ToString();
                           produit.QuantiteAvant = Convert.ToInt32(dgv.Cells[5].Value.ToString());
                           var dateExpiration = Convert.ToDateTime(dgv.Cells[7].Value.ToString());

                            _requete = "UPDATE medicament SET  prix_achat = @prix_achat, date_expiration=@date_expiration,  " +
                                            " prix_vente = @prix_vente,rayon = @place,qteAlerte=@qteAlerte WHERE (num_medi ='" + produit.IDProduit + "')";

                           command.Parameters.Add(new MySqlParameter("prix_achat", (decimal)produit.PrixCession));
                           command.Parameters.Add(new MySqlParameter("prix_vente", (decimal)produit.PrixPublic));
                           command.Parameters.Add(new MySqlParameter("place", produit.Emplacement));
                           command.Parameters.Add(new MySqlParameter("qteAlerte", produit.QuantiteAvant));
                           command.Parameters.Add(new MySqlParameter("date_expiration", dateExpiration));
                           command.CommandText = _requete;
                           command.Transaction = _transaction;
                           command.ExecuteNonQuery();

                           if (typeInventaire == "Pharmacie")
                           {
                               _requete = "UPDATE stock SET quantite=@qte WHERE num_medi=@id";
                           }
                           else if (typeInventaire == "Grand depôt")
                           {
                               _requete = "UPDATE stock SET grd_stock=@qte WHERE num_medi=@id";
                           }
                           command.Parameters.Add(new MySqlParameter("qte", (int)produit.Quantite));
                           command.Parameters.Add(new MySqlParameter("id", produit.IDProduit));
                           command.Transaction = _transaction;
                           command.CommandText = _requete;
                           command.ExecuteNonQuery();
                           command.Parameters.Clear();
                       }
                   }
               }
               else
               {
                   MonMessageBox.ShowBox("Cet inventaire a été deja validé", "Erreur", "erreur.png");
                   return false;
               }
                _transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static bool RemettreLeStock(DataGridView dgvInventaire)
        {
            try
            {

                connection.Open();

                foreach (DataGridViewRow dgv in dgvInventaire.Rows)
                {
                    var produit = new Inventaire();
                    produit.IDProduit = dgv.Cells[0].Value.ToString();
                    produit.Quantite = Convert.ToInt32(dgv.Cells[9].Value.ToString());

                    var requete = string.Format("UPDATE stock_tbl SET stock={0} WHERE id_produit ={1}", produit.Quantite, produit.IDProduit);
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                }
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        //creer un produit
        public static List<Inventaire> ListeInventaire()
        {
            try
            {
                var listeProduit = new List<Inventaire>();
                connection.Open();
               var  _requete = "SELECT * FROM invent_tbl ";
                command.CommandText = _requete;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Inventaire();
                    p.IDInventaire = reader.GetInt32(0);
                    p.DateInventaire = reader.GetDateTime(1);
                    p.Emplacement = !reader.IsDBNull(2) ? reader.GetString(2):"";
                    p.Etat = !reader.IsDBNull(3) ? reader.GetBoolean(3) : false ;
                    p.Employe = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    listeProduit.Add(p);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static List<Inventaire> ListeDetailsInventaire()
        {
            try
            {
                var listeProduit = new List<Inventaire>();
                connection.Open();
                var _requete = "SELECT * FROM det_invent ";
                command.CommandText = _requete;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Inventaire();
                    p.IDInventaire = reader.GetInt32(0);
                    p.IDProduit = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    p.Quantite = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0;
                    p.PrixCession = reader.GetDouble(3);
                    p.PrixPublic = reader.GetDouble(4);
                    p.Emplacement = !reader.IsDBNull(5) ? reader.GetString(5) : " ";
                    p.Ordre = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
                    p.QuantiteAvant = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0;
                    //p.ID = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    listeProduit.Add(p);
                }
                return listeProduit;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur", ex);
                return null;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
       //creer un produit
        public static bool SupprimerInventaire(int idInventaire)
        {
            try
            {
                connection.Open();

                var _requete = string.Format("DELETE FROM invent_tbl  WHERE id ={0}", idInventaire);
                command.CommandText = _requete;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données supprimées avec succés ", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        #endregion

        #region RAPPORT
        //rapport livraison par meadicament
        public static DataTable JournalDesLivraisons(DateTime dateDebut, DateTime dateFin, string nomFournisseur)
        {
            try
            {
                string requete = "SELECT detail_bl.* from detail_bl INNER JOIN fournisseur ON detail_bl.num_fourn=fournisseur.num_fourn" +
                    " WHERE detail_bl.date_bl >=@date1 AND detail_bl.date_bl < @date2 AND fournisseur.nom_fourn =@nom AND detail_bl.etat=3 ORDER BY detail_bl.date_bl, detail_bl.num_bl";
                command.Parameters.Add(new MySqlParameter("date1", dateDebut));
                command.Parameters.Add(new MySqlParameter("date2", dateFin.AddHours(24)));
                command.Parameters.Add(new MySqlParameter("nom", nomFournisseur));
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
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

        //livraison medicament entre deux dates
        public static DataTable RapportDesLivraisonsParMedicament(string fournisseur, DateTime debutDate, DateTime dateFin)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string requete =
                    "SELECT detail_bl.num_bl, medicament.nom_medi, bn_liv.quantite, bn_liv.prix_achat" +
                    ", prix_total FROM ((bn_liv INNER JOIN medicament ON bn_liv.num_medi = medicament.num_medi) INNER JOIN " +
                    " detail_bl ON bn_liv.num_bl = detail_bl.num_bl) INNER JOIN fournisseur ON detail_bl.num_fourn" +
                    "  = fournisseur.num_fourn WHERE (detail_bl.date_bl >= @date_bl1) AND (detail_bl.date_bl <=  @date_bl2) " +
                    "AND (fournisseur.nom_fourn= @fournisseur) ORDER BY  detail_bl.date_bl,detail_bl.num_bl,  medicament.nom_medi";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date_bl1", debutDate));
                command.Parameters.Add(new MySqlParameter("date_bl2", dateFin));
                command.Parameters.Add(new MySqlParameter("fournisseur", fournisseur));
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dataTable;
        }

        public static DataTable MontantTotalDeVente(string colone, string param, DateTime dt1, DateTime dt2)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string requete = "SELECT COUNT(vente.num_medi), medicament.nom_medi, SUM(vente.quantite) " +
                                 ", SUM(vente.prix_vente * vente.quantite) FROM vente INNER JOIN medicament ON" +
                                 " vente.num_medi = medicament.num_medi INNER JOIN detail_vente ON vente.num_vente=detail_vente.num_vente" +
                                 "  WHERE (" + colone + " = @param  AND detail_vente.date_vente >=@dt1 AND detail_vente.date_vente<@dt2)" +
                                 "  GROUP BY medicament.nom_medi, vente.num_medi ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("param", param));
                command.Parameters.Add(new MySqlParameter("dt1", dt1));
                command.Parameters.Add(new MySqlParameter("dt2", dt2.AddDays(1)));
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return dataTable;
        }

        public static DataTable MontantTotalDeLivraison(string colone, string param, DateTime dt1, DateTime dt2)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string requete = "SELECT bn_liv.num_medi, medicament.nom_medi, SUM(bn_liv.quantite) " +
                    ", SUM(bn_liv.prix_achat * bn_liv.quantite) FROM (bn_liv INNER JOIN medicament ON" +
                    " bn_liv.num_medi = medicament.num_medi) INNER JOIN detail_bl ON bn_liv.num_bl=detail_bl.num_bl" +
                   "  WHERE (" + colone + " = @param AND detail_bl.date_bl >=@dt1 AND detail_bl.date_bl<@dt2) " +
                   "GROUP BY medicament.nom_medi, bn_liv.num_medi ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("param", param));
                command.Parameters.Add(new MySqlParameter("dt1", dt1));
                command.Parameters.Add(new MySqlParameter("dt2", dt2.AddDays(1)));
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
            }
            finally
            {
                connection.Close(); command.Parameters.Clear();
            }
            return dataTable;
        }
        
        //rapport livraison par meadicament
        public static DataTable RapportDeLivraison(string numLivraison)
        {
            DataTable dT = new DataTable();
            try
            {
                string requete = "SELECT  medicament.nom_medi,cmd_tbl.quantite,cmd_tbl.prix_achat," +
                                 " cmd_tbl.prix_total FROM cmd_tbl INNER JOIN medicament ON cmd_tbl.num_medi" +
                                 "  = medicament.num_medi WHERE cmd_tbl.num_bl = @num ORDER BY  medicament.nom_medi";
                command.Parameters.Add(new MySqlParameter("num", numLivraison));
                command.CommandText = requete;
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                dT.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);

            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
            return dT;
        }
        
        #endregion

        #region DEPOT
        public static List<Depot> ListeDesDepots()
        {
            try
            {
                var listeDepot = new List<Depot>();
                var requete = "SELECT * FROM depot_tbl ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var depot = new Depot();
                    depot.ID = reader.GetInt32(0);
                    depot.NomDepot = reader.GetString(1);
                    depot.Adresse = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    depot.Telephone1 = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    depot.Telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    depot.Email = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    depot.Ville = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    depot.Pays = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    depot.Commentaire = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    depot.Reference = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    depot.Priorite = !reader.GetBoolean(9);
                    listeDepot.Add(depot);
                }
                return listeDepot;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        //creer un produit
        public static bool ModifierUnDepot(Depot depot)
        {
            try
            {
                connection.Open();
                var requete = "UPDATE  depot_tbl SET depot=@depot, adresse=@adresse,tel1=@tele1," +
                    "tel2=@tele2,email=@email,ville=@ville,pays=@pays, commentaire=@commentaire" +
                ",ref=@ref WHERE id = " + depot.ID;

                command.Parameters.Add(new MySqlParameter("depot", depot.NomDepot));
                command.Parameters.Add(new MySqlParameter("adresse", depot.Adresse));
                command.Parameters.Add(new MySqlParameter("tele1", depot.Telephone1));
                command.Parameters.Add(new MySqlParameter("tele2", depot.Telephone2));
                command.Parameters.Add(new MySqlParameter("email", depot.Email));
                command.Parameters.Add(new MySqlParameter("ville", depot.Ville));
                command.Parameters.Add(new MySqlParameter("pays", depot.Pays));
                command.Parameters.Add(new MySqlParameter("commentaire", depot.Commentaire));
                command.Parameters.Add(new MySqlParameter("ref", depot.Reference));
                command.CommandText = requete;
                command.ExecuteNonQuery();

                MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }
        
        #endregion
       
        #region Depenses
        //enregistrer une depenses
        public static bool EnregistrerUnLibelle(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.Rows.Count; i++)
                {
                    var depense = new Depenses();
                    var id = Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString());
                    depense.Categorie = dgv.Rows[i].Cells[1].Value.ToString();
                    depense.Libelle = dgv.Rows[i].Cells[2].Value.ToString();

                    if (id > 0)
                    {
                        var requete = "UPDATE catedep SET libelle =@libl  WHERE id = " + id;
                        command.Parameters.Add(new MySqlParameter("libl", depense.Libelle));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    else
                    {
                        string requete = "INSERT INTO catedep (cat , libelle) VALUES(@cat, @libl)";
                        command.CommandText = requete;
                        command.Parameters.Add(new MySqlParameter("cat", depense.Categorie));
                        command.Parameters.Add(new MySqlParameter("libl", depense.Libelle));
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement libelle", ex);
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }
        //enregistrer une depenses
        public static bool SuprimerUnLibelle(DataGridView dgv)
        {
            try
            {
                connection.Open();
                var j = 0;
                for (var i = 0; i < dgv.SelectedRows.Count; i++)
                {
                    var depense = new Depenses();
                    depense.ID = Convert.ToInt32(dgv.SelectedRows[i].Cells[0].Value.ToString());

                    if (depense.ID > 0)
                    {
                        var requete = "DELETE FROM catedep  WHERE id = " + depense.ID;
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        j++;
                    }
                }
                MonMessageBox.ShowBox(j + " Données ont été supprimées avec succées", "Affirmation","affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement libelle",  ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Depenses> ListeLibelle()
        {
            try
            {
                var liste = new List<Depenses>();
                connection.Open();
                var requete = "SELECT * FROM catedep";
                command.CommandText = requete;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.ID = reader.GetInt32(0);
                    depense.Categorie = reader.GetString(1);
                    depense.Libelle = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception) { MonMessageBox.ShowBox("", exception); return null; }
            finally { connection.Close(); }
        }

        //enregistrer une depenses
        public static bool EnregistrerUneDepense(Depenses depense)
        {
            try
            {
                connection.Open();

                var requete = "INSERT INTO `det_dep`(`id_lib`,  `montant`, `benef`,`date`, no_fact) VALUES ("
                     + "@id_lib, @montant,  @benef,  @date,@no_fact)";
                command.Parameters.Add(new MySqlParameter("date", depense.Date));
                command.Parameters.Add(new MySqlParameter("id_lib", depense.ID));
                command.Parameters.Add(new MySqlParameter("montant", depense.Montant));
                command.Parameters.Add(new MySqlParameter("benef", depense.Beneficiaire));
                command.Parameters.Add(new MySqlParameter("no_fact", depense.NumeroFacture));
                command.CommandText = requete;
                command.ExecuteNonQuery();

                MonMessageBox.ShowBox("L'enregistrement a été éffectuée avec succés", "ajouter depenses","affirmation");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Ajouter depenses", ex);
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        //enregistrer une depenses
        public static bool ModifierUneDepense(Depenses depense)
        {
            try
            {
                connection.Open();

                var requete = "UPDATE `det_dep` SET `id_lib` = @id_lib,  `montant` =@montant, " +
                    " `benef`=@benef,`date`=@date, no_fact=@no_fact WHERE id = " + depense.IDDepense;
                command.Parameters.Add(new MySqlParameter("date", depense.Date));
                command.Parameters.Add(new MySqlParameter("id_lib", depense.ID));
                command.Parameters.Add(new MySqlParameter("montant", depense.Montant));
                command.Parameters.Add(new MySqlParameter("benef", depense.Beneficiaire));
                command.Parameters.Add(new MySqlParameter("no_fact", depense.NumeroFacture));
                command.CommandText = requete;
                command.ExecuteNonQuery();

                MonMessageBox.ShowBox("L'enregistrement a été éffectuée avec succés", "ajouter depenses","affirmation.png");
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


        //enregistrer une hospitalisation
        public static void SupprimerUneDepense(int id)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM det_dep WHERE id =" + id;
                command.CommandText = query;
                command.ExecuteNonQuery();
                MonMessageBox.ShowBox("La suppression des données a été éffectuée avec succés", "Supprimer depense","affirmation.png");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données a echoue", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        //liste des depenses
        public static List<Depenses> ListeDesDepenses()
        {
            var liste = new List<Depenses>();
            try
            {
                connection.Open();
                string query = "SELECT * FROM det_dep";
                command.CommandText = query;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.IDDepense = reader.GetInt32(0);
                    depense.ID = reader.GetInt32(1);
                    depense.Date = reader.GetDateTime(2);
                    depense.Montant = reader.GetDouble(3);
                    depense.Beneficiaire = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    depense.NumeroFacture = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    liste.Add(depense);
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

        public static DataTable DataTableDesDepenses(DateTime date, DateTime date1)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                string query = "SELECT catedep.libelle ,det_dep.montant  FROM pharmdb.catedep INNER JOIN "+
                    "det_dep ON catedep.id=det_dep.id_lib WHERE det_dep.date >= @date AND det_dep.date < @date1";
                command.CommandText = query;
                command.Parameters.Add(new MySqlParameter("date", date));
                command.Parameters.Add(new MySqlParameter("date1", date1.AddHours(24)));
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Liste depenses", exception); }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeDesDepensesGrouperParLibelle(DateTime date, DateTime date1)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                string query = "SELECT catedep.libelle, SUM(det_dep.montant) FROM det_dep INNER JOIN catedep" +
                    " ON det_dep.id_lib=catedep.id WHERE " +
                    " det_dep.date >=@date AND det_dep.date <@date1 GROUP BY catedep.libelle ";
                command.CommandText = query;
                command.Parameters.Add(new MySqlParameter("date", date));
                command.Parameters.Add(new MySqlParameter("date1", date1.AddHours(24)));
                MySqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Liste depenses", exception); }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dt;
        }


        public static DataTable ListeDesDepenses(int id)
        {
            var dt = new DataTable();
            try
            {
                connection.Open();
                string query = "SELECT * FROM det_dep WHERE id =" + id;
                command.CommandText = query;
                var reader = command.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Liste depense", exception); return null; }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }
        //liste des depenses
        public static DataTable ListeDesDepenses(string nomEmploye, DateTime date)
        {
            var dtOperation = new DataTable();
            try
            {
                connection.Open();
                var query = "SELECT SUM(`prix_total`),`date` " +
                    "FROM `depen_tbl` WHERE (`date` >= @Param1 AND `date` < @Param2 )" +
                " AND (`num_empl` = '" + nomEmploye + "') ";
                command.CommandText = query;
                command.Parameters.Add(new MySqlParameter("Param1", date));
                command.Parameters.Add(new MySqlParameter("Param2", date.AddHours(24)));
                var reader = command.ExecuteReader();
                dtOperation.Load(reader);
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Liste depense", exception); }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtOperation;
        }
       
        #endregion

        public static  void InsererDansLog(string utilisateur, string operation, string formes)
        {
            try
            {
                string paths = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                string filePath = paths + @"\log\log_account.txt";
                string filePath1 =   @"\\SVR\log\log_account.txt";
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true);
                    sw.WriteLine("***************** Date et Heure " + DateTime.Now.ToString() + "  **********************");
                    sw.WriteLine(" Utilisateur  : {0}  ", utilisateur);
                    sw.WriteLine(" Operation  : {0}  ", operation);
                    sw.WriteLine("Fenetre vente  :  {0} ", formes);
                    sw.WriteLine();
                    sw.Close();

                }

                if (System.IO.File.Exists(filePath1))
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath1, true);
                    sw.WriteLine("***************** Date et Heure " + DateTime.Now.ToString() + "  **********************");
                    sw.WriteLine(" Utilisateur  : {0}  ", utilisateur);
                    sw.WriteLine(" Operation  : {0}  ", operation);
                    sw.WriteLine("Fenetre vente  :  {0} ", formes);
                    sw.WriteLine();
                    sw.Close();

                }
            }
            catch { }
        }

    }
}