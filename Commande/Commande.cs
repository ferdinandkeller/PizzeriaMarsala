﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Commande:IToCSV
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

        // la structure de la commande n'est pas bonne
        // elle doit contenir une liste de pizza et une liste de condiments !
        // de plus je ne suis pas sûr que la classe commande soit censée heberger la logique de la livraison

        public Commande(long id_commande, SortedList<Pizza,int> pizzas, SortedList<Boisson,int> boissons,DateTime date, long numClient, string nomCommis, string  nomLivreur)
        {

            IDCommande = id_commande;
            PizzasCommande = pizzas;
            BoissonsCommande = boissons;
            Date = date;
            NumClient = numClient;
            NomCommis = nomCommis;
            NomLivreur = nomLivreur;
            Etat = EtatCommande.EnPreparation;
            Solde = "-"+PrixCommande(PizzasCommande,BoissonsCommande).ToString();
        }

        public Commande(DateTime date, SortedList<Pizza,int> pizzas, SortedList<Boisson,int> boissons, Client client, Commis commis, Livreur livreur)
            : this(GenerateurIdentifiant.CreerIdentifiantAleatoire(), pizzas,boissons,date, client.NumeroTel, commis.Nom, livreur.Nom)
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

        //Comparaison des dates de commandes (par date croissante)
        public static int CompareDelais(Commande a, Commande b)
        {
            return a.Date.CompareTo(b.Date);
        }
        */

        //A FAIRE

        public static 
        public static Commande CSVToCommande(string commande)
        {
            {
                String[] infos = commande.Split(';');
                DateTime DatePremiereCommande = infos.Length == 5 ? DateTime.Now : Convert.ToDateTime(infos[5]);
                long cumul_commandes = infos.LongLength == 6 ? 0 : long.Parse(infos[6]);
                return new Commande(long.Parse(infos[0]), infos[1], infos[2], infos[3], long.Parse(infos[4]), DatePremiereCommande, cumul_commandes);
            }
        }

        public string ToCSV()
        {
            return $"{IDCommande.ToString()};{ Date.Hour.ToString()};{NumClient.ToString()};{NomCommis};{NomLivreur};{Etat.ToString()};{Solde}";
        }

        // saisie facture ?
    }
}
