using ShopWorld.Shared;
using ShopWorld.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWorld.BusinessLogic
{
    public interface IEmployeeLogic
    {
        IEnumerable<EmployeeModel> GetAllEmployees();
        EmployeeModel AddEmployee(EmployeeModel Employee);
        EmployeeModel GetEmployee(int EmployeeId);
        bool UpdateEmployee(EmployeeModel Employee);
        bool DeleteEmployee(int EmployeeId);
    }
}
