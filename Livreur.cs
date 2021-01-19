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

        //Propriétés
        //ToString?

    }
}
