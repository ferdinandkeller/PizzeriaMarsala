using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Interface permettant de "généraliser" la méthode ToCSV()
    /// </summary>
    public interface IToCSV
    {
        string ToCSV();
    }
}
