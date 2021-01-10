using EmployeeManager.DataRepository;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace EmployeeManager.UI.ViewModels
{
    public class EmployeeViewModel : BindableBase, IEmployeeViewModel
    {
        #region Fields

        private IEmployee _employee;

        //List of all the enums
        public IList<TypeOfEmployee> EmployeeTypes { get; set; } = new List<TypeOfEmployee>();

        #endregion Fields

        #region Delegates
        public event EventHandler EmployeeFired;

        #endregion Delegates

        #region Properties

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

        #endregion Constructor

        #region Methods
        private void FireEmployee()
        {
            EmployeeFired?.Invoke(this, EventArgs.Empty);
        }

      
        #endregion Methods
    }
}