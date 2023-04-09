using BusinessEntities;
using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IOrderItemLogic
    {
        List<OrderItem> GetAllOrderItems();
        void AddOrderItem(OrderItem OrderItem);
        OrderItem GetOrderItem(int OrderItemId);
        bool UpdateOrderItem(OrderItem OrderItem);
        bool DeleteOrderItem(int OrderItemId);
        List<OrderItem> AddOrderItems(int OrderId, int[] ItemId, int[] Quantity, decimal[] Price);
        List<OrderItemView> GetOrderViewItems(int OrderId);
    }
}
