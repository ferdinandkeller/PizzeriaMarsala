using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Deliverer : Person
    {
        public DelivererState State { get; set; }
        public string VehicleType { get; set; }

        public int ManagedDeliveryNumber { get; set; }

        public Deliverer(string last_name, string first_name, string address, long phone_number, DelivererState state, string vehicle_type)
            : base(last_name, first_name, address, phone_number)
        {
            State = state;
            VehicleType = vehicle_type;
            ManagedDeliveryNumber = 0;
        }

        public Deliverer(string last_name, string first_name, string address, long phone_number, DelivererState state, string vehicle_type, int managed_delivery_number)
            : base(last_name, first_name, address, phone_number)
        {
            State = state;
            VehicleType = vehicle_type;
            ManagedDeliveryNumber = managed_delivery_number;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nEtat du livreur : {State} | Type de véhicule : {VehicleType} | Nombre de livraisons effectuées : {ManagedDeliveryNumber}";
        }

        public override String ToCSV()
        {
            return base.ToCSV() + $";{State};{VehicleType}";
        }

        public static Deliverer CSVToDeliverer(String deliverer)
        {
            String[] infos = deliverer.Split(';');
            return new Deliverer(infos[0], infos[1], infos[2], long.Parse(infos[3]), (DelivererState)Enum.Parse(typeof(DelivererState), infos[4]), infos[5]);
        }

    }
}
