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
    /// Cette vue est utilisée pour modifier des boissons 
    /// </summary>
    public partial class EditBeverageView : Page
    {
        #region Attributs
        MainWindow main_window;
        Pair<Beverage, int> beverage_pair;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Référence à la fenêtre principale</param>
        /// <param name="beverage_pair">Référene à la paire en cours d'édition</param>
        public EditBeverageView(MainWindow main_window, Pair<Beverage, int> beverage_pair)
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le contexte
            this.DataContext = beverage_pair;

            // on sauvegarde la référence
            this.main_window = main_window;
            this.beverage_pair = beverage_pair;

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
        /// Exécutée lorsque l'utilisateur veut supprimer la boisson
        /// </summary>
        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            main_window.SelectedOrder.BeverageList.Remove(beverage_pair);
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
