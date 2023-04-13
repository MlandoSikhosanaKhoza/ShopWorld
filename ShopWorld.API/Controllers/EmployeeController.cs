using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared.Entities;

namespace ShopWorld.API.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeLogic _employeeLogic;
        public EmployeeController(IEmployeeLogic employeeLogic) { 
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        [Produces("application/json",Type=typeof(List<Employee>))]
        public IActionResult _GetAllEmployees()
        {
            return Ok(_employeeLogic.GetAllEmployees());
        }

        [HttpGet]
        [Produces("application/json",Type =typeof(Employee))]
        public IActionResult _GetEmployee(int EmployeeId)
        {
            return Ok(_employeeLogic.GetEmployee(EmployeeId));
        }

        [HttpPost]
        [Produces("application/json",Type=typeof(Employee))]
        public IActionResult _AddEmployee(Employee employee)
        {
            return Ok(_employeeLogic.AddEmployee(employee));
        }

        [HttpPost]
        [Produces("application/json",Type =typeof(bool))]
        public IActionResult _UpdateEmployee(Employee employee)
        {
            return Ok(_employeeLogic.UpdateEmployee(employee));
        }

        [HttpPost]
        [Produces("application/json",Type=typeof(bool))]
        public IActionResult _DeleteEmployee(int EmployeeId)
        {
            return Ok(_employeeLogic.DeleteEmployee(EmployeeId));
        }
    }
}
