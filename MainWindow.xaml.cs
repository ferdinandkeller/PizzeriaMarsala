using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzeriaMarsala
{
    /*
     * Classe représentant la fenêtre principale
     */ 
    public partial class MainWindow : Window
    {

        /** /!\ CODE A NE PAS SUPPRIMER /!\
        // delegate qui met à jour les variables locales 
        public event PropertyChangedEventHandler PropertyChanged;
        private List<StringWrapper> _Commandes = new List<StringWrapper>();
        public List<StringWrapper> Commandes
        {
            get => _Commandes;
            set
            {
                _Commandes = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Commandes)));
                }
            }
        } */

        // on conserve les instances des view afin de pouvoir switcher de l'une à l'autre
        private CommandView command_view;
        private CustomerView customer_view;
        private WorkerView worker_view;
        private DelivererView deliverer_view;

        /*
        * Fonction principale du WPF
        */
        public MainWindow()
        {
            // on initialise les composants WPF
            InitializeComponent();

            // on créer chacune des views
            command_view = new CommandView(this);
            customer_view = new CustomerView(this);
            worker_view = new WorkerView(this);
            deliverer_view = new DelivererView(this);

            // on charge la view qui contient la liste des commandes
            ViewFrame.Content = command_view;
        }

        public void SwitchToCommandView() { ViewFrame.Content = command_view; }
        public void SwitchToCustomerView() { ViewFrame.Content = customer_view; }
        public void SwitchToWorkerView() { ViewFrame.Content = worker_view; }
        public void SwitchToDelivererView() { ViewFrame.Content = deliverer_view; }

        public void SwitchToCreateCustomerView() {
            ViewFrame.Content = new CreateCustomerView(this);
        }
        public void SwitchToEditCustomerView(Customer customer)
        {
            ViewFrame.Content = new EditCustomerView(this, customer);
        }

        public void SwitchToCreateWorkerView()
        {
            ViewFrame.Content = new CreateWorkerView(this);
        }
        public void SwitchToEditWorkerView(Worker worker)
        {
            ViewFrame.Content = new EditWorkerView(this, worker);
        }

    }
}
