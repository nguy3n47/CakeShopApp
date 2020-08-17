using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for USProduct.xaml
    /// </summary>
    public class trvTypeProduct
    {
        public trvTypeProduct()
        {
            this.Prod = new ObservableCollection<Proddd>();
        }

        public string Name { get; set; }

        public ObservableCollection<Proddd> Prod { get; set; }
    }

    public class Proddd
    {
        public string Name { get; set; }
    }
    public partial class USProduct : UserControl
    {
        public USProduct()
        {
            InitializeComponent();
        }

        ObservableCollection<Product> _data;
        public Product product;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _data = new ObservableCollection<Product>();
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}DB.xlsx";
            trvTypeProduct type1 = new trvTypeProduct() { Name = "BAGELS" };
            trvTypeProduct type2 = new trvTypeProduct() { Name = "BREAD" };
            trvTypeProduct type3 = new trvTypeProduct() { Name = "BUNS" };
            trvTypeProduct type4 = new trvTypeProduct() { Name = "CAKE" };
            trvTypeProduct type5 = new trvTypeProduct() { Name = "CUPCAKE & MUFFIN" };
            trvTypeProduct type6 = new trvTypeProduct() { Name = "LOAF CAKE" };
            trvTypeProduct type7 = new trvTypeProduct() { Name = "OTHERS" };
            trvTypeProduct type8 = new trvTypeProduct() { Name = "ROLL CAKE" };

            var workbook = new Workbook(database);
            var sheet = workbook.Worksheets[0];

            var row = 1;

            var cell = sheet.Cells[$"A{row}"];

            while (cell.Value != null)
            {
                var _name = cell.StringValue;
                var _description = sheet.Cells[$"B{row}"].StringValue;
                var _price = long.Parse(sheet.Cells[$"C{row}"].StringValue);
                var _productType = sheet.Cells[$"D{row}"].StringValue;
                var _imgpath = sheet.Cells[$"F{row}"].StringValue;
              
                product = new Product()
                {
                    Name = _name,
                    Description = _description,
                    Price = _price,
                    ProductType = _productType,
                    ImagePath = _imgpath,
                    Foreground = "#FF334862",
                    Background = "White"
                };

                if (product.ProductType == "BAGELS")
                {
                    type1.Prod.Add(new Proddd() { Name = product.Name });
                }
                if (product.ProductType == "BREAD")
                {
                    type2.Prod.Add(new Proddd() { Name = product.Name });
                }
                if (product.ProductType == "BUNS")
                {
                    type3.Prod.Add(new Proddd() { Name = product.Name });
                }
                if (product.ProductType == "CAKE")
                {
                    type4.Prod.Add(new Proddd() { Name = product.Name });
                }
                if (product.ProductType == "CUPCAKE & MUFFIN")
                {
                    type5.Prod.Add(new Proddd() { Name = product.Name });
                }
                if (product.ProductType == "LOAF CAKE")
                {
                    type6.Prod.Add(new Proddd() { Name = product.Name });
                }
                if (product.ProductType == "OTHERS")
                {
                    type7.Prod.Add(new Proddd() { Name = product.Name });
                }
                if (product.ProductType == "ROLL CAKE")
                {
                    type8.Prod.Add(new Proddd() { Name = product.Name });
                }

                _data.Add(product);
                row++;
                cell = sheet.Cells[$"A{row}"];
            }
            
            info.CurrentPage = 1;
            info.RowsPerPage = 12;
            info.Count = _data.Count;
            info.TotalPages = (info.Count / info.RowsPerPage) +
                (info.Count % info.RowsPerPage == 0 ? 0 : 1);

            if (_data.Count > 12)
            {
                _pagination.Visibility = Visibility.Visible;
            }

            Thread thread = new Thread(delegate ()
            {
                // Cập nhật UI
                Dispatcher.Invoke(() =>
                {
                    dataListview.ItemsSource = _data.Take(info.RowsPerPage)/*.OrderBy(p => p.Name)*/;
                });
            });

            thread.Start();

            List<trvTypeProduct> prods = new List<trvTypeProduct>();
            prods.Add(type1);
            prods.Add(type2);
            prods.Add(type3);
            prods.Add(type4);
            prods.Add(type5);
            prods.Add(type6);
            prods.Add(type7);
            prods.Add(type8);

            dataTreeview.ItemsSource = prods;
        }

        PagingInfo info = new PagingInfo();
        class PagingInfo : INotifyPropertyChanged
        {
            public int TotalPages { get; set; }

            private int _currentPage = 0;
            public int CurrentPage
            {
                get => _currentPage;
                set
                {
                    _currentPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
                }
            }
            private int _page1 = 1;
            public int Page1
            {
                get => _page1;
                set
                {
                    _page1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Page1"));
                }
            }
            private int _page2 = 2;
            public int Page2
            {
                get => _page2;
                set
                {
                    _page2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Page2"));
                }
            }
            private int _page3 = 3;
            public int Page3
            {
                get => _page3;
                set
                {
                    _page3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Page3"));
                }
            }

            public int Count { get; set; }
            public int RowsPerPage { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        private void Next_Click(object sender, MouseButtonEventArgs e)
        {
            if (info.CurrentPage < info.TotalPages)
            {
                info.CurrentPage++;
                dataListview.ItemsSource =
                _data
                    .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                    .Take(info.RowsPerPage);
            }
            dataListview.ScrollIntoView(dataListview.Items[0]);
        }

        private void Prev_Click(object sender, MouseButtonEventArgs e)
        {
            if (info.CurrentPage <= info.TotalPages)
            {
                info.CurrentPage--;
                dataListview.ItemsSource =
                _data
                    .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                    .Take(info.RowsPerPage);
                if (info.CurrentPage <= 1)
                {
                    info.CurrentPage = 1;
                }
            }
            dataListview.ScrollIntoView(dataListview.Items[0]);
        }

        private void _order_MouseMove(object sender, MouseEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataListview.Items.IndexOf(item) + ((info.CurrentPage - 1) * info.RowsPerPage);
            _data[index].Foreground = "White";
            _data[index].Background = "#FF334862";
        }

        private void _order_MouseLeave(object sender, MouseEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataListview.Items.IndexOf(item) + ((info.CurrentPage - 1) * info.RowsPerPage);
            _data[index].Foreground = "#FF334862";
            _data[index].Background = "White";
        }

        private void dataListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as Product;
            int index = dataListview.Items.IndexOf(item) + ((info.CurrentPage - 1) * info.RowsPerPage);
            if (item != null)
            {
                _frame.Children.Clear();
                _frame.Children.Add(new USProductDetail(item));
            }
        }

        public delegate void PositionNotifyDelegate(string n);
        public event PositionNotifyDelegate PositionChanged;
        int total = 0;
        private void orther_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext as Product;
            int index = _data.IndexOf(item);
            //total++;
            //PositionChanged?.Invoke(total.ToString());
            int temp = 0;
            int flag = 0;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}ShoppingCart.txt";
            var lines = File.ReadAllLines(database);
            for (int i = 0; i < lines.Length; i += 6)
            {
                if (lines[i] == _data[index].Name)
                {
                    flag = 1;
                    temp = int.Parse(lines[i + 3]);
                    temp += 1;
                    lines[i + 3] = temp.ToString();
                    lines[i + 4] = (temp * _data[index].Price).ToString();
                    File.WriteAllLines(database, lines);
                    break;
                }
            }

            if (flag == 0)
            {
                using (StreamWriter sw = File.AppendText(database))
                {
                    sw.WriteLine(_data[index].Name);
                    sw.WriteLine(_data[index].ImagePath);
                    sw.WriteLine(_data[index].Price);
                    sw.WriteLine(_data[index].Quantity = 1);
                    sw.WriteLine(_data[index].Price * _data[index].Quantity);
                    sw.WriteLine(_data[index].ProductType);
                }
            }
            _frame.Children.Clear();
            _frame.Children.Add(new ShoppingCart());
        }

        private void dataTreeview_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ObservableCollection<Product> category = new ObservableCollection<Product>();
            _pagination.Visibility = Visibility.Collapsed;
            var item = (sender as TreeView).SelectedItem as trvTypeProduct;
            if (item != null)
            {
                for (int i = 0; i < _data.Count(); i++)
                {
                    if (_data[i].ProductType == item.Name)
                    {
                        category.Add(_data[i]);
                    }
                }
                dataListview.ItemsSource = category.Take(category.Count);
                if (category.Count > 0)
                {
                    dataListview.ScrollIntoView(dataListview.Items[0]);
                }    
            }
            else
            {
                var p = (sender as TreeView).SelectedItem as Proddd;
                for (int i = 0; i < _data.Count(); i++)
                {
                    if (_data[i].Name == p.Name)
                    {
                        category.Add(_data[i]);
                    }
                }
                dataListview.ItemsSource = category.Take(category.Count);
                if (category.Count > 0)
                {
                    dataListview.ScrollIntoView(dataListview.Items[0]);
                }
            }
        }
    }
}
