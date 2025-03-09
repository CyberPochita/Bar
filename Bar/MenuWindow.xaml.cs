using Bar.Models;
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

namespace Bar
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public DatabaseModule db;
        public MenuWindow(DatabaseModule db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            tubs.Content = new Menues.Orders(db);
        }

        private void DrinkButton_Click(object sender, RoutedEventArgs e)
        {
            tubs.Content = new Menues.Drinks(db);
        }
    }
}
