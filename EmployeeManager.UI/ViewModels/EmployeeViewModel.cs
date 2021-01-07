using EmployeeManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
namespace EmployeeManager.ViewModels
{
    public class EmployeeViewModel : BindableBase
    {
        #region Fields
        private IEmployee _employee;
        
     public IList<TypeOfEmployee> EmployeeTypes { get; set; } = new List<TypeOfEmployee>();
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
            PopulateEmployeeTypes();
            FireEmployeeCommand = new DelegateCommand(FireEmployee);
        }

        private void PopulateEmployeeTypes()
        {
            foreach (var item in Enum.GetValues(typeof(TypeOfEmployee)))
            {
                EmployeeTypes.Add((TypeOfEmployee)item);
            }
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
