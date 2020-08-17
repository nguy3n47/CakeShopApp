using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace CakeShop
{
    /// <summary>
    /// Interaction logic for USHome.xaml
    /// </summary>
    public partial class USHome : UserControl
    {
        DispatcherTimer timer;
        public USHome()
        {
            InitializeComponent();
            // Initialize the timer.
            timer = new DispatcherTimer();
            // Specify timer interval.
            timer.Interval = new TimeSpan(0, 0, 1);
            // Specify timer event handler function.
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (Banner1.Visibility == Visibility.Visible)
            {
                Banner1.Visibility = Visibility.Collapsed;
                Banner2.Visibility = Visibility.Visible;
            }
            else if (Banner2.Visibility == Visibility.Visible)
            {
                Banner2.Visibility = Visibility.Collapsed;
                Banner3.Visibility = Visibility.Visible;
            }
            else if (Banner3.Visibility == Visibility.Visible)
            {
                Banner3.Visibility = Visibility.Collapsed;
                Banner4.Visibility = Visibility.Visible;
            }
            else if (Banner4.Visibility == Visibility.Visible)
            {
                Banner4.Visibility = Visibility.Collapsed;
                Banner1.Visibility = Visibility.Visible;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
