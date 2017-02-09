using MahApps.Metro.Controls;
using RestaurantManagement.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for DishAddEditWindow.xaml
    /// </summary>
    public partial class DishAddEditWindow : MetroWindow
    {
        private Dish dish;

        public DishAddEditWindow(string title, Dish dish)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Title = title;
            using (var context = new RestaurantDBEntities())
            {
                type_NameComboBox.ItemsSource = context.Types.ToList();
                ingredient_NameComboBox.ItemsSource = context.Ingredients.ToList();
            }
            if (dish == null)
            {
                this.dish = new Dish();
                this.dish.ID = -1;
            }
            else
            {
                this.dish = dish;
                this.dish.selectedType = dish.Types.ToList()[0].Name;
            }
            grid1.DataContext = this.dish;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (Common.ExtractInteger(quantityTextBox.Text) != "" && ingredient_NameComboBox.SelectedIndex != -1)
            {
                quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
                ingredient_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
                dishes_contentsDataGrid.BorderThickness = new Thickness(0, 0, 0, 0);
                var contents = (from dc in dish.Dishes_contents
                                where dc.Ingredient_Name == ingredient_NameComboBox.Text
                                select dc).FirstOrDefault();

                string quantity = Common.ExtractInteger(quantityTextBox.Text);
                if (contents == null)
                {
                    var dc = new Dishes_contents
                    {
                        Quantity = Int32.Parse(quantity),
                        Ingredient_Name = ingredient_NameComboBox.Text,
                        Dish_ID = this.dish.ID
                    };
                    dish.Dishes_contents.Add(dc);
                }
                else
                {
                    contents.Quantity = Int32.Parse(quantity);
                }
                PopulateDishContentsDataGrid();
            }
            else
            {
                quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                ingredient_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool canExit = true;
            if (nameTextBox.Text == "")
            {
                canExit = false;
                nameTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (priceTextBox.Text == "")
            {
                canExit = false;
                priceTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (type_NameComboBox.SelectedIndex == -1)
            {
                canExit = false;
                type_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (dishes_contentsDataGrid.Items.Count == 0)
            {
                canExit = false;
                dishes_contentsDataGrid.BorderBrush = System.Windows.Media.Brushes.Red;
                dishes_contentsDataGrid.BorderThickness = new Thickness(1, 1, 1, 1);
            }
            if (canExit)
            {
                if (this.dish.ID == -1)
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Dishes.Add(this.dish);
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Dishes.Attach(this.dish);
                        context.Entry(this.dish).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                using (var context = new RestaurantDBEntities())
                {
                    var type = (from t in context.Types
                                where t.Name == this.dish.selectedType
                                select t).FirstOrDefault();
                    var dish = context.Dishes.ToList().Find(d => d.ID == this.dish.ID);
                    dish.Types.Clear();
                    dish.Types.Add(type);
                    dish.Dishes_contents.Clear();
                    foreach (var dc in this.dish.Dishes_contents)
                    {
                        dish.Dishes_contents.Add(new Dishes_contents { Dish_ID = dc.Dish_ID, Ingredient_Name = dc.Ingredient_Name, Quantity = dc.Quantity });
                    }
                    context.SaveChanges();
                }
                this.Close();
            }
        }

        private void dishes_contentsDataGrid_ContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            if (dishes_contentsDataGrid.SelectedItem == null) e.Handled = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dishes_contentsDataGrid.SelectedItem != null)
            {
                this.dish.Dishes_contents.Remove((Dishes_contents)dishes_contentsDataGrid.SelectedItem);
                PopulateDishContentsDataGrid();
            }
        }

        private void PopulateDishContentsDataGrid()
        {
            grid1.DataContext = null;
            grid1.DataContext = this.dish;
        }

        private void priceTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                priceTextBox.CaretIndex += 1;
                e.Handled = true;
            }
            if (!Common.IsInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void quantityTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            e.Handled = !Common.IsInteger(e.Text);
        }

        private void nameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            nameTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void type_NameComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            type_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void quantityTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var ingredient = ingredient_NameComboBox.SelectedItem as Ingredient;
            if (ingredient != null) quantityTextBox.Text = Common.ExtractInteger(quantityTextBox.Text) + ingredient.Unit;
        }

        private void ingredient_NameComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var ingredient = ingredient_NameComboBox.SelectedItem as Ingredient;
            if (ingredient != null) quantityTextBox.Text = Common.ExtractInteger(quantityTextBox.Text) + ingredient.Unit;
            ingredient_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }
    }
}