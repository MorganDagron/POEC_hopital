using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    class DAOAuthentification
    {
        SqlConnection connection = new SqlConnection(ConnectionString.ConnexionBdd);

        public List<Authentification> GetAllAuthentifications()
        {
            List<Authentification> authentifications = new List<Authentification>();

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM authentification", connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Authentification authentification = new Authentification
                {
                    Login = (string)reader["login"],
                    Password = (string)reader["password"],
                    Nom = (string)reader["nom"],
                    Metier = (int)reader["metier"]
                };
                authentifications.Add(authentification);
            }

            reader.Close();
            connection.Close();

            return authentifications;
        }

        public Authentification GetAuthentificationByLogin(string login)
        {
            Authentification authentification = null;

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM authentification WHERE login = @login", connection);
            command.Parameters.AddWithValue("@login", login);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                authentification = new Authentification
                {
                    Login = (string)reader["login"],
                    Password = (string)reader["password"],
                    Nom = (string)reader["nom"],
                    Metier = (int)reader["metier"]
                };
            }

            reader.Close();
            connection.Close();

            return authentification;

        }

        public void InsertAuthentification(Authentification authentification)
        {

            connection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO authentification (login, password, nom, metier) VALUES (@login, @password, @nom, @metier)", connection);
            command.Parameters.AddWithValue("@login", authentification.Login);
            command.Parameters.AddWithValue("@password", authentification.Password);
            command.Parameters.AddWithValue("@nom", authentification.Nom);
            command.Parameters.AddWithValue("@metier", authentification.Metier);

            command.ExecuteNonQuery();
            connection.Close();

        }

        public void UpdateAuthentification(Authentification authentification)
        {

            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE authentification SET password = @password, nom = @nom, metier = @metier WHERE login = @login", connection);
            command.Parameters.AddWithValue("@login", authentification.Login);
            command.Parameters.AddWithValue("@password", authentification.Password);
            command.Parameters.AddWithValue("@nom", authentification.Nom);
            command.Parameters.AddWithValue("@metier", authentification.Metier);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteAuthentification(string login)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM authentification WHERE login = @login", connection);
            command.Parameters.AddWithValue("@login", login);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}

