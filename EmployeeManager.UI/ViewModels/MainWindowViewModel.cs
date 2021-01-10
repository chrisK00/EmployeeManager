using EmployeeManager.DataRepository;
using EmployeeManager.DataRepository.Developers;
using EmployeeManager.DataRepository.Logic;
using EmployeeManager.DataRepository.Services;
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
        #region Properties

        public IEnumerable<IEmployee> Employees => GetEmployees();

        #endregion Properties

        #region View Models

        public ObservableCollection<EmployeeViewModel> EmployeeViewModels { get; set; } = new ObservableCollection<EmployeeViewModel>();

        #endregion View Models

        #region Commands

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SearchSortCommand { get; set; }

        #endregion Commands

        #region Constructor

        public MainWindowViewModel()
        {
            AddEmployeeCommand = new DelegateCommand(AddEmployee);
            SearchSortCommand = new DelegateCommand<string>(SearchSort);

            EmployeeLogic.SetCultureToUS();
            //Load employees from Db
            var loadedEmployees = DataAccess.LoadEmployees();
            //Create a employee viewmodel for each employee in loadedEmployees
            loadedEmployees.ForEach(emp => CreateEmployeeViewModel(emp));
        }

        #endregion Constructor

        #region Methods

        private void SearchSort(string filter)
        {
            switch (filter)
            {
                case "job":
                    var sortedByJob = EmployeeViewModels.OrderBy(emp => emp.Employee.TypeOfEmployee).ToList();
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
            var newEmployee = new FrontEndDev
            {
                Name = "Name",
                Email = "Email", //This will also be auto generated but the user can change
                PhoneNumber = "PhoneNumber", // Modify so that you have the string format in the view.
                Salary = 0 //This will depend on the type of employee and non changeable
            };

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
            viewModelToAdd.Employee.EmployeeTypeChanged += ChangeEmployeeType;

            // Add the view model to the list.
            EmployeeViewModels.Add(viewModelToAdd);
        }

        private void FireEmployee(object sender, EventArgs e)
        {
            EmployeeViewModels.Remove(sender as EmployeeViewModel);
        }

        //TODO REFACTOR
        private void ChangeEmployeeType(object sender, EventArgs e)
        {
            IEmployee newEmployee = null;
            var employee = sender as FrontEndDev;
            if (employee != null)
            {
                switch (employee.TypeOfEmployee)
                {
                    case TypeOfEmployee.Developer:
                        break;
                    case TypeOfEmployee.CustomerSupport:
                        newEmployee = new FrontEndDev
                        {
                            Name = employee.Name,
                            Email = employee.Email,
                            PhoneNumber = employee.PhoneNumber,
                            TypeOfEmployee = employee.TypeOfEmployee,
                            Salary = 0
                        };
                        break;
                    case TypeOfEmployee.SalesPerson:
                        break;
                    default:
                        break;
                }
                if (newEmployee is null)
                {
                    return;
                }
                var viewModelToAdd = new EmployeeViewModel
                {
                    Employee = newEmployee
                };
                viewModelToAdd.EmployeeFired += FireEmployee;
                viewModelToAdd.Employee.EmployeeTypeChanged += ChangeEmployeeType;
                EmployeeViewModels.Add(viewModelToAdd);
                var newEmpIndex = EmployeeViewModels.IndexOf(viewModelToAdd);
                EmployeeViewModels.RemoveAt(newEmpIndex - 1);
            }

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