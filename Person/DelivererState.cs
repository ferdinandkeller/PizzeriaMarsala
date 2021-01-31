using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Enumération pour être certain que chaque élément créé ne puisse prendre qu'une valeur autorisée
    /// Valeurs autorisées : SurPlace, EnConges, EnLivraison
    /// </summary>
    public enum DelivererState
    {
        SurPlace,
        EnConges,
        EnLivraison
    }
}
