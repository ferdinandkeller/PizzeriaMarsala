using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Enumération des types possibles d'une pizza
    /// Chaque type est associé à une valeur: le prix en centimes pour une pizza de taille moyenne
    /// </summary>
    public enum PizzaType
    {
        Margherita = 900,
        Reine = 1000,
        Vegetarienne = 1250,
        Chorizo = 1150, 
        Haiwaienne = 1500,
        QuatreFromages = 1300,
        Regina = 1500,
        Napolitaine = 1100
    }
}
