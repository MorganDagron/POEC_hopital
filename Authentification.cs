using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public class Authentification
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nom { get; set; }
        public int Metier { get; set; }
        private Hopital hopital;

        public Authentification()
        {
            hopital = Hopital.Instance;
        }

        public Authentification(string login, string password, string nom, int metier)
        {
            Login = login;
            Password = password;
            Nom = nom;
            Metier = metier;
        }


        public override string ToString()
        {
            return $"Login: {Login}, Nom: {Nom}, Métier: {Metier}";
        }
    }
}
