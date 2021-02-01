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
    /// Cette vue est utilisée pour créer des pizzas 
    /// </summary>
    public partial class CreatePizzaView : Page
    {
        #region Attributs
        MainWindow main_window;

        public PizzaType Type { get; set; } = PizzaType.Margherita;
        public PizzaSize Size { get; set; } = PizzaSize.Moyenne;
        public int Quantity { get; set; } = 1;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Référence à la fenêtre principale</param>
        public CreatePizzaView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le contexte
            this.DataContext = this;

            // on sauvegarde la référence à la fenêtre
            this.main_window = main_window;

            // on charge le titre
            AppTitle.Content = new AppTitleComponent();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Exéctuer lorsque l'utilisateur veut créer la pizza
        /// </summary>
        private void CreatePizza(object sender, RoutedEventArgs e)
        {
            main_window.SelectedOrder.PizzaList.Add(new Pair<Pizza, int>(new Pizza(Type, Size), Quantity));
            if (main_window.isEditingOrder)
            {
                main_window.SwitchToEditOrderView(main_window.SelectedOrder);
            } else
            {
                main_window.SwitchToCreateOrderView();
            }
        }
        #endregion
    }
}
