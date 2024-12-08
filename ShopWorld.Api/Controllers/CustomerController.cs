using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared;
using ShopWorld.Shared.Models;

namespace ShopWorld.Api.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Produces("application/json", Type = typeof(CustomerModel))]
        public IActionResult _GetCustomerById(int CustomerId)
        {
            return Ok(_customerLogic.GetCustomer(CustomerId));
        }

        [Authorize(Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json",Type=typeof(List<CustomerModel>))]
        public IActionResult _GetCustomerList()
        {
            return Ok(_customerLogic.GetAllCustomers());
        }

        [Authorize(Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(List<CustomerModel>))]
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
        [Produces("application/json",Type = typeof(CustomerModel))]
        public IActionResult _AddCustomer(CustomerModel CustomerObj) 
        {
            if (ModelState.IsValid)
            {
                return Ok(_customerLogic.AddCustomer(CustomerObj));
            }
            return BadRequest(ModelState.PrintError());
        }

        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json", Type = typeof(CustomerModel))]
        public IActionResult _GetCustomerByMobileNumber(string MobileNumber)
        {
            return Ok(_customerLogic.GetCustomerByMobileNumber(MobileNumber));
        }

        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json", Type = typeof(CustomerModel))]
        public IActionResult _ConfigureCustomer(CustomerModel CustomerObj)
        {
            if (ModelState.IsValid)
            {
                return Ok(_customerLogic.ConfigureCustomer(CustomerObj));
            }
            return BadRequest(ModelState.PrintError());
        }
    }
}
