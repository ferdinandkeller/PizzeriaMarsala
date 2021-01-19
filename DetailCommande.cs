using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class DetailCommande
    {
        private long numeroCommande=0; 
        private List<Pizza> pizzasCommande;
        private List<Boisson> boissonsCommande;

        public DetailCommande()
        { 
            this.pizzasCommande = null;
            this.boissonsCommande = null;
        }

        public DetailCommande(List<Pizza> pizzasCommande, List<Boisson> boissonsCommande)
        {
            this.numeroCommande += 1;
            this.pizzasCommande = pizzasCommande;
            this.boissonsCommande = boissonsCommande;
        }

        public long NumeroCommande
        {
            get { return this.numeroCommande; }
        }

        public List<Pizza> PizzasCommande
        {
            get { return this.pizzasCommande; }
        }

        public List<Boisson> BoissonsCommande
        {
            get { return this.boissonsCommande; }
        }

        public override string ToString()
        {
            string s = this.numeroCommande.ToString();
            if(this.pizzasCommande!=null && this.pizzasCommande.Count != 0)
            {
                this.pizzasCommande.ForEach(x => s += "\n" + x.ToString());
            }
            if (this.boissonsCommande != null && this.boissonsCommande.Count != 0)
            {
                this.boissonsCommande.ForEach(x => s += "\n" + x.ToString());
            }
            return s;
        }

        public double PrixTotal()
        {
            double p = 0;
            if (this.pizzasCommande != null && this.pizzasCommande.Count != 0)
            {
                this.pizzasCommande.ForEach(x => p += x.Prix());
            }
            if (this.boissonsCommande != null && this.boissonsCommande.Count != 0)
            {
                this.boissonsCommande.ForEach(x => p += x.Prix());
            }
            return p;
        }
    }
}
