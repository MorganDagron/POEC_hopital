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
            //TestAffectationSalle1Puis2();

            // Création d'une instance de DAOVisite
            DAOVisite daoVisite = new DAOVisite();

            // Appel de la méthode QuitMenu pour gérer les opérations de la classe Visite
            daoVisite.QuitMenu();

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
                    Console.WriteLine("2. Mettre à jour une visite");
                    Console.WriteLine("3. Supprimer une visite");
                    Console.WriteLine("4. Afficher les visites");
                    Console.WriteLine("5. Changer un patient de salle");
                    Console.WriteLine("6. Afficher les patients en attente");
                    Console.WriteLine("7. Sauvegarder les visites en base de données");
                    Console.WriteLine("8. Déconnexion");

                    string choix = Console.ReadLine();

                    switch (choix)
                    {
                        case "1":
                            AjouterNouvelleVisite(daoVisite);
                            break;
                        case "2":
                            MettreAJourVisite(daoVisite);
                            break;
                        case "3":
                            SupprimerVisite(daoVisite);
                            break;
                        case "4":
                            AfficherVisites(daoVisite);
                            break;
                        case "5":
                            ChangerPatientDansSalle(daoVisite);
                            break;
                        case "6":
                            AfficherPatientsEnAttente(daoVisite);
                            break;
                        case "7":
                            EnregistrerVisitesEnBaseDeDonnees(daoVisite);
                            break;
                        case "8":
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

        private static void MettreAJourVisite(DAOVisite daoVisite)
        {
            Console.Write("Entrez l'ID de la visite à mettre à jour : ");
            int id = int.Parse(Console.ReadLine());

            Visite visite = daoVisite.GetVisiteById(id);
            if (visite != null)
            {
                Console.Write("Entrez le nouvel ID du patient (actuel: " + visite.IdPatient + ") : ");
                visite.IdPatient = int.Parse(Console.ReadLine());
                Console.Write("Entrez le nouveau nom du médecin (actuel: " + visite.NomMedecin + ") : ");
                visite.NomMedecin = Console.ReadLine();
                Console.Write("Entrez le nouveau coût de la visite (actuel: " + visite.CoutVisite + ") : ");
                visite.CoutVisite = int.Parse(Console.ReadLine());
                Console.Write("Entrez la nouvelle date de la visite (actuel: " + visite.DateVisite.ToString("yyyy-MM-dd") + ") : ");
                visite.DateVisite = DateTime.Parse(Console.ReadLine());
                Console.Write("Entrez le nouveau numéro de la salle (actuel: " + visite.NumSalle + ") : ");
                visite.NumSalle = int.Parse(Console.ReadLine());

                daoVisite.UpdateVisite(visite);
                Console.WriteLine("Visite mise à jour.");
            }
            else
            {
                Console.WriteLine("Visite non trouvée.");
            }
        }

        private static void SupprimerVisite(DAOVisite daoVisite)
        {
            Console.Write("Entrez l'ID de la visite à supprimer : ");
            int id = int.Parse(Console.ReadLine());
            daoVisite.DeleteVisite(id);
            Console.WriteLine("Visite supprimée.");
        }

        private static void AfficherVisites(DAOVisite daoVisite)
        {
            List<Visite> visites = daoVisite.GetAllVisites();
            foreach (var visite in visites)
            {
                Console.WriteLine(visite.ToString());
            }
        }

        private static void ChangerPatientDansSalle(DAOVisite daoVisite)
        {
            Console.Write("Entrez le numéro de la nouvelle salle : ");
            int nouvelleSalle = int.Parse(Console.ReadLine());
            Console.Write("Entrez l'ID du nouveau patient : ");
            int nouveauPatient = int.Parse(Console.ReadLine());
            Console.Write("Entrez le numéro de l'ancienne salle : ");
            int ancienneSalle = int.Parse(Console.ReadLine());
            Console.Write("Entrez l'ID de l'ancien patient : ");
            int ancienPatient = int.Parse(Console.ReadLine());

            daoVisite.ChangePatientInSalle(nouvelleSalle, nouveauPatient, ancienneSalle, ancienPatient);
            Console.WriteLine("Changement de patient dans la salle effectué.");
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
            Salle s1 = new Salle(1);
            Salle s2 = new Salle(2);
            Hopital hopital = Hopital.Instance;
            hopital.AddSalle(s1);
            hopital.AddSalle(s2);
            Visite v1 = new Visite();
            Visite v2 = new Visite();

            Console.WriteLine("----------TEST1----------");
            AffecteSalle(v1, v2);
            Console.WriteLine("----------TEST2----------");
            s1.EstOuvert = false;
            AffecteSalle(v1, v2);
            Console.WriteLine("----------TEST3----------");
            hopital.DeleteSalle(s2);
            AffecteSalle(v1, v2);
        }

        private static void AffecteSalle(Visite v1, Visite v2)
        {
            v1.AffecteSalle(15);
            v2.AffecteSalle(13);
        }
    }
}

