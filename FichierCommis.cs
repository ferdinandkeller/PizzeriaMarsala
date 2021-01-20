using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class FichierCommis: IRechercheIndex, ISupprime 
    {
        private List<Commis> listeCommis;

        public FichierCommis()
        {
            this.listeCommis = null;
        }

        public FichierCommis(List<Commis> listeCommis)
        {
            this.listeCommis = listeCommis;
        }

        public List<Commis> ListeCommis
        {
            get { return this.listeCommis; }
        }

        public override string ToString()
        {
            string s = "";
            if (this.listeCommis != null && this.listeCommis.Count != 0)
            {
                this.listeCommis.ForEach(x => s += x.ToString() + "\n");
            }
            return s;
        }

        public int RechercheIndex(string nom, string prenom, string adresse)
        {
            Commis r = new Commis(nom, prenom, adresse, 0,"", default);
            int i = this.listeCommis.FindIndex(x => x.Egalite((Personne)r));
            return i;
        }

        public void Supprime(Personne c)
        {
            int i = this.listeCommis.FindIndex(x => x.Egalite((Personne)c));
            this.listeCommis.RemoveAt(i);
        }

        public void Supprime(string nom, string prenom, string adresse)
        {
            int i = RechercheIndex(nom, prenom, adresse);
            this.listeCommis.RemoveAt(i);
        }
    }
}
