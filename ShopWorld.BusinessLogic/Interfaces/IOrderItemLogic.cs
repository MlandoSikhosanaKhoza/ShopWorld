using ShopWorld.Shared.Models;
using ShopWorld.Shared;

namespace ShopWorld.BusinessLogic
{
    public interface IOrderItemLogic
    {
        IEnumerable<OrderItemModel> GetAllOrderItems();
        OrderItemModel AddOrderItem(OrderItemModel OrderItem);
        OrderItemModel GetOrderItem(int OrderItemId);
        bool UpdateOrderItem(OrderItemModel OrderItem);
        bool DeleteOrderItem(int OrderItemId);
        IEnumerable<OrderItemModel> AddOrderItems(int OrderId, int[] ItemId, int[] Quantity);
        IEnumerable<OrderItemResult> GetOrderViewItems(int OrderId);
    }
}
