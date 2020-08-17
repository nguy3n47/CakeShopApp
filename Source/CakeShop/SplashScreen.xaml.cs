using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private Random _rng = new Random();
        ObservableCollection<Product> _data;
        DispatcherTimer dT = new DispatcherTimer();
        string dataFile = "";
        public SplashScreen()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            dataFile = $"{folder}Check.txt";
            var data = File.ReadAllText(dataFile);
            if (data == "true")
            {
                MainWindow m = new MainWindow();
                m.Show();
                this.Close();
            }
            else
            {
                dT.Tick += new EventHandler(dt_Tick);
                dT.Interval = new TimeSpan(0, 0, 60);
                dT.Start();
            }
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            if (flag == true)
            {
                MainWindow m = new MainWindow();
                m.Show();
                dT.Stop();
                this.Close();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string folderc = AppDomain.CurrentDomain.BaseDirectory;
            string dataFilec = $"{folderc}Check.txt";
            File.AppendAllText(dataFilec, "true");
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            string folderc = AppDomain.CurrentDomain.BaseDirectory;
            string dataFilec = $"{folderc}Check.txt";
            File.Create(dataFilec).Close();
        }

        bool flag = true;
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}DB.xlsx";
            _data = new ObservableCollection<Product>();
            var workbook = new Workbook(database);
            var sheet = workbook.Worksheets[0];

            var row = 1;

            var cell = sheet.Cells[$"A{row}"];

            while (cell.Value != null)
            {
                var _name = cell.StringValue;
                var _description = sheet.Cells[$"B{row}"].StringValue;
                var _imgpath = sheet.Cells[$"F{row}"].StringValue;

                var product = new Product()
                {
                    Name = _name,
                    Description = _description,
                    ImagePath = _imgpath
                };

                _data.Add(product);
                row++;
                cell = sheet.Cells[$"A{row}"];
            }

            var k = _rng.Next(_data.Count);
            Title.Text = _data[k].Name;
            Description.Text = _data[k].Description;
            dataFile = $"{folder}Images\\{_data[k].ImagePath}";
            BackgoundImg.ImageSource = new BitmapImage(new Uri(dataFile));
        }
    }
}