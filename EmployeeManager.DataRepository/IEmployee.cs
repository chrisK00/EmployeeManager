﻿namespace EmployeeManager.Models
{
    public interface IEmployee
    {
        string Name { get; set; }
        int Id { get; set; }
        TypeOfEmployee TypeOfEmployee { get; set; }
        string PhoneNumber { get; set; }
        decimal Salary { get; set; }
        string Email { get; set; }
    }
}