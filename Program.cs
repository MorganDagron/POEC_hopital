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
            //AffichageLogin();
            //TestCoBDD();
            //TestAffectationSalle1Puis2();

            // Création d'une instance de DAOVisite
            DAOVisite daoVisite = new DAOVisite();

            // Appel de la méthode QuitMenu pour gérer les opérations de la classe Visite
            daoVisite.QuitMenu();

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
                Console.WriteLine("Nom :");
                string nomLogin = Console.ReadLine();
                Console.WriteLine("Password :");
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
            DAOVisite daoVisite = new DAOVisite();

            Console.WriteLine($"Bonjour {P.Metier} {P.Nom}");

            if (P.Metier == 0)
            {
                Console.WriteLine($"Interface {P.Metier}");
                Console.WriteLine("1. Accueillir un nouveau patient");
                Console.WriteLine("2. Afficher la file d'attente");
                Console.WriteLine("3. Sauvegarder la liste des patients du jour");
                Console.WriteLine("4. Déconnexion");
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

                    switch (choix)
                    {
                        case "1":
                            AjouterNouvelleVisite(daoVisite);
                            break;
                        case "2":
                            AfficherPatientsEnAttente(daoVisite);
                            break;
                        case "3":
                            EnregistrerVisitesEnBaseDeDonnees(daoVisite);
                            break;
                        case "4":
                            continuer = false;
                            break;
                        default:
                            Console.WriteLine("Choix invalide. Veuillez réessayer.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Menu à venir");
            }
        }

        private static void AjouterNouvelleVisite(DAOVisite daoVisite)
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

            daoVisite.InsertVisite(nouvelleVisite);
            Console.WriteLine("Nouvelle visite ajoutée.");
        }

        private static void AfficherPatientsEnAttente(DAOVisite daoVisite)
        {
            List<Patient> patients = daoVisite.GetWaitingPatients();
            foreach (var patient in patients)
            {
                Console.WriteLine(patient.ToString());
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
    }
}

