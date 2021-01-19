using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Pizza
    {
        private string type;
        private string taille; //petite, moyenne, grande

        public Pizza()
        {
            this.type = null;
            this.taille = null;
        }

        public Pizza(string type, string taille)
        {
            this.type = type;
            this.taille = taille;
        }

        public string Type
        {
            get { return this.type; }
        }

        public string Taille
        {
            get { return this.taille; }
        }

        //Propriétés
        //ToString?
        //Methode de calcul de prix (taille & prix)
    }
}
