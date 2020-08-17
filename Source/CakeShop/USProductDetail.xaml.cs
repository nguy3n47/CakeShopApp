using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Interaction logic for USProductDetail.xaml
    /// </summary>
    public partial class USProductDetail : UserControl
    {
        public Product _data;
        public Product prod;
        ObservableCollection<Product> _list;
        string nameProduct;
        public USProductDetail(Product p)
        {
            InitializeComponent();
            _data = p;
            nameProduct = _data.Name;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _data;
            _list = new ObservableCollection<Product>();
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}DB.xlsx";
            var workbook = new Workbook(database);
            var sheet = workbook.Worksheets[0];
            var row = 1;
            var cell = sheet.Cells[$"A{row}"];
            while (cell.Value != null)
            {
                if (_data.Name == cell.StringValue)
                {
                    break;
                }
                else
                {
                    row++;
                    cell = sheet.Cells[$"A{row}"];
                }
            }

            prod = new Product()
            {
                Name = _data.Name,
                Description = _data.Description,
                Price = _data.Price,
                ProductType = _data.ProductType,
                ImagePath = _data.ImagePath,
                Quantity = int.Parse(_number.Text),
                listImages = new BindingList<string>()
            };

            var count = sheet.Cells[$"E{row}"].IntValue;
            var col = 'F';
            for (int i = 0; i < count; i++)
            {
                var value = $"{folder}List\\{nameProduct}\\" + sheet.Cells[$"{char.ConvertFromUtf32(col + i)}{row}"].StringValue;
                prod.listImages.Add(value);
            };
            _list.Add(prod);
            PreviewPhoto.ItemsSource = _list;
        }
        int count = 1;
        private void Minus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            count = int.Parse(_number.Text.ToString());
            count--;
            if (count < 1) count = 1;
            _number.Text = count.ToString();
        }

        private void Plus_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var count = int.Parse(_number.Text.ToString());
            count++;
            _number.Text = count.ToString();
        }

        private void _editProduct_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            _frame.Children.Clear();
            _frame.Children.Add(new EditProduct(prod));
        }

        private void orderProduct(object sender, MouseButtonEventArgs e)
        {
            int temp = 0;
            int flag = 0;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}ShoppingCart.txt";
            var lines = File.ReadAllLines(database);
            for (int i = 0; i < lines.Length; i += 6)
            {
                if(lines[i] == prod.Name)
                {
                    flag = 1;
                    temp = int.Parse(lines[i + 3]);
                    temp += int.Parse(_number.Text);
                    lines[i + 3] = temp.ToString();
                    lines[i + 4] = (temp * prod.Price).ToString();
                    File.WriteAllLines(database, lines);
                    break;
                }
            }

            if (flag == 0)
            {
                using (StreamWriter sw = File.AppendText(database))
                {
                    sw.WriteLine(prod.Name);
                    sw.WriteLine(prod.ImagePath);
                    sw.WriteLine(prod.Price);
                    sw.WriteLine(prod.Quantity = int.Parse(_number.Text));
                    sw.WriteLine(prod.Price * prod.Quantity);
                    sw.WriteLine(prod.ProductType);
                }
            }
            _frame.Children.Clear();
            _frame.Children.Add(new ShoppingCart());
        }
    }
}
