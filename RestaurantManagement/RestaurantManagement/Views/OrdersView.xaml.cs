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
            var addEditWindow = new OrderAddEditWindow(title, order);
            addEditWindow.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrders = ordersDataGrid.SelectedItems;
            if (selectedOrders.Count == 1)
            {
                OpenOrderAddEditWindow("Edytuj zamówienie", null);
            }
        }

        private void PopulateOrdersDataGrid()
        {
            //get menu from database, add it to ObservableList<> and bind it to ItemSource property
            //add bindings to columns, set columns readonly
            var tests = new ObservableCollection<Test2>();
            tests.Add(new Test2("haha", 1));
            tests.Add(new Test2("hehe", 2));
            tests.Add(new Test2("hihi", 3));
            ordersDataGrid.ItemsSource = tests;
        }
    }

    public class Test2
    {
        public string Payment_method_Name { get; set; }
        public int Employee_ID { get; set; }

        public Test2(string a, int b)
        {
            this.Payment_method_Name = a;
            this.Employee_ID = b;
        }
    }
}