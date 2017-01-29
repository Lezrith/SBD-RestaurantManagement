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

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for ReservationAddEdit.xaml
    /// </summary>
    public partial class ReservationAddEditWindow : MetroWindow
    {
        public ReservationAddEditWindow(string title, Reservation reservation)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Title = title;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}