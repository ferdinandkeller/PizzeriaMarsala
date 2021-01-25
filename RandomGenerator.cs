using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class RandomGenerator
    {
        public static long CreateID()
        {
            Random rng = new Random();
            return ((long)rng.Next() << 32) | (long)rng.Next();
        }
    }
}
