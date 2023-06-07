using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using GestionPharmacetique.AppCode;

namespace GestionDuneClinique.AppCode
{
    public class ConnectionClassPharmacie
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        //private static MySqlTransaction _transaction = null;

        static ConnectionClassPharmacie()
        {
            string connectionString =
            @"server=192.168.1.211;user id=Hbebidja;database=;port=3306;password=Hbebidja2020;database=pharmdb";
            //@"server=localhost;user id=root;password=chris@2022;database=pharmdb"; 
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
       
        public static DataTable TableMedicament()
        {
            var dt = new DataTable();
            try
            {
               string requete = "SELECT nom_medi FROM medicament ";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste medicament", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable TableMedicament(string nom)
        {
            var dt = new DataTable();
            try
            {
              string  requete = "SELECT nom_medi FROM medicament WHERE nom_medi LIKE  '%" + nom + "%'";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste medicament", ex);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable ListeDesCredit(int matricule, string nomClient, string entreprise, DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT vente.num_vente,detail_vente.date_vente ,detail_vente.etat,vente.num_medi, medicament.nom_medi,"+
                "vente.quantite,vente.prix_vente,vente.prixTotal,clienttbl.nomClient FROM vente INNER JOIN medicament ON vente.num_medi=medicament.num_medi " +
                "INNER JOIn detail_vente ON vente.num_vente=detail_vente.num_vente INNER JOIN clienttbl ON clienttbl.id=detail_vente.num_client"+
                " WHERE clienttbl.nomClient = @nomClient AND clienttbl.entrep=@entrep AND detail_vente.etat=@etat AND "+
                " clienttbl.matricule =@matricule AND detail_vente.date_vente >=@date1 AND detail_vente.date_vente <@date2";
                connection.Open();
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("nomClient", nomClient));
                command.Parameters.Add(new MySqlParameter("entrep", entreprise));
                command.Parameters.Add(new MySqlParameter("matricule", matricule));
                command.Parameters.Add(new MySqlParameter("etat", false));
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste credi medicament", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }
        public static DataTable ListeDesVentes(int matricule, string nomClient, string entreprise, DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT vente.num_vente,detail_vente.date_vente ,detail_vente.etat,vente.num_medi, medicament.nom_medi," +
                                 "vente.quantite,vente.prix_vente,vente.prixTotal,clienttbl.nomClient FROM vente INNER JOIN medicament ON vente.num_medi=medicament.num_medi " +
                                 "INNER JOIn detail_vente ON vente.num_vente=detail_vente.num_vente INNER JOIN clienttbl ON clienttbl.id=detail_vente.num_client" +
                                 " WHERE clienttbl.nomClient = @nomClient AND clienttbl.entrep=@entrep AND detail_vente.etat=@etat AND " +
                                 " clienttbl.matricule =@matricule AND detail_vente.date_vente >=@date1 AND detail_vente.date_vente <@date2";
                connection.Open();
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.Parameters.Add(new MySqlParameter("nomClient", nomClient));
                command.Parameters.Add(new MySqlParameter("entrep", entreprise));
                command.Parameters.Add(new MySqlParameter("matricule", matricule));
                command.Parameters.Add(new MySqlParameter("etat", true));
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste credi medicament", ex);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return dtVente;
        }

        
        // ajouter un nouveau Employee
        public static void AjouterEmployee(Employe employee)
        {
            try
            {
                connection.Open();
                string requete = "SELECT * FROM employe WHERE num_empl ='" + employee.NumMatricule + "'";
                command.CommandText = requete;
                MySqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable(); dt.Load(reader); reader.Close();
                int count = dt.Rows.Count;
                if (count < 1)
                {
                    requete = "INSERT INTO `employe` (`num_empl`, `nom_empl`, `Addresse`, `telephone1`, `telephone2`," +
                            "`email`, `titre`,`ville`) VALUES (@num_empl, @nom_empl, @Addresse, @telephone1, @telephone2, @email, @titre,@ville )";

                    command.Parameters.Add(new MySqlParameter("num_empl", employee.NumMatricule));
                    command.Parameters.Add(new MySqlParameter("nom_empl", employee.NomEmployee));
                    command.Parameters.Add(new MySqlParameter("Addresse", employee.Addresse));
                    command.Parameters.Add(new MySqlParameter("telephone1", employee.Telephone1));
                    command.Parameters.Add(new MySqlParameter("telephone2", employee.Telephone2));
                    command.Parameters.Add(new MySqlParameter("email", employee.Email));
                    command.Parameters.Add(new MySqlParameter("titre", employee.Titre));
                    command.Parameters.Add(new MySqlParameter("ville", "Ndjamena"));
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Nouveau employé a été inseré avec succés dans la base de données",
                        "Information Insertion", "affirmation.png");
                }
                else
                {
                    MonMessageBox.ShowBox(
                        "Le numéro de l'employé " + employee.NumMatricule + " existe deja dans la base de données",
                        "erreur", "erreur.png");
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
        public static void ModifierEmployee(Employe employe)
        {
            try
            {
                connection.Open();
                string requete =
                    string.Format(
                        "UPDATE employe SET nom_empl = @nom_empl, Addresse = @Addresse, telephone1 = @telephone1, telephone2 = @telephone2" +
                        ", email = @email, titre = @titre,  ville = @ville WHERE (num_empl = '{0}')", employe.NumMatricule);
                command.Parameters.Add(new MySqlParameter("nom_empl", employe.NomEmployee));
                command.Parameters.Add(new MySqlParameter("Addresse", employe.Addresse));
                command.Parameters.Add(new MySqlParameter("telephone1", employe.Telephone1));
                command.Parameters.Add(new MySqlParameter("telephone2", employe.Telephone2));
                command.Parameters.Add(new MySqlParameter("email", employe.Email));
                command.Parameters.Add(new MySqlParameter("titre", employe.Titre));
                command.Parameters.Add(new MySqlParameter("ville", "Ndjamena"));
                command.CommandText = requete;
                int count = command.ExecuteNonQuery();
                if (count > 0)
                {
                    MonMessageBox.ShowBox("données employé modifiées avec succés.", "Information modification",
                        "affirmation.png");
                }
                else
                {
                    MonMessageBox.ShowBox(
                        "le numéro de l 'employé " + employe.NumMatricule + " n'existe pas dans la base de données.",
                        "erreur modification",
                        "erreur.png");

                }
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

        //Rechercher vente par date
        public static double  ListeDesVentes(DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {
                string requete = "SELECT SUM(montant) FROM paie_tbl WHERE date_paye>=@date1 AND date_paye <@date2";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date1", date1));
                command.Parameters.Add(new MySqlParameter("date2", date2));
                command.CommandText = requete;
                //return (double)command.ExecuteScalar();
                MySqlDataReader reader = command.ExecuteReader();
                dtVente.Load(reader);
                if (dtVente.Rows.Count > 0)
                {
                    return double.Parse(dtVente.Rows[0].ItemArray[0].ToString());
                }
                else
                {
                    return 0.0;
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Rapport vente", exception);
                return 0.0;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        //Rechercher vente par date
        public static int CompterLeNombreDePatient(DateTime date1, DateTime date2,string nomClient )
        {
            try
            {
                string requete = "SELECT COUNT(detail_vente.num_client) FROM detail_vente INNER JOIN clientTbl  " +
                "ON clientTbl.id =detail_vente.num_client  WHERE clienttbl.nomClient = @nomClient AND " +
                "detail_vente.date_vente >=@date_vente1 AND detail_vente.date_vente <@date_vente2 ";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2.AddHours(24)));
                command.Parameters.Add(new MySqlParameter("nomClient", nomClient));
                command.CommandText = requete;
                //return (int)command.ExecuteScalar();
                var reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                return count;
            }
            catch (Exception exception)
            {
                //MonMessageBox.ShowBox("Rapport vente", exception);
                return 0;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        //Rechercher vente par date
        public static DataTable ListeDesVentesPharmacieTousLesPatients(DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {

                string requete = "SELECT medicament.nom_medi, SUM(vente.prixTotal ), SUM(vente.quantite) " +
                  "  FROM vente INNER JOIN detail_vente ON vente.num_vente = detail_vente.num_vente INNER JOIN medicament " +
                  " ON vente.num_medi = medicament.num_medi INNER JOIN clienttbl ON clienttbl.id = detail_vente.num_client " +
                    " WHERE (detail_vente.date_vente >= @date_vente1 AND detail_vente.date_vente < @date_vente2) GROUP BY medicament.nom_medi ";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2.AddHours(24)));
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
        public static DataTable ListeDesVentesPharmacieParConvention(string entreprise, DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {

                string requete = "SELECT medicament.nom_medi,SUM(vente.prixTotal ), SUM(vente.quantite) " +
                  " FROM vente INNER JOIN detail_vente ON vente.num_vente = detail_vente.num_vente INNER JOIN medicament " +
                  " ON vente.num_medi = medicament.num_medi INNER JOIN clienttbl ON clienttbl.id = detail_vente.num_client WHERE clienttbl.entrep =@entrep AND detail_vente.etat=0" +
                    " AND (detail_vente.date_vente >= @date_vente1 AND detail_vente.date_vente < @date_vente2) GROUP BY medicament.nom_medi";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2.AddHours(24)));
                command.Parameters.Add(new MySqlParameter("entrep", entreprise));
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
         public static DataTable ListeDesVentesDeTousLesConventionnes(DateTime date1, DateTime date2)
        {
            DataTable dtVente = new DataTable();
            try
            {

                string requete = "SELECT medicament.nom_medi, SUM(vente.prixTotal),SUM(vente.quantite) FROM medicament INNER JOIN vente ON medicament.num_medi=" +
                           " vente.num_medi  INNER JOIN detail_vente ON vente.num_vente=detail_vente.num_vente WHERE  " +
                           "   detail_vente.date_vente >=@date_vente1 AND detail_vente.date_vente<@date_vente2  AND detail_vente.etat=0 GROUP BY medicament.nom_medi";
                connection.Open();
                command.Parameters.Add(new MySqlParameter("date_vente1", date1));
                command.Parameters.Add(new MySqlParameter("date_vente2", date2.AddHours(24)));
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
         public static DataTable ListeDesVentesDataTableParticulier(DateTime date1, DateTime date2)
         {
             DataTable dtVente = new DataTable();
             try
             {
                 string requete = "SELECT medicament.nom_medi, SUM(vente.prixTotal),SUM(vente.quantite) FROM medicament INNER JOIN vente ON medicament.num_medi=" +
                            " vente.num_medi  INNER JOIN detail_vente ON vente.num_vente=detail_vente.num_vente WHERE " +
                            "  detail_vente.date_vente >=@date1 AND detail_vente.date_vente<@date2  AND detail_vente.etat=1 GROUP BY medicament.nom_medi";
                 connection.Open();
                 command.Parameters.Add(new MySqlParameter("date1", date1));
                 command.Parameters.Add(new MySqlParameter("date2", date2.AddHours(24)));
                 command.CommandText = requete;
                 //return (double)command.ExecuteScalar();
                 MySqlDataReader reader = command.ExecuteReader();
                 dtVente.Load(reader);
                 return dtVente;
             }
             catch (Exception exception)
             {
                 MonMessageBox.ShowBox("Rapport vente", exception);
                 return null;
             }
             finally
             {
                 connection.Close();
                 command.Parameters.Clear();
             }
         }

         public static System.Data.DataTable ListeDesCredit(string nomClient, DateTime date1, DateTime date2)
         {
             var dtVente = new System.Data.DataTable();
             try
             {
                 string requete = "SELECT vente.num_vente,detail_vente.date_vente ,detail_vente.etat,vente.num_medi, medicament.nom_medi," +
                 "vente.quantite,vente.prix_vente,vente.prixTotal,clienttbl.nomClient FROM vente INNER JOIN medicament ON vente.num_medi=medicament.num_medi " +
                 "INNER JOIn detail_vente ON vente.num_vente=detail_vente.num_vente INNER JOIN clienttbl ON clienttbl.id=detail_vente.num_client" +
                 " WHERE clienttbl.nomClient = @nomClient AND clienttbl.entrep LIKE '%PCP MEMBRE FAMILLE PERSONNEL%'  AND detail_vente.etat=@etat "+
                 " AND detail_vente.date_vente >=@date1 AND detail_vente.date_vente <@date2";
                 connection.Open();
                 command.CommandText = requete;
                 command.Parameters.Add(new MySqlParameter("nomClient", nomClient));
                 command.Parameters.Add(new MySqlParameter("date1", date1));
                 command.Parameters.Add(new MySqlParameter("date2", date2));
                 command.Parameters.Add(new MySqlParameter("etat", false));
                 MySqlDataReader reader = command.ExecuteReader();
                 dtVente.Load(reader);
                 return dtVente;
             }
             catch (Exception ex)
             {
                 //MonMessageBox.ShowBox("Liste credi medicament", ex);
                 return null;
             }
             finally
             {
                 connection.Close();
                 command.Parameters.Clear();
             }
         }

    }
}
