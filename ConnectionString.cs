using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _projet_hopital
{
    public static class ConnectionString
    {
        public static string ConnexionBdd { get; private set; }

        static ConnectionString()
        {
            ConnexionBdd = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=hopital_ajc;Integrated Security=True"; ;
        }
    }
}
