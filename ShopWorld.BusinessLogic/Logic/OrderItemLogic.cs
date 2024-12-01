
using ShopWorld.Shared;
using ShopWorld.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Http;
using ShopWorld.Shared.Models;
using AutoMapper;
namespace ShopWorld.BusinessLogic
{
    public class OrderItemLogic : IOrderItemLogic
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        public OrderItemLogic(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IItemRepository itemRepository, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _orderRepository     = orderRepository;
            _orderItemRepository = orderItemRepository;
            _itemRepository      = itemRepository;
            _mapper              = mapper;
        }

        public IEnumerable<OrderItemModel> GetAllOrderItems()
        {
            return _orderItemRepository.GetAllOrderItems().Select(_mapper.Map<OrderItemModel>);
        }

        public IEnumerable<OrderItemResult> GetOrderViewItems(int OrderId)
        {
            return _orderItemRepository.GetOrderViewItems(OrderId);
        }

        public OrderItemModel AddOrderItem(OrderItemModel OrderItem)
        {
            OrderItem orderItem = _mapper.Map<OrderItem>(OrderItem);
            orderItem           = _orderItemRepository.AddOrderItem(orderItem);
            return _mapper.Map<OrderItemModel>(orderItem);
        }

        public OrderItemModel GetOrderItem(int OrderItemId)
        {
            OrderItem orderItem = _orderItemRepository.GetOrderItem(OrderItemId);
            return _mapper.Map<OrderItemModel>(orderItem);
        }

        public bool UpdateOrderItem(OrderItemModel OrderItem)
        {
            OrderItem orderItem = _orderItemRepository.GetOrderItem(OrderItem.OrderItemId);
            _mapper.Map(OrderItem, orderItem);
            return _orderItemRepository.UpdateOrderItem(orderItem);
        }

        public bool DeleteOrderItem(int OrderItemId)
        {
            return _orderItemRepository.DeleteOrderItem(OrderItemId);
        }

        public IEnumerable<OrderItemModel> AddOrderItems(int OrderId,int[] ItemId, int[] Quantity)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            for (int i = 0; i < ItemId.Length; i++)
            {
                OrderItem orderItem = _orderItemRepository.AddOrderItem(new OrderItem { OrderId = OrderId, ItemId = ItemId[i], Quantity = Quantity[i], Price = _itemRepository.GetItem(ItemId[i]).Price });
                orderItems.Add(orderItem);
            }
            Order order = _orderRepository.GetOrder(OrderId);
            order.Subtotal = orderItems.Sum(o => o.Quantity * o.Price);
            order.GrandTotal = order.Subtotal * 1.15m;
            _orderRepository.UpdateOrder(order);
            return orderItems.Select(_mapper.Map<OrderItemModel>);
        }
    }
}
