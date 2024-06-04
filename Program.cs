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
            TestAffectationSalle1Puis2();
        }

        static void AffichageLogin()
        {
            Authentification personne = new Authentification();
            bool accesAccorde = false;

            while (accesAccorde == false)
            {
                Console.WriteLine("Identifiez-vous pour accéder au service");
                Console.WriteLine("Identifiant :");
                string nomLogin = Console.ReadLine();
                Console.WriteLine("Mot de passe :");
                string passwordLogin = Console.ReadLine();
                personne = VerificationLogin(nomLogin, passwordLogin);

                if (personne.Login !=null)
                {
                    accesAccorde = true;
                    AffichageMenuPrincipal(personne);
                }
                else
                {
                    Console.WriteLine("Appuyer sur n'importe quelle touche pour réessayer de vous connecter ou 'Q'' pour quitter");
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
            Console.WriteLine($"Bonjour {P.Metier} {P.Nom}");
            if (P.Metier == 0)
            {
                Console.WriteLine($"Interface {P.Metier} - Tapez le n° correpondant à votre choix");
                Console.WriteLine("1. Ajouter un patient à la file d'attente");
                Console.WriteLine("2. Afficher la file d'attente");
                Console.WriteLine("3. Afficher le prochain patient de la file");
                Console.WriteLine("4. Déconnexion");
                int choixMenu = Convert.ToInt32(Console.ReadLine());

                switch (choixMenu)
                {
                    case 1:
                        AjoutPatientFileAttente(P);
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Veuillez selectionner un n° présent dans la liste");
                        break;
                }
            }
            else if (P.Metier >= 1)
            {
                Console.WriteLine($"Interface {P.Metier} - Tapez le n° correpondant à votre choix");
                Console.WriteLine("1. Accueillir un nouveau patient");
                Console.WriteLine("2. Afficher la file d'attente");
                Console.WriteLine("3. Sauvegarder la liste des patients du jour");
                Console.WriteLine("4. Déconnexion");
            } else
            {
                Console.WriteLine("Menu à venir");
            }
        }

        private static void TestCoBDD()
        {
            DAOAuthentification test = new DAOAuthentification();
            List<Authentification> all = test.GetAllAuthentifications();
            foreach (Authentification a in all)
                Console.WriteLine(a.ToString());
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

        static void AjoutPatientFileAttente(Authentification P)
        {

        }

    }
}

