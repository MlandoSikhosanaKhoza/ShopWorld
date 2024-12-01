using ShopWorld.Shared;

namespace ShopWorld.DAL
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        List<Order> GetOngoingOrdersForCustomer(int CustomerId);
        List<Order> GetCompleteOrdersForCustomer(int CustomerId);
        List<Order> GetOngoingOrders();
        List<Order> GetCompleteOrders();
        List<CustomerOrderResult> GetNumberOfCustomerOrders(List<Customer> Customers);
        List<CustomerOrderPriceResult> GetTotalSpentOfCustomerOrders(List<Customer> Customers);
        List<CustomerOrderPriceResult> GetAverageSpentOfCustomerOrders(List<Customer> Customers);
        Order AddOrder(Order Order);
        Order GetOrder(int OrderId);
        bool UpdateOrder(Order Order);
        bool DeleteOrder(int OrderId);

    }
}
