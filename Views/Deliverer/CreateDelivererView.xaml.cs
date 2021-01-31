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
    public partial class CreateDelivererView : Page
    {
        MainWindow main_window;

        public string FirstName { get; set; } = "Prénom";
        public string LastName { get; set; } = "Nom";
        public string Address { get; set; } = "42 allée des tilleuls Paris";
        public string PhoneNumber { get; set; } = "0600000000";
        public DelivererState State { get; set; } = DelivererState.surplace;
        public String VehicleType { get; set; } = "scooter";

        public CreateDelivererView(MainWindow main_window)
        {
            InitializeComponent();

            this.DataContext = this;

            this.main_window = main_window;

            AppTitle.Content = new AppTitleComponent();
        }

        private void CreateDeliverer(object sender, RoutedEventArgs e)
        {
            Pizzeria.DeliverersList.Add(new Deliverer(LastName, FirstName, Address, long.Parse(PhoneNumber), State, VehicleType));
            if (main_window.isEditingOrder)
            {
                main_window.SwitchToDelivererView();
            }
            else
            {
                main_window.SwitchToSelectDelivererView();
            }
        }
    }
}
