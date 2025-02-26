using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.Service
{
    public enum TypeNumder
    {
        Standart,
        Studio,
        Suite,
        Apartment
    };

    public class Nomer
    {
        private string _imagePath;

        public string ImagePath
        {
            get => _imagePath;
        }
        public Nomer(Guid id, int number, int floor, bool status, decimal cost, string description, TypeNumder typeNumder)
        {
            Id = id;
            Number = number;
            Floor = floor;
            Status = status;
            Cost = cost;
            Description = description;
            TypeNumder = typeNumder;

            // Устанавливаем путь к изображению при создании объекта
            _imagePath = typeNumder switch
            {
                TypeNumder.Standart => "/Assets/Resources/standart.jpg",
                TypeNumder.Studio => "/Assets/Resources/studio.jpg",
                TypeNumder.Suite => "/Assets/Resources/suite.jpg",
                TypeNumder.Apartment => "/Assets/Resources/apartment.jpg",
                _ => "/Assets/Resources/default.jpg"
            };
        }
        public Nomer()
        {
        }

        public Guid Id { get; set; }
        
       public TypeNumder TypeNumder { get; set; }   
        public int Number {  get; set; }
        public int Floor { get; set; }
        public bool Status { get; set; }
        public decimal Cost {  get; set; }
        public string Description {  get; set; }

        [NotMapped]

        public string TypeNumberString
        {
            get
            {
                switch (TypeNumder)
                {
                    case TypeNumder.Standart:
                        return "Стандарт";
                    case TypeNumder.Studio:
                        return "Студия";
                    case TypeNumder.Suite:
                        return "Люкс";
                    case TypeNumder.Apartment:
                        return "Аппартаменты";
                    default:
                        return "Не выбрано";
                }
            }
        }
       
        public override string ToString()
        {
            
            return $"{Number},{TypeNumder},{Floor},{Status},{Cost},{Description}";
        }
    }
}
