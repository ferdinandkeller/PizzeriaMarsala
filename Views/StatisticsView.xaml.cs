using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette vue affiche les statistiques
    /// </summary>
    public partial class StatisticsView : Page, INotifyPropertyChanged
    {
        #region Attributs
        public event PropertyChangedEventHandler PropertyChanged;

        public string NumberOfOrdersForWorker
        {
            get
            {
                int total = 0;
                foreach (Worker worker in Pizzeria.WorkersList)
                {
                    total += worker.ManagedCommandNumber;
                }
                return "nombre de commandes totales effectuées : " + total;
            }
        }
        public string AverageOrdersForWorker
        {
            get
            {
                int total = 0;
                int count = Pizzeria.WorkersList.Count;
                foreach (Worker worker in Pizzeria.WorkersList)
                {
                    total += worker.ManagedCommandNumber;
                }
                if (count == 0)
                {
                    count = 1;
                }
                return "nombre moyen de commande effectué par commis : " + ((double)total / count);
            }
        }
        public string AverageOrderPrice
        {
            get => "Prix moyen d'une commande : " + Pizzeria.AverageOrderPrice();
        }
        public string TroopsState
        {
            get => "Etat des effectifs :\n" + Pizzeria.TroopsState();
        }
        public DateTime Date1 { get; set; } = DateTime.Now;
        public DateTime Date2 { get; set; } = DateTime.Now; 
        public string OrdersInRange
        {
            get {
                string s = "Commandes dans l'interval:\n";
                foreach (Order order in Pizzeria.CommandesInTimeSpan(Date1, Date2))
                {
                    s += order.OrderID.ToString() + ", ";
                }
                return s;
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        public StatisticsView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le data context
            this.DataContext = this;

            // on affiche le titre de l'application
            AppTitle.Content = new AppTitleComponent();

            // on charge la barre des menus
            MenuBar.Content = new ViewSwitcherComponent(main_window);
        }
        #endregion

        #region 
        /// <summary>
        /// Méthode exécutée lorsqu'il est nécessaire de recharger l'interface graphique
        /// </summary>
        public void UpdateUI()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfOrdersForWorker"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AverageOrdersForWorker"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AverageOrderPrice"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TroopsState"));
        }

        /// <summary>
        /// Méthode exécutée lorsqu'il est nécessaire de changer la liste des commandes selectionnées
        /// </summary>
        private void UpdateRangeDisplay(object sender, RoutedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OrdersInRange"));
        }
        #endregion
    }
}
