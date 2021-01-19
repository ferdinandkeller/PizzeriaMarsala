using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Pizza : IPrix
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

        public override string ToString()
        {
            return this.type+" "+this.taille;
        }

        public static string ToMin(string s)
        {
            return s.ToLower();
        }

        public double Prix()
        {
            double p = 0;
            List<string> types = new List<string> { "sauce taumate/fromage", "vegetarienne", "chorizo", "haiwaienne", "4fromages", "margherita", "regina", "napolitaine", "toutes garnies", "completes" };
            List<string> tailles = new List<string> {" petite"," moyenne", "grande" };
            if(this.type!=null && this.taille != null)
            {
                p = 15.0 + types.IndexOf(ToMin(this.type)) * 0.2 + tailles.IndexOf(ToMin(this.taille)) * 1.7;
            }
            return p;
        }
    }
}
