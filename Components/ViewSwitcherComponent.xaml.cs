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
    /// <summary>
    /// Cette class affiche le menu permettant de changer de vue
    /// Ce menu est présent notamment sur le menu principal de l'application
    /// </summary>
    public partial class ViewSwitcherComponent : Page
    {
        #region Attributs
        // une référence à la fenêtre principale
        MainWindow main_window;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur princiapl
        /// </summary>
        /// <param name="main_window">La fenêtre principale</param>
        public ViewSwitcherComponent(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on sauvegarde la référence à la fenêtre principale
            this.main_window = main_window;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Cette fonction est appelée automatiquement lorsque l'utilisateur clique sur un bouton de l'interface
        /// On change ici de vue pour voir les commandes
        /// </summary>
        private void SwitchToOrderPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToOrderView();
        }

        /// <summary>
        /// Cette fonction est appelée automatiquement lorsque l'utilisateur clique sur un bouton de l'interface
        /// On change ici de vue pour voir les clients
        /// </summary>
        private void SwitchToCustomerPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToCustomerView();
        }

        /// <summary>
        /// Cette fonction est appelée automatiquement lorsque l'utilisateur clique sur un bouton de l'interface
        /// On change ici de vue pour voir les commis
        /// </summary>
        private void SwitchToWorkerPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToWorkerView();
        }

        /// <summary>
        /// Cette fonction est appelée automatiquement lorsque l'utilisateur clique sur un bouton de l'interface
        /// On change ici de vue pour voir les livreurs
        /// </summary>
        private void SwitchToDelivererPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToDelivererView();
        }

        /// <summary>
        /// Cette fonction est appelée automatiquement lorsque l'utilisateur clique sur un bouton de l'interface
        /// On change ici de vue pour voir les statistiques
        /// </summary>
        private void SwitchToStatisticsPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToStatisticsView();
        }

        /// <summary>
        /// Cette fonction est appelée automatiquement lorsque l'utilisateur clique sur un bouton de l'interface
        /// On change ici de vue pour voir les bonus
        /// </summary>
        private void SwitchToBonusPanel(object sender, MouseButtonEventArgs e)
        {
            main_window.SwitchToBonusView();
        }
        #endregion
    }
}
