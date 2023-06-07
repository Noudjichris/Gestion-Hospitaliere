using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace SGSP.AppCode
{
    class ConnectionClassClinique
    {
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static MySqlTransaction transaction = null;
        private static string requete;

        static ConnectionClassClinique()
        {
            string connectionString =
                   //@"server=192.168.1.3;user id=hnda;password=Hnda2021;database=clinique_db";
            @"server=localhost;port=3306;user id=root;password=chris@2020;database=clinique_db";
            connection = new MySqlConnection(connectionString);
            command = new MySqlCommand("", connection);
        }

        public static System.Data.DataTable TableDesDetailsFacturesProforma(int idPatient)
        {
            var dt = new System.Data.DataTable();
            try
            {
                requete = "SELECT pro_facture_tbl.num_patient, pro_facture_tbl.date_fact,pro_det_fact.id_fact," +
                    "pro_det_fact.design,pro_det_fact.prix,pro_det_fact.qte,pro_det_fact.prix_total ,pro_facture_tbl.acte FROM pro_facture_tbl" +
                    " INNER JOIN pro_det_fact ON pro_facture_tbl.id_fact=pro_det_fact.id_fact  WHERE  pro_facture_tbl.num_patient=" + idPatient;
                command.CommandText = requete;
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

        public static System.Data.DataTable ListeDesPatientsParEntreprise(string matricule)
        {
            try
            {
                requete = "SELECT * FROM patient_tbl WHERE matricule LIKE '" + matricule + "' AND entrep LIKE '%HNDA%' ORDER BY nom, prenom";
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

    }
}
