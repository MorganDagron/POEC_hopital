using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    class GestionFileAttente
    {
        List<Patient> fileAttente = new List<Patient>();
        //List<Patient> listePatientEnAttente = CreationPatients();

        public List<Patient> FileAttente {
            get;
            set;  
        }

        // On crée une liste de patient que l'ont va faire passer au fur et à mesure dans la liste d'attente
        //public static List<Patient> CreationPatients()
        //{
        //    Console.WriteLine("creation des patients attente ok");
        //    List<Patient> listePatient = new List<Patient>();

        //    listePatient.Add(new Patient(2, "MARTIN", "Marie", 25, "45 avenue de la République", "07 89 01 23 45"));
        //    listePatient.Add(new Patient(4, "ROUSSEAU", "François", 35, "90 avenue de la République", "07 12 34 56 78"));
        //    listePatient.Add(new Patient(0, "DUPONT", "Pierre", 30, "12 rue de la Paix", "06 12 34 56 78"));
        //    listePatient.Add(new Patient(0, "LEFEBVRE", "Jean", 40, "78 rue de la Liberté", "05 67 89 01 23"));
        //    listePatient.Add(new Patient(0, "DURAND", "Sophie", 28, "34 rue de la Paix", "06 78 90 12 34"));

        //    return listePatient;
        //}

        //public static void AjoutPatientFileAttente()
        //{
        //    List<Patient> listePatient = new List<Patient>();
        //    listePatient.Add(new Patient(2, "MARTIN", "Marie", 25, "45 avenue de la République", "07 89 01 23 45"));
        //    Console.WriteLine(listePatient[0].ToString());

        //    FileAttente.Add(new Patient(2, "MARTIN", "Marie", 25, "45 avenue de la République", "07 89 01 23 45"));
        //    Console.WriteLine(FileAttente[0].ToString());
        //    Console.WriteLine("Entrez l'ID du patient");
        //    int idPatient = Convert.ToInt32(Console.ReadLine());

        //    DAOPatient dao = new DAOPatient();
        //    if (dao.GetPatientById(idPatient)  != null)
        //    {
        //        FileAttente.Add(dao.GetPatientById(idPatient));
        //        Console.WriteLine("Patient connu dans l'hopital, ajouté à la liste d'attente");
        //        Console.WriteLine(dao.GetPatientById(idPatient).ToString());
        //    }

        //    else
        //    {
        //        Console.WriteLine("Attribuez un ID au nouveau patient");
        //        int newID = Convert.ToInt32(Console.ReadLine());
        //        Patient nouveauPatient = patient;
        //        nouveauPatient.Id = newID;
        //        dao.InsertPatient(nouveauPatient);
        //    }

        //}
    }
}
