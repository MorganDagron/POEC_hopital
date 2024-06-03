using System.Collections.Generic;
using System.Data.SqlClient;

namespace _projet_hopital
{
    class DAOPatient
    {
        SqlConnection connection = new SqlConnection(ConnectionString.ConnexionBdd);

        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM patients", connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Patient patient = new Patient
                {
                    Id = (int)reader["id"],
                    Nom = (string)reader["nom"],
                    Prenom = (string)reader["prenom"],
                    Age = (int)reader["age"],
                    Adresse = (string)reader["adresse"],
                    Telephone = (string)reader["telephone"]
                };

                patients.Add(patient);
            }

            reader.Close();
            connection.Close();
            return patients;
        }

        public Patient GetPatientById(int id)
        {
            Patient patient = null;

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM patients WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                patient = new Patient
                {
                    Id = (int)reader["id"],
                    Nom = (string)reader["nom"],
                    Prenom = (string)reader["prenom"],
                    Age = (int)reader["age"],
                    Adresse = (string)reader["adresse"],
                    Telephone = (string)reader["telephone"]
                };
            }

            reader.Close();
            connection.Close();
            return patient;
        }

        public void InsertPatient(Patient patient)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO patients (nom, prenom, age, adresse, telephone) VALUES (@nom, @prenom, @age, @adresse, @telephone)", connection);
            command.Parameters.AddWithValue("@nom", patient.Nom);
            command.Parameters.AddWithValue("@prenom", patient.Prenom);
            command.Parameters.AddWithValue("@age", patient.Age);
            command.Parameters.AddWithValue("@adresse", patient.Adresse);
            command.Parameters.AddWithValue("@telephone", patient.Telephone);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdatePatient(Patient patient)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE patients SET nom = @nom, prenom = @prenom, age = @age, adresse = @adresse, telephone = @telephone WHERE id = @id", connection);
            command.Parameters.AddWithValue("@nom", patient.Nom);
            command.Parameters.AddWithValue("@prenom", patient.Prenom);
            command.Parameters.AddWithValue("@age", patient.Age);
            command.Parameters.AddWithValue("@adresse", patient.Adresse);
            command.Parameters.AddWithValue("@telephone", patient.Telephone);
            command.Parameters.AddWithValue("@id", patient.Id);

            command.ExecuteNonQuery();

            connection.Close();

        }

        public void DeletePatient(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM patients WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
