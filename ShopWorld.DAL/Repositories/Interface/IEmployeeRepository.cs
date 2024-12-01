namespace ShopWorld.DAL
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee AddEmployee(Employee Employee);
        Employee GetEmployee(int EmployeeId);
        bool UpdateEmployee(Employee Employee);
        bool DeleteEmployee(int EmployeeId);
    }
}
