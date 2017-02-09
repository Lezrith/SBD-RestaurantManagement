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
    /// Interaction logic for DeliveryAddEditWindow.xaml
    /// </summary>
    public partial class DeliveryAddEditWindow : MetroWindow
    {
        private Delivery delivery;

        public DeliveryAddEditWindow(string title, Delivery delivery)
        {
            InitializeComponent();
            this.Title = title;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            using (var context = new RestaurantDBEntities())
            {
                supplier_NameComboBox.ItemsSource = context.Suppliers.ToList();
                ingredient_NameComboBox.ItemsSource = context.Ingredients.ToList();
            }
            if (delivery == null)
            {
                this.delivery = new Delivery();
                this.delivery.DATE = DateTime.Today;
            }
            else
            {
                this.delivery = delivery;
                supplier_NameComboBox.IsEnabled = false;
                dATEDatePicker.IsEnabled = false;
            }
            grid1.DataContext = this.delivery;
            PopulateItemsInDeliveriesDataGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool canExit = true;
            if (supplier_NameComboBox.SelectedIndex == -1)
            {
                canExit = false;
                supplier_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (costTextBox.Text == "")
            {
                canExit = false;
                costTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (items_in_deliveriesDataGrid.Items.Count == 0)
            {
                canExit = false;
                items_in_deliveriesDataGrid.BorderBrush = System.Windows.Media.Brushes.Red;
                items_in_deliveriesDataGrid.BorderThickness = new Thickness(1, 1, 1, 1);
            }
            if (canExit)
            {
                if (dATEDatePicker.IsEnabled)
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Deliveries.Add(this.delivery);
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Deliveries.Attach(this.delivery);
                        context.Entry(this.delivery).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                using (var context = new RestaurantDBEntities())
                {
                    var del = context.Deliveries.FirstOrDefault(d => d.Supplier_Name == this.delivery.Supplier_Name && d.DATE == this.delivery.DATE);
                    del.Items_in_deliveries.Clear();
                    foreach (var it in this.delivery.Items_in_deliveries)
                    {
                        del.Items_in_deliveries.Add(new Items_in_deliveries { Delivery = del, Ingredient_Name = it.Ingredient_Name, Quantity = it.Quantity });
                    }
                    context.SaveChanges();
                }
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (Common.ExtractInteger(quantityTextBox.Text) != "" && ingredient_NameComboBox.SelectedIndex != -1)
            {
                quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
                ingredient_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
                items_in_deliveriesDataGrid.BorderThickness = new Thickness(0, 0, 0, 0);
                var contents = (from it in delivery.Items_in_deliveries
                                where it.Ingredient_Name == ingredient_NameComboBox.Text
                                select it).FirstOrDefault();

                string quantity = Common.ExtractInteger(quantityTextBox.Text);
                if (contents == null)
                {
                    var it = new Items_in_deliveries
                    {
                        Quantity = Int32.Parse(quantity),
                        Ingredient_Name = ingredient_NameComboBox.Text,
                        Supplier_Name = this.delivery.Supplier_Name,
                        Delivery_Date = this.delivery.DATE
                    };
                    delivery.Items_in_deliveries.Add(it);
                }
                else
                {
                    contents.Quantity = Int32.Parse(quantity);
                }

                PopulateItemsInDeliveriesDataGrid();
            }
            else
            {
                quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                ingredient_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private void PopulateItemsInDeliveriesDataGrid()
        {
            items_in_deliveriesDataGrid.ItemsSource = null;
            items_in_deliveriesDataGrid.ItemsSource = this.delivery.Items_in_deliveries;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (items_in_deliveriesDataGrid.SelectedItem != null)
            {
                this.delivery.Items_in_deliveries.Remove((Items_in_deliveries)items_in_deliveriesDataGrid.SelectedItem);
                PopulateItemsInDeliveriesDataGrid();
            }
        }

        private void items_in_deliveriesDataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (items_in_deliveriesDataGrid.SelectedItem == null) e.Handled = true;
        }

        private void costTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                costTextBox.CaretIndex += 1;
                e.Handled = true;
            }
            if (!Common.IsInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void ingredient_NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ingredient = ingredient_NameComboBox.SelectedItem as Ingredient;
            if (ingredient != null) quantityTextBox.Text = Common.ExtractInteger(quantityTextBox.Text) + ingredient.Unit;
            ingredient_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void quantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            e.Handled = !Common.IsInteger(e.Text);
        }

        private void quantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ingredient = ingredient_NameComboBox.SelectedItem as Ingredient;
            if (ingredient != null) quantityTextBox.Text = Common.ExtractInteger(quantityTextBox.Text) + ingredient.Unit;
        }

        private void supplier_NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            supplier_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }
    }
}