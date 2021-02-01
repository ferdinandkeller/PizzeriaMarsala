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
    /// Cette vue est utilisée pour créer des boissons 
    /// </summary>
    public partial class CreateBeverageView : Page
    {
        #region Attributs
        MainWindow main_window;

        public BeverageType Type { get; set; } = BeverageType.Coca;
        public int Volume { get; set; } = 50;
        public int Quantity { get; set; } = 1;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Référence à la fenêtre principale</param>
        public CreateBeverageView(MainWindow main_window)
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
        /// Exécutée lorsque l'utilisateur veut créer la boisson
        /// </summary>
        private void CreateBeverage(object sender, RoutedEventArgs e)
        {
            main_window.SelectedOrder.BeverageList.Add(new Pair<Beverage, int>(new Beverage(Type, Volume), Quantity));
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
