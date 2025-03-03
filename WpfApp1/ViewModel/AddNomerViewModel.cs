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
using Microsoft.Win32;

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
        private string _imagePath;

        // Коллекция номеров
        private ObservableCollection<Nomer> _nomerList;
        private Nomer _selectedNomer;

        // Конструктор
        public AddNomerViewModel()
        {
            _context = new SqlServerContext();
            AddNomerCommand = new RelayCommand(AddNomer);
            DeleteNomerCommand = new RelayCommand(DeleteNomer, CanDeleteNomer);
            EditNomerCommand = new RelayCommand(EditNomer, CanEditNomer);
            SelectImageCommand = new RelayCommand(SelectImage); // Добавили команду выбора изображения

            TypeNumderList = new ObservableCollection<TypeNumder>
            {
                TypeNumder.Standart,
                TypeNumder.Studio,
                TypeNumder.Suite,
                TypeNumder.Apartment
            };

            // Загружаем список номеров из базы
            NomerList = new ObservableCollection<Nomer>(_context.Nomers.ToList());
        }

        // Свойства для привязки данных
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

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        public ObservableCollection<TypeNumder> TypeNumderList { get; set; }

        public ObservableCollection<Nomer> NomerList
        {
            get => _nomerList;
            set
            {
                _nomerList = value;
                OnPropertyChanged(nameof(NomerList));
            }
        }

        public Nomer SelectedNomer
        {
            get => _selectedNomer;
            set
            {
                _selectedNomer = value;
                OnPropertyChanged(nameof(SelectedNomer));
            }
        }

        // Команды
        public ICommand AddNomerCommand { get; }
        public ICommand DeleteNomerCommand { get; }
        public ICommand EditNomerCommand { get; }
        public ICommand SelectImageCommand { get; }

        // Метод выбора изображения
        private void SelectImage(object obj)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };

            if (dlg.ShowDialog() == true)
            {
                ImagePath = dlg.FileName;
            }
        }

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
                Status = true,
                Cost = Cost,
                Description = Description,
                TypeNumder = SelectedTypeNumder,
                ImagePath = ImagePath // Сохраняем путь к изображению
            };

            _context.Nomers.Add(newNomer);
            _context.SaveChanges();

            MessageBox.Show("Номер успешно добавлен!");

            NomerList.Add(newNomer);

            // Очищаем поля
            Number = 0;
            Floor = 0;
            Cost = 0;
            Description = string.Empty;
            SelectedTypeNumder = TypeNumder.Standart;
            ImagePath = null;
        }

        // Логика изменения номера
        private void EditNomer(object obj)
        {
            if (SelectedNomer == null)
            {
                MessageBox.Show("Пожалуйста, выберите номер для изменения.");
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите сохранить изменения?",
                                         "Подтверждение изменений",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var nomerToEdit = _context.Nomers.FirstOrDefault(n => n.Id == SelectedNomer.Id);
                if (nomerToEdit != null)
                {
                    nomerToEdit.Number = Number;
                    nomerToEdit.Floor = Floor;
                    nomerToEdit.Cost = Cost;
                    nomerToEdit.Description = Description;
                    nomerToEdit.TypeNumder = SelectedTypeNumder;
                    nomerToEdit.ImagePath = ImagePath; // Обновляем путь к изображению

                    _context.SaveChanges();

                    NomerList = new ObservableCollection<Nomer>(_context.Nomers.ToList());

                    MessageBox.Show("Изменения успешно сохранены!");
                }
            }
        }

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
                _context.Nomers.Remove(SelectedNomer);
                _context.SaveChanges();

                NomerList.Remove(SelectedNomer);

                MessageBox.Show("Номер успешно удален!");

                SelectedNomer = null;
            }
        }

        private bool CanDeleteNomer(object obj)
        {
            return SelectedNomer != null;
        }
    }
}
