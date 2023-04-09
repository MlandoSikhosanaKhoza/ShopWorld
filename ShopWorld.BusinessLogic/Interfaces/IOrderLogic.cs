using ShopWorld.Shared;
using ShopWorld.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopWorld.BusinessLogic
{
    public interface IOrderLogic
    {
        List<Order> GetAllOrders();
        List<Order> GetOngoingOrdersForCustomer(int CustomerId);
        List<Order> GetCompleteOrdersForCustomer(int CustomerId);
        List<Order> GetOngoingOrders();
        List<Order> GetCompleteOrders();
        List<CustomerOrderResult> GetNumberOfCustomerOrders();
        List<CustomerOrderPriceResult> GetTotalSpentOfCustomerOrders();
        List<CustomerOrderPriceResult> GetAverageSpentOfCustomerOrders();
        Order AddOrder(Order Order);
        Order GetOrder(int OrderId);
        bool UpdateOrder(Order Order);
        bool DeleteOrder(int OrderId);
    }
}
