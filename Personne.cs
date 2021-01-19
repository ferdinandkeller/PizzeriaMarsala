using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    abstract class Personne
    {
        protected string nom;
        protected string prenom;
        protected string adresse;
        protected long numeroTel;

        public Personne()
        {
            this.nom = null;
            this.prenom = null;
            this.adresse = null;
            this.numeroTel = 0;
        }

        public Personne(string nom, string prenom, string adresse, long numero)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.numeroTel = numeroTel;
        }

        //Propriétés
        //ToString?
    }
}
