
using ShopWorld.Shared;
using ShopWorld.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ShopWorld.Shared.Models;
using AutoMapper;
namespace ShopWorld.BusinessLogic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private IEmployeeRepository _employeeRepository;
        private IMapper _mapper;
        public EmployeeLogic(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees().Select(_mapper.Map<EmployeeModel>);
        }

        public EmployeeModel AddEmployee(EmployeeModel EmployeeModel)
        {
            Employee employee = _mapper.Map(EmployeeModel,new Employee());
            employee          = _employeeRepository.AddEmployee(employee); 
            return _mapper.Map<EmployeeModel>(employee);
        }

        public EmployeeModel GetEmployee(int EmployeeId)
        {
            Employee employee = _employeeRepository.GetEmployee(EmployeeId);
            return _mapper.Map<EmployeeModel>(employee);
        }

        public bool UpdateEmployee(EmployeeModel Employee)
        {
            Employee employee = _employeeRepository.GetEmployee(Employee.EmployeeId);
            _mapper.Map(Employee, employee);
            return _employeeRepository.UpdateEmployee(employee);
        }

        public bool DeleteEmployee(int EmployeeId)
        {
            return _employeeRepository.DeleteEmployee(EmployeeId);
        }
    }
}
