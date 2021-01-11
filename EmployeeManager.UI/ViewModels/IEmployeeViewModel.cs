using EmployeeManager.DataRepository.Employees;
using System;
using System.Windows.Input;

namespace EmployeeManager.UI.ViewModels
{
    public interface IEmployeeViewModel
    {
        event EventHandler EmployeeFired;
        string DisplaySalary { get; }
        IEmployee Employee { get; set; }
        ICommand FireEmployeeCommand { get; }
    }
}
