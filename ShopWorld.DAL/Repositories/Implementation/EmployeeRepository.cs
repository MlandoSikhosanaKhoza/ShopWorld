namespace ShopWorld.DAL
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeRepository(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            _employeeRepository = UnitOfWork.GetRepository<Employee>();
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.Get(e => !e.IsDeleted).ToList();
        }

        public Employee AddEmployee(Employee Employee)
        {
            Employee employee = _employeeRepository.Insert(Employee);
            _unitOfWork.SaveChanges();
            return employee;
        }

        public Employee GetEmployee(int EmployeeId)
        {
            return _employeeRepository.GetById(EmployeeId);
        }

        public bool UpdateEmployee(Employee Employee)
        {
            _employeeRepository.Update(Employee);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteEmployee(int EmployeeId)
        {
            Employee employee = GetEmployee(EmployeeId);
            employee.IsDeleted = true;
            UpdateEmployee(employee);
            _unitOfWork.SaveChanges();
            return true;
        }
    }
}
