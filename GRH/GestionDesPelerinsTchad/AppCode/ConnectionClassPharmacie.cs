using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace SGSP.AppCode
{
   public  class ConnectionClassPharmacie
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static MySqlTransaction _transaction = null;
        static string connectionString;
        static ConnectionClassPharmacie()
        {
            connectionString =
                @"server=192.168.1.3;user id=hnda;password=Hnda2021;database=pharmdb";
            //@"server=localhost;port=3306;user id=root;password=chris@2020;database=pharmdb";
            connection = new MySqlConnection(connectionString);
            command = new MySqlCommand("", connection);
        }
        public static double StockGeneral()
        {
            try
            {
                var dt = new System.Data.DataTable();
                connection.Open();

                var requete = "SELECT  SUM(medicament.prix_achat * (stock.quantite + stock.grd_stock)) FROM medicament INNER JOIN" +
                      " stock ON medicament.num_medi = stock.num_medi";
                command.CommandText = requete;
                return (double)command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public static bool AjouterEmployeDuneEntreprise(System.Windows.Forms.DataGridView dgvEmpl, int IdEntreprise, string nomEntreprise)
        {
            var flag = false;
            try
            {
                connection.Open();
                for (int i = 0; i < dgvEmpl.Rows.Count; i++)
                {
                    var nomEmploye = dgvEmpl.Rows[i].Cells[1].Value.ToString() + " " + dgvEmpl.Rows[i].Cells[2].Value.ToString();
                    var sexe = dgvEmpl.Rows[i].Cells[11].Value.ToString();
                    var fonction = dgvEmpl.Rows[i].Cells[3].Value.ToString();
                    var matricule = dgvEmpl.Rows[i].Cells[0].Value.ToString();
                    //string nom, prenom;
                    //if (nomEmploye.Contains(" "))
                    //{
                    //    nom = nomEmploye.Substring(0, nomEmploye.IndexOf(" "));
                    //    prenom = nomEmploye.Substring(nomEmploye.IndexOf(" ") + 1);
                    //}
                    //else
                    //{
                    //    nom = nomEmploye;
                    //    prenom = "";
                    //}
                    if (!string.IsNullOrEmpty(nomEmploye))
                    {
                        var requete = "SELECT * FROM emp_entr WHERE nom =  @nomEmploy";
                        command.CommandText = requete;
                        command.Parameters.Add(new MySqlParameter("nomEmploy", nomEmploye));
                        var reader = command.ExecuteReader();
                        var dt = new System.Data. DataTable();
                        dt.Load(reader);
                        reader.Close();
                        if (dt.Rows.Count <= 0)
                        {
                            requete = "INSERT INTO `emp_entr` (`nom`, `sexe`, `age`, `tele`, `id_entre`,matricule,fonction)" +
                                       " VALUES (@nomEmpl, @sexeEmpl, @ageEmpl, @teleEmpl, @id_entre ,@matricule,@fonction)";
    
                            command.Parameters.Add(new MySqlParameter("nomEmpl", nomEmploye));
                            command.Parameters.Add(new MySqlParameter("sexeEmpl", sexe));
                            command.Parameters.Add(new MySqlParameter("ageEmpl", "Adulte"));
                            command.Parameters.Add(new MySqlParameter("teleEmpl", ""));
                            command.Parameters.Add(new MySqlParameter("id_entre", 113));
                            command.Parameters.Add(new MySqlParameter("matricule", matricule));
                            command.Parameters.Add(new MySqlParameter("fonction", fonction ));
                            command.CommandText = requete;
                            command.ExecuteNonQuery();

                            //double poids= 0.0, tension=0.0, temperature= 0.0, fraisCarnet=0.0;
                            //requete = "INSERT INTO patient_tbl (`nom`,`prenom`,`sexe`,`age`,`telephone`,`poids`,`tension`,`temp`,`entrep`,`rhesus`,`frais_carn`,`date_enre`,`sc`)" +
                            //          " VALUES(@nom,@prenom,@sexe,@age,@telephone,@poids,@tension,@temp,@entrep,@rhesus,@frais_carn,@date_enre,@sc)";
                            //command.Parameters.Add(new MySqlParameter("nom", nom));
                            //command.Parameters.Add(new MySqlParameter("prenom", prenom));
                            //command.Parameters.Add(new MySqlParameter("sexe", "M"));
                            //command.Parameters.Add(new MySqlParameter("age", 30));
                            //command.Parameters.Add(new MySqlParameter("telephone", ""));
                            //command.Parameters.Add(new MySqlParameter("poids", poids));
                            //command.Parameters.Add(new MySqlParameter("tension", tension));
                            //command.Parameters.Add(new MySqlParameter("temp",temperature));
                            //command.Parameters.Add(new MySqlParameter("entrep", nomEntreprise));
                            //command.Parameters.Add(new MySqlParameter("rhesus", "-"));
                            //command.Parameters.Add(new MySqlParameter("frais_carn", fraisCarnet));
                            //command.Parameters.Add(new MySqlParameter("date_enre", DateTime.Now));
                            //command.Parameters.Add(new MySqlParameter("sc", ""));
                            //command.CommandText = requete;
                            //command.ExecuteNonQuery();

                        }
                    }
                    command.Parameters.Clear();
                    flag = true;
                }

             //SGDP.Formes.   MonMessageBox.ShowBox("Les données des employés sont été inserées avec succés dans la base de données",
                  //"affirmation.png");
            }
            catch (Exception exception)
            {
                flag = false;
                //MonMessageBox.ShowBox("L'insertion des données a échoué",exception);
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
            return flag;
        }

        public static System.Data.DataTable ListeDesCredit(string nomClient)
        {
            var dtVente = new System.Data.DataTable();
            try
            {
                string requete = "SELECT vente.num_vente,detail_vente.date_vente ,detail_vente.etat,vente.num_medi, medicament.nom_medi," +
                "vente.quantite,vente.prix_vente,vente.prixTotal,clienttbl.nomClient FROM vente INNER JOIN medicament ON vente.num_medi=medicament.num_medi " +
                "INNER JOIn detail_vente ON vente.num_vente=detail_vente.num_vente INNER JOIN clienttbl ON clienttbl.id=detail_vente.num_client" +
                " WHERE clienttbl.nomClient = @nomClient AND clienttbl.entrep LIKE '%HNDA%'  AND detail_vente.etat=@etat ";
                connection.Open();
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("nomClient", nomClient));
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
