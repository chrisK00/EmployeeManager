using EmployeeManager.DataRepository.Employees;
using System;
using Xunit;

namespace EmployeeManager.Tests
{
    public class ModelTest
    {
        [Fact]
        public void CreateEmployee()
        {
            var newEmployee = new Employee
            {
                Name = "Name",
                Email = "Email",
                PhoneNumber = "PhoneNumber",
                Salary = 0M,
                Roles = {
                     {new Role { Name = "Front-End Dev", BaseSalary = 5000M, Id=500 } }
                },
                StartDate = new DateTime(year: 2021, 1, 11),
                BirthDate = new DateTime(1996, 4, 20)
            };

            Assert.True(newEmployee != null);
            Assert.True(newEmployee.Roles.Count > 0);
        }
    }
}