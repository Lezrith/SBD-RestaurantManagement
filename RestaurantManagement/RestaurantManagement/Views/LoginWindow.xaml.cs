using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace RestaurantManagement
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            if (Common.IsAssemblyDebugBuild()) warningLabel.Content = "Aplikacja w trybie deweloperskim!";
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = username = usernameTextBox.Text;
            string password = passwordBox.Password;
            if (username == string.Empty) usernameTextBox.BorderBrush = Brushes.Red;
            if (password == string.Empty) passwordBox.BorderBrush = Brushes.Red;
            if (username != string.Empty && password != string.Empty)
            {
                //try to login user
            }
            else warningLabel.Content = "Uzupełnij puste pola.";
        }

        private void usernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.BorderBrush == Brushes.Red) usernameTextBox.ClearValue(BorderBrushProperty);
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.BorderBrush == System.Windows.Media.Brushes.Red) passwordBox.ClearValue(BorderBrushProperty);
        }
    }
}