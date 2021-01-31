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
    /// Cette vue permet de créer des commis
    /// </summary>
    public partial class CreateWorkerView : Page
    {
        #region Attributs
        MainWindow main_window;

        public string FirstName { get; set; } = "Prénom";
        public string LastName { get; set; } = "Nom";
        public string Address { get; set; } = "42 allee des tilleuls Paris";
        public string PhoneNumber { get; set; } = "0600000000";
        public WorkerState CurrentWorkerState { get; set; } = WorkerState.SurPlace;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        public CreateWorkerView(MainWindow main_window)
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
        /// Méthode qui s'exécute lorsque l'utilisateur veut crééer un commis
        /// </summary>
        private void CreateWorker(object sender, RoutedEventArgs e)
        {
            Pizzeria.WorkersList.Add(new Worker(LastName, FirstName, Address, long.Parse(PhoneNumber), CurrentWorkerState, DateTime.Now));
            if (main_window.isEditingOrder)
            {
                main_window.SwitchToWorkerView();
            }
            else
            {
                main_window.SwitchToSelectWorkerView();
            }
        }
        #endregion
    }
}
