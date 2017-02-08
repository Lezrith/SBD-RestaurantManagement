using MahApps.Metro.Controls;
using RestaurantManagement.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
    /// Interaction logic for EmployeeAddEditWindow.xaml
    /// </summary>
    public partial class EmployeeAddEditWindow : MetroWindow
    {
        private Employee employee;
        private Address address;

        public EmployeeAddEditWindow(string title, Employee employee)
        {
            InitializeComponent();
            this.Title = title;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            using (var context = new RestaurantDBEntities())
            {
                position_NameComboBox.ItemsSource = context.Positions.ToList();
            }
            if (employee == null)
            {
                this.employee = new Employee() { ID = -1 };
                this.address = new Address();
            }
            else
            {
                this.employee = employee;
                using (var context = new RestaurantDBEntities())
                {
                    this.address = context.Addresses.ToList().Find(address => address.Employee_ID == this.employee.ID);
                }
            }
            grid1.DataContext = this.employee;
            grid2.DataContext = this.address;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.employee.ID == -1)
            {
                try
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Addresses.Add(this.address);
                        context.SaveChanges();
                    }
                    using (var context = new RestaurantDBEntities())
                    {
                        this.employee.Hashed_password = Common.HashString(passwordTextBox.Password);
                        this.employee.Address = this.address;
                        this.address.Employee = this.employee;
                        context.Employees.Add(this.employee);
                        context.Addresses.Attach(this.address);
                        context.Entry(this.address).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
            else
            {
                using (var context = new RestaurantDBEntities())
                {
                    if (passwordTextBox.Password != String.Empty) this.employee.Hashed_password = Common.HashString(passwordTextBox.Password);
                    context.Addresses.Attach(this.address);
                    context.Entry(this.address).State = EntityState.Modified;
                    context.Employees.Attach(this.employee);
                    context.Entry(this.employee).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}