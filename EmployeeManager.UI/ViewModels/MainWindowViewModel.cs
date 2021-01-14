﻿using EmployeeManager.DataRepository.Employees;
using EmployeeManager.DataRepository.Logic;
using EmployeeManager.DataRepository.Services;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EmployeeManager.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly AppDbContext _db;

        #region Properties
        public IRole SelectedRole { get; set; }
        public string DisplayCurrentDate { get => DateTime.Now.ToShortDateString(); }
        public IEnumerable<IEmployee> Employees => GetEmployees();

        #endregion Properties

        #region View Models

        public ObservableCollection<IEmployeeViewModel> EmployeeViewModels { get; set; } = new ObservableCollection<IEmployeeViewModel>();
        //consider iroleviewmodel for delete, test textbox remove press del, also make a view
        public ObservableCollection<RoleViewModel> RoleViewModels { get; set; } = new ObservableCollection<RoleViewModel>();
        #endregion View Models

        #region Commands

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand AddRoleCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SearchSortCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        #endregion Commands

        #region Constructor

        public MainWindowViewModel(AppDbContext db)
        {
            AddEmployeeCommand = new DelegateCommand(AddEmployee);
            SearchSortCommand = new DelegateCommand<string>(SearchSort);
            SaveCommand = new DelegateCommand(Save);
            AddRoleCommand = new DelegateCommand(AddRole);

            this._db = db;
            EmployeeLogic.SetCultureToUS();

            //Include another table
            var loadedEmployees = _db.Employees.Include(x => x.Roles).Select(x => x).ToList();
            loadedEmployees.ForEach(emp => CreateEmployeeViewModel(emp));

            var loadedRoles = _db.Roles.Select(r => r).ToList();
            loadedRoles.ForEach(r => CreateRoleViewModel(r));
        }

        private void CreateRoleViewModel(IRole newRole)
        {
            // Plugging the model into the view model.
            var viewModelToAdd = new RoleViewModel
            {
                Role = newRole
            };

            // Handle the "Fire Employee" button when it's clicked
            viewModelToAdd.RoleRemoved += RemoveRole;

            // Add the view model to the list.
            RoleViewModels.Add(viewModelToAdd);
        }

        private void RemoveRole(object sender, EventArgs e)
        {
            RoleViewModels.Remove(sender as RoleViewModel);

            //TODO FIX BUG? crash as if it was null
            //   _db.Roles.Remove(sender as Role);
        }

        #endregion Constructor

        #region Methods
        private void Save()
        {
            _db.SaveChanges();
        }
        private void SearchSort(string filter)
        {
            switch (filter)
            {
                case "salary":
                    var sortedBySalary = EmployeeViewModels.OrderBy(emp => emp.Employee.Salary).ToList();
                    EmployeeViewModels.Clear();
                    sortedBySalary.ForEach(emp => EmployeeViewModels.Add(emp));
                    break;

                case "name":
                    var sortedByName = EmployeeViewModels.OrderBy(emp => emp.Employee.Name).ToList();
                    EmployeeViewModels.Clear();
                    sortedByName.ForEach(emp => EmployeeViewModels.Add(emp));
                    break;
            }
        }

        private void AddEmployee()
        {
            // todo: Which employee am I adding?
            // Add logic here.

            // Creating a new model.
            var newEmployee = new Employee
            {
                Name = "Name",
                Email = "Email", //This will also be auto generated but the user can change
                PhoneNumber = "PhoneNumber", // Modify so that you have the string format in the view.
                Salary = 0M, //This will depend on the type of employee and non changeable
                StartDate = new DateTime(year: 2021, 1, 11),
                BirthDate = new DateTime(1996, 4, 20)
            };

            _db.Add(newEmployee);
            // Create the view model.
            CreateEmployeeViewModel(newEmployee);
        }

        private void AddRole()
        {
            var newRole = new Role()
            {
                Name = "Developer",
                BaseSalary = 100
            };
            _db.Add(newRole);
            _db.SaveChanges();
            // Create the view model.
            CreateRoleViewModel(newRole);
        }

        private void CreateEmployeeViewModel(IEmployee newEmployee)
        {
            // Plugging the model into the view model.
            var viewModelToAdd = new EmployeeViewModel
            {
                Employee = newEmployee
            };

            // Handle the "Fire Employee" button when it's clicked
            viewModelToAdd.EmployeeFired += FireEmployee;

            // Add the view model to the list.
            EmployeeViewModels.Add(viewModelToAdd);
        }

        private void FireEmployee(object sender, EventArgs e)
        {
            EmployeeViewModels.Remove(sender as EmployeeViewModel);
          
            _db.Remove(sender as Employee);
        }

        private IEnumerable<IEmployee> GetEmployees()
        {
            // Give me the employee of each view model.
            return EmployeeViewModels
                .Select(x => x.Employee);
        }

        #endregion Methods
    }
}