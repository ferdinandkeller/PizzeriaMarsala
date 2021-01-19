using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Commande
    {
        private double heure;
        private DateTime date;
        private Client client;
        private Commis commis;
        private Livreur livreur;
        private string etatCommande; //en preparation, en livraison, fermee
        private string solde; //ok, perdue, valeur
        private DetailCommande detail; //num commande et détail

        public Commande()
        {
            this.heure = 0;
            this.date = default;
            this.client = null;
            this.commis = null;
            this.livreur = null;
            this.etatCommande = null;
            this.solde = null;
            this.detail = null;
        }

        public Commande(double heure, DateTime date, Client client, Commis commis, Livreur livreur, DetailCommande detail)
        {
            this.heure = heure;
            this.date = date;
            this.client = client;
            this.commis = commis;
            this.livreur = livreur;
            this.etatCommande = "En préparation";
            this.detail = detail;
            this.solde = "-"+(this.detail.PrixTotal()).ToString();
        }

        public double Heure
        {
            get { return this.heure; }
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        public Client Client
        {
            get { return this.client; }
        }

        public Commis Commis
        {
            get { return this.commis; }
        }

        public Livreur Livreur
        {
            get { return this.livreur; }
        }

        public string EtatCommande
        {
            get { return this.etatCommande; }
        }

        public string Solde
        {
            get { return this.solde; }
        }

        public DetailCommande Detail
        {
            get { return this.detail; }
        }
        public void PaiementRecu()
        {
            this.solde = "ok";
            this.etatCommande = "fermée";
            this.livreur.Etat = "sur place";
            this.client.CumulCommandes += 1;

        }

        public void PerteCommande()
        {
            this.solde = "perdue";
            this.etatCommande = "fermée";
            this.livreur.Etat = "sur place";
        }

        public void DepartLivraison()
        {
            this.etatCommande = "en livraison";
            this.livreur.Etat = "en livraison";
        }


        //Saisie facture??
    }
}
