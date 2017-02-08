using MahApps.Metro.Controls;
using RestaurantManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RestaurantManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Employee user { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            using (var context = new RestaurantDBEntities())
            {
                this.user = context.Employees.FirstOrDefault(em => em.ID == 7);
            }
        }

        /*private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            String text = tb.Text + e.Text;
            if (tb.SelectionLength > 0) text = text.Replace(tb.SelectedText, "");
            textBox1.Text = text;
            e.Handled = IsInteger(text);
        }*/
    }
}