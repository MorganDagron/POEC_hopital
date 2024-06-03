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
            TestCoBDD();
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

                if (true)
                {
                    accesAccorde = true;
                    //AffichageMenuPrincipal();
                } else
                {
                    Console.WriteLine("Identifiants non reconnus");
                    Console.WriteLine("Veuillez réessayer");
                }

            }
        }

        //static void AffichageMenuPrincipal()
        //{
        //    Console.WriteLine($"Bonjour {metier} {nom}");
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
            foreach(Authentification a in all )
                Console.WriteLine(a.ToString());
        }
    }
}
