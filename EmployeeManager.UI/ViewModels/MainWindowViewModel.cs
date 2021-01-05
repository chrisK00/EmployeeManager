using Prism.Commands;
using Prism.Mvvm;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using EmployeeManager.Models;
using EmployeeManager.Models.Developers;

namespace EmployeeManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Properties
        public IEnumerable<IEmployee> Employees => GetEmployees();
        #endregion

        #region View Models
        public ObservableCollection<EmployeeViewModel> EmployeeViewModels { get; set; } = new ObservableCollection<EmployeeViewModel>();
        #endregion

        #region Commands
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand SearchSortCommand { get; set; }
        #endregion

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
        #endregion

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
                Name = "Peter Griffin",
                Email = "peter.griffin@quahog.net",
                PhoneNumber = "46-741-769", // Modify so that you have the string format in the view.
                Salary = 40000
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

            // Add the view model to the list.
            EmployeeViewModels.Add(viewModelToAdd);
        }

        private void FireEmployee(object sender, EventArgs e)
        {
            EmployeeViewModels.Remove(sender as EmployeeViewModel);
        }

        private IEnumerable<IEmployee> GetEmployees()
        {
            // Give me the employee of each view model.
            return EmployeeViewModels
                .Select(x => x.Employee);
        }
        #endregion
    }
}