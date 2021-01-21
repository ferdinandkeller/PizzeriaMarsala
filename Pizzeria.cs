using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PizzeriaMarsala
{
    class Pizzeria
    {

        public ObservableCollection<Commande> ListeCommandes { get; set; } = new ObservableCollection<Commande>();
        // tri par : ordre alphabétique, ville, montant cummulé, urgence

        /*
         * j'ai besoin :    - tri tout le monde par toutes les manières
         *                  - Ouvrir fichier / Exporter
         *                  - modfier classe commande pour prendre en compte les details de la commande
         *                  - j'aime les frites
         *                  - rien
         */

        public static List<string> CreationListeDepuisFichier(string nomFicher)
        {
            StreamReader sr = new StreamReader(nomFicher);
            List<string> liste = new List<string>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(ligne);
            }
            return liste;
        }
        public static List<Client> CreationListeClientsDepuisFichier(string nomFicher)
        {
            StreamReader sr = new StreamReader(nomFicher);
            List<Client> liste = new List<Client>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                string[] mots = ligne.Split(';');
                liste.Add(Client.CreationClientDepuisString(mots));
            }
            return liste;
        }

        public static List<Commis> CreationListeCommisDepuisFichier(string nomFicher)
        {
            StreamReader sr = new StreamReader(nomFicher);
            List<Commis> liste = new List<Commis>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                string[] mots = ligne.Split(';');
                liste.Add(Commis.CreationCommisDepuisString(mots));
            }
            return liste;
        }

        public static List<Livreur> CreationListeLivreursDepuisFichier(string nomFicher)
        {
            StreamReader sr = new StreamReader(nomFicher);
            List<Livreur> liste = new List<Livreur>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                string[] mots = ligne.Split(';');
                liste.Add(Livreur.CreationLivreurDepuisString(mots));
            }
            return liste;
        }

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
            if (l is List<Personne>)
            {
                List<Personne> m = (List<Personne>)l;
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

        public static string AssociationFichier(object liste)
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
        public static string ToString(List<object> l)
        {
            string s = "";
            if (l != null && l.Count != 0)
            {
                l.ForEach(x => s += x.ToString() + "\n");
            }
            return s;
        }
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
    }
}
