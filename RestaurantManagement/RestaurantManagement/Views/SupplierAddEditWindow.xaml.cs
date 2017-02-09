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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using RestaurantManagement.Model;
using System.Data.Entity;

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for SupplierAddEditWindow.xaml
    /// </summary>
    public partial class SupplierAddEditWindow : MetroWindow
    {
        private Supplier supplier;
        private Address address;

        public SupplierAddEditWindow(string title, Supplier supplier)
        {
            InitializeComponent();
            this.Title = title;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            if (supplier == null)
            {
                this.supplier = new Supplier();
                this.address = new Address();
            }
            else
            {
                this.supplier = supplier;
                using (var context = new RestaurantDBEntities())
                {
                    this.address = context.Addresses.ToList().Find(address => address.Supplier_Name == this.supplier.Name);
                }
                nameTextBox.IsEnabled = true;
            }
            grid1.DataContext = this.supplier;
            grid2.DataContext = this.address;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool canExit = true;
            using (var context = new RestaurantDBEntities())
            {
                if (nameTextBox.Text == "" || context.Suppliers.FirstOrDefault(s => s.Name == nameTextBox.Text) != null)
                {
                    canExit = false;
                    nameTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                }
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
                using (var context = new RestaurantDBEntities())
                {
                    if (!nameTextBox.IsEnabled)
                    {
                        context.Addresses.Add(this.address);
                        context.SaveChanges();
                        this.supplier.Address = this.address;
                        this.address.Supplier = this.supplier;
                        context.Addresses.Attach(this.address);
                        context.Entry(this.address).State = EntityState.Modified;
                        context.Suppliers.Add(this.supplier);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Suppliers.Attach(this.supplier);
                        context.Entry(this.supplier).State = EntityState.Modified;
                        context.Addresses.Attach(this.address);
                        context.Entry(this.address).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                this.Close();
            }
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
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
                    postalCodeTextBox.Text += "-";
                    postalCodeTextBox.CaretIndex = postalCodeTextBox.Text.Length;
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