using EmployeeManager.DataRepository.Employees;
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
        public string DisplayCurrentDate { get => DateTime.Now.ToShortDateString(); }
        public IEnumerable<IEmployee> Employees => GetEmployees();

        #endregion Properties

        #region View Models

        public ObservableCollection<IEmployeeViewModel> EmployeeViewModels { get; set; } = new ObservableCollection<IEmployeeViewModel>();

        #endregion View Models

        #region Commands

        public ICommand AddEmployeeCommand { get; set; }
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

            this._db = db;
            EmployeeLogic.SetCultureToUS();

            //Include another table
            var loadedEmployees = _db.Employees.Include(x => x.Roles).Select(x => x).ToList();
            loadedEmployees.ForEach(emp => CreateEmployeeViewModel(emp));
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
                case "job":
                    var sortedByJob = EmployeeViewModels.OrderBy(emp => emp.Employee.Roles).ToList();
                    EmployeeViewModels.Clear();
                    sortedByJob.ForEach(emp => EmployeeViewModels.Add(emp));
                    break;

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