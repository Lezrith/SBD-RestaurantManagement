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
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : Page
    {
        public EmployeesView()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenEmployeeAddEditWindow("Dodaj pracownika", null);
        }

        private void OpenEmployeeAddEditWindow(string title, Employee order)
        {
            var addEditWindow = new EmployeeAddEditWindow(title, order);
            addEditWindow.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrders = employeesDataGrid.SelectedItems;
            if (selectedOrders.Count == 1)
            {
                OpenEmployeeAddEditWindow("Edytuj pracownika", null);
            }
        }
    }
}