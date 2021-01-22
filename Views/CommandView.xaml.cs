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
        public Pizzeria InstancePizzeria;

        public CommandView(MainWindow main_window, Pizzeria instance_pizzeria)
        {
            InitializeComponent();

            InstancePizzeria = instance_pizzeria;

            MenuBar.Content = new ViewSwitcherComponent(main_window);

            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Sort, Sort2, Sort,
                New, OpenFile,
                "CommandDataTemplate",
                "PAR NOM", "PAR VILLE", "PAR COMM"
            );
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = InstancePizzeria.ListeCommandes;

            InstancePizzeria.ListeCommandes.Add(new Commande(DateTime.Now, null, null, new Client("Ferdinand", "Keller", "adresse", 631232, DateTime.Now), new Commis("Commisnom", "Commisprenom", "azd", 234234, EtatCommis.surplace, DateTime.Now), new Livreur("azdazd", "azdazd", "azdazd", 12323, EtatLivreur.surplace, "azda")));
        }

        public void Sort()
        {
        }

        public void Sort2()
        {
        }

        public void New()
        {
        }

        public void OpenFile(String file_url)
        {
            Console.WriteLine(file_url);
        }

    }

}
