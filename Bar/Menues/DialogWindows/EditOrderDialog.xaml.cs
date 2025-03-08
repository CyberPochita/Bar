using Bar.Menues.AdditionDialog;
using Bar.Menues.AdditionUserControls;
using Bar.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Bar.Menues.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для EditOrderDialog.xaml
    /// </summary>
    public partial class EditOrderDialog : Window
    {
        private static readonly Regex regex = new("[^0-9]+");
        private OrdersList ordersList;
        private DatabaseModule db;
        private List<Drink>? drinks;

        public EditOrderDialog(DatabaseModule db)
        {
            InitializeComponent();
            this.db = db;
            ordersList = new(db, true);
            GridOrders.Children.Add(ordersList);
        }

        private static bool IsTextAllowed(string text) => !regex.IsMatch(text);

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
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (String)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void DrinkButton_Click(object sender, RoutedEventArgs e)
        {
            CompactDrinks compactDrinks = new CompactDrinks(db);
            if(compactDrinks.ShowDialog() == true)
            {
                drinks = compactDrinks.listDrinks;
            }
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            bool isHaveAnyDrinks = true, isHaveNumberTable = true, isHaveStatus = true;
            List<Order> orders = ordersList.GetSelectedOrders();
            double totalPrice = 0;
            int numberTable = 0;
            string status = "";

            if(orders.IsNullOrEmpty())
            {
                MessageBox.Show("Нужно выбрать хотя бы один заказ", "Ошибка при изменении заказа", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (drinks.IsNullOrEmpty())
            {
                isHaveAnyDrinks = false;
            }
            else
            {
                foreach(var drink in drinks)
                {
                    totalPrice += drink.price;
                    Console.WriteLine($"PriceUnit: {drink.price}");
                }
            }

            if (NumbersTableInput.Text == "")
            {
                isHaveNumberTable = false;
            }
            else
            {
                numberTable = int.Parse(NumbersTableInput.Text);
            }

            if (StatusCombo.Text == "")
            {
                isHaveStatus = false;
            }
            else
            {
                status = StatusCombo.Text;
            }
            if (!isHaveAnyDrinks && !isHaveNumberTable && !isHaveStatus)
            {
                MessageBox.Show("Вы ничего не поменяли!", "Ошибка при изменении заказа", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            foreach(var order in orders)
            {
                db.EditOrder(order,
                    isHaveNumberTable ? numberTable : order.idTable,
                    isHaveStatus ? status : order.status,
                    isHaveAnyDrinks ? totalPrice : order.totalPrice);

                //List<Drink> previousDrinks = db.GetDrinksByOrderItem(db.GetOrderItemsByOrderId(order.Id));

                if (isHaveAnyDrinks)
                {
                    db.DeleteOrderItem(order.Id);
                    db.AddOrderItem(drinks, order.Id);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Окно закрылось");
        }
    }
}
