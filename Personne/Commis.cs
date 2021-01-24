﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Commis : Personne
    {
        public EtatCommis Presence { get;private set; }
        private DateTime Embauche { get; set; }

        public int CommandesGerees { get; set; }

        public Commis(string nom, string prenom, string adresse, long numero, EtatCommis presence, DateTime embauche)
            : base(nom, prenom, adresse, numero)
        {
            Presence = presence;
            Embauche = embauche;
            CommandesGerees = 0;
        }

        public override string ToString()
        {
            return base.ToString() + $"Presence : {nameof(Presence)} | Date d'embauche : {Embauche.ToShortDateString()}";
        }

        public override string ToCSV()
        {
            return base.ToCSV() + $";{Presence};{Embauche.ToShortDateString()}";
        }

        public static Commis CSVToCommis(String commis)
        {
            String[] infos = commis.Split(';');
            return new Commis(infos[0], infos[1], infos[2], long.Parse(infos[3]), (EtatCommis)Enum.Parse(typeof(EtatCommis), infos[4]), DateTime.Parse(infos[5]));
        }

    }
}
