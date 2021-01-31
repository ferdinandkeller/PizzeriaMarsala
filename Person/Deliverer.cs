using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette classe sert à représenter un livreur
    /// Elle hérite de Personne car tout livreur est une personne
    /// </summary>
    /// <attributs>
    /// CurrentDelivererState : l'état du livreur (surplace, enconges, enlivraison)
    /// TransportType: le type de véhicule utilisé par le livreur
    /// ManagedDeliveryNumber: le nombre de commandes effectuées
    /// </attributs>
    public class Deliverer : Person
    {
        #region Attributs
        private DelivererState _CurrentDelivererState;
        public DelivererState CurrentDelivererState
        {
            get => _CurrentDelivererState;
            set
            {
                _CurrentDelivererState = value;
                NotifyPropertyChanged("CurrentDelivererState");
            }
        }
        private string _TransportType;
        public string TransportType
        {
            get => _TransportType;
            set
            {
                _TransportType = value;
                NotifyPropertyChanged("TransportType");
            }
        }
        public int _ManagedDeliveryNumber;
        public int ManagedDeliveryNumber
        {
            get => _ManagedDeliveryNumber;
            set
            {
                _ManagedDeliveryNumber = value;
                NotifyPropertyChanged("ManagedDeliveryNumber");
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// </summary>
        /// <param name="current_deliverer_state">Etat de présence du livreur</param>
        /// <param name="transport_type">Type de véhicule du livreur</param>
        /// <summary>
        /// Constructeur permettant à un commis de créer un nouveau livreur
        /// </summary>
        /// <param name="last_name">Nom</param>
        /// <param name="first_name">Prénom</param>
        /// <param name="address">Adresse</param>
        /// <param name="phone_number">Numéro de téléphone</param>
        /// <param name="current_deliverer_state">L'état du livreur</param>
        /// <param name="transport_type">Le moyen de transport utilisé par le livreur</param>
        public Deliverer(string last_name, string first_name, string address, long phone_number, DelivererState current_deliverer_state, string transport_type)
            : base(last_name, first_name, address, phone_number)
        {
            CurrentDelivererState = current_deliverer_state;
            TransportType = transport_type;
            ManagedDeliveryNumber = 0; //Le livreur vient d'être embauché, il n'a encore livré aucune commande
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// ToString() de la classe fille en utilisant celle de la classe mère
        /// </summary>
        /// <returns>
        /// NomDeFamille Prénom [NuméroDeTéléphone] : Adresse
        /// Etat du livreur : Etat | Type de véhicule : TypeVehicule | Nombre de livraisons effectuées : NombreLivraisons
        /// </returns>
        public override string ToString()
        {
            return base.ToString() + $"\nEtat du livreur : {CurrentDelivererState} | Type de véhicule : {TransportType} | Nombre de livraisons effectuées : {ManagedDeliveryNumber}";
        }

        #region Méthodes CSV
        /// <summary>
        /// ToCSV() de la classe fille en utilisant celle de la classe mère
        /// </summary>
        /// <returns>
        /// NomDeFamille;Prénom;Adresse;NuméroDeTéléphone;Etat;TypeVehicule
        /// </returns>
        public override string ToCSV()
        {
            return $"{base.ToCSV()};{CurrentDelivererState};{TransportType}";
        }

        /// <summary>
        /// Création d'un livreur depuis une ligne d'un fichier CSV
        /// </summary>
        /// <param name="deliverer">La ligne de fichier CSV représentant le livreur</param>
        /// <returns>Le livreur</returns>
        public static Deliverer CSVToDeliverer(String deliverer)
        {
            String[] infos = deliverer.Split(';');
            return new Deliverer(infos[0], infos[1], infos[2], long.Parse(infos[3]), (DelivererState)Enum.Parse(typeof(DelivererState), infos[4]), infos[5]);
        }
        #endregion

        #region Méthode de tri
        /// <summary>
        /// Comparaison de deux livreurs en fonction du nombre de commandes gérées
        /// </summary>
        /// <param name="deliverer1">Livreur 1</param>
        /// <param name="deliverer2">Livreur 2</param>
        /// <returns>
        /// -1 si le livreur 1 a géré moins de commandes que le 2
        /// 0 s'ils ont géré le même nombre de commandes
        /// 1 sinon
        /// </returns>
        public static int CompareManagedDeliveryNumber(Deliverer deliverer1, Deliverer deliverer2)
        {
            return deliverer2.ManagedDeliveryNumber.CompareTo(deliverer1.ManagedDeliveryNumber);
        }
        #endregion
        #endregion
    }
}
