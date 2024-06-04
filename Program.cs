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
            AffichageLogin();
            //TestCoBDD();
            //AddPatientToBdd();
            //TestAffectationSalle1Puis2();

            AddPatientToBdd();
            TestAffectationSalle1Puis2();
        }

        static void AffichageLogin()
        {
            Authentification personne = new Authentification();
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
                    AffichageMenuPrincipal(personne);
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

        static void AffichageMenuPrincipal(Authentification P)
        {
            List<Patient> fileAttente = new List<Patient>();
            DAOVisite daoVisite = new DAOVisite();

            Console.WriteLine($"Bonjour {P.Metier} {P.Nom}");

            if (P.Metier == 0)
            {
                Console.WriteLine($"Interface {P.Metier} - Tapez le n° correpondant à votre choix");
                Console.WriteLine("1. Ajouter un patient à la file d'attente");
                Console.WriteLine("2. Afficher la file d'attente");
                Console.WriteLine("3. Afficher le prochain patient de la file");
                Console.WriteLine("4. Déconnexion");
                int choixUtilisateur = Convert.ToInt32(Console.ReadLine());
                choixMenu(choixUtilisateur, P, fileAttente);
                //<List>Patient patientAttente = CreationPatients();

            }
            else if (P.Metier >= 1)
            {
                bool continuer = true;

                while (continuer)
                {
                    Console.WriteLine($"Interface {P.Metier} - Choix de la section via n° correspondant");
                    Console.WriteLine("1. Ajouter une nouvelle visite");
                    Console.WriteLine("2. Afficher les patients en attente");
                    Console.WriteLine("3. Sauvegarder les visites en base de données");
                    Console.WriteLine("4. Déconnexion");

                    string choix = Console.ReadLine();

                    //switch (choix)
                    //{
                    //    case "1":
                    //        AjouterNouvelleVisite(daoVisite);
                    //        break;
                    //    case "2":
                    //        AfficherPatientsEnAttente(daoVisite);
                    //        break;
                    //    case "3":
                    //        EnregistrerVisitesEnBaseDeDonnees(daoVisite);
                    //        break;
                    //    case "4":
                    //        continuer = false;
                    //        break;
                    //    default:
                    //        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    //        break;
                    //}
                }
            }
            else
            {
                Console.WriteLine("Menu à venir");
            }
        }


        public static void choixMenu(int choix, Authentification P, List<Patient> fileAttente)
        {
            switch (choix)
            {
                case 1:
                    AjoutPatientFileAttente(fileAttente);
                    break;
                case 2:
                    //Console.WriteLine("Liste des patients en attente");
                    //foreach (Patient p in fileAttente)
                    //{
                    //    Console.WriteLine(p.ToString());
                    //}
                    break;
                default:
                    Console.WriteLine("Veuillez selectionner un n° présent dans la liste");
                    break;
            }
        }

        private static void EnregistrerVisitesEnBaseDeDonnees(DAOVisite daoVisite)
        {
            // Obtenez la liste des visites à partir de la méthode GetAllVisites de daoVisite
            List<Visite> visites = daoVisite.GetAllVisites();

            // Enregistrez les visites dans la base de données
            daoVisite.SaveVisitsToDatabase(visites);

            // Affichez un message indiquant que les visites ont été enregistrées en base de données
            Console.WriteLine("Visites enregistrées en base de données.");
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
            Patient patient1 = new Patient (1, "DUPONT", "Pierre", 30, "12 rue de la Paix", "06 12 34 56 78");
            Patient patient2 = new Patient (2, "MARTIN", "Marie", 25, "45 avenue de la République", "07 89 01 23 45");
            Patient patient3 = new Patient (3, "LEFEBVRE", "Jean", 40, "78 rue de la Liberté", "05 67 89 01 23");
            Patient patient4 = new Patient (4, "DURAND", "Sophie", 28, "34 rue de la Paix",  "06 78 90 12 34");
            Patient patient5 = new Patient (5, "ROUSSEAU", "François", 35, "90 avenue de la République", "07 12 34 56 78");

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
            Console.WriteLine("Entrez l'ID du patient");
            int idPatient = Convert.ToInt32(Console.ReadLine());

            DAOPatient dao = new DAOPatient();
            if (dao.GetPatientById(idPatient) != null)
            {
                fileAttente.Add(dao.GetPatientById(idPatient));
                Console.WriteLine("Patient connu dans l'hopital, ajouté à la liste d'attente");
                Console.WriteLine(dao.GetPatientById(idPatient).ToString());
            }

            else
            {
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

                Patient nouveauPatient = new Patient(0, newNom, newPrenom, newAge, newAdresse, newTel);
                dao.InsertPatient(nouveauPatient);
            }

        }

    }
}

