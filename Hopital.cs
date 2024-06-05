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
        private List<Patient> fileAttente;
        private List<Visite> visites;
        DAOAuthentification daoAuth = new DAOAuthentification();
        DAOVisite daoVis = new DAOVisite();

        private Hopital()
        {
            visites = new List<Visite>();
            fileAttente = new List<Patient>();
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

        internal Authentification VerificationLogin(string nom, string password)
        {
            Authentification personneTemp = new Authentification();

            if (daoAuth.GetAuthentification(nom, password) != null)
            {
                personneTemp = daoAuth.GetAuthentification(nom, password);
                Console.WriteLine("Identifiant ok");
            }
            else
            {
                Console.WriteLine("Identifiant non reconnu");
            }

            return personneTemp;
        }

        internal void AjoutPatientFileAttente()
        {
            bool AjoutPatientOK = true;
            Console.WriteLine("Entrez l'ID du patient");
            int idPatient = Convert.ToInt32(Console.ReadLine());
            DAOPatient dao = new DAOPatient();


            foreach (var patient in fileAttente)
            {
                if (patient.Id == idPatient)
                {
                    AjoutPatientOK = false;
                }
            }

            if (AjoutPatientOK)
            {
                if (dao.GetPatientById(idPatient) != null)
                {
                    fileAttente.Add(dao.GetPatientById(idPatient));
                    Console.WriteLine("Patient connu dans l'hopital, ajouté à la liste d'attente");
                    Console.WriteLine(dao.GetPatientById(idPatient).ToString());
                }

                else
                {
                    Console.WriteLine("Patient non connu dans l'hopital, veuillez renseigner ses informations personelles");
                    Console.WriteLine("Saisissez son nom");
                    string newNom = Console.ReadLine();

                    Console.WriteLine("Saisissez son prénom");
                    string newPrenom = Console.ReadLine();

                    Console.WriteLine("Saisissez son âge");
                    int newAge = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Saisissez son adresse");
                    string newAdresse = Console.ReadLine();

                    Console.WriteLine("Saisissez son n° de téléphone");
                    string newTel = Console.ReadLine();

                    Patient nouveauPatient = new Patient(idPatient, newNom, newPrenom, newAge, newAdresse, newTel);
                    dao.InsertPatient(nouveauPatient);
                    fileAttente.Add(nouveauPatient);
                    Console.WriteLine("Patient ajouté dans la base de donnée et dans la file d'attente");
                }
            }
            else
            {
                Console.WriteLine("Patient déja présent dans la file d'attente");
            }

        }

        internal void AfficherPatientsEnAttente()
        {
            int cpt = 1;
            Console.WriteLine("\n\nPatients présents dans la file d'attente\n");
            if (fileAttente != null)
            {
                foreach (var patient in fileAttente)
                {
                    Console.WriteLine("Position " + cpt);
                    Console.WriteLine(patient.ToString() + "\n");
                    cpt++;
                }
            }
        }

        internal void AfficherProchainPatient()
        {
            Console.WriteLine("Prochaine personne en consultation :");
            if (fileAttente[0] != null)
            {
                Console.WriteLine(fileAttente[0].ToString());
            }
        }

        internal void AfficherPatient(Authentification p)
        {
            if (fileAttente.Any())
            {

                Patient patient = fileAttente.First();
                Console.WriteLine($"le patient suivant est affecté à la salle n° {p.Metier}.");
                Console.WriteLine(patient.ToString());
                //Ajouté la patient à la la liste des visites
                visites.Add(new Visite(patient.Id, p.Nom, DateTime.Now, p.Metier));
                //Si la liste est supérieur ou égal à 5 alors envoyé en bdd automatiquement
                if (visites.Count >= 5)
                {
                    foreach (Visite v in visites)
                        daoVis.InsertVisite(v);
                    Console.WriteLine("il y avait au moins 5 visites non enregistré\nenregistrement dans la bdd effectué");
                    visites.Clear();
                }
                //supprimer patient de la file d'attente
                fileAttente.Remove(patient);
            }
            else
            {
                Console.WriteLine("il n'y a personne dans la file d'attente");
            }

        }

        internal void EnvoyerVisite()
        {
            foreach (Visite v in visites)
                daoVis.InsertVisite(v);
            Console.WriteLine("Les Visites ont été envoyé à la base de donnée");
            visites.Clear();
        }
    }
}
