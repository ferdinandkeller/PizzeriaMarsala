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
using System.ComponentModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette vue permet d'éditer des comis
    /// </summary>
    public partial class EditWorkerView : Page
    {
        #region Attributs
        MainWindow main_window;
        Worker worker;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        /// <param name="worker">Le commis à modifier</param>
        public EditWorkerView(MainWindow main_window, Worker worker)
        {
            // On initialise les composants
            InitializeComponent();

            // On définit le contexte
            DataContext = worker;

            // On crééer le titre
            AppTitle.Content = new AppTitleComponent();

            // On sauvegarde la fenêtre principale et le commis
            this.main_window = main_window;
            this.worker = worker;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode executée lorsque l'utilisateur a terminé de modifier le commis
        /// </summary>
        private void EndEdition(object sender, RoutedEventArgs e)
        {
            main_window.SwitchToWorkerView();
        }

        /// <summary>
        /// Méthode exécutée lorsque l'utilisateur veut supprimer le commis
        /// </summary>
        private void DeleteElement(object sender, RoutedEventArgs e)
        {
            Pizzeria.WorkersList.Remove(worker);
            main_window.SwitchToWorkerView();
        }
        #endregion
    }
}
