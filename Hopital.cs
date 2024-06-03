using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public class Hopital
    {
        private static Hopital instance;
        private List<Salle> Salles{ get; set; }
        private List<Patient> Patients{ get; set; }
        private List<Visite> Visites{ get; set; }
        private List<Authentification> Authentifications;

        private Hopital()
        {
            Salles = new List<Salle>();
            Patients = new List<Patient>();
            Visites = new List<Visite>();
            Authentifications = new List<Authentification>();
        }

        public static Hopital Instance
        {
            get { return instance ?? (instance = new Hopital()); }
        }

    }
}
