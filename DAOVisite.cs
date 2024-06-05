using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _projet_hopital
{
    public class DAOVisite
    {
        private readonly SqlConnection connection;

        public DAOVisite()
        {
            connection = new SqlConnection(ConnectionString.ConnexionBdd);
        }

        // Méthode pour ajouter ou mettre à jour une visite avec le temps d'attente
        public void UpdateVisiteWithTempsAttente(int visiteId, TimeSpan tempsAttente)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("UPDATE visites SET temps_attente = @temps_attente WHERE id = @id", connection);
            command.Parameters.AddWithValue("@temps_attente", tempsAttente);
            command.Parameters.AddWithValue("@id", visiteId);

            command.ExecuteNonQuery();
            connection.Close();
        }

        // Méthode pour obtenir le nombre de visites par salle et médecin
        public List<Visite> GetVisitesBySalleAndMedecin()
        {
            List<Visite> visites = new List<Visite>();

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM visites ORDER BY num_salle, medecin", connection);
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
                    NumSalle = (int)reader["num_salle"],
                    TempsAttente = (TimeSpan)reader["temps_attente"]  // Ajouté
                };

                visites.Add(visite);
            }

            reader.Close();
            connection.Close();
            return visites;
        }

        // Méthode pour obtenir le nombre de visites pour un médecin depuis le début
        public int GetNombreVisitesDepuisDebut(int idMedecin)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM visites WHERE idmedecin = @idmedecin", connection);
            command.Parameters.AddWithValue("@idmedecin", idMedecin);

            int count = (int)command.ExecuteScalar();

            connection.Close();
            return count;
        }

        // Méthode pour obtenir le nombre de visites pour un médecin entre deux dates
        public int GetNombreVisitesEntreDates(int idMedecin, DateTime dateMin, DateTime dateMax)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM visites WHERE idmedecin = @idmedecin AND date BETWEEN @dateMin AND @dateMax", connection);
            command.Parameters.AddWithValue("@idmedecin", idMedecin);
            command.Parameters.AddWithValue("@dateMin", dateMin);
            command.Parameters.AddWithValue("@dateMax", dateMax);

            int count = (int)command.ExecuteScalar();

            connection.Close();
            return count;
        }
    }
}
