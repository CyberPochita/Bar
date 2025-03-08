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

namespace Bar.Menues.AdditionUserControls
{
    public class CardOrder
    {
        public int id { get; set; }
        public int idTable { get; set; }
        public DateTime dateOrder { get; set; }
        public double price { get; set; }
        public string status { get; set; }
    }

    public partial class OrdersList : UserControl
    {
        public ObservableCollection<CardOrder> cards { get; set; }
        public DatabaseModule db;
        private int clickCount = 0;
        private List<int>? selectedOrders;
        private string selectedOrder = "#111111";
        private bool isEnableActiveEffect = false;

        public OrdersList(DatabaseModule db, bool isEnableActiveEffect = false)
        {
            InitializeComponent();
            this.db = db;
            cards = new ObservableCollection<CardOrder>();
            DataContext = this;
            selectedOrders = new List<int>();
            this.isEnableActiveEffect = isEnableActiveEffect;

            List<Order> orders = db.GetOrders();

            foreach (Order order in orders)
            {
                cards.Add(new CardOrder { id = order.Id, idTable = order.idTable, dateOrder = order.datetime, price = order.totalPrice, status = order.status });
            }
        }

        public List<Order> GetSelectedOrders()
        {
            return new List<Order>(db.GetOrdersById(selectedOrders));
        }

        private void BorderOrder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(isEnableActiveEffect)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    clickCount++;

                    var timer = new System.Timers.Timer(300);
                    timer.Elapsed += (s, args) =>
                    {
                        clickCount = 0;
                        timer.Stop();
                        timer.Dispose();
                    };
                    timer.Start();

                    if (clickCount == 2)
                    {
                        var border = sender as Border;

                        if (border.BorderBrush == null)
                        {
                            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedOrder));
                            border.BorderThickness = new Thickness(5);
                            var cardOrder = border.DataContext as CardOrder;

                            if (cardOrder != null)
                            {
                                int id = cardOrder.id;
                                selectedOrders.Add(id);
                            }
                        }
                        else
                        {
                            border.BorderBrush = null;
                            border.BorderThickness = new Thickness(0);

                            var cardOrder = border.DataContext as CardOrder;
                            if (cardOrder != null)
                            {
                                foreach (var item in selectedOrders)
                                {
                                    if (cardOrder.id == item)
                                    {
                                        selectedOrders.Remove(item);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
