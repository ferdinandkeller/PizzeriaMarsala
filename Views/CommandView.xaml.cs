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
    public partial class CommandView : Page
    {
        public CommandView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on charge la barre des menus
            MenuBar.Content = new ViewSwitcherComponent(main_window);

            // on créer le content presenter
            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Pizzeria.SortCommandsByID, Pizzeria.SortCommandsByPrices, Pizzeria.SortCommandsByUrgency,
                New, OpenFile,
                "CommandDataTemplate",
                "PAR ID", "PAR PRIX", "PAR TEMPS", (o) => { }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.ListeCommandes;
        }

        public void Sort()
        {
        }

        public void Sort2()
        {
        }

        public void New()
        {
            Pizzeria.ListeCommandes.Add(new Command(
                new Client("Ferdinand", "Keller", "adresse clien", 06123912, DateTime.Now),
                new Commis("azd", "azd", "azd", 01238, EtatCommis.surplace, DateTime.Now),
                new Livreur("azd", "azd", "azd", 081238, EtatLivreur.surplace, "azd")
            ));
        }

        public void OpenFile(String file_url)
        {
            Console.WriteLine(file_url);
        }

    }

}
