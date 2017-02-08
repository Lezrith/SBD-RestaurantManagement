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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : Page
    {
        public OrdersView()
        {
            InitializeComponent();
            PopulateOrdersDataGrid();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenOrderAddEditWindow("Dodaj zamówienie", null);
        }

        private void OpenOrderAddEditWindow(string title, Order order)
        {
            var window = (MainWindow)Window.GetWindow(this);
            var addEditWindow = new OrderAddEditWindow(title, order, window.user);
            addEditWindow.ShowDialog();
            PopulateOrdersDataGrid();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrders = ordersDataGrid.SelectedItems;
            if (selectedOrders.Count == 1)
            {
                OpenOrderAddEditWindow("Edytuj zamówienie", selectedOrders[0] as Order);
            }
        }

        private void PopulateOrdersDataGrid()
        {
            using (var context = new RestaurantDBEntities())
            {
                var orders = context.Orders.Include("Ordering_dishes").ToList();
                foreach (var o in orders)
                {
                    o.fullPrice = 0;
                    foreach (var od in o.Ordering_dishes)
                    {
                        o.fullPrice += od.Dish.Price * od.Quantity;
                    }
                }
                ordersDataGrid.ItemsSource = orders;
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrders = ordersDataGrid.SelectedItems;
            if (selectedOrders.Count == 1)
            {
                using (var context = new RestaurantDBEntities())
                {
                    var toDelete = (Order)selectedOrders[0];
                    var or = context.Orders.ToList().Find(o => o.Table_Number == toDelete.Table_Number && o.Employee_ID == toDelete.Employee_ID && o.TIME.Date == toDelete.TIME.Date && o.TIME.Hour == toDelete.TIME.Hour && o.TIME.Minute == toDelete.TIME.Minute);
                    or.Ordering_dishes.Clear();
                    context.Orders.Remove(or);
                    context.SaveChanges();
                }
            }
            PopulateOrdersDataGrid();
        }
    }
}