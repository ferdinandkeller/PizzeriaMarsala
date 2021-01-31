using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Classe pizza
    /// Une pizza est un produit
    /// </summary>
    /// <attributs>
    /// Type: le type de la pizza (cf PizzaType)
    /// Size: la taille de la pizza (Petite, Moyenne, Grande)
    /// </attributs>
    public class Pizza : Product, INotifyPropertyChanged
    {
        #region Attributs
        public event PropertyChangedEventHandler PropertyChanged;

        private PizzaType _Type;
        public PizzaType Type
        {
            get => _Type;
            set
            {
                _Type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }
        private PizzaSize _Size;
        public PizzaSize Size
        {
            get => _Size;
            set
            {
                _Size = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Size"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }
        public new double Price
        {
            get => (double)Type / 100 * (double)Size / 100;
        }
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
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode pour convertir une pizza en un string (pour l'afficher dans la console par exemple)
        /// </summary>
        /// <returns>
        /// Type (Taille) [Prix Euros]
        /// </returns>
        public override string ToString()
        {
            return $"{Type} ({Size}) [{Price} Euros]";
        }

        /// <summary>
        /// Comparaison de deux pizzas en fonction de leur type puis leur taille
        /// </summary>
        /// <param name="pizza1">Pizza 1</param>
        /// <param name="pizza2">Pizza 2</param>
        /// <returns>
        /// -1 si le nom de la pizza 1 est alphabétiquement avant celui de la pizza 2 ou si les types sont les mêmes mais que la taille de la pizza 1 est inférieure à celle de la pizza 2
        /// 0 si les types et tailles sont les mêmes
        /// 1 sinon
        /// </returns>
        public static int CompareTypeSize(Pizza pizza1, Pizza pizza2)
        {
            int comparison = nameof(pizza1.Type).CompareTo(nameof(pizza2.Type)); //Nom associé à la valeur de l'énumération (pas la valeur chiffrée)
            if (comparison == 0)
            {
                comparison = pizza1.Size.CompareTo(pizza2.Size);//Comparaison de la valeur chiffrée associée à l'énumaration
            }
            return comparison;
        }
        #endregion
    }
}
