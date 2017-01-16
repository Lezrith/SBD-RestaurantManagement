using MahApps.Metro.Controls;
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
        //====move to Utility/common after merge
        private static bool IsInteger(string text)
        {
            if (Regex.IsMatch(text, "^0\\d+$")) return false;
            return Regex.IsMatch(text, "^\\d*$");
        }

        private static bool IsDecimal(string text, int precision, int scale)
        {
            if (Regex.IsMatch(text, "^0\\d+$")) return false;
            string regex = String.Format("^\\d{{1,{0}}}(,\\d{{0,{1}}})?$", precision - scale, scale);
            return Regex.IsMatch(text, regex);
        }

        //====

        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            String text = tb.Text + e.Text;
            if (tb.SelectionLength > 0) text = text.Replace(tb.SelectedText, "");
            textBox1.Text = text;
            e.Handled = IsInteger(text);
        }

        private void textBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            String text = tb.Text + e.Text;
            if (tb.SelectionLength > 0) text = text.Replace(tb.SelectedText, "");
            textBox1.Text = text;
            e.Handled = !IsDecimal(text, 8, 3);
        }
    }
}