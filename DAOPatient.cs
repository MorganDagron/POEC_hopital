using System.Collections.Generic;
using System.Data.SqlClient;

namespace _projet_hopital
{
    class DAOPatient
    {
        private readonly SqlConnection connection;

        public DAOPatient()
        {
            connection = new SqlConnection(ConnectionString.ConnexionBdd);
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            try
            {
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
            }
            finally
            {
                connection.Close();
            }

            return patients;
        }

        public Patient GetPatientById(int id)
        {
            Patient patient = null;

            try
            {
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
            }
            finally
            {
                connection.Close();
            }

            return patient;
        }

        public void InsertPatient(Patient patient)
        {
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO patients (id, nom, prenom, age, adresse, telephone) VALUES (@id, @nom, @prenom, @age, @adresse, @telephone)", connection);
                command.Parameters.AddWithValue("@id", patient.Id);
                command.Parameters.AddWithValue("@nom", patient.Nom);
                command.Parameters.AddWithValue("@prenom", patient.Prenom);
                command.Parameters.AddWithValue("@age", patient.Age);
                command.Parameters.AddWithValue("@adresse", patient.Adresse);
                command.Parameters.AddWithValue("@telephone", patient.Telephone);

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdatePatient(Patient patient)
        {
            try
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
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeletePatient(int id)
        {
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DELETE FROM patients WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
