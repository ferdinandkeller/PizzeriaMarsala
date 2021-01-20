using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class FichierLivreurs
    {
        private List<Livreur> listeLivreurs;

        public FichierLivreurs()
        {
            this.listeLivreurs = null;
        }

        public FichierLivreurs(List<Livreur> listeLivreurs)
        {
            this.listeLivreurs = listeLivreurs;
        }

        public List<Livreur> ListeLivreurs
        {
            get { return this.listeLivreurs; }
        }

        public override string ToString()
        {
            string s = "";
            if (this.listeLivreurs != null && this.listeLivreurs.Count != 0)
            {
                this.listeLivreurs.ForEach(x => s += x.ToString() + "\n");
            }
            return s;
        }

        public int RechercheIndex(string nom, string prenom, string adresse)
        {
            Livreur r = new Livreur(nom, prenom, adresse, 0,"","");
            int i = this.listeLivreurs.FindIndex(x => x.Egalite((Personne)r));
            return i;
        }

        public void Supprime(Personne c)
        {
            int i = this.listeLivreurs.FindIndex(x => x.Egalite((Personne)c));
            this.listeLivreurs.RemoveAt(i);
        }

        public void Supprime(string nom, string prenom, string adresse)
        {
            int i = RechercheIndex(nom, prenom, adresse);
            this.listeLivreurs.RemoveAt(i);
        }
    }
}
