using RestaurantManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            dishesDataGrid.ItemsSource = tests;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            openDishAddEditWindow("Dodaj danie", null);
        }

        private void openDishAddEditWindow(string title, Dish dish)
        {
            var addEditWindow = new DishAddEditWindow(title, null);
            addEditWindow.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dishesDataGrid.SelectedItems;
            if (selectedItems.Count == 1)
            {
                openDishAddEditWindow("Edytuj danie", null);
            }
        }
    }

    public class Test
    {
        public string Types { get; set; }
        public int ID { get; set; }

        public Test(string a, int b)
        {
            this.Types = a;
            this.ID = b;
        }
    }
}