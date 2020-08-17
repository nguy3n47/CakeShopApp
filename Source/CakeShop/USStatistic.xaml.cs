using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using LiveCharts;
using LiveCharts.Wpf;
using System.Threading;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for USStatistic.xaml
    /// </summary>
    public partial class USStatistic : UserControl
    {
        public USStatistic()
        {
            InitializeComponent();
            editMonth.ItemsSource = new string[] {
               "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"
            };
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection Data1 { get; set; }
        public SeriesCollection Data2 { get; set; }
        public char pMonth { get; set; }
        public string sMonth { get; set; }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}DB.xlsx";
            var workbook = new Workbook(database);
            var sheet = workbook.Worksheets[1];

            var row = 2;

            pMonth = new char();
            pMonth = (char)(dt.Month + 65);
            sMonth = dt.Month.ToString();

            Data1 = new SeriesCollection() { };
            Data2 = new SeriesCollection() { };

            var cell = sheet.Cells[$"A{row}"];
            int i = 12;
            int j = 1;
            var ColumSF = 'B';

            for (int n = 1; n < 13; n++)
            {
                long t = 0;
                char col = (char)(n + 65);
                for (int m = 2; m < 10; m++)
                {
                    t += (sheet.Cells[$"{col}{m}"].IntValue);
                }
                sheet.Cells[$"{col}10"].PutValue(t);
            }

            while (cell.Value != null)
            {

                PieSeries Pie = new PieSeries()
                {
                    Values = new ChartValues<float> { float.Parse(sheet.Cells[$"{pMonth}{row}"].StringValue) },
                    Title = $"{cell.StringValue}"
                };
                Data1.Add(Pie);
                row++;
                cell = sheet.Cells[$"A{row}"];
            }
            ColumnSeries c = new ColumnSeries()
            {
                Title = "Total",
                Values = new ChartValues<float> { }
            };
            while (j <= 12)
            {
                c.Values.Add(float.Parse(sheet.Cells[$"{char.ConvertFromUtf32(ColumSF + j - 1)}10"].StringValue));
                j++;
            }
            Data2.Add(c);

            Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
            Formatter = value => value.ToString("0 VND");
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var month = int.Parse(editMonth.SelectedItem as string);
            pMonth = (char)(month + 65);
            Thread thread = new Thread(delegate ()
            {
                // Đưa lên UI
                Dispatcher.Invoke(() =>
                {
                    var folder = AppDomain.CurrentDomain.BaseDirectory;
                    var database = $"{folder}DB.xlsx";
                    var ad = database.Length;
                    var workbook = new Workbook(database);
                    var sheet = workbook.Worksheets[1];

                    var row = 2;

                    Data1 = new SeriesCollection() { };

                    var cell = sheet.Cells[$"A{row}"];
                    while (cell.Value != null)
                    {

                        PieSeries Pie = new PieSeries()
                        {
                            Values = new ChartValues<float> { float.Parse(sheet.Cells[$"{pMonth}{row}"].StringValue) },
                            Title = $"{cell.StringValue}"
                        };
                        Data1.Add(Pie);
                        row++;
                        cell = sheet.Cells[$"A{row}"];
                    }
                    _pieChart.Series = Data1;
                });
            });
            thread.Start();
        }
    }
}
