using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public class Visite
    {
        public int Id { get; set; }
        public int IdPatient { get; set; }
        public string NomMedecin { get; set; }
        public int CoutVisite { get; set; }
        public DateTime DateVisite { get; set; }
        public int NumSalle { get; set; }
        private Hopital hopital;

        public Visite()
        {
            hopital = Hopital.Instance;
        }

        public void AffecteSalle(int IdPatient)
        {
            hopital.AffecteSalle(IdPatient);
        }

        public Visite(int idPatient, string nomMedecin, DateTime dateVisite, int numSalle)
        {
            IdPatient = idPatient;
            NomMedecin = nomMedecin;
            DateVisite = dateVisite;
            NumSalle = numSalle;
        }

        public override string ToString()
        {
            return $"Id: {Id}, IdPatient: {IdPatient}, NomMedecin: {NomMedecin}, CoutVisite: {CoutVisite}, DateVisite: {DateVisite}, NumSalle: {NumSalle}";
        }
    }
}
