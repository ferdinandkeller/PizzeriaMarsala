using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // delegate qui met à jour les variables locales 
        public event PropertyChangedEventHandler PropertyChanged;

        // delegate utilié pour effectuer des actions lorsque la fenêtre
        // change de taille
        public delegate void OnWindowResized();
        OnWindowResized WindowResized;

        /*
         * Fonction principale du WPF
         */
        public MainWindow()
        {
            // on initialise les composants WPF
            InitializeComponent();
            // on définit le data context (pour pouvoir accéder
            // aux variables de la classe depuis le fichier XAML)
            this.DataContext = this;

            // on enregistre notre delegate WindowResizeHandler
            this.SizeChanged += (sender, e) => { WindowResized(); };
            WindowResized += ResizeWrapPanel;
        }

        /*
         * Ces deux fonctions font en sorte que le WrapPanel change
         * de taille en même temps que la fenêtre
         */
        private WrapPanel panel = null;

        private int _RectangleWidth = 100;
        public int RectangleWidth
        {
            get => _RectangleWidth;
            set
            {
                _RectangleWidth = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RectangleWidth"));
            }
        }

        private void WrappanelLoaded(object sender, RoutedEventArgs e)
        {
            panel = (WrapPanel)sender;
            ResizeWrapPanel();
        }

        private void ResizeWrapPanel()
        {
            if (panel != null)
            {
                int n = (int)panel.ActualWidth / (350 + 2*5);
                if (n == 0) { n = 1; }
                RectangleWidth = (int)panel.ActualWidth / n - 2*5;
            }
        }
    }
}
