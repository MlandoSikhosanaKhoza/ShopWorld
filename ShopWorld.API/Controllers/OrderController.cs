using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared.Entities;
using ShopWorld.Shared;

namespace ShopWorld.API.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderLogic _orderLogic;
        public OrderController(IOrderLogic orderLogic) {
            _orderLogic = orderLogic;
        }
        [HttpGet]
        [Produces("application/json",Type =typeof(List<Order>))]
        public IActionResult _GetAllOrders()
        {
            return Ok(_orderLogic.GetAllOrders());
        }
        [HttpGet]
        [Produces("application/json",Type=typeof(List<Order>))]
        public IActionResult _GetOngoingOrdersForCustomer(int CustomerId)
        {
            return Ok(_orderLogic.GetOngoingOrdersForCustomer(CustomerId));
        }

        [HttpGet]
        [Produces("application/json",Type=typeof(List<Order>))]
        public IActionResult _GetCompleteOrdersForCustomer(int CustomerId)
        {
            return Ok(_orderLogic.GetCompleteOrdersForCustomer(CustomerId));
        }

        [HttpGet]
        [Produces("application/json",Type=typeof(List<Order>))]
        public IActionResult _GetOngoingOrders()
        {
            return Ok(_orderLogic.GetOngoingOrders());
        }

        [HttpGet]
        [Produces("application/json",Type=typeof(List<Order>))]
        public IActionResult _GetCompleteOrders()
        {
            return Ok(_orderLogic.GetCompleteOrders());
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(List<CustomerOrderResult>))]
        public IActionResult _GetNumberOfCustomerOrders() { 
            return Ok(_orderLogic.GetNumberOfCustomerOrders());
        }
        [HttpGet]
        [Produces("application/json", Type = typeof(List<CustomerOrderPriceResult>))]
        public IActionResult _GetTotalSpentOfCustomerOrders() {
            return Ok(_orderLogic.GetTotalSpentOfCustomerOrders());
        }
        [HttpGet]
        [Produces("application/json", Type = typeof(List<CustomerOrderPriceResult>))]
        public IActionResult _GetAverageSpentOfCustomerOrders()
        {
            return Ok(_orderLogic.GetAverageSpentOfCustomerOrders());
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(Order))]
        public IActionResult _AddOrder(Order Order) { 
            return Ok(_orderLogic.AddOrder(Order));
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(Order))]
        public IActionResult _GetOrder(int OrderId)
        {
            return Ok(_orderLogic.GetOrder(OrderId));
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _UpdateOrder(Order Order) {
            return Ok(_orderLogic.UpdateOrder(Order));
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _DeleteOrder(int OrderId) { 
            return Ok(_orderLogic.DeleteOrder(OrderId));
        }
    }
}
