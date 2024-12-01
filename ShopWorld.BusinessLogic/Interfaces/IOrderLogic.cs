using ShopWorld.Shared;
using ShopWorld.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWorld.BusinessLogic
{
    public interface IOrderLogic
    {
        IEnumerable<OrderModel> GetAllOrders();
        IEnumerable<OrderModel> GetOngoingOrdersForCustomer(int CustomerId);
        IEnumerable<OrderModel> GetCompleteOrdersForCustomer(int CustomerId);
        IEnumerable<OrderModel> GetOngoingOrders();
        IEnumerable<OrderModel> GetCompleteOrders();
        IEnumerable<CustomerOrderResult> GetNumberOfCustomerOrders();
        IEnumerable<CustomerOrderPriceResult> GetTotalSpentOfCustomerOrders();
        IEnumerable<CustomerOrderPriceResult> GetAverageSpentOfCustomerOrders();
        OrderModel AddOrder(OrderModel Order);
        OrderModel GetOrder(int OrderId);
        bool UpdateOrder(OrderModel Order);
        bool DeleteOrder(int OrderId);
    }
}
