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
    public partial class CreateCustomerView : Page
    {
        MainWindow main_window;

        public string FirstName { get; set; } = "Prénom";
        public string LastName { get; set; } = "Nom";
        public string Address { get; set; } = "Adresse";
        public long PhoneNumber { get; set; } = 06;

        public CreateCustomerView(MainWindow main_window)
        {
            InitializeComponent();

            this.DataContext = this;

            this.main_window = main_window;

            AppTitle.Content = new AppTitleComponent();
        }

        private void CreateCustomer(object sender, RoutedEventArgs e)
        {
            Pizzeria.ListeClients.Add(new Client(LastName, FirstName, Address, PhoneNumber, DateTime.Now));
            main_window.SwitchToCustomerView();
        }
    }
}
