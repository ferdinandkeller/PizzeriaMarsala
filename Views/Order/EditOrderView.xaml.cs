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
    public partial class EditOrderView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MainWindow main_window;
        public Order order { get; set; }

        public EditOrderView(MainWindow main_window, Order order)
        {
            InitializeComponent();

            this.DataContext = this;

            this.main_window = main_window;
            this.order = order;

            if (order.Balance == BalanceState.ok)
            {
                order.CommandCustomer.OrdersTotalValue -= order.Price();
            }

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

        private void ChangeState(object sender, RoutedEventArgs e)
        {
            if (order.State == OrderState.enpreparation) { order.State = OrderState.enlivraison; }
            else if (order.State == OrderState.enlivraison) {
                order.State = OrderState.fermee;
                main_window.SelectedOrder.CommandDeliverer.ManagedDeliveryNumber++;
            }
            else if (order.State == OrderState.fermee) {
                order.State = OrderState.enpreparation;
                main_window.SelectedOrder.CommandDeliverer.ManagedDeliveryNumber--;
            }

            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("order")); }
        }

        private void ChangeBalance(object sender, RoutedEventArgs e)
        {
            if (order.Balance == BalanceState.enattente) { order.Balance = BalanceState.ok; }
            else if (order.Balance == BalanceState.ok) { order.Balance = BalanceState.perdue; }
            else if (order.Balance == BalanceState.perdue) { order.Balance = BalanceState.enattente; }

            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("order")); }
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            main_window.SelectedOrder.CommandWorker.ManagedCommandNumber--;
            if (main_window.SelectedOrder.State == OrderState.fermee)
            {
                main_window.SelectedOrder.CommandDeliverer.ManagedDeliveryNumber--;
            }
            Pizzeria.OrdersList.Remove(main_window.SelectedOrder);
            main_window.SwitchToCommandView();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            order.UpdatePrice();
            if (order.Balance == BalanceState.ok)
            {
                main_window.SelectedOrder.CommandCustomer.OrdersTotalValue += order.Price();
            }
            main_window.SwitchToCommandView();
        }
    }
}
