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
        public TimeSpan TempsAttente { get; set; }
        public TimeSpan HeureArrivee { get; set; }
        public TimeSpan HeurePassage { get; set; }

        public Visite() { }

        public Visite(int id, DateTime dateVisite, string nomMedecin, int idPatient, int idMedecin, int coutVisite, int numSalle,TimeSpan tempsAttente, TimeSpan heureArrivee, TimeSpan heurePassage)
        {
            Id = id;
            DateVisite = dateVisite;
            NomMedecin = nomMedecin;
            IdPatient = idPatient;
            IdMedecin = idMedecin;
            CoutVisite = coutVisite;
            NumSalle = numSalle;
            TempsAttente = tempsAttente;
            HeureArrivee = heureArrivee;
            HeurePassage = heurePassage;
        }

        public override string ToString()
        {
            return $"Id: {Id}, IdPatient: {IdPatient}, NomMedecin: {NomMedecin}, CoutVisite: {CoutVisite}, DateVisite: {DateVisite}, NumSalle: {NumSalle}, TempsAttente: {TempsAttente}";
        }
    }
}
