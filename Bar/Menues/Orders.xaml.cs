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
    public class Card
    {
        public int id { get; set; }
        public int idTable { get; set; }
        public DateTime dateOrder { get; set; }
        public double price { get; set; }
        public string status { get; set; }
    }

    public partial class Orders : UserControl
    {
        public ObservableCollection<Card> cards { get; set; }
        public DatabaseModule db;

        public Orders(DatabaseModule db)
        {
            InitializeComponent();
            this.db = db;
            cards = new ObservableCollection<Card>();
            DataContext = this;

            List<Order> orders = db.GetOrders();

            foreach(Order order in orders)
            {
                cards.Add(new Card { id = order.Id, idTable = order.idTable, dateOrder = order.datetime, price = order.totalPrice, status = order.status });
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrderDialog addOrder = new AddOrderDialog(db);
            addOrder.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FreshOrders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
