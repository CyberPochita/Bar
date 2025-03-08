using Bar.Menues.AdditionUserControls;
using Bar.Menues.DialogWindows;
using Bar.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bar.Menues
{
    public partial class Orders : UserControl
    {
        public DatabaseModule db;
        private OrdersList ordersList;

        public Orders(DatabaseModule db)
        {
            InitializeComponent();
            this.db = db;
            ordersList = new(db);
            GridOrders.Children.Add(ordersList);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderDialog addOrder = new AddOrderDialog(db);
            addOrder.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditOrderDialog editOrder = new EditOrderDialog(db);
            editOrder.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FreshOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
