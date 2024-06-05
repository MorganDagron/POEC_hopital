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
            Hopital hopital = Hopital.Instance;
            bool accesAccorde = false;

            while (!accesAccorde)
            {
                Console.WriteLine("Identifiez-vous pour accéder au service");
                Console.WriteLine("Identifiant :");
                string nomLogin = Console.ReadLine();
                Console.WriteLine("Mot de passe :");
                string passwordLogin = Console.ReadLine();
                personne = hopital.VerificationLogin(nomLogin, passwordLogin);

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
            if (P.Metier == 0)
            {
                Console.WriteLine($"Bonjour Secrétaire {P.Nom}");
                choixMenuSecretaire();
            }
            else if (P.Metier >= 1)
            {
                Console.WriteLine($"Bonjour Médecin {P.Nom}");
                choixMenuMedecin(P);
            }
            else
            {
                Console.WriteLine($"Bonjour Admin {P.Nom}");
                Console.WriteLine("Menu à venir");
            }
        }


        public static void choixMenuSecretaire()
        {
            Hopital hopital = Hopital.Instance;
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
                        hopital.AjoutPatientFileAttente();
                        break;
                    case 2:
                        hopital.AfficherPatientsEnAttente();
                        break;
                    case 3:
                        hopital.AfficherProchainPatient();
                        break;
                    case 4:
                        quitter = true;
                        AffichageLogin();
                        break;
                    default:
                        Console.WriteLine("Veuillez selectionner un n° présent dans la liste");
                        break;
                }
            }
        }


        public static void choixMenuMedecin(Authentification P)
        {
            Hopital hopital = Hopital.Instance;
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
                        hopital.AfficherPatient(P);
                        break;
                    case 2:
                        hopital.AfficherPatientsEnAttente();
                        break;
                    case 3:
                        hopital.EnvoyerVisite();                        break;
                    case 4:
                        quitter = true;
                        AffichageLogin();
                        break;
                    default:
                        Console.WriteLine("Veuillez selectionner un n° présent dans la liste");
                        break;
                }

            }


        }

    }
}

