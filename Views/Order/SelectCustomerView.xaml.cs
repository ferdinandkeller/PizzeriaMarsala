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
    public partial class SelectCustomerView : Page
    {
        MainWindow main_window;

        public SelectCustomerView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on sauvegarde l'objet fenêtre
            this.main_window = main_window;

            // on créer le content presenter
            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Pizzeria.SortCustomersByName, Pizzeria.SortCustomersByTown, Pizzeria.SortCustomersByTotalOrders,
                null, null,
                "CustomerDataTemplate",
                "PAR NOM", "PAR VILLE", "PAR CMD TOTALES", (customer) => {
                    main_window.SelectedCustomer = (Customer)customer;
                    main_window.SwitchToCommandView();
                }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.CustomersList;
        }
    }
}
