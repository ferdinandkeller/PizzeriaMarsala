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
    /// Cette vue permet de créer des livreurs
    /// </summary>
    public partial class CreateDelivererView : Page
    {
        #region Attributs
        MainWindow main_window;

        public string FirstName { get; set; } = "Prénom";
        public string LastName { get; set; } = "Nom";
        public string Address { get; set; } = "42 allee des tilleuls Paris";
        public string PhoneNumber { get; set; } = "0600000000";
        public DelivererState CurrentDelivererState { get; set; } = DelivererState.SurPlace;
        public String VehicleType { get; set; } = "scooter";
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        public CreateDelivererView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le contexte
            this.DataContext = this;

            // on sauvegarde une référence à la fenêtre principale
            this.main_window = main_window;

            // on affiche le titre
            AppTitle.Content = new AppTitleComponent();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur veut crééer un livreur
        /// </summary>
        private void CreateDeliverer(object sender, RoutedEventArgs e)
        {
            Pizzeria.DeliverersList.Add(new Deliverer(LastName, FirstName, Address, long.Parse(PhoneNumber), CurrentDelivererState, VehicleType));
            if (main_window.isEditingOrder)
            {
                main_window.SwitchToDelivererView();
            }
            else
            {
                main_window.SwitchToSelectDelivererView();
            }
        }
        #endregion
    }
}
