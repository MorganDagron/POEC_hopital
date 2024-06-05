using System;
using System.Collections.Generic;
using System.Linq;

namespace _projet_hopital
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Patient> fileAttente = new List<Patient>();
            AffichageLogin(fileAttente);
        }

        static void AffichageLogin(List<Patient> fileAttente)
        {
            Authentification personne = new Authentification();

            Salle s1 = new Salle(1);
            Salle s2 = new Salle(2);
            Hopital hopital = Hopital.Instance;
            hopital.AddSalle(s1);
            hopital.AddSalle(s2);
            bool accesAccorde = false;

            while (!accesAccorde)
            {
                Console.WriteLine("Identifiez-vous pour accéder au service");
                Console.WriteLine("Identifiant :");
                string nomLogin = Console.ReadLine();
                Console.WriteLine("Mot de passe :");
                string passwordLogin = Console.ReadLine();
                personne = VerificationLogin(nomLogin, passwordLogin);

                if (personne.Login != null)
                {
                    accesAccorde = true;
                    AffichageMenuPrincipal(personne, fileAttente);
                }
                else
                {
                    Console.WriteLine("Appuyez sur n'importe quelle touche pour réessayer de vous connecter ou 'Q' pour quitter");
                    string quitter = Console.ReadLine();
                    if (quitter.ToLower() == "q")
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        static void AffichageMenuPrincipal(Authentification P, List<Patient> fileAttente)
        {
            if (P.Metier == 0)
            {
                Console.WriteLine($"Bonjour Secrétaire {P.Nom}");
                choixMenuSecretaire(P, fileAttente);
            }
            else if (P.Metier >= 1)
            {
                Console.WriteLine($"Bonjour Médecin {P.Nom}");
                choixMenuMedecin(P, fileAttente);
            }
            else
            {
                Console.WriteLine($"Bonjour Admin {P.Nom}");
                choixMenuAdmin();
            }
        }

        public static void choixMenuSecretaire(Authentification P, List<Patient> fileAttente)
        {
            bool quitter = false;
            while (!quitter)
            {
                Console.WriteLine("\n\n------------------------");
                Console.WriteLine($"Interface Secrétaire - Tapez le n° correspondant à votre choix");
                Console.WriteLine("1. Ajouter un patient à la file d'attente");
                Console.WriteLine("2. Afficher la file d'attente");
                Console.WriteLine("3. Afficher le prochain patient de la file");
                Console.WriteLine("4. Déconnexion");
                Console.WriteLine("------------------------");
                int choixUtilisateur = Convert.ToInt32(Console.ReadLine());

                switch (choixUtilisateur)
                {
                    case 1:
                        AjoutPatientFileAttente(fileAttente);
                        break;
                    case 2:
                        AfficherPatientsEnAttente(fileAttente);
                        break;
                    case 3:
                        AfficherProchainPatient(fileAttente);
                        break;
                    case 4:
                        quitter = true;
                        AffichageLogin(fileAttente);
                        break;
                    default:
                        Console.WriteLine("Veuillez sélectionner un n° présent dans la liste");
                        break;
                }
            }
        }

        public static void choixMenuMedecin(Authentification P, List<Patient> fileAttente)
        {
            List<Visite> visites = new List<Visite>();
            DAOVisite dao = new DAOVisite();
            bool quitter = false;
            while (!quitter)
            {
                Console.WriteLine("\n\n------------------------");
                Console.WriteLine($"Interface Médecin - Choix de la section via n° correspondant");
                Console.WriteLine("1. Ajouter une nouvelle visite");
                Console.WriteLine("2. Afficher les patients en attente");
                Console.WriteLine("3. Sauvegarder les visites en base de données");
                Console.WriteLine("4. Déconnexion");
                Console.WriteLine("------------------------");
                int choixUtilisateur = Convert.ToInt32(Console.ReadLine());

                switch (choixUtilisateur)
                {
                    case 1:
                        if (fileAttente.Any())
                            AjoutVisite(fileAttente.First(), visites, P);
                        else
                            Console.WriteLine("Il n'y a personne dans la file d'attente");
                        break;
                    case 2:
                        AfficherPatientsEnAttente(fileAttente);
                        break;
                    case 3:
                        foreach (Visite v in visites)
                            dao.UpdateVisiteWithTempsAttente(v.Id, v.TempsAttente);
                        break;
                    case 4:
                        quitter = true;
                        AffichageLogin(fileAttente);
                        break;
                    default:
                        Console.WriteLine("Veuillez sélectionner un n° présent dans la liste");
                        break;
                }

            }
        }

        public static void choixMenuAdmin()
        {
            bool quitter = false;
            DAOPatient dao = new DAOPatient();

            while (!quitter)
            {
                Console.WriteLine("\n\n------------------------");
                Console.WriteLine("Interface Admin - Tapez le n° correspondant à votre choix");
                Console.WriteLine("1. Ajouter un nouveau patient");
                Console.WriteLine("2. Supprimer un patient selon son ID");
                Console.WriteLine("3. Modifier les informations d'un patient");
                Console.WriteLine("4. Afficher la liste de tous les patients");
                Console.WriteLine("5. Afficher un patient selon son ID");
                Console.WriteLine("6. Déconnexion");
                Console.WriteLine("------------------------");
                int choixUtilisateur = Convert.ToInt32(Console.ReadLine());

                switch (choixUtilisateur)
                {
                    case 1:
                        AjouterNouveauPatient(dao);
                        break;
                    case 2:
                        SupprimerPatient(dao);
                        break;
                    case 3:
                        ModifierPatient(dao);
                        break;
                    case 4:
                        AfficherTousLesPatients(dao);
                        break;
                    case 5:
                        AfficherPatientParId(dao);
                        break;
                    case 6:
                        quitter = true;
                        break;
                    default:
                        Console.WriteLine("Veuillez sélectionner un n° présent dans la liste");
                        break;
                }
            }
        }

        private static void AjouterNouveauPatient(DAOPatient dao)
        {
            Console.WriteLine("Entrez l'ID du patient:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Entrez le nom du patient:");
            string nom = Console.ReadLine();
            Console.WriteLine("Entrez le prénom du patient:");
            string prenom = Console.ReadLine();
            Console.WriteLine("Entrez l'âge du patient:");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Entrez l'adresse du patient:");
            string adresse = Console.ReadLine();
            Console.WriteLine("Entrez le téléphone du patient:");
            string telephone = Console.ReadLine();

            Patient patient = new Patient(id, nom, prenom, age, adresse, telephone);
            dao.InsertPatient(patient);
            Console.WriteLine("Patient ajouté avec succès.");
        }

        private static void SupprimerPatient(DAOPatient dao)
        {
            Console.WriteLine("Entrez l'ID du patient à supprimer:");
            int id = Convert.ToInt32(Console.ReadLine());
            dao.DeletePatient(id);
            Console.WriteLine("Patient supprimé avec succès.");
        }

        private static void ModifierPatient(DAOPatient dao)
        {
            Console.WriteLine("Entrez l'ID du patient à modifier:");
            int id = Convert.ToInt32(Console.ReadLine());
            Patient patient = dao.GetPatientById(id);
            if (patient == null)
            {
                Console.WriteLine("Patient non trouvé.");
                return;
            }

            Console.WriteLine("Entrez le nouveau nom du patient (actuel: " + patient.Nom + "):");
            patient.Nom = Console.ReadLine();
            Console.WriteLine("Entrez le nouveau prénom du patient (actuel: " + patient.Prenom + "):");
            patient.Prenom = Console.ReadLine();
            Console.WriteLine("Entrez le nouvel âge du patient (actuel: " + patient.Age + "):");
            patient.Age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Entrez la nouvelle adresse du patient (actuel: " + patient.Adresse + "):");
            patient.Adresse = Console.ReadLine();
            Console.WriteLine("Entrez le nouveau téléphone du patient (actuel: " + patient.Telephone + "):");
            patient.Telephone = Console.ReadLine();

            dao.UpdatePatient(patient);
            Console.WriteLine("Patient modifié avec succès.");
        }

        private static void AfficherTousLesPatients(DAOPatient dao)
        {
            List<Patient> patients = dao.GetAllPatients();
            foreach (var patient in patients)
            {
                Console.WriteLine(patient.ToString());
            }
        }

        private static void AfficherPatientParId(DAOPatient dao)
        {
            Console.WriteLine("Entrez l'ID du patient à afficher:");
            int id = Convert.ToInt32(Console.ReadLine());
            Patient patient = dao.GetPatientById(id);
            if (patient != null)
            {
                Console.WriteLine(patient.ToString());
            }
            else
            {
                Console.WriteLine("Patient non trouvé.");
            }
        }

        private static void AfficherPatientsEnAttente(List<Patient> fileAttente)
        {
            int cpt = 1;
            Console.WriteLine("\n\nPatients présents dans la file d'attente:");
            foreach (Patient p in fileAttente)
            {
                Console.WriteLine("Patient n°" + cpt);
                Console.WriteLine(p.ToString());
                cpt++;
            }
        }

        private static void AfficherProchainPatient(List<Patient> fileAttente)
        {
            if (fileAttente.Any())
            {
                Patient p = fileAttente.Last();
                Console.WriteLine("\nProchain patient de la file d'attente:");
                Console.WriteLine(p.ToString());
            }
            else
            {
                Console.WriteLine("Il n'y a pas de patients dans la file d'attente.");
            }
        }

        private static void AfficherPatient(Patient p, List<Patient> fileAttente, Authentification P, List<Visite> visites)
        {
            Console.WriteLine("\n\nDétail du patient:");
            Console.WriteLine(p.ToString());
            fileAttente.Remove(p);

            Console.WriteLine("Souhaitez-vous créer une visite pour ce patient ? (o/n)");
            if (Console.ReadLine().ToLower() == "o")
            {
                AjoutVisite(p, visites, P);
            }
        }

        private static void AjoutPatientFileAttente(List<Patient> fileAttente)
        {
            Console.WriteLine("Veuillez entrer les informations suivantes pour ajouter un patient à la file d'attente:");
            Console.WriteLine("ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Nom:");
            string nom = Console.ReadLine();
            Console.WriteLine("Prénom:");
            string prenom = Console.ReadLine();
            Console.WriteLine("Âge:");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Adresse:");
            string adresse = Console.ReadLine();
            Console.WriteLine("Téléphone:");
            string telephone = Console.ReadLine();

            Patient nouveauPatient = new Patient(id, nom, prenom, age, adresse, telephone);
            fileAttente.Add(nouveauPatient);
            Console.WriteLine("Patient ajouté à la file d'attente.");
        }

        private static void AjoutVisite(Patient patient, List<Visite> visites, Authentification P)
        {
            Console.WriteLine("Date de la visite (jj/mm/aaaa) :");
            DateTime dateVisite = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Description de la visite :");
            string description = Console.ReadLine();

            Console.WriteLine("Coût de la visite :");
            int coutVisite = int.Parse(Console.ReadLine()); // Lire le coût de la visite

            int nouvelleId = visites.Count + 1; // Créer un nouvel ID pour la visite

            Console.WriteLine("Heure d'arrivée (hh:mm:ss) :");
            TimeSpan heureArrivee = TimeSpan.Parse(Console.ReadLine());

            Console.WriteLine("Heure de passage (hh:mm:ss) :");
            TimeSpan heurePassage = TimeSpan.Parse(Console.ReadLine());

            Visite nouvelleVisite = new Visite
            {
                Id = nouvelleId,
                DateVisite = dateVisite,
                NomMedecin = P.Nom, // Utiliser le nom de l'authentification comme nom du médecin
                IdPatient = patient.Id,
                CoutVisite = coutVisite,
                NumSalle = patient.NumSalle, // Utiliser le numéro de la salle du patient
                TempsAttente = heurePassage - heureArrivee,
                HeureArrivee = heureArrivee,
                HeurePassage = heurePassage
            };

            visites.Add(nouvelleVisite);
            Console.WriteLine("Visite ajoutée avec succès.");
        }

        private static Authentification VerificationLogin(string nomLogin, string passwordLogin)
        {
            DAOAuthentification daoAuth = new DAOAuthentification();
            Authentification P = daoAuth.GetAuthentificationByCredentials(nomLogin, passwordLogin);

            if (P != null)
            {
                return P;
            }
            return new Authentification();
        }
    }
}
