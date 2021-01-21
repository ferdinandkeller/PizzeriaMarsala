using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Pizza : Produit
    {
        public TypePizza Type { get; private set; }
        private TaillePizza Taille;

        public Pizza(TypePizza type, TaillePizza taille)
        {
            Type = type;
            Taille = taille;

            Prix = (double)Type/100 * (double)Taille/100;
        }

        public override string ToString()
        {
            return $"{Type} ({Taille}) [{Prix}$]";
        }
    }
}
