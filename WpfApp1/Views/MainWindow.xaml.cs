﻿using System;
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
using WpfApp1.Service;
using WpfApp1.ViewModel;
using WpfApp1.Views.Pages;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly SqlServerContext _context;
        private Guid _currentUserId;
        public MainWindow()
        {
            InitializeComponent();
            _context = new SqlServerContext();
            LoadCurrentUserId();
        }
        private void LoadCurrentUserId()
        {
            string currentUserLogin = App.CurrentUserLogin;

            // Получаем пользователя из базы данных по логину
            var user = _context.User.FirstOrDefault(u => u.Login == currentUserLogin);

            if (user != null)
            {
                _currentUserId = user.Id;
            }
            else
            {
                MessageBox.Show("Пользователь не найден.");
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
            ContentControlFrame.Content = new AdminServicePage(_currentUserId);
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            ContentControlFrame.Content = new ReservationPage();
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
       
    }
}
