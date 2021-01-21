using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class GenerateurIdentifiant
    {
        public static long CreerIdentifiantAleatoire()
        {
            Random rng = new Random();
            return ((long)rng.Next() << 32) | (long)rng.Next();
        }
    }
}
