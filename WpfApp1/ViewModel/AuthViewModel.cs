﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Command;
using WpfApp1.Context;
using WpfApp1.OtherServices;
using WpfApp1.Service;
using WpfApp1.Views;
using WpfApp1.ViewModel;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly SqlServerContext _context;
        private readonly AuthService _authService;

        public string Login { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public AuthViewModel()
        {
            _context = new SqlServerContext();
            _authService = new AuthService();
            LoginCommand = new RelayCommand(LoginExecute);
        }

        private void LoginExecute(object obj)
        {
            // Создаем переменную для хранения пользователя
            User user = null;

            // Проверяем, если логин и пароль правильные
            if (_authService.Login(Login, Password, out user))
            {
                // Сохраняем логин текущего пользователя в глобальное статическое свойство
                App.CurrentUserLogin = user.Login;

                // Получаем текущего пользователя по логину
                var currentUser = _context.User.FirstOrDefault(u => u.Login == App.CurrentUserLogin);

                if (currentUser != null)
                {
                    // Получаем userId из найденного пользователя
                    Guid userId = currentUser.Id;

                    // ** Добавляем вот эту строку для хранения Id текущего пользователя **
                    App.CurrentUserId = userId;

                    // Ищем гостя с таким же userId
                    var guest = _context.Guests.FirstOrDefault(g => g.Id == userId);

                    // Если гостя нет, создаём нового
                    if (guest == null)
                    {
                        guest = new Guests
                        {
                            Id = userId,
                            FirstName = currentUser.Firstname,
                            MiddleName = currentUser.Middlename,
                            LastName = currentUser.Lastname,
                            DateOfBirth = DateTime.MinValue, // Задайте нужные значения по умолчанию
                            PassportNumber = 0, // Задайте нужные значения по умолчанию
                            ContactDetails = string.Empty, // Задайте нужные значения по умолчанию
                            RegistrationDate = DateTime.Now, // Текущая дата
                            Preferences = string.Empty // Задайте нужные значения по умолчанию
                        };

                        // Добавляем нового гостя в базу данных
                        _context.Guests.Add(guest);
                    }
                    else
                    {
                        // Если гость найден, обновляем его данные
                        guest.FirstName = currentUser.Firstname;
                        guest.MiddleName = currentUser.Middlename;
                        guest.LastName = currentUser.Lastname;
                        _context.Guests.Update(guest);
                    }

                    // Сохраняем изменения в базе данных
                    _context.SaveChanges();

                    // Проверка роли пользователя
                    if (currentUser.Role == "Admin")
                    {
                        MessageBox.Show("Добро пожаловать, Админ!");
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                    }
                    else if (currentUser.Role == "User")
                    {
                        MessageBox.Show("Добро пожаловать, Пользователь!");
                        var userWindow = new UserMainWindow();
                        userWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка! Неизвестная роль пользователя.");
                    }

                    // Закрываем окно авторизации
                    Application.Current.Windows[0].Close();
                }
                else
                {
                    MessageBox.Show("Ошибка! Не удалось найти данные пользователя.");
                }
            }
            else
            {
                // Если логин или пароль неверные, показываем ошибку
                MessageBox.Show("Ошибка! Неверный логин или пароль.");
            }
        }
    }
}
