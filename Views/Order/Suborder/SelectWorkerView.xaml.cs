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
    /// Cette vue est utilisée pour sélectionner des client 
    /// </summary>
    public partial class SelectWorkerView : Page
    {
        #region Attributs
        MainWindow main_window;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Référence à la fenêtre principale</param>
        public SelectWorkerView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on sauvegarde l'objet fenêtre
            this.main_window = main_window;

            // on créer le content presenter
            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Pizzeria.SortWorkerByName, Pizzeria.SortWorkerByTown, Pizzeria.SortWorkerByManagedOrderNumber,
                main_window.SwitchToCreateWorkerView, Console.WriteLine,
                "WorkerDataTemplate",
                "PAR NOM", "PAR VILLE", "PAR CMD GEREES", (worker) => {
                    main_window.SelectedWorker = (Worker)worker;
                    main_window.SwitchToSelectCustomerView();
                },
                (o) => {
                    string name = (string)o;
                    Worker worker = Pizzeria.FindWorker(name);
                    if (worker != null)
                    {
                        main_window.SelectedWorker = worker;
                        main_window.SwitchToSelectCustomerView();
                    }
                }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.WorkersList;
        }
        #endregion
    }
}
