namespace EmployeeManager.DataRepository.Employees
{
    public interface IRole
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal BaseSalary { get; set; }
    }
}