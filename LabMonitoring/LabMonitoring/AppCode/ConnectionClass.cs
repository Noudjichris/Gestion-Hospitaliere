using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using GestionPharmacetique;
using System.Data;

namespace LabMonitoring.AppCode
{
    public class ConnectionClass
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static MySqlTransaction transaction = null;
        static  string requete;
        static ConnectionClass()
        {
            var connectionString =
              @"server=172.24.114.7;user id=sos;password=sos2019;database=lab_db";
            //@"server=localhost;port=3306;user id=root;password=chris@2022;database=lab_db";
            connection = new MySqlConnection(connectionString);
            command = new MySqlCommand("", connection);
        }

        public static  bool EnregistrerGroupeAnalyse(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.Rows.Count; i++ )
                {
                    var id = Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString());
                    var grp = dgv.Rows[i].Cells[1].Value.ToString();
                     
                    if (id  > 0)
                    {
                        requete = "UPDATE grp_exam SET grp = @grp WHERE id = " + id;
                        command.Parameters.Add(new MySqlParameter("grp", grp));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        requete = "INSERT INTO grp_exam(grp) VALUES(@grp2)";
                        command.Parameters.Add(new MySqlParameter("grp2", grp));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                    }
                    command.Parameters.Clear();
                }
                return true;
                
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool SupprimerUnGroupeAnalyse(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.SelectedRows.Count; i++)
                {
                    var id = Convert.ToInt32(dgv.SelectedRows[i].Cells[0].Value.ToString());
                    requete = "DELETE FROM grp_exam WHERE id =" + id;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                }
                MonMessageBox.ShowBox("Données suppriées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Examen> ListeGroupeExamen()
        {
            try
            {
                requete = "SELECT * FROM grp_exam ORDER BY grp";
                var listeExamen = new List<Examen>();
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var examen = new Examen();
                    examen.NumeroGroupe = reader.GetInt32(0);
                    examen.GroupeAnalyse = reader.GetString(1);
                    listeExamen.Add(examen);
                }
                return listeExamen;
            }
            catch (Exception )
            {
                return null ;
            }
            finally
            {
                connection.Close();
            }
        }
    
        public static  bool EnregistrerAnalyse(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.Rows.Count; i++ )
                {
                    var id = Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString());
                    var id_grp = Convert.ToInt32(dgv.Rows[i].Cells[1].Value.ToString());
                    var examen = dgv.Rows[i].Cells[3].Value.ToString();
                     
                    if (id  > 0)
                    {
                        requete = "UPDATE exam_tbl SET id_grp ="+ id_grp+", exam = @exam11 WHERE id = " + id;
                        command.Parameters.Add(new MySqlParameter("exam11", examen));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    else
                    {
                        requete = "INSERT INTO exam_tbl(id_grp, exam) VALUES("+id_grp +", @examen2)";
                        command.Parameters.Add(new MySqlParameter("examen2", examen));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }

                }
                return true;
                
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static bool SupprimerUnAnalyse(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.SelectedRows.Count; i++)
                {
                    var id = Convert.ToInt32(dgv.SelectedRows[i].Cells[0].Value.ToString());
                    requete = "DELETE FROM exam_tbl WHERE id =" + id;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                }
                MonMessageBox.ShowBox("Données suppriées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Examen> ListeExamen()
        {
            try
            {
                requete = "SELECT * FROM exam_tbl ORDER BY exam";
                var listeExamen = new List<Examen>();
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var examen = new Examen();
                    examen.NumeroAnalyse = reader.GetInt32(0);
                    examen.NumeroGroupe = reader.GetInt32(1);
                    examen.Analyse = reader.GetString(2);
                    listeExamen.Add(examen);
                }
                return listeExamen;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste exam", ex);
                return null ;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool EnregistrerLesResultats(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString());
                    var id_exam = Convert.ToInt32(dgv.Rows[i].Cells[1].Value.ToString());
                    var resultat = dgv.Rows[i].Cells[2].Value.ToString();

                    if (id > 0)
                    {
                        requete = "UPDATE result_tbl SET result =@result12  WHERE id = " + id;
                        command.Parameters.Add(new MySqlParameter("result12", resultat));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    else
                    {
                        requete = "INSERT INTO result_tbl(id_app,  result) VALUES(" + id_exam + ", @resultttt)";
                        command.Parameters.Add(new MySqlParameter("resultttt", resultat));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }

                }
                return true;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool SupprimerLesResultats(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.SelectedRows.Count; i++)
                {
                    var id = Convert.ToInt32(dgv.SelectedRows[i].Cells[0].Value.ToString());
                    requete = "DELETE FROM result_tbl WHERE id =" + id;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                }
                MonMessageBox.ShowBox("Données suppriées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Examen> ListeDesResultats()
        {
            try
            {
                requete = "SELECT * FROM result_tbl";
                var listeExamen = new List<Examen>();
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var examen = new Examen();
                    examen.IDResultat = reader.GetInt32(0);
                    examen.IDAppreciation = reader.GetInt32(1);
                    examen.Resultat = reader.GetString(2);
                    listeExamen.Add(examen);
                }
                return listeExamen;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool EnregistrerLesAppreciations(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.Rows.Count; i++)
                {
                    var id = Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString());
                    var id_exam = Convert.ToInt32(dgv.Rows[i].Cells[1].Value.ToString());
                    var app = dgv.Rows[i].Cells[2].Value.ToString();
                    if (id > 0)
                    {
                        requete = "UPDATE app_tbl SET app =@app11  WHERE id = " + id;
                        command.Parameters.Add(new MySqlParameter("app11", app));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    else
                    {
                        requete = "INSERT INTO app_tbl(exam,  app) VALUES(" + id_exam + ", @app22)";
                        command.Parameters.Add(new MySqlParameter("app22", app));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                }
                command.Parameters.Clear();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static bool SupprimerLesAppreciations(DataGridView dgv)
        {
            try
            {
                connection.Open();
                for (var i = 0; i < dgv.SelectedRows.Count; i++)
                {
                    var id = Convert.ToInt32(dgv.SelectedRows[i].Cells[0].Value.ToString());
                    requete = "DELETE FROM app_tbl WHERE id =" + id;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                }
                MonMessageBox.ShowBox("Données suppriées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Examen> ListeDesAppreciations()
        {
            try
            {
                requete = "SELECT * FROM app_tbl";
                var listeExamen = new List<Examen>();
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var examen = new Examen();
                    examen.IDAppreciation = reader.GetInt32(0);
                    examen.NumeroAnalyse = reader.GetInt32(1);
                    examen.Appreciation = reader.GetString(2);
                    listeExamen.Add(examen);
                }
                return listeExamen;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool EnregistrerUnPatient(Patient patient, string state)
        {
            try
            {
                connection.Open();
               
                    if (state=="2")
                    {
                        //if
                        requete = "UPDATE patient_tbl SET nom_pat =@nom_pattt, sexe = @sexxe, age = @agge WHERE num_pat = " + patient.IDPatient;
                        command.Parameters.Add(new MySqlParameter("nom_pattt", patient.NomPatient));
                        command.Parameters.Add(new MySqlParameter("sexxe", patient.Sexe));
                         command.Parameters.Add(new MySqlParameter("agge", patient.Age));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    else if(state=="1")
                    {
                        requete = "INSERT INTO patient_tbl VALUES(" + patient.IDPatient + ", @patient, @sexe,@age)";
                        command.Parameters.Add(new MySqlParameter("patient", patient.NomPatient));
                        command.Parameters.Add(new MySqlParameter("sexe", patient.Sexe));
                        command.Parameters.Add(new MySqlParameter("age", patient.Age));
                        command.CommandText = requete;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }

                    MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
                return true;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool SupprimerUnPatient(int IDPatient)
        {
            try
            {
                connection.Open();

                requete = "DELETE FROM patient_tbl WHERE num_pat =" + IDPatient;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                
                MonMessageBox.ShowBox("Données suppriées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Patient> ListeDesPatients()
        {
            try
            {
                requete = "SELECT * FROM patient_tbl";
                var listePatient = new List<Patient>();
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var patient = new Patient();
                    patient.IDPatient = reader.GetInt32(0);
                    patient.NomPatient = reader.GetString(1);
                    patient.Sexe = reader.GetString(2);
                    patient.Age = reader.GetString(3);
                    listePatient.Add(patient);
                }
                return listePatient;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool EnregistrerLesResultatsDuPatient(DataGridView dgv,Patient patient,bool flag)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                requete = "SELECT * FROM patient_tbl WHERE num_pat = "+ patient.IDPatient ;
                command.CommandText = requete;
                command.Transaction = transaction;
               var reader= command.ExecuteReader();
               if (!reader.HasRows)
               {

                   reader.Close();
                   requete = "INSERT INTO patient_tbl VALUES(" + patient.IDPatient + ", @patient, @sexe,@age)";
                   command.Parameters.Add(new MySqlParameter("patient", patient.NomPatient.ToUpper()));
                   command.Parameters.Add(new MySqlParameter("sexe", patient.Sexe));
                   command.Parameters.Add(new MySqlParameter("age", patient.Age));
                   command.CommandText = requete;
                   command.Transaction = transaction;
                   command.ExecuteNonQuery();

               }
               reader.Close();
                requete = "INSERT INTO res_tbl(num_pat,  date_exam) VALUES(" + patient.IDPatient + ", @date)";
                command.Parameters.Add(new MySqlParameter("date", DateTime.Now));
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();

                requete = "SELECT MAX(id) FROM res_tbl";
                command.CommandText = requete;
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                var id = (int)command.ExecuteScalar();

                for (var i = 0; i < dgv.Rows.Count; i++)
                {
                    var id_exam = Convert.ToInt32(dgv.Rows[i].Cells[0].Value.ToString());
                    string  app ;
                    //if (flag)
                    //{
                        app = dgv.Rows[i].Cells[2].Value.ToString();
                    //}
                    //else
                    //{
                    //    app = "";
                    //}
                    var resultat = dgv.Rows[i].Cells[3].Value.ToString();
                   
                        requete = "INSERT INTO det_res(id,  id_exam,app,resul,ordre) VALUES(" + id + ", "+id_exam+", @appp, @res, " + i+")";
                        command.Parameters.Add(new MySqlParameter("appp", app));
                        command.Parameters.Add(new MySqlParameter("res", resultat));
                        command.CommandText = requete;
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();                                        
                }
                transaction.Commit();
                MonMessageBox.ShowBox("Données enregistrées avec succés", "Affirmation", "affirmation.png");
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
                command.Parameters.Clear();
            }
        }

        public static List<Resultat> ListeResultatPatient()
        {
            try
            {
                var liste = new List<Resultat>();
                requete = "SELECT * FROM res_tbl";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var resultat = new Resultat();
                    resultat.NumeroResultat = reader.GetInt32(0);
                    resultat.IDPatient = reader.GetInt32(1);
                    resultat.DateResultat = reader.GetDateTime(2);
                    liste.Add(resultat);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                connection.Close();
            }
        }
        public static List<Resultat> ListeResultatDetaillePatient()
        {
            try
            {
                var liste = new List<Resultat>();
                requete = "SELECT * FROM det_res ORDER BY ordre";
                command.CommandText = requete;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var resultat = new Resultat();
                    resultat.NumeroResultat = reader.GetInt32(0);
                    resultat.IDExam = reader.GetInt32(1);
                    resultat.Appreciation =!reader.IsDBNull(2) ? reader.GetString(2) : " ";
                    resultat.ResultatExamen =!reader.IsDBNull(3) ? reader.GetString(3) : " ";
                    liste.Add(resultat);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                connection.Close();
            }
        }

        public static bool SupprimerUnResultatPatient (int ID)
        {
            try
            {
                connection.Open();
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation", "confirmation.png") == "1")
                {
                    requete = "DELETE FROM res_tbl WHERE id =" + ID;
                    command.CommandText = requete;
                    command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Données supprimées avec succés", "Affirmation", "affirmation.png");
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex, "erreur.png");
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
        public static DataTable BilanDesExamens(DateTime dt1, DateTime dt2)
        {
            var dt = new DataTable();
            try
            {
                requete = "SELECT  DISTINCT lab_db.exam_tbl.exam , COUNT(*)  FROM lab_db.exam_tbl " +
                    " INNER JOIN lab_db. det_res ON lab_db.exam_tbl.id = lab_db.det_res.id_exam " +
                    " INNER JOIN lab_db.res_tbl ON lab_db.res_tbl.id= lab_db.det_res.id where " +
                    " lab_db.res_tbl.date_exam >=@date1 AND lab_db.res_tbl.date_exam < @date2 " +
                    " group by lab_db.exam_tbl.exam ORDER BY lab_db.exam_tbl.exam ";
                command.CommandText = requete;
                command.Parameters.Add(new MySqlParameter("date1", dt1));
                command.Parameters.Add(new MySqlParameter("date2", dt2.AddHours(24)));
                connection.Open();
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

    }
}
