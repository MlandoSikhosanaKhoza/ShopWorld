using ShopWorld.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWorld.BusinessLogic
{
    public interface IEmployeeLogic
    {
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee Employee);
        Employee GetEmployee(int EmployeeId);
        bool UpdateEmployee(Employee Employee);
        bool DeleteEmployee(int EmployeeId);
    }
}
