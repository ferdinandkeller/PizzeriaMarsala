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

        public SortableObservableCollection<Commande> ListeCommandes { get; set ; } = new SortableObservableCollection<Commande>();
        public SortableObservableCollection<Client> ListeClients { get; set; } = new SortableObservableCollection<Client>();
        public SortableObservableCollection<Commis> ListeCommis { get; set; } = new SortableObservableCollection<Commis>();
        public SortableObservableCollection<Livreur> ListeLivreurs { get; set; } = new SortableObservableCollection<Livreur>();

        delegate int Compare(object obj1, object obj2);

        #region delegate Trouve
        delegate object Trouve(object critere);

        public Client TrouveClient(string s)
        {
            if(ListeClients!=null && ListeClients.Count != 0)
            {
                for (int i=0;i<ListeClients.Count; i++)
                {
                    if (ListeClients[i].NumeroTel.ToString() == s)
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

        public Livreur TrouveLivreur(string s)
        {
            if (ListeLivreurs != null && ListeLivreurs.Count != 0)
            {
                for (int i = 0; i < ListeLivreurs.Count; i++)
                {
                    if (ListeLivreurs[i].Nom.ToString() == s)
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
        public Commis TrouveCommis(string s)
        {
            if (ListeCommis != null && ListeCommis.Count != 0)
            {
                for (int i = 0; i < ListeCommis.Count; i++)
                {
                    if (ListeCommis[i].Nom.ToString() == s)
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

        #region Ouverture de fichiers et ajout aux listes automatique

        //Clients
        public void OuvrirFichierClient(string nomFichier)
        {
            List<Client> liste = CreationListeClientsDepuisFichier(nomFichier);
            List<Client> l2 = liste.FindAll(x => ListeClients.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x=>ListeClients.Add(x));
        }

        //Commis
        public void OuvrirFichierCommis(string nomFichier)
        {
            List<Commis> liste = CreationListeCommisDepuisFichier(nomFichier);
            List<Commis> l2 = liste.FindAll(x => ListeCommis.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x => ListeCommis.Add(x));
        }

        //Livreurs
        public void OuvrirFichierLivreurs(string nomFichier)
        {
            List<Livreur> liste = CreationListeLivreursDepuisFichier(nomFichier);
            List<Livreur> l2 = liste.FindAll(x => ListeLivreurs.Contains(x));
            l2.ForEach(x => liste.Remove(x));
            liste.ForEach(x => ListeLivreurs.Add(x));
        }

        //Commandes
        public void OuvrirFichierCommandes(string nomFichier)
        {
            List<Commande> liste = CreationListeCommandesDepuisFichier(nomFichier);
            List<Commande> l2 = liste.FindAll(x => ListeCommandes.Contains(x));
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
        public static List<Client> CreationListeClientsDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Client> liste = new List<Client>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Client.CSVToClient(ligne));
            }
            return liste;
        }

        public static List<Commis> CreationListeCommisDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Commis> liste = new List<Commis>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Commis.CSVToCommis(ligne));
            }
            return liste;
        }

        public static List<Livreur> CreationListeLivreursDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Livreur> liste = new List<Livreur>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Livreur.CSVToLivreur(ligne));
            }
            return liste;
        }

        public static List<Commande> CreationListeCommandesDepuisFichier(string nomFichier)
        {
            StreamReader sr = new StreamReader(nomFichier);
            List<Commande> liste = new List<Commande>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(Commande.CSVToCommande(ligne));
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


        #region EnregistrementFactureDansFichierTXT(identifiant, nom du fichier), StringCommandeRechercheId

        public void EnregistrementFactureDansFichierTXT(long id, string nomFichier)
        {
            List<Commande> liste = ListeCommandes.ToList();
            Commande c = liste.Find(x => x.IDCommande == id);
            c.EnregistreFactureTXT(nomFichier);
        }

        public string StringCommandeRechercheId(long id)
        {
            List<Commande> liste = ListeCommandes.ToList();
            Commande c = Commande.RechercheCommandeParID(liste, id);
            return c.ToString();
        }

        public string StringPrixCommandeRechercheId(long id)
        {
            List<Commande> liste = ListeCommandes.ToList();
            Commande c = Commande.RechercheCommandeParID(liste, id);
            double prix = Commande.PrixCommande(c.PizzasCommande, c.BoissonsCommande);
            return "N° Commande : "+id.ToString()+"; Prix : "+prix.ToString();
        }

        #endregion

        #region TriListe...

        //Commandes
        public void TriCommandesParId()
        {
            ListeCommandes.Sort(Commande.CompareIDCommande);
        }

        public void TriCommandesParUrgence() 
        {
            ListeCommandes.Sort(Commande.CompareUrgence);
        }

        //Clients
        public void TriClientParNom()
        {
            ListeClients.Sort(Personne.CompareNomPrenom);
        }

        public void TriClientParVille()
        {
            ListeClients.Sort(Personne.CompareVille);
        }

        public void TriClientParCumul()
        {
            ListeClients.Sort(Client.ComparePrixCumule);
        }

        //Livreurs
        public void TriLivreursParNom()
        {
            ListeLivreurs.Sort(Livreur.CompareNomPrenom);
        }

        public void TriLivreursParVille()
        {
            ListeLivreurs.Sort(Livreur.CompareVille);
        }

        //Commis
        public void TriCommisParNom()
        {
            ListeCommis.Sort(Commis.CompareNomPrenom);
        }

        public void TriCommisParVille()
        {
            ListeCommis.Sort(Commis.CompareVille);
        }

        #endregion

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

        #region EtatEffectifs
        public string EtatEffectifs()
        {
            string s = "";
            List<Commis> lc = ListeCommis.ToList();
            List<Livreur> ll = ListeLivreurs.ToList();
            if (lc!=null && lc.Count!=0)
            {
                lc.ForEach(x => s += x.Nom + ";" + x.Presence.ToString() + "\n");
            }
            if (ll != null && ll.Count != 0)
            {
                ll.ForEach(x => s += x.Nom + ";" + x.Etat.ToString() + "\n");
            }
            return s;
        }
        #endregion
    }
}
