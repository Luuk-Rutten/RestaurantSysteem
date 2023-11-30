using RestaurantSysteem.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace RestaurantSysteem
{
    //                                                                    V 1.2
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Restaurant restaurant = new Restaurant();

        public MainWindow()
        {
            InitializeComponent();
        }
        // opent de bestelmenu's met tafelnummer
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TafelWindow window1 = new TafelWindow(restaurant.Tafels[0]);
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TafelWindow window1 = new TafelWindow(restaurant.Tafels[1]);
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TafelWindow window1 = new TafelWindow(restaurant.Tafels[2]);
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            TafelWindow window1 = new TafelWindow(restaurant.Tafels[3]);
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            TafelWindow window1 = new TafelWindow(restaurant.Tafels[4]);
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            TafelWindow window1 = new TafelWindow(restaurant.Tafels[5]);
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }

    }
}
