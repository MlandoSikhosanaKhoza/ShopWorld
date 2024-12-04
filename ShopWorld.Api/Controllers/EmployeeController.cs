using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared;
using ShopWorld.Shared.Models;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json",Type=typeof(List<EmployeeModel>))]
        public IActionResult _GetAllEmployees()
        {
            return Ok(_employeeLogic.GetAllEmployees());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json",Type = typeof(EmployeeModel))]
        public IActionResult _GetEmployee(int EmployeeId)
        {
            return Ok(_employeeLogic.GetEmployee(EmployeeId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json",Type = typeof(EmployeeModel))]
        public IActionResult _AddEmployee(EmployeeModel employee)
        {
            if(ModelState.IsValid)
            {
                return Ok(_employeeLogic.AddEmployee(employee));
            }
            return BadRequest(ModelState.Values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json",Type = typeof(bool))]
        public IActionResult _UpdateEmployee(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                return Ok(_employeeLogic.UpdateEmployee(employee));
            }
            return BadRequest(ModelState.Values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json",Type=typeof(bool))]
        public IActionResult _DeleteEmployee(int EmployeeId)
        {
            return Ok(_employeeLogic.DeleteEmployee(EmployeeId));
        }
    }
}
