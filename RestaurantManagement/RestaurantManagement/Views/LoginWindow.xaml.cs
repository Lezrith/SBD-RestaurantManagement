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
using RestaurantManagement.Model;

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
            string username = userIDTextBox.Text;
            string password = passwordBox.Password;
            if (username == string.Empty) userIDTextBox.BorderBrush = Brushes.Red;
            if (password == string.Empty) passwordBox.BorderBrush = Brushes.Red;
            if (username != string.Empty && password != string.Empty)
            {
                using (var context = new RestaurantDBEntities())
                {
                    int id = Int32.Parse(username);
                    var user = context.Employees.Include("Position.PRIVILEGES").FirstOrDefault(em => em.ID == id);
                    if (user != null)
                    {
                        if (user.Position.PRIVILEGES.FirstOrDefault(p => p.Name == "CAN_LOG_IN") != null)
                        {
                            if (Common.IsAssemblyDebugBuild() || user.Hashed_password == Common.HashString(password))
                            {
                                var mainWindow = new MainWindow();
                                mainWindow.user = user;
                                mainWindow.Show();
                                this.Close();
                            }
                            else
                            {
                                warningLabel.Content = "Błędne hasło";
                            }
                        }
                        else
                        {
                            warningLabel.Content = "Nie masz uprawnień";
                        }
                    }
                    else
                    {
                        warningLabel.Content = "Nie ma takiego używtkownika";
                    }
                }
            }
            else warningLabel.Content = "Uzupełnij puste pola.";
        }

        private void usernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (userIDTextBox.BorderBrush == Brushes.Red) userIDTextBox.ClearValue(BorderBrushProperty);
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.BorderBrush == System.Windows.Media.Brushes.Red) passwordBox.ClearValue(BorderBrushProperty);
        }

        private void userIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Common.IsInteger(e.Text);
        }
    }
}