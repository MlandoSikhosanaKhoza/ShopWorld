using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopWorld.BusinessLogic;
using ShopWorld.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ShopWorld.Shared.Models;

namespace ShopWorld.Api.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemLogic _orderItemLogic;
        public OrderItemController(IOrderItemLogic orderItemLogic) { 
            _orderItemLogic = orderItemLogic;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(List<OrderItemModel>))]
        public IActionResult _GetAllOrderItems() {
            return Ok(_orderItemLogic.GetAllOrderItems());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json", Type = typeof(OrderItemModel))]
        public IActionResult _AddOrderItem(OrderItemModel OrderItem) {
            return Ok(_orderItemLogic.AddOrderItem(OrderItem));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpGet]
        [Produces("application/json", Type = typeof(OrderItemModel))]
        public IActionResult _GetOrderItem(int OrderItemId) { 
            return Ok(_orderItemLogic.GetOrderItem(OrderItemId));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _UpdateOrderItem(OrderItemModel OrderItem) {
            return Ok(_orderItemLogic.UpdateOrderItem(OrderItem));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Rights.Administrator)]
        [HttpPost]
        [Produces("application/json", Type = typeof(bool))]
        public IActionResult _DeleteOrderItem(int OrderItemId) {
            return Ok(_orderItemLogic.DeleteOrderItem(OrderItemId));
        }

        /// <summary>
        /// I intentionally left out the price because the user can then inject it into the API method
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="ItemId"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json",Type = typeof(List<OrderItemModel>))]
        public IActionResult _AddOrderItems(OrderItemInputModel Input)
        {
            return Ok(_orderItemLogic.AddOrderItems(Input.OrderId, Input.ItemId, Input.Quantity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(List<OrderItemResult>))]
        public IActionResult _GetOrderViewItems(int OrderId) {
            return Ok(_orderItemLogic.GetOrderViewItems(OrderId));
        }
    }
}
