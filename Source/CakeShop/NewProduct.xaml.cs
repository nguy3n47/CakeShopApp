using Aspose.Cells;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for NewProduct.xaml
    /// </summary>
    public partial class NewProduct : Window
    {
        public NewProduct()
        {
            InitializeComponent();
        }

        private void imgSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (textBoxName.Text.Trim() != "" && Img.ItemsSource != null && textBoxDescription.Text.Trim() != "" && textBoxPrice.Text.Trim() != "" && comboBoxitemType.Text.Trim() != "")
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    var folder = AppDomain.CurrentDomain.BaseDirectory;
                    var database = $"{folder}DB.xlsx";
                    var workbook = new Workbook(database);
                    var sheet = workbook.Worksheets[0];

                    var row = 1;
                    var cell = sheet.Cells[$"A{row}"];

                    while (cell.Value != null)
                    {
                        row++;
                        cell = sheet.Cells[$"A{row}"];
                    }

                    sheet.Cells[$"A{row}"].PutValue(textBoxName.Text);
                    sheet.Cells[$"B{row}"].PutValue(textBoxDescription.Text);
                    sheet.Cells[$"C{row}"].PutValue(textBoxPrice.Text.ToString().Replace(",", ""));
                    sheet.Cells[$"D{row}"].PutValue(comboBoxitemType.Text.ToString());
                    sheet.Cells[$"E{row}"].PutValue(_listImages.Count());
                    sheet.Cells[$"F{row}"].PutValue(System.IO.Path.GetFileName(_listImages[0]));

                    var imgFolder = $"{folder}Images";
                    string imgProd = System.IO.Path.GetFileName(_listImages[0]);
                    var appStartPathImgProd = String.Format(imgFolder + "\\" + imgProd);
                    if (!File.Exists(imgFolder + "\\" + imgProd))
                    {
                        File.Copy(_listImages[0], appStartPathImgProd, true);
                    }

                    var listImgFolder = $"{folder}List\\{textBoxName.Text}";
                    if (!Directory.Exists(listImgFolder))
                    {
                        Directory.CreateDirectory(listImgFolder);
                        foreach (string nameImg in _listImages)
                        {
                            string name = System.IO.Path.GetFileName(nameImg);
                            if (File.Exists(listImgFolder + "\\" + name))
                            {
                                //
                            }
                            else
                            {
                                appStartPathImgProd = String.Format(listImgFolder + "\\" + name);
                                File.Copy(nameImg, appStartPathImgProd, true);
                            }
                        }
                    }

                    var col = 'F';
                    for (int i = 1; i < _listImages.Count(); i++)
                    {
                        sheet.Cells[$"{char.ConvertFromUtf32(col + i)}{row}"].PutValue(System.IO.Path.GetFileName(_listImages[i]));
                    };

                    sheet.AutoFitColumns();
                    sheet.AutoFitRows();
                    workbook.Save(database);

                    MainWindow m = new MainWindow();
                    m.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show("Không được để trống tên, loại, giá, mô tả và hình ảnh của sản phẩm!!!"); 
        }

        private void imgCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        ObservableCollection<string> _listImages = new ObservableCollection<string>();
        private void ChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                foreach (string item in open.FileNames)
                {
                    _listImages.Add(item);
                }
                Img.ItemsSource = _listImages;
            }
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length > 0)
            {
                double value = 0;
                double.TryParse(textBox.Text, out value);
                textBox.Text = value.ToString("N0");
                textBox.CaretIndex = textBox.Text.Length;
            }
        }
    }
}
