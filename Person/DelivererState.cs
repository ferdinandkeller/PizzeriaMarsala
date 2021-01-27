using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Enumération pour être certain que chaque élément créé ne puisse prendre qu'une valeur autorisée
    /// Valeurs autorisées : surplace, enconges, enlivraison
    /// </summary>
    public enum DelivererState
    {
        surplace,
        enconges,
        enlivraison
    }
}
