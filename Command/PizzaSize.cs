using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Enumeration des tailles possibles d'une pizza
    /// A chaque nom d'énumération correspond un pourcentage
    /// Ce pourcentage permet de calculer le prix d'une pizza
    /// </summary>
    public enum PizzaSize 
    {
        Petite = 80,
        Moyenne = 100,
        Grande = 120
    }
}
