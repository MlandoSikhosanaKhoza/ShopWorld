using ShopWorld.Shared.Entities;
using ShopWorld.Shared;
using ShopWorld.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ShopWorld.BusinessLogic
{
    public class EmployeeLogic:IEmployeeLogic
    {
        private GenericRepository<Employee> EmployeeRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public EmployeeLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            EmployeeRepository = UnitOfWork.GetRepository<Employee>();
        }

        public List<Employee> GetAllEmployees()
        {
            return EmployeeRepository.Get(e => !e.IsDeleted).ToList();
        }

        public Employee AddEmployee(Employee Employee)
        {
            Employee employee=EmployeeRepository.Insert(Employee);
            _unitOfWork.SaveChanges();
            return employee;
        }

        public Employee GetEmployee(int EmployeeId)
        {
            return EmployeeRepository.GetById(EmployeeId);
        }

        public bool UpdateEmployee(Employee Employee)
        {
            EmployeeRepository.Update(Employee);
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
