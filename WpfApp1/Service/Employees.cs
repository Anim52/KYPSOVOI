using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Service
{
    public class Employees
    {
        public Employees(Guid id, string lastName, string firstName, string middleName, string jobTitle, string contactDetails)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            JobTitle = jobTitle;
            ContactDetails = contactDetails;

        }
        public Employees()
        {
        }

        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string JobTitle { get; set; }
        public string ContactDetails { get; set; }
        public string FullName => $"{LastName},{FirstName},{MiddleName}";
        public override string ToString()
        {
            return $"{FullName},{JobTitle},{ContactDetails}";
        }
    }
}
