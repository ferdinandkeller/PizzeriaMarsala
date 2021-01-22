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
        public long IDCommande { get; private set; }
        public SortedList<Pizza,int> PizzasCommande { get; private set; } //int correspond à la quantité
        public SortedList<Boisson,int> BoissonsCommande { get; private set; }
        public DateTime Date { get; private set; }
        public long NumClient { get; private set; }
        public string NomCommis { get; private set; }
        public string NomLivreur { get; private set; }
        public EtatCommande Etat { get; private set; }
        public string Solde { get; private set; }

        public Commande(long id_commande, SortedList<Pizza,int> pizzas, SortedList<Boisson,int> boissons,DateTime date, long numClient, string nomCommis, string  nomLivreur)
        {
            IDCommande = id_commande;
            PizzasCommande = pizzas;
            BoissonsCommande = boissons;
            Date = date;
            NumClient = numClient;
            NomCommis = nomCommis;
            NomLivreur = nomLivreur;
            Etat = EtatCommande.enpreparation;
            Solde = "-"+PrixCommande(PizzasCommande,BoissonsCommande).ToString();
        }

        public Commande(long id_commande, DateTime date, long numClient, string nomCommis, string nomLivreur,string etat, string solde)
        {
            IDCommande = id_commande;
            PizzasCommande = null;
            BoissonsCommande = null;
            Date = date;
            NumClient = numClient;
            NomCommis = nomCommis;
            NomLivreur = nomLivreur;
            Etat = (EtatCommande)Enum.Parse(typeof(EtatCommande), etat);
            Solde = solde;
        }

        public Commande(DateTime date, SortedList<Pizza,int> pizzas, SortedList<Boisson,int> boissons, Client client, Commis commis, Livreur livreur)
            : this(GenerateurIdentifiant.CreerIdentifiantAleatoire(), pizzas, boissons, date, client.NumeroTel, commis.Nom, livreur.Nom)
        {
            // rien à faire
        }


        public static double PrixCommande(SortedList<Pizza,int> pizzas, SortedList<Boisson,int> boissons)
        {
            double prix=0;
            if(pizzas!=null && pizzas.Count != 0)
            {
                foreach(KeyValuePair<Pizza,int> x in pizzas)
                {
                    prix += (x.Key).Prix * (x.Value);
                }
            }
            if (boissons != null && boissons.Count != 0)
            {
                foreach (KeyValuePair<Boisson, int> x in boissons)
                {
                    prix += (x.Key).Prix * (x.Value);
                }
            }
            return prix;
        }
        /*
        public void DepartLivraison()
        {
            Etat = EtatCommande.EnLivraison;
            // Livreur.Etat = EtatLivreur.EnLivraison;
        }

        public void PaiementRecu()
        {
            Etat = EtatCommande.Fermee;
            Solde = EtatSolde.PaiementRecu;
            // Livreur.Etat = EtatLivreur.Surplace;
            // Client.CumulCommandes += 4;
        }

        public void PerteCommande()
        {
            Etat = EtatCommande.Fermee;
            Solde = EtatSolde.ErreurDePaiement;
            // Livreur.Etat = EtatLivreur.Surplace;
        }

        
        */

        //A Mettre dans pizzéria FAIRE

        public static Commande CSVToCommande(string commande)
        {
            {
                String[] infos = commande.Split(';');
                DateTime DateCommande = Convert.ToDateTime(infos[1] + infos[2]);
                return new Commande(long.Parse(infos[0]), DateCommande, long.Parse(infos[3]),infos[4], infos[5], infos[6], infos[7]);
            }
        }

        public string ToCSV()
        {
            return $"{IDCommande.ToString()};{ Date.Hour.ToString()};{NumClient.ToString()};{NomCommis};{NomLivreur};{Etat.ToString()};{Solde}";
        }

        #region DetailCommandeToString() & DetailCommandeToCSV()
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

        public static int CompareIDCommande(Commande a, Commande b)
        {
            return a.IDCommande.CompareTo(b.IDCommande);
        }

        public static int CompareUrgence(Commande a, Commande b)
        {
            return a.Date.CompareTo(b.Date);
        }

        public static Commande RechercheCommandeParID(List<Commande> liste, long id)
        {
            return liste.Find(x => x.IDCommande == id);
        }


    }
}
