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
using Microsoft.Win32;

namespace PizzeriaMarsala
{
    public partial class BonusView : Page
    {

        MainWindow main_window;

        public long BillOrderId { get; set; } = 0;

        public BonusView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le data context
            this.DataContext = this;

            // on enregistre l'object windows localement
            this.main_window = main_window;

            // on charge le titre de l'application
            AppTitle.Content = new AppTitleComponent();

            // on charge la barre des menus
            MenuBar.Content = new ViewSwitcherComponent(main_window);
        }

        private void CreateBill(object sender, RoutedEventArgs e)
        {
            Order command = Pizzeria.FindOrder(BillOrderId);
            if (command != null)
            {
                SaveFileDialog file_dialog = new SaveFileDialog();
                if (file_dialog.ShowDialog() == true)
                {
                    command.SaveBill(file_dialog.FileName);
                }
            }
        }

        private void SaveClients(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Sauvegarder clients");
        }

        private void SaveWorkers(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Sauvegarder commis");
        }

        private void SaveDeliverers(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Sauvegarder livreur");
        }
    }
}
