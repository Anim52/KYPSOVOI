using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Service;

namespace WpfApp1.Context
{
    public class SqlServerContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Guests> Guests { get; set; }
        public DbSet<Nomer> Nomers { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Database = ToApp");
        }
    }
}
