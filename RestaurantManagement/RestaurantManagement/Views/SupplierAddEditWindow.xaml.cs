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

namespace RestaurantManagement.Views
{
    /// <summary>
    /// Interaction logic for SupplierAddEditWindow.xaml
    /// </summary>
    public partial class SupplierAddEditWindow : MetroWindow
    {
        public SupplierAddEditWindow(string title, Supplier supplier)
        {
            InitializeComponent();
            this.Title = title;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}