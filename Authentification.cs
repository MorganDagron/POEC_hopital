using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public class Authentification
    {
        private string _login;
        private string _password;
        private string _nom;
        private int _metier;

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public int Metier
        {
            get { return _metier; }
            set { _metier = value; }
        }
    }
}


