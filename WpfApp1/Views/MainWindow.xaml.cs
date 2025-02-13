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
using System.Windows.Shapes;
using WpfApp1.ViewModel;
using WpfApp1.Views.Pages;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MnViewModel();
            
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new AccauntPage();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new NumberPage();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame = new GuestPage();
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame = new EmploeePage();
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame = new ServicePage();
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame = new ReservationPage();
        }
    }
}
