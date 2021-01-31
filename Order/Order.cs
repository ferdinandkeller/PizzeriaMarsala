using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Classe correspondant à une commande
    /// Elle hérite de l'interface IToCSV, elle implémente la méthode ToCSV() permettant de la convertir au format CSV
    /// </summary>
    /// <attributs>
    /// OrderIDMax: L'identifiant actuel des commandes, on l'incrémente à chaque commande (static)
    /// OrderID: identifiant de la commande
    /// Date: date et heure de la commande
    /// PizzaList: liste des pizzas de la commande 
    /// BeverageList: liste des boissons de la commande
    /// CommandCustomer: le client ayant passé la commande
    /// CommandWorker: le commis ayant enregistré la commande
    /// CommandDeliverer: livreur de la commande
    /// State: état de la commande (enpreparation, enlivraison, fermee)
    /// Balance: état du solde de la commande (enattente, ok, perdue)
    /// OrderPrice: le prix de la commande (utilisé pour l'interface graphique)
    /// </attributs>
    public class Order : IToCSV, INotifyPropertyChanged
    {
        #region Attributs
        public event PropertyChangedEventHandler PropertyChanged;

        static long OrderIDMax { get; set; }
        public long OrderID { get; set; } 

        public DateTime Date { get; private set; }

        public ObservableCollection<Pair<Pizza, int>> PizzaList { get; set; } = new ObservableCollection<Pair<Pizza, int>>();
        public ObservableCollection<Pair<Beverage, int>> BeverageList { get; set; } = new ObservableCollection<Pair<Beverage, int>>();
       
        public Customer CommandCustomer { get; private set; }
        public Worker CommandWorker { get; private set; }
        public Deliverer CommandDeliverer { get; private set; }

        private OrderState _CurrentOrderState;
        public OrderState CurrentOrderState {
            get => _CurrentOrderState;
            set
            {
                _CurrentOrderState = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentOrderState"));
            }
        }
        private BalanceState _Balance;
        public BalanceState Balance
        {
            get => _Balance;
            set
            {
                _Balance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Balance"));
            }
        }
        public double OrderPrice { get => Price(); }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur utilisé lors d'une saisie par le commis
        /// Sans le détail de la commande
        /// </summary>
        /// <param name="customer">Client passant la commande</param>
        /// <param name="worker">Commis enregistrant la commande</param>
        /// <param name="deliverer">Livreur chargé de la livraison</param>
        public Order(Customer customer, Worker worker, Deliverer deliverer)
        {
            OrderIDMax++; //il y a une nouvelle commande pour la pizzeria
            OrderID = OrderIDMax; //identifiant de la commande
            Date = DateTime.Now; //la commande vient d'être passée
            CommandCustomer = customer;
            CommandWorker = worker;
            CommandDeliverer = deliverer;
            CurrentOrderState = OrderState.enpreparation; //automatiquement, la commande est passée en cuisine
            Balance = BalanceState.enattente; //la commande n'est pas encore payée
        }

        /// <summary>
        /// Construteur utilisé lorsqu'on ouvre un fichier
        /// </summary>
        /// <param name="order_id">Identifiant de la commande</param>
        /// <param name="date">Date et heure de commande</param>
        /// <param name="customer_phone_number">Numéro de téléphone du client</param>
        /// <param name="worker_name">Nom du commis</param>
        /// <param name="deliverer_name">Nom du livreur</param>
        /// <param name="order_state">L'état de la commande</param>
        /// <param name="solde">Etat du solde</param>
        public Order(long order_id, DateTime date, long customer_phone_number, string worker_name, string deliverer_name, string order_state, string solde)
            : this(Pizzeria.FindCustomer(customer_phone_number), Pizzeria.FindWorker(worker_name), Pizzeria.FindDeliverer(deliverer_name))
        {
            Date = date;
            OrderID = order_id;
            if (OrderIDMax < OrderID)//Si l'identifiant max des commandes de la pizzeria est inférieur à celui donné dans le fichier
            {
                OrderIDMax = OrderID; //On actualise l'identifiant max
            }
            CurrentOrderState = (OrderState)Enum.Parse(typeof(OrderState), order_state);
            Balance = (BalanceState)Enum.Parse(typeof(BalanceState), solde); //on récupère le solde donné dans le fichier en effectuant une conversion
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Cette commande informe l'interface que le prix a changé
        /// </summary>
        public void UpdatePrice()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OrderPrice"));
        }

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
            foreach(Pair<Pizza, int> pair in PizzaList)
            {
                price += pair.Key.Price * pair.Value;
            }

            // on compte le prix des boissons
            foreach(Pair<Beverage, int> pair in BeverageList)
            {
                price += pair.Key.Price * pair.Value;
            }

            // on renvoie le prix
            return price;
        }

        /// <summary>
        /// Lorsqu'une commande part en livraison
        ///     Etat de la commande: enpreparation->enlivraison
        ///     Etat du livreur: surplace->enlivraison
        /// </summary>
        public void StartDelivery()
        {
            CurrentOrderState = OrderState.enlivraison;
            //Le livreur assigné part en livraison
            CommandDeliverer.CurrentDelivererState = DelivererState.enlivraison;
        }

        /// <summary>
        /// Lorsqu'une livraison a été effectuée
        /// </summary>
        public void DeliveryDone()
        {
            CurrentOrderState = OrderState.fermee;
            //Le livreur assigné est sur place, il a effectué une livraison en plus
            CommandDeliverer.CurrentDelivererState = DelivererState.surplace;
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
            if(0 <= Date.CompareTo(d1) && Date.CompareTo(d2) <= 0)
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
        public static Order CSVToOrder(string commande)
        {
            String[] infos = commande.Split(';');
            Order order = new Order(long.Parse(infos[0]), DateTime.Parse(infos[1]), long.Parse(infos[2]), infos[3], infos[4], infos[5], infos[6]);
            // on compte le nombre de pizzas
            int length = int.Parse(infos[7]);
            // on ajouter les pizzas
            for (int i = 8; i < 8 + length * 3; i += 3)
            {
                order.PizzaList.Add(new Pair<Pizza, int>(
                    new Pizza(
                        (PizzaType)Enum.Parse(typeof(PizzaType), infos[i]),
                        (PizzaSize)Enum.Parse(typeof(PizzaSize), infos[i + 1])
                    ),
                    int.Parse(infos[i + 2])));
            }
            // on ajoute les boissons
            for (int i = 8 + length * 3; i < infos.Length; i += 3)
            {
                order.BeverageList.Add(new Pair<Beverage, int>(
                    new Beverage(
                        (BeverageType)Enum.Parse(typeof(BeverageType), infos[i]),
                        int.Parse(infos[i + 1])
                    ),
                    int.Parse(infos[i + 2])));
            }
            return order;
        }

        /// <summary>
        /// Méthode ToCSV()
        /// </summary>
        /// <returns>
        /// Identifiant;HeureH;Date;NuméroClient;NomCommis;NomLivreur;Etat;Solde
        /// </returns>
        public string ToCSV()
        {
            string s = $"{OrderID};{Date};{CommandCustomer.PhoneNumber};{CommandWorker.LastName};{CommandDeliverer.LastName};{CurrentOrderState};{Balance};";

            // on enregistrer le nombre de pizzas
            s += $"{PizzaList.Count};";

            // on enregistre les pizzas
            foreach (Pair<Pizza, int> pair in PizzaList)
            {
                s += $"{pair.Key.Type};{pair.Key.Size};{pair.Value};";
            }

            // on enregistre les boissons
            foreach (Pair<Beverage, int> pair in BeverageList)
            {
                s += $"{pair.Key.Type};{pair.Key.Volume};{pair.Value};";
            }

            return s.Substring(0, s.Length-1);
        }

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
        /// Méthodes permettant d'enregistrer une facture détaillée
        /// </summary>
        #region Enregistrement facture détaillée
        public string DetailedBillToString()
        {
            string s = "N° Commande : " + OrderID.ToString() + "\n" + Date + "\n\n";
            if (PizzaList != null && PizzaList.Count != 0)
            {
                foreach (Pair<Pizza, int> kv in PizzaList)
                {
                    s += "Pizza : " + kv.Key + " (x" + kv.Value + ")" + "\n";
                }
            }
            if (BeverageList != null && BeverageList.Count != 0)
            {
                foreach (Pair<Beverage, int> kv in BeverageList)
                {
                    s += "Boisson : " + kv.Key + " (x" + kv.Value + ")" + "\n";
                }
            }
            s += "\nTotal : " + Price() + " Euros\n";
            return s;
        }

        public void SaveBill(string nomFichier)
        {
            StreamWriter sw = new StreamWriter(nomFichier);
            sw.WriteLine(DetailedBillToString());
            sw.Close();
        }
        #endregion

        #endregion
    }
}
