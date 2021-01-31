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
    /// Cette vue permet de créer des clients
    /// </summary>
    public partial class CreateCustomerView : Page
    {
        #region Attributs
        MainWindow main_window; // référence à la fenêtre principale

        public string FirstName { get; set; } = "Prénom";
        public string LastName { get; set; } = "Nom";
        public string Address { get; set; } = "42 allee des tilleuls Paris";
        public string PhoneNumber { get; set; } = "0600000000";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        public CreateCustomerView(MainWindow main_window)
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

        #region Méthode
        /// <summary>
        /// Méthode qui s'exécute lorsque l'utilisateur veut crééer un client
        /// </summary>
        private void CreateCustomer(object sender, RoutedEventArgs e)
        {
            Pizzeria.CustomersList.Add(new Customer(LastName, FirstName, Address, long.Parse(PhoneNumber), DateTime.Now));
            if (main_window.isEditingOrder)
            {
                main_window.SwitchToCustomerView();
            } else
            {
                main_window.SwitchToSelectCustomerView();
            }
        }
        #endregion
    }
}
