﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette classe sert à représenter un client
    /// Elle hérite de Personne car tout client est une personne
    /// </summary>
    /// <attributs>
    /// CustomerID: Identifiant du client (N+1 e client de la pizzeria ou identifiant déclaré dans un fichier)
    /// FirstOrderDate: Date de la première commande du client
    /// OrdersTotalValue: Valeur des commandes cumulées (honorées) du client
    /// CustomerIDMax: permet d'enregistrer l'identifiant courant des clients de la pizzeria (le plus grand identifiant utilisé)
    /// </attributs>
    public class Customer : Person
    {

        #region Attributs
        public long CustomerID { get; set; }
        public DateTime FirstOrderDate { get; private set; }
        public double OrdersTotalValue { get; set; }
        static long CustomerIDMax { get; set; } = 0;
        #endregion

        #region Constructeurs

        /// <summary>
        /// Utilisé par les commis pour enregistrer un nouveau client (avec la date de première commande)
        /// </summary>
        /// <param name="last_name">Nom</param>
        /// <param name="first_name">Prénom</param>
        /// <param name="address">Adresse</param>
        /// <param name="phone_number">Numéro de téléphone</param>
        /// <param name="first_command_date">Date de première commande</param>
        public Customer(string last_name, string first_name, string address, long phone_number, DateTime first_command_date)
            : base(last_name, first_name, address, phone_number)
        {
            CustomerIDMax++; //Il y a un client de plus dans le "fichier clients"
            CustomerID = CustomerIDMax; //Le nouveau client prend le nouvel id créé pour lui
            FirstOrderDate = first_command_date;
        }

        /// <summary>
        /// Crééer un client depuis un fichier existant
        /// </summary>
        /// <param name="customer_id">Identifiant (on suppose qu'aucun client du fichier client ne possède le même identifiant)</param>
        /// <param name="last_name">Nom</param>
        /// <param name="first_name">Prénom</param>
        /// <param name="address">Adresse</param>
        /// <param name="phone_number">Numéro de téléphone</param>
        /// <param name="first_command_date">Date de première commande</param>
        /// <param name="total_value_Orders">Montant cumulé des commandes honorées</param>
        public Customer(long customer_id, string last_name, string first_name, string address, long phone_number, DateTime first_command_date, double total_value_Orders)
            : base(last_name, first_name, address, phone_number)
        {
            CustomerID = customer_id;
            FirstOrderDate = first_command_date;
            OrdersTotalValue = total_value_Orders;
            //On actualise l'id max si le client saisi possède un identifiant supérieur à l'id courant
            if (CustomerIDMax < CustomerID)
            {
                CustomerIDMax = CustomerID;
            }
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// Méthode ToString() de Customer héritant de celle de la classe Personne
        /// </summary>
        /// <returns>
        /// NomDeFamille Prénom [NuméroDeTéléphone] : Adresse 
        /// Première commande : DatePremièreCommande | Cumul des commandes : ValeurCommandesCumulées
        /// </returns>
        public override string ToString()
        {
            return base.ToString() + $"\nPremière commande : {FirstOrderDate.ToShortDateString()} | Cumul des commandes : {OrdersTotalValue}";
        }

        /// <summary>
        /// Méthode ToCSV() de Customer héritant de celle de la classe Personne
        /// </summary>
        /// <returns>
        /// Identifiant;NomDeFamille;Prénom;Adresse;NuméroDeTéléphone;DatePremiereCommande;CumulCommandes
        /// </returns>
        public override string ToCSV()
        {
            return $"{CustomerID};" + base.ToCSV() + $";{FirstOrderDate.ToShortDateString()};{OrdersTotalValue}";
        }

        /// <summary>
        /// Créer un client depuis une ligne de fichier CSV
        /// Utilisation du 2e constructeur de Customer
        /// </summary>
        /// <param name="client">La ligne de fichier CSV représentant le client à créer</param>
        /// <returns>
        /// Le Customer correspondant à la ligne de fichier
        /// </returns>
        public static Customer CSVToClient(String client)
        {
            String[] infos = client.Split(';');
            DateTime first_command_date = infos.Length == 5 ? DateTime.Now : Convert.ToDateTime(infos[5]);
            double Orders_total_value = infos.LongLength == 6 ? 0 : double.Parse(infos[6]);
            return new Customer(long.Parse(infos[0]), infos[1], infos[2], infos[3], long.Parse(infos[4]), first_command_date, Orders_total_value);
        }

        /// <summary>
        /// Comparaison de deux clients par le montant cumulé de leurs commandes
        /// </summary>
        /// <param name="c1">Client 1</param>
        /// <param name="c2">Client 2</param>
        /// <returns>
        /// -1 si le cumul du client 1 est inférieur à celui du client 2
        /// 0 s'ils sont égaux
        /// 1 sinon
        /// </returns>
        public static int CompareTotalOrders(Customer c1, Customer c2)
        {
            return c1.OrdersTotalValue.CompareTo(c2.OrdersTotalValue);
        }

        #endregion

    }
}
