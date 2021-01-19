using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Client:Personne
    {
        private DateTime premiereCommande;
        private long cumulCommandes;

        public Client() : base()
        {
            this.premiereCommande = default;
            this.cumulCommandes = 0;
        }

        public Client(string nom, string prenom, string adresse, long numero, DateTime premiereCommande):base(nom, prenom, adresse, numero)
        {
            this.premiereCommande = premiereCommande;
            this.cumulCommandes = 1;
        }

        public DateTime PremiereCommande
        {
            get { return this.premiereCommande; }
        }

        public long CumulCommandes
        {
            get { return this.cumulCommandes; }
            set { this.cumulCommandes = value; }
        }

        public override string ToString()
        {
            return base.ToString()+" "+this.premiereCommande.ToShortDateString()+" "+this.cumulCommandes.ToString();
        }

    }
}
