using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    abstract class Personne
    {
        protected string Nom { get; private set; }
        protected string Prenom { get; private set; }
        protected string Adresse { get; private set; }
        protected long NumeroTel { get; private set; }

        public Personne()
        {
            Nom = null;
            Prenom = null;
            Adresse = null;
            NumeroTel = 0;
        }

        public Personne(string nom, string prenom, string adresse, long numeroTel)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            NumeroTel = numeroTel;
        }

        public override string ToString()
        {
            return this.Nom + " " + this.Prenom + " " + this.Adresse + " " + this.NumeroTel;
        }

        public bool Egalite(Personne personne)
        {
            return Nom == personne.Nom && Prenom == personne.Prenom;
        }
    }
}
