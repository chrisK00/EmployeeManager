namespace EmployeeManager.DataRepository.Developers
{
    public interface IDeveloper : IEmployee
    {
        ProgrammingFramework Framework { get; set; }
        ProgrammingLanguage Language { get; set; }
    }
}