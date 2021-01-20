using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Boisson : Produit
    {
        public TypeBoisson Type { get; private set; }
        public double Volume { get; private set; } // volume en cl

        public Boisson(TypeBoisson type, double volume)
        {
            Type = type;
            Volume = volume;

            Prix = (double)Type / 1000 * Volume; // prix en euros
        }

        public override string ToString()
        {
            return $"{Type} ({Volume}cl) [{Prix}$]";
        }

    }
}
