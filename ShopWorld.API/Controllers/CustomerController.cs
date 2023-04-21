using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared.Entities;

namespace ShopWorld.API.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Please note that through out the project I've used the authorize attribute on the actual method. Here I'm white listing but throughout the project I blacklisted my authorize my endpoints.
        /// </summary>
        private readonly ICustomerLogic _customerLogic;
        public CustomerController(ICustomerLogic customerLogic) {
            _customerLogic = customerLogic;
        }
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json", Type = typeof(Customer))]
        public IActionResult _GetCustomerById(int CustomerId)
        {
            return Ok(_customerLogic.GetCustomer(CustomerId));
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Produces("application/json",Type=typeof(List<Customer>))]
        public IActionResult _GetCustomerList()
        {
            return Ok(_customerLogic.GetAllCustomers());
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Produces("application/json", Type = typeof(List<Customer>))]
        public IActionResult _SearchCustomer(string Search)
        {
            return Ok(_customerLogic.SearchForCustomers(Search));
        }
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _MobileNumberExists(string Mobile)
        {
            return Ok(_customerLogic.MobileNumberExists(Mobile));
        }
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json",Type=typeof(Customer))]
        public IActionResult _AddCustomer(Customer CustomerObj) {
            return Ok(_customerLogic.AddCustomer(CustomerObj));
        }
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json", Type = typeof(Customer))]
        public IActionResult _GetCustomerByMobileNumber(string MobileNumber)
        {
            return Ok(_customerLogic.GetCustomerByMobileNumber(MobileNumber));
        }
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json", Type = typeof(Customer))]
        public IActionResult _ConfigureCustomer(Customer CustomerObj)
        {
            return Ok(_customerLogic.ConfigureCustomer(CustomerObj));
        }
    }
}
