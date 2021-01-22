using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class TemplateSelector : DataTemplateSelector
    {
        String Template;

        public TemplateSelector(String template)
        {
            Template = template;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            return element.FindResource(Template) as DataTemplate;
        }
    }

    public partial class ListContentPresenterComponent : Page
    {

        // Ce delegate est utilisé pour effectuer des actions lorsque la fenêtre change de taille
        public delegate void OnWindowResized();
        OnWindowResized WindowResized;

        // Une ObservableCollection<> est un type de Liste sur lequel on peut écouter un évênement de modification
        // Celle-ci contient une liste de commande (en l'occurence de noms pour le moments)
        public ObservableCollection<String> ListeCommandes { get; set; } = new ObservableCollection<String>();

        public ListContentPresenterComponent(MainWindow main_window)
        {
            InitializeComponent();

            // on définit le data context (pour pouvoir accéder aux variables de la classe depuis le fichier XAML)
            this.DataContext = this;

            // on crééer des commandes
            ListeCommandes = new ObservableCollection<String>() { "Ferdinand", "Roxane", "Stephanie", "Antoine", "Amelie", "Mark", "Marcel" };

            // on enregistre notre delegate WindowResized
            this.SizeChanged += (sender, e) => { WindowResized(); };
            WindowResized += ResizeWrapPanelElements;

            AppTitle.Content = new AppTitleComponent();

            ItemsControlList.ItemTemplateSelector = new TemplateSelector("CommandDataTemplate");
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
            if (sender.GetType() == typeof(WrapPanel))
            {
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
                double prefered_element_size = 250;
                double element_margin = 5;
                int n = (int)(CommandWrapPanel.ActualWidth / (prefered_element_size + 2 * element_margin));
                if (n == 0) { n = 1; }
                double desired_width = CommandWrapPanel.ActualWidth / n;
                foreach (UIElement element in CommandWrapPanel.Children)
                {
                    ((ContentPresenter)element).Width = desired_width;
                }
            }
        }
    }
}
