using Bar.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DatabaseModule db;
        public MainWindow()
        {
            InitializeComponent();

            db = new DatabaseModule(new ApplicationContext());

            WindowState = WindowState.Maximized;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            placeholder.Visibility = string.IsNullOrWhiteSpace(textBox1.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            placeholder2.Visibility = string.IsNullOrWhiteSpace(textBox2.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            placeholder3.Visibility = string.IsNullOrWhiteSpace(textBox3.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            placeholder4.Visibility = string.IsNullOrWhiteSpace(textBox4.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow menuWindow = new MenuWindow(db);
            menuWindow.WindowState = WindowState.Maximized;
            menuWindow.Show();

            Close();
        }
    }
}