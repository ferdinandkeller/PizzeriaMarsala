using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Enumération détaillant les différents états possibles d'une commande
    /// </summary>
    public enum OrderState
    {
        EnPreparation, 
        EnLivraison,
        Fermee
    }
}
