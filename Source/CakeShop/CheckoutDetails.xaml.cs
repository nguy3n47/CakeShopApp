using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for CheckoutDetails.xaml
    /// </summary>
    public partial class CheckoutDetails : UserControl
    {
        public CheckoutDetails()
        {
            InitializeComponent();
        }
        ObservableCollection<Product> _data;
        long total = 0;
        int countOrder = 0;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        { 
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}ShoppingCart.txt";
            var lines = File.ReadAllLines(database);
            int count = lines.Length / 6;
            _data = new ObservableCollection<Product>();
            for (int i = 0; i < count; i++)
            {
                var line1 = lines[i * 6];
                var line3 = lines[i * 6 + 2];
                var line4 = lines[i * 6 + 3];
                var line5 = lines[i * 6 + 4];
                var line6 = lines[i * 6 + 5];

                var p = new Product()
                {
                    Name = line1,
                    Price = int.Parse(line3),
                    Quantity = int.Parse(line4),
                    Total = long.Parse(line5),
                    ProductType = line6,
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

        private void _order_Click(object sender, MouseButtonEventArgs e)
        {
            DateTime localDate = DateTime.Now;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}ListOfOrders.txt";
            var db = $"{folder}DB.xlsx";
            var workbook = new Workbook(db);
            var sheet = workbook.Worksheets[1];
            var cell = sheet.Cells["A1"];
            countOrder = cell.IntValue + 1;
            cell.PutValue(countOrder);
            using (StreamWriter sw = File.AppendText(database))
            {
                sw.WriteLine(localDate.ToString("dd/MM/yyyy"));
                sw.WriteLine("DH" + countOrder.ToString());
                sw.WriteLine(textBoxName.Text);
                sw.WriteLine(textBoxPhone.Text);
                sw.WriteLine(textBoxAddress.Text + ", " + textBoxWard.Text + ", " + textBoxDistrict.Text + ", " + textBoxCity.Text + ".");
                sw.WriteLine(total);
            }
            workbook.Save(db);
            _frame.Children.Clear();
            _frame.Children.Add(new OrderComplete(_data));
        }

        private void _shoppingcart_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new ShoppingCart());
        }

        private void textBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxPhone.Text = Regex.Replace(textBoxPhone.Text, "[^0-9]+", "");
        }
    }
}
