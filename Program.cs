using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddPatientToBdd();
            List<Patient> fileAttente = new List<Patient>();
            AffichageLogin(fileAttente);

            //TestAffectationSalle1Puis2();
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
                    Console.WriteLine("Appuyer sur n'importe quelle touche pour réessayer de vous connecter ou 'Q' pour quitter");
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
                Console.WriteLine("Menu à venir");
            }
        }


        public static void choixMenuSecretaire(Authentification P, List<Patient> fileAttente)
        {
            bool quitter = false;
            while (!quitter)
            {
                Console.WriteLine("\n\n------------------------");
                Console.WriteLine($"Interface Secrétaire - Tapez le n° correpondant à votre choix");
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
                        Console.WriteLine("Veuillez selectionner un n° présent dans la liste");
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
                Console.WriteLine("\n\n------------------------"); ;
                Console.WriteLine($"Interface Medecin - Choix de la section via n° correspondant");
                Console.WriteLine("1. Ajouter une nouvelle visite");
                Console.WriteLine("2. Afficher les patients en attente");
                Console.WriteLine("3. Sauvegarder les visites en base de données");
                Console.WriteLine("4. Déconnexion");
                Console.WriteLine("------------------------"); ;
                int choixUtilisateur = Convert.ToInt32(Console.ReadLine());

                switch (choixUtilisateur)
                {
                    case 1:
                        if (fileAttente.Any())
                            AfficherPatient(fileAttente.Last(), fileAttente, P, visites);
                        else
                            Console.WriteLine("il n'y a personne dans la file d'attente");
                        break;
                    case 2:
                        AfficherPatientsEnAttente(fileAttente);
                        break;
                    case 3:
                        foreach (Visite v in visites)
                            dao.InsertVisite(v);
                        break;
                    case 4:
                        quitter = true;
                        AffichageLogin(fileAttente);
                        break;
                    default:
                        Console.WriteLine("Veuillez selectionner un n° présent dans la liste");
                        break;
                }

            }


        }

        private static void AfficherPatientsEnAttente(List<Patient> fileAttente)
        {
            int cpt = 1;
            Console.WriteLine("\n\nPatients présents dans la file d'attente\n");
            if (fileAttente != null)
            {
                foreach (var patient in fileAttente)
                {
                    Console.WriteLine("Position " + cpt);
                    Console.WriteLine(patient.ToString() + "\n");
                    cpt++;
                }
            }
        }

        private static void AfficherPatient(Patient patient, List<Patient> fileAttente, Authentification p, List<Visite> visites)
        {
            DAOVisite dao = new DAOVisite();
            patient.AffecteSalle();
            Console.WriteLine(patient.ToString());
            //Ajouté la patient à la la liste des visites
            visites.Add(new Visite(patient.Id, p.Nom, DateTime.Now, patient.NumSalle));
            //Si la liste est supérieur ou égal à 5 alors envoyé en bdd automatiquement
            if (visites.Count >= 5)
                foreach (Visite v in visites)
                    dao.InsertVisite(v);
            //supprimer patient de la file d'attente
            fileAttente.Remove(patient);

        }

        private static void AfficherProchainPatient(List<Patient> fileAttente)
        {
            Console.WriteLine("Prochaine personne en consultation :");
            if (fileAttente[0] != null)
            {
                Console.WriteLine(fileAttente[0].ToString());
            }
        }


        static Authentification VerificationLogin(string nom, string password)
        {
            DAOAuthentification dao = new DAOAuthentification();
            Authentification personneTemp = new Authentification();

            if (dao.GetAuthentification(nom, password) != null)
            {
                personneTemp = dao.GetAuthentification(nom, password);
                Console.WriteLine("Identifiant ok");
            }
            else
            {
                Console.WriteLine("Identifiant non reconnu");
            }

            return personneTemp;
        }

        private static void TestAffectationSalle1Puis2()
        {
            DAOPatient dao = new DAOPatient();
            Salle s1 = new Salle(1);
            Salle s2 = new Salle(2);
            Hopital hopital = Hopital.Instance;
            hopital.AddSalle(s1);
            hopital.AddSalle(s2);

            Patient p1 = dao.GetPatientById(1);
            Patient p2 = dao.GetPatientById(2);

            Console.WriteLine("----------TEST1 → 2 salles ouvertes----------");
            AffecteSalle(p1, p2);
            Console.WriteLine("----------TEST2 → 1 salle ouverte----------");
            s1.EstOuvert = false;
            AffecteSalle(p1, p2);
            Console.WriteLine("----------TEST3 → 0 salle ouverte----------");
            hopital.DeleteSalle(s2);
        }
        private static void AffecteSalle(Patient p1, Patient p2)
        {
            p1.AffecteSalle();
            p2.AffecteSalle();
        }

        private static void AddPatientToBdd()
        {
            // Création de 5 patients
            Patient patient1 = new Patient(1, "DUPONT", "Pierre", 30, "12 rue de la Paix", "06 12 34 56 78");
            Patient patient2 = new Patient(2, "MARTIN", "Marie", 25, "45 avenue de la République", "07 89 01 23 45");
            Patient patient3 = new Patient(3, "LEFEBVRE", "Jean", 40, "78 rue de la Liberté", "05 67 89 01 23");
            Patient patient4 = new Patient(4, "DURAND", "Sophie", 28, "34 rue de la Paix", "06 78 90 12 34");
            Patient patient5 = new Patient(5, "ROUSSEAU", "François", 35, "90 avenue de la République", "07 12 34 56 78");

            // Insertion des patients dans la base de données à l'aide du DAO
            DAOPatient dao = new DAOPatient();
            dao.InsertPatient(patient1);
            dao.InsertPatient(patient2);
            dao.InsertPatient(patient3);
            dao.InsertPatient(patient4);
            dao.InsertPatient(patient5);
        }

        public static void AjoutPatientFileAttente(List<Patient> fileAttente)
        {
            bool AjoutPatientOK = true;
            Console.WriteLine("Entrez l'ID du patient");
            int idPatient = Convert.ToInt32(Console.ReadLine());
            DAOPatient dao = new DAOPatient();


            foreach (var patient in fileAttente)
            {
                if (patient.Id == idPatient)
                {
                    AjoutPatientOK = false;
                }
            }

            if (AjoutPatientOK)
            {
                if (dao.GetPatientById(idPatient) != null)
                {
                    fileAttente.Add(dao.GetPatientById(idPatient));
                    Console.WriteLine("Patient connu dans l'hopital, ajouté à la liste d'attente");
                    Console.WriteLine(dao.GetPatientById(idPatient).ToString());
                }

                else
                {
                    Console.WriteLine("Patient non connu dans l'hopital, veuillez renseigner ses informations personelles");
                    Console.WriteLine("Saisissez son nom");
                    string newNom = Console.ReadLine();

                    Console.WriteLine("Saisissez son prénom");
                    string newPrenom = Console.ReadLine();

                    Console.WriteLine("Saisissez son âge");
                    int newAge = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Saisissez son adresse");
                    string newAdresse = Console.ReadLine();

                    Console.WriteLine("Saisissez son n° de téléphone");
                    string newTel = Console.ReadLine();

                    Patient nouveauPatient = new Patient(idPatient, newNom, newPrenom, newAge, newAdresse, newTel);
                    dao.InsertPatient(nouveauPatient);
                    fileAttente.Add(nouveauPatient);
                }
            }
            else
            {
                Console.WriteLine("patient déja présent dans la file d'attente");
            }

        }

    }
}

