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
            DAOVisite dao = new DAOVisite();
            List<Patient> fileAttente = new List<Patient>();
            List<Visite> visites = new List<Visite>();
            Salle s1 = new Salle(1);
            Salle s2 = new Salle(2);
            Hopital hopital = Hopital.Instance;
            hopital.AddSalle(s1);
            hopital.AddSalle(s2);

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

                    switch (choix)
                    {
                        case "1":
                            AfficherPatient(fileAttente.Last(), fileAttente, P, visites);
                            break;
                        case "2":
                            //AfficherFileAttente();
                            break;
                        case "3":
                            foreach (Visite v in visites)
                                dao.InsertVisite(v);
                            break;
                        case "4":
                            continuer = false;
                            break;
                        default:
                            Console.WriteLine("choix invalide. veuillez réessayer.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Menu à venir");
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

