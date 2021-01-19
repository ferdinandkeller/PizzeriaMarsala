using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Boisson: IPrix
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

        public string NomBoisson
        {
            get { return this.nomBoisson; }
        }

        public double Volume
        {
            get { return this.volume; }
        }

        public override string ToString()
        {
            return this.nomBoisson+" "+this.volume;
        }
        public static string ToMin(string s)
        {
            return s.ToLower();
        }
        public double Prix()
        {
            double p = 0;
            if(ToMin(this.nomBoisson)=="coca")
            {
                p = 0.06 * this.volume;
            }
            if (ToMin(this.nomBoisson) == "jus d'orange")
            {
                p = 0.02 * this.volume;
            }
            if (ToMin(this.nomBoisson) == "eau")
            {
                p = 0.01 * this.volume;
            }
            if (ToMin(this.nomBoisson) == "perrier")
            {
                p = 0.03 * this.volume;
            }
            if (ToMin(this.nomBoisson) == "soda")
            {
                p = 0.05 * this.volume;
            }
            if (ToMin(this.nomBoisson) == "jus")
            {
                p = 0.025 * this.volume;
            }
            else
            {
                p = 0.3 * this.volume;
            }
            return p;
        }

    }
}
