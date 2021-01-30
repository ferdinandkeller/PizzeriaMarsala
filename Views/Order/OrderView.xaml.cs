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
    public partial class OrderView : Page
    {
        MainWindow main_window;

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
                main_window.SwitchToSelectWorkerView, Console.WriteLine,
                "OrderDataTemplate",
                "PAR ID", "PAR PRIX", "PAR TEMPS", (o) => { }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.OrdersList;
        }

    }

}
