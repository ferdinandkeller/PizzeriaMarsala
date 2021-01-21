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

        // la structure de la commande n'est pas bonne
        // elle doit contenir une liste de pizza et une liste de condiments !
        // de plus je ne suis pas sûr que la classe commande soit censée heberger la logique de la livraison

        public Commande(long id_commande, DateTime date, Client client, Commis commis, Livreur livreur)
        {

            IDCommande = id_commande;
            Date = date;
            Client = client;
            Commis = commis;
            Livreur = livreur;
            Etat = EtatCommande.EnPreparation;
            // Detail = detail;
            Solde = EtatSolde.EnAttente;
        }

        public Commande(DateTime date, Client client, Commis commis, Livreur livreur)
            : this(GenerateurIdentifiant.CreerIdentifiantAleatoire(), date, client, commis, livreur)
        {
            // rien à faire
        }

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

        // saisie facture ?
    }
}
