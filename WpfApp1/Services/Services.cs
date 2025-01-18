using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Services
{
    public class Services
    {
        public Services(Guid id, Employees employees, string description, decimal cost, string typeServices, string nameServices)
        {
            Id = id;
            Employees = employees;
            Description = description;
            Cost = cost;
            TypeServices = typeServices;
            NameServices = nameServices;
        }
        public Services()
        {
            
        }
        public Guid Id { get; set; }
        public  Employees Employees { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string TypeServices { get; set; }
        public string NameServices { get; set; }

        public override string ToString()
        {
            return $"{Employees},{Description},{Cost},{TypeServices},{NameServices}";

        }
    }
}
