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
    /// Cette vue permet d'éditer des livreurs
    /// </summary>
    public partial class EditDelivererView : Page
    {
        #region Attributs
        MainWindow main_window;
        Deliverer deliverer;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        /// <param name="deliverer">Le livreur à modifier</param>
        public EditDelivererView(MainWindow main_window, Deliverer deliverer)
        {
            // On initialise les composants
            InitializeComponent();

            // On définit le contexte
            DataContext = deliverer;

            // On crééer le titre
            AppTitle.Content = new AppTitleComponent();

            // On sauvegarde la fenêtre principale et le client
            this.main_window = main_window;
            this.deliverer = deliverer;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode executée lorsque l'utilisateur a terminé de modifier le livreur
        /// </summary>
        private void EndEdition(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToDelivererView();
        }

        /// <summary>
        /// Méthode exécutée lorsque l'utilisateur veut supprimer le livreur
        /// </summary>
        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            Pizzeria.DeliverersList.Remove(deliverer);
            main_window.SwitchToDelivererView();
        }
        #endregion
    }
}
