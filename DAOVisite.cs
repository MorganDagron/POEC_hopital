using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    NomMedecin = (string)reader["nom_medecin"],
                    CoutVisite = (int)reader["cout_visite"],
                    DateVisite = (DateTime)reader["date_visite"],
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
                    NomMedecin = (string)reader["nom_medecin"],
                    CoutVisite = (int)reader["cout_visite"],
                    DateVisite = (DateTime)reader["date_visite"],
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

            SqlCommand command = new SqlCommand("INSERT INTO visites (idpatient, nom_medecin, cout_visite, date_visite, num_salle) VALUES (@idpatient, @nom_medecin, @date_visite, @num_salle)", connection);
            command.Parameters.AddWithValue("@idpatient", visite.IdPatient);
            command.Parameters.AddWithValue("@nom_medecin", visite.NomMedecin);
            command.Parameters.AddWithValue("@date_visite", visite.DateVisite);
            command.Parameters.AddWithValue("@num_salle", visite.NumSalle);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateVisite(Visite visite)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE visites SET idpatient = @idpatient, nom_medecin = @nom_medecin, date_visite = @date_visite, num_salle = @num_salle WHERE id = @id", connection);
            command.Parameters.AddWithValue("@idpatient", visite.IdPatient);
            command.Parameters.AddWithValue("@nom_medecin", visite.NomMedecin);
            command.Parameters.AddWithValue("@date_visite", visite.DateVisite);
            command.Parameters.AddWithValue("@num_salle", visite.NumSalle);
            command.Parameters.AddWithValue("@id", visite.Id);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteVisite(int id)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM visites WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
