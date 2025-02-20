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

namespace Bar.Menues.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для AddOrderDialog.xaml
    /// </summary>
    public partial class AddOrderDialog : Window
    {
        public AddOrderDialog(DatabaseModule db)
        {
            InitializeComponent();
            Drinks drinks = new Drinks(db);
            GridDrinks.Children.Add(drinks);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NumbersTableInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            placeholderNumbersTable.Visibility = string.IsNullOrWhiteSpace(NumbersTableInput.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
