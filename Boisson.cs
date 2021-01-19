using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Boisson
    {
        private string nomBoisson;
        private double volume; //En cL

        public Boisson()
        {
            this.nomBoisson = null;
            this.volume = 0;
        }

        public Boisson(string nomBoisson, double volume)
        {
            this.nomBoisson = nomBoisson;
            this.volume = volume;
        }

        //Propriétés
        //ToString?
        //Ajout de méthode calcul de prix
    }
}
