using System;
using System.Collections.Generic;

namespace EmployeeManager.DataRepository.Employees
{
    public interface IEmployee
    {
        int Id { get; set; }
        string Name { get; set; }
        string PhoneNumber { get; set; }

        //calculated depending on startdate, birth date, role base salary
        decimal Salary { get; set; }

        string Email { get; set; }
        DateTime StartDate { get; set; }
        DateTime BirthDate { get; set; }
        List<Role> Roles { get; set; }
        List<Employee> ReportsTo { get; set; }
    }
}