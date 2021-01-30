using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Classe pizza
    /// Une pizza est un produit
    /// </summary>
    /// <attributs>
    /// Type: le type de la pizza (cf PizzaType)
    /// Size: la taille de la pizza (Petite,Moyenne,Grande)
    /// </attributs>
    public class Pizza : Product, IComparable<Pizza>
    {
        #region Attributs
        public PizzaType Type { get; set; }
        public PizzaSize Size { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="type">Type de pizza</param>
        /// <param name="size">Taille de la pizza</param>
        public Pizza(PizzaType type, PizzaSize size)
        {
            Type = type;
            Size = size;

            Price = (double)Type/100 * (double)Size/100; //Price est un attribut de la classe Product, prix en euros de la pizza
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// ToString()
        /// </summary>
        /// <returns>
        /// Type (Taille) [Prix Euros]
        /// </returns>
        public override string ToString()
        {
            return $"{Type} ({Size}) [{Price} Euros]";
        }

        /// <summary>
        /// COmparaison de deux pizzas en fonction de leur type puis leur taille
        /// </summary>
        /// <param name="p1">Pizza 1</param>
        /// <param name="p2">Pizza 2</param>
        /// <returns>
        /// -1 si le nom de la pizza 1 est alphabétiquement avant celui de la pizza 2 ou si les types sont les mêmes mais que la taille de la pizza 1 est inférieure à celle de la pizza 2
        /// 0 si les types et tailles sont les mêmes
        /// 1 sinon
        /// </returns>
        public static int CompareTypeSize(Pizza p1, Pizza p2)
        {
            int comparison = nameof(p1.Type).CompareTo(nameof(p2.Type)); //Nom associé à la valeur de l'énumération (pas la valeur chiffrée)
            if (comparison == 0)
            {
                comparison = p1.Size.CompareTo(p2.Size);//Comparaison de la valeur chiffrée associée à l'énumaration
            }
            return comparison;
        }

        public int CompareTo(Pizza pizza)
        {
            return CompareTypeSize(this, pizza);
        }
        #endregion


    }
}
