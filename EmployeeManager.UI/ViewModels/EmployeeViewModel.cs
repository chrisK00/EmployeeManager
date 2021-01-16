using EmployeeManager.DataRepository.Employees;
using EmployeeManager.DataRepository.Logic;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace EmployeeManager.UI.ViewModels
{
    public class EmployeeViewModel : BindableBase, IEmployeeViewModel
    {
        #region Fields

        private IEmployee _employee;
        private IRole _selRole;

        #endregion Fields

        #region Delegates

        public event EventHandler EmployeeFired;

        public event EventHandler RoleAssigned;

        public event EventHandler RoleRemoved;

        #endregion Delegates

        #region Properties

        /// <summary>
        /// The selected item in the employee's combobox
        /// </summary>
        public IRole SelectedRole
        {
            get => _selRole; set
            {
                SetProperty(ref _selRole, value);
                Employee.Email = EmployeeLogic.CreateMail(this);
            }
        }

        public IEmployee Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }

        #endregion Properties

        #region Commands

        public ICommand FireEmployeeCommand { get; }
        public ICommand AssignRoleCommand { get; }
        public ICommand RemoveRoleCommand { get; }

        #endregion Commands

        #region Constructor

        public EmployeeViewModel()
        {
            FireEmployeeCommand = new DelegateCommand(FireEmployee);
            AssignRoleCommand = new DelegateCommand(AssignRole);
            RemoveRoleCommand = new DelegateCommand(RemoveRole);
        }

        #endregion Constructor

        #region Methods

        private void FireEmployee()
        {
            EmployeeFired?.Invoke(this, EventArgs.Empty);
        }

        private void AssignRole()
        {
            RoleAssigned?.Invoke(this, EventArgs.Empty);
        }

        private void RemoveRole()
        {
            RoleRemoved?.Invoke(this, EventArgs.Empty);
        }

        #endregion Methods
    }
}