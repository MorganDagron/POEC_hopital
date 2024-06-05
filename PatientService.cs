using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public class PatientService
    {
        private readonly DAOVisite _daoVisite;

        public PatientService()
        {
            _daoVisite = new DAOVisite();
        }

        // Méthode pour calculer le temps d'attente et mettre à jour la visite
        public void CalculerTempsAttente(int visiteId, DateTime heureArrivee, DateTime heurePassage)
        {
            TimeSpan tempsAttente = heurePassage - heureArrivee;
            _daoVisite.UpdateVisiteWithTempsAttente(visiteId, tempsAttente);
        }
    }
}
