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
        private List<Salle> salles;
        private int index;
        /*private List<Patient> Patients{ get; set; }
        private List<Visite> Visites{ get; set; }
        private List<Authentification> Authentifications;*/

        private Hopital()
        {
            salles = new List<Salle>();
            index = -1;
        }

        public static Hopital Instance
        {
            //get { return instance ?? (instance = new Hopital()); }
            get
            {
                if (instance == null)
                {
                    instance = new Hopital();
                }
                return instance;
            }
        }

        public void AddSalle(Salle salle)
        {
            salles.Add(salle);
        }

        public void DeleteSalle(Salle salle)
        {
            salles.Remove(salle);
        }

        private Salle GetNextSalle()
        {
            if (salles.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < salles.Count; i++)
            {
                index = (index + 1) % salles.Count;
                if (salles[index].EstOuvert)
                {
                    return salles[index];
                }
            }

            return null;
        }

        public int AffecteSalle(int IdPatient)
        {
            Salle salle = GetNextSalle();
            if (salle != null)
            {
                
                return salle.AffecteSalle(IdPatient); ;
            }
            Console.WriteLine("Aucune salle ouverte.");
            return 0;
        }

    }
}
