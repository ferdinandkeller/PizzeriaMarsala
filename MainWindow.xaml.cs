using System.Collections.Generic;
using System.Windows;

namespace PizzeriaMarsala
{
    public partial class MainWindow : Window
    {
        public static List<string> noms;

        public MainWindow()
        {
            noms = new List<string>();
            noms.Add("Ferdinand");
            noms.Add("Albert");
            noms.Add("Flavie");
            noms.Add("Valentin");
            noms.Add("Carla");

            InitializeComponent();
        }

    }
}
