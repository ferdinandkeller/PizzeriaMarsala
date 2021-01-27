using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette classe sert à représenter un livreur
    /// Elle hérite de Personne car tout livreur est une personne
    /// </summary>
    /// <attributs>
    /// State : l'état du livreur (surplace, enconge, enlivraison)
    /// VehiculeType: le type de véhicule utilisé par le livreur
    /// ManagedDeliveryNumber: le nombre de commandes effectuées
    /// </attributs>
    public class Deliverer : Person
    {
        #region Attributs
        public DelivererState State { get; set; }
        public string VehicleType { get; set; }
        public int ManagedDeliveryNumber { get; set; }
        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur permettant à un commis de créer un nouveau livreur
        /// </summary>
        /// <param name="state">Etat de présence du livreur</param>
        /// <param name="vehicle_type">Type de véhicule du livreur</param>
        public Deliverer(string last_name, string first_name, string address, long phone_number, DelivererState state, string vehicle_type)
            : base(last_name, first_name, address, phone_number)
        {
            State = state;
            VehicleType = vehicle_type;
            ManagedDeliveryNumber = 0; //Le livreur vient d'être embauché, il n'a encore livré aucune commande
        }

        /// <summary>
        /// Pour créer un livreur depuis un fichier existant
        /// </summary>
        /// <param name="state">Etat de présence du livreur</param>
        /// <param name="vehicle_type">Type de véhicule du livreur</param>
        /// <param name="managed_delivery_number">Nombre de commandes déjà livrées</param>
        public Deliverer(string last_name, string first_name, string address, long phone_number, DelivererState state, string vehicle_type, int managed_delivery_number)
            : base(last_name, first_name, address, phone_number)
        {
            State = state;
            VehicleType = vehicle_type;
            ManagedDeliveryNumber = managed_delivery_number;
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
            return base.ToString() + $"\nEtat du livreur : {State} | Type de véhicule : {VehicleType} | Nombre de livraisons effectuées : {ManagedDeliveryNumber}";
        }

        /// <summary>
        /// ToCSV() de la classe fille en utilisant celle de la classe mère
        /// </summary>
        /// <returns>
        /// NomDeFamille;Prénom;Adresse;NuméroDeTéléphone;
        /// </returns>
        public override String ToCSV()
        {
            return base.ToCSV() + $";{State};{VehicleType}";
        }

        public static Deliverer CSVToDeliverer(String deliverer)
        {
            String[] infos = deliverer.Split(';');
            return new Deliverer(infos[0], infos[1], infos[2], long.Parse(infos[3]), (DelivererState)Enum.Parse(typeof(DelivererState), infos[4]), infos[5]);
        }

        public static int CompareManagedDeliveryNumber(Deliverer d1, Deliverer d2)
        {
            return d1.ManagedDeliveryNumber.CompareTo(d2.ManagedDeliveryNumber);
        }
        #endregion
    }
}
