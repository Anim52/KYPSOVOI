using System;
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

namespace WpfApp1.ViewModel
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        // Команды
        public RelayCommand LoginCommand { get; }

        // Конструктор
        public AuthViewModel()
        {
            _authService = new AuthService(); // Инициализация сервиса аутентификации
            LoginCommand = new RelayCommand(LoginExecute, CanLoginExecute); // Создание команды
        }

        // Метод, который проверяет возможность входа (например, если поля не пустые)
        private bool CanLoginExecute(object obj)
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }

        // Метод для обработки логики входа
        private void LoginExecute(object obj)
        {
            // Создаем переменную для хранения пользователя
            User? user = null;

            // Проверяем, если логин и пароль правильные
            if (_authService.Login(Login, Password, out user))
            {
                MessageBox.Show("Успешный вход!");

                // Создаем и открываем окно MainWindow
                var mainWindow = new MainWindow();
                mainWindow.Show();

                // Закрываем окно авторизации
                Application.Current.Windows[0].Close();
            }
            else
            {
                // Если логин или пароль неверные, показываем ошибку
                MessageBox.Show("Ошибка! Неверный логин или пароль.");
            }
        }
    }
}
