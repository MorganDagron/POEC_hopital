using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _projet_hopital
{
    class DAOVisite
    {
        SqlConnection connection = new SqlConnection(ConnectionString.ConnexionBdd);

        public List<Visite> GetAllVisites()
        {
            List<Visite> visites = new List<Visite>();

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM visites", connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Visite visite = new Visite
                {
                    Id = (int)reader["id"],
                    IdPatient = (int)reader["idpatient"],
                    NomMedecin = (string)reader["medecin"],
                    CoutVisite = (int)reader["cout_visite"],
                    DateVisite = (DateTime)reader["date"],
                    NumSalle = (int)reader["num_salle"]
                };

                visites.Add(visite);
            }

            reader.Close();
            connection.Close();
            return visites;
        }

        public Visite GetVisiteById(int id)
        {
            Visite visite = null;

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM visites WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                visite = new Visite
                {
                    Id = (int)reader["id"],
                    IdPatient = (int)reader["idpatient"],
                    NomMedecin = (string)reader["medecin"],
                    CoutVisite = (int)reader["cout_visite"],
                    DateVisite = (DateTime)reader["date"],
                    NumSalle = (int)reader["num_salle"]
                };
            }

            reader.Close();
            connection.Close();
            return visite;
        }

        public void InsertVisite(Visite visite)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO visites (idpatient, date, medecin, num_salle) VALUES (@idpatient, @date, @medecin, @num_salle)", connection);
            command.Parameters.AddWithValue("@idpatient", visite.IdPatient);
            command.Parameters.AddWithValue("@date", visite.DateVisite);
            command.Parameters.AddWithValue("@medecin", visite.NomMedecin);
            command.Parameters.AddWithValue("@num_salle", visite.NumSalle);

            command.ExecuteNonQuery();
            connection.Close();
        }

        //public List<Patient> GetWaitingPatients()
        //{
        //    List<Patient> patients = new List<Patient>();

        //    connection.Open();

        //    SqlCommand command = new SqlCommand("SELECT * FROM patients WHERE id IN (SELECT idpatient FROM visites WHERE num_salle IS NULL)", connection);

        //    SqlDataReader reader = command.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        Patient patient = new Patient
        //        {
        //            Id = (int)reader["id"],
        //            Nom = (string)reader["nom"],
        //            Prenom = (string)reader["prenom"],
        //            Age = (int)reader["age"],
        //            Adresse = (string)reader["adresse"],
        //            Telephone = (string)reader["telephone"]
        //        };

        //        patients.Add(patient);
        //    }

        //    reader.Close();
        //    connection.Close();
        //    return patients;
        //}
    }
}
