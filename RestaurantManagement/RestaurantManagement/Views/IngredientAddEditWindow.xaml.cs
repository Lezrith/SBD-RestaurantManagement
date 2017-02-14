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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using RestaurantManagement.Model;
using System.Data.Entity;

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for IngredientAddEditWindow.xaml
    /// </summary>
    public partial class IngredientAddEditWindow : MetroWindow
    {
        private Ingredient ingredient;

        public IngredientAddEditWindow(string title, Ingredient ingredient)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Title = title;
            if (ingredient == null)
            {
                this.ingredient = new Ingredient();
            }
            else
            {
                nameTextBox.IsEnabled = false;
                this.ingredient = ingredient;
            }
            grid1.DataContext = this.ingredient;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool canExit = true;
            if (nameTextBox.Text == "")
            {
                canExit = false;
                nameTextBox.BorderBrush = Brushes.Red;
            }
            if (quantity_in_stockTextBox.Text == "")
            {
                canExit = false;
                quantity_in_stockTextBox.BorderBrush = Brushes.Red;
            }
            if (unitTextBox.Text == "")
            {
                canExit = false;
                unitTextBox.BorderBrush = Brushes.Red;
            }

            if (nameTextBox.IsEnabled && canExit)
            {
                using (var context = new RestaurantDBEntities())
                {
                    if (context.Ingredients.SingleOrDefault(i => i.Name == nameTextBox.Text) != null)
                    {
                        canExit = false;
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Warning;
                        MessageBox.Show("Składnik o takiej nazwie już istnieje.", "Uwaga");
                    }
                }
            }
            if (canExit)
            {
                if (nameTextBox.IsEnabled)
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Ingredients.Add(this.ingredient);
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Ingredients.Attach(this.ingredient);
                        context.Entry(this.ingredient).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void unitTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            unitTextBox.BorderBrush = Brushes.Black;
            e.Handled = Common.IsInteger(e.Text);
        }

        private void quantity_in_stockTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            quantity_in_stockTextBox.BorderBrush = Brushes.Black;
            e.Handled = !Common.IsInteger(e.Text);
        }

        private void nameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            nameTextBox.BorderBrush = Brushes.Black;
        }
    }
}