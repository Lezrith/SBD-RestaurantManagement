using RestaurantManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;
using System.Data.Entity;

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
            PopulateMenuDataGrid(null);
        }

        private void PopulateMenuDataGrid(bool? inMenu)
        {
            using (var context = new RestaurantDBEntities())
            {
                var dishes = context.Dishes.Include("Types").Include("Dishes_contents").ToList();
                if (inMenu != null) dishes = dishes.FindAll(d => d.In_menu == inMenu);
                if (dishesDataGrid != null) dishesDataGrid.ItemsSource = dishes;
            }
            addButton.Content = "Dodaj";
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            openDishAddEditWindow("Dodaj danie", null);
        }

        private void openDishAddEditWindow(string title, Dish dish)
        {
            var addEditWindow = new DishAddEditWindow(title, dish);
            addEditWindow.ShowDialog();
            PopulateMenuDataGrid(null);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dishesDataGrid.SelectedItems;
            if (selectedItems.Count == 1)
            {
                openDishAddEditWindow("Edytuj danie", (Dish)selectedItems[0]);
            }
        }

        private void toogleInMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dishesDataGrid.SelectedItems;
            if (selectedItems.Count > 0)
            {
                using (var context = new RestaurantDBEntities())
                {
                    foreach (var i in selectedItems)
                    {
                        var result = context.Dishes.SingleOrDefault(d => d.ID == ((Dish)i).ID);
                        if (result != null)
                        {
                            result.In_menu = !result.In_menu;
                        }
                    }
                    context.SaveChanges();
                }
            }
            PopulateMenuDataGrid(null);
        }

        private void inMenuComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (inMenuComboBox.SelectedIndex)
            {
                case 0:
                    PopulateMenuDataGrid(null);
                    break;

                case 1:
                    PopulateMenuDataGrid(true);
                    break;

                case 2:
                    PopulateMenuDataGrid(false);
                    break;

                default:
                    break;
            }
        }
    }

    [ValueConversion(typeof(ICollection<Model.Type>), typeof(string))]
    public class TypeConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            var data = (ICollection<Model.Type>)value;
            return data.ToList()[0].Name;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}