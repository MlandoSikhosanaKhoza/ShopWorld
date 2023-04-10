using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared.Entities;

namespace ShopWorld.API.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerLogic _customerLogic;
        public CustomerController(ICustomerLogic customerLogic) {
            _customerLogic = customerLogic;
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(Customer))]
        public IActionResult _GetCustomerById(int CustomerId)
        {
            return Ok(_customerLogic.GetCustomer(CustomerId));
        }

        [HttpGet]
        [Produces("application/json",Type=typeof(List<Customer>))]
        public IActionResult _GetCustomerList()
        {
            return Ok(_customerLogic.GetAllCustomers());
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(List<Customer>))]
        public IActionResult _SearchCustomer(string Search)
        {
            return Ok(_customerLogic.SearchForCustomers(Search));
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _MobileNumberExists(string Mobile)
        {
            return Ok(_customerLogic.MobileNumberExists(Mobile));
        }

        [HttpPost]
        [Produces("application/json",Type=typeof(Customer))]
        public IActionResult _AddCustomer(Customer CustomerObj) {
            return Ok(_customerLogic.AddCustomer(CustomerObj));
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(Customer))]
        public IActionResult _GetCustomerByMobileNumber(string MobileNumber)
        {
            return Ok(_customerLogic.GetCustomerByMobileNumber(MobileNumber));
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(Customer))]
        public IActionResult _ConfigureCustomer(Customer CustomerObj)
        {
            return Ok(_customerLogic.ConfigureCustomer(CustomerObj));
        }
    }
}
