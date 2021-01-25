using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Beverage : Product
    {
        public BeverageType Type { get; private set; }
        public double Volume { get; private set; } // volume en cl

        public Beverage(BeverageType type, double volume)
        {
            Type = type;
            Volume = volume;

            Price = (double)Type / 1000 * Volume; // prix en euros
        }

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
