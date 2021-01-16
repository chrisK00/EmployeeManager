using EmployeeManager.DataRepository.Employees;
using System;
using System.Windows.Input;

namespace EmployeeManager.UI.ViewModels
{
    public interface IEmployeeViewModel
    {
        event EventHandler EmployeeFired;

        event EventHandler RoleRemoved;

        IRole SelectedRole { get; set; }
        IEmployee Employee { get; set; }
        ICommand FireEmployeeCommand { get; }
        ICommand RemoveRoleCommand { get; }
    }
}