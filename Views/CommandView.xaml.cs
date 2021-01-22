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
    public class Cool
    {
        public String Value { get; set; }
        public Cool(String value)
        {
            Value = value;
        }
    }

    public partial class CommandView : Page
    {
        SortableObservableCollection<Cool> list = new SortableObservableCollection<Cool>();

        public CommandView(MainWindow main_window)
        {
            InitializeComponent();

            list.Add(new Cool("salut"));
            list.Add(new Cool("tout"));
            list.Add(new Cool("le"));
            list.Add(new Cool("monde"));

            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Sort, Sort2, Sort,
                New, OpenFile,
                "CoolDataTemplate"
            );
            ListContentPresenter.Content = presenter;
            MenuBar.Content = new ViewSwitcherComponent(main_window);

            presenter.DataContext = list;
        }

        public void Sort()
        {
            list.Sort((Cool c1, Cool c2) => c1.Value.CompareTo(c2.Value));
        }

        public void Sort2()
        {
            list.Sort((Cool c1, Cool c2) => c1.Value.Substring(1).CompareTo(c2.Value.Substring(1)));
        }

        public void New()
        {
            list.Add(new Cool("Trop bien !"));
        }

        public void OpenFile(String file_url)
        {
            Console.WriteLine(file_url);
        }

    }

}
