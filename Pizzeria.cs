﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PizzeriaMarsala
{

    /*
     * Cette classe représente la pizzeria de notre programme
     * 
     * Elle n'est instanciée qu'une seule fois, et toutes les actions effectuées à l'aide de l'interface
     * graphique passent par elle (autrement dit c'est le centre de contrôle de l'application)
     */
    public static class Pizzeria
    {

        // on stocke ici les références à nos commandes, clients, commis, livreurs
        // ces listes sont directement affichées par l'interface, donc elle doivent
        // être de type ObservableCollection pour qu'une modification de la liste
        // mette à jour l'interface
        // nous avons de plus créer la class SortableObservableCollection afin de
        // rajouter la méthode Sort
        public static SortableObservableCollection<Command> ListeCommandes { get; set; } = new SortableObservableCollection<Command>();
        public static SortableObservableCollection<Customer> ListeClients { get; set; } = new SortableObservableCollection<Customer>();
        public static SortableObservableCollection<Worker> ListeCommis { get; set; } = new SortableObservableCollection<Worker>();
        public static SortableObservableCollection<Deliverer> ListeLivreurs { get; set; } = new SortableObservableCollection<Deliverer>();

        // delegate int Compare(object obj1, object obj2);

        /*
         * Fonctions de tri
         */
        public static void SortCommandsByID() { ListeCommandes.Sort(Command.CompareID); }
        public static void SortCommandsByPrices() { ListeCommandes.Sort(Command.ComparePrices); }
        public static void SortCommandsByUrgency() { ListeCommandes.Sort(Command.CompareUrgency); }

        public static void SortCustomersByName() { ListeClients.Sort(Person.CompareName); }
        public static void SortCustomersByTown() { ListeClients.Sort(Person.CompareTown); }
        public static void SortCustomersByTotalOrders() { ListeClients.Sort(Customer.CompareTotalOrders); }

        public static void SortWorkerByName() { ListeCommis.Sort(Worker.CompareName); }
        public static void SortWorkerByTown() { ListeCommis.Sort(Worker.CompareTown); }
        public static void SortWorkerByManagedCommandNumber() { ListeCommis.Sort(Worker.CompareManagedCommandNumber); }

        public static void SortDelivererByName() { ListeLivreurs.Sort(Deliverer.CompareName); }
        public static void SortDelivererByTown() { ListeLivreurs.Sort(Deliverer.CompareTown); }
        public static void SortDelivererByManagedDeliveryNumber() { ListeLivreurs.Sort(Deliverer.CompareManagedDeliveryNumber); }

        #region delegate Trouve
        delegate object Trouve(object critere);

        public static Customer TrouveClient(string s)
        {
            if(ListeClients!=null && ListeClients.Count != 0)
            {
                for (int i=0;i<ListeClients.Count; i++)
                {
                    if (ListeClients[i].PhoneNumber.ToString() == s)
                    {
                        return ListeClients[i];
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public static Deliverer TrouveLivreur(string s)
        {
            if (ListeLivreurs != null && ListeLivreurs.Count != 0)
            {
                for (int i = 0; i < ListeLivreurs.Count; i++)
                {
                    if (ListeLivreurs[i].LastName.ToString() == s)
                    {
                        return ListeLivreurs[i];
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public static Worker TrouveCommis(string s)
        {
            if (ListeCommis != null && ListeCommis.Count != 0)
            {
                for (int i = 0; i < ListeCommis.Count; i++)
                {
                    if (ListeCommis[i].LastName.ToString() == s)
                    {
                        return ListeCommis[i];
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        #endregion
        

        /*
         * Méthode retournant la liste des commandes passées dans une période de temps donnée
         */

        public static SortableObservableCollection<Command> CommandesInTimeSpan(DateTime d1,DateTime d2)
        {
            SortableObservableCollection<Command> c = new SortableObservableCollection<Command>();
            foreach(Command commande in ListeCommandes)
            {
                if (commande.MadeDuringTimeSpan(d1, d2))
                {
                    c.Add(commande);
                }
            }
            return c;
        }

        /*
         * Moyenne des prix de toutes les commandes
         */

        public static double MoyenneToutesCommandes()
        {
            double res = 0;
            List<Command> liste = ListeCommandes.ToList();
            liste.ForEach(x => res += x.Price());
            return res/liste.Count;
        }

        /*
         * Moyenne des comptes clients : AUCUNE IDEE DE CE QUE CA VEUT DIRE
         * Hypothèse : On retourne [Moyenne dates premiere commande; Moyenne Cumul Client]
         */

        public static SortedList<DateTime,double> MoyenneComptesClients()
        {
            double res = 0;
            int annee = 0;
            int mois = 0;
            int jour = 0;
            List<Customer> liste = ListeClients.ToList();
            int n = liste.Count;
            foreach(Customer client in liste)
            {
                res += client.CommandsTotalValue;
                annee += client.FirstCommandDate.Year;
                mois += client.FirstCommandDate.Month;
                jour += client.FirstCommandDate.Day;
            }
            DateTime dt = new DateTime(annee / n,mois/n,jour/n);
            SortedList<DateTime, double> sl = new SortedList<DateTime, double> ();
            sl.Add(dt, res / n);
            return sl;
        }


        
        #region Ouverture de fichiers et ajout aux listes automatique
        //Clients
        public static void OuvrirFichierClient(string nomFichier)
        {
            List<Customer> liste = CreationListeClientsDepuisFichier(nomFichier);
            List<Customer> l2 = liste.FindAll(x => ListeClients.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x=>ListeClients.Add(x));
        }

        //Commis
        public static void OuvrirFichierCommis(string nomFichier)
        {
            List<Worker> liste = CreationListeCommisDepuisFichier(nomFichier);
            List<Worker> l2 = liste.FindAll(x => ListeCommis.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x => ListeCommis.Add(x));
        }

        //Livreurs
        public static void OuvrirFichierLivreurs(string nomFichier)
        {
            List<Deliverer> liste = CreationListeLivreursDepuisFichier(nomFichier);
            List<Deliverer> l2 = liste.FindAll(x => ListeLivreurs.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x => ListeLivreurs.Add(x));
        }

        //Commandes
        public static void OuvrirFichierCommandes(string nomFichier)
        {
            List<Command> liste = CreationListeCommandesDepuisFichier(nomFichier);
            List<Command> l2 = liste.FindAll(x => ListeCommandes.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x => ListeCommandes.Add(x));
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

        public static List<Command> CreationListeCommandesDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Command> liste = new List<Command>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Command.FromCSV(ligne));
            }
            return liste;
        }
        #endregion

        #endregion

        /*Méthode permettant de créer ou modifier un fichier à partir d'une liste
         * Permet nottamment de trier un fichier existant:
            * On créé la liste à partir du fichier
            * On trie cette liste avec le critère choisi
            * On remet la liste dans le fichier
            */

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

        //Méthode permettant d'ajouter un élément en fin de fichier

        public static void AjoutFichier(string nomFichier, object obj)
        {
            StreamWriter sw = new StreamWriter(nomFichier);
            sw.WriteLine(obj.ToString());
            sw.Close();
        }

        //Méthode permettant de modifier une ligne du fichier
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

        public static Customer FindCustomer(String lastname, String firstname)
        {
            return ListeClients.Find(customer => customer.LastName == lastname && customer.FirstName == firstname);
        }
        public static Customer FindCustomer(long phone_numer)
        {
            return ListeClients.Find(customer => customer.PhoneNumber == phone_numer);
        }
        public static Worker FindWorker(String lastname)
        {
            return ListeCommis.Find(worker => worker.LastName == lastname);
        }
        public static Deliverer FindDeliverer(String lastname)
        {
            return ListeLivreurs.Find(deliverer => deliverer.LastName == lastname);
        }

        public static Command FindCommand(long id)
        {
            return ListeCommandes.Find(x => x.CommandID == id);
        }


        #region EnregistrementFactureDansFichierTXT(identifiant, nom du fichier), StringCommandeRechercheId

        public static void EnregistrementFactureDansFichierTXT(long id, string nomFichier)
        {
            List<Command> liste = ListeCommandes.ToList();
            Command c = liste.Find(x => x.CommandID == id);
            c.EnregistreFactureTXT(nomFichier);
        }

        public static string StringCommandeRechercheId(long id)
        {
            List<Command> liste = ListeCommandes.ToList();
            Command c = FindCommand(id);
            return c.ToString();
        }

        public static string StringPrixCommandeRechercheId(long id)
        {
            List<Command> liste = ListeCommandes.ToList();
            Command c = FindCommand(id);
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


        #region EtatEffectifs
        public static string EtatEffectifs()
        {
            string s = "";
            List<Worker> lc = ListeCommis.ToList();
            List<Deliverer> ll = ListeLivreurs.ToList();
            if (lc!=null && lc.Count!=0)
            {
                lc.ForEach(x => s += x.LastName + ";" + x.State.ToString() + "\n");
            }
            if (ll != null && ll.Count != 0)
            {
                ll.ForEach(x => s += x.LastName + ";" + x.State.ToString() + "\n");
            }
            return s;
        }
        #endregion

    }
}