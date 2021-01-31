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
    public partial class StatisticsView : Page, INotifyPropertyChanged
    {
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

        public void UpdateUI()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberOfOrdersForWorker"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AverageOrdersForWorker"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AverageOrderPrice"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TroopsState"));
        }
    }
}
