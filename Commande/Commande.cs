using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Commande : IToCSV
    {
        // attributs de la classe
        static long CommandIDMax { get; set; } //L'identifiant actuel qu'on incrément à chaque commande
        public long CommandID { get; set; }//L'identifiant de la commande

        public DateTime Date { get; private set; }

        public SortedList<Pizza, int> PizzaList { get; private set; } = new SortedList<Pizza, int>();
        public SortedList<Boisson, int> DrinkList { get; private set; } = new SortedList<Boisson, int>();
       
        public Client CommandCustomer { get; private set; }
        public Commis CommandWorker { get; private set; }
        public Livreur CommandDeliverer { get; private set; }
        
        public EtatCommande Etat { get; private set; }
        public EtatSolde Solde { get; private set; }

        /*
         * Constructeurs de la classe
         */

        //Lors d'une saisie par le commis
        public Commande(Client client, Commis commis, Livreur livreur)
        {
            CommandIDMax ++;
            CommandID = CommandIDMax;
            Date = DateTime.Now;
            CommandCustomer = client;
            CommandWorker = commis;
            CommandDeliverer = livreur;
            Etat = EtatCommande.enpreparation;
            Solde = EtatSolde.enattente;
            CommandWorker.CommandesGerees += 1;
        }

        //Lors de la saisie depuis un fichier
        public Commande(long id_commande, DateTime date, long customer_phone_number, string worker_name, string deliverer_name, string solde)
            : this(Pizzeria.FindCustomer(customer_phone_number), Pizzeria.FindWorker(worker_name), Pizzeria.FindDeliverer(deliverer_name))
        {
            Date = date;
            CommandID = id_commande;
            if (CommandIDMax < CommandID)
            {
                CommandIDMax = CommandID;
            }
            Solde = (EtatSolde)Enum.Parse(typeof(EtatSolde), solde);
        }


        /*
         * Fonction qui renvoie le prix de la commande
         */
        public double Price()
        {
            double price = 0;

            // on compte le prix des pizzas
            foreach(KeyValuePair<Pizza, int> pair in PizzaList)
            {
                price += pair.Key.Prix * pair.Value;
            }

            // on compte le prix des boissons
            foreach(KeyValuePair<Boisson, int> pair in DrinkList)
            {
                price += pair.Key.Prix * pair.Value;
            }

            // on renvoie le prix
            return price;
        }

        /*
         * Fonctions qui changent l'état de la commande et des personnes associées
         */
        public void DepartLivraison()
        {
            Etat = EtatCommande.enlivraison;
            //Le livreur assigné part en livraison
            CommandDeliverer.Etat = EtatLivreur.enlivraison;
        }
        public void PaiementRecu()
        {
            Etat = EtatCommande.fermee;
            Solde = EtatSolde.ok;
            //Le livreur assigné est sur place, il a effectué une livraison en plus
            CommandDeliverer.Etat = EtatLivreur.surplace;
            CommandDeliverer.CumulLivraisons++;
            //Cammande payée, le client augmente le cumul de ses commandes
            CommandCustomer.CumulCommandes += this.Price();
            //Commande fermée, le commis n'a plus à s'en occuper
            CommandWorker.CommandesGerees--; 
        }
        public void PerteCommande()
        {
            Etat = EtatCommande.fermee;
            Solde = EtatSolde.perdue;
            //Le livreur assigné est sur place, il a effectué une livraison en plus
            CommandDeliverer.Etat = EtatLivreur.surplace;
            CommandDeliverer.CumulLivraisons++;
            //Commande fermée, le commis n'a plus à s'en occuper
            CommandWorker.CommandesGerees--;
        }

        /*
         * Conversion de la commande dans d'autres formats
         */
        public static Commande FromCSV(string commande)
        {
            String[] infos = commande.Split(';');
            DateTime DateCommande = Convert.ToDateTime(infos[2]);
            DateCommande += new TimeSpan(int.Parse(infos[1].Substring(0, infos[1].Length - 1)), 0, 0);
            return new Commande(long.Parse(infos[0]), DateCommande, long.Parse(infos[3]), infos[4], infos[5], infos[6]);
        }
        public string ToCSV()
        {
            return $"{CommandID};{Date.Hour}H;{Date.ToShortDateString()};{CommandCustomer.NumeroTel};{CommandWorker.Nom};{CommandDeliverer.Nom};{Etat};{Solde}";
        }


        /*
         *  Fonctions de tri des commandes
         */
        public static int CompareID(Commande command_1, Commande command_2)
        {
            return command_1.CommandID.CompareTo(command_2.CommandID);
        }
        public static int CompareUrgency(Commande command_1, Commande command_2)
        {
            return command_1.Date.CompareTo(command_2.Date);
        }
        public static int ComparePrices(Commande command_1, Commande command_2)
        {
            return command_1.Price().CompareTo(command_2.Price());
        }

        /*
        public string DetailCommandeToString()
        {
            string s = "N° Commande : "+ IDCommande.ToString();
            if(PizzasCommande!=null && PizzasCommande.Count != 0)
            {
                foreach (KeyValuePair < Pizza,int> kv in PizzasCommande)
                {
                    s += "Pizza : " + kv.Key.ToString() + " (x" + kv.Value.ToString() + ")" + "\n";
                }
            }
            if (BoissonsCommande != null && BoissonsCommande.Count != 0)
            {
                foreach (KeyValuePair<Boisson, int> kv in BoissonsCommande)
                {
                    s += kv.Key.Type.ToString()+" : " + $"({ kv.Key.Volume}cl) [{ kv.Key.Prix}$]"+ " (x" + kv.Key.ToString() + " (x" + kv.Value.ToString() + ")" + "\n";
                }
            }
            return s;
        }

        public string DetailCommandeToCSV()
        {
            string s = "";
            if (PizzasCommande != null && PizzasCommande.Count != 0)
            {
                foreach (KeyValuePair<Pizza, int> kv in PizzasCommande)
                {
                    s += this.IDCommande.ToString()+";Pizza"+kv.Key.Prix.ToString()+";" + kv.Key.Type.ToString()+";"+kv.Key.Taille.ToString()+";;"+kv.Value.ToString() + "\n";
                }
            }
            if (BoissonsCommande != null && BoissonsCommande.Count != 0)
            {
                foreach (KeyValuePair<Boisson, int> kv in BoissonsCommande)
                {
                    s += this.IDCommande.ToString() + ";"+kv.Key.Type.ToString()+";" + kv.Key.Prix.ToString() + ";;;" + kv.Key.Volume.ToString() + ";" + kv.Value.ToString() + "\n";
                }
            }
            return s;
        }
        #endregion
        */

        /*
        #region Enregistrement Facture dans fichiers
        public void EnregistreFactureTXT(string nomFichier)
        {
            StreamWriter sw = new StreamWriter(nomFichier);
            sw.WriteLine(DetailCommandeToString());
            sw.Close();
        }

        public void EnregistreFactureCSV(string nomFichier)
        {
            StreamWriter sw = new StreamWriter(nomFichier);
            sw.WriteLine(DetailCommandeToCSV());
            sw.Close();
        }

        #endregion

        public static Commande RechercheCommandeParID(List<Commande> liste, long id)
        {
            return liste.Find(x => x.IDCommande == id);
        }
        */

    }
}
