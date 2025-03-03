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
    public class EditNomerViewModel : BaseViewModel
    {
        private readonly SqlServerContext _context;

        public ObservableCollection<TypeNumder> TypeNumderList { get; set; }

        private Nomer _selectedNomer;
        public Nomer SelectedNomer
        {
            get => _selectedNomer;
            set
            {
                _selectedNomer = value;
                OnPropertyChanged(nameof(SelectedNomer));
                LoadNomerData();
            }
        }

        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        private int _floor;
        public int Floor
        {
            get => _floor;
            set
            {
                _floor = value;
                OnPropertyChanged(nameof(Floor));
            }
        }

        private decimal _cost;
        public decimal Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private TypeNumder _selectedTypeNumder;
        public TypeNumder SelectedTypeNumder
        {
            get => _selectedTypeNumder;
            set
            {
                _selectedTypeNumder = value;
                OnPropertyChanged(nameof(SelectedTypeNumder));
            }
        }

        private bool _status;
        public bool Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ICommand EditNomerCommand { get; }

        public EditNomerViewModel(Nomer selectedNomer)
        {
            _context = new SqlServerContext();

            // Инициализация списка типов номеров
            TypeNumderList = new ObservableCollection<TypeNumder>(Enum.GetValues(typeof(TypeNumder)).Cast<TypeNumder>());

            SelectedNomer = selectedNomer;

            EditNomerCommand = new RelayCommand(EditNomer);
        }

        private void LoadNomerData()
        {
            if (SelectedNomer != null)
            {
                Number = SelectedNomer.Number;
                Floor = SelectedNomer.Floor;
                Cost = SelectedNomer.Cost;
                Description = SelectedNomer.Description;
                SelectedTypeNumder = SelectedNomer.TypeNumder;
                Status = SelectedNomer.Status;
            }
        }

        private void EditNomer(object obj)
        {
            var nomerToEdit = _context.Nomers.FirstOrDefault(n => n.Id == SelectedNomer.Id);
            if (nomerToEdit != null)
            {
                nomerToEdit.Number = Number;
                nomerToEdit.Floor = Floor;
                nomerToEdit.Cost = Cost;
                nomerToEdit.Description = Description;
                nomerToEdit.TypeNumder = SelectedTypeNumder;
                nomerToEdit.Status = Status;

                _context.SaveChanges();

                MessageBox.Show("Изменения успешно сохранены!");
            }
        }
    }
}
