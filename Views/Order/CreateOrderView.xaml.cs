using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette vue permet de créer des commandes
    /// </summary>
    public partial class CreateOrderView : Page
    {
        #region Attributs
        private MainWindow main_window;
        public ObservableCollection<Pair<Pizza, int>> PizzaList { get; set; }
        public ObservableCollection<Pair<Beverage, int>> BeverageList { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        public CreateOrderView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le contexte
            this.DataContext = this;

            // on sauvegarde une référence à la fenêtre principale
            this.main_window = main_window;

            // on créée une nouvelle commande
            main_window.SelectedOrder = new Order(main_window.SelectedCustomer, main_window.SelectedWorker, main_window.SelectedDeliverer);
            PizzaList = main_window.SelectedOrder.PizzaList;
            BeverageList = main_window.SelectedOrder.BeverageList;

            // on affiche le titre
            AppTitle.Content = new AppTitleComponent();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur veut crééer une pizza
        /// </summary>
        private void CreatePizza(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToCreatePizzaView();
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur veut éditer une pizza
        /// </summary>
        private void EditPizza(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToEditPizzaView((Pair<Pizza, int>)((Border)sender).Tag);
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur veut créer une boisson
        /// </summary>
        private void CreateBeverage(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToCreateBeverageView();
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur veut éditer une boisson
        /// </summary>
        private void EditBeverage(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToEditBeverageView((Pair<Beverage, int>)((Border)sender).Tag);
        }

        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur veut crééer une commande
        /// </summary>
        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            if (PizzaList.Count > 0)
            {
                Pizzeria.OrdersList.Add(main_window.SelectedOrder);
                main_window.SelectedOrder.CommandWorker.ManagedCommandNumber++;
                main_window.create_order_view = null;
            }
            main_window.isEditingOrder = true;
            main_window.SwitchToOrderView();
        }
        #endregion
    }
}
