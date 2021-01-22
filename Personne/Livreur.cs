using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Livreur : Personne
    {
        public EtatLivreur Etat { get; private set; }
        public string TypeVehicule { get; private set; }

        public Livreur(string nom, string prenom, string adresse, long numero, EtatLivreur etat, string type_vehicule)
            : base(nom, prenom, adresse, numero)
        {
            Etat = etat;
            TypeVehicule = type_vehicule;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nEtat du livreur : {Etat} | Type de véhicule : {TypeVehicule}";
        }

        public override String ToCSV()
        {
            return base.ToCSV() + $";{Etat};{TypeVehicule}";
        }

        public static Livreur CSVToLivreur(String livreur)
        {
            String[] infos = livreur.Split(';');
            return new Livreur(infos[0], infos[1], infos[2], long.Parse(infos[3]), (EtatLivreur)Enum.Parse(typeof(EtatLivreur), infos[4]), infos[5]);
        }

    }
}
