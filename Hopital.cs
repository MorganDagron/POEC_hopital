using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public class Hopital
    {
        private static Hopital _instance;
        private List<Salle> _salles;
        private List<Patient> _patients;
        private List<Visite> _visites;
        private List<Authentification> _authentifications;

        private Hopital()
        {
            _salles = new List<Salle>();
            _patients = new List<Patient>();
            _visites = new List<Visite>();
            _authentifications = new List<Authentification>();
        }

        public static Hopital Instance
        {
            get { return _instance ?? (_instance = new Hopital()); }
        }

        public List<Salle> GetSalles()
        {
            return _salles;
        }

        public List<Patient> GetPatients()
        {
            return _patients;
        }

        public List<Visite> GetVisites()
        {
            return _visites;
        }

        public List<Authentification> GetAuthentifications()
        {
            return _authentifications;
        }
    }
}
