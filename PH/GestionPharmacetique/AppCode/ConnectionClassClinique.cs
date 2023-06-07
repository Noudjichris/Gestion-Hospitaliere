using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
namespace GestionPharmacetique.AppCode
{
    class ConnectionClassClinique
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static string requete;

        static ConnectionClassClinique()
        {
            string connectionString =
                @"server=192.168.1.211;user id=Hbebidja;database=;port=3306;password=Hbebidja2020;database=clinique_db";
                //@"server=192.168.1.21;user id=Hbebidja;database=;port=3306;password=Hbebidja2020;database=clinique_db";
                //@"server=localhost;port=3306;user id=root;password=chris@2022;database=clinique_db";
            connection = new MySqlConnection(connectionString);
            command = new MySqlCommand("", connection);
        }

        // liste des patients
        public static DataTable ListeDesPatients()
        {
            var dt = new DataTable();//68188844
            try
            {
                requete = "SELECT * FROM patient_tbl ORDER BY nom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
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
            return dt;
        }
        // liste des patients
        public static DataTable ListeDesPatients(int id)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE id = " + id +" ORDER BY nom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
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
            return dt;
        }

        // liste des patients
        public static DataTable ListeDesPatients(string nomPatient, string entrep)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE entrep LIKE '%" + entrep + "%' AND  nom LIKE '" + nomPatient + "%' ORDER BY nom, prenom";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
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
            return dt;
        }

        // liste des patients
        public static void Utilisateur()
        {
            var dt = new DataTable();
            try
            {
                requete = "UPDATE utilisateur_tbl SET nom_utilisateur = @nom , type_utilisateur=@uti";
                command.CommandText = requete;
                connection.Open();
                command.ExecuteNonQuery();
              
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
            
        }

        public static DataTable ListeDesEntreprises()
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT * FROM entre_tbl ORDER BY entreprise ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable ListeDesEntreprises(string nomEntreprise)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT * FROM entre_tbl WHERE entreprise LIKE '%" + nomEntreprise +"%' ORDER BY entreprise ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste entreprise", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
      
        static  MySqlTransaction transaction = null ;
        public static bool AjouterEmployeDuneEntreprise(System.Windows.Forms.DataGridView dgv)
        {
            var flag = false;
            try
            {
                connection.Open();
               transaction= connection.BeginTransaction() ;
                for (var i = 0; i <= dgv.Rows.Count; i++)
                {
                    var nom = dgv.Rows[i].Cells[1].Value.ToString();
                    requete = "INSERT INTO `emp_entr` (`nom`, `sexe`, `age`, `tele`, `id_entre`)" +
                               " VALUES (@nomEmpl, @sexeEmpl, @ageEmpl, @teleEmpl, @id_entre )";

                    command.Parameters.Add(new MySqlParameter("nomEmpl", nom));
                    command.Parameters.Add(new MySqlParameter("sexeEmpl", "M"));
                    command.Parameters.Add(new MySqlParameter("ageEmpl", 30));
                    command.Parameters.Add(new MySqlParameter("teleEmpl", "-"));
                    command.Parameters.Add(new MySqlParameter("id_entre", 1));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();

                    requete = "INSERT INTO patient_tbl (`nom`,`prenom`,`sexe`,`age`,`telephone`,`poids`,`tension`,`temp`,`entrep`,`rhesus`,`frais_carn`,`date_enre`,`sc`)" +
                            " VALUES(@nom,@prenom,@sexe,@age,@telephone,@poids,@tension,@temp,@entrep,@rhesus,@frais_carn,@date_enre,@sc)";
                    command.Parameters.Add(new MySqlParameter("nom", nom));
                    command.Parameters.Add(new MySqlParameter("prenom"," "));
                    command.Parameters.Add(new MySqlParameter("sexe", "M"));
                    command.Parameters.Add(new MySqlParameter("age", 30));
                    command.Parameters.Add(new MySqlParameter("telephone","-"));
                    command.Parameters.Add(new MySqlParameter("poids", 0));
                    command.Parameters.Add(new MySqlParameter("tension",0));
                    command.Parameters.Add(new MySqlParameter("temp",0));
                    command.Parameters.Add(new MySqlParameter("entrep", "IRC"));
                    command.Parameters.Add(new MySqlParameter("rhesus", ""));
                    command.Parameters.Add(new MySqlParameter("frais_carn", 0));
                    command.Parameters.Add(new MySqlParameter("date_enre", DateTime.Now));
                    command.Parameters.Add(new MySqlParameter("sc", ""));
                    command.CommandText = requete;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();


                    command.Parameters.Clear();
                }
                transaction.Commit();
                flag = true;
            }
            catch (Exception exception)
            {
                if (transaction != null)
                    transaction.Rollback();
                MonMessageBox.ShowBox("L'insertion des données a échoué", "Erreur d'insertion", exception, "erreur.png");
                flag = false;
            }
            finally
            {
                connection.Close();
            }
            return flag;
        }


    }
}
