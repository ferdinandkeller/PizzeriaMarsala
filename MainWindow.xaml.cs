using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
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

        // on conserve les instances des view afin de pouvoir switcher de l'une à l'autre
        private OrderView command_view;
        private CustomerView customer_view;
        private WorkerView worker_view;
        private DelivererView deliverer_view;

        private SelectWorkerView select_worker_view;
        private SelectCustomerView select_customer_view;
        private SelectDelivererView select_deliverer_view;
        private CreateOrderView create_order_view;

        public Worker SelectedWorker;
        public Customer SelectedCustomer;
        public Deliverer SelectedDeliverer;
        public Order SelectedOrder;

        public bool isEditingOrder = true;

        /*
        * Fonction principale du WPF
        */
        public MainWindow()
        {
            // on initialise les composants WPF
            InitializeComponent();

            // on créer chacune des views
            command_view = new OrderView(this);
            customer_view = new CustomerView(this);
            worker_view = new WorkerView(this);
            deliverer_view = new DelivererView(this);

            select_worker_view = new SelectWorkerView(this);
            select_customer_view = new SelectCustomerView(this);
            select_deliverer_view = new SelectDelivererView(this);

            // on charge la view qui contient la liste des commandes
            ViewFrame.Content = command_view;
        }

        // switch view functions
        public void SwitchToCommandView() { ViewFrame.Content = command_view; }
        public void SwitchToCustomerView() { ViewFrame.Content = customer_view; }
        public void SwitchToWorkerView() { ViewFrame.Content = worker_view; }
        public void SwitchToDelivererView() { ViewFrame.Content = deliverer_view; }

        public void SwitchToCreateCustomerView() { ViewFrame.Content = new CreateCustomerView(this); }
        public void SwitchToEditCustomerView(Customer customer) { ViewFrame.Content = new EditCustomerView(this, customer); }

        public void SwitchToCreateWorkerView() { ViewFrame.Content = new CreateWorkerView(this); }
        public void SwitchToEditWorkerView(Worker worker) { ViewFrame.Content = new EditWorkerView(this, worker); }

        public void SwitchToCreateDelivererView() { ViewFrame.Content = new CreateDelivererView(this); }
        public void SwitchToEditDelivererView(Deliverer deliverer) { ViewFrame.Content = new EditDelivererView(this, deliverer); }

        public void SwitchToSelectWorkerView() { ViewFrame.Content = select_worker_view; }
        public void SwitchToSelectCustomerView() { ViewFrame.Content = select_customer_view; }
        public void SwitchToSelectDelivererView() { ViewFrame.Content = select_deliverer_view; }
        public void SwitchToCreateOrderView() { 
            if (create_order_view == null)
            {
                create_order_view = new CreateOrderView(this);
            }
            isEditingOrder = false;
            ViewFrame.Content = create_order_view;
        }
        public void SwitchToCreatePizzaView() {  ViewFrame.Content = new CreatePizzaView(this); }
        public void SwitchToEditPizzaView(Pair<Pizza, int> pizza_pair) {ViewFrame.Content = new EditPizzaView(this, pizza_pair); }
        public void SwitchToCreateBeverageView() { ViewFrame.Content = new CreateBeverageView(this); }
        public void SwitchToEditBeverageView(Pair<Beverage, int> beverage_pair) { ViewFrame.Content = new EditBeverageView(this, beverage_pair); }
        public void SwitchToEditOrderView(Order order) { isEditingOrder = true; SelectedOrder = order; ViewFrame.Content = new EditOrderView(this, order); }
    }
}
