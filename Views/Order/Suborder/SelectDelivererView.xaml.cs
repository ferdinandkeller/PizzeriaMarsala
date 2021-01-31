﻿using System;
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
    public partial class SelectDelivererView : Page
    {
        MainWindow main_window;

        public SelectDelivererView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on sauvegarde l'objet fenêtre
            this.main_window = main_window;

            // on créer le content presenter
            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Pizzeria.SortDelivererByName, Pizzeria.SortDelivererByTown, Pizzeria.SortDelivererByManagedDeliveryNumber,
                main_window.SwitchToCreateDelivererView, Console.WriteLine,
                "DelivererDataTemplate",
                "PAR NOM", "PAR VILLE", "PAR LIVRAISONS", (deliverer) => {
                    main_window.SelectedDeliverer = (Deliverer)deliverer;
                    main_window.SwitchToCreateOrderView();
                }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.DeliverersList;
        }
    }
}