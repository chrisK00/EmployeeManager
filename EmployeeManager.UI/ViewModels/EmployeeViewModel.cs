using EmployeeManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace EmployeeManager.ViewModels
{
    public class EmployeeViewModel : BindableBase
    {
        #region Fields
        private IEmployee _employee;
        #endregion

        #region Delegates
        public event EventHandler EmployeeFired;
        #endregion

        #region Properties
        public IEmployee Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }
        #endregion

        #region Commands
        public ICommand FireEmployeeCommand { get; }
        #endregion

        #region Constructor
        public EmployeeViewModel()
        {
            FireEmployeeCommand = new DelegateCommand(FireEmployee);
        }
        #endregion

        #region Methods
        private void FireEmployee()
        {
            EmployeeFired?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
