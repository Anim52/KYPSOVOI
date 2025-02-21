using Microsoft.EntityFrameworkCore;
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
using WpfApp1.Context;
using WpfApp1.Views.Pages;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        public readonly SqlServerContext _context;
        public UserMainWindow()
        {
            InitializeComponent();
            _context = new SqlServerContext();
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            string currentUserLogin = App.CurrentUserLogin;

            // Получаем пользователя из базы данных по логину
            var user = _context.User.FirstOrDefault(u => u.Login == currentUserLogin);

            if (user != null)
            {
                // Получаем userId из найденного пользователя
                Guid userId = user.Id;

                // Передаем userId в конструктор AccauntPage
                ContentControlFrame.Content = new AccauntPage(userId);  // Вместо this.Content =
            }
            else
            {
                MessageBox.Show("Пользователь не найден.");
            }
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new NumberPage();
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new GuestPage();
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new EmploeePage();
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new ServicePage();
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new ReservationPage();
        }
    }
}
