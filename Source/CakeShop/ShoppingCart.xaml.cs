using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : UserControl
    {
        public ShoppingCart()
        {
            InitializeComponent();
        }
        ObservableCollection<Product> _data;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            long total = 0;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}ShoppingCart.txt";
            var lines = File.ReadAllLines(database);
            int count = lines.Length / 6;
            _data = new ObservableCollection<Product>();
            for (int i = 0; i < count; i++)
            {
                var line1 = lines[i * 6];
                var line2 = lines[i * 6 + 1];
                var line3 = lines[i * 6 + 2];
                var line4 = lines[i * 6 + 3];
                var line5 = lines[i * 6 + 4];

                var p = new Product()
                {
                    Name = line1,
                    ImagePath = line2,
                    Price = int.Parse(line3),
                    Quantity = int.Parse(line4),
                    Total = long.Parse(line5),
                };
                _data.Add(p);
            }
            dataListview.ItemsSource = _data;

            foreach (var t in _data)
            {
                total += t.Total;
            }

            double newValue = double.Parse(total.ToString());
            _total.Content = newValue.ToString("N0").Replace(",", ".") + " VNĐ";
        }

        private void payment(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new CheckoutDetails());
        }

        private void buttonDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            long total = 0;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}ShoppingCart.txt";
            var item = (sender as FrameworkElement).DataContext;
            int index = dataListview.Items.IndexOf(item);
            _data.RemoveAt(index);
            foreach (var t in _data)
            {
                total += t.Total;
            }

            double newValue = double.Parse(total.ToString());
            _total.Content = newValue.ToString("N0").Replace(",", ".") + " VNĐ";

            List<string> resline = File.ReadAllLines(database).ToList();
            resline.RemoveAt(index * 6 + 5);
            resline.RemoveAt(index * 6 + 4);
            resline.RemoveAt(index * 6 + 3);
            resline.RemoveAt(index * 6 + 2);
            resline.RemoveAt(index * 6 + 1);
            resline.RemoveAt(index * 6);
            File.WriteAllLines(database, resline.ToArray());
        }

        private void _checkout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new CheckoutDetails());
        }
    }
}
