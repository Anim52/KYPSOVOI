﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Services
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
        public Nomer(Guid id, int number, int floor, bool status, decimal cost, string description)
        {
            Id = id;
            Number = number;
            Floor = floor;
            Status = status;
            Cost = cost;
            Description = description;
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
