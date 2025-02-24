﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WpfApp1.Service
{
    public class Services
    {
        public Services(Guid id, Guid userId, string description, DateTime requestDate, string status)
        {
            Id = id;
            UserId = userId;
            Description = description;
            RequestDate = requestDate;
            Status = status;
        }

        public Services()
        {

        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "В обработке";
        public User User { get; set; }

        // Новый проперт для отображения ФИО с инициалами
        public string Fullname => $"{User.Lastname} {User.Firstname[0]}. {User.Middlename[0]}.";



    }
}
