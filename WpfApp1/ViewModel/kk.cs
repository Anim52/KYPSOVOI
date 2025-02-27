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
    public class kk
    {
        //private readonly SqlServerContext _context;
        //public Nomer SelectedNomer { get; set; }

        //public string Number { get; set; }
        //public int Floor { get; set; }
        //public decimal Cost { get; set; }
        //public string Description { get; set; }
        //public TypeNumder SelectedTypeNumder { get; set; }
        //public ObservableCollection<TypeNumder> TypeNumderList { get; set; }

        //public ICommand SaveChangesCommand { get; }

        //public kk(Nomer nomer)
        //{
        //    _context = new SqlServerContext();
        //    SelectedNomer = nomer;

        //    // Заполняем поля данными из выбранного номера
        //    Number = nomer.Number;
        //    Floor = nomer.Floor;
        //    Cost = nomer.Cost;
        //    Description = nomer.Description;
        //    SelectedTypeNumder = nomer.TypeNumder;

        //    // Получаем список типов номеров
        //    TypeNumderList = new ObservableCollection<Nomer>(_context.TypeNumders.ToList());

        //    // Команда для сохранения изменений
        //    SaveChangesCommand = new RelayCommand(SaveChanges);
        //}

        //private void SaveChanges(object obj)
        //{
        //    var nomerToEdit = _context.Nomers.FirstOrDefault(n => n.Id == SelectedNomer.Id);
        //    if (nomerToEdit != null)
        //    {
        //        // Обновляем данные
        //        nomerToEdit.Number = Number;
        //        nomerToEdit.Floor = Floor;
        //        nomerToEdit.Cost = Cost;
        //        nomerToEdit.Description = Description;
        //        nomerToEdit.TypeNumder = SelectedTypeNumder;

        //        try
        //        {
        //            _context.SaveChanges();
        //            MessageBox.Show("Изменения успешно сохранены!");

        //            // Закрываем окно после сохранения
        //            Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive)?.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Ошибка сохранения изменений: {ex.Message}");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Ошибка! Номер не найден в базе данных.");
        //    }
        //}
    }
}
