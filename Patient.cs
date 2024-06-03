using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    class Patient
    {
        private int id;
        private string prenom;
        private string nom; 
        private int age;
        private string adresse;
        private int telephone;

        public Patient() { }


        public override string ToString()
        {
            string res = $"Patient : {nom} {prenom} (identifiant n° {id})";
            res = $"\nAdresse : {adresse} - Téléphone : {telephone}" ;
            return "Patient :";
        }
    }
}
