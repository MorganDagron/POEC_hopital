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
                    AffichageMenuPrincipal();
                } else
                {
                    Console.WriteLine("Identifiants non reconnus");
                    Console.WriteLine("Veuillez réessayer");
                }

            }
        }

        static void AffichageMenuPrincipal()
        {

        }
    }
}
