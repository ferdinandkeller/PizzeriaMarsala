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
    /// Cette vue permet d'éditer des clients
    /// </summary>
    public partial class EditCustomerView : Page
    {
        #region Attributs
        MainWindow main_window;
        Customer customer;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        /// <param name="customer">Le client à modifier</param>
        public EditCustomerView(MainWindow main_window, Customer customer)
        {
            // On initialise les composants
            InitializeComponent();

            // On définit le contexte
            DataContext = customer;

            // On crééer le titre
            AppTitle.Content = new AppTitleComponent();

            // On sauvegarde la fenêtre principale et le client
            this.main_window = main_window;
            this.customer = customer;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode executée lorsque l'utilisateur a terminé de modifier le client
        /// </summary>
        private void EndEdition(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToCustomerView();
        }

        /// <summary>
        /// Méthode exécutée lorsque l'utilisateur veut supprimer le client
        /// </summary>
        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            Pizzeria.CustomersList.Remove(customer);
            main_window.SwitchToCustomerView();
        }
        #endregion
    }
}
