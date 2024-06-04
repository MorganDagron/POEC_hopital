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
        public int IdMedecin { get; set; }
        public string NomMedecin { get; set; }
        public int CoutVisite { get; set; }
        public DateTime DateVisite { get; set; }
        public int NumSalle { get; set; }

        public Visite()
        {

        }

        public Visite(int id, DateTime dateVisite, string nomMedecin, int idPatient, int idMedecin, int coutVisite)
        {
            Id = id;
            DateVisite = dateVisite;
            NomMedecin = nomMedecin;
            IdPatient = idPatient;
            IdMedecin = idMedecin;
            CoutVisite = coutVisite;
        }

        public override string ToString()
        {
            return $"Id: {Id}, IdPatient: {IdPatient}, NomMedecin: {NomMedecin}, CoutVisite: {CoutVisite}, DateVisite: {DateVisite}, NumSalle: {NumSalle}";
        }
    }
}
