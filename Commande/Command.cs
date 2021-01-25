using System;
using System.Collections.Generic;
using System.IO;

namespace PizzeriaMarsala
{
    public class Command : IToCSV
    {
        // attributs de la classe
        static long CommandIDMax { get; set; } //L'identifiant actuel qu'on incrément à chaque commande
        public long CommandID { get; set; }//L'identifiant de la commande

        public DateTime Date { get; private set; }

        public SortedList<Pizza, int> PizzaList { get; private set; } = new SortedList<Pizza, int>();
        public SortedList<Beverage, int> BeverageList { get; private set; } = new SortedList<Beverage, int>();
       
        public Client CommandCustomer { get; private set; }
        public Commis CommandWorker { get; private set; }
        public Livreur CommandDeliverer { get; private set; }
        
        public CommandState State { get; private set; }
        public BalanceState Balance { get; private set; }

        /*
         * Constructeurs de la classe
         */

        //Lors d'une saisie par le commis
        public Command(Client client, Commis commis, Livreur livreur)
        {
            CommandIDMax ++;
            CommandID = CommandIDMax;
            Date = DateTime.Now;
            CommandCustomer = client;
            CommandWorker = commis;
            CommandDeliverer = livreur;
            State = CommandState.enpreparation;
            Balance = BalanceState.enattente;
            CommandWorker.CommandesGerees += 1;
        }

        //Lors de la saisie depuis un fichier
        public Command(long command_id, DateTime date, long customer_phone_number, string worker_name, string deliverer_name, string solde)
            : this(Pizzeria.FindCustomer(customer_phone_number), Pizzeria.FindWorker(worker_name), Pizzeria.FindDeliverer(deliverer_name))
        {
            Date = date;
            CommandID = command_id;
            if (CommandIDMax < CommandID)
            {
                CommandIDMax = CommandID;
            }
            Balance = (BalanceState)Enum.Parse(typeof(BalanceState), solde);
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
                price += pair.Key.Price * pair.Value;
            }

            // on compte le prix des boissons
            foreach(KeyValuePair<Beverage, int> pair in BeverageList)
            {
                price += pair.Key.Price * pair.Value;
            }

            // on renvoie le prix
            return price;
        }

        /*
         * Fonctions qui changent l'état de la commande et des personnes associées
         */
        public void StartDelivery()
        {
            State = CommandState.enlivraison;
            //Le livreur assigné part en livraison
            CommandDeliverer.Etat = EtatLivreur.enlivraison;
        }
        public void PayementReceived()
        {
            State = CommandState.fermee;
            Balance = BalanceState.ok;
            //Le livreur assigné est sur place, il a effectué une livraison en plus
            CommandDeliverer.Etat = EtatLivreur.surplace;
            CommandDeliverer.CumulLivraisons++;
            //Cammande payée, le client augmente le cumul de ses commandes
            CommandCustomer.CumulCommandes += this.Price();
            //Commande fermée, le commis n'a plus à s'en occuper
            CommandWorker.CommandesGerees--; 
        }
        public void CommandLost()
        {
            State = CommandState.fermee;
            Balance = BalanceState.perdue;
            //Le livreur assigné est sur place, il a effectué une livraison en plus
            CommandDeliverer.Etat = EtatLivreur.surplace;
            CommandDeliverer.CumulLivraisons++;
            //Commande fermée, le commis n'a plus à s'en occuper
            CommandWorker.CommandesGerees--;
        }

        /*
         * Méthode permettant de savoir si une commande a été passée dans l'intervalle de temps donné
         */

        public bool MadeDuringTimeSpan(DateTime d1, DateTime d2)
        {
            bool test = false;
            if(Date.CompareTo(d1)>=0 && Date.CompareTo(d2) <= 0)
            {
                test = true;
            }
            return test;
        }

        /*
         * Conversion de la commande dans d'autres formats
         */
        public static Command FromCSV(string commande)
        {
            String[] infos = commande.Split(';');
            DateTime DateCommande = Convert.ToDateTime(infos[2]);
            DateCommande += new TimeSpan(int.Parse(infos[1].Substring(0, infos[1].Length - 1)), 0, 0);
            return new Command(long.Parse(infos[0]), DateCommande, long.Parse(infos[3]), infos[4], infos[5], infos[6]);
        }
        public string ToCSV()
        {
            return $"{CommandID};{Date.Hour}H;{Date.ToShortDateString()};{CommandCustomer.NumeroTel};{CommandWorker.Nom};{CommandDeliverer.Nom};{State};{Balance}";
        }


        /*
         *  Fonctions de tri des commandes
         */
        public static int CompareID(Command command_1, Command command_2)
        {
            return command_1.CommandID.CompareTo(command_2.CommandID);
        }
        public static int CompareUrgency(Command command_1, Command command_2)
        {
            return command_1.Date.CompareTo(command_2.Date);
        }
        public static int ComparePrices(Command command_1, Command command_2)
        {
            return command_1.Price().CompareTo(command_2.Price());
        }


        /*
         * Méthode pour enregistrer une Facture au format Txt ou CSV
         * On ne peut pas utiliser la méthode EnregistreDansFichierTXT/CSV de SortableObservableCollection:
            * Les méthodes ToCSV et ToString de Commande n'affichent pas les éléments d'une commande détaillée
         */

        #region Enregistrement Facture dans fichiers

        #region Méthodes ToString() et ToCSV() adaptées
        public string DetailCommandeToString()
        {
            string s = "N° Commande : " + CommandID.ToString();
            if (PizzaList != null && PizzaList.Count != 0)
            {
                foreach (KeyValuePair<Pizza, int> kv in PizzaList)
                {
                    s += "Pizza : " + kv.Key.ToString() + " (x" + kv.Value.ToString() + ")" + "\n";
                }
            }
            if (BeverageList != null && BeverageList.Count != 0)
            {
                foreach (KeyValuePair<Beverage, int> kv in BeverageList)
                {
                    s += kv.Key.Type.ToString() + " : " + $"({ kv.Key.Volume}cl) [{ kv.Key.Price}$]" + " (x" + kv.Key.ToString() + " (x" + kv.Value.ToString() + ")" + "\n";
                }
            }
            return s;
        }

        public string DetailCommandeToCSV()
        {
            string s = "";
            if (PizzaList != null && PizzaList.Count != 0)
            {
                foreach (KeyValuePair<Pizza, int> kv in PizzaList)
                {
                    s += this.CommandID.ToString() + ";Pizza" + kv.Key.Price.ToString() + ";" + kv.Key.Type.ToString() + ";" + kv.Key.Size.ToString() + ";;" + kv.Value.ToString() + "\n";
                }
            }
            if (BeverageList != null && BeverageList.Count != 0)
            {
                foreach (KeyValuePair<Beverage, int> kv in BeverageList)
                {
                    s += this.CommandID.ToString() + ";" + kv.Key.Type.ToString() + ";" + kv.Key.Price.ToString() + ";;;" + kv.Key.Volume.ToString() + ";" + kv.Value.ToString() + "\n";
                }
            }
            return s;
        }
        #endregion
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


    }
}
