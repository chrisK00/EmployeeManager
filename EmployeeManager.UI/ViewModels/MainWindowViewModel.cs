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
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeManager.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly AppDbContext _db;
        private string _message;

        #region Properties

        public string Message { get => _message; set => SetProperty(ref _message, value); }
        private RoleViewModel _selRole;

        public RoleViewModel SelectedRole
        {
            get => _selRole; set
            {
                _selRole = value; RaisePropertyChanged();
            }
        }

        public static string DisplayCurrentDate { get => DateTime.Now.ToShortDateString(); }
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

        #endregion Constructor

        #region Methods

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
            //Should check so roleVM isnt null.
            var roleVM = sender as RoleViewModel;
            RoleViewModels.Remove(roleVM);
            //Remove the role inside the roleVM
            _db.Remove(roleVM.Role);
        }

        public void RemoveEmployeeRole(object sender, EventArgs e)
        {
            var empVM = sender as EmployeeViewModel;
            var roleToRemove = empVM.SelectedRole;
            empVM.Employee.Roles.Remove((Role)roleToRemove);
        }

        private async void Save()
        {
            _db.SaveChanges();
            Message = $"Saved {DisplayCurrentDate}";
            await Task.Delay(5000);
            Message = "";
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
                StartDate = new DateTime(year: DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day),
                BirthDate = new DateTime(2000, 1, 1)
            };

            _db.Add(newEmployee);
            // Create the view model.
            CreateEmployeeViewModel(newEmployee);
        }

        //should return task and name assignroleasync if is async
        private void AssignRole(object sender, EventArgs e)
        {
            if (SelectedRole is null)
            {
                MessageBuilder("Select a role");
                return;
            }

            var empVM = sender as EmployeeViewModel;
            if (empVM.Employee.Roles.Contains(SelectedRole.Role))
            {
                MessageBuilder($"{empVM.Employee.Name} already has this role");
                return;
            }
            empVM.Employee.Roles.Add((Role)SelectedRole.Role);
        }

        private void AddRole()
        {
            var newRole = new Role()
            {
                Name = "Name",
                BaseSalary = 0M
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

            //Display the first role of an employee
            if (newEmployee.Roles.Count > 0)
            {
                viewModelToAdd.SelectedRole = newEmployee.Roles[0];
            }
            viewModelToAdd.EmployeeFired += FireEmployee;
            viewModelToAdd.RoleAssigned += AssignRole;
            viewModelToAdd.RoleRemoved += RemoveEmployeeRole;
            // Add the view model to the list.
            EmployeeViewModels.Add(viewModelToAdd);
        }

        private void FireEmployee(object sender, EventArgs e)
        {
            var empVM = sender as EmployeeViewModel;
            EmployeeViewModels.Remove(empVM);

            _db.Remove(empVM.Employee);
        }

        private IEnumerable<IEmployee> GetEmployees()
        {
            // Give me the employee of each view model.
            return EmployeeViewModels
                .Select(x => x.Employee);
        }

        private async void MessageBuilder(string msg)
        {
            Message = msg;
            await Task.Delay(5000);
            Message = "";
        }

        #endregion Methods
    }
}