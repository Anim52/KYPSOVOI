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
using WpfApp1.ViewModel;
using WpfApp1.Service;

namespace WpfApp1.PageModelViews
{
    public class ServiceModelPage : BaseViewModel
    {
        private readonly SqlServerContext _context;
        private Services _selectedService;
        private string _newRequestDescription;
        private bool _isAdmin;

        // Конструктор
        public ServiceModelPage(Guid userId, bool isAdmin)
        {
            _context = new SqlServerContext();
            ServiceRequests = new ObservableCollection<Services>();
            _isAdmin = isAdmin;

            // Загружаем данные
            LoadServiceRequests();

            // Команды
            CreateRequestCommand = new RelayCommand(CreateRequest);
            CompleteRequestCommand = new RelayCommand(CompleteRequest, CanModifyRequest);
            DeleteRequestCommand = new RelayCommand(DeleteRequest, CanModifyRequest);
        }

        // Свойство для новых заявок
        public string NewRequestDescription
        {
            get => _newRequestDescription;
            set
            {
                _newRequestDescription = value;
                OnPropertyChanged(nameof(NewRequestDescription));
            }
        }

        // Свойство для выбранной заявки
        public Services SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged(nameof(SelectedService));
            }
        }

        // Коллекция для отображения заявок
        public ObservableCollection<Services> ServiceRequests { get; set; }

        // Команды
        public ICommand CreateRequestCommand { get; }
        public ICommand CompleteRequestCommand { get; }
        public ICommand DeleteRequestCommand { get; }

        // Метод для загрузки заявок из БД
        private void LoadServiceRequests()
        {
            ServiceRequests.Clear();
            var requests = _context.Services.ToList();

            foreach (var request in requests)
            {
                ServiceRequests.Add(request);
            }
        }

        // Создать новую заявку (доступно только для юзера)
        private void CreateRequest(object obj)
        {
            if (string.IsNullOrWhiteSpace(NewRequestDescription))
            {
                MessageBox.Show("Описание не может быть пустым.");
                return;
            }

            var newRequest = new Services
            {
                Description = NewRequestDescription,
                RequestDate = DateTime.Now,
                Status = "В обработке"
            };

            _context.Services.Add(newRequest);
            _context.SaveChanges();

            ServiceRequests.Add(newRequest);
            NewRequestDescription = string.Empty;
        }

        // Пометить как выполнено (только админ)
        private void CompleteRequest(object obj)
        {
            if (SelectedService != null && _isAdmin)
            {
                SelectedService.Status = "Выполнено";
                _context.Services.Update(SelectedService);
                _context.SaveChanges();
                LoadServiceRequests();
            }
        }

        // Удалить заявку (только админ)
        private void DeleteRequest(object obj)
        {
            if (SelectedService != null && _isAdmin)
            {
                _context.Services.Remove(SelectedService);
                _context.SaveChanges();
                LoadServiceRequests();
            }
        }

        // Проверка, можно ли изменить заявку (только админ)
        private bool CanModifyRequest(object obj)
        {
            return SelectedService != null && _isAdmin;
        }
    }
}
