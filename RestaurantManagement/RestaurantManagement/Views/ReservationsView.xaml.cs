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
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            OpenReservationAddEditWindow("Dodaj rezerwację", null);
        }

        private void OpenReservationAddEditWindow(string title, Reservation reservation)
        {
            var addEditWindow = new ReservationAddEditWindow(title, reservation);
            addEditWindow.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrders = reservationsDataGrid.SelectedItems;
            if (selectedOrders.Count == 1)
            {
                OpenReservationAddEditWindow("Edytuj rezerwację", null);
            }
        }
    }
}