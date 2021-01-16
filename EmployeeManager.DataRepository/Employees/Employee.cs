using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace EmployeeManager.DataRepository.Employees
{
    public class Employee : BindableBase, IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Role> Roles { get => _roles; set => SetProperty(ref _roles, value); }
        private List<Role> _roles;
        public DateTime StartDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }

        public string DisplaySalary { get => $"{Salary:c}"; }
        public decimal Salary { get => _salary; set { _salary = value; RaisePropertyChanged(DisplaySalary); } }
        private decimal _salary;
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