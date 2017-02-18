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
            PopulateEmployeesDataGrid();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenEmployeeAddEditWindow("Dodaj pracownika", null);
        }

        private void OpenEmployeeAddEditWindow(string title, Employee order)
        {
            var addEditWindow = new EmployeeAddEditWindow(title, order);
            addEditWindow.ShowDialog();
            PopulateEmployeesDataGrid();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployees = employeesDataGrid.SelectedItems;
            if (selectedEmployees.Count == 1)
            {
                OpenEmployeeAddEditWindow("Edytuj pracownika", (Employee)selectedEmployees[0]);
            }
        }

        private void PopulateEmployeesDataGrid()
        {
            using (var context = new RestaurantDBEntities())
            {
                employeesDataGrid.ItemsSource = context.Employees.ToList();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployees = employeesDataGrid.SelectedItems;
            if (selectedEmployees.Count == 1)
            {
                using (var context = new RestaurantDBEntities())
                {
                    var id = ((Employee)selectedEmployees[0]).ID;
                    var employee = context.Employees.SingleOrDefault(em => em.ID == id);
                    if (employee != null)
                    {
                        employee.Position = context.Positions.SingleOrDefault(p => p.Name == "Zwolniony");
                        context.SaveChanges();
                    }
                }
                PopulateEmployeesDataGrid();
            }
        }

        private void employeeOfTheMonthButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new RestaurantDBEntities())
            {
                var today = DateTime.Today;
                var result = context.Employee_of_the_month(today).FirstOrDefault();
                string message;
                if (result != null)
                {
                    var employee = context.Employees.FirstOrDefault(em => em.ID == result.ID);
                    message = "Pracownikiem miesiąca " + today.Month.ToString() + "/" + today.Year.ToString() + " jest: " + employee.ID.ToString() + ", " + employee.First_name + " " + employee.Last_name + ".\n"
                        + "Obsługiwał zamówienia na kwotę " + result.S + "zł.";
                }
                else
                {
                    message = "Nie można wyznaczyć pracownika miesiąca ponieważ nie ma jeszcze żadnych zamówień.";
                }

                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(message, "Pracownik miesiąca");
            }
        }
    }
}