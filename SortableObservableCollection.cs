using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PizzeriaMarsala
{
    public class SortableObservableCollection<T> : ObservableCollection<T>, IList<T>
    {

        // définition d'une comparaison entre deux éléments
        public delegate int ComparisonFunction(T el1, T el2);

        // fonction de tri par insertion qui prend en entrée
        // notre fonction de comparaison
        public void Sort(ComparisonFunction comparison_function)
        {
            for (int i = 1; i < this.Count; i++)
            {
                T val = this[i];
                int j = i - 1;
                while (j >= 0 && comparison_function(val, this[j]) < 0)
                {
                    this[j + 1] = this[j];
                    j--;
                }
                this[j + 1] = val;
            }
        }

        public List<T> ToList()
        {
            return (List<T>)this.Items;
        }

        public static bool NonVide(SortableObservableCollection<T> s)
        {
            return (s != null && s.Count != 0);
        }

        public void EnregistrerDansFichier(string nomFichier)
        {
            List<T> liste = this.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToString()));
            }
            sw.Close();
        }
        
    }
}
