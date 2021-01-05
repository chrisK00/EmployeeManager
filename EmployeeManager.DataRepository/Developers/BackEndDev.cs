namespace EmployeeManager.Models.Developers
{
    public class BackEndDev : IDeveloper
    {
        public int Id { get; set; }
        public ProgrammingFramework Framework { get; set; } = ProgrammingFramework.ASP;
        public ProgrammingLanguage Language { get; set; } = ProgrammingLanguage.Csharp;
        public string Name { get; set; }
        public TypeOfEmployee TypeOfEmployee { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public string DisplaySalary { get => $"{Salary:c}"; }
        public string Email { get; set; }
    }
}