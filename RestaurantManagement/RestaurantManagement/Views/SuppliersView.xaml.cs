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
            PopulateSuppliersDataGrid();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSupplierAddEditWindow("Dodaj dostawcę", null);
        }

        private void OpenSupplierAddEditWindow(string title, Supplier supplier)
        {
            var addEditWindow = new SupplierAddEditWindow(title, supplier);
            addEditWindow.ShowDialog();
            PopulateSuppliersDataGrid();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedSuppliers = suppliersDataGrid.SelectedItems;
            if (selectedSuppliers.Count == 1)
            {
                OpenSupplierAddEditWindow("Edytuj dostawcę", (Supplier)selectedSuppliers[0]);
            }
        }

        private void PopulateSuppliersDataGrid()
        {
            using (var context = new RestaurantDBEntities())
            {
                suppliersDataGrid.ItemsSource = context.Suppliers.ToList();
            }
        }
    }
}