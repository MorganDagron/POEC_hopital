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

        public Salle(int numero)
        {
            Numero = numero;
        }

        public override string ToString()
        {
            return $"Salle {Numero}";
        }
    }
}
