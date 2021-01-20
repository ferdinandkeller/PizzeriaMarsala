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
        public TypeBoisson Nom { get; private set; }
        public double Volume { get; private set; } // volume en cl

        public Boisson(TypeBoisson nom_boisson, double volume)
        {
            Nom = nom_boisson;
            Volume = volume;

            Prix = (double)Nom / 1000 * Volume; // prix en euros
        }

        public override string ToString()
        {
            return $"{Nom} ({Volume}cl) [{Prix}$]";
        }

    }
}
