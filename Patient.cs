using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public class Patient
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }

        public Patient()
        {
            
        }

        public Patient(int id, string nom, string prenom, int age, string adresse, string telephone)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Age = age;
            Adresse = adresse;
            Telephone = telephone;
        }

        public override string ToString()
        {
            return $"Patient : {Nom} {Prenom}, (identifiant n° {Id}), {Age} ans,\nAdresse: { Adresse} - Téléphone : {Telephone}";
        }
    }
}
