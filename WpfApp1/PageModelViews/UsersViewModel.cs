using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfApp1.Command;
using WpfApp1.Context;
using WpfApp1.Service;
using WpfApp1.ViewModel;

namespace WpfApp1.PageModelViews
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly SqlServerContext _context;

        // Коллекция пользователей
        public ObservableCollection<Guests> UsersList { get; set; }

        // Выбранный пользователь
        private Guests _selectedUser;
        public Guests SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        // Команда для удаления пользователя
        public ICommand DeleteUserCommand { get; }

        // Конструктор
        public UsersViewModel()
        {
            _context = new SqlServerContext();

            // Загружаем список пользователей из базы данных
            UsersList = new ObservableCollection<Guests>(_context.Guests.ToList());

            // Инициализируем команду удаления
            DeleteUserCommand = new RelayCommand(DeleteUser);
        }

        // Логика удаления пользователя и гостя
        private void DeleteUser(object obj)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.");
                return;
            }

            var result = MessageBox.Show($"Вы уверены, что хотите удалить пользователя: {SelectedUser.FirstName} {SelectedUser.LastName}?",
                                         "Подтверждение удаления",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Находим пользователя (User), связанного с этим гостем
                var userToDelete = _context.User.FirstOrDefault(u => u.Id == SelectedUser.Id);

                if (userToDelete != null)
                {
                    _context.User.Remove(userToDelete);
                }

                // Удаляем гостя из базы данных
                _context.Guests.Remove(SelectedUser);
                _context.SaveChanges();

                // Обновляем список
                UsersList.Remove(SelectedUser);
                MessageBox.Show("Пользователь успешно удален!");
            }
        }
    }
}
