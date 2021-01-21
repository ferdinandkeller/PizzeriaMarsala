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
    public partial class SwitchViewComponent : Page
    {
        MainWindow main_window;

        public SwitchViewComponent(MainWindow main_window)
        {
            InitializeComponent();

            this.main_window = main_window;
        }

        private void SwitchToCommandPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.ViewFrame.Content = main_window.command_view;
        }
        
        private void SwitchToClientPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.ViewFrame.Content = main_window.client_view;
        }

        private void SwitchToWorkerPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.ViewFrame.Content = main_window.worker_view;
        }

        private void SwitchToDelivererPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.ViewFrame.Content = main_window.deliverer_view;
        }
    }
}
