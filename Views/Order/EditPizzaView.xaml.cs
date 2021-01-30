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

namespace PizzeriaMarsala
{
    public partial class EditPizzaView : Page
    {
        MainWindow main_window;
        Pair<Pizza, int> pizza_pair;

        public EditPizzaView(MainWindow main_window, Pair<Pizza, int> pizza_pair)
        {
            InitializeComponent();

            DataContext = pizza_pair;

            AppTitle.Content = new AppTitleComponent();

            this.main_window = main_window;
            this.pizza_pair = pizza_pair;
        }

        private void EndEdition(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToCreateOrderView();
        }

        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            Pizzeria.PizzaList.Remove(pizza_pair);
            main_window.SwitchToCreateOrderView();
        }
    }
}
