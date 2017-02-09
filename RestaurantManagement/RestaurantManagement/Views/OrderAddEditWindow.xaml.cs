using MahApps.Metro.Controls;
using RestaurantManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for OrderAddEditWindow.xaml
    /// </summary>
    public partial class OrderAddEditWindow : MetroWindow
    {
        private Order order;
        private Employee user;

        public OrderAddEditWindow(string title, Order order, Employee user)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Title = title;
            using (var context = new RestaurantDBEntities())
            {
                table_NumberComboBox.ItemsSource = context.Tables.ToList();
                dish_NameComboBox.ItemsSource = context.Dishes.ToList();
                payment_method_NameComboBox.ItemsSource = context.Payment_methods.ToList();
            }
            this.user = user;
            if (order == null)
            {
                this.order = new Order();
                this.order.Employee_ID = this.user.ID;
                this.order.TIME = DateTime.Now;
            }
            else
            {
                this.order = order;
                table_NumberComboBox.IsEnabled = false;
            }
            grid1.DataContext = this.order;
            PopulateOrderingDishesDataGrid();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool canExit = true;
            if (table_NumberComboBox.SelectedIndex == -1)
            {
                canExit = false;
                table_NumberComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (payment_method_NameComboBox.SelectedIndex == -1)
            {
                canExit = false;
                payment_method_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (ordering_dishesDataGrid.Items.Count == 0)
            {
                canExit = false;
                ordering_dishesDataGrid.BorderBrush = System.Windows.Media.Brushes.Red;
                ordering_dishesDataGrid.BorderThickness = new Thickness(1, 1, 1, 1);
            }
            if (canExit)
            {
                var tmp = new List<Ordering_dishes>();
                tmp.AddRange(this.order.Ordering_dishes);
                if (table_NumberComboBox.IsEnabled)
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        this.order.Ordering_dishes.Clear();
                        context.Orders.Add(this.order);
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        this.order.Ordering_dishes.Clear();
                        var or = context.Orders.ToList().Find(o => o.Table_Number == this.order.Table_Number && o.Employee_ID == this.order.Employee_ID && o.TIME.Date == this.order.TIME.Date && o.TIME.Hour == this.order.TIME.Hour && o.TIME.Minute == this.order.TIME.Minute);
                        or.Payment_method_Name = this.order.Payment_method_Name;
                        or.Ordering_dishes.Clear();
                        context.SaveChanges();
                    }
                }
                using (var context = new RestaurantDBEntities())
                {
                    var or = context.Orders.ToList().Find(o => o.Table_Number == this.order.Table_Number && o.Employee_ID == this.order.Employee_ID && o.TIME.Date == this.order.TIME.Date && o.TIME.Hour == this.order.TIME.Hour && o.TIME.Minute == this.order.TIME.Minute);
                    or.Ordering_dishes.Clear();
                    context.SaveChanges();
                    foreach (var od in tmp)
                    {
                        or.Ordering_dishes.Add(new Ordering_dishes { Dish_ID = od.Dish_ID, Order = or, Quantity = od.Quantity });
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

        private void PopulateOrderingDishesDataGrid()
        {
            ordering_dishesDataGrid.ItemsSource = null;
            using (var context = new RestaurantDBEntities())
            {
                var orderingDishes = (from od in this.order.Ordering_dishes
                                      join d in context.Dishes on od.Dish_ID equals d.ID
                                      select new { ID = d.ID, Name = d.Name, Quantity = od.Quantity, PartialSum = d.Price * od.Quantity }).ToList();
                ordering_dishesDataGrid.ItemsSource = orderingDishes;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (quantityTextBox.Text != "" && dish_NameComboBox.SelectedItem != null)
            {
                quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
                dish_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
                ordering_dishesDataGrid.BorderThickness = new Thickness(0, 0, 0, 0);
                var contents = (from od in order.Ordering_dishes
                                where od.Dish_ID == ((Dish)dish_NameComboBox.SelectedItem).ID
                                select od).FirstOrDefault();

                if (contents == null)
                {
                    var od = new Ordering_dishes
                    {
                        Quantity = Int32.Parse(quantityTextBox.Text),
                        Order = this.order,
                        Dish_ID = ((Dish)dish_NameComboBox.SelectedItem).ID
                    };
                    order.Ordering_dishes.Add(od);
                }
                else
                {
                    contents.Quantity = Int32.Parse(quantityTextBox.Text);
                }
                PopulateOrderingDishesDataGrid();
            }
            else
            {
                quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                dish_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ordering_dishesDataGrid.SelectedItem != null)
            {
                var toDelete = new { ID = 1, Name = "", Quantity = 1, PartialSum = 1d };
                toDelete = Common.Cast(toDelete, ordering_dishesDataGrid.SelectedItem);
                this.order.Ordering_dishes.Remove(this.order.Ordering_dishes.FirstOrDefault(od => od.Dish_ID == toDelete.ID));
                PopulateOrderingDishesDataGrid();
            }
        }

        private void table_NumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            table_NumberComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void payment_method_NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            payment_method_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void quantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            quantityTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            e.Handled = !Common.IsInteger(e.Text);
        }

        private void dish_NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dish_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }
    }
}