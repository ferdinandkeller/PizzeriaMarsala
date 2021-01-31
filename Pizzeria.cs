using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Représente la pizzeria
    /// Puisqu'elle n'est instanciée qu'une seule fois, et afin de simplifier le code, elle est définie comme statique
    /// Toutes les actions effectuées à l'aide de l'interface graphique passent par elle
    /// (autrement dit c'est le centre de contrôle de l'application)
    /// </summary>
    /// <attributs>
    /// Tous statiques car doivent être accessibles et modifiables du côté de l'interface graphique
    /// 
    /// CommandList: liste des commandes de la pizzeria
    /// ClientsList : liste des clients
    /// WorkersList : liste des commis
    /// DeliverersList : liste des livreurs
    /// 
    /// Ces listes sont directement affichées par l'interface,
    /// donc elle doivent être de type ObservableCollection pour qu'une modification de la liste mette à jour l'interface
    /// Nous avons de plus créé la class SortableObservableCollection afin de rajouter la méthode Sort
    /// </attributs>
    public static class Pizzeria
    {

        #region Attributs
        public static SortableObservableCollection<Order> OrdersList { get; set; } = new SortableObservableCollection<Order>();
        public static SortableObservableCollection<Customer> CustomersList { get; set; } = new SortableObservableCollection<Customer>();
        public static SortableObservableCollection<Worker> WorkersList { get; set; } = new SortableObservableCollection<Worker>();
        public static SortableObservableCollection<Deliverer> DeliverersList { get; set; } = new SortableObservableCollection<Deliverer>();
        #endregion

        #region Méthodes
        #region Méthodes de tri
        /*
         * Sur la liste de commandes (par identifiant, prix, urgence)
         */
        public static void SortOrdersByID() { OrdersList.Sort(Order.CompareID); }
        public static void SortOrdersByPrices() { OrdersList.Sort(Order.ComparePrices); }
        public static void SortOrdersByUrgency() { OrdersList.Sort(Order.CompareUrgency); }

        /*
         * Sur la liste de clients (par nom, ville, commandes cumulées)
         */
        public static void SortCustomersByName() { CustomersList.Sort(Person.CompareName); }
        public static void SortCustomersByTown() { CustomersList.Sort(Person.CompareTown); }
        public static void SortCustomersByTotalOrders() { CustomersList.Sort(Customer.CompareTotalOrders); }

        /*
         * Sur la liste de commis (par nom, ville, commandes gérées)
         */
        public static void SortWorkerByName() { WorkersList.Sort(Worker.CompareName); }
        public static void SortWorkerByTown() { WorkersList.Sort(Worker.CompareTown); }
        public static void SortWorkerByManagedOrderNumber() { WorkersList.Sort(Worker.CompareManagedOrderNumber); }

        /*
         * Sur la liste de livreurs (par nom, ville, commandes livrées)
         */
        public static void SortDelivererByName() { DeliverersList.Sort(Deliverer.CompareName); }
        public static void SortDelivererByTown() { DeliverersList.Sort(Deliverer.CompareTown); }
        public static void SortDelivererByManagedDeliveryNumber() { DeliverersList.Sort(Deliverer.CompareManagedDeliveryNumber); }
        #endregion

        #region Méthodes permettant de trouver une personne ou une commande
        /// <summary>
        /// Trouver un client par son nom et prénom
        /// </summary>
        /// <param name="lastname">Nom</param>
        /// <param name="firstname">Prénom</param>
        /// <returns>Le client cherché</returns>
        public static Customer FindCustomer(String lastname, String firstname)
        {
            return CustomersList.Find(customer => customer.LastName == lastname && customer.FirstName == firstname);
        }
        /// <summary>
        /// Trouver un client par son numéro de téléphone
        /// </summary>
        /// <param name="phone_numer">Le numéro</param>
        /// <returns>Le client cherché</returns>
        public static Customer FindCustomer(long phone_numer)
        {
            return CustomersList.Find(customer => customer.PhoneNumber == phone_numer);
        }
        /// <summary>
        /// Trouver un commis avec son nom
        /// </summary>
        /// <param name="lastname">Nom de famille</param>
        /// <returns>Le commis cherché</returns>
        public static Worker FindWorker(String lastname)
        {
            return WorkersList.Find(worker => worker.LastName == lastname);
        }
        /// <summary>
        /// Trouver un livreur avec son nom
        /// </summary>
        /// <param name="lastname">Nom de famille</param>
        /// <returns>Le livreur cherché</returns>
        public static Deliverer FindDeliverer(String lastname)
        {
            return DeliverersList.Find(deliverer => deliverer.LastName == lastname);
        }
        /// <summary>
        /// Trouver une commande avec son identifiant
        /// </summary>
        /// <param name="id">L'identifiant de la commande</param>
        /// <returns>La commande cherchée</returns>
        public static Order FindOrder(long id)
        {
            return OrdersList.Find(x => x.OrderID == id);
        }
        #endregion

        #region Méthodes statistiques
        /// <summary>
        /// Liste des commandes passées durant une période de temps donnée
        /// Utilisation de la méthode MadeDuringTimeSpan de la classe Order
        /// </summary>
        /// <param name="d1">Borne inférieure de la période (moment initial)</param>
        /// <param name="d2">Borne supérieure (moment final)</param>
        /// <returns>La SortableObservableCollection<Order> des commandes recherchées </returns>
        public static SortableObservableCollection<Order> CommandesInTimeSpan(DateTime d1,DateTime d2)
        {
            SortableObservableCollection<Order> c = new SortableObservableCollection<Order>();
            foreach(Order commande in OrdersList)
            {
                if (commande.MadeDuringTimeSpan(d1, d2))
                {
                    c.Add(commande);
                }
            }
            return c;
        }

        /// <summary>
        /// Moyenne des prix de toutes les commandes faites dans la pizzéria
        /// </summary>
        /// <returns>La moyenne cherchée (prix en euros)</returns>
        public static double AverageOrderPrice()
        {
            double res = 0;
            int count = 0;
            foreach (Order order in OrdersList)
            {
                if (order.Balance == BalanceState.OK)
                {
                    count++;
                    res += order.Price();
                }
            }
            if (count > 0)
            {
                res /= count;
            }
            return (double)((int)(res*100))/100;
        }

        /// <summary>
        /// Résume l'état des effectifs
        /// Les effectifs se composent des commis et livreurs
        /// </summary>
        /// <returns>
        /// Chaine de caractères de plusieurs ligne (une ligne par travailleur)
        /// Pour chaque ligne :
        ///     NomDeFamille;Etat
        /// </returns>
        public static string TroopsState()
        {
            string s = "";
            List<Worker> lc = WorkersList.ToList();
            List<Deliverer> ll = DeliverersList.ToList();
            if (lc != null && lc.Count != 0)
            {
                lc.ForEach(x => s += x.LastName + ";" + x.CurrentWorkerState.ToString() + "\n");
            }
            if (ll != null && ll.Count != 0)
            {
                ll.ForEach(x => s += x.LastName + ";" + x.CurrentDelivererState.ToString() + "\n");
            }
            return s;
        }
        #endregion
        
        #region Ouverture de fichiers et ajout aux listes automatique
        /// <summary>
        /// Cette fonction permet d'ajouter une liste de commandes depuis un fichier CSV
        /// dans la liste des commandes, tout en évitant les doublons
        /// </summary>
        /// <param name="file_name">Le nom du fichier</param>
        public static void AddFileToOrderList(string file_name)
        {
            StreamReader stream_reader = new StreamReader(file_name);
            while (stream_reader.Peek() > 0)
            {
                string order_as_string = stream_reader.ReadLine();
                Order order = Order.CSVToOrder(order_as_string);
                if (FindOrder(order.OrderID) == null)
                {
                    OrdersList.Add(order);
                    order.CommandWorker.ManagedCommandNumber++;
                    if (order.Balance == BalanceState.OK) { order.CommandCustomer.OrdersTotalValue += order.Price(); }
                    if (order.CurrentOrderState == OrderState.Fermee) { order.CommandDeliverer.ManagedDeliveryNumber++; }
                }
            }
        }

        /// <summary>
        /// Cette fonction permet d'ajouter une liste de clients depuis un fichier CSV
        /// dans la liste de clients, tout en évitant les doublons
        /// </summary>
        /// <param name="file_name">Le nom du fichier</param>
        public static void AddFileToCustomerList(string file_name)
        {
            StreamReader stream_reader = new StreamReader(file_name);
            while (stream_reader.Peek() > 0)
            {
                Customer customer = Customer.CSVToCustomer(stream_reader.ReadLine());
                if (FindCustomer(customer.PhoneNumber) == null)
                {
                    CustomersList.Add(customer);
                }
            }
        }

        /// <summary>
        /// Cette fonction permet d'ajouter une liste de commis depuis un fichier CSV
        /// dans la liste de commis, tout en évitant les doublons
        /// </summary>
        /// <param name="file_name">Le nom du fichier</param>
        public static void AddFileToWorkerList(string file_name)
        {
            StreamReader stream_reader = new StreamReader(file_name);
            while (stream_reader.Peek() > 0)
            {
                Worker worker = Worker.CSVToWorker(stream_reader.ReadLine());
                if (FindWorker(worker.LastName) == null)
                {
                    WorkersList.Add(worker);
                }
            }
        }

        /// <summary>
        /// Cette fonction permet d'ajouter une liste de livreurs depuis un fichier CSV
        /// dans la liste de livreurs, tout en évitant les doublons
        /// </summary>
        /// <param name="file_name">Le nom du fichier</param>
        public static void AddFileToDelivererList(string file_name)
        {
            StreamReader stream_reader = new StreamReader(file_name);
            while (stream_reader.Peek() > 0)
            {
                Deliverer deliverer = Deliverer.CSVToDeliverer(stream_reader.ReadLine());
                if (FindDeliverer(deliverer.LastName) == null)
                {
                    DeliverersList.Add(deliverer);
                }
            }
        }
        #endregion

        #region bonus : sauvegarde des listes au format csv
        /// <summary>
        /// Cette fonction exporte la liste des clients au format CSV
        /// </summary>
        /// <param name="file_name">Le nom du fichier</param>
        public static void SaveCustomers(string file_name)
        {
            using (StreamWriter file = new StreamWriter(file_name))
            {
                foreach (Customer customer in CustomersList)
                file.Write($"{customer.ToCSV()}\n");
            }
        }

        /// <summary>
        /// Cette fonction exporte la liste des commis au format CSV
        /// </summary>
        /// <param name="file_name">Le nom du fichier</param>
        public static void SaveWorkers(string file_name)
        {
            using (StreamWriter file = new StreamWriter(file_name))
            {
                foreach (Worker worker in WorkersList)
                file.Write($"{worker.ToCSV()}\n");
            }
        }

        /// <summary>
        /// Cette fonction exporte la liste des livreurs au format CSV
        /// </summary>
        /// <param name="file_name">Le nom du fichier</param>
        public static void SaveDeliverers(string file_name)
        {
            using (StreamWriter file = new StreamWriter(file_name))
            {
                foreach (Deliverer deliverer in DeliverersList)
                file.Write($"{deliverer.ToCSV()}\n");
            }
        }

        /// <summary>
        /// Cette fonction exporte la liste des commandes au format CSV
        /// </summary>
        /// <param name="file_name">Le nom du fichier</param>
        public static void SaveOrders(string file_name)
        {
            using (StreamWriter file = new StreamWriter(file_name))
            {
                foreach (Order order in OrdersList)
                    file.Write($"{order.ToCSV()}\n");
            }
        }
        #endregion

        #region Cherche des commandes
        /// <summary>
        /// Trouve une commande avec son identifiant et la met sous forme de string
        /// </summary>
        /// <param name="id">L'identifiant de la commande</param>
        /// <returns>Description de la commande cherchée</returns>
        public static string SearchedOrderToString(long id)
        {
            List<Order> liste = OrdersList.ToList();
            Order c = FindOrder(id);
            return c.ToString();
        }

        /// <summary>
        /// Donne le prix d'une commande et rappelle l'identifiant de cette commande
        /// </summary>
        /// <param name="id">l'identifiant de la commande</param>
        /// <returns>N° Commande : NumeroCommande; Prix : PrixCommande</returns>
        public static string SearchedOrderPriceToString(long id)
        {
            List<Order> liste = OrdersList.ToList();
            Order c = FindOrder(id);
            double prix = c.Price();
            return "N° Commande : "+id.ToString()+"; Prix : "+prix.ToString();
        }
        #endregion
        #endregion
    }
}