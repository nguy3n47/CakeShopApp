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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : UserControl
    {
        public Product _data;
        BindingList<Product> _list;
        string nameProduct;
        public EditProduct(Product p)
        {
            InitializeComponent();
            _data = p;
            nameProduct = _data.Name;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxName.Text = _data.Name;
            textBoxDescription.Text = _data.Description;
            textBoxPrice.Text = _data.Price.ToString();
            comboBoxitemType.Text = _data.ProductType;
            avtImage.ImageSource = new BitmapImage(new Uri(_data.listImages[0]));

            _list = new BindingList<Product>();

            var t = new Product()
            {
                listImages = new BindingList<string>()
            };

            foreach (var l in _data.listImages)
            {
                _listImages.Add(l);
                t.listImages.Add(l);
            }

            _list.Add(t);

            ItemControlImages.ItemsSource = _list;
        }

        private void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(file)));

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(directory)));
        }

        private void imgSave_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (textBoxName.Text.Trim() != "" && avtImage.ImageSource != null && textBoxDescription.Text.Trim() != "" && textBoxPrice.Text.Trim() != "" && comboBoxitemType.Text.Trim() != "")
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    //Sửa dữ liệu file DB.xsls
                    var folder = AppDomain.CurrentDomain.BaseDirectory;
                    var database = $"{folder}DB.xlsx";

                    var workbook = new Workbook(database);
                    var sheet = workbook.Worksheets[0];

                    var row = 1;
                    var cell = sheet.Cells[$"A{row}"];

                    while (cell.Value != null)
                    {
                        if (cell.StringValue == _data.Name)
                            break;
                        row++;
                        cell = sheet.Cells[$"A{row}"];
                    }

                    //sheet.Cells.DeleteRows(row, 2);

                    var avatar = $"{folder}Images";
                    string imgAv = ((BitmapImage)avtImage.ImageSource).UriSource.ToString().Remove(0, 8);
                    string imgprodr = System.IO.Path.GetFileName(avtImage.ImageSource.ToString());
                    if (_data.ImagePath != imgprodr)
                    {
                        var appStartPathImg = String.Format(avatar + "\\" + imgprodr);
                        string imgAvName = System.IO.Path.GetFileName(imgAv.ToString());
                        if (!File.Exists(avatar + "\\" + imgAvName))
                        {
                            File.Copy(imgAv, appStartPathImg, true);
                        }
                    }

                    sheet.Cells[$"A{row}"].PutValue(textBoxName.Text);
                    sheet.Cells[$"B{row}"].PutValue(textBoxDescription.Text);
                    sheet.Cells[$"C{row}"].PutValue(textBoxPrice.Text.ToString().Replace(",", ""));
                    sheet.Cells[$"D{row}"].PutValue(comboBoxitemType.Text.ToString());
                    sheet.Cells[$"E{row}"].PutValue(_listImages.Count());
                    sheet.Cells[$"F{row}"].PutValue(imgprodr);

                    foreach (string nameImg in _list[0].listImages)
                    {
                        var path = $"{folder}List\\{nameProduct}";

                        string avt = System.IO.Path.GetFileName(imgAv);
                        var appStartPathImgAvt = String.Format(path + "\\" + avt);
                        if (!File.Exists(path + "\\" + avt))
                        {
                            File.Copy(imgAv, appStartPathImgAvt, true);
                        }

                        string name = System.IO.Path.GetFileName(nameImg);
                        if (File.Exists(path + "\\" + name))
                        {

                        }
                        else
                        {
                            var appStartPath = String.Format(path + "\\" + name);
                            File.Copy(nameImg, appStartPath, true);
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

                    //Đổi tên folder
                    var source = $"{folder}" + $"List\\{nameProduct}\\";
                    var des = $"{folder}" + $"List\\{textBoxName.Text}\\";
                    if (nameProduct == textBoxName.Text)
                    {
                        _data.Description = textBoxDescription.Text;
                        _data.Price = long.Parse(textBoxPrice.Text.ToString().Replace(",", ""));
                        _data.ImagePath = imgprodr;
                        _data.ProductType = comboBoxitemType.Text;

                        _frame.Children.Clear();
                        _frame.Children.Add(new USProductDetail(_data));
                    }
                    else
                    {
                        _data.Name = textBoxName.Text;
                        _data.Description = textBoxDescription.Text;
                        _data.Price = long.Parse(textBoxPrice.Text.ToString().Replace(",", ""));
                        _data.ImagePath = imgprodr;
                        _data.ProductType = comboBoxitemType.Text;

                        if (!Directory.Exists(des))
                        {
                            Directory.CreateDirectory(des);
                            Copy(source, des);
                            //Directory.Move(source, des);     
                        }
                        _frame.Children.Clear();
                        _frame.Children.Add(new USProductDetail(_data));
                    }
                }
            }
            else
                MessageBox.Show("Không được để trống tên, loại, giá, mô tả và hình ảnh của sản phẩm!!!");
        }

        private void imgCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _frame.Children.Clear();
            _frame.Children.Add(new USProductDetail(_data));
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
                    _list[0].listImages.Add(item);
                }
            }
        }

        List<string> imagesExsis = new List<string>();
        private void buttonDeleteImages_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            imagesExsis.Add(item.ToString());
            int index = _list[0].listImages.IndexOf(item.ToString());
            _list[0].listImages.RemoveAt(index);
            _listImages.RemoveAt(index);
        }

        private void avtImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                var img = open.FileNames;
                ImageSource imgsource = new BitmapImage(new Uri(img[0].ToString()));
                avtImage.ImageSource = imgsource;
            }
        }
    }
}
