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
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDeliveryAddEditWindow("Dodaj dostawę", null);
        }

        private void OpenDeliveryAddEditWindow(string title, Delivery delivery)
        {
            var addEditWindow = new DeliveryAddEditWindow(title, delivery);
            addEditWindow.ShowDialog();
            PopulateDeliveriesDataGrid();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = deliveriesDataGrid.SelectedItems;
            if (selectedItems.Count == 1)
            {
                OpenDeliveryAddEditWindow("Edytuj dostawę", selectedItems[0] as Delivery);
            }
        }

        public DeliveriesView()
        {
            InitializeComponent();
            PopulateDeliveriesDataGrid();
        }

        private void PopulateDeliveriesDataGrid()
        {
            using (var context = new RestaurantDBEntities())
            {
                deliveriesDataGrid.ItemsSource = context.Deliveries.Include(nameof(Items_in_deliveries)).ToList();
            }
        }
    }
}