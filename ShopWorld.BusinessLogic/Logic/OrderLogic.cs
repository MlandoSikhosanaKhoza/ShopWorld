
using ShopWorld.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using ShopWorld.Shared.Models;
using ShopWorld.DAL;
using Microsoft.AspNetCore.Http;
namespace ShopWorld.BusinessLogic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public OrderLogic(IOrderRepository orderRepository, ICustomerRepository customerRepository, IMapper mapper,IHttpContextAccessor contextAccessor)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderModel> GetAllOrders()
        {
            return _orderRepository.GetAllOrders().Select(_mapper.Map<OrderModel>);
        }

        public IEnumerable<OrderModel> GetOngoingOrdersForCustomer(int CustomerId)
        {
            return _orderRepository.GetOngoingOrdersForCustomer(CustomerId).Select(_mapper.Map<OrderModel>);
        }

        public IEnumerable<OrderModel> GetCompleteOrdersForCustomer(int CustomerId)
        {
            return _orderRepository.GetCompleteOrdersForCustomer(CustomerId).Select(_mapper.Map<OrderModel>);
        }

        public IEnumerable<OrderModel> GetOngoingOrders()
        {
            return _orderRepository.GetOngoingOrders().Select(_mapper.Map<OrderModel>);
        }

        public IEnumerable<OrderModel> GetCompleteOrders()
        {
            return _orderRepository.GetCompleteOrders().Select(_mapper.Map<OrderModel>);
        }

        public IEnumerable<CustomerOrderResult> GetNumberOfCustomerOrders()
        {
            List<Customer> customers = _customerRepository.GetAllCustomers();
            return _orderRepository.GetNumberOfCustomerOrders(customers);
        }

        public IEnumerable<CustomerOrderPriceResult> GetTotalSpentOfCustomerOrders()
        {
            List<Customer> customers = _customerRepository.GetAllCustomers();
            return _orderRepository.GetTotalSpentOfCustomerOrders(customers);
        }

        public IEnumerable<CustomerOrderPriceResult> GetAverageSpentOfCustomerOrders()
        {
            List<Customer> customers = _customerRepository.GetAllCustomers();
            return _orderRepository.GetAverageSpentOfCustomerOrders(customers);
        }

        public OrderModel AddOrder(OrderModel Order)
        {
            Order order     = _mapper.Map<Order>(Order);
            order           = _orderRepository.AddOrder(order);
            return _mapper.Map<OrderModel>(order);
        }

        public OrderModel GetOrder(int OrderId)
        {
            Order order = _orderRepository.GetOrder(OrderId);
            return _mapper.Map<OrderModel>(order);
        }

        public bool UpdateOrder(OrderModel Order)
        {
            Order order = _orderRepository.GetOrder(Order.OrderId);
            _mapper.Map(Order, order);
            return _orderRepository.UpdateOrder(order);
        }

        public bool DeleteOrder(int OrderId)
        {
            return _orderRepository.DeleteOrder(OrderId);
        }
    }
}
