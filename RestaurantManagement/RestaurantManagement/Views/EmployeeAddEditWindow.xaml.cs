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
            bool canExit = true;
            if (first_nameTextBox.Text == "")
            {
                canExit = false;
                first_nameTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (last_nameTextBox.Text == "")
            {
                canExit = false;
                last_nameTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (!Common.IsValidEmail(email_addressTextBox.Text))
            {
                canExit = false;
                email_addressTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (phone_numberTextBox.Text.Length != 9)
            {
                canExit = false;
                phone_numberTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (position_NameComboBox.SelectedIndex == -1)
            {
                canExit = false;
                position_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (this.employee.ID == -1 && passwordTextBox.Password == "")
            {
                canExit = false;
                passwordTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (streetTextBox.Text == "")
            {
                canExit = false;
                streetTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (nUMBERTextBox.Text == "")
            {
                canExit = false;
                nUMBERTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (postalCodeTextBox.Text.Length != 6)
            {
                canExit = false;
                postalCodeTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (cityTextBox.Text == "")
            {
                canExit = false;
                cityTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (canExit)
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
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void last_nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            last_nameTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void first_nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            first_nameTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void email_addressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            email_addressTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void phone_numberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            phone_numberTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            e.Handled = !Common.IsInteger(e.Text);
        }

        private void position_NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            position_NameComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void passwordTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            passwordTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void streetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            streetTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void nUMBERTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            nUMBERTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            e.Handled = !Common.IsInteger(e.Text);
        }

        private void flat_numberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Common.IsInteger(e.Text);
        }

        private void postalCodeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            postalCodeTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
            if (Common.IsInteger(e.Text))
            {
                if (postalCodeTextBox.Text.Length == 1)
                {
                    postalCodeTextBox.Text += e.Text;
                    postalCodeTextBox.Text += "-";
                    postalCodeTextBox.CaretIndex = postalCodeTextBox.Text.Length;
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cityTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }
    }
}