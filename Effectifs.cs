using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Effectifs
    {
        private List<Commis> listeCommis;
        private List<Livreur> listeLivreurs;

        public Effectifs()
        {
            this.listeCommis = null;
            this.listeLivreurs = null;
        }

        public Effectifs(List<Commis> listeCommis, List<Livreur> listeLivreurs)
        {
            this.listeLivreurs = listeLivreurs;
            this.listeCommis = listeCommis;
        }

        public List<Commis> ListeCommis
        {
            get { return this.listeCommis; }
        }

        public List<Livreur> ListeLivreurs
        {
            get { return this.listeLivreurs; }
        }

        public override string ToString()
        {
            string s="Commis : "+"\n";
            if (this.listeCommis != null && this.listeCommis.Count != 0)
            {
                this.listeCommis.ForEach(x => s += x.ToString() + "\n");
            }
            s += "Livreurs : " + "\n";
            if (this.listeLivreurs != null && this.listeLivreurs.Count != 0)
            {
                this.listeLivreurs.ForEach(x => s += x.ToString() + "\n");
            }
            return s;
        }

        public SortedList<string,string> EtatEffectifs()
        {
            SortedList<string, string> etats = new SortedList<string, string>();
            if (this.listeCommis != null && this.listeCommis.Count != 0)
            {
                this.listeCommis.ForEach(x => etats.Add(x.Nom, x.Presence));
            }
            if (this.listeLivreurs != null && this.listeLivreurs.Count != 0)
            {
                this.listeLivreurs.ForEach(x => etats.Add(x.Nom, x.Etat));
            }
            return etats;
        }
    }
}
