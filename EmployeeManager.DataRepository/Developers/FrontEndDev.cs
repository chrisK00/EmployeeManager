using System;

namespace EmployeeManager.Models.Developers
{
    public class FrontEndDev : IDeveloper
    {
        public int Id { get; set; }
        public ProgrammingFramework Framework { get; set; } = ProgrammingFramework.Angular;
        public ProgrammingLanguage Language { get; set; } = ProgrammingLanguage.JavaScript;
        public string Name { get; set; }
        private TypeOfEmployee _typeOfEmployee;
        public event EventHandler EmployeeTypeChanged;
        public TypeOfEmployee TypeOfEmployee { get => _typeOfEmployee; set { _typeOfEmployee = value; EmployeeTypeChanged?.Invoke(this, EventArgs.Empty); } }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public string DisplaySalary { get => $"{Salary:c}"; }
        public string Email { get; set; }
        public FrontEndDev()
        {
            TypeOfEmployee = TypeOfEmployee.Developer;
        }
    }
}