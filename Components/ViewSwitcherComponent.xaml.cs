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
    public partial class ViewSwitcherComponent : Page
    {
        MainWindow main_window;

        public ViewSwitcherComponent(MainWindow main_window)
        {
            InitializeComponent();

            this.main_window = main_window;
        }

        private void SwitchToCommandPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToCommandView();
        }
        
        private void SwitchToClientPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToCustomerView();
        }

        private void SwitchToWorkerPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToWorkerView();
        }

        private void SwitchToDelivererPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToDelivererView();
        }

        private void SwitchToStatisticsPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToStatisticsView();
        }

        private void SwitchToBonusPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToBonusView();
        }
    }
}
