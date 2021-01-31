using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Enumération des types de boissons
    /// Permet de ne donner qu'un type prédéfini à une boisson tout en associant un prix à chaque type de boisson
    /// Les prix sont exprimés en centimes/L (on ne peut pas mettre de double ici)
    /// </summary>
    public enum BeverageType
    {
        Coca = 120,
        JusOrange = 250,
        JusPomme = 200,
        EauPlate = 10,
        Perrier = 60,
        Schwepps = 190
    }
}
