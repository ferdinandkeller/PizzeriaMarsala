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
    /// Cette vue est utilisée pour modifier des pizzas 
    /// </summary>
    public partial class EditPizzaView : Page
    {
        #region Attributs
        MainWindow main_window;
        Pair<Pizza, int> pizza_pair;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Référence à la fenêtre principale</param>
        /// <param name="pizza_pair">Référene à la paire en cours d'édition</param>
        public EditPizzaView(MainWindow main_window, Pair<Pizza, int> pizza_pair)
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le contexte
            this.DataContext = this;

            // on sauvegarde la référence
            this.main_window = main_window;
            this.pizza_pair = pizza_pair;

            // on charge le titre
            AppTitle.Content = new AppTitleComponent();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Exécutée lorsque l'utilisateur veut finir l'édition
        /// </summary>
        private void EndEdition(object sender, RoutedEventArgs e)
        {
            if (main_window.isEditingOrder)
            {
                main_window.SwitchToEditOrderView(main_window.SelectedOrder);
            }
            else
            {
                main_window.SwitchToCreateOrderView();
            }
        }

        /// <summary>
        /// Exécutée lorsque l'utilisateur veut supprimer la pizza
        /// </summary>
        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            main_window.SelectedOrder.PizzaList.Remove(pizza_pair);
            if (main_window.isEditingOrder)
            {
                main_window.SwitchToEditOrderView(main_window.SelectedOrder);
            }
            else
            {
                main_window.SwitchToCreateOrderView();
            }
        }
        #endregion
    }
}
