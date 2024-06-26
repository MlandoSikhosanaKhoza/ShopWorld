﻿using ShopWorld.Shared.Entities;
using ShopWorld.Shared;
using ShopWorld.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ShopWorld.BusinessLogic
{
    public class OrderItemLogic:IOrderItemLogic
    {
        private GenericRepository<OrderItem> OrderItemRepository { get; set; }
        private GenericRepository<Order> OrderRepository { get; set; }
        private GenericRepository<Item> ItemRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public OrderItemLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            OrderItemRepository = UnitOfWork.GetRepository<OrderItem>();
            OrderRepository = UnitOfWork.GetRepository<Order>();
            ItemRepository= UnitOfWork.GetRepository<Item>();
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return OrderItemRepository.GetAll().ToList();
        }

        public List<OrderItemResult> GetOrderViewItems(int OrderId)
        {
            return OrderItemRepository.Get(oi=>oi.OrderId==OrderId, includeProperties: $"{nameof(Item)}").Select(oi=>new OrderItemResult { 
                OrderItemId=oi.OrderItemId,
                Description=oi.Item.Description,
                Quantity=oi.Quantity,
                Price=oi.Price 
            }).ToList();
        }

        public OrderItem AddOrderItem(OrderItem OrderItem)
        {
            OrderItem orderItemAdded=OrderItemRepository.Insert(OrderItem);
            _unitOfWork.SaveChanges();
            return orderItemAdded;
        }

        public OrderItem GetOrderItem(int OrderItemId)
        {
            return OrderItemRepository.GetById(OrderItemId);
        }

        public bool UpdateOrderItem(OrderItem OrderItem)
        {
            OrderItemRepository.Update(OrderItem);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteOrderItem(int OrderItemId)
        {
            OrderItemRepository.DeleteById(OrderItemId);
            _unitOfWork.SaveChanges();
            return true;
        }

        public List<OrderItem> AddOrderItems(int OrderId,int[] ItemId, int[] Quantity)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            for (int i = 0; i < ItemId.Length; i++)
            {
                OrderItem orderItem = OrderItemRepository.Insert(new OrderItem { OrderId = OrderId, ItemId = ItemId[i], Quantity = Quantity[i], Price = ItemRepository.GetById(ItemId[i]).Price });
                orderItems.Add(orderItem);
            }
            Order order = OrderRepository.GetById(OrderId);
            order.Subtotal = orderItems.Sum(o => o.Quantity * o.Price);
            order.GrandTotal = order.Subtotal * 1.15m;
            OrderRepository.Update(order);
            _unitOfWork.SaveChanges();
            return orderItems;
        }
    }
}
