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

namespace WpfApp1.ViewModel
{
    public class AddNomerViewModel : BaseViewModel
    {
        private readonly SqlServerContext _context;

        // Поля для ввода данных
        private int _number;
        private int _floor;
        private decimal _cost;
        private string _description;
        private TypeNumder _selectedTypeNumder;

        // Коллекция для хранения номеров
        private ObservableCollection<Nomer> _nomerList;
        private Nomer _selectedNomer;

        // Конструктор
        public AddNomerViewModel()
        {
            _context = new SqlServerContext();
            AddNomerCommand = new RelayCommand(AddNomer);
            DeleteNomerCommand = new RelayCommand(DeleteNomer, CanDeleteNomer);
            EditNomerCommand = new RelayCommand(EditNomer, CanEditNomer);

            // Заполняем список типов номеров
            TypeNumderList = new ObservableCollection<TypeNumder>
            {
                TypeNumder.Standart,
                TypeNumder.Studio,
                TypeNumder.Suite,
                TypeNumder.Apartment
            };

            // Инициализируем коллекцию номеров
            NomerList = new ObservableCollection<Nomer>(_context.Nomers.ToList());
        }

        // Свойства для привязки в XAML
        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public int Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                OnPropertyChanged(nameof(Floor));
            }
        }

        public decimal Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public TypeNumder SelectedTypeNumder
        {
            get => _selectedTypeNumder;
            set
            {
                _selectedTypeNumder = value;
                OnPropertyChanged(nameof(SelectedTypeNumder));
            }
        }

        public ObservableCollection<TypeNumder> TypeNumderList { get; set; }

        // Коллекция для отображения номеров
        public ObservableCollection<Nomer> NomerList
        {
            get => _nomerList;
            set
            {
                _nomerList = value;
                OnPropertyChanged(nameof(NomerList));
            }
        }

        // Свойство для выбранного номера
        public Nomer SelectedNomer
        {
            get => _selectedNomer;
            set
            {
                _selectedNomer = value;
                OnPropertyChanged(nameof(SelectedNomer));
            }
        }

        // Команда для добавления номера
        public ICommand AddNomerCommand { get; }

        // Команда для удаления номера
        public ICommand DeleteNomerCommand { get; }

        // Команда для изменения номера
        public ICommand EditNomerCommand { get; }

        // Логика добавления номера
        private void AddNomer(object obj)
        {
            if (Number <= 0 || Floor <= 0 || Cost <= 0 || string.IsNullOrWhiteSpace(Description))
            {
                MessageBox.Show("Все поля должны быть заполнены корректно.");
                return;
            }

            var newNomer = new Nomer
            {
                Id = Guid.NewGuid(),
                Number = Number,
                Floor = Floor,
                Status = true, // По умолчанию - Свободен
                Cost = Cost,
                Description = Description,
                TypeNumder = SelectedTypeNumder
            };

            _context.Nomers.Add(newNomer);
            _context.SaveChanges();

            MessageBox.Show("Номер успешно добавлен!");

            // Добавляем новый номер в коллекцию
            NomerList.Add(newNomer);

            // Очищаем поля ввода
            Number = 0;
            Floor = 0;
            Cost = 0;
            Description = string.Empty;
            SelectedTypeNumder = TypeNumder.Standart;
        }

        // Логика изменения номера
        private void EditNomer(object obj)
        {
            if (SelectedNomer == null)
            {
                MessageBox.Show("Пожалуйста, выберите номер для изменения.");
                return;
            }

            // Открываем окно редактирования с заполненными полями
            Number = SelectedNomer.Number;
            Floor = SelectedNomer.Floor;
            Cost = SelectedNomer.Cost;
            Description = SelectedNomer.Description;
            SelectedTypeNumder = SelectedNomer.TypeNumder;

            var result = MessageBox.Show("Вы уверены, что хотите сохранить изменения?",
                                         "Подтверждение изменений",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Обновляем данные в выбранном номере
                var nomerToEdit = _context.Nomers.FirstOrDefault(n => n.Id == SelectedNomer.Id);
                if (nomerToEdit != null)
                {
                    nomerToEdit.Number = Number;
                    nomerToEdit.Floor = Floor;
                    nomerToEdit.Cost = Cost;
                    nomerToEdit.Description = Description;
                    nomerToEdit.TypeNumder = SelectedTypeNumder;

                    _context.SaveChanges();

                    // Обновляем коллекцию для отображения изменений
                    NomerList = new ObservableCollection<Nomer>(_context.Nomers.ToList());

                    MessageBox.Show("Изменения успешно сохранены!");
                }
            }
        }

        // Метод для проверки возможности изменения
        private bool CanEditNomer(object obj)
        {
            return SelectedNomer != null;
        }

        // Логика удаления номера
        private void DeleteNomer(object obj)
        {
            if (SelectedNomer == null)
            {
                MessageBox.Show("Пожалуйста, выберите номер для удаления.");
                return;
            }

            var result = MessageBox.Show($"Вы уверены, что хотите удалить номер {SelectedNomer.Number}?",
                                         "Подтверждение",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Удаляем номер из базы данных
                _context.Nomers.Remove(SelectedNomer);
                _context.SaveChanges();

                // Удаляем номер из коллекции
                NomerList.Remove(SelectedNomer);

                MessageBox.Show("Номер успешно удален!");

                // Сбрасываем выбор
                SelectedNomer = null;
            }
        }

        // Метод для проверки возможности удаления
        private bool CanDeleteNomer(object obj)
        {
            return SelectedNomer != null;
        }
    }
}
