using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Effectifs
    {
        private FichierCommis fichierCommis;
        private FichierLivreurs fichierLivreurs;

        public Effectifs()
        {
            this.fichierCommis = null;
            this.fichierLivreurs = null;
        }

        public Effectifs(FichierCommis fichierCommis, FichierLivreurs fichierLivreurs)
        {
            this.fichierLivreurs = fichierLivreurs;
            this.fichierCommis = fichierCommis;
        }

        public FichierCommis FichierCommis
        {
            get { return this.fichierCommis; }
        }

        public FichierLivreurs FichierLivreurs
        {
            get { return this.fichierLivreurs; }
        }

        public override string ToString()
        {
            string s="Commis : "+"\n"+this.fichierCommis.ToString() + "\n"; 
            s += "Livreurs : " + "\n"+this.fichierLivreurs.ToString();
            return s;
        }

        public SortedList<string,string> EtatEffectifs()
        {
            SortedList<string, string> etats = new SortedList<string, string>();
            if (this.fichierCommis != null && this.fichierCommis.ListeCommis.Count != 0)
            {
                this.fichierCommis.ListeCommis.ForEach(x => etats.Add(x.Nom, x.Presence));
            }
            if (this.fichierLivreurs != null && this.fichierLivreurs.ListeLivreurs.Count != 0)
            {
                this.fichierLivreurs.ListeLivreurs.ForEach(x => etats.Add(x.Nom, x.Etat));
            }
            return etats;
        }
    }
}
