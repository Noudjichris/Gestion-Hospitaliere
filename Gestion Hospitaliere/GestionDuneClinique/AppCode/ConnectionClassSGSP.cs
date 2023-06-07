using System;
using MySql.Data.MySqlClient;

namespace GestionDuneClinique.AppCode
{
    class ConnectionClassSGSP
    {
           private static MySqlConnection _connection;
        private static MySqlCommand _command;
        private static MySqlTransaction _transaction = null;

        static ConnectionClassSGSP()
        {
            var connectionString =
               @"server=192.168.1.3;user id=hnda;password=Hnda2021;database=personnel_db";
            //@"server=localhost;port=3306;user id=root;password=chris@2020;database=personnel_db";
            _connection = new MySqlConnection(connectionString);
            _command = new MySqlCommand("", _connection);
        }
     
        public static System.Data. DataTable ListeDesPersonnelParNomPersonnel(string nom)
        {
            var dataTable = new System.Data.DataTable();
            try
            {
                var requete = "SELECT pers_tbl.num_mat, pers_tbl.nom, pers_tbl.prenom, pers_tbl.date_nai, pers_tbl.lieu_nai, " +
                    "pers_tbl.addres, pers_tbl.tele1, pers_tbl.tele2, pers_tbl.email, pers_tbl.sexe, pers_tbl.photo, dep_tbl.dep," +
                    "service_tbl.poste, service_tbl.date_pris,service_tbl.echelon,service_tbl.categorie,pers_tbl.no_compte,service_tbl.anciennete " +
                    ",service_tbl.etat_cntr,service_tbl.etat,service_tbl.date_retr,service_tbl.date_fnctr,pers_tbl.code_banc," +
                    "pers_tbl.code_gui,pers_tbl.cle,service_tbl.contrat FROM pers_tbl INNER JOIN dep_tbl ON pers_tbl.id_dep = dep_tbl.id_dep INNER JOIN service_tbl " +
                    "ON pers_tbl.num_mat = service_tbl.num_mat WHERE pers_tbl.nom LIKE '%" + nom + "%' ORDER BY pers_tbl.nom, pers_tbl.prenom";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                dataTable.Load(reader);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return dataTable;
        }
        static int idLibelle;
        static int idCategorie;
        public static bool EnregistrerUneRecette(System.Windows.Forms.DataGridView dgvRecette)
        {
            try
            {
                _connection.Open();
                var requete = "";
                //_transaction = _connection.BeginTransaction();
                //requete = "SELECT * FROM v_recette_tbl  WHERE date_recette =@date_recette";
                //_command.Parameters.Add(new MySqlParameter("date_recette", DateTime.Now.Date));
                //        _command.CommandText = requete;
                //        _command.Transaction = _transaction;
                //        var reader2 = _command.ExecuteReader();
                //        var dt2 = new DataTable();
                //        dt2.Load(reader2);
                //        reader2.Close();
                if (dgvRecette.Rows.Count > 0)
                {
                    foreach (System.Windows.Forms.DataGridViewRow dgRow in dgvRecette.Rows)
                    {
                        if (!string.IsNullOrEmpty(dgRow.Cells[2].Value.ToString()) &&
                            string.IsNullOrEmpty(dgRow.Cells[0].Value.ToString()) &&
                            string.IsNullOrEmpty(dgRow.Cells[3].Value.ToString()))
                        //string.IsNullOrWhiteSpace(dgRow.Cells[4].Value.ToString()))
                        {
                            requete = "SELECT * FROM catedeprec_tbl  WHERE cat =@categorie AND etat = 1";
                            _command.Parameters.Add(new MySqlParameter("categorie", dgRow.Cells[2].Value.ToString()));
                            _command.CommandText = requete;
                            _command.Transaction = _transaction;
                            var reader = _command.ExecuteReader();
                            var dt = new System.Data. DataTable();
                            dt.Load(reader);
                            reader.Close();
                            if (dt.Rows.Count > 0)
                            {
                                idCategorie = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
                            }
                            else
                            {
                                requete = "INSERT INTO catedeprec_tbl (cat , etat,code) VALUES(@cat, 1, '')";
                                _command.CommandText = requete;
                                _command.Transaction = _transaction;
                                _command.Parameters.Add(new MySqlParameter("cat", dgRow.Cells[2].Value.ToString()));
                                _command.ExecuteNonQuery();

                                requete = "SELECT MAX(id) FROM catedeprec_tbl  WHERE etat =1";
                                _command.CommandText = requete;
                                _command.Transaction = _transaction;
                                idCategorie = (int)_command.ExecuteScalar();
                            }
                        }
                        else if (!string.IsNullOrWhiteSpace(dgRow.Cells[2].Value.ToString()) &&
                                !string.IsNullOrWhiteSpace(dgRow.Cells[3].Value.ToString()) &&
                              !string.IsNullOrWhiteSpace(dgRow.Cells[4].Value.ToString()) &&
                              !(dgRow.Cells[2].Value.ToString().ToUpper().Contains("TOTAL")))
                        {
                            requete = "SELECT * FROM libelle_tbl  WHERE libelle =@libellle AND etat = 1";
                            _command.Parameters.Add(new MySqlParameter("libellle", dgRow.Cells[2].Value.ToString()));
                            _command.CommandText = requete;
                            _command.Transaction = _transaction;
                            var reader1 = _command.ExecuteReader();
                            var dt1 = new System.Data. DataTable();
                            dt1.Load(reader1);
                            reader1.Close();
                            if (dt1.Rows.Count > 0)
                            {
                                idLibelle = Convert.ToInt32(dt1.Rows[0].ItemArray[0].ToString());
                            }
                            else
                            {
                                requete = "INSERT INTO libelle_tbl (id_cat , libelle,code,etat) VALUES(@id_cat, @libelle,'',1)";
                                _command.CommandText = requete;
                                _command.Parameters.Add(new MySqlParameter("id_cat", idCategorie));
                                _command.Parameters.Add(new MySqlParameter("libelle", dgRow.Cells[2].Value.ToString()));
                                _command.Transaction = _transaction;
                                _command.ExecuteNonQuery();
                                _command.Parameters.Clear();

                                requete = "SELECT MAX(id) FROM libelle_tbl  WHERE etat =1";
                                _command.CommandText = requete;
                                _command.Transaction = _transaction;
                                idLibelle = (int)_command.ExecuteScalar();
                            }
                            var jour = DateTime.Parse(dgRow.Cells[4].Value.ToString());
                            var montant = double.Parse(dgRow.Cells[3].Value.ToString());
                            var mois = jour.ToLongDateString();
                            mois = mois.Remove(mois.LastIndexOf(" "), 5);
                            mois = mois.Substring(mois.LastIndexOf(" ") + 1);
                            requete = "SELECT * FROM recette_tbl  WHERE date_paiement =@date_paiement1 AND id_libelle=@id_libelle1 AND mois=@mois1 AND annee=@annee1";
                            _command.Parameters.Add(new MySqlParameter("date_paiement1", jour));
                            _command.Parameters.Add(new MySqlParameter("mois1", mois));
                            _command.Parameters.Add(new MySqlParameter("annee1", jour.Year));
                            _command.Parameters.Add(new MySqlParameter("id_libelle1", idLibelle));
                            _command.CommandText = requete;
                            _command.Transaction = _transaction;
                            var reader3 = _command.ExecuteReader();
                            var dt3 = new System.Data. DataTable();
                            dt3.Load(reader3);
                            reader3.Close();
                            if (dt3.Rows.Count > 0)
                            {
                                requete = "UPDATE recette_tbl SET montant=" + montant + "   WHERE date_paiement =@date_paiement AND id_libelle=@id_libelle AND @mois=mois AND annee=@annee";
                            }
                            else
                            {
                                requete = "INSERT INTO recette_tbl(date_paiement,montant,mois,annee,id_libelle)" +
                                               " VALUES( @date_paiement," + montant + ", @mois,@annee,@id_libelle)";
                            }
                            _command.CommandText = requete;
                            _command.Parameters.Add(new MySqlParameter("date_paiement", jour));
                            _command.Parameters.Add(new MySqlParameter("mois", mois));
                            _command.Parameters.Add(new MySqlParameter("annee", jour.Year));
                            _command.Parameters.Add(new MySqlParameter("id_libelle", idLibelle));
                            _command.Transaction = _transaction;
                            _command.ExecuteNonQuery();
                        }

                        _command.Parameters.Clear();

                    }
                    MonMessageBox.ShowBox("Données validées avec succés", "Affirmation", "affirmation.png");
                    _transaction.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("L'enregistrement a échoué", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }
    
    }
}
