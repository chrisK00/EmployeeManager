namespace EmployeeManager.Models.Developers
{
    public class FrontEndDev : IDeveloper
    {
        public int Id { get; set; }
        public ProgrammingFramework Framework { get; set; } = ProgrammingFramework.Angular;
        public ProgrammingLanguage Language { get; set; } = ProgrammingLanguage.JavaScript;
        public string Name { get; set; }
        public TypeOfEmployee TypeOfEmployee { get; set; } = TypeOfEmployee.Developer;
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
    }
}