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
using System.Collections.Specialized;
using Microsoft.Win32;

namespace PizzeriaMarsala
{

    /*
     * Le DataTemplateSelector permet de choisir le bon template en fonction de ce que l'on essaye d'afficher
     */
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
            return (element.FindResource(Template) as DataTemplate);
        }
    }

    /*
     * Classe représentation la page
     */ 
    public partial class ListContentPresenterComponent : Page
    {

        public delegate void Action();
        public delegate void OpenFileAction(String file_url);
        public delegate void ObjectClicked(object el);

        public Action SortMethod1;
        public Action SortMethod2;
        public Action SortMethod3;
        public Action NewElement;
        public OpenFileAction OpenFile;
        public ObjectClicked ObjectClickedFunc;

        public string NameSortMenu1 { get; set; }
        public string NameSortMenu2 { get; set; }
        public string NameSortMenu3 { get; set; }

        public ListContentPresenterComponent(
            Action sort_method_1, Action sort_method_2, Action sort_method_3,
            Action new_element, OpenFileAction open_file,
            string data_template,
            string name_sort_menu_1, string name_sort_menu_2, string name_sort_menu_3,
            ObjectClicked object_clicked_function
        )
        {
            InitializeComponent();

            // set context
            this.DataContext = this;

            ObjectClickedFunc = object_clicked_function;

            // on sauvegarde les paramêtres de la fênetre
            SortMethod1 = sort_method_1;
            SortMethod2 = sort_method_2;
            SortMethod3 = sort_method_3;
            NewElement = new_element;
            OpenFile = open_file;

            ((INotifyCollectionChanged)ItemsControlList.Items).CollectionChanged += (s, e) =>
            {
                ResizeWrapPanelElements();
            };

            // save menu's names
            NameSortMenu1 = name_sort_menu_1;
            NameSortMenu2 = name_sort_menu_2;
            NameSortMenu3 = name_sort_menu_3;

            // on enregistre notre delegate WindowResized
            this.SizeChanged += (sender, e) => { ResizeWrapPanelElements(); };

            // on affiche le titre
            AppTitle.Content = new AppTitleComponent();

            // on charge le bon data template
            ItemsControlList.ItemTemplateSelector = new TemplateSelector(data_template);
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
        }

        // fonction qui est exécutée à chaque fois qu'on veut changer la taille des éléments du wrap panel
        public void ResizeWrapPanelElements()
        {
            if (CommandWrapPanel != null)
            {
                double prefered_element_size = 250;
                double element_margin = 5;
                int n = (int)(CommandWrapPanel.ActualWidth / (prefered_element_size + 2 * element_margin));
                if (CommandWrapPanel.Children.Count < n) { n = CommandWrapPanel.Children.Count; }
                if (n == 0) { n = 1; }
                double desired_width = CommandWrapPanel.ActualWidth / n;
                foreach (UIElement element in CommandWrapPanel.Children)
                {
                    ((ContentPresenter)element).Width = desired_width;
                }
            }
        }

        /*
         * Les fonctions suivantes vont être executés lorsque l'utilisateur va clicker sur les boutons de l'interface
         */
        private void Tri1Click(object sender, MouseButtonEventArgs e) { SortMethod1(); }
        private void Tri2Click(object sender, MouseButtonEventArgs e) { SortMethod2(); }
        private void Tri3Click(object sender, MouseButtonEventArgs e) {  SortMethod3(); }

        private void AjouterClick(object sender, MouseButtonEventArgs e) { NewElement(); }
        private void ImporterClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog file_dialog = new OpenFileDialog();
            if (file_dialog.ShowDialog() == true)
            {
                OpenFile(file_dialog.FileName);
            }
        }

        private void ElementClicked(object sender, MouseButtonEventArgs e)
        {
            ObjectClickedFunc(((Border)sender).Tag);
        }
    }
}
