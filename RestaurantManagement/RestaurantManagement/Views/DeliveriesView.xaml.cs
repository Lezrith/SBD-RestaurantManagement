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
using MahApps.Metro;
using RestaurantManagement.Model;

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for DeliveriesView.xaml
    /// </summary>
    public partial class DeliveriesView : Page
    {
        private class Test3
        {
            public Test3(string v1, int v2)
            {
                this.Supplier_Name = v1;
                this.Cost = v2;
            }

            public int Cost { get; set; }
            public string Supplier_Name { get; set; }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDeliveryAddEditWindow("Dodaj dostawę", null);
        }

        private void OpenDeliveryAddEditWindow(string title, Delivery delivery)
        {
            var addEditWindow = new DeliveryAddEditWindow(title, delivery);
            addEditWindow.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = deliveriesDataGrid.SelectedItems;
            if (selectedItems.Count == 1)
            {
                OpenDeliveryAddEditWindow("Edytuj dostawę", null);
            }
        }

        public DeliveriesView()
        {
            InitializeComponent();
            PopulateDeliveriesDataGrid();
        }

        private void PopulateDeliveriesDataGrid()
        {
            //get menu from database, add it to ObservableList<> and bind it to ItemSource property
            //add bindings to columns, set columns readonly
            ObservableCollection<Test3> tests = new ObservableCollection<Test3>();
            tests.Add(new Test3("haha", 1));
            tests.Add(new Test3("hehe", 2));
            tests.Add(new Test3("hihi", 3));
            deliveriesDataGrid.ItemsSource = tests;
        }
    }
}