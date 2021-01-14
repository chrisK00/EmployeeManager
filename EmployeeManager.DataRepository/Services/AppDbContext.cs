using EmployeeManager.DataRepository.Employees;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace EmployeeManager.DataRepository.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(
             DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=Emp.db");
            base.OnConfiguring(optionsBuilder);
        }
       
    }
}