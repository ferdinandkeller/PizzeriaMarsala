using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Commande
    {
        public long IDCommande { get; private set; }
        public DateTime Date { get; private set; }
        public Client Client { get; private set; }
        public Commis Commis { get; private set; }
        public Livreur Livreur { get; private set; }
        public EtatCommande Etat { get; private set; }
        public EtatSolde Solde { get; private set; }
        // public DetailCommande Detail { get; private set; }

        // la structure de la commande n'est pas bonne
        // elle doit contenir une liste de pizza et une liste de condiments !
        // de plus je ne suis pas sûr que la classe commande soit censée heberger la logique de la livraison

        public Commande(long id_commande, DateTime date, Client client, Commis commis, Livreur livreur) //, DetailCommande detail)
            : this(date, client, commis, livreur) //, detail)
        {
            IDCommande = id_commande;
        }

        public Commande(DateTime date, Client client, Commis commis, Livreur livreur) //, DetailCommande detail)
        {
            Random rng = new Random();
            IDCommande = ((long)rng.Next() << 32) | (long)rng.Next();
            Date = date;
            Client = client;
            Commis = commis;
            Livreur = livreur;
            Etat = EtatCommande.EnPreparation;
            // Detail = detail;
            Solde = EtatSolde.EnAttente;
        }

        public void PaiementRecu()
        {
            Solde = EtatSolde.PaiementRecu;
            Etat = EtatCommande.Fermee;
            // Livreur.Etat = EtatLivreur.Surplace;
            // Client.CumulCommandes += 4;
        }

        public void PerteCommande()
        {
            Solde = EtatSolde.ErreurDePaiement;
            Etat = EtatCommande.Fermee;
            // Livreur.Etat = EtatLivreur.Surplace;
        }

        public void DepartLivraison()
        {
            Etat = EtatCommande.EnLivraison;
            // Livreur.Etat = EtatLivreur.EnLivraison;
        }

        // saisie facture ?
    }
}
