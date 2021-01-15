using System;
using System.Collections.Generic;

namespace EmployeeManager.DataRepository.Employees
{
    public class Employee : IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Role> Roles { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public List<Employee> ReportsTo { get; set; }

        public Employee()
        {
            ReportsTo = new List<Employee>();
            Roles = new List<Role>();
            Email = "";
            PhoneNumber = "";
        }
    }
}