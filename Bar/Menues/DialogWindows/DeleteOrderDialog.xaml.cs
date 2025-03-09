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
using Bar.Menues.AdditionUserControls;
using Bar.Models;
using Microsoft.IdentityModel.Tokens;

namespace Bar.Menues.DialogWindows
{
    public partial class DeleteOrderDialog : Window
    {
        private OrdersList ordersList;
        private DatabaseModule db;
        public DeleteOrderDialog(DatabaseModule db)
        {
            InitializeComponent();
            this.db = db;
            ordersList = new(db, true);
            GridOrders.Children.Add(ordersList);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            List<Order> orders = ordersList.GetSelectedOrders();

            if (orders.IsNullOrEmpty())
                MessageBox.Show("Не выбран ни один заказ", "Ошибка удаления заказов", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                var result = MessageBox.Show("Вы точно хотите удалить заказ\\заказы?", "Подтверждение удаления заказов", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                Console.WriteLine($"Результат вопроса: {result}");
                if (result == MessageBoxResult.OK)
                {
                    foreach (var order in orders)
                    {
                        db.DeleteOrder(order.Id);
                    }
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Выход из удаления");
            Close();
        }
    }
}
