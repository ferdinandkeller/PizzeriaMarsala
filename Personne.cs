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

        public string Nom
        {
            get { return this.nom; }
        }

        public string Prenom
        {
            get { return this.prenom; }
        }

        public string Adresse
        {
            get { return this.adresse; }
        }

        public long NumeroTel
        {
            get { return this.numeroTel; }
        }

        public override string ToString()
        {
            return this.nom+" "+this.prenom+" "+this.adresse+" "+this.numeroTel.ToString();
        }

        public bool Egalite(Personne p)
        {
            bool test = false;
            if(this.nom==p.Nom && this.prenom==p.Prenom && this.adresse == p.Adresse)
            {
                test = true;
            }
            return test;
        }
    }
}
