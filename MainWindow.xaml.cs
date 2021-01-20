﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

    /*
     * Cette classe permet d'englober un string
     * Sans cela, la modification de la variable ne se fait pas
     */ 
    public class StringWrapper
    {
        public String Value { get; set; }
        public StringWrapper(string value)
        {
            Value = value;
        }
    }

    /*
     * Classe représentant la fenêtre principale
     */ 
    public partial class MainWindow : Window
    {

        // Ce delegate est utilisé pour effectuer des actions lorsque la fenêtre change de taille
        public delegate void OnWindowResized();
        OnWindowResized WindowResized;

        /** /!\ CODE A NE PAS SUPPRIMER
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

        // Une ObservableCollection<> est un type de Liste sur lequel on peut écouter un évênement de modification
        // Celle-ci contient une liste de commande (en l'occurence de noms pour le moments)
        public ObservableCollection<StringWrapper> ListeCommandes { get; set; } = new ObservableCollection<StringWrapper>();

        /*
        * Fonction principale du WPF
        */
        public MainWindow()
        {
            // on initialise les composants WPF
            InitializeComponent();
            // on définit le data context (pour pouvoir accéder aux variables de la classe depuis le fichier XAML)
            this.DataContext = this;

            // on crééer des commandes
            List<String> Noms = new List<String>() { "Ferdinand", "Roxane", "Stephanie", "Antoine", "Amelie", "Mark", "Marcel" };
            Noms.ForEach(nom => { ListeCommandes.Add(new StringWrapper(nom)); });

            // on enregistre notre delegate WindowResized
            this.SizeChanged += (sender, e) => { WindowResized(); };
            WindowResized += ResizeWrapPanelElements;
        }

        /*
         * Ces deux fonctions font en sorte que le WrapPanel change
         * de taille en même temps que la fenêtre
         */
        // variable qui contient le WrapPanel contenant les commandes
        private WrapPanel CommandWrapPanel = null;

        // fonction qui s'execute lorsque le wrappanel a finit de charger
        private void WrapPanelLoaded(object sender, RoutedEventArgs evnt)
        {
            if (sender.GetType() == typeof(WrapPanel)) {
                CommandWrapPanel = (WrapPanel)sender;
            }
            ResizeWrapPanelElements();
            ListeCommandes.CollectionChanged += (s, e) => { ResizeWrapPanelElements(); };
        }

        // fonction qui est exécutée à chaque fois qu'on veut changer la taille des éléments du wrap panel
        private void ResizeWrapPanelElements()
        {
            if (CommandWrapPanel != null)
            {
                int n = (int)CommandWrapPanel.ActualWidth / (350 + 2*5);
                if (n == 0) { n = 1; }
                double desired_width = CommandWrapPanel.ActualWidth/n;
                foreach (UIElement element in CommandWrapPanel.Children)
                {
                    ((ContentPresenter)element).Width = desired_width;
                }
            }
        }

        // Cette fonction temporaire rajoute un élément à la liste des commandes
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListeCommandes.Add(new StringWrapper("Salut !"));
        }
    }
}