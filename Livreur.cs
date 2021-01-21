using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Livreur: Personne
    {
        private string etat; //"surplace", "enconges" ou "enlivraison"
        private string typeVehicule;

        public Livreur() : base()
        {
            this.etat=null;
            this.typeVehicule = null;
        }

        public Livreur(string nom, string prenom, string adresse, long numero,string etat, string typeVehicule) : base(nom, prenom, adresse, numero)
        {
            this.etat = etat;
            this.typeVehicule = typeVehicule;
        }

        public string Etat
        {
            get { return this.etat; }
            set { this.etat = value; }
        }

        public string TypeVehicule
        {
            get { return this.typeVehicule; }
        }

        public override string ToString()
        {
            return base.ToString()+" ; " +this.etat+" ; "+this.typeVehicule;
        }

        public static Livreur CreationLivreurDepuisString(string[] s)
        {
            Livreur l = new Livreur(s[0], s[1], s[2], long.Parse(s[3]), s[4],s[5]);
            return l;
        }
    }
}
