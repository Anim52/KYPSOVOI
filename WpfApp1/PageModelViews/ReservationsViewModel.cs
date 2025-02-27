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
    public class ReservationsViewModel : BaseViewModel
    {
        private readonly SqlServerContext _context;

        // Поля для ввода данных
        private Nomer _selectedNomer;
        private Reservations _selectedReservation;
        private DateTime? _arrivalDate;
        private DateTime? _departureDate;
        private int _numberOfPersons;

        // Коллекции для выбора и отображения
        public ObservableCollection<Nomer> NomerList { get; set; }
        public ObservableCollection<Reservations> ReservationList { get; set; }

        // Конструктор
        public ReservationsViewModel()
        {
            _context = new SqlServerContext();
            AddReservationCommand = new RelayCommand(AddReservation);
            CancelReservationCommand = new RelayCommand(CancelReservation);
            SetPopulatedCommand = new RelayCommand(SetPopulated);

            // Загружаем список номеров, которые свободны
            NomerList = new ObservableCollection<Nomer>(_context.Nomers.Where(n => n.Status).ToList());

            // Загружаем список бронирований
            ReservationList = new ObservableCollection<Reservations>(_context.Reservations.ToList());
        }

        // Свойства для привязки в XAML
        public Nomer SelectedNomer
        {
            get => _selectedNomer;
            set
            {
                _selectedNomer = value;
                OnPropertyChanged(nameof(SelectedNomer));
            }
        }

        public Reservations SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged(nameof(SelectedReservation));
            }
        }

        public DateTime? ArrivalDate
        {
            get => _arrivalDate;
            set
            {
                _arrivalDate = value;
                OnPropertyChanged(nameof(ArrivalDate));
            }
        }

        public DateTime? DepartureDate
        {
            get => _departureDate;
            set
            {
                _departureDate = value;
                OnPropertyChanged(nameof(DepartureDate));
            }
        }

        public int NumberOfPersons
        {
            get => _numberOfPersons;
            set
            {
                _numberOfPersons = value;
                OnPropertyChanged(nameof(NumberOfPersons));
            }
        }

        // Команды
        public ICommand AddReservationCommand { get; }
        public ICommand CancelReservationCommand { get; }
        public ICommand SetPopulatedCommand { get; }

        // Логика добавления бронирования
        private void AddReservation(object obj)
        {
            if (SelectedNomer == null || !ArrivalDate.HasValue || !DepartureDate.HasValue || NumberOfPersons <= 0)
            {
                MessageBox.Show("Все поля должны быть заполнены корректно.");
                return;
            }

            if (ArrivalDate >= DepartureDate)
            {
                MessageBox.Show("Дата приезда должна быть раньше даты отъезда.");
                return;
            }

            // Проверяем наличие текущего пользователя
            if (App.CurrentUserId == Guid.Empty)
            {
                MessageBox.Show("Ошибка: Пользователь не авторизован.");
                return;
            }

            // Находим пользователя по Id
            var currentUser = _context.Guests.FirstOrDefault(g => g.Id == App.CurrentUserId);

            if (currentUser == null)
            {
                MessageBox.Show("Ошибка: Пользователь не найден в базе данных.");
                return;
            }

            var newReservation = new Reservations
            {
                Id = Guid.NewGuid(),
                Nomer = SelectedNomer,
                Guests = currentUser, // Привязываем текущего пользователя
                DateReservations = DateTime.Now,
                ArrivalDate = ArrivalDate.Value,
                DepartureDate = DepartureDate.Value,
                NumberOfPersons = NumberOfPersons,
                Status = Status.New
            };

            _context.Reservations.Add(newReservation);
            _context.SaveChanges();

            MessageBox.Show("Бронирование успешно добавлено!");

            // Обновляем список бронирований
            ReservationList.Add(newReservation);

            // Сбрасываем выбор
            SelectedNomer = null;
            ArrivalDate = null;
            DepartureDate = null;
            NumberOfPersons = 0;
        }

        // Логика заселения гостя
        private void SetPopulated(object obj)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Пожалуйста, выберите бронирование.");
                return;
            }

            SelectedReservation.Status = Status.Populated;
            SelectedReservation.Nomer.Status = false; // Номер занят
            _context.Reservations.Update(SelectedReservation);
            _context.Nomers.Update(SelectedReservation.Nomer);
            _context.SaveChanges();

            MessageBox.Show("Гость заселен!");
        }

        // Отмена бронирования
        private void CancelReservation(object obj)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Пожалуйста, выберите бронирование.");
                return;
            }

            SelectedReservation.Nomer.Status = true; // Номер освобожден
            _context.Nomers.Update(SelectedReservation.Nomer);
            _context.Reservations.Remove(SelectedReservation);
            _context.SaveChanges();

            ReservationList.Remove(SelectedReservation);
            MessageBox.Show("Бронирование отменено!");
        }
    }
}
