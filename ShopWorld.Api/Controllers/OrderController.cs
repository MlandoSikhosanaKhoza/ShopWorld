using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared;
using ShopWorld.Shared;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ShopWorld.Shared.Models;

namespace ShopWorld.Api.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderLogic _orderLogic;
        public OrderController(IOrderLogic orderLogic) {
            _orderLogic = orderLogic;
        }

        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json",Type =typeof(IEnumerable<OrderModel>))]
        public IActionResult _GetAllOrders()
        {
            return Ok(_orderLogic.GetAllOrders());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Produces("application/json",Type=typeof(IEnumerable<OrderModel>))]
        public IActionResult _GetOngoingOrdersForCustomer(int CustomerId)
        {
            return Ok(_orderLogic.GetOngoingOrdersForCustomer(CustomerId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Produces("application/json",Type=typeof(IEnumerable<OrderModel>))]
        public IActionResult _GetCompleteOrdersForCustomer(int CustomerId)
        {
            return Ok(_orderLogic.GetCompleteOrdersForCustomer(CustomerId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json",Type=typeof(IEnumerable<OrderModel>))]
        public IActionResult _GetOngoingOrders()
        {
            return Ok(_orderLogic.GetOngoingOrders());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json",Type=typeof(IEnumerable<OrderModel>))]
        public IActionResult _GetCompleteOrders()
        {
            return Ok(_orderLogic.GetCompleteOrders());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<CustomerOrderResult>))]
        public IActionResult _GetNumberOfCustomerOrders() { 
            return Ok(_orderLogic.GetNumberOfCustomerOrders());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<CustomerOrderPriceResult>))]
        public IActionResult _GetTotalSpentOfCustomerOrders() {
            return Ok(_orderLogic.GetTotalSpentOfCustomerOrders());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<CustomerOrderPriceResult>))]
        public IActionResult _GetAverageSpentOfCustomerOrders()
        {
            return Ok(_orderLogic.GetAverageSpentOfCustomerOrders());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Produces("application/json", Type = typeof(OrderModel))]
        public IActionResult _AddOrder(OrderModel Order) 
        {
            if (ModelState.IsValid)
            {
                return Ok(_orderLogic.AddOrder(Order));
            }
            return BadRequest(ModelState.PrintError());
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(OrderModel))]
        public IActionResult _GetOrder(int OrderId)
        {
            return Ok(_orderLogic.GetOrder(OrderId));
        }

        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _UpdateOrder(OrderModel Order) {
            return Ok(_orderLogic.UpdateOrder(Order));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _DeleteOrder(int OrderId) { 
            return Ok(_orderLogic.DeleteOrder(OrderId));
        }
    }
}
