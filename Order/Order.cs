using System;
using System.Collections.Generic;
using System.IO;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Classe correspondant à une commande
    /// Elle hérite de l'interface IToCSV, elle implémente la méthode ToCSV() la mettant au format CSV
    /// </summary>
    /// <attributs>
    /// CommandIDMax: L'identifiant actuel des commandes, on l'incrémente à chaque commande (static)
    /// CommandID: identifiant de la commande
    /// Date: date et heure de la commande
    /// PizzaList: liste des pizzas de la commande 
    ///     ->SortedList : les clefs sont des pizzas et les valeurs sont les multiplicités de chaque pizza
    /// BeverageList: liste des boissons de la commande
    ///     ->SortedList : les clefs sont des boissons et les valeurs sont les multiplicités de chaque boisson
    /// CommandCustomer: le client ayant passé la commande
    /// CommandWorker: le commis ayant enregistré la commande
    /// CommandDeliverer: livreur de la commande
    /// State: état de la commande (enpreparation,enlivraison,fermee)
    /// Balance: état du solde de la commande (enattente,ok,perdue)
    /// </attributs>
    public class Order : IToCSV
    {
        #region Attributs
        static long OrderIDMax { get; set; }
        public long OrderID { get; set; } 

        public DateTime Date { get; private set; }

        public SortedList<Pizza, int> PizzaList { get; private set; } = new SortedList<Pizza, int>();
        public SortedList<Beverage, int> BeverageList { get; private set; } = new SortedList<Beverage, int>();
       
        public Customer CommandCustomer { get; private set; }
        public Worker CommandWorker { get; private set; }
        public Deliverer CommandDeliverer { get; private set; }
        
        public OrderState State { get; private set; }
        public BalanceState Balance { get; private set; }
        #endregion

        #region Constructeurs

        /// <summary>
        /// Lors d'une saisie par le commis
        /// Sans le détail de la commande
        /// </summary>
        /// <param name="client">Client passant la commande</param>
        /// <param name="commis">Commis enregistrant la commande</param>
        /// <param name="livreur">Livreur chargé de la livraison</param>
        public Order(Customer client, Worker commis, Deliverer livreur)
        {
            OrderIDMax ++; //il y a une nouvelle commande pour la pizzeria
            OrderID = OrderIDMax; //identifiant de la commande
            Date = DateTime.Now; //la commande vient d'être passée
            CommandCustomer = client;
            CommandWorker = commis;
            CommandDeliverer = livreur;
            State = OrderState.enpreparation; //automatiquement, la commande est passée en cuisie
            Balance = BalanceState.enattente; //la commande n'est pas encore payée
            CommandWorker.ManagedCommandNumber += 1; //Le commis a géré une commande de plus
        }

        /// <summary>
        /// Lors de la saisie depuis un fichier
        /// Sans le détail de la commande
        /// On appelle le premier constructeur
        ///     FindPerson(clé de recherche) : méthode définie dans la classe Pizzeria permettant d'associer une personne en fonction d'une clé de recherche
        /// </summary>
        /// <param name="command_id">Identifiant de la commande</param>
        /// <param name="date">Date et heure de commande</param>
        /// <param name="customer_phone_number">Numéro de téléphone du client</param>
        /// <param name="worker_name">Nom du commis</param>
        /// <param name="deliverer_name">Nom du livreur</param>
        /// <param name="solde">Etat du solde</param>
        public Order(long command_id, DateTime date, long customer_phone_number, string worker_name, string deliverer_name, string solde)
            : this(Pizzeria.FindCustomer(customer_phone_number), Pizzeria.FindWorker(worker_name), Pizzeria.FindDeliverer(deliverer_name))
        {
            Date = date;
            OrderID = command_id;
            if (OrderIDMax < OrderID)//Si l'identifiant max des commandes de la pizzeria est inférieur à celui donné dans le fichier
            {
                OrderIDMax = OrderID; //On actualise l'identifiant max
            }
            Balance = (BalanceState)Enum.Parse(typeof(BalanceState), solde); //on récupère le solde donné dans le fichier en effectuant une conversion
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// Prix d'une commande
        /// </summary>
        /// <returns>
        /// prix de la commande (0 si la commande est vide)
        /// </returns>
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

        /// <summary>
        /// Lorsqu'une commande part en livraison
        ///     Etat de la commande: enpreparation->enlivraison
        ///     Etat du livreur: surplace->enlivraison
        /// </summary>
        public void StartDelivery()
        {
            State = OrderState.enlivraison;
            //Le livreur assigné part en livraison
            CommandDeliverer.State = DelivererState.enlivraison;
        }

        /// <summary>
        /// Lorsqu'un paiement est reçu
        ///     Etat de la commande: enlivraison->fermee
        ///     Etat du solde: ok
        ///     Etat du livreur: enlivraison->surplace
        ///     Le nombre de commandes effectuées par le livreure augmente de 1
        ///     Le cumul des commandes du client augmente du prix payé
        /// </summary>
        public void PayementReceived()
        {
            State = OrderState.fermee;
            Balance = BalanceState.ok;
            //Le livreur assigné est sur place, il a effectué une livraison en plus
            CommandDeliverer.State = DelivererState.surplace;
            CommandDeliverer.ManagedDeliveryNumber++;
            //Cammande payée, le client augmente le cumul de ses commandes
            CommandCustomer.OrdersTotalValue += this.Price();
        }

        /// <summary>
        /// Lorsque la commande est perdue
        ///     Etat de la commande: enlivraison->fermee
        ///     Solde: enattente->perdue
        ///     Etat du livreur: enlivraison->surplace
        ///     Le cumul des livraisons du livreur augment de 1
        ///     Le client n'augmente pas le cumul de ses commandes
        /// </summary>
        public void CommandLost()
        {
            State = OrderState.fermee;
            Balance = BalanceState.perdue;
            //Le livreur assigné est sur place, il a effectué une livraison en plus
            CommandDeliverer.State = DelivererState.surplace;
            CommandDeliverer.ManagedDeliveryNumber++;
        }

        



        /// <summary>
        /// Méthode permettant de savoir si une commande a été passée dans l'intervalle de temps donné
        /// </summary>
        /// <param name="d1">Borne inférieure de l'intervalle</param>
        /// <param name="d2">Borne supérieure de l'intervalle</param>
        /// <returns>
        /// True si la commande a été effectuée sur l'intervalle donné
        /// False sinon
        /// </returns>
        public bool MadeDuringTimeSpan(DateTime d1, DateTime d2)
        {
            bool test = false;
            if(Date.CompareTo(d1)>=0 && Date.CompareTo(d2) <= 0)
            {
                test = true;
            }
            return test;
        }

        
        /// <summary>
        /// Conversion d'une ligne de fichier CSV en une commande
        /// </summary>
        /// <param name="commande">ligne de fichier CSV représentant la commande</param>
        /// <returns>
        /// La commande correspondante
        /// </returns>
        public static Order FromCSV(string commande)
        {
            String[] infos = commande.Split(';');
            DateTime DateCommande = Convert.ToDateTime(infos[2]);
            DateCommande += new TimeSpan(int.Parse(infos[1].Substring(0, infos[1].Length - 1)), 0, 0);
            return new Order(long.Parse(infos[0]), DateCommande, long.Parse(infos[3]), infos[4], infos[5], infos[6]);
        }

        /// <summary>
        /// Méthode ToCSV()
        /// </summary>
        /// <returns>
        /// Identifiant;HeureH;Date;NuméroClient;NomCommis;NomLivreur;Etat;Solde
        /// </returns>
        public string ToCSV()
        {
            return $"{OrderID};{Date.Hour}H;{Date.ToShortDateString()};{CommandCustomer.PhoneNumber};{CommandWorker.LastName};{CommandDeliverer.LastName};{State};{Balance}";
        }


        /*
         *  Fonctions de tri des commandes
         */
        /// <summary>
        /// Comparaison par identifiant
        /// </summary>
        /// <returns>
        /// -1,0 ou 1 en fonction de si l'identifiant de la commande 1 est inférieur, égal ou supérieur à celui de la commande 2
        /// </returns>
        public static int CompareID(Order command_1, Order command_2)
        {
            return command_1.OrderID.CompareTo(command_2.OrderID);
        }
        /// <summary>
        /// Comparaison par urgence ie date et heure de livraison
        /// </summary>
        /// <returns>
        /// -1,0 ou 1 en fonction de si la date de la commande 1 est antérieurs, égale ou postérieure à celle de la commande 2
        /// </returns>
        public static int CompareUrgency(Order command_1, Order command_2)
        {
            return command_1.Date.CompareTo(command_2.Date);
        }
        /// <summary>
        /// Comparaison par prix
        /// </summary>
        /// <returns>
        /// -1,0 ou 1 en fonction de si le prix de la commande 1 est inférieur, égal ou supérieur à celui de la commande 2
        /// </returns>
        public static int ComparePrices(Order command_1, Order command_2)
        {
            return command_1.Price().CompareTo(command_2.Price());
        }



        /// <summary>
        /// Méthodes permettant d'enregistrer une facture (le détail d'une commande) au format Txt ou CSV
        /// On ne peut pas utiliser la méthode EnregistreDansFichierTXT/CSV de SortableObservableCollection:
        ///     Les méthodes ToCSV et ToString de Commande n'affichent pas les éléments d'une commande détaillée
        /// </summary>
        #region Enregistrement Facture dans fichiers

        #region Méthodes ToString() et ToCSV() adaptées
        public string DetailCommandeToString()
        {
            string s = "N° Commande : " + OrderID.ToString();
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
                    s += this.OrderID.ToString() + ";Pizza" + kv.Key.Price.ToString() + ";" + kv.Key.Type.ToString() + ";" + kv.Key.Size.ToString() + ";;" + kv.Value.ToString() + "\n";
                }
            }
            if (BeverageList != null && BeverageList.Count != 0)
            {
                foreach (KeyValuePair<Beverage, int> kv in BeverageList)
                {
                    s += this.OrderID.ToString() + ";" + kv.Key.Type.ToString() + ";" + kv.Key.Price.ToString() + ";;;" + kv.Key.Volume.ToString() + ";" + kv.Value.ToString() + "\n";
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

        #endregion
    }
}
