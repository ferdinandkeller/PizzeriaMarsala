using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Client : Personne
    {
        public long IDClient { get; private set; }
        public DateTime PremiereCommande { get; private set; }
        public long CumulCommandes { get; private set; }

        public Client(string nom, string prenom, string adresse, long numero, DateTime premiere_commande)
            : this(GenerateurIdentifiant.CreerIdentifiantAleatoire(), nom, prenom, adresse, numero, premiere_commande, 0)
        {
            // rien
        }

        public Client(long id_client, string nom, string prenom, string adresse, long numero, DateTime premiere_commande, long cumul_commandes)
            : base(nom, prenom, adresse, numero)
        {
            IDClient = id_client;
            PremiereCommande = premiere_commande;
            CumulCommandes = cumul_commandes;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nPremière commande : {PremiereCommande.ToShortDateString()} | Cumul des commandes : {CumulCommandes}";
        }

        public override string ToCSV()
        {
            return $"{IDClient};" + base.ToCSV() + $";{PremiereCommande.ToShortDateString()};{CumulCommandes}";
        }

        public static Client CSVToClient(String client)
        {
            String[] infos = client.Split(';');
            DateTime DatePremiereCommande = infos.Length == 5 ? DateTime.Now : Convert.ToDateTime(infos[5]);
            long cumul_commandes = infos.LongLength == 6 ? 0 : long.Parse(infos[6]);
            return new Client(long.Parse(infos[0]), infos[1], infos[2], infos[3], long.Parse(infos[4]), DatePremiereCommande, cumul_commandes);
        }

        public static int ComparePrixCumule(Client p, Client q)
        {
            return p.CumulCommandes.CompareTo(q.CumulCommandes);
        }
        
    }
}
