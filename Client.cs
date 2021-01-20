using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Client : Personne
    {
        public DateTime PremiereCommande { get; private set; }
        public long CumulCommandes { get; private set; }

        public Client(string nom, string prenom, string adresse, long numero, DateTime premiere_commande, long cumul_commandes)
            : base(nom, prenom, adresse, numero)
        {
            PremiereCommande = premiere_commande;
            CumulCommandes = cumul_commandes;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nPremière commande : {PremiereCommande} | Cumul des commandes : {CumulCommandes}";
        }

    }
}
