using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Commis : Personne
    {
        private EtatCommis Presence;
        private DateTime Embauche;

        public Commis(string nom, string prenom, string adresse, long numero, EtatCommis presence, DateTime embauche)
            : base(nom, prenom, adresse, numero)
        {
            Presence = presence;
            Embauche = embauche;
        }

        public override string ToString()
        {
            return base.ToString() + $"Presence : {nameof(Presence)} | Date d'embauche : {Embauche.ToShortDateString()}";
        }

        public override string ToCSV()
        {
            return base.ToCSV() + $";{Presence};{Embauche.ToShortDateString()}";
        }

        public static Commis StringToCommis(String commis)
        {
            String[] infos = commis.Split(';');
            return new Commis(infos[0], infos[1], infos[2], long.Parse(infos[3]), (EtatCommis)Enum.Parse(typeof(EtatCommis), infos[4]), DateTime.Parse(infos[5]));
        }

    }
}
