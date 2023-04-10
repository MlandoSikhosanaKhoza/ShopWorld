using ShopWorld.Shared;
using ShopWorld.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWorld.BusinessLogic
{
    public interface IOrderItemLogic
    {
        List<OrderItem> GetAllOrderItems();
        OrderItem AddOrderItem(OrderItem OrderItem);
        OrderItem GetOrderItem(int OrderItemId);
        bool UpdateOrderItem(OrderItem OrderItem);
        bool DeleteOrderItem(int OrderItemId);
        List<OrderItem> AddOrderItems(int OrderId, int[] ItemId, int[] Quantity);
        List<OrderItemResult> GetOrderViewItems(int OrderId);
    }
}
