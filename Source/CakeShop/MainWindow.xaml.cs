using Aspose.Cells;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        USProduct _product = new USProduct();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _product.PositionChanged += Orther_PositionChanged;

            //Reset data every year
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}DB.xlsx";
            var workbook = new Workbook(database);
            var sheet = workbook.Worksheets[1];
            var oldYear = sheet.Cells["N1"].IntValue;
            var nowYear = DateTime.Now.Year;
            if (nowYear > oldYear)
            {
                sheet.Cells["N1"].PutValue(nowYear);
                for (int i = 1; i < 13; i++)
                {
                    char column = (char)(i + 65);
                    for (int j = 2; j < 10; j++)
                    {
                        sheet.Cells[$"{column}{j}"].PutValue(0);
                    }
                }
            }
            workbook.Save(database);

            _frame.Children.Clear();
            _frame.Children.Add(new USHome());
        }

        private void Orther_PositionChanged(string n)
        {
            Thread thread = new Thread(delegate ()
            {
                // Cập nhật UI
                Dispatcher.Invoke(() =>
                {
                    Total.Content = n;
                });
            });

            thread.Start();  
        }

        private void labelHome_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new USHome());
        }

        private void labelAbout_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new USAbout());
        }

        private void labelProduct_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new USProduct());
        }

        private void labelContact_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new USContact());
        }

        private void newProduct_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NewProduct p = new NewProduct();
            p.Show();
            this.Close();
        }

        private void _cart(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new ShoppingCart());
        }

        private void statisticProduct_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new USStatistic());
        }

        private void listOrder(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new USListOrders());
        }
    }
}
