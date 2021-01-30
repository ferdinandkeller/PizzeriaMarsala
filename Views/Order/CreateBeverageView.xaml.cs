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
    public partial class CreateBeverageView : Page
    {
        MainWindow main_window;

        public BeverageType Type { get; set; } = BeverageType.Coca;
        public int Volume { get; set; } = 50;
        public int Quantity { get; set; } = 1;

        public CreateBeverageView(MainWindow main_window)
        {
            InitializeComponent();

            this.DataContext = this;

            this.main_window = main_window;

            AppTitle.Content = new AppTitleComponent();
        }

        private void CreateBeverage(object sender, RoutedEventArgs e)
        {
            Pizzeria.BeverageList.Add(new Pair<Beverage, int>(new Beverage(Type, Volume), Quantity));
            main_window.SwitchToCreateOrderView();
        }
    }
}
