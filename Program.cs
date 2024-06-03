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
            TestAffectationSalle1Puis2();
        }

        static void AffichageLogin()
        {
            bool accesAccorde = false;



            while (accesAccorde == false)
            {
                Console.WriteLine("Identifiez-vous pour accéder au service");
                Console.WriteLine("Nom :");
                string nomLogin = Console.ReadLine();
                Console.WriteLine("Password :");
                string passwordLogin = Console.ReadLine();
                VerificationLogin(nomLogin, passwordLogin);

                //Authentification personne = new Authentification(nomLogin, passwordLogin, )

                if (true)
                {
                    accesAccorde = true;
                    Console.WriteLine("Login OK");
                    //AffichageMenuPrincipal();
                }
                else
                {
                    Console.WriteLine("Identifiants non reconnus");
                    Console.WriteLine("Veuillez réessayer");
                }

            }
        }

        //static void AffichageMenuPrincipal()
        //{
        //    Console.WriteLine($"Bonjour {nomLogin.Metier} {nom}");
        //    if ()
        //    {
        //        Console.WriteLine("Interface {metier}");
        //        Console.WriteLine("1. Accueillir un nouveau patient");
        //        Console.WriteLine("2. Afficher la file d'attente");
        //        Console.WriteLine("3. Sauvegarder la liste des patients du jour");
        //        Console.WriteLine("4. Déconnexion");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Interface {metier} - Choix de la section via n° correpondant");
        //        Console.WriteLine("1. Ajouter un patient à la file d'attente");
        //        Console.WriteLine("2. Afficher la file d'attente");
        //        Console.WriteLine("3. Afficher le prochain patient de la file");
        //        Console.WriteLine("4. Déconnexion");
        //    }
        //}

        private static void TestCoBDD()
        {
            DAOAuthentification test = new DAOAuthentification();
            List<Authentification> all = test.GetAllAuthentifications();
            foreach (Authentification a in all)
                Console.WriteLine(a.ToString());
        }

        static void VerificationLogin(string nom, string password)
        {
            DAOAuthentification dao = new DAOAuthentification();
            if (dao.GetAuthentification(nom, password) != null)
            {
                Console.WriteLine("Identifiant ok");
            }
            else
            {
                Console.WriteLine("Identifiant non reconnu");
            }
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

