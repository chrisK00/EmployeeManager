namespace EmployeeManager.DataRepository.Employees
{
    public class Role : IRole
    {
        public decimal BaseSalary { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}