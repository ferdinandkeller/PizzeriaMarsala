using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PizzeriaMarsala
{
    public class SortableObservableCollection<T> : ObservableCollection<T>, IList<T>
    {
        public delegate int Compare(T el1, T el2);

        public void Sort(Compare func_comparaison)
        {
            for (int i = 1; i < this.Count; i++)
            {
                T val = this[i];
                int j = i - 1;
                while (j >= 0 && func_comparaison(val, this[j]) < 0)
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

        public void EnregistrerDansFichierCSV(string nomFichier)
        {

        }

        public void EnregistrerDansFichierTXT(string nomFichier)
        {

        }
    }
}
