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
    /// Cette vue permet d'afficher des commandes
    /// </summary>
    public partial class OrderView : Page
    {
        #region Attributs
        MainWindow main_window;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Référence à la fenêtre principale</param>
        public OrderView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on sauvegarde l'objet main window
            this.main_window = main_window;

            // on charge la barre des menus
            MenuBar.Content = new ViewSwitcherComponent(main_window);

            // on créer le content presenter
            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Pizzeria.SortOrdersByID, Pizzeria.SortOrdersByPrices, Pizzeria.SortOrdersByUrgency,
                main_window.SwitchToSelectWorkerView, Pizzeria.AddFileToOrderList,
                "OrderDataTemplate",
                "PAR ID", "PAR PRIX", "PAR PRIORITE", (o) => { main_window.SwitchToEditOrderView((Order)o); },
                (o) => {
                    long id;
                    if (long.TryParse((string)o, out id))
                    {
                        Order order = Pizzeria.FindOrder(id);
                        if (order != null)
                        {
                            main_window.SwitchToEditOrderView(order);
                        }
                    }
                }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.OrdersList;
        }
        #endregion
    }
}
