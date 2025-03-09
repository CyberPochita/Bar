using Bar.Models;
using Microsoft.IdentityModel.Tokens;
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
using System.Windows.Shapes;
using Bar.Menues.AdditionUserControls;

namespace Bar.Menues.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для AddOrderDialog.xaml
    /// </summary>
    public partial class AddOrderDialog : Window
    {
        private DrinksList drinksList;
        private List<Drink>? objectDrinks;
        private DatabaseModule db;
        private static readonly Regex regex = new("[^0-9]+");

        public AddOrderDialog(DatabaseModule db)
        {
            InitializeComponent();
            this.db = db;
            drinksList = new DrinksList(db);
            GridDrinks.Children.Add(drinksList);
        }

        private static bool IsTextAllowed(string text) => !regex.IsMatch(text);

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Выход из окна");
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            objectDrinks = drinksList.GetSelectedDrinks();
            string numberTable = NumbersTableInput.Text;
            double totalPrice = 0;

            if (objectDrinks.IsNullOrEmpty())
            {
                MessageBox.Show("Нужно выбрать хотя бы один напиток", "Ошибка при добавлении заказа", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if(numberTable == null) // TODO: Добавить проверку на есть ли такой id вообще в базе данных
            {
                MessageBox.Show("Нужно ввести номер столика", "Ошибка при добавлении заказа", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            foreach(var drink in objectDrinks)
            {
                totalPrice += drink.price;
            }

            Order newOrder = db.AddOrder(int.Parse(numberTable.Trim()), totalPrice);

            db.AddOrderItem(objectDrinks, newOrder.Id);
        }

        private void NumbersTableInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            placeholderNumbersTable.Visibility = string.IsNullOrWhiteSpace(NumbersTableInput.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void NumbersTableInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void NumbersTableInput_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if(e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (String)e.DataObject.GetData(typeof(string));
                if(!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
