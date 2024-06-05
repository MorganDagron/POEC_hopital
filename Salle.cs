using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public class Salle
    {
        public int Numero { get; set; }
        bool estOuvert = true;

        public Salle(int numero)
        {
            Numero = numero;
            estOuvert = true;
        }

        public bool EstOuvert { get => estOuvert; set => estOuvert = value; }

        public int AffecteSalle(int IdPatient)
        {
            if (estOuvert)
            {
                Console.WriteLine($"le patient suivant est affecté à la salle n° {Numero}.");
            }
            else
            {
                Console.WriteLine($"Salle n° {Numero} est fermé");

            }
            return Numero;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
