using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Commis:Personne
    {
        private string presence; //"surplace" ou "enconges"
        private DateTime embauche;

        public Commis() : base()
        {
            this.presence = null;
            this.embauche = default;
        }

        public Commis(string nom, string prenom, string adresse, long numero,string presence, DateTime embauche) : base(nom, prenom, adresse, numero)
        {
            this.presence = presence;
            this.embauche = embauche;
        }

        public string Presence
        {
            get { return this.presence; }
        }

        public DateTime Embauche
        {
            get { return this.embauche; }
        }

        public override string ToString()
        {
            return base.ToString()+ " ; "+ this.presence+" ; "+this.embauche.ToShortDateString();
        }

        public static Commis CreationCommisDepuisString(string[] s)
        {
            DateTime dt = new DateTime();

            if (s.Length == 4)
            {
                dt = default;
            }
            else
            {
                dt = Convert.ToDateTime(s[5]);
            }
            Commis c = new Commis(s[0], s[1], s[2], long.Parse(s[3]), s[4],dt);
            return c;
        }
    }
}
