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
    public partial class EditCustomerView : Page
    {
        MainWindow main_window;
        Customer customer;

        public EditCustomerView(MainWindow main_window, Customer customer)
        {
            InitializeComponent();

            DataContext = customer;

            AppTitle.Content = new AppTitleComponent();

            this.main_window = main_window;
            this.customer = customer;
        }

        private void EndEdition(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToCustomerView();
        }

        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            Pizzeria.ListeClients.Remove(customer);
            main_window.SwitchToCustomerView();
        }
    }
}
