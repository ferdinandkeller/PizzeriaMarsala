using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Pizza : Product
    {
        public PizzaType Type { get; private set; }
        public PizzaSize Size { get; }

        public Pizza(PizzaType type, PizzaSize size)
        {
            Type = type;
            Size = size;

            Price = (double)Type/100 * (double)Size/100;
        }

        public override string ToString()
        {
            return $"{Type} ({Size}) [{Price}$]";
        }

        public static int CompareTypeSize(Pizza p1, Pizza p2)
        {
            int comparison = nameof(p1.Type).CompareTo(nameof(p2.Type));
            if (comparison == 0)
            {
                comparison = p1.Size.CompareTo(p2.Size);
            }
            return comparison;
        }


    }
}
