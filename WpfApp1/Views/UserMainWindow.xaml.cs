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
        private Guid _currentUserId;
        public UserMainWindow()
        {
            InitializeComponent();
            _context = new SqlServerContext();
            LoadCurrentUserId();
        }
        private void LoadCurrentUserId()
        {
            string currentUserLogin = App.CurrentUserLogin;

            if (string.IsNullOrWhiteSpace(currentUserLogin))
            {
                MessageBox.Show("Ошибка! Логин пользователя пустой.");
                return;
            }

            // Выведем логин для проверки
            MessageBox.Show($"Ищем пользователя с логином: {currentUserLogin}");

            var user = _context.User.FirstOrDefault(u => u.Login == currentUserLogin);

            if (user != null)
            {
                _currentUserId = user.Id;
                MessageBox.Show($"Найден UserId: {_currentUserId}");
            }
            else
            {
                MessageBox.Show("Ошибка! Пользователь с таким логином не найден.");
            }
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
            if (_currentUserId == Guid.Empty)
            {
                MessageBox.Show("Ошибка! UserId пустой.");
                return;
            }

            ContentControlFrame.Content = new ServicePage(_currentUserId);
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new ReservationPage();
        }
    }
}
