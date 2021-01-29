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
    public partial class EditWorkerView : Page
    {
        MainWindow main_window;
        Worker worker;

        public EditWorkerView(MainWindow main_window, Worker worker)
        {
            InitializeComponent();

            DataContext = worker;

            AppTitle.Content = new AppTitleComponent();

            this.main_window = main_window;
            this.worker = worker;
        }

        private void EndEdition(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToWorkerView();
        }

        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            Pizzeria.WorkerList.Remove(worker);
            main_window.SwitchToWorkerView();
        }
    }
}
