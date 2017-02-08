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
    /// Interaction logic for ReservationsView.xaml
    /// </summary>
    public partial class ReservationsView : Page
    {
        public ReservationsView()
        {
            InitializeComponent();
            PopulateReservationsDataGrid();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenReservationAddEditWindow("Dodaj rezerwację", null);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservations = reservationsDataGrid.SelectedItems;
            if (selectedReservations.Count == 1)
            {
                OpenReservationAddEditWindow("Edytuj rezerwację", selectedReservations[0] as Reservation);
            }
        }

        private void OpenReservationAddEditWindow(string title, Reservation reservation)
        {
            var addEditWindow = new ReservationAddEditWindow(title, reservation);
            addEditWindow.ShowDialog();
            PopulateReservationsDataGrid();
        }

        private void PopulateReservationsDataGrid()
        {
            using (var context = new RestaurantDBEntities())
            {
                reservationsDataGrid.ItemsSource = context.Reservations.ToList();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservations = reservationsDataGrid.SelectedItems;
            if (selectedReservations.Count == 1)
            {
                var res = selectedReservations[0] as Reservation;
                using (var context = new RestaurantDBEntities())
                {
                    var toRemove = context.Reservations.FirstOrDefault(r => r.DATE == res.DATE && r.START == res.START && r.Table_Number == res.Table_Number);
                    context.Reservations.Remove(toRemove);
                    context.SaveChanges();
                }
            }
            PopulateReservationsDataGrid();
        }
    }
}