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
    public partial class CreatePizzaView : Page
    {
        MainWindow main_window;

        public PizzaType Type { get; set; } = PizzaType.Margherita;
        public PizzaSize Size { get; set; } = PizzaSize.Moyenne;
        public int Quantity { get; set; } = 1;

        public CreatePizzaView(MainWindow main_window)
        {
            InitializeComponent();

            this.DataContext = this;

            this.main_window = main_window;

            AppTitle.Content = new AppTitleComponent();
        }

        private void CreatePizza(object sender, RoutedEventArgs e)
        {
            main_window.SelectedOrder.PizzaList.Add(new Pair<Pizza, int>(new Pizza(Type, Size), Quantity));
            if (main_window.isEditingOrder)
            {
                main_window.SwitchToEditOrderView(main_window.SelectedOrder);
            } else
            {
                main_window.SwitchToCreateOrderView();
            }
        }
    }
}
