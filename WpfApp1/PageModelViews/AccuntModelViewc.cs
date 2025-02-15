using System;
using System.Collections.Generic;
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
    public class AccountViewModel : BaseViewModel
    {
        private readonly SqlServerContext _context;
        private Guests _currentGuest;
        private bool _isEditing;

        // Свойства для отображения в профиле
        public string Firstname { get; set; } = string.Empty;
        public string Middlename { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; } = null; // Nullable тип
        public int PassportNumber { get; set; } = 0;
        public string ContactDetails { get; set; } = string.Empty;
        public DateTime? RegistrationDate { get; set; } = null; // Nullable тип
        public string Preferences { get; set; } = string.Empty;

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing)); // Уведомляем об изменении
            }
        }

        // Свойство для полного имени
        public string FullName
        {
            get
            {
                return _currentGuest != null ? _currentGuest.FullName : string.Empty;
            }
            set
            {
                if (_currentGuest != null)
                {
                    var nameParts = value.Split(' ');

                    if (nameParts.Length > 0) _currentGuest.LastName = nameParts[0]; // Присваиваем LastName (Фамилия)
                    if (nameParts.Length > 1) _currentGuest.FirstName = nameParts[1]; // Присваиваем FirstName (Имя)
                    if (nameParts.Length > 2) _currentGuest.MiddleName = nameParts[2]; // Присваиваем MiddleName (Отчество)

                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        // Команды
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }

        // Конструктор
        public AccountViewModel(Guid userId)
        {
            _context = new SqlServerContext();

            // Загружаем данные гостя
            _currentGuest = _context.Guests.FirstOrDefault(g => g.Id == userId);

            if (_currentGuest != null)
            {
                Lastname = _currentGuest.LastName ?? string.Empty;
                Firstname = _currentGuest.FirstName ?? string.Empty;
                Middlename = _currentGuest.MiddleName ?? string.Empty;
                DateOfBirth = _currentGuest.DateOfBirth; // Здесь будет корректно работать
                PassportNumber = _currentGuest.PassportNumber != 0 ? _currentGuest.PassportNumber : 0;
                ContactDetails = _currentGuest.ContactDetails ?? string.Empty;
                RegistrationDate = _currentGuest.RegistrationDate; // Используем Nullable тип
                Preferences = _currentGuest.Preferences ?? string.Empty;
            }
            else
            {
                MessageBox.Show("Ошибка загрузки профиля.");
            }

            IsEditing = false;

            // Команды
            EditCommand = new RelayCommand(EditProfile);
            SaveCommand = new RelayCommand(SaveProfile);
        }

        // Метод для редактирования профиля
        private void EditProfile(object obj)
        {
            IsEditing = true;
        }

        // Метод для сохранения профиля
        private void SaveProfile(object obj)
        {
            if (string.IsNullOrEmpty(Firstname) || string.IsNullOrEmpty(Lastname))
            {
                MessageBox.Show("ФИО не может быть пустым!");
                return;
            }

            // Обновляем данные в Guests
            _currentGuest.FirstName = Firstname;
            _currentGuest.MiddleName = Middlename;
            _currentGuest.LastName = Lastname;
            _currentGuest.DateOfBirth = DateOfBirth ?? DateTime.MinValue; // Если дата пустая, присваиваем минимальную
            _currentGuest.PassportNumber = PassportNumber;
            _currentGuest.ContactDetails = ContactDetails;
            _currentGuest.Preferences = Preferences;

            // Сохраняем изменения
            _context.Guests.Update(_currentGuest);
            _context.SaveChanges();

            MessageBox.Show("Данные сохранены!");
            IsEditing = false;
        }
    }
}
