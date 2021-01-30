using System;
using System.Collections.Generic;
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
     
    public partial class CreateOrderView : Page
    {
        private MainWindow main_window;

        public ObservableCollection<Pair<Pizza, int>> PizzaList { get; set; } = Pizzeria.PizzaList;
        public ObservableCollection<Pair<Beverage, int>> BeverageList { get; set; } = Pizzeria.BeverageList;

        public CreateOrderView(MainWindow main_window)
        {
            InitializeComponent();

            this.DataContext = this;

            this.main_window = main_window;

            AppTitle.Content = new AppTitleComponent();
        }

        private void CreatePizza(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToCreatePizzaView();
        }

        private void EditPizza(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToEditPizzaView((Pair<Pizza, int>)((Border)sender).Tag);
        }

        private void CreateBeverage(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToCreateBeverageView();
        }

        private void EditBeverage(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToEditBeverageView((Pair<Beverage, int>)((Border)sender).Tag);
        }

        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            if (PizzaList.Count > 0)
            {
                Order order = new Order(main_window.SelectedCustomer, main_window.SelectedWorker, main_window.SelectedDeliverer);
                order.PizzaList = Pizzeria.PizzaList.ToList<Pair<Pizza, int>>();
                order.BeverageList = Pizzeria.BeverageList.ToList<Pair<Beverage, int>>();
                Pizzeria.OrdersList.Add(order);
                Pizzeria.PizzaList.Clear();
                Pizzeria.BeverageList.Clear();
            }
            main_window.SwitchToCommandView();
        }
    }
}
