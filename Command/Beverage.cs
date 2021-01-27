using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Classe boisson, une boisson est un produit
    /// </summary>
    /// <attributs>
    /// Type: type de boisson (cf enum BeverageType)
    /// Volume: volume de la boisson
    /// </attributs>
    public class Beverage : Product
    {
        #region Attributs
        public BeverageType Type { get; private set; }
        public double Volume { get; private set; } // volume en cl
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur -> Boisson
        /// </summary>
        /// <param name="type">type de boisson</param>
        /// <param name="volume">volume de la boisson</param>
        public Beverage(BeverageType type, double volume)
        {
            Type = type;
            Volume = volume;

            Price = (double)Type / 1000 * Volume; // prix en euros
        }
        #endregion

        public override string ToString()
        {
            return $"{Type} ({Volume}cl) [{Price}$]";
        }

        public static int CompareTypeVolume(Beverage b1, Beverage b2)
        {
            int compa = nameof(b1.Type).CompareTo(nameof(b2.Type));
            if (compa == 0)
            {
                compa = b1.Volume.CompareTo(b2.Volume);
            }
            return compa;
        }
    }
}
