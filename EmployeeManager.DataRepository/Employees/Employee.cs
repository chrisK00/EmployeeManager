using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EmployeeManager.DataRepository.Employees
{
    public class Employee : BindableBase, IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Role> Roles { get; set; }
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
            Roles = new ObservableCollection<Role>();
            Email = "";
            PhoneNumber = "";
        }
    }
}