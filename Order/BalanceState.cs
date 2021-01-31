using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Différents états sont possibles pour le solde d'une commande
    /// On utilise ici une énumération pour être certain que chaque élément créé ne puisse prendre qu'une valeur parmi celles autorisées
    /// Valeurs autorisées : EnAttente, OK, Perdue
    /// </summary>
    public enum BalanceState
    {
        EnAttente,
        OK,
        Perdue
    }
}
