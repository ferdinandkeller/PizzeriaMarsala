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
    /// <summary>
    /// Cette vue permet d'éditer des commandes
    /// </summary>
    public partial class EditOrderView : Page, INotifyPropertyChanged
    {
        #region Attributs
        public event PropertyChangedEventHandler PropertyChanged;

        private MainWindow main_window;
        public Order order { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        /// <param name="order">La commande à modifier</param>
        public EditOrderView(MainWindow main_window, Order order)
        {
            // On initialise les composants
            InitializeComponent();

            // On définit le contexte
            this.DataContext = this;

            // On crééer le titre
            AppTitle.Content = new AppTitleComponent();

            // On sauvegarde la fenêtre principale et le client
            this.main_window = main_window;
            this.order = order;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode executée lorsque l'utilisateur veut créér une pizza
        /// </summary>
        private void CreatePizza(object sender, RoutedEventArgs e)
        {
            if (order.Balance != BalanceState.OK && order.CurrentOrderState == OrderState.EnPreparation)
            { 
                main_window.SwitchToCreatePizzaView();
            }
        }

        /// <summary>
        /// Méthode executée lorsque l'utilisateur veut modifier une pizza
        /// </summary>
        private void EditPizza(object sender, MouseButtonEventArgs e)
        {
            if (order.Balance != BalanceState.OK && order.CurrentOrderState == OrderState.EnPreparation)
            {
                main_window.SwitchToEditPizzaView((Pair<Pizza, int>)((Border)sender).Tag);
            }
        }

        /// <summary>
        /// Méthode executée lorsque l'utilisateur veut créér une boisson
        /// </summary>
        private void CreateBeverage(object sender, RoutedEventArgs e)
        {
            if (order.Balance != BalanceState.OK && order.CurrentOrderState == OrderState.EnPreparation)
            {
                main_window.SwitchToCreateBeverageView();
            }
        }

        /// <summary>
        /// Méthode executée lorsque l'utilisateur veut modifier une boisson
        /// </summary>
        private void EditBeverage(object sender, MouseButtonEventArgs e)
        {
            if (order.Balance != BalanceState.OK && order.CurrentOrderState == OrderState.EnPreparation)
            {
                main_window.SwitchToEditBeverageView((Pair<Beverage, int>)((Border)sender).Tag);
            }
        }

        /// <summary>
        /// Méthode executée lorsque l'utilisateur veut modifier l'état de la commande
        /// </summary>
        private void ChangeState(object sender, RoutedEventArgs e)
        {
            if (order.CurrentOrderState == OrderState.EnPreparation)
            {
                order.StartDelivery();
            }
            else if (order.CurrentOrderState == OrderState.EnLivraison) {
                order.DeliveryDone();
            }

            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("order")); }
        }

        /// <summary>
        /// Méthode executée lorsque l'utilisateur veut modifier le solde
        /// </summary>
        private void ChangeBalance(object sender, RoutedEventArgs e)
        {
            if (order.Balance == BalanceState.EnAttente) 
            {
                order.Balance = BalanceState.OK;
                order.CommandCustomer.OrdersTotalValue += order.Price();
            }
            else if (order.Balance == BalanceState.OK) 
            {
                order.Balance = BalanceState.Perdue;
                order.CommandCustomer.OrdersTotalValue -= order.Price();
            }
            else if (order.Balance == BalanceState.Perdue)
            {
                order.Balance = BalanceState.EnAttente;
            }

            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("order")); }
        }

        /// <summary>
        /// Méthode executée lorsque l'utilisateur a terminé de modifier la commande
        /// </summary>
        private void EndEdition(object sender, RoutedEventArgs e)
        {
            if (order.PizzaList.Count > 0)
            {
                order.UpdatePrice();
                main_window.SwitchToOrderView();
            }
        }

        /// <summary>
        /// Méthode exécutée lorsque l'utilisateur veut supprimer la commande
        /// </summary>
        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            main_window.SelectedOrder.CommandWorker.ManagedCommandNumber--;
            if (main_window.SelectedOrder.CurrentOrderState == OrderState.Fermee)
            {
                order.CommandDeliverer.ManagedDeliveryNumber--;
            }
            if (main_window.SelectedOrder.Balance == BalanceState.OK)
            {
                order.CommandCustomer.OrdersTotalValue -= order.Price();
            }
            Pizzeria.OrdersList.Remove(main_window.SelectedOrder);
            main_window.SwitchToOrderView();
        }
        #endregion
    }
}
