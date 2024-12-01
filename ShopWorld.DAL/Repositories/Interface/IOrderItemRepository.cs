using ShopWorld.Shared;

namespace ShopWorld.DAL
{
    public interface IOrderItemRepository
    {
        List<OrderItem> GetAllOrderItems();
        List<OrderItemResult> GetOrderViewItems(int OrderId);
        OrderItem AddOrderItem(OrderItem OrderItem);
        OrderItem GetOrderItem(int OrderItemId);
        bool UpdateOrderItem(OrderItem OrderItem);
        bool DeleteOrderItem(int OrderItemId);
        List<OrderItem> AddOrderItems(int OrderId, int[] ItemId, int[] Quantity, decimal[] Price);
    }
}
