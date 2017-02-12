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
using RestaurantManagement.Model;
using MahApps.Metro.Controls;
using System.Data.Entity;

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for ReservationAddEdit.xaml
    /// </summary>
    public partial class ReservationAddEditWindow : MetroWindow
    {
        private Reservation reservation;

        public ReservationAddEditWindow(string title, Reservation reservation)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Title = title;
            using (var context = new RestaurantDBEntities())
            {
                table_NumberComboBox.ItemsSource = context.Tables.ToList();
            }
            if (reservation == null)
            {
                this.reservation = new Reservation();
                this.reservation.DATE = DateTime.Today;
                this.reservation.START = DateTime.Now.TimeOfDay;
                this.reservation.End = DateTime.Now.AddMinutes(5).TimeOfDay;
            }
            else
            {
                this.reservation = reservation;
                dATEDatePicker.IsEnabled = false;
                table_NumberComboBox.IsEnabled = false;
                sTARTTimePicker.IsEnabled = false;
            }
            grid1.DataContext = this.reservation;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            bool canExit = true;
            if (nameTextBox.Text == "")
            {
                canExit = false;
                nameTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (table_NumberComboBox.SelectedIndex == -1)
            {
                canExit = false;
                table_NumberComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            if (this.reservation.START >= this.reservation.End)
            {
                canExit = false;
                sTARTTimePicker.BorderBrush = System.Windows.Media.Brushes.Red;
                endTimePicker.BorderBrush = System.Windows.Media.Brushes.Red;
            }
            using (var context = new RestaurantDBEntities())
            {
                if(dATEDatePicker.IsEnabled && context.Reservations.FirstOrDefault(r=>r.DATE==this.reservation.DATE && r.START<= this.reservation.START && r.End >=this.reservation.START && r.Table_Number==this.reservation.Table_Number)!=null)
                {
                    canExit = false;
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBox.Show("W tym czasie jest już rezerwacja na ten stolik.", "Uwaga");
                }
            }
            if (canExit)
            {
                if (dATEDatePicker.IsEnabled)
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Reservations.Add(this.reservation);
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new RestaurantDBEntities())
                    {
                        context.Reservations.Attach(this.reservation);
                        context.Entry(this.reservation).State = EntityState.Modified;
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

        private void sTARTTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            sTARTTimePicker.BorderBrush = System.Windows.Media.Brushes.Black;
            endTimePicker.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void endTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            endTimePicker.BorderBrush = System.Windows.Media.Brushes.Black;
            sTARTTimePicker.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void table_NumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            table_NumberComboBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameTextBox.BorderBrush = System.Windows.Media.Brushes.Black;
        }
    }
}