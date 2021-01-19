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

        public Client() : base()
        {
            this.premiereCommande = default;
        }

        public Client(string nom, string prenom, string adresse, long numero, DateTime premiereCommande):base(nom, prenom, adresse, numero)
        {
            this.premiereCommande = premiereCommande;
        }

        //Propriétés
        //ToString?
    }
}
