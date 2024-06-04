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

        public List<Patient> GetWaitingPatients()
        {
            List<Patient> patients = new List<Patient>();

            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM patients WHERE id IN (SELECT idpatient FROM visites WHERE num_salle IS NULL)", connection);

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

        public void SaveVisitsToDatabase(List<Visite> visites)
        {
            connection.Open();

            foreach (Visite visite in visites)
            {
                SqlCommand command = new SqlCommand("INSERT INTO visites (idpatient, nom_medecin, cout_visite, date_visite, num_salle) VALUES (@idpatient, @nom_medecin, @cout_visite, @date_visite, @num_salle)", connection);
                command.Parameters.AddWithValue("@idpatient", visite.IdPatient);
                command.Parameters.AddWithValue("@nom_medecin", visite.NomMedecin);
                command.Parameters.AddWithValue("@cout_visite", visite.CoutVisite);
                command.Parameters.AddWithValue("@date_visite", visite.DateVisite);
                command.Parameters.AddWithValue("@num_salle", visite.NumSalle);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        public void EnregistrerVisitesEnBaseDeDonnees()
        {
            List<Visite> visites = GetAllVisites(); // Obtenez la liste des visites à partir de votre source de données
            SaveVisitsToDatabase(visites);
            Console.WriteLine("Visites enregistrées en base de données.");
        }

        private void AfficherToutesLesVisites()
        {
            List<Visite> visites = GetAllVisites();
            foreach (var visite in visites)
            {
                Console.WriteLine(visite.ToString());
            }
        }

        private void AfficherVisiteParId()
        {
            Console.Write("Entrez l'ID de la visite : ");
            int id = int.Parse(Console.ReadLine());
            Visite visite = GetVisiteById(id);
            if (visite != null)
            {
                Console.WriteLine(visite.ToString());
            }
            else
            {
                Console.WriteLine("Visite non trouvée.");
            }
        }

        private void AjouterNouvelleVisite()
        {
            Console.Write("Entrez l'ID du patient : ");
            int idPatient = int.Parse(Console.ReadLine());
            Console.Write("Entrez le nom du médecin : ");
            string nomMedecin = Console.ReadLine();
            Console.Write("Entrez le coût de la visite : ");
            int coutVisite = int.Parse(Console.ReadLine());
            Console.Write("Entrez la date de la visite (aaaa-mm-jj) : ");
            DateTime dateVisite = DateTime.Parse(Console.ReadLine());
            Console.Write("Entrez le numéro de la salle : ");
            int numSalle = int.Parse(Console.ReadLine());

            Visite nouvelleVisite = new Visite
            {
                IdPatient = idPatient,
                NomMedecin = nomMedecin,
                CoutVisite = coutVisite,
                DateVisite = dateVisite,
                NumSalle = numSalle
            };

            InsertVisite(nouvelleVisite);
            Console.WriteLine("Nouvelle visite ajoutée.");
        }

        private void AfficherPatientsEnAttente()
        {
            List<Patient> patients = GetWaitingPatients();
            foreach (var patient in patients)
            {
                Console.WriteLine(patient.ToString());
            }
        }
    }
}
