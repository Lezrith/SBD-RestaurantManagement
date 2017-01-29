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
    /// Interaction logic for SuppliersView.xaml
    /// </summary>
    public partial class SuppliersView : Page
    {
        public SuppliersView()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSupplierAddEditWindow("Dodaj dostawcę", null);
        }

        private void OpenSupplierAddEditWindow(string title, Supplier order)
        {
            var addEditWindow = new SupplierAddEditWindow(title, order);
            addEditWindow.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrders = suppliersDataGrid.SelectedItems;
            if (selectedOrders.Count == 1)
            {
                OpenSupplierAddEditWindow("Edytuj dostawcę", null);
            }
        }
    }
}