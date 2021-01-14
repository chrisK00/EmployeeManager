using EmployeeManager.DataRepository.Employees;
using EmployeeManager.DataRepository.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace EmployeeManager.UI.ViewModels
{
    public class EmployeeViewModel : BindableBase, IEmployeeViewModel
    {
        #region Fields
        private IEmployee _employee;
        #endregion Fields

        #region Delegates
        public event EventHandler EmployeeFired;
        #endregion Delegates

        #region Properties
        public string DisplaySalary { get => $"{Employee.Salary:c}"; }
        public List<string> DisplayRoles { get => Employee.Roles.Select(x => x.Name).ToList(); }

        public IEmployee Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }
        #endregion Properties

        #region Commands
        public ICommand FireEmployeeCommand { get; }
        #endregion Commands

        #region Constructor

        public EmployeeViewModel()
        {
            FireEmployeeCommand = new DelegateCommand(FireEmployee);
        }
        #endregion Constructor

        #region Methods
        private void FireEmployee()
        {
            EmployeeFired?.Invoke(this, EventArgs.Empty);
        }
        #endregion Methods
    }
}