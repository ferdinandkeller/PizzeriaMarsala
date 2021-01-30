using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette classe permet de stocker une paire de valeurs dans une liste
    /// De plus puisqu'elle implémente l'interface INotifyPropertyChanged, elle met à jour
    /// l'interface graphique automatiquement
    /// </summary>
    /// <typeparam name="T">Le type de la clé</typeparam>
    /// <typeparam name="U">Le type de la valeur</typeparam>
    public class Pair<T, U> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private T _Key;
        private U _Value;

        /// <summary>
        /// La clé
        /// </summary>
        public T Key
        {
            get => _Key;
            set
            {
                _Key = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Key"));
                }
            }
        }
        /// <summary>
        /// La valeur
        /// </summary>
        public U Value
        {
            get => _Value;
            set
            {
                _Value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }

        /// <summary>
        /// Constructeur principal d'une paire
        /// </summary>
        /// <param name="key">La clé de la paire</param>
        /// <param name="value">La valeur de la paire</param>
        public Pair(T key, U value)
        {
            _Key = key;
            _Value = value;
        }
    }

}
