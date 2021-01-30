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
    public partial class EditBeverageView : Page
    {
        MainWindow main_window;
        Pair<Beverage, int> beverage_pair;

        public EditBeverageView(MainWindow main_window, Pair<Beverage, int> beverage_pair)
        {
            InitializeComponent();

            DataContext = beverage_pair;

            AppTitle.Content = new AppTitleComponent();

            this.main_window = main_window;
            this.beverage_pair = beverage_pair;
        }

        private void EndEdition(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToCreateOrderView();
        }

        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            Pizzeria.BeverageList.Remove(beverage_pair);
            main_window.SwitchToCreateOrderView();
        }
    }
}
