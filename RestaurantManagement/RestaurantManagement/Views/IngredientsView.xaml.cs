using RestaurantManagement.Model;
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

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for IngredientsView.xaml
    /// </summary>
    public partial class IngredientsView : Page
    {
        public IngredientsView()
        {
            InitializeComponent();
            PopulateIngredientsDataGrid();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenIngredientAddEditWindow("Dodaj składnik", null);
        }

        private void OpenIngredientAddEditWindow(string title, Ingredient ingredient)
        {
            var addEditWindow = new IngredientAddEditWindow(title, ingredient);
            addEditWindow.ShowDialog();
            PopulateIngredientsDataGrid();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = ingredientsDataGrid.SelectedItems;
            if (selectedItems.Count == 1)
            {
                OpenIngredientAddEditWindow("Edytuj składnik", selectedItems[0] as Ingredient);
            }
        }

        private void PopulateIngredientsDataGrid()
        {
            using (var context = new RestaurantDBEntities())
            {
                ingredientsDataGrid.ItemsSource = context.Ingredients.ToList();
            }
        }
    }
}