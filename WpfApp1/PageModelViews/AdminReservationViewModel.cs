using System;
using Microsoft.EntityFrameworkCore;
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
    public class AdminReservationViewModel : BaseViewModel
    {
        private readonly SqlServerContext _context;

        public ObservableCollection<Reservations> ReservationsList { get; set; }
        public Reservations SelectedReservation { get; set; }

        public ICommand SetPopulatedCommand { get; }
        public ICommand CancelReservationCommand { get; }
        public ICommand ConfirmReservationCommand { get; } // Новая команда для подтверждения

        public AdminReservationViewModel()
        {
            _context = new SqlServerContext();

            // Загружаем список бронирований с номерами и гостями
            ReservationsList = new ObservableCollection<Reservations>(_context.Reservations
                .Include(r => r.Nomer)
                .Include(r => r.Guests)
                .ToList());

            // Команды для администратора
            SetPopulatedCommand = new RelayCommand(SetPopulated, CanExecuteCommands);
            CancelReservationCommand = new RelayCommand(CancelReservation, CanExecuteCommands);
            ConfirmReservationCommand = new RelayCommand(ConfirmReservation, CanExecuteCommands); // Инициализация команды
        }

        // Проверка возможности выполнения команд
        private bool CanExecuteCommands(object obj)
        {
            return SelectedReservation != null;
        }

        // Подтверждение бронирования
        private void ConfirmReservation(object obj)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Выберите бронирование для подтверждения!");
                return;
            }

            var result = MessageBox.Show($"Вы уверены, что хотите подтвердить бронирование для номера {SelectedReservation.Nomer.Number}?",
                                         "Подтверждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Изменяем статус бронирования на "Подтверждено"
                SelectedReservation.Status = Status.Populated;
                SelectedReservation.Nomer.Status = false; // Номер занят

                _context.Reservations.Update(SelectedReservation);
                _context.Nomers.Update(SelectedReservation.Nomer);
                _context.SaveChanges();

                MessageBox.Show("Бронирование успешно подтверждено!");

                // Обновляем список бронирований
                ReservationsList = new ObservableCollection<Reservations>(_context.Reservations
                    .Include(r => r.Nomer)
                    .Include(r => r.Guests)
                    .ToList());
            }
        }

        // Заселение гостя
        private void SetPopulated(object obj)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Выберите бронирование для заселения!");
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
                MessageBox.Show("Выберите бронирование для отмены!");
                return;
            }

            _context.Reservations.Remove(SelectedReservation);
            _context.SaveChanges();

            ReservationsList.Remove(SelectedReservation);
            MessageBox.Show("Бронирование отменено!");
        }
    }

}
