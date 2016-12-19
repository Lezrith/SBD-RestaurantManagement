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

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Page
    {
        public MenuView()
        {
            InitializeComponent();
            PopulateMenuDataGrid();
        }

        private void PopulateMenuDataGrid()
        {
            //get menu from database, add it to ObservableList<> and bind it to ItemSource property
            //add bindings to columns, set columns readonly
            ObservableCollection<Test> tests = new ObservableCollection<Test>();
            tests.Add(new Test("haha", 1));
            tests.Add(new Test("hehe", 2));
            tests.Add(new Test("hihi", 3));
            menuDataGrid.ItemsSource = tests;
        }
    }

    public class Test
    {
        public string a { get; set; }
        public int b { get; set; }

        public Test(string a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }
}