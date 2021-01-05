namespace EmployeeManager.Models.Developers
{
    internal interface IDeveloper : IEmployee
    {
        ProgrammingFramework Framework { get; set; }
        ProgrammingLanguage Language { get; set; }
    }
}