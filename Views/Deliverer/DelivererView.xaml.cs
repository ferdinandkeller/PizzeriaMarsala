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
    /// Cette vue permet d'afficher des livreurs
    /// </summary>
    public partial class DelivererView : Page
    {
        #region Attribut
        MainWindow main_window;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        public DelivererView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on sauvegarde l'objet fenêtre
            this.main_window = main_window;

            // on charge la barre des menus
            MenuBar.Content = new ViewSwitcherComponent(main_window);

            // on créer le content presenter
            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Pizzeria.SortDelivererByName, Pizzeria.SortDelivererByTown, Pizzeria.SortDelivererByManagedDeliveryNumber,
                main_window.SwitchToCreateDelivererView, Pizzeria.AddFileToDelivererList,
                "DelivererDataTemplate",
                "PAR NOM", "PAR VILLE", "PAR LIVRAISONS", (o) => { main_window.SwitchToEditDelivererView((Deliverer)o); },
                (o) => {
                    string name = (string)o;
                    Deliverer deliverer = Pizzeria.FindDeliverer(name);
                    if (deliverer != null)
                    {
                        main_window.SwitchToEditDelivererView(deliverer);
                    }
                }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.DeliverersList;
        }
        #endregion
    }
}
