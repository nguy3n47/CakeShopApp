using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for USListOrders.xaml
    /// </summary>
    public class ListOfOrders
    {
        // Properties
        public string dateCreated { get; set; }
        public string idOrder { get; set; }
        public string customerName { get; set; }
        public string phoneNumber { get; set; }
        public string deliveryAddress  { get; set; }
        public long Total { get; set; }
    }
    public partial class USListOrders : UserControl
    {
        public USListOrders()
        {
            InitializeComponent();
        }
        ObservableCollection<ListOfOrders> _data;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _data = new ObservableCollection<ListOfOrders>();
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}ListOfOrders.txt";
            var lines = File.ReadAllLines(database);
            int count = lines.Length / 6;
            for (int i = 0; i < count; i++)
            {
                var line1 = lines[i * 6];
                var line2 = lines[i * 6 + 1];
                var line3 = lines[i * 6 + 2];
                var line4 = lines[i * 6 + 3];
                var line5 = lines[i * 6 + 4];
                var line6 = lines[i * 6 + 5];

                ListOfOrders l = new ListOfOrders()
                {
                    dateCreated = line1,
                    idOrder = line2,
                    customerName = line3,
                    phoneNumber = line4,
                    deliveryAddress = line5,
                    Total = long.Parse(line6)
                };
                _data.Add(l);
            }
            Thread thread = new Thread(delegate ()
            {
                // Cập nhật UI
                Dispatcher.Invoke(() =>
                {
                    datalistView.ItemsSource = _data;
                });
            });
            thread.Start();
        }
    }
}
