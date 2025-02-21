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


        // Свойства для отображения в профиле
        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged(nameof(Lastname));
                    OnPropertyChanged(nameof(FullName)); // Обновляем FullName
                }
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (_firstname != value)
                {
                    _firstname = value;
                    OnPropertyChanged(nameof(Firstname));
                    OnPropertyChanged(nameof(FullName)); // Обновляем FullName
                }
            }
        }

        private string _middlename;
        public string Middlename
        {
            get => _middlename;
            set
            {
                if (_middlename != value)
                {
                    _middlename = value;
                    OnPropertyChanged(nameof(Middlename));
                    OnPropertyChanged(nameof(FullName)); // Обновляем FullName
                }
            }
        }

        private DateTime? _dateOfBirth;
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged(nameof(DateOfBirth));
                }
            }
        }

        private int _passportNumber;
        public int PassportNumber
        {
            get => _passportNumber;
            set
            {
                if (_passportNumber != value)
                {
                    _passportNumber = value;
                    OnPropertyChanged(nameof(PassportNumber));
                }
            }
        }

        private string _contactDetails;
        public string ContactDetails
        {
            get => _contactDetails;
            set
            {
                if (_contactDetails != value)
                {
                    _contactDetails = value;
                    OnPropertyChanged(nameof(ContactDetails));
                }
            }
        }

        private DateTime? _registrationDate;
        public DateTime? RegistrationDate
        {
            get => _registrationDate;
            set
            {
                if (_registrationDate != value)
                {
                    _registrationDate = value;
                    OnPropertyChanged(nameof(RegistrationDate));
                }
            }
        }

        private string _preferences;
        public string Preferences
        {
            get => _preferences;
            set
            {
                if (_preferences != value)
                {
                    _preferences = value;
                    OnPropertyChanged(nameof(Preferences));
                }
            }
        }

       

        // Свойство для полного имени
        public string FullName
        {
            get
            {
                // Собираем полное имя из частей
                return $"{Lastname} {Firstname} {Middlename}".Trim();
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    // Разбиваем введенное значение на части
                    var nameParts = value.Split(' ');

                    // Присваиваем Lastname, Firstname и Middlename
                    Lastname = nameParts.Length > 0 ? nameParts[0] : string.Empty;
                    Firstname = nameParts.Length > 1 ? nameParts[1] : string.Empty;
                    Middlename = nameParts.Length > 2 ? nameParts[2] : string.Empty;

                    // Уведомляем об изменении свойств
                    OnPropertyChanged(nameof(FullName));
                    OnPropertyChanged(nameof(Lastname));
                    OnPropertyChanged(nameof(Firstname));
                    OnPropertyChanged(nameof(Middlename));
                }
            }
        }

        // Команды
        public ICommand SaveCommand { get; }

        // Конструктор
        public AccountViewModel(Guid userId)
        {
            _context = new SqlServerContext();

            // Загружаем данные гостя
            _currentGuest = _context.Guests.FirstOrDefault(g => g.Id == userId);

            if (_currentGuest != null)
            {
                LoadGuestData();
                Lastname = _currentGuest.LastName ?? string.Empty;
                Firstname = _currentGuest.FirstName ?? string.Empty;
                Middlename = _currentGuest.MiddleName ?? string.Empty;
                DateOfBirth = _currentGuest.DateOfBirth;
                PassportNumber = _currentGuest.PassportNumber != 0 ? _currentGuest.PassportNumber : 0;
                ContactDetails = _currentGuest.ContactDetails ?? string.Empty;
                RegistrationDate = _currentGuest.RegistrationDate;
                Preferences = _currentGuest.Preferences ?? string.Empty;
            }
            else
            {
                MessageBox.Show("Ошибка загрузки профиля.");
            }

            // Команды
            SaveCommand = new RelayCommand(SaveProfile);
        }


        private void LoadGuestData()
        {
            if (_currentGuest == null)
            {
                MessageBox.Show("Ошибка: данные пользователя отсутствуют.");
                return;
            }

            // Принудительное обновление данных из БД
            _context.Entry(_currentGuest).Reload();

            // Присваивание значений из обновленного объекта
            Lastname = _currentGuest.LastName ?? string.Empty;
            Firstname = _currentGuest.FirstName ?? string.Empty;
            Middlename = _currentGuest.MiddleName ?? string.Empty;
            DateOfBirth = _currentGuest.DateOfBirth;
            PassportNumber = _currentGuest.PassportNumber;
            ContactDetails = _currentGuest.ContactDetails ?? string.Empty;
            RegistrationDate = _currentGuest.RegistrationDate;
            Preferences = _currentGuest.Preferences ?? string.Empty;

            // Обновление привязок
            OnPropertyChanged(nameof(Lastname));
            OnPropertyChanged(nameof(Firstname));
            OnPropertyChanged(nameof(Middlename));
            OnPropertyChanged(nameof(DateOfBirth));
            OnPropertyChanged(nameof(PassportNumber));
            OnPropertyChanged(nameof(ContactDetails));
            OnPropertyChanged(nameof(RegistrationDate));
            OnPropertyChanged(nameof(Preferences));
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
            _currentGuest.DateOfBirth = DateOfBirth ?? DateTime.MinValue;
            _currentGuest.PassportNumber = PassportNumber;
            _currentGuest.ContactDetails = ContactDetails;
            _currentGuest.Preferences = Preferences;

            // Сохраняем изменения
            _context.Guests.Update(_currentGuest);
            _context.SaveChanges();

            LoadGuestData();

            MessageBox.Show("Данные сохранены!");
        }
    }
}
