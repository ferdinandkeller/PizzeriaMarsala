using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class DetailCommande
    {
        private long numeroCommande;
        private List<Pizza> pizzasCommande;
        private List<Boisson> produitsAnnexesCommande;

        public DetailCommande()
        {
            this.numeroCommande = 0;
            this.pizzasCommande = null;
            this.produitsAnnexesCommande = null;
        }

        public DetailCommande(long numeroCommande, List<Pizza> pizzasCommande, List<Boisson> produitsAnnexesCommande)
        {
            this.numeroCommande = numeroCommande;
            this.pizzasCommande = pizzasCommande;
            this.produitsAnnexesCommande = produitsAnnexesCommande;
        }

        //Propriétés
        //ToString?
    }
}
