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
    ///  Elle n'est instanciée qu'une seule fois
    ///  Toutes les actions effectuées à l'aide de l'interface graphique passent par elle(autrement dit c'est le centre de contrôle de l'application)
    ///  Cette classe fait office de Main
    /// </summary>
    /// <attributs>
    /// Tous statiques car doivent être accessibles et modifiables du côté de l'interface graphique
    /// CommandList: liste des commandes de la pizzeria
    /// ClientsList : liste des clients
    /// WorkersList : liste des commis
    /// DeliverersList : liste des livreurs
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
        /*
         * Toutes les méthodes sont statiques
         * On veut pouvoir appeler toutes les méthodes pour interagir facilement avec l'interface graphique
         */

        /// <summary>
        /// Implémentent la méthode Sort sur les différents attributs
        /// Utilisent différentes méthodes de comparaison en fonction du critère de tri
        /// </summary>
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
        public static double AllOrdersMean()
        {
            double res = 0;
            List<Order> liste = OrdersList.ToList();
            liste.ForEach(x => res += x.Price());
            return res/liste.Count;
        }

        /// <summary>
        /// "Moyenne des comptes clients"
        /// </summary>
        /// <returns>
        /// SortedList d'une seule KeyValuePair
        /// key = Moyenne des dates de premiere commande de l'ensemble des clients
        /// value= Moyenne des montants cumulés des clients
        /// </returns>
        public static SortedList<DateTime,double> MoyenneComptesClients()
        {
            double res = 0;
            int annee = 0;
            int mois = 0;
            int jour = 0;
            List<Customer> liste = CustomersList.ToList();
            int n = liste.Count;
            foreach(Customer client in liste)
            {
                res += client.OrdersTotalValue;
                annee += client.FirstOrderDate.Year;
                mois += client.FirstOrderDate.Month;
                jour += client.FirstOrderDate.Day;
            }
            DateTime dt = new DateTime(annee / n,mois/n,jour/n);
            SortedList<DateTime, double> sl = new SortedList<DateTime, double> ();
            sl.Add(dt, res / n);
            return sl;
        }
        
        #region Ouverture de fichiers et ajout aux listes automatique

        /// <summary>
        /// On ajoute le contenu d'un fichier à une des listes de personnes en fonction du type de fichier ouvert
        /// On ajoute seulement les éléments qui ne sont pas déjà présents dans la liste
        /// </summary>
        /// <param name="nomFichier">Le fichier source</param>

        //Clients
        public static void OuvrirFichierClient(string nomFichier)
        {
            List<Customer> liste = CreationListeClientsDepuisFichier(nomFichier);
            List<Customer> l2 = liste.FindAll(x => CustomersList.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x=>CustomersList.Add(x));
        }

        //Commis
        public static void OuvrirFichierCommis(string nomFichier)
        {
            List<Worker> liste = CreationListeCommisDepuisFichier(nomFichier);
            List<Worker> l2 = liste.FindAll(x => WorkersList.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x => WorkersList.Add(x));
        }

        //Livreurs
        public static void OuvrirFichierLivreurs(string nomFichier)
        {
            List<Deliverer> liste = CreationListeLivreursDepuisFichier(nomFichier);
            List<Deliverer> l2 = liste.FindAll(x => DeliverersList.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x => DeliverersList.Add(x));
        }

        //Commandes
        public static void OuvrirFichierCommandes(string nomFichier)
        {
            List<Order> liste = CreationListeCommandesDepuisFichier(nomFichier);
            List<Order> l2 = liste.FindAll(x => OrdersList.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x => OrdersList.Add(x));
        }

            #region Création d'une liste depuis un fichier
        public static List<string> CreationListeDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<string> liste = new List<string>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(ligne);
            }
            return liste;
        }
        public static List<Customer> CreationListeClientsDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Customer> liste = new List<Customer>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Customer.CSVToClient(ligne));
            }
            return liste;
        }

        public static List<Worker> CreationListeCommisDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Worker> liste = new List<Worker>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Worker.CSVToWorker(ligne));
            }
            return liste;
        }

        public static List<Deliverer> CreationListeLivreursDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Deliverer> liste = new List<Deliverer>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Deliverer.CSVToDeliverer(ligne));
            }
            return liste;
        }

        public static List<Order> CreationListeCommandesDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Order> liste = new List<Order>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Order.FromCSV(ligne));
            }
            return liste;
        }
        #endregion

        #endregion

        /// <summary>
        /// Méthode permettant de créer ou modifier un fichier à partir d'une liste
        /// Permet nottamment de trier un fichier existant:
        ///     On créé la liste à partir du fichier
        ///     On trie cette liste avec le critère choisi
        ///     On remet la liste dans le fichier
        /// </summary>
        /// <param name="nomFichier">Le fichier à modifier</param>
        /// <param name="l">le critère de tri</param>
        public static void ModificationFichierDepuisListe(string nomFichier, object l)
        {
            StreamWriter sw = new StreamWriter(nomFichier);
            if (l is List<string>)
            {
                List<string> m = (List<string>)l;
                if (m != null && m.Count != 0)
                {
                    m.ForEach(x => sw.WriteLine(x.ToString()));
                }
            }
            if (l is List<Person>)
            {
                List<Person> m = (List<Person>)l;
                if (m != null && m.Count != 0)
                {
                    m.ForEach(x => sw.WriteLine(x.ToString()));
                }
            }
            sw.Close();
        }

        /// <summary>
        /// Méthode permettant d'ajouter un élément en fin de fichier
        /// </summary>
        /// <param name="nomFichier">Le fichier en question</param>
        /// <param name="obj">L'élément à ajouter</param>

        public static void AjoutFichier(string nomFichier, object obj)
        {
            StreamWriter sw = new StreamWriter(nomFichier);
            sw.WriteLine(obj.ToString());
            sw.Close();
        }


        /// <summary>
        /// Méthode permettant de modifier une ligne du fichier
        /// </summary>
        /// <param name="nomFichier">Le fichier</param>
        /// <param name="index">L'index de la ligne à changer</param>
        /// <param name="obj">L'élément de remplacement</param>
        public static void ModificationLigneFichier(string nomFichier, int index, object obj)
        {
            List<string> l = CreationListeDepuisFichier(nomFichier);
            l.Insert(index, obj.ToString());
            StreamWriter sw = new StreamWriter(nomFichier);
            ModificationFichierDepuisListe(nomFichier, l);
            sw.Close();
        }

        
      /*  public static string AssociationFichier(object liste)
        {
            string s = "";
            if (liste is List<Client>)
            {
                s = "FichierClients";
            }
            else
            {
                if (liste is List<Livreur>)
                {
                    s = "FichierLivreurs";
                }
                else
                {
                    s = "FichierCommis";
                }
            }
            return s;
        }
      */

        /// <summary>
        /// ToString() sur l'ensemble des éléments de la liste
        /// </summary>
        /// <param name="l">La liste</param>
        /// <returns>
        /// Une chaîne de caractères de plusieurs lignes
        /// Chaque ligne est la représentation d'un élément de la liste sous forme de chaîne de caractères
        /// </returns>
        public static string ToString(List<object> l)
        {
            string s = "";
            if (l != null && l.Count != 0)
            {
                l.ForEach(x => s += x.ToString() + "\n");
            }
            return s;
        }


        /* Très interessant mais inutile x) (car ça ne peux pas marcher)
         * 
         * En effet, ça ne marche pas car : https://stackoverflow.com/questions/1777800/in-c-is-it-possible-to-cast-a-listchild-to-listparent (je te laisse jeter un coup d'oeil
         * 
         * La bonne méthode se trouve juste en dessous
         * 
        public static int RechercheIndexNomPrenom(List<Personne> liste, string nom, string prenom)
        {
            int i = liste.FindIndex(x => x.Nom == nom && x.Prenom == prenom);
            return i;
        }
        public static void SupprimePersonne(List<Personne> liste, Personne p)
        {
            int i = liste.FindIndex(x => x.Equals((Personne)p));
            liste.RemoveAt(i);
            ModificationFichierDepuisListe(AssociationFichier(liste), liste);
        }
        public static void SupprimePersonne(List<Personne> liste, string nom, string prenom)
        {
            int i = RechercheIndexNomPrenom(liste, nom, prenom);
            liste.RemoveAt(i);
            ModificationFichierDepuisListe(AssociationFichier(liste), liste);
        }
        public static void ModificationListePersonnes(List<Personne> liste, string nom, string prenom, Personne p)
        {
            int i = RechercheIndexNomPrenom(liste, nom, prenom);
            liste[i] = p;
            ModificationFichierDepuisListe(AssociationFichier(liste), liste);
        }
        */

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


        #region SaveReceiptTXTFile(identifiant, nom du fichier), SearchedOrderToString

        /// <summary>
        /// Enregistrer la facture d'une commande dans un fichier .txt
        /// </summary>
        /// <param name="id">Identifiant de la commande</param>
        /// <param name="nomFichier">Nom du fichier</param>
        public static void SaveReceiptTXTFile(long id, string nomFichier)
        {
            List<Order> liste = OrdersList.ToList();
            Order c = liste.Find(x => x.OrderID == id);
            c.EnregistreFactureTXT(nomFichier);
        }

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
    
        /*
        #region EnregistrerHistoriqueCommandes & EnregistrerHistoriqueFactures(TXT/CSV)

        public void EnregistrerHistoriqueCommandes(string nomFichier)
        {
            List<Commande> liste = ListeCommandes.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if(liste!=null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToCSV()));
            }
            sw.Close();
        }

        public void EnregistrerHistoriqueFacturesTXT(string nomFichier)
        {
            List<Commande> liste = ListeCommandes.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.DetailCommandeToString()));
            }
            sw.Close();
        }

        public void EnregistrerHistoriqueFacturesCSV(string nomFichier)
        {
            List<Commande> liste = ListeCommandes.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.DetailCommandeToCSV()));
            }
            sw.Close();
        }
        #endregion

        #region EnregistrerFichierClient, EnregistrerFichierCommis, EnregistrerFichierLivreur CSV

        public void EnregistrerFichierClient(string nomFichier)
        {
            List<Client> liste = ListeClients.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToCSV()));
            }
            sw.Close();
        }

        public void EnregistrerFichierCommis(string nomFichier)
        {
            List<Commis> liste = ListeCommis.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToCSV()));
            }
            sw.Close();
        }

        public void EnregistrerFichierLivreur(string nomFichier)
        {
            List<Livreur> liste = ListeLivreurs.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToCSV()));
            }
            sw.Close();
        }
        #endregion
        */


        #region Etat des effectifs

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
            if (lc!=null && lc.Count!=0)
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

        #endregion

    }
}